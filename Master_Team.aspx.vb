Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_Team
    Inherits System.Web.UI.Page

  
    Public rcno As String
    Public code As String
    Private Shared gddlvalue As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtTeamID.Text = ""
        txtTeamName.Text = ""
        txtDeptID.Text = ""
        ddlStatus.SelectedIndex = 0
        txtSecondInchargeID.Text = ""
        txtNotes.Text = ""
        txtVehNos.Text = ""
        ddlSupervisor.SelectedIndex = 0

        txtRcno.Text = ""
        txtOrigCode.Text = ""
        txtCoordinator.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        ddlInchargeID.SelectedIndex = 0
        'txtFrom.Text = ""
        txtLocationId.SelectedIndex = 0

        lblTeamIDStatus.Text = ""
        lblOldStatus.Text = ""

        ddlNewStatus.SelectedIndex = 0

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
        btnStatus.Enabled = False
        btnStatus.ForeColor = System.Drawing.Color.Gray



        txtTeamID.Enabled = False
        txtTeamName.Enabled = False
        txtDeptID.Enabled = False
        ddlInchargeID.Enabled = False
        ddlSupervisor.Enabled = False
        txtSecondInchargeID.Enabled = False
        txtNotes.Enabled = False
        txtVehNos.Enabled = False
        ddlStatus.Enabled = False
        txtLocationId.Enabled = False
        txtCoordinator.Enabled = False
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


        btnStatus.Enabled = False
        btnStatus.ForeColor = System.Drawing.Color.Gray


        txtTeamID.Enabled = True
        txtTeamName.Enabled = True
        txtDeptID.Enabled = True
        ddlInchargeID.Enabled = True
        ddlSupervisor.Enabled = True
        txtSecondInchargeID.Enabled = True
        txtNotes.Enabled = True
        txtVehNos.Enabled = True
        ddlStatus.Enabled = True
        txtLocationId.Enabled = True
        txtCoordinator.Enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        'If chkInActive.Checked = True Then
        '    SqlDataSource1.SelectCommand = txt.Text
        'Else
        '    SqlDataSource1.SelectCommand = txt.Text
        'End If
        SqlDataSource1.SelectCommand = txt.Text
        GridView1.DataBind()
        'GridView1.DataBind()
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
        code = DirectCast(GridView1.Rows(editindex).FindControl("Label2"), Label).Text
        txtOrigCode.Text = code.ToString()

        If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtTeamID.Text = ""
            lblTeamID.Text = ""
            lblTeamID2.Text = ""
        Else

            txtTeamID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
            lblTeamID.Text = txtTeamID.Text
            lblTeamID2.Text = txtTeamID.Text
        End If
        If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtTeamName.Text = ""
        Else
            txtTeamName.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtDeptID.Text = ""
        Else
            txtDeptID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(4).Text.Trim = "&nbsp;" Then
            ddlInchargeID.SelectedIndex = 0
        Else
            ddlInchargeID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text.Trim).ToString
        End If
        If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
            ddlSupervisor.SelectedIndex = 0
        Else

            If Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text.Trim).ToString <> "" Then
                'Dim gSalesman As String

                gddlvalue = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text.Trim).ToString

                If ddlSupervisor.Items.FindByValue(gddlvalue) Is Nothing Then
                    ddlSupervisor.Items.Add(gddlvalue)
                    ddlSupervisor.Text = gddlvalue
                Else
                    ddlSupervisor.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
                End If
            End If


        End If
        If GridView1.SelectedRow.Cells(6).Text = "&nbsp;" Then
            txtSecondInchargeID.Text = ""
        Else
            txtSecondInchargeID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(6).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            txtCoordinator.Text = ""
        Else
            txtCoordinator.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            txtNotes.Text = ""
        Else
            txtNotes.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            txtVehNos.Text = ""
        Else
            txtVehNos.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(10).Text = "&nbsp;" Or GridView1.SelectedRow.Cells(9).Text = "Y" Then
            ddlStatus.SelectedValue = "Y"
        ElseIf GridView1.SelectedRow.Cells(10).Text = "N" Then
            ddlStatus.SelectedValue = "N"
        End If

        If String.IsNullOrEmpty(GridView1.SelectedRow.Cells(18).Text) = True Or (GridView1.SelectedRow.Cells(18).Text) = "&nbsp;" Then
            txtLocationId.SelectedIndex = 0
        Else
            txtLocationId.Text = GridView1.SelectedRow.Cells(18).Text
        End If

      


        txtMode.Text = "View"

        'txtMode.Text = "Edit"
        '   DisableControls()
        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnStatus.Enabled = True
        btnStatus.ForeColor = System.Drawing.Color.Black

        If CheckIfExists() = True Then
            txtExists.Text = True
        Else
            txtExists.Text = False
        End If

        GridView3.DataBind()
        GridView2.DataBind()
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
        txtTeamID.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            DisableStaffControls()
            DisableAssetControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID
            tb1.ActiveTabIndex = 0

            'txtCreatedBy.Text = Session("userid")

            'FindLocation()
            'txtLocationId.Attributes.Add("disabled", "true")

            txt.Text = "Select * from tblTeam WHERE Status <>'N' order by TeamID"

            Dim query As String
            query = "Select LocationID from tbllocation"
            PopulateDropDownList(query, "LocationID", "LocationID", txtLocationId)

            If String.IsNullOrEmpty(Session("teamfrom")) = False Then
                txtFrom.Text = Session("teamfrom")
                btnQuit.Text = "BACK"
                Session.Remove("teamfrom")
            End If
        End If
        CheckTab()

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

    Public Sub FindLocation()
        Dim IsLock As String
        IsLock = ""

        Dim connLocation As MySqlConnection = New MySqlConnection()

        connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connLocation.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT LocationID, Location FROM tblstaff where StaffId='" & txtCreatedBy.Text.ToUpper & "'"
        command1.Connection = connLocation

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            txtLocationId.Text = dt.Rows(0)("LocationID").ToString
        End If

        connLocation.Close()
        connLocation.Dispose()
        command1.Dispose()
        dt.Dispose()
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
                'command.CommandText = "SELECT x0161,  x0161Add, x0161Edit, x0161Delete, x0161Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0161,  x0161Add, x0161Edit, x0161Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0161"))) = False Then
                        If Convert.ToBoolean(dt.Rows(0)("x0161")) = False Then
                            Response.Redirect("Home.aspx")
                        End If
                    End If



                    If String.IsNullOrEmpty(dt.Rows(0)("x0161Add")) = False Then
                        Me.btnADD.Enabled = dt.Rows(0)("x0161Add").ToString()
                    End If

                    'Me.btnInsert.Enabled = vpSec2412Add
                    If txtMode.Text = "View" Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0161Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0161Edit").ToString()
                        End If

                        If String.IsNullOrEmpty(dt.Rows(0)("x0161Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("x0161Delete").ToString()
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

                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
            End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtTeamID.Text = "" Then
            '   MessageBox.Message.Alert(Page, "Team ID cannot be blank!!!", "str")
            lblAlert.Text = "TEAM ID CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblteam where teamid=@ind"
                command1.Parameters.AddWithValue("@ind", txtTeamID.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '   MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtTeamID.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblteam(DepartmentID,TeamID,TeamName,InChargeID,SecondInChargeID,Notes,VehNos,  CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AllocationZoneOnly,Status,Supervisor,Location, Coordinator)"
                    qry = qry + " VALUES(@DepartmentID,@TeamID,@TeamName,@InChargeID,@SecondInChargeID,@Notes,@VehNos,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AllocationZoneOnly,@Status,@Supervisor,@Location, @Coordinator);"
                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@DepartmentID", txtDeptID.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@TeamID", txtTeamID.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@TeamName", txtTeamName.Text.ToUpper.Trim)
                        If ddlInchargeID.Text = "." Then
                            command.Parameters.AddWithValue("@InChargeID", "")
                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.ToUpper.Trim)
                        End If

                        If ddlSupervisor.Text = "." Then
                            command.Parameters.AddWithValue("@Supervisor", "")
                        Else
                            command.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text.ToUpper.Trim)
                        End If

                        command.Parameters.AddWithValue("@SecondInChargeID", txtSecondInchargeID.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@VehNos", txtVehNos.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@AllocationZoneOnly", 0)
                        command.Parameters.AddWithValue("@Status", ddlStatus.Text)

                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@Coordinator", txtCoordinator.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@DepartmentID", txtDeptID.Text.Trim)
                        command.Parameters.AddWithValue("@TeamID", txtTeamID.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@TeamName", txtTeamName.Text.ToUpper.Trim)
                        If ddlInchargeID.Text = "." Then
                            command.Parameters.AddWithValue("@InChargeID", "")
                        Else
                            command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.Trim)
                        End If
                        If ddlSupervisor.Text = "." Then
                            command.Parameters.AddWithValue("@Supervisor", "")
                        Else
                            command.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text.Trim)
                        End If

                        command.Parameters.AddWithValue("@SecondInChargeID", txtSecondInchargeID.Text.Trim)
                        command.Parameters.AddWithValue("@Notes", txtNotes.Text)
                        command.Parameters.AddWithValue("@VehNos", txtVehNos.Text.Trim)
                        command.Parameters.AddWithValue("@AllocationZoneOnly", 0)
                        command.Parameters.AddWithValue("@Status", ddlStatus.Text.Trim)

                        If txtLocationId.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@Location", "")
                        Else
                            command.Parameters.AddWithValue("@Location", txtLocationId.Text.Trim)
                        End If
                        command.Parameters.AddWithValue("@Coordinator", txtCoordinator.Text.Trim)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId
                    lblTeamID.Text = txtTeamID.Text
                    lblTeamID2.Text = txtTeamID.Text
                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")

                    Dim command0 As MySqlCommand = New MySqlCommand

                    command0.CommandType = CommandType.Text

                    command0.CommandText = "UPDATE tblteam SET OrigCode = " & Convert.ToInt16(txtRcno.Text) & " where rcno = " & Convert.ToInt16(txtRcno.Text)
                    command0.Connection = conn

                    command0.ExecuteNonQuery()
                    txtOrigCode.Text = txtRcno.Text

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
                '  MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtscheduletype.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblteam where teamid=@ind and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@ind", txtTeamID.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Team ID already exists!!!", "str")
                    lblAlert.Text = "TEAM ID ALREADY EXISTS"
                    txtTeamID.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblteam where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "UPDATE tblteam SET DepartmentID = @DepartmentID,TeamName = @TeamName,InChargeID = @InChargeID,SecondInChargeID = @SecondInChargeID,Notes = @Notes,VehNos = @VehNos,Status=@Status, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Supervisor=@Supervisor,Location=@Location, Coordinator=@Coordinator where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "UPDATE tblteam SET DepartmentID = @DepartmentID,TeamID = @TeamID,TeamName = @TeamName,InChargeID = @InChargeID,SecondInChargeID = @SecondInChargeID,Notes = @Notes,VehNos = @VehNos,Status=@Status,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Supervisor=@Supervisor,Location=@Location, Coordinator=@Coordinator where rcno=" & Convert.ToInt32(txtRcno.Text)

                        End If

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@DepartmentID", txtDeptID.Text.ToUpper)
                            command.Parameters.AddWithValue("@TeamID", txtTeamID.Text.ToUpper)
                            command.Parameters.AddWithValue("@TeamName", txtTeamName.Text.ToUpper)
                            If ddlInchargeID.Text = "." Then
                                command.Parameters.AddWithValue("@InChargeID", "")
                            Else
                                command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text.ToUpper)
                            End If
                            If ddlSupervisor.Text = "." Then
                                command.Parameters.AddWithValue("@Supervisor", "")
                            Else
                                command.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@SecondInChargeID", txtSecondInchargeID.Text.ToUpper)
                            command.Parameters.AddWithValue("@Notes", txtNotes.Text.ToUpper)
                            command.Parameters.AddWithValue("@VehNos", txtVehNos.Text.ToUpper)
                            command.Parameters.AddWithValue("@AllocationZoneOnly", 0)
                            command.Parameters.AddWithValue("@Status", ddlStatus.Text)

                            If txtLocationId.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                            End If
                            command.Parameters.AddWithValue("@Coordinator", txtCoordinator.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@DepartmentID", txtDeptID.Text)
                            command.Parameters.AddWithValue("@TeamID", txtTeamID.Text.ToUpper)
                            command.Parameters.AddWithValue("@TeamName", txtTeamName.Text.ToUpper)
                            If ddlInchargeID.Text = "." Then
                                command.Parameters.AddWithValue("@InChargeID", "")
                            Else
                                command.Parameters.AddWithValue("@InChargeID", ddlInchargeID.Text)
                            End If
                            If ddlSupervisor.Text = "." Then
                                command.Parameters.AddWithValue("@Supervisor", "")
                            Else
                                command.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text)
                            End If

                            command.Parameters.AddWithValue("@SecondInChargeID", txtSecondInchargeID.Text)
                            command.Parameters.AddWithValue("@Notes", txtNotes.Text)
                            command.Parameters.AddWithValue("@VehNos", txtVehNos.Text)
                            command.Parameters.AddWithValue("@AllocationZoneOnly", 0)
                            command.Parameters.AddWithValue("@Status", ddlStatus.Text)

                            If txtLocationId.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@Location", "")
                            Else
                                command.Parameters.AddWithValue("@Location", txtLocationId.Text)
                            End If
                            command.Parameters.AddWithValue("@Coordinator", txtCoordinator.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        End If



                        command.Connection = conn

                        command.ExecuteNonQuery()
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

                        'If txtExists.Text = "True" Then
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED. RECORD IS IN USE, SO TEAMID CANNOT BE UPDATED"
                        '    lblAlert.Text = ""
                        'Else
                        '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        '    lblAlert.Text = ""
                        'End If
                    End If
                End If

                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TEAM", txtTeamID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TEAM", txtTeamID.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
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

        If ddlStatus.SelectedValue = "N" Then
            lblAlert.Text = "INACTIVE RECORDS CANNOT BE EDITED"
            Return
        End If

        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

        If txtExists.Text = "True" Then
            txtTeamID.Enabled = False
        Else
            txtTeamID.Enabled = True
        End If
    End Sub



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()

    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        If String.IsNullOrEmpty(txtFrom.Text.Trim) = False Then
            Session("teamfrom") = "contract"
            Response.Redirect("Contract.aspx")
        Else
            Response.Redirect("Home.aspx")
        End If



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

                command1.CommandText = "SELECT * FROM tblteam where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblteam where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '   MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "TEAM", txtTeamID.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterTeam.aspx")
    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT * FROM tblcontract where teamid=@data"
        command3.Parameters.AddWithValue("@data", txtTeamID.Text)
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            Return True
        End If

        Dim command4 As MySqlCommand = New MySqlCommand

        command4.CommandType = CommandType.Text

        command4.CommandText = "SELECT * FROM tblservicerecord where teamid=@data"
        command4.Parameters.AddWithValue("@data", txtTeamID.Text)
        command4.Connection = conn

        Dim dr4 As MySqlDataReader = command4.ExecuteReader()
        Dim dt4 As New DataTable
        dt4.Load(dr4)

        If dt4.Rows.Count > 0 Then
            Return True
        End If
        conn.Close()

        Return False
    End Function

    Protected Sub btnPopUpAssetReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpAssetReset.Click
        txtPopUpAsset.Text = ""
        txtPopupAssetSearch.Text = ""
        SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0"
        SqlDSAsset.DataBind()
        gvAsset.DataBind()
        mdlPopUpAsset.Show()
    End Sub

    Protected Sub gvAsset_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAsset.PageIndexChanging
        gvAsset.PageIndex = e.NewPageIndex
        If txtPopUpAsset.Text.Trim = "" Then
            SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0"
        Else
            ' SqlDSasset.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and Name like '" + ViewState("assetCurrentAlphabet") + "%' And upper(Name) Like '%" + txtPopUpasset.Text.Trim.ToUpper + "%'"
            SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0 and (upper(assetno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%' or upper(assetregno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%')"
        End If

        SqlDSAsset.DataBind()
        gvAsset.DataBind()
        mdlPopUpAsset.Show()
    End Sub

    Protected Sub gvAsset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAsset.SelectedIndexChanged
        If (gvAsset.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtAssetNo.Text = " "
        Else
            txtAssetNo.Text = gvAsset.SelectedRow.Cells(1).Text
        End If
        If (gvAsset.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtAssetRegNo.Text = " "
        Else
            txtAssetRegNo.Text = gvAsset.SelectedRow.Cells(2).Text
        End If
        If (gvAsset.SelectedRow.Cells(7).Text = "&nbsp;") Then
            txtAssetDesc.Text = " "
        Else
            txtAssetDesc.Text = gvAsset.SelectedRow.Cells(7).Text
        End If
        If (gvAsset.SelectedRow.Cells(8).Text = "&nbsp;") Then
            txtType.Text = " "
        Else
            txtType.Text = gvAsset.SelectedRow.Cells(8).Text
        End If
    End Sub

    Protected Sub txtPopUpAsset_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpAsset.TextChanged
        If txtPopUpAsset.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter Asset", "str")
        Else
            txtPopupAssetSearch.Text = txtPopUpAsset.Text

            '  SqlDSasset.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and Name like '" + ViewState("assetCurrentAlphabet") + "%' And upper(Name) Like '%" + txtPopUpasset.Text.Trim.ToUpper + "%'"
            SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0 and (upper(assetno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%' or upper(assetregno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%')"
            SqlDSAsset.DataBind()
            gvAsset.DataBind()
            mdlPopUpAsset.Show()
        End If
        txtPopUpAsset.Text = "Search Here"
    End Sub

    Protected Sub btnAsset_Click(sender As Object, e As ImageClickEventArgs) Handles btnAsset.Click
        If String.IsNullOrEmpty(txtAssetNo.Text.Trim) = False Then
            txtPopUpAsset.Text = txtAssetNo.Text.Trim.ToUpper
            txtPopupAssetSearch.Text = txtPopUpAsset.Text

            '  SqlDSasset.SelectCommand = "SELECT distinct * From tblStaff where rcno <>0 and Name like '" + ViewState("assetCurrentAlphabet") + "%' And upper(Name) Like '%" + txtPopUpasset.Text.Trim.ToUpper + "%'"
            SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0 and (upper(assetno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%' or upper(assetregno) Like '%" + txtPopupAssetSearch.Text.Trim.ToUpper + "%')"
            SqlDSAsset.DataBind()
            gvAsset.DataBind()
        Else
            'txtPopUpasset.Text = ""
            'txtPopupassetSearch.Text = ""
            SqlDSAsset.SelectCommand = "SELECT distinct * From tblasset where rcno <>0"
            SqlDSAsset.DataBind()
            gvAsset.DataBind()
        End If

        mdlPopUpAsset.Show()
    End Sub

    Private Sub MakeAssetNull()
        txtAssetMode.Text = ""
        txtAssetNo.Text = ""
        txtAssetRegNo.Text = ""
        txtAssetRcNo.Text = ""
        txtAssetDesc.Text = ""
        txtType.Text = ""
    End Sub

    Private Sub EnableAssetControls()
        btnAssetSave.Enabled = True
        btnAssetSave.ForeColor = System.Drawing.Color.Black
        btnAssetCancel.Enabled = True
        btnAssetCancel.ForeColor = System.Drawing.Color.Black

        btnAssetAdd.Enabled = True
        btnAssetAdd.ForeColor = System.Drawing.Color.Black
        'btnAssetEdit.Enabled = True
        'btnAssetEdit.ForeColor = System.Drawing.Color.Black
        'btnAssetDelete.Enabled = True
        'btnAssetDelete.ForeColor = System.Drawing.Color.Black

        txtAssetNo.Enabled = True
        txtAssetRegNo.Enabled = True
        txtAssetDesc.Enabled = True
        txtType.Enabled = True

    End Sub

    Private Sub DisableAssetControls()
        'btnAssetSave.Enabled = True
        'btnAssetSave.ForeColor = System.Drawing.Color.Black
        'btnAssetCancel.Enabled = True
        'btnAssetCancel.ForeColor = System.Drawing.Color.Black

        btnAssetSave.Enabled = False
        btnAssetSave.ForeColor = System.Drawing.Color.Gray
        btnAssetCancel.Enabled = False
        btnAssetCancel.ForeColor = System.Drawing.Color.Gray


        'btnAssetAdd.Enabled = False
        'btnAssetAdd.ForeColor = System.Drawing.Color.Gray

        btnAssetEdit.Enabled = False
        btnAssetEdit.ForeColor = System.Drawing.Color.Gray

        btnAssetDelete.Enabled = False
        btnAssetDelete.ForeColor = System.Drawing.Color.Gray


        'btnQuit.Enabled = False
        'btnQuit.ForeColor = System.Drawing.Color.Gray

        txtAssetNo.Enabled = False
        txtAssetRegNo.Enabled = False
        txtAssetDesc.Enabled = False
        txtType.Enabled = False
        '  btnQuit.CssClass = "visiblebutton"
    End Sub

    Public AssetRcNo As String


    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged
        MakeAssetNull()
        Dim editindex As Integer = GridView3.SelectedIndex
        rcno = DirectCast(GridView3.Rows(editindex).FindControl("Label1"), Label).Text
        txtAssetRcNo.Text = rcno.ToString()
        txtAssetNo.Text = GridView3.SelectedRow.Cells(1).Text
        txtAssetRegNo.Text = GridView3.SelectedRow.Cells(2).Text
        txtAssetDesc.Text = GridView3.SelectedRow.Cells(3).Text
        txtType.Text = GridView3.SelectedRow.Cells(4).Text

        btnAssetEdit.Enabled = True
        btnAssetEdit.ForeColor = System.Drawing.Color.Black
        btnAssetDelete.Enabled = True
        btnAssetDelete.ForeColor = System.Drawing.Color.Black

        'EnableAssetControls()

    End Sub

    Private Sub CheckTab()
        'If tb1.ActiveTabIndex = 1 Or tb1.ActiveTabIndex = 2 Then
        '    GridView1.CssClass = "dummybutton"
        '    btnADD.CssClass = "dummybutton"
        '    btnEdit.CssClass = "dummybutton"
        '    btnDelete.CssClass = "dummybutton"
        '    btnQuit.CssClass = "dummybutton"
        '    btnPrint.CssClass = "dummybutton"

        'ElseIf tb1.ActiveTabIndex = 0 Then

        '    GridView1.CssClass = "visiblebutton"
        '    btnADD.CssClass = "visiblebutton"
        '    btnEdit.CssClass = "visiblebutton"
        '    btnDelete.CssClass = "visiblebutton"
        '    btnQuit.CssClass = "visiblebutton"
        '    btnPrint.CssClass = "visiblebutton"

        'End If
    End Sub

    Protected Sub btnAssetCancel_Click(sender As Object, e As EventArgs) Handles btnAssetCancel.Click
        MakeAssetNull()
        'EnableAssetControls()
        DisableAssetControls()
    End Sub

    Protected Sub btnAssetAdd_Click(sender As Object, e As EventArgs) Handles btnAssetAdd.Click
        'DisableAssetControls()
        EnableAssetControls()
        MakeAssetNull()
        lblMessage.Text = "ACTION: ADD TEAM ASSET"


        txtAssetMode.Text = "Add"
        txtAssetNo.Focus()
        CheckTab()
     
    End Sub

    Protected Sub btnAssetSave_Click(sender As Object, e As EventArgs) Handles btnAssetSave.Click
        If String.IsNullOrEmpty(txtOrigCode.Text) Then
            lblAlert.Text = "SELECT TEAM TO ADD ASSET"
            Return
        End If
        If String.IsNullOrEmpty(txtAssetNo.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ASSET NO CANNOT BE BLANK"
            Return
        End If

        If txtAssetMode.Text = "Add" Then
            '  Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblteamasset where TeamSQLID=@TeamSQLID and ASSETNO=@ASSETNO"
            command1.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)
            command1.Parameters.AddWithValue("@ASSETNO", txtAssetNo.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                '    MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")
                lblAlert.Text = "SELECTED ASSET ALREADY ASSIGNED FOR THIS TEAM"

            Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblteamasset(AssetNo,AssetRegNo,Description,Type,TeamSQLID,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)VALUES(@AssetNo,@AssetRegNo,@Description,@Type,@TeamSQLID,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"
                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)

                    command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)
                    command.Parameters.AddWithValue("@Description", txtAssetDesc.Text.ToUpper)
                    command.Parameters.AddWithValue("@Type", txtType.Text.ToUpper)
                    command.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text.ToUpper)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)

                    command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)
                    command.Parameters.AddWithValue("@Description", txtAssetDesc.Text)
                    command.Parameters.AddWithValue("@Type", txtType.Text)
                    command.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If


                command.Connection = conn

                command.ExecuteNonQuery()
                txtAssetRcNo.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

            End If
            conn.Close()

            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
            'End Try
            'EnableAssetControls()

        ElseIf txtAssetMode.Text = "Edit" Then
            If txtAssetRcNo.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            '    Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text
            command2.CommandText = "SELECT * FROM tblteamasset where TeamSQLID=@TeamSQLID and ASSETNO=@ASSETNO and rcno<>" & Convert.ToInt32(txtAssetRcNo.Text)
            command2.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)
            command2.Parameters.AddWithValue("@ASSETNO", txtAssetNo.Text)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                '   MessageBox.Message.Alert(Page, "Selected Staff already assigned for this service!!!", "str")
                lblAlert.Text = "SELECTED ASSET ALREADY ASSIGNED FOR THIS TEAM"


            Else

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblteamasset where rcno=" & Convert.ToInt32(txtAssetRcNo.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "UPDATE tblteamasset SET AssetNo = @AssetNo,AssetRegNo = @AssetRegNo,Description = @Description,Type = @Type,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE RcNo =" & Convert.ToInt32(txtAssetRcNo.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text.ToUpper)

                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text.ToUpper)
                        command.Parameters.AddWithValue("@Description", txtAssetDesc.Text.ToUpper)
                        command.Parameters.AddWithValue("@Type", txtType.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@AssetNo", txtAssetNo.Text)

                        command.Parameters.AddWithValue("@AssetRegNo", txtAssetRegNo.Text)
                        command.Parameters.AddWithValue("@Description", txtAssetDesc.Text)
                        command.Parameters.AddWithValue("@Type", txtType.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    lblAlert.Text = ""
                End If
            End If

            conn.Close()

            'Catch ex As Exception

            '    MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
            'End Try
            'EnableAssetControls()

        End If

        'MakeStaffNull()
        DisableAssetControls()


        GridView3.DataSourceID = "SqlDSTeamAsset"
        '    MakeTechNull()
        txtAssetMode.Text = ""
    End Sub

    Protected Sub btnAssetEdit_Click(sender As Object, e As EventArgs) Handles btnAssetEdit.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        If txtAssetRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
            DisableAssetControls()
        txtAssetMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT TEAM ASSET"


        txtAssetNo.Enabled = True
        txtAssetDesc.Enabled = True
        txtAssetRegNo.Enabled = True
        txtType.Enabled = True

        btnAssetSave.Enabled = True
        btnAssetSave.ForeColor = System.Drawing.Color.Black
        btnAssetCancel.Enabled = True
        btnAssetCancel.ForeColor = System.Drawing.Color.Black
    End Sub

    Protected Sub btnAssetDelete_Click(sender As Object, e As EventArgs) Handles btnAssetDelete.Click
        lblMessage.Text = ""
        If txtAssetRcNo.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE TEAM ASSET"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblteamasset where rcno=" & Convert.ToInt32(txtAssetRcNo.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblteamasset where rcno=" & Convert.ToInt32(txtAssetRcNo.Text)

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
            EnableAssetControls()


            GridView3.DataBind()
            MakeAssetNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If

    End Sub

    Protected Sub ddlTechID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTechID.SelectedIndexChanged
        Dim name As String = ddlTechID.SelectedItem.Text.Substring(ddlTechID.SelectedItem.Text.IndexOf("["))
        name = name.Replace("]", "")
        name = name.Replace("[", "")
        txtStaffName.Text = name

    End Sub

    Private Sub MakeStaffNull()

        txtStaffMode.Text = ""

        ddlTechID.SelectedIndex = 0
        txtStaffName.Text = ""
        txtRoles.Text = ""
       
        txtStaffRcno.Text = ""
        txtType.Text = ""
    End Sub

    Private Sub EnableStaffControls()
        btnStaffSave.Enabled = True
        btnStaffSave.ForeColor = System.Drawing.Color.Black
        btnStaffCancel.Enabled = True
        btnStaffCancel.ForeColor = System.Drawing.Color.Black

        btnStaffAdd.Enabled = True
        btnStaffAdd.ForeColor = System.Drawing.Color.Black
        'btnStaffEdit.Enabled = True
        'btnStaffEdit.ForeColor = System.Drawing.Color.Black
        'btnStaffDelete.Enabled = True
        'btnStaffDelete.ForeColor = System.Drawing.Color.Black

        ddlTechID.Enabled = True
        txtStaffName.Enabled = True
        txtRoles.Enabled = True

    End Sub

    Private Sub DisableStaffControls()
        'btnStaffSave.Enabled = True
        'btnStaffSave.ForeColor = System.Drawing.Color.Black
        'btnStaffCancel.Enabled = True
        'btnStaffCancel.ForeColor = System.Drawing.Color.Black

        btnStaffSave.Enabled = False
        btnStaffSave.ForeColor = System.Drawing.Color.Gray
        btnStaffCancel.Enabled = False
        btnStaffCancel.ForeColor = System.Drawing.Color.Gray

        'btnStaffAdd.Enabled = False
        'btnStaffAdd.ForeColor = System.Drawing.Color.Gray

        btnStaffEdit.Enabled = False
        btnStaffEdit.ForeColor = System.Drawing.Color.Gray

        btnStaffDelete.Enabled = False
        btnStaffDelete.ForeColor = System.Drawing.Color.Gray


        'btnQuit.Enabled = False
        'btnQuit.ForeColor = System.Drawing.Color.Gray

        ddlTechID.Enabled = False
        txtStaffName.Enabled = False
        txtRoles.Enabled = False
        '   btnQuit.CssClass = "visiblebutton"
    End Sub

    Public StaffRcNo As String


    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        MakeStaffNull()
        Dim editindex As Integer = GridView2.SelectedIndex
        rcno = DirectCast(GridView2.Rows(editindex).FindControl("Label1"), Label).Text
        txtStaffRcno.Text = rcno.ToString()
        ddlTechID.SelectedValue = GridView2.SelectedRow.Cells(1).Text
        txtStaffName.Text = GridView2.SelectedRow.Cells(2).Text
        If String.IsNullOrEmpty(GridView2.SelectedRow.Cells(3).Text) = True Or GridView2.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtRoles.Text = ""
        Else
            txtRoles.Text = GridView2.SelectedRow.Cells(3).Text
        End If

        btnStaffEdit.Enabled = True
        btnStaffEdit.ForeColor = System.Drawing.Color.Black
        btnStaffDelete.Enabled = True
        btnStaffDelete.ForeColor = System.Drawing.Color.Black

        'EnableStaffControls()
    End Sub

    Protected Sub btnStaffCancel_Click(sender As Object, e As EventArgs) Handles btnStaffCancel.Click
        MakeStaffNull()
        DisableStaffControls()
        'EnableStaffControls()
    End Sub

    Protected Sub btnStaffAdd_Click(sender As Object, e As EventArgs) Handles btnStaffAdd.Click
        'DisableStaffControls()
        EnableStaffControls()
        MakeStaffNull()
        lblMessage.Text = "ACTION: ADD TEAM STAFF"


        txtStaffMode.Text = "Add"
        ddlTechID.Focus()
        CheckTab()
    End Sub

    Protected Sub btnStaffSave_Click(sender As Object, e As EventArgs) Handles btnStaffSave.Click
        If String.IsNullOrEmpty(txtOrigCode.Text) Then
            lblAlert.Text = "SELECT TEAM TO ADD STAFF"
            Return
        End If
        If ddlTechID.Text = "--SELECT--" Or ddlTechID.Text = "-1" Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "SELECT STAFF TO PROCEED"
            Return
        End If

        If txtStaffMode.Text = "Add" Then
            '  Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblteamstaff where TeamSQLID=@TeamSQLID and STAFFID=@STAFFID"
            command1.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)
            command1.Parameters.AddWithValue("@STAFFID", ddlTechID.SelectedValue.ToString)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                '    MessageBox.Message.Alert(Page, "Selected  already assigned for this service!!!", "str")
                lblAlert.Text = "SELECTED STAFF ALREADY ASSIGNED FOR THIS TEAM"

            Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblteamstaff(Status,StaffID,StaffName,EffectiveDate,Roles,TeamSQLID,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)VALUES(@Status,@StaffID,@StaffName,@EffectiveDate,@Roles,@TeamSQLID,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);;"
                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                    command.Parameters.AddWithValue("@Status", "O")

                    command.Parameters.AddWithValue("@StaffID", ddlTechID.SelectedValue.ToString)

                    command.Parameters.AddWithValue("@StaffName", txtStaffName.Text.ToUpper)
                    command.Parameters.AddWithValue("@EffectiveDate", DBNull.Value)
                    command.Parameters.AddWithValue("@Roles", txtRoles.Text.ToUpper)
                    command.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text.ToUpper)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                    command.Parameters.AddWithValue("@Status", "O")

                    command.Parameters.AddWithValue("@StaffID", ddlTechID.SelectedValue.ToString)

                    command.Parameters.AddWithValue("@StaffName", txtStaffName.Text)
                    command.Parameters.AddWithValue("@EffectiveDate", DBNull.Value)
                    command.Parameters.AddWithValue("@Roles", txtRoles.Text)
                    command.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                End If



                command.Connection = conn

                command.ExecuteNonQuery()
                txtStaffRcno.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

            End If
            conn.Close()

            'Catch ex As Exception
            '    MessageBox.Message.Alert(Page, "Error!!!" + ex.Message.ToString, "str")
            'End Try
            'EnableStaffControls()

        ElseIf txtStaffMode.Text = "Edit" Then
            If txtStaffRcno.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            '    Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text
            command2.CommandText = "SELECT * FROM tblteamstaff where TeamSQLID=@TeamSQLID and StaffID=@StaffID and rcno<>" & Convert.ToInt32(txtStaffRcno.Text)
            command2.Parameters.AddWithValue("@TeamSQLID", txtOrigCode.Text)
            command2.Parameters.AddWithValue("@StaffID", ddlTechID.SelectedValue.ToString)
            command2.Connection = conn

            Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then

                '   MessageBox.Message.Alert(Page, "Selected  already assigned for this service!!!", "str")
                lblAlert.Text = "SELECTED STAFF ALREADY ASSIGNED FOR THIS TEAM"


            Else

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblteamSTAFF where rcno=" & Convert.ToInt32(txtStaffRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "UPDATE tblteamstaff SET StaffID = @StaffID,StaffName = @StaffName,Roles = @Roles,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE rcno = " & Convert.ToInt32(txtStaffRcno.Text)

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@Status", "O")

                        command.Parameters.AddWithValue("@StaffID", ddlTechID.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@StaffName", txtStaffName.Text)
                        command.Parameters.AddWithValue("@Roles", txtRoles.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@Status", "O")

                        command.Parameters.AddWithValue("@StaffID", ddlTechID.SelectedValue.ToString)
                        command.Parameters.AddWithValue("@StaffName", txtStaffName.ToString)
                        command.Parameters.AddWithValue("@Roles", txtRoles.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    End If
                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                    lblAlert.Text = ""
                End If
            End If

            conn.Close()

            'Catch ex As Exception

            '    MessageBox.Message.Alert(Page, "Error!!! " + ex.ToString, "str")
            'End Try
            'EnableStaffControls()

        End If
        MakeStaffNull()
        DisableStaffControls()
        GridView2.DataSourceID = "SqlDSTeamStaff"
        '    MakeTechNull()
        txtStaffMode.Text = ""
    End Sub

    Protected Sub btnStaffEdit_Click(sender As Object, e As EventArgs) Handles btnStaffEdit.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        If txtStaffRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return

        End If
        DisableStaffControls()
        txtStaffMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT TEAM STAFF"

        ddlTechID.Enabled = True
        txtStaffName.Enabled = True
        txtRoles.Enabled = True

        btnStaffSave.Enabled = True
        btnStaffSave.ForeColor = System.Drawing.Color.Black
        btnStaffCancel.Enabled = True
        btnStaffCancel.ForeColor = System.Drawing.Color.Black
    End Sub

    Protected Sub btnStaffDelete_Click(sender As Object, e As EventArgs) Handles btnStaffDelete.Click
        lblMessage.Text = ""
        If txtStaffRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE TEAM STAFF"

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblteamstaff where rcno=" & Convert.ToInt32(txtStaffRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblteamstaff where rcno=" & Convert.ToInt32(txtStaffRcno.Text)

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
            EnableStaffControls()


            GridView2.DataBind()
            MakeStaffNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim qry As String
        qry = "select * from tblteam where 1=1 "
        If String.IsNullOrEmpty(txtSearchStaffID.Text) = False Then

            qry = qry + " and teamid like '%" + txtSearchStaffID.Text + "%'"
        End If

        If String.IsNullOrEmpty(txtSearchName.Text) = False Then

            qry = qry + " and teamname like '%" + txtSearchName.Text + "%'"
        End If

        If String.IsNullOrEmpty(txtSearchInChargeId.Text) = False Then

            qry = qry + " and InChargeId like '%" + txtSearchInChargeId.Text + "%'"
        End If
       
        If chkInActive.Checked = True Then
            qry = qry + " and Status = 'N'"
        Else
            qry = qry + " and Status <> 'N'"
        End If
        qry = qry + " order by createdon desc,teamname;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        txtSearchStaffID.Text = ""
        txtSearchName.Text = ""
        txtSearchInChargeId.Text = ""
    End Sub

    Protected Sub txtSearchTeam_TextChanged(sender As Object, e As EventArgs) Handles txtSearchTeam.TextChanged
        txtSearchText.Text = txtSearchTeam.Text

        Dim qry As String
        qry = "select * from tblTeam where 1=1"
        If String.IsNullOrEmpty(txtSearchTeam.Text) = False Then

            qry = qry + " and (TeamID like '%" + txtSearchTeam.Text + "%'"

            qry = qry + " or TeamName like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or InChargeId like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or DepartmentID like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or Supervisor like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or SecondInChargeID like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or Coordinator like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or VehNos like '%" + txtSearchTeam.Text + "%'"
            qry = qry + " or Notes like '%" + txtSearchTeam.Text + "%')"
        End If

        qry = qry + " order by TeamID;"
        txt.Text = qry
        MakeMeNull()
        SqlDataSource1.SelectCommand = qry
        SqlDataSource1.DataBind()
        GridView1.DataBind()

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchTeam.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString

        txtSearchTeam.Text = "Search Here"
    End Sub

    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click

        txtSearchText.Text = txtSearchTeam.Text

        If txtSearchTeam.Text <> "Search Here" Then
            Dim qry As String
            qry = "select * from tblTeam where 1=1"
            If String.IsNullOrEmpty(txtSearchTeam.Text) = False Then

                qry = qry + " and (TeamID like '%" + txtSearchTeam.Text + "%'"

                qry = qry + " or TeamName like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or InChargeId like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or DepartmentID like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or Supervisor like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or SecondInChargeID like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or Coordinator like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or VehNos like '%" + txtSearchTeam.Text + "%'"
                qry = qry + " or Notes like '%" + txtSearchTeam.Text + "%')"
            End If

            qry = qry + " order by TeamID;"
            txt.Text = qry
            MakeMeNull()
            SqlDataSource1.SelectCommand = qry
            SqlDataSource1.DataBind()
            GridView1.DataBind()
          
            lblMessage.Text = "SEARCH CRITERIA : " + txtSearchTeam.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count.ToString


        Else
            txtSearchTeam.Text = "Search Here"
        End If
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
    End Sub

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
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#00ccff")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If


            End If
        Next
    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        lblMessage.Text = ""
        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO UPDATE STATUS"
            Return

        End If
        lblTeamIDStatus.Text = txtTeamID.Text
        lblOldStatus.Text = ddlStatus.SelectedItem.Text
        ddlNewStatus.SelectedValue = ddlStatus.SelectedValue

        lblAlertStatus.Text = ""
        mdlPopupStatus.Show()
    End Sub


    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        'If ddlNewStatus.Text = txtDDLText.Text Then
        '    lblAlertStatus.Text = "SELECT NEW STATUS"
        '    mdlPopupStatus.Show()

        '    Return

        'End If
        If ddlNewStatus.Text = ddlStatus.Text Then
            lblAlertStatus.Text = "STATUS ALREADY UPDATED"
            mdlPopupStatus.Show()

            Return
        End If
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            If lblOldStatus.Text = "D" Then
                command.CommandText = "UPDATE tblteam SET STatus='" + ddlNewStatus.SelectedValue + "', LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
            Else

                command.CommandText = "UPDATE tblteam SET STATUS='" + ddlNewStatus.SelectedValue + "',LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)

            End If

            command.Connection = conn
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

          
            command.ExecuteNonQuery()


            conn.Close()
            GridView1.DataBind()

            ddlStatus.Text = ddlNewStatus.Text
            ddlNewStatus.SelectedIndex = 0
             lblMessage.Text = "ACTION: STATUS UPDATED"
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST-STAFF", txtTeamID.Text, "CHST-TEAM", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtTeamID.Text, "", txtRcno.Text)

            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()
            GridView1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            mdlPopupStatus.Hide()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        MakeMeNull()
        EnableControls()

        txt.Text = "SELECT * FROM tblTEAM WHERE Status <>'N' order by teamId"
        SqlDataSource1.SelectCommand = "SELECT * FROM tblTEAM WHERE Status <>'N' order by teamId"
        SqlDataSource1.DataBind()
        GridView1.DataBind()
        lblMessage.Text = ""
    End Sub
End Class

