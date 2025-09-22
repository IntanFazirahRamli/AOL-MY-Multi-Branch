Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Drawing

Partial Class Master_SMSSetup
    Inherits System.Web.UI.Page

    Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtScreen.Text = ""
        txtModule.Text = ""
        txtSMSFormat.Text = ""
    
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
        txtScreen.Enabled = False
        txtModule.Enabled = False
        txtSMSFormat.Enabled = False
       
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

        txtScreen.Enabled = True
        txtModule.Enabled = True
        txtSMSFormat.Enabled = True
      
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
            txtModule.Text = ""
        Else
            txtModule.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtScreen.Text = ""
        Else
            txtScreen.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtSMSFormat.Text = ""
        Else
            txtSMSFormat.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If

        lblWordCount.Text = txtSMSFormat.Text.Count.ToString

    
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
        txtModule.Focus()

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
        If txtModule.Text.Trim = "" Then
            '  MessageBox.Message.Alert(Page, "Product ID cannot be blank!!!", "str")
            lblAlert.Text = "Module CANNOT BE BLANK"
            Return
        End If
        'If txtModule.Text.Trim = "" Then
        '    ' MessageBox.Message.Alert(Page, "Product Description cannot be blank!!!", "str")
        '    lblAlert.Text = "PRODUCT DESCRIPTION CANNOT BE BLANK"
        '    Return
        'End If

        '#Region "NewRecord"

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT * FROM tblsmsSetup where Module=@Module"
                command1.Parameters.AddWithValue("@Module", txtModule.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtModule.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblSMSSetup(Module,Screen, SMSFormat,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@Module,@Screen,@SMSFormat,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                        command.Parameters.AddWithValue("@Screen", txtScreen.Text.ToUpper)
                        command.Parameters.AddWithValue("@SMSFormat", txtSMSFormat.Text)
                      
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@Module", txtModule.Text)
                        command.Parameters.AddWithValue("@Screen", txtScreen.Text)
                        command.Parameters.AddWithValue("@SMSFormat", txtSMSFormat.Text)

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


                    End If

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""

                    command.Dispose()

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

                command2.CommandText = "SELECT * FROM tblsmssetup where Module=@Module and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@Module", txtModule.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then
                    '  MessageBox.Message.Alert(Page, "Product already exists!!!", "str")
                    lblAlert.Text = "SMS SETUP ALREADY EXISTS"
                    txtModule.Focus()
                    Exit Sub
                Else
                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text
                    command1.CommandText = "SELECT * FROM tblSMSSetup where rcno=" & Convert.ToInt32(txtRcno.Text)
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
                        qry = "update tblSMSSetup set Module=@Module,Screen=@Screen,SMSFormat=@SMSFormat,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()
                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                              command.Parameters.AddWithValue("@Module", txtModule.Text.ToUpper)
                            command.Parameters.AddWithValue("@Screen", txtScreen.Text.ToUpper)
                            command.Parameters.AddWithValue("@SMSFormat", txtSMSFormat.Text)

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@Module", txtModule.Text)
                            command.Parameters.AddWithValue("@Screen", txtScreen.Text)
                            command.Parameters.AddWithValue("@SMSFormat", txtSMSFormat.Text)

                             command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


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
                '#End Region
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"

        lblWordCount.Text = txtSMSFormat.Text.Count.ToString

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

                command1.CommandText = "SELECT * FROM tblsmsSetup where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblsmsSetup where rcno=" & Convert.ToInt32(txtRcno.Text)

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
        Response.Redirect("RV_MasterSMSSetUp.aspx")
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
End Class
