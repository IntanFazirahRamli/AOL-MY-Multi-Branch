Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_BillingFrequency
    Inherits System.Web.UI.Page


    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""
        txtFrequency.Text = ""
        txtNoofDays.Text = ""
        txtNoofMonths.Text = ""
        txtNoofWeeks.Text = ""
        txtNoofYears.Text = ""
        txtMaxDaySvc.Text = ""
        txtMaxWeekSvc.Text = ""
        txtMaxSvcInterval.Text = ""
        txtDOW.Text = ""
        txtDOWWeek.Text = ""
        txtMonthByMonth.Text = ""
        txtByDate.Text = ""
        txtFrequencyMethod.Text = ""
        txtActive.Text = ""
        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        chkAutoCalculateServiceValue.Checked = False
    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        'btnADD.Enabled = True
        'btnADD.ForeColor = System.Drawing.Color.Black
        'btnEdit.Enabled = True
        'btnEdit.ForeColor = System.Drawing.Color.Black
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black


        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        btnPrint.Enabled = True
        btnPrint.ForeColor = System.Drawing.Color.Black
        txtFrequency.Enabled = False
        txtNoofDays.Enabled = False
        txtNoofMonths.Enabled = False
        txtNoofWeeks.Enabled = False
        txtNoofYears.Enabled = False
        txtMaxDaySvc.Enabled = False
        txtMaxWeekSvc.Enabled = False
        txtMaxSvcInterval.Enabled = False
        txtDOW.Enabled = False
        txtDOWWeek.Enabled = False
        txtMonthByMonth.Enabled = False
        txtByDate.Enabled = False
        txtFrequencyMethod.Enabled = False
        chkAutoCalculateServiceValue.Enabled = False
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

        'btnQuit.Enabled = False
        'btnQuit.ForeColor = System.Drawing.Color.Gray

        'btnPrint.Enabled = False
        'btnPrint.ForeColor = System.Drawing.Color.Gray

        txtFrequency.Enabled = True
        txtNoofDays.Enabled = True
        txtNoofMonths.Enabled = True
        txtNoofWeeks.Enabled = True
        txtNoofYears.Enabled = True
        txtMaxDaySvc.Enabled = True
        txtMaxWeekSvc.Enabled = True
        txtMaxSvcInterval.Enabled = True
        txtDOW.Enabled = True
        txtDOWWeek.Enabled = True
        txtMonthByMonth.Enabled = True
        txtByDate.Enabled = True
        txtFrequencyMethod.Enabled = True
        chkAutoCalculateServiceValue.Enabled = True
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
            txtFrequency.Text = ""
        Else

            txtFrequency.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
            txtNoofDays.Text = ""
        Else
            txtNoofDays.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(7).Text = "&nbsp;" Then
            txtNoofWeeks.Text = ""
        Else
            txtNoofWeeks.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(7).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(8).Text = "&nbsp;" Then
            txtNoofMonths.Text = ""
        Else
            txtNoofMonths.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(8).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(9).Text = "&nbsp;" Then
            txtNoofYears.Text = ""
        Else
            txtNoofYears.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(9).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(10).Text = "&nbsp;" Then
            txtMaxDaySvc.Text = ""
        Else
            txtMaxDaySvc.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(10).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(11).Text = "&nbsp;" Then
            txtMaxWeekSvc.Text = ""
        Else
            txtMaxWeekSvc.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(11).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(12).Text = "&nbsp;" Then
            txtMaxSvcInterval.Text = ""
        Else
            txtMaxSvcInterval.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(12).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(13).Text = "&nbsp;" Then
            txtDOW.Text = ""
        Else
            txtDOW.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(13).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(14).Text = "&nbsp;" Then
            txtDOWWeek.Text = ""
        Else
            txtDOWWeek.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(14).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(15).Text = "&nbsp;" Then
            txtMonthByMonth.Text = ""
        Else
            txtMonthByMonth.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(15).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(16).Text = "&nbsp;" Then
            txtByDate.Text = ""
        Else
            txtByDate.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(16).Text).ToString
        End If
        If GridView1.SelectedRow.Cells(17).Text = "&nbsp;" Then
            txtFrequencyMethod.Text = ""
        Else
            txtFrequencyMethod.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(17).Text).ToString
        End If

        If GridView1.SelectedRow.Cells(18).Text = "1" Then
            chkAutoCalculateServiceValue.Checked = True
        Else
            chkAutoCalculateServiceValue.Checked = False
        End If


        txtMode.Text = "View"
        'txtMode.Text = "Edit"
        'DisableControls()
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
        EnableControls()
    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        MakeMeNull()

        txtMode.Text = "New"
        DisableControls()
        lblMessage.Text = "ACTION: ADD RECORD"
        txtFrequency.Focus()

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
            ' '''''''''''''''''''Access Control 
            'Dim conn As MySqlConnection = New MySqlConnection()
            'Dim command As MySqlCommand = New MySqlCommand

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'command.CommandType = CommandType.Text
            ''command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete, X0109Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            'command.CommandText = "SELECT X0109,  X0109Add, X0109Edit, X0109Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            'command.Connection = conn

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then

            '    If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("X0109"))) = False Then
            '        If Convert.ToBoolean(dt.Rows(0)("X0109")) = False Then
            '            Response.Redirect("Home.aspx")
            '        End If
            '    End If



            '    If String.IsNullOrEmpty(dt.Rows(0)("X0109Add")) = False Then
            '        Me.btnADD.Enabled = dt.Rows(0)("X0109Add").ToString()
            '    End If

            '    'Me.btnInsert.Enabled = vpSec2412Add
            '    If txtMode.Text = "View" Then
            '        If String.IsNullOrEmpty(dt.Rows(0)("X0109Edit")) = False Then
            '            Me.btnEdit.Enabled = dt.Rows(0)("X0109Edit").ToString()
            '        End If

            '        If String.IsNullOrEmpty(dt.Rows(0)("X0109Delete")) = False Then
            '            Me.btnDelete.Enabled = dt.Rows(0)("X0109Delete").ToString()
            '        End If
            '    Else
            '        Me.btnEdit.Enabled = False
            '        Me.btnDelete.Enabled = False

            '    End If

            '    If btnADD.Enabled = True Then
            '        btnADD.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnADD.ForeColor = System.Drawing.Color.Gray
            '    End If


            '    If btnEdit.Enabled = True Then
            '        btnEdit.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnEdit.ForeColor = System.Drawing.Color.Gray
            '    End If

            '    If btnDelete.Enabled = True Then
            '        btnDelete.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnDelete.ForeColor = System.Drawing.Color.Gray
            '    End If


            '    If btnPrint.Enabled = True Then
            '        btnPrint.ForeColor = System.Drawing.Color.Black
            '    Else
            '        btnPrint.ForeColor = System.Drawing.Color.Gray
            '    End If


            'End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtFrequency.Text = "" Then
            'MessageBox.Message.Alert(Page, "Frequency cannot be blank!!!", "str")
            lblAlert.Text = "FREQUENCY CANNOT BE BLANK"
            Return

        End If
        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblservicefrequency where frequency=@ind"
                command1.Parameters.AddWithValue("@ind", txtFrequency.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    Exit Sub


                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblservicefrequency(Frequency,NoService,NoDays,Active,CreatedBy,NoOfWks,NoOfMths,NoOfYears,MaxNoDaySvs,MaxNoWeekSvs,MaxNoSvsInterval,ByDOW,DOWByWhichWeek,MonthByWhichMonth,ByDate,FreqMtd,AutoCalculateServiceValue)"
                    qry = qry + "VALUES(@Frequency,@NoService,@NoDays,@Active,@CreatedBy,@NoOfWks,@NoOfMths,@NoOfYears,@MaxNoDaySvs,@MaxNoWeekSvs,@MaxNoSvsInterval,@ByDOW,@DOWByWhichWeek,@MonthByWhichMonth,@ByDate,@FreqMtd,@AutoCalculateServiceValue);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@frequency", txtFrequency.Text.ToUpper)
                        command.Parameters.AddWithValue("@NoService", 0)
                        If txtNoofDays.Text = "" Then
                            command.Parameters.AddWithValue("@NoDays", 0)
                        Else
                            command.Parameters.AddWithValue("@NoDays", Convert.ToDecimal(txtNoofDays.Text))
                        End If
                        command.Parameters.AddWithValue("@Active", 1)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        If txtNoofWeeks.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfWks", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfWks", Convert.ToDecimal(txtNoofWeeks.Text))
                        End If
                        If txtNoofMonths.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfMths", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfMths", Convert.ToDecimal(txtNoofMonths.Text))

                        End If
                        If txtNoofYears.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfYears", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfYears", Convert.ToDecimal(txtNoofYears.Text))
                        End If
                        If txtMaxDaySvc.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoDaySvs", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoDaySvs", Convert.ToDecimal(txtMaxDaySvc.Text))
                        End If
                        If txtMaxWeekSvc.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoWeekSvs", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoWeekSvs", Convert.ToDecimal(txtMaxWeekSvc.Text))
                        End If
                        If txtMaxSvcInterval.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoSvsInterval", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoSvsInterval", Convert.ToDecimal(txtMaxSvcInterval.Text))
                        End If
                        command.Parameters.AddWithValue("@ByDOW", txtDOW.Text.ToUpper)
                        command.Parameters.AddWithValue("@DOWByWhichWeek", txtDOWWeek.Text.ToUpper)

                        command.Parameters.AddWithValue("@MonthByWhichMonth", txtMonthByMonth.Text.ToUpper)
                        command.Parameters.AddWithValue("@ByDate", txtByDate.Text.ToUpper)
                        command.Parameters.AddWithValue("@FreqMtd", txtFrequencyMethod.Text.ToUpper)
                        command.Parameters.AddWithValue("@AutoCalculateServiceValue", chkAutoCalculateServiceValue.Checked)
                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@frequency", txtFrequency.Text.ToUpper)
                        command.Parameters.AddWithValue("@NoService", 0)
                        If txtNoofDays.Text = "" Then
                            command.Parameters.AddWithValue("@NoDays", 0)
                        Else
                            command.Parameters.AddWithValue("@NoDays", Convert.ToDecimal(txtNoofDays.Text))
                        End If
                        command.Parameters.AddWithValue("@Active", 1)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        If txtNoofWeeks.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfWks", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfWks", Convert.ToDecimal(txtNoofWeeks.Text))
                        End If
                        If txtNoofMonths.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfMths", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfMths", Convert.ToDecimal(txtNoofMonths.Text))

                        End If
                        If txtNoofYears.Text = "" Then
                            command.Parameters.AddWithValue("@NoOfYears", 0)
                        Else
                            command.Parameters.AddWithValue("@NoOfYears", Convert.ToDecimal(txtNoofYears.Text))
                        End If
                        If txtMaxDaySvc.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoDaySvs", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoDaySvs", Convert.ToDecimal(txtMaxDaySvc.Text))
                        End If
                        If txtMaxWeekSvc.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoWeekSvs", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoWeekSvs", Convert.ToDecimal(txtMaxWeekSvc.Text))
                        End If
                        If txtMaxSvcInterval.Text = "" Then
                            command.Parameters.AddWithValue("@MaxNoSvsInterval", 0)
                        Else
                            command.Parameters.AddWithValue("@MaxNoSvsInterval", Convert.ToDecimal(txtMaxSvcInterval.Text))
                        End If
                        command.Parameters.AddWithValue("@ByDOW", txtDOW.Text.ToUpper)
                        command.Parameters.AddWithValue("@DOWByWhichWeek", txtDOWWeek.Text.ToUpper)

                        command.Parameters.AddWithValue("@MonthByWhichMonth", txtMonthByMonth.Text.ToUpper)
                        command.Parameters.AddWithValue("@ByDate", txtByDate.Text.ToUpper)
                        command.Parameters.AddWithValue("@FreqMtd", txtFrequencyMethod.Text.ToUpper)
                        command.Parameters.AddWithValue("@AutoCalculateServiceValue", chkAutoCalculateServiceValue.Checked)
                    End If

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    txtRcno.Text = command.LastInsertedId

                    '  MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                    lblAlert.Text = ""
                End If
                conn.Close()
                If txtMode.Text = "NEW" Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVFREQ", txtFrequency.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVFREQ", txtFrequency.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
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

            'If txtExists.Text = "True" Then
            '    ' MessageBox.Message.Alert(Page, "Record is in use, cannot be modified!!!", "str")
            '    lblAlert.Text = "RECORD IS IN USE,CANNOT BE MODIFIED"
            '    Return
            'End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim ind As String
                'ind = txtscheduletype.Text
                'ind = ind.Replace("'", "\\'")

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                command2.CommandText = "SELECT * FROM tblservicefrequency where frequency=@ind and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@ind", txtFrequency.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '   MessageBox.Message.Alert(Page, "Frequency already exists!!!", "str")
                    lblAlert.Text = "FREQUENCY ALREADY EXISTS"
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblservicefrequency where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String

                        'qry = "update tblservicefrequency set Frequency = @Frequency,NoDays = @NoDays,NoOfWks = @NoOfWks,NoOfMths = @NoOfMths,NoOfYears = @NoOfYears,MaxNoDaySvs = @MaxNoDaySvs,MaxNoWeekSvs = @MaxNoWeekSvs,MaxNoSvsInterval = @MaxNoSvsInterval,ByDOW = @ByDOW,DOWByWhichWeek = @DOWByWhichWeek,MonthByWhichMonth = @MonthByWhichMonth,ByDate = @ByDate,FreqMtd = @FreqMtd, AutoCalculateServiceValue=@AutoCalculateServiceValue where rcno=" & Convert.ToInt32(txtRcno.Text)

                        qry = "update tblservicefrequency set  AutoCalculateServiceValue=@AutoCalculateServiceValue where rcno=" & Convert.ToInt32(txtRcno.Text)

                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            'command.Parameters.AddWithValue("@frequency", txtFrequency.Text.ToUpper)
                            'command.Parameters.AddWithValue("@NoService", 0)
                            'If txtNoofDays.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoDays", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoDays", Convert.ToDecimal(txtNoofDays.Text))
                            'End If
                            'command.Parameters.AddWithValue("@Active", 1)
                            'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                            'If txtNoofWeeks.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfWks", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfWks", Convert.ToDecimal(txtNoofWeeks.Text))
                            'End If
                            'If txtNoofMonths.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfMths", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfMths", Convert.ToDecimal(txtNoofMonths.Text))

                            'End If
                            'If txtNoofYears.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfYears", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfYears", Convert.ToDecimal(txtNoofYears.Text))
                            'End If
                            'If txtMaxDaySvc.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoDaySvs", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoDaySvs", Convert.ToDecimal(txtMaxDaySvc.Text))
                            'End If
                            'If txtMaxWeekSvc.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoWeekSvs", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoWeekSvs", Convert.ToDecimal(txtMaxWeekSvc.Text))
                            'End If
                            'If txtMaxSvcInterval.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoSvsInterval", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoSvsInterval", Convert.ToDecimal(txtMaxSvcInterval.Text))
                            'End If
                            'command.Parameters.AddWithValue("@ByDOW", txtDOW.Text.ToUpper)
                            'command.Parameters.AddWithValue("@DOWByWhichWeek", txtDOWWeek.Text.ToUpper)

                            'command.Parameters.AddWithValue("@MonthByWhichMonth", txtMonthByMonth.Text.ToUpper)
                            'command.Parameters.AddWithValue("@ByDate", txtByDate.Text.ToUpper)
                            'command.Parameters.AddWithValue("@FreqMtd", txtFrequencyMethod.Text.ToUpper)

                            command.Parameters.AddWithValue("@AutoCalculateServiceValue", chkAutoCalculateServiceValue.Checked)

                            'If chkAutoCalculateServiceValue.Checked = True Then
                            '    command.Parameters.AddWithValue("@AutoCalculateServiceValue", True)
                            'Else
                            '    command.Parameters.AddWithValue("@AutoCalculateServiceValue", False)
                            'End If

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            'command.Parameters.AddWithValue("@frequency", txtFrequency.Text.ToUpper)
                            'command.Parameters.AddWithValue("@NoService", 0)
                            'If txtNoofDays.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoDays", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoDays", Convert.ToDecimal(txtNoofDays.Text))
                            'End If
                            'command.Parameters.AddWithValue("@Active", 1)
                            'command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                            'If txtNoofWeeks.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfWks", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfWks", Convert.ToDecimal(txtNoofWeeks.Text))
                            'End If
                            'If txtNoofMonths.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfMths", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfMths", Convert.ToDecimal(txtNoofMonths.Text))

                            'End If
                            'If txtNoofYears.Text = "" Then
                            '    command.Parameters.AddWithValue("@NoOfYears", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@NoOfYears", Convert.ToDecimal(txtNoofYears.Text))
                            'End If
                            'If txtMaxDaySvc.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoDaySvs", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoDaySvs", Convert.ToDecimal(txtMaxDaySvc.Text))
                            'End If
                            'If txtMaxWeekSvc.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoWeekSvs", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoWeekSvs", Convert.ToDecimal(txtMaxWeekSvc.Text))
                            'End If
                            'If txtMaxSvcInterval.Text = "" Then
                            '    command.Parameters.AddWithValue("@MaxNoSvsInterval", 0)
                            'Else
                            '    command.Parameters.AddWithValue("@MaxNoSvsInterval", Convert.ToDecimal(txtMaxSvcInterval.Text))
                            'End If
                            'command.Parameters.AddWithValue("@ByDOW", txtDOW.Text.ToUpper)
                            'command.Parameters.AddWithValue("@DOWByWhichWeek", txtDOWWeek.Text.ToUpper)

                            'command.Parameters.AddWithValue("@MonthByWhichMonth", txtMonthByMonth.Text.ToUpper)
                            'command.Parameters.AddWithValue("@ByDate", txtByDate.Text.ToUpper)
                            'command.Parameters.AddWithValue("@FreqMtd", txtFrequencyMethod.Text.ToUpper)
                            command.Parameters.AddWithValue("@AutoCalculateServiceValue", chkAutoCalculateServiceValue.Checked)
                        End If



                        command.Connection = conn

                        command.ExecuteNonQuery()


                        '   MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                        lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                        lblAlert.Text = ""
                    End If
                End If

                conn.Close()

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

                command1.CommandText = "SELECT * FROM tblservicefrequency where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblservicefrequency where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()
                    MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVFREQ", txtFrequency.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                    ' MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
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
        Response.Redirect("RV_MasterServiceFrequency.aspx")

    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT * FROM tblcontract where ServiceFrequence=@data"
        command3.Parameters.AddWithValue("@data", txtFrequency.Text)
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            Return True
        End If

        conn.Close()

        Return False
    End Function
End Class
