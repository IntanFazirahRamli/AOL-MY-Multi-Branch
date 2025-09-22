Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel

Partial Class RV_Select_SalesInvoice
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
            '   Session.Add("Type", "PrintPDF")
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
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

        Dim selFormula As String
        Dim selection As String
        selFormula = ""

        Try
            lblAlert.Text = ""


            selection = ""
            selFormula = "{tblsalesdetail1.rcno} <> 0"

            Dim qry As String = ""

            If chkDistribution.Checked = True Then

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim cmd As MySqlCommand = New MySqlCommand
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                cmd.CommandTimeout = 2000

                'InsertIntoTblWebEventLog("GetData", Convert.ToString(Session("LocationEnabled")), "1")
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    cmd.CommandText = "spSalesListingDistributionLocation"
                Else
                    cmd.CommandText = "spSalesListingDistribution"

                End If

                cmd.Parameters.Clear()
                'InsertIntoTblWebEventLog("GetData", Session("UserID"), "3")
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    cmd.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))
                End If
                cmd.Parameters.AddWithValue("pr_CreatedBy", Session("UserID"))
                If rbtnSelectDetSumm.SelectedValue = "2" Then
                    cmd.Parameters.AddWithValue("pr_ReportName", "SalesListingSummary")
                ElseIf rbtnSelectDetSumm.SelectedValue = "1" Then
                    cmd.Parameters.AddWithValue("pr_ReportName", "SalesListingDetail")
                End If
                ' InsertIntoTblWebEventLog("GetData", rbtnSelectDetSumm.SelectedValue.ToString, "3")

                'cmd.Parameters.AddWithValue("pr_glperiodfrom", DBNull.Value)
                'cmd.Parameters.AddWithValue("pr_glperiodto", DBNull.Value)


                If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                        lblAlert.Text = "INVALID INVOICE FROM DATE"
                        Return False
                    End If
                    cmd.Parameters.AddWithValue("pr_startdate", Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd"))

                Else
                    cmd.Parameters.AddWithValue("pr_startdate", DBNull.Value)

                End If

                If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                        lblAlert.Text = "INVALID INVOICE TO DATE"
                        Return False
                    End If
                    cmd.Parameters.AddWithValue("pr_enddate", Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd"))

                Else
                    cmd.Parameters.AddWithValue("pr_enddate", DBNull.Value)

                End If

                'If Convert.ToString(Session("LocationEnabled")) = "Y" Then

                '    cmd.Parameters.AddWithValue("pr_Location", Convert.ToString(Session("Branch")))

                'End If

                cmd.ExecuteScalar()

                cmd.Dispose()

                conn.Close()
                conn.Dispose()
                If rbtnSelectDetSumm.SelectedValue = "1" Then

                    qry = "SELECT "
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "Location as Branch, "
                    End If
                    qry = qry + "InvoiceNumber, StaffCode,CompanyGroup, ContractGroup,BillingFrequency, SalesDate, CreditTerms, LedgerCode,"
                    qry = qry + "ContactType, AccountId,LocationID, CustName, BillAddress1, BillBuilding, BillStreet, BillPostal,"
                    qry = qry + "BillCountry, SubLedgerCode, RefType AS ServiceRecordNo, ServiceDate,ScheduleType, SourceInvoice, DetailDescription,"
                    qry = qry + "DetailLedgerCode, ServiceAddress, ValueBase, GstBase, ValueBase+GstBase as AppliedBase, GstCode, GSTRate,"
                    qry = qry + "Period, Description, Remarks, BalanceAmount, StaffDepartment, StaffLedgerCode,StaffSubLedgerCode"
                    qry = qry + " FROM tbwsaleslistingbyledger where createdby='" & Session("UserID") & "'"
                    qry = qry + "And ReportName = 'SalesListingDetail'"

                ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                    qry = "SELECT "
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "Location as Branch, "
                    End If
                    qry = qry + "InvoiceNumber, StaffCode,CompanyGroup,ContractGroup, SalesDate, CreditTerms, LedgerCode,"
                    qry = qry + "ContactType, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillPostal,"
                    qry = qry + "BillCountry,sum(ValueBase) as ValueBase,sum(GstBase) as GstBase,sum(ValueBase+GstBase) as AppliedBase,"
                    qry = qry + "GstCode, GstRate,Period,Description,Remarks,BalanceAmount"
                    qry = qry + " FROM tbwsaleslistingbyledger where createdby='" & Session("UserID") & "'"
                    qry = qry + "And ReportName = 'SalesListingSummary'"

                End If

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qry = qry + " and location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If


                If ddlInvoiceType.Text = "-1" Then
                Else
                    qry = qry + " and doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.doctype} = '" + ddlInvoiceType.SelectedValue.ToString + "'"
                    If selection = "" Then
                        selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    Else
                        selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
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

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.glperiod} >='" + d.ToString("yyyyMM") + "'"
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

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.glperiod} <='" + d.ToString("yyyyMM") + "'"
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

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                    If selection = "" Then
                        selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
                    Else
                        selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
                    End If
                    '  criteria = criteria + "_" + d.ToString("yyyyMMdd")
                End If

                If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                        lblAlert.Text = "INVALID INVOICE TO DATE"
                        Return False
                    End If
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"


                    If selection = "" Then
                        selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
                    Else
                        selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
                    End If


                End If


                If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
                    qry = qry + " and (ledgercode >= '" + txtGLFrom.Text + "'"
                    qry = qry + " or detailledgercode >= '" + txtGLFrom.Text + "')"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ledgercode} >= '" + txtGLFrom.Text + "'"
                    If selection = "" Then
                        selection = "Ledger Code >= " + txtGLFrom.Text
                    Else
                        selection = selection + ", Ledger Code >= " + txtGLFrom.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtGLTo.Text) = False Then
                    qry = qry + " and (ledgercode <= '" + txtGLTo.Text + "'"
                    qry = qry + " or detailledgercode <= '" + txtGLTo.Text + "')"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ledgercode} <= '" + txtGLTo.Text + "'"
                    If selection = "" Then
                        selection = "Ledger Code <= " + txtGLTo.Text
                    Else
                        selection = selection + ", Ledger Code <= " + txtGLTo.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtInvoiceNoFrom.Text) = False Then
                    qry = qry + " and InvoiceNumber >= '" + txtInvoiceNoFrom.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.InvoiceNumber} >= '" + txtInvoiceNoFrom.Text + "'"
                    If selection = "" Then
                        selection = "InvoiceNumber >= " + txtInvoiceNoFrom.Text
                    Else
                        selection = selection + ", InvoiceNumber >= " + txtInvoiceNoFrom.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtInvoiceNoTo.Text) = False Then
                    qry = qry + " and InvoiceNumber <= '" + txtInvoiceNoTo.Text + "'"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.InvoiceNumber} <= '" + txtInvoiceNoTo.Text + "'"
                    If selection = "" Then
                        selection = "InvoiceNumber <= " + txtInvoiceNoTo.Text
                    Else
                        selection = selection + ", InvoiceNumber <= " + txtInvoiceNoTo.Text
                    End If
                End If

                'If String.IsNullOrEmpty(txtOurRef.Text) = False Then
                '    qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

                '    selFormula = selFormula + " and {tblsales1.OurRef} like '" + txtOurRef.Text + "*'"
                '    If selection = "" Then
                '        selection = "OurRef = " + txtOurRef.Text
                '    Else
                '        selection = selection + ", OurRef = " + txtOurRef.Text
                '    End If
                'End If

                'If String.IsNullOrEmpty(txtYourRef.Text) = False Then
                '    qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
                '    selFormula = selFormula + " and {tblsales1.YourRef} like '" + txtYourRef.Text + "*'"
                '    If selection = "" Then
                '        selection = "YourRef = " + txtYourRef.Text
                '    Else
                '        selection = selection + ", YourRef = " + txtYourRef.Text
                '    End If
                'End If

                If String.IsNullOrEmpty(txtIncharge.Text) = False Then
                    qry = qry + " and StaffCode like '" + txtIncharge.Text + "*'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.StaffCode} like '" + txtIncharge.Text + "*'"
                    If selection = "" Then
                        selection = "Staff/Incharge = " + txtIncharge.Text
                    Else
                        selection = selection + ", Staff/Incharge = " + txtIncharge.Text
                    End If
                End If

                'If ddlContractGroup.Text = "-1" Then
                'Else
                '    qry = qry + " and contractgroup = '" + ddlContractGroup.Text + "'"
                '    selFormula = selFormula + " and {tbwsaleslistingbyledger1.contractgroup} = '" + ddlContractGroup.Text + "'"
                '    If selection = "" Then
                '        selection = "ContractGroup = " + ddlContractGroup.Text
                '    Else
                '        selection = selection + ", ContractGroup = " + ddlContractGroup.Text
                '    End If
                'End If

                'If String.IsNullOrEmpty(txtComments.Text) = False Then
                '    qry = qry + " and tblsales.Comments like '*" + txtComments.Text + "*'"
                '    selFormula = selFormula + " and {tblsales1.Comments} like '*" + txtComments.Text + "*'"
                '    If selection = "" Then
                '        selection = "Comments = " + txtComments.Text
                '    Else
                '        selection = selection + ", Comments = " + txtComments.Text
                '    End If
                'End If

                If ddlAccountType.Text = "-1" Then
                Else
                    qry = qry + " and ContactType = '" + ddlAccountType.SelectedItem.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ContactType} = '" + ddlAccountType.SelectedItem.Text + "'"
                    If selection = "" Then
                        selection = "AccountType : " + ddlAccountType.SelectedItem.Text
                    Else
                        selection = selection + ", AccountType : " + ddlAccountType.SelectedItem.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtAccountID.Text) = False Then
                    qry = qry + " and Accountid = '" + txtAccountID.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.Accountid} = '" + txtAccountID.Text + "'"
                    If selection = "" Then
                        selection = "AccountID : " + txtAccountID.Text
                    Else
                        selection = selection + ", AccountID : " + txtAccountID.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtCustName.Text) = False Then
                    qry = qry + " and CustName like '%" & txtCustName.Text.Replace("'", "''") & "%'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.CustName} like '*" + txtCustName.Text + "*'"
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
                    qry = qry + " and CompanyGroup in (" + YrStr + ")"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.CompanyGroup} in [" + YrStr + "]"
                    If selection = "" Then
                        selection = "CompanyGroup : " + YrStr
                    Else
                        selection = selection + ", CompanyGroup : " + YrStr
                    End If
                End If



                If ddlLocateGrp.Text = "-1" Then
                Else
                    qry = qry + " and LocateGrp = '" + ddlLocateGrp.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
                    If selection = "" Then
                        selection = "Location Group : " + ddlLocateGrp.Text
                    Else
                        selection = selection + ", Location Group : " + ddlLocateGrp.Text
                    End If
                End If

                'If ddlPaidStatus.Text = "-1" Then
                'Else
                '    If ddlPaidStatus.Text = "Y" Then
                '        qry = qry + " and balance = 0.00"

                '    ElseIf ddlPaidStatus.Text = "N" Then
                '        qry = qry + " and balance > 0.00"

                '    End If
                '    selFormula = selFormula + " and {@fPaidStatus} = '" + ddlPaidStatus.Text + "'"
                '    If selection = "" Then
                '        selection = "PaidStatus : " + ddlPaidStatus.Text
                '    Else
                '        selection = selection + ", PaidStatus : " + ddlPaidStatus.Text
                '    End If
                'End If


                'If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
                '    qry = qry + " and tblsales.GLStatus = '" + txtGLStatus.Text + "'"
                '    selFormula = selFormula + " and {tblsales1.GLStatus} = '" + txtGLStatus.Text + "'"
                '    If selection = "" Then
                '        selection = "GLStatus : " + txtGLStatus.Text
                '    Else
                '        selection = selection + ", GLStatus : " + txtGLStatus.Text
                '    End If
                'End If

                If ddlStatus.Text = "-1" Then
                Else
                    qry = qry + " and PostStatus = '" + ddlStatus.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.PostStatus} = '" + ddlStatus.Text + "'"
                    If selection = "" Then
                        selection = "Status : " + ddlStatus.Text
                    Else
                        selection = selection + ", Status : " + ddlStatus.Text
                    End If
                End If

                If chkVoid.Checked = False Then
                    qry = qry + " and PostStatus <> 'V'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.PostStatus} <> 'V'"
                    If selection = "" Then
                        selection = "Status NOT 'V'"
                    Else
                        selection = selection + ", Status NOT 'V'"
                    End If
                End If

                'If String.IsNullOrEmpty(txtCostCode.Text) = False Then
                '    qry = qry + " and tblsalesDETAIL.CostCode = '" + txtCostCode.Text + "'"
                '    selFormula = selFormula + " and {tblsalesDETAIL1.CostCode} = '" + txtCostCode.Text + "'"
                '    If selection = "" Then
                '        selection = "CostCode : " + txtCostCode.Text
                '    Else
                '        selection = selection + ", CostCode : " + txtCostCode.Text
                '    End If
                'End If

                'If ddlItemCode.Text = "-1" Then
                'Else
                '    qry = qry + " and tblsalesdetail.ItemCode = '" + ddlItemCode.SelectedValue.ToString + "'"
                '    selFormula = selFormula + " and {tblsalesdetail1.ItemCode} = '" + ddlItemCode.SelectedValue.ToString + "'"
                '    If selection = "" Then
                '        selection = "ItemCode : " + ddlItemCode.SelectedValue.ToString
                '    Else
                '        selection = selection + ", ItemCode : " + ddlItemCode.SelectedValue.ToString
                '    End If
                'End If


                'If String.IsNullOrEmpty(txtReference.Text) = False Then
                '    qry = qry + " and tblsalesDETAIL.reftype like '" + txtReference.Text + "*'"
                '    selFormula = selFormula + " and {tblsalesDETAIL1.reftype} like '" + txtReference.Text + "*'"
                '    If selection = "" Then
                '        selection = "Reference : " + txtReference.Text
                '    Else
                '        selection = selection + ", Reference : " + txtReference.Text
                '    End If
                'End If

                'If ddlSubCode.Text = "-1" Then
                'Else
                '    qry = qry + " and tblsalesDETAIL.SubCode = '" + ddlSubCode.Text + "'"
                '    selFormula = selFormula + " and {tblsalesdetail1.SubCode} = '" + ddlSubCode.Text + "'"
                '    If selection = "" Then
                '        selection = "SubCode : " + ddlSubCode.Text
                '    Else
                '        selection = selection + ", SubCode : " + ddlSubCode.Text
                '    End If
                'End If
                Session.Add("selFormula", selFormula)
                Session.Add("selection", selection)


                If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + " group by Invoicenumber"

                End If
                txtQuery.Text = qry

            Else

                qry = "SELECT "
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qry = qry + "tblsales.Location as Branch, "
                End If
                qry = qry + "tblsalesdetail.InvoiceNumber as DocumentNo,tblsales.StaffCode,tblsales.CompanyGroup,tblsales.ContractGroup,tblcontract.BillingFrequency,tblsales.SalesDate,tblsales.Terms as CreditTerms,tblsales.LedgerCode,if(tblsales.ContactType='COMPANY','CORPORATE','RESIDENTIAL') as ClientType,tblsales.AccountId,tblsales.CustName,tblsales.BillAddress1, tblsales.BillBuilding, tblsales.BillStreet, tblsales.BillPostal,tblsales.BillCountry, "

                If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                    qry = qry + " tblsalesdetail.SubCode, tblsalesdetail.RefType as ReferenceServiceRecord,tblservicerecord.ServiceDate,tblcontract.ScheduleType,tblsalesdetail.SourceInvoice,replace(replace(tblsalesdetail.description, char(10), ' '), char(13), ' ') as Description, tblsalesdetail.LedgerCode as DetailLedger,replace(replace(tblcontract.ServiceAddress, char(10), ' '), char(13), ' ') as ServiceAddress, tblsalesdetail.ValueBase, tblsalesdetail.GstBase, "
                    qry = qry + " tblsalesdetail.AppliedBase, tblsalesdetail.Gst, tblsalesdetail.GstRate, "
                    qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Description, char(10), ' '), char(13), ' ') as Description,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Remarks,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
                    qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber"
                    qry = qry + " LEFT OUTER JOIN tblcontract ON tblsalesdetail.CostCode=tblcontract.ContractNo "
                    qry = qry + " LEFT OUTER JOIN tblservicerecord ON tblsalesdetail.RefType=tblservicerecord.RecordNo where 1=1"

                ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + "sum(tblsalesdetail.ValueBase) as ValueBase,sum(tblsalesdetail.GstBase) as GstBase,sum(tblsalesdetail.AppliedBase) as AppliedBase,tblsales.Gst,tblsales.GstRate, "
                    qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Description, char(10), ' '), char(13), ' ') as Description,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Remarks,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
                    qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber where 1=1"

                End If

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblsales1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If

                If ddlInvoiceType.Text = "-1" Then
                Else
                    qry = qry + " and tblsales.doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"

                    selFormula = selFormula + " and {tblsales1.doctype} = '" + ddlInvoiceType.SelectedValue.ToString + "'"
                    If selection = "" Then
                        selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    Else
                        selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
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
                    qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
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
                    qry = qry + " and tblsalesDETAIL.reftype like '" + txtReference.Text + "*'"
                    selFormula = selFormula + " and {tblsalesDETAIL1.reftype} like '" + txtReference.Text + "*'"
                    If selection = "" Then
                        selection = "Reference : " + txtReference.Text
                    Else
                        selection = selection + ", Reference : " + txtReference.Text
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
                Session.Add("selFormula", selFormula)
                Session.Add("selection", selection)

                If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + " group by tblsalesdetail.invoicenumber"

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
                        qry = qry + " ORDER BY TBLSALESDETAIL.INVOICENUMBER"

                    End If

                End If
            End If


            txtQuery.Text = qry


            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)


                w.WriteLine(txtQuery.Text + vbLf & vbLf)
                w.WriteLine(selFormula.ToString + vbLf & vbLf)
            End Using

        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)

                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
                w.WriteLine(txtQuery.Text + vbLf & vbLf)
                w.WriteLine(selFormula.ToString + vbLf & vbLf)
            End Using
        End Try
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

    Private Function GetData2() As Boolean

        Try
            lblAlert.Text = ""


             Dim qry As String = "SELECT tblsalesdetail.Gst,sum(tblsalesdetail.ValueBase) as ValueBase,if(tblsalesdetail.GstRate=0,sum(tblsalesdetail.ValueBase),0) as NoTaxAmount,"
            qry = qry + "if(tblsalesdetail.GstRate>0,sum(tblsalesdetail.ValueBase),0) as TaxableAmount,sum(tblsalesdetail.GstBase) as GstBase,sum(tblsalesdetail.ValueBase+tblsalesdetail.GstBase) as Total "
                qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber where 1=1"

            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"
            End If

            If ddlInvoiceType.Text = "-1" Then
            Else
                qry = qry + " and tblsales.doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"
            End If

            If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else
                    '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                    lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                    Return False
                End If
                qry = qry + " and tblsales.glperiod >='" + d.ToString("yyyyMM") + "'"

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
            End If

            If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
                qry = qry + " and tblsalesdetail.ledgercode >= '" + txtGLFrom.Text + "'"
            End If

            If String.IsNullOrEmpty(txtGLTo.Text) = False Then
                qry = qry + " and tblsalesdetail.ledgercode <= '" + txtGLTo.Text + "'"

            End If

            If String.IsNullOrEmpty(txtInvoiceNoFrom.Text) = False Then
                qry = qry + " and tblsales.InvoiceNumber >= '" + txtInvoiceNoFrom.Text + "'"
               
            End If

            If String.IsNullOrEmpty(txtInvoiceNoTo.Text) = False Then
                qry = qry + " and tblsales.InvoiceNumber <= '" + txtInvoiceNoTo.Text + "'"

            End If

            If String.IsNullOrEmpty(txtOurRef.Text) = False Then
                qry = qry + " and tblsales.OurRef like '" + txtOurRef.Text + "*'"

            End If

            If String.IsNullOrEmpty(txtYourRef.Text) = False Then
                qry = qry + " and tblsales.YourRef like '" + txtYourRef.Text + "*'"
             
            End If

            If String.IsNullOrEmpty(txtIncharge.Text) = False Then
                qry = qry + " and tblsales.StaffCode like '" + txtIncharge.Text + "*'"
            
            End If

            If String.IsNullOrEmpty(txtPONo.Text) = False Then
                qry = qry + " and tblsales.PONo = '" + txtPONo.Text + "'"
             
            End If

            If String.IsNullOrEmpty(txtComments.Text) = False Then
                qry = qry + " and tblsales.Comments like '*" + txtComments.Text + "*'"
             
            End If

            If ddlAccountType.Text = "-1" Then
            Else
                qry = qry + " and tblsales.ContactType = '" + ddlAccountType.Text + "'"
            
            End If

            If String.IsNullOrEmpty(txtAccountID.Text) = False Then
                qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
             
            End If

            If String.IsNullOrEmpty(txtCustName.Text) = False Then
                qry = qry + " and tblsales.CustName like '%" + txtCustName.Text.Replace("'", "''") + "%'"
             
            End If

            Dim YrStrList1 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList1.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList1.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
             
            End If



            If ddlLocateGrp.Text = "-1" Then
            Else
                qry = qry + " and tblsales.LocateGrp = '" + ddlLocateGrp.Text + "'"
            
            End If

            If ddlPaidStatus.Text = "-1" Then
            Else
                If ddlPaidStatus.Text = "Y" Then
                    qry = qry + " and balance = 0.00"

                ElseIf ddlPaidStatus.Text = "N" Then
                    qry = qry + " and balance > 0.00"

                End If
            
            End If


            If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
                qry = qry + " and tblsales.GLStatus = '" + txtGLStatus.Text + "'"
            
            End If

            If ddlStatus.Text = "-1" Then
            Else
                qry = qry + " and tblsales.PostStatus = '" + ddlStatus.Text + "'"
             
            End If

            If chkVoid.Checked = False Then
                qry = qry + " and tblsales.PostStatus <> 'V'"
            
            End If

            If String.IsNullOrEmpty(txtCostCode.Text) = False Then
                qry = qry + " and tblsalesDETAIL.CostCode = '" + txtCostCode.Text + "'"
             
            End If

            If ddlItemCode.Text = "-1" Then
            Else
                qry = qry + " and tblsalesdetail.ItemCode = '" + ddlItemCode.SelectedValue.ToString + "'"
             
            End If


            If String.IsNullOrEmpty(txtReference.Text) = False Then
                qry = qry + " and tblsalesDETAIL.reftype like '" + txtReference.Text + "*'"
            
            End If

            If ddlSubCode.Text = "-1" Then
            Else
                qry = qry + " and tblsalesDETAIL.SubCode = '" + ddlSubCode.Text + "'"
            
            End If
            '   Session.Add("selFormula", selFormula)
            '   Session.Add("selection", selection)

      qry = qry + " group by tblsalesdetail.gst"


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
                    qry = qry + " ORDER BY TBLSALESDETAIL.INVOICENUMBER"

                End If

            End If
            txtQueryGst.Text = qry


            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)


                w.WriteLine(txtQueryGst.Text + vbLf & vbLf)

            End Using

        Catch ex As Exception
            Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            Using w As StreamWriter = File.AppendText(ErrOtLo)

                w.WriteLine(ex.Message.ToString + vbLf & vbLf)
                w.WriteLine(txtQueryGst.Text + vbLf & vbLf)

            End Using
        End Try
  
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
        cmd.CommandTimeout = 2000

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

    Private Function GetDataSet2() As DataTable
        Dim dt As New DataTable()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = txtQueryGst.Text

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

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Dim criteria As String = ""
        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID INVOICE FROM DATE"
                Return
            End If
            criteria = criteria + "_" + d.ToString("yyyyMMdd")

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID INVOICE TO DATE"
                Return
            End If
            criteria = criteria + "-" + d.ToString("yyyyMMdd")

        End If

        InsertIntoTblWebEventLog("ExportExcel", "1", "1")

        If GetData() = True Then
            InsertIntoTblWebEventLog("ExportExcel", txtQuery.Text, "2")

            'lblAlert.Text = txtQuery.Text
            'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
            'Return

            Dim dt As DataTable = GetDataSet()

            InsertIntoTblWebEventLog("ExportExcel", dt.Rows.Count.ToString, "3")

            WriteExcelWithNPOI(dt, "xlsx", criteria)

            InsertIntoTblWebEventLog("ExportExcel", "4", "4")

            Return

            Dim attachment As String = "attachment; filename=SalesInvoiceListing.xls"
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/vnd.ms-excel"
            Dim tab As String = ""
            Response.Write(Convert.ToString(Session("Selection")))
            Response.Write(vbLf)
            Response.Write(vbLf)
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
            '  Response.[End]()

            dt.Clear()

            If GetData2() = True Then
                dt = GetDataSet2()
                Response.Write(vbLf)
                Response.Write(vbLf)

                For Each dc As DataColumn In dt.Columns
                    Response.Write(dc.ColumnName + tab)
                    tab = vbTab
                Next
                Response.Write(vbLf)
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
        End If


    End Sub

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
        lstSort2.Items.Clear()
        lblAlert.Text = ""
    End Sub

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String, criteria As String)
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

        Dim _percentCellStyle As ICellStyle = Nothing

        If _percentCellStyle Is Nothing Then
            _percentCellStyle = workbook.CreateCellStyle()
            _percentCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _percentCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _percentCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00%")
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

        InsertIntoTblWebEventLog("ExcelNPOI", "1", "1")


        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '   If j = 21 Or j = 22 Or j = 23 Or j = 25 Or j = 29 Then
                        '   If j = 22 Or j = 23 Or j = 24 Or j = 26 Or j = 30 Then
                        '   If j = 24 Or j = 25 Or j = 26 Or j = 28 Or j = 32 Then
                        '  If j = 25 Or j = 26 Or j = 27 Or j = 29 Or j = 33 Then
                        If j = 26 Or j = 27 Or j = 28 Or j = 30 Or j = 34 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                            '  ElseIf j = 4 Or j = 16 Then
                            '  ElseIf j = 5 Or j = 18 Then
                        ElseIf j = 6 Or j = 20 Then
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

                        '  If j = 14 Or j = 15 Or j = 16 Or j = 18 Or j = 22 Then
                        ' If j = 15 Or j = 16 Or j = 17 Or j = 19 Or j = 23 Then
                        If j = 16 Or j = 17 Or j = 18 Or j = 20 Or j = 24 Then
                            ' If j = 17 Or j = 18 Or j = 19 Or j = 21 Or j = 25 Then
                            '   If j = 18 Or j = 19 Or j = 20 Or j = 22 Or j = 26 Then
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
        Else
            If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                InsertIntoTblWebEventLog("ExcelNPOI", "1", "2")
                If chkDistribution.Checked = True Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim row As IRow = sheet1.CreateRow(i + 2)

                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim cell As ICell = row.CreateCell(j)

                            'If j = 20 Or j = 21 Or j = 22 Or j = 24 Or j = 28 Then
                            '    If j = 21 Or j = 22 Or j = 23 Or j = 25 Or j = 29 Then
                            ' If j = 23 Or j = 24 Or j = 25 Or j = 27 Or j = 31 Then
                            ' If j = 24 Or j = 25 Or j = 26 Or j = 28 Or j = 32 Then
                            If j = 25 Or j = 26 Or j = 27 Or j = 29 Or j = 33 Then
                                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If

                                cell.CellStyle = _doubleCellStyle

                                ' ElseIf j = 3 Or j = 15 Then
                                ' ElseIf j = 4 Or j = 17 Then
                            ElseIf j = 5 Or j = 19 Then
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
                            '        If i = dt.Rows.Count - 1 Then
                            '            sheet1.AutoSizeColumn(j)
                            '        End If
                        Next
                    Next
                Else
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim row As IRow = sheet1.CreateRow(i + 2)

                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim cell As ICell = row.CreateCell(j)

                            'If j = 20 Or j = 21 Or j = 22 Or j = 24 Or j = 28 Then
                            '    If j = 21 Or j = 22 Or j = 23 Or j = 25 Or j = 29 Then
                            ' If j = 23 Or j = 24 Or j = 25 Or j = 27 Or j = 31 Then
                            If j = 24 Or j = 25 Or j = 26 Or j = 28 Or j = 32 Then
                                'If j = 25 Or j = 26 Or j = 27 Or j = 29 Or j = 33 Then
                                If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                    Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                    cell.SetCellValue(d)
                                Else
                                    Dim d As Double = Convert.ToDouble("0.00")
                                    cell.SetCellValue(d)
                                End If

                                cell.CellStyle = _doubleCellStyle

                                ' ElseIf j = 3 Or j = 15 Then
                                ' ElseIf j = 4 Or j = 17 Then
                            ElseIf j = 5 Or j = 18 Then
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
                            '        If i = dt.Rows.Count - 1 Then
                            '            sheet1.AutoSizeColumn(j)
                            '        End If
                        Next
                    Next
                End If
            


            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        '  If j = 13 Or j = 14 Or j = 15 Or j = 17 Or j = 21 Then
                        '      If j = 14 Or j = 15 Or j = 16 Or j = 18 Or j = 22 Then
                        If j = 15 Or j = 16 Or j = 17 Or j = 19 Or j = 23 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j = 4 Then
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


        dt.Clear()

        InsertIntoTblWebEventLog("ExcelNPOI", "2", "2")

        If GetData2() = True Then
            InsertIntoTblWebEventLog("ExcelNPOI", "3", "3")

            dt = GetDataSet2()

            InsertIntoTblWebEventLog("ExcelNPOI", "4", "4")

            Dim row2 As IRow = sheet1.CreateRow(sheet1.LastRowNum + 1)
            row2 = sheet1.CreateRow(sheet1.LastRowNum + 1)

            row2 = sheet1.CreateRow(sheet1.LastRowNum + 1)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row2.CreateCell(j)
                '  InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

                Dim columnName As String = dt.Columns(j).ToString()
                cell.SetCellValue(columnName)
                ' cell.Row.RowStyle.FillBackgroundColor = IndexedColors.LightBlue.Index
                cell.CellStyle = testeStyle

            Next

            InsertIntoTblWebEventLog("ExcelNPOI", "5", "5")


            Dim rownum As Integer = sheet1.LastRowNum
            For i As Integer = 0 To dt.Rows.Count - 1
                rownum = rownum + 1
                Dim row As IRow = sheet1.CreateRow(rownum)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    If j = 1 Or j = 2 Or j = 3 Or j = 4 Or j = 5 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0.00")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle


                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle
                    End If
                    'If i = dt.Rows.Count - 1 Then
                    '    sheet1.AutoSizeColumn(j)
                    'End If
                Next
            Next


        End If

        InsertIntoTblWebEventLog("ExcelNPOI", "6", "6")

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)


            If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
                criteria = "_" + txtInvDateFrom.Text + "_" + txtInvDateTo.Text
            End If

            If rbtnSelectDetSumm.SelectedValue = "1" Then
                criteria = criteria + "_Detail"
            Else
                criteria = criteria + "_Summary"
            End If

            criteria = criteria + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")

            Dim attachment As String = "attachment; filename=SalesInvoiceListing" + criteria


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

    Public Sub InsertIntoTblWebEventLog(events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", "SalesReport - " + Session("UserID"))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnEmailSalesInv_Click(sender As Object, e As EventArgs) Handles btnEmailSalesInv.Click
        Dim criteria As String = ""
        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID INVOICE FROM DATE"
                Return
            End If
            criteria = criteria + "_" + d.ToString("yyyyMMdd")

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID INVOICE TO DATE"
                Return
            End If
            criteria = criteria + "-" + d.ToString("yyyyMMdd")

        End If

        InsertIntoTblWebEventLog("ExportExcel", "1", "1")

        Dim selFormula As String
        Dim selection As String
        selFormula = ""

        Try
            lblAlert.Text = ""


            selection = ""
            selFormula = "{tblsalesdetail1.rcno} <> 0"

            Dim qry As String = ""

            If chkDistribution.Checked = True Then

                If rbtnSelectDetSumm.SelectedValue = "1" Then

                    qry = "SELECT "
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "Location as Branch, "
                    End If
                    qry = qry + "InvoiceNumber, StaffCode,CompanyGroup, ContractGroup,BillingFrequency, SalesDate, CreditTerms, LedgerCode,"
                    qry = qry + "ContactType, AccountId,LocationID, CustName, BillAddress1, BillBuilding, BillStreet, BillPostal,"
                    qry = qry + "BillCountry, SubLedgerCode, RefType AS ServiceRecordNo, ServiceDate,ScheduleType, SourceInvoice, DetailDescription,"
                    qry = qry + "DetailLedgerCode, ServiceAddress, ValueBase, GstBase, ValueBase+GstBase as AppliedBase, GstCode, GSTRate,"
                    qry = qry + "Period, Description, Remarks, BalanceAmount, StaffDepartment, StaffLedgerCode,StaffSubLedgerCode"
                    qry = qry + " FROM tbwsaleslistingbyledger where createdby='" & Session("UserID") & "'"
                    qry = qry + "And ReportName = 'SalesListingDetail'"

                ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                    qry = "SELECT "
                    If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                        qry = qry + "Location as Branch, "
                    End If
                    qry = qry + "InvoiceNumber, StaffCode,CompanyGroup,ContractGroup, SalesDate, CreditTerms, LedgerCode,"
                    qry = qry + "ContactType, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillPostal,"
                    qry = qry + "BillCountry,sum(ValueBase) as ValueBase,sum(GstBase) as GstBase,sum(ValueBase+GstBase) as AppliedBase,"
                    qry = qry + "GstCode, GstRate,Period,Description,Remarks,BalanceAmount"
                    qry = qry + " FROM tbwsaleslistingbyledger where createdby='" & Session("UserID") & "'"
                    qry = qry + "And ReportName = 'SalesListingSummary'"

                End If

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qry = qry + " and location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If


                If ddlInvoiceType.Text = "-1" Then
                Else
                    qry = qry + " and doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.doctype} = '" + ddlInvoiceType.SelectedValue.ToString + "'"
                    If selection = "" Then
                        selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    Else
                        selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    End If
                End If
                If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                        lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                        Return
                    End If

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.glperiod} >='" + d.ToString("yyyyMM") + "'"
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
                        Return
                    End If

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.glperiod} <='" + d.ToString("yyyyMM") + "'"
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
                        Return
                    End If

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.SalesDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
                    If selection = "" Then
                        selection = "Invoice Date >= " + d.ToString("dd-MM-yyyy")
                    Else
                        selection = selection + ", Invoice Date >= " + d.ToString("dd-MM-yyyy")
                    End If
                    '  criteria = criteria + "_" + d.ToString("yyyyMMdd")
                End If

                If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                        lblAlert.Text = "INVALID INVOICE TO DATE"
                        Return
                    End If
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.SalesDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"


                    If selection = "" Then
                        selection = "Invoice Date <= " + d.ToString("dd-MM-yyyy")
                    Else
                        selection = selection + ", Invoice Date <= " + d.ToString("dd-MM-yyyy")
                    End If


                End If


                If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
                    qry = qry + " and (ledgercode >= '" + txtGLFrom.Text + "'"
                    qry = qry + " or detailledgercode >= '" + txtGLFrom.Text + "')"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ledgercode} >= '" + txtGLFrom.Text + "'"
                    If selection = "" Then
                        selection = "Ledger Code >= " + txtGLFrom.Text
                    Else
                        selection = selection + ", Ledger Code >= " + txtGLFrom.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtGLTo.Text) = False Then
                    qry = qry + " and (ledgercode <= '" + txtGLTo.Text + "'"
                    qry = qry + " or detailledgercode <= '" + txtGLTo.Text + "')"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ledgercode} <= '" + txtGLTo.Text + "'"
                    If selection = "" Then
                        selection = "Ledger Code <= " + txtGLTo.Text
                    Else
                        selection = selection + ", Ledger Code <= " + txtGLTo.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtInvoiceNoFrom.Text) = False Then
                    qry = qry + " and InvoiceNumber >= '" + txtInvoiceNoFrom.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.InvoiceNumber} >= '" + txtInvoiceNoFrom.Text + "'"
                    If selection = "" Then
                        selection = "InvoiceNumber >= " + txtInvoiceNoFrom.Text
                    Else
                        selection = selection + ", InvoiceNumber >= " + txtInvoiceNoFrom.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtInvoiceNoTo.Text) = False Then
                    qry = qry + " and InvoiceNumber <= '" + txtInvoiceNoTo.Text + "'"

                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.InvoiceNumber} <= '" + txtInvoiceNoTo.Text + "'"
                    If selection = "" Then
                        selection = "InvoiceNumber <= " + txtInvoiceNoTo.Text
                    Else
                        selection = selection + ", InvoiceNumber <= " + txtInvoiceNoTo.Text
                    End If
                End If

             

                If String.IsNullOrEmpty(txtIncharge.Text) = False Then
                    qry = qry + " and StaffCode like '" + txtIncharge.Text + "*'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.StaffCode} like '" + txtIncharge.Text + "*'"
                    If selection = "" Then
                        selection = "Staff/Incharge = " + txtIncharge.Text
                    Else
                        selection = selection + ", Staff/Incharge = " + txtIncharge.Text
                    End If
                End If

                If ddlAccountType.Text = "-1" Then
                Else
                    qry = qry + " and ContactType = '" + ddlAccountType.SelectedItem.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.ContactType} = '" + ddlAccountType.SelectedItem.Text + "'"
                    If selection = "" Then
                        selection = "AccountType : " + ddlAccountType.SelectedItem.Text
                    Else
                        selection = selection + ", AccountType : " + ddlAccountType.SelectedItem.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtAccountID.Text) = False Then
                    qry = qry + " and Accountid = '" + txtAccountID.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.Accountid} = '" + txtAccountID.Text + "'"
                    If selection = "" Then
                        selection = "AccountID : " + txtAccountID.Text
                    Else
                        selection = selection + ", AccountID : " + txtAccountID.Text
                    End If
                End If

                If String.IsNullOrEmpty(txtCustName.Text) = False Then
                    qry = qry + " and CustName like '%" & txtCustName.Text.Replace("'", "''") & "%'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.CustName} like '*" + txtCustName.Text + "*'"
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
                    qry = qry + " and CompanyGroup in (" + YrStr + ")"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.CompanyGroup} in [" + YrStr + "]"
                    If selection = "" Then
                        selection = "CompanyGroup : " + YrStr
                    Else
                        selection = selection + ", CompanyGroup : " + YrStr
                    End If
                End If



                If ddlLocateGrp.Text = "-1" Then
                Else
                    qry = qry + " and LocateGrp = '" + ddlLocateGrp.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.LocateGrp} = '" + ddlLocateGrp.Text + "'"
                    If selection = "" Then
                        selection = "Location Group : " + ddlLocateGrp.Text
                    Else
                        selection = selection + ", Location Group : " + ddlLocateGrp.Text
                    End If
                End If

                If ddlStatus.Text = "-1" Then
                Else
                    qry = qry + " and PostStatus = '" + ddlStatus.Text + "'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.PostStatus} = '" + ddlStatus.Text + "'"
                    If selection = "" Then
                        selection = "Status : " + ddlStatus.Text
                    Else
                        selection = selection + ", Status : " + ddlStatus.Text
                    End If
                End If

                If chkVoid.Checked = False Then
                    qry = qry + " and PostStatus <> 'V'"
                    selFormula = selFormula + " and {tbwsaleslistingbyledger1.PostStatus} <> 'V'"
                    If selection = "" Then
                        selection = "Status NOT 'V'"
                    Else
                        selection = selection + ", Status NOT 'V'"
                    End If
                End If

                If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + " group by Invoicenumber"

                End If
                txtQuery.Text = qry

            Else

                qry = "SELECT "
                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    qry = qry + "tblsales.Location as Branch, "
                End If
                qry = qry + "tblsalesdetail.InvoiceNumber as DocumentNo,tblsales.StaffCode,tblsales.CompanyGroup,tblsales.ContractGroup,tblcontract.BillingFrequency,tblsales.SalesDate,tblsales.Terms as CreditTerms,tblsales.LedgerCode,if(tblsales.ContactType='COMPANY','CORPORATE','RESIDENTIAL') as ClientType,tblsales.AccountId,tblsales.CustName,tblsales.BillAddress1, tblsales.BillBuilding, tblsales.BillStreet, tblsales.BillPostal,tblsales.BillCountry, "

                If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
                    qry = qry + " tblsalesdetail.SubCode, tblsalesdetail.RefType as ReferenceServiceRecord,tblservicerecord.ServiceDate,tblcontract.ScheduleType,tblsalesdetail.SourceInvoice,replace(replace(tblsalesdetail.description, char(10), ' '), char(13), ' ') as Description, tblsalesdetail.LedgerCode as DetailLedger,replace(replace(tblcontract.ServiceAddress, char(10), ' '), char(13), ' ') as ServiceAddress, tblsalesdetail.ValueBase, tblsalesdetail.GstBase, "
                    qry = qry + " tblsalesdetail.AppliedBase, tblsalesdetail.Gst, tblsalesdetail.GstRate, "
                    qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Description, char(10), ' '), char(13), ' ') as Description,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Remarks,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
                    qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber"
                    qry = qry + " LEFT OUTER JOIN tblcontract ON tblsalesdetail.CostCode=tblcontract.ContractNo "
                    qry = qry + " LEFT OUTER JOIN tblservicerecord ON tblsalesdetail.RefType=tblservicerecord.RecordNo where 1=1"

                ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + "sum(tblsalesdetail.ValueBase) as ValueBase,sum(tblsalesdetail.GstBase) as GstBase,sum(tblsalesdetail.AppliedBase) as AppliedBase,tblsales.Gst,tblsales.GstRate, "
                    qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Description, char(10), ' '), char(13), ' ') as Description,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Remarks,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
                    qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber where 1=1"

                End If

                If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                    selFormula = selFormula + " and {tblsales1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
                    qry = qry + " and tblsales.location in (" + Convert.ToString(Session("Branch")) + ")"

                    If selection = "" Then
                        selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
                    Else
                        selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
                    End If
                End If

                If ddlInvoiceType.Text = "-1" Then
                Else
                    qry = qry + " and tblsales.doctype = '" + ddlInvoiceType.SelectedValue.ToString + "'"

                    selFormula = selFormula + " and {tblsales1.doctype} = '" + ddlInvoiceType.SelectedValue.ToString + "'"
                    If selection = "" Then
                        selection = "Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    Else
                        selection = selection + ", Invoice Type : " + ddlInvoiceType.SelectedValue.ToString
                    End If
                End If
                If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
                    Dim d As DateTime
                    If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                    Else
                        '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                        lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                        Return
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
                        Return
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
                        Return
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
                        Return
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

         
                Dim YrStrList1 As List(Of [String]) = New List(Of String)()

                For Each item As ListItem In ddlCompanyGrp.Items
                    If item.Selected Then

                        YrStrList1.Add("""" + item.Value + """")

                    End If
                Next

                If YrStrList1.Count > 0 Then

                    Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                    qry = qry + " and tblsales.CompanyGroup in (" + YrStr + ")"
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
                    qry = qry + " and tblsalesDETAIL.reftype like '" + txtReference.Text + "*'"
                    selFormula = selFormula + " and {tblsalesDETAIL1.reftype} like '" + txtReference.Text + "*'"
                    If selection = "" Then
                        selection = "Reference : " + txtReference.Text
                    Else
                        selection = selection + ", Reference : " + txtReference.Text
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
          
                If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
                    qry = qry + " group by tblsalesdetail.invoicenumber"

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
                        qry = qry + " ORDER BY TBLSALESDETAIL.INVOICENUMBER"

                    End If

                End If
            End If


            txtQuery.Text = qry

         
            Session.Add("selFormula", selFormula)
            Session.Add("selection", selection)

            GetData2()

            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry1 As String = "INSERT INTO tblautoemailreport(AccountIDFrom,AccountIDTo,DocumentType,CreatedBy,CreatedOn,"
            qry1 = qry1 + "Module,Generated,BatchNo,FileType,Selection,Selformula,RetryCount,DomainName,Distribution,ContractGroup,"
            qry1 = qry1 + "Branch,CutOffDate,"
            qry1 = qry1 + "InvoiceType,Location,PeriodFrom,PeriodTo,InvDateFrom,InvDateTo,DueDateFrom,DueDateTo,LedgerCodeFrom,"
            qry1 = qry1 + "LedgerCodeTo,Incharge,SalesMan,AccountType,AccountName,CompanyGroup,LocateGrp,Terms,GLStatus,"
            qry1 = qry1 + "ReportFormat,IncludeCompanyInfo,IncludeRecInfo,PrintDate,IncludeUnPaidBal,Query,Query2)"
            qry1 = qry1 + "VALUES(@AccountIDFrom,@AccountIDTo,@DocumentType,@CreatedBy,@CreatedOn,@Module,@Generated,"
            qry1 = qry1 + "@BatchNo,@FileType,@Selection,@Selformula,@RetryCount,@DomainName,@Distribution,@ContractGroup,@Branch,"
            qry1 = qry1 + "@CutOffDate,@InvoiceType,@Location,@PeriodFrom,@PeriodTo,@InvDateFrom,@InvDateTo,@DueDateFrom,"
            qry1 = qry1 + "@DueDateTo,@LedgerCodeFrom,@LedgerCodeTo,@Incharge,@SalesMan,@AccountType,@AccountName,@CompanyGroup,"
            qry1 = qry1 + "@LocateGrp,@Terms,@GLStatus,@ReportFormat,@IncludeCompanyInfo,@IncludeRecInfo,@PrintDate,"
            qry1 = qry1 + "@IncludeUnPaidBal,@Query,@Query2);"

            command.CommandText = qry1
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@AccountIDFrom", "")
            command.Parameters.AddWithValue("@AccountIDTo", "")

            If rbtnSelectDetSumm.SelectedValue = 1 Then
                command.Parameters.AddWithValue("@DocumentType", "SLSINVDET")

            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                command.Parameters.AddWithValue("@DocumentType", "SLSINVSUMM")

            End If
         

            command.Parameters.AddWithValue("@Module", "SLSINV")
            command.Parameters.AddWithValue("@Generated", 0)
            command.Parameters.AddWithValue("@BatchNo", Session("UserID").ToString + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))


            command.Parameters.AddWithValue("@FileType", "EXCEL")

            command.Parameters.AddWithValue("@Selection", Session("Selection"))

            command.Parameters.AddWithValue("@SelFormula", Session("SelFormula"))
            command.Parameters.AddWithValue("@RetryCount", 0)

            command.Parameters.AddWithValue("@DomainName", ConfigurationManager.AppSettings("DomainName").ToString())
            If chkDistribution.Checked Then
                command.Parameters.AddWithValue("@Distribution", True)

            Else
                command.Parameters.AddWithValue("@Distribution", False)

            End If
         
            If Convert.ToString(Session("LocationEnabled")) = "Y" Then
                command.Parameters.AddWithValue("@Branch", Convert.ToString(Session("Branch")))

                command.Parameters.AddWithValue("@Location", "")

            Else
                command.Parameters.AddWithValue("@Branch", "")
                command.Parameters.AddWithValue("@Location", "")
            End If

            command.Parameters.AddWithValue("@InvoiceType", "")


            command.Parameters.AddWithValue("@PeriodFrom", "")

            command.Parameters.AddWithValue("@PeriodTo", "")
            If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else

                End If
                command.Parameters.AddWithValue("@InvDateFrom", d.ToString("yyyy-MM-dd"))
            Else
                command.Parameters.AddWithValue("@InvDateFrom", "")

            End If

            If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
                Dim d As DateTime
                If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else

                End If
                command.Parameters.AddWithValue("@InvDateTo", d.ToString("yyyy-MM-dd"))
            Else
                command.Parameters.AddWithValue("@InvDateTo", "")

            End If


            command.Parameters.AddWithValue("@DueDateFrom", "")

            command.Parameters.AddWithValue("@DueDateTo", "")

            command.Parameters.AddWithValue("@LedgerCodeFrom", "")

            command.Parameters.AddWithValue("@LedgerCodeTo", "")

            If String.IsNullOrEmpty(txtIncharge.Text) = False Then
                command.Parameters.AddWithValue("@Incharge", txtIncharge.Text)
            Else
                command.Parameters.AddWithValue("@Incharge", "")
            End If

                 command.Parameters.AddWithValue("@SalesMan", "")
         

            If ddlAccountType.Text = "-1" Then
                command.Parameters.AddWithValue("@AccountType", "")
            Else
                command.Parameters.AddWithValue("@AccountType", ddlAccountType.Text)
            End If


            If String.IsNullOrEmpty(txtCustName.Text) = False Then
                command.Parameters.AddWithValue("@AccountName", txtCustName.Text)
            Else
                command.Parameters.AddWithValue("@AccountName", "")
            End If

            Dim YrStrList2 As List(Of [String]) = New List(Of String)()

            For Each item As ListItem In ddlCompanyGrp.Items
                If item.Selected Then

                    YrStrList2.Add("""" + item.Value + """")

                End If
            Next

            If YrStrList2.Count > 0 Then

                Dim YrStr As [String] = [String].Join(",", YrStrList2.ToArray)
                command.Parameters.AddWithValue("@CompanyGroup", YrStr)
            Else
                command.Parameters.AddWithValue("@CompanyGroup", "")
            End If

            If ddlLocateGrp.Text = "-1" Then
                command.Parameters.AddWithValue("@LocateGrp", "")
            Else
                command.Parameters.AddWithValue("@LocateGrp", ddlLocateGrp.Text)
            End If

            command.Parameters.AddWithValue("@Terms", "")

            command.Parameters.AddWithValue("@GLStatus", "")


            If String.IsNullOrEmpty(Convert.ToString(Session("SysDate"))) Then
                command.Parameters.AddWithValue("@PrintDate", "")
            Else

                Dim d As DateTime
                If Date.TryParseExact(Convert.ToString(Session("SysDate")), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

                Else

                End If
                command.Parameters.AddWithValue("@PrintDate", d.ToString("yyyy-MM-dd"))
            End If



            command.Parameters.AddWithValue("@IncludeUnpaidBal", False)


                command.Parameters.AddWithValue("@CutOffDate", "")
         


            command.Parameters.AddWithValue("@IncludeCompanyInfo", False)

            command.Parameters.AddWithValue("@IncludeRecInfo", False)


            command.Parameters.AddWithValue("@ContractGroup", "")
       

            If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
                command.Parameters.AddWithValue("@ReportFormat", "SLSINVDET")
            ElseIf rbtnSelectDetSumm.SelectedValue.ToString = "2" Then
                command.Parameters.AddWithValue("@ReportFormat", "SLSINVSUMM")
            End If

            command.Parameters.AddWithValue("@Query", txtQuery.Text)
            command.Parameters.AddWithValue("@Query2", txtQueryGst.Text)

            command.Connection = conn

            command.ExecuteNonQuery()

            command.Dispose()
            conn.Close()
            conn.Dispose()

            mdlPopupMsg.Show()
        Catch ex As Exception

        End Try


    End Sub

   
      
End Class
