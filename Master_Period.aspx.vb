Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.ServiceProcess
Imports System.Text.RegularExpressions
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Net
Imports System.IO
Imports EASendMail
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System.Drawing


Partial Class Master_Period
    Inherits System.Web.UI.Page

    Dim ToEmail As String = ""
  
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions
    Public rcno As String
    '  Public count As Integer = 0

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtCalendarPeriod.Text = ""
        txtAccountingPeriod.Text = ""

        chkInvoiceAddLock.Checked = True
        chkReceiptAddLock.Checked = True
        chkJournalAddLock.Checked = True
        chkInvoiceEditLock.Checked = True
        chkReceiptEditLock.Checked = True
        chkJournalEditLock.Checked = True

        chkEInvSubmitLock.Checked = True
        chkEInvCancelLock.Checked = True

        chkCNAddLock.Checked = True
        chkCNEditLock.Checked = True

        txtGSTType.Text = ""
        txtGSTRate.Text = ""
        txtGSTRate.Text = "0.00"
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        chkInvoice.Checked = True
        chkReceipt.Checked = True

        chkAll.Checked = True
        txtLocationId.SelectedIndex = 0

        btnAutoEmail.Text = "DISABLED"
        btnAutoEmailSOA.Text = "DISABLED"

        chkAdditionalFile.Checked = True

    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray
        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black

        txtCalendarPeriod.Enabled = False
        txtAccountingPeriod.Enabled = False
        chkInvoiceAddLock.Enabled = False
        chkReceiptAddLock.Enabled = False
        chkJournalAddLock.Enabled = False
        chkInvoiceEditLock.Enabled = False
        chkReceiptEditLock.Enabled = False
        chkJournalEditLock.Enabled = False

        chkCNAddLock.Enabled = False
        chkCNEditLock.Enabled = False

        chkEInvSubmitLock.Enabled = False
        chkEInvCancelLock.Enabled = False

        chkAdditionalFile.Enabled = False

        txtGSTRate.Enabled = False
        txtGSTType.Enabled = False
        chkInvoice.Enabled = False
        chkReceipt.Enabled = False
        chkJournal.Enabled = False

        chkAll.Enabled = False

        txtLocationId.Enabled = False
        '  chkAutoEmail.Enabled = False
        btnAutoEmail.Enabled = False
        btnAutoEmailSOA.Enabled = False

        AccessControl()
    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        txtCalendarPeriod.Enabled = True
        txtAccountingPeriod.Enabled = True
        chkInvoiceAddLock.Enabled = True
        chkReceiptAddLock.Enabled = True
        chkJournalAddLock.Enabled = True
        chkInvoiceEditLock.Enabled = True
        chkReceiptEditLock.Enabled = True
        chkJournalEditLock.Enabled = True

        chkCNAddLock.Enabled = True
        chkCNEditLock.Enabled = True

        chkEInvSubmitLock.Enabled = True
        chkEInvCancelLock.Enabled = True

        chkAdditionalFile.Enabled = True

        txtGSTRate.Enabled = True
        txtGSTType.Enabled = True
        chkInvoice.Enabled = True
        chkReceipt.Enabled = True
        chkJournal.Enabled = True

        chkAll.Enabled = True
        txtLocationId.Enabled = True
        '    chkAutoEmail.Enabled = True
        btnAutoEmail.Enabled = True
        btnAutoEmailSOA.Enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            If txtMode.Text = "Edit" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
                Return
            End If
            'EnableControls()
            MakeMeNull()
            Dim editindex As Integer = GridView1.SelectedIndex
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            txtRcno.Text = rcno.ToString()

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim sql As String
            sql = "Select * FROM tblperiod "
            sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("CalendarPeriod").ToString <> "" Then : txtCalendarPeriod.Text = dt.Rows(0)("CalendarPeriod").ToString : End If
                If dt.Rows(0)("AccountingPeriod").ToString <> "" Then : txtAccountingPeriod.Text = dt.Rows(0)("AccountingPeriod").ToString : End If
                If dt.Rows(0)("Location").ToString <> "" Then : txtLocationId.Text = dt.Rows(0)("Location").ToString : End If
                If dt.Rows(0)("ARLOCK").ToString = "Y" Then
                    chkInvoiceAddLock.Checked = True
                Else
                    chkInvoiceAddLock.Checked = False
                End If

                If dt.Rows(0)("ARLOCKE").ToString = "Y" Then
                    chkInvoiceEditLock.Checked = True
                Else
                    chkInvoiceEditLock.Checked = False
                End If

                If dt.Rows(0)("CNLOCK").ToString = "Y" Then
                    chkCNAddLock.Checked = True
                Else
                    chkCNAddLock.Checked = False
                End If

                If dt.Rows(0)("CNLOCKE").ToString = "Y" Then
                    chkCNEditLock.Checked = True
                Else
                    chkCNEditLock.Checked = False
                End If



                If dt.Rows(0)("RVLOCK").ToString = "Y" Then
                    chkReceiptAddLock.Checked = True
                Else
                    chkReceiptAddLock.Checked = False
                End If

                If dt.Rows(0)("RVLOCKE").ToString = "Y" Then
                    chkReceiptEditLock.Checked = True
                Else
                    chkReceiptEditLock.Checked = False
                End If



                If dt.Rows(0)("JNLOCK").ToString = "Y" Then
                    chkJournalAddLock.Checked = True
                Else
                    chkJournalAddLock.Checked = False
                End If

                If dt.Rows(0)("JNLOCKE").ToString = "Y" Then
                    chkJournalEditLock.Checked = True
                Else
                    chkJournalEditLock.Checked = False
                End If

                If dt.Rows(0)("EINVSubmitLock").ToString = "Y" Then
                    chkEInvSubmitLock.Checked = True
                Else
                    chkEInvSubmitLock.Checked = False
                End If

                If dt.Rows(0)("EINVCancelLock").ToString = "Y" Then
                    chkEInvCancelLock.Checked = True
                Else
                    chkEInvCancelLock.Checked = False
                End If

                If dt.Rows(0)("AdditionalFile").ToString = "Y" Then
                    chkAdditionalFile.Checked = True
                Else
                    chkAdditionalFile.Checked = False
                End If
            End If


            'If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '    txtCalendarPeriod.Text = ""
            'Else
            '    txtCalendarPeriod.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
            'End If
            'If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            '    txtAccountingPeriod.Text = ""
            'Else
            '    txtAccountingPeriod.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
            'End If


            'If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(3).Text) = True Or (GridView1.SelectedRow.Cells(3).Text) = "&nbsp;" Then
            '    txtLocationId.SelectedIndex = 0
            'Else
            '    txtLocationId.Text = GridView1.SelectedRow.Cells(3).Text
            'End If

            'txtAccountingPeriod.Text = GridView1.SelectedRow.Cells(4).Text
            'If GridView1.SelectedRow.Cells(4).Text = True Then
            '    chkInvoiceAddLock.Checked = True
            'Else
            '    chkInvoiceAddLock.Checked = False
            'End If

            'If GridView1.SelectedRow.Cells(5).Text = "Y" Then
            '    chkInvoiceEditLock.Checked = True
            'Else
            '    chkInvoiceEditLock.Checked = False
            'End If


            'If GridView1.SelectedRow.Cells(6).Text = "Y" Then
            '    chkCNAddLock.Checked = True
            'Else
            '    chkCNAddLock.Checked = False
            'End If

            'If GridView1.SelectedRow.Cells(7).Text = "Y" Then
            '    chkCNEditLock.Checked = True
            'Else
            '    chkCNEditLock.Checked = False
            'End If


            'If GridView1.SelectedRow.Cells(8).Text = "Y" Then
            '    chkReceiptAddLock.Checked = True
            'Else
            '    chkReceiptAddLock.Checked = False
            'End If


            'If GridView1.SelectedRow.Cells(9).Text = "Y" Then
            '    chkReceiptEditLock.Checked = True
            'Else
            '    chkReceiptEditLock.Checked = False
            'End If


            'If GridView1.SelectedRow.Cells(10).Text = "Y" Then
            '    chkJournalAddLock.Checked = True
            'Else
            '    chkJournalAddLock.Checked = False
            'End If


            'If GridView1.SelectedRow.Cells(11).Text = "Y" Then
            '    chkJournalEditLock.Checked = True
            'Else
            '    chkJournalEditLock.Checked = False
            'End If


            If GridView1.SelectedRow.Cells(12).Text = "&nbsp;" Then
                txtGSTType.Text = ""
            Else
                txtGSTType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(12).Text).ToString
            End If

            If GridView1.SelectedRow.Cells(13).Text = "&nbsp;" Then
                txtGSTRate.Text = ""
            Else
                txtGSTRate.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(13).Text).ToString
            End If

            If GridView1.SelectedRow.Cells(14).Text = "Y" Then
                '  chkAutoEmail.Checked = True
                btnAutoEmail.Text = "ENABLED"
                btnAutoEmail.BackColor = Drawing.Color.Green

            Else
                ' chkAutoEmail.Checked = False
                btnAutoEmail.Text = "DISABLED"
                btnAutoEmail.BackColor = Drawing.Color.OrangeRed

            End If

            If GridView1.SelectedRow.Cells(15).Text = "Y" Then
                '  chkAutoEmail.Checked = True
                btnAutoEmailSOA.Text = "ENABLED"
                btnAutoEmailSOA.BackColor = Drawing.Color.Green

            Else
                ' chkAutoEmail.Checked = False
                btnAutoEmailSOA.Text = "DISABLED"
                btnAutoEmailSOA.BackColor = Drawing.Color.OrangeRed

            End If


            RetrieveSOAStartDate(txtCalendarPeriod.Text)

            RetrieveInvoiceEmailInfo(txtCalendarPeriod.Text)

            RetrieveDocInfo(txtCalendarPeriod.Text)

            'If GridView1.SelectedRow.Cells(13).Text = "N" Then
            '    chkInvoice.Checked = False
            'Else
            '    chkInvoice.Checked = True
            'End If


            'If GridView1.SelectedRow.Cells(14).Text = "N" Then
            '    chkReceipt.Checked = False
            'Else
            '    chkReceipt.Checked = True
            'End If

            'If GridView1.SelectedRow.Cells(15).Text = "N" Then
            '    chkJournal.Checked = False
            'Else
            '    chkJournal.Checked = True
            'End If



            'If txtLocationId.SelectedIndex = 0 Then
            '    txtLocationId.Text = txtLocationId.SelectedIndex = 0
            'Else
            '    Command.Parameters.AddWithValue("@Location", txtLocationId.Text)
            'End If
            txtMode.Text = "View"


            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black
            btnDelete.Enabled = True
            btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black

            If CheckIfExists() = True Then
                txtExists.Text = "True"
            Else
                txtExists.Text = "False"
            End If

            'EnableControls()

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub RetrieveSOAStartDate(CalendarPeriod As String)
        Dim str As String = ""
        Dim strEnd As String = ""
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim commandInterval As MySqlCommand = New MySqlCommand

        commandInterval.CommandType = CommandType.Text

        commandInterval.CommandText = "SELECT SOAScheduleDATE,SOAScheduleLastDate FROM tblservicerecordmastersetup where rcno=1"

        commandInterval.Connection = conn

        Dim drInterval As MySqlDataReader = commandInterval.ExecuteReader()
        Dim dtInterval As New DataTable
        dtInterval.Load(drInterval)


        If dtInterval.Rows.Count > 0 Then
            str = dtInterval.Rows(0)("SOAScheduleDate").ToString + "/" + CalendarPeriod.Substring(4, 2).ToString + "/" + CalendarPeriod.Substring(0, 4).ToString
            strEnd = dtInterval.Rows(0)("SOAScheduleLastDate").ToString + "/" + CalendarPeriod.Substring(4, 2).ToString + "/" + CalendarPeriod.Substring(0, 4).ToString
        Else
            str = ""
        End If
        dtInterval.Clear()
        dtInterval.Dispose()
        drInterval.Close()
        commandInterval.Dispose()

        If str <> "" Then
            lblSOA.Text = "SOA will be sent from " & System.Convert.ToDateTime(str).AddMonths(1).ToString("dd/MM/yyyy") & " to " & System.Convert.ToDateTime(strEnd).AddMonths(1).ToString("dd/MM/yyyy")
        End If
        conn.Close()
        ' Return str
    End Sub

    Private Sub RetrieveInvoiceEmailInfo(CalendarPeriod As String)
        Dim str As String = ""
        Dim strEnd As String = ""
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim commandInv As MySqlCommand = New MySqlCommand

        commandInv.CommandType = CommandType.Text

        commandInv.CommandText = "SELECT Count(*) as TotalCount FROM tblsales where glperiod='" + CalendarPeriod + "'"

        commandInv.Connection = conn

        Dim drInv As MySqlDataReader = commandInv.ExecuteReader()
        Dim dtInv As New DataTable
        dtInv.Load(drInv)


        If dtInv.Rows.Count > 0 Then
            str = dtInv.Rows(0)("TotalCount").ToString
        Else
            str = ""
        End If
        dtInv.Clear()
        dtInv.Dispose()
        drInv.Close()
        commandInv.Dispose()

        Dim commandInv1 As MySqlCommand = New MySqlCommand

        commandInv1.CommandType = CommandType.Text

        commandInv1.CommandText = "SELECT Count(*) as TotalCount FROM tblsales where glperiod='" + CalendarPeriod + "' and EmailSentStatus='Y'"

        commandInv1.Connection = conn

        Dim drInv1 As MySqlDataReader = commandInv1.ExecuteReader()
        Dim dtInv1 As New DataTable
        dtInv1.Load(drInv1)


        If dtInv1.Rows.Count > 0 Then
            strEnd = dtInv1.Rows(0)("TotalCount").ToString
        Else
            strEnd = ""
        End If
        dtInv1.Clear()
        dtInv1.Dispose()
        drInv1.Close()
        commandInv1.Dispose()

        lblInvoice.Text = strEnd + " out of " + str + " Invoices sent"

        conn.Close()
        ' Return str
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()

        DisableControls()

        txtMode.Text = "New"
        lblMessage.Text = "ACTION: ADD RECORD"
        txtCalendarPeriod.Focus()

        txtGSTType.Text = txtDefaulTaxType.Text
        txtGSTRate.Text = txtDefaultTaxRate.Text

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '  count = 0

            MakeMeNull()
            EnableControls()

            'btnSave.Enabled = False
            'btnCancel.Enabled = False
            'btnADD.Enabled = False
            'btnEdit.Enabled = False
            'btnADD.ForeColor = System.Drawing.Color.Gray

            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            'txtCreatedBy.Text = Session("userid")

            'FindLocation()

            Dim query As String
            query = "Select LocationID from tbllocation"
            PopulateDropDownList(query, "LocationID", "LocationID", txtLocationId)
            'txtLocationId.Attributes.Add("disabled", "true")

            ''''
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT ShowInvoiceOnScreenLoad, InvoiceRecordMaxRec,DisplayRecordsLocationWise, PostInvoice, InvoiceOnlyEditableByCreator, DefaultTaxCode FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceRecordMaxRec")
            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            'txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostInvoice").ToString
            'txtOnlyEditableByCreator.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceOnlyEditableByCreator").ToString
            txtDefaulTaxType.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString

            ''''''''''''''''''''''''''''''''''''''''''

            Dim sql As String
            sql = ""


            sql = "Select TaxRatePct from tbltaxtype where TaxType = '" & txtDefaulTaxType.Text & "'"

            ''
            Dim commandTaxRatePct As MySqlCommand = New MySqlCommand
            commandTaxRatePct.CommandType = CommandType.Text
            commandTaxRatePct.CommandText = sql
            commandTaxRatePct.Connection = conn

            Dim drTaxRatePct As MySqlDataReader = commandTaxRatePct.ExecuteReader()
            Dim dtTaxRatePct As New DataTable
            dtTaxRatePct.Load(drTaxRatePct)

            txtDefaultTaxRate.Text = dtTaxRatePct.Rows(0)("TaxRatePct").ToString

            ''


            If txtDisplayRecordsLocationwise.Text = "Y" Then
                txtLocationId.Visible = True
                lblBranch.Visible = True
            Else
                txtLocationId.Visible = False
                lblBranch.Visible = False
            End If

            conn.Close()
            commandServiceRecordMasterSetup.Dispose()
            drServiceRecordMasterSetup.Close()
            dtServiceRecordMasterSetup.Dispose()

            ''''
        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")
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

    Public Sub FindLocation()
        Dim IsLock As String
        IsLock = ""

        Dim connLocation As MySqlConnection = New MySqlConnection()

        connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connLocation.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT LocationID, Location FROM tblstaff where StaffId='" & txtCreatedBy.Text.ToUpper & "'"
        command1.Connection = connLocation

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            txtLocationId.Text = dt.Rows(0)("LocationID").ToString
        End If

        connLocation.Close()
        connLocation.Dispose()
        command1.Dispose()
        dt.Dispose()
    End Sub

    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT x0128,  x0128Add, x0128Edit, x0128Delete, x0128Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0173,  x0173Add, x0173Edit, x0173Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0173")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0173"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0173")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0173Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0173Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0173Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0173Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0173Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0173Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0173Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0173Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0173Delete").ToString()
                            End If
                        End If
                    Else
                        Me.btnEdit.Enabled = False
                        Me.btnDelete.Enabled = False
                    End If

                    If btnADD.Enabled = True Then
                        btnADD.ForeColor = System.Drawing.Color.Black
                    Else
                        btnADD.ForeColor = System.Drawing.Color.Gray
                    End If


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


                    If btnPrint.Enabled = True Then
                        btnPrint.ForeColor = System.Drawing.Color.Black
                    Else
                        btnPrint.ForeColor = System.Drawing.Color.Gray
                    End If
                End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "0")

        If txtCalendarPeriod.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "CALENDAR PERIOD CANNOT BE BLANK"
            Return

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim conn1 As MySqlConnection = New MySqlConnection()

        conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn1.State = ConnectionState.Open Then
            conn1.Close()
            conn1.Dispose()
        End If
        conn1.Open()

        Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
        commandServiceRecordMasterSetup.CommandType = CommandType.Text
        commandServiceRecordMasterSetup.CommandText = "SELECT ShowInvoiceOnScreenLoad, InvoiceRecordMaxRec,DisplayRecordsLocationWise, PostInvoice, InvoiceOnlyEditableByCreator FROM tblservicerecordmastersetup"
        commandServiceRecordMasterSetup.Connection = conn1

        Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
        Dim dtServiceRecordMasterSetup As New DataTable
        dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

        'txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString

        If dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString = "Y" Then
            If txtLocationId.SelectedIndex = 0 Then
                lblAlert.Text = "PLEASE SELECT LOCATION"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub

            End If
        End If

        conn1.Close()
        conn1.Dispose()
        ''''''''''''''''''''''''''''''''''''''''''
        'If txtDisplayRecordsLocationwise.Text = "Y" Then
        '    lblBranch1.Visible = True
        '    txtLocation.Visible = True
        'Else
        '    lblBranch1.Visible = False
        '    txtLocation.Visible = False
        'End If


        'If IsNumeric(txtGSTRate.Text) = False Then
        '    '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
        '    lblAlert.Text = "TAX RATE SHOULD BE NUMERIC ONLY"
        '    Return

        'End If

        InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "01")


        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                If txtLocationId.SelectedIndex = 0 Then
                    command1.CommandText = "SELECT * FROM tblPeriod where CalendarPeriod=@CalendarPeriod"
                Else
                    command1.CommandText = "SELECT * FROM tblPeriod where CalendarPeriod=@CalendarPeriod and Location='" & txtLocationId.Text & "'"
                End If

                command1.Parameters.AddWithValue("@CalendarPeriod", txtCalendarPeriod.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtCalendarPeriod.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblPeriod(CalendarPeriod,AccountingPeriod, ARLock,  RVLock, JNLock,ARLockE,  RVLockE, JNLockE, GSTType, GSTRate,CNLock, CNLockE,Location,AUTOEMAIL,AUTOEMAILSOA,AdditionalFile,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,EINVSubmitLock,EINVCancelLock)"
                    qry = qry + "VALUES(@CalendarPeriod, @AccountingPeriod, @ARLock,  @RVLock, @JNLock,@ARLockE, @RVLockE, @JNLockE, @GSTType, @GSTRate, @CNLock, @CNLockE,@Location,@AUTOEMAIL,@AUTOEMAILSOA,@AdditionalFile,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@EINVSubmitLock,@EINVCancelLock);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@CalendarPeriod", txtCalendarPeriod.Text.ToUpper)
                        command.Parameters.AddWithValue("@AccountingPeriod", txtAccountingPeriod.Text.ToUpper)

                        If chkInvoiceAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@ARLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@ARLock", "N")
                        End If

                        If chkReceiptAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@RVLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@RVLock", "N")
                        End If

                        If chkJournalAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@JNLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@JNLock", "N")
                        End If

                        If chkInvoiceEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@ARLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@ARLockE", "N")
                        End If

                        If chkReceiptEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@RVLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@RVLockE", "N")
                        End If

                        If chkJournalEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@JNLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@JNLockE", "N")
                        End If


                        If chkCNAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@CNLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@CNLock", "N")
                        End If

                        If chkCNEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@CNLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@CNLockE", "N")
                        End If

                        If chkEInvSubmitLock.Checked = True Then
                            command.Parameters.AddWithValue("@EINVSubmitLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@EINVSubmitLock", "N")
                        End If

                        If chkEInvCancelLock.Checked = True Then
                            command.Parameters.AddWithValue("@EINVCancelLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@EINVCancelLock", "N")
                        End If

                        command.Parameters.AddWithValue("@GSTType", txtGSTType.Text.ToUpper)

                        If String.IsNullOrEmpty(txtGSTRate.Text) = True Then
                            command.Parameters.AddWithValue("@GSTRate", 0.0)
                        Else
                            command.Parameters.AddWithValue("@GSTRate", txtGSTRate.Text)
                        End If

                        'command.Parameters.AddWithValue("@GSTRate", txtGSTRate.Text)

                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text.ToUpper)
                        End If
                        If btnAutoEmail.Text = "ENABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAIL", "Y")
                        ElseIf btnAutoEmail.Text = "DISABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAIL", "N")
                        End If
                        If btnAutoEmailSOA.Text = "ENABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAILSOA", "Y")
                        ElseIf btnAutoEmailSOA.Text = "DISABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAILSOA", "N")
                        End If

                        If chkAdditionalFile.Checked = True Then
                            command.Parameters.AddWithValue("@AdditionalFile", "Y")
                        Else
                            command.Parameters.AddWithValue("@AdditionalFile", "N")
                        End If

                        'command.Parameters.AddWithValue("@ARIN", chkInvoice.Checked)
                        'command.Parameters.AddWithValue("@RECV", chkReceipt.Checked)

                        'command.Parameters.AddWithValue("@ARIN", "b'1'")
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        'command.Parameters.AddWithValue("@TaxType", txtCalendarPeriod.Text)
                        'command.Parameters.AddWithValue("@TaxDesc", txtTaxDesc.Text)
                        'command.Parameters.AddWithValue("@TaxRatePct", txtGSTRate.Text)
                        'command.Parameters.AddWithValue("@ARIN", chkInvoice.Checked)
                        'command.Parameters.AddWithValue("@RECV", chkReceipt.Checked)
                        ''command.Parameters.AddWithValue("@ARIN", "b'1'")
                        'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        'command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    ' MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If

            InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "1")


            ''''
            If chkInvoiceEditLock.Checked = True Or chkReceiptEditLock.Checked = True Or chkCNEditLock.Checked = True Or chkJournalEditLock.Checked = True Then
                Dim connOpenTransactions As MySqlConnection = New MySqlConnection()

                connOpenTransactions.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connOpenTransactions.Open()

                Dim commandOpenTransactions As MySqlCommand = New MySqlCommand

                commandOpenTransactions.CommandType = CommandType.Text

                commandOpenTransactions.CommandText = "SELECT * FROM vwopentransactions where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' limit 1"
                commandOpenTransactions.Connection = connOpenTransactions

                Dim drOpenTransactions As MySqlDataReader = commandOpenTransactions.ExecuteReader()
                Dim dtOpenTransactions As New DataTable
                dtOpenTransactions.Load(drOpenTransactions)

                If dtOpenTransactions.Rows.Count > 0 Then
                    connOpenTransactions.Close()
                    connOpenTransactions.Dispose()

                    sqlDSViewOpenTransactions.SelectCommand = "Select * from vwopentransactions where GLPeriod='" & txtAccountingPeriod.Text & "' order by srlno"
                    sqlDSViewOpenTransactions.DataBind()

                    grdViewEditHistory.DataSourceID = "sqlDSViewOpenTransactions"
                    grdViewEditHistory.DataBind()


                    mdlViewMsgOpenTransactions.Show()
                    Exit Sub
                End If

            End If

            InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "2")


            ''''

            Try


                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If txtLocationId.SelectedIndex = 0 Then
                    command2.CommandText = "SELECT * FROM tblPeriod where CalendarPeriod=@CalendarPeriod and rcno<>" & Convert.ToInt32(txtRcno.Text)
                Else
                    command2.CommandText = "SELECT * FROM tblPeriod where CalendarPeriod=@CalendarPeriod and rcno<>" & Convert.ToInt32(txtRcno.Text) & " and Location='" & txtLocationId.Text & "'"
                End If

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()
                command2.Parameters.AddWithValue("@CalendarPeriod", txtCalendarPeriod.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Industry already exists!!!", "str")
                    lblAlert.Text = "PERIOD ALREADY EXISTS"
                    txtCalendarPeriod.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblPeriod where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)
                    InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "3")


                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        'If txtExists.Text = "True" Then
                        '    qry = "update tblPeriod set TaxDesc=@TaxDesc, TaxRatePct=@TaxRatePct, ARIN =@ARIN, RECV=@RECV,  LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'Else
                        qry = "update tblPeriod set CalendarPeriod =@CalendarPeriod,AccountingPeriod=@AccountingPeriod, ARLock=@ARLock,  RVLock=@RVLock, JNLock=@JNLock,ARLockE=@ARLockE,  RVLockE=@RVLockE, JNLockE=@JNLockE, GSTType=@GSTType, GSTRate=@GSTRate, CNLock=@CNLock, CNLockE=@CNLockE, Location =@Location,AUTOEMAIL=@AUTOEMAIL,AUTOEMAILSOA=@AUTOEMAILSOA,AdditionalFile=@AdditionalFile,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn,EINVSubmitLock=@EINVSubmitLock,EINVCancelLock=@EINVCancelLock where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        '    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@CalendarPeriod", txtCalendarPeriod.Text.ToUpper)
                        command.Parameters.AddWithValue("@AccountingPeriod", txtAccountingPeriod.Text.ToUpper)

                        If chkInvoiceAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@ARLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@ARLock", "N")
                        End If

                        If chkReceiptAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@RVLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@RVLock", "N")
                        End If

                        If chkJournalAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@JNLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@JNLock", "N")
                        End If

                        If chkInvoiceEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@ARLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@ARLockE", "N")
                        End If

                        If chkReceiptEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@RVLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@RVLockE", "N")
                        End If

                        If chkJournalEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@JNLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@JNLockE", "N")
                        End If


                        If chkCNAddLock.Checked = True Then
                            command.Parameters.AddWithValue("@CNLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@CNLock", "N")
                        End If

                        If chkCNEditLock.Checked = True Then
                            command.Parameters.AddWithValue("@CNLockE", "Y")
                        Else
                            command.Parameters.AddWithValue("@CNLockE", "N")
                        End If

                        If chkEInvSubmitLock.Checked = True Then
                            command.Parameters.AddWithValue("@EINVSubmitLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@EINVSubmitLock", "N")
                        End If

                        If chkEInvCancelLock.Checked = True Then
                            command.Parameters.AddWithValue("@EINVCancelLock", "Y")
                        Else
                            command.Parameters.AddWithValue("@EINVCancelLock", "N")
                        End If

                        command.Parameters.AddWithValue("@GSTType", txtGSTType.Text.ToUpper)
                        If String.IsNullOrEmpty(txtGSTRate.Text) = True Then
                            command.Parameters.AddWithValue("@GSTRate", 0.0)
                        Else
                            command.Parameters.AddWithValue("@GSTRate", txtGSTRate.Text)
                        End If


                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                        End If
                        If btnAutoEmail.Text = "ENABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAIL", "Y")
                        ElseIf btnAutoEmail.Text = "DISABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAIL", "N")
                        End If
                        If btnAutoEmailSOA.Text = "ENABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAILSOA", "Y")
                        ElseIf btnAutoEmailSOA.Text = "DISABLED" Then
                            command.Parameters.AddWithValue("@AUTOEMAILSOA", "N")
                        End If

                        If chkAdditionalFile.Checked = True Then
                            command.Parameters.AddWithValue("@AdditionalFile", "Y")
                        Else
                            command.Parameters.AddWithValue("@AdditionalFile", "N")
                        End If

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        '  ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        'command.Parameters.AddWithValue("@TaxType", txtCalendarPeriod.Text)
                        'command.Parameters.AddWithValue("@TaxDesc", txtTaxDesc.Text)
                        'command.Parameters.AddWithValue("@TaxRatePct", txtGSTRate.Text)
                        'command.Parameters.AddWithValue("@ARIN", chkInvoice.Checked)
                        'command.Parameters.AddWithValue("@RECV", chkReceipt.Checked)
                        'command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        'command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        '    End If

                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO TAX CODE CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If


                    End If
                End If
                InsertIntoTblWebEventLog("Period", txtCalendarPeriod.Text, "4")


                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERIOD", txtCalendarPeriod.Text, "ADD", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PERIOD", txtCalendarPeriod.Text, "EDIT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        '  MakeMeNull()
        If CheckIfExists() = True Then
            txtExists.Text = "True"
        Else
            txtExists.Text = "False"
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

        txtCalendarPeriod.Enabled = False
        txtAccountingPeriod.Enabled = False

        'If txtExists.Text = "True" Then
        '    txtCalendarPeriod.Enabled = False
        'Else
        '    txtCalendarPeriod.Enabled = True
        'End If

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()

    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        lblMessage.Text = "ACTION: DELETE RECORD"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then



            '   MessageBox.Message.Confirm(Page, "Do you want to delete the selected record?", "str", vbYesNo)
            If txtExists.Text = "True" Then
                '  MessageBox.Message.Alert(Page, "Record is in use, cannot be deleted!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblperiod where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblperiod where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TAXRATE", txtCalendarPeriod.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            GridView1.DataSourceID = "SqlDataSource1"
            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterTaxRate.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tbltaxtype where taxtype=@taxtype"
        command1.Parameters.AddWithValue("@taxtype", txtCalendarPeriod.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If


        conn.Close()

        Return False
    End Function

    Protected Sub btnEmailInvoice_Click(sender As Object, e As EventArgs) Handles btnEmailInvoice.Click
        lblMessage.Text = ""
        lblAlert.Text = ""

        If txtRcno.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return

        End If
        If txtMode.Text = "" Or txtMode.Text = "View" Then
        Else

            lblAlert.Text = "CANNOT EMAIL INVOICE IN ADD OR EDIT MODE"
            Return

        End If
        If txtCalendarPeriod.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "CALENDAR PERIOD CANNOT BE EMPTY"
            Return

        End If

        If chkInvoiceAddLock.Checked = False Then
            lblAlert.Text = "AR LOCK SHOULD BE CHECKED"
            Return

        End If

        lblMessage.Text = "ACTION: EMAILING INVOICE"
        lblquery.text = "Do you want to Email Invoices for " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(txtCalendarPeriod.Text.Substring(4, 2)) + " " + txtCalendarPeriod.Text.Substring(0, 4) + "?"

        mdlpopupmsg.show()



    End Sub


    Public Function GetInvoiceNumber(connPeriod As MySqlConnection) As String
        Dim InvoiceNumber As String = ""
        Dim InvoiceFormat As String = ""
        Dim EmailAddress As String = ""
        Dim CustName As String = ""
        Dim SalesDate As String = ""
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        Dim DueDate As String = ""

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT InvoiceNumber,AccountID,ContactType,custname,SalesDate,DueDate,EmailSentStatus,EmailSentDate,EmailSentBy FROM tblsales where GLPeriod='" & txtCalendarPeriod.Text & "' and PostStatus='P' AND EmailSentStatus='N' and BalanceBase>0 and doctype='ARIN';"

        command1.Connection = connPeriod

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)


        If dt1.Rows.Count > 0 Then
            For i As Int32 = 0 To dt1.Rows.Count - 1
                'For i As Int32 = 0 To 10


                InvoiceNumber = ""
                InvoiceFormat = ""

                InvoiceNumber = dt1.Rows(i)("InvoiceNumber").ToString

                CustName = dt1.Rows(i)("CustName").ToString

                If dt1.Rows(i)("SalesDate").ToString = DBNull.Value.ToString Then
                Else
                    SalesDate = Convert.ToDateTime(dt1.Rows(i)("SalesDate")).ToString("dd/MM/yyyy")
                End If

                If dt1.Rows(i)("DueDate").ToString = DBNull.Value.ToString Then
                Else
                    DueDate = Convert.ToDateTime(dt1.Rows(i)("DueDate")).ToString("dd/MM/yyyy")
                End If

                'Retrieve Default Invoice Format and EmailID for the customer

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If dt1.Rows(i)("ContactType").ToString = "COMPANY" Then
                    command2.CommandText = "SELECT DefaultInvoiceFormat,BillContact1Email,BillContact2Email FROM tblcompany where accountid='" & dt1.Rows(i)("AccountID").ToString & "' and AutoEmailInvoice=1"

                ElseIf dt1.Rows(i)("ContactType").ToString = "PERSON" Then
                    command2.CommandText = "SELECT DefaultInvoiceFormat,BillEmail,BillContact2Email FROM tblperson where accountid='" & dt1.Rows(i)("AccountID").ToString & "' and AutoEmailInvoice=1"

                End If

                command2.Connection = connPeriod

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    InvoiceFormat = dt2.Rows(0)("DefaultInvoiceFormat").ToString

                    'If dt2.Rows(0)("BillContact1Email").ToString <> "" Or String.IsNullOrEmpty(dt2.Rows(0)("BillContact1Email").ToString) = False Then
                    '    ToEmail = dt2.Rows(0)("BillContact1Email").ToString + ";" + dt2.Rows(0)("BillContact2Email").ToString
                    'Else
                    '    ToEmail = dt2.Rows(0)("BillContact2Email").ToString
                    'End If

                    ToEmail = "sasi.vishwa@gmail.com;christian.reyes@anticimex.com.sg"

                    ' ToEmail = "sasi.vishwa@gmail.com"
                    dt2.Clear()
                    dt2.Dispose()
                    dr2.Close()


                    'Retrieve Email Template

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text

                    command.Connection = connPeriod

                    command.CommandText = "Select * from tblEmailSetUp where SetUpID='AUTO-INV'"


                    Dim dr As MySqlDataReader = command.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    Dim subject As String = ""
                    Dim content As String = ""

                    If dt.Rows.Count > 0 Then
                        subject = dt.Rows(0)("Subject").ToString
                        content = dt.Rows(0)("Contents").ToString

                    End If

                    Dim monthyear As String = ""

                    monthyear = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(txtCalendarPeriod.Text.Substring(4, 2)) + " " + txtCalendarPeriod.Text.Substring(0, 4)

                    content = System.Text.RegularExpressions.Regex.Replace(content, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
                    content = content.Replace("CALENDAR PERIOD", monthyear)
                    content = content.Replace("CLIENT NAME", CustName)
                    content = content.Replace("INVOICE DUE DATE", DueDate)
                    content = content.Replace("CN DATE", SalesDate)
                    content = content.Replace("SALES DATE", SalesDate)
                    'content = content.Replace("COMPANY NAME", "ANTICIMEX PEST MANAGEMENT PTE. LTD.")
                    content = content.Replace("COMPANY NAME", ConfigurationManager.AppSettings("CompanyName").ToString())

                    content = content.Replace("INVOICE NUMBER", InvoiceNumber)
                    content = content.Replace("CN NUMBER", InvoiceNumber)
                    content = content.Replace("STAFF ID", Convert.ToString(Session("UserID")))
                    content = content.Replace("EMAIL SENT DATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))
                    Dim domain As String = ConfigurationManager.AppSettings("DomainName").ToString()

                    If domain = "SINGAPORE (Beta)" Then
                        domain = "BETA"
                    Else
                    End If

                    content = content.Replace("Unsubscribe", "<a href=""http://103.9.61.71:91/UnsubscribeAutoInvoice.aspx?Domain=" + domain + "&AccountType=" + dt1.Rows(i)("ContactType").ToString + "&AccountID=" + dt1.Rows(i)("AccountID").ToString + """> Unsubscribe</a>")
                    '  content = content.Replace("Unsubscribe", "<a href=""http://103.9.61.71"">Unsubscribe</a>")


                    subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
                    subject = subject.Replace("CLIENT NAME", CustName)
                    subject = subject.Replace("SALES DATE", SalesDate)
                    subject = subject.Replace("INVOICE NUMBER", InvoiceNumber)
                    subject = subject.Replace("CALENDAR PERIOD", monthyear)

                    dt.Clear()
                    dt.Dispose()

                    dr.Close()

                    If String.IsNullOrEmpty(InvoiceFormat) Or InvoiceFormat = "" Then
                        InvoiceFormat = "Format1"
                    End If




                    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Dim insCmds As New MySqlCommand()
                    'insCmds.CommandType = CommandType.Text

                    'Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
                    'insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

                    'insCmds.CommandText = insQuery

                    'insCmds.Parameters.Clear()
                    'insCmds.Parameters.AddWithValue("@LoginId", "TESTAUTOEMAIL")
                    'insCmds.Parameters.AddWithValue("@Event", dt1.Rows.Count.ToString + " " + InvoiceFormat)
                    'insCmds.Parameters.AddWithValue("@Error", InvoiceNumber)
                    'insCmds.Parameters.AddWithValue("@ID", ID)
                    'insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


                    'insCmds.Connection = connPeriod
                    'insCmds.ExecuteNonQuery()
                    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                    GenerateAttachment(InvoiceFormat, InvoiceNumber, subject, content)

                    'Invoice Interval

                    Dim commandInterval As MySqlCommand = New MySqlCommand

                    commandInterval.CommandType = CommandType.Text

                    commandInterval.CommandText = "SELECT InvoiceEmailInterval FROM tblservicerecordmastersetup where rcno=1"

                    commandInterval.Connection = connPeriod

                    Dim drInterval As MySqlDataReader = commandInterval.ExecuteReader()
                    Dim dtInterval As New DataTable
                    dtInterval.Load(drInterval)

                    Dim interval As Integer = 0

                    If dtInterval.Rows.Count > 0 Then
                        interval = dtInterval.Rows(0)("InvoiceEmailInterval")
                    Else
                        interval = 0
                    End If
                    interval = interval * 1000
                    System.Threading.Thread.Sleep(interval)
                End If

            Next

            '  If dt1.Rows.Count = count Then
            ' lblMessage.Text = "EMAILING INVOICE COMPLETED. " ' + dt1.Rows.Count.ToString + " EMAILS SENT"
            'Else
            '    lblMessage.Text = "EMAILING INVOICE COMPLETED. " + (dt1.Rows.Count.ToString - count) + " EMAILS SENT," + count + " EMAILS FAILED"

            'End If


        End If


        command1.Dispose()
        dt1.Dispose()
        dr1.Close()
        Return InvoiceNumber
    End Function



    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn1 As New MySqlConnection()
            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "AUTOEMAIL")
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn1.Open()
            insCmds.Connection = conn1
            insCmds.ExecuteNonQuery()
            conn1.Close()
            conn1.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, ID)
        End Try
    End Sub


    Private Sub GenerateAttachment(query As String, InvoiceNumber As String, subject As String, content As String)
        Try
            Dim crReportDocument As New ReportDocument()

            If query = "Format1" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format1_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format2" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format2_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format3" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format3_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format4" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format4_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format5" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format5_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format6" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format6_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format7" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format7_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format8" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format8_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format9" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format9.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))
            ElseIf query = "Format10" Then
                crReportDocument.Load(Server.MapPath("~/Reports/ARReports/TaxInvoice_Format10_New.rpt"))
                crReportDocument.SetParameterValue("pUserID", Convert.ToString(Session("UserId")))

            End If


            Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()

            Dim myTables As Tables = crReportDocument.Database.Tables

            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
                myTable.ApplyLogOnInfo(myTableLogonInfo)
                myTableLogonInfo.ConnectionInfo = myConnectionInfo

            Next

            Dim FilePath As String = ""
            crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} = '" & InvoiceNumber & "'"

            FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")



            'If query = "Format1" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} = '" & InvoiceNumber & "'"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format2" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format3" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format4" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format5" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format6" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format7" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format8" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format9" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")
            'ElseIf query = "Format10" Then
            '    crReportDocument.RecordSelectionFormula = "{tblSales1.InvoiceNumber} in [" & InvoiceNumber & "]"

            '    FilePath = Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF")



            'End If


            If File.Exists(FilePath) Then
                File.Delete(FilePath)

            End If
            '   lblAlert.Text = FilePath + " " + query

            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

            expo = crReportDocument.ExportOptions

            expo.ExportDestinationType = ExportDestinationType.DiskFile

            expo.ExportFormatType = ExportFormatType.PortableDocFormat

            expo.DestinationOptions = oDfDopt

            crReportDocument.Export()
            '  InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", FilePath, ID)
            'lblAlert.Text = lblAlert.Text + " " + "yES"

            crReportDocument.Close()
            crReportDocument.Dispose()


            EmailInvoice(query, InvoiceNumber, subject, content)

        Catch ex As Exception
            InsertIntoTblWebEventLog("GenerateAttachment", ex.Message.ToString, InvoiceNumber)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub EmailInvoice(query As String, InvoiceNumber As String, subject As String, content As String)
        If String.IsNullOrEmpty(ToEmail) Then
            '  MessageBox.Message.Alert(Page, "Enter email address in TO field!!!", "str")
            Return
        End If
        Dim CCEmail As String = ""

        Try
            ToEmail = ValidateEmail(ToEmail)
            If ToEmail.Last.ToString = ";" Then
                ToEmail = ToEmail.Remove(ToEmail.Length - 1)

            End If

            If ToEmail.First.ToString = ";" Then
                ToEmail = ToEmail.Remove(0)

            End If



            Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
            Dim oSmtp As New SmtpClient()

            oMail.Subject = subject
            oMail.HtmlBody = content

            Dim pattern As String
            pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

            If String.IsNullOrEmpty(ToEmail) = False Then
                Dim ToAddress As String() = ToEmail.Split(";"c)
                If ToAddress.Count() > 0 Then
                    For i As Integer = 0 To ToAddress.Count() - 1
                        If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

                        Else
                            InsertIntoTblWebEventLog("AUTOSENDINVOICE", "EMAIL TO ADDRESS INVALID", InvoiceNumber)
                            ' MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
                            Return
                        End If
                        oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
                    Next
                End If
            End If

            '  txtCC.Text = txtCC.Text + ";sasi.vishwa@gmail.com;"

            If String.IsNullOrEmpty(CCEmail) = False Then
                Dim CCAddress As String() = CCEmail.Split(";"c)
                If CCAddress.Count() > 0 Then
                    For i As Integer = 0 To CCAddress.Count() - 1
                        If Regex.IsMatch(CCAddress(i).ToString(), pattern) Then

                        Else
                            InsertIntoTblWebEventLog("AUTOSENDINVOICE", "EMAIL CC ADDRESS INVALID", InvoiceNumber)
                            ' MessageBox.Message.Alert(Page, "Enter valid 'CC' Email address" + " (" + CCAddress(i).ToString() + ")", "str")
                            Return
                        End If
                        oMail.[Cc].Add(New MailAddress(CCAddress(i).ToString()))
                    Next
                End If
            End If


            Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
            oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL


            ' oMail.AddAttachment(Server.MapPath("~/PDFs/" + lblRecordNo.Text + ".PDF"))
            oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

            oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
            oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
            oMail.AddAttachment(Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF"))

            oSmtp.SendMail(oServer, oMail)

            'If System.IO.Directory.Exists(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID"))) Then
            '    System.IO.Directory.Delete(Server.MapPath("~/Uploads/Email/") + Convert.ToString(Session("UserID")), True)
            'End If

            ' Delete pdf attachment

            System.IO.File.Delete(Server.MapPath("~/PDFs/" + InvoiceNumber + ".PDF"))

            oSmtp.Close()

            '  UpdateInvoiceEmailSentField(conn,InvoiceNumber, query)

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            MessageBox.Message.Alert(Page, exstr, "str")
            InsertIntoTblWebEventLog("btnSend_Click", ex.Message.ToString, InvoiceNumber)

        End Try

    End Sub

    Protected Function ValidateEmail(ByVal EmailId As String) As String
        Dim resEmail As String = ""
        If EmailId.Contains(","c) Then EmailId = EmailId.Replace(","c, ";"c)
        If EmailId.Contains("/"c) Then EmailId = EmailId.Replace("/"c, ";"c)
        If EmailId.Contains(":"c) Then EmailId = EmailId.Replace(":"c, ";"c)
        resEmail = EmailId.TrimEnd(";"c)
        Return resEmail
    End Function

    Private Sub UpdateInvoiceEmailSentField(conn As MySqlConnection, InvoiceNumber As String, InvoiceFormat As String)
        Try
            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn
            command.CommandText = "UPDATE tblsales SET EmailSentStatus = 'Y',EmailSentDate = @EmailSentDate,EmailSentBy='AUTOSEND' WHERE InvoiceNumber = @InvoiceNumber"
            command.Parameters.Clear()

            ' command.Parameters.AddWithValue("@EmailSent", 1)
            command.Parameters.AddWithValue("@EmailSentDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Parameters.AddWithValue("@EmailSentBy", "AUTOSEND-" + InvoiceFormat)

            command.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber)
            command.ExecuteNonQuery()

            command.Dispose()

            'conn.Close()
            'conn.Dispose()
            'conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("UpdateInvoiceEmailSendField", ex.Message.ToString, InvoiceNumber)
        End Try
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As System.EventArgs) Handles btnConfirmYes.Click
        Dim InvoiceNumber As String = ""
        Dim CCEmail As String = "" 'ConfigurationManager.AppSettings("EmailInvoiceCC").ToString()
        '    Dim FromEmail As String = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()


        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            InvoiceNumber = GetInvoiceNumber(conn)
            conn.Close()

            lblMessage.Text = "EMAILING INVOICE COMPLETED. "
        Catch ex As Exception
            InsertIntoTblWebEventLog("PERIOD MASTER - btnEmailInvoice", ex.Message.ToString, "EMAILINVOICE")
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
        'Else
        '    lblMessage.Text = "EMAILING INVOICE CANCELLED"
        'End If

    End Sub

    Protected Sub btnAutoEmail_Click(sender As Object, e As EventArgs) Handles btnAutoEmail.Click
        If btnAutoEmail.Text = "ENABLED" Then
            btnAutoEmail.Text = "DISABLED"
            btnAutoEmail.BackColor = Drawing.Color.OrangeRed

        ElseIf btnAutoEmail.Text = "DISABLED" Then
            btnAutoEmail.Text = "ENABLED"
            btnAutoEmail.BackColor = Drawing.Color.Green

        End If
    End Sub

    Protected Sub btnAutoEmailSOA_Click(sender As Object, e As EventArgs) Handles btnAutoEmailSOA.Click
        If btnAutoEmailSOA.Text = "ENABLED" Then
            btnAutoEmailSOA.Text = "DISABLED"
            btnAutoEmailSOA.BackColor = Drawing.Color.OrangeRed

        ElseIf btnAutoEmailSOA.Text = "DISABLED" Then
            btnAutoEmailSOA.Text = "ENABLED"
            btnAutoEmailSOA.BackColor = Drawing.Color.Green

        End If
    End Sub

    Protected Sub btnViewMsgOpenTransactions_Click(sender As Object, e As EventArgs) Handles btnViewMsgOpenTransactions.Click
        mdlViewOpenTransactions.Show()
    End Sub

    Protected Sub chkInvoiceAddLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoiceAddLock.CheckedChanged
        If chkInvoiceAddLock.Checked = True Then
            If CheckforNoDetails("Invoice", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkInvoiceAddLock.Checked = False
            End If
        End If
    End Sub



    Protected Sub chkReceiptAddLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkReceiptAddLock.CheckedChanged
        If chkReceiptAddLock.Checked = True Then
            If CheckforNoDetails("Receipt", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkReceiptAddLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkInvoiceEditLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkInvoiceEditLock.CheckedChanged
        If chkInvoiceEditLock.Checked = True Then
            If CheckforNoDetails("Invoice", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkInvoiceEditLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkReceiptEditLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkReceiptEditLock.CheckedChanged
        If chkReceiptEditLock.Checked = True Then
            If CheckforNoDetails("Receipt", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkReceiptEditLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkCNAddLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkCNAddLock.CheckedChanged
        If chkCNAddLock.Checked = True Then
            If CheckforNoDetails("CNDN", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkCNAddLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkCNEditLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkCNEditLock.CheckedChanged
        If chkCNEditLock.Checked = True Then
            If CheckforNoDetails("CNDN", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkCNEditLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkJournalAddLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkJournalAddLock.CheckedChanged
        If chkJournalAddLock.Checked = True Then
            If CheckforNoDetails("Journal", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkJournalAddLock.Checked = False
            End If
        End If
    End Sub

    Protected Sub chkJournalEditLock_CheckedChanged(sender As Object, e As EventArgs) Handles chkJournalEditLock.CheckedChanged
        If chkJournalEditLock.Checked = True Then
            If CheckforNoDetails("Journal", txtAccountingPeriod.Text.Trim, txtLocationId.Text.Trim) = True Then
                chkJournalEditLock.Checked = False
            End If
        End If
    End Sub

    Private Function CheckforNoDetails(ByVal strSource As String, ByVal strGLPeriod As String, ByVal strLocationID As String) As Boolean
        Return False

        CheckforNoDetails = False

        If strSource = "Invoice" Then
            If chkInvoiceAddLock.Checked = True Or chkInvoiceEditLock.Checked = True Then
                Dim connNoDetails As MySqlConnection = New MySqlConnection()

                connNoDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connNoDetails.Open()

                Dim commandNoDetails As MySqlCommand = New MySqlCommand

                commandNoDetails.CommandType = CommandType.Text

                If String.IsNullOrEmpty(strLocationID) = False Then
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'INVOICE' limit 1"
                Else
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'INVOICE' and Location ='" & strLocationID & "' limit 1"
                End If

                commandNoDetails.Connection = connNoDetails

                Dim drNoDetails As MySqlDataReader = commandNoDetails.ExecuteReader()
                Dim dtNoDetails As New DataTable
                dtNoDetails.Load(drNoDetails)

                If dtNoDetails.Rows.Count > 0 Then
                    connNoDetails.Close()
                    connNoDetails.Dispose()

                    If String.IsNullOrEmpty(strLocationID) = False Then
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'INVOICE'"
                    Else
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'INVOICE' and Location ='" & strLocationID & "'"
                    End If


                    sqlDSViewNoDetails.DataBind()

                    grdViewNoDetails.DataSourceID = "sqlDSViewNoDetails"
                    grdViewNoDetails.DataBind()

                    CheckforNoDetails = True
                    Return CheckforNoDetails
                    mdlViewNoDetails.Show()
                    Exit Function
                End If

            End If
        End If


        If strSource = "Receipt" Then
            If chkReceiptAddLock.Checked = True Or chkReceiptEditLock.Checked = True Then
                Dim connNoDetails As MySqlConnection = New MySqlConnection()

                connNoDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connNoDetails.Open()

                Dim commandNoDetails As MySqlCommand = New MySqlCommand

                commandNoDetails.CommandType = CommandType.Text

                If String.IsNullOrEmpty(strLocationID) = False Then
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'RECEIPT' limit 1"
                Else
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'RECEIPT' and Location ='" & strLocationID & "' limit 1"
                End If

                commandNoDetails.Connection = connNoDetails

                Dim drNoDetails As MySqlDataReader = commandNoDetails.ExecuteReader()
                Dim dtNoDetails As New DataTable
                dtNoDetails.Load(drNoDetails)

                If dtNoDetails.Rows.Count > 0 Then
                    connNoDetails.Close()
                    connNoDetails.Dispose()

                    If String.IsNullOrEmpty(strLocationID) = False Then
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'RECEIPT'"
                    Else
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'RECEIPT' and Location ='" & strLocationID & "'"
                    End If


                    sqlDSViewNoDetails.DataBind()

                    grdViewNoDetails.DataSourceID = "sqlDSViewNoDetails"
                    grdViewNoDetails.DataBind()

                    CheckforNoDetails = True

                    mdlViewNoDetails.Show()
                    Return CheckforNoDetails
                    Exit Function
                End If

            End If
        End If

        If strSource = "CNDN" Then
            If chkCNAddLock.Checked = True Or chkCNEditLock.Checked = True Then
                Dim connNoDetails As MySqlConnection = New MySqlConnection()

                connNoDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connNoDetails.Open()

                Dim commandNoDetails As MySqlCommand = New MySqlCommand

                commandNoDetails.CommandType = CommandType.Text

                If String.IsNullOrEmpty(strLocationID) = False Then
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'CN / DN' limit 1"
                Else
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'CN / DN' and Location ='" & strLocationID & "' limit 1"
                End If

                commandNoDetails.Connection = connNoDetails

                Dim drNoDetails As MySqlDataReader = commandNoDetails.ExecuteReader()
                Dim dtNoDetails As New DataTable
                dtNoDetails.Load(drNoDetails)

                If dtNoDetails.Rows.Count > 0 Then
                    connNoDetails.Close()
                    connNoDetails.Dispose()

                    If String.IsNullOrEmpty(strLocationID) = False Then
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'CN / DN'"
                    Else
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'CN / DN' and Location ='" & strLocationID & "'"
                    End If


                    sqlDSViewNoDetails.DataBind()

                    grdViewNoDetails.DataSourceID = "sqlDSViewNoDetails"
                    grdViewNoDetails.DataBind()

                    CheckforNoDetails = True

                    mdlViewNoDetails.Show()
                    Return CheckforNoDetails
                    Exit Function
                End If

            End If
        End If


        If strSource = "JOURNAL" Then
            If chkJournalAddLock.Checked = True Or chkJournalEditLock.Checked = True Then
                Dim connNoDetails As MySqlConnection = New MySqlConnection()

                connNoDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connNoDetails.Open()

                Dim commandNoDetails As MySqlCommand = New MySqlCommand

                commandNoDetails.CommandType = CommandType.Text

                If String.IsNullOrEmpty(strLocationID) = False Then
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'JOURNAL' limit 1"
                Else
                    commandNoDetails.CommandText = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'JOURNAL' and Location ='" & strLocationID & "' limit 1"
                End If

                commandNoDetails.Connection = connNoDetails

                Dim drNoDetails As MySqlDataReader = commandNoDetails.ExecuteReader()
                Dim dtNoDetails As New DataTable
                dtNoDetails.Load(drNoDetails)

                If dtNoDetails.Rows.Count > 0 Then
                    connNoDetails.Close()
                    connNoDetails.Dispose()

                    If String.IsNullOrEmpty(strLocationID) = False Then
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'JOURNAL'"
                    Else
                        sqlDSViewNoDetails.SelectCommand = "SELECT * FROM vwnodetailsinarmodules where GLPeriod = '" & txtAccountingPeriod.Text.Trim & "' and DocumentType = 'JOURNAL' and Location ='" & strLocationID & "'"
                    End If


                    sqlDSViewNoDetails.DataBind()

                    grdViewNoDetails.DataSourceID = "sqlDSViewNoDetails"
                    grdViewNoDetails.DataBind()

                    CheckforNoDetails = True

                    mdlViewNoDetails.Show()
                    Return CheckforNoDetails
                    Exit Function
                End If

            End If
        End If
    End Function

    Private Sub RetrieveInvoiceEmailLog()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.Connection = conn
            command1.CommandText = "Select * from tblinvoiceemaillog where period='" & txtCalendarPeriod.Text & "'"

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "delete from tblinvoiceemaillog where period='" & txtCalendarPeriod.Text & "'"

                command2.Connection = conn

                command2.ExecuteNonQuery()
            End If

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text

            command.Connection = conn

            Dim qry As String = "insert into tblinvoiceemailLog(InvoiceNumber,InvoiceDate,AccountID,AccountType,EmailSentStatus,"
            qry = qry + "EmailSentDate,EmailSentBy,StatementDate,RetryCount,Period,CreatedOn,InvoiceAmount)"
            qry = qry + "(SELECT InvoiceNumber,SalesDate,AccountID,ContactType,EmailSentStatus,EmailSentDate,EmailSentBy,"
            qry = qry + "curdate(),0,GLPeriod,current_timestamp(),BalanceBase FROM tblsales where GLPeriod='" & txtCalendarPeriod.Text & "' and PostStatus='P' and "
            qry = qry + "doctype='ARIN' order by invoicenumber);"

            command.CommandText = qry
            command.ExecuteNonQuery()

            command.Dispose()


            command.CommandType = CommandType.Text

            command.Connection = conn
            command.CommandText = "UPDATE tblinvoiceemaillog SET Message='INVOICE SENT' where EmailSentstatus='Y' and PERIOD='" & txtCalendarPeriod.Text & "'"
            command.ExecuteNonQuery()

            command.Dispose()

            'Check Balance zero
            command.CommandType = CommandType.Text

            command.Connection = conn
            command.CommandText = "UPDATE tblinvoiceemaillog SET Message='ZERO BALANCE' where EmailSentstatus='N' and InvoiceAmount=0 and PERIOD='" & txtCalendarPeriod.Text & "'"
            command.ExecuteNonQuery()

            command.Dispose()

            Dim Str As String = "AR" + txtCalendarPeriod.Text + "%"

            '  Check for Invalid EmailID
            Dim commandchk1 As MySqlCommand = New MySqlCommand

            commandchk1.CommandType = CommandType.Text

            commandchk1.Connection = conn
            commandchk1.CommandText = "SELECT * FROM tblautoemaileventlog WHERE ERROR='EMAIL TO ADDRESS INVALID' AND ID LIKE '" & Str & "' GROUP BY ID;"

            Dim drchk1 As MySqlDataReader = commandchk1.ExecuteReader()
            Dim dtchk1 As New System.Data.DataTable
            dtchk1.Load(drchk1)

            If dtchk1.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk1.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='INVALID EMAILID' WHERE InvoiceNumber = @InvoiceNumber AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@InvoiceNumber", dtchk1.Rows(i)("ID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk1.Clear()
            drchk1.Close()
            commandchk1.Dispose()

            '  Check for No EmailID
            Dim commandchk2 As MySqlCommand = New MySqlCommand

            commandchk2.CommandType = CommandType.Text

            commandchk2.Connection = conn
            commandchk2.CommandText = "SELECT * FROM tblautoemaileventlog WHERE ERROR='Sequence contains no elements' AND ID LIKE '" & Str & "' GROUP BY ID;"

            Dim drchk2 As MySqlDataReader = commandchk2.ExecuteReader()
            Dim dtchk2 As New System.Data.DataTable
            dtchk2.Load(drchk2)

            If dtchk2.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk2.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='NO EMAILID' WHERE InvoiceNumber = @InvoiceNumber AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@InvoiceNumber", dtchk2.Rows(i)("ID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk2.Clear()
            drchk2.Close()
            commandchk2.Dispose()

            '  Check for No EmailID
            Dim commandchk21 As MySqlCommand = New MySqlCommand

            commandchk21.CommandType = CommandType.Text

            commandchk21.Connection = conn
            commandchk21.CommandText = "SELECT * FROM tblautoemaileventlog WHERE ERROR='NO EMAILID' AND ID LIKE '" & Str & "' GROUP BY ID;"

            Dim drchk21 As MySqlDataReader = commandchk21.ExecuteReader()
            Dim dtchk21 As New System.Data.DataTable
            dtchk21.Load(drchk21)

            If dtchk21.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk21.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='NO EMAILID' WHERE InvoiceNumber = @InvoiceNumber AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@InvoiceNumber", dtchk21.Rows(i)("ID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk21.Clear()
            drchk21.Close()
            commandchk21.Dispose()


            '  Check for No EmailID
            Dim commandchk3 As MySqlCommand = New MySqlCommand

            commandchk3.CommandType = CommandType.Text

            commandchk3.Connection = conn
            commandchk3.CommandText = "SELECT * FROM tblautoemaileventlog WHERE EVENT='GetInvoiceNumber - 3' AND (ERROR='' or ERROR='Format1 ' or Error='-1') AND ID LIKE '" & Str & "' GROUP BY ID;"

            Dim drchk3 As MySqlDataReader = commandchk3.ExecuteReader()
            Dim dtchk3 As New System.Data.DataTable
            dtchk3.Load(drchk3)

            If dtchk3.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk3.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='NO EMAILID' WHERE InvoiceNumber = @InvoiceNumber AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@InvoiceNumber", dtchk3.Rows(i)("ID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk3.Clear()
            drchk3.Close()
            commandchk3.Dispose()


            '  Check for AutoEmail disabled - corporate
            Dim commandchk4 As MySqlCommand = New MySqlCommand

            commandchk4.CommandType = CommandType.Text

            commandchk4.Connection = conn
            commandchk4.CommandText = "SELECT * FROM tblcompany WHERE AutoEmailInvoice=0;"

            Dim drchk4 As MySqlDataReader = commandchk4.ExecuteReader()
            Dim dtchk4 As New System.Data.DataTable
            dtchk4.Load(drchk4)

            If dtchk4.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk4.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='AUTOEMAIL DISABLED' WHERE Accountid = @AccountID AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@AccountID", dtchk4.Rows(i)("AccountID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk4.Clear()
            drchk4.Close()
            commandchk4.Dispose()

            '  Check for AutoEmail disabled - residential
            Dim commandchk5 As MySqlCommand = New MySqlCommand

            commandchk5.CommandType = CommandType.Text

            commandchk5.Connection = conn
            commandchk5.CommandText = "SELECT * FROM tblperson WHERE AutoEmailInvoice=0;"

            Dim drchk5 As MySqlDataReader = commandchk5.ExecuteReader()
            Dim dtchk5 As New System.Data.DataTable
            dtchk5.Load(drchk5)

            If dtchk5.Rows.Count > 0 Then
                For i As Int16 = 0 To dtchk5.Rows.Count - 1
                    command.CommandType = CommandType.Text

                    command.Connection = conn
                    command.CommandText = "UPDATE tblinvoiceemaillog SET Message='AUTOEMAIL DISABLED' WHERE Accountid = @AccountID AND EMAILSENTSTATUS='N'"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@AccountID", dtchk5.Rows(i)("AccountID"))
                    command.ExecuteNonQuery()

                    command.Dispose()
                Next

            End If

            dtchk5.Clear()
            drchk5.Close()
            commandchk5.Dispose()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveInvoiceEmailLog()", ex.Message.ToString, txtCreatedBy.Text)
        End Try
    End Sub



    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select InvoiceNumber,InvoiceDate,Message from tblinvoiceemaillog where period='" & txtCalendarPeriod.Text & "'"

        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If String.IsNullOrEmpty(txtCalendarPeriod.Text) Then
            lblAlert.Text = "SELECT PERIOD TO VIEW"
            Exit Sub

        End If
        RetrieveInvoiceEmailLog()
        '  InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "1")
        Dim dt As DataTable = GetDataSet()
        '  InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "2")
        WriteExcelWithNPOI(dt, "xlsx")

        '  InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "3")
    End Sub

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
        Dim workbook As IWorkbook
        If extension = "xlsx" Then
            workbook = New XSSFWorkbook()
        ElseIf extension = "xls" Then
            workbook = New HSSFWorkbook()
        Else
            Throw New Exception("This format is not supported")
        End If

        Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

        'Add Selection Criteria

        Dim row1 As IRow = sheet1.CreateRow(0)
        Dim cell1 As ICell = row1.CreateCell(0)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + txtCalendarPeriod.Text)
        '+ Session("Selection").ToString)

        ' cell1.SetCellValue(Session("Selection").ToString)
        cell1.CellStyle.WrapText = True
        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
        sheet1.AddMergedRegion(cra)

        'Add Column Heading
        row1 = sheet1.CreateRow(1)

        Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
        testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

        testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
        testeStyle.FillPattern = FillPattern.SolidForeground
        '  testeStyle.FillForegroundColor = IndexedColors.White.Index
        testeStyle.Alignment = HorizontalAlignment.Center

        Dim RowFont As IFont = workbook.CreateFont()
        RowFont.Color = IndexedColors.White.Index
        RowFont.IsBold = True

        testeStyle.SetFont(RowFont)

        For j As Integer = 0 To dt.Columns.Count - 1
            Dim cell As ICell = row1.CreateCell(j)
            '   InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

            Dim columnName As String = dt.Columns(j).ToString()
            cell.SetCellValue(columnName)
            ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
            cell.CellStyle = testeStyle

        Next

        'Add details
        Dim _doubleCellStyle As ICellStyle = Nothing

        If _doubleCellStyle Is Nothing Then
            _doubleCellStyle = workbook.CreateCellStyle()
            _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        End If

        Dim _intCellStyle As ICellStyle = Nothing

        If _intCellStyle Is Nothing Then
            _intCellStyle = workbook.CreateCellStyle()
            _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
        End If

        Dim dateCellStyle As ICellStyle = Nothing

        If dateCellStyle Is Nothing Then
            dateCellStyle = workbook.CreateCellStyle()
            dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium


        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet1.CreateRow(i + 2)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)

                'If j >= 13 And j <= 20 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Double = Convert.ToDouble("0.00")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _doubleCellStyle

                'ElseIf j = 26 Or j = 27 Or j = 28 Or j = 35 Or j = 39 Or j = 40 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Double = Convert.ToDouble("0.00")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _doubleCellStyle
                'ElseIf j = 48 Or j = 36 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Int32 = Convert.ToInt32("0")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _intCellStyle

                'Else
                If j = 4 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                        cell.SetCellValue(d)
                        'Else
                        '    Dim d As Double = Convert.ToDouble("0.00")
                        '    cell.SetCellValue(d)

                    End If
                    cell.CellStyle = dateCellStyle
                Else
                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                    cell.CellStyle = AllCellStyle

                End If
                If i = dt.Rows.Count - 1 Then
                    sheet1.AutoSizeColumn(j)
                End If
            Next
        Next

        ' InsertIntoTblWebEventLog("INVOICE", "WriteExcelWithNPOI1", dt.Rows.Count.ToString)

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = "attachment; filename=InvoiceEmailLog"
            '   InsertIntoTblWebEventLog("INVOICE", "WriteExcelWithNPOI2", dt.Rows.Count.ToString)


            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
        '  InsertIntoTblWebEventLog("INVOICE", "WriteExcelWithNPOI3", dt.Rows.Count.ToString)


    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView1.Rows
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If


            End If
        Next
    End Sub


    Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
        'Try

        '    If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
        '        lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
        '        Return
        '    End If

        '    lblMessage.Text = ""
        '    lblAlert.Text = ""

        '    Dim btn1 As Button = DirectCast(sender, Button)

        '    Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        '    Dim rowindex1 As Integer = xrow1.RowIndex


        '    Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

        '    txtRcno.Text = lblidRcno

        '    GridView1.SelectedIndex = rowindex1

        '    Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(2).Text
        '    txtLogDocNo.Text = strRecordNo
        '    sqlDSViewEditHistory.SelectCommand = "Select * from tblperiod_log where  CalendarPeriod = '" & txtLogDocNo.Text & "' order by LastModifiedOnLog desc"
        '    sqlDSViewEditHistory.DataBind()

        '    grdViewEditHistory1.DataSourceID = "sqlDSViewEditHistory"
        '    grdViewEditHistory1.DataBind()

        '    mdlViewEditHistory.Show()


        'Catch ex As Exception
        '    InsertIntoTblWebEventLog("PERIOD - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString)
        '    lblAlert.Text = ex.Message.ToString
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        'End Try

    End Sub

    'Protected Sub grdViewEditHistory1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdViewEditHistory1.PageIndexChanging
    '    'grdViewEditHistory1.PageIndex = e.NewPageIndex

    '    ''sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where Module='LOCK' and  DocRef = '" & txtLogDocNo.Text & "' order by logdate desc"
    '    ''sqlDSViewEditHistory.SelectCommand = "Select * from tbllockservicerecord_log where " & txtLogDocNo.Text & "  order by LastModifiedOnLog desc"
    '    'sqlDSViewEditHistory.SelectCommand = "Select * from tblperiod_log where  CalendarPeriod = '" & txtLogDocNo.Text & "' order by LastModifiedOnLog desc"

    '    'sqlDSViewEditHistory.DataBind()

    '    'grdViewEditHistory1.DataSourceID = "sqlDSViewEditHistory"
    '    'grdViewEditHistory1.DataBind()

    '    'mdlViewEditHistory.Show()

    '    'grdViewEditHistory1.DataBind()
    'End Sub

    Private Sub RetrieveDocInfo(Period As String)
        Try
            lblInvOpenCount.Text = "0"
            lblInvPostedCount.Text = "0"
            lblInvTotalCount.Text = "0"

            lblCNOpenCount.Text = "0"
            lblCNPostedCount.Text = "0"
            lblCNTotalCount.Text = "0"

            lblDNOpenCount.Text = "0"
            lblDNPostedCount.Text = "0"
            lblDNTotalCount.Text = "0"

            lblJNOpenCount.Text = "0"
            lblJNPostedCount.Text = "0"
            lblJNTotalCount.Text = "0"

            lblRecOpenCount.Text = "0"
            lblRecPostedCount.Text = "0"
            lblRecTotalCount.Text = "0"


            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command.CommandText = "SELECT PostStatus,DocType,Count(*) as Count FROM tblsales where Glperiod = '" & Period & "' AND Location = '" & txtLocationId.Text & "' GROUP BY DocType,PostStatus"

            Else
                command.CommandText = "SELECT PostStatus,DocType,Count(*) as Count FROM tblsales where Glperiod = '" & Period & "' GROUP BY DocType,PostStatus"

            End If
             command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                Dim invopencount As Integer = 0
                Dim invpostcount As Integer = 0
                Dim totinvcount As Integer = 0

                Dim cnopencount As Integer = 0
                Dim cnpostcount As Integer = 0
                Dim totcncount As Integer = 0

                Dim dnopencount As Integer = 0
                Dim dnpostcount As Integer = 0
                Dim totdncount As Integer = 0

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("DocType") = "ARIN" Then
                        If dt.Rows(i)("PostStatus") = "P" Then
                            invpostcount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblInvPostedCount.Text = invpostcount.ToString("#,##,###")

                        End If

                        If dt.Rows(i)("PostStatus") = "O" Then
                            invopencount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblInvOpenCount.Text = invopencount.ToString("#,##,###")

                        End If
                       
                        ' totinvcount = Convert.ToInt32(lblInvPostedCount.Text) + Convert.ToInt32(lblInvOpenCount.Text)
                        totinvcount = invpostcount + invopencount

                        lblInvTotalCount.Text = totinvcount.ToString("#,##,###")
                        '   lblInvPostedCount.Text = Convert.ToInt32(lblInvPostedCount.Text).ToString("#,##,###")
                        '   lblInvOpenCount.Text = Convert.ToInt32(lblInvOpenCount.Text).ToString("#,##,###")

                        'If lblInvOpenCount.Text <> "" Then
                        '    lblInvOpenCount.Text = Convert.ToInt32(lblInvOpenCount.Text).ToString("#,##,###")
                        'Else
                        '    lblInvOpenCount.Text = 0
                        'End If

                        ' Return

                    ElseIf dt.Rows(i)("DocType") = "ARCN" Then
                        If dt.Rows(i)("PostStatus") = "P" Then
                            cnpostcount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblCNPostedCount.Text = cnpostcount.ToString("#,##,###")
                            '  totcncount = dt.Rows(i)("Count")
                        End If
                        If dt.Rows(i)("PostStatus") = "O" Then
                            cnopencount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblCNOpenCount.Text = cnopencount.ToString("#,##,###")
                            '    totcncount = totcncount + dt.Rows(i)("Count")
                        End If
                        ' lblCNTotalCount.Text = totcncount.ToString
                        totcncount = cnpostcount + cnopencount
                        lblCNTotalCount.Text = totcncount.ToString("#,##,###")

                    ElseIf dt.Rows(i)("DocType") = "ARDN" Then
                        If dt.Rows(i)("PostStatus") = "P" Then
                            dnpostcount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblDNPostedCount.Text = dnpostcount.ToString("#,##,###")
                            '  totdncount = dt.Rows(i)("Count")
                        End If
                        If dt.Rows(i)("PostStatus") = "O" Then
                            dnopencount = Convert.ToInt32(dt.Rows(i)("Count"))
                            lblDNOpenCount.Text = dnopencount.ToString("#,##,###")
                            '  totdncount = totdncount + dt.Rows(i)("Count")
                        End If
                        ' lblDNTotalCount.Text = totdncount.ToString
                        totdncount = dnpostcount + dnopencount
                        lblDNTotalCount.Text = totdncount.ToString("#,##,###")
                    End If

                Next
            End If

            command.Dispose()
            dt.Dispose()
            dr.Close()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command1.CommandText = "SELECT PostStatus,Count(*) as Count FROM tblRECV where Glperiod = '" & Period & "'  AND Location = '" & txtLocationId.Text & "' GROUP BY PostStatus"

            Else
                command1.CommandText = "SELECT PostStatus,Count(*) as Count FROM tblRECV where Glperiod = '" & Period & "' GROUP BY PostStatus"
            End If

            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New System.Data.DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                Dim totreccount As Integer = 0
                Dim recpostcount As Integer = 0
                Dim recopencount As Integer = 0

                For i = 0 To dt1.Rows.Count - 1

                    If dt1.Rows(i)("PostStatus") = "P" Then
                        recpostcount = Convert.ToInt32(dt1.Rows(i)("Count"))
                        lblRecPostedCount.Text = recpostcount.ToString("#,##,###")
                        '  totreccount = dt1.Rows(i)("Count")
                    End If
                    If dt1.Rows(i)("PostStatus") = "O" Then
                        recopencount = Convert.ToInt32(dt1.Rows(i)("Count"))
                        lblRecOpenCount.Text = recopencount.ToString("#,##,###")
                        ' totreccount = totreccount + dt1.Rows(i)("Count")
                    End If
                    ' lblRecTotalCount.Text = totreccount.ToString
                    totreccount = recpostcount + recopencount
                    lblRecTotalCount.Text = totreccount.ToString("#,##,###")
                Next
            End If

            command1.Dispose()
            dt1.Dispose()
            dr1.Close()

            Dim command2 As MySqlCommand = New MySqlCommand
            command2.CommandType = CommandType.Text
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                 command2.CommandText = "SELECT PostStatus,Count(*) as Count FROM tbljrnv where Glperiod = '" & Period & "' AND Location = '" & txtLocationId.Text & "' GROUP BY PostStatus"

            Else
                command2.CommandText = "SELECT PostStatus,Count(*) as Count FROM tbljrnv where Glperiod = '" & Period & "' GROUP BY PostStatus"
            End If

            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New System.Data.DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                Dim totjrnvcount As Integer = 0
                Dim jnpostcount As Integer = 0
                Dim jnopencount As Integer = 0

                For i = 0 To dt2.Rows.Count - 1

                    If dt2.Rows(i)("PostStatus") = "P" Then
                        jnpostcount = Convert.ToInt32(dt2.Rows(i)("Count"))
                        lblJNPostedCount.Text = jnpostcount.ToString("#,##,###")
                        '  totjrnvcount = dt2.Rows(i)("Count")
                    End If
                    If dt2.Rows(i)("PostStatus") = "O" Then
                        jnopencount = Convert.ToInt32(dt2.Rows(i)("Count"))
                        lblJNOpenCount.Text = jnopencount.ToString("#,##,###")
                        'totjrnvcount = totjrnvcount + dt2.Rows(i)("Count")
                    End If
                    ' lblJNTotalCount.Text = totjrnvcount.ToString
                    totjrnvcount = jnpostcount + jnopencount
                    lblJNTotalCount.Text = totjrnvcount.ToString("#,##,###")

                Next
            End If

            command2.Dispose()
            dt2.Dispose()
            dr2.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveDocInfo", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
          GridView1.DataSourceID = "SqlDataSource1"
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked = True Then
            chkInvoiceAddLock.Checked = True
            chkInvoiceEditLock.Checked = True
            chkReceiptAddLock.Checked = True
            chkReceiptEditLock.Checked = True
            chkCNAddLock.Checked = True
            chkCNEditLock.Checked = True
            chkJournalAddLock.Checked = True
            chkJournalEditLock.Checked = True
            chkEInvSubmitLock.Checked = True
            chkEInvCancelLock.Checked = True

        ElseIf chkAll.Checked = False Then
            chkInvoiceAddLock.Checked = False
            chkInvoiceEditLock.Checked = False
            chkReceiptAddLock.Checked = False
            chkReceiptEditLock.Checked = False
            chkCNAddLock.Checked = False
            chkCNEditLock.Checked = False
            chkJournalAddLock.Checked = False
            chkJournalEditLock.Checked = False
            chkEInvSubmitLock.Checked = False
            chkEInvCancelLock.Checked = False
        End If
    End Sub
End Class
