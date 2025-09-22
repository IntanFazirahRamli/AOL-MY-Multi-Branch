Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Public Class RV_Selection_RevenueReportForAccounts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        If String.IsNullOrEmpty(ddlAccountType.Text) Then
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If
        If ddlAccountType.Text = "-1" Then
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If

        If String.IsNullOrEmpty(txtAccountID.Text.Trim) = False Then

            txtPopUpClient.Text = txtAccountID.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text
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
        lblAlert.Text = ""
        '   mdlPopUpClient.Show()
    End Sub

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 "
        Dim selection As String
        selection = ""
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

            '  qrySvcRec = qrySvcRec + " and tblservicerecord.location in [" + Convert.ToString(Session("Branch")) + "]"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
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
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date To is invalid", "str")
                Return
            End If
            selFormula = selFormula + "and {tblservicerecord1.ServiceDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Service Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Service Date <= " + d.ToString("dd-MM-yyyy")
            End If
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

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)
            selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"


            ' selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            ' qrySvcRec = qrySvcRec + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
        End If

        'If ddlMainContractGroup.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblContract1.ContractGroup} = '" + ddlMainContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "ContractGroup : " + ddlMainContractGroup.Text
        '    Else
        '        selection = selection + ", ContractGroup : " + ddlMainContractGroup.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        'If ddlIndustry.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblContract1.Industry} = '" + ddlIndustry.Text + "'"
        '    If selection = "" Then
        '        selection = "Industry : " + ddlIndustry.Text
        '    Else
        '        selection = selection + ", Industry : " + ddlIndustry.Text
        '    End If
        'End If

        Dim YrStrList3 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlIndustry.Items
            If item.Selected Then

                YrStrList3.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList3.Count > 0 Then

            Dim YrStr3 As [String] = [String].Join(",", YrStrList3.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            selFormula = selFormula + " and {tblcontract1.Industry} in [" + YrStr3 + "]"
            If selection = "" Then
                selection = "Industry : " + YrStr3
            Else
                selection = selection + ", Industry : " + YrStr3
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        If ddlBillingFrequency.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblContract1.BillingFrequency} = '" + ddlBillingFrequency.Text + "'"
            If selection = "" Then
                selection = "BillingFrequency : " + ddlBillingFrequency.Text
            Else
                selection = selection + ", BillingFrequency : " + ddlBillingFrequency.Text
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
            '   qry = qry + " and tblservicerecord.ContactType = '" + ddlAccountType.Text + "'"
        End If
        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            '  qry = qry + " and tblservicerecord.AccountID = '" + txtAccountID.Text + "'"
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            ' qry = qry + " and tblservicerecord.CustName like '%" + txtCustName.Text + "%'"
        End If

        If (rbtnLstStatus.SelectedValue = "All") Then
            selFormula = selFormula + " and ({tblServiceRecord1.Status} = 'O' OR {tblServiceRecord1.Status} = 'P')"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
        End If

        If (rbtnLstStatus.SelectedValue = "Open") Then
            selFormula = selFormula + " and {tblServiceRecord1.Status} = 'O'"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
        End If

        If (rbtnLstStatus.SelectedValue = "Complete") Then
            selFormula = selFormula + " and {tblServiceRecord1.Status} = 'P'"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If chkGrouping.SelectedValue = "1" Then
            Session.Add("ReportType", "RevenueRptForAccountsByContractGrp")
            Response.Redirect("RV_RevenueRptForAccountsByContractGrp.aspx")
        ElseIf chkGrouping.SelectedValue = "2" Then
            Session.Add("ReportType", "RevenueRptForAccountsByIndustry")
            Response.Redirect("RV_RevenueRptForAccountsByIndustry.aspx")
        ElseIf chkGrouping.SelectedValue = "3" Then
            mdlpnlFormat.Show()


        ElseIf chkGrouping.SelectedValue = "4" Then
            pnlFormat.Visible = False
            Session.Add("ReportType", "RevenueRptForAccountsByIndustry_Summary")
            Response.Redirect("RV_RevenueRptForAccountsByIndustry_Summary.aspx")
        End If
    End Sub

    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
        txtAccountID.Text = ""
        txtCustName.Text = ""
    End Sub

    Protected Sub chkGrouping_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''If chkGrouping.SelectedValue = "3" Then
        ''    pnlFormat.Visible = True
        ''Else
        ''    pnlFormat.Visible = False
        ''End If
    End Sub

    'Protected Sub rbtnLstFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtnLstFormat.SelectedIndexChanged
    '    If rbtnLstFormat.SelectedValue = "1" Then
    '        Response.Redirect("RV_RevenueRptForAccountsByContractGrpAndBillFreq_Format1.aspx")
    '    ElseIf rbtnLstFormat.SelectedValue = "2" Then
    '        Response.Redirect("RV_RevenueRptForAccountsByContractGrpAndBillFreq_Format2.aspx")
    '    End If
    'End Sub

    Protected Sub btnOkFormat_Click(sender As Object, e As EventArgs) Handles btnOkFormat.Click
        If rbtnLstFormat.SelectedValue = "1" Then
            Session.Add("ReportType", "RevenueRptForAccountsByContractGrpAndBillFreq_Format1")
            Response.Redirect("RV_RevenueRptForAccountsByContractGrpAndBillFreq_Format1.aspx")
        ElseIf rbtnLstFormat.SelectedValue = "2" Then
            Session.Add("ReportType", "RevenueRptForAccountsByContractGrpAndBillFreq_Format2")
            Response.Redirect("RV_RevenueRptForAccountsByContractGrpAndBillFreq_Format2.aspx")
        End If
    End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()
            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=RevenueRptforAccounts.xls"
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

        Dim selFormula As String
        selFormula = "{tblservicerecord1.rcno} <> 0 "
        Dim selection As String
        selection = ""

        Dim qry As String = "select tblcontract.ContactType,tblservicerecord.AccountID,tblservicerecord.CustName,replace(replace(tblservicerecord.Address1, char(10), ' '), char(13), ' ') as Address,tblcontract.BillingFrequency,"
        qry = qry + "tblcontract.ContractGroup,tblservicerecord.ServiceDate, replace(replace(tblcontract.Industry, char(10), ' '), char(13), ' ') as Industry,tblcontract.AgreeValue,tblservicerecord.billamount as AmountToBill "
        qry = qry + " from tblcontract,tblservicerecord where tblcontract.contractno=tblservicerecord.contractno "

        '       SELECT tblcontract1.AgreeValue, tblcontract1.ContractGroup, tblcontract1.BillingFrequency, tblservicerecord1.RecordNo, tblservicerecord1.CustName, tblservicerecord1.ServiceDate, tblservicerecord1.BillAmount, tblcontract1.Industry, tblservicerecord1.Address1, tblservicerecord1.AddBuilding, tblservicerecord1.AddStreet, tblservicerecord1.LocationID
        'FROM   sitadata.tblcontract tblcontract1 INNER JOIN sitadata.tblservicerecord tblservicerecord1 ON tblcontract1.ContractNo=tblservicerecord1.ContractNo
        'ORDER BY tblcontract1.ContractGroup, tblservicerecord1.RecordNo

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblservicerecord1.Location} in [" + Convert.ToString(Session("Branch")) + "]"

            qry = qry + " and tblservicerecord.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            Else
                MessageBox.Message.Alert(Page, "Service Date From is invalid", "str")
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

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "CompanyGroup : " + ddlCompanyGrp.Text
        '    Else
        '        selection = selection + ", CompanyGroup : " + ddlCompanyGrp.Text
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

            selFormula = selFormula + " and {tblservicerecord1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If

        Dim YrStrListZone As List(Of [String]) = New List(Of String)()

        For Each item As System.Web.UI.WebControls.ListItem In ddlZone.Items
            If item.Selected Then

                YrStrListZone.Add("""" + item.Value + """")

            End If
        Next

        If YrStrListZone.Count > 0 Then

            Dim YrStrZone As [String] = [String].Join(",", YrStrListZone.ToArray)
            selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"


            ' selFormula = selFormula + " and {tblservicerecord1.LocateGrp} in [" + YrStrZone + "]"
            If selection = "" Then
                selection = "Zone : " + YrStrZone
            Else
                selection = selection + ", Zone : " + YrStrZone
            End If
            qry = qry + " and tblservicerecord.LocateGrp in (" + YrStrZone + ")"
        End If

        'If ddlMainContractGroup.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblContract1.ContractGroup} = '" + ddlMainContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "ContractGroup : " + ddlMainContractGroup.Text
        '    Else
        '        selection = selection + ", ContractGroup : " + ddlMainContractGroup.Text
        '    End If
        'End If

        Dim YrStrList2 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In txtServiceID.Items
            If item.Selected Then

                YrStrList2.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList2.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            selFormula = selFormula + " and {tblcontract1.contractgroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "ContractGroup : " + YrStr
            Else
                selection = selection + ", ContractGroup : " + YrStr
            End If
            qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If


        'If ddlIndustry.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblContract1.Industry} = '" + ddlIndustry.Text + "'"
        '    If selection = "" Then
        '        selection = "Industry : " + ddlIndustry.Text
        '    Else
        '        selection = selection + ", Industry : " + ddlIndustry.Text
        '    End If
        'End If

        Dim YrStrList3 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlIndustry.Items
            If item.Selected Then

                YrStrList3.Add("""" + item.Value + """")

            End If
        Next

        If YrStrList3.Count > 0 Then

            Dim YrStr3 As [String] = [String].Join(",", YrStrList3.ToArray)

            '  selFormula = selFormula + " and {tblservicerecorddet1.ServiceID} in [" + YrStr + "]"
            selFormula = selFormula + " and {tblcontract1.Industry} in [" + YrStr3 + "]"
            If selection = "" Then
                selection = "Industry : " + YrStr3
            Else
                selection = selection + ", Industry : " + YrStr3
            End If
            qry = qry + " and tblcontract.Industry in (" + YrStr3 + ")"
        End If


        If ddlBillingFrequency.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblContract1.BillingFrequency} = '" + ddlBillingFrequency.Text + "'"
            If selection = "" Then
                selection = "BillingFrequency : " + ddlBillingFrequency.Text
            Else
                selection = selection + ", BillingFrequency : " + ddlBillingFrequency.Text
            End If
            qry = qry + " and tblcontract.BillingFrequency = '" + ddlBillingFrequency.Text + "'"
        End If
        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblservicerecord1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
            qry = qry + " and tblservicerecord.ContactType = '" + ddlAccountType.Text + "'"
        End If
        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            qry = qry + " and tblservicerecord.AccountID = '" + txtAccountID.Text + "'"
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblservicerecord1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            qry = qry + " and tblservicerecord.CustName like '%" + txtCustName.Text + "%'"
        End If

        If (rbtnLstStatus.SelectedValue = "All") Then
            selFormula = selFormula + " and ({tblServiceRecord1.Status} = 'O' OR {tblServiceRecord1.Status} = 'P')"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
            qry = qry + " and tblservicerecord.Status in ('O','P')"
        End If

        If (rbtnLstStatus.SelectedValue = "Open") Then
            selFormula = selFormula + " and {tblServiceRecord1.Status} = 'O'"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
            qry = qry + " and tblservicerecord.Status = 'O'"
        End If

        If (rbtnLstStatus.SelectedValue = "Complete") Then
            selFormula = selFormula + " and {tblServiceRecord1.Status} = 'P'"
            If selection = "" Then
                selection = "Status : " + rbtnLstStatus.SelectedValue
            Else
                selection = selection + ", Status : " + rbtnLstStatus.SelectedValue
            End If
            qry = qry + " and tblservicerecord.Status = 'P'"
        End If

        qry = qry + " order by tblcontract.ContractGroup,tblservicerecord.recordno"
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
        '  cell1.SetCellValue(Session("Selection").ToString)
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


        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet1.CreateRow(i + 2)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)

                If j = 8 Or j = 9 Then
                    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                        cell.SetCellValue(d)
                    Else
                        Dim d As Double = Convert.ToDouble("0.00")
                        cell.SetCellValue(d)

                    End If
                    cell.CellStyle = _doubleCellStyle


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



        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = "attachment; filename=RevenueRptforAccounts"


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
    End Sub
End Class