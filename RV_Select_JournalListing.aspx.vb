
Imports System.Drawing
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel


Partial Class RV_Select_JournalListing
    Inherits System.Web.UI.Page

  

    Protected Sub btnPrintServiceRecordList_Click(sender As Object, e As EventArgs) Handles btnPrintServiceRecordList.Click
        If GetData() = True Then
            '   Session.Add("Type", "PrintPDF")
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                Response.Redirect("RV_JournalListing.aspx?Type=Summary")
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                Response.Redirect("RV_JournalListing.aspx?Type=Detail")
            ElseIf rbtnSelectDetSumm.SelectedValue = "3" Then
                Response.Redirect("RV_JournalListing.aspx?Type=DetailLedger")
            End If

        Else
            Return

        End If

    End Sub

  

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

    Private Function GetData() As Boolean
        lblAlert.Text = ""

        Dim selFormula As String
        Dim selection As String
        selection = ""
        selFormula = "{tbljrnv1.rcno} <> 0 "
        Dim qry As String = "SELECT "
        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            qry = qry + "tbljrnv.Location as Branch, "
        End If
        If rbtnSelectDetSumm.SelectedValue = "1" Then
            qry = qry + "tbljrnv.VoucherNumber, tbljrnv.GlPeriod, tbljrnv.PostStatus, tbljrnv.GlStatus, tbljrnv.JournalDate, tbljrnv.DebitBase, tbljrnv.CreditBase, tbljrnv.Comments FROM tbljrnv where tbljrnv.rcno<>0"
        ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
            qry = qry + "tbljrnvdet.LedgerCode, tbljrnv.VoucherNumber, tbljrnv.GlPeriod, tbljrnv.JournalDate,tbljrnvdet.AccountID, tbljrnv.Comments, tbljrnvdet.Sub, tbljrnvdet.RefType,replace(replace(tbljrnvdet.Description, char(10), ' '), char(13), ' ') as Description, tbljrnvdet.LedgerName,tbljrnvdet.DebitBase, tbljrnvdet.CreditBase, tblsales.GstBase"
            qry = qry + " FROM   tbljrnvdet LEFT OUTER JOIN tbljrnv ON tbljrnvdet.VoucherNumber=tbljrnv.VoucherNumber left join tblsales on tbljrnvdet.RefType=tblsales.InvoiceNumber where tbljrnv.rcno<>0"
        ElseIf rbtnSelectDetSumm.SelectedValue = "3" Then
            qry = qry + "tbljrnvdet.LedgerCode, tbljrnvdet.SubLedgerCode,tblchartofaccounts.Description, sum(tbljrnvdet.DebitBase) as DebitBase, sum(tbljrnvdet.CreditBase) as CreditBase FROM  tbljrnvdet LEFT OUTER JOIN tbljrnv ON tbljrnvdet.VoucherNumber=tbljrnv.VoucherNumber LEFT OUTER JOIN tblchartofaccounts ON tbljrnvdet.LedgerCode=tblchartofaccounts.COACode where tbljrnv.rcno<>0"

        End If

        If Convert.ToString(Session("LocationEnabled")) = "Y" Then
            selFormula = selFormula + " and {tbljrnv1.Location} in [" + Convert.ToString(Session("Branch")) + "]"
            qry = qry + " and tbljrnv.location in (" + Convert.ToString(Session("Branch")) + ")"

            If selection = "" Then
                selection = "Branch/Location : " + Convert.ToString(Session("Branch"))
            Else
                selection = selection + ", Branch/Location : " + Convert.ToString(Session("Branch"))
            End If
        End If
        'Dim qry As String = "SELECT tblsalesdetail.InvoiceNumber,tblsales.StaffCode, tblsales.SalesDate, tblsales.LedgerCode,tblsales.AccountId,tblsales.CustName,tblsales.BillAddress1, tblsales.BillBuilding, tblsales.BillStreet, tblsales.BillPostal,tblsales.BillCountry, "
        'If rbtnSelectDetSumm.SelectedValue = "1" Then 'detail
        '    qry = qry + " tblsalesdetail.SubCode, tblsalesdetail.RefType as ReferenceServiceRecord, replace(replace(tblsalesdetail.description, char(10), ' '), char(13), ' ') as Description, tblsalesdetail.ValueBase, tblsalesdetail.GstBase, "
        '    qry = qry + " tblsalesdetail.AppliedBase, tblsalesdetail.Gst, tblsalesdetail.GstRate, "

        'ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
        '    qry = qry + "sum(tblsalesdetail.ValueBase) as ValueBase, sum(tblsalesdetail.GstBase) as GstBase, sum(tblsalesdetail.AppliedBase) as AppliedBase, tblsales.Gst, tblsales.GstRate, "

        'End If
        'qry = qry + " tblsales.GlPeriod,replace(replace(tblsales.Comments, char(10), ' '), char(13), ' ') as Comments,tblsales.ValueBase+tblsales.GstBase-tblsales.creditBase-tblsales.receiptBase as Balance"
        'qry = qry + " FROM tblsales LEFT OUTER JOIN tblsalesdetail ON tblsales.InvoiceNumber=tblsalesdetail.InvoiceNumber where 1=1 AND RECURRINGINVOICE='N'"


        If String.IsNullOrEmpty(txtAcctPeriodFrom.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtAcctPeriodFrom.Text, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '  MessageBox.Message.Alert(Page, "Accounting Period From is invalid", "str")
                lblAlert.Text = "ACCOUNTING PERIOD FROM IS INVALID"
                Return False
            End If
            qry = qry + " and tbljrnv.glperiod >='" + d.ToString("yyyyMM") + "'"
            selFormula = selFormula + " and {tbljrnv1.glperiod} >='" + d.ToString("yyyyMM") + "'"
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
            qry = qry + " and tbljrnv.glperiod <='" + d.ToString("yyyyMM") + "'"

            selFormula = selFormula + " and {tbljrnv1.glperiod} <='" + d.ToString("yyyyMM") + "'"
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
                lblAlert.Text = "INVALID JOURNAL FROM DATE"
                Return False
            End If
            qry = qry + " and tbljrnv.Journaldate>= '" + Convert.ToDateTime(txtInvDateFrom.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tbljrnv1.JournalDate} >=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Journal Date >= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Journal Date >= " + d.ToString("dd-MM-yyyy")
            End If

        End If

        If String.IsNullOrEmpty(txtInvDateTo.Text) = False Then
            Dim d As DateTime
            If Date.TryParseExact(txtInvDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

            Else
                '   MessageBox.Message.Alert(Page, "Date To is invalid", "str")
                lblAlert.Text = "INVALID JOURNAL TO DATE"
                Return False
            End If
            qry = qry + " and tbljrnv.Journaldate <= '" + Convert.ToDateTime(txtInvDateTo.Text).ToString("yyyy-MM-dd") + "'"

            selFormula = selFormula + " and {tbljrnv1.JournalDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
            If selection = "" Then
                selection = "Journal Date <= " + d.ToString("dd-MM-yyyy")
            Else
                selection = selection + ", Journal Date <= " + d.ToString("dd-MM-yyyy")
            End If
        End If

        If rbtnSelectDetSumm.SelectedValue <> "1" Then
            If String.IsNullOrEmpty(txtGLFrom.Text) = False Then
                qry = qry + " and tbljrnvdet.ledgercode >= '" + txtGLFrom.Text + "'"
                selFormula = selFormula + " and {tbljrnvdet1.ledgercode} >= '" + txtGLFrom.Text + "'"
                If selection = "" Then
                    selection = "Ledger Code >= " + txtGLFrom.Text
                Else
                    selection = selection + ", Ledger Code >= " + txtGLFrom.Text
                End If
            End If

            If String.IsNullOrEmpty(txtGLTo.Text) = False Then
                qry = qry + " and tbljrnvdet.ledgercode <= '" + txtGLTo.Text + "'"

                selFormula = selFormula + " and {tbljrnvdet1.ledgercode} <= '" + txtGLTo.Text + "'"
                If selection = "" Then
                    selection = "Ledger Code <= " + txtGLTo.Text
                Else
                    selection = selection + ", Ledger Code <= " + txtGLTo.Text
                End If
            End If

        End If


        If String.IsNullOrEmpty(txtComments.Text) = False Then
            qry = qry + " and tbljrnv.Comments like '*" + txtComments.Text + "*'"
            selFormula = selFormula + " and {tbljrnv1.Comments} like '*" + txtComments.Text + "*'"
            If selection = "" Then
                selection = "Comments = " + txtComments.Text
            Else
                selection = selection + ", Comments = " + txtComments.Text
            End If
        End If


        If String.IsNullOrEmpty(txtGLStatus.Text) = False Then
            qry = qry + " and tbljrnv.GLStatus = '" + txtGLStatus.Text + "'"
            selFormula = selFormula + " and {tbljrnv1.GLStatus} = '" + txtGLStatus.Text + "'"
            If selection = "" Then
                selection = "GLStatus : " + txtGLStatus.Text
            Else
                selection = selection + ", GLStatus : " + txtGLStatus.Text
            End If
        End If

        If ddlStatus.Text = "-1" Then
        Else
            qry = qry + " and tbljrnv.PostStatus = '" + ddlStatus.Text + "'"
            selFormula = selFormula + " and {tbljrnv1.PostStatus} = '" + ddlStatus.Text + "'"
            If selection = "" Then
                selection = "Status : " + ddlStatus.Text
            Else
                selection = selection + ", Status : " + ddlStatus.Text
            End If
        End If

        If chkVoid.Checked = False Then
            qry = qry + " and tbljrnv.PostStatus <> 'V'"
            selFormula = selFormula + " and {tbljrnv1.PostStatus} <> 'V'"
            If selection = "" Then
                selection = "Status NOT 'V'"
            Else
                selection = selection + ", Status NOT 'V'"
            End If
        End If

        If rbtnSelectDetSumm.SelectedValue <> "1" Then

            If String.IsNullOrEmpty(txtReference.Text) = False Then
                qry = qry + " and tbljrnvdet.reftype like '" + txtReference.Text + "*'"
                selFormula = selFormula + " and {tbljrnvdet1.reftype} like '" + txtReference.Text + "*'"
                If selection = "" Then
                    selection = "Reference : " + txtReference.Text
                Else
                    selection = selection + ", Reference : " + txtReference.Text
                End If
            End If

            If ddlSubCode.Text = "-1" Then
            Else
                qry = qry + " and tbljrnvdet.SubCode = '" + ddlSubCode.Text + "'"
                selFormula = selFormula + " and {tbljrnvdet1.SubCode} = '" + ddlSubCode.Text + "'"
                If selection = "" Then
                    selection = "SubCode : " + ddlSubCode.Text
                Else
                    selection = selection + ", SubCode : " + ddlSubCode.Text
                End If
            End If
        End If


        Session.Add("selFormula", selFormula)
        Session.Add("selection", selection)

        If rbtnSelectDetSumm.SelectedValue = "2" Then 'summary
            ' qry = qry + " group by tbljrnvdet.vouchernumber"
        ElseIf rbtnSelectDetSumm.SelectedValue = "3" Then 'summary
            qry = qry + " group by tbljrnvdet.LedgerCode,tbljrnvdet.subLedgerCode"
        End If

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
        '        qry = qry + " ORDER BY tbljrnv.VoucherNUMBER"

        '    End If

        'End If

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

            Dim attachment As String = "attachment; filename=JournalListing.xls"
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
        txtGLFrom.Text = ""
        txtGLTo.Text = ""
       
        txtComments.Text = ""
     
        txtGLStatus.Text = ""
        ddlStatus.SelectedIndex = 0
        chkVoid.Checked = False

        ddlSubCode.SelectedIndex = 0
        txtReference.Text = ""


        lblAlert.Text = ""
    End Sub

    Protected Sub btnPopUpGLReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpGLReset.Click
         txtPopUpGL.Text = "Search Here for Ledger Code or Description"
        txtPopupGLSearch.Text = ""
        SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
        SqlDSGL.DataBind()
        GrdViewGL.DataBind()
        mdlPopupGLCode.Show()
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
            If rbtnSelectDetSumm.SelectedValue = "1" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 6 Or j = 7 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 11 Or j = 12 Or j = 13 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "3" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 4 Or j = 5 Then
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
                        If i = dt.Rows.Count - 1 Then
                            sheet1.AutoSizeColumn(j)
                        End If
                    Next
                Next

            End If

        Else

            If rbtnSelectDetSumm.SelectedValue = "1" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 5 Or j = 6 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "2" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 10 Or j = 11 Or j = 12 Then
                            If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                                Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                                cell.SetCellValue(d)
                            Else
                                Dim d As Double = Convert.ToDouble("0.00")
                                cell.SetCellValue(d)

                            End If
                            cell.CellStyle = _doubleCellStyle

                        ElseIf j = 3 Then
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
            ElseIf rbtnSelectDetSumm.SelectedValue = "3" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim row As IRow = sheet1.CreateRow(i + 2)

                    For j As Integer = 0 To dt.Columns.Count - 1
                        Dim cell As ICell = row.CreateCell(j)

                        If j = 3 Or j = 4 Then
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
            Dim attachment As String = "attachment; filename=JournalListing"


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
