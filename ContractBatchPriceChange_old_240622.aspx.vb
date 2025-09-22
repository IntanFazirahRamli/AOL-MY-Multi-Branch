Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.Drawing

Partial Class ContractBatchPriceChange_old_240622
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Public rcno As String
    Public gridQuery As String
    Shared random As New Random()

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




    Protected Sub rbtnDOW_CheckedChanged(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "rbtnDOW_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub rbtnWeekNo_CheckedChanged(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "rbtnWeekNo_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged

        '''''''''''''''''
        Try
            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtClient.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            Else
                txtClient.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If



            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtName.Text = ""
            Else
                txtName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(2).Text.Trim)
            End If


            '''''''''''''''
            txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Query As String = ""

            'Restrict users manual date entries
            'txtPercChange.Attributes.Add("readonly", "readonly")
            'txtEffectiveDate.Attributes.Add("readonly", "readonly")
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

                lblAlert.Text = ""
                lblMessage.Text = ""

                Query = "Select ContractGroup from tblcontractgroup"
                PopulateDropDownList(Query, "ContractGroup", "ContractGroup", ddlContractGrp)

                chkUpdateServiceRecords.Checked = True

                '''
                Dim conn As MySqlConnection = New MySqlConnection()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                'commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise, BackDateContract, BackDateContractTermination, ContractRevisionTerminationCode, PrefixDocNoContract, AutoRenewal FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.CommandText = "SELECT showSConScreenLoad, ServiceContractMaxRec,DisplayRecordsLocationWise, ContractRevisionTerminationCode, PrefixDocNoContract, AutoRenewal, ContinuousContract, TeamIDMandatory, ServiceByMandatory, DefaultTaxCode, AllowTerminationBeforeLastService, PriceIncreaseLimit, PriceDecreaseLimit FROM tblservicerecordmastersetup"

                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)


                'txtPriceIncreaseLimit.Text = dtServiceRecordMasterSetup.Rows(0)("PriceIncreaseLimit")
                'txtPriceDecreaseLimit.Text = dtServiceRecordMasterSetup.Rows(0)("PriceDecreaseLimit")

                conn.Close()
                conn.Dispose()
                commandServiceRecordMasterSetup.Dispose()
                drServiceRecordMasterSetup.Close()
                dtServiceRecordMasterSetup.Dispose()

                '''
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
                    'mdlPopUpContractNo.Show()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
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
        txtPercChange.Text = ""
        txtEffectiveDate.Text = ""
        txtClient.Text = ""
        txtName.Text = ""

        lblIncreaseDecrease.Text = ""
        txtConfirmationCode.Text = ""

        txtRcno.Text = ""
        txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""

        txtTotalRecords.Text = "0"
        btnProcess.Enabled = False
        GridView1.Enabled = False
    End Sub

    Private Sub EnableControls()
        btnGo.Enabled = False
        btnGo.ForeColor = System.Drawing.Color.Gray
        btnReset.Enabled = False
        btnReset.ForeColor = System.Drawing.Color.Gray
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black


    End Sub


    Private Sub FindPriceIncreaseDecreaseLimit()
        Try
            Dim sqlstr As String
            sqlstr = ""

            sqlstr = "SELECT PriceIncreaseLimit, PriceDecreaseLimit FROM tblcontractgroup where contractgroup = '" & ddlContractGrp.Text & "'"

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                txtPriceIncreaseLimit.Text = dt.Rows(0)("PriceIncreaseLimit").ToString()
                txtPriceDecreaseLimit.Text = dt.Rows(0)("PriceDecreaseLimit").ToString()
            End If

            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
            'updPanelContract1.Update()

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "FUNCTION FindPriceIncreaseDecreaseLimit", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""

            UpdatePanel1.Update()

            'If GridView1.Rows.Count = 0 Then
            '    lblAlert.Text = "No Record to Process"
            '    Exit Sub
            'End If

            txtCreatedOn.Text = ""
            txtConfirmationCode.Text = ""

            gridQuery = ""
            Dim frmDate As String = ""
            Dim toDate As String = ""
            'Dim strdate As DateTime
            Dim allSearch As Boolean = False
            Dim multiSearch As Boolean = False

            If String.IsNullOrEmpty(txtPercChange.Text.Trim) = True Then
                lblAlert.Text = "Please Enter % Change"
                txtPercChange.Focus()
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
                lblAlert.Text = "Please Enter Effective Date"
                txtEffectiveDate.Focus()
                Exit Sub
            End If


            If ddlContractGrp.SelectedIndex = 0 Then
                lblAlert.Text = "Please Select Contract Group"
                ddlContractGrp.Focus()
                Exit Sub
            End If


            'If String.IsNullOrEmpty(txtClient.Text.Trim) = True And String.IsNullOrEmpty(txtName.Text.Trim) = True Then
            '    lblAlert.Text = "Please Enter AccountID or Name"
            '    Exit Sub
            'End If

            'If String.IsNullOrEmpty(txtName.Text.Trim) = True Then
            '    lblAlert.Text = "Please Enter Name"
            '    Exit Sub
            'End If

            Dim sql As String

            sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate from tblContract "
            sql = sql + " where 1=1 and ((Status ='O') or (Status ='H') or (Status ='C')) and ExcludeBatchPriceChange = False "
            'sql = sql + " and ((OContractNo is null) or (OContractNo ='')) "


            If String.IsNullOrEmpty(txtEffectiveDate.Text) = False Then
                sql = sql + " and ContractDate <= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If Len(txtContractNo.Text) > 3 Then
                If String.IsNullOrEmpty(txtContractNo.Text.Trim) = False Then
                    sql = sql + " and ContractNo like '%" + txtContractNo.Text.Trim + "%'"
                End If
            End If

            If Len(txtClient.Text) > 3 Then
                If String.IsNullOrEmpty(txtClient.Text.Trim) = False Then
                    sql = sql + " and AccountID like '%" + txtClient.Text.Trim + "%'"
                End If
            End If

            If Len(txtName.Text) > 3 Then
                If String.IsNullOrEmpty(txtName.Text.Trim) = False Then
                    sql = sql + " and CustName like '%" + txtName.Text.Trim + "%'"
                End If
            End If

            If ddlContractGrp.SelectedIndex > 0 Then
                sql = sql + " and ContractGroup = '" + ddlContractGrp.Text.Trim + "'"
            End If


            ''start: union

            'sql = sql + " UNION "
            'sql = sql + " Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate from tblContract "
            'sql = sql + " where 1=1 and ((Status ='O') or (Status ='H') or (Status ='C')) "
            ''sql = sql + " and ((OContractNo is not null) or (OContractNo <> ''))  "

            ''If String.IsNullOrEmpty(txtEffectiveDate.Text) = False Then
            ''    sql = sql + " and ContractDate <= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"
            ''End If

            'If Len(txtContractNo.Text) > 3 Then
            '    If String.IsNullOrEmpty(txtContractNo.Text.Trim) = False Then
            '        sql = sql + " and OContractNo like '%" + txtContractNo.Text.Trim + "%'"
            '    End If
            'End If

            'If Len(txtClient.Text) > 3 Then
            '    If String.IsNullOrEmpty(txtClient.Text.Trim) = False Then
            '        sql = sql + " and AccountID like '%" + txtClient.Text.Trim + "%'"
            '    End If
            'End If

            'If Len(txtName.Text) > 3 Then
            '    If String.IsNullOrEmpty(txtName.Text.Trim) = False Then
            '        sql = sql + " and CustName like '%" + txtName.Text.Trim + "%'"
            '    End If
            'End If

            'If ddlContractGrp.SelectedIndex > 0 Then
            '    sql = sql + " and ContractGroup = '" + ddlContractGrp.Text.Trim + "'"
            'End If


            ''end: union

            sql = sql + " Order by ContractDate, ContractNo"

            txt.Text = sql

            SqlDataSource1.SelectCommand = sql
            SqlDataSource1.DataBind()

            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            CalculateNewAgreeValue()

            txtTotalRecords.Text = GridView1.Rows.Count

            btnProcess.Enabled = True
            GridView1.Enabled = True
            'chkUpdateServiceContract.Enabled = True
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "btnGo_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub CalculateNewAgreeValue()
        For rowIndex As Integer = 0 To GridView1.Rows.Count - 1

            Dim lblid1 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
            Dim lblid2 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
            Dim lblid3 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
            Dim lblid4 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtTotalServiceValueGV"), TextBox)

            lblid2.Text = (Convert.ToDecimal(lblid1.Text) + (Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(txtPercChange.Text) * 0.01)).ToString("N2")


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim commandService As MySqlCommand = New MySqlCommand

            commandService.CommandType = CommandType.Text
            commandService.CommandText = "SELECT ifnull(sum(Billamount),0) as totservicevalue FROM tblServiceRecord where contractno ='" & lblid3.Text & "'"
            commandService.Connection = conn

            Dim drService As MySqlDataReader = commandService.ExecuteReader()
            Dim dtService As New DataTable
            dtService.Load(drService)


            lblid4.Text = dtService.Rows(0)("totservicevalue").ToString
            conn.Close()
            dtService.Dispose()
            commandService.Dispose()



        Next rowIndex
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
            'Dim selectedDOW As String = ddlDOWDetailsDOW.SelectedValue.ToString.ToUpper
            Dim newSvcDate As DateTime


            Return newSvcDate
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "GetNewSvcDate_ForDOW", ex.Message.ToString, "")
            'Exit Function
        End Try
    End Function

    Function GetNewSvcDate_ForWeek(ByVal svcDate As String) As DateTime
        Dim strdate As DateTime = DateTime.Parse(svcDate)
        Dim svcDayOfWeek As DayOfWeek = strdate.DayOfWeek
        Dim svcWeekNumber As String = Int((strdate.Day - 1) / 7 + 1)
        'Dim svcWeekNumber As String = Math.Round((strdate.Day - 1) / 7 + 1)
        'Dim selectedWeekNo As String = Left(ddlWeekNo.SelectedValue.ToString, 1)
        Dim newSvcDate As DateTime


        Return newSvcDate
    End Function

    'Pop-up




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
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "gvClient_PageIndexChanging", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
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
                SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson  From tblCompany A  where (A.Accountid is not null and A.Accountid <> '') and  A.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR A.Name Like '%" + txtPopUpClient.Text.Trim + "%' union SELECT C.id,C.name,C.accountid,C.contactperson  From tblperson C where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and C.AccountID like '" + txtPopUpClient.Text.Trim + "%' OR C.Name Like '%" + txtPopUpClient.Text.Trim + "%'  ORDER BY AccountId"

                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
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

                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR B.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR D.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%') ORDER BY AccountId, LocationId"

                Dim sql As String
                sql = "SELECT A.id,A.name,A.accountid,A.contactperson  From tblCompany A  where (A.Accountid is not null and A.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%'  union (SELECT C.id,C.name,C.accountid,C.contactperson From tblperson C  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' ORDER BY AccountId)"
                SqlDSClient.SelectCommand = sql


                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 "
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "


                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C  Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and D.Accountid <> '')   ORDER BY AccountId, LocationId"

                Dim sql As String
                sql = "SELECT A.id,A.name,A.accountid,A.contactperson  From tblCompany A  where (A.Accountid is not null and A.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson From tblperson C  where (C.Accountid is not null and C.Accountid <> '')   ORDER BY AccountId"
                SqlDSClient.SelectCommand = sql

                'SqlDSClient.SelectCommand = 


                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If


            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "imgbtnClient_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim PH As String = e.Row.Cells(25).Text.Trim
        '    If String.IsNullOrEmpty(PH) = True Or PH = "&nbsp;" Then
        '        'e.Row.BackColor = Color.Brown
        '    Else
        '        e.Row.ForeColor = Color.Red
        '    End If


        'End If
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        Try
            'SqlDataSource1.SelectCommand = txtGridQuery.Text
            GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "GridView1_Sorting", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try

            'If txtProcessed.Text = "Y" Then
            '    txtProcessed.Text = "N"
            '    Exit Sub
            'End If

            lblAlert.Text = ""
            lblMessage.Text = ""
            lblWarningAlert.Text = ""
            UpdatePanel1.Update()

            lblRandom.Text = random.Next(100000, 900000)

            updPanelMassChange1.Update()
            'lblLine4EditAgreeValueSave.Text = txtRandom.Text
            If chkUpdateServiceRecords.Checked = False Then
                mdlWarning.Show()
            Else
                ProcessUpdate()
            End If


        Catch ex As Exception
            'txtProcessed.Text = "N"
            lblAlert.Text = ex.Message
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub ProcessUpdate()
        Try
            txtConfirmationCode.Text = ""
            Dim TotRecsSelected, RecsProcessed As Long
            TotRecsSelected = 0
            RecsProcessed = 0
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

                'txtProcessed.Text = "N"
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtPercChange.Text.Trim) = True Then
                lblAlert.Text = "Please Enter % Change"
                txtPercChange.Focus()
                Exit Sub
            End If

            If (txtPercChange.Text.Trim) = "0" Then
                lblAlert.Text = "% Change cannot be 0"
                txtPercChange.Focus()
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
                lblAlert.Text = "Please Enter Effective Date"
                txtEffectiveDate.Focus()
                Exit Sub
            End If

            btnProcess.Enabled = False

            TotRecsSelected = 0

            For rowIndex As Integer = 0 To GridView1.Rows.Count - 1
                'Dim chkbox As CheckBox = row.FindControl("chkGrid")

                Dim TextBoxchkSelect As CheckBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("chkGrid"), CheckBox)

                If TextBoxchkSelect.Checked = True Then

                    Dim conn As MySqlConnection = New MySqlConnection()

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    conn.Open()

                    Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                    'For rowIndex As Integer = 0 To GridView1.Rows.Count - 1

                    'Dim lblid13 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
                    'Dim lblid14 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
                    'Dim lblid27 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractNoGV"), TextBox)
                    'Dim lblid28 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)

                    Dim lblid1 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
                    Dim lblid2 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
                    Dim lblid3 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtRcnoContractNoGV"), TextBox)
                    Dim lblid4 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)

                    Dim lblid5 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtStatusGV"), TextBox)
                    Dim lblid6 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractGroupGV"), TextBox)
                    Dim lblid7 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                    Dim lblid8 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtClientNameGV"), TextBox)

                    Dim lblid9 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtStartDateGV"), TextBox)
                    Dim lblid10 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtEndDateGV"), TextBox)
                    Dim lblid11 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtServiceAddressGV"), TextBox)
                    Dim lblid12 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtEndOfLastScheduleGV"), TextBox)
                    '''

                    Dim commandService As MySqlCommand = New MySqlCommand

                    commandService.CommandType = CommandType.Text
                    commandService.CommandText = "SELECT count(ContractNo) as totrec FROM tblContractPriceHistory where contractno ='" & lblid4.Text & "'"
                    commandService.Connection = conn

                    Dim drService As MySqlDataReader = commandService.ExecuteReader()
                    Dim dtService As New DataTable
                    dtService.Load(drService)

                    'txtTotServicerec.Text = "0"

                    If dtService.Rows(0)("totrec").ToString > 0 Then
                        Dim commandService1 As MySqlCommand = New MySqlCommand

                        commandService1.CommandType = CommandType.Text
                        commandService1.CommandText = "SELECT max(Date) as Maxdate FROM tblContractPriceHistory where contractno ='" & lblid4.Text & "'"
                        commandService1.Connection = conn

                        Dim drService1 As MySqlDataReader = commandService1.ExecuteReader()
                        Dim dtService1 As New DataTable
                        dtService1.Load(drService1)

                        'txtContractNo.Text = dtService1.Rows(0)("Maxdate").ToString
                        If Convert.ToDateTime(dtService1.Rows(0)("Maxdate").ToString).ToString("yyyy-MM-dd") > Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") Then

                            '''
                            Dim commandInsertIntoBatchContractPriceChangeFail As MySqlCommand = New MySqlCommand

                            commandInsertIntoBatchContractPriceChangeFail.CommandType = CommandType.Text
                            Dim qryfail As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
                            qryfail = qryfail + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

                            commandInsertIntoBatchContractPriceChangeFail.CommandText = qryfail
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.Clear()

                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

                            If String.IsNullOrEmpty(lblid10.Text) = True Then
                                commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
                            Else
                                commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                            End If
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))

                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StaffID", Session("UserID").ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))


                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessStatus", "FAIL")
                            commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ErrorMessage", "Effective Date is earlier than the latest record in Contract Price History")

                            commandInsertIntoBatchContractPriceChangeFail.Connection = conn
                            commandInsertIntoBatchContractPriceChangeFail.ExecuteNonQuery()

                            ''
                            GoTo ProcessNextRec
                        End If
                    End If
                    ''


                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.StoredProcedure
                    command.CommandText = "MassUpdateAgreeValue"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@pr_Rcno", Convert.ToInt32(lblid3.Text))
                    command.Parameters.AddWithValue("@pr_ContractNo", lblid4.Text)
                    command.Parameters.AddWithValue("@pr_OriginalAgreeValue", lblid1.Text)
                    command.Parameters.AddWithValue("@pr_NewAgreeValue", Convert.ToDecimal(lblid2.Text))

                    command.Parameters.AddWithValue("@pr_percChange", txtPercChange.Text)

                    If chkUpdateServiceRecords.Checked = True Then
                        command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "Y")
                    Else
                        command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "N")
                    End If

                    command.Parameters.AddWithValue("@pr_LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@pr_LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))


                    command.Parameters.AddWithValue("@pr_Date", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@pr_UpdateType", "MassUpdate")
                    command.Connection = conn
                    command.ExecuteScalar()

                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "EDITAGREEVALUE", Convert.ToDateTime(txtCreatedOn.Text), txtAgreeVal.Text, txtAgreeValueToUpdate.Text, 0, txtAccountId.Text, "", txtRcno.Text)


                    Dim commandInsertIntoBatchContractPriceChange As MySqlCommand = New MySqlCommand

                    commandInsertIntoBatchContractPriceChange.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
                    qry = qry + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

                    commandInsertIntoBatchContractPriceChange.CommandText = qry
                    commandInsertIntoBatchContractPriceChange.Parameters.Clear()

                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

                    If String.IsNullOrEmpty(lblid10.Text) = True Then
                        commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
                    Else
                        commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                    End If
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))

                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StaffID", Session("UserID").ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessStatus", "SUCCESS")
                    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ErrorMessage", "")


                    commandInsertIntoBatchContractPriceChange.Connection = conn
                    commandInsertIntoBatchContractPriceChange.ExecuteNonQuery()
                    RecsProcessed = RecsProcessed + 1
                    'Next rowIndex

                    commandUpdateServiceRecord.Dispose()

                    'If String.IsNullOrEmpty(txtAgreeValueEdit.Text) = True Then
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTRACTBATHPRICECHANGE", lblid4.Text.ToUpper, "EDITAGREEVALUE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDecimal(lblid1.Text), Convert.ToDecimal(lblid2.Text), 0, lblid7.Text.ToUpper, "Processed Record No. : " & RecsProcessed & "; Effective Date : " & Convert.ToDateTime(txtEffectiveDate.Text).ToString("dd/MM/yyyy") & "; Percentage Change : " & txtPercChange.Text, txtRcno.Text)

                    'Else
                    '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "EDITAGREEVALUE - " & lUpdateType, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), txtAgreeVal.Text, txtAgreeValueEdit.Text, 0, txtAccountId.Text, "", txtRcno.Text)

                    'End If
                End If
ProcessNextRec:
                TotRecsSelected = TotRecsSelected + 1
            Next


            lblMessage.Text = RecsProcessed & " out of " & TotRecsSelected & " Records Sucessfully Updated"
            'UpdatePanel1.Update()

            btnProcess.Enabled = False
            GridView1.Enabled = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            'SqlDataSource1.SelectCommand = ""
            'SqlDataSource1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            'GridView1.DataBind()


            'SqlDataSource1.SelectCommand = txt.Text
            'SqlDataSource1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            'GridView1.DataBind()
            Exit Sub
        Catch ex As Exception
            'txtProcessed.Text = "N"
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
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
                txtPopUpClient.Text = txtName.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                updPanelMassChange1.Update()

                ''SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%')"
                ''SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%' or B.LocationId like '" + txtPopupClientSearch.Text + "%') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contactperson like '%" + txtPopupClientSearch.Text + "%' or D.LocationId like '" + txtPopupClientSearch.Text + "%') order by Accountid, LocationId "
                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR B.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%' union (SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.ID Like '%" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' OR D.LocationID Like '%" + txtPopupClientSearch.Text.Trim + "%') ORDER BY AccountId, LocationId"

                Dim sql As String
                sql = "SELECT A.id,A.name,A.accountid,A.contactperson  From tblCompany A  where (A.Accountid is not null and A.Accountid <> '') and  A.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR A.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%'  union (SELECT C.id,C.name,C.accountid,C.contactperson From tblperson C  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') AND C.AccountID like '" + txtPopupClientSearch.Text.Trim + "%' OR C.NAME Like '%" + txtPopupClientSearch.Text.Trim + "%' ORDER BY AccountId)"
                SqlDSClient.SelectCommand = sql

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                'SqlDSClient.SelectCommand = "SELECT * From tblContactMaster where 1=1 "
                'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as ContType, A.AccountID, A.ID, A.Name, A.ContactPerson, A.Address1, A.Mobile, A.Email,  A.LocateGRP, A.CompanyGroup, A.AddBlock, A.AddNos, A.AddFloor, A.AddUnit, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.Fax, A.Mobile, A.Telephone, A.Salesman, A.Industry, A.billaddress1, A.billstreet, A.billbuilding, A.billcity, A.billpostal, b.LocationId, b.Address1  as ServiceAddress From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid where A.status = 'O' and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION SELECT 'PERSON' as ContType, C.AccountID, C.ID, C.Name, C.ContactPerson, C.Address1, C.TelMobile as Mobile, C.Email,  C.LocateGRP, C.PersonGroup as CompanyGroup, C.AddBlock, C.AddNos, C.AddFloor, C.AddUnit, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.TelFax as Fax, C.TelMobile as Mobile, C.TelHome as Telephone, C.Salesman, '' as Industry, C.billaddress1, C.billstreet, C.billbuilding, C.billcity, C.billpostal, D.LocationId, D.Address1  as ServiceAddress From tblPERSON C Left join tblPersonLocation D on C.Accountid = D.Accountid where 1=1 and C.status = 'O' and  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') order by Accountid, LocationId "
                'SqlDSClient.SelectCommand = "SELECT A.id,A.name,A.accountid,A.contactperson, B.LocationId, B.Address1 as ServiceAddress1,B.ContractGroup, B.serviceName  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson, D.LocationId, D.Address1 as ServiceAddress1, D.ContractGroup, D.serviceName From tblperson C  Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and D.Accountid <> '')   ORDER BY AccountId, LocationId"


                Dim sql As String
                sql = "SELECT A.id,A.name,A.accountid,A.contactperson  From tblCompany A  where (A.Accountid is not null and A.Accountid <> '')  union SELECT C.id,C.name,C.accountid,C.contactperson From tblperson C  where (C.Accountid is not null and C.Accountid <> '')   ORDER BY AccountId"
                SqlDSClient.SelectCommand = sql

                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If


            mdlPopUpClient.Show()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "ImageButton2_Click", ex.Message.ToString, "")
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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtPercChange_TextChanged(sender As Object, e As EventArgs) Handles txtPercChange.TextChanged
        lblAlert.Text = ""

        If (txtPercChange.Text.Trim) = "0" Then
            lblAlert.Text = "% Change cannot be 0"
            txtPercChange.Focus()
            Exit Sub
        End If


        If String.IsNullOrEmpty(txtPercChange.Text.Trim) = True Then
            txtPercChange.Text = 0.0
            'mdlPopupAgreeValue.Show()
            'Exit Sub
        End If

        'If Convert.ToDecimal(txtPriceDecreaseLimit.Text) <> 0.0 Then
        '    If Convert.ToDecimal(txtPercentageChangeAgreeValueEdit.Text) < Convert.ToDecimal(txtPriceDecreaseLimit.Text) Then
        '        txtPercentageChangeAgreeValueEdit.Text = 0.0
        '        txtNewValue.Text = 0.0
        '        lblAlertEditAgreeValue.Text = "Price Decrease Cannot Exceed the Limit of " & txtPriceDecreaseLimit.Text & " %"
        '        'btnAgreeValueSave.Enabled = False
        '        'btnAgreeValueSave.ForeColor = Color.Gray
        '        mdlPopupAgreeValue.Show()
        '        'Exit Sub
        '    End If
        'End If

        'If Convert.ToDecimal(txtPriceIncreaseLimit.Text) <> 0.0 Then
        '    If Convert.ToDecimal(txtPercentageChangeAgreeValueEdit.Text) > Convert.ToDecimal(txtPriceIncreaseLimit.Text) Then
        '        txtPercentageChangeAgreeValueEdit.Text = 0.0
        '        txtNewValue.Text = 0.0
        '        lblAlertEditAgreeValue.Text = "Prine Increase Cannot Exceed the Limit of " & txtPriceIncreaseLimit.Text & " %"
        '        'btnAgreeValueSave.Enabled = False
        '        'btnAgreeValueSave.ForeColor = Color.Gray
        '        mdlPopupAgreeValue.Show()
        '        'Exit Sub
        '    End If
        'End If

        If Convert.ToDecimal(txtPriceDecreaseLimit.Text) <> 0.0 Then
            If Convert.ToDecimal(txtPercChange.Text) < Convert.ToDecimal(txtPriceDecreaseLimit.Text) Then
                txtPercChange.Text = 0.0
                lblAlert.Text = "Price Decrease Cannot Exceed the Limit of " & txtPriceDecreaseLimit.Text & " % for Contract Group " & ddlContractGrp.Text
                Exit Sub

            End If
        End If

        If Convert.ToDecimal(txtPriceIncreaseLimit.Text) <> 0.0 Then
            If Convert.ToDecimal(txtPercChange.Text) > Convert.ToDecimal(txtPriceIncreaseLimit.Text) Then
                txtPercChange.Text = 0.0
                lblAlert.Text = "Prine Increase Cannot Exceed the Limit of " & txtPriceIncreaseLimit.Text & " % for Contract Group " & ddlContractGrp.Text
                Exit Sub
            End If
        End If

        'If Convert.ToDecimal(txtPercChange.Text) < Convert.ToDecimal(txtPriceDecreaseLimit.Text) Then
        '    txtPercChange.Text = 0.0
        '    'txtNewValue.Text = 0.0
        '    lblAlert.Text = "Percentage Change cannot be LESS than " & txtPriceDecreaseLimit.Text
        '    'mdlPopupAgreeValue.Show()
        '    Exit Sub
        'End If

        'If Convert.ToDecimal(txtPercChange.Text) > Convert.ToDecimal(txtPriceIncreaseLimit.Text) Then
        '    txtPercChange.Text = 0.0
        '    'txtNewValue.Text = 0.0
        '    lblAlert.Text = "Percentage Change cannot be MORE than " & txtPriceIncreaseLimit.Text
        '    'mdlPopupAgreeValue.Show()
        '    Exit Sub
        'End If


        If txtPercChange.Text > 0 Then
            lblIncreaseDecrease.Text = "INCREASE"
        Else
            lblIncreaseDecrease.Text = "DECREASE"
        End If
        If GridView1.Rows.Count > 0 Then
            txtCreatedOn.Text = ""
            CalculateNewAgreeValue()
        End If

    End Sub

    Protected Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Protected Sub btnEditAgreeValueSaveYes_Click(sender As Object, e As EventArgs) Handles btnEditAgreeValueSaveYes.Click
        lblWarningAlert.Text = ""

        If String.IsNullOrEmpty(txtConfirmationCode.Text.Trim) = True Then
            lblWarningAlert.Text = "Please Enter Confirmation Code"
            mdlWarning.Show()
            Exit Sub
        End If

        If txtConfirmationCode.Text.Trim <> lblRandom.Text.Trim Then
            lblWarningAlert.Text = "Confirmation Code does not match"
            mdlWarning.Show()
            Exit Sub
        End If

        ProcessUpdate()
    End Sub

    Protected Sub btnEditAgreeValueSaveNo_Click(sender As Object, e As EventArgs) Handles btnEditAgreeValueSaveNo.Click
        txtConfirmationCode.Text = ""
    End Sub

    Protected Sub txtEffectiveDate_TextChanged(sender As Object, e As EventArgs) Handles txtEffectiveDate.TextChanged
        Try
            ''Start:Check for Service Lock
            lblAlert.Text = ""

            Dim sqlstr As String
            sqlstr = ""

            sqlstr = "SELECT svcLock FROM tbllockservicerecord where '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "' between svcdatefrom and svcdateto"

            Dim commandLocked As MySqlCommand = New MySqlCommand

            Dim connLocked As MySqlConnection = New MySqlConnection()
            connLocked.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLocked.Open()

            commandLocked.CommandType = CommandType.Text
            commandLocked.CommandText = sqlstr
            commandLocked.Connection = connLocked

            Dim drLocked As MySqlDataReader = commandLocked.ExecuteReader()
            Dim dtLocked As New DataTable
            dtLocked.Load(drLocked)

            If dtLocked.Rows.Count > 0 Then
                If dtLocked.Rows(0)("svcLock").ToString = "Y" Then
                    lblAlert.Visible = True
                    lblAlert.Text = "Service Period is Locked for this DATE"
                    txtEffectiveDate.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If
            End If


            Dim CurrentYear, currentmonth As String
            CurrentYear = DateTime.Now.Year.ToString()
            currentmonth = DateTime.Now.Month.ToString()

            If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) < currentmonth Then
                lblAlert.Text = "Effective Date should be within current month "
                txtEffectiveDate.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If Year(txtEffectiveDate.Text) < CurrentYear Then
                lblAlert.Text = "Effective Date should be within current month "
                txtEffectiveDate.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            'If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) = currentmonth Then
            'Else
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If
            connLocked.Close()
            connLocked.Dispose()
            commandLocked.Dispose()
            drLocked.Close()


            'End: Check for Service Lock
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ddlContractGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContractGrp.SelectedIndexChanged
        FindPriceIncreaseDecreaseLimit()
    End Sub
End Class
