Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing

Partial Class EmailSetup
    Inherits System.Web.UI.Page

    Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtSetupId.Text = ""
        txtModule.Text = ""
        txtTo.Text = ""
        txtBCC.Text = ""
        txtCC.Text = ""
        txtSender.Text = ""
        txtRcno.Text = ""

        txtCategory.Text = ""
        txtSubject.Text = ""
        txtRemarks.Text = ""
        txtAttachments.Text = ""
        txtContents.Text = ""
        chkDefaultEmail.Checked = False
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
        txtSetupId.Enabled = False
        txtModule.Enabled = False
        txtTo.Enabled = False
        txtBCC.Enabled = False
        txtCC.Enabled = False
        txtSender.Enabled = False

        txtCategory.Enabled = False
        txtSubject.Enabled = False
        txtRemarks.Enabled = False
        txtAttachments.Enabled = False
        txtContents.Enabled = False
        chkDefaultEmail.Enabled = False

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

        txtSetupId.Enabled = True
        txtModule.Enabled = True
        txtTo.Enabled = True
        txtBCC.Enabled = True
        txtCC.Enabled = True
        txtSender.Enabled = True

        txtCategory.Enabled = True
        txtSubject.Enabled = True
        txtRemarks.Enabled = True
        txtAttachments.Enabled = True
        txtContents.Enabled = True
        chkDefaultEmail.Enabled = True

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
            txtSetupId.Text = ""
        Else
            txtSetupId.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtCategory.Text = ""
        Else
            txtCategory.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
     
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtTo.Text = ""
        Else
            txtTo.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            txtCC.Text = ""
        Else
            txtCC.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            txtBCC.Text = ""
        Else
            txtBCC.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtAttachments.Text = ""
        Else
            txtAttachments.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If


        'If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
        '    txtContents.Text = ""
        'Else
        '    txtContents.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        'End If

        If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            txtRemarks.Text = ""
        Else
            txtRemarks.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            txtModule.Text = ""
        Else
            txtModule.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(10).Text = "&nbsp;" Then
            txtSubject.Text = ""
        Else
            txtSubject.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(11).Text = "&nbsp;" Then
            txtSender.Text = ""
        Else
            txtSender.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(11).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(12).Text = "1" Then
            chkDefaultEmail.Checked = True
        Else
            chkDefaultEmail.Checked = False
        End If


        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT Contents FROM tblEmailSetup where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Contents").ToString <> "" Then : txtContents.Text = dt.Rows(0)("Contents").ToString : End If

            'If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            '    txtContents.Text = ""
            'Else
            '    txtContents.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
            'End If

            lblWordCount.Text = txtContents.Text.Count.ToString

        End If
        txtMode.Text = "View"

        'txtMode.Text = "Edit"
        '  DisableControls()
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
        txtSetupId.Focus()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                'command.CommandText = "SELECT x0110,  x0110Add, x0110Edit, x0110Delete, x0110Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0110,  x0110Add, x0110Edit, x0110Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then

                    If Not IsDBNull(dt.Rows(0)("x0110")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0110"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0110")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If


                    If Not IsDBNull(dt.Rows(0)("x0110Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0110Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0110Add").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0110Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0110Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0110Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0110Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0110Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0110Delete").ToString()
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
        If txtSetupId.Text.Trim = "" Then
            '  MessageBox.Message.Alert(Page, "Product ID cannot be blank!!!", "str")
            lblAlert.Text = "SETUP ID CANNOT BE BLANK"
            Return
        End If
        'If txtModule.Text.Trim = "" Then
        '    ' MessageBox.Message.Alert(Page, "Product Description cannot be blank!!!", "str")
        '    lblAlert.Text = "PRODUCT DESCRIPTION CANNOT BE BLANK"
        '    Return
        'End If

        '#Region "NewRecord"
        lblWordCount.Text = txtContents.Text.Count.ToString

        If txtContents.Text.Count > 10000 Then
            lblAlert.Text = "MAXIMUM SIZE ALLOWED FOR CONTENTS IS 10000."
            Return

        End If

        'txtTo.Text = ValidateEmail(txtTo.Text)
        'If txtTo.Text.Last.ToString = ";" Then
        '    txtTo.Text = txtTo.Text.Remove(txtTo.Text.Length - 1)

        'End If

        'If txtTo.Text.First.ToString = ";" Then
        '    txtTo.Text = txtTo.Text.Remove(0)

        'End If

        'If String.IsNullOrEmpty(txtCC.Text) = False Then
        '    txtCC.Text = ValidateEmail(txtCC.Text)
        '    If txtCC.Text.Last.ToString = ";" Then
        '        txtCC.Text = txtCC.Text.Remove(txtCC.Text.Length - 1)

        '    End If
        '    If txtCC.Text.First.ToString = ";" Then
        '        txtCC.Text = txtCC.Text.Remove(0)

        '    End If
        'End If

        'If String.IsNullOrEmpty(txtBCC.Text) = False Then
        '    txtBCC.Text = ValidateEmail(txtBCC.Text)
        '    If txtBCC.Text.Last.ToString = ";" Then
        '        txtBCC.Text = txtBCC.Text.Remove(txtBCC.Text.Length - 1)

        '    End If
        '    If txtBCC.Text.First.ToString = ";" Then
        '        txtBCC.Text = txtBCC.Text.Remove(0)

        '    End If
        'End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT * FROM tblEmailSetup where SetupID=@SetupId"
                command1.Parameters.AddWithValue("@SetupId", txtSetupId.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtSetupId.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblEmailSetup(SetupId,Module,Receiver, ReceiverCC, ReceiverBCC, Sender,Category,Subject,Remarks,Attachments,Contents,DefaultEmail, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@SetupId,@Module,@To,@CC,@BCC,@Sender,@Category,@Subject,@Remarks,@Attachments,@Contents,@DefaultEmail, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@SetupId", txtSetupId.Text.ToUpper)
                        command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                        command.Parameters.AddWithValue("@To", txtTo.Text.ToUpper)
                        command.Parameters.AddWithValue("@CC", txtCC.Text.ToUpper)
                        command.Parameters.AddWithValue("@BCC", txtBCC.Text.ToUpper)
                        command.Parameters.AddWithValue("@Sender", txtSender.Text.ToUpper)
                        command.Parameters.AddWithValue("@Category", txtCategory.Text.ToUpper)
                        command.Parameters.AddWithValue("@Subject", txtSubject.Text.ToUpper)
                        command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToUpper)
                        command.Parameters.AddWithValue("@Attachments", txtAttachments.Text.ToUpper)
                        command.Parameters.AddWithValue("@Contents", txtContents.Text)

                        If chkDefaultEmail.Checked = True Then
                            command.Parameters.AddWithValue("@DefaultEmail", True)
                        Else
                            command.Parameters.AddWithValue("@DefaultEmail", False)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@SetupId", txtSetupId.Text.ToUpper)
                        command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                        command.Parameters.AddWithValue("@To", txtTo.Text.ToUpper)
                        command.Parameters.AddWithValue("@CC", txtCC.Text)
                        command.Parameters.AddWithValue("@BCC", txtBCC.Text)
                        command.Parameters.AddWithValue("@Sender", txtSender.Text)
                        command.Parameters.AddWithValue("@Category", txtCategory.Text)
                        command.Parameters.AddWithValue("@Subject", txtSubject.Text)
                        command.Parameters.AddWithValue("@Remarks", txtRemarks.Text)
                        command.Parameters.AddWithValue("@Attachments", txtAttachments.Text)
                        command.Parameters.AddWithValue("@Contents", txtContents.Text)

                        If chkDefaultEmail.Checked = True Then
                            command.Parameters.AddWithValue("@DefaultEmail", True)
                        Else
                            command.Parameters.AddWithValue("@DefaultEmail", False)
                        End If

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
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""

        ElseIf txtMode.Text = "Edit" Then
            If txtRcno.Text = "" Then
                '      MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblEmailsetup where SetupID=@SetupId and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@SetupId", txtSetupId.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Product already exists!!!", "str")
                    lblAlert.Text = "EMAIL SETUP ALREADY EXISTS"
                    txtSetupId.Focus()
                    Exit Sub
                Else
                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text
                    command1.CommandText = "SELECT * FROM tblEmailSetup where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        'SetupId,Module,To, CC, BCC, Sender,Category,Subject,Remarks,Attachments,Contents,DefaultEmail
                        'If txtExists.Text = "True" Then
                        '    qry = "update tblEmailSetup set SetupId=@SetupId,Module=@Module,To=@To,CC=@CC, BCC=@BCC,Sender=@Sender,Category=@Category,Subject=@Subject, Remarks=@Remarks,Attachments=@Attachments,Contents=@Contents,DefaultEmail=@DefaultEmail, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'Else
                        '    qry = "update tblProduct set ProductID=@ProductID,ProductDesc=@Description,EstimateValue=@EstimateValue,Action=@Action,CostValue=@CostValue,Target=@Target,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'End If
                        qry = "update tblEmailSetup set SetupId=@SetupId,Module=@Module,Receiver=@To,ReceiverCC=@CC, ReceiverBCC=@BCC,Sender=@Sender,Category=@Category,Subject=@Subject, Remarks=@Remarks,Attachments=@Attachments,Contents=@Contents,DefaultEmail=@DefaultEmail, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()
                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@SetupId", txtSetupId.Text.ToUpper)
                            command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                            command.Parameters.AddWithValue("@To", txtTo.Text.ToUpper)
                            command.Parameters.AddWithValue("@CC", txtCC.Text.ToUpper)
                            command.Parameters.AddWithValue("@BCC", txtBCC.Text.ToUpper)
                            command.Parameters.AddWithValue("@Sender", txtSender.Text.ToUpper)
                            command.Parameters.AddWithValue("@Category", txtCategory.Text.ToUpper)
                            command.Parameters.AddWithValue("@Subject", txtSubject.Text.ToUpper)
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToUpper)
                            command.Parameters.AddWithValue("@Attachments", txtAttachments.Text.ToUpper)
                            command.Parameters.AddWithValue("@Contents", txtContents.Text)

                            If chkDefaultEmail.Checked = True Then
                                command.Parameters.AddWithValue("@DefaultEmail", True)
                            Else
                                command.Parameters.AddWithValue("@DefaultEmail", False)
                            End If
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@SetupId", txtSetupId.Text.ToUpper)
                            command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                            command.Parameters.AddWithValue("@To", txtTo.Text.ToUpper)
                            command.Parameters.AddWithValue("@CC", txtCC.Text)
                            command.Parameters.AddWithValue("@BCC", txtBCC.Text)
                            command.Parameters.AddWithValue("@Sender", txtSender.Text)
                            command.Parameters.AddWithValue("@Category", txtCategory.Text)
                            command.Parameters.AddWithValue("@Subject", txtSubject.Text)
                            command.Parameters.AddWithValue("@Remarks", txtRemarks.Text)
                            command.Parameters.AddWithValue("@Attachments", txtAttachments.Text)
                            command.Parameters.AddWithValue("@Contents", txtContents.Text)

                            If chkDefaultEmail.Checked = True Then
                                command.Parameters.AddWithValue("@DefaultEmail", True)
                            Else
                                command.Parameters.AddWithValue("@DefaultEmail", False)
                            End If

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If
                        command.Connection = conn
                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO SETUP ID CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If
                    End If
                End If

                conn.Close()
                lblWordCount.Text = txtContents.Text.Count.ToString

                '#End Region
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

    Protected Function ValidateEmail(ByVal EmailId As String) As String
        Dim resEmail As String = ""
        If EmailId.Contains(","c) Then EmailId = EmailId.Replace(","c, ";"c)
        If EmailId.Contains("/"c) Then EmailId = EmailId.Replace("/"c, ";"c)
        If EmailId.Contains(":"c) Then EmailId = EmailId.Replace(":"c, ";"c)
        resEmail = EmailId.TrimEnd(";"c)
        Return resEmail
    End Function

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

                command1.CommandText = "SELECT * FROM tblEmailSetup where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblEmailSetup where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry
                    command.Connection = conn
                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
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
        Response.Redirect("RV_MasterEmailSetUp.aspx")
    End Sub

    Private Function CheckIfExists() As Boolean
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()


        'Dim command3 As MySqlCommand = New MySqlCommand

        'command3.CommandType = CommandType.Text

        'command3.CommandText = "SELECT * FROM tblcontractdet where ServiceID=@data"
        'command3.Parameters.AddWithValue("@data", txtSetupId.Text)
        'command3.Connection = conn

        'Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        'Dim dt3 As New DataTable
        'dt3.Load(dr3)

        'If dt3.Rows.Count > 0 Then
        '    Return True
        'End If

        'Dim command4 As MySqlCommand = New MySqlCommand

        'command4.CommandType = CommandType.Text

        'command4.CommandText = "SELECT * FROM tblservicerecorddet where ServiceID=@data"
        'command4.Parameters.AddWithValue("@data", txtSetupId.Text)
        'command4.Connection = conn

        'Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        'Dim dt4 As New DataTable
        'dt4.Load(dr4)

        'If dt4.Rows.Count > 0 Then
        '    Return True
        'End If
        'conn.Close()

        'Return False
    End Function

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
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Protected Sub btnViewCount_Click(sender As Object, e As EventArgs) Handles btnViewCount.Click
        lblWordCount.Text = txtContents.Text.Count.ToString

    End Sub
End Class