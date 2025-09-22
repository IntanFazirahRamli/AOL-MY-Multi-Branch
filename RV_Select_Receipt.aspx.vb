Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_Receipt
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

    Protected Sub btnGL1_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL1.Click
        mdlPopupGLCode.TargetControlID = "btnGL1"
        txtModal.Text = "GL1"

        If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
            txtPopUpGL.Text = txtGLFrom.Text
            txtPopupGLSearch.Text = txtPopUpGL.Text
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()

        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
        End If
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub btnGL2_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL2.Click
        mdlPopupGLCode.TargetControlID = "btnGL2"
        txtModal.Text = "GL2"

        If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
            txtPopUpGL.Text = txtGLFrom.Text
            txtPopupGLSearch.Text = txtPopUpGL.Text
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()

        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
        End If
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub GrdViewGL_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdViewGL.PageIndexChanging
        GrdViewGL.PageIndex = e.NewPageIndex
        If txtPopUpGL.Text.Trim = "" Then
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
        Else
            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
        End If

        SqlDSGL.DataBind()
        GrdViewGL.DataBind()
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub GrdViewGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        Try
            If txtModal.Text = "GL1" Then
                If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    txtGLFrom.Text = " "
                Else
                    txtGLFrom.Text = GrdViewGL.SelectedRow.Cells(1).Text
                End If
            ElseIf txtModal.Text = "GL2" Then
                If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    txtGLTo.Text = " "
                Else
                    txtGLTo.Text = GrdViewGL.SelectedRow.Cells(1).Text
                End If
            End If


            mdlPopupGLCode.Hide()
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub txtPopUpGL_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpGL.TextChanged
        If txtPopUpGL.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter ledger Code", "str")
        Else
            txtPopupGLSearch.Text = txtPopUpGL.Text

            SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
            SqlDSGL.DataBind()
            GrdViewGL.DataBind()
            ' txtIsPopUp.Text = "GL"
        End If
        txtPopUpGL.Text = "Search Here for Ledger Code or Description"
    End Sub

    Protected Sub btnPopUpClientReset_Click1(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpGL.Text = "Search Here for Ledger Code or Description"
        txtPopupGLSearch.Text = ""
        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
        SqlDSGL.DataBind()
        GrdViewGL.DataBind()
        mdlPopupGLCode.Show()
    End Sub

    Protected Sub OnRowDataBoundgGL(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GrdViewGL, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgGL(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        For Each row As GridViewRow In GrdViewGL.Rows
            If row.RowIndex = GrdViewGL.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
    End Sub

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_ReceiptDetailByReceiptNo.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_ReceiptLisingSummary.aspx")
            End If

        Else
            Return

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

        End If
    End Sub

  
    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        If String.IsNullOrEmpty(txtPopupStaffSearch.Text.Trim) Then

            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"
        Else
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"
        End If

        SqlDSStaffID.DataBind()
        gvStaff.DataBind()
        gvStaff.PageIndex = e.NewPageIndex

        mdlPopupStaff.Show()
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        Try

            If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
                txtIncharge.Text = " "
            Else
                txtIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
        mdlPopupStaff.Show()

    End Sub

    Protected Sub txtPopUpStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpStaff.TextChanged
        If txtPopUpStaff.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter search text", "str")
        Else
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
            mdlPopupStaff.Show()
            ' txtIsPopUp.Text = "Staff"
        End If

        txtPopUpStaff.Text = "Search Here"
    End Sub

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        txtPopUpStaff.Text = ""
        txtPopupStaffSearch.Text = ""
        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

        SqlDSStaffID.DataBind()

        gvStaff.DataBind()
        mdlPopupStaff.Show()
    End Sub

    Protected Sub btnImgIncharge_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgIncharge.Click
        If String.IsNullOrEmpty(txtIncharge.Text.Trim) = False Then
            txtPopUpStaff.Text = txtIncharge.Text
            txtPopupStaffSearch.Text = txtPopUpStaff.Text
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
        Else
            SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
        End If
        mdlPopupStaff.Show()

    End Sub

    Protected Sub OnRowDataBoundgStaff(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvStaff, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgStaff(sender As Object, e As EventArgs)
        For Each row As GridViewRow In gvStaff.Rows
            If row.RowIndex = gvStaff.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
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

    Private Function GetData() As Boolean
        lblAlert.Text = ""

        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblrecv1.rcno} <> 0 "

        Dim qry As String = "SELECT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "tblrecv.Location as Branch, "
        End If
        If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
           
            qry = qry + "tblrecv.ReceiptNumber, tblrecv.PostStatus, tblrecv.GlStatus, tblrecv.BankStatus, tblrecv.GlPeriod, tblrecv.ReceiptDate,tblrecv.LedgerCode,tblrecv.BankID,replace(replace(replace(tblrecv.Cheque, char(10), ' '), char(13), ' '),'\t',' ') as Cheque,tblrecv.AccountID,tblrecv.ReceiptFrom, "
            qry = qry + "tblrecvdet.SubCode,tblrecvdet.LedgerCode, tblrecvdet.SubLedgerCode, tblrecvdet.RefType as Reference,tblsales.Salesman,tblsales.SalesDate, tblsales.DueDate, tblrecvdet.CostCode, replace(replace(tblrecv.comments, char(10), ' '), char(13), ' ') as Comments,tblrecvdet.ValueBase,tblsales.GstBase  "
        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
            qry = qry + "tblrecv.ReceiptNumber, tblrecv.PostStatus, tblrecv.GlStatus, tblrecv.BankStatus, tblrecv.GlPeriod, tblrecv.ReceiptDate,tblrecv.LedgerCode,tblrecv.BankID,replace(replace(replace(tblrecv.Cheque, char(10), ' '), char(13), ' '),'\t',' ') as Cheque,tblrecv.AccountID,tblrecv.ReceiptFrom, "
            qry = qry + "tblrecv.BaseAmount "
        End If
        qry = qry + "FROM ((tblrecv LEFT OUTER JOIN tblrecvdet ON tblrecv.ReceiptNumber=tblrecvdet.ReceiptNumber) left outer join tblsales on tblrecvdet.reftype = tblsales.invoicenumber) where 1=1"
    

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblrecv1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblrecv.location in (" + Convert.ToString(Session("Branch")) + ")"

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
            qry = qry + " and tblrecv.glperiod >='" + d.ToString("yyyyMM") + "'"
            selFormula = selFormula + " and {tblrecv1.glperiod} >='" + d.ToString("yyyyMM") + "'"
            If selection = "" Then
                selection = "Accounting Period >= " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Accounting Period >= " + d.ToString("yyyyMM")
            End If

        End If

        If String.IsNullOrEmpty(txtAcctPeriodTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodTo.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                ' MessageBox.Message.Alert(Page, "Accounting Period To is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD TO IS INVALID"
                Return False
            End If
            qry = qry + " and tblrecv.glperiod <='" + d.ToString("yyyyMM") + "'"

            selFormula = selFormula + " and {tblrecv1.glperiod} <='" + d.ToString("yyyyMM") + "'"
            If selection = "" Then
                selection = "Accounting Period <= " + d.ToString("yyyyMM")
            Else
                selection = selection + ", Accounting Period <= " + d.ToString("yyyyMM")
            End If
        End If

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID RECEIPT FROM DATE"
                Return False
            End If
            qry = qry + " and tblrecv.receiptdate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblrecv1.receiptDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Receipt Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Receipt Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID RECEIPT TO DATE"
                Return False
            End If
            qry = qry + " and tblrecv.Receiptdate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblrecv1.ReceiptDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Receipt Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Receipt Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        If String.IsNullOrEmpty(txtChequeAmtFrom.Text) = False Then
            Dim d As Double
            If String.IsNullOrEmpty(txtChequeAmtFrom.Text) = False Then
                If Double.TryParse(txtChequeAmtFrom.Text, d) = False Then
                    ' MessageBox.Message.Alert(Page, "Enter valid Cost Value!!", "str")
                    lblAlert.Text = "ENTER VALID VALUE: CHEQUE AMOUNT FROM"
                    Return False
                End If
            End If
            qry = qry + " and tblrecv.CHEQUEAMOUNT >= '" + txtChequeAmtFrom.Text + "'"
            selFormula = selFormula + " and {tblrecv1.CHEQUEAMOUNT} >= '" + txtChequeAmtFrom.Text + "'"
            If selection = "" Then
                selection = "Cheque Amount >= " + txtChequeAmtFrom.Text
            Else
                selection = selection + ", Cheque Amount >= " + txtChequeAmtFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtChequeAmtTo.Text) = False Then
            Dim d As Double
            If String.IsNullOrEmpty(txtChequeAmtTo.Text) = False Then
                If Double.TryParse(txtChequeAmtTo.Text, d) = False Then
                    ' MessageBox.Message.Alert(Page, "Enter valid Cost Value!!", "str")
                    lblAlert.Text = "ENTER VALID VALUE: CHEQUE AMOUNT TO"
                    Return False
                End If
            End If
            qry = qry + " and tblrecv.CHEQUEAMOUNT <= '" + txtChequeAmtTo.Text + "'"
            selFormula = selFormula + " and {tblrecv1.CHEQUEAMOUNT} <= '" + txtChequeAmtTo.Text + "'"
            If selection = "" Then
                selection = "Cheque Amount <= " + txtChequeAmtTo.Text
            Else
                selection = selection + ", Cheque Amount <= " + txtChequeAmtTo.Text
            End If
        End If


        If String.IsNullOrEmpty(txtReceiptFrom.Text) = False Then
            qry = qry + " and tblrecv.ReceiptNumber >= '" + txtReceiptFrom.Text + "'"
            selFormula = selFormula + " and {tblrecv1.ReceiptNumber} >= '" + txtReceiptFrom.Text + "'"
            If selection = "" Then
                selection = "ReceiptNumber >= " + txtReceiptFrom.Text
            Else
                selection = selection + ", ReceiptNumber >= " + txtReceiptFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtReceiptTo.Text) = False Then
            qry = qry + " and tblrecv.ReceiptNumber <= '" + txtReceiptTo.Text + "'"

            selFormula = selFormula + " and {tblrecv1.ReceiptNumber} <= '" + txtReceiptTo.Text + "'"
            If selection = "" Then
                selection = "ReceiptNumber <= " + txtReceiptTo.Text
            Else
                selection = selection + ", ReceiptNumber <= " + txtReceiptTo.Text
            End If
        End If

        If ddlBankCode.Text = "-1" Then
        Else

            qry = qry + " and tblrecv.BankId = '" + ddlBankCode.SelectedValue.ToString + "'"

            selFormula = selFormula + " and {tblrecv1.BankId} = '" + ddlBankCode.SelectedValue.ToString + "'"
            If selection = "" Then
                selection = "BankId = " + ddlBankCode.SelectedValue.ToString
            Else
                selection = selection + ", BankId = " + ddlBankCode.SelectedValue.ToString
            End If
        End If

        If String.IsNullOrEmpty(txtChequeNo.Text) = False Then
            qry = qry + " and tblrecv.Cheque = '" + txtChequeNo.Text + "'"
            selFormula = selFormula + " and {tblrecv1.Cheque} = '" + txtChequeNo.Text + "*'"
            If selection = "" Then
                selection = "ChequeNo = " + txtChequeNo.Text
            Else
                selection = selection + ", ChequeNo = " + txtChequeNo.Text
            End If
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblrecv.Salesman like '" + txtIncharge.Text + "*'"
            selFormula = selFormula + " and {tblrecv1.Salesman} like '" + txtIncharge.Text + "*'"
            If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If

        If ddlPaymentMode.Text = "-1" Then
        Else

            qry = qry + " and tblrecv.PaymentType = '" + ddlPaymentMode.Text + "'"

            selFormula = selFormula + " and {tblrecv1.PaymentType} = '" + ddlPaymentMode.Text + "'"
            If selection = "" Then
                selection = "PaymentType = " + ddlPaymentMode.Text
            Else
                selection = selection + ", PaymentType = " + ddlPaymentMode.Text
            End If
        End If

        If String.IsNullOrEmpty(txtComments.Text) = False Then
            qry = qry + " and tblrecv.Comments like '*" + txtComments.Text + "*'"
            selFormula = selFormula + " and {tblrecv1.Comments} like '*" + txtComments.Text + "*'"
            If selection = "" Then
                selection = "Comments = " + txtComments.Text
            Else
                selection = selection + ", Comments = " + txtComments.Text
            End If
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblrecv.ContactType = '" + ddlAccountType.Text + "'"
            selFormula = selFormula + " and {tblrecv1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
            selFormula = selFormula + " and {tblrecv1.Accountid} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and tblrecv.ReceiptFrom like '" + txtCustName.Text + "'"
            selFormula = selFormula + " and {tblrecv1.ReceiptFrom} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "AccountName : " + txtCustName.Text
            Else
                selection = selection + ", AccountName : " + txtCustName.Text
            End If
        End If


        If ddlBankStatus.Text = "-1" Then
        Else
            qry = qry + "tblrecv.BankStatus = '" + ddlBankStatus.Text + "'"
            selFormula = selFormula + " and {tblrecv1.BankStatus} = '" + ddlBankStatus.Text + "'"
            If selection = "" Then
                selection = "BankStatus : " + ddlBankStatus.Text
            Else
                selection = selection + ", BankStatus : " + ddlBankStatus.Text
            End If
        End If


        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
            qry = qry + " and tblrecv.GLStatus = '" + txtGLStatus.Text + "'"
            selFormula = selFormula + " and {tblrecv1.GLStatus} = '" + txtGLStatus.Text + "'"
            If selection = "" Then
                selection = "GLStatus : " + txtGLStatus.Text
            Else
                selection = selection + ", GLStatus : " + txtGLStatus.Text
            End If
        End If

        If ddlStatus.Text = "-1" Then
        Else
            qry = qry + " and tblrecv.PostStatus = '" + ddlStatus.Text + "'"
            selFormula = selFormula + " and {tblrecv1.PostStatus} = '" + ddlStatus.Text + "'"
            If selection = "" Then
                selection = "Status : " + ddlStatus.Text
            Else
                selection = selection + ", Status : " + ddlStatus.Text
            End If
        End If

        If chkVoid.Checked = False Then
            qry = qry + " and tblrecv.PostStatus <> 'V'"
            selFormula = selFormula + " and {tblrecv1.PostStatus} <> 'V'"
            If selection = "" Then
                selection = "Status NOT 'V'"
            Else
                selection = selection + ", Status NOT 'V'"
            End If
        End If
        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            qry = qry + " and tblrecvdet.ledgercode >= '" + txtGLFrom.Text + "'"
            selFormula = selFormula + " and {tblrecvdet1.ledgercode} >= '" + txtGLFrom.Text + "'"
            If selection = "" Then
                selection = "Ledger Code >= " + txtGLFrom.Text
            Else
                selection = selection + ", Ledger Code >= " + txtGLFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtGLTo.Text) = False Then
            qry = qry + " and tblrecvdet.ledgercode <= '" + txtGLTo.Text + "'"

            selFormula = selFormula + " and {tblrecvdet1.ledgercode} <= '" + txtGLTo.Text + "'"
            If selection = "" Then
                selection = "Ledger Code <= " + txtGLTo.Text
            Else
                selection = selection + ", Ledger Code <= " + txtGLTo.Text
            End If
        End If
        If String.IsNullOrEmpty(txtCostCode.Text) = False Then
            qry = qry + " and tblrecvdet.CostCode = '" + txtCostCode.Text + "'"
            selFormula = selFormula + " and {tblrecvdet1.CostCode} = '" + txtCostCode.Text + "'"
            If selection = "" Then
                selection = "CostCode : " + txtCostCode.Text
            Else
                selection = selection + ", CostCode : " + txtCostCode.Text
            End If
        End If

        If ddlItemCode.Text = "-1" Then
        Else
            qry = qry + " and tblrecvdet.ItemCode = '" + ddlItemCode.SelectedValue.ToString + "'"
            selFormula = selFormula + " and {tblrecvdet1.ItemCode} = '" + ddlItemCode.SelectedValue.ToString + "'"
            If selection = "" Then
                selection = "ItemCode : " + ddlItemCode.SelectedValue.ToString
            Else
                selection = selection + ", ItemCode : " + ddlItemCode.SelectedValue.ToString
            End If
        End If


        If String.IsNullOrEmpty(txtReference.Text) = False Then
            qry = qry + " and tblrecvdet.reftype like '" + txtReference.Text + "*'"
            selFormula = selFormula + " and {tblrecvdet1.reftype} like '" + txtReference.Text + "*'"
            If selection = "" Then
                selection = "Reference : " + txtReference.Text
            Else
                selection = selection + ", Reference : " + txtReference.Text
            End If
        End If

        If ddlSubCode.Text = "-1" Then
        Else
            qry = qry + " and tblrecvdet.SubCode = '" + ddlSubCode.Text + "'"
            selFormula = selFormula + " and {tblrecvdet1.SubCode} = '" + ddlSubCode.Text + "'"
            If selection = "" Then
                selection = "SubCode : " + ddlSubCode.Text
            Else
                selection = selection + ", SubCode : " + ddlSubCode.Text
            End If
        End If
        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
            qry = qry + " group by tblrecv.receiptnumber"

        End If

        If String.IsNullOrEmpty(lstSort2.Text) = False Then
            If lstSort2.Items(0).Selected = True Then


            End If
            Dim YrStrList As List(Of [String]) = New List(Of String)()
            Dim YrStrListVal As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In lstSort2.Items
                If item.Selected Then

                    YrStrList.Add(item.Text)
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

                        'ElseIf i = 3 Then
                        '    Session.Add("sort4", YrStrList.Item(i).ToString)
                        'ElseIf i = 4 Then
                        '    Session.Add("sort5", YrStrList.Item(i).ToString)
                        'ElseIf i = 5 Then
                        '    Session.Add("sort6", YrStrList.Item(i).ToString)
                        'ElseIf i = 6 Then
                        '    Session.Add("sort7", YrStrList.Item(i).ToString)
                    End If

                Next
            Else
                qry = qry + " ORDER BY tblrecvdet.receiptNUMBER"

            End If

        End If

        txtQuery.Text = qry

        'If rbtnSelect.SelectedValue = "1" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceByClient_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceByClient_Summary.aspx")
        '    End If
        'ElseIf rbtnSelect.SelectedValue = "2" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceByCompanyGrp_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceByCompanyGrp_Summary.aspx")
        '    End If
        'ElseIf rbtnSelect.SelectedValue = "3" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceBySalesperson_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceBySalesperson_Summary.aspx")
        '    End If
        'ElseIf rbtnSelect.SelectedValue = "4" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceByGLCode_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceByGLCode_Summary.aspx")
        '    End If
        'ElseIf rbtnSelect.SelectedValue = "5" Then
        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        'ElseIf rbtnSelect.SelectedValue = "6" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceByServiceID_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceByServiceID_Summary.aspx")
        '    End If
        'ElseIf rbtnSelect.SelectedValue = "7" Then
        '    If rbtnSelectDetSumm.SelectedValue = "1" Then
        '        Response.Redirect("RV_SalesInvoiceByBillingFrequency_Detail.aspx")
        '    ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
        '        Response.Redirect("RV_SalesInvoiceByBillingFrequency_Summary.aspx")
        '    End If

        'End If
        '  
        Return True
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
            Dim drTotal As DataRow = dt.NewRow


            If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                'qry = qry + "  tblrecvdet.SubCode,tblrecvdet.LedgerCode, tblrecvdet.SubLedgerCode, tblrecvdet.RefType as ReferenceServiceRecord, tblsales.DueDate, tblrecvdet.CostCode, replace(replace(tblrecvdet.description, char(10), ' '), char(13), ' ') as Description,tblrecvdet.ValueBase,  tblrecvdet.GstBase  "
                Dim ValueTotal As Decimal = 0
                Dim GSTTotal As Decimal = 0

                For Each dr As DataRow In dt.Rows
                    If dr.Item("ValueBase").ToString <> DBNull.Value.ToString Then
                        ValueTotal = ValueTotal + dr.Item("ValueBase")
                    End If
                    If dr.Item("GSTBase").ToString <> DBNull.Value.ToString Then
                        GSTTotal = GSTTotal + dr.Item("GSTBase")
                    End If

                Next

                drTotal.Item(0) = "Total"
                drTotal.Item("ValueBase") = ValueTotal
                drTotal.Item("GSTBase") = GSTTotal

                dt.Rows.Add(drTotal)
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                Dim Total As Decimal = 0

                For Each dr As DataRow In dt.Rows
                    If dr.Item("BaseAmount").ToString <> DBNull.Value.ToString Then
                        Total = Total + dr.Item("BaseAmount")
                    End If

                Next
                drTotal.Item(0) = "Total"
                drTotal.Item("BaseAmount") = Total

                dt.Rows.Add(drTotal)
            End If


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
        '   cell1.SetCellValue(Session("Selection").ToString)
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

            If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 21 Or j = 22 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 6 Or j = 17 Or j = 18 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 12 Then
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

        Else

            If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 20 Or j = 21 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle
                        ElseIf j = 5 Or j = 16 Or j = 17 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
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
                        ElseIf j = 5 Then
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

        End If
     




        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ReceiptListing.xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "ReceiptListing.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

        If GetData() = True Then
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return
            'lblAlert.Text = txtQuery.Text
            'Return


            Dim dt As DataTable = GetDataSet()

            WriteExcelWithNPOI(dt, "xlsx")
            Return

            Dim attachment As String = "attachment; filename=ReceiptListing.xls"
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


        ''   Dim cmd As New MySqlCommand(strQuery)



        ''Create a dummy GridView
        'Dim GridView1 As New GridView()
        'GridView1.AllowPaging = False
        'GridView1.DataSource = dt
        'GridView1.DataBind()

        'Response.Clear()
        'Response.Buffer = True
        'Response.AddHeader("content-disposition", "attachment;filename=SalesInvoiceListing.xls")
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)

        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    'Apply text style to each Row
        '    GridView1.Rows(i).Attributes.Add("class", "textmode")
        'Next
        'GridView1.RenderControl(hw)

        ''style to format numbers to string
        'Dim style As String = "<style> .textmode { } </style>"
        'Response.Write(style)
        'Response.Output.Write(sw.ToString())
        'Response.Flush()
        'Response.End()

        'Dim dt As DataTable = GetDataSet()

        'Response.Clear()
        'Response.Buffer = True
        'Response.AddHeader("content-disposition", _
        '        "attachment;filename=SalesInvoiceListing.xls")
        'Response.Charset = ""
        'Response.ContentType = "application/data"

        'Dim sb As New StringBuilder()
        'For k As Integer = 0 To dt.Columns.Count - 1
        '    'add separator
        '    sb.Append(dt.Columns(k).ColumnName)
        'Next
        ''append new line
        'sb.Append(vbCr & vbLf)
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    For k As Integer = 0 To dt.Columns.Count - 1
        '        'add separator
        '        sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";"))
        '    Next
        '    'append new line
        '    sb.Append(vbCr & vbLf)
        'Next
        'Response.Output.Write(sb.ToString())
        'Response.Flush()
        'Response.End()

        'Dim dt As DataTable = GetDataSet()


        'Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
        'Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = DirectCast(xlApp.Workbooks.Add(1), Microsoft.Office.Interop.Excel.Workbook)


        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(xlWorkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)


        'Dim misvalue As Object = System.Reflection.Missing.Value
        'For i As Integer = 0 To dt.Columns.Count - 1
        '    xlSheet.Cells(1, i + 1) = dt.Columns(i).ColumnName
        'Next
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    For j As Integer = 0 To dt.Columns.Count - 1
        '        xlSheet.Cells(i + 2, j + 1) = dt.Rows(i)(j).ToString().Trim()
        '    Next
        'Next
        'xlWorkBook.SaveAs("C:\Users\Downloads\BSDSubCategoriesTemplate_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx", misvalue, misvalue, misvalue, misvalue, misvalue, _
        '    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misvalue, misvalue, misvalue, misvalue, misvalue)
        'xlWorkBook.Close(True, misvalue, misvalue)
        'xlSheet = Nothing
        'xlApp = Nothing

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtAcctPeriodFrom.Text = ""
        txtAcctPeriodTo.Text = ""
        txtInvDateFrom.Text = ""
        txtInvDateTo.Text = ""
        txtChequeAmtFrom.Text = ""
        txtChequeAmtTo.Text = ""
        txtReceiptFrom.Text = ""
        txtReceiptTo.Text = ""
        ddlBankCode.SelectedIndex = 0
        txtChequeNo.Text = ""
        txtIncharge.Text = ""
        ddlPaymentMode.SelectedIndex = 0
        txtComments.Text = ""
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        ddlBankStatus.SelectedIndex = 0
        txtGLStatus.Text = ""
        ddlStatus.SelectedIndex = 0
        chkVoid.Checked = False
        txtGLFrom.Text = ""
        txtGLTo.Text = ""
        txtCostCode.Text = ""
        ddlItemCode.SelectedIndex = 0
        ddlSubCode.SelectedIndex = 0
        txtReference.Text = ""

        lblAlert.Text = ""
    End Sub
End Class
