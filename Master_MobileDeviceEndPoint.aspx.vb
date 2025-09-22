

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Imports System.Drawing

Partial Class Master_MobileDeviceEndPoint
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtCountry.Text = ""
        txtWebServiceURL.Text = ""
        chkActive.Checked = False

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        'txtCreatedOn.Text = ""
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
        txtCountry.Enabled = False
        txtWebServiceURL.Enabled = False
        chkActive.Enabled = False

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

        txtCountry.Enabled = True
        txtWebServiceURL.Enabled = True
        chkActive.Enabled = True


        AccessControl()

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
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

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
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
            txtCountry.Text = ""
        Else

            txtCountry.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtWebServiceURL.Text = ""
        Else
            txtWebServiceURL.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        'InsertIntoTblWebEventLog("Gridview1", GridView1.SelectedRow.Cells(3).Text, DirectCast(GridView1.Rows(editindex).FindControl("chkActive"), CheckBox).Text)

        If GridView1.SelectedRow.Cells(3).Text = "Y" Then
            chkActive.Checked = True
        Else
            chkActive.Checked = False
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

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "EndPointMaster")
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
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()



        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtCountry.Focus()

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
        Return

        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT x0112,  x0112Add, x0112Edit, x0112Delete, x0112Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0112,  x0112Add, x0112Edit, x0112Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0112")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0112"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0112")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If


                    If Not IsDBNull(dt.Rows(0)("x0112Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0112Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0112Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0112Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0112Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0112Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0112Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0112Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0112Delete").ToString()
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
        If txtCountry.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Country cannot be blank!!!", "str")
            lblAlert.Text = "COUNTRY CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblmobiledevicecountry where country=@country"
                command1.Parameters.AddWithValue("@country", txtCountry.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtCountry.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    'Dim qry As String = "INSERT INTO tblMobileDeviceCountry(country,WebServiceURL,Active,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,countrycode,WS)"
                    'qry = qry + "VALUES(@country,@WebServiceURL,@Active,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@countrycode,@WS);"

                    Dim qry As String = "INSERT INTO tblMobileDeviceCountry(country,WebServiceURL,Active,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@country,@WebServiceURL,@Active,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@country", txtCountry.Text.ToUpper)
                        command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text)
                        If chkActive.Checked = True Then
                            command.Parameters.AddWithValue("@Active", "Y")
                        Else
                            command.Parameters.AddWithValue("@Active", "N")
                        End If
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", txtCountryCode.Text.ToUpper)
                        'command.Parameters.AddWithValue("@WS", txtWS.Text.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@country", txtCountry.Text.ToUpper)
                        command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text)
                        If chkActive.Checked = True Then
                            command.Parameters.AddWithValue("@Active", "Y")
                        Else
                            command.Parameters.AddWithValue("@Active", "N")
                        End If
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", txtCountryCode.Text)
                        'command.Parameters.AddWithValue("@WS", txtWS.Text)


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
                lblAlert.Text = ex.Message.ToString
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblmobiledevicecountry where country=@country and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@country", txtCountry.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Country already exists!!!", "str")
                    lblAlert.Text = "COUNTRY ALREADY EXISTS"
                    txtCountry.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblmobiledevicecountry where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        'If txtExists.Text = "True" Then
                        '    qry = "update tblmobiledevicecountry set webserviceurl=@webserviceurl,Active=@Active,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'Else
                        '    qry = "update tblmobiledevicecountry set country=@country,webserviceurl=@webserviceurl,Active=@Active,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        'End If

                        If txtExists.Text = "True" Then
                            qry = "update tblmobiledevicecountry set webserviceurl=@webserviceurl,Active=@Active,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblmobiledevicecountry set country=@country,webserviceurl=@webserviceurl,Active=@Active,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@country", txtCountry.Text.ToUpper)
                            command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text)
                            If chkActive.Checked = True Then
                                command.Parameters.AddWithValue("@Active", "Y")
                            Else
                                command.Parameters.AddWithValue("@Active", "N")
                            End If
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@countrycode", txtCountryCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@WS", txtWS.Text.ToUpper)

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@country", txtCountry.Text.ToUpper)
                            command.Parameters.AddWithValue("@WebServiceURL", txtWebServiceURL.Text)
                            If chkActive.Checked = True Then
                                command.Parameters.AddWithValue("@Active", "Y")
                            Else
                                command.Parameters.AddWithValue("@Active", "N")
                            End If
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@countrycode", txtCountryCode.Text)
                            command.Parameters.AddWithValue("@WS", txtWS.Text)


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
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILECOUNTRY", txtCountry.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILECOUNTRY", txtCountry.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
                'MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        ' MakeMeNull()

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
            txtCountry.Enabled = False
        Else
            txtCountry.Enabled = True
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

        If txtExists.Text = "True" Then
            txtCountry.Enabled = False
        Else
            txtCountry.Enabled = True
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

                command1.CommandText = "SELECT * FROM tblmobiledevicecountry where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblmobiledevicecountry where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MOBILECOUNTRY", txtCountry.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterCountry.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblMOBILEDEVICE where Country=@Country"
        command1.Parameters.AddWithValue("@Country", txtCountry.Text)
        command1.Connection = conn

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then
            Return True
        End If

      
        'Dim command4 As MySqlCommand = New MySqlCommand

        'command4.CommandType = CommandType.Text

        'command4.CommandText = "SELECT * FROM tblservicerecord where CustAddCountry=@Country or AddCountry=@Country"
        'command4.Parameters.AddWithValue("@Country", txtCountry.Text)
        'command4.Connection = conn

        'Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        'Dim dt4 As New DataTable
        'dt4.Load(dr4)

        'If dt4.Rows.Count > 0 Then
        '    Return True
        'End If

    
        conn.Close()
        command1.Dispose()

        dt1.Dispose()
       

        Return False
    End Function


    Protected Sub OnRowDataBoundg1(sender As Object, e As GridViewRowEventArgs)
      
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If

    End Sub

    Protected Sub OnSelectedIndexChangedg1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            For Each row As GridViewRow In GridView1.Rows
                'If row.RowIndex = GridView1.SelectedIndex Then
                '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
                '    row.ToolTip = String.Empty
                'Else
                '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                '    row.ToolTip = "Click to select this row."
                'End If

                If row.RowIndex = GridView1.SelectedIndex Then
                    row.BackColor = ColorTranslator.FromHtml("#AEE4FF")
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
        Catch ex As Exception
            '  InsertIntoTblWebEventLog("OnSelectedIndexChangedg1", ex.Message.ToString, txtRcno.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()

        txt.Text = "SELECT * FROM tblmobiledevicecountry where rcno<>0"
        SqlDataSource1.SelectCommand = "SELECT * FROM tblmobiledevicecountry where rcno<>0"
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub



    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click
        txtSearchText.Text = txtSearchEndPoint.Text

        If txtSearchEndPoint.Text <> "Search Here" Then
            Dim qry As String
            qry = "select * from tblmobiledevicecountry where 1=1"
            If String.IsNullOrEmpty(txtSearchEndPoint.Text) = False Then

                qry = qry + " and (country like '%" + txtSearchEndPoint.Text + "%'"

                qry = qry + " or webserviceurl like '%" + txtSearchEndPoint.Text + "%')"
            End If

            qry = qry + " order by Country;"
            txt.Text = qry
            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            lblMessage.Text = "SEARCH CRITERIA : " + txtSearchEndPoint.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString


        Else
            txtSearchEndPoint.Text = "Search Here"
        End If

    End Sub

    Protected Sub txtSearchEndPoint_TextChanged(sender As Object, e As EventArgs) Handles txtSearchEndPoint.TextChanged
        txtSearchText.Text = txtSearchEndPoint.Text
        Dim qry As String
        qry = "select * from tblmobiledevicecountry where 1=1"
        If String.IsNullOrEmpty(txtSearchEndPoint.Text) = False Then

            qry = qry + " and (country like '%" + txtSearchEndPoint.Text + "%'"

            qry = qry + " or webserviceurl like '%" + txtSearchEndPoint.Text + "%')"
        End If

        qry = qry + " order by Country;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchEndPoint.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchEndPoint.Text = "Search Here"
    End Sub
End Class
