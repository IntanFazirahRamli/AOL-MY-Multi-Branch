Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_ContractGroup
    Inherits System.Web.UI.Page

  
    Public rcno As String

    Public Sub MakeMeNull()
        txtMode.Text = ""
        txtContractGroup.Text = ""
        txtGroupDescription.Text = ""
        txtRcno.Text = ""
        txtCommPct.Text = "0.00"

        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        lblAlert.Text = ""
        ddlDefServiceID.SelectedIndex = 0
        chkContractValueAllowEdit.Checked = False
        chkIncludeInPortfolio.Checked = False

        chkShowExpiryNotification.Checked = False
        chkAutoExpireContract.Checked = False
        chkAllowtoAddBackDatedContract.Checked = False
        chkAllowToBackdateContractTermination.Checked = False
        chkAllowExtension.Checked = False
        chkContractValueAllowEditAfterExpiry.Checked = False

        txtPriceIncreaseLimit.Text = ""
        txtPriceDecreaseLimit.Text = ""
        chkAllowReviseTerminatedContract.Checked = False
        rbtFixedContinuous.ClearSelection()
        ddlReportGroup.SelectedIndex = 0

        txtRevisionIncreaseLimit.Text = ""
        txtRevisionDecreaseLimit.Text = ""
        ddlTaxCode.SelectedIndex = 0
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
        txtContractGroup.Enabled = False
        txtGroupDescription.Enabled = False
        ddlCategory.Enabled = False
        txtCommPct.Enabled = False
        chkContractValueAllowEdit.Enabled = False
        ddlDefServiceID.Enabled = False
        chkIncludeInPortfolio.Enabled = False

        chkShowExpiryNotification.Enabled = False
        chkAutoExpireContract.Enabled = False

        chkAllowtoAddBackDatedContract.Enabled = False
        chkAllowToBackdateContractTermination.Enabled = False
        chkAllowExtension.Enabled = False
        chkContractValueAllowEditAfterExpiry.Enabled = False
        txtPriceIncreaseLimit.Enabled = False
        txtPriceDecreaseLimit.Enabled = False
        chkAllowReviseTerminatedContract.Enabled = False
        rbtFixedContinuous.Enabled = False
        ddlReportGroup.Enabled = False

        txtRevisionIncreaseLimit.Enabled = False
        txtRevisionDecreaseLimit.Enabled = False
        ddlTaxCode.Enabled = False
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

        txtContractGroup.Enabled = True
        txtGroupDescription.Enabled = True
        ddlCategory.Enabled = True
        txtCommPct.Enabled = True
        chkContractValueAllowEdit.Enabled = True
        ddlDefServiceID.Enabled = True
        chkIncludeInPortfolio.Enabled = True

        chkShowExpiryNotification.Enabled = True
        chkAutoExpireContract.Enabled = True

        chkAllowtoAddBackDatedContract.Enabled = True
        chkAllowToBackdateContractTermination.Enabled = True
        chkAllowExtension.Enabled = True
        chkContractValueAllowEditAfterExpiry.Enabled = True

        txtPriceIncreaseLimit.Enabled = True
        txtPriceDecreaseLimit.Enabled = True
        chkAllowReviseTerminatedContract.Enabled = True
        rbtFixedContinuous.Enabled = True
        ddlReportGroup.Enabled = True

        txtRevisionIncreaseLimit.Enabled = True
        txtRevisionDecreaseLimit.Enabled = True
        ddlTaxCode.Enabled = True
        AccessControl()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.Cells(5).Text.Trim = "F" Then
                    e.Row.Cells(5).Text = "FIXED"
                ElseIf e.Row.Cells(5).Text.Trim = "C" Then
                    e.Row.Cells(5).Text = "CONTINUOUS"
                End If
            End If
                  Catch ex As Exception
            'MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("CONTRACTGROUP - " + Session("UserID"), "FUNCTION GridView1_RowDataBound", ex.Message.ToString, txtContractGroup.Text)

            Exit Sub
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
        sql = "Select * FROM tblcontractgroup "
        Sql = Sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = Sql
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(dr)
        If dt.Rows.Count > 0 Then

            If dt.Rows(0)("ContractGroup").ToString <> "" Then : txtContractGroup.Text = dt.Rows(0)("ContractGroup").ToString : End If

            If dt.Rows(0)("ContractGroup").ToString <> "" Then : txtContractGroup.Text = dt.Rows(0)("ContractGroup").ToString : End If
            If dt.Rows(0)("GroupDescription").ToString <> "" Then : txtGroupDescription.Text = dt.Rows(0)("GroupDescription").ToString : End If
            If dt.Rows(0)("Category").ToString <> "" Then : ddlCategory.Text = dt.Rows(0)("Category").ToString : End If

        
            If dt.Rows(0)("DefaultServiceID").ToString <> "" Then
                ddlDefServiceID.Text = dt.Rows(0)("DefaultServiceID").ToString
            Else
                ddlDefServiceID.SelectedIndex = 0
            End If

            If dt.Rows(0)("CommPct").ToString <> "" Then : txtCommPct.Text = dt.Rows(0)("CommPct").ToString : End If


            If dt.Rows(0)("ContractValueAllowEdit").ToString = "1" Then
                chkContractValueAllowEdit.Checked = True
            Else
                chkContractValueAllowEdit.Checked = False
            End If

            If dt.Rows(0)("IncludeInPortfolio").ToString = "1" Then
                chkIncludeInPortfolio.Checked = True
            Else
                chkIncludeInPortfolio.Checked = False
            End If

            If dt.Rows(0)("ShowExpiryNotification").ToString = "1" Then
                chkShowExpiryNotification.Checked = True
            Else
                chkShowExpiryNotification.Checked = False
            End If

            If dt.Rows(0)("AutoExpireContract").ToString = "1" Then
                chkAutoExpireContract.Checked = True
            Else
                chkAutoExpireContract.Checked = False
            End If

            If dt.Rows(0)("BackDateContract").ToString = "1" Then
                chkAllowtoAddBackDatedContract.Checked = True
            Else
                chkAllowtoAddBackDatedContract.Checked = False
            End If

            If dt.Rows(0)("BackDateContractTermination").ToString = "1" Then
                chkAllowToBackdateContractTermination.Checked = True
            Else
                chkAllowToBackdateContractTermination.Checked = False
            End If

            If dt.Rows(0)("AllowExtension").ToString = "1" Then
                chkAllowExtension.Checked = True
            Else
                chkAllowExtension.Checked = False
            End If

            If dt.Rows(0)("ContractValueAllowEditAfterExpiry").ToString = "1" Then
                chkContractValueAllowEditAfterExpiry.Checked = True
            Else
                chkContractValueAllowEditAfterExpiry.Checked = False
            End If

            If dt.Rows(0)("ReviseTerminatedContract").ToString = "1" Then
                chkAllowReviseTerminatedContract.Checked = True
            Else
                chkAllowReviseTerminatedContract.Checked = False
            End If

            If dt.Rows(0)("PriceIncreaseLimit").ToString <> "" Then : txtPriceIncreaseLimit.Text = dt.Rows(0)("PriceIncreaseLimit").ToString : End If
            If dt.Rows(0)("PriceDecreaseLimit").ToString <> "" Then : txtPriceDecreaseLimit.Text = dt.Rows(0)("PriceDecreaseLimit").ToString : End If

            If dt.Rows(0)("RevisionIncreaseLimit").ToString <> "" Then : txtRevisionIncreaseLimit.Text = dt.Rows(0)("RevisionIncreaseLimit").ToString : End If
            If dt.Rows(0)("RevisionDecreaseLimit").ToString <> "" Then : txtRevisionDecreaseLimit.Text = dt.Rows(0)("RevisionDecreaseLimit").ToString : End If

            If dt.Rows(0)("TaxType").ToString <> "" Then
                ddlTaxCode.Text = dt.Rows(0)("TaxType").ToString
            Else
                ddlTaxCode.SelectedIndex = 0
            End If
        End If


        If dt.Rows(0)("FixedContinuous").ToString <> "" Then
            If dt.Rows(0)("FixedContinuous").ToString = "F" Then
                rbtFixedContinuous.SelectedIndex = 0
            ElseIf dt.Rows(0)("FixedContinuous").ToString = "C" Then
                rbtFixedContinuous.SelectedIndex = 1
            End If
        End If


        If dt.Rows(0)("ReportGroup").ToString <> "" Then
            ddlReportGroup.Text = dt.Rows(0)("ReportGroup").ToString
        Else
            ddlReportGroup.SelectedIndex = 0
        End If

        'If dt.Rows(0)("ReportGroup").ToString <> "" Then
        '    If dt.Rows(0)("ReportGroup").ToString = "SMART" Then
        '        rdbReportGroup.SelectedIndex = 0
        '    ElseIf dt.Rows(0)("ReportGroup").ToString = "OTHERS" Then
        '        rdbReportGroup.SelectedIndex = 1
        '    End If
        'End If


        'If GridView1.SelectedRow.Cells(1).Text = "&nbsp;" Then
        '    txtContractGroup.Text = ""
        'Else

        '    txtContractGroup.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(1).Text).ToString
        'End If
        'If GridView1.SelectedRow.Cells(2).Text = "&nbsp;" Then
        '    txtGroupDescription.Text = ""
        'Else
        '    txtGroupDescription.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(2).Text).ToString
        'End If

        'If GridView1.SelectedRow.Cells(3).Text = "&nbsp;" Then
        '    ddlCategory.Text = ""
        'Else
        '    ddlCategory.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(3).Text).ToString
        'End If


        'If GridView1.SelectedRow.Cells(4).Text = "&nbsp;" Or String.IsNullOrEmpty(GridView1.SelectedRow.Cells(4).Text) = True Then
        '    ddlDefServiceID.SelectedIndex = 0
        'Else
        '    ddlDefServiceID.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(4).Text).ToString
        'End If


        'If GridView1.SelectedRow.Cells(5).Text = "&nbsp;" Then
        '    txtCommPct.Text = ""
        'Else
        '    txtCommPct.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells(5).Text).ToString
        'End If

        ''If GridView1.SelectedRow.Cells(5).Text = True Then
        'chkContractValueAllowEdit.Checked = GridView1.SelectedRow.Cells(6)

        'chkIncludeInPortfolio.Checked = GridView1.SelectedRow.Cells(7).Text
        ''Else
        ''chkContractValueAllowEdit.Checked = False
        ''End If


        'chkShowExpiryNotification.Checked = GridView1.SelectedRow.Cells(8).Text

        'chkAutoExpireContract.Checked = GridView1.SelectedRow.Cells(9).Text


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
        txtContractGroup.Focus()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")

        If Not IsPostBack Then
            MakeMeNull()
            EnableControls()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim Query3 As String
            Query3 = "Select ProductID from tblProduct order by ProductID"
            PopulateDropDownList(Query3, "ProductID", "ProductID", ddlDefServiceID)

            Query3 = ""
            Query3 = "Select Reportgroup from tblContractgroupReportgroup order by Reportgroup"
            PopulateDropDownList(Query3, "Reportgroup", "Reportgroup", ddlReportGroup)

            Query3 = ""
            Query3 = "Select TaxType from tblTaxType order by TaxType"
            PopulateDropDownList(Query3, "TaxType", "TaxType", ddlTaxCode)
        End If
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
            InsertIntoTblWebEventLog("CONTRACTGROUP - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, valueField & txtContractGroup.Text)
            Exit Sub
        End Try
    End Sub

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
            'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x0152,  x0152Add, x0152Edit, x0152Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0152"))) = False Then
                    If Convert.ToBoolean(dt.Rows(0)("x0152")) = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If



                If String.IsNullOrEmpty(dt.Rows(0)("x0152Add")) = False Then
                    Me.btnADD.Enabled = dt.Rows(0)("x0152Add").ToString()
                End If

                'Me.btnInsert.Enabled = vpSec2412Add
                If txtMode.Text = "View" Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0152Edit")) = False Then
                        Me.btnEdit.Enabled = dt.Rows(0)("x0152Edit").ToString()
                    End If

                    If String.IsNullOrEmpty(dt.Rows(0)("x0152Delete")) = False Then
                        Me.btnDelete.Enabled = dt.Rows(0)("x0152Delete").ToString()
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
            'End If

            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(txtContractGroup.Text) = True Then
            lblAlert.Text = "GROUP CANNOT BE BLANK"
            Return
        End If

        If rbtFixedContinuous.SelectedValue.ToString = "" Then
            lblAlert.Text = "DURATION TYPE CANNOT BE BLANK"
            Return
        End If

        Dim d As Double

        If String.IsNullOrEmpty(txtPriceIncreaseLimit.Text) = True Then
            lblAlert.Text = "PRICE INCREASE LIMIT CANNOT BE BLANK"
            txtPriceIncreaseLimit.Text = "0.00"
            Exit Sub
        End If


        If Double.TryParse(txtPriceIncreaseLimit.Text, d) = False Then
            lblAlert.Text = "PRICE INCREASE LIMIT IS INVALID"
            txtPriceIncreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtPriceIncreaseLimit.Text) < 0.0 Then
            lblAlert.Text = "PRICE INCREASE LIMIT SHOULD BE POSITIVE"
            txtPriceIncreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If


        'Dim d As Double

        If String.IsNullOrEmpty(txtPriceDecreaseLimit.Text) = True Then
            lblAlert.Text = "PRICE DECREASE LIMIT CANNOT BE BLANK"
            txtPriceDecreaseLimit.Text = "0.00"
            Exit Sub
        End If


        If Double.TryParse(txtPriceDecreaseLimit.Text, d) = False Then
            lblAlert.Text = "PRICE DECREASE LIMIT IS INVALID"
            txtPriceDecreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtPriceDecreaseLimit.Text) > 0.0 Then
            lblAlert.Text = "PRICE DECREASE LIMIT SHOULD BE NEGATIVE"
            txtPriceDecreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtPriceIncreaseLimit.Text) < 0 Then
            lblAlert.Text = "PRICE INCREASE CANNOT BE A NEGATIVE VALUE"
            Return
        End If

        If Convert.ToDecimal(txtPriceDecreaseLimit.Text) > 0 Then
            lblAlert.Text = "PRICE DECREASE CANNOT BE A POSITIVE VALUE"
            Return
        End If



        '28.03.23


        If String.IsNullOrEmpty(txtRevisionIncreaseLimit.Text) = True Then
            lblAlert.Text = "REVISION INCREASE LIMIT CANNOT BE BLANK"
            txtRevisionIncreaseLimit.Text = "0.00"
            Exit Sub
        End If


        If Double.TryParse(txtRevisionIncreaseLimit.Text, d) = False Then
            lblAlert.Text = "REVISION INCREASE LIMIT IS INVALID"
            txtRevisionIncreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtRevisionIncreaseLimit.Text) < 0.0 Then
            lblAlert.Text = "REVISION INCREASE LIMIT SHOULD BE POSITIVE"
            txtRevisionIncreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If


        'Dim d As Double

        If String.IsNullOrEmpty(txtRevisionDecreaseLimit.Text) = True Then
            lblAlert.Text = "REVISION DECREASE LIMIT CANNOT BE BLANK"
            txtRevisionDecreaseLimit.Text = "0.00"
            Exit Sub
        End If


        If Double.TryParse(txtRevisionDecreaseLimit.Text, d) = False Then
            lblAlert.Text = "REVISION DECREASE LIMIT IS INVALID"
            txtRevisionDecreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtRevisionDecreaseLimit.Text) > 0.0 Then
            lblAlert.Text = "REVISION DECREASE LIMIT SHOULD BE NEGATIVE"
            txtRevisionDecreaseLimit.Text = "0.00"
            'Return False
            Exit Sub
        End If

        If Convert.ToDecimal(txtRevisionIncreaseLimit.Text) < 0 Then
            lblAlert.Text = "REVISION INCREASE CANNOT BE A NEGATIVE VALUE"
            txtRevisionIncreaseLimit.Text = "0.00"
            Return
        End If

        If Convert.ToDecimal(txtRevisionDecreaseLimit.Text) > 0 Then
            lblAlert.Text = "REVISION DECREASE CANNOT BE A POSITIVE VALUE"
            txtRevisionDecreaseLimit.Text = "0.00"
            Return
        End If

        '28.03.23


        If chkIncludeInPortfolio.Checked Then
            If ddlReportGroup.SelectedIndex = 0 Then
                lblAlert.Text = "REPORT GROUP CANNOT BE BLANK"
                Return
            End If
        End If

        If txtMode.Text = "New" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblContractGroup where ContractGroup=@ContractGroup"
                command1.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                    lblAlert.Text = "RECORD ALREADY EXISTS"
                    txtContractGroup.Focus()
                    Exit Sub
                Else

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblContractGroup(ContractGroup,Category,GroupDescription, CommPct, ContractValueAllowEdit, DefaultServiceID, IncludeInPortfolio, ShowExpiryNotification, AutoExpireContract, BackDateContract,BackDateContractTermination, AllowExtension, ContractValueAllowEditAfterExpiry, PriceIncreaseLimit, PriceDecreaseLimit, ReviseTerminatedContract, FixedContinuous,ReportGroup, RevisionIncreaseLimit, RevisionDecreaseLimit, TaxType, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@ContractGroup,@Category,@Description,@CommPct, @ContractValueAllowEdit, @DefaultServiceID, @IncludeInPortfolio,  @ShowExpiryNotification, @AutoExpireContract, @BackDateContract, @BackDateContractTermination, @AllowExtension, @ContractValueAllowEditAfterExpiry, @PriceIncreaseLimit, @PriceDecreaseLimit, @ReviseTerminatedContract, @FixedContinuous,@ReportGroup, @RevisionIncreaseLimit, @RevisionDecreaseLimit, @TaxType, @CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                        command.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text.ToUpper)
                        command.Parameters.AddWithValue("@Category", ddlCategory.Text.ToUpper)
                        command.Parameters.AddWithValue("@Description", txtGroupDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@CommPct", txtCommPct.Text)
                        command.Parameters.AddWithValue("@ContractValueAllowEdit", chkContractValueAllowEdit.Checked)

                        If ddlDefServiceID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@DefaultServiceID", "")
                        Else
                            command.Parameters.AddWithValue("@DefaultServiceID", ddlDefServiceID.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@IncludeInPortfolio", chkIncludeInPortfolio.Checked)
                        command.Parameters.AddWithValue("@ShowExpiryNotification", chkShowExpiryNotification.Checked)
                        command.Parameters.AddWithValue("@AutoExpireContract", chkAutoExpireContract.Checked)

                        command.Parameters.AddWithValue("@BackDateContract", chkAllowtoAddBackDatedContract.Checked)
                        command.Parameters.AddWithValue("@BackDateContractTermination", chkAllowToBackdateContractTermination.Checked)
                        command.Parameters.AddWithValue("@AllowExtension", chkAllowExtension.Checked)
                        command.Parameters.AddWithValue("@ContractValueAllowEditAfterExpiry", chkContractValueAllowEditAfterExpiry.Checked)

                        command.Parameters.AddWithValue("@PriceIncreaseLimit", txtPriceIncreaseLimit.Text)
                        command.Parameters.AddWithValue("@PriceDecreaseLimit", txtPriceDecreaseLimit.Text)
                        command.Parameters.AddWithValue("@ReviseTerminatedContract", chkAllowReviseTerminatedContract.Checked)
                        command.Parameters.AddWithValue("@FixedContinuous", rbtFixedContinuous.SelectedValue.ToString)
                        '  command.Parameters.AddWithValue("@ReportGroup", rdbReportGroup.SelectedValue.ToString)
                        If ddlReportGroup.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@ReportGroup", "")
                        Else
                            command.Parameters.AddWithValue("@ReportGroup", ddlReportGroup.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@RevisionIncreaseLimit", txtRevisionIncreaseLimit.Text)
                        command.Parameters.AddWithValue("@RevisionDecreaseLimit", txtRevisionDecreaseLimit.Text)

                        If ddlTaxCode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@TaxType", "")
                        Else
                            command.Parameters.AddWithValue("@TaxType", ddlTaxCode.Text.ToUpper)
                        End If

                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text)
                        command.Parameters.AddWithValue("@Category", ddlCategory.Text)
                        command.Parameters.AddWithValue("@Description", txtGroupDescription.Text)
                        command.Parameters.AddWithValue("@CommPct", txtCommPct.Text)
                        command.Parameters.AddWithValue("@ContractValueAllowEdit", chkContractValueAllowEdit.Checked)

                        If ddlDefServiceID.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@DefaultServiceID", "")
                        Else
                            command.Parameters.AddWithValue("@DefaultServiceID", ddlDefServiceID.Text)
                        End If

                        command.Parameters.AddWithValue("@IncludeInPortfolio", chkIncludeInPortfolio.Checked)
                        command.Parameters.AddWithValue("@ShowExpiryNotification", chkShowExpiryNotification.Checked)
                        command.Parameters.AddWithValue("@AutoExpireContract", chkAutoExpireContract.Checked)

                        command.Parameters.AddWithValue("@BackDateContract", chkAllowtoAddBackDatedContract.Checked)
                        command.Parameters.AddWithValue("@BackDateContractTermination", chkAllowToBackdateContractTermination.Checked)
                        command.Parameters.AddWithValue("@AllowExtension", chkAllowExtension.Checked)
                        command.Parameters.AddWithValue("@ContractValueAllowEditAfterExpiry", chkContractValueAllowEditAfterExpiry.Checked)

                        command.Parameters.AddWithValue("@ReviseTerminatedContract", chkAllowReviseTerminatedContract.Checked)

                        command.Parameters.AddWithValue("@PriceIncreaseLimit", txtPriceIncreaseLimit.Text)
                        command.Parameters.AddWithValue("@PriceDecreaseLimit", txtPriceDecreaseLimit.Text)
                        command.Parameters.AddWithValue("@FixedContinuous", rbtFixedContinuous.SelectedValue.ToString)
                        If ddlReportGroup.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@ReportGroup", "")
                        Else
                            command.Parameters.AddWithValue("@ReportGroup", ddlReportGroup.Text.ToUpper)
                        End If
                        command.Parameters.AddWithValue("@RevisionIncreaseLimit", txtRevisionIncreaseLimit.Text)
                        command.Parameters.AddWithValue("@RevisionDecreaseLimit", txtRevisionDecreaseLimit.Text)

                        If ddlTaxCode.SelectedIndex = 0 Then
                            command.Parameters.AddWithValue("@TaxType", "")
                        Else
                            command.Parameters.AddWithValue("@TaxType", ddlTaxCode.Text.ToUpper)
                        End If
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

                command2.CommandText = "SELECT * FROM tblContractGroup where ContractGroup=@ContractGroup and rcno<>" & Convert.ToInt32(txtRcno.Text)
                command2.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text)
                command2.Connection = conn

                Dim dr1 As MySqlDataReader = command2.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  MessageBox.Message.Alert(Page, "Contract Group already exists!!!", "str")
                    lblAlert.Text = "CONTRACT GROUP ALREADY EXISTS"

                    txtContractGroup.Focus()
                    Exit Sub
                Else

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblContractGroup where rcno=" & Convert.ToInt32(txtRcno.Text)
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then

                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String
                        If txtExists.Text = "True" Then
                            qry = "update tblContractGroup set Category=@Category,GroupDescription=@Description, CommPct=@CommPct, ContractValueAllowEdit=@ContractValueAllowEdit, DefaultServiceID =@DefaultServiceID, IncludeInPortfolio=@IncludeInPortfolio,   ShowExpiryNotification=@ShowExpiryNotification, AutoExpireContract=@AutoExpireContract, BackDateContract= @BackDateContract, BackDateContractTermination = @BackDateContractTermination, AllowExtension=@AllowExtension, ContractValueAllowEditAfterExpiry=@ContractValueAllowEditAfterExpiry, PriceIncreaseLimit =@PriceIncreaseLimit, PriceDecreaseLimit =@PriceDecreaseLimit, ReviseTerminatedContract=@ReviseTerminatedContract, FixedContinuous=@FixedContinuous,ReportGroup=@ReportGroup, RevisionIncreaseLimit=@RevisionIncreaseLimit, RevisionDecreaseLimit=@RevisionDecreaseLimit, TaxType =@TaxType, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        Else
                            qry = "update tblContractGroup set ContractGroup=@ContractGroup,Category=@Category,GroupDescription=@Description, CommPct=@CommPct, ContractValueAllowEdit=@ContractValueAllowEdit, DefaultServiceID =@DefaultServiceID,IncludeInPortfolio=@IncludeInPortfolio,  ShowExpiryNotification=@ShowExpiryNotification, AutoExpireContract=@AutoExpireContract, BackDateContract= @BackDateContract, BackDateContractTermination = @BackDateContractTermination, AllowExtension=@AllowExtension, ContractValueAllowEditAfterExpiry=@ContractValueAllowEditAfterExpiry, PriceIncreaseLimit =@PriceIncreaseLimit, PriceDecreaseLimit =@PriceDecreaseLimit, ReviseTerminatedContract=@ReviseTerminatedContract, FixedContinuous=@FixedContinuous,ReportGroup=@ReportGroup, RevisionIncreaseLimit=@RevisionIncreaseLimit, RevisionDecreaseLimit=@RevisionDecreaseLimit, TaxType =@TaxType, LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(txtRcno.Text)
                        End If


                        command.CommandText = qry
                        command.Parameters.Clear()

                        If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then

                            command.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text.ToUpper)
                            command.Parameters.AddWithValue("@Category", ddlCategory.Text.ToUpper)
                            command.Parameters.AddWithValue("@Description", txtGroupDescription.Text.ToUpper)
                            command.Parameters.AddWithValue("@CommPct", txtCommPct.Text)
                            command.Parameters.AddWithValue("@ContractValueAllowEdit", chkContractValueAllowEdit.Checked)

                            If ddlDefServiceID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@DefaultServiceID", "")
                            Else
                                command.Parameters.AddWithValue("@DefaultServiceID", ddlDefServiceID.Text.ToUpper)
                            End If


                            command.Parameters.AddWithValue("@IncludeInPortfolio", chkIncludeInPortfolio.Checked)

                            command.Parameters.AddWithValue("@ShowExpiryNotification", chkShowExpiryNotification.Checked)
                            command.Parameters.AddWithValue("@AutoExpireContract", chkAutoExpireContract.Checked)

                            command.Parameters.AddWithValue("@BackDateContract", chkAllowtoAddBackDatedContract.Checked)
                            command.Parameters.AddWithValue("@BackDateContractTermination", chkAllowToBackdateContractTermination.Checked)
                            command.Parameters.AddWithValue("@AllowExtension", chkAllowExtension.Checked)
                            command.Parameters.AddWithValue("@ContractValueAllowEditAfterExpiry", chkContractValueAllowEditAfterExpiry.Checked)
                            command.Parameters.AddWithValue("@ReviseTerminatedContract", chkAllowReviseTerminatedContract.Checked)
                            command.Parameters.AddWithValue("@PriceIncreaseLimit", txtPriceIncreaseLimit.Text)
                            command.Parameters.AddWithValue("@PriceDecreaseLimit", txtPriceDecreaseLimit.Text)
                            command.Parameters.AddWithValue("@FixedContinuous", rbtFixedContinuous.SelectedValue.ToString)
                            If ddlReportGroup.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@ReportGroup", "")
                            Else
                                command.Parameters.AddWithValue("@ReportGroup", ddlReportGroup.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@RevisionIncreaseLimit", txtRevisionIncreaseLimit.Text)
                            command.Parameters.AddWithValue("@RevisionDecreaseLimit", txtRevisionDecreaseLimit.Text)

                            If ddlTaxCode.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@TaxType", "")
                            Else
                                command.Parameters.AddWithValue("@TaxType", ddlTaxCode.Text.ToUpper)
                            End If

                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                            command.Parameters.AddWithValue("@ContractGroup", txtContractGroup.Text)
                            command.Parameters.AddWithValue("@Category", ddlCategory.Text)
                            command.Parameters.AddWithValue("@Description", txtGroupDescription.Text)
                            command.Parameters.AddWithValue("@CommPct", txtCommPct.Text)
                            command.Parameters.AddWithValue("@ContractValueAllowEdit", chkContractValueAllowEdit.Checked)

                            If ddlDefServiceID.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@DefaultServiceID", "")
                            Else
                                command.Parameters.AddWithValue("@DefaultServiceID", ddlDefServiceID.Text)
                            End If


                            command.Parameters.AddWithValue("@IncludeInPortfolio", chkIncludeInPortfolio.Checked)

                            command.Parameters.AddWithValue("@ShowExpiryNotification", chkShowExpiryNotification.Checked)
                            command.Parameters.AddWithValue("@AutoExpireContract", chkAutoExpireContract.Checked)

                            command.Parameters.AddWithValue("@BackDateContract", chkAllowtoAddBackDatedContract.Checked)
                            command.Parameters.AddWithValue("@BackDateContractTermination", chkAllowToBackdateContractTermination.Checked)
                            command.Parameters.AddWithValue("@AllowExtension", chkAllowExtension.Checked)
                            command.Parameters.AddWithValue("@ContractValueAllowEditAfterExpiry", chkContractValueAllowEditAfterExpiry.Checked)
                            command.Parameters.AddWithValue("@ReviseTerminatedContract", chkAllowReviseTerminatedContract.Checked)
                            command.Parameters.AddWithValue("@PriceIncreaseLimit", txtPriceIncreaseLimit.Text)
                            command.Parameters.AddWithValue("@PriceDecreaseLimit", txtPriceDecreaseLimit.Text)
                            command.Parameters.AddWithValue("@FixedContinuous", rbtFixedContinuous.SelectedValue.ToString)
                            If ddlReportGroup.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@ReportGroup", "")
                            Else
                                command.Parameters.AddWithValue("@ReportGroup", ddlReportGroup.Text.ToUpper)
                            End If
                            command.Parameters.AddWithValue("@RevisionIncreaseLimit", txtRevisionIncreaseLimit.Text)
                            command.Parameters.AddWithValue("@RevisionDecreaseLimit", txtRevisionDecreaseLimit.Text)

                            If ddlTaxCode.SelectedIndex = 0 Then
                                command.Parameters.AddWithValue("@TaxType", "")
                            Else
                                command.Parameters.AddWithValue("@TaxType", ddlTaxCode.Text.ToUpper)
                            End If

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
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTGROUP", txtContractGroup.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                Else
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTGROUP", txtContractGroup.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
                End If
            Catch ex As Exception
                MessageBox.Message.Alert(Page, ex.ToString, "str")
            End Try
            EnableControls()
            txtMode.Text = ""
        End If

        GridView1.DataSourceID = "SqlDataSource1"
        'MakeMeNull()

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
            txtContractGroup.Enabled = False
            ddlCategory.Enabled = False
        Else
            txtContractGroup.Enabled = True
            ddlCategory.Enabled = True
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

                command1.CommandText = "SELECT * FROM tblContractGroup where rcno=" & Convert.ToInt32(txtRcno.Text)
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "delete from tblContractGroup where rcno=" & Convert.ToInt32(txtRcno.Text)

                    command.CommandText = qry

                    command.Connection = conn

                    command.ExecuteNonQuery()

                    '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                    lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                End If
                conn.Close()
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTGROUP", txtContractGroup.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)
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
        Response.Redirect("RV_MasterContractGroup.aspx")

    End Sub

    Private Function CheckIfExists() As Boolean
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text

        command3.CommandText = "SELECT contractgroup FROM tblcontract where contractgroup=@data"
        command3.Parameters.AddWithValue("@data", txtContractGroup.Text)
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

    Protected Sub chkIncludeInPortfolio_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeInPortfolio.CheckedChanged
        If chkIncludeInPortfolio.Checked = True Then
            chkAllowExtension.Checked = False
        End If
    End Sub

    Protected Sub chkAllowExtension_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowExtension.CheckedChanged
        If chkAllowExtension.Checked = True Then
            chkIncludeInPortfolio.Checked = False
        End If
    End Sub
End Class
