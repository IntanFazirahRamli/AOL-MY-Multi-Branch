Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_TerminatedServiceContract
    Inherits System.Web.UI.Page

   

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

        Protected Sub btnCloseServiceContractList_Click(sender As Object, e As EventArgs) Handles btnCloseServiceContractList.Click

            txtContractDateFrom.Text = ""
            txtContractDateTo.Text = ""
            ddlCompanyGrp.SelectedIndex = 0
        txtServiceID.SelectedIndex = 0

            ddlSalesMan.SelectedIndex = 0

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
        Dim qry As String = "select * from tblcontract where tblcontract.rcno<>0"
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

            selFormula = selFormula + "and {tblcontract1.Status} in [" + YrStr + "]"
            If selection = "" Then
                selection = "Status = " + YrStr
            Else
                selection = selection + "Status = " + YrStr
            End If
            qry = qry + " and tblcontract.Status in (" + YrStr + ")"

        End If

        'If chkIncludeRevised.Checked = True Then
        '    selFormula = selFormula + "and {tblcontract1.Status} in [""T"",""R"",""E""]"
        '    selection = selection + "Status = [T,R,E]"
        '    qry = qry + " and tblcontract.Status in [""T"",""R"",""E""]"

        'ElseIf chkIncludeRevised.Checked = False Then
        '    selFormula = selFormula + "and {tblcontract1.Status} in [""T"",""E""]"
        '    selection = selection + "Status = [T,E]"
        '    qry = qry + " and tblcontract.Status in [""T"",""E""]"

        'End If
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblcontract.location in [" + Convert.ToString(Session("Branch")) + "]"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
            If String.IsNullOrEmpty(txtContractDateFrom.Text) = False Then
                Dim d As DateTime
            If Date.TryParseExact(txtContractDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Terminated Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Terminated Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Terminated Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblcontract.ActualEnd >= '" + Convert.ToDateTime(txtContractDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If

            If String.IsNullOrEmpty(txtContractDateTo.Text) = False Then
                Dim d As DateTime
            If Date.TryParseExact(txtContractDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Terminated Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Terminated Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Terminated Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblcontract.ActualEnd <= '" + Convert.ToDateTime(txtContractDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        'If selection = "" Then
        '    selection = "CompanyGroup = " + ddlCompanyGrp.Text
        'Else
        '    selection = selection + ", CompanyGroup = " + ddlCompanyGrp.Text
        'End If
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
            qry = qry + " and tblcontract.CompanyGroup in [" + YrStr + "]"
        End If

        Dim YrStrList11 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlTermCode.Items
            If item.Selected Then

                YrStrList11.Add("""" + item.Value.Substring(0, (item.Value.IndexOf(" - "))) + """")

            End If
        Next

        If YrStrList11.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList11.ToArray)

            selFormula = selFormula + " and {tblcontract1.TerminationCode} in [" + YrStr + "]"
            If selection = "" Then
                selection = "TerminationCode : " + YrStr
            Else
                selection = selection + ", TerminationCode : " + YrStr
            End If
            qry = qry + " and tblcontract.TerminationCode in [" + YrStr + "]"
        End If


        '    If ddlContractGroup.Text = "-1" Then
        '    Else
        '        selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", Department = " + ddlContractGroup.Text
        '    End If
        '    qry = qry + " and tblcontract.ContractGroup = '" + ddlContractGroup.Text + "'"
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
            qry = qry + " and tblcontract.ContractGroup in (" + YrStr + ")"
        End If

            If ddlSalesMan.Text = "-1" Then
            Else
                selFormula = selFormula + " and {tblcontract1.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
            End If
            qry = qry + " and tblcontract.Salesman = '" + ddlSalesMan.Text + "'"
        End If

        If ddlIndustry.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Industry} = '" + ddlIndustry.Text + "'"
            If selection = "" Then
                selection = "Industry = " + ddlIndustry.Text
            Else
                selection = selection + ", Industry = " + ddlIndustry.Text
            End If
            qry = qry + " and tblcontract.Industry = '" + ddlIndustry.Text + "'"
        End If

            If ddlAccountType.Text = "-1" Then
            Else
            selFormula = selFormula + " and {tblcontract1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
            qry = qry + " and tblcontract.ContactType = '" + ddlAccountType.Text + "'"
        End If


            If String.IsNullOrEmpty(txtAccountID.Text) = False Then
                selFormula = selFormula + " and {tblcontract1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            qry = qry + " and tblcontract.AccountID = '" + txtAccountID.Text + "'"
        End If


            If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            qry = qry + " and tblcontract.CustName like '%" + txtCustName.Text + "%'"
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
                            'ElseIf i = 5 Then
                            '    Session.Add("sort6", YrStrList.Item(i).ToString)
                            'ElseIf i = 6 Then
                            '    Session.Add("sort7", YrStrList.Item(i).ToString)
                        End If

                    Next
                End If

            End If
            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

        If chkGrouping.SelectedValue = "ContractGroup" Then
            Session.Add("ReportType", "TerminatedContract02")
            Response.Redirect("RV_TerminatedContract02.aspx")
        ElseIf chkGrouping.SelectedValue = "AccountID" Then
            Session.Add("ReportType", "TerminatedContract01")
            Response.Redirect("RV_TerminatedContract01.aspx")
        ElseIf chkGrouping.SelectedValue = "Salesman" Then
            Session.Add("ReportType", "TerminatedContract03")
            Response.Redirect("RV_TerminatedContract03.aspx")

        End If

        End Sub


    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            '   MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            '    lblAlert.Text = txtQuery.Text

            '    Return

            Dim dt As DataTable = GetDataSet()
            WriteExcelWithNPOI(dt, "xlsx")

            Dim attachment As String = "attachment; filename=TerminatedContract.xls"
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

    Private Function GetData() As Boolean
        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblcontract1.rcno} <> 0"
        Dim qry As String = "select "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "location as Branch,"
        End If
        qry = qry + "ContractNo, ContractDate, Status, ContactType, AccountID, CustName, TeamID, Salesman, CompanyGroup, "
        qry = qry + "ContractGroup,AgreeValue,ActualEnd,replace(replace(TerminationDescription, char(10), ' '), char(13), ' ') as TerminationDescription,replace(replace(CommentsStatus, char(10), ' '), char(13), ' ') as TerminationComments,"
        qry = qry + "replace(replace(ServiceAddress, char(10), ' '), char(13), ' ') as ServiceAddress,replace(replace(ServiceDescription, char(10), ' '), char(13), ' ') as ServiceDescription"
        qry = qry + " from tblcontract where tblcontract.rcno<>0"

        If String.IsNullOrEmpty(chkStatusSearch.Text) = False Then
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            '  Dim YrStrList1 As List(Of [String]) = New List(Of String)()


            For Each item As ListItem In chkStatusSearch.Items
                If item.Selected Then
                    If item.Value = "ALL" Then
                    Else

                        YrStrList.Add("""" + item.Value + """")
                        '   YrStrList1.Add("'" + item.Value + "'")
                    End If

                End If
            Next


            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
            '  Dim YrStr1 As [String] = [String].Join(",", YrStrList1.ToArray())

            selFormula = selFormula + "and {tblcontract1.Status} in [" + YrStr + "]"
            If selection = "" Then
                selection = "Status = " + YrStr
            Else
                selection = selection + "Status = " + YrStr
            End If
            qry = qry + " and tblcontract.Status in (" + YrStr + ")"

        End If
       

        'If chkIncludeRevised.Checked = True Then
        '    selFormula = selFormula + "and {tblcontract1.Status} in [""T"",""R"",""E""]"
        '    selection = selection + "Status = [T,R,E]"
        '    qry = qry + " and tblcontract.Status in ('T','R','E')"

        'ElseIf chkIncludeRevised.Checked = False Then
        '    selFormula = selFormula + "and {tblcontract1.Status} in [""T"",""E""]"
        '    selection = selection + "Status = [T,E]"
        '    qry = qry + " and tblcontract.Status in ('T','E')"

        'End If
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblcontract1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblcontract.location in (" + Convert.ToString(Session("Branch")) + ")"
            
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        If String.IsNullOrEmpty(txtContractDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Terminated Date From is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Terminated Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Terminated Date >= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblcontract.ActualEnd >= '" + Convert.ToDateTime(txtContractDateFrom.Text).ToString("yyyy-MM-dd") + "'"

        End If

        If String.IsNullOrEmpty(txtContractDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtContractDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                MessageBox.Message.Alert(Page, "Terminated Date To is invalid", "str")
                '  lblAlert.Text = "INVALID START DATE"
                Return False
            End If
            selFormula = selFormula + "and {tblcontract1.ActualEnd} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Terminated Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Terminated Date <= " + d.ToString("dd-MM-yyyy")
            End If
            qry = qry + " AND tblcontract.ActualEnd <= '" + Convert.ToDateTime(txtContractDateTo.Text).ToString("yyyy-MM-dd") + "'"

        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
        'If selection = "" Then
        '    selection = "CompanyGroup = " + ddlCompanyGrp.Text
        'Else
        '    selection = selection + ", CompanyGroup = " + ddlCompanyGrp.Text
        'End If
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
            qry = qry + " and tblcontract.CompanyGroup in (" + YrStr + ")"
        End If

        Dim YrStrList11 As List(Of [String]) = New List(Of String)()

        For Each item As ListItem In ddlTermCode.Items
            If item.Selected Then

                YrStrList11.Add("""" + item.Value.Substring(0, (item.Value.IndexOf(" - "))) + """")

            End If
        Next

        If YrStrList11.Count > 0 Then

            Dim YrStr As [String] = [String].Join(",", YrStrList11.ToArray)

            selFormula = selFormula + " and {tblcontract1.TerminationCode} in [" + YrStr + "]"
            If selection = "" Then
                selection = "TerminationCode : " + YrStr
            Else
                selection = selection + ", TerminationCode : " + YrStr
            End If
            qry = qry + " and tblcontract.TerminationCode in (" + YrStr + ")"
        End If


        'If ddlContractGroup.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblcontract1.ContractGroup} = '" + ddlContractGroup.Text + "'"
        '    If selection = "" Then
        '        selection = "Department = " + ddlContractGroup.Text
        '    Else
        '        selection = selection + ", Department = " + ddlContractGroup.Text
        '    End If
        '    qry = qry + " and tblcontract.ContractGroup = '" + ddlContractGroup.Text + "'"
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
            qry = qry + " and tblcontract.ContractGroup in (" + YrStr + ")"
        End If

        If ddlSalesMan.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Salesman} = '" + ddlSalesMan.Text + "'"
            If selection = "" Then
                selection = "Salesman = " + ddlSalesMan.Text
            Else
                selection = selection + ", Salesman = " + ddlSalesMan.Text
            End If
            qry = qry + " and tblcontract.Salesman = '" + ddlSalesMan.Text + "'"
        End If

        If ddlIndustry.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.Industry} = '" + ddlIndustry.Text + "'"
            If selection = "" Then
                selection = "Industry = " + ddlIndustry.Text
            Else
                selection = selection + ", Industry = " + ddlIndustry.Text
            End If
            qry = qry + " and tblcontract.Industry = '" + ddlIndustry.Text + "'"
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            selFormula = selFormula + " and {tblcontract1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "Account Type = " + ddlAccountType.Text
            Else
                selection = selection + ", Account Type = " + ddlAccountType.Text
            End If
            qry = qry + " and tblcontract.ContactType = '" + ddlAccountType.Text + "'"
        End If


        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.AccountID} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID = " + txtAccountID.Text
            Else
                selection = selection + ", AccountID = " + txtAccountID.Text
            End If
            qry = qry + " and tblcontract.AccountID = '" + txtAccountID.Text + "'"
        End If


        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            selFormula = selFormula + " and {tblcontract1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "Client Name = " + txtCustName.Text
            Else
                selection = selection + ", Client Name = " + txtCustName.Text
            End If
            qry = qry + " and tblcontract.CustName like '%" + txtCustName.Text + "%'"
        End If


        If String.IsNullOrEmpty(lstSort2.Text) = False Then
            If lstSort2.Items(0).Selected = True Then


            End If
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            Dim YrStrListVal As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In lstSort2.Items
                If item.Selected Then

                    YrStrList.Add(item.Value)
                    YrStrListVal.Add(item.Value)
                End If
            Next
            If YrStrList.Count > 0 Then
                qry = qry + " ORDER BY "
                For i As Integer = 0 To YrStrList.Count - 1
                    If i = 0 Then
                        Session.Add("sort1", YrStrList.Item(i).ToString)
                        qry = qry + YrStrListVal.Item(i).ToString
                    ElseIf i = 1 Then
                        Session.Add("sort2", YrStrList.Item(i).ToString)
                        qry = qry + "," + YrStrListVal.Item(i).ToString
                    ElseIf i = 2 Then
                        Session.Add("sort3", YrStrList.Item(i).ToString)
                        qry = qry + "," + YrStrListVal.Item(i).ToString
                    ElseIf i = 3 Then
                        Session.Add("sort4", YrStrList.Item(i).ToString)
                        qry = qry + "," + YrStrListVal.Item(i).ToString
                    ElseIf i = 4 Then
                        Session.Add("sort5", YrStrList.Item(i).ToString)
                        qry = qry + "," + YrStrListVal.Item(i).ToString
                        'ElseIf i = 5 Then
                        '    Session.Add("sort6", YrStrList.Item(i).ToString)
                        'ElseIf i = 6 Then
                        '    Session.Add("sort7", YrStrList.Item(i).ToString)
                    End If

                Next
            Else
                qry = qry + " ORDER BY tblcontract.contractno"
            End If

        End If
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)



        txtQuery.Text = qry


        Return True

        'If chkGrouping.SelectedValue = "ContractGroup" Then
        '    Session.Add("ReportType", "TerminatedContract02")
        '    Response.Redirect("RV_TerminatedContract02.aspx")
        'ElseIf chkGrouping.SelectedValue = "AccountID" Then
        '    Session.Add("ReportType", "TerminatedContract01")
        '    Response.Redirect("RV_TerminatedContract01.aspx")
        'ElseIf chkGrouping.SelectedValue = "Salesman" Then
        '    Session.Add("ReportType", "TerminatedContract03")
        '    Response.Redirect("RV_TerminatedContract03.aspx")

        'End If
    End Function

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

                    If j = 11 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 2 Or j = 12 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)

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

                    If j = 10 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle
                    ElseIf j = 1 Or j = 11 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                            cell.SetCellValue(d)

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
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "TerminatedContract.xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "TerminatedContract.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
    End Class
