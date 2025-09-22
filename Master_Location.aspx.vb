Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Location
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtLocationID.Text = ""
        txtMode.Text = ""
        txtRcno.Text = ""
        txtDescription.Text = ""

        txtCompanyName.Text = ""
        txtOfficeAddress.Text = ""
        txtBusinessRegNumber.Text = ""
        txtGSTNumber.Text = ""
        txtTelNumber.Text = ""
        txtFaxNumber.Text = ""
        txtWebsite.Text = ""
        txtEmail.Text = ""
        txtMobile.Text = ""
        txtInvoiceEmail.Text = ""

        txtBusinessEntityName.Text = ""
        txtBankName.Text = ""
        txtBankCode.Text = ""
        txtBranchCode.Text = ""
        txtBankAccountNo.Text = ""
        txtSWIFTCode.Text = ""
        txtAccountName.Text = ""

        ''txtCreatedBy.Text = ""
        'txtCreatedOn.Text = ""
        'txtLastModifiedBy.Text = ""
        'txtLastModifiedOn.Text = ""

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
        txtLocationID.Enabled = False
        txtDescription.Enabled = False

        txtCompanyName.Enabled = False
        txtOfficeAddress.Enabled = False
        txtBusinessRegNumber.Enabled = False
        txtGSTNumber.Enabled = False
        txtTelNumber.Enabled = False
        txtFaxNumber.Enabled = False
        txtWebsite.Enabled = False
        txtEmail.Enabled = False
        txtMobile.Enabled = False
        txtInvoiceEmail.Enabled = False

        txtBusinessEntityName.Enabled = False
        txtBankName.Enabled = False
        txtBankCode.Enabled = False
        txtBranchCode.Enabled = False
        txtBankAccountNo.Enabled = False
        txtSWIFTCode.Enabled = False
        txtAccountName.Enabled = False

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

        txtLocationID.Enabled = True
        txtDescription.Enabled = True

        txtCompanyName.Enabled = True
        txtOfficeAddress.Enabled = True
        txtBusinessRegNumber.Enabled = True
        txtGSTNumber.Enabled = True
        txtTelNumber.Enabled = True
        txtFaxNumber.Enabled = True
        txtWebsite.Enabled = True
        txtEmail.Enabled = True
        txtMobile.Enabled = True

        txtBusinessEntityName.Enabled = True
        txtBankName.Enabled = True
        txtBankCode.Enabled = True
        txtBranchCode.Enabled = True
        txtBankAccountNo.Enabled = True
        txtSWIFTCode.Enabled = True
        txtAccountName.Enabled = True
        txtInvoiceEmail.Enabled = True

        AccessControl()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If

        MakeMeNull()

        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtLocationID.Text = ""
        Else

            txtLocationID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else

            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

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

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tbllocation where locationid=@locationid"
        command1.Parameters.AddWithValue("@locationid", txtLocationID.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            txtCompanyName.Text = dt.Rows(0)("BranchName").ToString

            txtOfficeAddress.Text = dt.Rows(0)("OfficeAddress1").ToString
            txtBusinessRegNumber.Text = dt.Rows(0)("BusinessRegistrationNumber").ToString
            txtGSTNumber.Text = dt.Rows(0)("GSTNumber").ToString
            txtTelNumber.Text = dt.Rows(0)("TelephoneNumber").ToString
            txtFaxNumber.Text = dt.Rows(0)("FaxNumber").ToString
            txtWebsite.Text = dt.Rows(0)("Website").ToString
            txtEmail.Text = dt.Rows(0)("Email").ToString
            txtMobile.Text = dt.Rows(0)("Mobile").ToString
            txtInvoiceEmail.Text = dt.Rows(0)("InvoiceEmail").ToString

            txtBusinessEntityName.Text = dt.Rows(0)("BusinessEntityName").ToString
            txtBankName.Text = dt.Rows(0)("BankName").ToString
            txtBankCode.Text = dt.Rows(0)("BankCode").ToString
            txtBranchCode.Text = dt.Rows(0)("BranchCode").ToString
            txtBankAccountNo.Text = dt.Rows(0)("BankAccountNo").ToString
            txtSWIFTCode.Text = dt.Rows(0)("SWIFTCode").ToString
            txtAccountName.Text = dt.Rows(0)("AccountName").ToString
        End If

        command1.Dispose()
        dt.Clear()
        dr.Close()
        dt.Dispose()
        conn.Close()
        conn.Dispose()


    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtLocationID.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then

            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
        End If
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
                'command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete, X0109Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0175,  x0175Add, x0175Edit, x0175Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0175")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0175"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0175")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0175Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0175Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0175Add").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0175Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0175Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0175Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0175Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0175Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0175Delete").ToString()
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
        If txtLocationID.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Company Group cannot be blank!!!", "str")
            lblAlert.Text = "BRANCH/LOCATION ID CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tbllocation where locationid=@locationid"
                command1.Parameters.AddWithValue("@locationid", txtLocationID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtLocationID.Focus()
                    Exit Sub
                Else


                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tbllocation(locationid,location, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,BranchName,OfficeAddress1,OfficeAddress2,BusinessRegistrationNumber,GSTNumber,TelephoneNumber,FaxNumber,Website,Email,Mobile,InvoiceEmail,  BusinessEntityName, BankName, BankCode, BranchCode, BankAccountNo, SWIFTCode, AccountName)"
                    qry = qry + "VALUES(@locationid,@location, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@CompanyName,@OfficeAddress1,@OfficeAddress2,@BusinessRegistrationNumber,@GSTNumber,@TelephoneNumber,@FaxNumber,@Website,@Email,@Mobile,@InvoiceEmail,  @BusinessEntityName, @BankName, @BankCode, @BranchCode, @BankAccountNo, @SWIFTCode, @AccountName);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@locationid", txtLocationID.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@location", txtDescription.Text.ToUpper)

                        command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.ToUpper)
                        command.Parameters.AddWithValue("@OfficeAddress1", txtOfficeAddress.Text.ToUpper)
                        command.Parameters.AddWithValue("@OfficeAddress2", "")
                        command.Parameters.AddWithValue("@BusinessRegistrationNumber", txtBusinessRegNumber.Text.ToUpper)
                        command.Parameters.AddWithValue("@GSTNumber", txtGSTNumber.Text.ToUpper)
                        command.Parameters.AddWithValue("@TelephoneNumber", txtTelNumber.Text.ToUpper)
                        command.Parameters.AddWithValue("@FaxNumber", txtFaxNumber.Text.ToUpper)
                        command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.ToUpper)
                        command.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToUpper)
                        command.Parameters.AddWithValue("@InvoiceEmail", txtInvoiceEmail.Text.ToUpper)

                        command.Parameters.AddWithValue("@BusinessEntityName", txtBusinessEntityName.Text.ToUpper)
                        command.Parameters.AddWithValue("@BankName", txtBankName.Text.ToUpper)
                        command.Parameters.AddWithValue("@BankCode", txtBankCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@BranchCode", txtBranchCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@BankAccountNo", txtBankAccountNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@SWIFTCode", txtSWIFTCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@AccountName", txtAccountName.Text.ToUpper)
                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@locationid", txtLocationID.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@location", txtDescription.Text)

                        command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text)
                        command.Parameters.AddWithValue("@OfficeAddress1", txtOfficeAddress.Text)
                        command.Parameters.AddWithValue("@OfficeAddress2", "")
                        command.Parameters.AddWithValue("@BusinessRegistrationNumber", txtBusinessRegNumber.Text)
                        command.Parameters.AddWithValue("@GSTNumber", txtGSTNumber.Text)
                        command.Parameters.AddWithValue("@TelephoneNumber", txtTelNumber.Text)
                        command.Parameters.AddWithValue("@FaxNumber", txtFaxNumber.Text)
                        command.Parameters.AddWithValue("@Website", txtWebsite.Text)
                        command.Parameters.AddWithValue("@Email", txtEmail.Text)
                        command.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                        command.Parameters.AddWithValue("@InvoiceEmail", txtInvoiceEmail.Text)

                        command.Parameters.AddWithValue("@BusinessEntityName", txtBusinessEntityName.Text)
                        command.Parameters.AddWithValue("@BankName", txtBankName.Text)
                        command.Parameters.AddWithValue("@BankCode", txtBankCode.Text)
                        command.Parameters.AddWithValue("@BranchCode", txtBranchCode.Text)
                        command.Parameters.AddWithValue("@BankAccountNo", txtBankAccountNo.Text)
                        command.Parameters.AddWithValue("@SWIFTCode", txtSWIFTCode.Text)
                        command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()

            Catch ex As Exception
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
                lblAlert.Text = ex.Message
            End Try
            EnableControls()
            txtMode.Text = ""
        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            'If txtExists.Text = "True" Then
            '    '  MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE, SO CANNOT BE MODIFIED"
            '    Return
            'End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtIndustry.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tbllocation where locationid=@locationid and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@locationID", txtLocationID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Company Group already exists!!!", "str")
                    lblAlert.Text = "COMPANY GROUP ALREADY EXISTS"
                    txtLocationID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tbllocation where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tbllocation set LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn,location=@location,BranchName = @CompanyName,OfficeAddress1 = @OfficeAddress1,OfficeAddress2 = @OfficeAddress2,BusinessRegistrationNumber = @BusinessRegistrationNumber,GSTNumber = @GSTNumber,TelephoneNumber = @TelephoneNumber,FaxNumber = @FaxNumber,Website = @Website,Email = @Email,Mobile = @Mobile,InvoiceEmail=@InvoiceEmail,  BusinessEntityName=@BusinessEntityName, BankName=@BankName, BankCode=@BankCode, BranchCode=@BranchCode, BankAccountNo=@BankAccountNo, SWIFTCode=@SWIFTCode, AccountName=@AccountName where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tbllocation set locationid=@locationid,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn,location=@location,BranchName = @CompanyName,OfficeAddress1 = @OfficeAddress1,OfficeAddress2 = @OfficeAddress2,BusinessRegistrationNumber = @BusinessRegistrationNumber,GSTNumber = @GSTNumber,TelephoneNumber = @TelephoneNumber,FaxNumber = @FaxNumber,Website = @Website,Email = @Email,Mobile = @Mobile,InvoiceEmail=@InvoiceEmail, BusinessEntityName=@BusinessEntityName, BankName=@BankName, BankCode=@BankCode, BranchCode=@BranchCode, BankAccountNo=@BankAccountNo, SWIFTCode=@SWIFTCode, AccountName=@AccountName where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()
                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@locationid", txtLocationID.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@location", txtDescription.Text.ToUpper)

                            command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.ToUpper)
                            command.Parameters.AddWithValue("@OfficeAddress1", txtOfficeAddress.Text.ToUpper)
                            command.Parameters.AddWithValue("@OfficeAddress2", "")
                            command.Parameters.AddWithValue("@BusinessRegistrationNumber", txtBusinessRegNumber.Text.ToUpper)
                            command.Parameters.AddWithValue("@GSTNumber", txtGSTNumber.Text.ToUpper)
                            command.Parameters.AddWithValue("@TelephoneNumber", txtTelNumber.Text.ToUpper)
                            command.Parameters.AddWithValue("@FaxNumber", txtFaxNumber.Text.ToUpper)
                            command.Parameters.AddWithValue("@Website", txtWebsite.Text.ToUpper)
                            command.Parameters.AddWithValue("@Email", txtEmail.Text.ToUpper)
                            command.Parameters.AddWithValue("@Mobile", txtMobile.Text.ToUpper)
                            command.Parameters.AddWithValue("@InvoiceEmail", txtInvoiceEmail.Text.ToUpper)

                            command.Parameters.AddWithValue("@BusinessEntityName", txtBusinessEntityName.Text.ToUpper)
                            command.Parameters.AddWithValue("@BankName", txtBankName.Text.ToUpper)
                            command.Parameters.AddWithValue("@BankCode", txtBankCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@BranchCode", txtBranchCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@BankAccountNo", txtBankAccountNo.Text.ToUpper)
                            command.Parameters.AddWithValue("@SWIFTCode", txtSWIFTCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@AccountName", txtAccountName.Text.ToUpper)

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@locationid", txtLocationID.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@location", txtDescription.Text)

                            command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text)
                            command.Parameters.AddWithValue("@OfficeAddress1", txtOfficeAddress.Text)
                            command.Parameters.AddWithValue("@OfficeAddress2", "")
                            command.Parameters.AddWithValue("@BusinessRegistrationNumber", txtBusinessRegNumber.Text)
                            command.Parameters.AddWithValue("@GSTNumber", txtGSTNumber.Text)
                            command.Parameters.AddWithValue("@TelephoneNumber", txtTelNumber.Text)
                            command.Parameters.AddWithValue("@FaxNumber", txtFaxNumber.Text)
                            command.Parameters.AddWithValue("@Website", txtWebsite.Text)
                            command.Parameters.AddWithValue("@Email", txtEmail.Text)
                            command.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                            command.Parameters.AddWithValue("@InvoiceEmail", txtInvoiceEmail.Text)

                            command.Parameters.AddWithValue("@BusinessEntityName", txtBusinessEntityName.Text)
                            command.Parameters.AddWithValue("@BankName", txtBankName.Text)
                            command.Parameters.AddWithValue("@BankCode", txtBankCode.Text)
                            command.Parameters.AddWithValue("@BranchCode", txtBranchCode.Text)
                            command.Parameters.AddWithValue("@BankAccountNo", txtBankAccountNo.Text)
                            command.Parameters.AddWithValue("@SWIFTCode", txtSWIFTCode.Text)
                            command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                        End If

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
                End If

                conn.Close()

                txtMode.Text = ""

                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCATION", txtLocationID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCATION", txtLocationID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                lblAlert.Text = ex.Message
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

        If txtExists.Text = "True" Then
            txtLocationID.Enabled = False
        Else
            txtLocationID.Enabled = True
        End If

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

            If txtExists.Text = "True" Then
                ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE DELETED"
                Return
            End If


            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblLocation where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblLocation where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOCATION", txtLocationID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterLocation.aspx")
    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcompany where location=@data"
        command1.Parameters.AddWithValue("@data", txtLocationID.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text

        command2.CommandText = "SELECT * FROM tblperson where location=@data"
        command2.Parameters.AddWithValue("@data", txtLocationID.Text)
        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        Dim dt2 As New DataTable
        dt2.Load(dr2)

        If dt2.Rows.Count > 0 Then
            Return True
        End If

        'Dim command3 As MySqlCommand = New MySqlCommand

        'command3.CommandType = CommandType.Text

        'command3.CommandText = "SELECT * FROM tblcontract where companygroup=@data"
        'command3.Parameters.AddWithValue("@data", txtLocationID.Text)
        'command3.Connection = conn

        'Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        'Dim dt3 As New DataTable
        'dt3.Load(dr3)

        'If dt3.Rows.Count > 0 Then
        '    Return True
        'End If

        'Dim command4 As MySqlCommand = New MySqlCommand

        'command4.CommandType = CommandType.Text

        'command4.CommandText = "SELECT * FROM tblservicerecord where companygroup=@data"
        'command4.Parameters.AddWithValue("@data", txtLocationID.Text)
        'command4.Connection = conn

        'Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        'Dim dt4 As New DataTable
        'dt4.Load(dr4)

        'If dt4.Rows.Count > 0 Then
        '    Return True
        'End If
        conn.Close()

        Return False
    End Function

End Class

