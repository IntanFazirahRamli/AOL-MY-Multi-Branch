Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports EASendMail


Partial Class RV_Select_StatementOfAccounts
    Inherits System.Web.UI.Page

    Protected Sub btnClientTo_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientTo.Click
        txtModal.Text = "ClientTo"
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

        If String.IsNullOrEmpty(txtAccountIDTo.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountIDTo.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            txtPopUpClient.Text = txtCustName.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

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

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        txtModal.Text = "ClientFrom"
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

        If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountIDFrom.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
        ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            txtPopUpClient.Text = txtCustName.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

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
        If rdbAccountName.Checked = True Then
            If txtPopupClientSearch.Text.Trim = "" Then
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany   order by name"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON   order by name"
                End If


            Else
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where   (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

                End If
            End If
        Else
            If txtPopupClientSearch.Text.Trim = "" Then
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany   order by accountid"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON   order by accountid"
                End If


            Else
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

                End If
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
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        If txtModal.Text = "ClientFrom" Then
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDFrom.Text = ""
            Else
                txtAccountIDFrom.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If
        ElseIf txtModal.Text = "ClientTo" Then
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDTo.Text = ""
            Else
                txtAccountIDTo.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If
        ElseIf txtModal.Text = "ClientName" Then
            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                txtCustName.Text = ""
            Else
                txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            End If
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDFrom.Text = ""
            Else
                txtAccountIDFrom.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDTo.Text = ""
            Else
                txtAccountIDTo.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If
        End If


        'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
        '    txtCustName.Text = ""
        'Else
        '    txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim).ToString
        'End If

    End Sub



    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged

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

    Private Function GetData() As Boolean
        lblAlert.Text = ""
        Dim qry As String = ""
        Dim selFormula As String
        Dim selection As String
        selection = ""
        If chkCheckCutOff.Checked = True Then
            selFormula = "{tblsales1.rcno} <> 0 AND {tblsales1.OPeriodBalance}<>0 and {tblsales1.PostStatus} ='P'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase, CreditBase, ReceiptBase,"
            qry = qry + "OPeriodBalance as Balance,"
            qry = qry + "StaffCode, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where poststatus='P'"
            'If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then

            '    qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"


            'End If
        Else
            selFormula = "{tblsales1.rcno} <> 0 AND {tblsales1.BalanceBase}<>0 and {tblsales1.PostStatus} ='P'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase, CreditBase, ReceiptBase,"
            qry = qry + "BalanceBase as Balance,"
            qry = qry + "StaffCode, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where balancebase <> 0 and poststatus='P'"
        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblsales1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        'If chkUnpaidBal0.Checked = True Then
        '    If chkUnpaidBal.Checked = True Then
        '        selFormula = selFormula + " and {@pUnpaid}<=0"
        '    Else
        '        selFormula = selFormula + " and {@pUnpaid}=0"
        '    End If
        'Else
        '    If chkUnpaidBal.Checked = True Then
        '        selFormula = selFormula + " and {@pUnpaid}<0"
        '    Else
        '        selFormula = selFormula + " and {@pUnpaid}>0"
        '    End If
        'End If
        'If chkUnpaidBal0.Checked = True Then
        '    If chkUnpaidBal.Checked = True Then

        '    Else
        '        selFormula = selFormula + " and {@pUnpaid}>=0"
        '    End If
        'Else
        '    If chkUnpaidBal.Checked = True Then
        '        selFormula = selFormula + " and {@pUnpaid}<>0"
        '    Else
        '        selFormula = selFormula + " and {@pUnpaid}>0"
        '    End If
        'End If
        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            selFormula = selFormula + " and {tblsales1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) = False Then
            qry = qry + " and tblsales.Accountid >= '" + txtAccountIDFrom.Text + "'"
            selFormula = selFormula + " and {tblsales1.Accountid} >= '" + txtAccountIDFrom.Text + "'"
            If selection = "" Then
                selection = "AccountID From : " + txtAccountIDFrom.Text
            Else
                selection = selection + ", AccountID From : " + txtAccountIDFrom.Text
            End If
        End If
        If String.IsNullOrEmpty(txtAccountIDTo.Text) = False Then
            qry = qry + " and tblsales.Accountid <= '" + txtAccountIDTo.Text + "'"
            selFormula = selFormula + " and {tblsales1.Accountid} <= '" + txtAccountIDTo.Text + "'"
            If selection = "" Then
                selection = "AccountID To : " + txtAccountIDTo.Text
            Else
                selection = selection + ", AccountID To : " + txtAccountIDTo.Text
            End If
        End If
        If String.IsNullOrEmpty(txtCustName.Text) = False And (String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text)) Then
            qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
            selFormula = selFormula + " and {tblsales1.CustName} like '*" + txtCustName.Text + "*'"
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
            qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
            selFormula = selFormula + " and {tblsales1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If



        If ddlLocateGrp.Text = "-1" Then
        Else
            qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            selFormula = selFormula + " and {tblsales1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            If selection = "" Then
                selection = "Location Group : " + ddlLocateGrp.Text
            Else
                selection = selection + ", Location Group : " + ddlLocateGrp.Text
            End If
        End If

        If String.IsNullOrEmpty(txtOurRef.Text) = False Then
            qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

            selFormula = selFormula + " and {tblsales1.OurRef} like '" + txtOurRef.Text + "*'"
            If selection = "" Then
                selection = "OurRef = " + txtOurRef.Text
            Else
                selection = selection + ", OurRef = " + txtOurRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtYourRef.Text) = False Then
            qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
            selFormula = selFormula + " and {tblsales1.YourRef} like '" + txtYourRef.Text + "*'"
            If selection = "" Then
                selection = "YourRef = " + txtYourRef.Text
            Else
                selection = selection + ", YourRef = " + txtYourRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            selFormula = selFormula + " and {tblsales1.StaffCode} like '" + txtIncharge.Text + "*'"
            If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    qry = qry + " and tblcontractgroup.ContractGroup = '" + ddlContractGroup.Text + "'"

        '    selFormula = selFormula + " and {tblcontractgroup1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + "<br>Department = " + ddlContractGroup.Text
        '    End If
        'End If


        If ddlSalesMan.Text = "-1" Then
        Else
            qry = qry + " and tblsales.Salesman = '" + ddlSalesMan.Text + "'"

            selFormula = selFormula + " and {tblsales1.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
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

                selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
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

                selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        qry = qry + " ORDER BY AccountId, InvoiceNumber"

        txtQuery.Text = qry
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("PrintDate", txtPrintDate.Text)

        Return True
    End Function


    Private Function GetDataInvRecv() As Boolean
        lblAlert.Text = ""
        Dim qry As String = ""
        Dim qryrecv As String = ""
        Dim qryrecv1 As String = ""
        Dim selFormula As String
        Dim selection As String
        selection = ""
        If chkCheckCutOff.Checked = True Then
            selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
            qry = qry + "OPeriodBalance as Balancebase,"
            qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where poststatus='P' and (closingdate<=salesdate or closingdate is null or"
            'If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then

            qry = qry + " tblsales.closingdate> '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "')"

            'End If
        Else
            selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
            qry = qry + "BalanceBase,"
            qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where balancebase <> 0 and poststatus='P'"
        End If
        qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
        qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"

        '  qryrecv1 = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,-tblrecvdet.appliedbase as appliedbase,-tblrecvdet.valuebase as valuebase"
        '  qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID='CONTRA'"
        '  qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where BankID='CONTRA'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {m02Sales.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryrecv = qryrecv + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryrecv1 = qryrecv1 + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        If rbtnSelectDetSumm.SelectedValue = "2" Then
            If chkUnpaidBal0.Checked = True Then
                If chkUnpaidBal.Checked = True Then

                Else
                    selFormula = selFormula + " and {m02AR22.Balance}>=0"
                End If
            Else
                If chkUnpaidBal.Checked = True Then
                    selFormula = selFormula + " and {m02AR22.Balance}<>0"
                Else
                    selFormula = selFormula + " and {m02AR22.Balance}>0"
                End If
            End If
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            qryrecv = qryrecv + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
            ' selFormula = selFormula + " and {m02AR22.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) = False Then
            qry = qry + " and tblsales.Accountid >= '" + txtAccountIDFrom.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid >= '" + txtAccountIDFrom.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid >= '" + txtAccountIDFrom.Text + "'"
            '    selFormula = selFormula + " and {m02AR22.Accountid} >= '" + txtAccountIDFrom.Text + "'"
            If selection = "" Then
                selection = "AccountID From : " + txtAccountIDFrom.Text
            Else
                selection = selection + ", AccountID From : " + txtAccountIDFrom.Text
            End If
        End If
        If String.IsNullOrEmpty(txtAccountIDTo.Text) = False Then
            qry = qry + " and tblsales.Accountid <= '" + txtAccountIDTo.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid <= '" + txtAccountIDTo.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid <= '" + txtAccountIDTo.Text + "'"

            '   selFormula = selFormula + " and {m02AR22.Accountid} <= '" + txtAccountIDTo.Text + "'"
            If selection = "" Then
                selection = "AccountID To : " + txtAccountIDTo.Text
            Else
                selection = selection + ", AccountID To : " + txtAccountIDTo.Text
            End If
        End If
        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
            qryrecv = qryrecv + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"
            qryrecv1 = qryrecv1 + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"

            '  selFormula = selFormula + " and {m02AR22.CustName} like '*" + txtCustName.Text + "*'"
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
            qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
            qryrecv = qryrecv + " and tblrecv.CompanyGroup in (" + YrStr + ")"
            qryrecv1 = qryrecv1 + " and tblrecv.CompanyGroup in (" + YrStr + ")"

            '      selFormula = selFormula + " and {m02sales.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If



        If ddlLocateGrp.Text = "-1" Then
        Else
            qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            '   selFormula = selFormula + " and {m02sales.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            If selection = "" Then
                selection = "Location Group : " + ddlLocateGrp.Text
            Else
                selection = selection + ", Location Group : " + ddlLocateGrp.Text
            End If
        End If

        If String.IsNullOrEmpty(txtOurRef.Text) = False Then
            qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

            '    selFormula = selFormula + " and {m02sales.OurRef} like '" + txtOurRef.Text + "*'"
            If selection = "" Then
                selection = "OurRef = " + txtOurRef.Text
            Else
                selection = selection + ", OurRef = " + txtOurRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtYourRef.Text) = False Then
            qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
            '  selFormula = selFormula + " and {m02sales.YourRef} like '" + txtYourRef.Text + "*'"
            If selection = "" Then
                selection = "YourRef = " + txtYourRef.Text
            Else
                selection = selection + ", YourRef = " + txtYourRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            '     selFormula = selFormula + " and {m02sales.StaffCode} like '" + txtIncharge.Text + "*'"
            If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    qry = qry + " and tblcontractgroup.ContractGroup = '" + ddlContractGroup.Text + "'"

        '    selFormula = selFormula + " and {tblcontractgroup1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + "<br>Department = " + ddlContractGroup.Text
        '    End If
        'End If


        If ddlSalesMan.Text = "-1" Then
        Else
            qry = qry + " and tblsales.Salesman = '" + ddlSalesMan.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Salesman = '" + ddlSalesMan.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Salesman = '" + ddlSalesMan.Text + "'"

            '    selFormula = selFormula + " and {m02sales.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
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
                qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
                qryrecv1 = qryrecv1 + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                '     selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
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
                qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"
                qryrecv1 = qryrecv1 + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"

                '  selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        qry = qry + " ORDER BY AccountId, InvoiceNumber"
        qryrecv = qryrecv + " ORDER BY AccountId, ReceiptNumber"
        qryrecv1 = qryrecv1 + " ORDER BY AccountId, ReceiptNumber"


        txtQuery.Text = qry
        txtQueryRecv.Text = qryrecv
        '      txtQueryRecv1.Text = qryrecv1

        InsertM02AR22()

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("PrintDate", txtPrintDate.Text)

        Return True
    End Function

    Private Function GetDataInvRecvSen() As Boolean
        lblAlert.Text = ""
        Dim qry As String = ""
        Dim qryrecv As String = ""
        Dim qryrecv1 As String = ""

        Dim qryJrnl As String = ""
        Dim qryJrnl1 As String = ""


        Dim selFormula As String
        Dim selection As String
        selection = ""
        If chkCheckCutOff.Checked = True Then
            selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
            qry = qry + "OPeriodBalance as Balancebase,"
            qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo, Rcno FROM tblsales"
            qry = qry + " where poststatus='P' "
            'qry = qry + " and (closingdate<=salesdate or closingdate is null or"
            'qry = qry + " tblsales.closingdate> '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "')"
            qry = qry + " and (tblsales.salesdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "')"
            'End If
        Else
            selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
            qry = qry + "BalanceBase,"
            qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo, Rcno FROM tblsales"
            qry = qry + " where balancebase <> 0 and poststatus='P'"
        End If

        qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
        'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.SubCode,tblrecvdet.RefType from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
        qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.SubCode,tblrecvdet.RefType, tblrecvdet.Rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "

        qryJrnl = "select tbljrnv.vouchernumber,tblrecv.journaldate,tblrecv.receiptfrom,tbljrnv.comments, tbljrnv.Location"
        'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.SubCode,tblrecvdet.RefType from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
        qryJrnl = qryJrnl + ",tbljrnvdet.debitbase,tbljrnvdet.creditbase, tbljrnvdet.LedgerCode,tblrecvdet.LedgerName, tbljrnvdet.ContactType, tbljrnvdet.Reftype ,tbljrnvdet.AccountId, tbljrnvdet.LocationId ,tbljrnvdet.ItemType,  tbljrnvdet.Rcno tbljrnvdet.Rcno from tbljrnv left outer join tbljrnvdet on tbljrnv.vouchernumber=tbljrnvdet.vouchernumber where tbljrnvdet.ItemType ='RECEIPT' and 1=1 "
        'qryJrnl = qryJrnl + ",tbljrnvdet.debitbase,tbljrnvdet.creditbase, tbljrnvdet.LedgerCode,tblrecvdet.LedgerName, tbljrnvdet.ContactType, tbljrnvdet.Reftype ,tbljrnvdet.AccountId, tbljrnvdet.LocationId ,tbljrnvdet.ItemType,  tbljrnvdet.Rcno tbljrnvdet.Rcno from tbljrnv left outer join tbljrnvdet on tbljrnv.vouchernumber=tbljrnvdet.vouchernumber where 1=1 "



        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {m02Sales.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryrecv = qryrecv + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryrecv1 = qryrecv1 + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"

            qryJrnl = qryJrnl + " and tbljrnv.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryJrnl1 = qryJrnl1 + " and tbljrnv.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        'If rbtnSelectDetSumm.SelectedValue = "2" Then
        '    If chkUnpaidBal0.Checked = True Then
        '        If chkUnpaidBal.Checked = True Then

        '        Else
        '            selFormula = selFormula + " and {m02AR22.Balance}>=0"
        '        End If
        '    Else
        '        If chkUnpaidBal.Checked = True Then
        '            selFormula = selFormula + " and {m02AR22.Balance}<>0"
        '        Else
        '            selFormula = selFormula + " and {m02AR22.Balance}>0"
        '        End If
        '    End If
        'End If

        'Start: 15.06.18
        If rbtnSelectDetSumm.SelectedValue = "2" Then
            If chkUnpaidBal0.Checked = True Then
                If chkUnpaidBal.Checked = True Then

                Else
                    selFormula = selFormula + " and  {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "
                End If
            Else
                If chkUnpaidBal.Checked = True Then
                    selFormula = selFormula + " and  {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "
                Else
                    selFormula = selFormula + " and  {vwcustomermainbillinginfo1.SendStatement}=True and {m02AR22.Balance} <> 0 "
                End If
            End If
        End If

        'End: 15.06.18

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            qryrecv = qryrecv + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"

            qryJrnl = qryJrnl + " and tbljrnvdet.ContactType = '" + ddlAccountType.Text + "'"
            qryJrnl1 = qryJrnl1 + " and tbljrnvdet.ContactType = '" + ddlAccountType.Text + "'"

            ' selFormula = selFormula + " and {m02AR22.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) = False Then
            qry = qry + " and tblsales.Accountid >= '" + txtAccountIDFrom.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid >= '" + txtAccountIDFrom.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid >= '" + txtAccountIDFrom.Text + "'"

            qryJrnl = qryJrnl + " and tbljrnvdet.Accountid = '" + txtAccountIDFrom.Text + "'"
            qryJrnl1 = qryJrnl1 + " and tbljrnvdet.Accountid = '" + txtAccountIDFrom.Text + "'"

            '    selFormula = selFormula + " and {m02AR22.Accountid} >= '" + txtAccountIDFrom.Text + "'"
            If selection = "" Then
                selection = "AccountID From : " + txtAccountIDFrom.Text
            Else
                selection = selection + ", AccountID From : " + txtAccountIDFrom.Text
            End If
        End If
        If String.IsNullOrEmpty(txtAccountIDTo.Text) = False Then
            qry = qry + " and tblsales.Accountid <= '" + txtAccountIDTo.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Accountid <= '" + txtAccountIDTo.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Accountid <= '" + txtAccountIDTo.Text + "'"

            qryJrnl = qryJrnl + " and tbljrnvdet.Accountid = '" + txtAccountIDTo.Text + "'"
            qryJrnl1 = qryJrnl1 + " and tbljrnvdet.Accountid = '" + txtAccountIDTo.Text + "'"

            '   selFormula = selFormula + " and {m02AR22.Accountid} <= '" + txtAccountIDTo.Text + "'"
            If selection = "" Then
                selection = "AccountID To : " + txtAccountIDTo.Text
            Else
                selection = selection + ", AccountID To : " + txtAccountIDTo.Text
            End If
        End If
        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
            qryrecv = qryrecv + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"
            qryrecv1 = qryrecv1 + " and tblrecv.ReceiptFrom like '%" + txtCustName.Text + "%'"

            qryJrnl = qryJrnl + " and tbljrnvdet.CustName = '" + ddlAccountType.Text + "'"
            qryJrnl1 = qryJrnl1 + " and tbljrnvdet.CustName = '" + ddlAccountType.Text + "'"

            '  selFormula = selFormula + " and {m02AR22.CustName} like '*" + txtCustName.Text + "*'"
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
            qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
            qryrecv = qryrecv + " and tblrecv.CompanyGroup in (" + YrStr + ")"
            qryrecv1 = qryrecv1 + " and tblrecv.CompanyGroup in (" + YrStr + ")"

            'qryJrnl = qryJrnl + " and tbljrnvdet.CustName = '" + ddlAccountType.Text + "'"
            'qryJrnl1 = qryJrnl1 + " and tbljrnvdet.CustName = '" + ddlAccountType.Text + "'"

            '      selFormula = selFormula + " and {m02sales.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If



        If ddlLocateGrp.Text = "-1" Then
        Else
            qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            '   selFormula = selFormula + " and {m02sales.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            If selection = "" Then
                selection = "Location Group : " + ddlLocateGrp.Text
            Else
                selection = selection + ", Location Group : " + ddlLocateGrp.Text
            End If
        End If

        If String.IsNullOrEmpty(txtOurRef.Text) = False Then
            qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

            '    selFormula = selFormula + " and {m02sales.OurRef} like '" + txtOurRef.Text + "*'"
            If selection = "" Then
                selection = "OurRef = " + txtOurRef.Text
            Else
                selection = selection + ", OurRef = " + txtOurRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtYourRef.Text) = False Then
            qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
            '  selFormula = selFormula + " and {m02sales.YourRef} like '" + txtYourRef.Text + "*'"
            If selection = "" Then
                selection = "YourRef = " + txtYourRef.Text
            Else
                selection = selection + ", YourRef = " + txtYourRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            '     selFormula = selFormula + " and {m02sales.StaffCode} like '" + txtIncharge.Text + "*'"
            If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If

        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    qry = qry + " and tblcontractgroup.ContractGroup = '" + ddlContractGroup.Text + "'"

        '    selFormula = selFormula + " and {tblcontractgroup1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + "<br>Department = " + ddlContractGroup.Text
        '    End If
        'End If


        If ddlSalesMan.Text = "-1" Then
        Else
            qry = qry + " and tblsales.Salesman = '" + ddlSalesMan.Text + "'"
            qryrecv = qryrecv + " and tblrecv.Salesman = '" + ddlSalesMan.Text + "'"
            qryrecv1 = qryrecv1 + " and tblrecv.Salesman = '" + ddlSalesMan.Text + "'"

            '    selFormula = selFormula + " and {m02sales.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
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
                qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
                qryrecv1 = qryrecv1 + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                qryJrnl = qryJrnl + " and tbljrnv.journaldate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
                qryJrnl1 = qryJrnl1 + " and tbljrnv.journaldate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                '     selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
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
                qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"
                qryrecv1 = qryrecv1 + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"

                qryJrnl = qryJrnl + " and tbljrnv.journaldate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
                qryJrnl1 = qryJrnl1 + " and tbljrnv.journaldate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

                '  selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        qry = qry + " ORDER BY AccountId, InvoiceNumber"
        qryrecv = qryrecv + " ORDER BY AccountId, tblRecv.ReceiptNumber"
        qryrecv1 = qryrecv1 + " ORDER BY AccountId, tblRecv.ReceiptNumber"

        qryJrnl = qryJrnl + " ORDER BY tbljrnvdet.AccountId, tbljrnv.VoucherNumber"
        qryJrnl1 = qryJrnl1 + " ORDER BY tbljrnvdet.AccountId, tbljrnv.VoucherNumber"

        txtQuery.Text = qry
        txtQueryRecv.Text = qryrecv
        txtQueryJrnl.Text = qryJrnl

        '      txtQueryRecv1.Text = qryrecv1

        InsertM02AR22Sen()

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("PrintDate", txtPrintDate.Text)

        Return True
    End Function
    Private Sub InsertM02AR22()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command0 As MySqlCommand = New MySqlCommand
            command0.CommandType = CommandType.Text
            command0.CommandText = "delete from tblAR22 where Joborder='" + Convert.ToString(Session("UserID")) + "'"
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
                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode, ReferenceNumber) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode,@ReferenceNumber);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@VoucherDate", dt.Rows(i)("SalesDate"))
                    command.Parameters.AddWithValue("@Type", "INVOICE")
                    command.Parameters.AddWithValue("@VoucherNumber", dt.Rows(i)("InvoiceNumber"))
                    command.Parameters.AddWithValue("@Debit", dt.Rows(i)("AppliedBase"))
                    command.Parameters.AddWithValue("@Credit", dt.Rows(i)("CreditBase") + dt.Rows(i)("ReceiptBase"))
                    command.Parameters.AddWithValue("@Balance", dt.Rows(i)("BalanceBase"))
                    command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
                    command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountID"))
                    command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@Comments", dt.Rows(i)("Comments"))
                    command.Parameters.AddWithValue("@AccountDate", dt.Rows(i)("SalesDate"))
                    command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
                    command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
                    command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
                    command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
                    command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
                    command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
                    command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)
                    command.Parameters.AddWithValue("@ReferenceNumber", dt.Rows(i)("InvoiceNumber"))
                    command.Connection = conn

                    command.ExecuteNonQuery()
                    command.Dispose()

                Next


            End If
            dt.Clear()
            dr.Close()
            dt.Dispose()
            command1.Dispose()

            For j As Int16 = 0 To 1
                Dim command2 As MySqlCommand = New MySqlCommand

                command2.CommandType = CommandType.Text

                If j = 0 Then
                    command2.CommandText = txtQueryRecv.Text
                ElseIf j = 1 Then
                    command2.CommandText = txtQueryRecv1.Text

                End If
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt2.Rows.Count - 1
                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode, ReferenceNumber) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode, @ReferenceNumber);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@VoucherDate", dt2.Rows(i)("ReceiptDate"))
                        command.Parameters.AddWithValue("@Type", "RECEIPT")
                        command.Parameters.AddWithValue("@VoucherNumber", dt2.Rows(i)("Cheque"))
                        command.Parameters.AddWithValue("@Debit", 0)
                        command.Parameters.AddWithValue("@Credit", dt2.Rows(i)("AppliedBase"))
                        command.Parameters.AddWithValue("@Balance", -(dt2.Rows(i)("AppliedBase")))
                        command.Parameters.AddWithValue("@ContactType", dt2.Rows(i)("ContactType"))
                        command.Parameters.AddWithValue("@AccountId", dt2.Rows(i)("AccountID"))
                        command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@Comments", dt2.Rows(i)("Comments"))
                        command.Parameters.AddWithValue("@AccountDate", dt2.Rows(i)("ReceiptDate"))
                        command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
                        command.Parameters.AddWithValue("@CustName", dt2.Rows(i)("ReceiptFrom"))
                        command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@ReferenceNumber", dt2.Rows(i)("receiptnumber"))
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


            'Journal

            For k As Int16 = 0 To 1
                Dim command3 As MySqlCommand = New MySqlCommand

                command3.CommandType = CommandType.Text

                If k = 0 Then
                    command3.CommandText = txtQueryJrnl.Text
                ElseIf k = 1 Then
                    command3.CommandText = txtQueryJrnl1.Text

                End If
                command3.Connection = conn

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New DataTable
                dt3.Load(dr3)

                If dt3.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt3.Rows.Count - 1
                        Dim command As MySqlCommand = New MySqlCommand

                        command.CommandType = CommandType.Text
                        Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode, ReferenceNumber) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode, @ReferenceNumber);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@VoucherDate", dt3.Rows(i)("JournalDate"))
                        command.Parameters.AddWithValue("@Type", "RECEIPT")
                        command.Parameters.AddWithValue("@VoucherNumber", dt3.Rows(i)("VoucherNumber"))
                        command.Parameters.AddWithValue("@Debit", dt3.Rows(i)("DebitBase"))
                        command.Parameters.AddWithValue("@Credit", dt3.Rows(i)("DebitBase") - dt3.Rows(i)("CreditBase"))
                        command.Parameters.AddWithValue("@Balance", -(dt3.Rows(i)("AppliedBase")))
                        command.Parameters.AddWithValue("@ContactType", dt3.Rows(i)("ContactType"))
                        command.Parameters.AddWithValue("@AccountId", dt3.Rows(i)("AccountID"))
                        command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        command.Parameters.AddWithValue("@Comments", dt3.Rows(i)("Comments"))
                        command.Parameters.AddWithValue("@AccountDate", dt3.Rows(i)("JournalDate"))
                        command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
                        command.Parameters.AddWithValue("@CustName", dt3.Rows(i)("CustName"))
                        command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)
                        command.Parameters.AddWithValue("@ReferenceNumber", dt3.Rows(i)("VoucherNumber"))
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

            'Journal


            conn.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InserM02AR22", ex.Message.ToString, "")

        End Try

    End Sub


    Private Sub InsertM02AR22Sen()
        Try
            Dim sqlst, sqlst1, isWhere, isWhere1, sqlst2, isWhere2 As String
            Dim inIsWhere, inIsWhere1, inIsWhere2 As Integer

            sqlst = ""
            sqlst1 = ""
            sqlst2 = ""


            isWhere = txtQuery.Text
            inIsWhere = isWhere.IndexOf("where")

            'txtCustName.Text = ""

            sqlst = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,ReferenceNumber, ModuleRcno, DocumentType) SELECT SalesDate, 'INVOICE',  InvoiceNumber, appliedbase, (CreditBase+ReceiptBase), OPeriodBalance, contacttype, AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' ,comments, SalesDate,'" & Session("UserID") & "', CustName, InvoiceNumber, Rcno, Doctype from tblSales "
            sqlst = sqlst + " " + txtQuery.Text.Substring(inIsWhere)

            InsertIntoTblWebEventLog("Sales", sqlst, "SEN")
            ''''''''''''''''''''''''
            'txtCustName.Text = txtQueryRecv.Text
            isWhere1 = txtQueryRecv.Text
            inIsWhere1 = isWhere1.IndexOf("where")

            'txtCustName.Text = ""

            sqlst1 = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tblrecv.ReceiptDate, 'RECEIPT',  tblrecv.Cheque, 0, tblrecvdet.appliedbase, tblrecvdet.appliedbase * (-1), tblrecv.contacttype, tblrecv.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblrecv.comments, tblrecv.ReceiptDate,'" & Session("UserID") & "', tblrecv.ReceiptFrom,  tblRecv.ReceiptNumber, tblRecvdet.Rcno, 'RECV' from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber  "
            sqlst1 = sqlst1 + " " + txtQueryRecv.Text.Substring(inIsWhere1)

            InsertIntoTblWebEventLog("Receipt", sqlst1, "SEN")


            isWhere2 = txtQueryJrnl.Text
            InsertIntoTblWebEventLog("Journal", txtQueryJrnl.Text, "SEN")
            inIsWhere2 = isWhere2.IndexOf("where")
            InsertIntoTblWebEventLog("Journal", inIsWhere2, "SEN")

            sqlst2 = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tbljrnv.JournalDate, 'JOURNAL',  tbljrnv.VoucherNumber, 0, tblJrnvDet.debitbase-tblJrnvDet.Creditbase, tblJrnvDet.debitbase-tblJrnvDet.Creditbase, tblJrnvDet.contacttype, tblJrnvDet.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblJrnv.comments, tblJrnv.JournalDate,'" & Session("UserID") & "', tblJrnvDet.CustName,  tblJrnvdet.VoucherNumber, tblJrnvDet.Rcno, 'JRNV' from tblJrnv left outer join tblJrnvdet on tblJrnv.VoucherNumber=tblJrnvdet.VoucherNumber  "
            sqlst2 = sqlst2 + " " + txtQueryJrnl.Text.Substring(inIsWhere2)

            InsertIntoTblWebEventLog("Journal", sqlst2, "SEN")
            ''''''''''''''''''''''''''

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            If rbtnSelectDetSumm.SelectedIndex = 1 Then
                command.CommandText = "SaveTbwAR22New"
            Else
                command.CommandText = "SaveTbwAR22New"
            End If

            command.Parameters.Clear()
            command.CommandTimeout = 3000
            'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
            command.Parameters.AddWithValue("@pr_Query", sqlst)
            command.Parameters.AddWithValue("@pr_Query1", sqlst1)
            command.Parameters.AddWithValue("@pr_Query2", sqlst2)
            command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
            command.Connection = conn
            command.ExecuteScalar()
            conn.Close()
            conn.Dispose()
            command.Dispose()


            '''''''''''''''''''''''''''''''''''''''''

            'Dim command0 As MySqlCommand = New MySqlCommand
            'command0.CommandType = CommandType.Text
            'command0.CommandText = "delete from tblAR22 where Joborder='" + Convert.ToString(Session("UserID")) + "'"
            'command0.Connection = conn
            'command0.ExecuteNonQuery()

            'command0.Dispose()

            'Dim command1 As MySqlCommand = New MySqlCommand

            'command1.CommandType = CommandType.Text

            'command1.CommandText = txtQuery.Text
            'command1.Connection = conn

            'Dim dr As MySqlDataReader = command1.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then
            '    For i As Int16 = 0 To dt.Rows.Count - 1
            '        Dim command As MySqlCommand = New MySqlCommand

            '        command.CommandType = CommandType.Text
            '        Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

            '        command.CommandText = qry
            '        command.Parameters.Clear()

            '        command.Parameters.AddWithValue("@VoucherDate", dt.Rows(i)("SalesDate"))
            '        command.Parameters.AddWithValue("@Type", "INVOICE")
            '        command.Parameters.AddWithValue("@VoucherNumber", dt.Rows(i)("InvoiceNumber"))
            '        command.Parameters.AddWithValue("@Debit", dt.Rows(i)("AppliedBase"))
            '        command.Parameters.AddWithValue("@Credit", dt.Rows(i)("CreditBase") + dt.Rows(i)("ReceiptBase"))
            '        command.Parameters.AddWithValue("@Balance", dt.Rows(i)("BalanceBase"))
            '        command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
            '        command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountID"))
            '        command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            '        command.Parameters.AddWithValue("@Comments", dt.Rows(i)("Comments"))
            '        command.Parameters.AddWithValue("@AccountDate", dt.Rows(i)("SalesDate"))
            '        command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
            '        command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
            '        command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
            '        command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
            '        command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
            '        command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
            '        command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

            '        command.Connection = conn

            '        command.ExecuteNonQuery()
            '        command.Dispose()

            '    Next


            'End If
            'dt.Clear()
            'dr.Close()
            'dt.Dispose()
            'command1.Dispose()

            'For j As Int16 = 0 To 1
            '    Dim command2 As MySqlCommand = New MySqlCommand

            '    command2.CommandType = CommandType.Text

            '    If j = 0 Then
            '        command2.CommandText = txtQueryRecv.Text
            '    ElseIf j = 1 Then
            '        command2.CommandText = txtQueryRecv1.Text

            '    End If
            '    command2.Connection = conn

            '    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            '    Dim dt2 As New DataTable
            '    dt2.Load(dr2)

            '    If dt2.Rows.Count > 0 Then
            '        For i As Int16 = 0 To dt2.Rows.Count - 1
            '            Dim command As MySqlCommand = New MySqlCommand

            '            command.CommandType = CommandType.Text
            '            Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

            '            command.CommandText = qry
            '            command.Parameters.Clear()

            '            command.Parameters.AddWithValue("@VoucherDate", dt2.Rows(i)("ReceiptDate"))
            '            command.Parameters.AddWithValue("@Type", "RECEIPT")
            '            command.Parameters.AddWithValue("@VoucherNumber", dt2.Rows(i)("Cheque"))
            '            command.Parameters.AddWithValue("@Debit", 0)
            '            command.Parameters.AddWithValue("@Credit", dt2.Rows(i)("AppliedBase"))
            '            command.Parameters.AddWithValue("@Balance", -(dt2.Rows(i)("AppliedBase")))
            '            command.Parameters.AddWithValue("@ContactType", dt2.Rows(i)("ContactType"))
            '            command.Parameters.AddWithValue("@AccountId", dt2.Rows(i)("AccountID"))
            '            command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            '            command.Parameters.AddWithValue("@Comments", dt2.Rows(i)("Comments"))
            '            command.Parameters.AddWithValue("@AccountDate", dt2.Rows(i)("ReceiptDate"))
            '            command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
            '            command.Parameters.AddWithValue("@CustName", dt2.Rows(i)("ReceiptFrom"))
            '            command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
            '            command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
            '            command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
            '            command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
            '            command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

            '            command.Connection = conn

            '            command.ExecuteNonQuery()

            '            command.Dispose()

            '        Next

            '    End If

            '    command2.Dispose()
            '    dt2.Clear()
            '    dt2.Dispose()
            '    dr2.Close()

            'Next

            'conn.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("InserM02AR22", ex.Message.ToString, "")

        End Try

    End Sub
    Private Sub GetDataSet()
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQuery.Text


        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            If chkCheckCutOff.Checked = True Then
                If dt.Rows.Count > 0 Then
                    RecalculateBalance(dt, conn)
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Sub


    Private Sub GetDataSetInvRecv()
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tblar22 where JobOrder = '" + Convert.ToString(Session("UserID")) + "'"


        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            If chkCheckCutOff.Checked = True Then
                If dt.Rows.Count > 0 Then
                    RecalculateBalanceInvRecv(dt, conn)
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Sub

    Private Sub GetDataSetInvRecvSen()
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select * from tblar22 where JobOrder = '" + Convert.ToString(Session("UserID")) + "'"


        cmd.Connection = conn
        Try
            conn.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            If chkCheckCutOff.Checked = True Then
                If dt.Rows.Count > 0 Then
                    RecalculateBalanceInvRecv(dt, conn)
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
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
        For i As Int64 = 0 To dt.Rows.Count - 1

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
                    lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString
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
            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString + " " + recno
                InsertIntoTblWebEventLog("RecalculateBalance", ex.Message.ToString, invno)
            End Try

        Next

    End Sub

    Private Sub RecalculateBalanceInvRecv(dt As DataTable, conn As MySqlConnection)
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
        For i As Int64 = 0 To dt.Rows.Count - 1

            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalRecvcn = 0.0
            lTotalcn = 0.0
            lTotalJournalAmt = 0.0
            lbalance = 0.0
            If dt.Rows(i)("Type") = "INVOICE" Then

                invno = dt.Rows(i)("VoucherNumber").ToString.Trim

                Try

                    'Retrieve Invoice Amount
                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT AppliedBase,doctype,balancebase FROM tblSales where InvoiceNumber = '" & invno & "'"
                    command1.Connection = conn

                    Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                    Dim dt1 As New DataTable
                    dt1.Load(dr1)

                    If dt1.Rows.Count > 0 Then
                        lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString
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

                        command21.CommandText = "UPDATE tblar22 SET Balance = '" & lbalance & "', Debit='" & lInvoiceAmount & "',Credit='" & lTotalcn + lTotalReceipt & "' WHERE VoucherNumber = '" & invno & "'"
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

                        command21.CommandText = "UPDATE tblar22 SET Balance = '" & dt1.Rows(0)("balancebase").ToString & "', Debit=0,Credit=0 WHERE VoucherNumber = '" & invno & "'"
                        command21.Connection = conn

                        command21.ExecuteNonQuery()


                        command21.Dispose()

                    End If
                Catch ex As Exception
                    lblAlert.Text = ex.Message.ToString + " " + recno
                    InsertIntoTblWebEventLog("RecalculateBalanceInvRecv", ex.Message.ToString, invno)
                End Try
            End If
        Next

    End Sub


    Private Sub RecalculateBalanceInvRecvSen(dt As DataTable, conn As MySqlConnection)
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
        For i As Int64 = 0 To dt.Rows.Count - 1

            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalRecvcn = 0.0
            lTotalcn = 0.0
            lTotalJournalAmt = 0.0
            lbalance = 0.0
            If dt.Rows(i)("Type") = "INVOICE" Then

                invno = dt.Rows(i)("VoucherNumber").ToString.Trim

                Try

                    'Retrieve Invoice Amount
                    Dim command1 As MySqlCommand = New MySqlCommand

                    command1.CommandType = CommandType.Text

                    command1.CommandText = "SELECT AppliedBase,doctype,balancebase FROM tblSales where InvoiceNumber = '" & invno & "'"
                    command1.Connection = conn

                    Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                    Dim dt1 As New DataTable
                    dt1.Load(dr1)

                    If dt1.Rows.Count > 0 Then
                        lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString



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

                        command21.CommandText = "UPDATE tblar22 SET Balance = '" & lbalance & "', Debit='" & lInvoiceAmount & "',Credit='" & lTotalcn + lTotalReceipt & "' WHERE VoucherNumber = '" & invno & "'"
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

                        command21.CommandText = "UPDATE tblar22 SET Balance = '" & dt1.Rows(0)("balancebase").ToString & "', Debit=0,Credit=0 WHERE VoucherNumber = '" & invno & "'"
                        command21.Connection = conn

                        command21.ExecuteNonQuery()


                        command21.Dispose()

                    End If
                Catch ex As Exception
                    lblAlert.Text = ex.Message.ToString + " " + recno
                    InsertIntoTblWebEventLog("RecalculateBalanceInvRecv", ex.Message.ToString, invno)
                End Try
            End If
        Next

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

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable ' = GetDataSet()
            Dim attachment As String = "attachment; filename=StatementOfAccounts.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
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
        ddlAccountType.SelectedIndex = 0
        txtAccountIDFrom.Text = ""
        txtAccountIDTo.Text = ""
        txtCustName.Text = ""
        txtOurRef.Text = ""
        txtYourRef.Text = ""
        txtIncharge.Text = ""
        ddlCompanyGrp.SelectedIndex = 0

        ddlLocateGrp.SelectedIndex = 0
        ddlSalesMan.SelectedIndex = 0
        txtPrintDate.Text = Convert.ToString(Session("SysDate"))
        chkCheckCutOff.Checked = False
        txtCutOffDate.Text = ""
        lblAlert.Text = ""
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")
            txtPrintDate.Text = Convert.ToString(Session("SysDate"))
            txtCutOffDate.Enabled = True
            txtCutOffDate.Text = Convert.ToString(Session("SysDate"))

            Dim query As String
            query = ""

            query = "SELECT contractgroup FROM tblcontractgroup ORDER BY contractgroup"
            PopulateDropDownList(query, "contractgroup", "contractgroup", ddlContractGroup)
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
            InsertIntoTblWebEventLog("SOA - " + Session("UserID"), ex.Message.ToString, txtCutOffDate.Text)
            'InsertIntoTblWebEventLog("SOA", ex.Message.ToString, txtCutOffDate.Text)
        End Try
    End Sub
    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        lblAlert.Text = ""


        System.Threading.Thread.Sleep(5000)
        lblAlert.Text = ""

        'UpdatePanel2.Update()
        If String.IsNullOrEmpty(txtPrintDate.Text) Then
            txtPrintDate.Text = Convert.ToString(Session("SysDate"))
        End If

        Try


            ''''''''''''''''''''
            lblAlert.Text = ""

            Dim str1 As String = ""
            Dim str2 As String = ""
            Dim str3 As String = ""
            str1 = "`tbwSOArptar_" & Session("UserID") & "`"
            str2 = "`tbwSOArptar1_" & Session("UserID") & "`"
            str3 = "`tbwsoarptaccountidar_" & Session("UserID") & "`"

            str1 = str1.Replace(".", "")
            str2 = str2.Replace(".", "")
            str3 = str3.Replace(".", "")

            If txtAccountIDFrom.Text = txtAccountIDTo.Text Then
                Dim conn1 As MySqlConnection = New MySqlConnection()
                Dim commandE1 As MySqlCommand = New MySqlCommand

                conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn1.Open()

                commandE1.CommandType = CommandType.Text
                commandE1.CommandText = "select SendStatement as SS from tblCompany where  AccountID = '" & txtAccountIDFrom.Text & "'"
                commandE1.Connection = conn1

                Dim drservice As MySqlDataReader = commandE1.ExecuteReader()
                Dim dtservice As New DataTable
                dtservice.Load(drservice)

                If dtservice.Rows.Count > 0 Then
                    If dtservice.Rows(0)("SS").ToString() = False Then
                        lblAlert.Text = "Account " & txtAccountIDFrom.Text & " does not require a Statement of Account, this report will NOT display any information for " & txtAccountIDFrom.Text
                        conn1.Close()
                        conn1.Dispose()
                        Exit Sub
                    End If

                Else
                    commandE1.CommandType = CommandType.Text
                    commandE1.CommandText = "select SendStatement as SS from tblPerson where  AccountID = '" & txtAccountIDFrom.Text & "'"
                    commandE1.Connection = conn1

                    Dim drservice1 As MySqlDataReader = commandE1.ExecuteReader()
                    Dim dtservice1 As New DataTable
                    dtservice1.Load(drservice1)

                    If dtservice1.Rows.Count > 0 Then
                        If dtservice1.Rows(0)("SS").ToString() = False Then
                            lblAlert.Text = "Account " & txtAccountIDFrom.Text & " does not require a Statement of Account, this report will NOT display any information for " & txtAccountIDFrom.Text
                            conn1.Close()
                            conn1.Dispose()
                            Exit Sub
                        End If

                    End If

                End If

            End If


            '''''''''''''''''''''


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

            '''''''''''''''''''''''''''''''''''''''''''''''''''

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


            'Locked? 

            'Exit Sub
            Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
                  "Serial, LogDate, Comments,SOURCESQLID) " & _
                  "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
            '"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text
            command.CommandText = strSql
            command.Parameters.Clear()
            'Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@StaffID", Session("UserID"))
            command.Parameters.AddWithValue("@Module", "REPORTS")
            command.Parameters.AddWithValue("@DocRef", "SOA")
            command.Parameters.AddWithValue("@Action", "")
            command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
            command.Parameters.AddWithValue("@Serial", "")
            'command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
            command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
            'command.Parameters.AddWithValue("@Amount", 0)
            'command.Parameters.AddWithValue("@BaseValue", 0)
            'command.Parameters.AddWithValue("@BaseGSTValue", 0)
            'command.Parameters.AddWithValue("@CustCode", "")
            command.Parameters.AddWithValue("@Comments", "")
            command.Parameters.AddWithValue("@SOURCESQLID", 0)
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Dim conn As MySqlConnection = New MySqlConnection(constr)
            conn.Open()
            command.Connection = conn
            command.ExecuteNonQuery()

            'conn.Close()
            'conn.Dispose()
            command.Dispose()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Session.Add("ClientName", txtCustName.Text)
            Session.Add("CutOffDate", txtCutOffDate.Text)

            If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                If GetData() = True Then
                    If chkCheckCutOff.Checked = True Then
                        GetDataSet()

                        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=CutOff")
                    Else
                        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=Today")
                    End If

                    '   Session.Add("Type", "PrintPDF")
                    'If rbtnSelectDetSumm.SelectedValue = "1" Then
                    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
                    'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
                    'End If

                Else
                    Return

                End If

            ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then


                'If GetDataInvRecv() = True Then
                '    GetDataSetInvRecv()

                '    Response.Redirect("RV_StatementOfAccounts_InvRecv.aspx?Type=DEBIT")

                '    'Session.Add("Type", "PrintPDF")
                '    'If rbtnSelectDetSumm.SelectedValue = "1" Then
                '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
                '    'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
                '    'End If

                'Else
                '    Return

                'End If
                InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "1", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountIDFrom.Text & " : " & rbtnSelectDetSumm.SelectedIndex)


                If GetDataInvRecvSen() = True Then
                    InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "2", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountIDFrom.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    'Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.StoredProcedure

                    'command.CommandText = "SaveTbwARDetail1SOA"
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

                    command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountIDFrom.Text.Trim)
                    command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)


                    'command.Parameters.AddWithValue("@pr_ContractGroupFrom", ddlContractGroup.Text.Trim)
                    'command.Parameters.AddWithValue("@pr_ContractGroupTo", ddlContractGroup.Text.Trim)

                    'command.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))

                    command.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                    command.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                    command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)

                    command.Parameters.AddWithValue("@pr_ReportName", "SOA-Report")

                    'command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)
                    'command.Parameters.AddWithValue("@pr_CustName", txtCustName.Text.Trim)

                    'If ddlAccountType.Text = "-1" And String.IsNullOrEmpty(txtCustName.Text.Trim) = True Then
                    '    command.Parameters.AddWithValue("@pr_SeletionCode", "NoAccountTypeCustName")
                    'ElseIf ddlAccountType.Text <> "-1" And String.IsNullOrEmpty(txtCustName.Text.Trim) = True Then
                    '    command.Parameters.AddWithValue("@pr_SeletionCode", "AccountTypeOnlyNoCustName")
                    'ElseIf ddlAccountType.Text <> "-1" And String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
                    '    command.Parameters.AddWithValue("@pr_SeletionCode", "BothAccountTypeCustName")
                    'ElseIf ddlAccountType.Text = "-1" And String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
                    '    command.Parameters.AddWithValue("@pr_SeletionCode", "NoAccountTypeCustNameOnly")
                    'End If
                    InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "Can?", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountIDFrom.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    command.Connection = conn
                    command.ExecuteScalar()
                    InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "3", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountIDFrom.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    'conn.Close()
                    'conn.Dispose()
                    command.Dispose()

                    '''''''''''''''''''''''''''''''''''''

                    'Dim command5 As MySqlCommand = New MySqlCommand
                    'command5.CommandType = CommandType.StoredProcedure

                    'command5.CommandText = "SaveTbwARDetail2SOANew"
                    'command5.Parameters.Clear()

                    'command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                    'command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                    'command5.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                    'command5.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArpt1ar_" & Session("UserID"))
                    'command5.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))
                    ''command5.Parameters.AddWithValue("@pr_SOA_vw", "SOA_vw_" & Session("UserID"))

                    'command5.Connection = conn
                    'command5.ExecuteScalar()
                    ''conn.Close()
                    ''conn.Dispose()
                    'command5.Dispose()


                    ''''''''''''''''''''''''''''''''''''''
                    'GetDataSetInvRecv()
                    InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "4", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & txtAccountIDFrom.Text & " : " & rbtnSelectDetSumm.SelectedIndex)

                    UnLockReport()
                    Response.Redirect("RV_StatementOfAccounts_InvRecv.aspx?Type=DEBIT")
                Else
                    Return

                End If

            ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "3" Then
                'If GetData() = True Then
                '    If chkCheckCutOff.Checked = True Then
                '        GetDataSet()

                '        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=CutOffZERO")
                '    Else
                '        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=TodayZERO")
                '    End If

                '    '   Session.Add("Type", "PrintPDF")
                '    'If rbtnSelectDetSumm.SelectedValue = "1" Then
                '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
                '    'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
                '    'End If

                'Else
                '    Return

                'End If
                If GetDataInvRecvSen() = True Then
                    'GetDataSetInvRecv()

                    'Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.StoredProcedure
                    command.CommandTimeout = 3000
                    'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                    'If rbtnSelectDetSumm.SelectedIndex = 0 Then
                    '    command.CommandText = "SaveTbwARDetail1SOA"
                    'Else
                    '    'command.CommandText = "SaveTbwARSummary"
                    '    command.CommandText = "SaveTbwARDetail1"
                    'End If
                    command.CommandText = "SaveTbwARDetail3SOANew"
                    command.Parameters.Clear()

                    'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                    'command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                    'If ddlAccountType.Text = "-1" Then
                    '    command.Parameters.AddWithValue("@pr_AccountType", "")
                    'Else
                    '    command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
                    'End If

                    'command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountIDFrom.Text.Trim)
                    'command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)

                    '17.11.20

                    command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

                    If ddlAccountType.Text = "-1" Then
                        command.Parameters.AddWithValue("@pr_AccountType", "")
                    Else
                        command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
                    End If

                    command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountIDFrom.Text.Trim)
                    command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)

                    'command.Parameters.AddWithValue("@pr_ContractGroupFrom", ddlContractGroup.Text.Trim)
                    'command.Parameters.AddWithValue("@pr_ContractGroupTo", ddlContractGroup.Text.Trim)

                    'command.Parameters.AddWithValue("@pr_tbwarSOA", "tbwSOArptar_" & Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_tbwar1SOA", "tbwSOArptar1_" & Session("UserID"))
                    'command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", "tbwsoarptaccountidar_" & Session("UserID"))

                    command.Parameters.AddWithValue("@pr_tbwarSOA", str1)
                    command.Parameters.AddWithValue("@pr_tbwar1SOA", str2)
                    command.Parameters.AddWithValue("@pr_tbwaraccountidSOA", str3)

                    command.Parameters.AddWithValue("@pr_ReportName", "SOA-Report")

                    '17.11.20
                    command.Connection = conn
                    command.ExecuteScalar()
                    'conn.Close()
                    'conn.Dispose()
                    command.Dispose()

                    '''''''''''''''''''''''''''''''''''''

                    'Dim command5 As MySqlCommand = New MySqlCommand
                    'command5.CommandType = CommandType.StoredProcedure

                    'command5.CommandText = "SaveTbwARDetail2SOA"
                    'command5.Parameters.Clear()

                    'command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
                    'command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
                    'command5.Connection = conn
                    'command5.ExecuteScalar()
                    ''conn.Close()
                    ''conn.Dispose()
                    'command5.Dispose()


                    UnLockReport()
                    Response.Redirect("RV_StatementOfAccounts_InvRecv.aspx?Type=ZERO")

                    '   Session.Add("Type", "PrintPDF")
                    'If rbtnSelectDetSumm.SelectedValue = "1" Then
                    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
                    'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
                    'End If

                Else
                    Return

                End If
            End If
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblAgeingEventlog("btnPrintServiceRecordList_Click : " + Session("UserID"), "ERROR", "", ddlAccountType.Text & " : " & txtCutOffDate.Text & " : " & ex.Message.ToString & " : " & rbtnSelectDetSumm.SelectedIndex)

            lblAlert.Text = ex.Message.ToString


            'SEND EMAIL TO ADMIN WHEN ERROR

            'Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.'
            'Lock wait timeout exceeded; try restarting transaction'
            'Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.'


            If Left(ex.Message.ToString, 15).Trim = "Timeout expired" Or Left(ex.Message.ToString, 15).Trim = "Insufficient st" Or Left(ex.Message.ToString, 15).Trim = "Lock wait timeo" Then

                Dim connadmin As MySqlConnection = New MySqlConnection()

                connadmin.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

                If connadmin.State = ConnectionState.Open Then
                    connadmin.Close()
                    connadmin.Dispose()
                End If
                connadmin.Open()

                Dim commandAdmin As MySqlCommand = New MySqlCommand

                commandAdmin.CommandType = CommandType.Text
                commandAdmin.Connection = connadmin
                commandAdmin.CommandText = "Select EmailPerson from tblstaff where StaffId='ADMIN'"
                commandAdmin.Connection = connadmin

                Dim dradmin As MySqlDataReader = commandAdmin.ExecuteReader()
                Dim dtadmin As New DataTable
                dtadmin.Load(dradmin)

                If dtadmin.Rows.Count > 0 Then
                    Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
                    Dim oSmtp As New SmtpClient()

                    oMail.Subject = "NOTIFICATION: AOL ERROR "

                    Dim content As String = ""
                    content = "Auto-generated Email for Error : " + ex.Message.ToString
                    content = content + " <br/>" + "Date : " + DateTime.Now.ToString("yyyy-MM-dd", New System.Globalization.CultureInfo("en-GB"))
                    content = content + " <br/>" + "Time : " + DateTime.Now.ToString("HH:mm:ss", New System.Globalization.CultureInfo("en-GB"))
                    content = content + " <br/>" + "Staff ID : " + Convert.ToString(Session("UserID")).ToUpper
                    content = content + " <br/>" + "Staff Name : " + Convert.ToString(Session("Name")).ToUpper
                    content = content + " <br/>" + "Domain : " + ConfigurationManager.AppSettings("DomainName").ToString()
                    content = content + " <br/>" + "Source : Statement of Accounts"
                    oMail.HtmlBody = content

                    Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
                    oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
                    oServer.ConnectType = SmtpConnectType.ConnectDirectSSL

                    oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
                    oMail.To = dtadmin.Rows(0)("EmailPerson").ToString

                    oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
                    oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

                    oSmtp.SendMail(oServer, oMail)

                End If

                connadmin.Close()
                dradmin.Close()
                dtadmin.Dispose()
                commandAdmin.Dispose()

            Else
                lblAlert.Text = ex.Message.ToString

               
            End If
            'SEND EMAIL TO ADMIN WHEN LOGIN IS LOCKED OR DISABLED
            '''''''''''''''''''''''''''''''''''''
            UnLockReport()
        End Try

        ' Catch ex As Exception
        '    InsertIntoTblWebEventLog("PERSON - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
        'End Try
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
            InsertIntoTblWebEventLog("LockReport", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("UnLockReport", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
        'Locked? 
    End Sub
    'Private Sub SendEmail(ToEmail As String, otp As String, StaffName As String)
    '    Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
    '    Dim oSmtp As New SmtpClient()

    '    oMail.From = ConfigurationManager.AppSettings("EmailFrom").ToString()
    '    oMail.Subject = "ANTICIMEX LOGIN OTP"
    '    oMail.HtmlBody = "Hi " + StaffName + ",<br/><br/>Welcome to AOL " + lblDomainName.Text + ".<br/><br/>Your One-Time Password(OTP) for your Anticimex login is : " + otp + "<br/><br/>Please note that this OTP is valid only for 10 minutes. Time Generated: " + lblSysTime.Text + "<br/><br/>Thank You.<br/><br/>-AOL Secure Login."


    '    oMail.To = ToEmail

    '    Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
    '    oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
    '    oServer.ConnectType = SmtpConnectType.ConnectDirectSSL
    '    oServer.User = ConfigurationManager.AppSettings("EmailFrom").ToString()
    '    oServer.Password = ConfigurationManager.AppSettings("EmailPassword").ToString()

    '    oSmtp.SendMail(oServer, oMail)
    '    oSmtp.Close()

    'End Sub
    Protected Sub btnClientName_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientName.Click
        txtModal.Text = "ClientName"
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

    Protected Sub chkCheckCutOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckCutOff.CheckedChanged
        If chkCheckCutOff.Checked = True Then
            txtCutOffDate.Enabled = True

        Else
            txtCutOffDate.Enabled = False

        End If
    End Sub

    Protected Sub txtPrintDate_TextChanged(sender As Object, e As EventArgs) Handles txtPrintDate.TextChanged

    End Sub

    Protected Sub rdbAccountId_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAccountId.CheckedChanged
        If txtPopUpClient.Text.Trim = "" Or txtPopUpClient.Text.Trim = "Search Here for AccountID or Client details" Then
            'MessageBox.Message.Alert(Page, "Please enter search text", "str")

            '''''''''''''''''''
            If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIDFrom.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

                End If
                SqlDSClient.DataBind()
                gvClient.DataBind()
            ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
                txtPopUpClient.Text = txtCustName.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

                End If
                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else
                ' txtPopUpClient.Text = ""
                ' txtPopupClientSearch.Text = ""
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 order by accountid"

                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by accountid"

                End If
                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If
            mdlPopUpClient.Show()


            ''''''''''''''''''''
        Else
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by accountid"

            End If
            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
    End Sub

    Protected Sub rdbAccountName_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAccountName.CheckedChanged
        If txtPopUpClient.Text.Trim = "" Or txtPopUpClient.Text.Trim = "Search Here for AccountID or Client details" Then
            'MessageBox.Message.Alert(Page, "Please enter search text", "str")

            '''''''''''''''''''
            If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIDFrom.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

                End If
                SqlDSClient.DataBind()
                gvClient.DataBind()
            ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
                txtPopUpClient.Text = txtCustName.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

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


            ''''''''''''''''''''
        Else
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where  (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()
        End If

        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
    End Sub

    Private Sub EmailSOA()
        GetData()

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
        qry1 = qry1 + "ReportFormat,IncludeCompanyInfo,IncludeRecInfo,PrintDate,IncludeUnPaidBal,Query)"
        qry1 = qry1 + "VALUES(@AccountIDFrom,@AccountIDTo,@DocumentType,@CreatedBy,@CreatedOn,@Module,@Generated,"
        qry1 = qry1 + "@BatchNo,@FileType,@Selection,@Selformula,@RetryCount,@DomainName,@Distribution,@ContractGroup,@Branch,"
        qry1 = qry1 + "@CutOffDate,@InvoiceType,@Location,@PeriodFrom,@PeriodTo,@InvDateFrom,@InvDateTo,@DueDateFrom,"
        qry1 = qry1 + "@DueDateTo,@LedgerCodeFrom,@LedgerCodeTo,@Incharge,@SalesMan,@AccountType,@AccountName,@CompanyGroup,"
        qry1 = qry1 + "@LocateGrp,@Terms,@GLStatus,@ReportFormat,@IncludeCompanyInfo,@IncludeRecInfo,@PrintDate,"
        qry1 = qry1 + "@IncludeUnPaidBal,@Query);"

        command.CommandText = qry1
        command.Parameters.Clear()

        command.Parameters.AddWithValue("@AccountIDFrom", txtAccountIDFrom.Text)
        command.Parameters.AddWithValue("@AccountIDTo", txtAccountIDTo.Text)
        command.Parameters.AddWithValue("@DocumentType", "SOA")


        command.Parameters.AddWithValue("@Module", "SOA")
        command.Parameters.AddWithValue("@Generated", 0)
        command.Parameters.AddWithValue("@BatchNo", Session("UserID").ToString + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


        command.Parameters.AddWithValue("@FileType", "PDF")

        command.Parameters.AddWithValue("@Selection", Session("Selection"))

        command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
        command.Parameters.AddWithValue("@RetryCount", 0)

        command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())
        command.Parameters.AddWithValue("@Distribution", True)

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            command.Parameters.AddWithValue("@Branch", Convert.ToString(Session("Branch")))

            command.Parameters.AddWithValue("@Location", "")

        Else
            command.Parameters.AddWithValue("@Branch", "")
            command.Parameters.AddWithValue("@Location", "")
        End If

        command.Parameters.AddWithValue("@InvoiceType", "")


        command.Parameters.AddWithValue("@PeriodFrom", "")

        command.Parameters.AddWithValue("@PeriodTo", "")

        command.Parameters.AddWithValue("@InvDateFrom", "")

        command.Parameters.AddWithValue("@InvDateTo", "")

        command.Parameters.AddWithValue("@DueDateFrom", "")

        command.Parameters.AddWithValue("@DueDateTo", "")

        command.Parameters.AddWithValue("@LedgerCodeFrom", txtOurRef.Text)

        command.Parameters.AddWithValue("@LedgerCodeTo", txtYourRef.Text)

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            command.Parameters.AddWithValue("@Incharge", txtIncharge.Text)
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

        command.Parameters.AddWithValue("@Terms", "")

        command.Parameters.AddWithValue("@GLStatus", "")


        If String.IsNullOrEmpty(Convert.ToString(Session("SysDate"))) Then
            command.Parameters.AddWithValue("@PrintDate", "")
        Else

            Dim d As DateTime
            '  If Date.TryParseExact(Convert.ToString(Session("SysDate")), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
        command.Parameters.AddWithValue("@PrintDate", d.ToString("yyyy-MM-dd"))
        End If



        command.Parameters.AddWithValue("@IncludeUnpaidBal", False)


        If String.IsNullOrEmpty(txtCutOffDate.Text) Then
            command.Parameters.AddWithValue("@CutOffDate", "")
        Else

            Dim d As DateTime
            If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else

            End If
            command.Parameters.AddWithValue("@CutOffDate", d.ToString("yyyy-MM-dd"))
        End If


        command.Parameters.AddWithValue("@IncludeCompanyInfo", False)

        command.Parameters.AddWithValue("@IncludeRecInfo", False)


        If ddlContractGroup.SelectedIndex > 1 Then
            command.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text.Trim)
        Else
            command.Parameters.AddWithValue("@ContractGroup", "")
        End If


        If rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
            command.Parameters.AddWithValue("@ReportFormat", "SOA-INVRECV")
        ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "3" Then
            command.Parameters.AddWithValue("@ReportFormat", "SOA-ZEROCREDIT")
        End If

        command.Parameters.AddWithValue("@Query", txtQuery.Text)

        command.Connection = conn

        command.ExecuteNonQuery()

        command.Dispose()
        conn.Close()
        conn.Dispose()


    End Sub

    Protected Sub btnEmailSOA_Click(sender As Object, e As EventArgs) Handles btnEmailSOA.Click
        If txtAccountIDFrom.Text = txtAccountIDTo.Text Then
            Dim conn1 As MySqlConnection = New MySqlConnection()
            Dim commandE1 As MySqlCommand = New MySqlCommand

            conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn1.Open()

            commandE1.CommandType = CommandType.Text
            commandE1.CommandText = "select SendStatement as SS from tblCompany where  AccountID = '" & txtAccountIDFrom.Text & "'"
            commandE1.Connection = conn1

            Dim drservice As MySqlDataReader = commandE1.ExecuteReader()
            Dim dtservice As New DataTable
            dtservice.Load(drservice)

            If dtservice.Rows.Count > 0 Then
                If dtservice.Rows(0)("SS").ToString() = False Then
                    lblAlert.Text = "Account " & txtAccountIDFrom.Text & " does not require a Statement of Account, this report will NOT display any information for " & txtAccountIDFrom.Text
                    conn1.Close()
                    conn1.Dispose()
                    Exit Sub
                End If

            Else
                commandE1.CommandType = CommandType.Text
                commandE1.CommandText = "select SendStatement as SS from tblPerson where  AccountID = '" & txtAccountIDFrom.Text & "'"
                commandE1.Connection = conn1

                Dim drservice1 As MySqlDataReader = commandE1.ExecuteReader()
                Dim dtservice1 As New DataTable
                dtservice1.Load(drservice1)

                If dtservice1.Rows.Count > 0 Then
                    If dtservice1.Rows(0)("SS").ToString() = False Then
                        lblAlert.Text = "Account " & txtAccountIDFrom.Text & " does not require a Statement of Account, this report will NOT display any information for " & txtAccountIDFrom.Text
                        conn1.Close()
                        conn1.Dispose()
                        Exit Sub
                    End If

                End If

            End If

        End If

        If String.IsNullOrEmpty(txtCutOffDate.Text) Then
            lblAlert.Text = "ENTER CUTOFF DATE"
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

        Dim strSql As String = "INSERT INTO tblEventLog (StaffID,Module,DocRef,Action,ComputerName," & _
                   "Serial, LogDate, Comments,SOURCESQLID) " & _
                   "VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate,  @Comments, @SOURCESQLID)"
        '"VALUES (@StaffID, @Module, @DocRef, @Action, @ComputerName, @Serial, @LogDate, @Amount, @BaseValue, @BaseGSTValue, @CustCode, @Comments, @SOURCESQLID)"


        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.Text
        command.CommandText = strSql
        command.Parameters.Clear()
        'Convert.ToDateTime(txtContractStart.Text).ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@StaffID", Session("UserID"))
        command.Parameters.AddWithValue("@Module", "REPORTS")
        command.Parameters.AddWithValue("@DocRef", "SOA")
        command.Parameters.AddWithValue("@Action", "")
        command.Parameters.AddWithValue("@ComputerName", Strings.Left(My.Computer.Name.ToString, 20))
        command.Parameters.AddWithValue("@Serial", "")
        'command.Parameters.AddWithValue("@LogDate", Convert.ToString(Session("SysDate")))
        command.Parameters.AddWithValue("@LogDate", Convert.ToDateTime(Session("SysTime")))
        'command.Parameters.AddWithValue("@Amount", 0)
        'command.Parameters.AddWithValue("@BaseValue", 0)
        'command.Parameters.AddWithValue("@BaseGSTValue", 0)
        'command.Parameters.AddWithValue("@CustCode", "")
        command.Parameters.AddWithValue("@Comments", "")
        command.Parameters.AddWithValue("@SOURCESQLID", 0)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Dim conn As MySqlConnection = New MySqlConnection(constr)
        conn.Open()
        command.Connection = conn
        command.ExecuteNonQuery()

        'conn.Close()
        'conn.Dispose()
        command.Dispose()

        GetData()
        EmailSOA()
        mdlPopupMsg.Show()

    End Sub
End Class
