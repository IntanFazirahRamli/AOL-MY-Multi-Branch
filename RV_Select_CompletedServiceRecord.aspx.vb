Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System


Partial Class RV_Select_CompletedServiceRecord
    Inherits System.Web.UI.Page

    Protected Sub gvTeam_SelectedIndexChanged(sender As Object, e As EventArgs)

        If gvTeam.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtTeam.Text = " "
        Else
            txtTeam.Text = gvTeam.SelectedRow.Cells(2).Text
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

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSchDateFrom.Text = ""
        txtSchDateTo.Text = ""
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        txtServiceID.Text = ""
        ddlProduct.SelectedIndex = 0
        ddlScheduleType.SelectedIndex = 0
        txtTeam.Text = ""
        ddlIncharge.SelectedIndex = 0

        ddlCompanyGrp.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""

    End Sub


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' "
        Dim selection As String
        selection = ""
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '  qry = qry + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        If String.IsNullOrEmpty(txtSchDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.SchServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Schedule Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Schedule Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If
        If String.IsNullOrEmpty(txtSchDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.SchServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            If selection = "" Then
                selection = "Schedule Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Schedule Date <= " + d.ToString("dd-MM-yyyy")
            End If



        End If
        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.Contractno} = '" + txtContractNo.Text + "'"
            If selection = "" Then
                selection = "Contract No = " + txtContractNo.Text
            Else
                selection = selection + ", Contract No = " + txtContractNo.Text
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
        End If
        If txtServiceID.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
            If selection = "" Then
                selection = "ServiceID = " + txtServiceID.SelectedItem.Text
            Else
                selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
            End If
        End If
        If chkProductAll.Checked Then
            If selection = "" Then
                selection = "Product : ALL "
            Else
                selection = selection + ", Product : ALL "
            End If

        Else

            If ddlProduct.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} = '" + ddlProduct.SelectedItem.Value.ToString + "'"
                If selection = "" Then
                    selection = "Product : " + ddlProduct.SelectedItem.Value.ToString
                Else
                    selection = selection + ", Product : " + ddlProduct.SelectedItem.Value.ToString
                End If
            End If

        End If
        'If ddlScheduleType.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.ScheduleType} = '" + ddlScheduleType.Text + "'"
        '    If selection = "" Then
        '        selection = "ScheduleType : " + ddlScheduleType.Text
        '    Else
        '        selection = selection + ", ScheduleType : " + ddlScheduleType.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlScheduleType.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr2 As [String] = [String].Join(",", YrStrList2.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.ScheduleType} in [" + YrStr2 + "]"
            If selection = "" Then
                selection = "ScheduleType : " + YrStr2
            Else
                selection = selection + ", ScheduleType : " + YrStr2
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        If String.IsNullOrEmpty(txtTeam.Text) = False Then

            selFormula = selFormula + " and {tblservicerecord1.TeamID} = '" + txtTeam.Text + "'"

            If selection = "" Then
                selection = "TeamID : " + txtTeam.Text
            Else
                selection = selection + ", TeamID : " + txtTeam.Text
            End If
        End If

        If ddlIncharge.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"

            If selection = "" Then
                selection = "ServiceBy : " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy : " + ddlIncharge.Text
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

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If

        'End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
        End If


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rdbFormat.SelectedValue = "Format1" Then
            Session.Add("ReportType", "CompletedSvcList01")
            Response.Redirect("RV_CompletedSvcList01.aspx")
        ElseIf rdbFormat.SelectedValue = "Format2" Then
            Session.Add("ReportType", "CompletedSvcList02")
            Response.Redirect("RV_CompletedSvcList02.aspx")
        ElseIf rdbFormat.SelectedValue = "Format3" Then
            Session.Add("ReportType", "CompletedSvcList03")
            Response.Redirect("RV_CompletedSvcList03.aspx")
        End If

    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
       
        If GetData() = True Then
            'lblAlert.Text = txtQuery.Text
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=ActiveContract.xls"
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

    End Sub


    Private Function GetDataSet() As DataTable
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

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function

    Private Function GetData() As Boolean

       Dim qry As String = "select distinct tblservicerecord.RecordNo, tblservicerecord.Status, "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "tblservicerecord.Location as Branch, "
        End If

        qry = qry + "tblservicerecord.AccountID, tblservicerecord.CustName, tblcontract.ContractGroup,tblcontract.Industry,tblservicerecord.TeamID,tblservicerecord.ServiceBy, tblservicerecord.ServiceDate, tblservicerecord.TimeIn, tblservicerecord.TimeOut, replace(replace(tblservicerecord.Notes, char(10), ' '), char(13), ' ') as Notes, tblservicerecord.Duration, tblservicerecord.BillAmount, tblservicerecord.CustCode, replace(replace(replace(tblservicerecord.Address1, char(10), ' '), char(13), ' '),'\t',' ') as Address, replace(replace(replace(tblservicerecord.AddBuilding, char(10), ' '), char(13), ' '),'\t',' ') as AddBuilding, replace(replace(replace(tblservicerecord.AddStreet, char(10), ' '), char(13), ' '),'\t',' ') as AddStreet,tblservicerecord.AddCountry,tblservicerecord.AddPostal,tblservicerecord.SchServiceDate,replace(replace(tblservicerecord.Remarks, char(10), ' '), char(13), ' ') as Remarks,tblservicerecord.CreatedBy,tblservicerecord.CreatedOn,tblservicerecord.LastModifiedBy as LastEditedBy,tblservicerecord.LastModifiedOn as LastEditedOn,tblservicerecord.ScheduleType"
        qry = qry + " from tblservicerecord left join tblservicerecorddet on tblservicerecord.recordno=tblservicerecorddet.recordno left join tblcontract on tblservicerecord.contractno=tblcontract.contractno where tblservicerecord.rcno<>0"

        qry = qry + " and tblservicerecord.Status in ('P')"

        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 and {tblservicerecord1.Status} = 'P' "
        Dim selection As String
        selection = ""
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        If String.IsNullOrEmpty(txtSchDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblservicerecord1.SchServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Schedule Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Schedule Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblservicerecord.schserviceDate >= '" + Convert.ToDateTime(txtSchDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If
        If String.IsNullOrEmpty(txtSchDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSchDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Schedule Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblservicerecord1.SchServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"

            If selection = "" Then
                selection = "Schedule Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Schedule Date <= " + d.ToString("dd-MM-yyyy")
            End If

            qry = qry + " AND tblservicerecord.schserviceDate <= '" + Convert.ToDateTime(txtSchDateTo.Text).ToString("yyyy-MM-dd") + "'"


        End If
        If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.Contractno} = '" + txtContractNo.Text + "'"
            If selection = "" Then
                selection = "Contract No = " + txtContractNo.Text
            Else
                selection = selection + ", Contract No = " + txtContractNo.Text
            End If
            qry = qry + " and tblservicerecord.contractno='" + txtContractNo.Text + "'"
        End If
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblservicerecord.serviceDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If


        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblservicerecord.serviceDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If
        If txtServiceID.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} = '" + txtServiceID.SelectedItem.Text + "'"
            If selection = "" Then
                selection = "ServiceID = " + txtServiceID.SelectedItem.Text
            Else
                selection = selection + ", ServiceID = " + txtServiceID.SelectedItem.Text
            End If
            qry = qry + " and tblservicerecorddet.Serviceid='" + txtServiceID.SelectedItem.Text + "'"

        End If
        If chkProductAll.Checked Then
            If selection = "" Then
                selection = "Product : ALL "
            Else
                selection = selection + ", Product : ALL "
            End If

        Else

            If ddlProduct.SelectedItem.Value.ToString = "-1" Then
            Else
                selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} = '" + ddlProduct.SelectedItem.Value.ToString + "'"
                If selection = "" Then
                    selection = "Product : " + ddlProduct.SelectedItem.Value.ToString
                Else
                    selection = selection + ", Product : " + ddlProduct.SelectedItem.Value.ToString
                End If
                qry = qry + " and tblservicerecorddet.serviceid='" + ddlProduct.SelectedItem.Value.ToString + "'"

            End If
         
        End If

        'If ddlScheduleType.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.ScheduleType} = '" + ddlScheduleType.Text + "'"
        '    If selection = "" Then
        '        selection = "ScheduleType : " + ddlScheduleType.Text
        '    Else
        '        selection = selection + ", ScheduleType : " + ddlScheduleType.Text
        '    End If
        '    qry = qry + " and tblservicerecord.ScheduleType = '" + ddlScheduleType.Text + "'"

        'End If


        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlScheduleType.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr2 As [String] = [String].Join(",", YrStrList2.ToArray)

            selFormula = selFormula + " and {tblservicerecord1.ScheduleType} in [" + YrStr2 + "]"
            If selection = "" Then
                selection = "ScheduleType : " + YrStr2
            Else
                selection = selection + ", ScheduleType : " + YrStr2
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If

        If String.IsNullOrEmpty(txtTeam.Text) = False Then

            selFormula = selFormula + " and {tblservicerecord1.TeamID} = '" + txtTeam.Text + "'"

            If selection = "" Then
                selection = "TeamID : " + txtTeam.Text
            Else
                selection = selection + ", TeamID : " + txtTeam.Text
            End If
            qry = qry + " and tblservicerecord.TeamID = '" + txtTeam.Text + "'"

        End If

        If ddlIncharge.Text = "-1" Then
        Else

            selFormula = selFormula + " and {tblservicerecord1.serviceby} = '" + ddlIncharge.Text + "'"

            If selection = "" Then
                selection = "ServiceBy : " + ddlIncharge.Text
            Else
                selection = selection + ", ServiceBy : " + ddlIncharge.Text
            End If
            qry = qry + " and tblservicerecord.serviceby = '" + ddlIncharge.Text + "'"

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
            qry = qry + " and tblservicerecord.CompanyGroup in (" + YrStr + ")"
        End If


        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
        '    End If

        'End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
            qry = qry + " and tblservicerecord.ContactType = '" + ddlAccountType.Text + "'"
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
            qry = qry + " and tblservicerecord.AccountID = '" + txtAccountID.Text + "'"
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
            qry = qry + " and tblservicerecord.CustName like '%" + txtCustName.Text + "%'"
        End If


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)


        txtQuery.Text = qry

        Return True

    End Function

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String)
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
        ' cell1.SetCellValue(Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)

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


        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 13 Or j = 14 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 9 Or j = 21 Or j = 24 Or j = 26 Then
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

                    If j = 12 Or j = 13 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 8 Or j = 20 Or j = 23 Or j = 25 Then
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



        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "CompletedServiceRecordListing.xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "CompletedServiceRecordListing.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
End Class
