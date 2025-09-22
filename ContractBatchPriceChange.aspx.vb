Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.Drawing
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel

Imports EASendMail
Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Collections.Generic

Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Data.Odbc
Imports AjaxControlToolkit


Partial Class ContractBatchPriceChange
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Public rcno As String
    Public gridQuery As String
    Shared random As New Random()


    Private Word As Microsoft.Office.Interop.Word.ApplicationClass
    ' The Interop Object for Word
    Private Excel As Microsoft.Office.Interop.Excel.ApplicationClass
    ' The Interop Object for Excel
    Private Unknown As Object = Type.Missing
    ' For passing Empty values
    Public Enum StatusType
        SUCCESS
        FAILED
    End Enum

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
                Page.Form.Attributes.Add("enctype", "multipart/form-data")
                mdlPopUpClient.Hide()
                mdlPopUpTeam.Hide()
                MakeMeNull()
                DisbleControls()
                lblAlert.Text = ""
                lblMessage.Text = ""
                'lblMessage.Text = Session("UserID")
                If Session("UserID") = "SEN" Then
                    btnReset0.Visible = True
                Else
                    btnReset0.Visible = False
                End If

                If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                    btnProcess.Enabled = False
                    btnGo.Enabled = False
                    rdoImport.Enabled = False
                    rdoSearch.Enabled = False
                    txtEffectiveDate.Enabled = False
                    btnQuit_Click(sender, e)
                End If
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
                txtCreatedBy.Text = Session("Userid")
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


    Private Sub DisbleControls()
        ddlContractGrp.Enabled = False
        txtContractNo.Enabled = False
        txtClient.Enabled = False
        txtName.Enabled = False
        txtPercChange.Enabled = False
        btnGo.Enabled = False
        btnImportExcelUpload.Enabled = False
        btnReset.Enabled = False
        FileUpload1.Enabled = False
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

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim cmd As MySqlCommand = conn.CreateCommand()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "CreateTempTableTblContractPriceMasschangetemp"
            cmd.Parameters.Clear()

            cmd.ExecuteNonQuery()
            cmd.Dispose()



            'If String.IsNullOrEmpty(txtClient.Text.Trim) = True And String.IsNullOrEmpty(txtName.Text.Trim) = True Then
            '    lblAlert.Text = "Please Enter AccountID or Name"
            '    Exit Sub
            'End If

            'If String.IsNullOrEmpty(txtName.Text.Trim) = True Then
            '    lblAlert.Text = "Please Enter Name"
            '    Exit Sub
            'End If

            'Dim sql As String

            'sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate from tblContract "
            'sql = sql + " where 1=1 and ((Status ='O') or (Status ='H') or (Status ='C')) and ExcludeBatchPriceChange = False "

            'If String.IsNullOrEmpty(txtEffectiveDate.Text) = False Then
            '    sql = sql + " and ContractDate <= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"
            'End If

            'If Len(txtContractNo.Text) > 3 Then
            '    If String.IsNullOrEmpty(txtContractNo.Text.Trim) = False Then
            '        sql = sql + " and ContractNo like '%" + txtContractNo.Text.Trim + "%'"
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

            'sql = sql + " Order by ContractDate, ContractNo"

            'txt.Text = sql

            'SqlDataSource1.SelectCommand = sql
            'SqlDataSource1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            'GridView1.DataBind()


            '26.06.22

            Dim cmd1 As MySqlCommand = conn.CreateCommand()

            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.CommandText = "InsertintoTblContractPriceMasschangetemp"
            cmd1.Parameters.Clear()

            cmd1.Parameters.AddWithValue("@pr_ContractNo", txtContractNo.Text.Trim.ToUpper)
            cmd1.Parameters.AddWithValue("@pr_ContractGroup", ddlContractGrp.Text.Trim.ToUpper)
            cmd1.Parameters.AddWithValue("@pr_AccountId", txtClient.Text.Trim.ToUpper)
            cmd1.Parameters.AddWithValue("@pr_CustName", txtName.Text.Trim.ToUpper)
            cmd1.Parameters.AddWithValue("@pr_EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
            cmd1.Parameters.AddWithValue("@pr_PercChange", txtPercChange.Text)

            If chkExcludeContractsWithPO.Checked = True Then
                cmd1.Parameters.AddWithValue("@pr_ExcludeContractsWithPO", "Y")
            Else
                cmd1.Parameters.AddWithValue("@pr_ExcludeContractsWithPO", "N")
            End If
            cmd1.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text.ToUpper)
            cmd1.Parameters.AddWithValue("@pr_SelectionType", "Search")
            cmd1.Connection = conn

            cmd1.ExecuteNonQuery()
            cmd1.Dispose()


            '25.06.22
            Dim sql As String

            sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate, PercChange, CreatedBy, CreatedOn, ProcessStatus, ProcessedOn from tblcontractpricemasschangetemp "
            sql = sql + " Order by ContractDate, ContractNo"

            txt.Text = sql

            SqlDataSource1.SelectCommand = sql
            SqlDataSource1.DataBind()

            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            '26.06.22
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
            System.Threading.Thread.Sleep(1000)

            lblAlert.Text = ""
            lblMessage.Text = ""
            lblWarningAlert.Text = ""
            UpdatePanel1.Update()

            lblRandom.Text = random.Next(100000, 900000)

            updPanelMassChange1.Update()

            'If chkUpdateServiceRecords.Checked = False Then
            '    mdlWarning.Show()
            'Else

            '    ProcessUpdate()

            'End If


            mdlPopupZeroValue.Show()
        Catch ex As Exception
            'txtProcessed.Text = "N"
            UnLockReport()
            lblAlert.Text = ex.Message
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "btnProcess_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub LockReport()
        Try
            Dim connLock As MySqlConnection = New MySqlConnection()
            Dim commadLock As MySqlCommand = New MySqlCommand

            connLock.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLock.Open()

            'Dim strSqlLock As String = "INSERT INTO tbllock (ReportName, StartDateTime, RunBy, Status) " & _
            '           "VALUES (@ReportName, @StartDateTime, @RunBy, @Status)"

            Dim commandLock As MySqlCommand = New MySqlCommand
            commandLock.CommandType = CommandType.StoredProcedure
            commandLock.CommandText = "LockTable"

            'commandLock.CommandText = strSqlLock
            commandLock.Parameters.Clear()

            commandLock.Parameters.AddWithValue("@pr_RunBy", Session("UserID"))
            commandLock.Parameters.AddWithValue("@pr_ReportName", "MassContractPriceChange")
            commandLock.Parameters.AddWithValue("@pr_Status", "Locked")
            commandLock.Parameters.AddWithValue("@pr_StartDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            commandLock.Connection = connLock
            commandLock.ExecuteScalar()

            connLock.Close()
            connLock.Dispose()
            commandLock.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("OSINVOICE - " + Session("UserID"), "FUNCTION LOCKREPORT", ex.Message.ToString, "")

            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub UnLockReport()
        ' Locked?

        Try
            Dim connUnLock As MySqlConnection = New MySqlConnection()
            Dim commandUnLock As MySqlCommand = New MySqlCommand

            connUnLock.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connUnLock.Open()


            Dim strSqlUnLock As String = "UPDATE tbllock SET  EndDateTime = @EndDateTime, Status= @Status where Reportname = @ReportName and Status = 'Locked'"


            commandUnLock.CommandType = CommandType.Text
            commandUnLock.CommandText = strSqlUnLock
            commandUnLock.Parameters.Clear()

            'commandUnLock.Parameters.AddWithValue("@RunBy", Session("UserID"))
            commandUnLock.Parameters.AddWithValue("@ReportName", "MassContractPriceChange")
            commandUnLock.Parameters.AddWithValue("@Status", "UnLocked")
            commandUnLock.Parameters.AddWithValue("@EndDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            commandUnLock.Connection = connUnLock
            commandUnLock.ExecuteNonQuery()

            connUnLock.Close()
            connUnLock.Dispose()
            commandUnLock.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("OSINVOICE - " + Session("UserID"), "FUNCTION LOCKREPORT", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
        'Locked? 
    End Sub

    Private Sub FindLastRcnoContractpricehistory(ByVal pContractNo As String)
        Dim conn As MySqlConnection = New MySqlConnection(constr)
        conn.Open()

        txtRcnoFirstContractPriceHistory.Text = 0

        Dim commandLastRcno As MySqlCommand = New MySqlCommand
        commandLastRcno.CommandType = CommandType.Text
        commandLastRcno.CommandText = "SELECT BatchNo FROM tblcontractpricehistory where ContractNo=@ContractNo order by rcno desc Limit 1 "

        commandLastRcno.Parameters.AddWithValue("@ContractNo", pContractNo.Trim)
        commandLastRcno.Connection = conn

        Dim drLastRcno As MySqlDataReader = commandLastRcno.ExecuteReader()
        Dim dtLastRcno As New DataTable
        dtLastRcno.Load(drLastRcno)


        If dtLastRcno.Rows.Count > 0 Then
            txtRcnoFirstContractPriceHistory.Text = dtLastRcno.Rows(0)("BatchNo").ToString
        End If

        'If String.IsNullOrEmpty(txtRcnoFirstContractPriceHistory.Text) = True Then
        '    txtRcnoFirstContractPriceHistory.Text = 0
        'End If
        txtRcnoFirstContractPriceHistory.Text = txtRcnoFirstContractPriceHistory.Text + 1
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

            If rdoSearch.Checked = True Then
                If String.IsNullOrEmpty(txtPercChange.Text.Trim) = True Then
                    lblAlert.Text = "Please Enter % Change"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

                    txtPercChange.Focus()
                    Exit Sub
                End If
            End If

            If rdoSearch.Checked = True Then
                If (txtPercChange.Text.Trim) = "0" Then
                    lblAlert.Text = "% Change cannot be 0"
                    txtPercChange.Focus()
                    Exit Sub
                End If
            End If

            If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
                lblAlert.Text = "Please Enter Effective Date"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

                txtEffectiveDate.Focus()
                Exit Sub
            End If

            'FindLastRcnoContractpricehistory()


            btnProcess.Enabled = False

            TotRecsSelected = 0
            Dim lContractNo As String
            Dim lcount As Integer
            lContractNo = ""
            lcount = 0

            txtCreatedOn.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            System.Threading.Thread.Sleep(2000)

            For rowIndex As Integer = 0 To GridView1.Rows.Count - 1
                'Dim chkbox As CheckBox = row.FindControl("chkGrid")

                Dim TextBoxchkSelect As CheckBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("chkGrid"), CheckBox)

                If TextBoxchkSelect.Checked = True Then

                    txtActualEffectiveDate.Text = txtEffectiveDate.Text
                    Dim conn As MySqlConnection = New MySqlConnection()

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    conn.Open()

                    Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                    'For rowIndex As Integer = 0 To GridView1.Rows.Count - 1

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
                    Dim lblid13 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtPercChangeGV"), TextBox)
                    '''

                    Dim commandService As MySqlCommand = New MySqlCommand

                    commandService.CommandType = CommandType.Text
                    commandService.CommandText = "SELECT count(ContractNo) as totrec FROM tblContractPriceHistory where contractno ='" & lblid4.Text & "'"
                    commandService.Connection = conn

                    Dim drService As MySqlDataReader = commandService.ExecuteReader()
                    Dim dtService As New DataTable
                    dtService.Load(drService)


                    If dtService.Rows(0)("totrec").ToString > 0 Then
                        Dim commandService1 As MySqlCommand = New MySqlCommand

                        commandService1.CommandType = CommandType.Text
                        commandService1.CommandText = "SELECT max(Date) as Maxdate FROM tblContractPriceHistory where contractno ='" & lblid4.Text.Trim & "'"
                        commandService1.Connection = conn

                        Dim drService1 As MySqlDataReader = commandService1.ExecuteReader()
                        Dim dtService1 As New DataTable
                        dtService1.Load(drService1)

                        'txtContractNo.Text = dtService1.Rows(0)("Maxdate").ToString
                        'txtContractNo.Text = Convert.ToDateTime(dtService1.Rows(0)("Maxdate").ToString).ToString("yyyy-MM-dd")
                        If Convert.ToDateTime(dtService1.Rows(0)("Maxdate").ToString).ToString("yyyy-MM-dd") >= Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") Then

                            ' '''
                            'Dim commandInsertIntoBatchContractPriceChangeFail As MySqlCommand = New MySqlCommand

                            'commandInsertIntoBatchContractPriceChangeFail.CommandType = CommandType.Text
                            'Dim qryfail As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
                            'qryfail = qryfail + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

                            'commandInsertIntoBatchContractPriceChangeFail.CommandText = qryfail
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.Clear()

                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

                            'If String.IsNullOrEmpty(lblid10.Text) = True Then
                            '    commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
                            'Else
                            '    commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                            'End If
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))

                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@StaffID", Session("UserID").ToUpper)
                            ''commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ProcessStatus", "FAIL")
                            'commandInsertIntoBatchContractPriceChangeFail.Parameters.AddWithValue("@ErrorMessage", "Effective Date is earlier than the latest record in Contract Price History")

                            'commandInsertIntoBatchContractPriceChangeFail.Connection = conn
                            'commandInsertIntoBatchContractPriceChangeFail.ExecuteNonQuery()

                            ' ''
                            'GoTo ProcessNextRec
                            txtActualEffectiveDate.Text = Convert.ToDateTime(dtService1.Rows(0)("Maxdate").ToString).AddDays(1)

                        End If
                    End If
                    ''

                    If lContractNo <> lblid4.Text.Trim Then
                        lContractNo = lblid4.Text.Trim
                        lcount = 1

                        FindLastRcnoContractpricehistory(lblid4.Text)
                    Else
                        lcount = lcount + 1
                    End If

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.StoredProcedure
                    command.CommandText = "MassUpdateAgreeValue"
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@pr_Rcno", Convert.ToInt32(lblid3.Text))
                    command.Parameters.AddWithValue("@pr_ContractNo", lblid4.Text)
                    command.Parameters.AddWithValue("@pr_OriginalAgreeValue", lblid1.Text)
                    command.Parameters.AddWithValue("@pr_NewAgreeValue", Convert.ToDecimal(lblid2.Text))
                    command.Parameters.AddWithValue("@pr_count", lcount)

                    If rdoSearch.Checked = True Then
                        command.Parameters.AddWithValue("@pr_percChange", txtPercChange.Text)
                    Else
                        command.Parameters.AddWithValue("@pr_percChange", Convert.ToDecimal(lblid13.Text))
                    End If

                    If chkUpdateServiceRecords.Checked = True Then
                        command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "Y")
                    Else
                        command.Parameters.AddWithValue("@pr_UpdateServiceRecord", "N")
                    End If
                    command.Parameters.AddWithValue("@pr_ContractGroup", lblid6.Text)
                    command.Parameters.AddWithValue("@pr_LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@pr_LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    command.Parameters.AddWithValue("@pr_EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@pr_Date", Convert.ToDateTime(txtActualEffectiveDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@pr_UpdateType", "MassUpdate")
                    command.Parameters.AddWithValue("@pr_RcnoFirstContractPriceHistory", txtRcnoFirstContractPriceHistory.Text)
                    command.Parameters.AddWithValue("@pr_BatchNo", Session("UserID") & "_" & Convert.ToDateTime(txtCreatedOn.Text))

                    command.Connection = conn
                    command.ExecuteScalar()

                    'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERVCONT", txtContractNo.Text, "EDITAGREEVALUE", Convert.ToDateTime(txtCreatedOn.Text), txtAgreeVal.Text, txtAgreeValueToUpdate.Text, 0, txtAccountId.Text, "", txtRcno.Text)

                    ''28.06.22
                    'Dim commandInsertIntoBatchContractPriceChange As MySqlCommand = New MySqlCommand

                    'commandInsertIntoBatchContractPriceChange.CommandType = CommandType.Text
                    'Dim qry As String = "INSERT INTO tblBatchContractPriceChange(ContractNo, Status, ContractGroup, AccountID, Name,StartDate,EndDate, ServiceAddress,CurrentAgreedValue,NewAgreedValue, PercentChange,EffectiveDate, StaffID,ProcessedOn,ProcessStatus,ErrorMessage)"
                    'qry = qry + "VALUES(@ContractNo, @Status, @ContractGroup, @AccountID, @Name,@StartDate,@EndDate, @ServiceAddress,@CurrentAgreedValue,@NewAgreedValue, @PercentChange,@EffectiveDate, @StaffID,@ProcessedOn,@ProcessStatus,@ErrorMessage);"

                    'commandInsertIntoBatchContractPriceChange.CommandText = qry
                    'commandInsertIntoBatchContractPriceChange.Parameters.Clear()

                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractNo", lblid4.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Status", lblid5.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ContractGroup", lblid6.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@AccountID", lblid7.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@Name", lblid8.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(lblid9.Text).ToString("yyyy-MM-dd"))

                    'If String.IsNullOrEmpty(lblid10.Text) = True Then
                    '    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid12.Text).ToString("yyyy-MM-dd"))
                    'Else
                    '    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                    'End If
                    ''commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(lblid10.Text).ToString("yyyy-MM-dd"))
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ServiceAddress", lblid11.Text.ToUpper)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@CurrentAgreedValue", Convert.ToDecimal(lblid1.Text))
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@NewAgreedValue", Convert.ToDecimal(lblid2.Text))

                    'If rdoSearch.Checked = True Then
                    '    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
                    'Else
                    '    commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@PercentChange", Convert.ToDecimal(lblid13.Text))
                    'End If

                    ''commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@PercentChange", txtPercChange.Text)
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(txtActualEffectiveDate.Text).ToString("yyyy-MM-dd"))

                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@StaffID", Session("UserID"))
                    ''commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd"))

                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ProcessStatus", "SUCCESS")
                    'commandInsertIntoBatchContractPriceChange.Parameters.AddWithValue("@ErrorMessage", "")

                    'commandInsertIntoBatchContractPriceChange.Connection = conn
                    'commandInsertIntoBatchContractPriceChange.ExecuteNonQuery()
                    ''28.06.22
                    RecsProcessed = RecsProcessed + 1
                    'Next rowIndex

                    commandUpdateServiceRecord.Dispose()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTRACTBATHPRICECHANGE", lblid4.Text.ToUpper, "EDITAGREEVALUE", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDecimal(lblid1.Text), Convert.ToDecimal(lblid2.Text), 0, lblid7.Text.ToUpper, "Processed Record No. : " & RecsProcessed & "; Effective Date : " & Convert.ToDateTime(txtEffectiveDate.Text).ToString("dd/MM/yyyy") & "; Percentage Change : " & txtPercChange.Text, txtRcno.Text)

                End If
ProcessNextRec:
                TotRecsSelected = TotRecsSelected + 1
                System.Threading.Thread.Sleep(500)
            Next

            Label4.Text = "Process Completes on :" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & ". "
            Label5.Text = RecsProcessed & " out of " & TotRecsSelected & " Records Sucessfully Updated"

            mdlPopupSuccess.Show()
            'lblMessage.Text = RecsProcessed & " out of " & TotRecsSelected & " Records Sucessfully Updated"
            'UpdatePanel1.Update()

            btnProcess.Enabled = False
            GridView1.Enabled = False
            FileUpload1.Enabled = False
            btnImportExcelUpload.Enabled = False

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            'SqlDataSource1.SelectCommand = ""
            'SqlDataSource1.DataBind()

            'GridView1.DataSourceID = "SqlDataSource1"
            'GridView1.DataBind()


            SqlDataSource1.SelectCommand = txt.Text
            SqlDataSource1.DataBind()

            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            txtEffectiveDate.Text = ""
            txtActualEffectiveDate.Text = ""
            UnLockReport()
            'lblMessage.Text = "Process is Complete"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)

            Exit Sub
        Catch ex As Exception
            'txtProcessed.Text = "N"
            UnLockReport()
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


            Dim sql As String

            sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, NewAgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate, PercChange, CreatedBy, CreatedOn, ProcessStatus, ProcessedOn from tblcontractpricemasschangetemp "
            sql = sql + " Order by  ContractNo, ContractGroup"

            sql = ""
            txt.Text = sql

            SqlDataSource1.SelectCommand = Sql
            SqlDataSource1.DataBind()

            GridView1.DataSourceID = "SqlDataSource1"
            GridView1.DataBind()

            txtTotalRecords.Text = 0
            Dim sqlstr As String
            sqlstr = ""


            'Contract
            Dim sqlstr1 As String
            sqlstr1 = ""

            sqlstr1 = "SELECT ContractLock FROM tbllockservicerecord where '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "' between svcdatefrom and svcdateto"

            Dim commandLocked1 As MySqlCommand = New MySqlCommand

            Dim conn1 As MySqlConnection = New MySqlConnection()
            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn1.Open()

            commandLocked1.CommandType = CommandType.Text
            commandLocked1.CommandText = sqlstr1
            commandLocked1.Connection = conn1

            Dim dr1 As MySqlDataReader = commandLocked1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)


            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("ContractLock").ToString = "Y" Then
                    lblAlert.Text = "Contract Period " & txtEffectiveDate.Text & " is Locked"
                    txtEffectiveDate.Text = ""
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    conn1.Close()
                    conn1.Dispose()
                    commandLocked1.Dispose()
                    dr1.Close()
                    Exit Sub
                End If
            End If

            conn1.Close()
            conn1.Dispose()
            commandLocked1.Dispose()
            dr1.Close()


            'Contract

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
                    lblAlert.Text = "Service Period " & txtEffectiveDate.Text & " is Locked for this DATE"
                    txtEffectiveDate.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub
                End If
            End If

            'Label4.Text = txtEffectiveDate.Text

            Label11.Text = "Do you wish to process records effective on " & txtEffectiveDate.Text & " ?"
            'Dim CurrentYear, currentmonth As String
            'CurrentYear = DateTime.Now.Year.ToString()
            'currentmonth = DateTime.Now.Month.ToString()

            'If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) < currentmonth Then
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If

            'If Year(txtEffectiveDate.Text) < CurrentYear Then
            '    lblAlert.Text = "Effective Date should be within current month "
            '    txtEffectiveDate.Text = ""
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If

            ''If Year(txtEffectiveDate.Text) = CurrentYear And Month(txtEffectiveDate.Text) = currentmonth Then
            ''Else
            ''    lblAlert.Text = "Effective Date should be within current month "
            ''    txtEffectiveDate.Text = ""
            ''    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            ''    Exit Sub
            ''End If
            connLocked.Close()
            connLocked.Dispose()
            commandLocked.Dispose()
            drLocked.Close()

            FileUpload1.Enabled = True
            btnImportExcelUpload.Enabled = True

            'End: Check for Service Lock
        Catch ex As Exception
            InsertIntoTblWebEventLog("MASS CONTRACT PRICE - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ddlContractGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContractGrp.SelectedIndexChanged
        FindPriceIncreaseDecreaseLimit()
    End Sub


    
  

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblMessage.Text = ""
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            If String.IsNullOrEmpty(filePath) Or filePath = "" Then
                lblMessage.Text = "NO FILE TO DOWNLOAD"
                Return

            End If
            filePath = Server.MapPath("~/Uploads/Asset/") + filePath


            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("DownloadFile", ex.Message.ToString, "")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

   

    Private Function Excel_To_DataTable(ByVal pRutaArchivo As String, ByVal pHojaIndex As Integer) As DataTable
        Dim Tabla As DataTable = Nothing

        Try

            If System.IO.File.Exists(pRutaArchivo) Then
                Dim workbook As IWorkbook = Nothing
                Dim worksheet As ISheet = Nothing
                Dim first_sheet_name As String = ""

                Using FS As FileStream = New FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read)
                    workbook = WorkbookFactory.Create(FS)
                    worksheet = workbook.GetSheetAt(pHojaIndex)
                    first_sheet_name = worksheet.SheetName

                    '    If worksheet.SheetName.ToUpper = rdbModule.SelectedValue.ToString.ToUpper Then
                    '  InsertIntoTblWebEventLog("TestExcel0", worksheet.SheetName.ToUpper + worksheet.LastRowNum.ToString + "Aa", "")


                    If worksheet.LastRowNum > 0 Then

                        Tabla = New DataTable(first_sheet_name)
                        Tabla.Rows.Clear()
                        Tabla.Columns.Clear()

                        For rowIndex As Integer = 0 To worksheet.LastRowNum

                            Dim NewReg As DataRow = Nothing
                            Dim row As IRow = worksheet.GetRow(rowIndex)
                            Dim row2 As IRow = Nothing
                            Dim row3 As IRow = Nothing

                            If rowIndex = 0 Then
                                row2 = worksheet.GetRow(rowIndex + 2)
                                row3 = worksheet.GetRow(rowIndex + 3)

                                'row2 = worksheet.GetRow(rowIndex + 1)
                                'row3 = worksheet.GetRow(rowIndex + 2)
                            End If
                            '    InsertIntoTblWebEventLog("TestExcel1", rowIndex.ToString, worksheet.LastRowNum.ToString)

                            If row IsNot Nothing Then
                                If rowIndex > 1 Then NewReg = Tabla.NewRow()
                                Dim colIndex As Integer = 0

                                For Each cell As ICell In row.Cells
                                    Dim valorCell As Object = Nothing
                                    Dim cellType As String = ""
                                    Dim cellType2 As String() = New String(1) {}
                                    If rowIndex = 1 Then

                                    ElseIf rowIndex = 0 Then

                                        For i As Integer = 0 To 2 - 1
                                            Dim cell2 As ICell = Nothing

                                            If i = 0 Then
                                                cell2 = row2.GetCell(cell.ColumnIndex)
                                            Else
                                                cell2 = row3.GetCell(cell.ColumnIndex)
                                            End If
                                            '   InsertIntoTblWebEventLog("TestExcel5", cell.ColumnIndex.ToString, i.ToString)

                                            If cell2 IsNot Nothing Then

                                                Select Case cell2.CellType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        'cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        cellType2(i) = "System.Boolean"
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        cellType2(i) = "System.String"
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                            cellType2(i) = "System.DateTime"
                                                        Else
                                                            cellType2(i) = "System.Double"
                                                        End If

                                                    Case NPOI.SS.UserModel.CellType.Formula
                                                        Dim continuar As Boolean = True

                                                        Select Case cell2.CachedFormulaResultType
                                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                                cellType2(i) = "System.Boolean"
                                                            Case NPOI.SS.UserModel.CellType.String
                                                                cellType2(i) = "System.String"
                                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                                If HSSFDateUtil.IsCellDateFormatted(cell2) Then
                                                                    cellType2(i) = "System.DateTime"
                                                                Else

                                                                    Try

                                                                        If cell2.CellFormula = "TRUE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar AndAlso cell2.CellFormula = "FALSE()" Then
                                                                            cellType2(i) = "System.Boolean"
                                                                            continuar = False
                                                                        End If

                                                                        If continuar Then
                                                                            cellType2(i) = "System.Double"
                                                                            continuar = False
                                                                        End If

                                                                    Catch
                                                                    End Try
                                                                End If
                                                        End Select

                                                    Case Else
                                                        cellType2(i) = "System.String"
                                                End Select
                                            Else
                                                cellType2(i) = "System.String"
                                            End If
                                            '  InsertIntoTblWebEventLog("TestExcel4", "", cellType2(i).ToString)

                                        Next

                                        If cellType2(0) = cellType2(1) Then
                                            cellType = cellType2(0)
                                        Else
                                            If cellType2(0) Is Nothing Then cellType = cellType2(1)
                                            If cellType2(1) Is Nothing Then cellType = cellType2(0)
                                            If cellType = "" Then cellType = "System.String"
                                        End If

                                        Dim colName As String = "Column_{0}"

                                        Try
                                            colName = cell.StringCellValue
                                        Catch
                                            colName = String.Format(colName, colIndex)
                                        End Try
                                        ' InsertIntoTblWebEventLog("TestExcel2", colName, colIndex)


                                        For Each col As DataColumn In Tabla.Columns
                                            If col.ColumnName = colName Then colName = String.Format("{0}_{1}", colName, colIndex)
                                        Next
                                        '   InsertIntoTblWebEventLog("TestExcel3", colName, cellType)

                                        Dim codigo As DataColumn = New DataColumn(colName, System.Type.[GetType](cellType))
                                        Tabla.Columns.Add(codigo)
                                        colIndex += 1
                                    Else

                                        Select Case cell.CellType
                                            Case NPOI.SS.UserModel.CellType.Blank
                                                valorCell = DBNull.Value
                                            Case NPOI.SS.UserModel.CellType.Boolean
                                                'Select Case cell.BooleanCellValue
                                                '    Case True
                                                valorCell = cell.BooleanCellValue
                                                '    Case False
                                                '        valorCell = cell.BooleanCellValue
                                                '    Case Else
                                                '        valorCell = False

                                                'End Select

                                            Case NPOI.SS.UserModel.CellType.String
                                                valorCell = cell.StringCellValue
                                            Case NPOI.SS.UserModel.CellType.Numeric

                                                If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                    valorCell = cell.DateCellValue
                                                Else
                                                    valorCell = cell.NumericCellValue
                                                End If

                                            Case NPOI.SS.UserModel.CellType.Formula

                                                Select Case cell.CachedFormulaResultType
                                                    Case NPOI.SS.UserModel.CellType.Blank
                                                        valorCell = DBNull.Value
                                                    Case NPOI.SS.UserModel.CellType.String
                                                        valorCell = cell.StringCellValue
                                                    Case NPOI.SS.UserModel.CellType.Boolean
                                                        valorCell = cell.BooleanCellValue
                                                    Case NPOI.SS.UserModel.CellType.Numeric

                                                        If HSSFDateUtil.IsCellDateFormatted(cell) Then
                                                            valorCell = cell.DateCellValue
                                                        Else
                                                            valorCell = cell.NumericCellValue
                                                        End If
                                                End Select

                                            Case Else
                                                valorCell = cell.StringCellValue
                                        End Select
                                        '   InsertIntoTblWebEventLog("TestExcel6", cell.ColumnIndex.ToString + " " + cell.CellType.ToString, valorCell.ToString)

                                        'If cell.CellType.ToString = "Boolean" Then
                                        '    If valorCell.ToString = "TRUE" Or valorCell.ToString = "FALSE" Then

                                        '    Else
                                        '        Continue For

                                        '    End If
                                        'End If

                                        If cell.ColumnIndex <= Tabla.Columns.Count - 1 Then NewReg(cell.ColumnIndex) = valorCell
                                    End If
                                Next
                            End If

                            If rowIndex > 1 Then Tabla.Rows.Add(NewReg)
                        Next


                        Tabla.AcceptChanges()
                    Else
                        lblMessage.Text = "Please import more than one Account details to proceed."
                    End If
                    'Else
                    'lblMessage.Text = "Sheet Name does not match with the selected template."
                    'End If

                End Using
            Else
                Throw New Exception("ERROR 404")
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            'InsertIntoTblWebEventLog("Excel_To_DataTable", ex.Message.ToString, txtCreatedBy.Text)
            Throw ex

        End Try

        Return Tabla
    End Function

    Protected Function InsertData(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable
        '  Try
        Dim qry As String = "INSERT INTO tblcontractpricemasschangetemp (ContractNo, AccountID, CustName, ServiceAddress, ContractGroup, Status, StartDate, EndDate, AgreeValue,  "
        'qry = qry + "PurfmCCode,PurfmInvNo,PurchRef,PurchDt,PurchVal,ObbkVal,ObbkDate,CurrAddr,InCharge,CurrCont,Phone1,Phone2,EstDateIn,"
        'qry = qry + "Notes,ActDateIn,LastMoveId,Project,DateOut,Purpose,AssetCode,Make,Model,Year,Capacity,EngineNo,Value,Remarks,PurfmCName,"
        'qry = qry + "ReferenceOur,SvcFreq,NextSvcDate,LastSvcDate,SvcBy,DWM,SoldToCoID oldToCoName,SoldVal,SoldRef,SoldDate,DisposedRef,"
        'qry = qry + "DisposedDate,AuthorBy,Desc1,AltNo,MfgYear,CustCode,CustName,Reference,RefDate,PriceCode,PROJECTCODE,PROJECTNAME,Cost,"
        'qry = qry + "SupplierCode,SupplierName,LocCode,ValueDate,SoldBy,MarketValue,ScrapToCoID,ScrapToCoName,ScrapVal,ScrapRef,ScrapDate,"
        'qry = qry + "ScrapBy,EngineBrand,EngineModel,ArNo,DueDate,TestNo,TestRemarks,CertType,RentalYN,InChargeID,SelfOwnYN,PurchBy,CostOther,"
        'qry = qry + "IncomeOther,DeprDur,DeprMonthly,DeprEnd,EstLife,DeprOps,CostBillPct,PurchOVal,PurchExRate,PurchCurr,RoadTaxExpiry,CoeExpiry,"
        'qry = qry + "InspectDate,VpcExpiry,VpcNo,PaymentType,MarketCost,CostDate,GroupID,GroupName,SaleableYN,FinCoID,FinCoName,FinDtFrom,"
        'qry = qry + "FinDtTo,GltypeSales,GltypePurchase,LedgercodeSales,LedgercodePurchase,ContactType,TechnicalSpecs,type,CapacityUnitMS,"
        'qry = qry + "RegDate,AgmtNo,LoanAmt,NoInst,IntRate,TermCharges,FirstInst,LastInst,MthlyInst,SubLedgercodeSales,SubLedgercodePurchase,"
        qry = qry + " CreatedBy,CreatedOn)"
        qry = qry + "VALUES (@ContractNo,@AccountID,@CustName, @ServiceAddress, @ContractGroup, @Status, @StartDate, @EndDate, @AgreeValue,"
        'qry = qry + "@PurchVal,@ObbkVal,@ObbkDate,@CurrAddr,@InCharge,@CurrCont,@Phone1,@Phone2,@EstDateIn,@Notes,@ActDateIn,@LastMoveId,@Project,@DateOut,@Purpose,"
        'qry = qry + "@AssetCode,@Make,@Model,@Year,@Capacity,@EngineNo,@Value,@Remarks,@PurfmCName,@ReferenceOur,@SvcFreq,@NextSvcDate,@LastSvcDate,@SvcBy,@DWM,"
        'qry = qry + "@SoldToCoID,@SoldToCoName,@SoldVal,@SoldRef,@SoldDate,@DisposedRef,@DisposedDate,@AuthorBy,@Desc1,@AltNo,@MfgYear,@CustCode,@CustName,@Reference,"
        'qry = qry + "@RefDate,@PriceCode,@PROJECTCODE,@PROJECTNAME,@Cost,@SupplierCode,@SupplierName,@LocCode,@ValueDate,@SoldBy,@MarketValue,@ScrapToCoID,@ScrapToCoName,"
        'qry = qry + "@ScrapVal,@ScrapRef,@ScrapDate,@ScrapBy,@EngineBrand,@EngineModel,@ArNo,@DueDate,@TestNo,@TestRemarks,@CertType,@RentalYN,@InChargeID,@SelfOwnYN,"
        'qry = qry + "@PurchBy,@CostOther,@IncomeOther,@DeprDur,@DeprMonthly,@DeprEnd,@EstLife,@DeprOps,@CostBillPct,@PurchOVal,@PurchExRate,@PurchCurr,@RoadTaxExpiry,"
        'qry = qry + "@CoeExpiry,@InspectDate,@VpcExpiry,@VpcNo,@PaymentType,@MarketCost,@CostDate,@GroupID,@GroupName,@SaleableYN,@FinCoID,@FinCoName,@FinDtFrom,@FinDtTo,"
        'qry = qry + "@GltypeSales,@GltypePurchase,@LedgercodeSales,@LedgercodePurchase,@ContactType,@TechnicalSpecs,@type,@CapacityUnitMS,@RegDate,@AgmtNo,@LoanAmt,"
        'qry = qry + "@NoInst,@IntRate,@TermCharges,@FirstInst,@LastInst,@MthlyInst,@SubLedgercodeSales,@SubLedgercodePurchase,@GPSLabel,@UploadDate,@DelGoogleCalendar,"
        qry = qry + " @CreatedBy,@CreatedOn);"

        Dim lContractNo As String = ""
        Dim Exists As Boolean = True



        'InsertIntoTblWebEventLog("InsertAssetData", dt.Rows.Count.ToString, "")

        Dim drow As DataRow
        Dim dc1 As DataColumn = New DataColumn("ContractNo", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("AccountID", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("PercentageChange", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("CustName", GetType(String))
        Dim dc5 As DataColumn = New DataColumn("ServiceAddress", GetType(String))
        Dim dc6 As DataColumn = New DataColumn("ContractGroup", GetType(String))
        Dim dc7 As DataColumn = New DataColumn("Status", GetType(String))
        'Dim dc8 As DataColumn = New DataColumn("ContractGroup", GetType(String))

        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)
        dtLog.Columns.Add(dc5)
        dtLog.Columns.Add(dc6)
        dtLog.Columns.Add(dc7)
        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            If IsDBNull(r("ContractNo")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + lContractNo + "(SERIALNO_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "SERIALNO_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                'InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("SerialNo"))

                lContractNo = r("ContractNo")
                drow("ContractNo") = lContractNo

            End If



            Dim command3 As MySqlCommand = New MySqlCommand

            command3.CommandType = CommandType.Text

            command3.CommandText = "SELECT Status, AccountId, CustName, ContractGroup, StartDate, EndDate,ServiceAddress, AgreeValue, StartDate, EndDate FROM tblContract where ContractNo=@sno"
            command3.Parameters.AddWithValue("@sno", lContractNo)
            'If IsDBNull(r("IMEI")) Then
            '    command3.Parameters.AddWithValue("@imei", "")
            'Else
            '    command3.Parameters.AddWithValue("@imei", r("IMEI"))
            'End If

            command3.Connection = conn

            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            Dim dt3 As New System.Data.DataTable
            dt3.Load(dr3)

            'InsertIntoTblWebEventLog("AssetImport", r("SerialNo"), "")

            If dt3.Rows.Count > 0 Then

                ''  MessageBox.Message.Alert(Page, "Account ID already exists!!!", "str")
                '' lblAlert.Text = "Account ID already exists!!!"
                'Failure = Failure + 1
                'FailureString = FailureString + " " + ContractNo + "(DUPLICATE)"
                'drow("Status") = "Failed"
                'drow("Remarks") = "ASSET ALREADY EXISTS"
                'If IsDBNull(r("AssetGroup")) = False Then
                '    drow("AssetGroup") = r("AssetGroup")
                'End If
                'dtLog.Rows.Add(drow)

                'Else

                'Check for dropdownlist values, if it exists

                'If IsDBNull(r("AssetGroup")) = False Then
                '    If CheckAssetGroupExists(r("AssetGroup"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(ASSETGROUP DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "ASSET GROUP DOES NOT EXIST"
                '        'If IsDBNull(r("AssetGroup")) = False Then
                '        '    drow("AssetGroup") = r("AssetGroup")
                '        'End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                'If IsDBNull(r("AssetClass")) = False Then
                '    If CheckAssetClassExists(r("AssetClass"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(ASSETCLASS DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "ASSET CLASS DOES NOT EXIST"
                '        If IsDBNull(r("AssetGroup")) = False Then
                '            drow("AssetGroup") = r("AssetGroup")
                '        End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                'If IsDBNull(r("AssetBrand")) = False Then
                '    If CheckAssetBrandExists(r("AssetBrand"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(ASSETBRAND DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "ASSET BRAND DOES NOT EXIST"
                '        If IsDBNull(r("AssetGroup")) = False Then
                '            drow("AssetGroup") = r("AssetGroup")
                '        End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                'If IsDBNull(r("AssetModel")) = False Then
                '    If CheckAssetModelExists(r("AssetBrand"), r("AssetModel"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(ASSETMODEL DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "ASSET MODEL DOES NOT EXIST"
                '        If IsDBNull(r("AssetGroup")) = False Then
                '            drow("AssetGroup") = r("AssetGroup")
                '        End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                'If IsDBNull(r("AssetColor")) = False Then
                '    If CheckAssetColorExists(r("AssetColor"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(ASSETCOLOR DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "ASSET COLOR DOES NOT EXIST"
                '        If IsDBNull(r("AssetGroup")) = False Then
                '            drow("AssetGroup") = r("AssetGroup")
                '        End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                'If IsDBNull(r("SupplierCode")) = False Then
                '    If CheckSupplierExists(r("SupplierCode"), r("SupplierName"), conn) = False Then

                '        Failure = Failure + 1
                '        FailureString = FailureString + " " + SerialNo + "(SUPPLIER DOES NOT EXIST)"
                '        drow("Status") = "Failed"
                '        drow("Remarks") = "SUPPLIER DOES NOT EXIST"
                '        If IsDBNull(r("AssetGroup")) = False Then
                '            drow("AssetGroup") = r("AssetGroup")
                '        End If
                '        dtLog.Rows.Add(drow)
                '        Continue For
                '    End If
                'End If

                Dim cmd As MySqlCommand = conn.CreateCommand()
                '  Dim cmd As MySqlCommand = New MySqlCommand

                cmd.CommandType = CommandType.Text
                cmd.CommandText = qry
                cmd.Parameters.Clear()

                'If IsDBNull(r("AssetNo")) Then
                '    Failure = Failure + 1
                '    FailureString = FailureString + " " + SerialNo + "(ASSETNO_BLANK)"
                '    drow("Status") = "Failed"
                '    drow("Remarks") = "ASSETNO_BLANK"
                '    If IsDBNull(r("AssetGroup")) = False Then
                '        drow("AssetGroup") = r("AssetGroup")
                '    End If
                '    dtLog.Rows.Add(drow)

                '    Continue For

                'Else
                '    cmd.Parameters.AddWithValue("@AssetNo", r("AssetNo").ToString.ToUpper)
                'End If

                If IsDBNull(r("ContractNo")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + lContractNo + "(ASSETGROUP_BLANK)"
                    drow("Status") = "Failed"
                    drow("Remarks") = "ASSETGROUP_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    drow("ContractNo") = r("ContractNo")
                    cmd.Parameters.AddWithValue("@ContractNo", lContractNo.ToUpper)
                End If

                cmd.Parameters.AddWithValue("@AccountID", r("AccountID"))
                cmd.Parameters.AddWithValue("@CustName", r("Name"))
                cmd.Parameters.AddWithValue("@Status", dt3.Rows(0)("Status").ToString)
                cmd.Parameters.AddWithValue("@ContractGroup", dt3.Rows(0)("ContractGroup").ToString)
                cmd.Parameters.AddWithValue("@ServiceAddress", dt3.Rows(0)("ServiceAddress").ToString)

                If IsDBNull(dt3.Rows(0)("StartDate").ToString) Then
                    cmd.Parameters.AddWithValue("@StartDate", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(dt3.Rows(0)("StartDate").ToString).ToString("yyyy-MM-dd"))
                End If

                If IsDBNull(dt3.Rows(0)("EndDate").ToString) Then
                    cmd.Parameters.AddWithValue("@EndDate", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(dt3.Rows(0)("EndDate").ToString).ToString("yyyy-MM-dd"))
                End If
                'cmd.Parameters.AddWithValue("@StartDate", dt3.Rows(0)("StartDate").ToString)
                'cmd.Parameters.AddWithValue("@EndDate", dt3.Rows(0)("EndDate").ToString)
                cmd.Parameters.AddWithValue("@AgreeValue", dt3.Rows(0)("AgreeValue").ToString)


             

                cmd.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text + "_IMPORT")
                'cmd.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text + "_IMPORT")
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                'cmd.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                cmd.Connection = conn

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                'txtAssetNo.Text = ""

                Success = Success + 1
                'drow("Status") = "Success"
                'drow("Remarks") = ""
                dtLog.Rows.Add(drow)
            End If
            ' End If

        Next
        'txtSuccessCount.Text = Success.ToString
        'txtFailureCount.Text = Failure.ToString
        'txtFailureString.Text = FailureString

        'GridView1.DataSource = dtLog
        'GridView1.DataBind()

        'SqlDataSource1.SelectCommand = "Select * from tblcontractpricemasschangetemp "
        'SqlDataSource1.DataBind()
        'GridView1.DataSourceID = "SqlDataSource1"
        'GridView1.DataBind()

        dt.Clear()


        '25.06.22
        Dim sql As String

        sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate from tblcontractpricemasschangetemp "
        'sql = sql + " where 1=1 and ((Status ='O') or (Status ='H') or (Status ='C')) and ExcludeBatchPriceChange = False "
        'sql = sql + " and ((OContractNo is null) or (OContractNo ='')) "


        'If String.IsNullOrEmpty(txtEffectiveDate.Text) = False Then
        '    sql = sql + " and ContractDate <= '" & Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd") & "'"
        'End If

        'If Len(txtContractNo.Text) > 3 Then
        '    If String.IsNullOrEmpty(txtContractNo.Text.Trim) = False Then
        '        sql = sql + " and ContractNo like '%" + txtContractNo.Text.Trim + "%'"
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

        '25.06.22
        Return True

    End Function


    Protected Function InsertDataIntoTable(dt As DataTable, conn As MySqlConnection) As Boolean
        Dim Success As Int32 = 0
        Dim Failure As Int32 = 0
        Dim FailureString As String = ""

        Dim dtLog As New DataTable
        '  Try

        Dim lContractNo As String = ""
        Dim Exists As Boolean = True
        Dim lPercChange As Decimal


        'InsertIntoTblWebEventLog("InsertAssetData", dt.Rows.Count.ToString, "")

        Dim drow As DataRow
        Dim dc0 As DataColumn = New DataColumn("ContractNo", GetType(String))
        Dim dc1 As DataColumn = New DataColumn("ContractNo", GetType(String))
        Dim dc2 As DataColumn = New DataColumn("AccountID", GetType(String))
        Dim dc3 As DataColumn = New DataColumn("PercentageChange", GetType(String))
        Dim dc4 As DataColumn = New DataColumn("CustName", GetType(String))
        Dim dc5 As DataColumn = New DataColumn("ServiceAddress", GetType(String))
        Dim dc6 As DataColumn = New DataColumn("ContractGroup", GetType(String))
        Dim dc7 As DataColumn = New DataColumn("Status", GetType(String))
        'Dim dc8 As DataColumn = New DataColumn("ContractGroup", GetType(String))

        dtLog.Columns.Add(dc1)
        dtLog.Columns.Add(dc2)
        dtLog.Columns.Add(dc3)
        dtLog.Columns.Add(dc4)
        dtLog.Columns.Add(dc5)
        dtLog.Columns.Add(dc6)
        dtLog.Columns.Add(dc7)
        For Each r As DataRow In dt.Rows

            drow = dtLog.NewRow()

            If IsDBNull(r("ContractNo")) Then
                Failure = Failure + 1
                FailureString = FailureString + " " + lContractNo + "(SERIALNO_BLANK)"
                drow("Status") = "Failed"
                drow("Remarks") = "SERIALNO_BLANK"
                dtLog.Rows.Add(drow)
                Continue For
            Else
                'InsertIntoTblWebEventLog("InsertData1", dt.Rows.Count.ToString, r("SerialNo"))

                lContractNo = r("ContractNo")
                drow("ContractNo") = lContractNo
                lPercChange = r("PercentageChange")
            End If



            Dim command3 As MySqlCommand = New MySqlCommand

            command3.CommandType = CommandType.Text

            command3.CommandText = "SELECT Status, AccountId, CustName, ContractGroup, StartDate, EndDate,ServiceAddress, AgreeValue, StartDate, EndDate, PONo FROM tblContract where ContractNo=@sno"
            command3.Parameters.AddWithValue("@sno", lContractNo)

            command3.Connection = conn

            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            Dim dt3 As New System.Data.DataTable
            dt3.Load(dr3)

            If dt3.Rows.Count > 0 Then

                Dim cmd1 As MySqlCommand = conn.CreateCommand()

                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.CommandText = "InsertintoTblContractPriceMasschangetemp"
                cmd1.Parameters.Clear()


                If IsDBNull(r("ContractNo")) Then
                    Failure = Failure + 1
                    FailureString = FailureString + " " + lContractNo
                    drow("Status") = "Failed"
                    drow("Remarks") = "CONTRACT_BLANK"
                    dtLog.Rows.Add(drow)
                    Continue For
                Else
                    drow("ContractNo") = r("ContractNo")
                    cmd1.Parameters.AddWithValue("@pr_ContractNo", lContractNo.ToUpper)
                End If
                cmd1.Parameters.AddWithValue("@pr_EffectiveDate", Convert.ToDateTime(txtEffectiveDate.Text).ToString("yyyy-MM-dd"))
                cmd1.Parameters.AddWithValue("@pr_ContractGroup", "")
                cmd1.Parameters.AddWithValue("@pr_AccountId", "")
                cmd1.Parameters.AddWithValue("@pr_CustName", "")

                If chkExcludeContractsWithPO.Checked = True Then
                    cmd1.Parameters.AddWithValue("@pr_ExcludeContractsWithPO", "Y")
                Else
                    cmd1.Parameters.AddWithValue("@pr_ExcludeContractsWithPO", "N")
                End If

                cmd1.Parameters.AddWithValue("@pr_PercChange", lPercChange * 100)
                cmd1.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text.ToUpper)
                cmd1.Parameters.AddWithValue("@pr_SelectionType", "Import")
                cmd1.Connection = conn

                cmd1.ExecuteNonQuery()
                cmd1.Dispose()

                Success = Success + 1

                dtLog.Rows.Add(drow)
            End If
            ' End If

        Next

        dt.Clear()


        '25.06.22
        Dim sql As String

        sql = "Select ContractNo, Status, ContractGroup, AccountID, CustName, StartDate, EndDate, Rcno, AgreeValue, NewAgreeValue, ServiceAddress, EndOfLastSchedule, ContractDate, PercChange, CreatedBy, CreatedOn, ProcessStatus, ProcessedOn from tblcontractpricemasschangetemp "

        'sql = sql + " Order by ContractDate, ContractNo"
        sql = sql + " Order by  ContractNo, ContractGroup"
        txt.Text = sql

        SqlDataSource1.SelectCommand = sql
        SqlDataSource1.DataBind()

        GridView1.DataSourceID = "SqlDataSource1"
        GridView1.DataBind()

        'CalculateNewAgreeValue()

        CalculateNewAgreeValueImport()

        txtTotalRecords.Text = GridView1.Rows.Count

        btnProcess.Enabled = True
        GridView1.Enabled = True

        '25.06.22
        Return True

    End Function


    Private Sub CalculateNewAgreeValueImport()
        For rowIndex As Integer = 0 To GridView1.Rows.Count - 1

            Dim lblid1 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtAgreeValueGV"), TextBox)
            Dim lblid2 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtNewAgreeValueGV"), TextBox)
            Dim lblid3 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
            Dim lblid4 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtTotalServiceValueGV"), TextBox)
            Dim lblid5 As TextBox = CType(GridView1.Rows(rowIndex).Cells(0).FindControl("txtPercChangeGV"), TextBox)

            'lblid2.Text = (Convert.ToDecimal(lblid1.Text) + (Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid5.Text) * 0.01)).ToString("N2")


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

    Protected Sub rdoSearch_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSearch.CheckedChanged
        lblAlert.Text = ""

        If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
            lblAlert.Text = "Please Enter Effective Date"
            txtEffectiveDate.Focus()
            rdoImport.Checked = False
            Exit Sub
        End If

        If rdoImport.Checked = True Then
            rdoImport.Checked = False
            btnImportExcelUpload.Enabled = False
            btnGo.Enabled = True
            ddlContractGrp.Enabled = True
            txtContractNo.Enabled = True
            chkExcludeContractsWithPO.Enabled = True
            chkExcludeContractsWithPO.Checked = False
            txtClient.Enabled = True
            txtName.Enabled = True
            txtPercChange.Enabled = True
            btnReset.Enabled = True
        Else
            'ddlContractGrp.SelectedIndex = 0
            txtClient.Text = ""
            txtName.Enabled = False
            txtPercChange.Enabled = False
            btnGo.Enabled = False
            btnReset.Enabled = False
        End If
    End Sub

    Protected Sub rdoImport_CheckedChanged(sender As Object, e As EventArgs) Handles rdoImport.CheckedChanged
        lblAlert.Text = ""

        If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
            lblAlert.Text = "Please Enter Effective Date"
            txtEffectiveDate.Focus()
            rdoImport.Checked = False
            Exit Sub
        End If

        If rdoImport.Checked = True Then
            rdoSearch.Checked = False
            ddlContractGrp.SelectedIndex = 0
            txtClient.Text = ""
            txtName.Text = ""
            txtPercChange.Text = ""
            chkExcludeContractsWithPO.Enabled = True
            chkExcludeContractsWithPO.Checked = False
            'ddlContractGrp.SelectedIndex = 0
            ddlContractGrp.Enabled = False
            txtContractNo.Enabled = False
            txtClient.Enabled = False
            txtName.Enabled = False
            txtPercChange.Enabled = False
            btnGo.Enabled = False
            btnImportExcelUpload.Enabled = True
            btnReset.Enabled = False
        Else
            btnImportExcelUpload.Enabled = False
        End If
    End Sub

    Protected Sub btnImportExcelUpload_Click(sender As Object, e As EventArgs) Handles btnImportExcelUpload.Click
        'Protected Sub btnImportExcelUpload_Click(sender As Object, e As ImageClickEventArgs) Handles btnImportExcelUpload.Click
        lblAlert.Text = ""
        'lblMessageImportExcel.Text = ""
        lblMessage.Text = ""


        ' Locked?


        Dim connLock As MySqlConnection = New MySqlConnection()
        Dim commandIsLock As MySqlCommand = New MySqlCommand

        connLock.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connLock.Open()

        commandIsLock.CommandType = CommandType.Text
        commandIsLock.CommandText = "select StartDateTime, RunBy from tbllock where  ReportName ='MassContractPriceChange' and Status ='Locked'"
        commandIsLock.Connection = connLock

        Dim drLock As MySqlDataReader = commandIsLock.ExecuteReader()
        Dim dtLock As New DataTable
        dtLock.Load(drLock)

        If dtLock.Rows.Count > 0 Then
            If Convert.ToDateTime(dtLock.Rows(0)("StartDateTime").ToString).AddMinutes(15) <= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") Then
                UnLockReport()
                LockReport()
            Else
                lblAlert.Text = "This Module is currently running. Please wait until it completes before you can run. " & vbNewLine & "Last run by " & dtLock.Rows(0)("RunBy").ToString
                connLock.Close()
                connLock.Dispose()
                Exit Sub
            End If
        Else
            LockReport()
        End If

        If String.IsNullOrEmpty(txtEffectiveDate.Text.Trim) = True Then
            lblAlert.Text = "Please Enter Effective Date"
            txtEffectiveDate.Focus()
            Exit Sub
        End If

        If FileUpload1.HasFile = False Then
            lblMessage.Text = "CHOOSE FILE TO IMPORT"
            'mdlPopupImportExcel.Show()
            Exit Sub
        End If

        Dim ofilename As String = ""
        Dim sfilename As String = ""
        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "11", "TEST")

        ofilename = Path.GetFileName(FileUpload1.PostedFile.FileName)
        '   InsertIntoTblWebEventLog("btnImportExcelUpload_Click", "11", ofilename)

        Dim ext As String = Path.GetExtension(ofilename)
        sfilename = ofilename.Split("."c)(0)


        Dim folderPath As String = Server.MapPath("~/Uploads/Excel/")
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileName As String = folderPath + sfilename + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + "_" + txtCreatedBy.Text + ext

        If System.IO.File.Exists(fileName) Then

            System.IO.File.Delete(fileName)
        End If
        'Save the File to the Directory (Folder).
        FileUpload1.PostedFile.SaveAs(fileName)

        txtWorkBookName.Text = sfilename
        Dim file As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
        Dim dropdownworkbook = New XSSFWorkbook(file)
        file.Close()
        file.Dispose()

        Dim dt As New DataTable
        dt = Excel_To_DataTable(fileName, 0)
        ' Response.Write(dt.Rows.Count.ToString)
        If dt Is Nothing Then
            lblAlert.Text = "DATA NOT IMPORTED"
        Else
            Dim res As Boolean = True


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim count As String = dt.Rows.Count.ToString

            Dim cmd As MySqlCommand = conn.CreateCommand()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "CreateTempTableTblContractPriceMasschangetemp"
            cmd.Parameters.Clear()

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            'InsertData(dt, conn)
            InsertDataIntoTable(dt, conn)
            'lblMessage.Text = "Total Records Imported : " + count

            Dim chk1 As CheckBox
            chk1 = DirectCast(GridView1.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)

            'Dim TextBoxchkSelect2 As CheckBox = CType(GridView1.Rows(rowIndex1).Cells(0).FindControl("chkAll"), CheckBox)
            chk1.Checked = True
            chk1.Enabled = False

            For rowIndex1 As Integer = 0 To GridView1.Rows.Count - 1
                Dim chk2 As CheckBox = CType(GridView1.Rows(rowIndex1).Cells(0).FindControl("chkGrid"), CheckBox)


                chk2.Checked = True
                chk2.Enabled = True


            Next rowIndex1
        End If
        'mdlPopupImportExcel.Show()
    End Sub

    Protected Sub btnImportExcelTemplate_Click(sender As Object, e As ImageClickEventArgs) Handles btnImportExcelTemplate.Click
        Try
            lblAlert.Text = "aa"
            Dim filePath As String = ""

            filePath = "BatchContractPriceChangeTemplate.xlsx"
            lblAlert.Text = lblAlert.Text + "bb"
            filePath = Server.MapPath("~/Uploads/Excel/ExcelTemplates/") + filePath
            'filePath = Server.MapPath("~/Uploads/BatchContractPriceChange/") + filePath
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
            lblAlert.Text = lblAlert.Text + "cc"

            'Dim filePath As String = ""

            'filePath = "Asset_ExcelTemplate.xlsx"

            'filePath = Server.MapPath("~/Uploads/Excel/ExcelTemplates/") + filePath
            'Response.ContentType = ContentType
            'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            'Response.WriteFile(filePath)
            'Response.End()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

            'InsertIntoTblWebEventLog("Template", ex.Message.ToString, "Asset")
        End Try
    End Sub

    Protected Sub btnZeroValueYes_Click(sender As Object, e As EventArgs) Handles btnZeroValueYes.Click
        ProcessUpdate()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mdlPopupSuccess.Hide()
    End Sub

    Private Sub UpdateFirstRcno(ByVal pStartRcno As Long, ByVal pEndRcno As Long)
        Try
            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            'command.CommandText = "Select Rcno, ContractNo, ContractGroup, Type, date, ModifiedOn from tblContractpriceHistory where Type <> 'OPENING' and ((ContractNo ='SVCN202106-000281') or (ContractNo ='SVCN202106-000282') or (ContractNo = 'SVCN202106-000285')) order by ContractNo, Date, ModifiedOn "
            'command.CommandText = "Select Rcno, ContractNo, ContractGroup, Type, date, ModifiedOn from tblContractpriceHistory where BatchNo <>0 and ((ContractNo ='SVCN202106-000281') or (ContractNo ='SVCN202106-000282') or (ContractNo = 'SVCN202106-000285')) order by ContractNo, Date, ModifiedOn "

            command.CommandText = "Select Rcno, ContractNo, ContractGroup, Type, date, ModifiedOn from tblContractpriceHistory where BatchNo = 0 and Rcno between " & pStartRcno & " and " & pEndRcno & " order by ContractNo, Date, ModifiedOn "
            command.CommandTimeout = 6000
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            Dim lContractNo As String
            Dim lContractGroup As String
            Dim ldate As Date
            Dim lType As String
            Dim lRcno As Long
            Dim ctr As Long

            lContractNo = ""
            lContractGroup = ""
            ldate = "2020-01-01"
            lType = ""
            lRcno = 0
            ctr = 0

            If dt.Rows.Count > 0 Then
                For x As Integer = 0 To dt.Rows.Count - 1

                    lRcno = dt.Rows(x)("Rcno").ToString()

                    If lContractNo <> dt.Rows(x)("ContractNo").ToString() Or lType <> dt.Rows(x)("Type").ToString() Or ldate <> dt.Rows(x)("Date").ToString() Then
                        PopulateFindLastRcnoContractpricehistory(dt.Rows(x)("ContractNo").ToString())
                    End If

                    'If lType <> dt.Rows(x)("Type").ToString() Then
                    '    PopulateFindLastRcnoContractpricehistory(dt.Rows(x)("ContractNo").ToString())
                    'End If

                    'If ldate <> dt.Rows(x)("Date").ToString() Then
                    '    PopulateFindLastRcnoContractpricehistory(dt.Rows(x)("ContractNo").ToString())
                    'End If

                    Dim commandUpdateBatchNo As MySqlCommand = New MySqlCommand
                    commandUpdateBatchNo.CommandType = CommandType.StoredProcedure
                    commandUpdateBatchNo.CommandText = "UpdateBtachNoTblContractpriceHistory"
                    commandUpdateBatchNo.Parameters.Clear()

                    commandUpdateBatchNo.Parameters.AddWithValue("@pr_Rcno", lRcno)
                    commandUpdateBatchNo.Parameters.AddWithValue("@pr_BatchNo", txtRcnoFirstContractPriceHistory.Text)

                    commandUpdateBatchNo.Connection = conn
                    commandUpdateBatchNo.ExecuteNonQuery()

                    lContractNo = dt.Rows(x)("ContractNo").ToString()
                    ldate = dt.Rows(x)("Date").ToString()
                    lContractGroup = dt.Rows(x)("ContractGroup").ToString()
                    lType = dt.Rows(x)("Type").ToString()
                    ctr = ctr + 1
                    'lblMessage.Text = "Processed - ctr = " & ctr & "; rcno : " & lRcno
                    'updPanelMassChange1.Update()
                Next x

            End If

            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
            lblMessage.Text = "Complete - ctr = " & ctr & "; rcno : " & lRcno
            updPanelMassChange1.Update()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnReset0_Click(sender As Object, e As EventArgs) Handles btnReset0.Click
        Try
            lblMessage.Text = ""

            'UpdateFirstRcno(0, 0)

            UpdateFirstRcno(0, 50000)
            lblMessage.Text = "Complete upto ctr 50000"
            updPanelMassChange1.Update()

            UpdateFirstRcno(50001, 100000)

            lblMessage.Text = "Complete upto ctr 100000"
            updPanelMassChange1.Update()

            UpdateFirstRcno(100001, 150000)
            lblMessage.Text = "Complete upto ctr 150000"
            updPanelMassChange1.Update()

            UpdateFirstRcno(150001, 200000)
            UpdateFirstRcno(200001, 250000)
            UpdateFirstRcno(250001, 300000)
            UpdateFirstRcno(300001, 350000)
            UpdateFirstRcno(350001, 400000)
            'UpdateFirstRcno(400001, 450000)

       


            lblMessage.Text = "Complete.. "
            updPanelMassChange1.Update()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub


    Private Sub PopulateFindLastRcnoContractpricehistory(ByVal pContractNo As String)
        Try
            Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()

            txtRcnoFirstContractPriceHistory.Text = 0

            Dim commandLastRcno As MySqlCommand = New MySqlCommand
            commandLastRcno.CommandType = CommandType.Text
            commandLastRcno.CommandText = "SELECT BatchNo FROM tblcontractpricehistory where ContractNo=@ContractNo and BatchNo <>0 order by rcno desc Limit 1 "

            commandLastRcno.Parameters.AddWithValue("@ContractNo", pContractNo.Trim)
            commandLastRcno.Connection = conn

            Dim drLastRcno As MySqlDataReader = commandLastRcno.ExecuteReader()
            Dim dtLastRcno As New DataTable
            dtLastRcno.Load(drLastRcno)


            If dtLastRcno.Rows.Count > 0 Then
                txtRcnoFirstContractPriceHistory.Text = dtLastRcno.Rows(0)("BatchNo").ToString
            End If

            'If String.IsNullOrEmpty(txtRcnoFirstContractPriceHistory.Text) = True Then
            '    txtRcnoFirstContractPriceHistory.Text = 0
            'End If
            txtRcnoFirstContractPriceHistory.Text = txtRcnoFirstContractPriceHistory.Text + 1
            conn.Close()
            conn.Dispose()
            commandLastRcno.Dispose()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

        End Try
    End Sub
End Class
