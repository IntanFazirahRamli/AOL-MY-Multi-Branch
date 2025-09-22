Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.Drawing

Partial Class ServiceMassChange
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Public rcno As String
    Public gridQuery As String

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpClientSearch.Click
        'If txtPopUpClient.Text.Trim = "" Then
        '    MessageBox.Message.Alert(Page, "Please enter client name", "str")
        'Else
        '    SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
        '    SqlDSClient.DataBind()
        '    gvClient.DataBind()
        '    mdlPopUpClient.Show()
        'End If
    End Sub

    'Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs) Handles btnPopUpClientReset.Click
    '    txtPopUpClient.Text = ""
    '    ''SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%'"
    '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 "
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub


    Protected Sub btnPopUpTeamSearch_Click(sender As Object, e As EventArgs)
        Try
            If txtPopUpTeam.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Team name", "str")
            Else
                SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
                SqlDSTeam.DataBind()
                gvTeam.DataBind()
                mdlPopUpTeam.Show()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpTeamSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)
    '    txtPopUpTeam.Text = ""
    '    'SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%'"
    '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 "
    '    SqlDSTeam.DataBind()
    '    gvTeam.DataBind()
    '    mdlPopUpTeam.Show()
    'End Sub



    Protected Sub btnPopUpTeamDetailsSearch_Click(sender As Object, e As EventArgs)
        '    If txtTeamDetailsName.Text.Trim = "" Then
        '        MessageBox.Message.Alert(Page, "Please enter Team name", "str")
        '    Else
        '        sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtTeamDetailsName.Text.Trim.ToUpper + "%'"
        '        sqlDSTeamDetails.DataBind()
        '        gvTeamDetails.DataBind()
        '        modPopUPTeamDetails.Show()
        '    End If
    End Sub

    Protected Sub btnPopUpTeamDetailsReset_Click(sender As Object, e As EventArgs)
        '    txtTeamDetailsName.Text = ""
        '    'sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamDetailsCurrentAlphabet") + "%'"
        '    sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0"
        '    sqlDSTeamDetails.DataBind()
        '    gvTeamDetails.DataBind()
        '    modPopUPTeamDetails.Show()
    End Sub



    'Protected Sub btnPopUpContractNoSearch_Click(sender As Object, e As EventArgs) Handles btnPopUpContractNoSearch.Click
    '    If txtPopUpContractNo.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter Contract No", "str")
    '    Else
    '        SqlDSContractNo.SelectCommand = "SELECT  ContractNo, CustName From tblContract where  Status='O' and RenewalST='O' and Upper(ContractNo) Like '%" + txtPopUpContractNo.Text.Trim.ToUpper + "%'"
    '        SqlDSContractNo.DataBind()
    '        gvPopUpContractNo.DataBind()
    '        mdlPopUpContractNo.Show()
    '    End If
    'End Sub

    'Protected Sub btnPopUpContractNoReset_Click(sender As Object, e As EventArgs) Handles btnPopUpContractNoReset.Click
    '    txtPopUpContractNo.Text = ""
    '    SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where  Status='O' and RenewalST='O'"
    '    SqlDSContractNo.DataBind()
    '    gvPopUpContractNo.DataBind()
    '    mdlPopUpContractNo.Show()
    'End Sub


    Protected Sub rbtnTeamDetails_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnTeamDetails.CheckedChanged
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            UpdatePanel1.Update()

            If (rbtnTeamDetails.Checked = True) Then
                txtTeamDetailsId.Enabled = True
                txtTeamDetailsIncharge.Enabled = True
                txtTeamDetailsServiceBy.Enabled = True

                ddlDOWDetailsDOW.Enabled = False
                ddlWeekNo.Enabled = False
                txtScheduleTimeIn.Enabled = False
                txtScheduleTimeOut.Enabled = False

                txtTeamDetailsId.Text = ""
                txtTeamDetailsIncharge.Text = ""
                'txtTeamDetailsName.Text = ""
                txtTeamDetailsServiceBy.Text = ""
                ddlDOWDetailsDOW.SelectedIndex = 0
                ddlWeekNo.SelectedIndex = 0
                txtScheduleTimeIn.Text = ""
                txtScheduleTimeOut.Text = ""
               
                rbtnDOWDetails.Checked = False
                rbtnScheduleTime.Checked = False
                rbtnDOW.Checked = False
                rbtnWeekNo.Checked = False

                rbtnWeekNo.Enabled = False
                rbtnDOW.Enabled = False
                rbtnScheduler.Checked = False

                imgBtnTeamDetails.Visible = True
                ImgBtnInChargeDetails.Visible = True
                ImgBtnServiceByDetails.Visible = True

                rbtnScheduleType.Checked = False
                ddlScheduleType.Enabled = False
                chkUpdateServiceContractST.Checked = False
                chkUpdateServiceContractScheduler.Checked = False
                ddlScheduleType.SelectedIndex = 0

                ddlScheduler.Enabled = False
                ddlScheduler.SelectedIndex = 0

                rbtnServiceInstruction.Checked = False
                txtServiceInstruction.Text = ""
                chkUpdateServiceContract.Enabled = True
                chkUpdateServiceContractSI.Checked = False
                chkUpdateServiceContractSI.Enabled = False
                chkUpdateServiceContractST.Enabled = False
                chkUpdateServiceContractScheduler.Enabled = False
                txtServiceInstruction.Enabled = False

                ddlSupervisor.SelectedIndex = 0
                ddlSupervisor.Enabled = True

                chkUpdateServiceContractDOWDetails.Enabled = False
                chkUpdateServiceContractScheduledTime.Enabled = False
                chkUpdateServiceContractDOWDetails.Checked = False
                chkUpdateServiceContractScheduledTime.Checked = False
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "rbtnTeamDetails_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnDOWDetails_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDOWDetails.CheckedChanged
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            UpdatePanel1.Update()
            If (rbtnDOWDetails.Checked = True) Then
                txtTeamDetailsId.Enabled = False
                txtTeamDetailsIncharge.Enabled = False
                txtTeamDetailsServiceBy.Enabled = False
                ddlDOWDetailsDOW.Enabled = False
                ddlWeekNo.Enabled = False
                txtScheduleTimeIn.Enabled = False
                txtScheduleTimeOut.Enabled = False

                txtTeamDetailsId.Text = ""
                txtTeamDetailsIncharge.Text = ""
                txtTeamDetailsServiceBy.Text = ""
                ddlDOWDetailsDOW.SelectedIndex = 0
                ddlWeekNo.SelectedIndex = 0
                txtScheduleTimeIn.Text = ""
                txtScheduleTimeOut.Text = ""

                rbtnScheduleTime.Checked = False
                rbtnTeamDetails.Checked = False
                rbtnWeekNo.Checked = False
                rbtnDOW.Checked = False

                rbtnWeekNo.Enabled = True
                rbtnDOW.Enabled = True

                'chkUpdateServiceContractDOWDetails.Enabled = True

                rbtnScheduler.Checked = False
                imgBtnTeamDetails.Visible = False


                rbtnScheduleType.Checked = False
                ddlScheduleType.Enabled = False
                chkUpdateServiceContractST.Checked = False
                chkUpdateServiceContractScheduler.Checked = False
                ddlScheduleType.SelectedIndex = 0

                ddlScheduler.Enabled = False
                ddlScheduler.SelectedIndex = 0

                rbtnServiceInstruction.Checked = False
                txtServiceInstruction.Text = ""

                imgBtnTeamDetails.Visible = False
                ImgBtnServiceByDetails.Visible = False
                ImgBtnInChargeDetails.Visible = False
                chkUpdateServiceContract.Enabled = False
                chkUpdateServiceContractSI.Checked = False
                chkUpdateServiceContractSI.Enabled = False
                chkUpdateServiceContractST.Enabled = False
                chkUpdateServiceContractScheduler.Enabled = False
                txtServiceInstruction.Enabled = False

                ddlSupervisor.SelectedIndex = 0
                ddlSupervisor.Enabled = False

                chkUpdateServiceContractDOWDetails.Enabled = False
                'chkUpdateServiceContractScheduledTime.Enabled = False
                chkUpdateServiceContractDOWDetails.Checked = False
                'chkUpdateServiceContractScheduledTime.Checked = False
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "rbtnDOWDetails_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnScheduleTime_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnScheduleTime.CheckedChanged
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            UpdatePanel1.Update()

            If rbtnScheduleTime.Checked = True Then
                txtTeamDetailsId.Enabled = False
                txtTeamDetailsIncharge.Enabled = False
                txtTeamDetailsServiceBy.Enabled = False
                ddlDOWDetailsDOW.Enabled = False
                ddlWeekNo.Enabled = False
                txtScheduleTimeIn.Enabled = True
                txtScheduleTimeOut.Enabled = True
                chkUpdateServiceContractScheduledTime.Enabled = True

                txtTeamDetailsId.Text = ""
                txtTeamDetailsIncharge.Text = ""
                'txtTeamDetailsName.Text = ""
                txtTeamDetailsServiceBy.Text = ""
                ddlDOWDetailsDOW.SelectedIndex = 0
                ddlWeekNo.SelectedIndex = 0
                txtScheduleTimeIn.Text = ""
                txtScheduleTimeOut.Text = ""

                rbtnDOWDetails.Checked = False
                rbtnTeamDetails.Checked = False
                rbtnDOW.Checked = False
                rbtnWeekNo.Checked = False

                rbtnWeekNo.Enabled = False
                rbtnDOW.Enabled = False
                imgBtnTeamDetails.Visible = False

                rbtnScheduleType.Checked = False
                rbtnScheduler.Checked = False

                ddlScheduleType.Enabled = False
                chkUpdateServiceContractST.Checked = False
                chkUpdateServiceContractScheduler.Checked = False
                ddlScheduleType.SelectedIndex = 0

                ddlScheduler.Enabled = False
                ddlScheduler.SelectedIndex = 0

                rbtnServiceInstruction.Checked = False
                txtServiceInstruction.Text = ""
                imgBtnTeamDetails.Visible = False
                ImgBtnServiceByDetails.Visible = False
                ImgBtnInChargeDetails.Visible = False
                chkUpdateServiceContract.Enabled = False
                chkUpdateServiceContractSI.Checked = False
                chkUpdateServiceContractSI.Enabled = False
                chkUpdateServiceContractST.Enabled = False
                chkUpdateServiceContractScheduler.Enabled = False
                txtServiceInstruction.Enabled = False
                ddlSupervisor.SelectedIndex = 0
                ddlSupervisor.Enabled = False
                txtScheduleTimeIn.Focus()

                chkUpdateServiceContractDOWDetails.Enabled = False
                'chkUpdateServiceContractScheduledTime.Enabled = T
                chkUpdateServiceContractDOWDetails.Checked = False
                'chkUpdateServiceContractScheduledTime.Checked = False
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "rbtnScheduleTime_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnDOW_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If rbtnDOW.Checked = True Then
                rbtnWeekNo.Checked = False
                ddlWeekNo.SelectedIndex = 0
                ddlWeekNo.Enabled = False
                ddlDOWDetailsDOW.Enabled = True
                chkUpdateServiceContractDOWDetails.Enabled = True
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "rbtnDOW_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnWeekNo_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If rbtnWeekNo.Checked = True Then
                rbtnDOW.Checked = False
                ddlDOWDetailsDOW.SelectedIndex = 0
                ddlDOWDetailsDOW.Enabled = False
                ddlWeekNo.Enabled = True
                chkUpdateServiceContractDOWDetails.Enabled = True
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "rbtnWeekNo_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    

    Protected Sub gvPopUpContractNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPopUpContractNo.SelectedIndexChanged
        If (gvPopUpContractNo.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtContractNo.Text = ""
        Else
            txtContractNo.Text = gvPopUpContractNo.SelectedRow.Cells(1).Text.Trim
        End If
    End Sub

    Protected Sub gvPopUpContractNo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPopUpContractNo.PageIndexChanging
        Try
            gvPopUpContractNo.PageIndex = e.NewPageIndex

            If txtPopUpContractNo.Text.Trim = "" Then
                SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where Status='O' and RenewalST='O'"
            Else
                SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where Status='O' and RenewalST='O' and upper(ContractNo) Like '%" + txtPopUpContractNo.Text.Trim.ToUpper + "%' or CustName Like '%" + txtPopUpContractNo.Text.Trim.ToUpper + "%' order by ContractNo"
            End If
            SqlDSContractNo.DataBind()
            gvPopUpContractNo.DataBind()
            mdlPopUpContractNo.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvPopUpContractNo_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvClient_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvClient.RowDataBound
     

    End Sub


    'Protected Sub gvTeamDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTeamDetails.SelectedIndexChanged
    '    If gvTeamDetails.SelectedRow.Cells(1).Text = "&nbsp;" Then
    '        txtTeamDetailsId.Text = " "
    '    Else
    '        txtTeamDetailsId.Text = gvTeamDetails.SelectedRow.Cells(1).Text
    '    End If

    '    If gvTeamDetails.SelectedRow.Cells(2).Text = "&nbsp;" Then
    '        txtTeamDetailsServiceBy.Text = " "
    '    Else
    '        txtTeamDetailsServiceBy.Text = gvTeamDetails.SelectedRow.Cells(2).Text
    '    End If

    '    If gvTeamDetails.SelectedRow.Cells(3).Text = "&nbsp;" Then
    '        txtTeamDetailsIncharge.Text = " "
    '    Else
    '        txtTeamDetailsIncharge.Text = gvTeamDetails.SelectedRow.Cells(3).Text
    '    End If
    'End Sub

    'Protected Sub gvTeamDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeamDetails.PageIndexChanging
    '    gvTeamDetails.PageIndex = e.NewPageIndex

    '    'If txtTeamDetailsName.Text.Trim = "" Then
    '    '    sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamDetailsCurrentAlphabet") + "%'"
    '    'Else
    '    '    sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamDetailsCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtTeamDetailsName.Text.Trim.ToUpper + "%'"
    '    'End If

    '    If txtTeamDetailsName.Text.Trim = "" Then
    '        sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 "
    '    Else
    '        sqlDSTeamDetails.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 And upper(TeamName) Like '%" + txtTeamDetailsName.Text.Trim.ToUpper + "%'"
    '    End If

    '    sqlDSTeamDetails.DataBind()
    '    gvTeamDetails.DataBind()
    '    modPopUPTeamDetails.Show()
    'End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
        '    txtClient.Text = ""
        'Else
        '    txtClient.Text = gvClient.SelectedRow.Cells(2).Text.Trim
        'End If

        'If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
        '    txtName.Text = ""
        'Else
        '    txtName.Text = gvClient.SelectedRow.Cells(3).Text.Trim
        'End If




        '''''''''''''''''
        Try
            If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
                txtClient.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            Else
                txtClient.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            End If



            If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
                txtName.Text = ""
            Else
                txtName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(4).Text.Trim)
            End If


            '''''''''''''''
            txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTeam.SelectedIndexChanged
        Try
            txtIsPopup.Text = ""
            If txtTeamSelect.Text = "Source" Then
                If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtTeam.Text = ""
                Else
                    txtTeam.Text = gvTeam.SelectedRow.Cells(1).Text
                End If

                If gvTeam.SelectedRow.Cells(3).Text = "&nbsp;" Then
                    txtServiceBy.Text = ""
                Else
                    txtServiceBy.Text = gvTeam.SelectedRow.Cells(3).Text
                End If

                If gvTeam.SelectedRow.Cells(3).Text = "&nbsp;" Then
                    txtIncharge.Text = " "
                Else
                    txtIncharge.Text = gvTeam.SelectedRow.Cells(3).Text
                End If
            Else
                If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtTeamDetailsId.Text = ""
                Else
                    txtTeamDetailsId.Text = gvTeam.SelectedRow.Cells(1).Text
                End If

                If gvTeam.SelectedRow.Cells(3).Text = "&nbsp;" Then
                    txtTeamDetailsServiceBy.Text = ""
                Else
                    txtTeamDetailsServiceBy.Text = gvTeam.SelectedRow.Cells(3).Text
                End If

                If gvTeam.SelectedRow.Cells(3).Text = "&nbsp;" Then
                    txtTeamDetailsIncharge.Text = ""
                Else
                    txtTeamDetailsIncharge.Text = gvTeam.SelectedRow.Cells(3).Text
                End If


                If gvTeam.SelectedRow.Cells(4).Text = "&nbsp;" Or gvTeam.SelectedRow.Cells(4).Text = "" Then
                    ddlSupervisor.SelectedIndex = 0
                Else
                    ddlSupervisor.Text = gvTeam.SelectedRow.Cells(4).Text
                End If

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvTeam_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Query As String = ""

            'Restrict users manual date entries
            txtFromDate.Attributes.Add("readonly", "readonly")
            txtToDate.Attributes.Add("readonly", "readonly")
            'txtClient.Attributes.Add("readonly", "readonly")
            'txtName.Attributes.Add("readonly", "readonly")
            'txtTeam.Attributes.Add("readonly", "readonly")
            'txtIncharge.Attributes.Add("readonly", "readonly")
            'txtServiceBy.Attributes.Add("readonly", "readonly")

            'txtTeamDetailsId.Attributes.Add("readonly", "readonly")
            'txtTeamDetailsIncharge.Attributes.Add("readonly", "readonly")
            'txtTeamDetailsServiceBy.Attributes.Add("readonly", "readonly")

            If Not Page.IsPostBack Then
                mdlPopUpClient.Hide()
                mdlPopUpTeam.Hide()
                MakeMeNull()
                txtTeamDetailsId.Enabled = False
                txtTeamDetailsServiceBy.Enabled = False
                txtTeamDetailsIncharge.Enabled = False
                ddlWeekNo.Enabled = False
                ddlDOWDetailsDOW.Enabled = False
                txtScheduleTimeIn.Enabled = False
                txtScheduleTimeOut.Enabled = False
                'ddlSupervisor.SelectedIndex = 0
                ddlSupervisor.Enabled = False
                lblAlert.Text = ""
                lblMessage.Text = ""

                'Dim Query As String
                Query = "Select ScheduleType from tblscheduletype "
                PopulateDropDownList(Query, "ScheduleType", "ScheduleType", ddlScheduleType)

                Query = "Select trim(StaffId) As StaffId from tblStaff where SecGroupAuthority <> 'GUEST' and Status = 'O'"
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlScheduler)
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlSchedulerSearch)

                Query = "Select distinct(Supervisor) from tblTeam where Supervisor is not null or Supervisor<> '' order by Supervisor"
                PopulateDropDownList(Query, "Supervisor", "Supervisor", ddlSupervisor)

                SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where status <> 'N' order by TeamName"
                SqlDSStaff.SelectCommand = "SELECT StaffID, Name, NRIC from tblStaff where  status <> 'N' order by Name"
            Else
                If txtIsPopup.Text = "Team" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpTeam.Show()
                ElseIf txtIsPopup.Text = "Staff" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpStaff.Show()
                ElseIf txtIsPopup.Text = "Client" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpClient.Show()
                ElseIf txtIsPopup.Text = "Contract" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpContractNo.Show()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
            Exit Sub
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
            con.Dispose()
        End Using
        'End Using
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        MakeMeNull()
        GridView1.DataSource = Nothing
        GridView1.DataBind()
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")
    End Sub

    Public Sub MakeMeNull()
        txtFromDate.Text = ""
        txtToDate.Text = ""
        txtClient.Text = ""
        txtName.Text = ""
        txtTeam.Text = ""
        txtIncharge.Text = ""
        txtServiceBy.Text = ""
        txtRcno.Text = ""
        txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        txtContractNo.Text = ""
        ddlDOW.SelectedIndex = 0
        txtSvcDescription.Text = ""

        rbtnDOWDetails.Checked = False
        rbtnScheduleTime.Checked = False
        rbtnTeamDetails.Checked = False
        rbtnDOW.Checked = False
        rbtnWeekNo.Checked = False

        rbtnScheduleType.Checked = False
        rbtnServiceInstruction.Checked = False

        chkUpdateServiceContract.Checked = False


        chkUpdateServiceContract.Enabled = False
        rbtnDOWDetails.Enabled = False
        rbtnScheduleTime.Enabled = False
        rbtnTeamDetails.Enabled = False
        rbtnDOW.Enabled = False
        rbtnWeekNo.Enabled = False

        rbtnScheduleType.Enabled = False
        rbtnServiceInstruction.Enabled = False
        rbtnScheduler.Checked = False
        rbtnScheduler.Enabled = False

        txtScheduleTimeIn.Enabled = False
        txtScheduleTimeOut.Enabled = False
        ddlWeekNo.Enabled = False
        txtTeamDetailsId.Text = ""
        txtTeamDetailsIncharge.Text = ""
        'txtTeamDetailsName.Text = ""
        txtTeamDetailsServiceBy.Text = ""

        ddlDOWDetailsDOW.SelectedIndex = 0
        ddlWeekNo.SelectedIndex = 0
        txtScheduleTimeIn.Text = ""
        txtScheduleTimeOut.Text = ""

        txtTeamDetailsId.Enabled = False
        txtTeamDetailsIncharge.Enabled = False
        txtTeamDetailsServiceBy.Enabled = False
        imgBtnTeamDetails.Visible = False
        txtTotalRecords.Text = "0"
        btnProcess.Enabled = False

        ImgBtnInChargeDetails.Visible = False
        ImgBtnServiceByDetails.Visible = False

        ddlScheduleType.Enabled = False
        chkUpdateServiceContractSI.Enabled = False
        chkUpdateServiceContractST.Enabled = False
        chkUpdateServiceContractScheduler.Enabled = False
        chkUpdateServiceContract.Enabled = False
        ddlSupervisor.SelectedIndex = 0

        ddlScheduler.Enabled = False

        chkUpdateServiceContractDOWDetails.Enabled = False
        chkUpdateServiceContractScheduledTime.Enabled = False
        chkUpdateServiceContractDOWDetails.Checked = False
        chkUpdateServiceContractScheduledTime.Checked = False
    End Sub

    Private Sub EnableControls()
        btnGo.Enabled = False
        btnGo.ForeColor = System.Drawing.Color.Gray
        btnReset.Enabled = False
        btnReset.ForeColor = System.Drawing.Color.Gray
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        txtTeamDetailsId.Enabled = False
        txtTeamDetailsIncharge.Enabled = False
        txtTeamDetailsServiceBy.Enabled = False

        ddlDOWDetailsDOW.Enabled = False
        ddlWeekNo.Enabled = False

        txtScheduleTimeIn.Enabled = False
        txtScheduleTimeOut.Enabled = False
        ddlSupervisor.Enabled = False
    End Sub

    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            UpdatePanel1.Update()

            gridQuery = ""
            Dim frmDate As String = ""
            Dim toDate As String = ""
            Dim strdate As DateTime
            Dim allSearch As Boolean = False
            Dim multiSearch As Boolean = False

            'If (txtFromDate.Text.Trim Is Nothing And
            '    txtToDate.Text.Trim Is Nothing And
            '    txtClient.Text.Trim Is Nothing And
            '    txtTeam.Text.Trim Is Nothing And
            '    txtContractNo.Text.Trim Is Nothing And
            '    ddlDOW.SelectedIndex = 0
            ') Then
            '    MessageBox.Message.Alert(Page, "Please enter any search criteria", "str")
            '    Exit Sub
            'End If


            If (String.IsNullOrEmpty(txtFromDate.Text.Trim) = True And
               String.IsNullOrEmpty(txtToDate.Text.Trim) = True And
               String.IsNullOrEmpty(txtClient.Text.Trim) = True And
               String.IsNullOrEmpty(txtTeam.Text.Trim) = True And
               String.IsNullOrEmpty(txtContractNo.Text.Trim) = True And
                String.IsNullOrEmpty(ddlSchedulerSearch.Text.Trim) = True And
               ddlDOW.SelectedIndex = 0
           ) Then
                'MessageBox.Message.Alert(Page, "Please enter any search criteria", "str")
                lblAlert.Text = "ENTER SEARCH CRITERIA"
                Exit Sub
            End If

            'btnGo.Enabled = False
            'populate grid based on search criteria
            gridQuery = "select a.recordno, a.custcode, a.custname, a.address1, a.accountid, "
            gridQuery += "a.schServiceDate, a.SchServiceTime, a.SchServiceTimeOut, DayName(a.schServiceDate) as 'DOW', a.TeamId, a.InchargeId, "
            gridQuery += "a.ServiceBy, a.ContractNo, a.vehno, a.rcNo, a.LocationID, a.ScheduleType, a.Comments, a.Supervisor,  b.Holiday,a.notes, a.Scheduler, "
            gridQuery += " a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn from tblServiceRecord a Left join tblHoliday b on a.schServiceDate = b.Holiday "
            gridQuery += "where 1=1 "

            'If (Not txtFromDate.Text.Trim Is Nothing And
            '    Not txtToDate.Text.Trim Is Nothing And
            '    Not txtClient.Text.Trim Is Nothing And
            '    Not txtTeam.Text.Trim Is Nothing And
            '    Not txtContractNo.Text.Trim Is Nothing And
            '    Not ddlDOW.SelectedIndex = 0
            ') Then
            '    allSearch = True
            '    gridQuery += " and schServiceDate >= '" & frmDate & "' and schServiceDate <='" & frmDate & "'"
            '    gridQuery += " and CustCode = '" & txtClient.Text.Trim & "'"
            '    gridQuery += " and TeamId = '" & txtTeam.Text.Trim & "'"
            '    gridQuery += " and ContractNo = '" & txtContractNo.Text.Trim & "'"
            '    'gridQuery += " and DOW = '" & ddlDOW.SelectedValue.ToString & "'"
            '    gridQuery += " and DayName(schServiceDate) = '" & ddlDOW.SelectedValue.ToString & "'"
            'End If

            If (allSearch = False) Then
                If (Not txtFromDate.Text.Trim = "" And
                   Not txtToDate.Text.Trim = "") Then
                    multiSearch = True
                    strdate = DateTime.Parse(txtFromDate.Text.Trim)
                    frmDate = strdate.ToString("yyyy-MM-dd")
                    strdate = DateTime.Parse(txtToDate.Text.Trim)
                    toDate = strdate.ToString("yyyy-MM-dd")
                    gridQuery += " and a.schServiceDate >= '" & frmDate & "' and a.schServiceDate <='" & toDate & "'"
                End If

                If (Not txtFromDate.Text.Trim = "" And txtToDate.Text.Trim = "") Then
                    multiSearch = True
                    strdate = DateTime.Parse(txtFromDate.Text.Trim)
                    frmDate = strdate.ToString("yyyy-MM-dd")
                    gridQuery += " and a.schServiceDate >= '" & frmDate & "'"
                End If

                'If (Not txtClient.Text.Trim = "") Then
                '    If (multiSearch = True) Then
                '        gridQuery += " and "
                '        gridQuery += " AccountId = '" & txtClient.Text.Trim & "'"
                '    Else
                '        gridQuery += " and AccountId = '" & txtClient.Text.Trim & "'"
                '        multiSearch = True
                '    End If
                'End If

                If (Not txtClient.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.LocationID = '" & txtClient.Text.Trim & "'"
                    Else
                        gridQuery += " and a.LocationID = '" & txtClient.Text.Trim & "'"
                        multiSearch = True
                    End If
                End If


                If (ddlSchedulerSearch.SelectedIndex > 0) Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.Scheduler = '" & ddlSchedulerSearch.Text.Trim & "'"
                    Else
                        gridQuery += " and a.Scheduler = '" & ddlSchedulerSearch.Text.Trim & "'"
                        multiSearch = True
                    End If
                End If

                If (Not txtTeam.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.TeamId = '" & txtTeam.Text.Trim & "'"
                    Else
                        gridQuery += " and a.TeamId = '" & txtTeam.Text.Trim & "'"
                        multiSearch = True
                    End If
                End If

                If (Not txtServiceBy.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.ServiceBy = '" & txtServiceBy.Text.Trim & "'"
                    Else
                        gridQuery += " and a.ServiceBy = '" & txtServiceBy.Text.Trim & "'"
                        multiSearch = True
                    End If
                End If

                If (Not txtContractNo.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.ContractNo = '" & txtContractNo.Text.Trim & "'"
                    Else
                        gridQuery += " and a.ContractNo = '" & txtContractNo.Text.Trim & "'"
                        multiSearch = True
                    End If
                End If


                If (Not txtServiceAddressSearch.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " a.Address1 like '%" & txtServiceAddressSearch.Text.Trim & "%'"
                    Else
                        gridQuery += " and a.Address1 like '%" & txtServiceAddressSearch.Text.Trim & "%'"
                        multiSearch = True
                    End If
                End If


                If (Not ddlDOW.Text.Trim = "--SELECT--") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " DayName(a.schServiceDate) = '" & ddlDOW.SelectedValue.ToString & "'"
                    Else
                        gridQuery += " and DayName(a.schServiceDate) = '" & ddlDOW.SelectedValue.ToString & "'"
                        multiSearch = True
                    End If
                End If

                '05.03.20

                If (Not ddlWeekNoSearch.Text.Trim = "--SELECT--") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "

                        'If ddlWeekNoSearch.Text = "1st Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 1"
                        'ElseIf ddlWeekNoSearch.Text = "2nd Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 2"
                        'ElseIf ddlWeekNoSearch.Text = "3rd Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 3"
                        'ElseIf ddlWeekNoSearch.Text = "4th Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 4"
                        'ElseIf ddlWeekNoSearch.Text = "5th Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 5"
                        'End If

                        If ddlWeekNoSearch.Text = "1st Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 1"
                        ElseIf ddlWeekNoSearch.Text = "2nd Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 2"
                        ElseIf ddlWeekNoSearch.Text = "3rd Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 3"
                        ElseIf ddlWeekNoSearch.Text = "4th Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1  = 4"
                        ElseIf ddlWeekNoSearch.Text = "5th Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 5"
                        End If
                    Else
                        'If ddlWeekNoSearch.Text = "1st Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 1"
                        'ElseIf ddlWeekNoSearch.Text = "2nd Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 2"
                        'ElseIf ddlWeekNoSearch.Text = "3rd Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 3"
                        'ElseIf ddlWeekNoSearch.Text = "4th Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 4"
                        'ElseIf ddlWeekNoSearch.Text = "5th Week" Then
                        '    gridQuery += " WEEK(a.servicedate,5) - WEEK(DATE_SUB(a.servicedate, INTERVAL DAYOFMONTH(a.servicedate) - 1 DAY),5) + 1 = 5"
                        '    'gridQuery += " and DayName(a.schServiceDate) = '" & ddlDOW.SelectedValue.ToString & "'"
                        '    multiSearch = True
                        'End If

                        If ddlWeekNoSearch.Text = "1st Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 1"
                        ElseIf ddlWeekNoSearch.Text = "2nd Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 2"
                        ElseIf ddlWeekNoSearch.Text = "3rd Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 3"
                        ElseIf ddlWeekNoSearch.Text = "4th Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 4"
                        ElseIf ddlWeekNoSearch.Text = "5th Week" Then
                            gridQuery += " FLOOR((DayOfMonth(servicedate)-1)/7)+1 = 5"
                            'gridQuery += " and DayName(a.schServiceDate) = '" & ddlDOW.SelectedValue.ToString & "'"
                            multiSearch = True
                        End If
                    End If
                End If

                '05.03.20

                'SASI - ADD SERVICE DESCRIPTION CRITERIA'

                If (Not txtSvcDescription.Text.Trim = "") Then
                    If (multiSearch = True) Then
                        gridQuery += " and "
                        gridQuery += " replace(replace(a.Notes, char(10), ' '), char(13), '') LIKE '%" & txtSvcDescription.Text & "%'"
                    Else
                        gridQuery += " and replace(replace(a.Notes, char(10), ' '), char(13), '') LIKE '%" & txtSvcDescription.Text & "%'"
                        multiSearch = True
                    End If
                End If

            End If
            gridQuery += " and a.Status='O'"
            gridQuery += " order by a.schServiceDate"

            'lblMessage.Text = gridQuery

            SqlDataSource1.SelectCommand = gridQuery
            SqlDataSource1.DataBind()
            GridView1.DataBind()
            'btnGo.Enabled = True

            txtTotalRecords.Text = GridView1.Rows.Count

            txtGridQuery.Text = gridQuery

            If Convert.ToInt32(txtTotalRecords.Text) > 0 Then
                rbtnDOWDetails.Checked = False
                rbtnScheduleTime.Checked = False
                rbtnTeamDetails.Checked = False
                rbtnDOW.Checked = False
                rbtnWeekNo.Checked = False
                rbtnScheduleType.Checked = False
                rbtnServiceInstruction.Checked = False
                chkUpdateServiceContract.Checked = False


                rbtnDOW.Enabled = False
                rbtnWeekNo.Enabled = False
                rbtnScheduler.Checked = False

                txtScheduleTimeIn.Enabled = False
                txtScheduleTimeOut.Enabled = False
                ddlWeekNo.Enabled = False
                txtTeamDetailsId.Text = ""
                txtTeamDetailsIncharge.Text = ""
                'txtTeamDetailsName.Text = ""
                txtTeamDetailsServiceBy.Text = ""

                ddlDOWDetailsDOW.SelectedIndex = 0
                ddlWeekNo.SelectedIndex = 0
                txtScheduleTimeIn.Text = ""
                txtScheduleTimeOut.Text = ""

                txtTeamDetailsId.Enabled = False
                txtTeamDetailsIncharge.Enabled = False
                txtTeamDetailsServiceBy.Enabled = False
                imgBtnTeamDetails.Visible = False

                rbtnDOWDetails.Enabled = True
                rbtnScheduleTime.Enabled = True
                rbtnTeamDetails.Enabled = True
                rbtnScheduleType.Enabled = True
                rbtnServiceInstruction.Enabled = True
                rbtnScheduler.Enabled = True
                btnProcess.Enabled = True
            End If
            btnGo.Enabled = True
            'chkUpdateServiceContract.Enabled = True
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnGo_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            Exit Sub
        End Try
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        'Select All Check Boxes

        lblAlert.Text = ""
        lblMessage.Text = ""
        UpdatePanel1.Update()

        For Each row As GridViewRow In GridView1.Rows
            Dim chkSelectCtrl As CheckBox = DirectCast(row.FindControl("chkGrid"), CheckBox)
            If chkSelectCtrl.Checked = False Then
                chkSelectCtrl.Checked = True
            Else
                chkSelectCtrl.Checked = False
            End If
        Next
    End Sub


    Function GetNewSvcDate_ForDOW(ByVal svcDate As String) As DateTime
        Try
            Dim strdate As DateTime = DateTime.Parse(svcDate)
            Dim svcDayOfWeek As DayOfWeek = strdate.DayOfWeek
            Dim svcWeekNumber As String = Int((strdate.Day - 1) / 7 + 1)
            'Dim svcWeekNumber As String = (strdate.Day - 1) / 7 + 1
            Dim selectedDOW As String = ddlDOWDetailsDOW.SelectedValue.ToString.ToUpper
            Dim newSvcDate As DateTime

            Select Case svcDayOfWeek.ToString.ToUpper
                Case "MONDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(1)
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(2)
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(3)
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(4)
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(5)
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(6)
                    End Select

                Case "TUESDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "TUESDAY"
                            newSvcDate = strdate
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(1)
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(2)
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(3)
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(4)
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(5)
                    End Select
                Case "WEDNESDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-2)
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "WEDNESDAY"
                            newSvcDate = strdate
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(1)
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(2)
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(3)
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(4)
                    End Select
                Case "THURSDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-3)
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(-2)
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "THURSDAY"
                            newSvcDate = strdate
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(1)
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(2)
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(3)
                    End Select
                Case "FRIDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-4)
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(-3)
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(-2)
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "FRIDAY"
                            newSvcDate = strdate
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(1)
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(2)
                    End Select
                Case "SATURDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-5)
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(-4)
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(-3)
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(-2)
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "SATURDAY"
                            newSvcDate = strdate
                        Case "SUNDAY"
                            newSvcDate = strdate.AddDays(1)
                    End Select
                Case "SUNDAY"
                    Select Case selectedDOW
                        Case "MONDAY"
                            newSvcDate = strdate.AddDays(-6)
                        Case "TUESDAY"
                            newSvcDate = strdate.AddDays(-5)
                        Case "WEDNESDAY"
                            newSvcDate = strdate.AddDays(-4)
                        Case "THURSDAY"
                            newSvcDate = strdate.AddDays(-3)
                        Case "FRIDAY"
                            newSvcDate = strdate.AddDays(-2)
                        Case "SATURDAY"
                            newSvcDate = strdate.AddDays(-1)
                        Case "SUNDAY"
                            newSvcDate = strdate
                    End Select
            End Select
            Return newSvcDate
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "GetNewSvcDate_ForDOW", ex.Message.ToString, "")
            Exit Function
        End Try
    End Function

    Function GetNewSvcDate_ForWeek(ByVal svcDate As String) As DateTime
        Dim strdate As DateTime = DateTime.Parse(svcDate)
        Dim svcDayOfWeek As DayOfWeek = strdate.DayOfWeek
        Dim svcWeekNumber As String = Int((strdate.Day - 1) / 7 + 1)
        'Dim svcWeekNumber As String = Math.Round((strdate.Day - 1) / 7 + 1)
        Dim selectedWeekNo As String = Left(ddlWeekNo.SelectedValue.ToString, 1)
        Dim newSvcDate As DateTime

        If selectedWeekNo > svcWeekNumber Then
            Dim diff As Integer = Convert.ToInt32(selectedWeekNo) - Convert.ToInt32(svcWeekNumber)

            Select Case diff
                Case 1
                    newSvcDate = strdate.AddDays(7)
                Case 2
                    newSvcDate = strdate.AddDays(14)
                Case 3
                    newSvcDate = strdate.AddDays(21)
                Case 4
                    newSvcDate = strdate.AddDays(28)
            End Select
        End If

        If selectedWeekNo < svcWeekNumber Then
            Dim diff As Integer = Convert.ToInt32(selectedWeekNo) - Convert.ToInt32(svcWeekNumber)

            Select Case diff
                Case -1
                    newSvcDate = strdate.AddDays(-7)
                Case -2
                    newSvcDate = strdate.AddDays(-14)
                Case -3
                    newSvcDate = strdate.AddDays(-21)
                Case -4
                    newSvcDate = strdate.AddDays(-28)
            End Select
        End If
        If selectedWeekNo = svcWeekNumber Then
            newSvcDate = strdate
        End If
        Return newSvcDate
    End Function

    'Pop-up

    Private Sub GenerateClientAlphabets()
        Dim alphabets As New List(Of ListItem)()
        Dim alphabet As New ListItem()
        alphabet.Value = "A"
        alphabet.Selected = alphabet.Value.Equals(ViewState("ClientCurrentAlphabet"))
        alphabets.Add(alphabet)
        For i As Integer = 66 To 90
            alphabet = New ListItem()
            alphabet.Value = [Char].ConvertFromUtf32(i)
            alphabet.Selected = alphabet.Value.Equals(ViewState("ClientCurrentAlphabet"))
            alphabets.Add(alphabet)
        Next
        'rptClientAlphabets.DataSource = alphabets
        'rptClientAlphabets.DataBind()
    End Sub

    Protected Sub ClientAlphabet_Click(sender As Object, e As EventArgs)
        'please check when user enter search criteria for one alphabet and then without clearing the textPoPUp client
        'select another alphabet ---records are not selected

        'Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
        'ViewState("ClientCurrentAlphabet") = lnkAlphabet.Text
        'Me.GenerateClientAlphabets()
        'gvClient.PageIndex = 0
        'If txtPopUpClient.Text.Trim = "" Then
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 And ContName Like '" + lnkAlphabet.Text + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        'Else
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + lnkAlphabet.Text + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        'End If

        'SqlDSClient.DataBind()
        'gvClient.DataBind()
        'mdlPopUpClient.Show()
    End Sub


    Protected Sub TeamAlphabet_Click(sender As Object, e As EventArgs)
        'please check when user enter search criteria for one alphabet and then without clearing the textPoPUp client
        'select another alphabet ---records are not selected

        Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
        ViewState("TeamCurrentAlphabet") = lnkAlphabet.Text
        Me.GenerateTeamAlphabets()
        gvTeam.PageIndex = 0

        If txtPopUpTeam.Text.Trim = "" Then
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1 = 1 and TeamName like '" + lnkAlphabet.Text + "%'"
        Else
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1 =1  and TeamName like '" + lnkAlphabet.Text + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
        End If

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub


    Private Sub GenerateTeamAlphabets()
        Dim alphabets As New List(Of ListItem)()
        Dim alphabet As New ListItem()
        alphabet.Value = "A"
        alphabet.Selected = alphabet.Value.Equals(ViewState("TeamCurrentAlphabet"))
        alphabets.Add(alphabet)
        For i As Integer = 66 To 90
            alphabet = New ListItem()
            alphabet.Value = [Char].ConvertFromUtf32(i)
            alphabet.Selected = alphabet.Value.Equals(ViewState("TeamCurrentAlphabet"))
            alphabets.Add(alphabet)
        Next
        rptrTeam.DataSource = alphabets
        rptrTeam.DataBind()
    End Sub




    'Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs) Handles btnPopUpClientReset.Click
    '    txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    '    SqlDSClient.SelectCommand = "SELECT * From tblContactMaster "
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub



    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        Try
            'gvClient.PageIndex = e.NewPageIndex

            ''If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            ''    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster "

            ''Else
            ''    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') "
            ''End If
            ''SqlDSClient.DataBind()
            ''gvClient.DataBind()
            ''mdlPopUpClient.Show()


            'If String.IsNullOrEmpty(txtPopUpClient.Text.Trim) = False Then
            '    'txtPopUpClient.Text = txtClient.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    updPanelMassChange1.Update()

            '    'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%')"
            '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%' or B.LocationId like '" + txtPopupClientSearch.Text + "%') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contactperson like '%" + txtPopupClientSearch.Text + "%' or D.LocationId like '" + txtPopupClientSearch.Text + "%') order by Accountid, LocationId "
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
            '    mdlPopUpClient.Show()
            'Else

            '    'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 "
            '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "


            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
            '    'txtIsPopup.Text = "Client"
            '    mdlPopUpClient.Show()
            'End If

            If txtPopUpClient.Text.Trim = "" Or txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.ServiceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.ServiceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') ORDER BY AccountId, LocationId"

                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C, tblPersonLocation D  ORDER BY AccountId, LocationId"
            Else
                'If txtClientModal.Text = "ID" Then
                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR A.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR A.NAME Like '%" + txtPopUpClient.Text.Trim + "%' OR B.LocationID Like '%" + txtPopUpClient.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR C.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR C.NAME Like '%" + txtPopUpClient.Text.Trim + "%' OR D.LocationID Like '%" + txtPopUpClient.Text.Trim + "%') ORDER BY AccountId, LocationId"
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.ServiceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR A.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR A.Name Like '%" + txtPopUpClient.Text.Trim + "%' OR B.LocationID Like '%" + txtPopUpClient.Text.Trim + "%' union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.ServiceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and C.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR C.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR C.Name Like '%" + txtPopUpClient.Text.Trim + "%' OR D.LocationID Like '%" + txtPopUpClient.Text.Trim + "%' ORDER BY AccountId, LocationId"

                'Else
                '    SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPpclient.Text.Trim + "%' OR A.ID Like '%" + txtPpclient.Text.Trim + "%' OR A.NAME Like '%" + txtPpclient.Text.Trim + "%' OR B.LocationID Like '%" + txtPpclient.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1 From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPpclient.Text.Trim + "%' OR C.ID Like '%" + txtPpclient.Text.Trim + "%' OR C.NAME Like '%" + txtPpclient.Text.Trim + "%' OR D.LocationID Like '%" + txtPpclient.Text.Trim + "%') ORDER BY AccountId, LocationId"

                'End If

            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            gvClient.PageIndex = e.NewPageIndex

            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvClient_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvTeam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeam.PageIndexChanging
        Try

            gvTeam.PageIndex = e.NewPageIndex

            If txtPopUpTeam.Text.Trim = "Search Here for Team or In-ChargeId" Then
                SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where Status <> 'N' order by TeamName"
            Else
                SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where 1=1 and TeamName like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'  order by TeamName"
            End If


            SqlDSTeam.DataBind()
            gvTeam.DataBind()
            mdlPopUpTeam.Show()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvTeam_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        Try
            txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
            'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster "
            'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "
            'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "
            SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.ServiceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.ServiceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') ORDER BY AccountId, LocationId"

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        Try
            If txtPopUpClient.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter client name", "str")
            Else
                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%')"
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or A.accountid like '" + txtPopUpClient.Text + "%' or A.contactperson like '%" + txtPopUpClient.Text + "%' or B.LocationId like '" + txtPopUpClient.Text + "%') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or C.accountid like '" + txtPopUpClient.Text + "%' or C.contactperson like '%" + txtPopUpClient.Text + "%' or D.LocationId like '" + txtPopUpClient.Text + "%') order by Accountid, LocationId "
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, b.LocationId, b.Address1 as ServiceAddress1, B.ContractGroup, B.ServiceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR A.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR A.Name Like '%" + txtPopUpClient.Text.Trim + "%' OR B.LocationID Like '%" + txtPopUpClient.Text.Trim + "%' union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.ServiceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and C.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR C.ID Like '%" + txtPopUpClient.Text.Trim + "%' OR C.Name Like '%" + txtPopUpClient.Text.Trim + "%' OR D.LocationID Like '%" + txtPopUpClient.Text.Trim + "%' ORDER BY AccountId, LocationId"

                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub imgbtnClient_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnClient.Click
        Try
            ' mdlPopUpTeam.Hide()
            txtPopUpClient.Text = ""
            txtPopupClientSearch.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtClient.Text.Trim) = False Then
                txtPopUpClient.Text = txtClient.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                updPanelMassChange1.Update()

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%')"
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%' or B.LocationId like '" + txtPopupClientSearch.Text + "%') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contactperson like '%" + txtPopupClientSearch.Text + "%' or D.LocationId like '" + txtPopupClientSearch.Text + "%') order by Accountid, LocationId "
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR B.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR D.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%') ORDER BY AccountId, LocationId"

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 "
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C  Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and D.Accountid <> '')   ORDER BY AccountId, LocationId"


                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If


            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "imgbtnClient_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtPopUpTeam_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpTeam.TextChanged
        Try
            If txtPopUpTeam.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Team name", "str")
            Else
                'SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
                SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and TeamName like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and status <> 'N' order by TeamName"

                SqlDSTeam.DataBind()
                gvTeam.DataBind()
                mdlPopUpTeam.Show()
                txtIsPopup.Text = "Team"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "txtPopUpTeam_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub imgBtnTeam_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnTeam.Click
        Try
            txtTeamSelect.Text = "Source"
            txtPopupTeamSearch.Text = ""
            txtPopUpTeam.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtTeam.Text.Trim) = False Then
                txtPopupTeamSearch.Text = txtTeam.Text.Trim
                txtPopUpTeam.Text = txtPopupTeamSearch.Text
                updPanelMassChange1.Update()

                SqlDSTeam.SelectCommand = "SELECT * From tblTeam where  1=1  and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
                SqlDSTeam.DataBind()
                gvTeam.DataBind()
            Else
                SqlDSTeam.SelectCommand = "SELECT * From tblTeam where 1 =1  and Status <> 'N' order by TeamName"
                SqlDSTeam.DataBind()
                gvTeam.DataBind()
            End If
            mdlPopUpTeam.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "imgBtnTeam_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub imgBtnTeamDetails_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnTeamDetails.Click
        Try
            txtTeamSelect.Text = "Destination"

            txtPopupTeamSearch.Text = ""
            txtPopUpTeam.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtTeamDetailsId.Text.Trim) = False Then
                txtPopupTeamSearch.Text = txtTeamDetailsId.Text.Trim
                txtPopUpTeam.Text = txtPopupTeamSearch.Text
                updPanelMassChange1.Update()

                SqlDSTeam.SelectCommand = "SELECT * From tblTeam where  1=1  and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
                SqlDSTeam.DataBind()
                gvTeam.DataBind()
            Else
                SqlDSTeam.SelectCommand = "SELECT * From tblTeam where 1 =1  and Status <> 'N' order by TeamName"
                SqlDSTeam.DataBind()
                gvTeam.DataBind()
            End If

            mdlPopUpTeam.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "imgBtnTeamDetails_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim PH As String = e.Row.Cells(26).Text.Trim
            If String.IsNullOrEmpty(PH) = True Or PH = "&nbsp;" Then
                'e.Row.BackColor = Color.Brown
            Else
                e.Row.ForeColor = Color.Red
            End If


        End If
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        Try
            SqlDataSource1.SelectCommand = txtGridQuery.Text
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "GridView1_Sorting", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try

            If txtProcessed.Text = "Y" Then
                txtProcessed.Text = "N"

                Exit Sub
            End If

            If ddlWeekNoSearch.SelectedIndex > 0 Then
                If ddlWeekNo.Text = ddlWeekNoSearch.Text Then
                    lblAlert.Text = "WEEK NO. CANNOT BE SAME"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If
            End If

            lblAlert.Text = ""
            lblMessage.Text = ""
            UpdatePanel1.Update()

            If rbtnTeamDetails.Checked = False And rbtnScheduleTime.Checked = False And rbtnDOWDetails.Checked = False And rbtnScheduleType.Checked = False And rbtnServiceInstruction.Checked = False And rbtnScheduler.Checked = False Then
                lblAlert.Text = "SELECT AN OPTION"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If


            Dim TotRecsSelected As Long
            TotRecsSelected = 0

            For Each row As GridViewRow In GridView1.Rows
                Dim chkbox As CheckBox = row.FindControl("chkGrid")

                If chkbox.Checked = True Then
                    TotRecsSelected = 1
                    GoTo IsRecords

                End If
            Next

            If TotRecsSelected = 0 Then
                lblAlert.Text = "NO RECORD IS SELECTED"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
IsRecords:


            If GridView1.Rows.Count = 0 Then
                lblAlert.Text = "NO RECORD TO PROCESS"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                txtProcessed.Text = "N"
                Exit Sub
            End If



            btnProcess.Enabled = False
            Dim frmDate As String = ""
            Dim toDate As String = ""
            Dim strdate As DateTime
            Dim strUpdate As String = ""
            Dim strUpdate1 As String = ""
            Dim contractNo As String = ""
            Dim svcDate As String = ""
            Dim teamDetails As Boolean = False
            Dim DOW As Boolean = False
            Dim ScheduleTIme As Boolean = False
            Dim allSearch As Boolean = False
            Dim multiSearch As Boolean = False
            Dim RecordNo As String = ""
            Dim TeamId As String = ""
            Dim InCharge As String = ""
            Dim Vehicle As String = ""
            Dim ServiceBy As String = ""
            Dim lteamID As String
            Dim lvehNo As String
            Dim lTeamName As String

            Dim lCondition As String
            lCondition = ""

            TotRecsSelected = 0
            For Each row As GridViewRow In GridView1.Rows
                Dim chkbox As CheckBox = row.FindControl("chkGrid")
                rcno = DirectCast(row.FindControl("Label1"), Label).Text

                'RecordNo = row.Cells(1).Text
                'svcDate = row.Cells(7).Text
                'TeamId = row.Cells(11).Text
                'InCharge = row.Cells(12).Text
                'Vehicle = row.Cells(14).Text
                'ServiceBy = row.Cells(15).Text
                'contractNo = row.Cells(16).Text

                RecordNo = row.Cells(1).Text
                svcDate = row.Cells(8).Text
                TeamId = row.Cells(12).Text
                InCharge = row.Cells(13).Text
                Vehicle = row.Cells(15).Text
                ServiceBy = row.Cells(16).Text
                contractNo = row.Cells(2).Text

                If chkbox.Checked = True Then
                    Dim conn As MySqlConnection = New MySqlConnection()
                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text
                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

                    If (rbtnTeamDetails.Checked = True) Then
                        teamDetails = True

                        If (txtTeamDetailsId.Text.Trim = "") Then
                            MessageBox.Message.Alert(Page, "Please enter Team Id", "str")
                            Exit Sub
                        End If



                        'Update ServiceRecordStaff

                        strUpdate = "Update tblServiceRecordStaff set StaffId=@StaffId, StaffName=@StaffName where StaffName = @OriginalStaffName and RecordNo=@RecordNo "
                        command.CommandText = strUpdate
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@StaffId", txtTeamDetailsIncharge.Text.Trim)
                        command.Parameters.AddWithValue("@StaffName", txtTeamDetailsIncharge.Text.Trim)
                        command.Parameters.AddWithValue("@OriginalStaffName", ServiceBy.Trim)
                        command.Parameters.AddWithValue("@RecordNo", RecordNo.Trim)
                        conn.Open()
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()
                        lCondition = "Service Staff: " & txtTeamDetailsServiceBy.Text.Trim & "-" & txtTeamDetailsIncharge.Text.Trim & "; "
                        ''''''''''''''''''''''''''
                        lteamID = txtTeamDetailsId.Text
                        lvehNo = ""

                        If lteamID.Trim <> "" Then
                            Dim commandVehNo As MySqlCommand = New MySqlCommand
                            commandVehNo.CommandType = CommandType.Text
                            commandVehNo.CommandText = "SELECT VehNos, TeamName FROM tblTeam where TeamID='" & lteamID & "'"
                            commandVehNo.Connection = conn
                            conn.Open()
                            commandVehNo.Connection = conn
                            'command.ExecuteNonQuery()

                            Dim drVehno As MySqlDataReader = commandVehNo.ExecuteReader()
                            Dim dtVehno As New DataTable
                            dtVehno.Load(drVehno)

                            If dtVehno.Rows.Count > 0 Then
                                lvehNo = dtVehno.Rows(0)("VehNos").ToString
                                lTeamName = dtVehno.Rows(0)("TeamName").ToString
                            End If
                            conn.Close()
                            conn.Dispose()
                            commandVehNo.Dispose()
                        End If

                        ''''''''''''''''''''''''''

                        'Update tblServiceRecord
                        strUpdate = "Update tblServiceRecord set TeamId=@TeamId, InchargeId=@InchargeId, ServiceBy=@ServiceBy, Vehno = @Vehno, Supervisor=@Supervisor, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                        command.CommandText = strUpdate
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@TeamId", txtTeamDetailsId.Text.Trim)
                        command.Parameters.AddWithValue("@InchargeId", txtTeamDetailsIncharge.Text.Trim)
                        command.Parameters.AddWithValue("@ServiceBy", txtTeamDetailsServiceBy.Text.Trim)
                        command.Parameters.AddWithValue("@VehNo", lvehNo.Trim)
                        command.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text.Trim)
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        conn.Open()
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()

                        lCondition = lCondition & "Team ID: " & txtTeamDetailsId.Text.Trim & ", " & "In Charge: " & txtTeamDetailsIncharge.Text.Trim & ", " & "Service By: " & txtTeamDetailsServiceBy.Text.Trim & ", " & "Vehicle: " & lvehNo.Trim & ", " & "Supervisor: " & ddlSupervisor.Text.Trim & "; "

                        'InsertNewLogService(RecordNo.Trim)

                        If (chkUpdateServiceContract.Checked = True) Then
                            'Update tblContract
                            strUpdate1 = "Update tblContract set TeamId=@TeamId, InchargeId=@InchargeId,  Support=@Support, Supervisor=@Supervisor, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                            command1.CommandText = strUpdate1
                            command1.Parameters.Clear()
                            command1.Parameters.AddWithValue("@TeamId", txtTeamDetailsId.Text.Trim)
                            command1.Parameters.AddWithValue("@InchargeId", txtTeamDetailsIncharge.Text.Trim)
                            command1.Parameters.AddWithValue("@Support", txtTeamDetailsServiceBy.Text.Trim)
                            command1.Parameters.AddWithValue("@Supervisor", ddlSupervisor.Text.Trim)
                            command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command1.Connection = conn
                            command1.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            'InsertNewLogContract(contractNo.Trim)

                        End If

                    End If

                    If (rbtnScheduleTime.Checked = True) Then
                        ScheduleTIme = True
                        Dim timeIn As String = ""
                        Dim timeOut As String = ""

                        'If (txtScheduleTimeIn.Text.Trim = "") Then
                        '    lblAlert.Text = "Please enter Time In"
                        '    'MessageBox.Message.Alert(Page, , "str")
                        '    Exit Sub
                        'End If

                        'If (txtScheduleTimeOut.Text.Trim = "") Then
                        '    MessageBox.Message.Alert(Page, "Please enter Time Out", "str")
                        '    Exit Sub
                        'End If

                        'If txtScheduleTimeIn.Text.Trim.ToUpper.Contains("AM") Then
                        '    timeIn = txtScheduleTimeIn.Text.ToUpper.Replace("AM", "").Trim
                        'End If
                        'If txtScheduleTimeIn.Text.Trim.ToUpper.Contains("PM") Then
                        '    timeIn = txtScheduleTimeIn.Text.ToUpper.Replace("PM", "").Trim
                        'End If

                        'If txtScheduleTimeOut.Text.Trim.ToUpper.Contains("AM") Then
                        '    timeOut = txtScheduleTimeOut.Text.ToUpper.Replace("AM", "").Trim
                        'End If
                        'If txtScheduleTimeOut.Text.Trim.ToUpper.Contains("PM") Then
                        '    timeOut = txtScheduleTimeOut.Text.ToUpper.Replace("PM", "").Trim
                        'End If

                        'Update tblServiceRecord

                        If ((txtScheduleTimeOut.Text.Trim = "") Or (txtScheduleTimeOut.Text.Trim = "__:__")) And ((txtScheduleTimeIn.Text.Trim <> "") Or (txtScheduleTimeIn.Text.Trim <> "__:__")) Then
                            strUpdate = "Update tblServiceRecord Set SchServiceTime=@TimeIn,  LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                            command.CommandText = strUpdate
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                            'command.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command.Connection = conn
                            command.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()


                            '22.02.24

                            If (chkUpdateServiceContractScheduledTime.Checked = True) Then
                                'Update tblContract
                                strUpdate1 = "Update tblContract set Timein=@Timein, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                command1.Parameters.AddWithValue("@Timein", txtScheduleTimeIn.Text.Trim)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                            End If

                            '22.02.24

                        ElseIf ((txtScheduleTimeIn.Text.Trim = "") Or (txtScheduleTimeIn.Text.Trim = "__:__")) And ((txtScheduleTimeOut.Text.Trim <> "") Or (txtScheduleTimeOut.Text.Trim <> "__:__")) Then
                            strUpdate = "Update tblServiceRecord Set  SchServiceTimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                            command.CommandText = strUpdate
                            command.Parameters.Clear()
                            'command.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                            command.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command.Connection = conn
                            command.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            '22.02.24

                            If (chkUpdateServiceContractScheduledTime.Checked = True) Then
                                'Update tblContract
                                strUpdate1 = "Update tblContract set TimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text.Trim)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                            End If

                            '22.02.24

                        ElseIf ((txtScheduleTimeIn.Text.Trim <> "") Or (txtScheduleTimeIn.Text.Trim <> "__:__")) And ((txtScheduleTimeOut.Text.Trim <> "") Or (txtScheduleTimeOut.Text.Trim <> "__:__")) Then

                            strUpdate = "Update tblServiceRecord Set SchServiceTime=@TimeIn, SchServiceTimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                            command.CommandText = strUpdate
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                            command.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command.Connection = conn
                            command.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            '22.02.24

                            If (chkUpdateServiceContractScheduledTime.Checked = True) Then
                                'Update tblContract
                                strUpdate1 = "Update tblContract set TimeIn=@TimeIn, TimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                command1.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                                command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text.Trim)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                            End If

                            '22.02.24
                        End If

                        lCondition = lCondition & "Time In: " & txtScheduleTimeIn.Text & ", " & "Time Out: " & txtScheduleTimeOut.Text & "; "


                        If (chkUpdateServiceContract.Checked = True) Then
                            'Update tblContract

                            If ((txtScheduleTimeOut.Text.Trim = "") Or (txtScheduleTimeOut.Text.Trim = "__:__")) And ((txtScheduleTimeIn.Text.Trim <> "") Or (txtScheduleTimeIn.Text.Trim <> "__:__")) Then
                                strUpdate1 = "Update tblContract Set TimeIn=@TimeIn,  LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                command1.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                                'command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                                'InsertNewLogContract(contractNo.Trim)

                            ElseIf ((txtScheduleTimeIn.Text.Trim = "") Or (txtScheduleTimeIn.Text.Trim = "__:__")) And ((txtScheduleTimeOut.Text.Trim <> "") Or (txtScheduleTimeOut.Text.Trim <> "__:__")) Then
                                strUpdate1 = "Update tblContract Set  TimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                'command1.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                                command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                                'InsertNewLogContract(contractNo.Trim)
                                'ElseIf (txtScheduleTimeOut.Text.Trim <> "") Or txtScheduleTimeIn.Text.Trim <> "" Then
                            ElseIf ((txtScheduleTimeIn.Text.Trim <> "") Or (txtScheduleTimeIn.Text.Trim <> "__:__")) And ((txtScheduleTimeOut.Text.Trim <> "") Or (txtScheduleTimeOut.Text.Trim <> "__:__")) Then

                                strUpdate1 = "Update tblContract Set TimeIn=@TimeIn, TimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                command1.CommandText = strUpdate1
                                command1.Parameters.Clear()
                                command1.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                                command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                                command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command1.Connection = conn
                                command1.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                                'InsertNewLogContract(contractNo.Trim)
                            End If

                            'strUpdate1 = "Update tblContract Set TimeIn=@TimeIn, TimeOut=@TimeOut, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                            'command1.CommandText = strUpdate1
                            'command1.Parameters.Clear()
                            'command1.Parameters.AddWithValue("@TimeIn", txtScheduleTimeIn.Text)
                            'command1.Parameters.AddWithValue("@TimeOut", txtScheduleTimeOut.Text)
                            'command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            'command1.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString("yyyy-MM-dd"))
                            'conn.Open()
                            'command1.Connection = conn
                            'command1.ExecuteNonQuery()
                            'conn.Close()
                            'conn.Dispose()
                        End If
                    End If

                    If (rbtnDOWDetails.Checked = True) Then
                        If (ddlDOWDetailsDOW.SelectedIndex = 0 And ddlWeekNo.SelectedIndex = 0) Then
                            MessageBox.Message.Alert(Page, "Please select DOW or Week", "str")
                            Exit Sub
                        End If

                        Dim newSvcDate = DateTime.Now
                        If Not svcDate = "" Then
                            If (rbtnDOW.Checked = True) Then
                                DOW = True
                                newSvcDate = GetNewSvcDate_ForDOW(svcDate)
                            End If

                            If (rbtnWeekNo.Checked = True) Then
                                newSvcDate = GetNewSvcDate_ForWeek(svcDate)
                            End If

                            If IsDate(newSvcDate) = True Then
                                strUpdate = "Update tblServiceRecord set ServiceDate=@ServiceDate, schServiceDate=@schServiceDate, LastModifiedBy=@LastModifiedBy ,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                                command.CommandText = strUpdate
                                command.Parameters.Clear()
                                command.Parameters.AddWithValue("@ServiceDate", newSvcDate.ToString("yyyy-MM-dd"))
                                command.Parameters.AddWithValue("@schServiceDate", newSvcDate.ToString("yyyy-MM-dd"))
                                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                conn.Open()
                                command.Connection = conn
                                command.ExecuteNonQuery()
                                conn.Close()
                                conn.Dispose()

                                'InsertNewLogService(RecordNo.Trim)

                                If (rbtnDOW.Checked = True) Then
                                    lCondition = lCondition & "DOW: " & ddlDOWDetailsDOW.Text & ": " & newSvcDate.ToString("yyyy-MM-dd") & "; "
                                End If

                                If (rbtnWeekNo.Checked = True) Then
                                    lCondition = lCondition & "WeekNo: " & ddlWeekNo.Text & ": " & newSvcDate.ToString("yyyy-MM-dd") & "; "
                                End If


                                '22.02.24

                                If (rbtnDOW.Checked = True) Then
                                    If chkUpdateServiceContractDOWDetails.Checked = True Then
                                        strUpdate1 = "Update tblservicecontractfrequency Set WeekDOW=@WeekDOW, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                        command1.CommandText = strUpdate1
                                        command1.Parameters.Clear()
                                        command1.Parameters.AddWithValue("@WeekDOW", ddlDOWDetailsDOW.Text)
                                        command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                        command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                        conn.Open()
                                        command1.Connection = conn
                                        command1.ExecuteNonQuery()
                                        conn.Close()
                                        conn.Dispose()
                                    End If
                                End If

                                If (rbtnWeekNo.Checked = True) Then
                                    If chkUpdateServiceContractDOWDetails.Checked = True Then
                                        strUpdate1 = "Update tblservicecontractfrequency Set WeekNo=@WeekNo, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                                        command1.CommandText = strUpdate1
                                        command1.Parameters.Clear()
                                        command1.Parameters.AddWithValue("@WeekNo", Left(ddlWeekNo.Text, 1))
                                        command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                        command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                        conn.Open()
                                        command1.Connection = conn
                                        command1.ExecuteNonQuery()
                                        conn.Close()
                                        conn.Dispose()
                                    End If
                                End If
                                '22.02.24

                            End If

                        End If
                    End If


                    '''''''''''''''''''''''
                    If (rbtnScheduleType.Checked = True) Then
                        ScheduleTIme = True
                        Dim timeIn As String = ""
                        Dim timeOut As String = ""

                        If (ddlScheduleType.SelectedIndex = 0) Then
                            MessageBox.Message.Alert(Page, "Please Select Schedule Type", "str")
                            Exit Sub
                        End If


                        'Update tblServiceRecord
                        strUpdate = "Update tblServiceRecord Set ScheduleType =@ScheduleType, LastModifiedBy=@LastModifiedBy ,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                        command.CommandText = strUpdate
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@ScheduleType", ddlScheduleType.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        conn.Open()
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()

                        lCondition = lCondition & "Schedule Type: " & ddlScheduleType.Text & "; "
                        'InsertNewLogService(RecordNo.Trim)
                        If (chkUpdateServiceContractST.Checked = True) Then
                            'Update tblContract
                            strUpdate1 = "Update tblContract Set ScheduleType=@ScheduleType, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                            command1.CommandText = strUpdate1
                            command1.Parameters.Clear()
                            command1.Parameters.AddWithValue("@ScheduleType", ddlScheduleType.Text)
                            command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command1.Connection = conn
                            command1.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            'InsertNewLogContract(contractNo.Trim)

                        End If
                    End If


                    '''''''''''''''''''''''


                    '11.08.22

                    '''''''''''''''''''''''
                    If (rbtnScheduler.Checked = True) Then
                        ScheduleTIme = True
                        Dim timeIn As String = ""
                        Dim timeOut As String = ""

                        If (ddlScheduler.SelectedIndex = 0) Then
                            MessageBox.Message.Alert(Page, "Please Select Scheduler", "str")
                            Exit Sub
                        End If


                        'Update tblServiceRecord
                        strUpdate = "Update tblServiceRecord Set scheduler =@scheduler, LastModifiedBy=@LastModifiedBy ,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                        command.CommandText = strUpdate
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@scheduler", ddlScheduler.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        conn.Open()
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()

                        lCondition = lCondition & "Scheduler: " & ddlScheduler.Text & "; "
                        'InsertNewLogService(RecordNo.Trim)
                        If (chkUpdateServiceContractScheduler.Checked = True) Then
                            'Update tblContract
                            strUpdate1 = "Update tblContract Set scheduler=@scheduler, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                            command1.CommandText = strUpdate1
                            command1.Parameters.Clear()
                            command1.Parameters.AddWithValue("@scheduler", ddlScheduler.Text)
                            command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command1.Connection = conn
                            command1.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            'InsertNewLogContract(contractNo.Trim)

                        End If
                    End If


                    '''''''''''''''''''''''


                    '11.08.22

                    '''''''''''''''''''''''
                    If (rbtnServiceInstruction.Checked = True) Then
                        ScheduleTIme = True
                        Dim timeIn As String = ""
                        Dim timeOut As String = ""

                        If (txtServiceInstruction.Text.Trim = "") Then
                            MessageBox.Message.Alert(Page, "Please Enter Service Instruction", "str")
                            Exit Sub
                        End If


                        'Update tblServiceRecord
                        strUpdate = "Update tblServiceRecord Set Comments =@Comments, LastModifiedBy=@LastModifiedBy ,LastModifiedOn=@LastModifiedOn where rcno=" & Convert.ToInt32(rcno)
                        command.CommandText = strUpdate
                        command.Parameters.Clear()
                        'command.Parameters.AddWithValue("@Comments", Left(txtServiceInstruction.Text.ToUpper, 500))
                        command.Parameters.AddWithValue("@Comments", (txtServiceInstruction.Text.ToUpper))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        conn.Open()
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        conn.Close()
                        conn.Dispose()

                        lCondition = lCondition & "Service Inst: " & Left(txtServiceInstruction.Text, 200) & "; "
                        'InsertNewLogService(RecordNo.Trim)

                        If (chkUpdateServiceContractSI.Checked = True) Then
                            'Update tblContract
                            strUpdate1 = "Update tblContract Set Comments=@Comments, LastModifiedBy=@LastModifiedBy , LastModifiedOn=@LastModifiedOn where ContractNo='" & contractNo & "'"
                            command1.CommandText = strUpdate1
                            command1.Parameters.Clear()
                            'command1.Parameters.AddWithValue("@Comments", Left(txtServiceInstruction.Text.ToUpper, 500))
                            command1.Parameters.AddWithValue("@Comments", (txtServiceInstruction.Text.ToUpper))
                            command1.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            command1.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            conn.Open()
                            command1.Connection = conn
                            command1.ExecuteNonQuery()
                            conn.Close()
                            conn.Dispose()

                            'InsertNewLogContract(contractNo.Trim)

                        End If
                    End If

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "MASSCHANGE", RecordNo, "EDIT - MASS CHANGE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")), 0, 0, 0, "", Right(lCondition, 2000), rcno)
                    TotRecsSelected = TotRecsSelected + 1
                    '''''''''''''''''''''''

                    command.Dispose()
                    command1.Dispose()

                End If


            Next

            'Dim gridQuery As String = "select recordno, custcode, custname, custaddress1, AccountId,"
            'gridQuery += "schServiceDate, TimeIn, TimeOut, DayName(schServiceDate) as 'DOW', TeamId, InchargeId, "
            'gridQuery += "ServiceBy, ContractNo, vehno, rcNo from tblServiceRecord "
            'gridQuery += "where "

            'If (Not txtContractNo.Text.Trim = "") Then
            '    gridQuery += " ContractNo = '" & txtContractNo.Text.Trim & "'"
            '    multiSearch = True
            'End If

            'If (Not txtClient.Text.Trim = "") Then
            '    If (multiSearch = True) Then
            '        gridQuery += " and "
            '        gridQuery += " AccountId = '" & txtClient.Text.Trim & "'"
            '    Else
            '        gridQuery += " AccountId = '" & txtClient.Text.Trim & "'"
            '        multiSearch = True
            '    End If
            'End If

            'If (Not txtFromDate.Text.Trim = "" And
            '   Not txtToDate.Text.Trim = "") Then
            '    If DOW = True Then
            '        If multiSearch = True Then
            '            gridQuery += " and DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        End If
            '    Else
            '        strdate = DateTime.Parse(txtFromDate.Text.Trim)
            '        frmDate = strdate.ToString("yyyy-MM-dd")
            '        strdate = DateTime.Parse(txtToDate.Text.Trim)
            '        toDate = strdate.ToString("yyyy-MM-dd")
            '        If multiSearch = True Then
            '            gridQuery += " and  schServiceDate >= '" & frmDate & "' and schServiceDate <='" & toDate & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " schServiceDate >= '" & frmDate & "' and schServiceDate <='" & toDate & "'"
            '        End If
            '    End If
            'End If

            'If (Not txtFromDate.Text.Trim = "" And txtToDate.Text.Trim = "") Then
            '    If DOW = True Then
            '        If multiSearch = True Then
            '            gridQuery += " and DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        End If
            '    Else
            '        strdate = DateTime.Parse(txtFromDate.Text.Trim)
            '        frmDate = strdate.ToString("yyyy-MM-dd")

            '        If multiSearch = True Then
            '            gridQuery += " and schServiceDate >= '" & frmDate & "'"
            '        Else
            '            gridQuery += " schServiceDate >= '" & frmDate & "'"
            '            multiSearch = True
            '        End If
            '    End If
            'End If

            'If (Not txtFromDate.Text.Trim = "" And Not txtToDate.Text.Trim = "") Then
            '    If DOW = True Then
            '        If multiSearch = True Then
            '            gridQuery += " and DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " DayName(schServiceDate) = '" & ddlDOWDetailsDOW.SelectedValue.ToString & "'"
            '        End If
            '    End If
            'End If

            'If (Not txtTeam.Text.Trim = "") Then
            '    If teamDetails = True Then
            '        If multiSearch = True Then
            '            gridQuery += " and TeamId = '" & txtTeamDetailsId.Text.Trim & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " TeamId = '" & txtTeamDetailsId.Text.Trim & "'"
            '        End If
            '    Else
            '        If multiSearch = True Then
            '            gridQuery += " and "
            '            gridQuery += " TeamId = '" & txtTeam.Text.Trim & "'"
            '        Else
            '            gridQuery += " TeamId = '" & txtTeam.Text.Trim & "'"
            '            multiSearch = True
            '        End If
            '    End If
            'Else
            '    If teamDetails = True Then
            '        If multiSearch = True Then
            '            gridQuery += " and TeamId = '" & txtTeamDetailsId.Text.Trim & "'"
            '        Else
            '            multiSearch = True
            '            gridQuery += " TeamId = '" & txtTeamDetailsId.Text.Trim & "'"
            '        End If
            '    End If
            'End If
            'gridQuery += " and Status='O'"
            'gridQuery += " order by schServiceDate asc"

            'GridView1.DataSource = Nothing
            'GridView1.DataBind()

            'SqlDataSource1.SelectCommand = gridQuery
            'SqlDataSource1.DataBind()
            'GridView1.DataBind()
            'btnProcess.Enabled = True

            btnGo_Click(sender, e)
            ''
            rbtnDOWDetails.Checked = False
            rbtnScheduleTime.Checked = False
            rbtnTeamDetails.Checked = False
            rbtnDOW.Checked = False
            rbtnWeekNo.Checked = False

            chkUpdateServiceContract.Checked = False


            rbtnDOW.Enabled = False
            rbtnWeekNo.Enabled = False

            txtScheduleTimeIn.Enabled = False
            txtScheduleTimeOut.Enabled = False
            ddlWeekNo.Enabled = False
            txtTeamDetailsId.Text = ""
            txtTeamDetailsIncharge.Text = ""
            'txtTeamDetailsName.Text = ""
            txtTeamDetailsServiceBy.Text = ""

            ddlDOWDetailsDOW.SelectedIndex = 0
            ddlWeekNo.SelectedIndex = 0
            txtScheduleTimeIn.Text = ""
            txtScheduleTimeOut.Text = ""

            txtTeamDetailsId.Enabled = False
            txtTeamDetailsIncharge.Enabled = False
            txtTeamDetailsServiceBy.Enabled = False
            imgBtnTeamDetails.Visible = False

            ''
            rbtnDOWDetails.Enabled = True
            rbtnScheduleTime.Enabled = True
            rbtnTeamDetails.Enabled = True
            btnProcess.Enabled = True
            txtProcessed.Text = "Y"
            lblMessage.Text = TotRecsSelected & " RECORDS SUCCESSFULLY UPDATED"
            'UpdatePanel1.Update()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

        Catch ex As Exception
            txtProcessed.Text = "N"
            lblAlert.Text = ex.Message
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImgBtnInCharge_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnInCharge.Click
        Try
            txtStaffSelect.Text = "SourceInCharge"
            txtPopupStaffSearch.Text = ""
            txtPopupStaff.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtIncharge.Text.Trim) = False Then
                txtPopupStaffSearch.Text = txtIncharge.Text.Trim
                txtPopupStaff.Text = txtPopupStaffSearch.Text
                updPanelMassChange1.Update()

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            End If
            mdlPopUpStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImgBtnInCharge_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImgBtnServiceBy_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnServiceBy.Click
        Try
            txtStaffSelect.Text = "SourceServiceBy"
            txtPopupStaffSearch.Text = ""
            txtPopupStaff.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
                txtPopupStaffSearch.Text = txtServiceBy.Text.Trim
                txtPopupStaff.Text = txtPopupStaffSearch.Text
                updPanelMassChange1.Update()

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            End If
            mdlPopUpStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImgBtnServiceBy_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        Try
            txtIsPopup.Text = ""
            If txtStaffSelect.Text = "SourceInCharge" Then
                If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtIncharge.Text = ""
                Else
                    txtIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
                End If

            ElseIf txtStaffSelect.Text = "SourceServiceBy" Then
                If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtServiceBy.Text = ""
                Else
                    txtServiceBy.Text = gvStaff.SelectedRow.Cells(1).Text
                End If



            ElseIf txtStaffSelect.Text = "DestinationInCharge" Then
                If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtTeamDetailsIncharge.Text = ""
                Else
                    txtTeamDetailsIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
                End If

            ElseIf txtStaffSelect.Text = "DestinationServiceBy" Then
                If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    txtTeamDetailsServiceBy.Text = ""
                Else
                    txtTeamDetailsServiceBy.Text = gvStaff.SelectedRow.Cells(1).Text
                End If

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvStaff_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

  
    Protected Sub txtPopupStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopupStaff.TextChanged
        Try
            If txtPopupStaff.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Staff name", "str")
            Else
                'SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
                SqlDSStaff.SelectCommand = "SELECT  * From tblStaff where 1=1 and Name like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' and status <> 'N' order by Name"

                SqlDSStaff.DataBind()
                gvStaff.DataBind()
                mdlPopUpStaff.Show()
                txtIsPopup.Text = "Staff"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "txtPopupStaff_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpTeamReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpTeamReset.Click
        Try
            txtPopUpTeam.Text = "Search Here for Team or In-ChargeId"
            SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where 1=1 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and status <> 'N' order by TeamName"

            'SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where status <> 'N' order by TeamName"
            'SqlDSStaff.SelectCommand = "SELECT StaffID, Name, NRIC from tblStaff where  status <> 'N' order by Name"


            SqlDSTeam.DataBind()
            gvTeam.DataBind()
            mdlPopUpTeam.Show()
            txtIsPopup.Text = "Team"
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpTeamReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        Try
            txtPopupStaff.Text = "Search Here for Staff"
            SqlDSStaff.SelectCommand = "SELECT  StaffID, Name, NRIC From tblStaff where 1=1 and Name like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N' order by Name"
            SqlDSStaff.DataBind()
            gvStaff.DataBind()
            mdlPopUpStaff.Show()
            txtIsPopup.Text = "Staff"
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpStaffReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
        Try
            If txtPopupStaff.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Name", "str")
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where upper(Name) Like '%" + txtPopupStaff.Text.Trim.ToUpper + "%'"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
                mdlPopUpStaff.Show()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "btnPopUpStaffSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub StaffAlphabet_Click(sender As Object, e As EventArgs)
        'please check when user enter search criteria for one alphabet and then without clearing the textPoPUp client
        'select another alphabet ---records are not selected

        Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
        ViewState("StaffCurrentAlphabet") = lnkAlphabet.Text
        Me.GenerateStaffAlphabets()
        gvStaff.PageIndex = 0

        If txtPopupStaff.Text.Trim = "" Then
            SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 = 1 and Name like '" + lnkAlphabet.Text + "%'"
        Else
            SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Name like '" + lnkAlphabet.Text + "%'"
        End If

        SqlDSStaff.DataBind()
        gvStaff.DataBind()
        mdlPopUpStaff.Show()
    End Sub


    Private Sub GenerateStaffAlphabets()
        Dim alphabets As New List(Of ListItem)()
        Dim alphabet As New ListItem()
        alphabet.Value = "A"
        alphabet.Selected = alphabet.Value.Equals(ViewState("StaffCurrentAlphabet"))
        alphabets.Add(alphabet)
        For i As Integer = 66 To 90
            alphabet = New ListItem()
            alphabet.Value = [Char].ConvertFromUtf32(i)
            alphabet.Selected = alphabet.Value.Equals(ViewState("StaffCurrentAlphabet"))
            alphabets.Add(alphabet)
        Next
        rptrStaff.DataSource = alphabets
        rptrStaff.DataBind()
    End Sub

    Protected Sub ImgBtnInChargeDetails_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnInChargeDetails.Click
        Try
            txtStaffSelect.Text = "DestinationInCharge"
            txtPopupStaffSearch.Text = ""
            txtPopupStaff.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtTeamDetailsIncharge.Text.Trim) = False Then
                txtPopupStaffSearch.Text = txtTeamDetailsIncharge.Text.Trim
                txtPopupStaff.Text = txtPopupStaffSearch.Text
                updPanelMassChange1.Update()

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            End If
            mdlPopUpStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImgBtnInChargeDetails_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImgBtnServiceByDetails_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnServiceByDetails.Click
        Try
            txtStaffSelect.Text = "DestinationServiceBy"
            txtPopupStaffSearch.Text = ""
            txtPopupStaff.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtTeamDetailsServiceBy.Text.Trim) = False Then
                txtPopupStaffSearch.Text = txtTeamDetailsServiceBy.Text.Trim
                txtPopupStaff.Text = txtPopupStaffSearch.Text
                updPanelMassChange1.Update()

                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where  1=1  and (upper(Name) Like '%" + txtPopupStaffSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            Else
                SqlDSStaff.SelectCommand = "SELECT * From tblStaff where 1 =1  and Status <> 'N' order by Name"
                SqlDSStaff.DataBind()
                gvStaff.DataBind()
            End If
            mdlPopUpStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImgBtnServiceByDetails_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnScheduleType_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnScheduleType.CheckedChanged
        lblAlert.Text = ""
        lblMessage.Text = ""
        UpdatePanel1.Update()

        If (rbtnScheduleType.Checked = True) Then
            ddlScheduleType.Enabled = True
            chkUpdateServiceContractST.Enabled = True

            chkUpdateServiceContractScheduler.Enabled = False
            chkUpdateServiceContractScheduler.Checked = False

            txtTeamDetailsId.Enabled = False
            txtTeamDetailsIncharge.Enabled = False
            txtTeamDetailsServiceBy.Enabled = False

            ddlDOWDetailsDOW.Enabled = False
            ddlWeekNo.Enabled = False
            txtScheduleTimeIn.Enabled = False
            txtScheduleTimeOut.Enabled = False

            ddlScheduler.Enabled = False
            ddlScheduler.SelectedIndex = 0

            txtTeamDetailsId.Text = ""
            txtTeamDetailsIncharge.Text = ""
            'txtTeamDetailsName.Text = ""
            txtTeamDetailsServiceBy.Text = ""
            ddlDOWDetailsDOW.SelectedIndex = 0
            ddlWeekNo.SelectedIndex = 0
            txtScheduleTimeIn.Text = ""
            txtScheduleTimeOut.Text = ""

            rbtnTeamDetails.Checked = False
            rbtnDOWDetails.Checked = False
            rbtnScheduleTime.Checked = False
            rbtnDOW.Checked = False
            rbtnWeekNo.Checked = False

            rbtnWeekNo.Enabled = False
            rbtnDOW.Enabled = False
            rbtnScheduler.Checked = False
            rbtnServiceInstruction.Checked = False
            txtServiceInstruction.Text = ""
            imgBtnTeamDetails.Visible = False
            ImgBtnServiceByDetails.Visible = False
            ImgBtnInChargeDetails.Visible = False
            chkUpdateServiceContract.Enabled = False
            chkUpdateServiceContractSI.Checked = False
            chkUpdateServiceContractSI.Enabled = False
            txtServiceInstruction.Enabled = False

            ddlSupervisor.SelectedIndex = 0
            ddlSupervisor.Enabled = False

            chkUpdateServiceContractDOWDetails.Enabled = False
            chkUpdateServiceContractScheduledTime.Enabled = False

            chkUpdateServiceContractDOWDetails.Checked = False
            chkUpdateServiceContractScheduledTime.Checked = False
        End If
    End Sub

    Protected Sub rbtnServiceInstruction_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnServiceInstruction.CheckedChanged
        lblAlert.Text = ""
        lblMessage.Text = ""
        UpdatePanel1.Update()

        If (rbtnServiceInstruction.Checked = True) Then
            txtServiceInstruction.Enabled = True
            chkUpdateServiceContractSI.Enabled = True

            txtTeamDetailsId.Enabled = False
            txtTeamDetailsIncharge.Enabled = False
            txtTeamDetailsServiceBy.Enabled = False
            ddlDOWDetailsDOW.Enabled = False
            ddlWeekNo.Enabled = False
            txtScheduleTimeIn.Enabled = False
            txtScheduleTimeOut.Enabled = False

            txtTeamDetailsId.Text = ""
            txtTeamDetailsIncharge.Text = ""
            'txtTeamDetailsName.Text = ""
            txtTeamDetailsServiceBy.Text = ""
            ddlDOWDetailsDOW.SelectedIndex = 0
            ddlWeekNo.SelectedIndex = 0
            txtScheduleTimeIn.Text = ""
            txtScheduleTimeOut.Text = ""

            rbtnTeamDetails.Checked = False
            rbtnDOWDetails.Checked = False
            rbtnScheduleTime.Checked = False
            rbtnDOW.Checked = False
            rbtnWeekNo.Checked = False

            rbtnWeekNo.Enabled = False
            rbtnDOW.Enabled = False
            rbtnScheduler.Checked = False
            rbtnScheduleType.Checked = False
            ddlScheduleType.Enabled = False
            imgBtnTeamDetails.Visible = False
            ImgBtnServiceByDetails.Visible = False
            ImgBtnInChargeDetails.Visible = False
            chkUpdateServiceContract.Enabled = False
            chkUpdateServiceContractST.Checked = False
            chkUpdateServiceContractScheduler.Checked = False
            ddlScheduleType.SelectedIndex = 0
            ddlSupervisor.SelectedIndex = 0
            ddlSupervisor.Enabled = False
            chkUpdateServiceContractST.Enabled = False
            chkUpdateServiceContractScheduler.Enabled = False
            ddlScheduler.Enabled = False
            ddlScheduler.SelectedIndex = 0

            txtServiceInstruction.Focus()

            chkUpdateServiceContractDOWDetails.Enabled = False
            chkUpdateServiceContractScheduledTime.Enabled = False

            chkUpdateServiceContractDOWDetails.Checked = False
            chkUpdateServiceContractScheduledTime.Checked = False
        End If
    End Sub

    Protected Sub txtPopUpContractNo_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpContractNo.TextChanged
        Try
            If txtPopUpContractNo.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter ContractNo/CustName", "str")
            Else
                SqlDSContractNo.SelectCommand = "SELECT  ContractNo, CustName from tblContract where (ContractNo like '%" & txtPopUpContractNo.Text & "%' or CustName like '%" & txtPopUpContractNo.Text & "%') and  Status='O' and RenewalST='O' order by ContractNo"

                SqlDSContractNo.DataBind()
                gvPopUpContractNo.DataBind()
                mdlPopUpContractNo.Show()
                txtIsPopup.Text = "Contract"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "txtPopUpContractNo_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Try
            txtPopUpContractNo.Text = ""
            SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where  Status='O' and RenewalST='O'"
            SqlDSContractNo.DataBind()
            gvPopUpContractNo.DataBind()
            mdlPopUpContractNo.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImageButton1_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub imgBtnContractNo_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnContractNo.Click
        Try
            If String.IsNullOrEmpty(txtContractNo.Text.Trim) = True Then
                SqlDSContractNo.SelectCommand = "SELECT  ContractNo, CustName from tblContract where  Status='O' and RenewalST='O' order by ContractNo"
            Else
                SqlDSContractNo.SelectCommand = "SELECT  ContractNo, CustName from tblContract where  Status='O' and RenewalST='O' and ContractNo like '%" & txtContractNo.Text & "%' order by ContractNo"

            End If
            updPanelMassChange1.Update()
            SqlDSContractNo.DataBind()
            gvPopUpContractNo.DataBind()
            mdlPopUpContractNo.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "imgBtnContractNo_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        Try
            ' mdlPopUpTeam.Hide()
            txtPopUpClient.Text = ""
            txtPopupClientSearch.Text = ""
            updPanelMassChange1.Update()

            If String.IsNullOrEmpty(txtClient.Text.Trim) = False Then
                txtPopUpClient.Text = txtClient.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                updPanelMassChange1.Update()

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%')"
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%' or B.LocationId like '" + txtPopupClientSearch.Text + "%') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contactperson like '%" + txtPopupClientSearch.Text + "%' or D.LocationId like '" + txtPopupClientSearch.Text + "%') order by Accountid, LocationId "
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR B.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR D.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%') ORDER BY AccountId, LocationId"

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 "
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C  Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and D.Accountid <> '')   ORDER BY AccountId, LocationId"


                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If



            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "ImageButton2_Click", ex.Message.ToString, "")
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
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        Try

            gvStaff.PageIndex = e.NewPageIndex

            If txtPopupStaff.Text.Trim = "Search Here for Staff" Then
                'SqlDSStaff.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where Status <> 'N' order by TeamName"
                SqlDSStaff.SelectCommand = "SELECT  StaffID, Name, NRIC From tblStaff where status <> 'N' order by Name"

            Else
                'SqlDSTeam.SelectCommand = "SELECT distinct TeamId, TeamName, Inchargeid, Supervisor From tblTeam where 1=1 and TeamName like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'  order by TeamName"
                SqlDSStaff.SelectCommand = "SELECT  StaffID, Name, NRIC From tblStaff where  Name like '%" + txtPopupStaff.Text.Trim.ToUpper + "%' and status <> 'N' order by Name"

            End If


            SqlDSStaff.DataBind()
            gvStaff.DataBind()
            mdlPopUpStaff.Show()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "gvStaff_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ddlWeekNo0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWeekNoSearch.SelectedIndexChanged

    End Sub

    'Private Sub InsertNewLogContract(lContractNo As String)

    '    Try

    '        ' Start: Insert NEW Log table

    '        ''
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforContract FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforContract")) = False Then
    '            If dt.Rows(0)("EnableLogforContract").ToString = "1" Then

    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLog_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Contract")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", lContractNo.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()

    '                ''''

    '            End If
    '            'End If

    '        End If
    '        conn.Close()
    '        conn.Dispose()
    '        command.Dispose()
    '        dt.Dispose()
    '        dr.Close()
    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "InsertNewLogContract", ex.Message.ToString, lContractNo.Trim)

    '    End Try
    'End Sub


    'Private Sub InsertNewLogService(lRecordNo As String)

    '    Try

    '        ' Start: Insert NEW Log table

    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforService FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforContract")) = False Then
    '            If dt.Rows(0)("EnableLogforService").ToString = "1" Then


    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLog_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Service")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", lRecordNo.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()
    '            End If
    '        End If

    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("MASS SERVICE CHANGE - " + Session("UserID"), "InsertNewLogService", ex.Message.ToString, lRecordNo.Trim)

    '    End Try
    'End Sub

    Protected Sub rbtnScheduler_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnScheduler.CheckedChanged
        lblAlert.Text = ""
        lblMessage.Text = ""
        UpdatePanel1.Update()

        If (rbtnScheduler.Checked = True) Then

            ddlScheduler.Enabled = True
            'chkUpdateServiceContractST.Enabled = True

            ddlScheduleType.Enabled = False
            chkUpdateServiceContractST.Enabled = False
            chkUpdateServiceContractScheduler.Enabled = True
            txtTeamDetailsId.Enabled = False
            txtTeamDetailsIncharge.Enabled = False
            txtTeamDetailsServiceBy.Enabled = False

            ddlDOWDetailsDOW.Enabled = False
            ddlWeekNo.Enabled = False
            txtScheduleTimeIn.Enabled = False
            txtScheduleTimeOut.Enabled = False


            txtTeamDetailsId.Text = ""
            txtTeamDetailsIncharge.Text = ""
            'txtTeamDetailsName.Text = ""
            txtTeamDetailsServiceBy.Text = ""
            ddlDOWDetailsDOW.SelectedIndex = 0
            ddlWeekNo.SelectedIndex = 0
            txtScheduleTimeIn.Text = ""
            txtScheduleTimeOut.Text = ""

            rbtnTeamDetails.Checked = False
            rbtnDOWDetails.Checked = False
            rbtnScheduleTime.Checked = False
            rbtnDOW.Checked = False
            rbtnWeekNo.Checked = False
            rbtnScheduleType.Checked = False
            rbtnWeekNo.Enabled = False
            rbtnDOW.Enabled = False

            rbtnServiceInstruction.Checked = False
            txtServiceInstruction.Text = ""
            imgBtnTeamDetails.Visible = False
            ImgBtnServiceByDetails.Visible = False
            ImgBtnInChargeDetails.Visible = False
            chkUpdateServiceContract.Enabled = False
            chkUpdateServiceContractSI.Checked = False
            chkUpdateServiceContractSI.Enabled = False
            txtServiceInstruction.Enabled = False

            ddlSupervisor.SelectedIndex = 0
            ddlSupervisor.Enabled = False

            chkUpdateServiceContractDOWDetails.Enabled = False
            chkUpdateServiceContractScheduledTime.Enabled = False
            chkUpdateServiceContractDOWDetails.Checked = False
            chkUpdateServiceContractScheduledTime.Checked = False
        End If
    End Sub

    Protected Sub chkUpdateServiceContractScheduler_CheckedChanged(sender As Object, e As EventArgs) Handles chkUpdateServiceContractScheduler.CheckedChanged

    End Sub
End Class
