Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_SettleType
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtSettleType.Text = ""
        ' ddlbank.Text = ""
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        ddlBank.SelectedIndex = 0
        chkEnableReturn.Checked = False
        ddlLocation.SelectedIndex = 0
        txtDefaultReference.Text = ""
        txtcode.text = ""

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

        txtSettleType.Enabled = False
        ddlbank.Enabled = False

        chkEnableReturn.Enabled = False
        ddlLocation.Enabled = False
        txtDefaultReference.Enabled = False
        txtcode.enabled = False
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

        txtSettleType.Enabled = True
        ddlbank.Enabled = True
        chkEnableReturn.Enabled = True
        ddlLocation.Enabled = True
        txtDefaultReference.Enabled = True
        txtcode.enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If
        'EnableControls()

        MakeMeNull()
        Dim editindex As Integer = GridView1.SelectedIndex
        rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        txtRcno.Text = rcno.ToString()

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtSettleType.Text = ""
        Else

            txtSettleType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Or GridView1.SelectedRow.Cells(2).Text = "" Then
            txtCode.Text = ""
        Else
            txtCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            ddlBank.SelectedIndex = 0
        Else

            ddlBank.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
        '    chkEnableReturn.Checked = 0
        'Else




        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Or GridView1.SelectedRow.Cells(4).Text = "" Then
            ddlLocation.SelectedIndex = 0
        Else
            ddlLocation.SelectedValue = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If
        chkEnableReturn.Checked = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString

        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtDefaultReference.Text = ""
        Else

            txtDefaultReference.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
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
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()



        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtSettleType.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim query As String
            query = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT ShowReceiptOnScreenLoad, ReceiptRecordMaxRec,DisplayRecordsLocationWise,PostReceipt, ReceiptOnlyEditableByCreator FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
         
            conn.Close()
            conn.Dispose()
            commandServiceRecordMasterSetup.Dispose()
            dtServiceRecordMasterSetup.Dispose()
            drServiceRecordMasterSetup.Close()

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                ddlLocation.Visible = True
                Label24.Visible = True
                query = "Select LocationID from tblLocation order by LocationID "
                PopulateDropDownList(query, "LocationID", "LocationID", ddlLocation)
            Else
                ddlLocation.Visible = False
                Label24.Visible = False
            End If



        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Dim con As MySqlConnection = New MySqlConnection()
        Try
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
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString

            lblAlert.Text = exstr
            'updPnlMsg.Update()
            'InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, textField)
        End Try
        'End Using
    End Sub
    Private Sub AccessControl()
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "SELECT x0171,  x0171Add, x0171Edit, x0171Delete, x0171Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.CommandText = "SELECT x0171,  x0171Add, x0171Edit, x0171Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)
            conn.Close()

            If dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)("x0171")) Then
                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0171"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0171")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0171Add")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0171Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0171Add").ToString()
                    End If
                End If


                If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0171Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0171Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0171Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0171Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0171Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0171Delete").ToString()
                        End If
                    End If
                Else
                    Me.btnEdit.Enabled = False
                    Me.btnDelete.Enabled = False
                End If

                'If String.IsNullOrEmpty(dt.Rows(0)("x0171Print")) = False Then
                '    Me.btnDelete.Enabled = dt.Rows(0)("x0171Print").ToString()
                'End If

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

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If ddlLocation.SelectedIndex = 0 Then
                ' MessageBox.Message.Alert(Page, "SettleType cannot be blank!!!", "str")
                lblAlert.Text = "Location CANNOT BE BLANK"
                ddlLocation.Focus()
                Return
            End If
        End If

        If txtSettleType.Text = "" Then
            ' MessageBox.Message.Alert(Page, "SettleType cannot be blank!!!", "str")
            lblAlert.Text = "Settle Type CANNOT BE BLANK"
            txtSettleType.Focus()
            Return

        End If
        If txtCode.Text = "" Then
            ' MessageBox.Message.Alert(Page, "SettleType cannot be blank!!!", "str")
            lblAlert.Text = "Code CANNOT BE BLANK"
            txtCode.Focus()
            Return

        End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblSettleType where SettleType=@SettleType"
                command1.Parameters.AddWithValue("@SettleType", txtSettleType.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtSettleType.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblSettleType(SettleType, Code, defaultbank,EnableReturn, Location,  DefaultPaymentReference, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@SettleType, @Code, @defaultbank, @EnableReturn, @Location, @DefaultPaymentReference, @CreatedBy,  @CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@SettleType", txtSettleType.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Code", txtCode.Text.ToUpper.Trim)

                        If ddlbank.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@defaultbank", "")
                        Else
                            command.Parameters.AddWithValue("@defaultbank", ddlBank.SelectedValue.ToString.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@EnableReturn", chkEnableReturn.Checked)

                        If ddlLocation.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@DefaultPaymentReference", txtDefaultReference.Text.ToUpper)

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@SettleType", txtSettleType.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Code", txtCode.Text.Trim)

                        If ddlBank.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@defaultbank", "")
                        Else
                            command.Parameters.AddWithValue("@defaultbank", ddlBank.SelectedValue.ToString)
                        End If
                        command.Parameters.AddWithValue("@EnableReturn", chkEnableReturn.Checked)
                        If ddlLocation.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@DefaultPaymentReference", txtDefaultReference.Text.ToUpper)
                        'command.Parameters.AddWithValue("@country", ddlbank.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                End If
                conn.Close()

            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
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
                'ind = txtSettleType.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT SettleType FROM tblSettleType where SettleType=@SettleType and rcno <>" & Convert.ToInt32(txtRcno.Text) & " Limit 1"
                command2.Parameters.AddWithValue("@SettleType", txtSettleType.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "SettleType already exists!!!", "str")
                    lblAlert.Text = "SettleType ALREADY EXISTS"
                    txtSettleType.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblSettleType where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblSettleType set defaultbank=@defaultbank,EnableReturn=@EnableReturn, Location=@Location, DefaultPaymentReference=@DefaultPaymentReference, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        Else
                            qry = "update tblSettleType set SettleType=@SettleType, Code=@Code,defaultbank=@defaultbank, EnableReturn=@EnableReturn, Location =@Location,  DefaultPaymentReference=@DefaultPaymentReference, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@SettleType", txtSettleType.Text.ToUpper)
                            command.Parameters.AddWithValue("@Code", txtCode.Text.ToUpper.Trim)

                            If ddlBank.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@defaultbank", "")
                            Else
                                command.Parameters.AddWithValue("@defaultbank", ddlBank.SelectedValue.ToString.ToUpper)
                            End If
                            If ddlLocation.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@DefaultPaymentReference", txtDefaultReference.Text.ToUpper)
                            command.Parameters.AddWithValue("@EnableReturn", chkEnableReturn.Checked)
                             command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@SettleType", txtSettleType.Text.Trim)
                            command.Parameters.AddWithValue("@Code", txtCode.Text.Trim)

                            If ddlBank.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@defaultbank", "")
                            Else
                                command.Parameters.AddWithValue("@defaultbank", ddlBank.SelectedValue.ToString)
                            End If
                            command.Parameters.AddWithValue("@EnableReturn", chkEnableReturn.Checked)
                            If ddlLocation.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", ddlLocation.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@DefaultPaymentReference", txtDefaultReference.Text.ToUpper)
                            'command.Parameters.AddWithValue("@country", ddlbank.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so SettleType cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO SettleType CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SettleType", txtSettleType.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SettleType", txtSettleType.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, "Error!!!", "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        '   MakeMeNull()
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

                command1.CommandText = "SELECT * FROM tblSettleType where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblSettleType where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '   MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SettleType", txtSettleType.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterSettleType.aspx")



    End Sub

    Protected Sub ddlbank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBank.SelectedIndexChanged

    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT PaymentType FROM tblrecv where PaymentType=@SettleType limit 1"
        command1.Parameters.AddWithValue("@SettleType", txtSettleType.Text)
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


End Class
