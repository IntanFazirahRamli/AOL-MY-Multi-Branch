Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Partial Class RV_Select_StatementOfAccounts1
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
                txtCustName.Text = gvClient.SelectedRow.Cells(3).Text.Trim
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
            selFormula = "{tblsales1.rcno} <> 0 AND {tblsales1.OPeriodBalance}<>0 and {tblsales1.doctype} ='ARIN' and {tblsales1.PostStatus} ='P'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase, CreditBase, ReceiptBase,"
            qry = qry + "OPeriodBalance as Balance,"
            qry = qry + "StaffCode, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where doctype='ARIN' and poststatus='P'"
            'If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then

            '    qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"


            'End If
        Else
            selFormula = "{tblsales1.rcno} <> 0 AND {tblsales1.BalanceBase}<>0 and {tblsales1.doctype} ='ARIN' and {tblsales1.PostStatus} ='P'"
            qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase, CreditBase, ReceiptBase,"
            qry = qry + "BalanceBase as Balance,"
            qry = qry + "StaffCode, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
            qry = qry + " where balancebase <> 0 and doctype='ARIN' and poststatus='P'"
        End If

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
        If String.IsNullOrEmpty(txtCustName.Text) = False Then
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

    Private Sub RecalculateBalance(dt As DataTable, conn As MySqlConnection)
        Dim lTotalReceipt As Decimal
        Dim lInvoiceAmount As Decimal
        Dim lTotalcn As Decimal
        Dim lTotalRecvcn As Decimal
        Dim lTotalJournalAmt As Decimal
        Dim lbalance As Decimal
        Dim invno As String = ""
        Dim cnno As String = ""
        For i As Int16 = 0 To dt.Rows.Count - 1
            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalRecvcn = 0.0
            lTotalcn = 0.0
            lTotalJournalAmt = 0.0
            lbalance = 0.0
            invno = dt.Rows(i)("InvoiceNumber").ToString.Trim

            'Retrieve Invoice Amount
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT AppliedBase FROM tblSales where InvoiceNumber = '" & invno & "'"
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString
            End If

            'Retrieve CN and DN Amount

            Dim command2 As MySqlCommand = New MySqlCommand
            command2.CommandType = CommandType.Text

            command2.CommandText = "SELECT  A.invoicenumber as cnno,ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn,ifnull(SUM(ifnull(B.AppliedBase,0)),0) as sumCN FROM tblSalesDetail A, tblSales B WHERE " & _
              "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and B.SalesDate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'" & _
            "and A.SourceInvoice = '" & invno & "'"

            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            Dim dt2 As New DataTable
            dt2.Load(dr2)

            If dt2.Rows.Count > 0 Then
                lTotalcn = dt2.Rows(0)("totalcn").ToString

                If String.IsNullOrEmpty(dt2.Rows(0)("cnno").ToString) = False Then
                    'Retrieve Receipt Amount for CreditNote
                    cnno = dt2.Rows(0)("cnno").ToString

                    Dim command31 As MySqlCommand = New MySqlCommand
                    command31.CommandType = CommandType.Text

                    command31.CommandText = "SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalrecvcn FROM tblRecvDet A, tblRecv B WHERE " & _
                     "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN' and B.PostStatus = 'P' AND " & _
                     "B.receiptdate<='" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "' and A.RefType = '" & cnno & "'"
                    command31.Connection = conn

                    Dim dr31 As MySqlDataReader = command31.ExecuteReader()
                    Dim dt31 As New DataTable
                    dt31.Load(dr31)

                    If dt31.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dt31.Rows(0)("totalrecvcn").ToString) Or dt31.Rows(0)("totalrecvcn") = 0 Then
                            lTotalRecvcn = 0
                        Else
                            lTotalRecvcn = dt31.Rows(0)("totalrecvcn").ToString
                        End If
                        If dt2.Rows(0)("sumcn") = lTotalRecvcn Then
                            lTotalcn = 0
                            lTotalRecvcn = 0
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

            lbalance = lInvoiceAmount + lTotalcn - lTotalReceipt + lTotalJournalAmt - lTotalRecvcn
            lTotalcn = lTotalcn + lTotalJournalAmt - lTotalRecvcn + lTotalJournalAmt

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

        Next


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
            txtCutOffDate.Enabled = False

        End If
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtPrintDate.Text) Then
            txtPrintDate.Text = Convert.ToString(Session("SysDate"))
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
                txtPrintDate.Text = txtCutOffDate.Text

            End If
        End If
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

    End Sub

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
End Class
