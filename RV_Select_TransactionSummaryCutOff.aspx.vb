Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel



Partial Class RV_Select_TransactionSummaryCutOff
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
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            '    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
            '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            '    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
            '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            '    End If
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
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
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            'ElseIf String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
            '    txtPopUpClient.Text = txtCustName.Text
            '    txtPopupClientSearch.Text = txtPopUpClient.Text
            '    ' SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where rcno <>0 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "' order by contname"
            '    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
            '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
            '    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
            '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            '    End If
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
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
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

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
                SqlDSClient.SelectCommand = "SELECT distinct * From tblCOMPANY where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSClient.SelectCommand = "SELECT distinct * From tblPERSON where rcno <>0 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"

            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
        End If

        txtPopUpClient.Text = "Search Here for AccountID or Client details"
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        lblAlert.Text = ""
        Dim name As String = ""
        If txtModal.Text = "ClientFrom" Then
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDFrom.Text = ""
            Else
                txtAccountIDFrom.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                txtAccountIDTo.Text = txtAccountIDFrom.Text

            End If
            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                lblAccountIDFrom.Text = ""
            Else
                lblAccountIDFrom.Text = gvClient.SelectedRow.Cells(3).Text.Trim
                lblAccountIDTo.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            End If
            name = RetrieveClientName(txtAccountIDFrom.Text)

            If name = "Error" Then
                lblAlert.Text = "The text entered in the Account ID From field is not a valid Account ID."
                'Else
                '    lblAccountIDFrom.Text = name
                '    txtAccountIDTo.Text = txtAccountIDFrom.Text
                '    lblAccountIDTo.Text = lblAccountIDFrom.Text
            End If

        ElseIf txtModal.Text = "ClientTo" Then
            If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                txtAccountIDTo.Text = ""
            Else
                txtAccountIDTo.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            End If
            If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                lblAccountIDTo.Text = ""
            Else
                lblAccountIDTo.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            End If
            name = RetrieveClientName(txtAccountIDTo.Text)
            If name = "Error" Then
                lblAlert.Text = "The text entered in the Account ID From field is not a valid Account ID."
                'Else
                '    lblAccountIDFrom.Text = name
                '    txtAccountIDTo.Text = txtAccountIDFrom.Text
                '    lblAccountIDTo.Text = lblAccountIDFrom.Text
            End If

            'ElseIf txtModal.Text = "ClientName" Then
            '    'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            '    '    txtCustName.Text = ""
            '    'Else
            '    '    txtCustName.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            '    'End If
            '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '        txtAccountIDFrom.Text = ""
            '    Else
            '        txtAccountIDFrom.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            '    End If
            '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '        txtAccountIDTo.Text = ""
            '    Else
            '        txtAccountIDTo.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            '    End If
        End If
        If txtAccountIDFrom.Text.Trim = txtAccountIDTo.Text.Trim Then
            txtContractNo.Enabled = True
            txtLocationID.Enabled = True
            btnImgContractNo.Enabled = True
            btnImgLocationID.Enabled = True
        Else
            txtContractNo.Enabled = False
            txtLocationID.Enabled = False
            btnImgContractNo.Enabled = False
            btnImgLocationID.Enabled = False
        End If
        If String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then
            txtContractNo.Text = ""
            txtLocationID.Text = ""
        End If


        'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
        '    txtCustName.Text = ""
        'Else
        '    txtCustName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim).ToString
        'End If

    End Sub



    Protected Sub ddlAccountType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccountType.SelectedIndexChanged

    End Sub


    Private Function GetData() As Boolean
        lblAlert.Text = ""
        Dim qry As String = ""
        Dim selFormula As String
        Dim selection As String
        selection = ""
        '  If chkCheckCutOff.Checked = True Then
        selFormula = "{Command.AccountId} <> """""

        selFormula = selFormula + " and {Command.CreatedBy} = '" & Convert.ToString(Session("UserId")) & "'"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {Command.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            '   qry = qry + " and tblsales.location in [" + Convert.ToString(Session("Branch")) + "]"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If



        'If ddlAccountType.Text = "-1" Then
        'Else

        '    selFormula = selFormula + " and {Command.ContactType} = '" + ddlAccountType.Text + "'"
        '    If selection = "" Then
        '        selection = "AccountType : " + ddlAccountType.Text
        '    Else
        '        selection = selection + ", AccountType : " + ddlAccountType.Text
        '    End If
        'End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) = False Then
            '  qry = qry + " and tblsales.Accountid >= '" + txtAccountIDFrom.Text + "'"
            selFormula = selFormula + " and {Command.Accountid} >= '" + txtAccountIDFrom.Text + "'"
            If selection = "" Then
                selection = "AccountID From : " + txtAccountIDFrom.Text
            Else
                selection = selection + ", AccountID From : " + txtAccountIDFrom.Text
            End If
        End If
        If String.IsNullOrEmpty(txtAccountIDTo.Text) = False Then
            ' qry = qry + " and tblsales.Accountid <= '" + txtAccountIDTo.Text + "'"
            selFormula = selFormula + " and {Command.Accountid} <= '" + txtAccountIDTo.Text + "'"
            If selection = "" Then
                selection = "AccountID To : " + txtAccountIDTo.Text
            Else
                selection = selection + ", AccountID To : " + txtAccountIDTo.Text
            End If
        End If
        'If String.IsNullOrEmpty(txtCustName.Text) = False And (String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text)) Then
        '    '  qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
        '    selFormula = selFormula + " and {Command.CustName} like '*" + txtCustName.Text + "*'"
        '    If selection = "" Then
        '        selection = "CustName : " + txtCustName.Text
        '    Else
        '        selection = selection + ", CustName : " + txtCustName.Text
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
            '  qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
            selFormula = selFormula + " and {Command.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If



        'If ddlLocateGrp.Text = "-1" Then
        'Else
        '    qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
        '    selFormula = selFormula + " and {tblsales1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
        '    If selection = "" Then
        '        selection = "Location Group : " + ddlLocateGrp.Text
        '    Else
        '        selection = selection + ", Location Group : " + ddlLocateGrp.Text
        '    End If
        'End If


        '  If chkCheckCutOff.Checked = True Then

        If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            '    qry = qry + " and Command.VoucherDate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {Command.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
        End If
        'Else
        'If String.IsNullOrEmpty(txtPrintDate.Text) = False Then
        '    Dim d As DateTime
        '    If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

        '    Else
        '        MessageBox.Message.Alert(Page, "Print Date is invalid", "str")
        '        '  lblAlert.Text = "INVALID START DATE"
        '        Return False
        '    End If
        '    qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"

        '    selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
        '    selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
        'End If
        ' End If

        qry = qry + " ORDER BY AccountId, VoucherDate"

        txtQuery.Text = qry
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)
        Session.Add("CutOffDate", Convert.ToDateTime(txtCutOffDate.Text))

        Return True
    End Function

    Private Function GetData1() As Boolean
        lblAlert.Text = ""
        GetData()

        If String.IsNullOrEmpty(txtStartDate.Text.Trim) = False Then
            Dim d1 As DateTime
            If Date.TryParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d1) Then
            Else
                MessageBox.Message.Alert(Page, "Start Date is invalid", "str")
                Return False
            End If
        End If

        If String.IsNullOrEmpty(txtCutOffDate.Text.Trim) Then
            lblAlert.Text = "ENTER CUTOFF DATE IF CUTOFF DATE IS CHECKED"
            Return False
        Else

            Dim d As DateTime
            If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            '  txtPrintDate.Text = txtCutOffDate.Text

        End If

        Dim qry As String = ""

        If String.IsNullOrEmpty(txtContractNo.Text) And String.IsNullOrEmpty(txtLocationID.Text) And String.IsNullOrEmpty(txtStartDate.Text) Then

       
            qry = "select "
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qry = qry + "B.Location as Branch,"
            End If
            qry = qry + "B.Industry,A.AccountID,AccountName as CustName,"
            qry = qry + "if(DocumentType='ARIN', 'INVOICE', if(DocumentType='RECV','RECEIPTS',if(DocumentType='ARCN', 'CN',if(DocumentType='ARDN', 'DN', if(DocumentType='JOURNAL', 'JOURNAL',''))))) as Type,"
            qry = qry + "ContractGroup,DocumentNo as VoucherNumber, Documentdate,Amount"
            qry = qry + " FROM tbwartranssummary A left join vwcustomermainbillinginfo B on A.AccountID=B.AccountID where A.AccountID<>'' and createdby='" & Convert.ToString(Session("UserId")) & "'"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qry = qry + " and B.location in (" + Convert.ToString(Session("Branch")) + ")"


            End If

            If ddlAccountType.Text = "-1" Then
            Else
                qry = qry + " and A.ContactType = '" + ddlAccountType.Text + "'"
            End If

            If String.IsNullOrEmpty(txtAccountIDFrom.Text) = False Then
                qry = qry + " and A.Accountid >= '" + txtAccountIDFrom.Text + "'"
            End If

            If String.IsNullOrEmpty(txtAccountIDTo.Text) = False Then
                qry = qry + " and A.Accountid <= '" + txtAccountIDTo.Text + "'"
            End If

            'If String.IsNullOrEmpty(txtCustName.Text) = False And (String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text)) Then
            '    qry = qry + " and A.CustName like '%" + txtCustName.Text + "%'"
            'End If


            'If String.IsNullOrEmpty(txtContractNo.Text) = False Then
            '    qry = qry + " and A.ContractNo = '" + txtContractNo.Text + "'"
            'End If

            Dim YrStrList1 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList1.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList1.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                qry = qry + " and A.CompanyGroup in (" + YrStr + ")"

            End If


            If String.IsNullOrEmpty(txtCutOffDate.Text) = False And String.IsNullOrEmpty(txtStartDate.Text) = True Then
                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                    '  lblAlert.Text = "INVALID START DATE"
                    Return False
                End If
                qry = qry + " and A.DocumentDate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
            End If


            'If String.IsNullOrEmpty(txtStartDate.Text) = False Then
            '    Dim d As DateTime
            '    If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            '    Else
            '        MessageBox.Message.Alert(Page, "Start Date is invalid", "str")
            '        '  lblAlert.Text = "INVALID START DATE"
            '        Return False
            '    End If
            '    qry = qry + " and A.DocumentDate >= '" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "' and A.DocumentDate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
            'End If

            qry = qry + " ORDER BY VoucherDate"

        Else

            qry = "select "
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qry = qry + "B.Location as Branch,"
            End If
            qry = qry + "A.ContractNo,A.LocationID,A.StartDate,B.Industry,A.AccountID,AccountName as CustName,"
            qry = qry + "if(DocumentType='ARIN', 'INVOICE', if(DocumentType='RECV','RECEIPTS',if(DocumentType='ARCN', 'CN',if(DocumentType='ARDN', 'DN', if(DocumentType='JOURNAL', 'JOURNAL',''))))) as Type,"
            qry = qry + "ContractGroup,DocumentNo as VoucherNumber, Documentdate,Amount"
            qry = qry + " FROM tbwnewartranssummary A left join vwcustomermainbillinginfo B on A.AccountID=B.AccountID where A.AccountID<>'' and createdby='" & Convert.ToString(Session("UserId")) & "'"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qry = qry + " and B.location in (" + Convert.ToString(Session("Branch")) + ")"


            End If


            Dim YrStrList1 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList1.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList1.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                qry = qry + " and A.CompanyGroup in (" + YrStr + ")"

            End If


            If String.IsNullOrEmpty(txtCutOffDate.Text) = False And String.IsNullOrEmpty(txtStartDate.Text) = True Then
                Dim d As DateTime
                If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
                    '  lblAlert.Text = "INVALID START DATE"
                    Return False
                End If
                qry = qry + " and A.DocumentDate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
            End If


            'If String.IsNullOrEmpty(txtStartDate.Text) = False Then
            '    Dim d As DateTime
            '    If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            '    Else
            '        MessageBox.Message.Alert(Page, "Start Date is invalid", "str")
            '        '  lblAlert.Text = "INVALID START DATE"
            '        Return False
            '    End If
            '    qry = qry + " and A.DocumentDate >= '" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "' and A.DocumentDate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
            'End If

            qry = qry + " ORDER BY VoucherDate"
        End If

        txtQueryNew.Text = qry

        Session.Add("CutOffDate", Convert.ToDateTime(txtCutOffDate.Text))
        '   InsertIntoTblWebEventLog("GetData1", txtQueryNew.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)


        '''''''''''''''''''''''
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        If String.IsNullOrEmpty(txtContractNo.Text) And String.IsNullOrEmpty(txtLocationID.Text) And String.IsNullOrEmpty(txtStartDate.Text) Then
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            '   command.CommandText = "SaveTbwARDetail1TransSummary"
            command.CommandText = "SaveTransSummary"
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

            If ddlAccountType.Text = "-1" Then
                command.Parameters.AddWithValue("@pr_AccountType", "")
            Else
                command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
            End If

            command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountIDFrom.Text.Trim)
            command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)

            command.Connection = conn
            command.ExecuteScalar()
            command.Dispose()
        Else
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            '   command.CommandText = "SaveTbwARDetail1TransSummary"
            command.CommandText = "SaveNewTransSummary"
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

            command.Parameters.AddWithValue("@pr_AccountId", txtAccountIDFrom.Text.Trim)
            If String.IsNullOrEmpty(txtStartDate.Text) = False Then
                command.Parameters.AddWithValue("@pr_StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
            Else
                '    command.Parameters.AddWithValue("@pr_StartDate", DateTime.MinValue.ToShortTimeString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@pr_StartDate", "20000101")
            End If

            command.Parameters.AddWithValue("@pr_ContractNo", txtContractNo.Text.Trim)
            command.Parameters.AddWithValue("@pr_LocationID", txtLocationID.Text.Trim)

            command.Connection = conn
            command.ExecuteScalar()
            command.Dispose()
        End If
        '''''''''''''''''''''''

        conn.Close()
        conn.Dispose()

        Return True
    End Function


    Private Function GetDataSet() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQueryNew.Text


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


    Private Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "SOA - " + Convert.ToString(Session("UserID")))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub
    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData1() = True Then

            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=TransactionSummary.xls"
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

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ddlAccountType.SelectedIndex = 0
        txtAccountIDFrom.Text = ""
        txtAccountIDTo.Text = ""
        'txtCustName.Text = ""

        ddlCompanyGrp.SelectedIndex = 0

        '   ddlLocateGrp.SelectedIndex = 0

        txtCutOffDate.Text = ""
        lblAlert.Text = ""
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")
            txtCutOffDate.Text = Convert.ToString(Session("SysDate"))
            ' txtCutOffDate.Enabled = False
            txtContractNo.Enabled = False
            txtLocationID.Enabled = False
            btnImgContractNo.Enabled = False
            btnImgLocationID.Enabled = False
        End If

        If txtAccountIDFrom.Text.Trim = txtAccountIDTo.Text.Trim Then
            txtContractNo.Enabled = True
            txtLocationID.Enabled = True
            btnImgContractNo.Enabled = True
            btnImgLocationID.Enabled = True
        Else
            txtContractNo.Enabled = False
            txtLocationID.Enabled = False
            btnImgContractNo.Enabled = False
            btnImgLocationID.Enabled = False
        End If
        If String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then
            txtContractNo.Text = ""
            txtLocationID.Text = ""
            txtContractNo.Enabled = False
            txtLocationID.Enabled = False
            btnImgContractNo.Enabled = False
            btnImgLocationID.Enabled = False
        End If
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        lblAlert.Text = ""
        'If String.IsNullOrEmpty(txtPrintDate.Text) Then
        '    txtPrintDate.Text = Convert.ToString(Session("SysDate"))
        'End If
        '  If chkCheckCutOff.Checked = True Then


        'If String.IsNullOrEmpty(txtStartDate.Text) Then
        '    lblAlert.Text = "ENTER CUTOFF DATE IF CUTOFF DATE IS CHECKED"
        '    Return
        'Else
         If String.IsNullOrEmpty(txtStartDate.Text.Trim) = False Then
            Dim d1 As DateTime
            If Date.TryParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d1) Then
            Else
                MessageBox.Message.Alert(Page, "Start Date is invalid", "str")
                Return
            End If
        End If


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
            '  txtPrintDate.Text = txtCutOffDate.Text

        End If
        '   End If
        InsertIntoTblWebEventLog("TransSummary1", txtCutOffDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        If String.IsNullOrEmpty(txtContractNo.Text) And String.IsNullOrEmpty(txtLocationID.Text) And String.IsNullOrEmpty(txtStartDate.Text) Then
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            '   command.CommandText = "SaveTbwARDetail1TransSummary"
            command.CommandText = "SaveTransSummary"
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

            If ddlAccountType.Text = "-1" Then
                command.Parameters.AddWithValue("@pr_AccountType", "")
            Else
                command.Parameters.AddWithValue("@pr_AccountType", ddlAccountType.Text)
            End If

            command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountIDFrom.Text.Trim)
            command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountIDTo.Text.Trim)

            command.Connection = conn
            command.ExecuteScalar()
            command.Dispose()
            Session.Add("ReportType", "TransSummary")
            InsertIntoTblWebEventLog("TransSummary", txtCutOffDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)


        Else
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            '   command.CommandText = "SaveTbwARDetail1TransSummary"
            command.CommandText = "SaveNewTransSummary"
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

            command.Parameters.AddWithValue("@pr_AccountId", txtAccountIDFrom.Text.Trim)
            If String.IsNullOrEmpty(txtStartDate.Text) = False Then
                command.Parameters.AddWithValue("@pr_StartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))
            Else
                '    command.Parameters.AddWithValue("@pr_StartDate", DateTime.MinValue.ToShortTimeString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@pr_StartDate", "20000101")
            End If

            command.Parameters.AddWithValue("@pr_ContractNo", txtContractNo.Text.Trim)
            command.Parameters.AddWithValue("@pr_LocationID", txtLocationID.Text.Trim)

            command.Connection = conn
            command.ExecuteScalar()
            command.Dispose()
            Session.Add("ReportType", "NewTransSummary")
            If String.IsNullOrEmpty(txtContractNo.Text) Then
                Session.Add("ContractNo", "")
            Else
                Session.Add("ContractNo", txtContractNo.Text.Trim)

            End If
            InsertIntoTblWebEventLog("NewTransSummary", txtStartDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)

        End If
        '''''''''''''''''''''''


        conn.Close()
        conn.Dispose()


        '''''''''''''''''''''''''''''''''''''
        InsertIntoTblWebEventLog("TransSummary2", txtCutOffDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)


        '''''''''''''''''''''''
        If GetData() = True Then
            InsertIntoTblWebEventLog("TransSummary3", Convert.ToString(Session("SelFormula")), txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)

            Response.Redirect("RV_TransactionSummaryCutOff.aspx")
        Else
            Return
        End If
        'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
        '    If GetData() = True Then
        '        If chkCheckCutOff.Checked = True Then
        '            GetDataSet()

        '            Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=CutOff")
        '        Else
        '            Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=Today")
        '        End If

        '        '   Session.Add("Type", "PrintPDF")
        '        'If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        '        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
        '        'End If

        '    Else
        '        Return

        '    End If

        'ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
        '    If GetDataInvRecv() = True Then
        '        GetDataSetInvRecv()

        '        Response.Redirect("RV_StatementOfAccounts_InvRecv.aspx?Type=DEBIT")

        '        '   Session.Add("Type", "PrintPDF")
        '        'If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        '        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
        '        'End If

        '    Else
        '        Return

        '    End If
        'ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "3" Then
        '    'If GetData() = True Then
        '    '    If chkCheckCutOff.Checked = True Then
        '    '        GetDataSet()

        '    '        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=CutOffZERO")
        '    '    Else
        '    '        Response.Redirect("RV_StatementOfAccounts_Format1.aspx?Type=TodayZERO")
        '    '    End If

        '    '    '   Session.Add("Type", "PrintPDF")
        '    '    'If rbtnSelectDetSumm.SelectedValue = "1" Then
        '    '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        '    '    'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '    '    '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
        '    '    'End If

        '    'Else
        '    '    Return

        '    'End If
        '    If GetDataInvRecv() = True Then
        '        GetDataSetInvRecv()

        '        Response.Redirect("RV_StatementOfAccounts_InvRecv.aspx?Type=ZERO")

        '        '   Session.Add("Type", "PrintPDF")
        '        'If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        '        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
        '        'End If

        '    Else
        '        Return

        '    End If
        ' End If


    End Sub

    'Protected Sub btnClientName_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientName.Click
    '    txtModal.Text = "ClientName"
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

    '    If String.IsNullOrEmpty(txtCustName.Text.Trim) = False Then
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

    Protected Sub chkCheckCutOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckCutOff.CheckedChanged
        If chkCheckCutOff.Checked = True Then
            txtCutOffDate.Enabled = True

        Else
            txtCutOffDate.Enabled = False

        End If
    End Sub


    Protected Sub gvPopUpContractNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPopUpContractNo.SelectedIndexChanged

        If (gvPopUpContractNo.SelectedRow.Cells(2).Text = "&nbsp;") Then
            txtContractNo.Text = ""
        Else
            txtContractNo.Text = gvPopUpContractNo.SelectedRow.Cells(2).Text.Trim
        End If

        mdlPopUpContractNo.Hide()

    End Sub

    Protected Sub txtPopUpContractNo_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpContractNo.TextChanged
        If String.IsNullOrEmpty(txtPopUpContractNo.Text.Trim) = True Then
            MessageBox.Message.Alert(Page, "Please enter ContractNo", "str")
        Else
            'SqlDSContractNo.SelectCommand = "SELECT  Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription from tblContract where ((ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%') or (CustName like '%" & txtPopUpContractNo.Text & "%')) and  ((Status = 'O') or (Status = 'P') or (Status = 'E') or (Status = 'T')) order by Status, ContractNo, CustName"
            SqlDSContractNo.SelectCommand = "SELECT  Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription from tblContract where ((ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%')) and  ((Status = 'O') or (Status = 'P') or (Status = 'E') or (Status = 'T')) order by Status, ContractNo, CustName"

        End If

        mdlPopUpContractNo.Show()

    End Sub

    Protected Sub gvPopUpContractNo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPopUpContractNo.PageIndexChanging

        Try

            gvPopUpContractNo.PageIndex = e.NewPageIndex

            If String.IsNullOrEmpty(txtPopUpContractNo.Text.Trim) = True Then
                SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription  FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T'))  order by Status, ContractNo, CustName"
            Else
                SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription  FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by Status, ContractNo, CustName"
            End If

            SqlDSContractNo.DataBind()
            gvPopUpContractNo.DataBind()

            mdlPopUpContractNo.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("TransSummary5", txtCutOffDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)
        End Try
    End Sub


    Protected Sub btnImgContractNo_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgContractNo.Click
        lblAlert.Text = ""

        If txtAccountIDFrom.Text.Trim <> txtAccountIDTo.Text.Trim Then
            lblAlert.Text = "ACCOUNTID FROM AND ACCOUNTID TO ARE DIFFERENT. PLEASE ENTER ONE ACCOUNTID TO RETRIEVE ITS CONTRACTS"
            Return

        ElseIf String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then

        Else

        End If

        Try

            If String.IsNullOrEmpty(txtContractNo.Text.Trim) = False Then
                txtPopUpContractNo.Text = txtContractNo.Text

                If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = True Then
                    SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' AND ACCOUNTID = '" & txtAccountIDFrom.Text.Trim.ToUpper & "' order by ContractNo"
                Else
                    SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"

                End If

                SqlDSClient.DataBind()
                gvPopUpContractNo.DataBind()

            ElseIf String.IsNullOrEmpty(txtContractNo.Text.Trim) = True Then
                txtPopUpContractNo.Text = txtContractNo.Text

                If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = True Then
                    SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) order by ContractNo"
                Else
                    SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) and  AccountID = '" + txtAccountIDFrom.Text.Trim.ToUpper + "' order by ContractNo"
                End If

                SqlDSContractNo.DataBind()
                gvPopUpContractNo.DataBind()

            End If

            mdlPopUpContractNo.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("TransSummary4", txtCutOffDate.Text, txtAccountIDFrom.Text + " " + txtAccountIDTo.Text)
        End Try
    End Sub

    Protected Sub btnResetContractNo_Click(sender As Object, e As ImageClickEventArgs) Handles btnResetContractNo.Click
        If String.IsNullOrEmpty(txtPopUpContractNo.Text.Trim) = True Then
            MessageBox.Message.Alert(Page, "Please enter ContractNo", "str")
        Else
            If String.IsNullOrEmpty(txtAccountIDFrom.Text.Trim) = True Then
                SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) order by ContractNo"
            Else
                SqlDSContractNo.SelectCommand = "SELECT Status, ContractNo, ContractGroup, AccountId, Custname, StartDate, EndDate, ServiceAddress, ServiceDescription   FROM tblcontract WHERE ((Status = 'O') or (Status = 'P') or (Status ='E') or (Status ='T')) and  AccountID = '" + txtAccountIDFrom.Text.Trim.ToUpper + "' order by ContractNo"
            End If
        End If


        mdlPopUpContractNo.Show()

    End Sub

    Protected Sub txtAccountIDFrom_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIDFrom.TextChanged
        If String.IsNullOrEmpty(txtAccountIDFrom.Text) Then
            lblAccountIDFrom.Text = ""
        Else
            Dim name As String = RetrieveClientName(txtAccountIDFrom.Text)
            If name = "Error" Then
                lblAlert.Text = "The text entered in the Account ID From field is not a valid Account ID."
            Else
                lblAccountIDFrom.Text = name
                txtAccountIDTo.Text = txtAccountIDFrom.Text
                lblAccountIDTo.Text = lblAccountIDFrom.Text
            End If
            
        End If
        If lblAccountIDFrom.Text.Trim = lblAccountIDTo.Text.Trim Then
            If String.IsNullOrEmpty(lblAccountIDFrom.Text) = False Then
                txtContractNo.Enabled = True
                txtLocationID.Enabled = True
                btnImgContractNo.Enabled = True
                btnImgLocationID.Enabled = True
            Else
                txtContractNo.Enabled = False
                txtLocationID.Enabled = False
                btnImgContractNo.Enabled = False
                btnImgLocationID.Enabled = False
            End If
        Else
            If txtAccountIDFrom.Text.Trim = txtAccountIDTo.Text.Trim Then
                txtContractNo.Enabled = True
                txtLocationID.Enabled = True
                btnImgContractNo.Enabled = True
                btnImgLocationID.Enabled = True
            Else
                txtContractNo.Enabled = False
                txtLocationID.Enabled = False
                btnImgContractNo.Enabled = False
                btnImgLocationID.Enabled = False
            End If
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then
            txtContractNo.Text = ""
            txtLocationID.Text = ""
        End If

    End Sub

    Protected Sub txtAccountIDTo_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIDTo.TextChanged
      
        If String.IsNullOrEmpty(txtAccountIDTo.Text) Then
            lblAccountIDTo.Text = ""
        Else
            Dim name As String = RetrieveClientName(txtAccountIDTo.Text)
            If name = "Error" Then
                lblAlert.Text = "The text entered in the Account ID To field is not a valid Account ID."
            Else
                lblAccountIDTo.Text = name
                txtAccountIDFrom.Text = txtAccountIDTo.Text
                lblAccountIDFrom.Text = lblAccountIDTo.Text
            End If
          
        End If


        If lblAccountIDFrom.Text.Trim = lblAccountIDTo.Text.Trim Then
            If String.IsNullOrEmpty(lblAccountIDFrom.Text) = False Then
                txtContractNo.Enabled = True
                txtLocationID.Enabled = True
                btnImgContractNo.Enabled = True
                btnImgLocationID.Enabled = True
            Else
                txtContractNo.Enabled = False
                txtLocationID.Enabled = False
                btnImgContractNo.Enabled = False
                btnImgLocationID.Enabled = False
            End If
        Else
            If txtAccountIDFrom.Text.Trim = txtAccountIDTo.Text.Trim Then
                txtContractNo.Enabled = True
                txtLocationID.Enabled = True
                btnImgContractNo.Enabled = True
                btnImgLocationID.Enabled = True
            Else
                txtContractNo.Enabled = False
                txtLocationID.Enabled = False
                btnImgContractNo.Enabled = False
                btnImgLocationID.Enabled = False
            End If
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then
            txtContractNo.Text = ""
            txtLocationID.Text = ""
        End If

    End Sub

    Private Function RetrieveClientName(id As String) As String
        Dim qry As String = ""

        If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
            qry = "SELECT Name From tblCOMPANY where rcno <>0 and accountid = '" + id + "' order by name"

        ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
            qry = "SELECT Name From tblPERSON where rcno <>0 and accountid = '" + id + "' order by name"
        Else
            qry = "SELECT Name From tblCOMPANY where rcno <>0 and accountid = '" + id + "' union SELECT Name From tblPERSON where rcno <>0 and accountid = '" + id + "'"
        End If

        Dim conn As New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()
        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = qry
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
          

            Return dt.Rows(0)("Name").ToString
        Else

            Return "Error"
        End If

        conn.Close()
        conn.Dispose()

    End Function

    Protected Sub btnImgLocationID_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgLocationID.Click
        Dim qry As String = ""
        txtPopupLocation.Text = ""
        txtPopupLocationSearch.Text = ""

        If txtAccountIDFrom.Text.Trim <> txtAccountIDTo.Text.Trim Then
            lblAlert.Text = "ACCOUNTID FROM AND ACCOUNTID TO ARE DIFFERENT. PLEASE ENTER ONE ACCOUNTID TO RETRIEVE ITS SERVICE LOCATIONS"
            Return
        ElseIf String.IsNullOrEmpty(txtAccountIDFrom.Text) And String.IsNullOrEmpty(txtAccountIDTo.Text) Then

        Else

        End If

        If ddlAccountType.Text = "-1" Then
            lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            Return
        End If

        If String.IsNullOrEmpty(txtAccountIDFrom.Text) Then
            lblAlert.Text = "ENTER ACCOUNT ID TO PROCEED"
            Return
        End If
        Try
            If String.IsNullOrEmpty(txtLocationID.Text.Trim) = False Then
                txtPopupLocation.Text = txtLocationID.Text.Trim
                txtPopupLocationSearch.Text = txtPopupLocation.Text

                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblcompanylocation where accountid='" & txtAccountIDFrom.Text.Trim.ToUpper & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblpersonlocation where accountid='" & txtAccountIDFrom.Text & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                Else
                    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
                    Return
                End If

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qry = qry + " and  location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & Session("SecGroupAuthority") & "')"
                End If
                SqlDSLocation.SelectCommand = qry
                SqlDSLocation.DataBind()
                gvLocation.DataBind()
            Else
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                        SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblCompanylocation where accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                        SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblPersonlocation where accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                    Else
                        lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
                        Return
                    End If
                Else
                    If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                        SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblCompanylocation where  accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                    ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                        SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblPersonlocation where  accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                    Else
                        lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
                        Return
                    End If

                End If


                SqlDSLocation.DataBind()

                gvLocation.DataBind()
            End If
            mdlPopupLocation.Show()

      
        Catch ex As Exception
            InsertIntoTblWebEventLog("btnImgLoc_click", ex.Message.ToString, qry)
        End Try
    End Sub

    Protected Sub txtPopupLocation_TextChanged(sender As Object, e As EventArgs) Handles txtPopupLocation.TextChanged

        Dim qry As String
        Try
            If txtPopupLocation.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter search text", "str")
            Else
                txtPopupLocationSearch.Text = txtPopupLocation.Text
                If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblcompanylocation where accountid='" & txtAccountIDFrom.Text & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblpersonlocation where accountid='" & txtAccountIDFrom.Text & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                Else
                    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
                    Return
                End If

                SqlDSLocation.SelectCommand = qry
                SqlDSLocation.DataBind()
                gvLocation.DataBind()
                mdlPopupLocation.Show()

            End If

            txtPopupLocation.Text = "Search Here for Location Address, Postal Code or Description"
        Catch ex As Exception
            InsertIntoTblWebEventLog("txtPopupLocation_TextChanged", ex.Message.ToString, qry)
        End Try
    End Sub

    Protected Sub btnPopupLocationReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopupLocationReset.Click
        Try
            txtPopupLocation.Text = ""
            txtPopupLocationSearch.Text = ""
            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then

                SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblCompanylocation where  accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblPersonlocation where  accountid = '" + txtAccountIDFrom.Text + "' order by locationid"

            End If

            SqlDSLocation.DataBind()

            gvLocation.DataBind()
            mdlPopupLocation.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("btnPopupLocationReset_Click", ex.Message.ToString, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub gvLocation_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLocation.PageIndexChanging
        Dim qry As String
        Try


            gvLocation.PageIndex = e.NewPageIndex

            If ddlAccountType.SelectedItem.Text = "CORPORATE" Then
                If txtPopupLocationSearch.Text.Trim = "" Then
                    SqlDSLocation.SelectCommand = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblCompanylocation where  accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                Else
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblcompanylocation where accountid='" & txtAccountIDFrom.Text & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                    SqlDSLocation.SelectCommand = qry
                End If

            ElseIf ddlAccountType.SelectedItem.Text = "RESIDENTIAL" Then
                If txtPopupLocationSearch.Text.Trim = "" Then
                    SqlDSLocation.SelectCommand = "SELECT distinct accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup From tblpersonlocation where rcno <>0 and accountid = '" + txtAccountIDFrom.Text + "' order by locationid"
                Else
                    qry = "SELECT accountid, locationid, description, address1,addbuilding, addstreet,addcity,addstate,addcountry, addpostal,rcno,ServiceLocationGroup,ContractGroup FROM tblpersonlocation where accountid='" & txtAccountIDFrom.Text & "'"

                    If String.IsNullOrEmpty(txtPopupLocationSearch.Text) = False Then
                        qry = qry + " and (locationid='" & txtPopupLocationSearch.Text & "')"
                        '' or address1='" & txtSearch.Text & "' or addstreet='" & txtSearch.Text & "' or addbuilding='" & txtSearch.Text & "'or addpostal='" & txtSearch.Text & "';"
                        'qry = qry + " or description like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or address1 like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addbuilding like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addstreet like '%" + txtPopupLocationSearch.Text + "%'"
                        'qry = qry + " or addpostal like '" + txtPopupLocationSearch.Text + "%')"
                    End If
                    SqlDSLocation.SelectCommand = qry
                End If
            Else
                lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
                Return
            End If


            SqlDSLocation.DataBind()
            gvLocation.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("gvLocation_PageIndexChanging", ex.Message.ToString, qry)
        End Try
        mdlPopupLocation.Show()

    End Sub

    Protected Sub gvLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLocation.SelectedIndexChanged

        If (gvLocation.SelectedRow.Cells(1).Text = "&nbsp;") Then
            txtLocationID.Text = ""
        Else
            txtLocationID.Text = gvLocation.SelectedRow.Cells(1).Text.Trim
        End If
        mdlPopupLocation.Hide()

    End Sub

    Protected Sub OnRowDataBoundgLoc(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvLocation, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
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
        '    cell1.SetCellValue(Session("Selection").ToString)
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

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 8 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 7 Then
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

                    If j = 7 Then
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

        End If
      


        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = "attachment; filename=TransactionSummary"


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
