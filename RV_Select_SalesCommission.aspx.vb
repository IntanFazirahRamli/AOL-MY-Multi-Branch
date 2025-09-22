Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_SalesCommission
    Inherits System.Web.UI.Page

 
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
    '    gvClient.DataBind()
    'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
    '    txtPopUpClient.Text = txtCustName.Text
    '    txtPopupClientSearch.Text = txtPopUpClient.Text
    '    ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
    '    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
    '    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
    '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

    '    End If
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
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

    'Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged
    '    txtAccountID.Text = ""
    '    txtCustName.Text = ""

    'End Sub

     
        Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            Response.Redirect("RV_SalesCommissionByInvoiceDate.aspx")
        
        Else
            Return

        End If

        End Sub

    Private Function GetData() As Boolean
        Dim selFormula As String
        Dim selection As String
        '     Dim qry As String = " SELECT DISTINCT tblcontractgroup.ContractGroup, tblsales.InvoiceNumber, tblsales.SalesDate, tblsales.Terms, tblsales.CustName, tblsales.ValueBase, tblsales.StaffCode, tblsales.AccountId, tblsalesdetail.ValueBase, tblsalesdetail.AppliedBase, tblrecv.ReceiptDate, tblrecvdet.ReceiptNumber, tblrecvdet.AppliedBase, tblrecv.Cheque, tblcontractgroup.CommPct"
        '   qry = qry + " FROM  (((((tblrecv INNER JOIN tblrecvdet ON tblrecv.ReceiptNumber=tblrecvdet.ReceiptNumber) INNER JOIN tblsales ON tblrecvdet.RefType=tblsales.InvoiceNumber) INNER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber) INNER JOIN tblservicerecord tblservicerecord ON tblsalesdetail.RefType=tblservicerecord.RecordNo) INNER JOIN tblcontract ON tblservicerecord.ContractNo=tblcontract.ContractNo) INNER JOIN tblcontractgroup ON tblcontract.ContractGroup=tblcontractgroup.ContractGroup"

        Dim qry As String = " SELECT DISTINCT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "tblrecv.Location as Branch, "
        End If
        qry = qry + "tblsales.StaffCode,tblsales.AccountId,tblsales.CustName,tblsales.InvoiceNumber,tblcontractgroup.ContractGroup,  tblsales.SalesDate,tblsales.Terms,"
        qry = qry + "tblcontract.ContractNo,tblcontract.StartDate,(select count(rcno) from tbwserviceschedulegenerate where contractno=tblcontract.contractno) as Sequence,"
        qry = qry + "(round(tblRecvDet.AppliedBase/sum(tblSalesDetail.AppliedBase) * sum(tblSalesDetail.ValueBase),2)) as AmtPaid,tblrecv.ReceiptDate, round(round(tblRecvDet.AppliedBase/sum(tblSalesDetail.AppliedBase) * sum(tblSalesDetail.ValueBase),2) * tblcontractgroup.CommPct / 100,2) as Commission, tblrecv.Cheque,tblrecvdet.ReceiptNumber as VoucherNumber,IFNULL(tblcontractgroup.CommPct,0) as CommPct"
        qry = qry + ",tblsalesdetail.rcno,tblrecv.Comments "
        qry = qry + " FROM  ((((tblrecv LEFT OUTER JOIN tblrecvdet ON tblrecv.ReceiptNumber=tblrecvdet.ReceiptNumber) LEFT OUTER JOIN tblsales ON tblrecvdet.RefType=tblsales.InvoiceNumber) LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber) LEFT OUTER JOIN tblcontract ON tblsalesdetail.costcode=tblcontract.ContractNo) LEFT OUTER JOIN tblcontractgroup ON tblcontract.ContractGroup=tblcontractgroup.ContractGroup"

        qry = qry + " where tblsalesdetail.appliedbase<>0 and tblsalesdetail.valuebase<>0 and tblrecv.poststatus<>'V'"
        qry = qry + " and tblsalesdetail.rcno in (select max(rcno) from tblsalesdetail where appliedbase<>0 and valuebase<>0 group by invoicenumber)"
        selection = ""
        selFormula = "{tblsalesdetail1.Appliedbase}<>0 and {tblsalesdetail1.Valuebase}<>0 and {tblrecv1.poststatus}<>'V'"


        Dim qryJrnv As String = " SELECT DISTINCT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qryJrnv = qryJrnv + "tbljrnv.Location as Branch, "
        End If
        qryJrnv = qryJrnv + "tblsales.StaffCode,tblsales.AccountId,tblsales.CustName,tblsales.InvoiceNumber,tblcontractgroup.ContractGroup,  tblsales.SalesDate,tblsales.Terms,"
        qryJrnv = qryJrnv + "tblcontract.ContractNo,tblcontract.StartDate,(select count(rcno) from tbwserviceschedulegenerate where contractno=tblcontract.contractno) as Sequence,"
        qryJrnv = qryJrnv + "(round(tbljrnvDet.CreditBase/sum(tblSalesDetail.AppliedBase) * sum(tblSalesDetail.ValueBase),2)) as AmtPaid,tbljrnv.JournalDate, round(round(tbljrnvDet.CreditBase/sum(tblSalesDetail.AppliedBase) * sum(tblSalesDetail.ValueBase),2) * tblcontractgroup.CommPct / 100,2) as Commission, '',tbljrnvdet.VoucherNumber,IFNULL(tblcontractgroup.CommPct,0) as CommPct"
        qryJrnv = qryJrnv + ",tblsalesdetail.rcno,tbljrnv.Comments "
        qryJrnv = qryJrnv + " FROM  ((((tbljrnv LEFT OUTER JOIN tbljrnvdet ON tbljrnv.VoucherNumber=tbljrnvdet.VoucherNumber) LEFT OUTER JOIN tblsales ON tbljrnvdet.RefType=tblsales.InvoiceNumber) LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber) LEFT OUTER JOIN tblcontract ON tblsalesdetail.costcode=tblcontract.ContractNo) LEFT OUTER JOIN tblcontractgroup ON tblcontract.ContractGroup=tblcontractgroup.ContractGroup"

        qryJrnv = qryJrnv + " where tblsalesdetail.appliedbase<>0 and tblsalesdetail.valuebase<>0 and tbljrnv.poststatus<>'V'"
        qryJrnv = qryJrnv + " and tblsalesdetail.rcno in (select max(rcno) from tblsalesdetail where appliedbase<>0 and valuebase<>0 group by invoicenumber)"
      

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblrecv1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"
            qryJrnv = qryJrnv + " and tbljrnv.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If


        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                Return False
            End If
            qry = qry + " and tblrecv.glperiod ='" + d.ToString("yyyyMM") + "'"
            qryJrnv = qryJrnv + " and tbljrnv.glperiod ='" + d.ToString("yyyyMM") + "'"

            selFormula = selFormula + " and {tblrecv1.glperiod} ='" + d.ToString("yyyyMM") + "'"
            If selection = "" Then
                selection = "Accounting Period = " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Accounting Period = " + d.ToString("yyyyMM")
            End If

        End If

        '      If String.IsNullOrEmpty(txtAcctPeriodTo.Text) = False Then
        '          Dim d As DateTime
        '          If Date.TryParseExact(txtAcctPeriodTo.Text, "yyyyMM", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '          Else
        '              ' MessageBox.Message.Alert(Page, "Accounting Period To is invalid", "str")
        '              lblAlert.Text = "ACCOUNTING PERIOD TO IS INVALID"
        '              Return False
        '          End If
        '          qry = qry + " and tblsales.glperiod <='" + d.ToString("yyyyMM") + "'"

        '          selFormula = selFormula + " and {tblsales1.glperiod} <='" + d.ToString("yyyyMM") + "'"
        '          If selection = "" Then
        '              selection = "Accounting Period <= " + d.ToString("yyyyMM")
        '          Else
        '              selection = selection + ", Accounting Period <= " + d.ToString("yyyyMM")
        '          End If
        '      End If

        '      If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
        '          Dim d As DateTime
        '          If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '          Else
        '              '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
        '              lblAlert.Text = "INVALID INVOICE FROM DATE"
        '              Return False
        '          End If
        '          qry = qry + " and tblsales.salesdate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        '          selFormula = selFormula + " and {tblsales1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '          If selection = "" Then
        '              selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
        '          Else
        '              selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
        '          End If

        '      End If

        '      If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
        '          Dim d As DateTime
        '          If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '          Else
        '              '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
        '              lblAlert.Text = "INVALID INVOICE TO DATE"
        '              Return False
        '          End If
        '          qry = qry + " and tblsales.salesdate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

        '          selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '          If selection = "" Then
        '              selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
        '          Else
        '              selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
        '          End If
        '      End If

        '      If ddlAccountType.Text = "-1" Then
        '      Else
        '          qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
        '          selFormula = selFormula + " and {tblsales1.ContactType} = '" + ddlAccountType.Text + "'"
        '          If selection = "" Then
        '              selection = "AccountType : " + ddlAccountType.Text
        '          Else
        '              selection = selection + ", AccountType : " + ddlAccountType.Text
        '          End If
        '      End If

        '      If String.IsNullOrEmpty(txtAccountID.Text) = False Then
        '          qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
        '          selFormula = selFormula + " and {tblsales1.Accountid} = '" + txtAccountID.Text + "'"
        '          If selection = "" Then
        '              selection = "AccountID : " + txtAccountID.Text
        '          Else
        '              selection = selection + ", AccountID : " + txtAccountID.Text
        '          End If
        '      End If

        '      If String.IsNullOrEmpty(txtCustName.Text) = False Then
        '          qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
        '          selFormula = selFormula + " and {tblsales1.CustName} like '*" + txtCustName.Text + "*'"
        '          If selection = "" Then
        '              selection = "CustName : " + txtCustName.Text
        '          Else
        '              selection = selection + ", CustName : " + txtCustName.Text
        '          End If
        '      End If

        'Dim YrStrList1 As List(Of [String]) = New List(Of String)()

        '      For Each item As ListItem In ddlCompanyGrp.Items
        '          If item.Selected Then

        '              YrStrList1.Add("""" + item.Value + """")

        '          End If
        '      Next

        '      If YrStrList1.Count > 0 Then

        '          Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
        '          qry = qry + " and tblsales.CompanyGroup in [" + YrStr + "]"
        '          selFormula = selFormula + " and {tblsales1.CompanyGroup} in [" + YrStr + "]"
        '          If selection = "" Then
        '              selection = "CompanyGroup : " + YrStr
        '          Else
        '              selection = selection + ", CompanyGroup : " + YrStr
        '          End If
        '      End If

        '      If ddlContractGroup.Text = "-1" Then
        '      Else
        '          qry = qry + " and tblcontractgroup.ContractGroup = '" + ddlContractGroup.Text + "'"

        '          selFormula = selFormula + " and {tblcontractgroup1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '          If selection = "" Then
        '              selection = "Department = " + ddlContractGroup.Text
        '          Else
        '              selection = selection + "<br>Department = " + ddlContractGroup.Text
        '          End If
        '      End If


        If ddlSalesMan.Text = "-1" Then
        Else
            qry = qry + " and tblsales.StaffCode = '" + ddlSalesMan.Text + "'"
            qryJrnv = qryJrnv + " and tblsales.StaffCode = '" + ddlSalesMan.Text + "'"

            selFormula = selFormula + " and {tblsales1.StaffCode} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
            End If
        End If

    

        'If String.IsNullOrEmpty(txtCommissionDays.Text) = False Then
        '    qry = qry + " and DATEDIFF(tblSales.SalesDate, tblrecv.ReceiptDate) <= '" + txtCommissionDays.Text + "'"

        '    selFormula = selFormula + " and DATEDIFF(""d"", {tblSales1.SalesDate}, {tblrecv1.ReceiptDate}) <= " + txtCommissionDays.Text + ""
        '    If selection = "" Then
        '        selection = "OverdueDays <= " + txtCommissionDays.Text
        '    Else
        '        selection = selection + ", OverdueDays <= " + txtCommissionDays.Text
        '    End If
        'End If

        qry = qry + " group by tblsales.invoicenumber,tblrecvdet.receiptnumber " 'ORDER BY tblsales.InvoiceNumber, tblrecvdet.ReceiptNumber,tblsalesdetail.rcno,amtpaid"
        qryJrnv = qryJrnv + " group by tblsales.invoicenumber,tbljrnvdet.vouchernumber " 'ORDER BY tblsales.InvoiceNumber, tbljrnvdet.VoucherNumber,tblsalesdetail.rcno,amtpaid"

        If chkIncludeJournalEntries.checked Then
            txtQuery.Text = qry + " union " + qryJrnv + " ORDER BY InvoiceNumber,VoucherNumber,rcno,amtpaid"
        Else
            txtQuery.Text = qry + " ORDER BY InvoiceNumber,VoucherNumber,rcno,amtpaid"

        End If

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        Return True

    End Function
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
            txtCommissionDays.Text = "120"

            End If
        End Sub

   
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
      
        ddlSalesMan.SelectedIndex = 0
      
        txtAcctPeriodFrom.Text = ""
      

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
            dt.Columns("rcno").ColumnMapping = MappingType.Hidden

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=CommissionListing.xls"
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

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("###0")
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

                    If j = 11 Or j = 13 Or j = 16 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 10 Or j = 17 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 6 Or j = 9 Or j = 12 Then
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

                    If j = 7 Or j = 9 Or j = 12 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 13 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle

                    ElseIf j = 4 Or j = 8 Then
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
            Dim attachment As String = "attachment; filename=CommissionListing"


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
