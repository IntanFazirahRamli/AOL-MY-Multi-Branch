Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Partial Class RV_Select_OutstandingInvoiceDetailsByDueDate_Sasi

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

        End If
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
        Dim qryrecv2 As String = ""

        '     If rbtnSelectDetSumm.SelectedValue = "1" Then
        'If chkCheckCutOff.Checked = True Then
        '    '  qry = "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"

        '    '  qry = qry + " ,OPeriodCredit as CreditBase,OPeriodPaid as ReceiptBase,OPeriodBalance as UnpaidBalance FROM tblsales where 1=1 and poststatus='P'"

        '    qry = qry + "SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention,tblsales.CustTelephone,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,"
        '    qry = qry + "tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,tblsales.balancebase,tblsales.doctype,"
        '    qry = qry + "tblsales.OPeriodCredit as Creditbase,tblsales.OPeriodPaid as Receiptbase,tblsales.OPeriodBalance as UnpaidBalance,tblsales.createdby as InvoicePreparedBy,vwcustomermainbillinginfo.BillContactPerson as BillContactPerson,vwcustomermainbillinginfo.BillMobile,vwcustomermainbillinginfo.BillTelephone,"
        '    qry = qry + "vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillFax, replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman,tblsales.PONo "
        '    qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P'"

        '    qryrecv = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,receiptdate,salesdate,invoicenumber,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
        '    qryrecv = qryrecv + " left outer join tblsales on tblrecvdet.reftype=tblsales.invoicenumber where tblrecv.poststatus='P' and tblsales.salesdate > tblrecv.receiptdate and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and tblsales.salesdate > '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

        '    qryrecv1 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
        '    qryrecv1 = qryrecv1 + " where tblrecv.poststatus='P' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

        'Else
        '    '   qry = qry + "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"
        '    '  qry = qry + " ,Creditbase,Receiptbase,Balancebase as UnpaidBalance FROM tblsales where 1=1 and poststatus='P' and UnpaidBalance<>0"
        '    qry = qry + "SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention,tblsales.CustTelephone,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,"
        '    qry = qry + "tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,"
        '    qry = qry + "tblsales.Creditbase,tblsales.Receiptbase,tblsales.Balancebase as UnpaidBalance,tblsales.createdby as InvoicePreparedBy,vwcustomermainbillinginfo.BillContactPerson,vwcustomermainbillinginfo.BillMobile,vwcustomermainbillinginfo.BillTelephone,"
        '    qry = qry + "vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillFax,replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman,tblsales.PONo "
        '    qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P' and Balancebase<>0"

        'End If
        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then

        If chkCheckCutOff.Checked = True Then
            qry = "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"

            qry = qry + " ,OPeriodCredit as CreditBase,OPeriodPaid as ReceiptBase,OPeriodBalance as UnpaidBalance,balancebase,doctype FROM tblsales where poststatus='P'"
            qry = qry + " and (closingdate<=salesdate or closingdate is null or"

            qry = qry + " closingdate> '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "')"

            qryrecv = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,receiptdate,salesdate,invoicenumber,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
            qryrecv = qryrecv + " left outer join tblsales on tblrecvdet.reftype=tblsales.invoicenumber where tblrecv.poststatus='P' and tblsales.salesdate > tblrecv.receiptdate and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and tblsales.salesdate > '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            qryrecv1 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
            qryrecv1 = qryrecv1 + " where tblrecv.poststatus='P' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' AND tblrecv.BankID<>'CONTRA'"

            qryrecv2 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(tblrecvdet.valuebase) as ReceiptValueAmt,(tblrecvdet.gstbase) as ReceiptGstAmt,(tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
            qryrecv2 = qryrecv2 + " where tblrecv.poststatus='P' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND tblrecv.BankID='CONTRA'"

        Else
            qry = qry + "SELECT ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,date_add(salesdate, INTERVAL termsday DAY) AS DueDate,Valuebase,Gstbase,sum(AppliedBase) as InvoiceAmount"
            qry = qry + " ,Creditbase,Receiptbase,Balancebase as UnpaidBalance FROM tblsales where poststatus='P' and BalanceBase<>0"

            qryrecv1 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(-tblrecvdet.valuebase) as ReceiptValueAmt,(-tblrecvdet.gstbase) as ReceiptGstAmt,(-tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
            qryrecv1 = qryrecv1 + " where tblrecv.poststatus='P' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND tblrecv.BankID<>'CONTRA'"

            qryrecv2 = "select tblrecv.contacttype,tblrecv.accountid,tblrecv.receiptfrom,tblrecv.receiptnumber,tblrecv.receiptdate,(tblrecvdet.valuebase) as ReceiptValueAmt,(tblrecvdet.gstbase) as ReceiptGstAmt,(tblrecvdet.appliedbase) as ReceiptAmt from tblrecv left outer join tblrecvdet on tblrecv.receiptnumber = tblrecvdet.receiptnumber"
            qryrecv2 = qryrecv2 + " where tblrecv.poststatus='P' and tblrecv.appliedbase<>0 and (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND tblrecv.BankID='CONTRA'"

        End If
        '    End If


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
            qryrecv2 = qryrecv2 + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
            qryrecv2 = qryrecv2 + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
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
            qryrecv2 = qryrecv2 + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"
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
            qryrecv2 = qryrecv2 + " and tblrecv.CompanyGroup in (" + YrStr + ")"
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

            'selFormula = selFormula + " and {tblsales1.PostStatus} <> 'V'"
            'If selection = "" Then
            '    selection = "Status NOT 'V'"
            'Else
            '    selection = selection + ", Status NOT 'V'"
            'End If
        End If

        'If String.IsNullOrEmpty(txtCostCode.Text) = False Then
        '    qry = qry + " and tblsalesDETAIL.CostCode = '" + txtCostCode.Text + "'"
        '    selFormula = selFormula + " and {tblsalesDETAIL1.CostCode} = '" + txtCostCode.Text + "'"
        '    If selection = "" Then
        '        selection = "CostCode : " + txtCostCode.Text
        '    Else
        '        selection = selection + ", CostCode : " + txtCostCode.Text
        '    End If
        'End If

        'If ddlItemCode.Text = "-1" Then
        'Else
        '    qry = qry + " and tblsalesdetail.ItemCode = '" + ddlItemCode.SelectedValue.ToString + "'"
        '    selFormula = selFormula + " and {tblsalesdetail1.ItemCode} = '" + ddlItemCode.SelectedValue.ToString + "'"
        '    If selection = "" Then
        '        selection = "ItemCode : " + ddlItemCode.SelectedValue.ToString
        '    Else
        '        selection = selection + ", ItemCode : " + ddlItemCode.SelectedValue.ToString
        '    End If
        'End If


        'If String.IsNullOrEmpty(txtReference.Text) = False Then
        '    qry = qry + " and tblsalesDETAIL.reftype like '" + txtReference.Text + "*'"
        '    selFormula = selFormula + " and {tblsalesDETAIL1.reftype} like '" + txtReference.Text + "*'"
        '    If selection = "" Then
        '        selection = "Reference : " + txtReference.Text
        '    Else
        '        selection = selection + ", Reference : " + txtReference.Text
        '    End If
        'End If

        'If ddlSubCode.Text = "-1" Then
        'Else
        '    qry = qry + " and tblsalesDETAIL.SubCode = '" + ddlSubCode.Text + "'"
        '    selFormula = selFormula + " and {tblsalesdetail1.SubCode} = '" + ddlSubCode.Text + "'"
        '    If selection = "" Then
        '        selection = "SubCode : " + ddlSubCode.Text
        '    Else
        '        selection = selection + ", SubCode : " + ddlSubCode.Text
        '    End If
        'End If
        'Session.Add("selFormula", selFormula)
        'Session.Add("selection", selection)

        'If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
        '    qry = qry + " group by tblsalesdetail.invoicenumber"

        'End If

        'If String.IsNullOrEmpty(lstSort2.Text) = False Then
        '    If lstSort2.Items(0).Selected = True Then


        '    End If
        '    Dim YrStrList As List(Of [String]) = New List(Of String)()
        '    Dim YrStrListVal As List(Of [String]) = New List(Of String)()

        '    For Each item As ListItem In lstSort2.Items
        '        If item.Selected Then

        '            YrStrList.Add(item.Text)
        '            YrStrListVal.Add(item.Value)

        '        End If
        '    Next
        '    If YrStrList.Count > 0 Then
        '        qry = qry + " ORDER BY "
        '        For i As Integer = 0 To YrStrList.Count - 1
        '            If i = 0 Then
        '                Session.Add("sort1", YrStrList.Item(i).ToString)
        '                qry = qry + YrStrListVal.Item(i).ToString

        '            ElseIf i = 1 Then
        '                Session.Add("sort2", YrStrList.Item(i).ToString)
        '                qry = qry + "," + YrStrListVal.Item(i).ToString

        '            ElseIf i = 2 Then
        '                Session.Add("sort3", YrStrList.Item(i).ToString)
        '                qry = qry + "," + YrStrListVal.Item(i).ToString

        '                'ElseIf i = 3 Then
        '                '    Session.Add("sort4", YrStrList.Item(i).ToString)
        '                'ElseIf i = 4 Then
        '                '    Session.Add("sort5", YrStrList.Item(i).ToString)
        '                'ElseIf i = 5 Then
        '                '    Session.Add("sort6", YrStrList.Item(i).ToString)
        '                'ElseIf i = 6 Then
        '                '    Session.Add("sort7", YrStrList.Item(i).ToString)
        '            End If

        '        Next
        '    Else
        '        qry = qry + " ORDER BY TBLSALESDETAIL.INVOICENUMBER"

        '    End If

        'End If

        'txtQuery.Text = qry

        ''If rbtnSelect.SelectedValue = "1" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceByClient_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceByClient_Summary.aspx")
        ''    End If
        ''ElseIf rbtnSelect.SelectedValue = "2" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceByCompanyGrp_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceByCompanyGrp_Summary.aspx")
        ''    End If
        ''ElseIf rbtnSelect.SelectedValue = "3" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceBySalesperson_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceBySalesperson_Summary.aspx")
        ''    End If
        ''ElseIf rbtnSelect.SelectedValue = "4" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceByGLCode_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceByGLCode_Summary.aspx")
        ''    End If
        ''ElseIf rbtnSelect.SelectedValue = "5" Then
        ''    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        ''ElseIf rbtnSelect.SelectedValue = "6" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceByServiceID_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceByServiceID_Summary.aspx")
        ''    End If
        ''ElseIf rbtnSelect.SelectedValue = "7" Then
        ''    If rbtnSelectDetSumm.SelectedValue = "1" Then
        ''        Response.Redirect("RV_SalesInvoiceByBillingFrequency_Detail.aspx")
        ''    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        ''        Response.Redirect("RV_SalesInvoiceByBillingFrequency_Summary.aspx")
        ''    End If

        ''End If
        ''  

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
        txtQueryRecv2.Text = qryrecv2

        'ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
        'qry = qry + " group by accountid ORDER BY AccountId"

        'End If
        txtQuery.Text = qry

        If chkCheckCutOff.Checked = True Then
            'If rbtnSelectDetSumm.SelectedValue = "1" Then

            '    InsertRptAgeingDetail()
            'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
            InsertRptAgeingCutOff()
            '  End If
        Else
            InsertRptAgeing()

        End If




        Return True
    End Function

    Private Sub InsertRptAgeing()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command0 As MySqlCommand = New MySqlCommand

            command0.CommandType = CommandType.Text
            command0.CommandText = "delete from tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

            command0.Connection = conn

            command0.ExecuteNonQuery()

            command0.Dispose()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = txtQuery.Text
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                For i As Int16 = 0 To dt.Rows.Count - 1

                    Dim DueDays As Int16
                    Dim Current = 0
                    Dim c1to10 = 0
                    Dim c11to30 = 0
                    Dim c31to60 = 0
                    Dim c61to90 = 0
                    Dim c91to150 = 0
                    Dim c151to180 = 0
                    Dim GreaterThan180 = 0

                    If dt.Rows(i)("DueDate").ToString <> DBNull.Value.ToString Then
                        DueDays = (Convert.ToDateTime(txtPrintDate.Text) - Convert.ToDateTime(dt.Rows(i)("DueDate"))).Days

                    End If

                    If DueDays <= 0 Then
                        '  dt.Rows(i)("Current") = dt.Rows(i)("ValueBase") + dt.Rows(i)("GstBase") - dt.Rows(i)("creditBase") - dt.Rows(i)("receiptBase")
                        Current = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 0 And DueDays <= 10 Then
                        c1to10 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 10 And DueDays <= 30 Then
                        c11to30 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 30 And DueDays <= 60 Then
                        c31to60 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 60 And DueDays <= 90 Then
                        c61to90 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 90 And DueDays <= 150 Then
                        c91to150 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 150 And DueDays <= 180 Then
                        c151to180 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 180 Then
                        GreaterThan180 = dt.Rows(i)("UnpaidBalance")

                    End If

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    'Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,SalesPerson,,CreatedBy,CreatedOn)"
                    'qry = qry + "(SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention,tblsales.CustTelephone,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,"
                    'qry = qry + "0,0,0,vwcustomermainbillinginfo.BillContactPerson,vwcustomermainbillinginfo.BillTelephone,vwcustomermainbillinginfo.BillFax,vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillMobile,replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman," + Convert.ToString(Session("UserID")) + "," + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) + ")"
                    'qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P'"

                    Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"
                    command.CommandText = qry
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
                    command.Parameters.AddWithValue("@UnPaidBalance", dt.Rows(i)("UnpaidBalance"))
                    command.Parameters.AddWithValue("@Current", Current)
                    command.Parameters.AddWithValue("@1To10", c1to10)
                    command.Parameters.AddWithValue("@11To30", c11to30)
                    command.Parameters.AddWithValue("@31To60", c31to60)
                    command.Parameters.AddWithValue("@61To90", c61to90)
                    command.Parameters.AddWithValue("@91To150", c91to150)
                    command.Parameters.AddWithValue("@151To180", c151to180)
                    command.Parameters.AddWithValue("@GreaterThan180", GreaterThan180)
                    command.Parameters.AddWithValue("@ServiceAddress", "")
                    ''command.Parameters.AddWithValue("@BillContactPerson", dt.Rows(i)("BillContactPerson"))
                    ''command.Parameters.AddWithValue("@BillTelephone", dt.Rows(i)("BillTelephone"))
                    ''command.Parameters.AddWithValue("@BillFax", dt.Rows(i)("BillFax"))
                    ''command.Parameters.AddWithValue("@BillTelephone2", dt.Rows(i)("BillTelephone2"))
                    ''command.Parameters.AddWithValue("@BillMobile", dt.Rows(i)("BillMobile"))
                    ''command.Parameters.AddWithValue("@BillContact1Email", dt.Rows(i)("BillContact1Email"))
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
                    '    command.Parameters.AddWithValue("@SalesPerson", dt.Rows(i)("Salesman"))
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
                    command.Parameters.AddWithValue("@GLCode", "INVOICE")
                    command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
                    command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


                    command.Connection = conn

                    command.ExecuteNonQuery()

                    command.Dispose()

                Next
            End If

            command1.Dispose()
            dt.Clear()
            dt.Dispose()
            dr.Close()

            For j As Int16 = 0 To 1
                Dim command3 As MySqlCommand = New MySqlCommand

                command3.CommandType = CommandType.Text

                If j = 0 Then
                    command3.CommandText = txtQueryRecv1.Text
                ElseIf j = 1 Then
                    command3.CommandText = txtQueryRecv2.Text

                End If
                command3.Connection = conn

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New DataTable
                dt3.Load(dr3)

                If dt3.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt3.Rows.Count - 1
                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@ContactType", dt3.Rows(i)("ContactType"))
                        command.Parameters.AddWithValue("@AccountId", dt3.Rows(i)("AccountId"))
                        command.Parameters.AddWithValue("@CustName", dt3.Rows(i)("ReceiptFrom"))
                        command.Parameters.AddWithValue("@CustAttention", "")
                        command.Parameters.AddWithValue("@CustTelephone", "")
                        command.Parameters.AddWithValue("@StaffId", "")
                        command.Parameters.AddWithValue("@InvoiceNumber", dt3.Rows(i)("ReceiptNumber"))
                        command.Parameters.AddWithValue("@SalesDate", dt3.Rows(i)("ReceiptDate"))
                        command.Parameters.AddWithValue("@Terms", "")
                        command.Parameters.AddWithValue("@TermsDay", 0)
                        command.Parameters.AddWithValue("@DueDate", DBNull.Value)
                        command.Parameters.AddWithValue("@ValueBase", dt3.Rows(i)("ReceiptValueAmt"))

                        command.Parameters.AddWithValue("@GstBase", dt3.Rows(i)("ReceiptGstAmt"))
                        command.Parameters.AddWithValue("@AppliedBase", dt3.Rows(i)("ReceiptAmt"))
                        command.Parameters.AddWithValue("@CreditBase", 0)
                        command.Parameters.AddWithValue("@ReceiptBase", 0)
                        command.Parameters.AddWithValue("@UnPaidBalance", dt3.Rows(i)("ReceiptAmt"))
                        command.Parameters.AddWithValue("@Current", 0)
                        command.Parameters.AddWithValue("@1To10", 0)
                        command.Parameters.AddWithValue("@11To30", 0)
                        command.Parameters.AddWithValue("@31To60", 0)
                        command.Parameters.AddWithValue("@61To90", 0)
                        command.Parameters.AddWithValue("@91To150", 0)
                        command.Parameters.AddWithValue("@151To180", 0)
                        command.Parameters.AddWithValue("@GreaterThan180", 0)
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
                        command.Parameters.AddWithValue("@GLCode", "RECEIPT")
                        command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
                        command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        command.Dispose()

                    Next

                End If

                command3.Dispose()
                dt3.Clear()
                dt3.Dispose()
                dr3.Close()

            Next


            conn.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertRptAgeing", ex.Message.ToString, "")

        End Try

    End Sub

    Private Sub InsertRptAgeingCutOff()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command0 As MySqlCommand = New MySqlCommand

            command0.CommandType = CommandType.Text
            command0.CommandText = "delete from tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

            command0.Connection = conn

            command0.ExecuteNonQuery()
            command0.Dispose()


            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = txtQuery.Text
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    txtCredit.Text = 0
                    txtReceipt.Text = 0
                    txtBalance.Text = 0

                    RecalculateBalanceNew(dt.Rows(i)("InvoiceNumber").ToString.Trim, dt.Rows(i)("InvoiceAmount"), dt.Rows(i)("BalanceBase"), dt.Rows(i)("doctype"), conn)

                    Dim DueDays As Int16
                    Dim Current = 0
                    Dim c1to10 = 0
                    Dim c11to30 = 0
                    Dim c31to60 = 0
                    Dim c61to90 = 0
                    Dim c91to150 = 0
                    Dim c151to180 = 0
                    Dim GreaterThan180 = 0

                    If dt.Rows(i)("DueDate").ToString <> DBNull.Value.ToString Then
                        DueDays = (Convert.ToDateTime(txtPrintDate.Text) - Convert.ToDateTime(dt.Rows(i)("DueDate"))).Days

                    End If

                    If DueDays <= 0 Then
                        '  dt.Rows(i)("Current") = dt.Rows(i)("ValueBase") + dt.Rows(i)("GstBase") - dt.Rows(i)("creditBase") - dt.Rows(i)("receiptBase")
                        Current = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 0 And DueDays <= 10 Then
                        c1to10 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 10 And DueDays <= 30 Then
                        c11to30 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 30 And DueDays <= 60 Then
                        c31to60 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 60 And DueDays <= 90 Then
                        c61to90 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 90 And DueDays <= 150 Then
                        c91to150 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 150 And DueDays <= 180 Then
                        c151to180 = dt.Rows(i)("UnpaidBalance")

                    ElseIf DueDays > 180 Then
                        GreaterThan180 = dt.Rows(i)("UnpaidBalance")

                    End If

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    'Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,SalesPerson,,CreatedBy,CreatedOn)"
                    'qry = qry + "(SELECT tblsales.ContactType,tblsales.AccountId,tblsales.CustName,tblsales.CustAttention,tblsales.CustTelephone,tblsales.StaffId,tblsales.InvoiceNumber,tblsales.SalesDate,tblsales.Terms,tblsales.TermsDay,date_add(tblsales.salesdate,INTERVAL tblsales.termsday DAY) AS DueDate,tblsales.Valuebase,tblsales.Gstbase,sum(tblsales.AppliedBase) as InvoiceAmount,"
                    'qry = qry + "0,0,0,vwcustomermainbillinginfo.BillContactPerson,vwcustomermainbillinginfo.BillTelephone,vwcustomermainbillinginfo.BillFax,vwcustomermainbillinginfo.BillTelephone2, vwcustomermainbillinginfo.BillMobile,replace(replace(vwcustomermainbillinginfo.BillContact1Email, char(10), ' '), char(13), ' ') as BillContact1Email,tblsales.Salesman," + Convert.ToString(Session("UserID")) + "," + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) + ")"
                    'qry = qry + "FROM tblsales left outer join vwcustomermainbillinginfo on tblsales.accountid=vwcustomermainbillinginfo.accountid where poststatus='P'"

                    Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"
                    command.CommandText = qry
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
                    command.Parameters.AddWithValue("@CreditBase", Convert.ToDecimal(txtCredit.Text))
                    command.Parameters.AddWithValue("@ReceiptBase", Convert.ToDecimal(txtReceipt.Text))
                    command.Parameters.AddWithValue("@UnPaidBalance", Convert.ToDecimal(txtBalance.Text))
                    command.Parameters.AddWithValue("@Current", Current)
                    command.Parameters.AddWithValue("@1To10", c1to10)
                    command.Parameters.AddWithValue("@11To30", c11to30)
                    command.Parameters.AddWithValue("@31To60", c31to60)
                    command.Parameters.AddWithValue("@61To90", c61to90)
                    command.Parameters.AddWithValue("@91To150", c91to150)
                    command.Parameters.AddWithValue("@151To180", c151to180)
                    command.Parameters.AddWithValue("@GreaterThan180", GreaterThan180)
                    command.Parameters.AddWithValue("@ServiceAddress", "")
                    ''command.Parameters.AddWithValue("@BillContactPerson", dt.Rows(i)("BillContactPerson"))
                    ''command.Parameters.AddWithValue("@BillTelephone", dt.Rows(i)("BillTelephone"))
                    ''command.Parameters.AddWithValue("@BillFax", dt.Rows(i)("BillFax"))
                    ''command.Parameters.AddWithValue("@BillTelephone2", dt.Rows(i)("BillTelephone2"))
                    ''command.Parameters.AddWithValue("@BillMobile", dt.Rows(i)("BillMobile"))
                    ''command.Parameters.AddWithValue("@BillContact1Email", dt.Rows(i)("BillContact1Email"))
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
                    '    command.Parameters.AddWithValue("@SalesPerson", dt.Rows(i)("Salesman"))
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
                    command.Parameters.AddWithValue("@GLCode", "INVOICE")
                    command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
                    command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    command.Dispose()


                Next
            End If

            command1.Dispose()
            dt.Clear()
            dt.Dispose()
            dr.Close()

            For j As Int16 = 0 To 2
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If j = 0 Then
                    command2.CommandText = txtQueryRecv.Text
                ElseIf j = 1 Then
                    command2.CommandText = txtQueryRecv1.Text
                ElseIf j = 2 Then
                    command2.CommandText = txtQueryRecv2.Text
                End If

                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt2.Rows.Count - 1
                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@ContactType", dt2.Rows(i)("ContactType"))
                        command.Parameters.AddWithValue("@AccountId", dt2.Rows(i)("AccountId"))
                        command.Parameters.AddWithValue("@CustName", dt2.Rows(i)("ReceiptFrom"))
                        command.Parameters.AddWithValue("@CustAttention", "")
                        command.Parameters.AddWithValue("@CustTelephone", "")
                        command.Parameters.AddWithValue("@StaffId", "")
                        command.Parameters.AddWithValue("@InvoiceNumber", dt2.Rows(i)("ReceiptNumber"))
                        command.Parameters.AddWithValue("@SalesDate", dt2.Rows(i)("ReceiptDate"))
                        command.Parameters.AddWithValue("@Terms", "")
                        command.Parameters.AddWithValue("@TermsDay", 0)
                        command.Parameters.AddWithValue("@DueDate", DBNull.Value)
                        command.Parameters.AddWithValue("@ValueBase", dt2.Rows(i)("ReceiptValueAmt"))
                        command.Parameters.AddWithValue("@GstBase", dt2.Rows(i)("ReceiptGstAmt"))
                        command.Parameters.AddWithValue("@AppliedBase", dt2.Rows(i)("ReceiptAmt"))
                        command.Parameters.AddWithValue("@CreditBase", 0)
                        command.Parameters.AddWithValue("@ReceiptBase", 0)
                        command.Parameters.AddWithValue("@UnPaidBalance", dt2.Rows(i)("ReceiptAmt"))
                        command.Parameters.AddWithValue("@Current", 0)
                        command.Parameters.AddWithValue("@1To10", 0)
                        command.Parameters.AddWithValue("@11To30", 0)
                        command.Parameters.AddWithValue("@31To60", 0)
                        command.Parameters.AddWithValue("@61To90", 0)
                        command.Parameters.AddWithValue("@91To150", 0)
                        command.Parameters.AddWithValue("@151To180", 0)
                        command.Parameters.AddWithValue("@GreaterThan180", 0)
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
                        command.Parameters.AddWithValue("@GLCode", "RECEIPT")
                        command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
                        command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


                        command.Connection = conn

                        command.ExecuteNonQuery()

                        command.Dispose()

                    Next

                End If

                command2.Dispose()
                dt2.Clear()
                dt2.Dispose()
                dr2.Close()

            Next


            conn.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertRptAgeing", ex.Message.ToString, "")

        End Try

    End Sub


    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        Dim sda As New MySqlDataAdapter()

        '   Try

        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select ContactType, AccountId,CustName,CustAttention, CustTelephone, StaffId,InvoiceNumber, SalesDate, Terms,TermsDay,DueDate,Valuebase,Gstbase,AppliedBase,Creditbase,Receiptbase,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,UnpaidBalance,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email From tblrptosageing where createdby = '" + Convert.ToString(Session("UserID")) + "' and UnpaidBalance<>0"

        cmd.Connection = conn
        conn.Open()
        sda.SelectCommand = cmd
        sda.Fill(dt)

        '    dt.Columns.Add(New DataColumn("DueDays"))
        'Dim col1 = dt.Columns.Add("Current")
        'col1.SetOrdinal(17)
        'Dim col2 = dt.Columns.Add("1-10 Days")
        'col2.SetOrdinal(18)
        'Dim col3 = dt.Columns.Add("11-30 Days")
        'col3.SetOrdinal(19)
        'Dim col4 = dt.Columns.Add("31-60 Days")
        'col4.SetOrdinal(20)
        'Dim col5 = dt.Columns.Add("61-90 Days")
        'col5.SetOrdinal(21)
        'Dim col6 = dt.Columns.Add("91-150 Days")
        'col6.SetOrdinal(22)
        'Dim col7 = dt.Columns.Add("151-180 Days")
        'col7.SetOrdinal(23)
        'Dim col8 = dt.Columns.Add(">180 Days")
        'col8.SetOrdinal(24)
        'Dim col9 = dt.Columns.Add("ServiceAddress")
        'col9.SetOrdinal(25)

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




        If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then


            Dim DueDays As Int16


            Dim svcaddr As String = ""
            Dim billcontactperson As String = ""
            Dim billmobile As String = ""
            Dim billtel As String = ""
            Dim billfax As String = ""
            Dim billtel2 As String = ""
            Dim billcontact1email As String = ""

            For Each dr As DataRow In dt.Rows


                svcaddr = ""
                If String.IsNullOrEmpty(dr("InvoiceNumber")) = False Then
                    Dim cmdSalesDet As MySqlCommand = New MySqlCommand

                    cmdSalesDet.CommandType = CommandType.Text

                    cmdSalesDet.CommandText = "Select reftype,locationid from tblsalesdetail where invoicenumber='" + dr("InvoiceNumber").ToString + "' group by locationid"


                    cmdSalesDet.Connection = conn

                    Dim drSalesDet As MySqlDataReader = cmdSalesDet.ExecuteReader()
                    Dim dtSalesDet As New DataTable
                    dtSalesDet.Load(drSalesDet)

                    If dtSalesDet.Rows.Count > 0 Then
                        For j As Int16 = 0 To dtSalesDet.Rows.Count - 1
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
                                    addr = dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                    If addr.Trim = "" Then
                                        addr = "--"
                                    End If

                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                        billcontactperson = "--"
                                    Else
                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                        billmobile = "--"
                                    Else
                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                        billtel = "--"
                                    Else
                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                        billtel2 = "--"
                                    Else
                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                        billfax = "--"
                                    Else
                                        billfax = dtLoc.Rows(0)("BillFax").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                        billcontact1email = "--"
                                    Else
                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString
                                    End If

                                    If j = 0 Then
                                        svcaddr = addr

                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                            billcontactperson = "--"
                                        Else
                                            billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                            billmobile = "--"
                                        Else
                                            billmobile = dtLoc.Rows(0)("BillMobile").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                            billtel = "--"
                                        Else
                                            billtel = dtLoc.Rows(0)("BillTelephone").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                            billtel2 = "--"
                                        Else
                                            billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                            billfax = "--"
                                        Else
                                            billfax = dtLoc.Rows(0)("BillFax").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                            billcontact1email = "--"
                                        Else
                                            billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString
                                        End If
                                    Else
                                        svcaddr = svcaddr + "; " + addr
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                            billcontactperson = billcontactperson + "--"
                                        Else
                                            billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                            billmobile = billmobile + "--"
                                        Else
                                            billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                            billtel = billtel + "--"
                                        Else
                                            billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                            billtel2 = billtel2 + "--"
                                        Else
                                            billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                            billfax = billfax + "--"
                                        Else
                                            billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                            billcontact1email = billcontact1email + "--"
                                        Else
                                            billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString
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
                                    addr = dtLoc.Rows(0)("address1").ToString + " " + dtLoc.Rows(0)("addbuilding").ToString + " " + dtLoc.Rows(0)("addstreet").ToString + " " + dtLoc.Rows(0)("addpostal").ToString

                                    If addr.Trim = "" Then
                                        addr = "--"
                                    End If

                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                        billcontactperson = "--"
                                    Else
                                        billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                        billmobile = "--"
                                    Else
                                        billmobile = dtLoc.Rows(0)("BillMobile").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                        billtel = "--"
                                    Else
                                        billtel = dtLoc.Rows(0)("BillTelephone").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                        billtel2 = "--"
                                    Else
                                        billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                        billfax = "--"
                                    Else
                                        billfax = dtLoc.Rows(0)("BillFax").ToString
                                    End If
                                    If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                        billcontact1email = "--"
                                    Else
                                        billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString
                                    End If

                                    If j = 0 Then
                                        svcaddr = addr

                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                            billcontactperson = "--"
                                        Else
                                            billcontactperson = dtLoc.Rows(0)("BillContactPerson").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                            billmobile = "--"
                                        Else
                                            billmobile = dtLoc.Rows(0)("BillMobile").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                            billtel = "--"
                                        Else
                                            billtel = dtLoc.Rows(0)("BillTelephone").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                            billtel2 = "--"
                                        Else
                                            billtel2 = dtLoc.Rows(0)("BillTelephone2").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                            billfax = "--"
                                        Else
                                            billfax = dtLoc.Rows(0)("BillFax").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                            billcontact1email = "--"
                                        Else
                                            billcontact1email = dtLoc.Rows(0)("BillContact1Email").ToString
                                        End If
                                    Else
                                        svcaddr = svcaddr + "; " + addr
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContactPerson")) Then
                                            billcontactperson = billcontactperson + "--"
                                        Else
                                            billcontactperson = billcontactperson + "; " + dtLoc.Rows(0)("BillContactPerson").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillMobile")) Then
                                            billmobile = billmobile + "--"
                                        Else
                                            billmobile = billmobile + "; " + dtLoc.Rows(0)("BillMobile").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone")) Then
                                            billtel = billtel + "--"
                                        Else
                                            billtel = billtel + "; " + dtLoc.Rows(0)("BillTelephone").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillTelephone2")) Then
                                            billtel2 = billtel2 + "--"
                                        Else
                                            billtel2 = billtel2 + "; " + dtLoc.Rows(0)("BillTelephone2").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillFax")) Then
                                            billfax = billfax + "--"
                                        Else
                                            billfax = billfax + "; " + dtLoc.Rows(0)("BillFax").ToString
                                        End If
                                        If String.IsNullOrEmpty(dtLoc.Rows(0)("BillContact1Email")) Then
                                            billcontact1email = billcontact1email + "--"
                                        Else
                                            billcontact1email = billcontact1email + "; " + dtLoc.Rows(0)("BillContact1Email").ToString
                                        End If
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
                dr("BillContactPerson") = billcontactperson
                dr("BillMobile") = billmobile
                dr("BillTelephone") = billtel
                dr("BillTelephone2") = billtel2
                dr("BillFax") = billfax
                dr("BillContact1Email") = billcontact1email

                'If dr("DueDate").ToString <> DBNull.Value.ToString Then
                '    DueDays = (Convert.ToDateTime(txtPrintDate.Text) - Convert.ToDateTime(dr("DueDate"))).Days

                'End If

                'If DueDays <= 0 Then
                '    '  dr("Current") = dr("ValueBase") + dr("GstBase") - dr("creditBase") - dr("receiptBase")
                '    dr("Current") = dr("UnpaidBalance")
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0

                'ElseIf DueDays > 0 And DueDays <= 10 Then
                '    dr("1to10") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 10 And DueDays <= 30 Then
                '    dr("11to30") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 30 And DueDays <= 60 Then
                '    dr("31to60") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 60 And DueDays <= 90 Then
                '    dr("61to90") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 90 And DueDays <= 150 Then
                '    dr("91to150") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("151to180") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 150 And DueDays <= 180 Then
                '    dr("151to180") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("GreaterThan180") = 0
                'ElseIf DueDays > 180 Then
                '    dr("GreaterThan180") = dr("UnpaidBalance")
                '    dr("Current") = 0
                '    dr("1to10") = 0
                '    dr("11to30") = 0
                '    dr("31to60") = 0
                '    dr("61to90") = 0
                '    dr("91to150") = 0
                '    dr("151to180") = 0

                '    End If
            Next


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
                If dr.Item("AppliedBase").ToString <> DBNull.Value.ToString Then
                    TotalInvoice = TotalInvoice + dr.Item("AppliedBase")
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
                If dr.Item("1to10").ToString <> DBNull.Value.ToString Then
                    Total1to10 = Total1to10 + dr.Item("1to10")
                End If
                If dr.Item("11to30").ToString <> DBNull.Value.ToString Then
                    Total11to30 = Total11to30 + dr.Item("11to30")
                End If
                If dr.Item("31to60").ToString <> DBNull.Value.ToString Then
                    Total31to60 = Total31to60 + dr.Item("31to60")
                End If
                If dr.Item("61to90").ToString <> DBNull.Value.ToString Then
                    Total61to90 = Total61to90 + dr.Item("61to90")
                End If
                If dr.Item("91to150").ToString <> DBNull.Value.ToString Then
                    Total91to150 = Total91to150 + dr.Item("91to150")
                End If
                If dr.Item("151to180").ToString <> DBNull.Value.ToString Then
                    Total151to180 = Total151to180 + dr.Item("151to180")
                End If
                If dr.Item("GreaterThan180").ToString <> DBNull.Value.ToString Then
                    Total180 = Total180 + dr.Item("GreaterThan180")
                End If
            Next

            drTotal.Item(0) = "Total"
            drTotal.Item("AppliedBase") = TotalInvoice
            drTotal.Item("CreditBase") = TotalCredit
            drTotal.Item("ReceiptBase") = TotalReceipt
            drTotal.Item("UnpaidBalance") = TotalBalance
            drTotal.Item("Current") = TotalCurrent
            drTotal.Item("1to10") = Total1to10
            drTotal.Item("11to30") = Total11to30
            drTotal.Item("31to60") = Total31to60
            drTotal.Item("61to90") = Total61to90
            drTotal.Item("91to150") = Total91to150
            drTotal.Item("151to180") = Total151to180
            drTotal.Item("GreaterThan180") = Total180
            dt.Rows.Add(drTotal)


            Return dt
        ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then


            'Dim command0 As MySqlCommand = New MySqlCommand

            'command0.CommandType = CommandType.Text
            'command0.CommandText = "delete from tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "'"

            'command0.Connection = conn

            'command0.ExecuteNonQuery()

            'command0.Dispose()

            'If dt.Rows.Count > 0 Then
            '    For i As Int64 = 0 To dt.Rows.Count - 1
            '        Dim command As MySqlCommand = New MySqlCommand

            '        command.CommandType = CommandType.Text
            '        command.CommandText = "INSERT INTO tblrptosageing(ContactType,AccountId,CustName,CustAttention,CustTelephone,StaffId,InvoiceNumber,SalesDate,Terms,TermsDay,DueDate,ValueBase,GstBase,AppliedBase,CreditBase,ReceiptBase,UnPaidBalance,Current,1To10,11To30,31To60,61To90,91To150,151To180,GreaterThan180,ServiceAddress,BillContactPerson,BillTelephone,BillFax,BillTelephone2,BillMobile,BillContact1Email,CompanyGroup,ContractGroup,BillingPeriod,InvoiceType,SalesPerson,TotOutstanding,TotDue,DaysOverDue,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,AmtReceivable,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount)VALUES(@ContactType,@AccountId,@CustName,@CustAttention,@CustTelephone,@StaffId,@InvoiceNumber,@SalesDate,@Terms,@TermsDay,@DueDate,@ValueBase,@GstBase,@AppliedBase,@CreditBase,@ReceiptBase,@UnPaidBalance,@Current,@1To10,@11To30,@31To60,@61To90,@91To150,@151To180,@GreaterThan180,@ServiceAddress,@BillContactPerson,@BillTelephone,@BillFax,@BillTelephone2,@BillMobile,@BillContact1Email,@CompanyGroup,@ContractGroup,@BillingPeriod,@InvoiceType,@SalesPerson,@TotOutstanding,@TotDue,@DaysOverDue,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AmtReceivable,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount);"

            '        command.Parameters.Clear()


            '        command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
            '        command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountId"))
            '        command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
            '        command.Parameters.AddWithValue("@CustAttention", dt.Rows(i)("CustAttention"))
            '        command.Parameters.AddWithValue("@CustTelephone", dt.Rows(i)("CustTelephone"))
            '        command.Parameters.AddWithValue("@StaffId", dt.Rows(i)("StaffId"))
            '        command.Parameters.AddWithValue("@InvoiceNumber", dt.Rows(i)("InvoiceNumber"))
            '        command.Parameters.AddWithValue("@SalesDate", dt.Rows(i)("SalesDate"))
            '        command.Parameters.AddWithValue("@Terms", dt.Rows(i)("Terms"))
            '        command.Parameters.AddWithValue("@TermsDay", dt.Rows(i)("TermsDay"))
            '        command.Parameters.AddWithValue("@DueDate", dt.Rows(i)("DueDate"))
            '        command.Parameters.AddWithValue("@ValueBase", dt.Rows(i)("ValueBase"))
            '        command.Parameters.AddWithValue("@GstBase", dt.Rows(i)("GstBase"))
            '        command.Parameters.AddWithValue("@AppliedBase", dt.Rows(i)("InvoiceAmount"))
            '        command.Parameters.AddWithValue("@CreditBase", dt.Rows(i)("CreditBase"))
            '        command.Parameters.AddWithValue("@ReceiptBase", dt.Rows(i)("ReceiptBase"))
            '        command.Parameters.AddWithValue("@UnPaidBalance", dt.Rows(i)("UnPaidBalance"))
            '        command.Parameters.AddWithValue("@Current", dt.Rows(i)("Current"))
            '        command.Parameters.AddWithValue("@1To10", dt.Rows(i)("1-10 Days"))
            '        command.Parameters.AddWithValue("@11To30", dt.Rows(i)("11-30 Days"))
            '        command.Parameters.AddWithValue("@31To60", dt.Rows(i)("31-60 Days"))
            '        command.Parameters.AddWithValue("@61To90", dt.Rows(i)("61-90 Days"))
            '        command.Parameters.AddWithValue("@91To150", dt.Rows(i)("91-150 Days"))
            '        command.Parameters.AddWithValue("@151To180", dt.Rows(i)("151-180 Days"))
            '        command.Parameters.AddWithValue("@GreaterThan180", dt.Rows(i)(">180 Days"))
            '        command.Parameters.AddWithValue("@ServiceAddress", "")
            '        command.Parameters.AddWithValue("@BillContactPerson", "")
            '        command.Parameters.AddWithValue("@BillTelephone", "")
            '        command.Parameters.AddWithValue("@BillFax", "")
            '        command.Parameters.AddWithValue("@BillTelephone2", "")
            '        command.Parameters.AddWithValue("@BillMobile", "")
            '        command.Parameters.AddWithValue("@BillContact1Email", "")
            '        command.Parameters.AddWithValue("@CompanyGroup", "")
            '        command.Parameters.AddWithValue("@ContractGroup", "")
            '        command.Parameters.AddWithValue("@BillingPeriod", "")
            '        command.Parameters.AddWithValue("@InvoiceType", "")
            '        command.Parameters.AddWithValue("@SalesPerson", "")
            '        command.Parameters.AddWithValue("@TotOutstanding", 0)
            '        command.Parameters.AddWithValue("@TotDue", 0)
            '        command.Parameters.AddWithValue("@DaysOverDue", DBNull.Value)
            '        command.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session("UserID")))
            '        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            '        command.Parameters.AddWithValue("@LastModifiedBy", DBNull.Value)
            '        command.Parameters.AddWithValue("@LastModifiedOn", DBNull.Value)
            '        command.Parameters.AddWithValue("@AmtReceivable", DBNull.Value)
            '        command.Parameters.AddWithValue("@VoucherNumber", DBNull.Value)
            '        command.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '        command.Parameters.AddWithValue("@GLCode", DBNull.Value)
            '        command.Parameters.AddWithValue("@DebitAmount", DBNull.Value)
            '        command.Parameters.AddWithValue("@CreditAmount", DBNull.Value)


            '        command.Connection = conn

            '        command.ExecuteNonQuery()

            '        command.Dispose()

            '    Next
            'Dim DueDays As Int16

            'For Each dr As DataRow In dt.Rows

            '    If dr("DueDate").ToString <> DBNull.Value.ToString Then
            '        DueDays = (Convert.ToDateTime(txtPrintDate.Text) - Convert.ToDateTime(dr("DueDate"))).Days

            '    End If

            '    If DueDays <= 0 Then
            '        '  dr("Current") = dr("ValueBase") + dr("GstBase") - dr("creditBase") - dr("receiptBase")
            '        dr("Current") = dr("UnpaidBalance")
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0

            '    ElseIf DueDays > 0 And DueDays <= 10 Then
            '        dr("1to10") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 10 And DueDays <= 30 Then
            '        dr("11to30") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 30 And DueDays <= 60 Then
            '        dr("31to60") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 60 And DueDays <= 90 Then
            '        dr("61to90") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 90 And DueDays <= 150 Then
            '        dr("91to150") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("151to180") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 150 And DueDays <= 180 Then
            '        dr("151to180") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("GreaterThan180") = 0
            '    ElseIf DueDays > 180 Then
            '        dr("GreaterThan180") = dr("UnpaidBalance")
            '        dr("Current") = 0
            '        dr("1to10") = 0
            '        dr("11to30") = 0
            '        dr("31to60") = 0
            '        dr("61to90") = 0
            '        dr("91to150") = 0
            '        dr("151to180") = 0

            'End If

            'Dim command As MySqlCommand = New MySqlCommand

            'command.CommandType = CommandType.Text
            'command.CommandText = "UPDATE tblrptosageing SET Current=@Current,1To10=@1TO10,11To30=@11TO30,31To60=@31TO60,61To90=@61TO90,91To150=@91TO150,151To180=@151TO180,GreaterThan180=@GreaterThan180"
            'command.Parameters.Clear()

            'command.Parameters.AddWithValue("@Current", dr("Current"))
            'command.Parameters.AddWithValue("@1To10", dr("1-10 Days"))
            'command.Parameters.AddWithValue("@11To30", dr("11-30 Days"))
            'command.Parameters.AddWithValue("@31To60", dr("31-60 Days"))
            'command.Parameters.AddWithValue("@61To90", dr("61-90 Days"))
            'command.Parameters.AddWithValue("@91To150", dr("91-150 Days"))
            'command.Parameters.AddWithValue("@151To180", dr("151-180 Days"))
            'command.Parameters.AddWithValue("@GreaterThan180", dr(">180 Days"))

            'command.Connection = conn

            'command.ExecuteNonQuery()

            'command.Dispose()
            'Next


            'Dim drTotal As DataRow = dt.NewRow
            'Dim TotalInvoice As Decimal = 0
            'Dim TotalBalance As Decimal = 0
            'Dim TotalCurrent As Decimal = 0
            'Dim Total1to10 As Decimal = 0
            'Dim Total11to30 As Decimal = 0
            'Dim Total31to60 As Decimal = 0
            'Dim Total61to90 As Decimal = 0
            'Dim Total91to150 As Decimal = 0
            'Dim Total151to180 As Decimal = 0
            'Dim Total180 As Decimal = 0
            'Dim TotalCredit As Decimal = 0
            'Dim TotalReceipt As Decimal = 0

            'For Each dr As DataRow In dt.Rows
            '    If dr.Item("AppliedBase").ToString <> DBNull.Value.ToString Then
            '        TotalInvoice = TotalInvoice + dr.Item("AppliedBase")
            '    End If
            '    If dr.Item("CreditBase").ToString <> DBNull.Value.ToString Then
            '        TotalCredit = TotalCredit + dr.Item("CreditBase")
            '    End If
            '    If dr.Item("ReceiptBase").ToString <> DBNull.Value.ToString Then
            '        TotalReceipt = TotalReceipt + dr.Item("ReceiptBase")
            '    End If
            '    If dr.Item("UnpaidBalance").ToString <> DBNull.Value.ToString Then
            '        TotalBalance = TotalBalance + dr.Item("UnpaidBalance")
            '    End If
            '    If dr.Item("Current").ToString <> DBNull.Value.ToString Then
            '        TotalCurrent = TotalCurrent + dr.Item("Current")
            '    End If
            '    If dr.Item("1to10").ToString <> DBNull.Value.ToString Then
            '        Total1to10 = Total1to10 + dr.Item("1to10")
            '    End If
            '    If dr.Item("11to30").ToString <> DBNull.Value.ToString Then
            '        Total11to30 = Total11to30 + dr.Item("11to30")
            '    End If
            '    If dr.Item("31to60").ToString <> DBNull.Value.ToString Then
            '        Total31to60 = Total31to60 + dr.Item("31to60")
            '    End If
            '    If dr.Item("61to90").ToString <> DBNull.Value.ToString Then
            '        Total61to90 = Total61to90 + dr.Item("61to90")
            '    End If
            '    If dr.Item("91to150").ToString <> DBNull.Value.ToString Then
            '        Total91to150 = Total91to150 + dr.Item("91to150")
            '    End If
            '    If dr.Item("151to180").ToString <> DBNull.Value.ToString Then
            '        Total151to180 = Total151to180 + dr.Item("151to180")
            '    End If
            '    If dr.Item("GreaterThan180").ToString <> DBNull.Value.ToString Then
            '        Total180 = Total180 + dr.Item("GreaterThan180")
            '    End If
            'Next

            'drTotal.Item(0) = "Total"
            'drTotal.Item("AppliedBase") = TotalInvoice
            'drTotal.Item("CreditBase") = TotalCredit
            'drTotal.Item("ReceiptBase") = TotalReceipt
            'drTotal.Item("UnpaidBalance") = TotalBalance
            'drTotal.Item("Current") = TotalCurrent
            'drTotal.Item("1to10") = Total1to10
            'drTotal.Item("11to30") = Total11to30
            'drTotal.Item("31to60") = Total31to60
            'drTotal.Item("61to90") = Total61to90
            'drTotal.Item("91to150") = Total91to150
            'drTotal.Item("151to180") = Total151to180
            'drTotal.Item("GreaterThan180") = Total180
            'dt.Rows.Add(drTotal)


            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text

            Dim qry As String = "SELECT AccountID,CustName AS AccountName,sum(AppliedBase) as TotalOutstanding,sum(Current) as TotalCurrent,sum(1To10) as Total1To10,sum(11To30) as Total11To30,"
            qry = qry + "sum(31To60) as Total31To60,sum(61To90) as Total61To90,sum(91To150) as Total91To150,sum(151To180) as Total151To180,sum(GreaterThan180) as TotalGreaterThan180,sum(UnPaidBalance) as TotalUnpaidBalance"
            qry = qry + " FROM tblrptosageing where CreatedBy='" + Convert.ToString(Session("UserID")) + "' and unpaidbalance<>0 GROUP BY ACCOUNTID"

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


        conn.Close()
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
        For i As Int32 = 0 To dt.Rows.Count - 1

            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalRecvcn = 0.0
            lTotalcn = 0.0
            lTotalJournalAmt = 0.0
            lbalance = 0.0
            If dt.Rows(i)("GLCode") = "INVOICE" Then

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
                        dt2.Clear()
                        dt2.Dispose()
                        dr2.Close()
                        command2.Dispose()

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

                        dt3.Clear()
                        dt3.Dispose()
                        dr3.Close()
                        command3.Dispose()

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
                        dt4.Clear()
                        dt4.Dispose()
                        dr4.Close()
                        command4.Dispose()


                        dt1.Clear()
                        dt1.Dispose()
                        dr1.Close()
                        command1.Dispose()
                        'Calculate Balance amount and update

                        lbalance = lInvoiceAmount + lTotalcn - lTotalReceipt + lTotalJournalAmt
                        lTotalcn = lTotalcn + lTotalJournalAmt

                        Dim command21 As MySqlCommand = New MySqlCommand
                        command21.CommandType = CommandType.Text

                        command21.CommandText = "UPDATE tblrptosageing SET UnpaidBalance = '" & lbalance & "', ReceiptBase='" & lTotalReceipt & "',CreditBase='" & lTotalcn & "' WHERE InvoiceNumber = '" & invno & "'"
                        command21.Connection = conn

                        command21.ExecuteNonQuery()


                        'dt2.Clear()
                        'dt2.Dispose()
                        'dr2.Close()
                        'command2.Dispose()

                        'dt3.Clear()
                        'dt3.Dispose()
                        'dr3.Close()
                        'command3.Dispose()


                        'dt4.Clear()
                        'dt4.Dispose()
                        'dr4.Close()
                        'command4.Dispose()

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

                        command21.CommandText = "UPDATE tblrptosageing SET UnpaidBalance = " & dt1.Rows(0)("balancebase") & ", ReceiptBase=0,CreditBase=0 WHERE InvoiceNumber = '" & invno & "'"
                        command21.Connection = conn

                        command21.ExecuteNonQuery()


                        command21.Dispose()

                    End If

                Catch ex As Exception
                    lblAlert.Text = ex.Message.ToString + " " + recno
                    InsertIntoTblWebEventLog("RecalculateBalance", ex.Message.ToString, invno)
                End Try
            End If

        Next

    End Sub


    Private Sub RecalculateBalanceNew(invno As String, lInvoiceAmount As Decimal, lBalanceBase As Decimal, doctype As String, conn As MySqlConnection)
        Dim acctid As String = ""
        Dim lTotalReceipt As Decimal
        Dim lTotalcn As Decimal
        Dim lTotalRecvcn As Decimal
        Dim lTotalJournalAmt As Decimal
        Dim lbalance As Decimal
        Dim cnno As String = ""
        Dim recno As String = ""


        Try

            If doctype = "ARIN" Then
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


                        Else
                            lTotalRecvcn = 0

                        End If

                        dt31.Clear()
                        dt31.Dispose()
                        dr31.Close()
                        command31.Dispose()

                    Else
                        '   cnno = invno

                    End If

                End If
                dt2.Clear()
                dt2.Dispose()
                dr2.Close()
                command2.Dispose()

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

                dt3.Clear()
                dt3.Dispose()
                dr3.Close()
                command3.Dispose()

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

                End If
                dt4.Clear()
                dt4.Dispose()
                dr4.Close()
                command4.Dispose()


                'Calculate Balance amount and update

                lbalance = lInvoiceAmount + lTotalcn - lTotalReceipt + lTotalJournalAmt
                lTotalcn = lTotalcn + lTotalJournalAmt


                txtCredit.Text = lTotalcn.ToString
                txtReceipt.Text = lTotalReceipt
                txtBalance.Text = lbalance

                'Dim command21 As MySqlCommand = New MySqlCommand
                'command21.CommandType = CommandType.Text

                'command21.CommandText = "UPDATE tblrptosageing SET UnpaidBalance = '" & lbalance & "', ReceiptBase='" & lTotalReceipt & "',CreditBase='" & lTotalcn & "' WHERE InvoiceNumber = '" & invno & "'"
                'command21.Connection = conn

                'command21.ExecuteNonQuery()

                'command21.Dispose()
            ElseIf doctype = "ARCN" Or doctype = "ARDN" Then


                txtCredit.Text = 0
                txtReceipt.Text = 0
                txtBalance.Text = lBalanceBase

                'Dim command21 As MySqlCommand = New MySqlCommand
                'command21.CommandType = CommandType.Text

                'command21.CommandText = "UPDATE tblrptosageing SET UnpaidBalance = " + lbalancebase + ", ReceiptBase=0,CreditBase=0 WHERE InvoiceNumber = '" & invno & "'"
                'command21.Connection = conn

                'command21.ExecuteNonQuery()


                'command21.Dispose()

            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString + " " + recno
            InsertIntoTblWebEventLog("RecalculateBalance", ex.Message.ToString, invno)
        End Try

    End Sub

    Private Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "SOA - " + Convert.ToString(Session("UserID")))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
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
                txtPrintDate.Text = txtCutOffDate.Text

            End If
        End If
        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            Dim attachment As String = "attachment; filename=OutstandingInvoiceByDueDate.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            If dt.Rows.Count > 0 Then
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


            End If


            dt.Clear()

        End If


        ''   Dim cmd As New MySqlCommand(strQuery)



        ''Create a dummy GridView
        'Dim GridView1 As New GridView()
        'GridView1.AllowPaging = False
        'GridView1.DataSource = dt
        'GridView1.DataBind()

        'Response.Clear()
        'Response.Buffer = True
        'Response.AddHeader("content-disposition", "attachment;filename=SalesInvoiceListing.xls")
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)

        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    'Apply text style to each Row
        '    GridView1.Rows(i).Attributes.Add("class", "textmode")
        'Next
        'GridView1.RenderControl(hw)

        ''style to format numbers to string
        'Dim style As String = "<style> .textmode { } </style>"
        'Response.Write(style)
        'Response.Output.Write(sw.ToString())
        'Response.Flush()
        'Response.End()

        'Dim dt As DataTable = GetDataSet()

        'Response.Clear()
        'Response.Buffer = True
        'Response.AddHeader("content-disposition", _
        '        "attachment;filename=SalesInvoiceListing.xls")
        'Response.Charset = ""
        'Response.ContentType = "application/data"

        'Dim sb As New StringBuilder()
        'For k As Integer = 0 To dt.Columns.Count - 1
        '    'add separator
        '    sb.Append(dt.Columns(k).ColumnName)
        'Next
        ''append new line
        'sb.Append(vbCr & vbLf)
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    For k As Integer = 0 To dt.Columns.Count - 1
        '        'add separator
        '        sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";"))
        '    Next
        '    'append new line
        '    sb.Append(vbCr & vbLf)
        'Next
        'Response.Output.Write(sb.ToString())
        'Response.Flush()
        'Response.End()

        'Dim dt As DataTable = GetDataSet()


        'Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
        'Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = DirectCast(xlApp.Workbooks.Add(1), Microsoft.Office.Interop.Excel.Workbook)


        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(xlWorkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)


        'Dim misvalue As Object = System.Reflection.Missing.Value
        'For i As Integer = 0 To dt.Columns.Count - 1
        '    xlSheet.Cells(1, i + 1) = dt.Columns(i).ColumnName
        'Next
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    For j As Integer = 0 To dt.Columns.Count - 1
        '        xlSheet.Cells(i + 2, j + 1) = dt.Rows(i)(j).ToString().Trim()
        '    Next
        'Next
        'xlWorkBook.SaveAs("C:\Users\Downloads\BSDSubCategoriesTemplate_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx", misvalue, misvalue, misvalue, misvalue, misvalue, _
        '    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misvalue, misvalue, misvalue, misvalue, misvalue)
        'xlWorkBook.Close(True, misvalue, misvalue)
        'xlSheet = Nothing
        'xlApp = Nothing

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


    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim lTotalReceipt As Decimal
        Dim lInvoiceAmount As Decimal

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command0 As MySqlCommand = New MySqlCommand

        command0.CommandType = CommandType.Text

        command0.CommandText = "SELECT InvoiceNumber, SalesDate,AppliedBase,doctype FROM tblsales where poststatus='P'"

        command0.Connection = conn

        Dim dr1 As MySqlDataReader = command0.ExecuteReader()
        Dim dt1 As New DataTable
        dt1.Load(dr1)

        Dim invno As String = ""

        '    Try

        If dt1.Rows.Count > 0 Then

            For i As Int32 = 0 To dt1.Rows.Count - 1

                lTotalReceipt = 0.0
                lInvoiceAmount = 0.0


                invno = dt1.Rows(i)("InvoiceNumber").ToString.Trim

                'Retrieve Invoice Amount

                lInvoiceAmount = dt1.Rows(i)("AppliedBase").ToString

                If dt1.Rows(0)("DocType") = "ARIN" Then
                    'Retrieve CN and DN Amount

                    Dim command2 As MySqlCommand = New MySqlCommand
                    command2.CommandType = CommandType.Text

                    command2.CommandText = "SELECT  a.invoicenumber as cnno FROM tblSalesDETAIL A,  tblSales B WHERE " & _
                      "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN') and A.SourceInvoice = '" & invno & "'"

                    command2.Connection = conn

                    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                    Dim dt2 As New DataTable
                    dt2.Load(dr2)

                    If dt2.Rows.Count > 0 Then

                    Else
                        'Retrieve Adjustment/Journal Amount

                        Dim command4 As MySqlCommand = New MySqlCommand
                        command4.CommandType = CommandType.Text

                        command4.CommandText = "SELECT  b.vouchernumber  FROM tbljrnvdet A, tbljrnv B WHERE " & _
                           "A.VoucherNumber=B.VoucherNumber and A.RefType = '" & invno & "' "

                        command4.Connection = conn

                        Dim dr4 As MySqlDataReader = command4.ExecuteReader()
                        Dim dt4 As New DataTable
                        dt4.Load(dr4)

                        If dt4.Rows.Count > 0 Then

                        Else

                            'Retrieve Receipt Amount for Invoice


                            Dim command3 As MySqlCommand = New MySqlCommand
                            command3.CommandType = CommandType.Text

                            command3.CommandText = "SELECT A.ValueBase+A.GstBase as totalrecv,b.receiptdate FROM tblRecvDet A, tblRecv B WHERE " & _
                             "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN' and B.PostStatus = 'P' AND A.RefType = '" & invno & "'"
                            command3.Connection = conn

                            Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                            Dim dt3 As New DataTable
                            dt3.Load(dr3)

                            If dt3.Rows.Count > 0 Then
                                If dt3.Rows.Count > 1 Then
                                Else

                                    lTotalReceipt = dt3.Rows(0)("totalrecv").ToString

                                    If lInvoiceAmount - lTotalReceipt = 0 Then
                                        Dim command21 As MySqlCommand = New MySqlCommand
                                        command21.CommandType = CommandType.Text

                                        command21.CommandText = "UPDATE tblsales SET Closingdate = @closingdate WHERE InvoiceNumber = '" & invno & "'"
                                        command21.Parameters.Clear()
                                        command21.Parameters.AddWithValue("@closingdate", dt3.Rows(0)("receiptdate"))

                                        command21.Connection = conn

                                        command21.ExecuteNonQuery()
                                        command21.Dispose()
                                    End If

                                End If

                            End If

                            dt3.Clear()
                            dt3.Dispose()
                            dr3.Close()
                            command3.Dispose()


                        End If
                        dt4.Clear()
                        dt4.Dispose()
                        dr4.Close()
                        command4.Dispose()


                    End If
                    dt2.Clear()
                    dt2.Dispose()
                    dr2.Close()
                    command2.Dispose()





                End If



            Next
            dt1.Clear()
            dt1.Dispose()
            dr1.Close()

        End If

        '    Catch ex As Exception
        '    lblAlert.Text = ex.Message.ToString + " " + recno
        '    InsertIntoTblWebEventLog("RecalculateBalance", ex.Message.ToString, invno)
        'End Try
    End Sub
End Class
