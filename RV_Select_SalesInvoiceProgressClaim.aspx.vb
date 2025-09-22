

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class RV_Select_SalesInvoiceProgressClaim
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

    Protected Sub btnCloseServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceRecordList.Click
        ddlCompanyGrp.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        Dim selection As String
        Dim qry As String = ""
        If rbtnSelectDetSumm.SelectedValue = "1" Then
            qry = "SELECT * FROM tblservicebillingdetail where rcno<>0"
        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then

            qry = "SELECT * FROM tblcontract where rcno<>0"
        End If

        selection = ""
        selFormula = "{tblCONTRACT1.rcno} <> 0"

        'If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Date From is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return
        '    End If
        '    selFormula = selFormula + "and {tblar1.DueDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
        '    qry = qry + " AND DueDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        'End If
        'If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Date To is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return
        '    End If
        '    selFormula = selFormula + "and {tblar1.DueDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    selection = selection + ", Date <= " + d.ToString("dd-MM-yyyy")
        '    qry = qry + " AND DueDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
        'End If
        'If String.IsNullOrEmpty(txtBillingPeriod.Text) = False Then
        '    selFormula = selFormula + " and {tblar1.BillingPeriod} = '" + txtBillingPeriod.Text + "'"
        '    qry = qry + " AND BillingPeriod = '" + txtBillingPeriod.Text + "'"
        '    If selection = "" Then
        '        selection = "BillingPeriod : " + txtBillingPeriod.Text
        '    Else
        '        selection = selection + ", BillingPeriod : " + txtBillingPeriod.Text
        '    End If
        'End If
        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    qry = qry + " and CompanyGroup = '" + ddlCompanyGrp.Text + "'"
        '    selFormula = selFormula + " and {tblcontract1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"

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

            selFormula = selFormula + " and {tblcontract1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            '  qry = qry + " and tblservicerecord.CompanyGroup in [" + YrStr + "]"
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and ContactType = '" + ddlAccountType.Text + "'"

            selFormula = selFormula + " and {tblcontract1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and AccountID = '" + txtAccountID.Text + "'"

            selFormula = selFormula + " and {tblcontract1.AccountID} = '" + txtAccountID.Text + "'"

            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and CustName like '%" + txtCustName.Text + "%'"

            selFormula = selFormula + " and {tblcontract1.CustName} like '*" + txtCustName.Text + "*'"

            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
        End If



        If rbtnSelectDetSumm.SelectedValue = "1" Then

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdContract As MySqlCommand = New MySqlCommand

            cmdContract.CommandType = CommandType.Text

            cmdContract.CommandText = qry


            cmdContract.Connection = conn

            Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()
            Dim dtContract As New DataTable
            dtContract.Load(drContract)


            If dtContract.Rows.Count > 0 Then
                For i As Integer = 0 To dtContract.Rows.Count - 1

                    Dim cmdSvcBilling As MySqlCommand = New MySqlCommand

                    cmdSvcBilling.CommandType = CommandType.Text

                    cmdSvcBilling.CommandText = "Select retentionperc from tblcontract where contractno='" + dtContract.Rows(i)("Contractno").ToString + "'"


                    cmdSvcBilling.Connection = conn

                    Dim drSvcBilling As MySqlDataReader = cmdSvcBilling.ExecuteReader()
                    Dim dtSvcBilling As New DataTable
                    dtSvcBilling.Load(drSvcBilling)


                    Dim cmdRecv As MySqlCommand = New MySqlCommand

                    cmdRecv.CommandType = CommandType.Text

                    cmdRecv.CommandText = "Select sum(ReceiptValue) from tblrecvdet where invoiceno='" + dtContract.Rows(i)("InvoiceNo").ToString + "' group by Invoiceno"


                    cmdRecv.Connection = conn

                    Dim drRecv As MySqlDataReader = cmdRecv.ExecuteReader()
                    Dim dtRecv As New DataTable
                    dtRecv.Load(drRecv)

                    'calculate retention amount
                    Dim billamt As Double = 0


                    billamt = dtContract.Rows(i)("BillAmount")
                    Dim retentionAmt As Double = 0
                    Dim retentionperc As Double = 0
                    If dtSvcBilling.Rows.Count > 0 Then
                        retentionAmt = billamt * dtSvcBilling.Rows(0)("RetentionPerc") / 100
                        retentionperc = dtSvcBilling.Rows(0)("RetentionPerc")
                    End If

                    ''''DELETE EXISTING RECORDS IN tblrptretentionsummary

                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandText = "delete from tblrptretentionsummary where createdby='" & txtCreatedBy.Text & "' and ReportType='Detail';"

                    command2.Connection = conn

                    command2.ExecuteNonQuery()

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qryInsert As String = "INSERT INTO tblrptretentionsummary(ContractNo,AccountID,ContactType,CustName,ContractDate,ContractEndDate,CompanyGroup,ContractValue,AccumClaimAmt,RetentionPerc,RetentionAmt,BalanceAmt,ReceiptAmt,UnBilledAmt,SalesPerson,CreatedBy,CreatedOn,CustAddr,InvoiceNumber,ReportType,BillAmount,GSTAmount)VALUES(@ContractNo,@AccountID,@ContactType,@CustName,@ContractDate,@ContractEndDate,@CompanyGroup,@ContractValue,@AccumClaimAmt,@RetentionPerc,@RetentionAmt,@BalanceAmt,@ReceiptAmt,@UnBilledAmt,@SalesPerson,@CreatedBy,@CreatedOn,@CustAddr,@InvoiceNumber,@ReportType,@BillAmount,@GSTAmount);"
                    command.CommandText = qryInsert
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@ContractNo", dtContract.Rows(i)("ContractNo").ToString)
                    command.Parameters.AddWithValue("@AccountID", dtContract.Rows(i)("AccountID").ToString)
                    command.Parameters.AddWithValue("@ContactType", dtContract.Rows(i)("ContactType").ToString)
                    command.Parameters.AddWithValue("@CustName", dtContract.Rows(i)("CustName").ToString)
                    command.Parameters.AddWithValue("@ContractDate", DBNull.Value)
                    command.Parameters.AddWithValue("@ContractEndDate", DBNull.Value)
                    command.Parameters.AddWithValue("@CompanyGroup", dtContract.Rows(i)("CompanyGroup").ToString)
                    command.Parameters.AddWithValue("@ContractValue", DBNull.Value)
                    command.Parameters.AddWithValue("@AccumClaimAmt", dtContract.Rows(i)("NetAmount"))
                    command.Parameters.AddWithValue("@RetentionPerc", retentionperc)
                    command.Parameters.AddWithValue("@RetentionAmt", retentionAmt)
                    command.Parameters.AddWithValue("@BalanceAmt", DBNull.Value)
                    If dtRecv.Rows.Count > 0 Then
                        command.Parameters.AddWithValue("@ReceiptAmt", dtRecv.Rows(0)("Sum(ReceiptValue)"))
                    Else
                        command.Parameters.AddWithValue("@ReceiptAmt", 0)

                    End If
                    command.Parameters.AddWithValue("@UnBilledAmt", DBNull.Value)
                    command.Parameters.AddWithValue("@SalesPerson", dtContract.Rows(i)("Salesman").ToString)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@CustAddr", dtContract.Rows(i)("Address1").ToString)
                    command.Parameters.AddWithValue("@InvoiceNumber", dtContract.Rows(i)("InvoiceNo").ToString)
                    command.Parameters.AddWithValue("@ReportType", "Detail")
                    command.Parameters.AddWithValue("@BillAmount", billamt)
                    command.Parameters.AddWithValue("@GSTAmount", dtContract.Rows(i)("GSTAmount"))

                    command.Connection = conn

                    command.ExecuteNonQuery()
                Next
            End If
            conn.Close()

            selFormula = "{tblrptretentionsummary1.createdby} = '" + txtCreatedBy.Text + "' and {tblrptretentionsummary1.ReportType} = 'Detail'"

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            Response.Redirect("RV_RetentionReport_Detail.aspx")
        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdContract As MySqlCommand = New MySqlCommand

            cmdContract.CommandType = CommandType.Text

            cmdContract.CommandText = qry


            cmdContract.Connection = conn

            Dim drContract As MySqlDataReader = cmdContract.ExecuteReader()
            Dim dtContract As New DataTable
            dtContract.Load(drContract)


            If dtContract.Rows.Count > 0 Then
                For i As Integer = 0 To dtContract.Rows.Count - 1

                    Dim cmdSvcBilling As MySqlCommand = New MySqlCommand

                    cmdSvcBilling.CommandType = CommandType.Text

                    cmdSvcBilling.CommandText = "Select sum(NetAmount),sum(billamount),SUM(GSTAmount) from tblservicebillingdetail where contractno='" + dtContract.Rows(i)("Contractno").ToString + "' group by contractno"


                    cmdSvcBilling.Connection = conn

                    Dim drSvcBilling As MySqlDataReader = cmdSvcBilling.ExecuteReader()
                    Dim dtSvcBilling As New DataTable
                    dtSvcBilling.Load(drSvcBilling)


                    Dim cmdRecv As MySqlCommand = New MySqlCommand

                    cmdRecv.CommandType = CommandType.Text

                    cmdRecv.CommandText = "Select sum(ReceiptValue) from tblrecvdet where contractno='" + dtContract.Rows(i)("Contractno").ToString + "' group by contractno"


                    cmdRecv.Connection = conn

                    Dim drRecv As MySqlDataReader = cmdRecv.ExecuteReader()
                    Dim dtRecv As New DataTable
                    dtRecv.Load(drRecv)

                    'calculate retention amount
                    Dim claimamt As Double = 0
                    Dim billamt As Double = 0
                    Dim gstamt As Double = 0
                    If dtSvcBilling.Rows.Count > 0 Then
                        claimamt = dtSvcBilling.Rows(0)("sum(NetAmount)")
                        billamt = dtSvcBilling.Rows(0)("sum(BillAmount)")
                        gstamt = dtSvcBilling.Rows(0)("sum(GSTAmount)")
                    Else
                        claimamt = 0

                    End If
                    Dim retentionAmt As Double = billamt * dtContract.Rows(i)("RetentionPerc") / 100

                    ''''DELETE EXISTING RECORDS IN tblrptretentionsummary

                    Dim command2 As MySqlCommand = New MySqlCommand

                    command2.CommandType = CommandType.Text
                    command2.CommandText = "delete from tblrptretentionsummary where createdby='" & txtCreatedBy.Text & "' and ReportType='Summary';"

                    command2.Connection = conn

                    command2.ExecuteNonQuery()

                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qryInsert As String = "INSERT INTO tblrptretentionsummary(ContractNo,AccountID,ContactType,CustName,ContractDate,ContractEndDate,CompanyGroup,ContractValue,AccumClaimAmt,RetentionPerc,RetentionAmt,BalanceAmt,ReceiptAmt,UnBilledAmt,SalesPerson,CreatedBy,CreatedOn,CustAddr,InvoiceNumber,ReportType,BillAmount,GSTAmount)VALUES(@ContractNo,@AccountID,@ContactType,@CustName,@ContractDate,@ContractEndDate,@CompanyGroup,@ContractValue,@AccumClaimAmt,@RetentionPerc,@RetentionAmt,@BalanceAmt,@ReceiptAmt,@UnBilledAmt,@SalesPerson,@CreatedBy,@CreatedOn,@CustAddr,@InvoiceNumber,@ReportType,@BillAmount,@GSTAmount);"
                    command.CommandText = qryInsert
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@ContractNo", dtContract.Rows(i)("ContractNo").ToString)
                    command.Parameters.AddWithValue("@AccountID", dtContract.Rows(i)("AccountID").ToString)
                    command.Parameters.AddWithValue("@ContactType", dtContract.Rows(i)("ContactType").ToString)
                    command.Parameters.AddWithValue("@CustName", dtContract.Rows(i)("CustName").ToString)
                    command.Parameters.AddWithValue("@ContractDate", dtContract.Rows(i)("ContractDate"))
                    command.Parameters.AddWithValue("@ContractEndDate", dtContract.Rows(i)("EndDate"))
                    command.Parameters.AddWithValue("@CompanyGroup", dtContract.Rows(i)("CompanyGroup").ToString)
                    command.Parameters.AddWithValue("@ContractValue", dtContract.Rows(i)("ContractValue"))
                    command.Parameters.AddWithValue("@AccumClaimAmt", billamt)
                    command.Parameters.AddWithValue("@RetentionPerc", dtContract.Rows(i)("RetentionPerc").ToString)
                    command.Parameters.AddWithValue("@RetentionAmt", retentionAmt)
                    command.Parameters.AddWithValue("@BalanceAmt", dtContract.Rows(i)("ContractValue") - billamt)
                    If dtRecv.Rows.Count > 0 Then
                        command.Parameters.AddWithValue("@ReceiptAmt", dtRecv.Rows(0)("Sum(ReceiptValue)"))
                    Else
                        command.Parameters.AddWithValue("@ReceiptAmt", 0)

                    End If
                    command.Parameters.AddWithValue("@UnBilledAmt", dtContract.Rows(i)("ContractValue") - billamt + retentionAmt)
                    command.Parameters.AddWithValue("@SalesPerson", dtContract.Rows(i)("Salesman").ToString)
                    command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@CustAddr", dtContract.Rows(i)("CustAddr").ToString)
                    command.Parameters.AddWithValue("@InvoiceNumber", "")
                    command.Parameters.AddWithValue("@ReportType", "Summary")
                    command.Parameters.AddWithValue("@BillAmount", billamt)
                    command.Parameters.AddWithValue("@GSTAmount", gstamt)


                    command.Connection = conn

                    command.ExecuteNonQuery()

                Next
            End If
            conn.Close()

            selFormula = "{tblrptretentionsummary1.createdby} = '" + txtCreatedBy.Text + "' and {tblrptretentionsummary1.ReportType} = 'Summary'"

            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            Response.Redirect("RV_RetentionReport_Summary.aspx")
        End If



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
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
        End If
    End Sub



End Class
