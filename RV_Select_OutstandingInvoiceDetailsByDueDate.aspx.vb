Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_OutstandingInvoiceDetailsByDueDate
    Inherits System.Web.UI.Page

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        If String.IsNullOrEmpty(ddlAccountType.Text) Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If
        If ddlAccountType.Text = "-1" Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If

        If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            txtPopUpClient.Text = txtCustName.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        Else
            ' txtPopUpClient.Text = ""
            ' txtPopupClientSearch.Text = ""
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()
    End Sub


    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

        If txtPopupClientSearch.Text.Trim = "" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
            End If


        Else
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        gvClient.PageIndex = e.NewPageIndex

        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs)
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
            SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtAccountID.Text = ""
        Else
            txtAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
        End If

        If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            txtCustName.Text = ""
        Else
            txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim).ToString
        End If

    End Sub



    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        txtCustName.Text = ""

    End Sub


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click


    End Sub

    'Protected Sub btnSvcBy_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcBy.Click
    '    mdlPopUpTeam.TargetControlID = "btnSvcBy"
    '    txtModal.Text = "btnSvcBy"
    '    If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
    '        txtPopupTeamSearch.Text = txtServiceBy.Text.Trim
    '        txtPopUpTeam.Text = txtPopupTeamSearch.Text
    '        ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
    '        SqlDSTeam.DataBind()
    '        gvTeam.DataBind()
    '    Else
    '        'txtPopUpTeam.Text = ""
    '        'txtPopupTeamSearch.Text = ""
    '        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

    '        SqlDSTeam.DataBind()
    '        gvTeam.DataBind()
    '    End If
    '    mdlPopUpTeam.Show()
    'End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")
            txtPrintDate.Text = Convert.ToString(Session("SysDate"))
            txtCutOffDate.Enabled = False

            txtLocation.Text = ""

            Dim query As String
            query = ""

            Query = "SELECT contractgroup FROM tblcontractgroup ORDER BY contractgroup"
            PopulateDropDownList(query, "contractgroup", "contractgroup", ddlContractGroup)



            'SELECT * FROM tblcompany WHERE rcno <> 0 order by name
            SqlDSClient.SelectCommand = "SELECT * FROM tblcompany order by name"
            SqlDSClient.DataBind()

            gvClient.DataSourceID = "SqlDSClient"
            gvClient.DataBind()

            'SELECT * FROM tblcompany WHERE rcno <> 0 order by name
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
            SqlDSGL.DataBind()

            GrdViewGL.DataSourceID = "SqlDSGL"
            GrdViewGL.DataBind()


            'SELECT * FROM tblcompany WHERE rcno <> 0 order by name
            SqlDSStaffID.SelectCommand = "SELECT StaffId,Name FROM tblstaff ORDER BY STAFFID"
            SqlDSStaffID.DataBind()

            gvStaff.DataSourceID = "SqlDSStaffID"
            gvStaff.DataBind()

            'SELECT * FROM tblcompany WHERE rcno <> 0 order by name
            SqlDSSalesman.SelectCommand = "Select StaffId from tblStaff where Roles = 'SALES MAN'"
            SqlDSSalesman.DataBind()

            ddlSalesMan.DataSourceID = "SqlDSSalesman"
            ddlSalesMan.DataBind()



        End If
        txtLocation.Text = Convert.ToString(Session("Branch"))
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
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, textField.Trim() & valueField.Trim())
        End Try
    End Sub

    Protected Sub btnGL1_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL1.Click
        mdlPopupGLCode.TargetControlID = "btnGL1"
        txtModal.Text = "GL1"

        If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
            txtPopUpGL.Text = txtGLFrom.Text
            txtPopupGLSearch.Text = txtPopUpGL.Text
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()

        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
        End If
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub btnGL2_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL2.Click
        mdlPopupGLCode.TargetControlID = "btnGL2"
        txtModal.Text = "GL2"

        If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
            txtPopUpGL.Text = txtGLFrom.Text
            txtPopupGLSearch.Text = txtPopUpGL.Text
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()

        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
        End If
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub GrdViewGL_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdViewGL.PageIndexChanging
        GrdViewGL.PageIndex = e.NewPageIndex
        If txtPopUpGL.Text.Trim = "" Then
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
        End If

        SqlDSGL.DataBind()
        GrdViewGL.DataBind()
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub GrdViewGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        Try
            If txtModal.Text = "GL1" Then
                If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    txtGLFrom.Text = " "
                Else
                    txtGLFrom.Text = GrdViewGL.SelectedRow.Cells(1).Text
                End If
            ElseIf txtModal.Text = "GL2" Then
                If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    txtGLTo.Text = " "
                Else
                    txtGLTo.Text = GrdViewGL.SelectedRow.Cells(1).Text
                End If
            End If


            mdlPopupGLCode.Hide()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub txtPopUpGL_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpGL.TextChanged
        If txtPopUpGL.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter ledger Code", "str")
        Else
            txtPopupGLSearch.Text = txtPopUpGL.Text

            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
            ' txtIsPopUp.Text = "GL"
        End If
        txtPopUpGL.Text = "Search Here for Ledger Code or Description"
    End Sub

    Protected Sub btnPopUpClientReset_Click1(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpGL.Text = "Search Here for Ledger Code or Description"
        txtPopupGLSearch.Text = ""
        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
        SqlDSGL.DataBind()
        GrdViewGL.DataBind()
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub OnRowDataBoundgGL(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GrdViewGL, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgGL(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        For Each row As GridViewRow In GrdViewGL.Rows
            If row.RowIndex = GrdViewGL.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        If String.IsNullOrEmpty(txtPopupStaffSearch.Text.Trim) Then

            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"
        Else
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"
        End If

        SqlDSStaffID.DataBind()
        gvStaff.DataBind()
        gvStaff.PageIndex = e.NewPageIndex

        mdlPopupStaff.Show()
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        Try

            If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtIncharge.Text = " "
            Else
                txtIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
        mdlPopupStaff.Show()

    End Sub

    Protected Sub txtPopUpStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpStaff.TextChanged
        If txtPopUpStaff.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
            mdlPopupStaff.Show()
            ' txtIsPopUp.Text = "Staff"
        End If

        txtPopUpStaff.Text = "Search Here"
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        txtPopUpStaff.Text = ""
        txtPopupStaffSearch.Text = ""
        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

        SqlDSStaffID.DataBind()

        gvStaff.DataBind()
        mdlPopupStaff.Show()
    End Sub

    Protected Sub btnImgIncharge_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgIncharge.Click
        If String.IsNullOrEmpty(txtIncharge.Text.Trim) = False Then
            txtPopUpStaff.Text = txtIncharge.Text
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
        Else
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
        End If
        mdlPopupStaff.Show()

    End Sub

    Protected Sub OnRowDataBoundgStaff(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvStaff, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgStaff(sender As Object, e As EventArgs)
        For Each row As GridViewRow In gvStaff.Rows
            If row.RowIndex = gvStaff.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    'Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
    '    Dim i As Integer = lstSort1.SelectedIndex

    '    If i = -1 Then
    '        Exit Sub 'skip if no item is selected
    '    End If

    '    lstSort2.Items.Add(lstSort1.Items(i))
    '    lstSort1.Items.RemoveAt(i)


    'End Sub

    'Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
    '    Dim i As Integer = lstSort2.SelectedIndex

    '    If i = -1 Then
    '        Exit Sub 'skip if no item is selected
    '    End If

    '    lstSort1.Items.Add(lstSort2.Items(i))
    '    lstSort2.Items.RemoveAt(i)


    'End Sub

    Private Function GetData() As Boolean
        '  Dim qrySumm As String = ""
        Dim qry As String = ""
        Dim qryrecv As String = ""
        Dim qryrecv1 As String = ""

        If rbtnSelectDetSumm.SelectedValue = "1" Then
            If chkCheckCutOff.Checked = True Then
                '  qry = "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"

                '  qry = qry + " ,OPeriodCredit as CreditBase,OPeriodPaid as ReceiptBase,OPeriodBalance as UnpaidBalance FROM tblsales where 1=1 and poststatus='P'"

                qry = qry + "SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention as ContactPerson,tblsales.CustTelephone as ContactTel,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,"
                qry = qry + "tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,"
                qry = qry + "tblsales.OPeriodCredit as Creditbase,tblsales.OPeriodPaid as Receiptbase,tblsales.OPeriodBalance as UnpaidBalance,tblsales.createdby as InvoicePreparedBy,vwcustomermainbillinginfo.BillContactPerson,vwcustomermainbillinginfo.BillMobile,vwcustomermainbillinginfo.BillTelephone,"
                qry = qry + "vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillFax, replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman,tblsales.PONo "
                qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P'"

                qryrecv = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,receiptdate,salesdate,invoicenumber,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
                qryrecv = qryrecv + " left outer join tblsales on tblrecvdet.reftype=tblsales.invoicenumber where tblrecv.poststatus='P' and tblsales.salesdate > tblrecv.receiptdate and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and tblsales.salesdate > '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                qryrecv1 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
                qryrecv1 = qryrecv1 + " where tblrecv.poststatus='P' and tblrecv.bankid<>'CONTRA' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            Else
                '   qry = qry + "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"
                '  qry = qry + " ,Creditbase,Receiptbase,Balancebase as UnpaidBalance FROM tblsales where 1=1 and poststatus='P' and UnpaidBalance<>0"
                qry = qry + "SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention as ContactPerson,tblsales.CustTelephone as ContactTel,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,"
                qry = qry + "tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,"
                qry = qry + "tblsales.Creditbase,tblsales.Receiptbase,tblsales.Balancebase as UnpaidBalance,tblsales.createdby as InvoicePreparedBy,vwcustomermainbillinginfo.BillContactPerson,vwcustomermainbillinginfo.BillMobile,vwcustomermainbillinginfo.BillTelephone,"
                qry = qry + "vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillFax,replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman,tblsales.PONo "
                qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P' and Balancebase<>0"

            End If
        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then

            If chkCheckCutOff.Checked = True Then
                qry = "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"

                qry = qry + " ,OPeriodCredit as CreditBase,OPeriodPaid as ReceiptBase,OPeriodBalance as UnpaidBalance FROM tblsales where poststatus='P'"

                qryrecv = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,receiptdate,salesdate,invoicenumber,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
                qryrecv = qryrecv + " left outer join tblsales on tblrecvdet.reftype=tblsales.invoicenumber where tblrecv.poststatus='P' and tblsales.salesdate > tblrecv.receiptdate and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and tblsales.salesdate > '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                qryrecv1 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
                qryrecv1 = qryrecv1 + " where tblrecv.poststatus='P' and tblrecv.bankid<>'CONTRA' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            Else
                qry = qry + "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"
                qry = qry + " ,Creditbase,Receiptbase,Balancebase as UnpaidBalance FROM tblsales where poststatus='P' and BalanceBase<>0"


            End If
        End If


        lblAlert.Text = ""


        If ddlInvoiceType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.doctype ='" + ddlInvoiceType.SelectedValue.ToString + "'"
            'selFormula = selFormula + " and {tblsales1.doctype} = '" + ddlInvoiceType.SelectedValue.text + "'"
            'If selection = "" Then
            '    selection = "Invoice Type : " + txtAccountID.Text
            'Else
            '    selection = selection + ", Invoice Type : " + txtAccountID.Text
            'End If
        End If
        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                Return False
            End If
            qry = qry + " and tblsales.glperiod >='" + d.ToString("yyyyMM") + "'"
            'selFormula = selFormula + " and {tblsales1.glperiod} "
            'If selection = "" Then
            '    selection = "Accounting Period >= " + d.ToString("yyyyMM")
            'Else
            '    selection = selection + ", Accounting Period >= " + d.ToString("yyyyMM")
            'End If

        End If

        If String.IsNullOrEmpty(txtAcctPeriodTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodTo.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Accounting Period To is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD TO IS INVALID"
                Return False
            End If
            qry = qry + " and tblsales.glperiod <='" + d.ToString("yyyyMM") + "'"

            'selFormula = selFormula + " and {tblsales1.glperiod} <='" + d.ToString("yyyyMM") + "'"
            'If selection = "" Then
            '    selection = "Accounting Period <= " + d.ToString("yyyyMM")
            'Else
            '    selection = selection + ", Accounting Period <= " + d.ToString("yyyyMM")
            'End If
        End If

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID BILLED FROM DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            'selFormula = selFormula + " and {tblsales1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            'If selection = "" Then
            '    selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
            'Else
            '    selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
            'End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID BILLED TO DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

            'selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            'If selection = "" Then
            '    selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
            'Else
            '    selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
            'End If
        End If
        If String.IsNullOrEmpty(txtDueDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Due Date From is invalid", "str")
                lblAlert.Text = "INVALID DUE FROM DATE"
                Return False
            End If
            qry = qry + " and tblsales.DUEdate>= '" + Convert.ToDateTime(txtDueDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            'selFormula = selFormula + " and {tblsales1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            'If selection = "" Then
            '    selection = "Due Date >= " + d.ToString("dd-MM-yyyy")
            'Else
            '    selection = selection + ", Due Date >= " + d.ToString("dd-MM-yyyy")
            'End If

        End If

        If String.IsNullOrEmpty(txtDueDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID Due TO DATE"
                Return False
            End If
            qry = qry + " and tblsales.duedate <= '" + Convert.ToDateTime(txtDueDateTo.Text).ToString("yyyy-MM-dd") + "'"

            'selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            'If selection = "" Then
            '    selection = "Due Date <= " + d.ToString("dd-MM-yyyy")
            'Else
            '    selection = selection + ", Due Date <= " + d.ToString("dd-MM-yyyy")
            'End If
        End If
        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            qry = qry + " and tblsalesdetail.ledgercode >= '" + txtGLFrom.Text + "'"
            'selFormula = selFormula + " and {tblsalesdetail1.ledgercode} >= '" + txtGLFrom.Text + "'"
            'If selection = "" Then
            '    selection = "Ledger Code >= " + txtGLFrom.Text
            'Else
            '    selection = selection + ", Ledger Code >= " + txtGLFrom.Text
            'End If
        End If

        If String.IsNullOrEmpty(txtGLTo.Text) = False Then
            qry = qry + " and tblsalesdetail.ledgercode <= '" + txtGLTo.Text + "'"

            'selFormula = selFormula + " and {tblsalesdetail1.ledgercode} <= '" + txtGLTo.Text + "'"
            'If selection = "" Then
            '    selection = "Ledger Code <= " + txtGLTo.Text
            'Else
            '    selection = selection + ", Ledger Code <= " + txtGLTo.Text
            'End If
        End If

        'If String.IsNullOrEmpty(txtInvoiceNoFrom.Text) = False Then
        '    qry = qry + " and tblsales.InvoiceNumber >= '" + txtInvoiceNoFrom.Text + "'"
        '    selFormula = selFormula + " and {tblsales1.InvoiceNumber} >= '" + txtInvoiceNoFrom.Text + "'"
        '    If selection = "" Then
        '        selection = "InvoiceNumber >= " + txtInvoiceNoFrom.Text
        '    Else
        '        selection = selection + ", InvoiceNumber >= " + txtInvoiceNoFrom.Text
        '    End If
        'End If

        'If String.IsNullOrEmpty(txtInvoiceNoTo.Text) = False Then
        '    qry = qry + " and tblsales.InvoiceNumber <= '" + txtInvoiceNoTo.Text + "'"

        '    selFormula = selFormula + " and {tblsales1.InvoiceNumber} <= '" + txtInvoiceNoTo.Text + "'"
        '    If selection = "" Then
        '        selection = "InvoiceNumber <= " + txtInvoiceNoTo.Text
        '    Else
        '        selection = selection + ", InvoiceNumber <= " + txtInvoiceNoTo.Text
        '    End If
        'End If

        'If String.IsNullOrEmpty(txtOurRef.Text) = False Then
        '    qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

        '    selFormula = selFormula + " and {tblsales1.OurRef} like '" + txtOurRef.Text + "*'"
        '    If selection = "" Then
        '        selection = "OurRef = " + txtOurRef.Text
        '    Else
        '        selection = selection + ", OurRef = " + txtOurRef.Text
        '    End If
        'End If

        'If String.IsNullOrEmpty(txtYourRef.Text) = False Then
        '    qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
        '    selFormula = selFormula + " and {tblsales1.YourRef} like '" + txtYourRef.Text + "*'"
        '    If selection = "" Then
        '        selection = "YourRef = " + txtYourRef.Text
        '    Else
        '        selection = selection + ", YourRef = " + txtYourRef.Text
        '    End If
        'End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            'selFormula = selFormula + " and {tblsales1.StaffCode} like '" + txtIncharge.Text + "*'"
            'If selection = "" Then
            '    selection = "Staff/Incharge = " + txtIncharge.Text
            'Else
            '    selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            'End If
        End If

        If ddlSalesMan.Text = "-1" Then
        Else

            qry = qry + " and tblsales.Salesman = '" + ddlSalesMan.Text + "'"
            'selFormula = selFormula + " and {tblsales1.PONo} = '" + txtPONo.Text + "'"
            'If selection = "" Then
            '    selection = "PONo = " + txtPONo.Text
            'Else
            '    selection = selection + ", PONo = " + txtPONo.Text
            'End If
        End If

        'If String.IsNullOrEmpty(txtComments.Text) = False Then
        '    qry = qry + " and tblsales.Comments like '*" + txtComments.Text + "*'"
        '    selFormula = selFormula + " and {tblsales1.Comments} like '*" + txtComments.Text + "*'"
        '    If selection = "" Then
        '        selection = "Comments = " + txtComments.Text
        '    Else
        '        selection = selection + ", Comments = " + txtComments.Text
        '    End If
        'End If

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            'selFormula = selFormula + " and {tblsales1.ContactType} = '" + ddlAccountType.Text + "'"
            'If selection = "" Then
            '    selection = "AccountType : " + ddlAccountType.Text
            'Else
            '    selection = selection + ", AccountType : " + ddlAccountType.Text
            'End If
            qryrecv = qryrecv + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
            'selFormula = selFormula + " and {tblsales1.Accountid} = '" + txtAccountID.Text + "'"
            'If selection = "" Then
            '    selection = "AccountID : " + txtAccountID.Text
            'Else
            '    selection = selection + ", AccountID : " + txtAccountID.Text
            'End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
            qryrecv = qryrecv + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"
            qryrecv1 = qryrecv1 + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"
            'selFormula = selFormula + " and {tblsales1.CustName} like '" + txtCustName.Text + "*'"
            'If selection = "" Then
            '    selection = "CustName : " + txtCustName.Text
            'Else
            '    selection = selection + ", CustName : " + txtCustName.Text
            'End If
        End If

        ''If ddlCompanyGrp.Text = "-1" Then
        ''Else
        ''    selFormula = selFormula + " and {tblsales1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        ''    If selection = "" Then
        ''        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        ''    Else
        ''        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        ''    End If

        ''End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
            qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
            qryrecv = qryrecv + " and tblrecv.CompanyGroup in (" + YrStr + ")"
            qryrecv1 = qryrecv1 + " and tblrecv.CompanyGroup in (" + YrStr + ")"
            'selFormula = selFormula + " and {tblsales1.CompanyGroup} in [" + YrStr + "]"
            'If selection = "" Then
            '    selection = "CompanyGroup : " + YrStr
            'Else
            '    selection = selection + ", CompanyGroup : " + YrStr
            'End If
        End If



        If ddlLocateGrp.Text = "-1" Then
        Else
            qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            'selFormula = selFormula + " and {tblsales1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            'If selection = "" Then
            '    selection = "Location Group : " + ddlLocateGrp.Text
            'Else
            '    selection = selection + ", Location Group : " + ddlLocateGrp.Text
            'End If
        End If

        If ddlTerms.Text = "-1" Then
        Else
            qry = qry + " and tblsales.Terms = '" + ddlTerms.Text + "'"
            'selFormula = selFormula + " and {@fPaidStatus} = '" + ddlPaidStatus.Text + "'"
            'If selection = "" Then
            '    selection = "PaidStatus : " + ddlPaidStatus.Text
            'Else
            '    selection = selection + ", PaidStatus : " + ddlPaidStatus.Text
            'End If
        End If


        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
            qry = qry + " and tblsales.GLStatus = '" + txtGLStatus.Text + "'"
            'selFormula = selFormula + " and {tblsales1.GLStatus} = '" + txtGLStatus.Text + "'"
            'If selection = "" Then
            '    selection = "GLStatus : " + txtGLStatus.Text
            'Else
            '    selection = selection + ", GLStatus : " + txtGLStatus.Text
            'End If
        End If

        'If ddlStatus.Text = "-1" Then
        'Else
        '    qry = qry + " and tblsales.PostStatus = '" + ddlStatus.Text + "'"
        '    selFormula = selFormula + " and {tblsales1.PostStatus} = '" + ddlStatus.Text + "'"
        '    If selection = "" Then
        '        selection = "Status : " + ddlStatus.Text
        '    Else
        '        selection = selection + ", Status : " + ddlStatus.Text
        '    End If
        'End If

        If chkUnpaidBal.Checked = True Then
            If chkCheckCutOff.Checked = True Then
                qry = qry + " and tblsales.operiodBalance > 0 "
            Else
                qry = qry + " and tblsales.Balancebase > 0 "
            End If


        End If



        If chkCheckCutOff.Checked = True Then

            If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                    '  lblAlert.Text = "INVALID START DATE"
                    Return False
                End If
                qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            End If
        Else
            If String.IsNullOrEmpty(txtPrintDate.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    MessageBox.Message.Alert(Page, "Print Date is invalid", "str")
                    '  lblAlert.Text = "INVALID START DATE"
                    Return False
                End If
                qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"

            End If
        End If

        ' If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
        qry = qry + " group by accountid,invoicenumber ORDER BY AccountId, InvoiceNumber"
        qryrecv = qryrecv + " ORDER BY AccountId, ReceiptNumber"
        txtQueryRecv.Text = qryrecv
        txtQueryRecv1.Text = qryrecv1

        'ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
        'qry = qry + " group by accountid ORDER BY AccountId"

        'End If
        txtQuery.Text = qry

        Return True
    End Function

    Private Function getdatasetSen() As DataTable
        Dim lInvoiceNo As String
        lInvoiceNo = ""
        Try
            'Dim dt2 As New DataTable
            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry As String
            qry = ""

            If chkIncludeCompanyGroupInfo.Checked = True Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 6000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,AddCity, AddState, AddCountry, AddPostal, BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket, "
                        qry = qry + " (Current) as Current, DaysOverdue, (1To10) as 1To10,(11To30) as 11To30,"
                        qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress, BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                        qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If
                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to10 As Decimal = 0
                    Dim Total11to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""
                    Dim addCity As String = ""
                    Dim addState As String = ""
                    Dim addCountry As String = ""
                    Dim addPostal As String = ""

                    For Each dr As DataRow In dt2.Rows


                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email, AddCity, AddState, AddCountry, AddPostal from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                '29.11.23
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCity")) Then
                                                    addCity = "-"
                                                Else
                                                    addCity = dtLoc.Rows(0)("AddCity").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddState")) Then
                                                    addState = "-"
                                                Else
                                                    addState = dtLoc.Rows(0)("AddState").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCountry")) Then
                                                    addCountry = "-"
                                                Else
                                                    addCountry = dtLoc.Rows(0)("AddCountry").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddPostal")) Then
                                                    addPostal = "-"
                                                Else
                                                    addPostal = dtLoc.Rows(0)("AddPostal").ToString.Trim
                                                End If
                                                '29.11.23

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email, AddCity, AddState, AddCountry, AddPostal from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                '29.11.23
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCity")) Then
                                                    addCity = "-"
                                                Else
                                                    addCity = dtLoc.Rows(0)("AddCity").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddState")) Then
                                                    addState = "-"
                                                Else
                                                    addState = dtLoc.Rows(0)("AddState").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCountry")) Then
                                                    addCountry = "-"
                                                Else
                                                    addCountry = dtLoc.Rows(0)("AddCountry").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddPostal")) Then
                                                    addPostal = "-"
                                                Else
                                                    addPostal = dtLoc.Rows(0)("AddPostal").ToString.Trim
                                                End If

                                                '29.11.23

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)
                        dr("AddCity") = Left(addCity.Trim, 50)
                        dr("AddState") = Left(addState.Trim, 50)
                        dr("AddCountry") = Left(addCountry.Trim, 50)
                        dr("AddPostal") = Left(addPostal.Trim, 20)


                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                            Total1to10 = Total1to10 + dr.Item("1To10")
                        End If
                        If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                            Total11to30 = Total11to30 + dr.Item("11To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                            Total91to150 = Total91to150 + dr.Item("91To150")
                        End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To10") = Total1to10
                    drTotal.Item("11To30") = Total11to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To150") = Total91to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 6000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location, CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    End If


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY Location, ACCOUNTID, CompanyGroup"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"

                    End If
                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to10 As Decimal = 0
                    Dim Total11to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0


                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                            Total1to10 = Total1to10 + dr.Item("1To10")
                        End If
                        If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                            Total11to30 = Total11to30 + dr.Item("11To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                            Total91to150 = Total91to150 + dr.Item("91To150")
                        End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To10") = Total1to10
                    drTotal.Item("11To30") = Total11to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To150") = Total91to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
                End If  'end for Company Group

            ''''''''''''''''''''''''''''''''''''''''''''''''''''

            If chkIncludeCompanyGroupInfo.Checked = False Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 6000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress, AddCity, AddState, AddCountry, AddPostal,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket, "
                        qry = qry + " (Current) as Current, DaysOverdue, (1To10) as 1To10,(11To30) as 11To30,"
                        qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    Else
                        If chkIncludeReceiptDetails.Checked = False Then
                            qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                            qry = qry + " invoicenumber,salesdate, ServiceAddress, BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                            qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                            qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                            qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                            qry = qry + " FROM tblrptosageingsoa "
                            qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                        Else
                            qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                            qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                            qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                            qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                            qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance, "
                            qry = qry + " ReceiptDetails "
                            qry = qry + " FROM tblrptosageingsoa left join vwselectreceipts on  invoicenumber= vwselectreceipts.InvNos"
                            qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                        End If

                        'qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                        'qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                        'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        'qry = qry + " FROM tblrptosageingsoa "
                        'qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    End If

                    'InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "50", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    'qry = "SELECT Location, AccountID,CustName AS AccountName, ContactType, "
                    'qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                    'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                    'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                    'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to10 As Decimal = 0
                    Dim Total11to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""
                    Dim addCity As String = ""
                    Dim addState As String = ""
                    Dim addCountry As String = ""
                    Dim addPostal As String = ""

                    For Each dr As DataRow In dt2.Rows

                        'If String.IsNullOrEmpty(dr("InvoiceNumber")) = True Then
                        '    dr("InvoiceNumber").ToString()
                        'End If

                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            'InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "51", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email, AddCity, AddState, AddCountry, AddPostal from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCity")) Then
                                                    addCity = "-"
                                                Else
                                                    addCity = dtLoc.Rows(0)("AddCity").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddState")) Then
                                                    addState = "-"
                                                Else
                                                    addState = dtLoc.Rows(0)("AddState").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCountry")) Then
                                                    addCountry = "-"
                                                Else
                                                    addCountry = dtLoc.Rows(0)("AddCountry").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddPostal")) Then
                                                    addPostal = "-"
                                                Else
                                                    addPostal = dtLoc.Rows(0)("AddPostal").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email, AddCity, AddState, AddCountry, AddPostal from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCity")) Then
                                                    addCity = "-"
                                                Else
                                                    addCity = dtLoc.Rows(0)("AddCity").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddState")) Then
                                                    addState = "-"
                                                Else
                                                    addState = dtLoc.Rows(0)("AddState").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddCountry")) Then
                                                    addCountry = "-"
                                                Else
                                                    addCountry = dtLoc.Rows(0)("AddCountry").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("AddPostal")) Then
                                                    addPostal = "-"
                                                Else
                                                    addPostal = dtLoc.Rows(0)("AddPostal").ToString.Trim
                                                End If
                                            

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")

                        'InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "52", lInvoiceNo, ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)
                        dr("AddCity") = Left(addCity.Trim, 50)
                        dr("AddState") = Left(addState.Trim, 50)
                        dr("AddCountry") = Left(addCountry.Trim, 50)
                        dr("AddPostal") = Left(addPostal.Trim, 20)


                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                            Total1to10 = Total1to10 + dr.Item("1To10")
                        End If
                        If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                            Total11to30 = Total11to30 + dr.Item("11To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                            Total91to150 = Total91to150 + dr.Item("91To150")
                        End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If


                    Next

                    InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "53", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To10") = Total1to10
                    drTotal.Item("11To30") = Total11to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To150") = Total91to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)

                    InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "54", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 6000


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                         qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa "
                        qry = qry + " where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    End If


                    'qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                    'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If

                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, Location "
                    Else
                        qry = qry + "GROUP BY ACCOUNTID "
                    End If
                    'qry = qry + "GROUP BY ACCOUNTID "
                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to10 As Decimal = 0
                    Dim Total11to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0


                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                            Total1to10 = Total1to10 + dr.Item("1To10")
                        End If
                        If dr.Item("11To30").ToString <> DBNull.Value.ToString Then
                            Total11to30 = Total11to30 + dr.Item("11To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                            Total91to150 = Total91to150 + dr.Item("91To150")
                        End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To10") = Total1to10
                    drTotal.Item("11To30") = Total11to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To150") = Total91to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("AGEING - " + Session("UserID"), "Page_Load", ex.Message.ToString, lInvoiceNo)
            'Return dt2
            'Exit Function
        End Try

    End Function



    Private Function getdatasetSenFormat2() As DataTable
        Dim lInvoiceNo As String
        lInvoiceNo = ""
        Try
            'Dim dt2 As New DataTable
            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry As String
            qry = ""

            If chkIncludeCompanyGroupInfo.Checked = True Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket,"
                        qry = qry + " (Current) as Current, DaysOverdue, (1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To270,(271To360) as 271To360,(greaterThan360) as '>360',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,  (1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To270,(271To360) as 271To360,(greaterThan360) as '>360',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                    Dim Total181to270 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    Dim Total271to360 As Decimal = 0
                    Dim Total360 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows


                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To270").ToString <> DBNull.Value.ToString Then
                            Total181to270 = Total181to270 + dr.Item("181To270")
                        End If
                        If dr.Item("271To360").ToString <> DBNull.Value.ToString Then
                            Total271to360 = Total271to360 + dr.Item("271To360")
                        End If
                        'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("151To180")
                        'End If
                        If dr.Item(">360").ToString <> DBNull.Value.ToString Then
                            Total360 = Total360 + dr.Item(">360")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To270") = Total181to270
                    drTotal.Item("271To360") = Total271to360
                    drTotal.Item(">360") = Total360
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location, CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To270,sum(271To360) as 271To360, sum(GreaterThan360) as '>360',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To270,sum(271To360) as 271To360, sum(GreaterThan360) as '>360',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If


                    '17.07.22

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup, Location"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    End If
                    '17.07.22

                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0

                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                    Dim Total181to270 As Decimal = 0
                    Dim Total271to360 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    Dim Total360 As Decimal = 0

                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To270").ToString <> DBNull.Value.ToString Then
                            Total181to270 = Total181to270 + dr.Item("181To270")
                        End If
                        If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                            Total271to360 = Total271to360 + dr.Item("271To360")
                        End If
                        'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("151To180")
                        'End If
                        If dr.Item(">360").ToString <> DBNull.Value.ToString Then
                            Total360 = Total360 + dr.Item(">360")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To270") = Total181to270
                    drTotal.Item("271To360") = Total271to360
                    'drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">360") = Total360
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            ''''''''''''''''''''''''''''''''''''''''''''''''''''

            If chkIncludeCompanyGroupInfo.Checked = False Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket,"
                        qry = qry + " (Current) as Current, DaysOverdue, (1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To270,(271To360) as 271To360,(greaterThan360) as '>360',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,(1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To270,(271To360) as 271To360,(greaterThan360) as '>360',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If



                    'qry = "SELECT Location, AccountID,CustName AS AccountName, ContactType, "
                    'qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                    'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                    'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                    'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0


                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                    Dim Total181to270 As Decimal = 0
                    Dim Total271to360 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    Dim Total360 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows

                        'If String.IsNullOrEmpty(dr("InvoiceNumber")) = True Then
                        '    dr("InvoiceNumber").ToString()
                        'End If

                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To270").ToString <> DBNull.Value.ToString Then
                            Total181to270 = Total181to270 + dr.Item("181To270")
                        End If
                        If dr.Item("271To360").ToString <> DBNull.Value.ToString Then
                            Total271to360 = Total271to360 + dr.Item("271To360")
                        End If
                        'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("151To180")
                        'End If
                        If dr.Item(">360").ToString <> DBNull.Value.ToString Then
                            Total360 = Total360 + dr.Item(">360")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To270") = Total181to270
                    drTotal.Item("271To360") = Total271to360
                    'drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">360") = Total360
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To270,sum(271To360) as 271To360, sum(GreaterThan360) as '>360',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To270,sum(271To360) as 271To360, sum(GreaterThan360) as '>360',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    'qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                    'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"

                    '17.07.22

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, Location"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID"
                    End If
                    '17.07.22
                    'qry = qry + "GROUP BY ACCOUNTID "

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0

                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                    Dim Total181to270 As Decimal = 0
                    Dim Total271to360 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    Dim Total360 As Decimal = 0

                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To270").ToString <> DBNull.Value.ToString Then
                            Total181to270 = Total181to270 + dr.Item("181To270")
                        End If
                        If dr.Item("271To360").ToString <> DBNull.Value.ToString Then
                            Total271to360 = Total271to360 + dr.Item("271To360")
                        End If
                        'If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("151To180")
                        'End If
                        If dr.Item(">360").ToString <> DBNull.Value.ToString Then
                            Total360 = Total360 + dr.Item(">360")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To270") = Total181to270
                    drTotal.Item("271To360") = Total271to360
                    'drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">360") = Total360
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("AGEING - " + Session("UserID"), "Page_Load", ex.Message.ToString, lInvoiceNo)
            'Return dt2
            'Exit Function
        End Try

    End Function



    Private Function getdatasetSenFormat3() As DataTable
        Dim lInvoiceNo As String
        lInvoiceNo = ""
        Try
            'Dim dt2 As New DataTable
            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry As String
            qry = ""

            If chkIncludeCompanyGroupInfo.Checked = True Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch, CompanyGroup, ContractGroup, AccountID,CustName AS AccountName, invoicenumber,salesdate, (UnPaidBalance) as UnpaidBalance, AgeingBucket, TermsDay, DaysOverdue,  "
                        qry = qry + "   ServiceAddress, BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms,   PONo, ContactType, InvoiceBranch, SalesPerson, InvoicePreparedBy,  ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,  (1To30) as 1To30,  (31To60) as 31To60,"
                        qry = qry + " (61To90) as 61To90, (91To120) as 91To120, (121To150) as 121To150, (151To180) as 151To180, (GreaterThan180) as '>180' "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,  (1To30) as 1To30,  (31To60) as 31To60,"
                        qry = qry + " (61To90) as 61To90, (91To120) as 91To120, (121To150) as 121To150, (151To180) as 151To180, (GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If
                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0

                    Dim Total1to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0

                    Dim Total91to120 As Decimal = 0
                    Dim Total121to150 As Decimal = 0

                    'Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0



                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows


                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If

                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If

                        If dr.Item("91To120").ToString <> DBNull.Value.ToString Then
                            Total91to120 = Total91to120 + dr.Item("91To120")
                        End If

                        If dr.Item("121To150").ToString <> DBNull.Value.ToString Then
                            Total121to150 = Total121to150 + dr.Item("121To150")
                        End If

                        'If dr.Item("91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("91To150")
                        'End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90

                    drTotal.Item("91To120") = Total91to120
                    drTotal.Item("121To150") = Total121to150
                    'drTotal.Item("91To150") = Total91to150
                    drTotal.Item("151To180") = Total151to180

                   

                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        'qry = "SELECT Location, CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        'qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                        qry = "SELECT Location, CompanyGroup, AccountID,CustName AS AccountName,"
                        qry = qry + "sum(Current) as Current,sum(1To30) as 1To30,sum(31To60) as 31To60, sum(61To90) as 61To90, "
                        qry = qry + "sum(91To120) as 91To120, sum(121To150) as 121To150, sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    Else
                        'qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        'qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName,"
                        qry = qry + "sum(Current) as Current,sum(1To30) as 1To30,sum(31To60) as 31To60, sum(61To90) as 61To90, "
                        qry = qry + "sum(91To120) as 91To120, sum(121To150) as 121To150, sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If

                    '17.07.22
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup, Location"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    End If
                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"

                    '17.07.22

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0

                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0


                    Dim Total1to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0

                    Dim Total91to120 As Decimal = 0
                    Dim Total121to150 As Decimal = 0

                    'Dim Total91to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0


                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If

                        'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("1To10")
                        'End If

                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If

                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To120").ToString <> DBNull.Value.ToString Then
                            Total91to120 = Total91to120 + dr.Item("91To120")
                        End If

                        If dr.Item("121To150").ToString <> DBNull.Value.ToString Then
                            Total121to150 = Total121to150 + dr.Item("121To150")
                        End If

                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To120") = Total91to120
                    drTotal.Item("121To150") = Total121to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            ''''''''''''''''''''''''''''''''''''''''''''''''''''

            If chkIncludeCompanyGroupInfo.Checked = False Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        'qry = "SELECT Location as MasterBranch,  InvoiceBranch, AccountID,CustName AS AccountName, ContactType, "
                        'qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket, "
                        'qry = qry + " (Current) as Current, DaysOverdue, (1To30) as 1To30,  (31To60) as 31To60,"
                        'qry = qry + " (61To90) as 61To90, (91To120) as 91To120, (121To150) as 121To150, (151To180) as 151To180, (GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "

                        qry = "SELECT Location as MasterBranch,  ContractGroup, AccountID,CustName AS AccountName, invoicenumber,salesdate, (UnPaidBalance) as UnpaidBalance, AgeingBucket, TermsDay, DaysOverdue,  "
                        qry = qry + "   ServiceAddress, BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms,   PONo, ContactType, InvoiceBranch, SalesPerson, InvoicePreparedBy,  ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,  (1To30) as 1To30,  (31To60) as 31To60,"
                        qry = qry + " (61To90) as 61To90, (91To120) as 91To120, (121To150) as 121To150, (151To180) as 151To180, (GreaterThan180) as '>180' "



                        'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                        'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,  (1To30) as 1To30,  (31To60) as 31To60,"
                        qry = qry + " (61To90) as 61To90, (91To120) as 91To120, (121To150) as 121To150, (151To180) as 151To180, (GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                        'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If



                    'qry = "SELECT Location, AccountID,CustName AS AccountName, ContactType, "
                    'qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                    'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                    'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                    'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    Dim Total1to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to120 As Decimal = 0
                    Dim Total121to150 As Decimal = 0

                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows

                        'If String.IsNullOrEmpty(dr("InvoiceNumber")) = True Then
                        '    dr("InvoiceNumber").ToString()
                        'End If

                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("1To10")
                        'End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To120").ToString <> DBNull.Value.ToString Then
                            Total91to120 = Total91to120 + dr.Item("91To120")
                        End If

                        If dr.Item("121To150").ToString <> DBNull.Value.ToString Then
                            Total121to150 = Total121to150 + dr.Item("121To150")
                        End If

                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To120") = Total91to120
                    drTotal.Item("121To150") = Total121to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        'qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        'qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                        qry = "SELECT Location, AccountID,CustName AS AccountName,"
                        qry = qry + "sum(Current) as Current,sum(1To30) as 1To30, sum(31To60) as 31To60,sum(61To90) as 61To90,"
                        qry = qry + "sum(91To120) as 91To120,sum(121To150) as 121To150, sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        'qry = "SELECT  AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                        'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        'qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                        qry = "SELECT  AccountID,CustName AS AccountName,"
                        qry = qry + "sum(Current) as Current,sum(1To30) as 1To30, sum(31To60) as 31To60,sum(61To90) as 61To90,"
                        qry = qry + "sum(91To120) as 91To120,sum(121To150) as 121To150, sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    'qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                    'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    If ddlSalesMan.SelectedIndex > 0 Then
                        qry = qry + " and Salesperson = '" & ddlSalesMan.Text.Trim & "'"
                    End If


                    '17.07.22

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID,Location "
                    Else

                        qry = qry + "GROUP BY ACCOUNTID "
                    End If

                    '17.07.22
                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    'qry = qry + "GROUP BY ACCOUNTID "
                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    Dim Total1to30 As Decimal = 0
                    Dim Total31to60 As Decimal = 0
                    Dim Total61to90 As Decimal = 0
                    Dim Total91to120 As Decimal = 0
                    Dim Total121to150 As Decimal = 0
                    Dim Total151to180 As Decimal = 0
                    Dim Total180 As Decimal = 0


                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        'If dr.Item("1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("1To10")
                        'End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To60").ToString <> DBNull.Value.ToString Then
                            Total31to60 = Total31to60 + dr.Item("31To60")
                        End If
                        If dr.Item("61To90").ToString <> DBNull.Value.ToString Then
                            Total61to90 = Total61to90 + dr.Item("61To90")
                        End If
                        If dr.Item("91To120").ToString <> DBNull.Value.ToString Then
                            Total91to120 = Total91to120 + dr.Item("91To120")
                        End If
                        If dr.Item("121To150").ToString <> DBNull.Value.ToString Then
                            Total121to150 = Total121to150 + dr.Item("121To150")
                        End If
                        If dr.Item("151To180").ToString <> DBNull.Value.ToString Then
                            Total151to180 = Total151to180 + dr.Item("151To180")
                        End If
                        If dr.Item(">180").ToString <> DBNull.Value.ToString Then
                            Total180 = Total180 + dr.Item(">180")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To60") = Total31to60
                    drTotal.Item("61To90") = Total61to90
                    drTotal.Item("91To120") = Total91to120
                    drTotal.Item("121To150") = Total121to150
                    drTotal.Item("151To180") = Total151to180
                    drTotal.Item(">180") = Total180
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("AGEING - " + Session("UserID"), "Page_Load", ex.Message.ToString, lInvoiceNo)
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            'Return dt2
            'Exit Function
        End Try

    End Function

    Private Function getdatasetSenFormat4() As DataTable
        Dim lInvoiceNo As String
        lInvoiceNo = ""
        Try
            'Dim dt2 As New DataTable
            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection()
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry As String
            qry = ""

            If chkIncludeCompanyGroupInfo.Checked = True Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket,"
                        qry = qry + " (Current) as Current, DaysOverdue, (1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To365,(greaterThan365) as '>365',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,replace(replace(BillContact1Email, char(46), ' '), char(13), ' ') as BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,(1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To365,(greaterThan365) as '>365',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                     Dim Total181to365 As Decimal = 0
                    Dim Total365 As Decimal = 0

                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows


                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To365").ToString <> DBNull.Value.ToString Then
                            Total181to365 = Total181to365 + dr.Item("181To365")
                        End If
                        If dr.Item(">365").ToString <> DBNull.Value.ToString Then
                            Total365 = Total365 + dr.Item(">365")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    'drTotal.Item("1To10") = Total1to10
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To365") = Total181to365
                    drTotal.Item(">365") = Total365
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location, CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To365, sum(GreaterThan360) as '>365',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  CompanyGroup, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To365, sum(GreaterThan360) as '>365',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If


                    '17.07.22

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup, Location"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    End If
                    '17.07.22

                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"
                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0

                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                   Dim Total181to365 As Decimal = 0
                    Dim Total365 As Decimal = 0


                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To365").ToString <> DBNull.Value.ToString Then
                            Total181to365 = Total181to365 + dr.Item("181To365")
                        End If
                        If dr.Item(">365").ToString <> DBNull.Value.ToString Then
                            Total365 = Total365 + dr.Item(">365")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To365") = Total181to365
                    drTotal.Item(">365") = Total365
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            ''''''''''''''''''''''''''''''''''''''''''''''''''''

            If chkIncludeCompanyGroupInfo.Checked = False Then
                If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location as MasterBranch,  InvoiceBranch, AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ContractGroup, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, AgeingBucket,"
                        qry = qry + " (Current) as Current, DaysOverdue, (1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To365, (GreaterThan360) as '>365',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName, ContactType, "
                        qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                        qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                        qry = qry + " (Current) as Current,(1To30) as 1To30,(31To90) as 31To90,"
                        qry = qry + " (91To180) as 91To180,(181To270) as 181To365, (GreaterThan360) as '>365',(UnPaidBalance) as UnpaidBalance "
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If



                    'qry = "SELECT Location, AccountID,CustName AS AccountName, ContactType, "
                    'qry = qry + " invoicenumber,salesdate, ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,  "
                    'qry = qry + " CustAttention, CustTelephone,  Terms, TermsDay,  PONo, SalesPerson, InvoicePreparedBy, ValueBase, GSTbase, AppliedBase, CreditBase, ReceiptBase, "
                    'qry = qry + " (Current) as Current,(1To10) as 1To10,(11To30) as 11To30,"
                    'qry = qry + " (31To60) as 31To60,(61To90) as 61To90,(91To150) as 91To150,(151To180) as 151To180,(GreaterThan180) as '>180',(UnPaidBalance) as UnpaidBalance "
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + " group by accountIdAmount having  sum(unpaidbalance) <> 0 "
                    qry = qry + " Order BY ACCOUNTID"

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0


                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                    Dim Total181to365 As Decimal = 0
                    Dim Total365 As Decimal = 0


                    Dim svcaddr As String = ""
                    Dim billcontactperson As String = ""
                    Dim billmobile As String = ""
                    Dim billtel As String = ""
                    Dim billfax As String = ""
                    Dim billtel2 As String = ""
                    Dim billcontact1email As String = ""

                    For Each dr As DataRow In dt2.Rows

                        'If String.IsNullOrEmpty(dr("InvoiceNumber")) = True Then
                        '    dr("InvoiceNumber").ToString()
                        'End If

                        ''''''''''''''''''''''''''''
                        svcaddr = ""
                        If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                            Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                            lInvoiceNo = dr("InvoiceNumber").ToString()

                            cmdSalesDet.CommandType = CommandType.Text

                            'cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"
                            cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + lInvoiceNo + "' group by locationid"


                            cmdSalesDet.Connection = conn

                            Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                            Dim dtSalesDet As New DataTable
                            dtSalesDet.Load(drSalesDet)

                            If dtSalesDet.Rows.Count > 0 Then
                                For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                                    If String.IsNullOrEmpty(dr("ContactType")) = False Then
                                        If dr("ContactType") = "COMPANY" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)
                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If
                                            Else

                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        ElseIf dr("ContactType") = "PERSON" Then
                                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                                            cmdLoc.CommandType = CommandType.Text

                                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal,billcontact1svc as billcontactperson,billmobile1svc as billmobile,billtelephone1svc as billtelephone, billfax1svc as billfax,billtelephone12svc as billtelephone2,replace(replace(BillEmail1svc, char(10), ' '), char(13), ' ') as BillContact1Email from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                                            cmdLoc.Connection = conn

                                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                                            Dim dtLoc As New DataTable
                                            dtLoc.Load(drLoc)

                                            Dim addr As String = ""
                                            If dtLoc.Rows.Count > 0 Then
                                                addr = dtLoc.Rows(0)("address1").ToString.Trim + " " + dtLoc.Rows(0)("addbuilding").ToString.Trim + " " + dtLoc.Rows(0)("addstreet").ToString.Trim + " " + dtLoc.Rows(0)("addpostal").ToString.Trim

                                                If addr.Trim = "" Then
                                                    addr = "-"
                                                End If

                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                    billcontactperson = "-"
                                                Else
                                                    billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                    billmobile = "-"
                                                Else
                                                    billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                    billtel = "-"
                                                Else
                                                    billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                    billtel2 = "-"
                                                Else
                                                    billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                    billfax = "-"
                                                Else
                                                    billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                End If
                                                If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                    billcontact1email = "-"
                                                Else
                                                    billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                End If

                                                If j = 0 Then
                                                    svcaddr = addr

                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = "-"
                                                    Else
                                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = "-"
                                                    Else
                                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = "-"
                                                    Else
                                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = "-"
                                                    Else
                                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = "-"
                                                    Else
                                                        billfax = dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = "-"
                                                    Else
                                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                Else
                                                    svcaddr = svcaddr + "; " + addr
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                                        billcontactperson = billcontactperson + "-"
                                                    Else
                                                        billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                                        billmobile = billmobile + "-"
                                                    Else
                                                        billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                                        billtel = billtel + "-"
                                                    Else
                                                        billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                                        billtel2 = billtel2 + "-"
                                                    Else
                                                        billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                                        billfax = billfax + "-"
                                                    Else
                                                        billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString.Trim
                                                    End If
                                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                                        billcontact1email = billcontact1email + "-"
                                                    Else
                                                        billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString.Trim
                                                    End If
                                                End If


                                            End If

                                            dtLoc.Clear()
                                            drLoc.Close()
                                            dtLoc.Dispose()
                                            cmdLoc.Dispose()

                                        End If
                                    End If


                                Next
                            End If

                            dtSalesDet.Clear()
                            drSalesDet.Close()
                            dtSalesDet.Dispose()
                            cmdSalesDet.Dispose()

                        End If

                        ' svcaddr = svcaddr.Replace(Char(10),' ')

                        ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
                        ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")


                        dr("ServiceAddress") = Left(svcaddr.Trim, 1000)
                        dr("BillContactPerson") = Left(billcontactperson.Trim, 100)
                        dr("BillMobile") = Left(billmobile.Trim, 100)
                        dr("BillTelephone") = Left(billtel.Trim, 100)
                        dr("BillTelephone2") = Left(billtel2.Trim, 100)
                        dr("BillFax") = Left(billfax.Trim, 100)
                        dr("BillContact1Email") = Left(billcontact1email.Trim, 200)



                        ''''''''''''''''''''''''''''

                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To365").ToString <> DBNull.Value.ToString Then
                            Total181to365 = Total181to365 + dr.Item("181To365")
                        End If
                       
                        If dr.Item(">365").ToString <> DBNull.Value.ToString Then
                            Total365 = Total365 + dr.Item(">365")
                        End If


                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                   drTotal.Item("181To365") = Total181to365
                    drTotal.Item(">365") = Total365
                    dt2.Rows.Add(drTotal)


                    Return dt2
                ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandTimeout = 3000


                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To365, sum(GreaterThan360) as '>365',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    Else
                        qry = "SELECT  AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To30) as 1To30,sum(31To90) as 31To90,"
                        qry = qry + "sum(91To180) as 91To180,sum(181To270) as 181To365, sum(GreaterThan360) as '>365',sum(UnPaidBalance) as UnpaidBalance"
                        qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
                    End If


                    'qry = "SELECT Location, AccountID,CustName AS AccountName,sum(Current) as Current,sum(1To10) as 1To10,sum(11To30) as 11To30,"
                    'qry = qry + "sum(31To60) as 31To60,sum(61To90) as 61To90,sum(91To150) as 91To150,sum(151To180) as 151To180, sum(GreaterThan180) as '>180',sum(UnPaidBalance) as UnpaidBalance"
                    'qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + " and Location in (" & txtLocation.Text & ")"
                    End If

                    'qry = qry + "GROUP BY ACCOUNTID, CompanyGroup"

                    '17.07.22

                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "GROUP BY ACCOUNTID, Location"
                    Else
                        qry = qry + "GROUP BY ACCOUNTID"
                    End If
                    '17.07.22
                    'qry = qry + "GROUP BY ACCOUNTID "

                    command2.CommandText = qry
                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    Dim drTotal As DataRow = dt2.NewRow
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBalance As Decimal = 0
                    Dim TotalCurrent As Decimal = 0
                    'Dim Total1to10 As Decimal = 0
                    'Dim Total11to30 As Decimal = 0
                    'Dim Total31to60 As Decimal = 0
                    'Dim Total61to90 As Decimal = 0
                    'Dim Total91to150 As Decimal = 0
                    'Dim Total151to180 As Decimal = 0
                    'Dim Total180 As Decimal = 0

                    Dim Total1to30 As Decimal = 0
                    Dim Total31to90 As Decimal = 0
                    Dim Total91to180 As Decimal = 0
                   Dim Total181to365 As Decimal = 0
                    Dim Total365 As Decimal = 0

                    For Each dr As DataRow In dt2.Rows
                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        'If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        '    TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                        'End If
                        'If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        '    TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                        'End If
                        'If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        '    Total1to10 = Total1to10 + dr.Item("Total1To10")
                        'End If
                        'If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        '    Total11to30 = Total11to30 + dr.Item("Total11To30")
                        'End If
                        'If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        '    Total31to60 = Total31to60 + dr.Item("Total31To60")
                        'End If
                        'If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        '    Total61to90 = Total61to90 + dr.Item("Total61To90")
                        'End If
                        'If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        '    Total91to150 = Total91to150 + dr.Item("Total91To150")
                        'End If
                        'If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        '    Total151to180 = Total151to180 + dr.Item("Total151To180")
                        'End If
                        'If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        '    Total180 = Total180 + dr.Item("TotalGreaterThan180")
                        'End If



                        'If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        '    TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                        'End If
                        If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                            TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                        End If
                        If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                            TotalCurrent = TotalCurrent + dr.Item("Current")
                        End If
                        If dr.Item("1To30").ToString <> DBNull.Value.ToString Then
                            Total1to30 = Total1to30 + dr.Item("1To30")
                        End If
                        If dr.Item("31To90").ToString <> DBNull.Value.ToString Then
                            Total31to90 = Total31to90 + dr.Item("31To90")
                        End If
                        If dr.Item("91To180").ToString <> DBNull.Value.ToString Then
                            Total91to180 = Total91to180 + dr.Item("91To180")
                        End If
                        If dr.Item("181To365").ToString <> DBNull.Value.ToString Then
                            Total181to365 = Total181to365 + dr.Item("181To365")
                        End If
                        If dr.Item(">365").ToString <> DBNull.Value.ToString Then
                            Total365 = Total365 + dr.Item(">365")
                        End If
                    Next

                    drTotal.Item(0) = "GrandTotal"
                    'drTotal.Item("TotalOutstanding") = TotalInvoice
                    drTotal.Item("UnpaidBalance") = TotalBalance
                    drTotal.Item("Current") = TotalCurrent
                    drTotal.Item("1To30") = Total1to30
                    drTotal.Item("31To90") = Total31to90
                    drTotal.Item("91To180") = Total91to180
                    drTotal.Item("181To365") = Total181to365
                    drTotal.Item(">365") = Total365
                    dt2.Rows.Add(drTotal)
                    Return dt2
                End If
            End If  'end for Company Group

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("AGEING - " + Session("UserID"), "Page_Load", ex.Message.ToString, lInvoiceNo)
            'Return dt2
            'Exit Function
        End Try

    End Function

    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text

        cmd.Connection = conn
        '    Try
        conn.Open()
        sda.SelectCommand = cmd
        sda.Fill(dt)
        If chkCheckCutOff.Checked = True Then
            If dt.Rows.Count > 0 Then
                RecalculateBalance(dt, conn)

                dt.Clear()
                dt.Dispose()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Select * From (" + txtQuery.Text + ") as der where UnpaidBalance<>0"

                cmd.Connection = conn
                sda.SelectCommand = cmd
                sda.Fill(dt)


            End If
            'Retrieve Receipts where ReceiptDate greater than Sales date

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = txtQueryRecv.Text

            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                For i As Int16 = 0 To dt1.Rows.Count - 1
                    Dim drRecv As DataRow = dt.NewRow
                    drRecv.Item("ContactType") = dt1.Rows(i)("ContactType")
                    drRecv.Item("AccountID") = dt1.Rows(i)("AccountID")
                    drRecv.Item("CustName") = dt1.Rows(i)("ReceiptFrom")
                    drRecv.Item("InvoiceNumber") = dt1.Rows(i)("ReceiptNumber")
                    drRecv.Item("SalesDate") = dt1.Rows(i)("ReceiptDate")
                    drRecv.Item("ValueBase") = dt1.Rows(i)("ReceiptValueAmt")
                    drRecv.Item("GstBase") = dt1.Rows(i)("ReceiptGstAmt")
                    drRecv.Item("InvoiceAmount") = dt1.Rows(i)("ReceiptAmt")
                    drRecv.Item("UnpaidBalance") = dt1.Rows(i)("ReceiptAmt")

                    dt.Rows.Add(drRecv)

                Next


            End If

            command1.Dispose()
            dt1.Clear()
            dt1.Dispose()
            dr1.Close()

            'Retrieve Receipts which are not applied to invoice

            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            command2.CommandText = txtQueryRecv1.Text

            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                For i As Int16 = 0 To dt2.Rows.Count - 1
                    Dim drRecv As DataRow = dt.NewRow
                    drRecv.Item("ContactType") = dt2.Rows(i)("ContactType")
                    drRecv.Item("AccountID") = dt2.Rows(i)("AccountID")
                    drRecv.Item("CustName") = dt2.Rows(i)("ReceiptFrom")
                    drRecv.Item("InvoiceNumber") = dt2.Rows(i)("ReceiptNumber")
                    drRecv.Item("SalesDate") = dt2.Rows(i)("ReceiptDate")
                    drRecv.Item("ValueBase") = dt2.Rows(i)("ReceiptValueAmt")
                    drRecv.Item("GstBase") = dt2.Rows(i)("ReceiptGstAmt")
                    drRecv.Item("InvoiceAmount") = dt2.Rows(i)("ReceiptAmt")
                    drRecv.Item("UnpaidBalance") = dt2.Rows(i)("ReceiptAmt")

                    dt.Rows.Add(drRecv)
                Next


            End If
            command2.Dispose()
            dt2.Clear()
            dt2.Dispose()
            dr2.Close()
        End If



        '    dt.Columns.Add(New DataColumn("DueDays"))
        Dim col1 = dt.Columns.Add("Current")
        col1.SetOrdinal(17)
        Dim col2 = dt.Columns.Add("1-10 Days")
        col2.SetOrdinal(18)
        Dim col3 = dt.Columns.Add("11-30 Days")
        col3.SetOrdinal(19)
        Dim col4 = dt.Columns.Add("31-60 Days")
        col4.SetOrdinal(20)
        Dim col5 = dt.Columns.Add("61-90 Days")
        col5.SetOrdinal(21)
        Dim col6 = dt.Columns.Add("91-150 Days")
        col6.SetOrdinal(22)
        Dim col7 = dt.Columns.Add("151-180 Days")
        col7.SetOrdinal(23)
        Dim col8 = dt.Columns.Add(">180 Days")
        col8.SetOrdinal(24)
        Dim col9 = dt.Columns.Add("ServiceAddress")
        col9.SetOrdinal(25)

        'dt.Columns.Add(New DataColumn("1-10 Days"))
        'dt.Columns.Add(New DataColumn("11-30 Days"))
        'dt.Columns.Add(New DataColumn("31-60 Days"))
        'dt.Columns.Add(New DataColumn("61-90 Days"))
        'dt.Columns.Add(New DataColumn("91-150 Days"))
        'dt.Columns.Add(New DataColumn("151-180 Days"))
        'dt.Columns.Add(New DataColumn(">180 Days"))


        '  dt.Columns("Current").SetOrdinal(dt.Columns.IndexOf("BillContactPerson"))
        '   dt.Columns("<30 Days").SetOrdinal(dt.Columns.IndexOf("BillContactPerson"))
        'dt.Columns("31-60 Days").SetOrdinal(19)
        'dt.Columns("61-90 Days").SetOrdinal(20)
        'dt.Columns("91-150 Days").SetOrdinal(21)
        'dt.Columns("151-180 Days").SetOrdinal(22)
        'dt.Columns(">180 Days").SetOrdinal(23)

        Dim DueDays As Int16
        Dim svcaddr As String = ""

        For Each dr As DataRow In dt.Rows



            If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                cmdSalesDet.CommandType = CommandType.Text

                cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"


                cmdSalesDet.Connection = conn

                Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                Dim dtSalesDet As New DataTable
                dtSalesDet.Load(drSalesDet)

                If dtSalesDet.Rows.Count > 0 Then
                    svcaddr = ""
                    For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
                        If dr("ContactType") = "COMPANY" Then
                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                            cmdLoc.CommandType = CommandType.Text

                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal from tblcompanyLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"


                            cmdLoc.Connection = conn

                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                            Dim dtLoc As New DataTable
                            dtLoc.Load(drLoc)

                            If dtLoc.Rows.Count > 0 Then
                                If svcaddr = "" Then
                                    svcaddr = dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                Else
                                    svcaddr = svcaddr + "; " + dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                End If

                            End If

                            dtLoc.Clear()
                            drLoc.Close()
                            dtLoc.Dispose()
                            cmdLoc.Dispose()

                        ElseIf dr("ContactType") = "PERSON" Then
                            Dim cmdLoc As MySqlCommand = New MySqlCommand

                            cmdLoc.CommandType = CommandType.Text

                            cmdLoc.CommandText = "Select replace(replace(address1, char(10), ' '), char(13), ' ') as address1,replace(replace(addbuilding, char(10), ' '), char(13), ' ') as addbuilding,replace(replace(addstreet, char(10), ' '), char(13), ' ') as addstreet,replace(replace(addpostal, char(10), ' '), char(13), ' ') as addpostal from tblpersonLocation where locationid='" + dtSalesDet.Rows(j)("Locationid").ToString + "'"

                            cmdLoc.Connection = conn

                            Dim drLoc As MySqlDataReader = cmdLoc.ExecuteReader()
                            Dim dtLoc As New DataTable
                            dtLoc.Load(drLoc)

                            If dtLoc.Rows.Count > 0 Then
                                If svcaddr = "" Then
                                    svcaddr = dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                Else
                                    svcaddr = svcaddr + "; " + dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                End If
                            End If

                            dtLoc.Clear()
                            drLoc.Close()
                            dtLoc.Dispose()
                            cmdLoc.Dispose()

                        End If



                    Next
                End If

                dtSalesDet.Clear()
                drSalesDet.Close()
                dtSalesDet.Dispose()
                cmdSalesDet.Dispose()

            End If

            ' svcaddr = svcaddr.Replace(Char(10),' ')

            ' dr("ServiceAddress") =  replace(replace(svcaddr, char(10), ' '), char(13), ' ')
            ' svcaddr = svcaddr.Replace(";", "<br style=""mso-data-placement:same-cell;"" />")
            dr("ServiceAddress") = svcaddr

            If dr("DueDate").ToString <> DBNull.Value.ToString Then
                DueDays = (Convert.ToDateTime(txtPrintDate.Text) - Convert.ToDateTime(dr("DueDate"))).Days

            End If

            If DueDays <= 0 Then
                '  dr("Current") = dr("ValueBase") + dr("GstBase") - dr("creditBase") - dr("receiptBase")
                dr("Current") = dr("UnpaidBalance")
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0

            ElseIf DueDays > 0 And DueDays <= 10 Then
                dr("1-10 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 10 And DueDays <= 30 Then
                dr("11-30 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 30 And DueDays <= 60 Then
                dr("31-60 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 60 And DueDays <= 90 Then
                dr("61-90 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 90 And DueDays <= 150 Then
                dr("91-150 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("151-180 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 150 And DueDays <= 180 Then
                dr("151-180 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr(">180 Days") = 0
            ElseIf DueDays > 180 Then
                dr(">180 Days") = dr("UnpaidBalance")
                dr("Current") = 0
                dr("1-10 Days") = 0
                dr("11-30 Days") = 0
                dr("31-60 Days") = 0
                dr("61-90 Days") = 0
                dr("91-150 Days") = 0
                dr("151-180 Days") = 0

            End If
        Next




        If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

            Dim drTotal As DataRow = dt.NewRow
            Dim TotalInvoice As Decimal = 0
            Dim TotalBalance As Decimal = 0
            Dim TotalCurrent As Decimal = 0
            Dim Total1to10 As Decimal = 0
            Dim Total11to30 As Decimal = 0
            Dim Total31to60 As Decimal = 0
            Dim Total61to90 As Decimal = 0
            Dim Total91to150 As Decimal = 0
            Dim Total151to180 As Decimal = 0
            Dim Total180 As Decimal = 0
            Dim TotalCredit As Decimal = 0
            Dim TotalReceipt As Decimal = 0

            For Each dr As DataRow In dt.Rows
                If dr.Item("InvoiceAmount").ToString <> DBNull.Value.ToString Then
                    TotalInvoice = TotalInvoice + dr.Item("InvoiceAmount")
                End If
                If dr.Item("CreditBase").ToString <> DBNull.Value.ToString Then
                    TotalCredit = TotalCredit + dr.Item("CreditBase")
                End If
                If dr.Item("ReceiptBase").ToString <> DBNull.Value.ToString Then
                    TotalReceipt = TotalReceipt + dr.Item("ReceiptBase")
                End If
                If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
                    TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
                End If
                If dr.Item("Current").ToString <> DBNull.Value.ToString Then
                    TotalCurrent = TotalCurrent + dr.Item("Current")
                End If
                If dr.Item("1-10 Days").ToString <> DBNull.Value.ToString Then
                    Total1to10 = Total1to10 + dr.Item("1-10 Days")
                End If
                If dr.Item("11-30 Days").ToString <> DBNull.Value.ToString Then
                    Total11to30 = Total11to30 + dr.Item("11-30 Days")
                End If
                If dr.Item("31-60 Days").ToString <> DBNull.Value.ToString Then
                    Total31to60 = Total31to60 + dr.Item("31-60 Days")
                End If
                If dr.Item("61-90 Days").ToString <> DBNull.Value.ToString Then
                    Total61to90 = Total61to90 + dr.Item("61-90 Days")
                End If
                If dr.Item("91-150 Days").ToString <> DBNull.Value.ToString Then
                    Total91to150 = Total91to150 + dr.Item("91-150 Days")
                End If
                If dr.Item("151-180 Days").ToString <> DBNull.Value.ToString Then
                    Total151to180 = Total151to180 + dr.Item("151-180 Days")
                End If
                If dr.Item(">180 Days").ToString <> DBNull.Value.ToString Then
                    Total180 = Total180 + dr.Item(">180 Days")
                End If
            Next

            drTotal.Item(0) = "Total"
            drTotal.Item("InvoiceAmount") = TotalInvoice
            drTotal.Item("CreditBase") = TotalCredit
            drTotal.Item("ReceiptBase") = TotalReceipt
            drTotal.Item("UnpaidBalance") = TotalBalance
            drTotal.Item("Current") = TotalCurrent
            drTotal.Item("1-10 Days") = Total1to10
            drTotal.Item("11-30 Days") = Total11to30
            drTotal.Item("31-60 Days") = Total31to60
            drTotal.Item("61-90 Days") = Total61to90
            drTotal.Item("91-150 Days") = Total91to150
            drTotal.Item("151-180 Days") = Total151to180
            drTotal.Item(">180 Days") = Total180
            dt.Rows.Add(drTotal)


            Return dt
        ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

            Dim command0 As MySqlCommand = New MySqlCommand

            command0.CommandType = CommandType.Text
            command0.CommandText = "delete from tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"
            command0.Connection = conn

            command0.ExecuteNonQuery()

            If dt.Rows.Count > 0 Then
                For i As Int64 = 0 To dt.Rows.Count - 1
                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    command.CommandText = "INSERT INTO tblrptosageingsoa(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"

                    command.Parameters.Clear()


                    command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
                    command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountId"))
                    command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
                    command.Parameters.AddWithValue("@CustAttention", dt.Rows(i)("CustAttention"))
                    command.Parameters.AddWithValue("@CustTelephone", dt.Rows(i)("CustTelephone"))
                    command.Parameters.AddWithValue("@StaffId", dt.Rows(i)("StaffId"))
                    command.Parameters.AddWithValue("@InvoiceNumber", dt.Rows(i)("InvoiceNumber"))
                    command.Parameters.AddWithValue("@SalesDate", dt.Rows(i)("SalesDate"))
                    command.Parameters.AddWithValue("@Terms", dt.Rows(i)("Terms"))
                    command.Parameters.AddWithValue("@TermsDay", dt.Rows(i)("TermsDay"))
                    command.Parameters.AddWithValue("@DueDate", dt.Rows(i)("DueDate"))
                    command.Parameters.AddWithValue("@ValueBase", dt.Rows(i)("ValueBase"))
                    command.Parameters.AddWithValue("@GstBase", dt.Rows(i)("GstBase"))
                    command.Parameters.AddWithValue("@AppliedBase", dt.Rows(i)("InvoiceAmount"))
                    command.Parameters.AddWithValue("@CreditBase", dt.Rows(i)("CreditBase"))
                    command.Parameters.AddWithValue("@ReceiptBase", dt.Rows(i)("ReceiptBase"))
                    command.Parameters.AddWithValue("@UnPaidBalance", dt.Rows(i)("UnPaidBalance"))
                    command.Parameters.AddWithValue("@Current", dt.Rows(i)("Current"))
                    command.Parameters.AddWithValue("@1To10", dt.Rows(i)("1-10 Days"))
                    command.Parameters.AddWithValue("@11To30", dt.Rows(i)("11-30 Days"))
                    command.Parameters.AddWithValue("@31To60", dt.Rows(i)("31-60 Days"))
                    command.Parameters.AddWithValue("@61To90", dt.Rows(i)("61-90 Days"))
                    command.Parameters.AddWithValue("@91To150", dt.Rows(i)("91-150 Days"))
                    command.Parameters.AddWithValue("@151To180", dt.Rows(i)("151-180 Days"))
                    command.Parameters.AddWithValue("@GreaterThan180", dt.Rows(i)(">180 Days"))
                    command.Parameters.AddWithValue("@ServiceAddress", "")
                    command.Parameters.AddWithValue("@BillContactPerson", "")
                    command.Parameters.AddWithValue("@BillTelephone", "")
                    command.Parameters.AddWithValue("@BillFax", "")
                    command.Parameters.AddWithValue("@BillTelephone2", "")
                    command.Parameters.AddWithValue("@BillMobile", "")
                    command.Parameters.AddWithValue("@BillContact1Email", "")
                    command.Parameters.AddWithValue("@CompanyGroup", "")
                    command.Parameters.AddWithValue("@ContractGroup", "")
                    command.Parameters.AddWithValue("@BillingPeriod", "")
                    command.Parameters.AddWithValue("@InvoiceType", "")
                    command.Parameters.AddWithValue("@SalesPerson", "")
                    command.Parameters.AddWithValue("@TotOutstanding", 0)
                    command.Parameters.AddWithValue("@TotDue", 0)
                    command.Parameters.AddWithValue("@DaysOverDue", DBNull.Value)
                    command.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session("UserID")))
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@LastModifiedBy", DBNull.Value)
                    command.Parameters.AddWithValue("@LastModifiedOn", DBNull.Value)
                    command.Parameters.AddWithValue("@AmtReceivable", DBNull.Value)
                    command.Parameters.AddWithValue("@VoucherNumber", DBNull.Value)
                    command.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                    command.Parameters.AddWithValue("@GLCode", DBNull.Value)
                    command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
                    command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


                    command.Connection = conn

                    command.ExecuteNonQuery()

                Next

                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                Dim qry As String = "SELECT AccountID,CustName AS AccountName,sum(AppliedBase) as TotalOutstanding,sum(Current) as TotalCurrent,sum(1To10) as Total1To10,sum(11To30) as Total11To30,"
                qry = qry + "sum(31To60) as Total31To60,sum(61To90) as Total61To90,sum(91To150) as Total91To150,sum(151To180) as Total151To180,sum(GreaterThan180) as TotalGreaterThan180,sum(UnPaidBalance) as TotalUnpaidBalance"

                qry = qry + " FROM tblrptosageingsoa where CreatedBy='" + Convert.ToString(Session("UserID")) + "' GROUP BY ACCOUNTID"

                command2.CommandText = qry
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                Dim drTotal As DataRow = dt2.NewRow
                Dim TotalInvoice As Decimal = 0
                Dim TotalBalance As Decimal = 0
                Dim TotalCurrent As Decimal = 0
                Dim Total1to10 As Decimal = 0
                Dim Total11to30 As Decimal = 0
                Dim Total31to60 As Decimal = 0
                Dim Total61to90 As Decimal = 0
                Dim Total91to150 As Decimal = 0
                Dim Total151to180 As Decimal = 0
                Dim Total180 As Decimal = 0


                For Each dr As DataRow In dt2.Rows
                    If dr.Item("TotalOutstanding").ToString <> DBNull.Value.ToString Then
                        TotalInvoice = TotalInvoice + dr.Item("TotalOutstanding")
                    End If
                    If dr.Item("TotalUnpaidBalance").ToString <> DBNull.Value.ToString Then
                        TotalBalance = TotalBalance + dr.Item("TotalUnpaidBalance")
                    End If
                    If dr.Item("TotalCurrent").ToString <> DBNull.Value.ToString Then
                        TotalCurrent = TotalCurrent + dr.Item("TotalCurrent")
                    End If
                    If dr.Item("Total1To10").ToString <> DBNull.Value.ToString Then
                        Total1to10 = Total1to10 + dr.Item("Total1To10")
                    End If
                    If dr.Item("Total11To30").ToString <> DBNull.Value.ToString Then
                        Total11to30 = Total11to30 + dr.Item("Total11To30")
                    End If
                    If dr.Item("Total31To60").ToString <> DBNull.Value.ToString Then
                        Total31to60 = Total31to60 + dr.Item("Total31To60")
                    End If
                    If dr.Item("Total61To90").ToString <> DBNull.Value.ToString Then
                        Total61to90 = Total61to90 + dr.Item("Total61To90")
                    End If
                    If dr.Item("Total91To150").ToString <> DBNull.Value.ToString Then
                        Total91to150 = Total91to150 + dr.Item("Total91To150")
                    End If
                    If dr.Item("Total151To180").ToString <> DBNull.Value.ToString Then
                        Total151to180 = Total151to180 + dr.Item("Total151To180")
                    End If
                    If dr.Item("TotalGreaterThan180").ToString <> DBNull.Value.ToString Then
                        Total180 = Total180 + dr.Item("TotalGreaterThan180")
                    End If
                Next

                drTotal.Item(0) = "GrandTotal"
                drTotal.Item("TotalOutstanding") = TotalInvoice
                drTotal.Item("TotalUnpaidBalance") = TotalBalance
                drTotal.Item("TotalCurrent") = TotalCurrent
                drTotal.Item("Total1To10") = Total1to10
                drTotal.Item("Total11To30") = Total11to30
                drTotal.Item("Total31To60") = Total31to60
                drTotal.Item("Total61To90") = Total61to90
                drTotal.Item("Total91To150") = Total91to150
                drTotal.Item("Total151To180") = Total151to180
                drTotal.Item("TotalGreaterThan180") = Total180
                dt2.Rows.Add(drTotal)

                Return dt2
            End If


        End If

        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    conn.Close()
        '    sda.Dispose()
        '    conn.Dispose()
        'End Try
    End Function

    Private Sub SetColumnsOrder(table As DataTable, ParamArray columnNames As [String]())
        Dim columnIndex As Integer = 0
        For Each columnName In columnNames
            table.Columns(columnName).SetOrdinal(columnIndex)
            columnIndex += 1
        Next
    End Sub

    Private Sub RecalculateBalance(dt As DataTable, conn As MySqlConnection)
        Dim acctid As String = ""
        Dim lTotalReceipt As Decimal
        Dim lInvoiceAmount As Decimal
        Dim lTotalcn As Decimal
        Dim lTotalRecvcn As Decimal
        Dim lTotalJournalAmt As Decimal
        Dim lbalance As Decimal
        Dim invno As String = ""
        Dim cnno As String = ""
        Dim recno As String = ""
        For i As Long = 0 To dt.Rows.Count - 1

            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalRecvcn = 0.0
            lTotalcn = 0.0
            lTotalJournalAmt = 0.0
            lbalance = 0.0
            invno = dt.Rows(i)("InvoiceNumber").ToString.Trim

            Try

                'Retrieve Invoice Amount
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT AppliedBase,balancebase,doctype FROM tblSales where InvoiceNumber = '" & invno & "'"
                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then

                    '  If dt1.Rows(0)("DocType") = "ARIN" Then
                    lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString

                    'End If



                End If
                If dt1.Rows(0)("DocType") = "ARIN" Then
                    'Retrieve CN and DN Amount

                    Dim command2 As MySqlCommand = New MySqlCommand
                    command2.CommandType = CommandType.Text

                    command2.CommandText = "SELECT  a.invoicenumber as cnno,ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblSalesDetail A, tblSales B WHERE " & _
                      "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and B.SalesDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'" & _
                    "and A.SourceInvoice = '" & invno & "'"

                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    If dt2.Rows.Count > 0 Then
                        lTotalcn = dt2.Rows(0)("totalcn").ToString

                        If String.IsNullOrEmpty(dt2.Rows(0)("cnno").ToString) = False Then
                            cnno = dt2.Rows(0)("cnno").ToString


                            '        'Retrieve records for which the CN is not assigned to any invoice

                            '        Dim command32 As MySqlCommand = New MySqlCommand
                            '        command32.CommandType = CommandType.Text

                            '        command32.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as OthersCNAmt FROM tblSalesDetail A, tblSales B WHERE " & _
                            '  "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' OR B.DocType='ARDN')  and B.PostStatus = 'P' and B.SalesDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'" & _
                            '"and A.InvoiceNumber = '" & cnno & "' and A.RefType=''"
                            '        command32.Connection = conn

                            '        Dim dr32 As MySqlDataReader = command32.ExecuteReader()
                            '        Dim dt32 As New DataTable
                            '        dt32.Load(dr32)

                            '        If dt32.Rows.Count > 0 Then
                            '            ' lTotalRecvcn = dt32.Rows(0)("totalrecvcn").ToString
                            '            lTotalcn = lTotalcn + dt32.Rows(0)("OthersCNAmt")

                            '        Else

                            '        End If

                            '        dt32.Clear()
                            '        dt32.Dispose()
                            '        dr32.Close()
                            '        command32.Dispose()

                            'Retrieve Receipt Amount for CreditNote

                            Dim command31 As MySqlCommand = New MySqlCommand
                            command31.CommandType = CommandType.Text

                            command31.CommandText = "SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalrecvcn FROM tblRecvDet A, tblRecv B WHERE " & _
                             "A.ReceiptNumber=B.ReceiptNumber and B.PostStatus = 'P' AND " & _
                             "B.receiptdate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and A.RefType = '" & dt2.Rows(0)("cnno") & "'"
                            command31.Connection = conn

                            Dim dr31 As MySqlDataReader = command31.ExecuteReader()
                            Dim dt31 As New DataTable
                            dt31.Load(dr31)



                            If dt31.Rows.Count > 0 Then
                                '     lTotalcn = lTotalcn - dt31.Rows(0)("totalrecvcn")
                                If dt31.Rows(0)("totalrecvcn") <> 0 Then
                                    lTotalcn = 0

                                End If

                                ' recno = dt31.Rows(0)("recno").ToString


                                '        'Retrieve records for which the Receipt is not assigned to any invoice

                                '        Dim command33 As MySqlCommand = New MySqlCommand
                                '        command33.CommandType = CommandType.Text

                                '        command33.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as OthersRecCNAmt FROM tblRecvDet A, tblRecv B WHERE " & _
                                '  "A.ReceiptNumber=B.ReceiptNumber and B.PostStatus = 'P' and B.receiptDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'" & _
                                '"and A.ReceiptNumber = '" & recno & "' and A.RefType=''"
                                '        command33.Connection = conn

                                '        Dim dr33 As MySqlDataReader = command33.ExecuteReader()
                                '        Dim dt33 As New DataTable
                                '        dt33.Load(dr33)

                                '        If dt33.Rows.Count > 0 Then
                                '            lTotalRecvcn = dt31.Rows(0)("totalrecvcn").ToString + dt33.Rows(0)("OthersRecCNAmt")

                                '        Else

                                '        End If

                                '        dt33.Clear()
                                '        dt33.Dispose()
                                '        dr33.Close()
                                '        command33.Dispose()

                            Else
                                lTotalRecvcn = 0
                                '  cnno = invno
                            End If

                            dt31.Clear()
                            dt31.Dispose()
                            dr31.Close()
                            command31.Dispose()

                        Else
                            '   cnno = invno

                        End If

                    End If

                    'Retrieve Receipt Amount for Invoice


                    Dim command3 As MySqlCommand = New MySqlCommand
                    command3.CommandType = CommandType.Text

                    command3.CommandText = "SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalrecv FROM tblRecvDet A, tblRecv B WHERE " & _
                     "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN' and B.PostStatus = 'P' AND " & _
                     "B.receiptdate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and A.RefType = '" & invno & "'"
                    command3.Connection = conn

                    Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                    Dim dt3 As New DataTable
                    dt3.Load(dr3)

                    If dt3.Rows.Count > 0 Then
                        lTotalReceipt = dt3.Rows(0)("totalrecv").ToString
                    End If


                    'Retrieve Adjustment/Journal Amount

                    Dim command4 As MySqlCommand = New MySqlCommand
                    command4.CommandType = CommandType.Text

                    command4.CommandText = "SELECT  ifnull(SUM(ifnull(A.DebitBase,0)),0) as debitbase, ifnull(SUM(ifnull(A.CreditBase,0)),0) as creditbase  FROM tbljrnvdet A, tbljrnv B WHERE " & _
                       "A.VoucherNumber=B.VoucherNumber AND  B.PostStatus = 'P' and B.JournalDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and A.RefType = '" & invno & "' "

                    command4.Connection = conn

                    Dim dr4 As MySqlDataReader = command4.ExecuteReader()
                    Dim dt4 As New DataTable
                    dt4.Load(dr4)

                    If dt4.Rows.Count > 0 Then
                        lTotalJournalAmt = dt4.Rows(0)("debitbase").ToString - dt4.Rows(0)("creditbase").ToString
                        'If dt4.Rows(0)("debitbase").ToString > 0.0 Then
                        '    lTotalReceipt = dt4.Rows(0)("debitbase").ToString
                        'Else
                        '    lTotalReceipt = dt4.Rows(0)("creditbase").ToString
                        'End If
                    End If

                    'Calculate Balance amount and update

                    lbalance = lInvoiceAmount + lTotalcn - lTotalReceipt + lTotalJournalAmt
                    lTotalcn = lTotalcn + lTotalJournalAmt

                    Dim command21 As MySqlCommand = New MySqlCommand
                    command21.CommandType = CommandType.Text

                    command21.CommandText = "UPDATE tblSales SET OPeriodBalance = '" & lbalance & "', OPeriodPaid='" & lTotalReceipt & "',OPeriodCredit='" & lTotalcn & "' WHERE InvoiceNumber = '" & invno & "'"
                    command21.Connection = conn

                    command21.ExecuteNonQuery()

                    dt1.Clear()
                    dt1.Dispose()
                    dr1.Close()
                    command1.Dispose()

                    dt2.Clear()
                    dt2.Dispose()
                    dr2.Close()
                    command2.Dispose()

                    dt3.Clear()
                    dt3.Dispose()
                    dr3.Close()
                    command3.Dispose()


                    dt4.Clear()
                    dt4.Dispose()
                    dr4.Close()
                    command4.Dispose()

                    command21.Dispose()
                ElseIf dt1.Rows(0)("DocType") = "ARCN" Or dt1.Rows(0)("DocType") = "ARDN" Then
                    ''Retrieve CN and DN Amount

                    'Dim command2 As MySqlCommand = New MySqlCommand
                    'command2.CommandType = CommandType.Text

                    'command2.CommandText = "SELECT  a.invoicenumber as cnno,ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn,b.balancebase ad balance FROM tblSalesDetail A, tblSales B WHERE " & _
                    '  "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and B.SalesDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'" & _
                    '"and B.Balancebase <> 0"

                    'command2.Connection = conn

                    'Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    'Dim dt2 As New DataTable
                    'dt2.Load(dr2)

                    'If dt2.Rows.Count > 0 Then
                    '    lTotalcn = dt2.Rows(0)("totalcn").ToString
                    'End If


                    Dim command21 As MySqlCommand = New MySqlCommand
                    command21.CommandType = CommandType.Text

                    command21.CommandText = "UPDATE tblSales SET OPeriodBalance = '" & dt1.Rows(0)("balancebase").ToString & "', OPeriodPaid=0,OPeriodCredit=0 WHERE InvoiceNumber = '" & invno & "'"
                    command21.Connection = conn

                    command21.ExecuteNonQuery()


                    command21.Dispose()

                End If

                'Catch ex As Exception
                '    lblAlert.Text = ex.Message.ToString + " " + recno
                '    InsertIntoTblWebEventLog("RecalculateBalance", ex.Message.ToString, invno)
                'End Try
             Catch ex As Exception
                InsertIntoTblWebEventLog("RecalculateBalance - " + Session("UserID"), "btnEditBillingDetailsSave_Click", ex.Message.ToString, invno)
            End Try
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
        Catch ex As Exception

        End Try
    End Sub

    Public Sub InsertIntoTblAgeingEventlog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblageingeventlog(LoginId, Event, Error,ID, CreatedOn)"
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RetrieveSelectionCriteria()
        Dim selection As String = ""

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
            If String.IsNullOrEmpty(txtLocation.Text) = False Then
                If selection = "" Then
                    selection = "SelectLocation : " + txtLocation.Text
                Else
                    selection = selection + ", SelectLocation : " + txtLocation.Text
                End If
            End If
        End If
        If ddlInvoiceType.Text = "-1" Then
        Else
            If selection = "" Then
                selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
            Else
                selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
            End If
        End If
        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                '  lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                '  Return False
            End If
              If selection = "" Then
                selection = "Accounting Period >= " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Accounting Period >= " + d.ToString("yyyyMM")
            End If

        End If

        If String.IsNullOrEmpty(txtAcctPeriodTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodTo.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Accounting Period To is invalid", "str")
                ' lblAlert.Text = "ACCOUNTING PERIOD TO IS INVALID"
                'Return False
            End If
              If selection = "" Then
                selection = "Accounting Period <= " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Accounting Period <= " + d.ToString("yyyyMM")
            End If
        End If
        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
             
            End If
               If selection = "" Then
                selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
             
            End If
               If selection = "" Then
                selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtDueDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            If selection = "" Then
                selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtDueDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            If selection = "" Then
                selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            If selection = "" Then
                selection = "Ledger Code >= " + txtGLFrom.Text
            Else
                selection = selection + ", Ledger Code >= " + txtGLFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtGLTo.Text) = False Then
             If selection = "" Then
                selection = "Ledger Code <= " + txtGLTo.Text
            Else
                selection = selection + ", Ledger Code <= " + txtGLTo.Text
            End If
        End If
        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
             If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If
        If ddlSalesMan.Text = "-1" Then
        Else
            If selection = "" Then
                selection = "SalesMan : " + ddlSalesMan.Text
            Else
                selection = selection + ", SalesMan : " + ddlSalesMan.Text
            End If
        End If
        If ddlAccountType.Text = "-1" Then
        Else
             If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
        End If
        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
             If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If
        If ddlLocateGrp.Text = "-1" Then
        Else
            If selection = "" Then
                selection = "Location Group : " + ddlLocateGrp.Text
            Else
                selection = selection + ", Location Group : " + ddlLocateGrp.Text
            End If
        End If
        If ddlTerms.Text = "-1" Then
        Else
            If selection = "" Then
                selection = "Terms : " + ddlTerms.Text
            Else
                selection = selection + ", Status : " + ddlTerms.Text
            End If
        End If
        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
             If selection = "" Then
                selection = "GLStatus : " + txtGLStatus.Text
            Else
                selection = selection + ", GLStatus : " + txtGLStatus.Text
            End If
        End If
        If String.IsNullOrEmpty(txtPrintDate.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            If selection = "" Then
                selection = "PrintDate = " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", PrintDate = " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If chkUnpaidBal.Checked = True Then
            If selection = "" Then
                selection = "Include Unpaid Bal  < 0"
            Else
                selection = selection + ", Include Unpaid Bal<0"
            End If
        End If


        If chkCheckCutOff.Checked = True Then
            If String.IsNullOrEmpty(txtCutOffDate.Text) Then
               Else

                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                
                End If
                If selection = "" Then
                    selection = "CutOffDate = " + d.ToString("dd-MM-yyyy")
                Else
                    selection = selection + ", CutOffDate = " + d.ToString("dd-MM-yyyy")
                End If

            End If
        End If

        If chkIncludeCompanyGroupInfo.Checked = True Then
            If selection = "" Then
                selection = "Include Company Group Info"
            Else
                selection = selection + ", Include Company Group Info"
            End If
        End If

        If ddlContractGroup.SelectedIndex > 1 Then
            selection = selection + ", Contract Group : " + ddlContractGroup.Text.Trim
        End If
        Session.Add("selection", selection)

    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            lblAlert.Text = ""


            If chkCheckCutOff.Checked = False Then
                lblAlert.Text = "Please Select Cut-off Date"
                Exit Sub
            End If


            If chkCheckCutOff.Checked = True Then
                If String.IsNullOrEmpty(txtCutOffDate.Text) Then
                    lblAlert.Text = "ENTER CUTOFF DATE IF CUTOFF DATE IS CHECKED"
                    Return
                Else

                    Dim d As DateTime
                    If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                        '  lblAlert.Text = "INVALID START DATE"
                        Return
                    End If
                    'txtPrintDate.Text = txtCutOffDate.Text

                End If
            End If

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                If String.IsNullOrEmpty(txtLocation.Text) = True Then
                    lblAlert.Text = "Please Select Location"
                    Exit Sub
                End If
            End If

            ' Locked?


            Dim connLock As MySqlConnection = New MySqlConnection()
            Dim commandIsLock As MySqlCommand = New MySqlCommand

            connLock.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLock.Open()

            commandIsLock.CommandType = CommandType.Text
            commandIsLock.CommandText = "select StartDateTime, RunBy from tbllock where  ReportName ='SOA-Report' and Status ='Locked'"
            commandIsLock.Connection = connLock

            Dim drLock As MySqlDataReader = commandIsLock.ExecuteReader()
            Dim dtLock As New DataTable
            dtLock.Load(drLock)

            If dtLock.Rows.Count > 0 Then
                If Convert.ToDateTime(dtLock.Rows(0)("StartDateTime").ToString).AddMinutes(15) <= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") Then
                    UnLockReport()
                    LockReport()
                Else
                    lblAlert.Text = "This Report is currently running. Please wait until the report completes before you can run. " & vbNewLine & "Last run by " & dtLock.Rows(0)("RunBy").ToString
                    connLock.Close()
                    connLock.Dispose()
                    Exit Sub
                End If
            Else
                LockReport()
            End If


            Dim str1 As String = ""
            Dim str2 As String = ""
            Dim str3 As String = ""
            str1 = "`tbwSOArptar_" & Session("UserID") & "`"
            str2 = "`tbwSOArptar1_" & Session("UserID") & "`"
            str3 = "`tbwsoarptaccountidar_" & Session("UserID") & "`"

            str1 = str1.Replace(".", "")
            str2 = str2.Replace(".", "")
            str3 = str3.Replace(".", "")

            'Locked? 
            Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()

            If ddlContractGroup.SelectedIndex = 0 Then

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "1", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                RetrieveSelectionCriteria()
                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "2", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)


                ' ''''''''''''''''''''''''''''''''''' SEN
                ''SP()
                'Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                'conn.Open()

                'Start :07.03.20

                Dim command As MySqlCommand = New MySqlCommand
                command.CommandType = CommandType.StoredProcedure
                command.CommandTimeout = 3000

                'InsertIntoTblWebEventLog("Start Calc : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)
                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "3", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'If String.IsNullOrEmpty(txtAccountID.Text) Then
                'InsertIntoTblWebEventLog("SaveTbwARDetail1 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "4", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'command.CommandText = "SaveTbwARDetail1"
                command.CommandText = "SaveTbwARDetail1SOANew"

                command.Parameters.Clear()
                command.CommandTimeout = 3000
                command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                If ddlAccountType.Text = "-1" Then
                    command.Parameters.AddWithValue("@pr_AccountType", "")
                Else
                    command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
                End If

                command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountID.Text.Trim)
                command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountID.Text.Trim)

                'If ddlSalesMan.SelectedIndex > 0 Then
                '    command.Parameters.AddWithValue("@pr_SalesManFrom", ddlSalesMan.Text.Trim)
                '    command.Parameters.AddWithValue("@pr_SalesManTo", ddlSalesMan.Text.Trim)
                'Else
                '    command.Parameters.AddWithValue("@pr_SalesManFrom", "")
                '    command.Parameters.AddWithValue("@pr_SalesManTo", "")
                'End If

                'command.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                'command.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                'command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))



                command.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                command.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)

                'command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report")

                If rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-4")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-4")
                End If

                'Else
                '    InsertIntoTblWebEventLog("SaveTbwARDetail1AccountID : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                '    command.CommandText = "SaveTbwARDetail1AccountID"
                '    command.Parameters.Clear()

                '    command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                '    command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                '    command.Parameters.AddWithValue("@pr_AccountID", txtAccountID.Text)
                'End If


                command.Connection = conn
                command.ExecuteScalar()
                'conn.Close()
                'conn.Dispose()
                command.Dispose()

                'InsertIntoTblWebEventLog("Select tbwar : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "5", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'Exit Sub


                ''''''''''''''''''''''''''''''''''''''''''''' Blank Reftype
                'If rbtnSelectDetSumm.SelectedIndex = 0 Then  ' 06.01.18
                Dim sqlstr, sqlstr1, lDocumentNo, lAccountId As String
                sqlstr = ""
                sqlstr1 = ""
                lAccountId = ""

                Dim lRcno As Long = 0

                'sqlstr = "SELECT DocumentNo, RefType, Rcno FROM tbwar where ((RefType ='') or (RefType is null)) and Accountid = '10000037'"
                sqlstr = "SELECT AccountId, DocumentNo, RefType, Rcno FROM tbwar where (trim((RefType) ='') or (RefType is null)) and CreatedBy = '" & Session("UserID") & "' order by AccountId, DocumentNo"
                Dim command2 As MySqlCommand = New MySqlCommand

                'Dim conn As MySqlConnection = New MySqlConnection()
                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'conn.Open()

                command2.CommandType = CommandType.Text
                command2.CommandText = sqlstr
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                InsertIntoTblWebEventLog("Selected tbwar : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                If dt2.Rows.Count > 0 Then

                    InsertIntoTblWebEventLog("RecCount > 0 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    For j = 0 To dt2.Rows.Count - 1
                        Dim command3 As MySqlCommand = New MySqlCommand
                        lDocumentNo = dt2.Rows(j)("DocumentNo").ToString().Trim
                        lRcno = dt2.Rows(j)("Rcno").ToString()
                        lAccountId = dt2.Rows(j)("AccountId").ToString()

                        sqlstr1 = "SELECT count(RefType) as lCount FROM tbwar where CreatedBy = '" & Session("UserID") & "' and RefType='" & lDocumentNo.Trim & "'"

                        command3.CommandType = CommandType.Text
                        command3.CommandText = sqlstr1
                        command3.Connection = conn

                        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                        Dim dt3 As New DataTable
                        dt3.Load(dr3)

                        If dt3.Rows(0)("lCount").ToString() > 0 Then
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text
                            Dim qry As String = "Update tbwar set RefTYpe = '" & lDocumentNo & "' where CreatedBy = '" & Session("UserID") & "' and  Rcno =" & lRcno
                            command4.CommandText = qry
                            command4.Parameters.Clear()
                            command4.Connection = conn
                            command4.ExecuteNonQuery()
                            command4.Dispose()
                        Else
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text
                            Dim qry As String = "Update tbwar set RefTYpe = 'X-" & lDocumentNo & "', Contra = 'X-" & lDocumentNo & "' where CreatedBy = '" & Session("UserID") & "' and  Rcno =" & lRcno
                            command4.CommandText = qry
                            command4.Parameters.Clear()
                            command4.Connection = conn
                            command4.ExecuteNonQuery()
                            command4.Dispose()
                        End If
                        command3.Dispose()
                        'j = j + 1
                    Next
                    'dt2.Rows(0)("Category").ToString()
                    'txtCategoryID.Text = dt2.Rows(0)("Category").ToString()
                End If
                'conn.Close()
                'conn.Dispose()

                dt2.Dispose()

                'InsertIntoTblWebEventLog("SaveTbwARDetail2 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "6", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                '''''''''''''''''''''''''''''''''''''''''''''''''''

                'If rbtnSelectFormat.SelectedIndex = 0 Then
                Dim command5 As MySqlCommand = New MySqlCommand
                command5.CommandType = CommandType.StoredProcedure
                'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                'command5.CommandText = "SaveTbwARDetail2"
                command5.CommandText = "SaveTbwARDetail2SOANew"
                command5.CommandTimeout = 3000
                command5.Parameters.Clear()

                'command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                'command5.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                'command5.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                'command5.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))


                command5.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                command5.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                command5.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)


                'If rbtnSelectFormat.SelectedIndex = 0 Then
                '    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report")
                'ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                '    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-2")
                'ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                '    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-3")
                'End If

                If rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-4")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-4")
                End If



                command5.Connection = conn

                command5.ExecuteScalar()
                'conn.Close()
                'conn.Dispose()
                command5.Dispose()
                'End If

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "7", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'If rbtnSelectFormat.SelectedIndex = 1 Then
                '    Dim command5 As MySqlCommand = New MySqlCommand
                '    command5.CommandType = CommandType.StoredProcedure
                '    'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                '    command5.CommandText = "SaveTbwARDetail3"
                '    command5.Parameters.Clear()

                '    command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                '    command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                '    command5.Connection = conn
                '    command5.ExecuteScalar()
                '    'conn.Close()
                '    'conn.Dispose()
                '    command5.Dispose()
                'End If
                'InsertIntoTblWebEventLog("Start getdatasetSen : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'End If  ' 06.01.18

                'conn.Close()
                'conn.Dispose()
                ''''''''''''''''''''''''''''''''''''''''''

                'End :07.03.20

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "8", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                Dim dt As DataTable
                If rbtnSelectFormat.SelectedIndex = 0 Then
                    dt = getdatasetSen()
                ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                    dt = getdatasetSenFormat2()
                ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                    dt = getdatasetSenFormat3()
                ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
                    dt = getdatasetSenFormat4()
                End If


                ''''''''''''''''''''''''''''''''''''''
                'InsertIntoTblWebEventLog("End getdatasetSen : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "9", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'Dim commandDeleteFromTemp As MySqlCommand = New MySqlCommand
                'commandDeleteFromTemp.CommandType = CommandType.StoredProcedure
                ''If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                'commandDeleteFromTemp.CommandText = "DeleteTbwARDetail1and2"
                'commandDeleteFromTemp.Parameters.Clear()

                ''commandDeleteFromTemp.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                'commandDeleteFromTemp.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                'commandDeleteFromTemp.Connection = conn
                'commandDeleteFromTemp.ExecuteScalar()

                'commandDeleteFromTemp.Dispose()

                ''''''''''''

                If rbtnSelectFormat.SelectedIndex = 0 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format1_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format1_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If



                If rbtnSelectFormat.SelectedIndex = 1 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format2_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format2_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If

                If rbtnSelectFormat.SelectedIndex = 2 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format3_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                        WriteExcelWithNPOI3(dt, "xlsx", attachment)
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format3_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                        WriteExcelWithNPOI(dt, "xlsx", attachment)
                    End If
                    'WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If
                'conn.Close()
                'conn.Dispose()

                If rbtnSelectFormat.SelectedIndex = 3 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format4_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format4_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If

            ElseIf ddlContractGroup.SelectedIndex > 0 Then  'Added on 09.04.20

                RetrieveSelectionCriteria()


                ' ''''''''''''''''''''''''''''''''''' SEN
                ''SP()
                'Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                'conn.Open()

                'Start :07.03.20

                Dim command As MySqlCommand = New MySqlCommand
                command.CommandType = CommandType.StoredProcedure


                InsertIntoTblWebEventLog("Start Calc : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)


                'If String.IsNullOrEmpty(txtAccountID.Text) Then
                InsertIntoTblWebEventLog("SaveTbwARDetail1 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'command.CommandText = "SaveTbwARDetail1ContractGroup"
                command.CommandText = "SaveTbwARDetail1SOAContractGroupNew"
                command.Parameters.Clear()
                command.CommandTimeout = 600
                command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                command.Parameters.AddWithValue("@pr_ContractGroup", ddlContractGroup.Text.Trim)

                'command.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                'command.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                'command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))


                command.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                command.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)

                If ddlAccountType.Text = "-1" Then
                    command.Parameters.AddWithValue("@pr_AccountType", "")
                Else
                    command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
                End If

                command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountID.Text.Trim)
                command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountID.Text.Trim)

                'If rbtnSelectFormat.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report")
                'ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                '    command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-2")
                'ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                '    command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-3")
                'End If


                If rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 0 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Detail Ageing-Report-Format-4")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 And rbtnSelectDetSumm.SelectedIndex = 1 Then
                    command.Parameters.AddWithValue("@pr_ReportName", "Summary Ageing-Report-Format-4")
                End If

                'command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report")
                'ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                '    command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-2")
                'ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                '    command.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-3")
                'End If
                'Else
                '    InsertIntoTblWebEventLog("SaveTbwARDetail1AccountID : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                '    Command.CommandText = "SaveTbwARDetail1AccountIDContractGroup"
                '    Command.Parameters.Clear()

                '    Command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                '    Command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                '    Command.Parameters.AddWithValue("@pr_AccountID", txtAccountID.Text)
                '    Command.Parameters.AddWithValue("@pr_ContractGroup", ddlContractGroup.Text.Trim)
                'End If


                command.Connection = conn
                command.ExecuteScalar()
                'conn.Close()
                'conn.Dispose()
                command.Dispose()

                InsertIntoTblWebEventLog("Select tbwar : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)


                ''''''''''''''''''''''''''''''''''''''''''''' Blank Reftype
                'If rbtnSelectDetSumm.SelectedIndex = 0 Then  ' 06.01.18
                Dim sqlstr, sqlstr1, lDocumentNo, lAccountId As String
                sqlstr = ""
                sqlstr1 = ""
                lAccountId = ""

                Dim lRcno As Long = 0

                'sqlstr = "SELECT DocumentNo, RefType, Rcno FROM tbwar where ((RefType ='') or (RefType is null)) and Accountid = '10000037'"
                sqlstr = "SELECT AccountId, DocumentNo, RefType, Rcno FROM tbwar where (trim((RefType) ='') or (RefType is null)) and CreatedBy = '" & Session("UserID") & "' order by AccountId, DocumentNo"
                Dim command2 As MySqlCommand = New MySqlCommand

                'Dim conn As MySqlConnection = New MySqlConnection()
                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'conn.Open()

                command2.CommandType = CommandType.Text
                command2.CommandText = sqlstr
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                InsertIntoTblWebEventLog("Selected tbwar : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                If dt2.Rows.Count > 0 Then

                    InsertIntoTblWebEventLog("RecCount > 0 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    For j = 0 To dt2.Rows.Count - 1
                        Dim command3 As MySqlCommand = New MySqlCommand
                        lDocumentNo = dt2.Rows(j)("DocumentNo").ToString().Trim
                        lRcno = dt2.Rows(j)("Rcno").ToString()
                        lAccountId = dt2.Rows(j)("AccountId").ToString()

                        sqlstr1 = "SELECT count(RefType) as lCount FROM tbwar where CreatedBy = '" & Session("UserID") & "' and RefType='" & lDocumentNo.Trim & "'"

                        command3.CommandType = CommandType.Text
                        command3.CommandText = sqlstr1
                        command3.Connection = conn

                        Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                        Dim dt3 As New DataTable
                        dt3.Load(dr3)

                        If dt3.Rows(0)("lCount").ToString() > 0 Then
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text
                            Dim qry As String = "Update tbwar set RefTYpe = '" & lDocumentNo & "' where CreatedBy = '" & Session("UserID") & "' and  Rcno =" & lRcno
                            command4.CommandText = qry
                            command4.Parameters.Clear()
                            command4.Connection = conn
                            command4.ExecuteNonQuery()
                            command4.Dispose()
                        Else
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text
                            Dim qry As String = "Update tbwar set RefTYpe = 'X-" & lDocumentNo & "', Contra = 'X-" & lDocumentNo & "' where CreatedBy = '" & Session("UserID") & "' and  Rcno =" & lRcno
                            command4.CommandText = qry
                            command4.Parameters.Clear()
                            command4.Connection = conn
                            command4.ExecuteNonQuery()
                            command4.Dispose()
                        End If
                        command3.Dispose()
                        'j = j + 1
                    Next
                    'dt2.Rows(0)("Category").ToString()
                    'txtCategoryID.Text = dt2.Rows(0)("Category").ToString()
                End If
                'conn.Close()
                'conn.Dispose()

                dt2.Dispose()

                InsertIntoTblWebEventLog("SaveTbwARDetail2 : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                '''''''''''''''''''''''''''''''''''''''''''''''''''

                'If rbtnSelectFormat.SelectedIndex = 0 Then
                Dim command5 As MySqlCommand = New MySqlCommand
                command5.CommandType = CommandType.StoredProcedure
                'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                command5.CommandText = "SaveTbwARDetail2SOAContractGroupNew"
                command5.Parameters.Clear()

                command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                command5.Parameters.AddWithValue("@pr_ContractGroup", ddlContractGroup.Text.Trim)

                'command5.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                'command5.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                'command5.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))

                command5.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                command5.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                command5.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)

                If rbtnSelectFormat.SelectedIndex = 0 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report")
                ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-2")
                ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-3")
                ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
                    command5.Parameters.AddWithValue("@pr_ReportName", "Ageing-Report-Format-4")
                End If
                command5.Connection = conn
                command5.ExecuteScalar()
                'conn.Close()
                'conn.Dispose()
                command5.Dispose()
                'End If

                'If rbtnSelectFormat.SelectedIndex = 1 Then
                '    Dim command5 As MySqlCommand = New MySqlCommand
                '    command5.CommandType = CommandType.StoredProcedure
                '    'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                '    command5.CommandText = "SaveTbwARDetail3ContractGroup"
                '    command5.Parameters.Clear()

                '    command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                '    command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                '    command5.Parameters.AddWithValue("@pr_ContractGroup", ddlContractGroup.Text.Trim)
                '    command5.Connection = conn
                '    command5.ExecuteScalar()
                '    'conn.Close()
                '    'conn.Dispose()
                '    command5.Dispose()
                'End If
                'InsertIntoTblWebEventLog("Start getdatasetSen : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                'End If  ' 06.01.18

                'conn.Close()
                'conn.Dispose()
                ''''''''''''''''''''''''''''''''''''''''''

                'End :07.03.20


                Dim dt As DataTable
                If rbtnSelectFormat.SelectedIndex = 0 Then
                    dt = getdatasetSen()
                ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                    dt = getdatasetSenFormat2()
                ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                    dt = getdatasetSenFormat3()
                ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
                    dt = getdatasetSenFormat4()
                End If


                ''''''''''''''''''''''''''''''''''''''
                InsertIntoTblWebEventLog("End getdatasetSen : " + Session("UserID"), "btnExportToExcel", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)


                Dim commandDeleteFromTemp As MySqlCommand = New MySqlCommand
                commandDeleteFromTemp.CommandType = CommandType.StoredProcedure
                'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then

                commandDeleteFromTemp.CommandText = "DeleteTbwARDetail1and2"
                commandDeleteFromTemp.Parameters.Clear()

                'commandDeleteFromTemp.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                commandDeleteFromTemp.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                commandDeleteFromTemp.Connection = conn
                commandDeleteFromTemp.ExecuteScalar()

                commandDeleteFromTemp.Dispose()

                ''''''''''''

                If rbtnSelectFormat.SelectedIndex = 0 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format1_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format1_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If



                If rbtnSelectFormat.SelectedIndex = 1 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format2_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format2_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return


                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If


                If rbtnSelectFormat.SelectedIndex = 2 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format3_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format3_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If

                If rbtnSelectFormat.SelectedIndex = 3 Then
                    Dim attachment As String

                    If rbtnSelectDetSumm.SelectedIndex = 0 Then
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateDetail_format4_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    Else
                        attachment = "attachment; filename=OutstandingInvoiceByDueDateSummary_format4_" & Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyyMMdd") & ".xls"
                    End If
                    WriteExcelWithNPOI(dt, "xlsx", attachment)
                    Return

                    Response.ClearContent()
                    Response.AddHeader("content-disposition", attachment)
                    Response.ContentType = "application/vnd.ms-excel"
                    Dim tab As String = ""
                    Response.Write(Convert.ToString(Session("Selection")))
                    Response.Write(vbLf)
                    Response.Write(vbLf)

                    For Each dc As DataColumn In dt.Columns
                        Response.Write(tab + dc.ColumnName)
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                    Dim i As Integer
                    For Each dr As DataRow In dt.Rows
                        tab = ""
                        For i = 0 To dt.Columns.Count - 1
                            Response.Write(tab & dr(i).ToString())
                            tab = vbTab
                        Next
                        Response.Write(vbLf)
                    Next
                    Response.[End]()

                    dt.Clear()
                End If

                'conn.Close()
                'conn.Dispose()

                'UnLockReport()
            End If

            ''''''''''''''''''''''''''''''''''' SEN
            conn.Close()
            conn.Dispose()

            UnLockReport()
            Exit Sub

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            UnLockReport()
            InsertIntoTblWebEventLog("OSDUE_RPT - " + Session("UserID"), "btnExportToExcel", ex.Message.ToString, ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)
            'conn.Close()
            'conn.Dispose()
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
            commandLock.Parameters.AddWithValue("@pr_ReportName", "SOA-Report")
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
            commandUnLock.Parameters.AddWithValue("@ReportName", "SOA-Report")
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
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ddlInvoiceType.SelectedIndex = 0
        txtAcctPeriodFrom.Text = ""
        txtAcctPeriodTo.Text = ""
        txtInvDateFrom.Text = ""
        txtInvDateTo.Text = ""
        txtGLFrom.Text = ""
        txtGLTo.Text = ""
        txtDueDateFrom.Text = ""
        txtDueDateTo.Text = ""

        txtIncharge.Text = ""
        ddlSalesMan.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        ddlCompanyGrp.SelectedIndex = 0

        ddlLocateGrp.SelectedIndex = 0

        ddlTerms.SelectedIndex = 0
        txtGLStatus.Text = ""
        chkCheckCutOff.Checked = False
        txtCutOffDate.Text = ""

        chkUnpaidBal.Checked = False
        txtPrintDate.Text = Convert.ToString(Session("SysDate"))
        lblAlert.Text = ""
    End Sub

    Protected Sub chkCheckCutOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckCutOff.CheckedChanged
        If chkCheckCutOff.Checked = True Then
            txtCutOffDate.Enabled = True
        Else
            txtCutOffDate.Enabled = False

        End If
    End Sub

    Protected Sub btnClientName_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientName.Click
        If String.IsNullOrEmpty(ddlAccountType.Text) Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If
        If ddlAccountType.Text = "-1" Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If


        If String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            txtPopUpClient.Text = txtCustName.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        ElseIf String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        Else
            ' txtPopUpClient.Text = ""
            ' txtPopupClientSearch.Text = ""
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        End If
        mdlPopUpClient.Show()

    End Sub

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String, attachment As String)
        Try
            Dim workbook As IWorkbook

            If extension = "xlsx" Then
                workbook = New XSSFWorkbook()
            ElseIf extension = "xls" Then
                workbook = New HSSFWorkbook()
            Else
                Throw New Exception("This format is not supported")
            End If

            Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

            'Add Selection Criteria

            Dim row1 As IRow = sheet1.CreateRow(0)
            Dim cell1 As ICell = row1.CreateCell(0)
            cell1.SetCellValue(Session("Selection").ToString)
            cell1.CellStyle.WrapText = True
            Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
            sheet1.AddMergedRegion(cra)

            'Add Column Heading
            row1 = sheet1.CreateRow(1)

            Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
            testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
            testeStyle.FillPattern = FillPattern.SolidForeground
            '  testeStyle.FillForegroundColor = IndexedColors.White.Index
            testeStyle.Alignment = HorizontalAlignment.Center

            Dim RowFont As IFont = workbook.CreateFont()
            RowFont.Color = IndexedColors.White.Index
            RowFont.IsBold = True

            testeStyle.SetFont(RowFont)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row1.CreateCell(j)
                '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

                Dim columnName As String = dt.Columns(j).ToString()
                cell.SetCellValue(columnName)
                ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
                cell.CellStyle = testeStyle

            Next

            'Add details
            Dim _doubleCellStyle As ICellStyle = Nothing

            If _doubleCellStyle Is Nothing Then
                _doubleCellStyle = workbook.CreateCellStyle()
                _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
            End If

            Dim _intCellStyle As ICellStyle = Nothing

            If _intCellStyle Is Nothing Then
                _intCellStyle = workbook.CreateCellStyle()
                _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
            End If

            Dim dateCellStyle As ICellStyle = Nothing

            If dateCellStyle Is Nothing Then
                dateCellStyle = workbook.CreateCellStyle()
                dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
            End If

            Dim AllCellStyle As ICellStyle = Nothing

            AllCellStyle = workbook.CreateCellStyle()
            AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            'Format-1
            If rbtnSelectFormat.SelectedIndex = 0 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 21 And j <= 34 Then
                                    'If j >= 26 And j <= 39 Then '20.01.24
                                    'If j >= 27 And j <= 40 Then  '27.07.25
                                    'If j = 27 And (j >= 29 And j <= 41) Then  '27.07.25
                                    If j = 28 And (j >= 30 And j <= 42) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 33 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 4 And j <= 12 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 33 Then
                                    'If j >= 24 And j <= 37 Then
                                    'If j >= 25 And j <= 38 Then  '20.01.24
                                    'If j >= 26 And j <= 39 Then  '27.07.25
                                    'If j = 26 And (j >= 28 And j <= 40) Then  '27.07.25
                                    If j = 27 And (j >= 29 And j <= 41) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)  'Error
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            InsertIntoTblWebEventLog("Aging", "WriteExcel", "Test", Session("UserID").ToString)

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    '  If j >= 19 Or j <= 32 Then
                                    If j >= 19 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If

                'Format 2
            ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 34 Then '20.01.24
                                    'If j >= 23 And j <= 35 Then  '27.07.25
                                    'If j = 23 And (j >= 25 And j <= 36) Then  '27.07.25
                                    If j = 24 And (j >= 26 And j <= 37) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 4 Or j <= 12 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else

                            InsertIntoTblWebEventLog("Aging", "WriteExcel", dt.Rows.Count.ToString, dt.Columns.Count.ToString)


                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 3 Or j <= 11 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 32 Then
                                    'If j >= 21 And j <= 33 Then '20.01.24
                                    'If j >= 22 And j <= 34 Then  '27.07.25
                                    'If j = 22 And (j >= 24 And j <= 35) Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 36) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 19 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 9 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If


                '07.08.21
                '  Format-3
            ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 35 Then
                                    'If j >= 23 And j <= 36 Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 37) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 33 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 4 And j <= 12 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 33 Then
                                    'If j >= 21 And j <= 34 Then '20.01.24
                                    'If j >= 22 And j <= 35 Then  '27.07.25
                                    If j = 22 And (j >= 24 And j <= 36) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            InsertIntoTblWebEventLog("Aging", "WriteExcel", "Test", Session("UserID").ToString)

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    '  If j >= 19 Or j <= 32 Then
                                    If j >= 19 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If
                '07.08.21

                'Format 4
            ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 33 Then '20.01.24
                                    'If j >= 23 And j <= 34 Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 35) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 4 Or j <= 12 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else

                            InsertIntoTblWebEventLog("Aging", "WriteExcel", dt.Rows.Count.ToString, dt.Columns.Count.ToString)


                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 3 Or j <= 11 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 32 Then
                                    'If j >= 21 And j <= 32 Then '20.01.24
                                    'If j >= 22 And j <= 33 Then  '27.07.25
                                    If j = 22 And (j >= 24 And j <= 34) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 9 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If

            End If



            Using exportData = New MemoryStream()
                Response.Clear()
                workbook.Write(exportData)
                '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
                '  Dim attachment As String = "attachment; filename=ActiveContract"


                If extension = "xlsx" Then
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                    Response.BinaryWrite(exportData.ToArray())
                ElseIf extension = "xls" Then
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                    Response.BinaryWrite(exportData.GetBuffer())
                End If

                Response.[End]()
            End Using

        Catch ex As Exception
            InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "1", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)
            lblAlert.Text = ex.Message.ToString
        End Try

    End Sub



    '03.08.25

    Public Sub WriteExcelWithNPOI3(ByVal dt As DataTable, ByVal extension As String, attachment As String)
        Try
            Dim workbook As IWorkbook

            If extension = "xlsx" Then
                workbook = New XSSFWorkbook()
            ElseIf extension = "xls" Then
                workbook = New HSSFWorkbook()
            Else
                Throw New Exception("This format is not supported")
            End If

            Dim sheet1 As ISheet = workbook.CreateSheet("Sheet1")

            'Add Selection Criteria

            Dim row1 As IRow = sheet1.CreateRow(0)
            Dim cell1 As ICell = row1.CreateCell(0)
            cell1.SetCellValue(Session("Selection").ToString)
            cell1.CellStyle.WrapText = True
            Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8)
            sheet1.AddMergedRegion(cra)

            'Add Column Heading
            row1 = sheet1.CreateRow(1)

            Dim testeStyle As ICellStyle = workbook.CreateCellStyle()
            testeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            testeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            testeStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index
            testeStyle.FillPattern = FillPattern.SolidForeground
            '  testeStyle.FillForegroundColor = IndexedColors.White.Index
            testeStyle.Alignment = HorizontalAlignment.Center

            Dim RowFont As IFont = workbook.CreateFont()
            RowFont.Color = IndexedColors.White.Index
            RowFont.IsBold = True

            testeStyle.SetFont(RowFont)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row1.CreateCell(j)
                '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

                Dim columnName As String = dt.Columns(j).ToString()
                cell.SetCellValue(columnName)
                ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
                cell.CellStyle = testeStyle

            Next

            'Add details
            Dim _doubleCellStyle As ICellStyle = Nothing

            If _doubleCellStyle Is Nothing Then
                _doubleCellStyle = workbook.CreateCellStyle()
                _doubleCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                _doubleCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
            End If

            Dim _intCellStyle As ICellStyle = Nothing

            If _intCellStyle Is Nothing Then
                _intCellStyle = workbook.CreateCellStyle()
                _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
            End If

            Dim dateCellStyle As ICellStyle = Nothing

            If dateCellStyle Is Nothing Then
                dateCellStyle = workbook.CreateCellStyle()
                dateCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
                dateCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

                dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-mm-yyyy")
            End If

            Dim AllCellStyle As ICellStyle = Nothing

            AllCellStyle = workbook.CreateCellStyle()
            AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            'Format-1
            If rbtnSelectFormat.SelectedIndex = 0 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 21 And j <= 34 Then
                                    'If j >= 26 And j <= 39 Then '20.01.24
                                    'If j >= 27 And j <= 40 Then  '27.07.25
                                    'If j = 27 And (j >= 29 And j <= 41) Then  '27.07.25
                                    If j = 28 And (j >= 30 And j <= 42) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 33 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 4 And j <= 12 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 33 Then
                                    'If j >= 24 And j <= 37 Then
                                    'If j >= 25 And j <= 38 Then  '20.01.24
                                    'If j >= 26 And j <= 39 Then  '27.07.25
                                    'If j = 26 And (j >= 28 And j <= 40) Then  '27.07.25
                                    If j = 27 And (j >= 29 And j <= 41) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)  'Error
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            InsertIntoTblWebEventLog("Aging", "WriteExcel", "Test", Session("UserID").ToString)

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    '  If j >= 19 Or j <= 32 Then
                                    If j >= 19 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If

                'Format 2
            ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 34 Then '20.01.24
                                    'If j >= 23 And j <= 35 Then  '27.07.25
                                    'If j = 23 And (j >= 25 And j <= 36) Then  '27.07.25
                                    If j = 24 And (j >= 26 And j <= 37) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 4 Or j <= 12 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else

                            InsertIntoTblWebEventLog("Aging", "WriteExcel", dt.Rows.Count.ToString, dt.Columns.Count.ToString)


                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 3 Or j <= 11 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 32 Then
                                    'If j >= 21 And j <= 33 Then '20.01.24
                                    'If j >= 22 And j <= 34 Then  '27.07.25
                                    'If j = 22 And (j >= 24 And j <= 35) Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 36) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 19 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 9 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If


                '07.08.21
                '  Format-3
            ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 35 Then
                                    'If j >= 23 And j <= 36 Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 37) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                        'ElseIf j = 7 Then
                                    ElseIf j = 6 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 33 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 4 And j <= 12 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 33 Then
                                    'If j >= 21 And j <= 34 Then '20.01.24
                                    'If j >= 22 And j <= 35 Then  '27.07.25
                                    'If j = 22 And (j >= 24 And j <= 36) Then  '27.07.25
                                    If j = 6 And (j >= 25 And j <= 37) Then  '03.08.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle



                                        'ElseIf j = 6 Then '20.01.24
                                    ElseIf j = 5 Then '03.08.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            InsertIntoTblWebEventLog("Aging", "WriteExcel", "Test", Session("UserID").ToString)

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    '  If j >= 19 Or j <= 32 Then
                                    If j >= 19 And j <= 32 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 4 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 11 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If
                '07.08.21

                'Format 4
            ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
                If chkIncludeCompanyGroupInfo.Checked = True Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 22 And j <= 33 Then '20.01.24
                                    'If j >= 23 And j <= 34 Then  '27.07.25
                                    If j = 23 And (j >= 25 And j <= 35) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 7 Then '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next


                        Else

                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next

                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 4 Or j <= 12 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else

                            InsertIntoTblWebEventLog("Aging", "WriteExcel", dt.Rows.Count.ToString, dt.Columns.Count.ToString)


                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 3 Or j <= 11 Then
                                    '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    '        cell.SetCellValue(d)
                                    '    Else
                                    '        Dim d As Double = Convert.ToDouble("0.00")
                                    '        cell.SetCellValue(d)

                                    '    End If
                                    '    cell.CellStyle = _doubleCellStyle

                                    'Else
                                    cell.SetCellValue(dt.Rows(i)(j).ToString)
                                    cell.CellStyle = AllCellStyle

                                    'End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If

                ElseIf chkIncludeCompanyGroupInfo.Checked = False Then
                    If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    'If j >= 20 And j <= 32 Then
                                    'If j >= 21 And j <= 32 Then '20.01.24
                                    'If j >= 22 And j <= 33 Then  '27.07.25
                                    If j = 22 And (j >= 24 And j <= 34) Then  '27.07.25
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 6 Then  '20.01.24
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 20 And j <= 31 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle


                                    ElseIf j = 5 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                                            cell.SetCellValue(d)
                                            'Else
                                            '    Dim d As Double = Convert.ToDouble("0.00")
                                            '    cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = dateCellStyle
                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If

                    ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then

                        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 3 And j <= 10 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        Else
                            For i As Integer = 0 To dt.Rows.Count - 1
                                Dim row As IRow = sheet1.CreateRow(i + 2)

                                For j As Integer = 0 To dt.Columns.Count - 1
                                    Dim cell As ICell = row.CreateCell(j)

                                    If j >= 2 And j <= 9 Then
                                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                            cell.SetCellValue(d)
                                        Else
                                            Dim d As Double = Convert.ToDouble("0.00")
                                            cell.SetCellValue(d)

                                        End If
                                        cell.CellStyle = _doubleCellStyle

                                    Else
                                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                                        cell.CellStyle = AllCellStyle

                                    End If
                                    If i = dt.Rows.Count - 1 Then
                                        sheet1.AutoSizeColumn(j)
                                    End If
                                Next
                            Next
                        End If
                    End If
                End If

            End If



            Using exportData = New MemoryStream()
                Response.Clear()
                workbook.Write(exportData)
                '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
                '  Dim attachment As String = "attachment; filename=ActiveContract"


                If extension = "xlsx" Then
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                    Response.BinaryWrite(exportData.ToArray())
                ElseIf extension = "xls" Then
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", String.Format(attachment + ".xls"))
                    Response.BinaryWrite(exportData.GetBuffer())
                End If

                Response.[End]()
            End Using

        Catch ex As Exception
            InsertIntoTblAgeingEventlog("SaveTbwARDetail1 : " + Session("UserID"), "1", "", ddlInvoiceType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountID.Text & " : " & rbtnSelectDetSumm.SelectedIndex)
            lblAlert.Text = ex.Message.ToString
        End Try

    End Sub

    '03.08.25

    Protected Sub btnEmailOSInv_Click(sender As Object, e As EventArgs) Handles btnEmailOSInv.Click
        'Try
        lblAlert.Text = ""


        If chkCheckCutOff.Checked = False Then
            lblAlert.Text = "Please Select Cut-off Date"
            Exit Sub
        End If


        If chkCheckCutOff.Checked = True Then
            If String.IsNullOrEmpty(txtCutOffDate.Text) Then
                lblAlert.Text = "ENTER CUTOFF DATE IF CUTOFF DATE IS CHECKED"
                Return
            Else

                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                    '  lblAlert.Text = "INVALID START DATE"
                    Return
                End If
                'txtPrintDate.Text = txtCutOffDate.Text

            End If
        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            If String.IsNullOrEmpty(txtLocation.Text) = True Then
                lblAlert.Text = "Please Select Location"
                Exit Sub
            End If
        End If

        RetrieveSelectionCriteria()

        ' Locked?
        Dim conn As New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry1 As String = "INSERT INTO tblautoemailreport(AccountIDFrom,AccountIDTo,DocumentType,CreatedBy,CreatedOn,"
        qry1 = qry1 + "Module,Generated,BatchNo,FileType,Selection,Selformula,RetryCount,DomainName,Distribution,ContractGroup,"
        qry1 = qry1 + "Branch,CutOffDate,"
        qry1 = qry1 + "InvoiceType,Location,PeriodFrom,PeriodTo,InvDateFrom,InvDateTo,DueDateFrom,DueDateTo,LedgerCodeFrom,"
        qry1 = qry1 + "LedgerCodeTo,Incharge,SalesMan,AccountType,AccountName,CompanyGroup,LocateGrp,Terms,GLStatus,"
        qry1 = qry1 + "ReportFormat,IncludeCompanyInfo,IncludeRecInfo,PrintDate,IncludeUnPaidBal,IncludeBillingFrqe,IncludeServiceFreq)"
        qry1 = qry1 + "VALUES(@AccountIDFrom,@AccountIDTo,@DocumentType,@CreatedBy,@CreatedOn,@Module,@Generated,"
        qry1 = qry1 + "@BatchNo,@FileType,@Selection,@Selformula,@RetryCount,@DomainName,@Distribution,@ContractGroup,@Branch,"
        qry1 = qry1 + "@CutOffDate,@InvoiceType,@Location,@PeriodFrom,@PeriodTo,@InvDateFrom,@InvDateTo,@DueDateFrom,"
        qry1 = qry1 + "@DueDateTo,@LedgerCodeFrom,@LedgerCodeTo,@Incharge,@SalesMan,@AccountType,@AccountName,@CompanyGroup,"
        qry1 = qry1 + "@LocateGrp,@Terms,@GLStatus,@ReportFormat,@IncludeCompanyInfo,@IncludeRecInfo,@PrintDate,"
        qry1 = qry1 + "@IncludeUnPaidBal,@IncludeBillingFrqe,@IncludeServiceFreq);"

        command.CommandText = qry1
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@AccountIDFrom", txtAccountID.Text)
        command.Parameters.AddWithValue("@AccountIDTo", txtAccountID.Text)
        If rbtnSelectDetSumm.SelectedIndex = 0 Then
            command.Parameters.AddWithValue("@DocumentType", "OSINVDETAIL")
        Else
            command.Parameters.AddWithValue("@DocumentType", "OSINVSUMMARY")
        End If

        command.Parameters.AddWithValue("@Module", "OSINV")
        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", Session("UserID").ToString + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "EXCEL")

        command.Parameters.AddWithValue("@Selection", Session("Selection"))
        command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
        '   command.Parameters.AddWithValue("@qry", qry)
        command.Parameters.AddWithValue("@RetryCount", 0)
        'If deleteqry = "delete from tblrptserviceanalysis where createdby='" & Session("UserID") & "' and report='ProdTeamMemberNormalOTSumm'" Then
        '    command.Parameters.AddWithValue("@ContractGroup", "-")
        'Else
        '    command.Parameters.AddWithValue("@ContractGroup", deleteqry)
        'End If
        command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())
        command.Parameters.AddWithValue("@Distribution", True)

        'Dim Selection As String = Session("Selection")
        'Dim ContractGroup As String = ""
        ''  If Selection.StartsWith("ContractGroup :") Then
        'If Selection.IndexOf("Contract Group : ") <> -1 Then
        '    ContractGroup = Selection.Substring(Selection.IndexOf("Contract Group : ") + 16)
        'Else
        '    ContractGroup = String.Empty

        'End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            command.Parameters.AddWithValue("@Branch", Convert.ToString(Session("Branch")))

            If String.IsNullOrEmpty(txtLocation.Text) = False Then
                command.Parameters.AddWithValue("@Location", txtLocation.Text)
            Else
                command.Parameters.AddWithValue("@Location", "")
            End If
        Else
            command.Parameters.AddWithValue("@Branch", "")
            command.Parameters.AddWithValue("@Location", "")
        End If

        If ddlInvoiceType.Text = "-1" Then
            command.Parameters.AddWithValue("@InvoiceType", "")
        Else
            command.Parameters.AddWithValue("@InvoiceType", ddlInvoiceType.SelectedValue.ToString)
        End If

        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                '  lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                '  Return False
            End If
            command.Parameters.AddWithValue("@PeriodFrom", d.ToString("yyyyMM"))
        Else
            command.Parameters.AddWithValue("@PeriodFrom", "")

        End If

        If String.IsNullOrEmpty(txtAcctPeriodTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodTo.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Accounting Period To is invalid", "str")
                ' lblAlert.Text = "ACCOUNTING PERIOD TO IS INVALID"
                'Return False
            End If
            command.Parameters.AddWithValue("@PeriodTo", d.ToString("yyyyMM"))
        Else
            command.Parameters.AddWithValue("@PeriodTo", "")
        End If

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@InvDateFrom", d.ToString("dd-MM-yyyy"))
        Else
            command.Parameters.AddWithValue("@InvDateFrom", "")

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@InvDateTo", d.ToString("dd-MM-yyyy"))
        Else
            command.Parameters.AddWithValue("@InvDateTo", "")

        End If

        If String.IsNullOrEmpty(txtDueDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@DueDateFrom", d.ToString("dd-MM-yyyy"))
        Else
            command.Parameters.AddWithValue("@DueDateFrom", "")

        End If

        If String.IsNullOrEmpty(txtDueDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtDueDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@DueDateTo", d.ToString("dd-MM-yyyy"))
        Else
            command.Parameters.AddWithValue("@DueDateTo", "")

        End If

        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            command.Parameters.AddWithValue("@LedgerCodeFrom", txtGLFrom.Text)
        Else
            command.Parameters.AddWithValue("@LedgerCodeFrom", "")

        End If

        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            command.Parameters.AddWithValue("@LedgerCodeTo", txtGLTo.Text)
        Else
            command.Parameters.AddWithValue("@LedgerCodeTo", "")
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            command.Parameters.AddWithValue("@Incharge", txtGLTo.Text)
        Else
            command.Parameters.AddWithValue("@Incharge", "")
        End If

        If ddlSalesMan.Text = "-1" Then
            command.Parameters.AddWithValue("@SalesMan", "")
        Else
            command.Parameters.AddWithValue("@SalesMan", ddlSalesMan.Text)

        End If

        If ddlAccountType.Text = "-1" Then
            command.Parameters.AddWithValue("@AccountType", "")
        Else
            command.Parameters.AddWithValue("@AccountType", ddlAccountType.Text)
        End If


        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            command.Parameters.AddWithValue("@AccountName", txtCustName.Text)
        Else
            command.Parameters.AddWithValue("@AccountName", "")
        End If

        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
            command.Parameters.AddWithValue("@CompanyGroup", YrStr)
        Else
            command.Parameters.AddWithValue("@CompanyGroup", "")
        End If

        If ddlLocateGrp.Text = "-1" Then
            command.Parameters.AddWithValue("@LocateGrp", "")
        Else
            command.Parameters.AddWithValue("@LocateGrp", ddlLocateGrp.Text)
        End If

        If ddlTerms.Text = "-1" Then
            command.Parameters.AddWithValue("@Terms", "")
        Else
            command.Parameters.AddWithValue("@Terms", ddlTerms.Text)
        End If

        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
            command.Parameters.AddWithValue("@GLStatus", txtGLStatus.Text)
        Else
            command.Parameters.AddWithValue("@GLStatus", "")
        End If

        If String.IsNullOrEmpty(txtPrintDate.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@PrintDate", d.ToString("dd-MM-yyyy"))
        Else
            command.Parameters.AddWithValue("@PrintDate", "")
        End If

        If chkUnpaidBal.Checked = True Then
            command.Parameters.AddWithValue("@IncludeUnpaidBal", True)
        Else
            command.Parameters.AddWithValue("@IncludeUnpaidBal", False)
        End If


        If chkCheckCutOff.Checked = True Then
            If String.IsNullOrEmpty(txtCutOffDate.Text) Then
                command.Parameters.AddWithValue("@CutOffDate", "")
            Else

                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else

                End If
                command.Parameters.AddWithValue("@CutOffDate", d.ToString("yyyy-MM-dd"))
            End If

        Else
            command.Parameters.AddWithValue("@CutOffDate", "")
        End If

        If chkIncludeCompanyGroupInfo.Checked = True Then
            command.Parameters.AddWithValue("@IncludeCompanyInfo", True)
        Else
            command.Parameters.AddWithValue("@IncludeCompanyInfo", False)
        End If

        If chkIncludeReceiptDetails.Checked = True Then
            command.Parameters.AddWithValue("@IncludeRecInfo", True)
        Else
            command.Parameters.AddWithValue("@IncludeRecInfo", False)
        End If

        If ddlContractGroup.SelectedIndex > 1 Then
            command.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text.Trim)
        Else
            command.Parameters.AddWithValue("@ContractGroup", "")
        End If


        If rbtnSelectFormat.SelectedIndex = 0 Then
            command.Parameters.AddWithValue("@ReportFormat", "Ageing-Report")
        ElseIf rbtnSelectFormat.SelectedIndex = 1 Then
            command.Parameters.AddWithValue("@ReportFormat", "Ageing-Report-Format-2")
        ElseIf rbtnSelectFormat.SelectedIndex = 2 Then
            command.Parameters.AddWithValue("@ReportFormat", "Ageing-Report-Format-3")
        ElseIf rbtnSelectFormat.SelectedIndex = 3 Then
            command.Parameters.AddWithValue("@ReportFormat", "Ageing-Report-Format-4")
        End If


        If chkBillingFreq.Checked = True Then
            command.Parameters.AddWithValue("@IncludeBillingFrqe", True)
        Else
            command.Parameters.AddWithValue("@IncludeBillingFrqe", False)
        End If


        If chkServiceFreq.Checked = True Then
            command.Parameters.AddWithValue("@IncludeServiceFreq", True)
        Else
            command.Parameters.AddWithValue("@IncludeServiceFreq", False)
        End If


        command.Connection = conn

        command.ExecuteNonQuery()

        command.Dispose()
        conn.Close()
        conn.Dispose()



        mdlPopupMsg.Show()

        Return

    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetSearch.Click
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            'Dim chk As CheckBoxList = TryCast(Me.Master.FindControl("chkBranch"), CheckBoxList)
            'Dim YrStrList As List(Of [String]) = New List(Of String)()
            'Dim count As Int16 = 0

            'For Each item As ListItem In chk.Items
            '    If item.Selected = True Then
            '        YrStrList.Add("""" + item.Value + """")
            '        count = count + 1
            '    End If
            'Next
            'If count = 0 Then
            '    lblAlert.Text = "SELECT BRANCH/LOCATION"
            'End If

            'Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
            'Session.Add("Branch", YrStr)

            txtLocation.Text = Convert.ToString(Session("Branch"))
        End If
    End Sub
End Class
