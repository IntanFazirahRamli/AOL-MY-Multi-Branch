
Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Partial Class RV_Select_InvoicePrinting
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


    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then

            '   Dim invcheck As String = ""
            '  Session("PrintType") = "MultiPrint"

            '    Dim YrStrList As List(Of [String]) = New List(Of String)()
            '   If grdViewMultiPrint.Rows.Count > 0 Then
            'For Each row As GridViewRow In grdViewMultiPrint.Rows
            'If row.RowType = DataControlRowType.DataRow Then
            '    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelectMultiPrintGV"), CheckBox)
            '    If chkRow.Checked Then
            '        YrStrList.Add("""" + TryCast(row.Cells(1).FindControl("lblInvNo"), Label).Text() + """")

            ''''''''''''''''''''''''''''''''''''''''''''''''''''
          
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
          
            '
            Response.Redirect("RV_InvoicePrinting.aspx")

        ''   Session.Add("Type", "PrintPDF")
        'If rbtnSelectDetSumm.SelectedValue = "1" Then
        '    Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then

        'End If

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

    'Protected Sub btnSort1_Click(sender As Object, e As EventArgs) Handles btnSort1.Click
    '    Dim i As Integer = lstSort1.SelectedIndex

    '    If i = -1 Then
    '        Exit Sub 'skip if no item is selected
    '    End If

    '    lstSort2.Items.Add(lstSort1.Items(i))
    '    lstSort1.Items.RemoveAt(i)


    'End Sub

    'Protected Sub btnSort2_Click(sender As Object, e As EventArgs) Handles btnSort2.Click
    '    Dim i As Integer = lstSort2.SelectedIndex

    '    If i = -1 Then
    '        Exit Sub 'skip if no item is selected
    '    End If

    '    lstSort1.Items.Add(lstSort2.Items(i))
    '    lstSort2.Items.RemoveAt(i)


    'End Sub

    Private Function GetData() As Boolean
        lblAlert.Text = ""

        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tblsalesdetail1.rcno} <> 0 AND (isnull({tblsales1.PrintCounter}) or {tblsales1.PrintCounter}="""" or {tblsales1.PrintCounter}=""0"")"
        'Dim qry As String = "SELECT tblsalesdetail.InvoiceNumber,tblsales.StaffCode, tblsales.SalesDate, tblsales.LedgerCode,tblsales.AccountId,tblsales.CustName,tblsales.BillAddress1, tblsales.BillBuilding, tblsales.BillStreet, tblsales.BillPostal,tblsales.BillCountry, "
        ''     If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
        'qry = qry + " tblsalesdetail.SubCode, tblsalesdetail.RefType as ReferenceServiceRecord, replace(replace(tblsalesdetail.description, char(10), ' '), char(13), ' ') as Description, tblsalesdetail.ValueBase, tblsalesdetail.GstBase, "
        'qry = qry + " tblsalesdetail.AppliedBase, tblsalesdetail.Gst, tblsalesdetail.GstRate, "

        ''  ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
        'qry = qry + "sum(tblsalesdetail.ValueBase) as ValueBase, sum(tblsalesdetail.GstBase) as GstBase, sum(tblsalesdetail.AppliedBase) as AppliedBase, tblsales.Gst, tblsales.GstRate, "

        ''   End If
        'qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Comments,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
        'qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber where 1=1"
      
        Dim qry As String = ""
        qry = "Update tblSales set PrintCounter = 1 where rcno<>0 and (PrintCounter="""" or PrintCounter is null or PrintCounter=""0"")"

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tblsales1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tblsales.location in [" + Convert.ToString(Session("Branch")) + "]"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        'If ddlInvoiceType.Text = "-1" Then
        'Else
        '    qry = qry + " and tblsales.doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"

        '    selFormula = selFormula + " and {tblsales1.doctype} = '" + ddlInvoiceType.SelectedValue.ToString + "'"
        '    If selection = "" Then
        '        selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
        '    Else
        '        selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
        '    End If
        'End If
        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                Return False
            End If
            qry = qry + " and tblsales.glperiod >='" + d.ToString("yyyyMM") + "'"
            selFormula = selFormula + " and {tblsales1.glperiod} >='" + d.ToString("yyyyMM") + "'"
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
            qry = qry + " and tblsales.glperiod <='" + d.ToString("yyyyMM") + "'"

            selFormula = selFormula + " and {tblsales1.glperiod} <='" + d.ToString("yyyyMM") + "'"
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
                lblAlert.Text = "INVALID INVOICE FROM DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblsales1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID INVOICE TO DATE"
                Return False
            End If
            qry = qry + " and tblsales.salesdate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tblsales1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
            qry = qry + " and tblsalesdetail.ledgercode >= '" + txtGLFrom.Text + "'"
            selFormula = selFormula + " and {tblsalesdetail1.ledgercode} >= '" + txtGLFrom.Text + "'"
            If selection = "" Then
                selection = "Ledger Code >= " + txtGLFrom.Text
            Else
                selection = selection + ", Ledger Code >= " + txtGLFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtGLTo.Text) = False Then
            qry = qry + " and tblsalesdetail.ledgercode <= '" + txtGLTo.Text + "'"

            selFormula = selFormula + " and {tblsalesdetail1.ledgercode} <= '" + txtGLTo.Text + "'"
            If selection = "" Then
                selection = "Ledger Code <= " + txtGLTo.Text
            Else
                selection = selection + ", Ledger Code <= " + txtGLTo.Text
            End If
        End If

        If String.IsNullOrEmpty(txtInvoiceNoFrom.Text) = False Then
            qry = qry + " and tblsales.InvoiceNumber >= '" + txtInvoiceNoFrom.Text + "'"
            selFormula = selFormula + " and {tblsales1.InvoiceNumber} >= '" + txtInvoiceNoFrom.Text + "'"
            If selection = "" Then
                selection = "InvoiceNumber >= " + txtInvoiceNoFrom.Text
            Else
                selection = selection + ", InvoiceNumber >= " + txtInvoiceNoFrom.Text
            End If
        End If

        If String.IsNullOrEmpty(txtInvoiceNoTo.Text) = False Then
            qry = qry + " and tblsales.InvoiceNumber <= '" + txtInvoiceNoTo.Text + "'"

            selFormula = selFormula + " and {tblsales1.InvoiceNumber} <= '" + txtInvoiceNoTo.Text + "'"
            If selection = "" Then
                selection = "InvoiceNumber <= " + txtInvoiceNoTo.Text
            Else
                selection = selection + ", InvoiceNumber <= " + txtInvoiceNoTo.Text
            End If
        End If

        If String.IsNullOrEmpty(txtOurRef.Text) = False Then
            qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

            selFormula = selFormula + " and {tblsales1.OurRef} like '" + txtOurRef.Text + "*'"
            If selection = "" Then
                selection = "OurRef = " + txtOurRef.Text
            Else
                selection = selection + ", OurRef = " + txtOurRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtYourRef.Text) = False Then
            qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
            selFormula = selFormula + " and {tblsales1.YourRef} like '" + txtYourRef.Text + "*'"
            If selection = "" Then
                selection = "YourRef = " + txtYourRef.Text
            Else
                selection = selection + ", YourRef = " + txtYourRef.Text
            End If
        End If

        If String.IsNullOrEmpty(txtIncharge.Text) = False Then
            qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            selFormula = selFormula + " and {tblsales1.StaffCode} like '" + txtIncharge.Text + "*'"
            If selection = "" Then
                selection = "Staff/Incharge = " + txtIncharge.Text
            Else
                selection = selection + ", Staff/Incharge = " + txtIncharge.Text
            End If
        End If

        If String.IsNullOrEmpty(txtPONo.Text) = False Then
            qry = qry + " and tblsales.PONo = '" + txtPONo.Text + "'"
            selFormula = selFormula + " and {tblsales1.PONo} = '" + txtPONo.Text + "'"
            If selection = "" Then
                selection = "PONo = " + txtPONo.Text
            Else
                selection = selection + ", PONo = " + txtPONo.Text
            End If
        End If

        If String.IsNullOrEmpty(txtComments.Text) = False Then
            qry = qry + " and tblsales.Comments like '*" + txtComments.Text + "*'"
            selFormula = selFormula + " and {tblsales1.Comments} like '*" + txtComments.Text + "*'"
            If selection = "" Then
                selection = "Comments = " + txtComments.Text
            Else
                selection = selection + ", Comments = " + txtComments.Text
            End If
        End If

        If ddlAccountType.Text = "-1" Then
        Else
            qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            selFormula = selFormula + " and {tblsales1.ContactType} = '" + ddlAccountType.Text + "'"
            If selection = "" Then
                selection = "AccountType : " + ddlAccountType.Text
            Else
                selection = selection + ", AccountType : " + ddlAccountType.Text
            End If
        End If

        If String.IsNullOrEmpty(txtAccountID.Text) = False Then
            qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
            selFormula = selFormula + " and {tblsales1.Accountid} = '" + txtAccountID.Text + "'"
            If selection = "" Then
                selection = "AccountID : " + txtAccountID.Text
            Else
                selection = selection + ", AccountID : " + txtAccountID.Text
            End If
        End If

        If String.IsNullOrEmpty(txtCustName.Text) = False Then
            qry = qry + " and tblsales.CustName like '%" + txtCustName.Text + "%'"
            selFormula = selFormula + " and {tblsales1.CustName} like '*" + txtCustName.Text + "*'"
            If selection = "" Then
                selection = "CustName : " + txtCustName.Text
            Else
                selection = selection + ", CustName : " + txtCustName.Text
            End If
        End If

        'If ddlCompanyGrp.Text = "-1" Then
        'Else
        '    selFormula = selFormula + " and {tblsales1.CompanyGroup} = '" + ddlCompanyGrp.Text + "'"
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
            qry = qry + " and tblsales.CompanyGroup in [" + YrStr + "]"
            selFormula = selFormula + " and {tblsales1.CompanyGroup} in [" + YrStr + "]"
            If selection = "" Then
                selection = "CompanyGroup : " + YrStr
            Else
                selection = selection + ", CompanyGroup : " + YrStr
            End If
        End If



        If ddlLocateGrp.Text = "-1" Then
        Else
            qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            selFormula = selFormula + " and {tblsales1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
            If selection = "" Then
                selection = "Location Group : " + ddlLocateGrp.Text
            Else
                selection = selection + ", Location Group : " + ddlLocateGrp.Text
            End If
        End If

        If ddlPaidStatus.Text = "-1" Then
        Else
            If ddlPaidStatus.Text = "Y" Then
                qry = qry + " and balance = 0.00"

            ElseIf ddlPaidStatus.Text = "N" Then
                qry = qry + " and balance > 0.00"

            End If
            selFormula = selFormula + " and {@fPaidStatus} = '" + ddlPaidStatus.Text + "'"
            If selection = "" Then
                selection = "PaidStatus : " + ddlPaidStatus.Text
            Else
                selection = selection + ", PaidStatus : " + ddlPaidStatus.Text
            End If
        End If


        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
            qry = qry + " and tblsales.GLStatus = '" + txtGLStatus.Text + "'"
            selFormula = selFormula + " and {tblsales1.GLStatus} = '" + txtGLStatus.Text + "'"
            If selection = "" Then
                selection = "GLStatus : " + txtGLStatus.Text
            Else
                selection = selection + ", GLStatus : " + txtGLStatus.Text
            End If
        End If

        If ddlStatus.Text = "-1" Then
        Else
            qry = qry + " and tblsales.PostStatus = '" + ddlStatus.Text + "'"
            selFormula = selFormula + " and {tblsales1.PostStatus} = '" + ddlStatus.Text + "'"
            If selection = "" Then
                selection = "Status : " + ddlStatus.Text
            Else
                selection = selection + ", Status : " + ddlStatus.Text
            End If
        End If

        If chkVoid.Checked = False Then
            qry = qry + " and tblsales.PostStatus <> 'V'"
            selFormula = selFormula + " and {tblsales1.PostStatus} <> 'V'"
            If selection = "" Then
                selection = "Status NOT 'V'"
            Else
                selection = selection + ", Status NOT 'V'"
            End If
        End If

        If String.IsNullOrEmpty(txtCostCode.Text) = False Then
            qry = qry + " and tblsalesDETAIL.CostCode = '" + txtCostCode.Text + "'"
            selFormula = selFormula + " and {tblsalesDETAIL1.CostCode} = '" + txtCostCode.Text + "'"
            If selection = "" Then
                selection = "CostCode : " + txtCostCode.Text
            Else
                selection = selection + ", CostCode : " + txtCostCode.Text
            End If
        End If

        If ddlItemCode.Text = "-1" Then
        Else
            qry = qry + " and tblsalesdetail.ItemCode = '" + ddlItemCode.SelectedValue.ToString + "'"
            selFormula = selFormula + " and {tblsalesdetail1.ItemCode} = '" + ddlItemCode.SelectedValue.ToString + "'"
            If selection = "" Then
                selection = "ItemCode : " + ddlItemCode.SelectedValue.ToString
            Else
                selection = selection + ", ItemCode : " + ddlItemCode.SelectedValue.ToString
            End If
        End If


        If String.IsNullOrEmpty(txtReference.Text) = False Then
            qry = qry + " and tblsales.createdby like '" + txtReference.Text + "*'"
            selFormula = selFormula + " and {tblsales1.createdby} like '" + txtReference.Text + "*'"
            If selection = "" Then
                selection = "Created By : " + txtReference.Text
            Else
                selection = selection + ", Created By : " + txtReference.Text
            End If
        End If

        If ddlSubCode.Text = "-1" Then
        Else
            qry = qry + " and tblsalesDETAIL.SubCode = '" + ddlSubCode.Text + "'"
            selFormula = selFormula + " and {tblsalesdetail1.SubCode} = '" + ddlSubCode.Text + "'"
            If selection = "" Then
                selection = "SubCode : " + ddlSubCode.Text
            Else
                selection = selection + ", SubCode : " + ddlSubCode.Text
            End If
        End If

        txtQuery.Text = qry

        '  lblAlert.Text = qry
        Session.Add("Query", txtQuery.Text)

        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        '  If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
        'qry = qry + " group by tblsalesdetail.invoicenumber"

        'End If

        'If String.IsNullOrEmpty(lstSort2.Text) = False Then
        '    If lstSort2.Items(0).Selected = True Then


        '    End If
        '    Dim YrStrList As List(Of [String]) = New List(Of String)()
        '    Dim YrStrListVal As List(Of [String]) = New List(Of String)()

        '    For Each item As ListItem In lstSort2.Items
        '        If item.Selected Then

        '            YrStrList.Add(item.Text)
        '            YrStrListVal.Add(item.Value)

        '        End If
        '    Next
        '    If YrStrList.Count > 0 Then
        '        qry = qry + " ORDER BY "
        '        For i As Integer = 0 To YrStrList.Count - 1
        '            If i = 0 Then
        '                Session.Add("sort1", YrStrList.Item(i).ToString)
        '                qry = qry + YrStrListVal.Item(i).ToString

        '            ElseIf i = 1 Then
        '                Session.Add("sort2", YrStrList.Item(i).ToString)
        '                qry = qry + "," + YrStrListVal.Item(i).ToString

        '            ElseIf i = 2 Then
        '                Session.Add("sort3", YrStrList.Item(i).ToString)
        '                qry = qry + "," + YrStrListVal.Item(i).ToString

        '                'ElseIf i = 3 Then
        '                '    Session.Add("sort4", YrStrList.Item(i).ToString)
        '                'ElseIf i = 4 Then
        '                '    Session.Add("sort5", YrStrList.Item(i).ToString)
        '                'ElseIf i = 5 Then
        '                '    Session.Add("sort6", YrStrList.Item(i).ToString)
        '                'ElseIf i = 6 Then
        '                '    Session.Add("sort7", YrStrList.Item(i).ToString)
        '            End If

        '        Next
        '    Else
        '        qry = qry + " ORDER BY TBLSALESDETAIL.INVOICENUMBER"

        '    End If

        'End If


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

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
    End Function



    'Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click

    '    If GetData() = True Then
    '        'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
    '        'Return

    '        Dim dt As DataTable = GetDataSet()
    '        Dim attachment As String = "attachment; filename=SalesInvoiceListing.xls"
    '        Response.ClearContent()
    '        Response.AddHeader("content-disposition", attachment)
    '        Response.ContentType = "application/vnd.ms-excel"
    '        Dim tab As String = ""
    '        For Each dc As DataColumn In dt.Columns
    '            Response.Write(tab + dc.ColumnName)
    '            tab = vbTab
    '        Next
    '        Response.Write(vbLf)
    '        Dim i As Integer
    '        For Each dr As DataRow In dt.Rows
    '            tab = ""
    '            For i = 0 To dt.Columns.Count - 1
    '                Response.Write(tab & dr(i).ToString())
    '                tab = vbTab
    '            Next
    '            Response.Write(vbLf)
    '        Next
    '        Response.[End]()

    '        dt.Clear()

    '    End If


    '    ''   Dim cmd As New MySqlCommand(strQuery)



    '    ''Create a dummy GridView
    '    'Dim GridView1 As New GridView()
    '    'GridView1.AllowPaging = False
    '    'GridView1.DataSource = dt
    '    'GridView1.DataBind()

    '    'Response.Clear()
    '    'Response.Buffer = True
    '    'Response.AddHeader("content-disposition", "attachment;filename=SalesInvoiceListing.xls")
    '    'Response.Charset = ""
    '    'Response.ContentType = "application/vnd.ms-excel"
    '    'Dim sw As New StringWriter()
    '    'Dim hw As New HtmlTextWriter(sw)

    '    'For i As Integer = 0 To GridView1.Rows.Count - 1
    '    '    'Apply text style to each Row
    '    '    GridView1.Rows(i).Attributes.Add("class", "textmode")
    '    'Next
    '    'GridView1.RenderControl(hw)

    '    ''style to format numbers to string
    '    'Dim style As String = "<style> .textmode { } </style>"
    '    'Response.Write(style)
    '    'Response.Output.Write(sw.ToString())
    '    'Response.Flush()
    '    'Response.End()

    '    'Dim dt As DataTable = GetDataSet()

    '    'Response.Clear()
    '    'Response.Buffer = True
    '    'Response.AddHeader("content-disposition", _
    '    '        "attachment;filename=SalesInvoiceListing.xls")
    '    'Response.Charset = ""
    '    'Response.ContentType = "application/data"

    '    'Dim sb As New StringBuilder()
    '    'For k As Integer = 0 To dt.Columns.Count - 1
    '    '    'add separator
    '    '    sb.Append(dt.Columns(k).ColumnName)
    '    'Next
    '    ''append new line
    '    'sb.Append(vbCr & vbLf)
    '    'For i As Integer = 0 To dt.Rows.Count - 1
    '    '    For k As Integer = 0 To dt.Columns.Count - 1
    '    '        'add separator
    '    '        sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";"))
    '    '    Next
    '    '    'append new line
    '    '    sb.Append(vbCr & vbLf)
    '    'Next
    '    'Response.Output.Write(sb.ToString())
    '    'Response.Flush()
    '    'Response.End()

    '    'Dim dt As DataTable = GetDataSet()


    '    'Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
    '    'Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook = DirectCast(xlApp.Workbooks.Add(1), Microsoft.Office.Interop.Excel.Workbook)


    '    'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet = DirectCast(xlWorkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)


    '    'Dim misvalue As Object = System.Reflection.Missing.Value
    '    'For i As Integer = 0 To dt.Columns.Count - 1
    '    '    xlSheet.Cells(1, i + 1) = dt.Columns(i).ColumnName
    '    'Next
    '    'For i As Integer = 0 To dt.Rows.Count - 1
    '    '    For j As Integer = 0 To dt.Columns.Count - 1
    '    '        xlSheet.Cells(i + 2, j + 1) = dt.Rows(i)(j).ToString().Trim()
    '    '    Next
    '    'Next
    '    'xlWorkBook.SaveAs("C:\Users\Downloads\BSDSubCategoriesTemplate_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx", misvalue, misvalue, misvalue, misvalue, misvalue, _
    '    '    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misvalue, misvalue, misvalue, misvalue, misvalue)
    '    'xlWorkBook.Close(True, misvalue, misvalue)
    '    'xlSheet = Nothing
    '    'xlApp = Nothing

    'End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtAcctPeriodFrom.Text = ""
        txtAcctPeriodTo.Text = ""
        txtInvDateFrom.Text = ""
        txtInvDateTo.Text = ""
        txtGLFrom.Text = ""
        txtGLTo.Text = ""
        txtInvoiceNoFrom.Text = ""
        txtInvoiceNoTo.Text = ""
        txtOurRef.Text = ""
        txtYourRef.Text = ""
        txtIncharge.Text = ""
        txtPONo.Text = ""
        txtComments.Text = ""
        ddlCompanyGrp.SelectedIndex = 0
        ddlAccountType.SelectedIndex = 0
        txtAccountID.Text = ""
        txtCustName.Text = ""
        ddlLocateGrp.SelectedIndex = 0
        ddlPaidStatus.SelectedIndex = 0
        txtGLStatus.Text = ""
        ddlStatus.SelectedIndex = 0
        chkVoid.Checked = False
        txtCostCode.Text = ""
        ddlSubCode.SelectedIndex = 0
        txtReference.Text = ""
        ddlItemCode.SelectedIndex = 0
        '   lstSort2.Items.Clear()
        lblAlert.Text = ""
    End Sub
End Class
