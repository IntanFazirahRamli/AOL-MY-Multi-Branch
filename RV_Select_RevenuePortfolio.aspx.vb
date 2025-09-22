
Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_RevenuePortfolio
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


    'Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
    '    If GetData() = True Then
    '        '   Session.Add("Type", "PrintPDF")
    '        If rbtnSelectDetSumm.SelectedValue = "1" Then
    '            Response.Redirect("RV_SalesInvoiceByInvoiceNo_Detail.aspx")
    '        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
    '            Response.Redirect("RV_SalesInvoiceByInvoiceNo_Summary.aspx")
    '        End If

    '    Else
    '        Return

    '    End If

    'End Sub

    ''Protected Sub btnSvcBy_Click(sender As Object, e As ImageClickEventArgs) Handles btnSvcBy.Click
    ''    mdlPopUpTeam.TargetControlID = "btnSvcBy"
    ''    txtModal.Text = "btnSvcBy"
    ''    If String.IsNullOrEmpty(txtServiceBy.Text.Trim) = False Then
    ''        txtPopupTeamSearch.Text = txtServiceBy.Text.Trim
    ''        txtPopUpTeam.Text = txtPopupTeamSearch.Text
    ''        ' SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
    ''        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and (upper(TeamName) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(Inchargeid) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%' or upper(TeamID) Like '%" + txtPopupTeamSearch.Text.Trim.ToUpper + "%') and Status <> 'N' order by TeamName"
    ''        SqlDSTeam.DataBind()
    ''        gvTeam.DataBind()
    ''    Else
    ''        'txtPopUpTeam.Text = ""
    ''        'txtPopupTeamSearch.Text = ""
    ''        '   SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' and Status <> 'N'"
    ''        SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where rcno <>0 and Status <> 'N' order by TeamName"

    ''        SqlDSTeam.DataBind()
    ''        gvTeam.DataBind()
    ''    End If
    ''    mdlPopUpTeam.Show()
    ''End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'chkStatusSearch.Attributes.Add("onchange", "javascript: CheckBoxListSelect ('" & chkStatusSearch.ClientID & "');")


            ''''''''''''''''

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
            commandServiceRecordMasterSetup.CommandType = CommandType.Text
            commandServiceRecordMasterSetup.CommandText = "SELECT ShowInvoiceOnScreenLoad, InvoiceRecordMaxRec,DisplayRecordsLocationWise, PostInvoice, InvoiceOnlyEditableByCreator, DefaultTaxCode FROM tblservicerecordmastersetup"
            commandServiceRecordMasterSetup.Connection = conn

            Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
            Dim dtServiceRecordMasterSetup As New DataTable
            dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

            'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceRecordMaxRec")
            txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
            'txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostInvoice").ToString
            'txtOnlyEditableByCreator.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceOnlyEditableByCreator").ToString
            'txtDefaultTaxCode.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString

            '''''''''''''''''
        End If
    End Sub

    'Protected Sub btnGL1_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL1.Click
    '    mdlPopupGLCode.TargetControlID = "btnGL1"
    '    txtModal.Text = "GL1"

    '    If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
    '        txtPopUpGL.Text = txtGLFrom.Text
    '        txtPopupGLSearch.Text = txtPopUpGL.Text
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
    '        SqlDSGL.DataBind()
    '        GrdViewGL.DataBind()

    '    Else
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
    '        SqlDSGL.DataBind()
    '        GrdViewGL.DataBind()
    '    End If
    '    mdlPopupGLCode.Show()
    'End Sub

    'Protected Sub btnGL2_Click(sender As Object, e As ImageClickEventArgs) Handles btnGL2.Click
    '    mdlPopupGLCode.TargetControlID = "btnGL2"
    '    txtModal.Text = "GL2"

    '    If String.IsNullOrEmpty(txtGLFrom.Text.Trim) = False Then
    '        txtPopUpGL.Text = txtGLFrom.Text
    '        txtPopupGLSearch.Text = txtPopUpGL.Text
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
    '        SqlDSGL.DataBind()
    '        GrdViewGL.DataBind()

    '    Else
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
    '        SqlDSGL.DataBind()
    '        GrdViewGL.DataBind()
    '    End If
    '    mdlPopupGLCode.Show()
    'End Sub

    'Protected Sub GrdViewGL_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdViewGL.PageIndexChanging
    '    GrdViewGL.PageIndex = e.NewPageIndex
    '    If txtPopUpGL.Text.Trim = "" Then
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
    '    Else
    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
    '    End If

    '    SqlDSGL.DataBind()
    '    GrdViewGL.DataBind()
    '    mdlPopupGLCode.Show()
    'End Sub

    'Protected Sub GrdViewGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
    '    Try
    '        If txtModal.Text = "GL1" Then
    '            If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '                txtGLFrom.Text = " "
    '            Else
    '                txtGLFrom.Text = GrdViewGL.SelectedRow.Cells(1).Text
    '            End If
    '        ElseIf txtModal.Text = "GL2" Then
    '            If (GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '                txtGLTo.Text = " "
    '            Else
    '                txtGLTo.Text = GrdViewGL.SelectedRow.Cells(1).Text
    '            End If
    '        End If


    '        mdlPopupGLCode.Hide()
    '    Catch ex As Exception
    '        MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    'Protected Sub txtPopUpGL_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpGL.TextChanged
    '    If txtPopUpGL.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter ledger Code", "str")
    '    Else
    '        txtPopupGLSearch.Text = txtPopUpGL.Text

    '        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where (upper(COACode) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%' or upper(description) Like '%" + txtPopupGLSearch.Text.Trim.ToUpper + "%') order by COACode"
    '        SqlDSGL.DataBind()
    '        GrdViewGL.DataBind()
    '        ' txtIsPopUp.Text = "GL"
    '    End If
    '    txtPopUpGL.Text = "Search Here for Ledger Code or Description"
    'End Sub

    'Protected Sub btnPopUpClientReset_Click1(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
    '    txtPopUpGL.Text = "Search Here for Ledger Code or Description"
    '    txtPopupGLSearch.Text = ""
    '    SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
    '    SqlDSGL.DataBind()
    '    GrdViewGL.DataBind()
    '    mdlPopupGLCode.Show()
    'End Sub

    'Protected Sub OnRowDataBoundgGL(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
    '        'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
    '        'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
    '        'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
    '        'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GrdViewGL, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Click to select this row."
    '    End If
    'End Sub

    'Protected Sub OnSelectedIndexChangedgGL(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
    '    For Each row As GridViewRow In GrdViewGL.Rows
    '        If row.RowIndex = GrdViewGL.SelectedIndex Then
    '            row.BackColor = ColorTranslator.FromHtml("#738A9C")
    '            row.ToolTip = String.Empty
    '        Else
    '            row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
    '            row.ToolTip = "Click to select this row."
    '        End If
    '    Next
    'End Sub

    'Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
    '    If String.IsNullOrEmpty(txtPopupStaffSearch.Text.Trim) Then

    '        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"
    '    Else
    '        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"
    '    End If

    '    SqlDSStaffID.DataBind()
    '    gvStaff.DataBind()
    '    gvStaff.PageIndex = e.NewPageIndex

    '    mdlPopupStaff.Show()
    'End Sub

    'Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
    '    Try

    '        If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
    '            txtIncharge.Text = " "
    '        Else
    '            txtIncharge.Text = gvStaff.SelectedRow.Cells(1).Text
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    'Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
    '    mdlPopupStaff.Show()

    'End Sub

    'Protected Sub txtPopUpStaff_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpStaff.TextChanged
    '    If txtPopUpStaff.Text.Trim = "" Then
    '        MessageBox.Message.Alert(Page, "Please enter search text", "str")
    '    Else
    '        txtPopupStaffSearch.Text = txtPopUpStaff.Text
    '        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

    '        SqlDSStaffID.DataBind()
    '        gvStaff.DataBind()
    '        mdlPopupStaff.Show()
    '        ' txtIsPopUp.Text = "Staff"
    '    End If

    '    txtPopUpStaff.Text = "Search Here"
    'End Sub

    'Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
    '    txtPopUpStaff.Text = ""
    '    txtPopupStaffSearch.Text = ""
    '    SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

    '    SqlDSStaffID.DataBind()

    '    gvStaff.DataBind()
    '    mdlPopupStaff.Show()
    'End Sub

    'Protected Sub btnImgIncharge_Click(sender As Object, e As ImageClickEventArgs) Handles btnImgIncharge.Click
    '    If String.IsNullOrEmpty(txtIncharge.Text.Trim) = False Then
    '        txtPopUpStaff.Text = txtIncharge.Text
    '        txtPopupStaffSearch.Text = txtPopUpStaff.Text
    '        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

    '        SqlDSStaffID.DataBind()
    '        gvStaff.DataBind()
    '    Else
    '        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

    '        SqlDSStaffID.DataBind()
    '        gvStaff.DataBind()
    '    End If
    '    mdlPopupStaff.Show()

    'End Sub

    'Protected Sub OnRowDataBoundgStaff(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
    '        'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
    '        'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
    '        'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
    '        'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

    '        e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvStaff, "Select$" & e.Row.RowIndex)
    '        e.Row.ToolTip = "Click to select this row."
    '    End If
    'End Sub

    'Protected Sub OnSelectedIndexChangedgStaff(sender As Object, e As EventArgs)
    '    For Each row As GridViewRow In gvStaff.Rows
    '        If row.RowIndex = gvStaff.SelectedIndex Then
    '            row.BackColor = ColorTranslator.FromHtml("#738A9C")
    '            row.ToolTip = String.Empty
    '        Else
    '            row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
    '            row.ToolTip = "Click to select this row."
    '        End If
    '    Next
    'End Sub

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



    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        txtLocation.Text = ""

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            Dim d1 As DateTime

            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID FROM DATE"
                Return
            End If

            If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then

                If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d1) Then

                Else
                    '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                    lblAlert.Text = "INVALID TO DATE"
                    Return
                End If
            End If
       
        End If

        Dim criteria As String = ""
        Dim selection As String = ""

        Dim conn As MySqlConnection = New MySqlConnection()
        Dim cmd As MySqlCommand = New MySqlCommand
        Dim dt As New DataTable

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        Dim sda As New MySqlDataAdapter()

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 1200

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
          

        Else

            cmd.CommandText = "spRevenuePortfolioNew"
          
        End If

        cmd.Connection = conn

        cmd.Parameters.Clear()
              cmd.Parameters.AddWithValue("pr_CreatedBy", Session("UserID"))
       

        If String.IsNullOrEmpty(txtInvDateFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Invoice Date From is invalid", "str")
                lblAlert.Text = "INVALID FROM DATE"
                Return
            End If
            cmd.Parameters.AddWithValue("pr_startdate", Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd"))
            criteria = criteria + "_" + d.ToString("yyyyMMdd")
            selection = "Date >= " + d.ToString("dd-MM-yyyy")
        Else
            cmd.Parameters.AddWithValue("pr_startdate", DBNull.Value)

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID TO DATE"
                Return
            End If
            cmd.Parameters.AddWithValue("pr_enddate", Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd"))
            criteria = criteria + "-" + d.ToString("yyyyMMdd")
            If selection = "" Then
                selection = "Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Date <= " + d.ToString("dd-MM-yyyy")
            End If
        Else
            cmd.Parameters.AddWithValue("pr_enddate", DBNull.Value)

        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            txtLocation.Text = Convert.ToString(Session("Branch"))

            cmd.Parameters.AddWithValue("pr_Location", txtLocation.Text)
            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If

        Session.Add("selection", selection)

        '  cmd.ExecuteScalar()

        Try
            conn.Open()
            sda.SelectCommand = cmd

            InsertIntoTblWebEventLog("Excel1", txtInvDateFrom.Text, txtInvDateTo.Text)

            sda.Fill(dt)
            InsertIntoTblWebEventLog("Excel2", dt.Rows.Count.ToString, Session("UserID"))

        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            sda.Dispose()
            conn.Dispose()
        End Try
        InsertIntoTblWebEventLog("Excel3", dt.Rows.Count.ToString, Session("UserID"))

        WriteExcelWithNPOI(dt, "xlsx", criteria)
        InsertIntoTblWebEventLog("Excel4", criteria, Session("UserID"))

        Return

        '   Dim dt As DataTable = GetDataSet()
        Dim attachment As String = "attachment; filename=SalesByServicePeriod" + criteria + ".xls"
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


    End Sub

    Public Sub WriteExcelWithNPOI(ByVal dt As DataTable, ByVal extension As String, ByVal criteria As String)
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

        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 10)
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
            'InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

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

        Dim _intCellStyle As ICellStyle = Nothing

        If _intCellStyle Is Nothing Then
            _intCellStyle = workbook.CreateCellStyle()
            _intCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
            _intCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium

            _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0")
        End If

        Dim AllCellStyle As ICellStyle = Nothing

        AllCellStyle = workbook.CreateCellStyle()
        AllCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium
        AllCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium
        InsertIntoTblWebEventLog("ExcelNPOI1", dt.Rows.Count.ToString, Session("UserID"))

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
          
        Else
            InsertIntoTblWebEventLog("ExcelNPOI2", dt.Rows.Count.ToString, dt.Columns.Count.ToString)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As IRow = sheet1.CreateRow(i + 2)

                For j As Integer = 0 To dt.Columns.Count - 1
                    Dim cell As ICell = row.CreateCell(j)

                    ''If j >= 12 Then
                    If j = 3 Or j > 5 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Double = Convert.ToDouble("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _doubleCellStyle

                    ElseIf j = 4 Then
                        If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                            Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                            cell.SetCellValue(d)
                        Else
                            Dim d As Int32 = Convert.ToInt32("0")
                            cell.SetCellValue(d)

                        End If
                        cell.CellStyle = _intCellStyle
                    Else
                        cell.SetCellValue(dt.Rows(i)(j).ToString)
                        cell.CellStyle = AllCellStyle
                    End If
        If i = dt.Rows.Count - 1 Then
            sheet1.AutoSizeColumn(j)
        End If

                Next
        '   InsertIntoTblWebEventLog("ExcelNPOI3", i.ToString, Session("UserID"))

            Next

        End If

        InsertIntoTblWebEventLog("ExcelNPOI4", extension, criteria)



        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "RevenuePortfolio" + criteria + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "SalesByServicePeriod" + criteria + ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
        InsertIntoTblWebEventLog("ExcelNPOI5", extension, criteria)


    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'txtAcctPeriodFrom.Text = ""
        'txtAcctPeriodTo.Text = ""
        txtInvDateFrom.Text = ""
        txtInvDateTo.Text = ""
        'txtGLFrom.Text = ""
        'txtGLTo.Text = ""
        'txtInvoiceNoFrom.Text = ""
        'txtInvoiceNoTo.Text = ""
        'txtOurRef.Text = ""
        'txtYourRef.Text = ""
        'txtIncharge.Text = ""
        'txtPONo.Text = ""
        'txtComments.Text = ""
        'ddlCompanyGrp.SelectedIndex = 0
        'ddlAccountType.SelectedIndex = 0
        'txtAccountID.Text = ""
        'txtCustName.Text = ""
        'ddlLocateGrp.SelectedIndex = 0
        'ddlPaidStatus.SelectedIndex = 0
        'txtGLStatus.Text = ""
        'ddlStatus.SelectedIndex = 0
        'chkVoid.Checked = False
        'txtCostCode.Text = ""
        'ddlSubCode.SelectedIndex = 0
        'txtReference.Text = ""
        'ddlItemCode.SelectedIndex = 0
        'lstSort2.Items.Clear()
        'lblAlert.Text = ""
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
            insCmds.Parameters.AddWithValue("@LoginId", "EMAIL - " + Session("UserID"))
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
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, Session("UserID"))
        End Try
    End Sub
End Class

