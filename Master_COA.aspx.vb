Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_COA
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtCOACode.Text = ""
        txtDescription.Text = ""

        txtArea.Text = ""
        txtFunction.Text = ""
        txtOrganisation.Text = ""
        txtServiceType.Text = ""
        txtCostCenter.Text = ""

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlGLType.SelectedIndex = 0
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
        txtCOACode.Enabled = False
        txtDescription.Enabled = False
        txtArea.Enabled = False
        txtFunction.Enabled = False
        txtOrganisation.Enabled = False
        txtServiceType.Enabled = False
        txtCostCenter.Enabled = False
        ddlCompanyGrp.Enabled = False
        ddlGLType.Enabled = False

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

        txtCOACode.Enabled = True
        txtDescription.Enabled = True
        txtArea.Enabled = True
        txtFunction.Enabled = True
        txtOrganisation.Enabled = True
        txtServiceType.Enabled = True
        txtCostCenter.Enabled = True
        ddlCompanyGrp.Enabled = True
        ddlGLType.Enabled = True

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
            txtCOACode.Text = ""
        Else

            txtCOACode.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtDescription.Text = ""
        Else
            txtDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If

        'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
        '    ddlCompanyGrp.SelectedIndex = 0
        'Else
        '    ddlCompanyGrp.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        'End If



        If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Then
            txtArea.Text = ""
        Else
            txtArea.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        End If



        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            txtFunction.Text = ""
        Else
            txtFunction.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtOrganisation.Text = ""
        Else
            txtOrganisation.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            txtServiceType.Text = ""
        Else
            txtServiceType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            txtCostCenter.Text = ""
        Else
            txtCostCenter.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            ddlGLType.SelectedIndex = 0
        Else
            ddlGLType.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
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

        DisableControls()

        txtMode.Text = "New"
        lblMessage.Text = "ACTION: ADD RECORD"
        txtCOACode.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim Query As String
            Query = "Select CompanyGroup from tblCompanyGroup"

            PopulateDropDownList(Query, "CompanyGroup", "CompanyGroup", ddlCompanyGrp)

            txt.Text = SqlDataSource1.SelectCommand
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
                'command.CommandText = "SELECT x0209,  x0209Add, x0209Edit, x0209Delete, x0209Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0209,  x0209Add, x0209Edit, x0209Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0209")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0209"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0209")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0209Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0209Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0209Add").ToString()
                        End If
                    End If

                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0209Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0209Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0209Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0209Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0209Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0209Delete").ToString()
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtCOACode.Text = "" Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "CODE CANNOT BE BLANK"
            Return

        End If

        If ddlGLType.Text = "--SELECT--" Then
            '  MessageBox.Message.Alert(Page, "Industry cannot be blank!!!", "str")
            lblAlert.Text = "TYPE CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblchartofaccounts where COACode=@COACode"
                command1.Parameters.AddWithValue("@COACode", txtCOACode.Text)
                'command1.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtCOACode.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblchartofaccounts(COACode,Description,Area, Function, Organization, ServiceType, CostCenter, CompanyGroup, GLType, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@COACode,@Description, @Area, @Function, @Organization, @ServiceType, @CostCenter, @CompanyGroup, @GLType, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@COACode", txtCOACode.Text.ToUpper)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@Area", txtArea.Text.ToUpper)
                        command.Parameters.AddWithValue("@Function", txtFunction.Text.ToUpper)
                        command.Parameters.AddWithValue("@Organization", txtOrganisation.Text.ToUpper)
                        command.Parameters.AddWithValue("@ServiceType", txtServiceType.Text.ToUpper)
                        command.Parameters.AddWithValue("@CostCenter", txtCostCenter.Text.ToUpper)

                        command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                        command.Parameters.AddWithValue("@GLtype", ddlGLType.Text.ToUpper)

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@COACode", txtCOACode.Text)
                        command.Parameters.AddWithValue("@Description", txtDescription.Text)
                        command.Parameters.AddWithValue("@Area", txtArea.Text)
                        command.Parameters.AddWithValue("@Function", txtFunction.Text)
                        command.Parameters.AddWithValue("@Organization", txtOrganisation.Text)
                        command.Parameters.AddWithValue("@ServiceType", txtServiceType.Text)
                        command.Parameters.AddWithValue("@CostCenter", txtCostCenter.Text)

                        command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                        command.Parameters.AddWithValue("@GLtype", ddlGLType.Text)

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

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try
            EnableControls()
            'txtMode.Text = ""
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

                command2.CommandText = "SELECT * FROM tblchartofaccounts where COACode=@COACode  and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@COACode", txtCOACode.Text)
                'command2.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    ' MessageBox.Message.Alert(Page, "Industry already exists!!!", "str")
                    lblAlert.Text = "GL Code ALREADY EXISTS"
                    txtCOACode.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblchartofaccounts where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        If txtExists.Text = "True" Then
                            qry = "update tblchartofaccounts set Description=@Description, Area = @Area, Function = @Function, Organization =@Organization, ServiceType =@ServiceType, CostCenter =@CostCenter, CompanyGroup =@CompanyGroup, GLType=@GLType, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblchartofaccounts set COACode=@COACode,Description=@Description, Area = @Area, Function = @Function, Organization =@Organization, ServiceType =@ServiceType, CostCenter =@CostCenter, CompanyGroup =@CompanyGroup, GLType=@GLType, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@COACode", txtCOACode.Text.ToUpper)

                            command.Parameters.AddWithValue("@Description", txtDescription.Text.ToUpper)
                            command.Parameters.AddWithValue("@Area", txtArea.Text.ToUpper)
                            command.Parameters.AddWithValue("@Function", txtFunction.Text.ToUpper)
                            command.Parameters.AddWithValue("@Organization", txtOrganisation.Text.ToUpper)
                            command.Parameters.AddWithValue("@ServiceType", txtServiceType.Text.ToUpper)
                            command.Parameters.AddWithValue("@CostCenter", txtCostCenter.Text.ToUpper)

                            command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text.ToUpper)
                            command.Parameters.AddWithValue("@GLtype", ddlGLType.Text.ToUpper)

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@COACode", txtCOACode.Text)

                            command.Parameters.AddWithValue("@Description", txtDescription.Text)
                            command.Parameters.AddWithValue("@Area", txtArea.Text)
                            command.Parameters.AddWithValue("@Function", txtFunction.Text)
                            command.Parameters.AddWithValue("@Organization", txtOrganisation.Text)
                            command.Parameters.AddWithValue("@ServiceType", txtServiceType.Text)
                            command.Parameters.AddWithValue("@CostCenter", txtCostCenter.Text)

                            command.Parameters.AddWithValue("@CompanyGroup", ddlCompanyGrp.Text)
                            command.Parameters.AddWithValue("@GLtype", ddlGLType.Text)

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        End If


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        If txtExists.Text = "True" Then
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO GL CODE CANNOT BE UPDATED"
                            lblAlert.Text = ""
                        Else
                            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                            lblAlert.Text = ""
                        End If
                    End If
                End If

                conn.Close()
              
            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try
            EnableControls()

        End If


        If txtMode.Text = "New" Then
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "COA", txtCOACode.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
        Else
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "COA", txtCOACode.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
        End If

        txtMode.Text = ""

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

                command1.CommandText = "SELECT * FROM tblchartofaccounts where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblchartofaccounts where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "COA", txtCOACode.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterChartOfAccounts.aspx")



    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblsalesdetail where LedgerCode=@data limit 1"
        command1.Parameters.AddWithValue("@data", txtCOACode.Text)
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

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
        GridView1.DataBind()
    End Sub

    Protected Sub txtSearchCOA_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCOA.TextChanged
        'txtSearchCOA.Text = txtSearchStaff.Text
        Dim qry As String
        qry = "select * from tblchartofaccounts where 1=1 "
        If String.IsNullOrEmpty(txtSearchCOA.Text) = False Then
            qry = qry + " and (COACode like '%" + txtSearchCOA.Text + "%'"
            qry = qry + " or Description like '%" + txtSearchCOA.Text + "%')"
        End If

        qry = qry + " order by Description,COACode;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchText.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchCOA.Text = "Search Here"

        '      

        If txtMode.Text = "Edit" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
            Return
        End If

        'MakeMeNull()
        'Dim editindex As Integer = 0
        'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
        'txtRcno.Text = rcno.ToString()


        'If Convert.ToInt32(GridView1.Rows.Count.ToString) > 0 Then

        '    'btnQuickSearch_Click(sender, e)

        '    'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
        '    'MakeMeNull()
        '    'MakeMeNullBillingDetails()

        '    If GridView1.Rows.Count > 0 Then
        '        txtMode.Text = "View"
        '        txtRcno.Text = GridView1.Rows(0).Cells(1).Text
        '        PopulateRecord()
        '        'UpdatePanel2.Update()

        '        'updPanelSave.Update()
        '        'UpdatePanel3.Update()

        '        'GridView1_SelectedIndexChanged(sender, e)
        '    End If
        'End If
        'txtSearchStaff.Text = txtSearchStaffText.Text

     
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()

        txt.Text = "SELECT * FROM tblchartofaccounts  order by Description"
        SqlDataSource1.SelectCommand = txt.Text
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub
End Class
