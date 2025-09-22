Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class RV_Select_Ageing
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
        txtSvcDateFrom.Text = ""
        txtSvcDateTo.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""

    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        Dim selFormula As String
        Dim selection As String
        Dim qry As String
        selection = ""
        selFormula = "{tblrptageing1.rcno} <> 0"
        qry = "SELECT *,sum(CREDITAMOUNT) as creditamtsum FROM tblar WHERE invoicenumber<>'' and GLTYPE='DEBTOR'"
        If String.IsNullOrEmpty(txtSvcDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateFrom.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblrptageing1.DueDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
            qry = qry + " AND DueDate >= '" + Convert.ToDateTime(txtSvcDateFrom.Text).ToString("yyyy-MM-dd") + "'"
        End If
        If String.IsNullOrEmpty(txtSvcDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtSvcDateTo.Text, "dd/MM/yyyy", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblrptageing1.DueDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Date <= " + d.ToString("dd-MM-yyyy")
            qry = qry + " AND DueDate <= '" + Convert.ToDateTime(txtSvcDateTo.Text).ToString("yyyy-MM-dd") + "'"
        End If
        If String.IsNullOrEmpty(txtBillingPeriod.Text) = False Then
            selFormula = selFormula + " and {tblrptageing1.BillingPeriod} = '" + txtBillingPeriod.Text + "'"
            qry = qry + " AND BillingPeriod = '" + txtBillingPeriod.Text + "'"
            If selection = "" Then
                selection = "BillingPeriod : " + txtBillingPeriod.Text
            Else
                selection = selection + ", BillingPeriod : " + txtBillingPeriod.Text
            End If
        End If
        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblrptageing1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        '    qry = qry + " AND CompanyGroup = '" + ddlCompanyGrp.Text + "'"

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

            selFormula = selFormula + " and {tblrptageing1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
            qry = qry + " and CompanyGroup in [" + YrStr + "]"
        End If

        'If ddlAccountType.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblar1.ContactType} = '" + ddlAccountType.Text + "'"
        '    If selection = "" Then
        '        selection = "AccountType : " + ddlAccountType.Text
        '    Else
        '        selection = selection + ", AccountType : " + ddlAccountType.Text
        '    End If
        'End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblrptageing1.AccountID} = '" + txtAccountID.Text + "'"
            qry = qry + " AND AccountID = '" + txtAccountID.Text + "'"

            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblrptageing1.CustName} like '*" + txtCustName.Text + "*'"
            qry = qry + " AND CUSTOMERNAME = '" + txtCustName.Text + "'"

            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
        End If
        qry = qry + " GROUP BY InvoiceNumber;"

        'MessageBox.Message.Alert(Page, qry, "Str")
        'Return

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim cmdInv As MySqlCommand = New MySqlCommand

        cmdInv.CommandType = CommandType.Text

        cmdInv.CommandText = qry


        cmdInv.Connection = conn

        Dim drInv As MySqlDataReader = cmdInv.ExecuteReader()
        Dim dtInv As New DataTable
        dtInv.Load(drInv)

      

        If dtInv.Rows.Count > 0 Then
            Dim command2 As MySqlCommand = New MySqlCommand

            command2.CommandType = CommandType.Text
            command2.CommandText = "delete from tblrptAgeing where createdby='" & txtCreatedBy.Text & "';"

            command2.Connection = conn

            command2.ExecuteNonQuery()

            For i As Integer = 0 To dtInv.Rows.Count - 1
                'MessageBox.Message.Alert(Page, dtInv.Rows.Count.ToString & " " & dtInv.Rows(i)("InvoiceNumber") & " " & dtInv.Rows(i)("sum(creditamount)"), "str")
                ' Return



                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry1 As String = "INSERT INTO tblrptageing(CustName,CompanyGroup,ContractGroup,InvoiceNumber,BillingPeriod,InvoiceType,SalesPerson, CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DueDate,VoucherNumber,VoucherDate,GLCode,DebitAmount,CreditAmount,AccountID)VALUES(@CustName,@CompanyGroup,@ContractGroup,@InvoiceNumber,@BillingPeriod,@InvoiceType,@SalesPerson,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@DueDate,@VoucherNumber,@VoucherDate,@GLCode,@DebitAmount,@CreditAmount,@AccountID);"
                command.CommandText = qry1
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@CustName", dtInv.Rows(i)("CustomerName").ToString)
                command.Parameters.AddWithValue("@CompanyGroup", dtInv.Rows(i)("CompanyGroup").ToString)
                command.Parameters.AddWithValue("@ContractGroup", dtInv.Rows(i)("ContractGroup").ToString)
                command.Parameters.AddWithValue("@InvoiceNumber", dtInv.Rows(i)("InvoiceNumber").ToString)
                command.Parameters.AddWithValue("@BillingPeriod", dtInv.Rows(i)("BillingPeriod").ToString)
                command.Parameters.AddWithValue("@InvoiceType", dtInv.Rows(i)("InvoiceType").ToString)
                command.Parameters.AddWithValue("@SalesPerson", dtInv.Rows(i)("SalesMan").ToString)
                command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedBy", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(dtInv.Rows(i)("DueDate")))
                command.Parameters.AddWithValue("@VoucherNumber", dtInv.Rows(i)("VoucherNumber").ToString)
                command.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(dtInv.Rows(i)("VoucherDate")))
                command.Parameters.AddWithValue("@GLCode", dtInv.Rows(i)("GLCode").ToString)
                command.Parameters.AddWithValue("@DebitAmount", dtInv.Rows(i)("DebitAmount"))
                command.Parameters.AddWithValue("@CreditAmount", dtInv.Rows(i)("creditamtsum"))
                command.Parameters.AddWithValue("@AccountID", dtInv.Rows(i)("AccountID").ToString)

                command.Connection = conn

                command.ExecuteNonQuery()

            Next

        End If
        conn.Close()

        selFormula = selFormula + " and {tblrptageing1.createdby} = '" + txtCreatedBy.Text + "'"

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
      
        If rbtnSelect.SelectedValue = "6" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingByClient_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingByClient_Summary.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "5" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingByCompanyGroup_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingByCompanyGroup_Summary.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "4" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingByDepartment_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingByDepartment_Summary.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "3" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingByInvoiceNumber_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingByInvoiceNumber_Summary.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "2" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingByInvoiceType_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingByInvoiceType_Summary.aspx")
            End If
        ElseIf rbtnSelect.SelectedValue = "1" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_AgeingBySalesperson_Details.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_AgeingBySalesperson_Summary.aspx")
            End If
        End If
        '  

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
