Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Bank
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""

        txtBankID.Text = ""
        txtBankName.Text = ""
        ddlReceiptVoucherLedger.SelectedIndex = 0
        txtReceiptVoucherPrefix.Text = ""
        ddlPaymentVoucherLedger.SelectedIndex = 0
        txtPaymentVoucherPrefix.Text = ""
        ddlBankLedger.SelectedIndex = 0
        ddlCurrency.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""

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

    

        txtBankID.Enabled = False
        txtBankName.Enabled = False

        ddlReceiptVoucherLedger.Enabled = False
        txtReceiptVoucherPrefix.Enabled = False
        ddlPaymentVoucherLedger.Enabled = False
        txtPaymentVoucherPrefix.Enabled = False
        ddlBankLedger.Enabled = False
        ddlCurrency.Enabled = False
        ddlBranch.Enabled = False
        ddlBankLedger.Enabled = False
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

        txtBankID.Enabled = True
        txtBankName.Enabled = True
        ddlBankLedger.Enabled = True
        ddlReceiptVoucherLedger.Enabled = True
        txtReceiptVoucherPrefix.Enabled = True
        ddlPaymentVoucherLedger.Enabled = True
        txtPaymentVoucherPrefix.Enabled = True
        ddlBankLedger.Enabled = True
        ddlCurrency.Enabled = True
        ddlBranch.Enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            lblBranch1.Visible = True
            ddlBranch.Visible = True
            SqlDataSource1.SelectCommand = "SELECT * FROM tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
        Else
            lblBranch1.Visible = False
            ddlBranch.Visible = False
            SqlDataSource1.SelectCommand = "SELECT * FROM tblBank order by BankID"
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
           
            If txtDisplayRecordsLocationwise.Text = "N" Then
                e.Row.Cells(10).Visible = False
                GridView1.HeaderRow.Cells(10).Visible = False

            ElseIf txtDisplayRecordsLocationwise.Text = "Y" Then
                e.Row.Cells(10).Visible = True
                GridView1.HeaderRow.Cells(10).Visible = True
            End If

            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
      
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        'EnableControls()
        'MakeMeNull()
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtBankID.Text = ""
        Else

            txtBankID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtBankName.Text = ""
        Else
            txtBankName.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            txtReceiptVoucherPrefix.Text = ""
        Else
            txtReceiptVoucherPrefix.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            txtPaymentVoucherPrefix.Text = ""
        Else
            txtPaymentVoucherPrefix.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        End If

        'txtBankID.Text = GridView1.SelectedRow.Cells(7).Text.Trim
        If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(6).Text.Trim) = True Or GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            ddlBankLedger.SelectedIndex = 0
        Else
            ddlBankLedger.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If

        If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(7).Text.Trim) Or GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            ddlReceiptVoucherLedger.SelectedIndex = 0
        Else
            ddlReceiptVoucherLedger.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        End If

        If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(8).Text.Trim) Or GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            ddlPaymentVoucherLedger.SelectedIndex = 0
        Else
            ddlPaymentVoucherLedger.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            ddlCurrency.SelectedIndex = 0
        Else
            ddlCurrency.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(10).Text = "&nbsp;" Then
            ddlBranch.SelectedIndex = 0
        Else
            ddlBranch.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text).ToString
        End If

        'If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Or String.IsNullOrEmpty(GridView1.SelectedRow.Cells(4).Text) = True Then
        '    ddlBankLedger.SelectedIndex = 0
        'Else
        '    ddlBankLedger.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString.Trim & " - " & Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString.Trim
        'End If

        txtMode.Text = "View"

        'txtMode.Text = "Edit"
        ' DisableControls()
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
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtBankID.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
            txtGroupAuthority.Text = Session("SecGroupAuthority")
            Dim Query As String

            Query = "Select COACode,Description from tblchartofaccounts  "
            PopulateDropDownList(Query, "Description", "COACode", ddlPaymentVoucherLedger)

            Query = "Select COACode, Description from tblchartofaccounts  "
            PopulateDropDownList(Query, "Description", "COACode", ddlBankLedger)

            Query = "Select COACode,Description from tblchartofaccounts  "
            PopulateDropDownList(Query, "Description", "COACode", ddlReceiptVoucherLedger)

            Query = "Select Currency from tblCurrency Order by Currency  "
            PopulateDropDownList(Query, "Currency", "Currency", ddlCurrency)

            Query = "SELECT LocationID, Location FROM tblGroupAccessLocation where GroupAccess = '" & txtGroupAuthority.Text.ToUpper & "'"
            'PopulateDropDownList(Query, "LocationID", "LocationID", ddlLocation)
            PopulateDropDownList(Query, "LocationID", "LocationID", ddlBranch)


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

            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString

            If dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString = "Y" Then
                lblBranch1.Visible = True
                ddlBranch.Visible = True
                SqlDataSource1.SelectCommand = "SELECT * FROM tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
            Else
                lblBranch1.Visible = False
                ddlBranch.Visible = False
                SqlDataSource1.SelectCommand = "SELECT * FROM tblBank order by BankID"
            End If

            conn1.Close()
            conn1.Dispose()
            ''''''''''''''''''''''''''''''''''''''''''
            'location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
            'SqlDataSource1.SelectCommand = "SELECT *  FROM tblBank order by BankID"



            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()
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

            'ddl.DataTextField = textField & " - " & valueField
            'ddl.DataValueField = textField & " - " & valueField
            ddl.DataBind()
            con.Close()
        End Using
        'End Using
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
                'command.CommandText = "SELECT X0104,  X0104Add, X0104Edit, X0104Delete, X0104Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0169,  x0169Add, x0169Edit, x0169Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()
                If dt.Rows.Count > 0 Then

                    If Not IsDBNull(dt.Rows(0)("x0169")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0169"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0169")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0169Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0169Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0169Add").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0169Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0169Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0169Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0169Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0169Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0169Delete").ToString()
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
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtBankID.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "ID CANNOT BE BLANK"
            Return
        End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlBranch.SelectedIndex = 0 Then
                lblAlert.Text = "PLEASE SELECT LOCATION"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblBank where BankID=@ID"
                command1.Parameters.AddWithValue("@id", txtBankID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtBankID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblBank(BankID, Bank, LedgerCode, RecvPrefix, PayvPrefix, LedgerPayvDist, LedgerRecvDist, Currency, Location,  CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@BankID, @Bank, @LedgerCode, @RecvPrefix, @PayvPrefix, @LedgerPayvDist, @LedgerRecvDist, @Currency, @Location, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@BankID", txtBankID.Text.ToUpper)
                        command.Parameters.AddWithValue("@Bank", txtBankName.Text.ToUpper)

                        command.Parameters.AddWithValue("@RecvPrefix", txtReceiptVoucherPrefix.Text.ToUpper)
                        command.Parameters.AddWithValue("@PayvPrefix", txtPaymentVoucherPrefix.Text.ToUpper)


                        If ddlBankLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerCode", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerCode", (ddlBankLedger.Text).ToUpper)
                        End If

                        If ddlPaymentVoucherLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerPayvDist", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerPayvDist", (ddlPaymentVoucherLedger.Text).ToUpper)
                        End If

                        If ddlReceiptVoucherLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerRecvDist", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerRecvDist", (ddlReceiptVoucherLedger.Text).ToUpper)
                        End If

                        If ddlCurrency.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Currency", "")
                        Else
                            command.Parameters.AddWithValue("@Currency", (ddlCurrency.Text).ToUpper)
                        End If

                        If ddlBranch.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", (ddlBranch.Text).ToUpper)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@BankID", txtBankID.Text)
                        command.Parameters.AddWithValue("@Bank", txtBankName.Text)

                        command.Parameters.AddWithValue("@RecvPrefix", txtReceiptVoucherPrefix.Text)
                        command.Parameters.AddWithValue("@PayvPrefix", txtPaymentVoucherPrefix.Text)


                        If ddlBankLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerCode", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerCode", (ddlBankLedger.Text))
                        End If

                        If ddlPaymentVoucherLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerPayvDist", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerPayvDist", (ddlPaymentVoucherLedger.Text))
                        End If

                        If ddlReceiptVoucherLedger.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@LedgerRecvDist", "")
                        Else
                            command.Parameters.AddWithValue("@LedgerRecvDist", (ddlReceiptVoucherLedger.Text).ToUpper)
                        End If

                        If ddlCurrency.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Currency", "")
                        Else
                            command.Parameters.AddWithValue("@Currency", (ddlCurrency.Text).ToUpper)
                        End If

                        If ddlBranch.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", (ddlBranch.Text).ToUpper)
                        End If

                        'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                        'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                    End If

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    ' MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BANK", txtBankID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BANK", txtBankID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
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
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblBank where BankId=@id and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@id", txtBankID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Industry already exists!!!", "str")
                    lblAlert.Text = "ID ALREADY EXISTS"
                    txtBankID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblBank where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        'If txtExists.Text = "True" Then
                        qry = "update tblBank set BankID=@BankID, Bank=@Bank, LedgerCode=@LedgerCode, RecvPrefix=@RecvPrefix, PayvPrefix=@PayvPrefix, LedgerPayvDist=@LedgerPayvDist, LedgerRecvDist=@LedgerRecvDist, currency = @currency, Location=@Location,  LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'Else
                        '    qry = "update tblBank set BankID, Bank, LedgerCode, RecvPrefix, PayvPrefix, LedgerPayvDist, LedgerRecvDist, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@BankID", txtBankID.Text.ToUpper)
                            command.Parameters.AddWithValue("@Bank", txtBankName.Text.ToUpper)

                            command.Parameters.AddWithValue("@RecvPrefix", txtReceiptVoucherPrefix.Text.ToUpper)
                            command.Parameters.AddWithValue("@PayvPrefix", txtPaymentVoucherPrefix.Text.ToUpper)


                            If ddlBankLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerCode", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerCode", (ddlBankLedger.Text).ToUpper)
                            End If

                            If ddlPaymentVoucherLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerPayvDist", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerPayvDist", (ddlPaymentVoucherLedger.Text).ToUpper)
                            End If

                            If ddlReceiptVoucherLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerRecvDist", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerRecvDist", (ddlReceiptVoucherLedger.Text).ToUpper)
                            End If

                            If ddlCurrency.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Currency", "")
                            Else
                                command.Parameters.AddWithValue("@Currency", (ddlCurrency.Text).ToUpper)
                            End If

                            If ddlBranch.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", (ddlBranch.Text).ToUpper)
                            End If
                            'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@BankID", txtBankID.Text)
                            command.Parameters.AddWithValue("@Description", txtBankName.Text)

                            command.Parameters.AddWithValue("@RecvPrefix", txtReceiptVoucherPrefix.Text)
                            command.Parameters.AddWithValue("@PayvPrefix", txtPaymentVoucherPrefix.Text)


                            If ddlBankLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerCode", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerCode", (ddlBankLedger.Text))
                            End If

                            If ddlPaymentVoucherLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerPayvDist", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerPayvDist", (ddlPaymentVoucherLedger.Text))
                            End If

                            If ddlReceiptVoucherLedger.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@LedgerRecvDist", "")
                            Else
                                command.Parameters.AddWithValue("@LedgerRecvDist", (ddlReceiptVoucherLedger.Text))
                            End If

                            If ddlCurrency.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Currency", "")
                            Else
                                command.Parameters.AddWithValue("@Currency", (ddlCurrency.Text).ToUpper)
                            End If

                            If ddlBranch.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", (ddlBranch.Text).ToUpper)
                            End If
                            'If ddlBankLedger.SelectedIndex = 0 Then
                            '    command.Parameters.AddWithValue("@MarketSegmentID", "")
                            '    command.Parameters.AddWithValue("@MarketSegmentDescription", "")
                            'Else
                            '    command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlBankLedger.Text, 3).ToUpper)
                            '    command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlBankLedger.Text, 6, 65).ToUpper)
                            'End If

                            'command.Parameters.AddWithValue("@MarketSegmentID", Left(ddlMarketSegmentID.Text, 3).ToUpper)
                            'command.Parameters.AddWithValue("@MarketSegmentDescription", Mid(ddlMarketSegmentID.Text, 6, 65).ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                        End If

                        command.Connection = conn

                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO INDUSTRY CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        'GridView1.DataSourceID = "SqlDataSource1"

        'SqlDataSource1.SelectCommand = "SELECT *  FROM tblBank order by BankID"
        
        If txtDisplayRecordsLocationwise.Text = "Y" Then
            lblBranch1.Visible = True
            ddlBranch.Visible = True
            SqlDataSource1.SelectCommand = "SELECT * FROM tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
        Else
            lblBranch1.Visible = False
            ddlBranch.Visible = False
            SqlDataSource1.SelectCommand = "SELECT * FROM tblBank order by BankID"
        End If
        GridView1.DataSourceID = "SqlDataSource1"
        GridView1.DataBind()
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

        If txtExists.Text = "True" Then
            txtBankID.Enabled = False
        Else
            txtBankID.Enabled = True
        End If

        txtReceiptVoucherPrefix.Enabled = False
        txtPaymentVoucherPrefix.Enabled = False
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

                command1.CommandText = "SELECT * FROM tblBank where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblBank where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INDUSTRY", txtBankID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()


            If txtDisplayRecordsLocationwise.Text = "Y" Then
                lblBranch1.Visible = True
                ddlBranch.Visible = True
                SqlDataSource1.SelectCommand = "SELECT * FROM tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
            Else
                lblBranch1.Visible = False
                ddlBranch.Visible = False
                SqlDataSource1.SelectCommand = "SELECT * FROM tblBank order by BankID"
            End If
            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            MakeMeNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Response.Redirect("RV_MasterBank.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblRecv where BankId=@data"
        command1.Parameters.AddWithValue("@data", txtBankID.Text)
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

    Protected Sub ddlMarketSegmentID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankLedger.SelectedIndexChanged

    End Sub
End Class
