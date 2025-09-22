Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO.FileStream

Imports System.Drawing

Partial Class Master_Terminationcode
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtCode.Text = ""
        txtDescription.Text = ""
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        chkStatus.Checked = True
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
        txtCode.Enabled = False
        txtDescription.Enabled = False
        chkStatus.Enabled = False
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

        txtCode.Enabled = True
        txtDescription.Enabled = True
        chkStatus.Enabled = True
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


        If GridView1.SelectedRow.Cells(1).Text = "Y" Then
            chkStatus.Checked = True
        Else
            chkStatus.Checked = False
        End If



        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtCode.Text = ""
        Else
            txtCode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If


        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If


        SqlDataSource2.SelectCommand = "Select * from tblterminationcodestatus where Code ='" & txtCode.Text & "' and Selected='Y' order by Code"
        GridView2.DataSourceID = "SqlDataSource2"
        GridView2.DataBind()

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
        txtCode.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

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
                'command.CommandText = "SELECT x0162,  x0162Add, x0162Edit, x0162Delete, x0162Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0162,  x0162Add, x0162Edit, x0162Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0162"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0162")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If



                    If String.IsNullOrEmpty(dt.Rows(0)("x0162Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0162Add").ToString()
                    End If

                    'Me.btnInsert.Enabled = vpSec2412Add
                    If txtMode.Text = "View" Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0162Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0162Edit").ToString()
                        End If

                        If String.IsNullOrEmpty(dt.Rows(0)("x0162Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0162Delete").ToString()
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
        If txtCode.Text = "" Then
            ' MessageBox.Message.Alert(Page, "City cannot be blank!!!", "str")
            lblAlert.Text = "CODE CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblterminationcode where code=@code"
                command1.Parameters.AddWithValue("@code", txtCode.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtCode.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblterminationcode(code,description,Status, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@code,@description,@Status, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@code", txtCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@description", txtDescription.Text.ToUpper)

                        If chkStatus.Checked = True Then
                            command.Parameters.AddWithValue("@Status", "Y")
                        Else
                            command.Parameters.AddWithValue("@Status", "N")
                        End If
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now.ToUniversalTime)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", 0)
                        'command.Parameters.AddWithValue("@State", txtState.Text)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@code", txtCode.Text.ToUpper)
                        command.Parameters.AddWithValue("@description", txtDescription.Text)
                        If chkStatus.Checked = True Then
                            command.Parameters.AddWithValue("@Status", "Y")
                        Else
                            command.Parameters.AddWithValue("@Status", "N")
                        End If
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now.ToUniversalTime)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@countrycode", 0)
                        'command.Parameters.AddWithValue("@State", txtState.Text)

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
                'ind = txtcity.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblterminationcode where code=@code and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@code", txtCode.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "city already exists!!!", "str")
                    lblAlert.Text = "CODE ALREADY EXISTS"
                    txtCode.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblterminationcode where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblterminationcode set description=@description,status=@status, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        Else
                            qry = "update tblterminationcode set code=@code,description=@description, status=@status, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@code", txtCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@description", txtDescription.Text.ToUpper)
                            If chkStatus.Checked = True Then
                                command.Parameters.AddWithValue("@Status", "Y")
                            Else
                                command.Parameters.AddWithValue("@Status", "N")
                            End If
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@code", txtCode.Text.ToUpper)
                            command.Parameters.AddWithValue("@description", txtDescription.Text)
                            If chkStatus.Checked = True Then
                                command.Parameters.AddWithValue("@Status", "Y")
                            Else
                                command.Parameters.AddWithValue("@Status", "N")
                            End If
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If
                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO CITY CANNOT BE UPDATED"
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
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TERMICODE", txtCode.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TERMICODE", txtCode.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        txtCode.Enabled = False
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

                command1.CommandText = "SELECT * FROM tblterminationcode where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblterminationcode where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '   MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TERMICODE", txtCode.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterTerminationCode.aspx")

    End Sub


    Private Function CheckIfExists() As Boolean
        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblcontract where code=@code"
        'command1.Parameters.AddWithValue("@code", txtCode.Text)
        'command1.Connection = conn

        'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        'Dim dt1 As New DataTable
        'dt1.Load(dr1)

        'If dt1.Rows.Count > 0 Then
        '    Return True
        'End If

        ''Dim command2 As MySqlCommand = New MySqlCommand

        ''command2.CommandType = CommandType.Text

        ''command2.CommandText = "SELECT * FROM tblperson where AddCity=@city or BillCity=@city"
        ''command2.Parameters.AddWithValue("@city", txtCity.Text)
        ''command2.Connection = conn

        ''Dim dr2 As MySqlDataReader = command2.ExecuteReader()
        ''Dim dt2 As New DataTable
        ''dt2.Load(dr2)

        ''If dt2.Rows.Count > 0 Then
        ''    Return True
        ''End If

        ''Dim command4 As MySqlCommand = New MySqlCommand

        ''command4.CommandType = CommandType.Text

        ''command4.CommandText = "SELECT * FROM tblservicerecord where CustAddCity=@city or AddCity=@city"
        ''command4.Parameters.AddWithValue("@city", txtCity.Text)
        ''command4.Connection = conn

        ''Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        ''Dim dt4 As New DataTable
        ''dt4.Load(dr4)

        ''If dt4.Rows.Count > 0 Then
        ''    Return True
        ''End If

        'conn.Close()

        'Return False
    End Function

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


    Protected Sub btnStatus_Click(sender As Object, e As EventArgs)
        Try


            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
                lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
                Return
            End If

            lblMessage.Text = ""
            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""

            Dim btn1 As Button = DirectCast(sender, Button)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex

            'MakeMeNull()
            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno
            'RetrieveData()

            GridView1.SelectedIndex = rowindex1

            'Dim lblStatus As String = GridView1.Rows(rowindex1).Cells(2).Text
            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(2).Text

            Label8.Text = strRecordNo.Trim
            lblMessage.Text = strRecordNo

            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""
            'txtGridIndex.Text = rowindex1.ToString

            ''sqlDSViewEditHistory.SelectCommand = "Select * from tblterminationstatus where  DocRef = '" & strRecordNo & "' order by status"
            'sqlDSViewEditHistory.SelectCommand = "Select * from tblterminationcodestatus where Code = '" & strRecordNo & "' order by status"
            'sqlDSViewEditHistory.DataBind()

            'grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            'grdViewEditHistory.DataBind()


            '12.03.22

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "SELECT x0162,  x0162Add, x0162Edit, x0162Delete, x0162Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.CommandText = "Select * from tblterminationcodestatus where Code = '" & strRecordNo & "' order by status"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count = 0 Then

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblterminationcodestatus(code,comments,Status, Selected, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                qry = qry + " Select '" & strRecordNo & "', comments, Status, 'N', @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn from tblterminationcodestatus group by status;"

                command1.CommandText = qry
                command1.Parameters.Clear()

                command1.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command1.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now.ToUniversalTime)
                command1.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command1.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command1.Connection = conn

                command1.ExecuteNonQuery()


            Else
                '''

                'Dim commandValues As MySqlCommand = New MySqlCommand

                'commandValues.CommandType = CommandType.Text
                ''commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where BatchNo ='" & txtBatchNo.Text.Trim & "'"
                'commandValues.CommandText = "SELECT *  FROM tblTerminationcodeStatus  where Code = '" & Label8.Text & "' order by Code"
                'commandValues.Connection = conn

                'Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                'Dim dtValues As New DataTable
                'dtValues.Load(drValues)

                'Dim rowIndex = 0
                'For Each row As DataRow In dtValues.Rows

                '    Dim TextBoxchkSelect As CheckBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                '    Dim TextBoxStatus As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtStatusGVB"), TextBox)
                '    Dim TextBoxComments As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtCommentsGVB"), TextBox)

                '    If (row("Selected")) = "Y" Then
                '        TextBoxchkSelect.Checked = True
                '    Else
                '        TextBoxchkSelect.Checked = False

                '    End If
                '    TextBoxStatus.Text = (row("Status"))
                '    TextBoxComments.Text = (row("Comments"))
                '    rowIndex = rowIndex + 1
                'Next row

                ''''

                'For rowIndex As Integer = 0 To grdViewTerminationStatus.Rows.Count - 1

                '    Dim TextBoxchkSelect As CheckBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                '    Dim TextBoxStatus As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtStatusGVB"), TextBox)
                '    Dim TextBoxComments As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtCommentsGVB"), TextBox)


                '    Dim command2 As MySqlCommand = New MySqlCommand

                '    command2.CommandType = CommandType.Text
                '    Dim qry As String = "Select * from tblTerminationcodeStatus order by Code"
                '    qry = qry + " where Code = '" & Label8.Text & "'"

                '    command2.CommandText = qry
                '    command2.Parameters.Clear()

                '    command2.Parameters.AddWithValue("@code", Label8.Text.Trim.ToUpper)
                '    command2.Parameters.AddWithValue("@Comments", TextBoxComments.Text.Trim.ToUpper)
                '    command2.Parameters.AddWithValue("@Status", TextBoxStatus.Text)
                '    If TextBoxchkSelect.Checked = True Then
                '        command2.Parameters.AddWithValue("@Selected", "Y")
                '    Else
                '        command2.Parameters.AddWithValue("@Selected", "N")
                '    End If


                '    command2.Connection = conn

                '    command2.ExecuteNonQuery()


                'Next

            End If

            sqlDSViewEditHistory.SelectCommand = "Select * from tblterminationcodestatus where Code = '" & strRecordNo & "' order by status"
            sqlDSViewEditHistory.DataBind()
            'If dt.Rows.Count > 0 Then
            '    sqlDSViewEditHistory.SelectCommand = "Select * from tblterminationcodestatus where Code = '" & strRecordNo & "' order by status"
            '    sqlDSViewEditHistory.DataBind()
            'Else
            '    command.CommandText = "Select * from tblterminationstatus order by status"
            '    sqlDSViewEditHistory.SelectCommand = "Select * from tblterminationstatus  order by status"
            '    sqlDSViewEditHistory.DataBind()


            'End If

            grdViewTerminationStatus.DataSourceID = "sqlDSViewEditHistory"
            grdViewTerminationStatus.DataBind()

            '12.03.22
            mdlViewEditHistory.Show()


        Catch ex As Exception
            'InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnSaveStatus_Click(sender As Object, e As EventArgs) Handles btnSaveStatus.Click
        Try
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            For rowIndex As Integer = 0 To grdViewTerminationStatus.Rows.Count - 1

                Dim TextBoxchkSelect As CheckBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim TextBoxStatus As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtStatusGVB"), TextBox)
                Dim TextBoxComments As TextBox = CType(grdViewTerminationStatus.Rows(rowIndex).Cells(0).FindControl("txtCommentsGVB"), TextBox)


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "Update tblTerminationcodeStatus SET code =@Code, Comments =@Comments, Status = @Status, Selected=@Selected, CreatedBy=@CreatedBy,CreatedOn=@CreatedOn,LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn"
                qry = qry + " where Code = '" & Label8.Text & "' and Status ='" & TextBoxStatus.Text.Trim & "'"

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@code", Label8.Text.Trim.ToUpper)
                command.Parameters.AddWithValue("@Comments", TextBoxComments.Text.Trim.ToUpper)
                command.Parameters.AddWithValue("@Status", TextBoxStatus.Text)
                If TextBoxchkSelect.Checked = True Then
                    command.Parameters.AddWithValue("@Selected", "Y")
                Else
                    command.Parameters.AddWithValue("@Selected", "N")
                End If
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@CreatedOn", DateAndTime.Now.ToUniversalTime)
                command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command.Connection = conn

                command.ExecuteNonQuery()
                'txtRcno.Text = command.LastInsertedId

                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""
            Next

        Catch ex As Exception
            'InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub
End Class
