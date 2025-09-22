Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class RV_Select_ActualVsForecastRevenue
    Inherits System.Web.UI.Page

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

        If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtTeam.Text = " "
        Else
            txtTeam.Text = gvTeam.SelectedRow.Cells(2).Text
        End If

        mdlTeamSel.Show()


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
        If String.IsNullOrEmpty(txtTeam.Text.Trim) = False Then
            txtPopupTeamSearch.Text = txtTeam.Text.Trim
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

        mdlPpClientID.Show()

    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        If String.IsNullOrEmpty(ddlAccountType.Text) Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlertClientID.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If
        If ddlAccountType.Text = "-1" Then
            '  MessageBox.Message.Alert(Page, "Select Contact Type to proceed!!!", "str")
            lblAlertClientID.Text = "SELECT ACCOUNT TYPE TO PROCEED"
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
        mdlPpClientID.Show()

    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.SelectedIndex = 0

        ddlCompanyGrp.SelectedIndex = 0
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' "
        Dim selection As String
        selection = ""
        Dim qry As String
        qry = "select sum(billamount) as TotalBillAmount,svcdet.serviceid,svc.companygroup from tblservicerecord as svc,tblservicerecorddet as svcdet where svc.recordno=svcdet.recordno"


        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblrptactualforecastrevenue1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND svc.ServiceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If


        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblrptactualforecastrevenue1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND svc.ServiceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
        End If
        If txtServiceID.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblrptactualforecastrevenue1.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
            If selection = "" Then
                selection = "ServiceID = " + txtServiceID.SelectedItem.Text
            Else
                selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
            End If
            qry = qry + " and svcdet.ServiceID = '" + txtServiceID.SelectedItem.Text + "'"
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
        Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlCompanyGrp.Items
            If item.Selected Then

                YrStrList1.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList1.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)

            selFormula = selFormula + " and {tblrptactualforecastrevenue1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and svc.CompanyGroup in [" + YrStr + "]"
        End If

        'MessageBox.Message.Alert(Page, qry, "str")
        'Return

        RetrieveData(qry)

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        Response.Redirect("RV_ActualVsForecastByCompGrpAndSvcID.aspx")

        'If chkGrouping.SelectedValue = "Date" Then
        '    Session.Add("rptTitle", "ACTUAL REVENUE REPORT BY DATE")

        '    Response.Redirect("RV_RevenueRptByDate.aspx")
        'ElseIf chkGrouping.SelectedValue = "Client" Then
        '    Session.Add("rptTitle", "ACTUAL REVENUE REPORT BY CLIENT")

        '    mdlPpClientID.Show()

        'ElseIf chkGrouping.SelectedValue = "Team" Then

        '    mdlTeamSel.Show()

        'ElseIf chkGrouping.SelectedValue = "PostalCode" Then
        '    Session.Add("rptTitle", "ACTUAL REVENUE REPORT BY POSTAL CODE")

        '    Response.Redirect("RV_RevenueRptByPostalCode.aspx")
        'End If


    End Sub

    Private Sub RetrieveData(qry As String)
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        'delete existing data from tblrptActualForecastRevenue

        Dim command2 As MySqlCommand = New MySqlCommand

        command2.CommandType = CommandType.Text
        command2.CommandText = "delete from tblrptactualforecastrevenue where createdby='" & txtCreatedBy.Text & "';"

        command2.Connection = conn

        command2.ExecuteNonQuery()

        'group by companygroup

        Dim cmdCG As MySqlCommand = New MySqlCommand
        cmdCG.CommandType = CommandType.Text

        cmdCG.CommandText = "SELECT distinct * from tblCOMPANYGROUP order by COMPANYGROUP"
        cmdCG.Connection = conn

        Dim drCG As MySqlDataReader = cmdCG.ExecuteReader()
        Dim dtCG As New DataTable
        dtCG.Load(drCG)

        If dtCG.Rows.Count > 0 Then

            For c As Integer = 0 To dtCG.Rows.Count - 1


                'Actual Revenue
                Dim cmdContract As MySqlCommand = New MySqlCommand

                cmdContract.CommandType = CommandType.Text
            

                cmdContract.CommandText = qry + " and svc.Status='P' and svc.companygroup='" & dtCG.Rows(c)("CompanyGroup") & "' group by svc.companygroup,svcdet.serviceid;"
                '     cmdContract.Parameters.AddWithValue("@cg", dtCG.Rows(c)("CompanyGroup"))

                cmdContract.Connection = conn

                Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()

                Dim dtContract As New DataTable
                dtContract.Load(drContract)

                'Forecast Revenue

                Dim cmdContract1 As MySqlCommand = New MySqlCommand

                cmdContract1.CommandType = CommandType.Text

                cmdContract1.CommandText = qry + " and svc.Status='O' and svc.companygroup='" & dtCG.Rows(c)("CompanyGroup") & "' group by svc.companygroup,svcdet.serviceid;"
                '     cmdContract1.Parameters.AddWithValue("@cg", dtCG.Rows(c)("CompanyGroup"))

                cmdContract1.Connection = conn

                Dim drContract1 As MySqlDataReader = cmdContract1.ExecuteReader()
                Dim dtContract1 As New DataTable
                dtContract1.Load(drContract1)



                Dim cmdSvcID As MySqlCommand = New MySqlCommand
                cmdSvcID.CommandType = CommandType.Text

                cmdSvcID.CommandText = "SELECT distinct * from tblProduct order by ProductID"
                cmdSvcID.Connection = conn

                Dim drSvcID As MySqlDataReader = cmdSvcID.ExecuteReader()
                Dim dtSvcID As New DataTable
                dtSvcID.Load(drSvcID)

                If dtSvcID.Rows.Count > 0 Then

                    Dim actualrevenue As Double = 0.0
                    Dim forecastrevenue As Double = 0.0
                    Dim variance As Double = 0.0

                    Dim check As String = "no"

                    For k As Integer = 0 To dtSvcID.Rows.Count - 1
                        actualrevenue = 0
                        forecastrevenue = 0
                        variance = 0
                        check = "no"

                        For i As Integer = 0 To dtContract.Rows.Count - 1
                            If dtContract.Rows(i)("serviceid").ToString = dtSvcID.Rows(k)("ProductID").ToString Then
                                check = "yes"
                                actualrevenue = dtContract.Rows(i)("TotalBillAmount")
                            End If
                        Next
                        For j As Integer = 0 To dtContract1.Rows.Count - 1
                            If dtContract1.Rows(j)("serviceid").ToString = dtSvcID.Rows(k)("ProductID").ToString Then
                                check = "yes"
                                forecastrevenue = dtContract1.Rows(j)("TotalBillAmount")
                            End If
                        Next

                        If check = "yes" Then
                            If forecastrevenue <> 0 Then
                                variance = Math.Round(((actualrevenue - forecastrevenue) / forecastrevenue) * 100)
                            Else
                                variance = 0
                            End If

                            If actualrevenue = 0 And forecastrevenue = 0 And variance = 0 Then

                            Else
                                InsertRec(conn, dtCG.Rows(c)("CompanyGroup").ToString, dtSvcID.Rows(k)("ProductID").ToString, actualrevenue, forecastrevenue, variance)

                            End If
                           End If
                    Next
                End If
            Next

        End If

        conn.Close()


    End Sub

    Private Sub InsertRec(conn As MySqlConnection, cg As String, svcid As String, actualrev As Double, forecastrev As Double, variance As Double)
        Dim command As MySqlCommand = New MySqlCommand

        command.CommandType = CommandType.Text
        Dim qry1 As String = "INSERT INTO tblrptactualforecastrevenue(CompanyGroup,ServiceID,ReportType,CreatedOn,CreatedBy,Variance,ActualRev,ForecastRev) VALUES(@CompanyGroup,@ServiceID,@ReportType,@CreatedOn,@CreatedBy,@Variance,@ActualRev,@ForecastRev);"
        command.CommandText = qry1
        command.Parameters.Clear()
        command.Parameters.AddWithValue("@CompanyGroup", cg)
        command.Parameters.AddWithValue("@ServiceID", svcid)
        command.Parameters.AddWithValue("@ReportType", "")
        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
        command.Parameters.AddWithValue("@Variance", variance)
        command.Parameters.AddWithValue("@ActualRev", actualrev)
        command.Parameters.AddWithValue("@ForecastRev", forecastrev)
        command.Connection = conn

        command.ExecuteNonQuery()
    End Sub

    Protected Sub btnPrintClientID_Click(sender As Object, e As EventArgs) Handles btnPrintClientID.Click
        Dim selFormula As String = Convert.ToString(Session("selFormula"))
        Dim selection As String = Convert.ToString(Session("selection"))


        If ddlContractGroup.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
            If selection = "" Then
                selection = "Contract Group = " + ddlContractGroup.Text
            Else
                selection = selection + ", Contract Group = " + ddlContractGroup.Text
            End If
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        Response.Redirect("RV_RevenueRptByClient.aspx")
    End Sub

    Protected Sub btnPrintTeamSel_Click(sender As Object, e As EventArgs) Handles btnPrintTeamSel.Click
        Dim selFormula As String = Convert.ToString(Session("selFormula"))
        Dim selection As String = Convert.ToString(Session("selection"))


        If String.IsNullOrEmpty(txtTeam.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.TeamID} = '" + txtTeam.Text + "'"
            If selection = "" Then
                selection = "TeamID = " + txtTeam.Text
            Else
                selection = selection + ", TeamID = " + txtTeam.Text
            End If
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rdbSelect.SelectedItem.Text = "Detail" Then
            Session.Add("rptTitle", "ACTUAL REVENUE REPORT BY TEAM - DETAIL")

            Response.Redirect("RV_RevenueRptByTeamDetail.aspx")
        ElseIf rdbSelect.SelectedItem.Text = "Summary" Then
            Session.Add("rptTitle", "ACTUAL REVENUE REPORT BY TEAM - SUMMARY")

            Response.Redirect("RV_RevenueRptByTeamSummary.aspx")

        End If

    End Sub
End Class


