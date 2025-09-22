
Partial Class RV_Select_OffsetContractExpRenew
    Inherits System.Web.UI.Page


    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

        If gvTeam.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtTeam.Text = " "
        Else
            txtTeam.Text = gvTeam.SelectedRow.Cells(1).Text
        End If



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
        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

        SqlDSTeam.DataBind()
        gvTeam.DataBind()

        mdlPopUpTeam.Show()
    End Sub

    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        txtCustName.Text = ""

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

    Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
        Dim i As Integer = lstSort1.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort2.Items.Add(lstSort1.Items(i))
        lstSort1.Items.RemoveAt(i)


    End Sub

    Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
        Dim i As Integer = lstSort2.SelectedIndex

        If i = -1 Then
            Exit Sub 'skip if no item is selected
        End If

        lstSort1.Items.Add(lstSort2.Items(i))
        lstSort2.Items.RemoveAt(i)


    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'chkStatusSearch.Attributes.Add("onclick", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")
        'chkRenewalStatus.Attributes.Add("onclick", "javascript: CheckBoxListRenewalSelect ('" & chkRenewalStatus.ClientID & "');")

    End Sub

    Protected Sub btnCloseServiceContractList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceContractList.Click
        chkStatusSearch.ClearSelection()
        chkRenewalStatus.ClearSelection()
        txtContractDateFrom.Text = ""
        txtContractDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlContractGroup.SelectedIndex = 0
        txtTeam.Text = ""
        txtServiceID.SelectedIndex = 0
        txtStartDateFrom.Text = ""
        txtStartDateTo.Text = ""
        txtEndDateFrom.Text = ""
        txtEndDateTo.Text = ""
        txtActualEndFrom.Text = ""
        txtActualEndTo.Text = ""
        txtRenewalDateFrom.Text = ""
        txtRenewalDateTo.Text = ""
        txtLessThan.Text = ""
        ddlLessThan.SelectedIndex = 0

        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        lstSort2.Items.Clear()
    End Sub

    Protected Sub btnPrintServiceContractList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceContractList.Click
        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblcontract1.rcno} <> 0"
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
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

            selFormula = selFormula + " and {tblcontract1.Status} in [" + YrStr + "]"
            selection = selection + "Status = " + YrStr
        End If

        If String.IsNullOrEmpty(chkRenewalStatus.Text) = False Then
            Dim YrStrList As List(Of [String]) = New List(Of String)()


            For Each item As ListItem In chkRenewalStatus.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else

                        YrStrList.Add("""" + item.Value + """")
                    End If

                End If
            Next


            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            selFormula = selFormula + " and {tblcontract1.RenewalSt} in [" + YrStr + "]"
            If selection = "" Then
                selection = selection + "RenewalStatus = " + YrStr
            Else
                selection = selection + ", RenewalStatus = " + YrStr
            End If
        End If

        If String.IsNullOrEmpty(txtContractDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Contract Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ContractDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Contract Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Contract Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtContractDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Contract Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ContractDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Contract Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Contract Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup = " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup = " + ddlCompanyGrp.Text
        '    End If
        'End If


        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblcontract1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            ' qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        If ddlContractGroup.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
            If selection = "" Then
                selection = "Department = " + ddlContractGroup.Text
            Else
                selection = selection + ", Department = " + ddlContractGroup.Text
            End If
        End If
        If String.IsNullOrEmpty(txtTeam.Text) = False Then

            selFormula = selFormula + " and {tblcontract1.StaffID} = '" + txtTeam.Text + "'"

            If selection = "" Then
                selection = "StaffID : " + txtTeam.Text
            Else
                selection = selection + ", StaffID : " + txtTeam.Text
            End If
        End If
        If txtServiceID.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontractdet1.ServiceID} = '" + txtServiceID.Text + "'"
            If selection = "" Then
                selection = "ServiceID = " + txtServiceID.Text
            Else
                selection = selection + ", ServiceID = " + txtServiceID.Text
            End If
        End If
        If String.IsNullOrEmpty(txtStartDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtStartDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Start Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.StartDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Start Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Start Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtStartDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtStartDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Start Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.StartDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Start Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Start Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtEndDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtEndDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "End Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.EndDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "End Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", End Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtEndDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtEndDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "End Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.EndDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "End Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", End Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtActualEndFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtActualEndFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Actual End Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Actual End Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Actual End Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtActualEndTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtActualEndTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Actual End Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Actual End Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Actual End Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtRenewalDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtRenewalDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Renewal Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.RenewalDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Renewal Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Renewal Date >= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If String.IsNullOrEmpty(txtRenewalDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtRenewalDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Renewal Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.RenewalDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Renewal Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Renewal Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If
        If ddlLessThan.Text = "-1" Then

        Else
         
            Select Case ddlLessThan.Text
                Case "Hours"
                    selFormula = selFormula + " and  {tblContract1.HourNo} > 0 AND {tblContract1.HourBal} <= " + txtLessThan.Text
                Case "Visit"
                    selFormula = selFormula + " and  {tblContract1.ServiceNo} > 0 AND {tblContract1.ServiceBal} <= " + txtLessThan.Text
                Case "Calls"
                    selFormula = selFormula + " and  {tblContract1.CallNo} > 0 AND {tblContract1.CallBal} <= " + txtLessThan.Text
                Case "Units"
                    selFormula = selFormula + " and  {tblContract1.UnitNo} > 0 AND {tblContract1.UnitBal} <= " + txtLessThan.Text
                Case "% Bal"
                    selFormula = selFormula + " and  {tblContract1.HourNo} > 0 AND ({tblContract1.HourBal}/{tblContract1.HourNo})*100 <= " + txtLessThan.Text



            End Select
            If selection = "" Then
                selection = "Less Than " + txtLessThan.Text + " " + ddlLessThan.Text

            Else
                selection = selection + ", Less Than " + txtLessThan.Text + " " + ddlLessThan.Text

            End If
        End If
        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text

            End If
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
                    ElseIf i = 5 Then
                        Session.Add("sort6", YrStrList.Item(i).ToString)
                    ElseIf i = 6 Then
                        Session.Add("sort7", YrStrList.Item(i).ToString)
                    End If

                Next
            End If

        End If
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("LessThan", ddlLessThan.Text)
        Dim number As Integer

        Dim result As Boolean = Int32.TryParse(txtLessThan.Text, number)
        If result Then
            Session.Add("LessthanAmt", number)

        Else
            Session.Add("LessthanAmt", 0)

        End If
        Session.Add("ReportType", "OffsetContractExpRenew")
        Response.Redirect("RV_OffsetContractExpRenew.aspx")
        'If chkGrouping.SelectedValue = "ContractGroup" Then
        '    Response.Redirect("RV_Contract01.aspx")
        'ElseIf chkGrouping.SelectedValue = "Incharge" Then
        '    Response.Redirect("RV_Contract01_Incharge.aspx")
        'ElseIf chkGrouping.SelectedValue = "SupportStaff" Then
        '    Response.Redirect("RV_Contract01_SupportStaff.aspx")
        'End If

    End Sub


End Class
