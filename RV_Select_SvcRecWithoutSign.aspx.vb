

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System


Partial Class RV_Select_SvcRecWithoutSign
    Inherits System.Web.UI.Page

    'Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    'If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
    '    '    txtServiceBy.Text = " "
    '    'Else
    '    '    txtServiceBy.Text = gvTeam.SelectedRow.Cells(2).Text
    '    'End If



    'End Sub

    'Protected Sub gvTeam_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTeam.PageIndexChanging
    '    gvTeam.PageIndex = e.NewPageIndex

    '    If txtPopUpTeam.Text.Trim = "" Then
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status<>'N' order by TeamName"
    '    Else
    '        ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    '        '  SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' and Status <> 'N'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"

    '    End If

    '    SqlDSTeam.DataBind()
    '    gvTeam.DataBind()
    '    mdlPopUpTeam.Show()
    'End Sub

    'Protected Sub btnPopUpTeamSearch_Click(sender As Object, e As EventArgs)

    'End Sub

    'Protected Sub btnPopUpTeamReset_Click(sender As Object, e As EventArgs)
    '    txtPopUpTeam.Text = ""
    '    txtPopupTeamSearch.Text = ""
    '    '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
    '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

    '    SqlDSTeam.DataBind()
    '    gvTeam.DataBind()
    '    mdlPopUpTeam.Show()
    'End Sub

    'Protected Sub txtPopUpTeam_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpTeam.TextChanged
    '    If txtPopUpTeam.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter search text", "str")
    '    Else
    '        txtPopupTeamSearch.Text = txtPopUpTeam.Text
    '        ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    '        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
    '        SqlDSTeam.DataBind()
    '        gvTeam.DataBind()
    '        mdlPopUpTeam.Show()
    '    End If
    '    txtPopUpTeam.Text = "Search Here for Team, Incharge or ServiceBy"
    'End Sub

    'Protected Sub btnTeam_Click(sender As Object, e As ImageClickEventArgs) Handles btnTeam.Click
    '    mdlPopUpTeam.TargetControlID = "btnTeam"
    '    'If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
    '    '    txtPopupTeamSearch.Text = txtServiceBy.Text.Trim
    '    '    txtPopUpTeam.Text = txtPopupTeamSearch.Text
    '    '    ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    '    '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
    '    '    SqlDSTeam.DataBind()
    '    '    gvTeam.DataBind()
    '    'Else
    '    '    'txtPopUpTeam.Text = ""
    '    '    'txtPopupTeamSearch.Text = ""
    '    '    '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
    '    '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

    '    '    SqlDSTeam.DataBind()
    '    '    gvTeam.DataBind()
    '    'End If
    '    mdlPopUpTeam.Show()
    'End Sub

    'Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
    '    txtAccountID.Text = ""
    '    txtCustName.Text = ""

    'End Sub



    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        Dim selection As String
        Dim qry As String = "select * from tblservicerecord where rcno<>0 and status='P'"
        selection = ""
        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.status} = 'P'"
        'If String.IsNullOrEmpty(chkStatusSearch.Text) = False Then
        '    Dim YrStrList As List(Of [String]) = New List(Of String)()


        '    For Each item As ListItem In chkStatusSearch.Items
        '        If item.Selected Then
        '            If item.Value = "ALL" Then
        '            Else

        '                YrStrList.Add("""" + item.Value + """")
        '            End If

        '        End If
        '    Next


        '    Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

        '    selFormula = selFormula + " and {tblservicerecord1.Status} in [" + YrStr + "]"
        '    If selection = "" Then
        '        selection = "Status = " + YrStr
        '    Else
        '        selection = selection + "Status = " + YrStr
        '    End If
        '    qry = qry + " and status in (" + YrStr + ")"
        'End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.Contractno} = '" + txtContractNo.Text + "'"
            If selection = "" Then
                selection = "Contract No = " + txtContractNo.Text
            Else
                selection = selection + ", Contract No = " + txtContractNo.Text
            End If
            qry = qry + " and contractno='" + txtContractNo.Text + "'"

        End If
      

        If ddlIncharge.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"
            If selection = "" Then
                selection = "ServiceBy = " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy = " + ddlIncharge.Text
            End If
            qry = qry + " and serviceby = '" + ddlIncharge.Text + "'"

        End If


        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and CompanyGroup in (" + YrStr + ")"
        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup = " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup = " + ddlCompanyGrp.Text
        '    End If
        'End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
            qry = qry + " and ContactType = '" + ddlAccountType.Text + "'"
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            qry = qry + " and AccountID = '" + txtAccountID.Text + "'"
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text.Replace("'", "''") + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            qry = qry + " and CustName like '%" + txtCustName.Text + "%'"
        End If

        'If String.IsNullOrEmpty(txtCustName.Text) = False Then
        '    txtCustName.Text = txtCustName.Text.Replace("'", "''")
        '    selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
        '    If selection = "" Then
        '        selection = "CustName : " + txtCustName.Text
        '    Else
        '        selection = selection + ", CustName : " + txtCustName.Text
        '    End If
        '    qry = qry + " and tblservicerecord.CustName like '%" + txtCustName.Text + "%'"
        'End If
        If String.IsNullOrEmpty(txtServiceLocationID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.LocationID} = '" + txtServiceLocationID.Text + "'"
            qry = qry + " and LocationID = '" + txtServiceLocationID.Text + "'"
            If selection = "" Then
                selection = "LocationID : " + txtServiceLocationID.Text
            Else
                selection = selection + ", LocationID : " + txtServiceLocationID.Text
            End If
        End If


        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

      
        qry = qry + " and customersign is null"
   
    
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

        Session.Add("ReportType", "SvcRecWithoutSign")
        Response.Redirect("RV_SvcRecWithoutSign.aspx")


    End Sub

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
    '    ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
    '        txtPopUpClient.Text = txtCustName.Text
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


    'Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

    '    If txtPopupClientSearch.Text.Trim = "" Then
    '        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where rcno <>0 order by name"
    '        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 order by name"
    '        End If


    '    Else
    '        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '            SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
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
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")

        End If
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        'chkStatusSearch.ClearSelection()
        ddlCompanyGrp.SelectedIndex = 0
        ddlIncharge.SelectedIndex = 0
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
      
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        lstSort2.Items.Clear()

    End Sub

    Protected Sub btnSvcLocation_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcLocation.Click
        txtModal.Text = "Location"
        If String.IsNullOrEmpty(txtServiceLocationID.Text.Trim) = False Then
            txtPopUpClient.Text = txtServiceLocationID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

        ElseIf String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ClientQuery("Search")

        Else
            ClientQuery("Reset")

        End If
        mdlPopUpClient.Show()

    End Sub

    Protected Sub btnPopUpClientReset_Click1(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        ClientQuery("Reset")

        mdlPopUpClient.Show()
        ' txtIsPopUp.Text = "Client"
    End Sub

    Protected Sub OnRowDataBoundgClient(sender As Object, e As GridViewRowEventArgs)
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

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs)
        For Each row As GridViewRow In gvClient.Rows
            If row.RowIndex = gvClient.SelectedIndex Then
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Private Sub ClientQuery(type As String)
        Dim qry As String = ""

        If type = "Search" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"
            Else
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))"
                qry = qry + " union "
                qry = qry + "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  and (upper(A.Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or A.accountid like '%" + txtPopupClientSearch.Text + "%' or B.Locationid like '%" + txtPopupClientSearch.Text + "%' or A.contACTperson like '%" + txtPopupClientSearch.Text + "%'))  order by Accountid, LocationId"

            End If
        ElseIf type = "Reset" Then
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"
            Else
                qry = "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))"
                qry = qry + " union "
                qry = qry + "(SELECT  a.id,a.name,a.contactperson,a.accountid, b.LocationId, b.Address1 as ServiceAddress1,b.contractgroup  From tblPERSON A Left join tblPersonLocation B on A.Accountid = B.Accountid   where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> ''))  order by Accountid, LocationId"

            End If

        End If
        SqlDSClient.SelectCommand = qry
        SqlDSClient.DataBind()
        gvClient.DataBind()

    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        'If String.IsNullOrEmpty(ddlAccountType.Text) Then
        '    '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
        '    Return
        'End If
        'If ddlAccountType.Text = "-1" Then
        '    '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
        '    Return
        'End If
        txtModal.Text = "ID"

        If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

        ElseIf String.IsNullOrEmpty(txtServiceLocationID.Text.Trim) = False Then
            txtPopUpClient.Text = txtServiceLocationID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
            ClientQuery("Search")

            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ClientQuery("Search")

        Else
            ' txtPopUpClient.Text = ""
            ' txtPopupClientSearch.Text = ""
            ClientQuery("Reset")

        End If
        mdlPopUpClient.Show()
    End Sub


    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

        If String.IsNullOrEmpty(txtPopupClientSearch.Text.Trim) Then
            ClientQuery("Reset")


        Else
            ClientQuery("Search")

        End If


        gvClient.PageIndex = e.NewPageIndex

        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As EventArgs)
        mdlPopUpClient.Show()
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs)
        txtPopUpClient.Text = "Search Here for AccountID or Client details"
        txtPopupClientSearch.Text = ""
        ClientQuery("Reset")

        mdlPopUpClient.Show()
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else

            ClientQuery("Search")

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
            txtServiceLocationID.Text = ""
        Else
            txtServiceLocationID.Text = gvClient.SelectedRow.Cells(3).Text.Trim
        End If
        If (gvClient.SelectedRow.Cells(4).Text = "&nbsp;") Then
            txtCustName.Text = ""
        Else
            txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(4).Text.Trim).ToString
        End If

    End Sub

    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        txtCustName.Text = ""
        txtServiceLocationID.Text = ""
    End Sub
End Class

