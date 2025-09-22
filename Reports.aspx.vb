
Partial Class Reports
    Inherits System.Web.UI.Page

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub chkServiceRecordsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkServiceRecordsList.SelectedIndexChanged
        If chkServiceRecordsList.Items(0).Selected Then
            mdlPopupSvcRecordListing.Show()
            chkServiceRecordsList.Items(0).Selected = False

        ElseIf chkServiceRecordsList.Items(5).Selected Then
            mdlPopupSvcSchedule.Show()
            chkServiceRecordsList.Items(5).Selected = False


        End If
    End Sub

    'Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
    '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
    '        txtAccountID.Text = ""
    '    Else
    '        txtAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
    '    End If

    '    If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
    '        txtCustName.Text = ""
    '    Else
    '        txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim).ToString
    '    End If

    'End Sub

    'Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

    '    If txtPopupClientSearch.Text.Trim = "" Then
    '        If ddlAccountType.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
    '        ElseIf ddlAccountType.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
    '        End If


    '    Else
    '        If ddlAccountType.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        ElseIf ddlAccountType.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        End If
    '    End If

    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    gvClient.PageIndex = e.NewPageIndex

    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs)
    '    txtPopUpClient.Text = "Search Here for AccountID or Client details"
    '    txtPopupClientSearch.Text = ""
    '    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
    '    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
    '    End If
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
    '    If txtPopUpClient.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter search text", "str")
    '    Else
    '        txtPopupClientSearch.Text = txtPopUpClient.Text
    '        ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
    '        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        End If
    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '        mdlPopUpClient.Show()
    '    End If

    '    txtPopUpClient.Text = "Search Here for AccountID or Client details"
    'End Sub

    'Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click

    '    If String.IsNullOrEmpty(ddlAccountType.Text) Then
    '        '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
    '        lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
    '        Return
    '    End If
    '    If ddlAccountType.Text = "-1" Then
    '        '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
    '        lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
    '        Return
    '    End If

    '    If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
    '        txtPopUpClient.Text = txtAccountID.Text
    '        txtPopupClientSearch.Text = txtPopUpClient.Text
    '        ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
    '        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
    '        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        End If
    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '    Else
    '        ' txtPopUpClient.Text = ""
    '        ' txtPopupClientSearch.Text = ""
    '        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 order by name"

    '        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"

    '        End If
    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '    End If
    '    mdlPopUpClient.Show()

    'End Sub

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)
      
            If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
                txtServiceBy.Text = " "
            Else
                txtServiceBy.Text = gvTeam.SelectedRow.Cells(2).Text
            End If

        mdlPopupSvcRecordListing.Show()


    End Sub

    Protected Sub gvTeam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeam.PageIndexChanging
        gvTeam.PageIndex = e.NewPageIndex

        If txtPopUpTeam.Text.Trim = "" Then
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status<>'N' order by TeamName"
        Else
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            '  SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"

        End If

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub btnPopUpTeamSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)
        txtPopUpTeam.Text = ""
        txtPopupTeamSearch.Text = ""
        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

        SqlDSTeam.DataBind()
        gvTeam.DataBind()
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub txtPopUpTeam_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpTeam.TextChanged
        If txtPopUpTeam.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupTeamSearch.Text = txtPopUpTeam.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
            mdlPopUpTeam.Show()
        End If
        txtPopUpTeam.Text = "Search Here for Team, Incharge or ServiceBy"
    End Sub

    Protected Sub btnTeam_Click(sender As Object, e As ImageClickEventArgs) Handles btnTeam.Click
        mdlPopUpTeam.TargetControlID = "btnTeam"
        If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtServiceBy.Text.Trim
            txtPopUpTeam.Text = txtPopupTeamSearch.Text
            ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        Else
            'txtPopUpTeam.Text = ""
            'txtPopupTeamSearch.Text = ""
            '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
            SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

            SqlDSTeam.DataBind()
            gvTeam.DataBind()
        End If
        mdlPopUpTeam.Show()
    End Sub

    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        txtCustName.Text = ""
        mdlPopupSvcRecordListing.Show()

    End Sub

 

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblservicerecord.rcno} <> 0"
        If String.IsNullOrEmpty(chkStatusSearch.Text) = False Then
            Dim YrStrList As List(Of [String]) = New List(Of String)()


            For Each item As ListItem In chkStatusSearch.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else

                        YrStrList.Add("""" + item.Value + """")
                    End If

                End If
            Next


            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
        
            selFormula = selFormula + " and {tblservicerecord.Status} in [" + YrStr + "]"
            selection = selection + "Status = " + YrStr
        End If

        If String.IsNullOrEmpty(txtServiceRecord.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.Recordno} = '" + txtServiceRecord.Text + "'"
            selection = selection + ", Record No = " + txtServiceRecord.Text
        End If

        If txtServiceID.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecorddet.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
            selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
        End If

        If String.IsNullOrEmpty(txtServiceBy.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.serviceby} = '" + txtServiceBy.Text + "'"
            selection = selection + ", ServiceBy = " + txtServiceBy.Text
        End If


        If ddlCompanyGrp.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
            selection = selection + ", CompanyGroup = " + ddlCompanyGrp.Text
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord.ContactType} = '" + ddlAccountType.Text + "'"
            selection = selection + ", Account Type = " + ddlAccountType.Text
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.AccountID} = '" + txtAccountID.Text + "'"
            selection = selection + ", AccountID = " + txtAccountID.Text
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.CustName} = '" + txtCustName.Text + "'"
            selection = selection + ", Client Name = " + txtCustName.Text
        End If

        If ddlServiceFrequency.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecorddet.Frequency} = '" + ddlServiceFrequency.Text + "'"
            selection = selection + ", Service Frequency = " + ddlServiceFrequency.Text
        End If

        If ddlBillingFrequency.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.BillingFrequency} = '" + ddlBillingFrequency.Text + "'"
            selection = selection + ", Billing Frequency = " + ddlBillingFrequency.Text
        End If

        If ddlTargetID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecorddet.TargetID} = '" + ddlTargetID.SelectedItem.Text + "'"
            selection = selection + ", TargetID = " + ddlTargetID.SelectedItem.Text

        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Service Date >= " + d.ToString("MM-dd-yyyy")
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Service Date <= " + d.ToString("MM-dd-yyyy")
        End If

        If String.IsNullOrEmpty(txtBillAmtFrom.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.BillAmount} >= " + txtBillAmtFrom.Text + ""
            selection = selection + ", BillAmount >= " + txtBillAmtFrom.Text
        End If

        If String.IsNullOrEmpty(txtBillAmtTo.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.BillAmount} <= " + txtBillAmtTo.Text + ""
            selection = selection + ", BillAmount <= " + txtBillAmtTo.Text
        End If

        If String.IsNullOrEmpty(txtNotes.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord.Notes} like '*" + txtNotes.Text + "*'"
            selection = selection + ", Notes = " + txtNotes.Text
        End If

        If String.IsNullOrEmpty(lstSort2.Text) = False Then
            If lstSort2.Items(0).Selected = True Then


            End If
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            For Each item As ListItem In lstSort2.Items
                If item.Selected Then

                    YrStrList.Add(item.Value)

                End If
            Next
            If YrStrList.Count > 0 Then
                For i As Integer = 0 To YrStrList.Count - 1
                    If i = 0 Then
                        Session.Add("sort1", YrStrList.Item(i).ToString)
                    ElseIf i = 1 Then
                        Session.Add("sort2", YrStrList.Item(i).ToString)
                    ElseIf i = 2 Then
                        Session.Add("sort3", YrStrList.Item(i).ToString)
                    ElseIf i = 3 Then
                        Session.Add("sort4", YrStrList.Item(i).ToString)
                    ElseIf i = 4 Then
                        Session.Add("sort5", YrStrList.Item(i).ToString)

                    End If

                Next
            End If

            '  Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            '  selFormula = selFormula + " order by " + YrStr
            '  Session.Add("sort", YrStr)
        End If
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        Response.Redirect("RV_ServiceRecord02.aspx")
    End Sub

    Protected Sub btnPrintServiceSchedule_Click(sender As Object, e As EventArgs) Handles btnPrintServiceSchedule.Click
        Dim selFormula As String
        selFormula = "{tblservicerecord.rcno} <> 0"
        Dim selection As String
        selection = ""

        If String.IsNullOrEmpty(txtSchDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord.SchServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Schedule Date >= " + d.ToString("MM-dd-yyyy")
        End If
        If String.IsNullOrEmpty(txtSchDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord.SchServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + "\n Schedule Date <= " + d.ToString("MM-dd-yyyy")
        End If
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Response.Redirect("RV_ServiceSchedule.aspx")

    End Sub

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
        mdlPopupSvcRecordListing.Show()

    End Sub

    Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
        Dim i As Integer = lstSort1.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort2.Items.Add(lstSort1.Items(i))
        lstSort1.Items.RemoveAt(i)

        mdlPopupSvcRecordListing.Show()

    End Sub

    Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
        Dim i As Integer = lstSort2.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort1.Items.Add(lstSort2.Items(i))
        lstSort2.Items.RemoveAt(i)

        mdlPopupSvcRecordListing.Show()

    End Sub

    Protected Sub chkServiceContractList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkServiceContractList.SelectedIndexChanged
        If chkServiceContractList.Items(0).Selected Then
            Response.Redirect("select_24Contract01.aspx", False)
            chkServiceContractList.Items(0).Selected = False

            'ElseIf chkServiceContractList.Items(5).Selected Then
            '    mdlPopupSvcSchedule.Show()
            '    chkServiceContractList.Items(5).Selected = False


        End If
    End Sub
End Class
