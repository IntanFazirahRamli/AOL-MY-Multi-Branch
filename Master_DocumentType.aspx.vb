
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_DocumentType
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtDocumentDescription.Text = ""
        txtDocumentType.Text = ""
        ddlModule.SelectedIndex = 0
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        'txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        ChkSendEmail.Checked = False
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
        txtDocumentDescription.Enabled = False
        txtDocumentType.Enabled = False
        ddlModule.Enabled = False
        ChkSendEmail.Enabled = False
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

        txtDocumentDescription.Enabled = True
        txtDocumentType.Enabled = True
        ddlModule.Enabled = True
        ChkSendEmail.Enabled = True
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

        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtDocumentDescription.Text = ""
        Else

            txtDocumentDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDocumentType.Text = ""
        Else
            txtDocumentType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            ddlModule.SelectedIndex = 0
        Else
            ddlModule.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(4).Text = 1 Then
            ChkSendEmail.Checked = True
        Else
            ChkSendEmail.Checked = False
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
        ddlModule.Focus()

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
                'command.CommandText = "SELECT x0112,  x0112Add, x0112Edit, x0112Delete, x0112Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0180,  x0180Add, x0180Edit, x0180Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0180")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0180"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0180")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If


                    If Not IsDBNull(dt.Rows(0)("x0180Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0180Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0180Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0180Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0180Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0180Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0180Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0180Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0180Delete").ToString()
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
        If ddlModule.SelectedIndex = 0 Then
            ' MessageBox.Message.Alert(Page, "Country cannot be blank!!!", "str")
            lblAlert.Text = "MODULE CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tbldocumenttype where NoteID=@NoteID"
                'command1.Parameters.AddWithValue("@NoteID", txtDocumentDescription.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                '    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                '    lblAlert.Text = "RECORD ALREADY EXISTS"
                '    txtDocumentDescription.Focus()
                '    Exit Sub
                'Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblDocumentType(DocumentType,DocumentTypeDescription,module, SendEmail, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                qry = qry + "VALUES(@DocumentType,@DocumentTypeDescription,@module, @SendEmail, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@DocumentType", txtDocumentType.Text.ToUpper)
                    command.Parameters.AddWithValue("@DocumentTypeDescription", txtDocumentDescription.Text.ToUpper)
                    command.Parameters.AddWithValue("@Module", ddlModule.Text.ToUpper)
                    If ChkSendEmail.Checked = True Then
                        command.Parameters.AddWithValue("@SendEmail", True)
                    Else
                        command.Parameters.AddWithValue("@SendEmail", False)
                    End If
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))


                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@DocumentType", txtDocumentType.Text)
                    command.Parameters.AddWithValue("@DocumentTypeDescription", txtDocumentDescription.Text)
                    command.Parameters.AddWithValue("@Module", ddlModule.Text)
                    If ChkSendEmail.Checked = True Then
                        command.Parameters.AddWithValue("@SendEmail", True)
                    Else
                        command.Parameters.AddWithValue("@SendEmail", False)
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
                'End If
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
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtcountry.Text
                'ind = ind.Replace("'", "\\'")

                'Dim command2 As MySqlCommand = New MySqlCommand

                'command2.CommandType = CommandType.Text

                'command2.CommandText = "SELECT * FROM tblDocumentType where NoteID=@NoteID and rcno<>" & Convert.ToInt32(txtRcno.Text)
                'command2.Parameters.AddWithValue("@NoteID", txtDocumentDescription.Text)
                'command2.Connection = conn

                'Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                'Dim dt1 As New DataTable
                'dt1.Load(dr1)

                'If dt1.Rows.Count > 0 Then

                '    ' MessageBox.Message.Alert(Page, "Country already exists!!!", "str")
                '    lblAlert.Text = "NOTE ID ALREADY EXISTS"
                '    txtDocumentDescription.Focus()
                '    Exit Sub
                'Else

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT NoteId FROM tbldocumenttype where rcno=" & Convert.ToInt32(txtRcno.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String
                If txtExists.Text = "True" Then
                    qry = "update tblDocumentType set DocumentType=@DocumentType,DocumentTypeDescription=@DocumentTypeDescription, Module=@Module, SendEmail=@SendEmail, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                Else
                    qry = "update tblDocumentType set DocumentType=@DocumentType,DocumentTypeDescription=@DocumentTypeDescription,Module=@Module, SendEmail=@SendEmail, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                End If

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@DocumentTypeDescription", txtDocumentDescription.Text.ToUpper)
                    command.Parameters.AddWithValue("@DocumentType", txtDocumentType.Text.ToUpper)
                    command.Parameters.AddWithValue("@Module", ddlModule.Text.ToUpper)
                    If ChkSendEmail.Checked = True Then
                        command.Parameters.AddWithValue("@SendEmail", True)
                    Else
                        command.Parameters.AddWithValue("@SendEmail", False)
                    End If

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@DocumentTypeDescription", txtDocumentDescription.Text)
                    command.Parameters.AddWithValue("@DocumentType", txtDocumentType.Text)
                    command.Parameters.AddWithValue("@Module", ddlModule.Text)
                    If ChkSendEmail.Checked = True Then
                        command.Parameters.AddWithValue("@SendEmail", True)
                    Else
                        command.Parameters.AddWithValue("@SendEmail", False)
                    End If
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                End If

                command.Connection = conn

                command.ExecuteNonQuery()
                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                'If txtExists.Text = "True" Then
                '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so Country cannot be updated!!!", "str")
                '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO COUNTRY CANNOT BE UPDATED"
                '    lblAlert.Text = ""
                'Else
                '    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                '    lblAlert.Text = ""
                'End If
                'End If
                'End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "DOCUMENT TYPE", txtDocumentType.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "DOCUMENT TYPE", txtDocumentType.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        ' MakeMeNull()
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
            txtDocumentDescription.Enabled = False
        Else
            txtDocumentDescription.Enabled = True
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

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT NoteID FROM tblNotesTemplate where rcno=" & Convert.ToInt32(txtRcno.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tbldocumenttype where rcno=" & Convert.ToInt32(txtRcno.Text)

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                'End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "DOCUMENT TYPE", txtDocumentDescription.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterNotesTemplate.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblcompany where AddCountry=@Country or BillCountry=@Country"
        'command1.Parameters.AddWithValue("@Country", txtNoteID.Text)
        'command1.Connection = conn

        'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        'Dim dt1 As New DataTable
        'dt1.Load(dr1)

        'If dt1.Rows.Count > 0 Then
        '    Return True
        'End If

        'Dim command2 As MySqlCommand = New MySqlCommand

        'command2.CommandType = CommandType.Text

        'command2.CommandText = "SELECT * FROM tblperson where AddCountry=@Country or BillCountry=@Country"
        'command2.Parameters.AddWithValue("@Country", txtNoteID.Text)
        'command2.Connection = conn

        'Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        'Dim dt2 As New DataTable
        'dt2.Load(dr2)

        'If dt2.Rows.Count > 0 Then
        '    Return True
        'End If


        'conn.Close()

        'Return False
    End Function

End Class
