Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading
'Imports Microsoft.Office.Interop.Word
'Imports Microsoft.Office.Interop.Excel

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Globalization
Imports EASendMail

'Imports System.Web.UI.HtmlControls.HtmlIframe

Partial Class CustomerPortal
    Inherits System.Web.UI.Page
    'End Class

    Public rcno As String
    Public svcrcno As String
    Private Shared gScheduler, gSalesman, gIncharge As String
    Public isInPage As Boolean = False
    Public Message As String = String.Empty
    ' To store the Error or Message
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
    ' To Specify Success or Failure Types
    Public Status As StatusType


    'Public Function IsValidEmailAddress(email As String) As Boolean
    '    Try
    '        IsValidEmailAddress = False
    '        'Dim emailChecked = New System.Net.Mail.MailAddress(email)
    '        'Return True

    '        IsValidEmailAddress = Regex.IsMatch(email.Trim(), "\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");

    '        If (IsValidEmailAddress) Then
    '            Return True
    '        End If
    '        'Then
    '    Catch

    '        Return False
    '    End Try
    'End Function


  

    Public Sub MakeUserAccessNull()
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            txtRcno.Text = ""
            ddlLocationAccessType.SelectedIndex = 0
            txtAccountIDCPUserAccess.Text = ""
            txtAccountNameUserAccess.Text = ""
            txtAccountTypeCPUserAccess.SelectedIndex = 0
            btnLocations.Text = "LOCATIONS"

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "FUNCTION MAKEMENULL", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub EnableControls()
        Try
            txtAccountIDCP.Enabled = True
            txtEmailCP.Enabled = True
            txtNameCP.Enabled = True
            txtPwdCP.Enabled = True
            txtUserIDCP.Enabled = True
            chkServiceLocationAccess.Enabled = False
            chkServiceRecordAccess.Enabled = True
            chkInvoiceAccess.Enabled = True
            chkCNAccess.Enabled = True
            chkDNAccess.Enabled = True
            chkReceiptAccess.Enabled = True
            chkReportAccess.Enabled = True
            chkAdjustmentAccess.Enabled = True
            txtStatusRemarks.Enabled = True
            chkChangePasswordonNextLogin.Enabled = True
            'chkStatusCP.Enabled = True
            chkViewOpenServiceRecord.Enabled = True
            ddlStatus.Text = "A - Active"
           

            'btnSave.Enabled = False
            'btnSave.ForeColor = System.Drawing.Color.Gray
            'btnCancel.Enabled = False
            'btnCancel.ForeColor = System.Drawing.Color.Gray

            'btnADD.Enabled = True
            'btnADD.ForeColor = System.Drawing.Color.Black
            'btnCopyAdd.Enabled = False
            'btnCopyAdd.ForeColor = System.Drawing.Color.Gray
            'btnDelete.Enabled = False
            'btnDelete.ForeColor = System.Drawing.Color.Gray
            'btnQuit.Enabled = True
            'btnQuit.ForeColor = System.Drawing.Color.Black
            'btnPrint.Enabled = True
            'btnPrint.ForeColor = System.Drawing.Color.Black

            'btnContract.Enabled = False
            'btnContract.ForeColor = System.Drawing.Color.Gray
            'btnTransactions.Enabled = False
            'btnTransactions.ForeColor = System.Drawing.Color.Gray
            'btnChangeStatus.Enabled = False
            'btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            'btnFilter.Enabled = True
            'btnFilter.ForeColor = System.Drawing.Color.Black

            'btnTransfersSvc.Enabled = True
            'btnTransfersSvc.ForeColor = System.Drawing.Color.Black

            'btnSvcAdd.Enabled = True
            'btnSvcCancel.Enabled = True
            'btnSvcEdit.Enabled = True
            'btnSvcSave.Enabled = True
            'btnSvcDelete.Enabled = True

            'btnSvcAdd.ForeColor = System.Drawing.Color.Black
            'btnSvcCancel.ForeColor = System.Drawing.Color.Black
            'btnSvcEdit.ForeColor = System.Drawing.Color.Black
            'btnSvcSave.ForeColor = System.Drawing.Color.Black
            'btnSvcDelete.ForeColor = System.Drawing.Color.Black
            'btnContract.Enabled = False
            'btnContract.ForeColor = System.Drawing.Color.Black

           
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "FUNCTION ENABLECONTROLS", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Private Sub DisableControls()
        Try

          

            'btnSave.Enabled = True
            'btnSave.ForeColor = System.Drawing.Color.Black
            'btnCancel.Enabled = True
            'btnCancel.ForeColor = System.Drawing.Color.Black

            'btnADD.Enabled = False
            'btnADD.ForeColor = System.Drawing.Color.Gray

            'btnCopyAdd.Enabled = False
            'btnCopyAdd.ForeColor = System.Drawing.Color.Gray

            'btnDelete.Enabled = False
            'btnDelete.ForeColor = System.Drawing.Color.Gray

            btnQuit.Enabled = False
            btnQuit.ForeColor = System.Drawing.Color.Gray

            'btnPrint.Enabled = False
            'btnPrint.ForeColor = System.Drawing.Color.Gray
            'btnTransactions.Enabled = False
            'btnTransactions.ForeColor = System.Drawing.Color.Gray
            'btnChangeStatus.Enabled = False
            'btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            'btnSvcAdd.Enabled = False
            'btnSvcCancel.Enabled = False
            'btnSvcEdit.Enabled = False
            'btnSvcCopy.Enabled = False
            'btnSvcSave.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcAdd.ForeColor = System.Drawing.Color.Gray
            'btnSvcCancel.ForeColor = System.Drawing.Color.Gray
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcCopy.ForeColor = System.Drawing.Color.Gray
            'btnSvcSave.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            'btnSvcContract.Enabled = False
            'btnSvcContract.ForeColor = System.Drawing.Color.Gray

            'btnSvcService.Enabled = False
            'btnSvcService.ForeColor = System.Drawing.Color.Gray

            'btnContract.Enabled = False
            'btnContract.ForeColor = System.Drawing.Color.Gray

            'btnFilter.Enabled = False
            'btnFilter.ForeColor = System.Drawing.Color.Gray

            'btnTransfersSvc.Enabled = False
            'btnTransfersSvc.ForeColor = System.Drawing.Color.Gray

            'btnSpecificLocation.Enabled = False
            'btnSpecificLocation.ForeColor = System.Drawing.Color.Gray

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "FUNCTION DISABLECONTROLS", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


   

    Private Sub AccessControl()
        '''''''''''''''''''Access Control 
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        Try

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT x0181, x0181Add, x0181Edit, x0181Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                If Convert.ToBoolean(dt.Rows(0)("x0181")) = False Then
                    If dt.Rows(0)("x0181").ToString() = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0181Add")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0181Add")) = False Then
                        Me.btnAddCP.Enabled = dt.Rows(0)("x0181Add").ToString()
                        'Me.btnCopy.Enabled = dt.Rows(0)("X0252Add").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0181Edit")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0181Edit")) = False Then
                        Me.btnEditCP.Enabled = dt.Rows(0)("x0181Edit").ToString()
                        'Me.btnCopy.Enabled = dt.Rows(0)("X0252Add").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0181Delete")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0181Delete")) = False Then
                        Me.btnDeleteCP.Enabled = dt.Rows(0)("x0181Delete").ToString()
                        'Me.btnCopy.Enabled = dt.Rows(0)("X0252Add").ToString()
                    End If
                End If

            End If
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "FUNCTION ACCESSCONTROL", ex.Message.ToString, Session("SecGroupAuthority"))
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
        'End If

        '''''''''''''''''''Access Control 

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                MakeCPNull()
                DisableCPControls()
                DisableUserAccessControls()
                'MakeMeNull()
                'EnableControls()

                'MakeCPNull()
                'EnableCPControls()

                AccessControl()

                'ddlLocation.Attributes.Add("disabled", "true")
                lblDomainName.Text = ConfigurationManager.AppSettings("DomainName").ToString()
                Dim UserID As String = Convert.ToString(Session("UserID"))
                txtCreatedBy.Text = UserID
                txtLastModifiedBy.Text = UserID
                txtCreatedOn.Attributes.Add("readonly", "readonly")
                txtGroupAuthority.Text = Session("SecGroupAuthority")
                txtPwdCP.Attributes.Add("type", "password")

                txtDDLText.Text = "-1"

                Try
                    tb1.ActiveTabIndex = 0
                    tb1.Visible = True

                    SqlDSCP.SelectCommand = "SELECT * FROM tblcustomerportaluser order by accountid"
                    gvCP.DataSourceID = "SqlDSCP"
                    gvCP.DataBind()
                    txt.Text = "SELECT * FROM tblcustomerportaluser order by accountid"
                    'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                    '    tb1.Tabs(4).Visible = False
                    'End If



                Catch ex As Exception
                    InsertIntoTblWebEventLog("CustomerPortalUser - " + txtCreatedBy.Text, "PAGE LOAD - NOT POSTBACK", ex.Message.ToString, txtGroupAuthority.Text)
                End Try

            Else
                'If txtTransactiindex.Text = "Y" Then
                '    Session.Add("customerfrom", "Corporate")
                '    Session.Add("AccountID", txtAccountID.Text)
                '    Session("contracttype") = "CORPORATE"
                '    Session("companygroup") = ddlCompanyGrp.Text
                '    Session("accountid") = txtAccountID.Text.Trim
                '    Session("custname") = txtNameE.Text
                '    txtTransactionSelected.Text = "N"
                'Else
                Session.Remove("customerfrom")
                'End If

            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "Page_Load", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
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
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "PopulateDropDownList", ex.Message.ToString, query + " , " + TryCast(ddl, DropDownList).Text + " , " + " , " + textField + " , " + valueField)
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub




    Private Sub EnableUserAccessControls()
        btnSaveUserAccess.Enabled = True
        btnSaveUserAccess.ForeColor = System.Drawing.Color.Black

        btnCancelUserAccess.Enabled = True
        btnCancelUserAccess.ForeColor = System.Drawing.Color.Black

        btnAddCPUserAccess.Enabled = False
        btnAddCPUserAccess.ForeColor = System.Drawing.Color.Gray

        btnEditCPUserAccess.Enabled = False
        btnEditCPUserAccess.ForeColor = System.Drawing.Color.Gray

        btnDeleteCPUserAccess.Enabled = False
        btnDeleteCPUserAccess.ForeColor = System.Drawing.Color.Gray

        btnLocations.Enabled = False
        btnLocations.ForeColor = System.Drawing.Color.Gray

        btnClientSearch.Visible = True

        txtAccountTypeCPUserAccess.Enabled = True
        txtAccountIDCPUserAccess.Enabled = True
        txtAccountNameUserAccess.Enabled = True
        ddlLocationAccessType.Enabled = True

        'gvNotesMaster.Enabled = False
        'gvNotesMaster.ForeColor = System.Drawing.Color.Gray
    End Sub

    Private Sub DisableUserAccessControls()
        btnSaveUserAccess.Enabled = False
        btnSaveUserAccess.ForeColor = System.Drawing.Color.Gray

        btnCancelUserAccess.Enabled = False
        btnCancelUserAccess.ForeColor = System.Drawing.Color.Gray

        btnAddCPUserAccess.Enabled = True
        btnAddCPUserAccess.ForeColor = System.Drawing.Color.Black

        btnEditCPUserAccess.Enabled = False
        btnEditCPUserAccess.ForeColor = System.Drawing.Color.Gray

        btnDeleteCPUserAccess.Enabled = False
        btnDeleteCPUserAccess.ForeColor = System.Drawing.Color.Gray

        btnLocations.Enabled = False
        btnLocations.ForeColor = System.Drawing.Color.Gray

        btnClientSearch.Visible = False

        txtAccountTypeCPUserAccess.Enabled = False
        txtAccountIDCPUserAccess.Enabled = False
        txtAccountNameUserAccess.Enabled = False
        ddlLocationAccessType.Enabled = False

        'gvNotesMaster.Enabled = True
        'gvNotesMaster.ForeColor = System.Drawing.Color.Black
    End Sub


    Protected Sub OnSelectedIndexChangedgCP(sender As Object, e As EventArgs) Handles gvCP.SelectedIndexChanged
       
        For Each row As GridViewRow In gvCP.Rows
            'If row.RowIndex = gvNotesMaster.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If

            If row.RowIndex = gvCP.SelectedIndex Then
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
            insCmds.Dispose()

            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

   

    Protected Sub OnRowDataBoundgCP(sender As Object, e As GridViewRowEventArgs) Handles gvCP.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvCP, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Public Sub MakeCPNull()
        txtAccountIDCP.Text = ""
        'txtStatusCP.Text = ""
        txtAccountTypeCP.Text = ""
        'chkStatusCP.Checked = True
        ddlStatus.SelectedIndex = 0
        txtNameCP.Text = ""
        txtEmailCP.Text = ""
        txtUserIDCP.Text = ""
        txtPwdCP.Text = ""
        txtStatusRemarks.Text = ""
        chkServiceLocationAccess.Checked = False
        chkServiceRecordAccess.Checked = False
        chkInvoiceAccess.Checked = False
        chkCNAccess.Checked = False
        chkDNAccess.Checked = False
        chkReceiptAccess.Checked = False
        chkReportAccess.Checked = False
        chkAdjustmentAccess.Checked = False
        chkViewOpenServiceRecord.Checked = False
        chkSMARTDashBoardAccess.Checked = False
        txtCPRcno.Text = 0
        'chkServiceLocationAccess.Checked = False

        chkChangePasswordonNextLogin.Checked = False
        txtExpiryDate.Text = ""
        chkDashBoardAccess.Checked = False
        chkRequestAccess.Checked = False

        chkSMARTFloorPlanViewAccess.Checked = False
        chkSMARTDailyChartAccess.Checked = False
        chkSMARTMonthlyChartAccess.Checked = False
        chkSMARTWeeklyChartAccess.Checked = False
        chkSMARTYearlyChartAccess.Checked = False

        chkSMARTFloorPlanActivityCountAccess.Checked = False
        chkSMARTFloorPlanExcludeHoursAccess.Checked = False
        chkSMARTDashboardAdvancedInformation.Checked = False

        chkSmartZoneEmailHigh.Checked = False
        chkSmartZoneEmailMedium.Checked = False
        chkSmartDeviceEmailHigh.Checked = False
        chkSmartDeviceEmailMedium.Checked = False

        chkSMARTDailyMovementAccess.Checked = False

    End Sub


    Private Sub DisableCPControls()

        btnSaveCP.Enabled = False
        btnSaveCP.ForeColor = System.Drawing.Color.Gray
        btnCancelCP.Enabled = False
        btnCancelCP.ForeColor = System.Drawing.Color.Gray

        btnAddCP.Enabled = True
        btnAddCP.ForeColor = System.Drawing.Color.Black

        btnEditCP.Enabled = False
        btnEditCP.ForeColor = System.Drawing.Color.Gray

        btnDeleteCP.Enabled = False
        btnDeleteCP.ForeColor = System.Drawing.Color.Gray

        btnResetPwd.Enabled = False
        btnResetPwd.ForeColor = System.Drawing.Color.Gray

        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray


        txtAccountIDCP.Enabled = False
        txtEmailCP.Enabled = False
        txtNameCP.Enabled = False
        txtPwdCP.Enabled = False
        txtUserIDCP.Enabled = False
        chkServiceLocationAccess.Enabled = False
        chkServiceRecordAccess.Enabled = False
        chkInvoiceAccess.Enabled = False
        chkCNAccess.Enabled = False
        chkDNAccess.Enabled = False
        chkReceiptAccess.Enabled = False
        chkReportAccess.Enabled = False
        chkAdjustmentAccess.Enabled = False
        txtStatusRemarks.Enabled = False
        chkChangePasswordonNextLogin.Enabled = False
        'chkStatusCP.Enabled = False
        ddlStatus.Enabled = False
        chkViewOpenServiceRecord.Enabled = False
        txtExpiryDate.Enabled = False
        chkSMARTDashBoardAccess.Enabled = False

        chkDashBoardAccess.Enabled = False
        chkRequestAccess.Enabled = False

        chkSMARTFloorPlanViewAccess.Enabled = False
        chkSMARTDailyChartAccess.Enabled = False
        chkSMARTMonthlyChartAccess.Enabled = False
        chkSMARTWeeklyChartAccess.Enabled = False
        chkSMARTYearlyChartAccess.Enabled = False

        chkSMARTFloorPlanActivityCountAccess.Enabled = False
        chkSMARTFloorPlanExcludeHoursAccess.Enabled = False
        chkSMARTDashboardAdvancedInformation.Enabled = False

        chkSmartZoneEmailHigh.Enabled = False
        chkSmartZoneEmailMedium.Enabled = False
        chkSmartDeviceEmailHigh.Enabled = False
        chkSmartDeviceEmailMedium.Enabled = False

        chkSMARTDailyMovementAccess.Enabled = False
    End Sub

    Private Sub EnableCPControls()

        btnSaveCP.Enabled = True
        btnSaveCP.ForeColor = System.Drawing.Color.Black
        btnCancelCP.Enabled = True
        btnCancelCP.ForeColor = System.Drawing.Color.Black

        btnAddCP.Enabled = False
        btnAddCP.ForeColor = System.Drawing.Color.Gray

        btnEditCP.Enabled = False
        btnEditCP.ForeColor = System.Drawing.Color.Gray

        btnDeleteCP.Enabled = False
        btnDeleteCP.ForeColor = System.Drawing.Color.Gray

        btnResetPwd.Enabled = False
        btnResetPwd.ForeColor = System.Drawing.Color.Gray

        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray

        txtAccountIDCP.Enabled = True
        txtEmailCP.Enabled = True
        txtNameCP.Enabled = True
        'txtPwdCP.Enabled = True
        'txtUserIDCP.Enabled = True
        chkServiceLocationAccess.Enabled = True
        chkServiceRecordAccess.Enabled = True
        chkInvoiceAccess.Enabled = True
        chkCNAccess.Enabled = True
        chkDNAccess.Enabled = True
        chkReceiptAccess.Enabled = True
        chkReportAccess.Enabled = True
        chkAdjustmentAccess.Enabled = True
        txtStatusRemarks.Enabled = True
        chkChangePasswordonNextLogin.Enabled = True
        'chkStatusCP.Enabled = True
        ddlStatus.Enabled = True
        chkViewOpenServiceRecord.Enabled = True
        txtExpiryDate.Enabled = True
        chkSMARTDashBoardAccess.Enabled = True

        chkDashBoardAccess.Enabled = True
        chkRequestAccess.Enabled = True

        chkSMARTFloorPlanViewAccess.Enabled = True
        chkSMARTDailyChartAccess.Enabled = True
        chkSMARTMonthlyChartAccess.Enabled = True
        chkSMARTWeeklyChartAccess.Enabled = True
        chkSMARTYearlyChartAccess.Enabled = True

        chkSMARTFloorPlanActivityCountAccess.Enabled = True
        chkSMARTFloorPlanExcludeHoursAccess.Enabled = True
        chkSMARTDashboardAdvancedInformation.Enabled = True

        chkSmartZoneEmailHigh.Enabled = True
        chkSmartZoneEmailMedium.Enabled = True
        chkSmartDeviceEmailHigh.Enabled = True
        chkSmartDeviceEmailMedium.Enabled = True

        chkSMARTDailyMovementAccess.Enabled = True

    End Sub

    Protected Sub btnAddCP_Click(sender As Object, e As EventArgs) Handles btnAddCP.Click
        'DisableCPControls()
        lblAlert.Text = ""
        lblMessage.Text = ""
        EnableCPControls()
        MakeCPNull()
        lblMessage.Text = "ACTION: ADD USER INFORMATION"
        txtCreatedOn.Text = ""
        txtCPMode.Text = "Add"
        'txtPwdCP.Attributes.Remove("type")

        'txtPwdCP.TextMode = TextBoxMode.SingleLine
        'chkStatusCP.Enabled = False
        ddlStatus.Enabled = False
        chkChangePasswordonNextLogin.Checked = True
        chkChangePasswordonNextLogin.Enabled = False
        txtAccountIDCP.Focus()
    End Sub

    Protected Sub btnEditCP_Click(sender As Object, e As EventArgs) Handles btnEditCP.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            txtCreatedOn.Text = ""
            If txtCPRcno.Text = "" Then
                ' MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"
                Return

            End If

            EnableCPControls()
            txtCPMode.Text = "Edit"
            'If ddlStatus.Text = "O" Then
            '    DisableCPControls()
            '    txtCPMode.Text = "Edit"
            lblMessage.Text = "ACTION: EDIT USER INFORMATION"
            'Else
            '    lblMessage.Text = "ONLY OPEN RECORDS CAN BE EDITED"
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnEditCP_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnDeleteCP_Click(sender As Object, e As EventArgs) Handles btnDeleteCP.Click

        lblMessage.Text = ""
        If txtCPRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE USER INFORMATION"

        Dim confirmValue As String = Request.Form("confirm_value")
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                'Dim command1 As MySqlCommand = New MySqlCommand

                'command1.CommandType = CommandType.Text

                'command1.CommandText = "SELECT * FROM tblcompanycustomeraccess where rcno=" & Convert.ToInt32(txtNotesRcNo.Text)
                'command1.Connection = conn

                'Dim dr As MySqlDataReader = command1.ExecuteReader()
                'Dim dt As New DataTable
                'dt.Load(dr)

                'If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblcustomerportaluser where rcno=" & Convert.ToInt32(txtCPRcno.Text)

                command.CommandText = qry
                command.Connection = conn
                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                'End If
                conn.Close()
                conn.Dispose()
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnDeleteCP_Click", ex.Message.ToString, "")
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            DisableCPControls()

            'SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where Accountid = '" + txtAccountID.Text + "'"
            SqlDSCP.SelectCommand = "SELECT * FROM tblcustomerportaluser order by accountid"
            gvCP.DataSourceID = "SqlDSCP"
            gvCP.DataBind()
            MakeCPNull()
            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

        End If
    End Sub


    Private Sub GenerateUserIdPwd()
        Try
            Dim lPrefix As String
            'Dim lYear As String
            'Dim lMonth As String
            'Dim lInvoiceNo As String
            Dim lSuffixVal As String
            Dim lSuffix As String
            Dim lSetWidth As Integer
            Dim lSetZeroes As String
            Dim lSeparator As String
            Dim strUpdate As String
            Dim lName As String
            lSeparator = "-"
            strUpdate = ""
            lName = ""

            lPrefix = "CP"
            lPrefix = Left(txtNameCP.Text, 2)
            lName = Left(txtNameCP.Text.ToUpper, 2)
            'lName = "U"
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim commandDocControl As MySqlCommand = New MySqlCommand
            commandDocControl.CommandType = CommandType.Text
            commandDocControl.CommandText = "SELECT * FROM tbldoccontrol where prefix='" & lPrefix & "'"
            commandDocControl.Connection = conn

            Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                'Start: Continuous Number
                'If lMonth = "01" Then
                lSuffixVal = dt.Rows(0)("Period01").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period01 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                Dim commandDocControlEdit As MySqlCommand = New MySqlCommand

                commandDocControlEdit.CommandType = CommandType.Text
                commandDocControlEdit.CommandText = strUpdate
                commandDocControlEdit.Connection = conn

                Dim dr2 As MySqlDataReader = commandDocControlEdit.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)
                commandDocControlEdit.Dispose()
                dt2.Dispose()
                dr2.Close()
            Else

                Dim lSuffixVal1 As String


                'lSuffixVal1 = 0


                'If lMonth = "01" Then
                lSuffixVal1 = 1

                'End If

                Dim commandDocControlInsert As MySqlCommand = New MySqlCommand

                commandDocControlInsert.CommandType = CommandType.Text
                'commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                '               "('" & lPrefix & "','M','" & lSeparator & "',6,0,0,0,0,0,0,0,0,0,0,0,0)"

                commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                         "('" & lPrefix & "','M','" & lSeparator & "',6," & lSuffixVal1 & ", 0,0,0,0,0,0,0,0,0,0,0)"

                commandDocControlInsert.Connection = conn

                Dim dr2 As MySqlDataReader = commandDocControlInsert.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                lSetWidth = 6
                lSuffixVal = 1


            End If

            lSetZeroes = ""

            Dim i As Integer
            If lSetWidth > 0 Then
                For i = 1 To lSetWidth - (Len(lSuffixVal))
                    lSetZeroes = lSetZeroes & "0"
                Next i

            End If
            lSuffix = lSetZeroes + lSuffixVal.ToString()
            'gBillNo = lInvoiceNo + lSuffix
            txtUserIDCP.Text = lName + lSuffix


            ''''''''''''''''''''''''
            GeneratePwd()

            'Const charsprefix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            'Const charsnumber As String = "0123456789"

            'Dim pwd As String = ""
            'pwd = RandomString(charsprefix, 7)
            'txtPwdCP.Text = pwd
            'pwd = RandomString(charsnumber, 3)
            'txtPwdCP.Text = txtPwdCP.Text + pwd


            '''''''''''''''''''''''''
            'txtPwdCP.Text = lName + lSuffix

            'txtPwdCP.Text = "aaaaa"
            dt.Dispose()
            commandDocControl.Dispose()
            conn.Close()
            conn.Dispose()
            dr.Close()


        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "FUNCTION GenerateUserIdPwd", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub GeneratePwd()
        Const charsprefix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Const charsnumber As String = "0123456789"

        Dim pwd As String = ""
        pwd = RandomString(charsprefix, 7)
        txtPwdCP.Text = pwd
        pwd = RandomString(charsnumber, 3)
        txtPwdCP.Text = txtPwdCP.Text + pwd

        '''''''''''''''''''''''''''''''''
        'Password encryption
        '''''''''''''''''''''''''''''''''''
        Dim NameEncodein As Byte() = New Byte(txtPwdCP.Text.Length - 1) {}
        NameEncodein = System.Text.Encoding.UTF8.GetBytes(txtPwdCP.Text)
        Dim EcodedName As String = Convert.ToBase64String(NameEncodein)

        '''''''''''''''''''''''''''''''''
        'Password encryption
        '''''''''''''''''''''''''''''''''''
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()


        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry As String = "UPDATE tblcustomerportaluser SET Password = @Password WHERE  rcno=" & Convert.ToInt32(txtCPRcno.Text)

        command.CommandText = qry
        command.Parameters.Clear()
        'command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
        command.Parameters.AddWithValue("@Password", EcodedName)
        command.Connection = conn
        command.ExecuteNonQuery()

        command.Dispose()
        conn.Dispose()
        conn.Close()

    End Sub


    Private Shared random As New Random()
    Public Shared Function RandomString(chars As String, length As Integer) As String
        '    Const chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
        Return New String(Enumerable.Repeat(chars, length).[Select](Function(s) s(Random.[Next](s.Length))).ToArray())
    End Function

    Protected Sub btnSaveCP_Click(sender As Object, e As EventArgs) Handles btnSaveCP.Click
        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtNameCP.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER NAME"
            txtNameCP.Focus()
            Return
        End If

        If String.IsNullOrEmpty(txtEmailCP.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER EMAILL ADDRESS"
            txtEmailCP.Focus()
            Return
        End If

        If txtCPMode.Text = "Add" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                GenerateUserIdPwd()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblcustomerportaluser(AccountID, Status, Name, AccountType, EmailAddress, Password, UserID,  ChangepasswordOnNextLogin, ServiceLocationAccess, ServiceRecordAccess, InvoiceAccess, CreditNoteAccess, DebitNoteAccess,ReceiptAccess, JournalAdjustmentAccess, ReportsAccess, StatusRemarks, ViewOpenServiceRecords, SMARTDashBoardAccess, ExpiryDate,  DashBoardAccess, SMARTFloorPlanViewAccess, SMARTDailyChartAccess, SMARTWeeklyChartAccess, SMARTMonthlyChartAccess, SMARTYearlyChartAccess,   SMARTFloorPlanActivityCountAccess, SMARTFloorPlanExcludeBusinessHoursDataAccess, SMARTDashboardAdvancedInformation,SmartZoneEmailNotificationHigh,SmartZoneEmailNotificationMedium,SmartDeviceEmailNotificationMedium,SmartDeviceEmailNotificationHigh, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn,RequestAccess, SMARTDailyMovementCatchAccess)VALUES(@AccountID, @Status, @Name, @AccountType, @EmailAddress, @Password, @UserID,  @ChangepasswordOnNextLogin,  @ServiceLocationAccess, @ServiceRecordAccess, @InvoiceAccess, @CreditNoteAccess, @DebitNoteAccess, @ReceiptAccess, @JournalAdjustmentAccess, @ReportsAccess, @StatusRemarks, @ViewOpenServiceRecords, @SMARTDashBoardAccess, @ExpiryDate, @DashBoardAccess, @SMARTFloorPlanViewAccess, @SMARTDailyChartAccess, @SMARTWeeklyChartAccess, @SMARTMonthlyChartAccess, @SMARTYearlyChartAccess, @SMARTFloorPlanActivityCountAccess, @SMARTFloorPlanExcludeHoursAccess, @SMARTDashboardAdvancedInformation,@SmartZoneEmailNotificationHigh,@SmartZoneEmailNotificationMedium,@SmartDeviceEmailNotificationMedium,@SmartDeviceEmailNotificationHigh, @CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn,@RequestAccess,@SMARTDailyMovementCatchAccess);"
                command.CommandText = qry
                command.Parameters.Clear()
                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", "")

                    command.Parameters.AddWithValue("@Status", Left(ddlStatus.Text, 1))
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)

                    '''''''''''''''''''''''''''''''''
                    'Password encryption
                    '''''''''''''''''''''''''''''''''''
                    Dim NameEncodein As Byte() = New Byte(txtPwdCP.Text.Length - 1) {}
                    NameEncodein = System.Text.Encoding.UTF8.GetBytes(txtPwdCP.Text)
                    Dim EcodedName As String = Convert.ToBase64String(NameEncodein)

                    '''''''''''''''''''''''''''''''''
                    'Password encryption
                    '''''''''''''''''''''''''''''''''''

                    command.Parameters.AddWithValue("@Password", EcodedName)

                    'command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@AccountType", "")
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@ServiceLocationAccess", chkServiceLocationAccess.Checked)
                    command.Parameters.AddWithValue("@ServiceRecordAccess", chkServiceRecordAccess.Checked)
                    command.Parameters.AddWithValue("@InvoiceAccess", chkInvoiceAccess.Checked)
                    command.Parameters.AddWithValue("@CreditNoteAccess", chkCNAccess.Checked)
                    command.Parameters.AddWithValue("@DebitNoteAccess", chkDNAccess.Checked)
                    command.Parameters.AddWithValue("@ReceiptAccess", chkReceiptAccess.Checked)
                    command.Parameters.AddWithValue("@JournalAdjustmentAccess", chkAdjustmentAccess.Checked)
                    command.Parameters.AddWithValue("@ReportsAccess", chkReportAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashBoardAccess", chkSMARTDashBoardAccess.Checked)

                    If String.IsNullOrEmpty(txtStatusRemarks.Text.ToUpper) = False Then
                        command.Parameters.AddWithValue("@StatusRemarks", txtStatusRemarks.Text.ToUpper)
                    Else
                        command.Parameters.AddWithValue("@StatusRemarks", "")
                    End If

                    command.Parameters.AddWithValue("@ViewOpenServiceRecords", chkViewOpenServiceRecord.Checked)


                    'If txtContractDate.Text.Trim = "" Then
                    '    command.Parameters.AddWithValue("@ContractDate", DBNull.Value)
                    'Else
                    '    command.Parameters.AddWithValue("@ContractDate", Convert.ToDateTime(txtContractDate.Text).ToString("yyyy-MM-dd"))
                    'End If

                    If String.IsNullOrEmpty(txtExpiryDate.Text) = False Then
                        command.Parameters.AddWithValue("@Expirydate", Convert.ToDateTime(txtExpiryDate.Text).ToString("yyyy-MM-dd"))
                    Else
                        command.Parameters.AddWithValue("@Expirydate", DBNull.Value)
                    End If

                    command.Parameters.AddWithValue("@DashBoardAccess", chkDashBoardAccess.Checked)
                    command.Parameters.AddWithValue("@RequestAccess", chkRequestAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanViewAccess", chkSMARTFloorPlanViewAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDailyChartAccess", chkSMARTDailyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTWeeklyChartAccess", chkSMARTMonthlyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTMonthlyChartAccess", chkSMARTWeeklyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTYearlyChartAccess", chkSMARTYearlyChartAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanActivityCountAccess", chkSMARTFloorPlanActivityCountAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTFloorPlanExcludeHoursAccess", chkSMARTFloorPlanExcludeHoursAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashboardAdvancedInformation", chkSMARTDashboardAdvancedInformation.Checked)

                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationHigh", chkSmartZoneEmailHigh.Checked)
                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationMedium", chkSmartZoneEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationMedium", chkSmartDeviceEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationHigh", chkSmartDeviceEmailHigh.Checked)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@SMARTDailyMovementCatchAccess", chkSMARTDailyMovementAccess.Checked)

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", "")
                    'command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Status", Left(ddlStatus.Text, 1))
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text)
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text)
                    command.Parameters.AddWithValue("@AccountType", "")
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@ServiceLocationAccess", chkServiceLocationAccess.Checked)
                    command.Parameters.AddWithValue("@ServiceRecordAccess", chkServiceRecordAccess.Checked)
                    command.Parameters.AddWithValue("@InvoiceAccess", chkInvoiceAccess.Checked)
                    command.Parameters.AddWithValue("@CreditNoteAccess", chkCNAccess.Checked)
                    command.Parameters.AddWithValue("@DebitNoteAccess", chkDNAccess.Checked)
                    command.Parameters.AddWithValue("@ReceiptAccess", chkReceiptAccess.Checked)

                    command.Parameters.AddWithValue("@JournalAdjustmentAccess", chkAdjustmentAccess.Checked)
                    command.Parameters.AddWithValue("@ReportsAccess", chkReportAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashBoardAccess", chkSMARTDashBoardAccess.Checked)

                    If String.IsNullOrEmpty(txtStatusRemarks.Text.ToUpper) = False Then
                        command.Parameters.AddWithValue("@StatusRemarks", txtStatusRemarks.Text)
                    Else
                        command.Parameters.AddWithValue("@StatusRemarks", "")
                    End If

                    command.Parameters.AddWithValue("@ViewOpenServiceRecords", chkViewOpenServiceRecord.Checked)

                    If String.IsNullOrEmpty(txtExpiryDate.Text) = False Then
                        command.Parameters.AddWithValue("@Expirydate", Convert.ToDateTime(txtExpiryDate.Text).ToString("yyyy-MM-dd"))
                    Else
                        command.Parameters.AddWithValue("@Expirydate", DBNull.Value)
                    End If

                    command.Parameters.AddWithValue("@DashBoardAccess", chkDashBoardAccess.Checked)
                    command.Parameters.AddWithValue("@RequestAccess", chkRequestAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanViewAccess", chkSMARTFloorPlanViewAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDailyChartAccess", chkSMARTDailyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTWeeklyChartAccess", chkSMARTMonthlyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTMonthlyChartAccess", chkSMARTWeeklyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTYearlyChartAccess", chkSMARTYearlyChartAccess.Checked)

                    'chkSMARTFloorPlanViewAccess.Enabled = True
                    'chkSMARTDailyChartAccess.Enabled = True
                    'chkSMARTMonthlyAccess.Enabled = True
                    'chkSMARTWeeklyAccess.Enabled = True
                    'chkSMARTYearlyAccess
                    command.Parameters.AddWithValue("@SMARTFloorPlanActivityCountAccess", chkSMARTFloorPlanActivityCountAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTFloorPlanExcludeHoursAccess", chkSMARTFloorPlanExcludeHoursAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashboardAdvancedInformation", chkSMARTDashboardAdvancedInformation.Checked)

                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationHigh", chkSmartZoneEmailHigh.Checked)
                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationMedium", chkSmartZoneEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationMedium", chkSmartDeviceEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationHigh", chkSmartDeviceEmailHigh.Checked)

                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@SMARTDailyMovementCatchAccess", chkSMARTDailyMovementAccess.Checked)

                End If


                command.Connection = conn

                command.ExecuteNonQuery()
                txtCPRcno.Text = command.LastInsertedId

                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: USER INFORMATION ACCESS RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

                'End If
                conn.Close()
                conn.Dispose()

                SendEmail(txtEmailCP.Text, txtNameCP.Text.ToUpper, "ADD")
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountIDCP.Text)
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            EnableCPControls()

        ElseIf txtCPMode.Text = "Edit" Then
            If txtCPRcno.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblcustomerportaluser SET AccountID=@AccountID, Name=@Name, AccountType=@AccountType, Status = @Status, EmailAddress=@EmailAddress,Password = @Password, UserID=@UserID, ChangepasswordOnNextLogin=@ChangepasswordOnNextLogin, ServiceLocationAccess=@ServiceLocationAccess, ServiceRecordAccess=@ServiceRecordAccess, InvoiceAccess=@InvoiceAccess, CreditNoteAccess=@CreditNoteAccess, DebitNoteAccess=@DebitNoteAccess, ReceiptAccess=@ReceiptAccess, ReportsAccess=@ReportsAccess, JournalAdjustmentAccess=@JournalAdjustmentAccess, StatusRemarks=@StatusRemarks, ViewOpenServiceRecords=@ViewOpenServiceRecords, SMARTDashBoardAccess = @SMARTDashBoardAccess, Expirydate=@Expirydate, DashBoardAccess =@DashBoardAccess,  SMARTFloorPlanViewAccess = @SMARTFloorPlanViewAccess, SMARTDailyChartAccess =@SMARTDailyChartAccess, SMARTWeeklyChartAccess =@SMARTWeeklyChartAccess, SMARTMonthlyChartAccess =@SMARTMonthlyChartAccess, SMARTYearlyChartAccess =@SMARTYearlyChartAccess,  SMARTFloorPlanActivityCountAccess =@SMARTFloorPlanActivityCountAccess, SMARTFloorPlanExcludeBusinessHoursDataAccess =@SMARTFloorPlanExcludeHoursAccess, SMARTDashboardAdvancedInformation=@SMARTDashboardAdvancedInformation,SmartZoneEmailNotificationHigh=@SmartZoneEmailNotificationHigh,SmartZoneEmailNotificationMedium=@SmartZoneEmailNotificationMedium,SmartDeviceEmailNotificationMedium=@SmartDeviceEmailNotificationMedium,SmartDeviceEmailNotificationHigh=@SmartDeviceEmailNotificationHigh, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,RequestAccess=@RequestAccess,SMARTDailyMovementCatchAccess=@SMARTDailyMovementCatchAccess WHERE  rcno=" & Convert.ToInt32(txtCPRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", "")
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text.ToUpper)
                    'command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Status", Left(ddlStatus.Text, 1))
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text.ToUpper)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@ServiceLocationAccess", chkServiceLocationAccess.Checked)
                    command.Parameters.AddWithValue("@ServiceRecordAccess", chkServiceRecordAccess.Checked)
                    command.Parameters.AddWithValue("@InvoiceAccess", chkInvoiceAccess.Checked)
                    command.Parameters.AddWithValue("@CreditNoteAccess", chkCNAccess.Checked)
                    command.Parameters.AddWithValue("@DebitNoteAccess", chkDNAccess.Checked)
                    command.Parameters.AddWithValue("@ReceiptAccess", chkReceiptAccess.Checked)

                    command.Parameters.AddWithValue("@JournalAdjustmentAccess", chkAdjustmentAccess.Checked)
                    command.Parameters.AddWithValue("@ReportsAccess", chkReportAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashBoardAccess", chkSMARTDashBoardAccess.Checked)

                    If String.IsNullOrEmpty(txtStatusRemarks.Text.ToUpper) = False Then
                        command.Parameters.AddWithValue("@StatusRemarks", txtStatusRemarks.Text.ToUpper)
                    Else
                        command.Parameters.AddWithValue("@StatusRemarks", "")
                    End If
                    command.Parameters.AddWithValue("@AccountType", "")

                    command.Parameters.AddWithValue("@ViewOpenServiceRecords", chkViewOpenServiceRecord.Checked)

                    If String.IsNullOrEmpty(txtExpiryDate.Text) = False Then
                        command.Parameters.AddWithValue("@Expirydate", Convert.ToDateTime(txtExpiryDate.Text).ToString("yyyy-MM-dd"))
                    Else
                        command.Parameters.AddWithValue("@Expirydate", DBNull.Value)
                    End If

                    command.Parameters.AddWithValue("@DashBoardAccess", chkDashBoardAccess.Checked)
                    command.Parameters.AddWithValue("@RequestAccess", chkRequestAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanViewAccess", chkSMARTFloorPlanViewAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDailyChartAccess", chkSMARTDailyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTWeeklyChartAccess", chkSMARTMonthlyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTMonthlyChartAccess", chkSMARTWeeklyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTYearlyChartAccess", chkSMARTYearlyChartAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanActivityCountAccess", chkSMARTFloorPlanActivityCountAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTFloorPlanExcludeHoursAccess", chkSMARTFloorPlanExcludeHoursAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashboardAdvancedInformation", chkSMARTDashboardAdvancedInformation.Checked)

                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationHigh", chkSmartZoneEmailHigh.Checked)
                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationMedium", chkSmartZoneEmailMedium.Checked)

                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationMedium", chkSmartDeviceEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationHigh", chkSmartDeviceEmailHigh.Checked)

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Parameters.AddWithValue("@SMARTDailyMovementCatchAccess", chkSMARTDailyMovementAccess.Checked)

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", "")
                    command.Parameters.AddWithValue("@Name", txtNameCP.Text)
                    'command.Parameters.AddWithValue("@Status", chkStatusCP.Checked)
                    command.Parameters.AddWithValue("@Status", Left(ddlStatus.Text, 1))
                    command.Parameters.AddWithValue("@EmailAddress", txtEmailCP.Text)
                    command.Parameters.AddWithValue("@Password", txtPwdCP.Text)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCP.Text)
                    command.Parameters.AddWithValue("@ChangepasswordOnNextLogin", chkChangePasswordonNextLogin.Checked)

                    command.Parameters.AddWithValue("@ServiceLocationAccess", chkServiceLocationAccess.Checked)
                    command.Parameters.AddWithValue("@ServiceRecordAccess", chkServiceRecordAccess.Checked)
                    command.Parameters.AddWithValue("@InvoiceAccess", chkInvoiceAccess.Checked)
                    command.Parameters.AddWithValue("@CreditNoteAccess", chkCNAccess.Checked)
                    command.Parameters.AddWithValue("@DebitNoteAccess", chkDNAccess.Checked)
                    command.Parameters.AddWithValue("@ReceiptAccess", chkReceiptAccess.Checked)

                    command.Parameters.AddWithValue("@JournalAdjustmentAccess", chkAdjustmentAccess.Checked)
                    command.Parameters.AddWithValue("@ReportsAccess", chkReportAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashBoardAccess", chkSMARTDashBoardAccess.Checked)

                    If String.IsNullOrEmpty(txtStatusRemarks.Text.ToUpper) = False Then
                        command.Parameters.AddWithValue("@StatusRemarks", txtStatusRemarks.Text)
                    Else
                        command.Parameters.AddWithValue("@StatusRemarks", "")
                    End If
                    command.Parameters.AddWithValue("@AccountType", "")

                    command.Parameters.AddWithValue("@ViewOpenServiceRecords", chkViewOpenServiceRecord.Checked)

                    If String.IsNullOrEmpty(txtExpiryDate.Text) = False Then
                        command.Parameters.AddWithValue("@Expirydate", Convert.ToDateTime(txtExpiryDate.Text).ToString("yyyy-MM-dd"))
                    Else
                        command.Parameters.AddWithValue("@Expirydate", DBNull.Value)
                    End If

                    command.Parameters.AddWithValue("@DashBoardAccess", chkDashBoardAccess.Checked)
                    command.Parameters.AddWithValue("@RequestAccess", chkRequestAccess.Checked)


                    command.Parameters.AddWithValue("@SMARTFloorPlanViewAccess", chkSMARTFloorPlanViewAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDailyChartAccess", chkSMARTDailyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTWeeklyChartAccess", chkSMARTMonthlyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTMonthlyChartAccess", chkSMARTWeeklyChartAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTYearlyChartAccess", chkSMARTYearlyChartAccess.Checked)

                    command.Parameters.AddWithValue("@SMARTFloorPlanActivityCountAccess", chkSMARTFloorPlanActivityCountAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTFloorPlanExcludeHoursAccess", chkSMARTFloorPlanExcludeHoursAccess.Checked)
                    command.Parameters.AddWithValue("@SMARTDashboardAdvancedInformation", chkSMARTDashboardAdvancedInformation.Checked)

                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationHigh", chkSmartZoneEmailHigh.Checked)
                    command.Parameters.AddWithValue("@SmartZoneEmailNotificationMedium", chkSmartZoneEmailMedium.Checked)

                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationMedium", chkSmartDeviceEmailMedium.Checked)
                    command.Parameters.AddWithValue("@SmartDeviceEmailNotificationHigh", chkSmartDeviceEmailHigh.Checked)

                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Parameters.AddWithValue("@SMARTDailyMovementCatchAccess", chkSMARTDailyMovementAccess.Checked)

                End If

                command.Connection = conn

                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: USER INFORMATION ACCESS RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                'End If
                'End If


                txtCPMode.Text = ""

                conn.Close()
                conn.Dispose()
                'command2.Dispose()
                'dt1.Dispose()
                'dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnSaveCP_Click", ex.Message.ToString, txtAccountIDCP.Text)
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            'DisableCPControls()
            'EnableCPControls()
        End If
        'SqlDSCP.SelectCommand = "select * from tblcustomerportaluser where AccountId = '" + txtAccountID.Text + "'"
        '  SqlDSCP.SelectCommand = "select * from tblcustomerportaluser order by accountid"
        SqlDSCP.SelectCommand = txt.Text

        SqlDSCP.DataBind()
        gvCP.DataBind()
        DisableCPControls()
        txtCPMode.Text = ""
    End Sub

    Protected Sub btnCancelCP_Click(sender As Object, e As EventArgs) Handles btnCancelCP.Click
        lblAlert.Text = ""
        MakeCPNull()
        DisableCPControls()
        txtCPMode.Text = ""
    End Sub

    Protected Sub gvCP_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCP.PageIndexChanging
        gvNotesMaster.PageIndex = e.NewPageIndex

        SqlDSCP.SelectCommand = "SELECT * FROM tblcustomerportaluser order by accountid"

        SqlDSCP.DataBind()
        gvNotesMaster.DataBind()
    End Sub

    Protected Sub gvCP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCP.SelectedIndexChanged
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            MakeCPNull()
            '  txtTechMode.Text = "Edit"
            Dim editindex As Integer = gvCP.SelectedIndex
            rcno = DirectCast(gvCP.Rows(editindex).FindControl("Label1"), Label).Text
            txtCPRcno.Text = rcno.ToString()

            ' InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "gvCP_SelectedIndexChanged", editindex.ToString, txtCPRcno.Text)
            'Start: 12.02.20
            Dim sql As String
            sql = ""
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            sql = "Select * "
            sql = sql + " FROM tblcustomerportaluser "
            sql = sql + "where rcno = " & Convert.ToInt64(txtCPRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)
            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("AccountID").ToString <> "" Then : txtAccountIDCP.Text = dt.Rows(0)("AccountID").ToString : End If
                If dt.Rows(0)("Name").ToString <> "" Then : txtNameCP.Text = dt.Rows(0)("Name").ToString : End If

                If dt.Rows(0)("EmailAddress").ToString <> "" Then : txtEmailCP.Text = dt.Rows(0)("EmailAddress").ToString : End If
                If dt.Rows(0)("UserID").ToString <> "" Then : txtUserIDCP.Text = dt.Rows(0)("UserID").ToString : End If
                txtAccountIDCPReset.Text = txtUserIDCP.Text & " - " & txtNameCP.Text

                If dt.Rows(0)("Status").ToString = "A" Then
                    ddlStatus.SelectedIndex = 0
                    btnResetPwd.Enabled = True
                    btnResetPwd.ForeColor = System.Drawing.Color.Black
                ElseIf dt.Rows(0)("Status").ToString = "I" Then
                    ddlStatus.SelectedIndex = 1
                    btnResetPwd.Enabled = False
                    btnResetPwd.ForeColor = System.Drawing.Color.Gray
                ElseIf dt.Rows(0)("Status").ToString = "D" Then
                    ddlStatus.SelectedIndex = 2
                    btnResetPwd.Enabled = False
                    btnResetPwd.ForeColor = System.Drawing.Color.Gray
                End If


                If dt.Rows(0)("StatusRemarks").ToString = "&nbsp;" Then
                    txtStatusRemarks.Text = ""
                Else
                    txtStatusRemarks.Text = Server.HtmlDecode(dt.Rows(0)("StatusRemarks").ToString)
                End If

                If dt.Rows(0)("ExpiryDate").ToString = "&nbsp;" Then
                    txtExpiryDate.Text = ""
                Else
                    txtExpiryDate.Text = Server.HtmlDecode(dt.Rows(0)("ExpiryDate").ToString)
                End If

                chkSMARTDashboardAdvancedInformation.Checked = dt.Rows(0)("SMARTDashboardAdvancedInformation").ToString
                chkDashBoardAccess.Checked = dt.Rows(0)("DashboardAccess").ToString
                chkRequestAccess.Checked = dt.Rows(0)("RequestAccess").ToString
                chkSMARTDashBoardAccess.Checked = dt.Rows(0)("SMARTDashboardAccess").ToString
                chkSMARTFloorPlanViewAccess.Checked = dt.Rows(0)("SMARTFloorPlanViewAccess").ToString
                chkSMARTFloorPlanActivityCountAccess.Checked = dt.Rows(0)("SMARTFloorPlanActivityCountAccess").ToString
                chkSMARTFloorPlanExcludeHoursAccess.Checked = dt.Rows(0)("SMARTFloorPlanExcludeBusinessHoursDataAccess").ToString

                chkSMARTDailyChartAccess.Checked = dt.Rows(0)("SMARTDailyChartAccess").ToString
                chkSMARTWeeklyChartAccess.Checked = dt.Rows(0)("SMARTWeeklyChartAccess").ToString
                chkSMARTMonthlyChartAccess.Checked = dt.Rows(0)("SMARTMonthlyChartAccess").ToString
                chkSMARTYearlyChartAccess.Checked = dt.Rows(0)("SMARTYearlyChartAccess").ToString

                chkSmartZoneEmailHigh.Checked = dt.Rows(0)("SmartZoneEmailNotificationHigh").ToString
                chkSmartZoneEmailMedium.Checked = dt.Rows(0)("SmartZoneEmailNotificationMedium").ToString
                chkSmartDeviceEmailMedium.Checked = dt.Rows(0)("SmartDeviceEmailNotificationMedium").ToString
                chkSmartDeviceEmailHigh.Checked = dt.Rows(0)("SmartDeviceEmailNotificationHigh").ToString

                chkServiceRecordAccess.Checked = dt.Rows(0)("ServiceRecordAccess").ToString
                chkViewOpenServiceRecord.Checked = dt.Rows(0)("ViewOpenServiceRecords").ToString

                chkInvoiceAccess.Checked = dt.Rows(0)("InvoiceAccess").ToString
                chkCNAccess.Checked = dt.Rows(0)("CreditNoteAccess").ToString
                chkDNAccess.Checked = dt.Rows(0)("DebitNoteAccess").ToString
                chkReceiptAccess.Checked = dt.Rows(0)("ReceiptAccess").ToString
                chkAdjustmentAccess.Checked = dt.Rows(0)("JournalAdjustmentAccess").ToString
                chkReportAccess.Checked = dt.Rows(0)("ReportsAccess").ToString

                If dt.Rows(0)("AccountType").ToString = "&nbsp;" Then
                    txtAccountTypeCP.Text = ""
                Else
                    txtAccountTypeCP.Text = dt.Rows(0)("AccountType").ToString
                End If

                txtPwdCP.Text = dt.Rows(0)("Password").ToString
                chkChangePasswordonNextLogin.Checked = dt.Rows(0)("ChangePasswordonNextLogin").ToString

                chkSMARTDailyMovementAccess.Checked = dt.Rows(0)("SMARTDailyMovementCatchAccess").ToString
            End If
            CountUserAccess(conn)


            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()

            dr.Close()
            'End: 12.03.20

            ''txtStatusCP.Text = gvCP.SelectedRow.Cells(2).Text
            'txtAccountIDCP.Text = gvCP.SelectedRow.Cells(1).Text
            'txtNameCP.Text = Server.HtmlDecode(gvCP.SelectedRow.Cells(2).Text)

            'txtAccountIDCPReset.Text = gvCP.SelectedRow.Cells(4).Text & " - " & gvCP.SelectedRow.Cells(2).Text
            ''txtNameCPReset.Text = gvCP.SelectedRow.Cells(2).Text

            'txtEmailCP.Text = gvCP.SelectedRow.Cells(3).Text
            'txtUserIDCP.Text = gvCP.SelectedRow.Cells(4).Text
            ''chkStatusCP.Checked = gvCP.SelectedRow.Cells(5).Text

            'If gvCP.SelectedRow.Cells(5).Text = "A" Then
            '    ddlStatus.SelectedIndex = 0
            'ElseIf gvCP.SelectedRow.Cells(5).Text = "I" Then
            '    ddlStatus.SelectedIndex = 1
            'ElseIf gvCP.SelectedRow.Cells(5).Text = "D" Then
            '    ddlStatus.SelectedIndex = 2
            'End If


            'If gvCP.SelectedRow.Cells(6).Text = "&nbsp;" Then
            '    txtStatusRemarks.Text = ""
            'Else
            '    txtStatusRemarks.Text = Server.HtmlDecode(gvCP.SelectedRow.Cells(6).Text)
            'End If

            'If String.IsNullOrEmpty(gvCP.SelectedRow.Cells(7).Text) = True Or gvCP.SelectedRow.Cells(7).Text = "&nbsp;" Then
            '    txtExpiryDate.Text = ""
            'Else
            '    txtExpiryDate.Text = Convert.ToDateTime(gvCP.SelectedRow.Cells(7).Text).ToString("dd/MM/yyyy")
            'End If


            'chkDashBoardAccess.Checked = gvCP.SelectedRow.Cells(8).Text

            'chkSMARTDashBoardAccess.Checked = gvCP.SelectedRow.Cells(9).Text

            'chkSMARTFloorPlanViewAccess.Checked = gvCP.SelectedRow.Cells(10).Text


            'chkSMARTFloorPlanActivityCountAccess.Checked = gvCP.SelectedRow.Cells(11).Text
            'chkSMARTFloorPlanExcludeHoursAccess.Checked = gvCP.SelectedRow.Cells(12).Text


            'chkSMARTDailyChartAccess.Checked = gvCP.SelectedRow.Cells(13).Text
            'chkSMARTWeeklyChartAccess.Checked = gvCP.SelectedRow.Cells(14).Text
            'chkSMARTMonthlyChartAccess.Checked = gvCP.SelectedRow.Cells(15).Text
            'chkSMARTYearlyChartAccess.Checked = gvCP.SelectedRow.Cells(16).Text


            'chkServiceRecordAccess.Checked = gvCP.SelectedRow.Cells(17).Text
            'chkViewOpenServiceRecord.Checked = gvCP.SelectedRow.Cells(18).Text

            'chkInvoiceAccess.Checked = gvCP.SelectedRow.Cells(19).Text
            'chkCNAccess.Checked = gvCP.SelectedRow.Cells(20).Text
            'chkDNAccess.Checked = gvCP.SelectedRow.Cells(21).Text
            'chkReceiptAccess.Checked = gvCP.SelectedRow.Cells(22).Text
            'chkAdjustmentAccess.Checked = gvCP.SelectedRow.Cells(23).Text
            'chkReportAccess.Checked = gvCP.SelectedRow.Cells(24).Text

            'If gvCP.SelectedRow.Cells(30).Text = "&nbsp;" Then
            '    txtAccountTypeCP.Text = ""
            'Else
            '    txtAccountTypeCP.Text = gvCP.SelectedRow.Cells(30).Text
            'End If

            'txtPwdCP.Text = gvCP.SelectedRow.Cells(32).Text
            'chkChangePasswordonNextLogin.Checked = gvCP.SelectedRow.Cells(33).Text


            ''txtPwdCP.Attributes.Add("type", "password")
            btnEditCP.Enabled = True
            btnEditCP.ForeColor = System.Drawing.Color.Black

            'btnDeleteCP.Enabled = True
            'btnDeleteCP.ForeColor = System.Drawing.Color.Black
            ''txtPwdCP.TextMode = TextBoxMode.Password

            ''If gvCP.SelectedRow.Cells(5).Text = "A" Then
            ''    btnResetPwd.Enabled = True
            ''    btnResetPwd.ForeColor = System.Drawing.Color.Black
            ''Else
            ''    btnResetPwd.Enabled = False
            ''    btnResetPwd.ForeColor = System.Drawing.Color.Gray
            ''End If

            btnChangeStatus.Enabled = True
            btnChangeStatus.ForeColor = System.Drawing.Color.Black

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "gvCP_SelectedIndexChanged", ex.Message.ToString, txtAccountIDCP.Text)
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub


    Protected Sub btnSaveUserAccess_Click(sender As Object, e As EventArgs) Handles btnSaveUserAccess.Click

        If String.IsNullOrEmpty(txtAccountIDCPUserAccess.Text) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER ACCOUNT ID"
            txtAccountIDCPUserAccess.Focus()
            txtCreatedOn.Text = ""
            Return
        End If
        If (ddlLocationAccessType.SelectedIndex = 0) Then
            ' MessageBox.Message.Alert(Page, "Select Staff to proceed!!", "str")
            lblAlert.Text = "ENTER LOCATION ACCESS TYPE"
            txtCreatedOn.Text = ""
            Return
        End If

        If txtCPModeUserAccess.Text = "Add" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                '''''''''''''''''''''
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT * FROM tblcustomerportaluseraccess where Userid = '" & txtUserIDCPUserAccess.Text & "' and AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()
                Dim dt As New System.Data.DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    lblAlert.Text = "ACCOUNT ID ALREADY EXISTS FOR THIS USER ID"
                    txtCreatedOn.Text = ""
                    Exit Sub
                End If

                '''''''''''''''''''''''

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "INSERT INTO tblCustomerPortalUserAccess(AccountID,  UserID, AccountType, LocationAccessType, AccountName, UserName, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @UserID, @AccountType, @LocationAccessType,  @AccountName, @UserName, @CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                command.CommandText = qry
                command.Parameters.Clear()
                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                    command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@AccountName", txtAccountNameUserAccess.Text)
                    command.Parameters.AddWithValue("@UserName", txtUserNameCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@LocationAccessType", ddlLocationAccessType.Text.ToUpper)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                    command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text)
                    command.Parameters.AddWithValue("@AccountName", txtAccountNameUserAccess.Text)
                    command.Parameters.AddWithValue("@UserName", txtUserNameCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@LocationAccessType", ddlLocationAccessType.Text.ToUpper)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                End If


                command.Connection = conn

                command.ExecuteNonQuery()
                txtCPUserAccessRcno.Text = command.LastInsertedId


                '''''''''''''''''''''''''''Start: Location for All Location

                If ddlLocationAccessType.Text = "ALL LOCATIONS" Then

                    ''''''''''''''''''''''''

                    Dim command3 As MySqlCommand = New MySqlCommand

                    command3.CommandType = CommandType.Text

                    If txtAccountTypeCPUserAccess.Text = "CORPORATE" Then
                        command3.CommandText = "select LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal from tblCompanyLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"

                    Else
                        command3.CommandText = "select LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal from tblPersonLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"

                    End If
                    command3.Connection = conn

                    Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                    Dim dt3 As New DataTable
                    dt3.Load(dr3)

                    If dt3.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt3.Rows.Count - 1

                            ''''''''''''''''''''''''''''''
                            Dim commandLocation As MySqlCommand = New MySqlCommand
                            commandLocation.CommandType = CommandType.Text


                            Dim qryLocation As String = "INSERT INTO tblcustomerportaluseraccesslocation(AccountID,  UserID, AccountType, LocationID, Address1,  AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal, ServiceName, Selected, CreatedOn, CreatedBy,LastModifiedBy, LastModifiedOn)VALUES(@AccountID, @UserID, @AccountType, @LocationID, @Address1, @AddBuilding, @AddStreet, @AddState, @AddCity, @AddCountry, @AddPostal, @ServiceName, @Selected, @CreatedOn, @CreatedBy,@LastModifiedBy, @LastModifiedOn);"
                            commandLocation.CommandText = qryLocation
                            commandLocation.Parameters.Clear()
                            If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                                commandLocation.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                                commandLocation.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@LocationID", dt3.Rows(i)("LocationID").ToUpper)
                                commandLocation.Parameters.AddWithValue("@Address1", dt3.Rows(i)("Address1").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddBuilding", dt3.Rows(i)("AddBuilding").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddStreet", dt3.Rows(i)("AddStreet").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddState", dt3.Rows(i)("AddState").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddCity", dt3.Rows(i)("AddCity").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddCountry", dt3.Rows(i)("AddCountry").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddPostal", dt3.Rows(i)("AddPostal").ToUpper)

                                commandLocation.Parameters.AddWithValue("@ServiceName", dt3.Rows(i)("ServiceName").ToUpper)
                                commandLocation.Parameters.AddWithValue("@Selected", True)

                                commandLocation.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandLocation.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                            ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                                commandLocation.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                                commandLocation.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text)
                                commandLocation.Parameters.AddWithValue("@LocationID", dt3.Rows(i)("LocationID"))
                                commandLocation.Parameters.AddWithValue("@Address1", dt3.Rows(i)("Address1"))
                                commandLocation.Parameters.AddWithValue("@AddBuilding", dt3.Rows(i)("AddBuilding"))
                                commandLocation.Parameters.AddWithValue("@AddStreet", dt3.Rows(i)("AddStreet"))
                                commandLocation.Parameters.AddWithValue("@AddState", dt3.Rows(i)("AddState"))
                                commandLocation.Parameters.AddWithValue("@AddCity", dt3.Rows(i)("AddCity"))
                                commandLocation.Parameters.AddWithValue("@AddCountry", dt3.Rows(i)("AddCountry"))
                                commandLocation.Parameters.AddWithValue("@AddPostal", dt3.Rows(i)("AddPostal"))

                                commandLocation.Parameters.AddWithValue("@ServiceName", dt3.Rows(i)("ServiceName"))
                                commandLocation.Parameters.AddWithValue("@Selected", True)

                                commandLocation.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text)
                                commandLocation.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                                commandLocation.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandLocation.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                                commandLocation.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                            End If

                            commandLocation.Connection = conn
                            commandLocation.ExecuteNonQuery()
                            btnLocations.Text = "LOCATIONS [" & i + 1 & " of " & i + 1 & "]"
                        Next

                    End If

                End If


                '''''''''''''''''''''''''''End: Locations for All Locations


                '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                lblMessage.Text = "ADD: USER ACCESS RECORD SUCCESSFULLY ADDED"
                lblAlert.Text = ""

                'End If
                conn.Close()
                conn.Dispose()
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnSaveUserAccess_Click", ex.Message.ToString, txtAccountIDCPUserAccess.Text)
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try
            'EnableCPControls()

        ElseIf txtCPModeUserAccess.Text = "Edit" Then
            If txtCPRcno.Text = "" Then
                '   MessageBox.Message.Alert(Page, "Select a record to edit!!!", "str")
                lblAlert.Text = "SELECT RECORD TO EDIT"

                Return

            End If
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()


                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblCustomerPortalUserAccess SET AccountID=@AccountID, UserID=@UserID, AccountType=@AccountType,AccountName=@AccountName, UserName=@UserName, LocationAccessType=@LocationAccessType, LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE  rcno=" & Convert.ToInt32(txtCPUserAccessRcno.Text)

                command.CommandText = qry
                command.Parameters.Clear()

                If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text)

                    command.Parameters.AddWithValue("@AccountName", txtAccountNameUserAccess.Text)
                    command.Parameters.AddWithValue("@UserName", txtUserNameCPUserAccess.Text.ToUpper)

                    command.Parameters.AddWithValue("@LocationAccessType", ddlLocationAccessType.Text.ToUpper)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                    command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                    command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text)

                    command.Parameters.AddWithValue("@AccountName", txtAccountNameUserAccess.Text)
                    command.Parameters.AddWithValue("@UserName", txtUserNameCPUserAccess.Text.ToUpper)

                    command.Parameters.AddWithValue("@LocationAccessType", ddlLocationAccessType.Text.ToUpper)
                    command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text)
                    command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                End If

                command.Connection = conn

                command.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: USER ACCESS RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""
                'End If
                'End If


                txtCPMode.Text = ""

                conn.Close()
                conn.Dispose()
                'command2.Dispose()
                'dt1.Dispose()
                'dr1.Close()

            Catch ex As Exception
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + txtCreatedBy.Text, "btnSaveUserAccess_Click", ex.Message.ToString, txtAccountIDCPUserAccess.Text)
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            End Try

            'EnableCPControls()
        End If
        'SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where AccountId = '" + txtAccountID.Text + "'"

        'SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
        'gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
        'gvNotesMaster.DataBind()


        SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
        gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
        gvNotesMaster.DataBind()

        DisableUserAccessControls()

        btnLocations.Enabled = True
        btnLocations.ForeColor = System.Drawing.Color.Black

        btnEditCPUserAccess.Enabled = True
        btnEditCPUserAccess.ForeColor = System.Drawing.Color.Black

        btnDeleteCPUserAccess.Enabled = True
        btnDeleteCPUserAccess.ForeColor = System.Drawing.Color.Black

        'If ddlLocationAccessType.Text = "DEFINED LOCATIONS" Then
        '    btnLocations.Enabled = True
        '    btnLocations.ForeColor = System.Drawing.Color.Black
        'End If
        txtCPModeUserAccess.Text = ""
    End Sub

    Protected Sub btnLocation_Click(sender As Object, e As EventArgs)
        'Try
        '    txtCreatedOn.Text = ""
        '    'gvCPUserAccess_SelectedIndexChanged(sender, e)

        '    Dim rowindex As Int64 = 0

        '    Dim row As GridViewRow = CType((TryCast(sender, Control)).Parent.Parent, GridViewRow)
        '    Dim lblId As Label = CType(row.FindControl("label10"), Label)
        '    'Dim lblRutaCompleta As Label = CType(row.FindControl("lblRutaCompleta"), Label)

        '    rowindex = Convert.ToInt64(lblId.Text.ToString())

        '    rowindex = GVCPUserAccess.SelectedIndex
        '    Dim TextBoxlocationButton As Button = CType(GVCPUserAccess.Rows(rowindex).Cells(0).FindControl("BtnLocation"), Button)

        '    sqlDSLocations.SelectCommand = "SELECT Rcno, LocationID, ServiceName, Address1 FROM tblCompanyLocation where AccountID = '" & txtAccountIDCPUserAccess.Text & "' order by LocationID"
        '    grvServiceRecDetails.DataSourceID = "SqlDSLocations"
        '    grvServiceRecDetails.DataBind()

        '    mdlPopupLocation.Show()


        'Catch ex As Exception
        '    Dim exstr As String
        '    exstr = ex.Message.ToString
        '    lblAlert.Text = exstr
        '    InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "btnContractNoGV_Click", ex.Message.ToString, "")
        'End Try

    End Sub


    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged
        lblAlert.Text = ""

        If tb1.ActiveTabIndex = 0 Then
            '  SqlDSCP.SelectCommand = "SELECT * FROM tblcustomerportaluser  order by accountid"
            SqlDSCP.SelectCommand = txt.Text
            gvCP.DataSourceID = "SqlDSCP"
            gvCP.DataBind()
        End If



        If tb1.ActiveTabIndex = 1 Then
            If txtCPMode.Text = "Add" Or txtCPMode.Text = "Edit" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtUserIDCP.Text) = True Then
                lblAlert.Text = "Please Select a User first"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            DisableUserAccessControls()
            txtUserIDCPUserAccess.Text = txtUserIDCP.Text
            txtUserNameCPUserAccess.Text = txtNameCP.Text

            SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
            gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
            gvNotesMaster.DataBind()

            If gvNotesMaster.Rows.Count > 0 Then
                gvNotesMaster.SelectedIndex = 0
                gvNotesMaster_SelectedIndexChanged(sender, e)
            Else

                btnLocations.Text = "LOCATIONS"
          
            End If
           
        End If


        If tb1.ActiveTabIndex = 0 Then
            If txtCPModeUserAccess.Text = "Add" Or txtCPModeUserAccess.Text = "Edit" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                tb1.ActiveTabIndex = 1
                Exit Sub
            End If

          

          
        End If
    End Sub

    Protected Sub btnAddCPUserAccess_Click(sender As Object, e As EventArgs) Handles btnAddCPUserAccess.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        txtCreatedOn.Text = ""
        txtCPModeUserAccess.Text = "Add"
        lblMessage.Text = "ACTION: ADD USER ACCESS"
        EnableUserAccessControls()
        MakeUserAccessNull()
    End Sub

    Protected Sub btnSaveLocation_Click(sender As Object, e As EventArgs) Handles btnSaveLocation.Click
        Try
            Dim totalRows As Long
            totalRows = 0

            For rowIndex1 As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
                If (TextBoxchkSelect1.Checked = True) Then
                    totalRows = totalRows + 1
                    GoTo insertRec2
                End If
            Next rowIndex1
            'End If


            If totalRows = 0 Then
                mdlPopupLocation.Show()
                Dim message As String = "alert('PLEASE SELECT A RECORD')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                Exit Sub
            End If

insertRec2:
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            'txtCreatedOn.Text = ""


            ''''''''''''''
            Dim ctrChecked As Integer
            ctrChecked = 0

            Dim commandDel As MySqlCommand = New MySqlCommand

            commandDel.CommandType = CommandType.Text
            Dim qryDel As String = "Delete from tblcustomerportaluseraccesslocation where userid ='" & txtUserIDCPUserAccess.Text & "' and Accountid ='" & txtAccountIDCPUserAccess.Text & "'"
            commandDel.CommandText = qryDel
            commandDel.Parameters.Clear()

            commandDel.Connection = conn

            commandDel.ExecuteNonQuery()

            '''''''''''''''
            Dim rowselected As Integer
            rowselected = 0

            'rowselected = grvBillingDetails.Rows.Count - 1 '26.10.17

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim lblidLocationID As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                Dim lblidContractGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractGroupGV"), TextBox)

                Dim lblidServiceName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtClientNameGV"), TextBox)
                Dim lblidAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceAddressGV"), TextBox)
                Dim lblidBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBuildingGV"), TextBox)
                Dim lblidStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtStreetGV"), TextBox)
                Dim lblidCity As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCityGV"), TextBox)
                Dim lblidState As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtStateGV"), TextBox)
                Dim lblidCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCountryGV"), TextBox)
                Dim lblidPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtPostalGV"), TextBox)
                

                If (TextBoxchkSelect.Checked = True) Then
                    ctrChecked = ctrChecked + 1
                    'If txtCPModeUserAccess.Text = "Add" Then
                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblcustomerportaluseraccesslocation(AccountID,  UserID, AccountType, LocationID, Address1, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, ServiceName, Selected, ContractGroup, CreatedOn, CreatedBy,LastModifiedBy, LastModifiedOn)VALUES(@AccountID, @UserID, @AccountType, @LocationID, @Address1, @AddStreet, @AddBuilding, @AddCity, @AddState, @AddCountry, @AddPostal, @ServiceName, @Selected, @ContractGroup, @CreatedOn, @CreatedBy,@LastModifiedBy, @LastModifiedOn);"
                    command.CommandText = qry
                    command.Parameters.Clear()
                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                        command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text.ToUpper)
                        command.Parameters.AddWithValue("@LocationID", lblidLocationID.Text.ToUpper)

                        If String.IsNullOrEmpty(lblidAddress1.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@Address1", lblidAddress1.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@Address1", "")
                        End If


                        If String.IsNullOrEmpty(lblidStreet.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddStreet", lblidStreet.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddStreet", "")
                        End If

                        If String.IsNullOrEmpty(lblidBuilding.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddBuilding", lblidBuilding.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddBuilding", "")
                        End If

                        If String.IsNullOrEmpty(lblidCity.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddCity", lblidCity.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddCity", "")
                        End If

                        If String.IsNullOrEmpty(lblidState.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddState", lblidState.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddState", "")
                        End If

                        If String.IsNullOrEmpty(lblidCountry.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddCountry", lblidCountry.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddCountry", "")
                        End If

                        If String.IsNullOrEmpty(lblidPostal.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddPostal", lblidPostal.Text.ToUpper)
                        Else
                            command.Parameters.AddWithValue("@AddPostal", "")
                        End If

                        'command.Parameters.AddWithValue("@AddStreet", lblidStreet.Text.ToUpper)
                        'command.Parameters.AddWithValue("@AddBuilding", lblidBuilding.Text.ToUpper)
                        'command.Parameters.AddWithValue("@AddCity", lblidCity.Text.ToUpper)
                        'command.Parameters.AddWithValue("@AddState", lblidState.Text.ToUpper)
                        'command.Parameters.AddWithValue("@AddCountry", lblidCountry.Text.ToUpper)
                        'command.Parameters.AddWithValue("@AddPostal", lblidPostal.Text.ToUpper)

                        command.Parameters.AddWithValue("@ServiceName", lblidServiceName.Text.ToUpper)
                        command.Parameters.AddWithValue("@Selected", True)
                        command.Parameters.AddWithValue("@ContractGroup", lblidContractGroup.Text.ToUpper)

                        command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then
                        command.Parameters.AddWithValue("@AccountID", txtAccountIDCPUserAccess.Text)
                        command.Parameters.AddWithValue("@AccountType", txtAccountTypeCPUserAccess.Text)
                        command.Parameters.AddWithValue("@LocationID", lblidLocationID.Text)

                        If String.IsNullOrEmpty(lblidAddress1.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@Address1", lblidAddress1.Text)
                        Else
                            command.Parameters.AddWithValue("@Address1", "")
                        End If


                        If String.IsNullOrEmpty(lblidStreet.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddStreet", lblidStreet.Text)
                        Else
                            command.Parameters.AddWithValue("@AddStreet", "")
                        End If

                        If String.IsNullOrEmpty(lblidBuilding.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddBuilding", lblidBuilding.Text)
                        Else
                            command.Parameters.AddWithValue("@AddBuilding", "")
                        End If

                        If String.IsNullOrEmpty(lblidCity.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddCity", lblidCity.Text)
                        Else
                            command.Parameters.AddWithValue("@AddCity", "")
                        End If

                        If String.IsNullOrEmpty(lblidState.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddState", lblidState.Text)
                        Else
                            command.Parameters.AddWithValue("@AddState", "")
                        End If

                        If String.IsNullOrEmpty(lblidCountry.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddCountry", lblidCountry.Text)
                        Else
                            command.Parameters.AddWithValue("@AddCountry", "")
                        End If

                        If String.IsNullOrEmpty(lblidPostal.Text.Trim) = False Then
                            command.Parameters.AddWithValue("@AddPostal", lblidPostal.Text)
                        Else
                            command.Parameters.AddWithValue("@AddPostal", "")
                        End If

                     
                        command.Parameters.AddWithValue("@ServiceName", lblidServiceName.Text)
                        command.Parameters.AddWithValue("@Selected", True)
                        command.Parameters.AddWithValue("@ContractGroup", lblidContractGroup.Text.ToUpper)

                        command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    End If

                        command.Connection = conn

                        command.ExecuteNonQuery()
                        txtCPRcno.Text = command.LastInsertedId

                        '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
                        lblMessage.Text = "ADD: LOCATION ACCESS RECORD SUCCESSFULLY ADDED"
                        lblAlert.Text = ""

                    'EnableCPControls()

                    End If
            Next

            If ctrChecked = grvServiceRecDetails.Rows.Count Then

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblcustomerportaluseraccess Set LocationAccessType ='ALL LOCATIONS' where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                command1.CommandText = qry
                command1.Parameters.Clear()
                command1.Connection = conn
                command1.ExecuteNonQuery()
                ddlLocationAccessType.Text = "ALL LOCATIONS"
            Else
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                Dim qry As String = "UPDATE tblcustomerportaluseraccess Set LocationAccessType ='DEFINED LOCATIONS' where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                command1.CommandText = qry
                command1.Parameters.Clear()
                command1.Connection = conn
                command1.ExecuteNonQuery()
                ddlLocationAccessType.Text = "DEFINED LOCATIONS"
            End If

            conn.Close()
            conn.Dispose()
            'SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where AccountId = '" + txtAccountID.Text + "'"
            'SqlDSCP.DataBind()
            'gvCP.DataBind()

            SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
            gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
            gvNotesMaster.DataBind()

            'SqlDSCPUserAccess.DataBind()
            'gvNotesMaster.DataBind()
            txtCPModeUserAccess.Text = ""
            txtCPMode.Text = ""

            btnLocations.Text = "LOCATIONS [" & ctrChecked & " of " & grvServiceRecDetails.Rows.Count.ToString & "]"
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnSaveLocation_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        gvClient.PageIndex = e.NewPageIndex

        ''If txtClientFrom.Text = "ImportService" Then
        'SqlDSClient.SelectCommand = txtImportService.Text
        ''End If

        'If txtSearch.Text = "CustomerSearch" Then
        SqlDSClient.SelectCommand = txtCustomerSearch.Text

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopupAccountID.Show()
    End Sub

    Protected Sub OnRowDataBoundgClient(sender As Object, e As GridViewRowEventArgs) Handles gvClient.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvClient, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        For Each row As GridViewRow In gvClient.Rows
            If row.RowIndex = gvClient.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

  

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        Try
            '    If txtSearch.Text = "InvoiceSearch" Then
            '        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                'ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                'Else
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                'End If
            Else
                'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                'ElseIf txtAccountTypeCPUserAccess.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                'Else
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'RESIDENTIAL' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                'End If



                If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '')  order by Name"
                ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,  C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') order by Name"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') order by Name"
                End If
            End If
            txtCustomerSearch.Text = SqlDSClient.SelectCommand

            mdlPopupAccountID.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        Try
            If String.IsNullOrEmpty(txtAccountIDCPUserAccess.Text.Trim) = False Then
                txtPopUpClient.Text = ""
                txtPopUpClient.Text = txtAccountIDCPUserAccess.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text


                'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%'  or B.CompanyID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                'ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.PersonID  like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                'Else
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.CompanyID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.PersonID  like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                'End If

                If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson  From tblCompany A where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '')  and (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or A.accountid like '" + txtPopupClientSearch.Text + "%'  or A.contactperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
                ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType, C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C where  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
                End If
                txtCustomerSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            Else
                txtPopUpClient.Text = ""


                'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.CompanyID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                'ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.PersonID  like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                'Else
                '    'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.CompanyID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.PersonID  like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                'End If

                If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '')  order by Name"
                ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,  C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') order by Name"
                Else
                    'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.CompanyID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.PersonID  like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') order by Name"
                End If


                txtCustomerSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            End If
            'txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            mdlPopupAccountID.Show()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnClientSearch_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter client name", "str")
            Exit Sub
        End If

        Try

            txtPopupClientSearch.Text = txtPopUpClient.Text.Trim.ToUpper
            'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
            '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
            'ElseIf txtAccountTypeCPUserAccess.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
            '    SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
            'Else
            '    SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (D.Accountid is not null and D.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
            'End If

            If txtAccountTypeCPUserAccess.Text = "CORPORATE" Or txtAccountTypeCPUserAccess.Text = "COMPANY" Then
                SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType, A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson  From tblCompany A where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '')  and (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or A.accountid like '" + txtPopupClientSearch.Text + "%'  or A.contactperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
            ElseIf txtAccountTypeCPUserAccess.SelectedItem.Text = "RESIDENTIAL" Or txtAccountTypeCPUserAccess.Text = "PERSON" Then
                SqlDSClient.SelectCommand = "SELECT 'RESIDENTIAL' as AccountType, C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C where  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
            Else
                SqlDSClient.SelectCommand = "SELECT 'CORPORATE' as AccountType,  A.CompanyGroup, A.AccountID, A.Name,  A.Address1, A.AddStreet, A.AddBuilding, A.AddCity, A.AddState, A.AddCountry, A.AddPostal, A.ContactPerson From tblCompany A  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and (upper(A.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or A.accountid like '" + txtPopupClientSearch.Text + "%' or A.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'RESIDENTIAL' as AccountType,   C.PersonGroup as CompanyGroup, C.AccountID, C.Name, C.Address1, C.AddStreet, C.AddBuilding, C.AddCity, C.AddState, C.AddCountry, C.AddPostal, C.ContactPerson From tblperson C  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and (upper(C.Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or C.accountid like '" + txtPopupClientSearch.Text + "%' or C.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by Name"
            End If



            txtCustomerSearch.Text = SqlDSClient.SelectCommand
            'txtImportService.Text = SqlDSClient.SelectCommand
            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()
            'txtIsPopup.Text = "Client"


            'txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopupAccountID.Show()
            'txtIsPopup.Text = "Client"


        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        Try
            lblAlert.Text = ""
            txtCreatedOn.Text = ""
            'txtIsPopup.Text = ""
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo

            txtAccountIDCPUserAccess.Text = ""
            txtAccountNameUserAccess.Text = ""

            If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtAccountTypeCPUserAccess.Text = ""
            Else
                txtAccountTypeCPUserAccess.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            End If

            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDCPUserAccess.Text = ""
            Else
                txtAccountIDCPUserAccess.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If

            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtAccountNameUserAccess.Text = ""
            Else
                txtAccountNameUserAccess.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            End If


            'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '    txtAccountTypeCP.Text = ""
            'Else
            '    txtAccountTypeCP.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            'End If

            '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '    txtAccountIDCP.Text = ""
            '    Else
            '    txtAccountIDCP.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            '    End If

            '    If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
            '    txtNameCP.Text = ""
            '    Else
            '    txtNameCP.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
            '    End If

            'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
            '    ddlCompanyGrpSearch.Text = ""
            'Else
            '    ddlCompanyGrpSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
            'End If

            'If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
            '    ddlCompanyGrpSearch.Text = ""
            'Else
            '    ddlCompanyGrpSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
            'End If
            'txtSearch.Text = ""


            'gvClient.DataBind()
            mdlPopupAccountID.Hide()
      

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
        End Try

    End Sub



    Protected Sub OnRowDataBoundgNotes(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvNotesMaster, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgNotes(sender As Object, e As EventArgs) Handles gvNotesMaster.SelectedIndexChanged


        For Each row As GridViewRow In gvNotesMaster.Rows
           

            If row.RowIndex = gvNotesMaster.SelectedIndex Then
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

    Protected Sub gvNotesMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvNotesMaster.SelectedIndexChanged
        Try
            If txtCPModeUserAccess.Text = "Edit" Then
                lblAlert.Text = "CANNOT SELECT RECORD IN EDIT MODE. SAVE OR CANCEL TO PROCEED"
                Return
            End If

            lblAlert.Text = ""
            lblMessage.Text = ""
            MakeUserAccessNull()

            Dim editindex As Integer = gvNotesMaster.SelectedIndex
            rcno = DirectCast(gvNotesMaster.Rows(editindex).FindControl("Label1"), Label).Text
            txtCPUserAccessRcno.Text = rcno.ToString()
            ' InsertIntoTblWebEventLog("Portal", "gvNotesMaster", editindex.ToString, gvNotesMaster.SelectedRow.Cells(1).Text)

            txtAccountIDCPUserAccess.Text = gvNotesMaster.SelectedRow.Cells(1).Text
            txtAccountTypeCPUserAccess.Text = gvNotesMaster.SelectedRow.Cells(2).Text
            txtAccountNameUserAccess.Text = gvNotesMaster.SelectedRow.Cells(3).Text
            ddlLocationAccessType.Text = gvNotesMaster.SelectedRow.Cells(4).Text

            'btnAddCPUserAccess.Enabled = False
            'btnAddCPUserAccess.ForeColor = System.Drawing.Color.Gray

            btnEditCPUserAccess.Enabled = True
            btnEditCPUserAccess.ForeColor = System.Drawing.Color.Black

            btnDeleteCPUserAccess.Enabled = True
            btnDeleteCPUserAccess.ForeColor = System.Drawing.Color.Black

            'If ddlLocationAccessType.Text = "DEFINED LOCATIONS" Then
            btnLocations.Enabled = True
            btnLocations.ForeColor = System.Drawing.Color.Black
            'Else
            '    btnLocations.Enabled = False
            '    btnLocations.ForeColor = System.Drawing.Color.Gray
            'End If


            ''''''''''''''''''''''''''''''''''''''''
            'Start:Retrive Service Records

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandService As MySqlCommand = New MySqlCommand

            commandService.CommandType = CommandType.Text

            ' If ddlLocationAccessType.Text = "DEFINED LOCATIONS" Then
            commandService.CommandText = "SELECT count(rcno) as totrec FROM tblcustomerportaluseraccesslocation where UserID ='" & txtUserIDCPUserAccess.Text & "' and AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
            'Else
            'If txtAccountTypeCPUserAccess.Text = "CORPORATE" Then
            '    commandService.CommandText = "SELECT count(rcno) as totrec FROM tblCompanyLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
            'Else
            '    commandService.CommandText = "SELECT count(rcno) as totrec FROM tblPersonLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
            'End If
            ' End If

            commandService.Connection = conn

            Dim drService As MySqlDataReader = commandService.ExecuteReader()
            Dim dtService As New DataTable
            dtService.Load(drService)

            If dtService.Rows.Count > 0 Then

                Dim commandService1 As MySqlCommand = New MySqlCommand

                commandService1.CommandType = CommandType.Text

                ' If ddlLocationAccessType.Text = "DEFINED LOCATIONS" Then
                'commandService1.CommandText = "SELECT count(rcno) as totrec FROM tblcustomerportaluseraccesslocation where UserID ='" & txtUserIDCPUserAccess.Text & "' and AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                'Else
                If txtAccountTypeCPUserAccess.Text = "CORPORATE" Then
                    commandService1.CommandText = "SELECT count(rcno) as totrec FROM tblCompanyLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                Else
                    commandService1.CommandText = "SELECT count(rcno) as totrec FROM tblPersonLocation where AccountID ='" & txtAccountIDCPUserAccess.Text & "'"
                End If
                ' End If

                commandService1.Connection = conn

                Dim drService1 As MySqlDataReader = commandService1.ExecuteReader()
                Dim dtService1 As New DataTable
                dtService1.Load(drService1)

                btnLocations.Text = "LOCATIONS [" & Val(dtService.Rows(0)("totrec").ToString).ToString & " of " & Val(dtService1.Rows(0)("totrec").ToString).ToString & "]"

            End If

            'End:Retrieve Service Records
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "gvNotesMaster_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnLocations_Click(sender As Object, e As EventArgs) Handles btnLocations.Click
        Try
            lblAlert.Text = ""
            txtCreatedOn.Text = ""


            Dim qry As String
            qry = ""


            qry = "SELECT  Selected, Rcno, LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, ContractGroup FROM tblcustomerportaluseraccesslocation "
            qry = qry + " where AccountID = '" & txtAccountIDCPUserAccess.Text & "'"
            qry = qry + " and UserID = '" & txtUserIDCPUserAccess.Text & "'"
            qry = qry + " UNION "

            If txtAccountTypeCPUserAccess.Text = "CORPORATE" Then
                qry = qry + "Select Selected,  Rcno, LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, ContractGroup FROM tblCompanyLocation "
            Else
                qry = qry + "Select Selected,  Rcno, LocationID, ServiceName, Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, ContractGroup FROM tblPersonLocation "
            End If

            'qry = qry + " where AccountID = '" & txtAccountIDCPUserAccess.Text & "'"
            qry = qry + " where LocationID not in (select LocationID FROM tblcustomerportaluseraccesslocation "
            qry = qry + " where UserID = '" & txtUserIDCPUserAccess.Text & "' "
            qry = qry + " and AccountID = '" & txtAccountIDCPUserAccess.Text & "')"
            qry = qry + " and AccountID = '" & txtAccountIDCPUserAccess.Text & "'"
            qry = qry + " order by LocationID"
            sqlDSLocations.SelectCommand = qry

            grvServiceRecDetails.DataSourceID = "SqlDSLocations"
            grvServiceRecDetails.DataBind()

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                Dim lblidLocationID As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                Dim lblidContractGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractGroupGV"), TextBox)

                If String.IsNullOrEmpty(lblidContractGroup.Text) Then
                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry1 As String = "UPDATE tblcustomerportaluseraccesslocation SET ContractGroup=(select contractgroup from "

                    If txtAccountTypeCPUserAccess.Text = "CORPORATE" Then
                        qry1 = qry1 + "tblcompanylocation"
                    ElseIf txtAccountTypeCPUserAccess.Text = "RESIDENTIAL" Then
                        qry1 = qry1 + "tblpersonlocation"
                    End If
                    qry1 = qry1 + " where locationid='" & lblidLocationID.Text & "')"
                    qry1 = qry1 + " where locationid='" & lblidLocationID.Text & "' and AccountID = '" & txtAccountIDCPUserAccess.Text & "' and UserID = '" & txtUserIDCPUserAccess.Text & "'"

                    command.CommandText = qry1
                    command.Connection = conn
                    command.ExecuteNonQuery()

                    command.Dispose()
                End If
            Next
            conn.Close()
            conn.Dispose()

            sqlDSLocations.SelectCommand = qry

            grvServiceRecDetails.DataSourceID = "SqlDSLocations"
            grvServiceRecDetails.DataBind()

            mdlPopupLocation.Show()

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnLocations_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnCancelUserAccess_Click(sender As Object, e As EventArgs) Handles btnCancelUserAccess.Click
        DisableUserAccessControls()
        MakeUserAccessNull()
        txtCPModeUserAccess.Text = ""
    End Sub

    Protected Sub btnEditCPUserAccess_Click(sender As Object, e As EventArgs) Handles btnEditCPUserAccess.Click
        lblAlert.Text = ""
        lblMessage.Text = ""
        txtCreatedOn.Text = ""
        txtCPModeUserAccess.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT USER ACCESS"
        EnableUserAccessControls()
    End Sub

    Protected Sub btnDeleteCPUserAccess_Click(sender As Object, e As EventArgs) Handles btnDeleteCPUserAccess.Click
        lblMessage.Text = ""
        If txtCPUserAccessRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO DELETE"
            Return
        End If
        lblMessage.Text = "ACTION: DELETE USER INFORMATION"

        Dim confirmValue As String = Request.Form("confirm_value")
        If Right(confirmValue, 3) = "Yes" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblcustomerportaluseraccess where rcno=" & Convert.ToInt32(txtCPUserAccessRcno.Text)

                command.CommandText = qry
                command.Connection = conn
                command.ExecuteNonQuery()

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                Dim qry1 As String = "delete from tblcustomerportaluseraccesslocation where AccountId='" & txtAccountIDCPUserAccess.Text & "' and UserID ='" & txtUserIDCPUserAccess.Text & "'"

                command1.CommandText = qry1
                command1.Connection = conn
                command1.ExecuteNonQuery()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

                'End If
                conn.Close()
                conn.Dispose()
                'command1.Dispose()
                'dt.Dispose()
                'dr.Close()

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnDeleteCPUserAccess_Click", ex.Message.ToString, "")
            End Try
            DisableCPControls()
            ' DisableUserAccessControls()
            MakeUserAccessNull()

            'SqlDSCP.SelectCommand = "select * from tblcompanycustomeraccess where Accountid = '" + txtAccountID.Text + "'"
            SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
            gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
            gvNotesMaster.DataBind()
            '   InsertIntoTblWebEventLog("Portal", "DeleteAcess", gvNotesMaster.Rows.Count.ToString, txtCreatedBy.Text)
            If gvNotesMaster.Rows.Count > 0 Then
                '  InsertIntoTblWebEventLog("Portal1", "DeleteAcess", gvNotesMaster.Rows.Count.ToString, txtCreatedBy.Text)

                gvNotesMaster.SelectedIndex = 0
                gvNotesMaster_SelectedIndexChanged(sender, e)
            Else
                '  InsertIntoTblWebEventLog("Portal2", "DeleteAcess", gvNotesMaster.Rows.Count.ToString, txtCreatedBy.Text)

                btnLocations.Text = "LOCATIONS"

            End If


            lblMessage.Text = "DELETE: RECORD SUCCESSFULLY DELETED"

        End If
    End Sub

    Protected Sub txtAccountTypeCPUserAccess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtAccountTypeCPUserAccess.SelectedIndexChanged
        txtAccountIDCPUserAccess.Text = ""
        txtAccountNameUserAccess.Text = ""
        txtCreatedOn.Text = ""
    End Sub

    Protected Sub txtAccountIDCPUserAccess_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIDCPUserAccess.TextChanged
        'txtAccountIDCPUserAccess.Text = ""
        txtAccountNameUserAccess.Text = ""
        txtCreatedOn.Text = ""
    End Sub


    Private Sub SendEmail(ToEmail As String, StaffName As String, Mode As String)
        Try


            Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
            Dim oSmtp As New SmtpClient()

            oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()

            If Mode = "ADD" Then
                Dim Zone As String
                If lblDomainName.Text = "SINGAPORE-NEW" Then
                    Zone = "SINGAPORE"
                ElseIf lblDomainName.Text = "SINGAPORE-NEW (Beta)" Then
                    Zone = "SINGAPORE-BETA"
                ElseIf lblDomainName.Text = "MALAYSIA-NEW" Then
                    Zone = "MALAYSIA"
                ElseIf lblDomainName.Text = "PEST-PRO" Then
                    Zone = "PESTPRO"
                Else
                    Zone = lblDomainName.Text
                End If
                oMail.Subject = "ACTIVATION OF ACCOUNT"
                oMail.HtmlBody = "Hi " + StaffName + ",<br/><br/>Welcome to Anticimex Customer Portal - " + lblDomainName.Text + ".<br/><br/>Your account details are as follows: <br/><br/>" + " Zone: " + Zone + "<br/> User ID: " + txtUserIDCP.Text + "<br/> Password: " + txtPwdCP.Text + "<br/><br/> Please click on the below link to activate your account. <br/><br/> " + ConfigurationManager.AppSettings("ClientPortalURL").ToString() + " <br/><br/>Thank You.<br/><br/>-AOL Secure Login."
            Else
                oMail.Subject = "RESET PASSWORD"
                oMail.HtmlBody = "Hi " + StaffName + ",<br/><br/>Your Password has been reset.<br/><br/> USER ID: " + txtUserIDCP.Text.Trim + "<br/><br/> Email Address: " + txtEmailCP.Text + " <br/><br/>  Your new password is : " + txtPwdCP.Text + "<br/><br/> URL : " + ConfigurationManager.AppSettings("ClientPortalURL").ToString() + " <br/><br/>Thank You.<br/><br/>-Anticimex Customer Portal."
            End If


            oMail.To = ToEmail

            Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
            oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
            oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
            oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
            oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

            oSmtp.SendMail(oServer, oMail)
            oSmtp.Close()
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "SendEmail", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnResetPwd_Click(sender As Object, e As EventArgs) Handles btnResetPwd.Click
        Try
            mdlPopupConfirmPost.Show()
         
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnResetPwd_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        GeneratePwd()
        SendEmail(txtEmailCP.Text, txtNameCP.Text.ToUpper, "RESET")
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        txtCreatedOn.Text = ""
        lblAlertStatus.Text = ""
        ddlNewStatus.Text = ddlStatus.SelectedValue
        txtChangeStatusRemarks.Text = txtStatusRemarks.Text
        mdlPopupStatus.Show()
    End Sub

    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        'command.Parameters.AddWithValue("@Status", Left(ddlStatus.Text, 1))

        If ddlNewStatus.Text = txtDDLText.Text Then
            lblAlertStatus.Text = "SELECT NEW STATUS"
            mdlPopupStatus.Show()

            Return

        End If

        If ddlNewStatus.Text = ddlStatus.Text Then
            lblAlertStatus.Text = "STATUS ALREADY UPDATED"
            mdlPopupStatus.Show()

            Return
        End If

        Try
            If ddlNewStatus.SelectedIndex > 0 Then
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.CommandText = "UPDATE tblcustomerportaluser SET Status='" + Left(ddlNewStatus.Text, 1) + "' where rcno=" & Convert.ToInt32(txtCPRcno.Text)
                command.Connection = conn
                command.ExecuteNonQuery()

                '   UpdateContractActSvcDate(conn)

                conn.Close()
                conn.Dispose()
                command.Dispose()

                Dim newstatus As String = Left(ddlNewStatus.Text, 1)

                'ddlStatus.Text = ddlNewStatus.Text
                ddlStatus.Text = ddlNewStatus.SelectedValue
                ddlNewStatus.SelectedIndex = 0
                lblMessage.Text = "ACTION: STATUS UPDATED"
                '  CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CUSTOMERPORTAL", txtAccountIDCP.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIDCP.Text, "", txtRcno.Text)
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNoQR.Text, "ADD/POST", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmountQR.Text, 0, txtReceivedAmountQR.Text, txtAccountIdBilling.Text, "", txtRcno.Text)


                'SQLDSInvoice.SelectCommand = txt.Text
                'SQLDSInvoice.DataBind()
                'gvCP.DataBind()

                If newstatus = "A" Then
                    If txtStatusRemarks.Text <> "" Then
                        mdlConfirmRemarks.Show()
                    End If
                End If



                SqlDSCP.SelectCommand = "select * from tblcustomerportaluser order by accountid, userid"
                SqlDSCP.DataBind()
                gvCP.DataBind()

                mdlPopupStatus.Hide()

            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CUSTOMER PORTAL - " + Session("UserID"), "btnUpdateStatus_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        gvCP.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)
        SqlDSCP.SelectCommand = txt.Text
        SqlDSCP.DataBind()
        gvCP.DataBind()
    End Sub

    Protected Sub txtSearchCust_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCust.TextChanged
        Dim qry As String
        Try
            '''''''''''

            txtSearchCustText.Text = txtSearchCust.Text

            qry = "select *  from tblCustomerPortalUser where 1=1 "

            If String.IsNullOrEmpty(txtSearchCust.Text.Trim) = False And txtSearchCust.Text.Trim <> "Search Here" Then
                qry = qry + " and tblCustomerPortalUser.Name like ""%" & txtSearchCust.Text.Trim & "%"""
                qry = qry + " or tblCustomerPortalUser.EmailAddress like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblCustomerPortalUser.UserID like ""%" + txtSearchCust.Text.Trim + "%"""

            End If
            'End If


            qry = qry + " order by tblCustomerPortalUser.UserID;"
            txt.Text = qry
            MakeCPNull()
            SqlDSCP.SelectCommand = qry
            SqlDSCP.DataBind()
            gvCP.AllowPaging = False
            gvCP.DataBind()

            If gvCP.Rows.Count > 0 Then
                gvCP.SelectedIndex = 0
                gvCP_SelectedIndexChanged(sender, e)

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSearchCust_TextChanged", ex.Message.ToString, qry)
        End Try



        'lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count

        'txtSearchCust.Text = "Search Here"

        gvCP.AllowPaging = True

        tb1.ActiveTabIndex = 0
        'txtSearch.Text = txtSearchCust.Text

        ' '''''''''''

        'If gvCP.Rows.Count > 0 Then
        '    txtMode.Text = "View"

        '    txtRcno.Text = GridView1.Rows(0).Cells(4).Text

        '    'PopulateRecord()


        'Else
        'MakeCPNull()
        'MakeMeNullBillingDetails()
        'End If

        'btnGoSvc_Click(sender, e)

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + gvCP.Rows.Count.ToString
        ''''''''''''''''''''''
    End Sub

    Protected Sub btnGoCust_Click(sender As Object, e As EventArgs) Handles btnGoCust.Click
        Dim qry As String
        Try
            '''''''''''

            txtSearchCustText.Text = txtSearchCust.Text

            qry = "select *  from tblCustomerPortalUser where 1=1 "

            If String.IsNullOrEmpty(txtSearchCust.Text.Trim) = False And txtSearchCust.Text.Trim <> "Search Here" Then
                qry = qry + " and tblCustomerPortalUser.Name like ""%" & txtSearchCust.Text.Trim & "%"""
                qry = qry + " or tblCustomerPortalUser.EmailAddress like ""%" + txtSearchCust.Text.Trim + "%"""
                qry = qry + " or tblCustomerPortalUser.UserID like ""%" + txtSearchCust.Text.Trim + "%"""

            End If
            'End If


            qry = qry + " order by tblCustomerPortalUser.UserID;"
            txt.Text = qry
            MakeCPNull()
            SqlDSCP.SelectCommand = qry
            SqlDSCP.DataBind()
            gvCP.AllowPaging = False
            gvCP.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "btnSearchCust_TextChanged", ex.Message.ToString, qry)
        End Try



        'lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + GridView1.Rows.Count

        'txtSearchCust.Text = "Search Here"

        gvCP.AllowPaging = True

        tb1.ActiveTabIndex = 0
        'txtSearch.Text = txtSearchCust.Text

        ' '''''''''''

        'If gvCP.Rows.Count > 0 Then
        '    txtMode.Text = "View"

        '    txtRcno.Text = GridView1.Rows(0).Cells(4).Text

        '    'PopulateRecord()


        'Else
        'MakeCPNull()
        'MakeMeNullBillingDetails()
        'End If

        'btnGoSvc_Click(sender, e)

        lblMessage.Text = "SEARCH CRITERIA : " + txtSearchCust.Text + " <br/>NUMBER OF RECORDS FOUND : " + gvCP.Rows.Count.ToString
        ''''''''''''''''''''''
    End Sub

    Protected Sub gvCP_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvCP.Sorting
        Try

            'If GridSelected = "SQLDSContract" Then
            '    SQLDSContract.SelectCommand = txt.Text
            '    SQLDSContract.DataBind()
            'ElseIf GridSelected = "SQLDSContractClientId" Then
            '    'SqlDataSource1.SelectCommand = txt.Text
            '    SQLDSContractClientId.DataBind()
            'ElseIf GridSelected = "SQLDSContractNo" Then
            '    ''SqlDataSource1.SelectCommand = txt.Text
            '    SqlDSContractNo.DataBind()
            'End If

            SqlDSCP.SelectCommand = txt.Text
            gvCP.DataBind()
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
            lblAlert.Text = ex.Message.ToString
            'InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "GridView1_Sorting", ex.Message.ToString, txtContractNo.Text)
            'Exit Function
        End Try
    End Sub

    Protected Sub gvNotesMaster_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvNotesMaster.Sorting
        Try

            'If GridSelected = "SQLDSContract" Then
            '    SQLDSContract.SelectCommand = txt.Text
            '    SQLDSContract.DataBind()
            'ElseIf GridSelected = "SQLDSContractClientId" Then
            '    'SqlDataSource1.SelectCommand = txt.Text
            '    SQLDSContractClientId.DataBind()
            'ElseIf GridSelected = "SQLDSContractNo" Then
            '    ''SqlDataSource1.SelectCommand = txt.Text
            '    SqlDSContractNo.DataBind()
            'End If

            SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
            gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
            gvNotesMaster.DataBind()
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, "Error!!! " + ex.Message.ToString, "str")
            lblAlert.Text = ex.Message.ToString
            'InsertIntoTblWebEventLog("CONTRACT - " + Session("UserID"), "GridView1_Sorting", ex.Message.ToString, txtContractNo.Text)
            'Exit Function
        End Try
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        If Left(ddlStatus.Text, 1) = "A" Then
            txtStatusRemarks.Text = ""
        End If
    End Sub

    Protected Sub btnConfirmRemarksYes_Click(sender As Object, e As EventArgs) Handles btnConfirmRemarksYes.Click
        Try
            mdlPopupStatus.Hide()

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            Dim qry As String = "UPDATE tblcustomerportaluser SET StatusRemarks=@StatusRemarks"
            command1.CommandText = qry
            command1.Parameters.Clear()
            command1.Parameters.AddWithValue("@StatusRemarks", "")
            command1.Connection = conn

            command1.ExecuteNonQuery()

            command1.Dispose()
            conn.Close()
            conn.Dispose()

            SqlDSCP.SelectCommand = "select * from tblcustomerportaluser order by accountid, userid"
            SqlDSCP.DataBind()
            gvCP.DataBind()

            mdlConfirmRemarks.Hide()

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

        End Try
    End Sub

    Protected Sub txtSearchCopyAccess_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCopyAccess.TextChanged
        Try
         
            txtSearchCopyAccess1.Text = txtSearchCopyAccess.Text
            txtSearchCopyAccess.Text = "Search Here for User Access details"

            Dim searchstring As String = txtSearchCopyAccess1.Text

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = "select A.UserID,A.UserName,B.Name as Name,B.EmailAddress from tblcustomerportaluseraccess A left join tblcustomerportaluser B on A.UserID=B.UserID where B.Status='A' and (A.userid = '" + searchstring + "' or A.username like '%" + searchstring + "%')"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                txtCopyAccessName.Text = dt1.Rows(0)("Name").ToString
                txtCopyAccessEmailAddress.Text = dt1.Rows(0)("EmailAddress").ToString
                txtCopyAccessUserID.Text = dt1.Rows(0)("UserID").ToString

                UpdateCopySelected(conn, txtCopyAccessUserID.Text, txtUserIDCPUserAccess.Text)

                Dim qryCopyAccess As String = "select C.AccountID,C.LocationID,C.ContractGroup,C.ServiceName,C.Address1,C.AddBuilding,"
                qryCopyAccess = qryCopyAccess + "C.AddStreet,C.AddCity,C.AddCountry,C.AddState,C.AddPostal,C.Rcno as LocationRcNo,C.CopySelected"
                ' qryCopyAccess = qryCopyAccess + " from tblcustomerportaluseraccess A left join tblcustomerportaluseraccesslocation C on A.UserID=C.UserID and A.AccountID=C.AccountID"
                qryCopyAccess = qryCopyAccess + " from tblcustomerportaluseraccesslocation C"
                qryCopyAccess = qryCopyAccess + " where C.userid = '" + txtCopyAccessUserID.Text + "' and C.CopySelected=0"
                qryCopyAccess = qryCopyAccess + " order by C.rcno"
                ' InsertIntoTblWebEventLog("txtSearchCustRequest_TextChanged2", qryCustRequest, txtCreatedBy.Text)
             
                SqlDSCopyAccess.SelectCommand = qryCopyAccess

                grdCopyAccess.DataSourceID = "SqlDSCopyAccess"
                grdCopyAccess.DataBind()

            End If
            conn.Close()

            conn.Dispose()

          
            mdlCopyAccess.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CUSTOMER PORTAL", "txtSearchCopyAccessTextChanged", ex.Message.ToString, txtCreatedBy.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub UpdateCopySelected(conn As MySqlConnection, UserID As String, NewUserID As String)
        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text


        command.CommandText = "Update tblcustomerportaluseraccesslocation set CopySelected=0 where userid='" & UserID & "'"

        command.Connection = conn

        command.ExecuteNonQuery()
        command.Dispose()


        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text


        Dim qry As String = "update tblcustomerportaluseraccesslocation A, tblcustomerportaluseraccesslocation B"
        qry = qry + " set A.copyselected = 1 where A.userid='" & UserID & "' and A.locationid=B.locationid AND b.userid = '" & NewUserID & "'"

        command1.CommandText = qry

        command1.Connection = conn

        command1.ExecuteNonQuery()
        command1.Dispose()
    End Sub

    Protected Sub btnCopyAccess_Click(sender As Object, e As EventArgs) Handles btnCopyAccess.Click
        txtCopyAccessName.Text = ""
        txtCopyAccessEmailAddress.Text = ""
        txtCopyAccessUserID.Text = ""
        txtSearchCopyAccess.Text = ""

        SqlDSCopyAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where rcno=0"
        grdCopyAccess.DataSourceID = "SqlDSCopyAccess"
        grdCopyAccess.DataBind()


        mdlCopyAccess.Show()

    End Sub

    Protected Sub btnSaveCopyAccess_Click(sender As Object, e As EventArgs) Handles btnSaveCopyAccess.Click
        Dim totalRows As Long
        totalRows = 0

        For rowIndex1 As Integer = 0 To grdCopyAccess.Rows.Count - 1
            Dim TextBoxchkSelect1 As CheckBox = CType(grdCopyAccess.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
            If (TextBoxchkSelect1.Checked = True) Then
                totalRows = totalRows + 1
            End If
        Next rowIndex1
      

        If totalRows = 0 Then
            mdlCopyAccess.Show()
            Dim message As String = "alert('PLEASE SELECT A RECORD')"
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

            Exit Sub
        End If

        If grdCopyAccess.Rows.Count > 0 Then

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            For rowIndex As Integer = 0 To grdCopyAccess.Rows.Count - 1
                Dim TextBoxchkSelect As CheckBox = CType(grdCopyAccess.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

                If (TextBoxchkSelect.Checked = True) Then
                    ' Dim lblAccessRcNo As Label = CType(grdCopyAccess.Rows(rowIndex).Cells(0).FindControl("lblAccessRCNO"), Label)
                    Dim lblLocationRcNo As Label = CType(grdCopyAccess.Rows(rowIndex).Cells(0).FindControl("lblLocationRCNO"), Label)
                    Dim lblidAccountID As TextBox = CType(grdCopyAccess.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                    Dim lblidLocationID As TextBox = CType(grdCopyAccess.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)

                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT * FROM tblcustomerportaluseraccess where Userid = '" & txtUserIDCPUserAccess.Text & "' and AccountID = '" & lblidAccountID.Text & "'"
                    command1.Connection = conn

                    Dim dr As MySqlDataReader = command1.ExecuteReader()
                    Dim dt As New System.Data.DataTable
                    dt.Load(dr)

                    If dt.Rows.Count > 0 Then
                        'lblAlert.Text = "USER ACCESS DETAILS ALREADY AVAILABLE FOR THIS USER"
                        'txtCreatedOn.Text = ""
                        'Exit Sub

                    Else

                        Dim commandAccess As MySqlCommand = New MySqlCommand

                        commandAccess.CommandType = CommandType.Text

                        commandAccess.CommandText = "SELECT * FROM tblcustomerportaluseraccess where Userid = '" & txtCopyAccessUserID.Text & "' and accountid = '" & lblidAccountID.Text & "'"
                        commandAccess.Connection = conn

                        Dim drAccess As MySqlDataReader = commandAccess.ExecuteReader()
                        Dim dtAccess As New System.Data.DataTable
                        dtAccess.Load(drAccess)

                        If dtAccess.Rows.Count > 0 Then
                            ' InsertIntoTblWebEventLog("Portal", "SaveCopyAccess", txtUserIDCPUserAccess.Text + " " + lblidAccountID.Text, lblAccessRcNo.Text + " " + lblLocationRcNo.Text)
                            Dim command As MySqlCommand = New MySqlCommand

                            command.CommandType = CommandType.Text

                            'Dim qry1 As String = "INSERT INTO tblCustomerPortalUserAccess(AccountID, UserID,AccountType,LocationAccessType,AccountName,UserName,CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) "
                            'qry1 = qry1 + "select AccountID,@pr_NewUserID,AccountType,LocationAccessType,AccountName,@pr_NewUserName, Now(), @pr_CreatedBy, @pr_CreatedBy, Now() "
                            'qry1 = qry1 + "from tblcustomerportaluseraccess where userid=@pr_UserID and rcno=@pr_accessrcno;"
                            Dim qry1 As String = "INSERT INTO tblCustomerPortalUserAccess(AccountID,  UserID, AccountType, LocationAccessType, AccountName, UserName, CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn)VALUES(@AccountID, @UserID, @AccountType, @LocationAccessType,  @AccountName, @UserName, @CreatedOn,@CreatedBy,@LastModifiedBy,@LastModifiedOn);"
                            command.CommandText = qry1
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@AccountID", dtAccess.Rows(0)("AccountID").ToString)
                            command.Parameters.AddWithValue("@AccountType", dtAccess.Rows(0)("AccountType").ToString)
                            command.Parameters.AddWithValue("@AccountName", dtAccess.Rows(0)("AccountName").ToString)
                            command.Parameters.AddWithValue("@UserName", txtUserNameCPUserAccess.Text.ToUpper)
                            command.Parameters.AddWithValue("@LocationAccessType", dtAccess.Rows(0)("LocationAccessType").ToString)
                            command.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                            command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                            'command.CommandText = qry1

                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)
                            'command.Parameters.AddWithValue("@pr_UserID", txtCopyAccessUserID.Text)
                            'command.Parameters.AddWithValue("@pr_NewUserID", txtUserIDCPUserAccess.Text)
                            'command.Parameters.AddWithValue("@pr_NewUserName", txtUserNameCPUserAccess.Text)
                            'command.Parameters.AddWithValue("@pr_accessrcno", lblAccessRcNo)
                            command.Connection = conn
                            command.ExecuteNonQuery()

                            command.Dispose()

                        End If

                        dtAccess.Clear()

                        drAccess.Close()
                        commandAccess.Dispose()


                        End If

                        Dim command2 As MySqlCommand = New MySqlCommand

                        command2.CommandType = CommandType.Text

                        command2.CommandText = "SELECT * FROM tblcustomerportaluseraccesslocation where Userid = '" & txtUserIDCPUserAccess.Text & "' and AccountID = '" & lblidAccountID.Text & "' and LocationID = '" & lblidLocationID.Text & "'"
                        command2.Connection = conn

                        Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                        Dim dt2 As New System.Data.DataTable
                        dt2.Load(dr2)

                        If dt2.Rows.Count > 0 Then
                            'lblAlert.Text = "USER ACCESS DETAILS ALREADY AVAILABLE FOR THIS USER"
                            'txtCreatedOn.Text = ""
                            'Exit Sub

                        Else

                        Dim commandLoc As MySqlCommand = New MySqlCommand

                        commandLoc.CommandType = CommandType.Text

                        commandLoc.CommandText = "SELECT * FROM tblcustomerportaluseraccesslocation where Userid = '" & txtCopyAccessUserID.Text & "' and rcno = '" & lblLocationRcNo.Text & "'"
                        commandLoc.Connection = conn

                        Dim drLoc As MySqlDataReader = commandLoc.ExecuteReader()
                        Dim dtLoc As New System.Data.DataTable
                        dtLoc.Load(drLoc)

                        If dtLoc.Rows.Count > 0 Then
                            Dim commandLocation As MySqlCommand = New MySqlCommand
                            commandLocation.CommandType = CommandType.Text


                            Dim qryLocation As String = "INSERT INTO tblcustomerportaluseraccesslocation(AccountID,  UserID, AccountType, LocationID, Address1,  AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal, ServiceName, Selected, CreatedOn, CreatedBy,LastModifiedBy, LastModifiedOn)VALUES(@AccountID, @UserID, @AccountType, @LocationID, @Address1, @AddBuilding, @AddStreet, @AddState, @AddCity, @AddCountry, @AddPostal, @ServiceName, @Selected, @CreatedOn, @CreatedBy,@LastModifiedBy, @LastModifiedOn);"
                            commandLocation.CommandText = qryLocation
                            commandLocation.Parameters.Clear()
                                commandLocation.Parameters.AddWithValue("@AccountID", dtLoc.Rows(0)("AccountID").ToString)
                                commandLocation.Parameters.AddWithValue("@AccountType", dtLoc.Rows(0)("AccountType").ToString)
                                commandLocation.Parameters.AddWithValue("@LocationID", dtLoc.Rows(0)("LocationID").ToUpper)
                                commandLocation.Parameters.AddWithValue("@Address1", dtLoc.Rows(0)("Address1").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddBuilding", dtLoc.Rows(0)("AddBuilding").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddStreet", dtLoc.Rows(0)("AddStreet").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddState", dtLoc.Rows(0)("AddState").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddCity", dtLoc.Rows(0)("AddCity").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddCountry", dtLoc.Rows(0)("AddCountry").ToUpper)
                                commandLocation.Parameters.AddWithValue("@AddPostal", dtLoc.Rows(0)("AddPostal").ToUpper)

                                commandLocation.Parameters.AddWithValue("@ServiceName", dtLoc.Rows(0)("ServiceName").ToUpper)
                                commandLocation.Parameters.AddWithValue("@Selected", True)

                                commandLocation.Parameters.AddWithValue("@UserID", txtUserIDCPUserAccess.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandLocation.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                                commandLocation.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                                commandLocation.Connection = conn
                                commandLocation.ExecuteNonQuery()
                            '  InsertIntoTblWebEventLog("Portal", "SaveCopyAccess2", commandLocation.LastInsertedId.ToString, txtCreatedBy.Text)

                                commandLocation.Dispose()

                            End If
                            'Dim command As MySqlCommand = New MySqlCommand

                            'command.CommandType = CommandType.Text

                            'Dim qry1 As String = "INSERT INTO tblcustomerportaluseraccesslocation(AccountID,UserID,AccountType,LocationID,Address1,AddBuilding,AddStreet,AddState,AddCity,AddCountry,"
                            'qry1 = qry1 + "AddPostal,ServiceName,Selected,CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) select AccountID,@pr_NewUserID,AccountType,LocationID,Address1,"
                            'qry1 = qry1 + "AddBuilding, AddStreet, AddState, AddCity, AddCountry, AddPostal, ServiceName,Selected,Now(),@pr_CreatedBy,@pr_CreatedBy, Now()"
                            'qry1 = qry1 + " from tblcustomerportaluseraccesslocation where userid=@pr_UserID and rcno=@pr_locationrcno"


                            'command.CommandText = qry1

                            'command.Parameters.Clear()

                            'command.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)
                            'command.Parameters.AddWithValue("@pr_UserID", txtCopyAccessUserID.Text)
                            'command.Parameters.AddWithValue("@pr_NewUserID", txtUserIDCPUserAccess.Text)
                            'command.Parameters.AddWithValue("@pr_NewUserName", txtUserNameCPUserAccess.Text)
                            'command.Parameters.AddWithValue("@pr_locationrcno", lblLocationRcNo)
                         
                        End If
                End If
            Next

            conn.Close()
            conn.Dispose()

        End If


        'If grdCopyAccess.Rows.Count > 0 Then
        '    Dim YrStrList As List(Of [String]) = New List(Of String)()
        '    Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        '    For Each row As GridViewRow In grdCopyAccess.Rows
        '        If row.RowType = DataControlRowType.DataRow Then
        '            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelectGV"), CheckBox)
        '            If chkRow.Checked Then
        '                YrStrList.Add("""" + TryCast(row.Cells(1).FindControl("lblAccessRCNO"), Label).Text() + """")
        '                YrStrList1.Add("""" + TryCast(row.Cells(1).FindControl("lblLocationRCNO"), Label).Text() + """")
        '            End If
        '            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
        '            Dim YrStr1 As [String] = [String].Join(",", YrStrList1.ToArray())
        '            If String.IsNullOrEmpty(YrStr) = False Then
        '                invcheck = YrStr
        '            End If


        'Dim command1 As MySqlCommand = New MySqlCommand

        'command1.CommandType = CommandType.Text

        'command1.CommandText = "SELECT * FROM tblcustomerportaluseraccess where Userid = '" & txtUserIDCPUserAccess.Text & "'"
        'command1.Connection = conn

        'Dim dr As MySqlDataReader = command1.ExecuteReader()
        'Dim dt As New System.Data.DataTable
        'dt.Load(dr)

        'If dt.Rows.Count > 0 Then
        '    lblAlert.Text = "USER ACCESS DETAILS ALREADY AVAILABLE FOR THIS USER"
        '    txtCreatedOn.Text = ""
        '    Exit Sub
        'End If

        ' '''''''''''''''''''''''
        '' InsertIntoTblWebEventLog("PORTAL", txtCopyAccessUserID.Text, txtUserIDCPUserAccess.Text, txtUserNameCPUserAccess.Text + " " + txtCreatedBy.Text)

        'Dim command As MySqlCommand = New MySqlCommand

        'command.CommandType = CommandType.StoredProcedure

        'command.CommandText = "spcustomerportalcopyaccess"

        'command.Parameters.Clear()

        'command.Parameters.AddWithValue("@pr_CreatedBy", txtCreatedBy.Text)
        'command.Parameters.AddWithValue("@pr_UserID", txtCopyAccessUserID.Text)
        'command.Parameters.AddWithValue("@pr_NewUserID", txtUserIDCPUserAccess.Text)
        'command.Parameters.AddWithValue("@pr_NewUserName", txtUserNameCPUserAccess.Text)
        'command.Connection = conn
        'command.ExecuteScalar()

        'command.Dispose()

        'conn.Close()
        'conn.Dispose()



        '''''''''''''''''''''''''''End: Locations for All Locations


        '   MessageBox.Message.Alert(Page, "Record added successfully!!!", "str")
        lblMessage.Text = "COPY: USER ACCESS RECORDS COPIED SUCCESSFULLY"
        lblAlert.Text = ""

        'End If
      
        SqlDSCPUserAccess.SelectCommand = "SELECT * FROM tblcustomerportaluseraccess where UserID = '" & txtUserIDCPUserAccess.Text & "' order by accountid"
        gvNotesMaster.DataSourceID = "SqlDSCPUserAccess"
        gvNotesMaster.DataBind()

        If gvNotesMaster.Rows.Count > 0 Then
            gvNotesMaster.SelectedIndex = 0
            gvNotesMaster_SelectedIndexChanged(sender, e)
        Else

            btnLocations.Text = "LOCATIONS"

        End If

        DisableUserAccessControls()

        btnLocations.Enabled = True
        btnLocations.ForeColor = System.Drawing.Color.Black

        btnEditCPUserAccess.Enabled = True
        btnEditCPUserAccess.ForeColor = System.Drawing.Color.Black

        btnDeleteCPUserAccess.Enabled = True
        btnDeleteCPUserAccess.ForeColor = System.Drawing.Color.Black

        'If ddlLocationAccessType.Text = "DEFINED LOCATIONS" Then
        '    btnLocations.Enabled = True
        '    btnLocations.ForeColor = System.Drawing.Color.Black
        'End If
        txtCPModeUserAccess.Text = ""
    End Sub

    Private Sub CountUserAccess(conn As MySqlConnection)

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'conn.Open()

        Dim command3 As MySqlCommand = New MySqlCommand

        command3.CommandType = CommandType.Text


        command3.CommandText = "SELECT COUNT(*) AS COUNT FROM tblcustomerportaluseraccess where userid='" + txtUserIDCP.Text + "'"
        command3.Connection = conn

        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
        Dim dt3 As New DataTable
        dt3.Load(dr3)

        If dt3.Rows.Count > 0 Then
            If dt3.Rows(0)("Count").ToString = "0" Then
                '   btnMovement.Text = "MOVEMENT"
                lblAccessCount.Text = "User Access"
            Else
                ' btnMovement.Text = "MOVEMENT" + "[" + dt3.Rows(0)("Count").ToString + "]"
                lblAccessCount.Text = "User Access" + "[" + dt3.Rows(0)("Count").ToString + "]"
            End If

        End If
        dr3.Close()
        dt3.Dispose()
        command3.Dispose()

        'conn.Close()
        'conn.Dispose()


    End Sub
End Class
