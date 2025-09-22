Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_PestGender
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        txtMode.Text = ""
        ddlPestID.SelectedIndex = 0
        txtGender.Text = ""
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        lblAlert.Text = ""
        
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
        ddlPestID.Enabled = False
        txtGender.Enabled = False
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

        ddlPestID.Enabled = True
        txtGender.Enabled = True
        AccessControl()
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
                cmd.Dispose()
            End Using
            'End Using


        Catch ex As Exception
            InsertIntoTblWebEventLog("PopulateDropDownList", ex.Message.ToString, query + " , " + TryCast(ddl, DropDownList).Text + " , " + " , " + textField + " , " + valueField)
        End Try
    End Sub

    Private Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "PESTGENDER - " + txtCreatedBy.Text)
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
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
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

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim sql As String
        sql = "Select * FROM tblpestGender "
        sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = sql
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(dr)
        If dt.Rows.Count > 0 Then

            If dt.Rows(0)("PestID").ToString <> "" Then : ddlPestID.Text = dt.Rows(0)("PestID").ToString : End If
            If dt.Rows(0)("Gender").ToString <> "" Then : txtGender.Text = dt.Rows(0)("Gender").ToString : End If

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

        'If CheckIfExists() = True Then
        '    txtExists.Text = "True"

        'Else
        '    txtExists.Text = "False"

        'End If

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
        ddlPestID.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim qry As String
            qry = " SELECT PestID FROM tblPest  ORDER BY PestID"
            PopulateDropDownList(qry, "PestID", "PestID", ddlPestID)

        End If
    End Sub

    'Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
    '    Try
    '        Dim con As MySqlConnection = New MySqlConnection()

    '        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        'Using con As New MySqlConnection(constr)
    '        Using cmd As New MySqlCommand(query)
    '            cmd.CommandType = CommandType.Text
    '            cmd.Connection = con
    '            con.Open()
    '            ddl.DataSource = cmd.ExecuteReader()
    '            ddl.DataTextField = textField.Trim()
    '            ddl.DataValueField = valueField.Trim()
    '            ddl.DataBind()
    '            con.Close()
    '            con.Dispose()
    '            cmd.Dispose()
    '        End Using
    '        'End Using
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, valueField & txtPestID.Text)
    '        Exit Sub
    '    End Try
    'End Sub

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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
        Catch ex As Exception
            Exit Sub
        End Try
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
                command.CommandText = "SELECT x0187,  x0187Add, x0187Edit, x0187Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0187"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0187")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If


                    If String.IsNullOrEmpty(dt.Rows(0)("x0187Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0187Add").ToString()
                    End If

                    'Me.btnInsert.Enabled = vpSec2412Add
                    If txtMode.Text = "View" Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0187Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0187Edit").ToString()
                        End If

                        If String.IsNullOrEmpty(dt.Rows(0)("x0187Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0187Delete").ToString()
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
        If ddlPestID.Text = "" Then
            '   MessageBox.Message.Alert(Page, "Group cannot be blank!!!", "str")
            lblAlert.Text = "PEST ID CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblpestgender where PesTID =@PesTID and Gender=@Gender"
                command1.Parameters.AddWithValue("@PesTID", ddlPestID.Text)
                command1.Parameters.AddWithValue("@Gender", txtGender.Text)

                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    ddlPestID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblpestGender (PestID, Gender, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@PestID,@Gender, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@PestID", ddlPestID.Text.ToUpper)
                        command.Parameters.AddWithValue("@Gender", txtGender.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@PestID", ddlPestID.Text)
                        command.Parameters.AddWithValue("@Gender", txtGender.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                    'MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
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

            If CheckIfExists() = True Then
                txtExists.Text = "True"

            Else
                txtExists.Text = "False"

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

                command2.CommandText = "SELECT * FROM tblpestGender where PestID=@PestID and Gender=@Gender and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@PestID", ddlPestID.Text)
                command2.Parameters.AddWithValue("@Gender", txtGender.Text)

                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Contract Group already exists!!!", "str")
                    lblAlert.Text = "PEST ALREADY EXISTS"

                    ddlPestID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblPestGender where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblpestGender set Gender=@Gender, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblpestGender set PestID=@PestID, Gender=@Gender,  LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@PestID", ddlPestID.Text.ToUpper)
                            command.Parameters.AddWithValue("@Gender", txtGender.Text.ToUpper)
                            'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@PestID", ddlPestID.Text)
                            command.Parameters.AddWithValue("@Gender", txtGender.Text)
                            'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If


                        command.Connection = conn

                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                        'If txtExists.Text = "True" Then
                        '    'MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so Contract Group cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED "
                        '    lblAlert.Text = ""
                        'Else
                        '    'MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PESTGENDER", ddlPestID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PESTGENDER", ddlPestID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        MakeMeNull()

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtRcno.Text = "" Then
            '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"

            Return

        End If
        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

        If txtExists.Text = "True" Then
            ddlPestID.Enabled = False
            'ddlCategory.Enabled = False
        Else
            ddlPestID.Enabled = True
            'ddlCategory.Enabled = True
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

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            If txtRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
                lblAlert.Text = "SELECT RECORD TO DELETE"

                Return

            End If
            lblMessage.Text = "ACTION: DELETE RECORD"

            '   MessageBox.Message.Confirm(Page, "Do you want to delete the selected record?", "str", vbYesNo)
            If txtExists.Text = "True" Then
                '   MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
                lblAlert.Text = "RECORD IS IN USE, CANNOT BE MODIFIED"
                Return
            End If



            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblpestGender where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblpestGender where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "PESTGENDER", ddlPestID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        'Response.Redirect("RV_MasterContractGroup.aspx")

    End Sub

    Private Function CheckIfExists() As Boolean
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()
        'Dim command3 As MySqlCommand = New MySqlCommand

        'command3.CommandType = CommandType.Text

        'command3.CommandText = "SELECT contractgroup FROM tblcontract where contractgroup=@data"
        'command3.Parameters.AddWithValue("@data", txtPestID.Text)
        'command3.Connection = conn

        'Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        'Dim dt3 As New DataTable
        'dt3.Load(dr3)

        'If dt3.Rows.Count > 0 Then
        '    Return True
        'End If

        'conn.Close()

        'Return False
    End Function

    Protected Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtGender.TextChanged

    End Sub
End Class
