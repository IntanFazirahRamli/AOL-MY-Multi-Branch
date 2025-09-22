Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Drawing
Imports System.IO

' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading

Partial Class AdjustmentNote
    Inherits System.Web.UI.Page
    Public rcno As String
    Private Shared GridSelected As String = String.Empty
    Private Shared gScheduler, gddlvalue As String
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Dim client As String
    'Public rcno As String

    Public TotDetailRecords As Integer

    Dim gSeq As String
    Dim gServiceDate As Date

    Dim rowdeleted As String
    Dim RowNumber As Integer
    Dim RowIndexSch As Integer

    Dim mode As String
    Dim vStrStatus As String

    Public HasDuplicateTarget As Boolean
    Public HasDuplicateLocaion As Boolean
    Public HasDuplicateServices As Boolean
    Public HasDuplicateFrequency As Boolean
    Public xgrvBillingDetails As GridViewRow
    Public sqltext As String


    Public lGLCode As String
    Public lGLDescription As String
    Public lCreditAmount, lDebitAmount As Decimal

    Public IsSuccess As Boolean
    Dim gtotal As Double = 0

    Public Message As String = String.Empty
    ' To store the Error or Message

    Private Word As Microsoft.Office.Interop.Word.ApplicationClass
    ' The Interop Object for Word
    Private Excel As Microsoft.Office.Interop.Excel.ApplicationClass
    ' The Interop Object for Excel
    Private Unknown As Object = Type.Missing
    ' For passing Empty values
    Public Enum StatusType
        SUCCESS
        FAILED
    End Enum
    ' To Specify Success or Failure Types
    Public Status As StatusType
    Shared random As New Random()

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim Query As String

        'Restrict users manual date entries

        'txtAccountIdBilling.Attributes.Add("readonly", "readonly")
        Try
            'txtAccountName.Attributes.Add("readonly", "readonly")
            'txtContactPerson.Attributes.Add("readonly", "readonly")

            'txtBillAddress.Attributes.Add("readonly", "readonly")
            'txtBillBuilding.Attributes.Add("readonly", "readonly")
            'txtBillStreet.Attributes.Add("readonly", "readonly")
            'txtBillCountry.Attributes.Add("readonly", "readonly")
            'txtBillPostal.Attributes.Add("readonly", "readonly")
            'txtTotal.Attributes.Add("readonly", "readonly")
            'txtTaxRatePct.Attributes.Add("readonly", "readonly")

            txtCNNo.Attributes.Add("readonly", "readonly")
            txtReceiptPeriod.Attributes.Add("readonly", "readonly")
            txtCompanyGroup.Attributes.Add("readonly", "readonly")
            'ddlContactType.Attributes.Add("readonly", "readonly")
            'txtSalesman.Attributes.Add("readonly", "readonly")
            'txtBankGLCode.Attributes.Add("readonly", "readonly")
            'txtChequeNo.Attributes.Add("readonly", "readonly")
            'txtChequeDate.Attributes.Add("readonly", "readonly")

            'txtInvoiceAmount.Attributes.Add("readonly", "readonly")
            'txtDiscountAmount.Attributes.Add("readonly", "readonly")
            'txtAmountWithDiscount.Attributes.Add("readonly", "readonly")

            'txtGSTAmount.Attributes.Add("readonly", "readonly")
            'txtNetAmount.Attributes.Add("readonly", "readonly")

            'txtCreatedOn.Attributes.Add("readonly", "readonly")
            'txtServTimeOut.Attributes.Add("onchange", "getTheDiffTime()")
            'txtCreditDays.Attributes.Add("readonly", "readonly")

            txtCNDate.Attributes.Add("readonly", "readonly")

            txtCreditAmount.Attributes.Add("readonly", "readonly")
            txtDebitAmount.Attributes.Add("readonly", "readonly")

            btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")

            If Not Page.IsPostBack Then
                mdlPopUpClient.Hide()
                mdlImportInvoices.Hide()
                'mdlImportServices.Hide()
                mdlPopupGL.Hide()

                txtGroupAuthority.Text = Session("SecGroupAuthority")

                Dim Query As String
                Query = "Select StaffID from tblStaff where Status = 'O' and Roles <> 'TECHNICAL'"
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchEditedBy)
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchCreatedBy)

                tb1.ActiveTabIndex = 0


                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim sql As String
                sql = ""
                sql = "Select TaxRatePct from tbltaxtype where TaxType = 'SR'"

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = sql
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()

                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("TaxRatePct").ToString <> "" Then : txtTaxRatePct.Text = dt.Rows(0)("TaxRatePct").ToString : End If
                End If


                sql = ""
                sql = "Select COACode, COADescription from tblbillingproducts where ProductCode = 'CN-REVENUE'"

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text
                command2.CommandText = sql
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("COACode").ToString <> "" Then : txtGLCodeIS.Text = dt2.Rows(0)("COACode").ToString : End If
                    If dt2.Rows(0)("COACode").ToString <> "" Then : txtGLCodeII.Text = dt2.Rows(0)("COACode").ToString : End If

                    If dt2.Rows(0)("COADescription").ToString <> "" Then : txtLedgerNameIS.Text = dt2.Rows(0)("COADescription").ToString : End If
                    If dt2.Rows(0)("COADescription").ToString <> "" Then : txtLedgerNameII.Text = dt2.Rows(0)("COADescription").ToString : End If
                End If

                ''''''''''''''''''''''''''''''''''''''''''''''''
                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                commandServiceRecordMasterSetup.CommandText = "SELECT ShowJournalOnScreenLoad, JournalRecordMaxRec,DisplayRecordsLocationWise,PostJournal, JournalOnlyEditableByCreator FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("JournalRecordMaxRec")
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostJournal").ToString
                txtOnlyEditableByCreator.Text = dtServiceRecordMasterSetup.Rows(0)("JournalOnlyEditableByCreator").ToString

                conn.Close()
                conn.Dispose()
                ''''''''''''''''''''''''''''''''''''''''''
                'conn.Close()

                MakeMeNull()
                DisableControls()
                PopulateArCode()
                txt.Text = SQLDSCN.SelectCommand

                txtSearch1Status.Text = "O,P"

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    Label58.Visible = True
                    txtLocation.Visible = True
                Else
                    Label58.Visible = False
                    txtLocation.Visible = False
                End If
                '''''''''''''''''''''''
                'Session.Add("customerfrom", Request.QueryString("CustomerFrom"))
                'If String.IsNullOrEmpty(Session("customerfrom")) = False Then
                '    Session.Add("invoiceno", Request.QueryString("VoucherNumber"))
                '    If Request.QueryString("VoucherNumber") <> "" Then
                '        txtReceiptnoSearch.Text = Session("invoiceno")
                '        txtFrom.Text = Session("customerfrom")

                '        btnQuickSearch_Click(sender, e)
                '        Session.Remove("invoiceno")
                '        Session.Remove("customerfrom")

                '        btnQuit.Text = "BACK"
                '        ''''' Retrieve rcno for the Invoice 

                '        ''''' Retrieve rcno for the Invoice 
                '        GridView1_SelectedIndexChanged(New Object(), New EventArgs)

                '    End If
                '    Exit Sub
                'End If


                ''''''''''''''''''''''''''


                'If Session("receiptfrom") = "invoice" Then
                'txtInvoiceNoSearch.Text = Session("invoiceno")
                'sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.CNNumber, A.CNDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.CustomerName,  A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno FROM tblCN A, tblCNdet B where A.CNNumber = B.CNNumber and B.InvoiceNo = '" & txtReceiptnoSearch.Text & "' ORDER BY Rcno DESC, CustomerName"
                'SQLDSCN.SelectCommand = sqltext
                'btnBack.Visible = True
                'btnQuit.Visible = False
                'Else
                'sqltext = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule, LedgerCode, LedgerName FROM tblsales WHERE (DocType='ARCN' or DocType='ARDN')  AND (PostStatus = 'O' OR PostStatus = 'P') and  GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) ORDER BY Rcno DESC, CustName;"
                'SQLDSCN.SelectCommand = sqltext
                If Convert.ToBoolean(dtServiceRecordMasterSetup.Rows(0)("ShowJournalOnScreenLoad")) = False Then
                    txt.Text = ""
                    SQLDSCN.SelectCommand = ""
                    GridView1.DataSourceID = "SQLDSCN"
                    GridView1.DataBind()
                Else
                    txt.Text = "SELECT * FROM tbljrnv  WHERE 1=1  "
                    txtCondition.Text = " and  (PostStatus = 'O' OR PostStatus = 'P') and  GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) "

                    If txtDisplayRecordsLocationwise.Text = "Y" Then
                        'txtCondition.Text = txtCondition.Text & " and Location = '" & txtLocation.Text & "'"
                        txtCondition.Text = txtCondition.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

                    End If

                    txtOrderBy.Text = " ORDER BY Rcno DESC "

                    txt.Text = txt.Text + txtCondition.Text + txtOrderBy.Text
                    sqltext = txt.Text

                    SQLDSCN.SelectCommand = sqltext
                    CalculateTotal()
                End If

                'End If

                SQLDSCN.DataBind()
                GridView1.DataBind()

                txtLocation.Attributes.Add("disabled", "true")
                txtCreatedBy.Text = Session("userid")
                'FindLocation()

                'updPnlBillingRecs.Update()
            Else
                If txtIsPopup.Text = "Team" Then
                    txtIsPopup.Text = "N"
                    'mdlPopUpTeam.Show()
                ElseIf txtIsPopup.Text = "Client" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpClient.Show()
                End If

                'If txtSearch.Text = "ImportService" Then
                '    mdlPopUpClient.Show()
                'End If

                'If txtIsPopup.Text = "ContractNo" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpContractNo.Show()
                'End If

                If txtSearch.Text = "ImportInvoice" Then
                    mdlPopUpClient.Show()
                End If

                If txtIsPopup.Text = "GL" Then
                    txtIsPopup.Text = "N"
                    mdlPopupGL.Show()
                    'Dim TextBoxItemCode1 As mo As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                    'mdl mdlImportServices.Show()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub CalculateTotal()
        Try
            Dim sqlstr As String
            sqlstr = ""

            sqlstr = "SELECT ifnull(Sum(b.DebitBase),0) as totalamount FROM tbljrnv a, tbljrnvDet b  where a.VoucherNumber = b.VoucherNumber and 1=1 " + txtCondition.Text

            Dim command As MySqlCommand = New MySqlCommand

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = sqlstr
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)
            'Convert.ToDecimal(tot.ToString()).ToString("N2")
            If dt.Rows.Count > 0 Then
                txtTotalInvoiceAmount.Text = Convert.ToDecimal(dt.Rows(0)("totalamount").ToString()).ToString("N2")
            End If
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "CalculateTotal", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    If e.Row.Cells(9).Text = "COMPANY" Then
            '        e.Row.Cells(9).Text = "CORPORATE"
            '    Else
            '        e.Row.Cells(9).Text = "RESIDENTIAL"
            '    End If
            'End If


            'gtotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "debitbase"))
            'txtTotalInvoiceAmount.Text = gtotal
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "CalculateTotal", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo


        lblAlert.Text = ""
        lblMessage.Text = ""


        If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN ADD/EDIT MODE. SAVE OR CANCEL TO PROCEED"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        Try
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            txtMode.Text = "View"

            'grvBillingDetails.ShowHeader = False
            'grvBillingDetails.Visible = False

            'btnSvcEdit.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcEdit.Enabled = False
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.Enabled = False
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            Dim editindex As Integer = GridView1.SelectedIndex

            '
            txtRcno.Text = 0

            'If txtFrom.Text = "Corporate" Or txtFrom.Text = "Residential" Then
            '    'txtRcno.Text = Session("rcnoinv")
            '    'Session.Remove("rcnoinv")
            '    ''txtRcno.Text = Session("rcnoinv")
            '    FindRcno(txtReceiptnoSearch.Text)
            '    'Session.Remove("customerfrom")
            '    'ddlSalesmanBilling.SelectedIndex = -1
            '    'ddlCreditTerms.SelectedIndex = -1
            'Else
            rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
            '    'txtComments.Text = GridView1.SelectedRow.Cells(1).Text.Trim
            txtRcno.Text = GridView1.SelectedRow.Cells(1).Text.Trim
            'End If
            'txtRcno.Text = GridView1.SelectedRow.Cells(1).Text.Trim


            PopulateRecord()


            'If grvBillingDetailsNew.Rows.Count = 0 Then
            '    'grvBillingDetailsNew.Visible = False
            '    grvBillingDetails.ShowHeader = True

            'Else
            '    grvBillingDetailsNew.Visible = True
            '    'grvBillingDetails.ShowHeader = False

            'End If

            'updpnlBillingDetails.Update()

            'If grvBillingDetailsNew.Rows.Count = 0 Then
            '    'grvBillingDetailsNew.Visible = False
            '    grvBillingDetails.ShowHeader = True

            'Else
            '    grvBillingDetailsNew.Visible = True
            '    grvBillingDetails.ShowHeader = False

            'End If
            If tb1.ActiveTabIndex = 1 Then
                tb1.ActiveTabIndex = 0
            End If

            updPanelCN.Update()

            lblFileUploadCount.Text = "File Upload"

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "GridView1_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub IsDetailBlank()
        Dim TextBoxItemTypeNew As TextBox
        Dim TextBoxItemType As DropDownList

        Dim rownew As String
        Dim rowold As Integer
        Dim rowoldstr As String
        rownew = "0"
        rowold = 0

        If grvBillingDetailsNew.Rows.Count > 0 Then
            TextBoxItemTypeNew = CType(grvBillingDetailsNew.Rows(0).Cells(0).FindControl("txtItemTypeGVB"), TextBox)
            rownew = String.IsNullOrEmpty(TextBoxItemTypeNew.Text)
            If String.IsNullOrEmpty(rownew) = "False" Then
                rownew = "1"
            End If
        End If

        If grvBillingDetails.Rows.Count > 0 Then
            TextBoxItemType = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
            rowoldstr = TextBoxItemType.Text
            If rowoldstr = "-1" Then
                rowold = 0
            End If
        End If
        'txtComments.Text = rownew & rowold

        If rownew = "0" And rowold = 0 Then
            'grvBillingDetails.ShowHeader = True
            grvBillingDetailsNew.Visible = False
            grvBillingDetails.Visible = True
            FirstGridViewRowBillingDetailsRecs()
            grvBillingDetails.ShowHeader = True
            updPanelCN.Update()
        ElseIf rownew = "1" And rowold = 0 Then
            'grvBillingDetails.ShowHeader = True
            grvBillingDetailsNew.Visible = True
            grvBillingDetails.Visible = False
            updPanelCN.Update()
            'FirstGridViewRowBillingDetailsRecs()
        ElseIf rownew = "0" And rowold = 1 Then
            'grvBillingDetails.ShowHeader = True
            grvBillingDetailsNew.Visible = False
            FirstGridViewRowBillingDetailsRecs()
            grvBillingDetails.Visible = True
            updPanelCN.Update()
            'FirstGridViewRowBillingDetailsRecs()
        ElseIf rownew = "1" And rowold = 1 Then
            'grvBillingDetails.ShowHeader = True
            grvBillingDetailsNew.Visible = True
            grvBillingDetails.Visible = True
            'FirstGridViewRowBillingDetailsRecs()
            updPanelCN.Update()
        End If


    End Sub

    'Private Sub FindRcno(source As String)

    '    Dim sqlstr As String
    '    sqlstr = ""

    '    sqlstr = "SELECT Rcno FROM tblSales where InvoiceNumber ='" & source & "'"

    '    Dim command As MySqlCommand = New MySqlCommand

    '    Dim conn As MySqlConnection = New MySqlConnection()
    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    conn.Open()

    '    command.CommandType = CommandType.Text
    '    command.CommandText = sqlstr
    '    command.Connection = conn

    '    Dim dr As MySqlDataReader = command.ExecuteReader()
    '    Dim dt As New System.Data.DataTable
    '    dt.Load(dr)

    '    If dt.Rows.Count > 0 Then
    '        txtRcno.Text = dt.Rows(0)("Rcno").ToString()
    '    End If
    '    conn.Close()
    '    conn.Dispose()
    'End Sub

    Private Sub PopulateRecord()
        Try
            'grvBillingDetails.ShowHeader = False
            'grvBillingDetails.Visible = False
            lblAlertStatus.Text = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim sql As String
            sql = ""
            sql = "Select * "
            sql = sql + " FROM tblJrnv "
            sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            'command1.CommandText = "SELECT * FROM tblcontract where rcno=" & Convert.ToInt64(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()


            Dim dt As New DataTable
            dt.Load(dr)
            'Convert.ToDateTime(dt.Rows(0)("ContractDate")).ToString("dd/MM/yyyy")
            If dt.Rows.Count > 0 Then

                'If dt.Rows(0)("InvoiceType").ToString = "M" Then
                '    rbtInvoiceType.SelectedIndex = 0
                'Else
                '    rbtInvoiceType.SelectedIndex = 1
                'End If

                'If dt.Rows(0)("DocType").ToString = "ARCN" Then
                '    ddlDocType.Text = "CREDIT NOTE"
                'ElseIf dt.Rows(0)("DocType").ToString = "ARDN" Then
                '    ddlDocType.Text = "DEBIT NOTE"
                'End If



                'ddlDocType.Text = dt.Rows(0)("DocType").ToString

                If dt.Rows(0)("PostStatus").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("PostStatus").ToString : End If
                If dt.Rows(0)("VoucherNumber").ToString <> "" Then : txtCNNo.Text = dt.Rows(0)("VoucherNumber").ToString : End If
                'If dt.Rows(0)("InvoiceNumber").ToString <> "" Then : txtCNNoSelected.Text = dt.Rows(0)("InvoiceNumber").ToString : End If
                If dt.Rows(0)("JournalDate").ToString <> "" Then : txtCNDate.Text = Convert.ToDateTime(dt.Rows(0)("JournalDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtReceiptPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If

                'If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtBillingPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                'If dt.Rows(0)("CompanyGroup").ToString <> "" Then : txtCompanyGroup.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                'If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountIdBilling.Text = dt.Rows(0)("AccountId").ToString : End If
                'If dt.Rows(0)("ContactType").ToString <> "" Then : txtAccountType.Text = dt.Rows(0)("ContactType").ToString : End If
                'If dt.Rows(0)("ContactType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContactType").ToString : End If
                'If dt.Rows(0)("CustName").ToString <> "" Then : txtAccountName.Text = dt.Rows(0)("CustName").ToString : End If
                'If dt.Rows(0)("BillAddress1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("BillAddress1").ToString : End If
                'If dt.Rows(0)("BillStreet").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("BillStreet").ToString : End If

                'If dt.Rows(0)("BillBuilding").ToString <> "" Then : txtBillBuilding.Text = dt.Rows(0)("BillBuilding").ToString : End If
                'If dt.Rows(0)("BillPostal").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("BillPostal").ToString : End If

                'If dt.Rows(0)("CustAddress1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("CustAddress1").ToString : End If
                ''If dt.Rows(0)("CustAddCountry").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("CustAddCountry").ToString : End If
                'If dt.Rows(0)("CustAddPostal").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("CustAddPostal").ToString : End If

                'Command.Parameters.AddWithValue("@CustAddress1", txtBillAddress.Text)
                'Command.Parameters.AddWithValue("@CustAddPostal", txtBillPostal.Text)
                'Command.Parameters.AddWithValue("@CustAddCountry", txtBillCountry.Text)

                'If dt.Rows(0)("CustAddress1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("CustAddress1").ToString : End If
                'If dt.Rows(0)("CustAddStreet").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("CustAddStreet").ToString : End If
                'If dt.Rows(0)("CustAddBuilding").ToString <> "" Then : txtBillBuilding.Text = dt.Rows(0)("CustAddBuilding").ToString : End If

                'If dt.Rows(0)("CustAddCountry").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("CustAddCountry").ToString : End If
                'If dt.Rows(0)("CustAddPostal").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("CustAddPostal").ToString : End If
                'If dt.Rows(0)("CustAttention").ToString <> "" Then : txtContactPerson.Text = dt.Rows(0)("CustAttention").ToString : End If

                'txtComments.Text = dt.Rows(0)("Salesman").ToString
                'If String.IsNullOrEmpty(dt.Rows(0)("Salesman").ToString) = True Then
                '    ddlSalesmanBilling.SelectedIndex = 0
                'Else
                '    ddlSalesmanBilling.Text = dt.Rows(0)("Salesman").ToString
                'End If


                'If dt.Rows(0)("SalesMan").ToString <> "" Then
                '    Dim gSalesman As String

                '    gSalesman = dt.Rows(0)("SalesMan").ToString.ToUpper()

                '    If ddlSalesmanBilling.Items.FindByValue(gSalesman) Is Nothing Then
                '        ddlSalesmanBilling.Items.Add(gSalesman)
                '        ddlSalesmanBilling.Text = gSalesman
                '    Else
                '        ddlSalesmanBilling.Text = dt.Rows(0)("SalesMan").ToString.Trim().ToUpper()
                '    End If
                'End If


                'If dt.Rows(0)("StaffCode").ToString <> "" Then
                '    'Dim gSalesman As String

                '    gddlvalue = dt.Rows(0)("StaffCode").ToString.ToUpper()

                '    'If ddlSalesmanBilling.Items.FindByValue(gddlvalue) Is Nothing Then
                '    '    ddlSalesmanBilling.Items.Add(gddlvalue)
                '    '    ddlSalesmanBilling.Text = gddlvalue
                '    'Else
                '    '    ddlSalesmanBilling.Text = dt.Rows(0)("StaffCode").ToString.Trim().ToUpper()
                '    'End If
                'End If


                'If dt.Rows(0)("CustAddCountry").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("CustAddCountry").ToString : End If

                'If dt.Rows(0)("PONo").ToString <> "" Then : txtPONo.Text = dt.Rows(0)("PONo").ToString : End If
                'If dt.Rows(0)("OurRef").ToString <> "" Then : txtOurReference.Text = dt.Rows(0)("OurRef").ToString : End If
                'If dt.Rows(0)("YourRef").ToString <> "" Then : txtYourReference.Text = dt.Rows(0)("YourRef").ToString : End If


                'If dt.Rows(0)("Terms").ToString <> "" Then
                '    'Dim gSalesman As String

                '    gddlvalue = dt.Rows(0)("Terms").ToString.ToUpper()

                '    If ddlCreditTerms.Items.FindByValue(gddlvalue) Is Nothing Then
                '        ddlCreditTerms.Items.Add(gddlvalue)
                '        ddlCreditTerms.Text = gddlvalue
                '    Else
                '        ddlCreditTerms.Text = dt.Rows(0)("Terms").ToString.Trim().ToUpper()
                '    End If
                'End If

                'If dt.Rows(0)("BatchNo").ToString <> "" Then : txtBatchNo.Text = dt.Rows(0)("BatchNo").ToString : End If
                If dt.Rows(0)("Comments").ToString <> "" Then : txtComments.Text = dt.Rows(0)("Comments").ToString : End If
                'If dt.Rows(0)("TermsDay").ToString <> "" Then : txtCreditDays.Text = dt.Rows(0)("TermsDay").ToString : End If
                'If dt.Rows(0)("RecurringInvoice").ToString = "N" Then : chkRecurringInvoice.Text = dt.Rows(0)("RecurringInvoice").ToString : End If

                If dt.Rows(0)("DebitBase").ToString <> "" Then : txtDebitAmount.Text = dt.Rows(0)("DebitBase").ToString : End If
                'If dt.Rows(0)("ValueOriginal").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("ValueOriginal").ToString : End If
                If dt.Rows(0)("CreditBase").ToString <> "" Then : txtCreditAmount.Text = dt.Rows(0)("CreditBase").ToString : End If
                'If dt.Rows(0)("GSTOriginal").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("GSTOriginal").ToString : End If
                'If dt.Rows(0)("AppliedBase").ToString <> "" Then : txtCNNetAmount.Text = dt.Rows(0)("AppliedBase").ToString : End If

                If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If

                txtReasonChSt.Text = dt.Rows(0)("ReasonChSt").ToString.Trim
                txtRecordCreatedBy.Text = dt.Rows(0)("CreatedBy").ToString
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

            '
            'updpnlBillingDetails.Update()
        

            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnCopy.Enabled = True
                btnReverse.Enabled = True
                btnReverse.ForeColor = System.Drawing.Color.Black

                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray
                btnPrint.Enabled = True
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnReverse.Enabled = False
                btnReverse.ForeColor = System.Drawing.Color.Gray
                btnDelete.ForeColor = System.Drawing.Color.Gray
                btnChangeStatus.Enabled = True
                btnChangeStatus.ForeColor = System.Drawing.Color.Black

                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPost.Enabled = True
                btnPost.ForeColor = System.Drawing.Color.Black

            End If

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                If txtOnlyEditableByCreator.Text = "1" Then
                    If txtRecordCreatedBy.Text.Trim <> txtCreatedBy.Text.Trim Then
                        btnEdit.Enabled = False
                        btnPost.Enabled = False
                        btnReverse.Enabled = False
                        btnCopy.Enabled = False
                    End If
                End If
            End If

            updPnlMsg.Update()

            'PopulateArCode()
            DisplayGLGrid()

            grvBillingDetailsNew.DataSourceID = "SqlDSSalesDetail"
            grvBillingDetailsNew.DataBind()
            IsDetailBlank()

            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If


            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
            Else
                btnReverse.Enabled = False
                btnReverse.ForeColor = System.Drawing.Color.Gray
            End If

            Session.Add("RecordNo", txtCNNo.Text)
            'Session.Add("Title", ddlDocType.SelectedItem.Text)

            Dim IsLock = FindJNPeriod(txtReceiptPeriod.Text)

            If IsLock = "Y" Then
                'lblAlert.Text = "PERIOD IS LOCKED"
                'updPnlMsg.Update()
                'txtInvoiceDate.Focus()
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                'btnSaveInvoice.Enabled = False
                'btnSaveInvoice.ForeColor = System.Drawing.Color.Gray
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
                btnReverse.Enabled = False
                btnReverse.ForeColor = System.Drawing.Color.Gray
                btnChangeStatus.Enabled = False
                btnChangeStatus.ForeColor = System.Drawing.Color.Gray
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PopulateRecord", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Function

    Private Sub GenerateJournalNo()
        Try
            Dim lPrefix As String
            Dim lYear As String
            Dim lMonth As String
            Dim lInvoiceNo As String
            Dim lSuffixVal As String
            Dim lSuffix As String
            Dim lSetWidth As Integer
            Dim lSetZeroes As String
            Dim lSeparator As String
            Dim strUpdate As String
            lSeparator = "-"
            strUpdate = ""

            Dim strdate As Date
            Dim d As Date
            If Date.TryParseExact(txtCNDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                strdate = d.ToShortDateString
            End If

            lPrefix = Format(CDate(strdate), "yyyyMM")

            'If ddlDocType.Text = "ARCN" Then
            '    lInvoiceNo = "DC" + lPrefix + "-"
            '    lMonth = Right(lPrefix, 2)
            '    lYear = Left(lPrefix, 4)
            '    lPrefix = "DC" + lYear
            'Else
            lInvoiceNo = "JV" + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)
            lPrefix = "JV" + lYear
            'End If


            lSuffixVal = 0

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandDocControl As MySqlCommand = New MySqlCommand
            commandDocControl.CommandType = CommandType.Text
            commandDocControl.CommandText = "SELECT * FROM tbldoccontrol where prefix='" & lPrefix & "'"
            commandDocControl.Connection = conn

            Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then


                If lMonth = "01" Then
                    lSuffixVal = dt.Rows(0)("Period01").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period01 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "02" Then
                    lSuffixVal = dt.Rows(0)("Period02").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period02 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "03" Then
                    lSuffixVal = dt.Rows(0)("Period03").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period03 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "04" Then
                    lSuffixVal = dt.Rows(0)("Period04").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period04 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "05" Then
                    lSuffixVal = dt.Rows(0)("Period05").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period05 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "06" Then
                    lSuffixVal = dt.Rows(0)("Period06").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period06 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "07" Then
                    lSuffixVal = dt.Rows(0)("Period07").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period07 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "08" Then
                    lSuffixVal = dt.Rows(0)("Period08").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period08 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "09" Then
                    lSuffixVal = dt.Rows(0)("Period09").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period09 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "10" Then
                    lSuffixVal = dt.Rows(0)("Period10").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period10 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "11" Then
                    lSuffixVal = dt.Rows(0)("Period11").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period11 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                ElseIf lMonth = "12" Then
                    lSuffixVal = dt.Rows(0)("Period12").ToString + 1
                    lSetWidth = dt.Rows(0)("Width").ToString
                    strUpdate = " Update tbldoccontrol set Period12 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                End If

                Dim commandDocControlEdit As MySqlCommand = New MySqlCommand

                commandDocControlEdit.CommandType = CommandType.Text
                commandDocControlEdit.CommandText = strUpdate
                commandDocControlEdit.Connection = conn

                Dim dr2 As MySqlDataReader = commandDocControlEdit.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)
            Else

                Dim lSuffixVal1 As String
                Dim lSuffixVal2 As String
                Dim lSuffixVal3 As String
                Dim lSuffixVal4 As String
                Dim lSuffixVal5 As String
                Dim lSuffixVal6 As String
                Dim lSuffixVal7 As String
                Dim lSuffixVal8 As String
                Dim lSuffixVal9 As String
                Dim lSuffixVal10 As String
                Dim lSuffixVal11 As String
                Dim lSuffixVal12 As String

                lSuffixVal1 = 0
                lSuffixVal2 = 0
                lSuffixVal3 = 0
                lSuffixVal4 = 0
                lSuffixVal5 = 0
                lSuffixVal6 = 0
                lSuffixVal7 = 0
                lSuffixVal8 = 0
                lSuffixVal9 = 0
                lSuffixVal10 = 0
                lSuffixVal11 = 0
                lSuffixVal12 = 0

                If lMonth = "01" Then
                    lSuffixVal1 = 1
                ElseIf lMonth = "02" Then
                    lSuffixVal2 = 1
                ElseIf lMonth = "03" Then
                    lSuffixVal3 = 1
                ElseIf lMonth = "04" Then
                    lSuffixVal4 = 1
                ElseIf lMonth = "05" Then
                    lSuffixVal5 = 1
                ElseIf lMonth = "06" Then
                    lSuffixVal6 = 1
                ElseIf lMonth = "07" Then
                    lSuffixVal7 = 1
                ElseIf lMonth = "08" Then
                    lSuffixVal8 = 1
                ElseIf lMonth = "09" Then
                    lSuffixVal9 = 1
                ElseIf lMonth = "10" Then
                    lSuffixVal10 = 1
                ElseIf lMonth = "11" Then
                    lSuffixVal11 = 1
                ElseIf lMonth = "12" Then
                    lSuffixVal12 = 1
                End If

                Dim commandDocControlInsert As MySqlCommand = New MySqlCommand

                commandDocControlInsert.CommandType = CommandType.Text
                'commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                '               "('" & lPrefix & "','M','" & lSeparator & "',6,0,0,0,0,0,0,0,0,0,0,0,0)"

                commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                            "('" & lPrefix & "','M','" & lSeparator & "',6," & lSuffixVal1 & "," & lSuffixVal2 & "," & lSuffixVal3 & "," & lSuffixVal4 & "," & lSuffixVal5 & "," & lSuffixVal6 & "," & lSuffixVal7 & "," & lSuffixVal8 & "," & lSuffixVal9 & "," & lSuffixVal10 & "," & lSuffixVal11 & "," & lSuffixVal12 & ")"
                commandDocControlInsert.Connection = conn

                Dim dr2 As MySqlDataReader = commandDocControlInsert.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                lSetWidth = 6
                lSuffixVal = 1


            End If

            lSetZeroes = ""

            Dim i As Integer
            If lSetWidth > 0 Then
                For i = 1 To lSetWidth - (Len(lSuffixVal))
                    lSetZeroes = lSetZeroes & "0"
                Next i
                'ElseIf pLength = 0 Then                     ' Use 6 and save it in Doc Control
                '    strZeros = "000000"
                '    setWidth = 6
                'Else                                        ' Use vLength and save it in Doc Control
                '    For i = 1 To pLength
                '        strZeros = strZeros & "0"
                '    Next i
                '    setWidth = pLength
            End If
            lSuffix = lSetZeroes + lSuffixVal.ToString()
            txtCNNo.Text = lInvoiceNo + lSuffix

            conn.Close()
            conn.Dispose()
            commandDocControl.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "GenerateJournalNo", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    Public Sub MakeMeNull()
        Try

            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            'txtSearch1Status.Text = "O,P"

            'txtLedgerSearch.Text = ""
            'txtAccountIdSearch.Text = ""
            'txtClientNameSearch.Text = ""
            'txtReceiptnoSearch.Text = ""

            'txtInvoiceDateFromSearch.Text = ""
            'txtInvoiceDateToSearch.Text = ""

            'txtAmountSearch.Text = ""
            'txtInvoiceNoSearch.Text = ""
            'ddlCompanyGrpSearch.SelectedIndex = 0


            'txtMode.Text = "NEW"
            txtRcno.Text = "0"

            txtPopUpClient.Text = ""
            ddlCompanyGrpII.SelectedIndex = 0
            ddlCompanyGrp.SelectedIndex = 0

            grvBillingDetails.ShowHeader = True
            grvBillingDetails.Visible = True

            grvServiceRecDetails.DataSourceID = ""
            grvServiceRecDetails.DataBind()

            grvInvoiceRecDetails.DataSourceID = ""
            grvInvoiceRecDetails.DataBind()

            btnEdit.Enabled = False
            btnCopy.Enabled = False
            btnReverse.Enabled = False

            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnPrint.Enabled = False
            btnPost.Enabled = False
            btnDelete.Enabled = False
            updPnlMsg.Update()

            DisableControls()
            'Dim query As String

            'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
            'PopulateDropDownList(Query, "ProductCode", "ProductCode", ddlItemCode)

            FirstGridViewRowBillingDetailsRecs()
            'FirstGridViewRowServiceRecs()

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
            'updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        End Try
    End Sub

    Private Sub AccessControl()
        If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            Try
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT x0258,  x0258Add, x0258Edit, x0258Delete, x0258Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0258,  x0258Add, x0258Edit, x0258Delete, x0258Print, x0258Post, x0258Reverse,x0258FileUpload FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0258")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0258"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0258")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("x0258Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0258Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("x0258Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("x0258Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("x0258Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0258Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("x0258Delete").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0258Print")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258Print")) = False Then
                                Me.btnPrint.Enabled = dt.Rows(0)("x0258Print").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0258Post")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258Post")) = False Then
                                Me.btnPost.Enabled = dt.Rows(0)("x0258Post").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0258Reverse")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258Reverse")) = False Then
                                Me.btnReverse.Enabled = dt.Rows(0)("x0258Reverse").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0258FileUpload")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0258FileUpload")) = False Then
                                txtFileUpload.Text = dt.Rows(0)("x0258FileUpload").ToString()
                                btnUpload.Enabled = dt.Rows(0)("x0258FileUpload").ToString()
                                FileUpload1.Enabled = dt.Rows(0)("x0258FileUpload").ToString()
                            Else
                                txtFileUpload.Text = "0"
                            End If
                        Else
                            txtFileUpload.Text = "0"
                        End If
                    Else
                        Me.btnEdit.Enabled = False
                        Me.btnDelete.Enabled = False
                        Me.btnPrint.Enabled = False
                        Me.btnPost.Enabled = False
                        Me.btnReverse.Enabled = False
                    End If

                    'If String.IsNullOrEmpty(dt.Rows(0)("X0256Print")) = False Then
                    '    Me.btnDelete.Enabled = dt.Rows(0)("X0256Print").ToString()
                    'End If

                    If btnADD.Enabled = True Then
                        btnADD.ForeColor = System.Drawing.Color.Black
                    Else
                        btnADD.ForeColor = System.Drawing.Color.Gray
                    End If

                    If btnEdit.Enabled = True Then
                        btnEdit.ForeColor = System.Drawing.Color.Black
                    Else
                        btnEdit.ForeColor = System.Drawing.Color.Gray
                    End If

                    If btnDelete.Enabled = True Then
                        btnDelete.ForeColor = System.Drawing.Color.Black
                    Else
                        btnDelete.ForeColor = System.Drawing.Color.Gray
                    End If

                    If btnPrint.Enabled = True Then
                        btnPrint.ForeColor = System.Drawing.Color.Black
                    Else
                        btnPrint.ForeColor = System.Drawing.Color.Gray
                    End If

                    If btnPost.Enabled = True Then
                        btnPost.ForeColor = System.Drawing.Color.Black
                    Else
                        btnPost.ForeColor = System.Drawing.Color.Gray
                    End If

                    If btnReverse.Enabled = True Then
                        btnReverse.ForeColor = System.Drawing.Color.Black
                    Else
                        btnReverse.ForeColor = System.Drawing.Color.Gray
                    End If
                End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
                dr.Close()
            Catch ex As Exception
                InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "AccessControl", ex.Message.ToString, "")
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub DisableControls()
        'btnSave.Enabled = False
        'btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = False
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        'ddlDocType.Enabled = False

        txtCNNo.Enabled = False
        txtCNDate.Enabled = False
        txtReceiptPeriod.Enabled = False
        txtCompanyGroup.Enabled = False
        'txtAccountIdBilling.Enabled = False
        'ddlContactType.Enabled = False
        'txtAccountName.Enabled = False
        'txtBankGLCode.Enabled = False

        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True
        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True
        'ddlSalesmanBilling.Enabled = False

        'txtOurReference.Enabled = False
        'txtYourReference.Enabled = False
        'txtPONo.Enabled = False
        'ddlCreditTerms.Enabled = False
        txtComments.Enabled = False

        txtCreditAmount.Enabled = False
        txtDebitAmount.Enabled = False
        txtCreditAmount.Enabled = False
        'ddlContractNoCN.Enabled = False
        'ddlItemCode.Enabled = False
        'txtARDescription10.Enabled = False

        btnShowInvoices.Enabled = False
        btnShowServices.Enabled = False

        btnSave.Enabled = False
        'btnShowRecords.Enabled = False

        grvBillingDetails.Enabled = False
        grvBillingDetailsNew.Enabled = False
        btnDelete.Enabled = False
        'btnClient.Visible = False


        txtReceiptnoSearch.Enabled = True
        'txtInvoiceNoSearch.Enabled = True
        txtInvoiceDateFromSearch.Enabled = True
        txtInvoiceDateToSearch.Enabled = True
        ddlCompanyGrpSearch.Enabled = True
        ddlContactTypeSearch.Enabled = True
        txtAccountIdSearch.Enabled = True
        txtSearch1Status.Enabled = True
        btnQuickSearch.Enabled = True
        btnQuickReset.Enabled = True
        btnLedgerCode.Enabled = True

        'txtCommentsSearch.Enabled = True
        txtClientNameSearch.Enabled = True

        txtInvoiceNoSearch.Enabled = True
        txtLedgerSearch.Enabled = True
        txtAmountSearch.Enabled = True

        btnSearch1Status.Enabled = True
        'btnClientSearch0.Enabled = True
        btnClientSearch.Enabled = True
        'rdbSearchPaidStatus.Enabled = True
        'ddlBankIDSearch.Enabled = True
        'txtChequeNoSearch.Enabled = True

        updPnlSearch.Update()
        AccessControl()
    End Sub

    Private Sub EnableControls()
        'btnSave.Enabled = True
        'btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = True
        btnADD.Enabled = False
        btnADD.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray


        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray

        'rdbAll.Attributes.Remove("disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")

        'rdbAll.Enabled = True
        'rdbCompleted.Enabled = True
        'rdbNotCompleted.Enabled = True
        'ddlDocType.Enabled = True
        txtCNNo.Enabled = True
        txtCNDate.Enabled = True
        txtReceiptPeriod.Enabled = True
        txtCompanyGroup.Enabled = True
        'txtAccountId.Enabled = True
        'ddlContactType.Enabled = True
        'txtAccountIdBilling.Enabled = True
        'txtAccountName.Enabled = True
        'txtContactPerson.Enabled = True
        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True

        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True
        'ddlSalesmanBilling.Enabled = True
        'txtOurReference.Enabled = True
        'txtYourReference.Enabled = True
        'txtPONo.Enabled = True
        'ddlCreditTerms.Enabled = True
        txtComments.Enabled = True

        txtCreditAmount.Enabled = True
        txtDebitAmount.Enabled = True
        'ddlContractNoCN.Enabled = True
        'ddlItemCode.Enabled = True
        'txtARDescription10.Enabled = True

        btnShowInvoices.Enabled = True
        btnShowServices.Enabled = True
        btnSave.Enabled = True
        'btnShowRecords.Enabled = True

        'ddlContactType.Enabled = True
        'ddlCompanyGrp.Enabled = True
        'txtAccountId.Enabled = True
        'txtContractNo.Enabled = True
        'txtClientName.Enabled = True
        'txtLocationId.Enabled = True
        'ddlContractGrp.Enabled = True

        'ddlContractNo.Enabled = True
        'txtDateFrom.Enabled = True
        'txtDateTo.Enabled = True


        btnDelete.Enabled = True

        txtReceiptnoSearch.Enabled = False
        'txtInvoiceNoSearch.Enabled = False
        txtInvoiceDateFromSearch.Enabled = False
        txtInvoiceDateToSearch.Enabled = False
        ddlCompanyGrpSearch.Enabled = False
        ddlContactTypeSearch.Enabled = False
        txtAccountIdSearch.Enabled = False
        txtSearch1Status.Enabled = False

        txtInvoiceNoSearch.Enabled = False
        txtLedgerSearch.Enabled = False
        txtAmountSearch.Enabled = False

        btnQuickSearch.Enabled = False
        btnQuickReset.Enabled = False
        'txtCommentsSearch.Enabled = True
        txtClientNameSearch.Enabled = False
        btnSearch1Status.Enabled = False
        btnLedgerCode.Enabled = False

        'btnClientSearch0.Enabled = True
        btnClientSearch.Enabled = False
        'rdbSearchPaidStatus.Enabled = True
        'ddlBankIDSearch.Enabled = True
        'txtChequeNoSearch.Enabled = True

        updPnlSearch.Update()

        grvBillingDetails.Enabled = True
        'grvServiceRecDetails.Enabled = True
        updPnlBillingRecs.Update()
        'updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        'updPanelSearchServiceRec.Update()

        'btnClient.Visible = True

        AccessControl()
    End Sub



    'Function

    'Button-click
    Protected Sub ShowMessage(sender As Object, e As EventArgs, message As String)

        'Dim message As String = "alert('Hello! Mudassar.')"

        ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

    End Sub

    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        Try
            'txtInvoiceDate.Text = ""
            MakeMeNullBillingDetails()
            MakeMeNull()

            EnableControls()

            updPanelCN.Update()
            txtSearch1Status.Text = "O,P"
            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtCNDate.Text)) & Format(Month(Convert.ToDateTime(txtCNDate.Text)), "00")




        Catch ex As Exception
            lblAlert.Text = ex.Message
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnAdd_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Button clic


    'Pop-up



    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Try
            Dim con As MySqlConnection = New MySqlConnection()

            con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand(query)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                ddl.DataSource = cmd.ExecuteReader()
                ddl.DataTextField = textField.Trim()
                ddl.DataValueField = valueField.Trim()
                ddl.DataBind()
                con.Close()
                con.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PopulateDropDownList", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Public Sub PopulateComboBox(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As AjaxControlToolkit.ComboBox)
        Dim con As MySqlConnection = New MySqlConnection()

        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        Using cmd As New MySqlCommand(query)
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            con.Open()
            cmb.DataSource = cmd.ExecuteReader()
            cmb.DataTextField = textField.Trim()
            cmb.DataValueField = valueField.Trim()
            cmb.DataBind()
            con.Close()
        End Using
        'End Using
    End Sub



    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    ''MessageBox.Message.Alert(Page, ddlSearchStatus.Text + " " + txtDDLText.Text, "str")
    ''Return
    'MakeMeNull()
    'Dim qry As String
    'qry = "select Status, RenewalSt, NotedST, Gst, ContractNo, ContractDate, AccountId, CustName, CustAddr, InchargeId, AgreeValue, StartDate, EndDate, ActualEnd, "
    'qry = qry + " Scheduler, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno from tblcontract where 1 =1 "

    'If String.IsNullOrEmpty(txtSearchID.Text) = False Then
    '    'txtID.Text = txtSearchID.Text
    '    qry = qry + " and contractno like '%" + txtSearchID.Text + "%'"
    'End If
    'If (ddlSearchStatus.Text) <> "-1" Then
    '    If ddlSearchStatus.Text <> txtDDLText.Text Then
    '        'ddlStatus.Text = ddlSearchStatus.Text
    '        qry = qry + " and status = '" + ddlSearchStatus.Text + "'"
    '    End If
    'Else
    '    qry = qry + " and status = 'O'"
    'End If


    ''If String.IsNullOrEmpty(txtSearchCustCode.Text) = False Then
    ''    'txtNameE.Text = txtSearchCompany.Text
    ''    qry = qry + " and custcode like '%" + txtSearchCustCode.Text + "%'"
    ''End If

    'If String.IsNullOrEmpty(txtSearchCustCode.Text) = False Then
    '    'txtNameE.Text = txtSearchCompany.Text
    '    qry = qry + " and Accountid like '%" + txtSearchCustCode.Text + "%'"
    'End If

    'If String.IsNullOrEmpty(txtSearchCompany.Text) = False Then
    '    'txtNameE.Text = txtSearchCompany.Text
    '    qry = qry + " and custname like '%" + txtSearchCompany.Text + "%'"
    'End If
    'If String.IsNullOrEmpty(txtSearchAddress.Text) = False Then

    '    qry = qry + " and (custaddr like '%" + txtSearchAddress.Text + "%')"
    '    'qry = qry + " or addbuilding like '%" + txtSearchAddress.Text + "%'"
    '    'qry = qry + " or addstreet like '%" + txtSearchAddress.Text + "%')"
    'End If

    'If String.IsNullOrEmpty(txtSearchContactNo.Text) = False Then
    '    'txtNameE.Text = txtSearchCompany.Text
    '    qry = qry + " and (telephone = '" + txtSearchContactNo.Text + "'"
    '    qry = qry + " or contactpersonmobile = '" + txtSearchContactNo.Text + "')"
    'End If

    'If String.IsNullOrEmpty(txtSearchPostal.Text) = False Then
    '    'txtPostal.Text = txtSearchPostal.Text
    '    qry = qry + " and postal  = '" + txtSearchPostal.Text + "'"
    'End If
    'If String.IsNullOrEmpty(txtSearchContact.Text) = False Then
    '    qry = qry + " and contact like '%" + txtSearchContact.Text + "%'"

    'End If
    'If (ddlSearchSalesman.Text) <> "-1" Then

    '    qry = qry + " and salesman  = '" + ddlSearchSalesman.Text.Trim + "'"
    'End If
    'If (ddlSearchContactType.Text) <> "-1" Then
    '    'If ddlSearchContactType.Text <> txtDDLText.Text Then
    '    qry = qry + " and ContactType  = '" + ddlSearchContactType.Text + "'"
    '    'End If
    'End If
    'If (ddlSearchScheduler.Text) <> "-1" Then
    '    qry = qry + " and Scheduler  = '" + ddlSearchScheduler.Text + "'"
    'End If


    'If (ddlSearchInChargeId.Text) <> "-1" Then
    '    qry = qry + " and Inchargeid = '" + ddlSearchInChargeId.Text + "'"
    'End If

    'If (ddlSearchContractGroup.Text) <> "-1" Then
    '    qry = qry + " and ContractGroup  = '" + ddlSearchContractGroup.Text + "'"
    'End If

    'If (ddlSearchCompanyGroup.Text) <> "-1" Then
    '    qry = qry + " and CompanyGroup  = '" + ddlSearchCompanyGroup.Text + "'"
    'End If

    'If (ddlSearchLocationGroup.Text) <> "-1" Then
    '    qry = qry + " and LocateGRp  = '" + ddlSearchLocationGroup.Text + "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchOurRef.Text) = False Then
    '    'txtPostal.Text = txtSearchPostal.Text
    '    qry = qry + " and ourreference  = '" + txtSearchOurRef.Text + "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchYourRef.Text) = False Then
    '    qry = qry + " and yourreference  = '" + txtSearchYourRef.Text + "'"
    'End If



    'If String.IsNullOrEmpty(txtSearchContractDateFrom.Text) = False And IsDate(txtSearchContractDateFrom.Text) Then
    '    qry = qry + " and ContractDate>= '" + Convert.ToDateTime(txtSearchContractDateFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchContractDateTo.Text) = False And IsDate(txtSearchContractDateTo.Text) Then
    '    qry = qry + " and ContractDate  <= '" + Convert.ToDateTime(txtSearchContractDateTo.Text).ToString("yyyy-MM-dd") & "'"

    'End If


    'If String.IsNullOrEmpty(txtSearchStartDateFrom.Text) = False And IsDate(txtSearchStartDateFrom.Text) Then
    '    qry = qry + " and StartDate  >= '" + Convert.ToDateTime(txtSearchStartDateFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchStartDateTo.Text) = False And IsDate(txtSearchStartDateTo.Text) Then
    '    qry = qry + " and StartDate  <= '" + Convert.ToDateTime(txtSearchStartDateTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchEndDateFrom.Text) = False And IsDate(txtSearchEndDateFrom.Text) Then
    '    qry = qry + " and EndDate  >= '" + Convert.ToDateTime(txtSearchEndDateFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchEndDateTo.Text) = False And IsDate(txtSearchEndDateTo.Text) Then
    '    qry = qry + " and EndDate  <= '" + Convert.ToDateTime(txtSearchEndDateTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If



    'If String.IsNullOrEmpty(txtSearchServiceStartFrom.Text) = False And IsDate(txtSearchServiceStartFrom.Text) Then
    '    qry = qry + " and ServiceStart  >= '" + Convert.ToDateTime(txtSearchServiceStartFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchServiceStartTo.Text) = False And IsDate(txtSearchServiceStartTo.Text) Then
    '    qry = qry + " and ServiceStart  <= '" + Convert.ToDateTime(txtSearchServiceStartTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchServiceEndFrom.Text) = False And IsDate(txtSearchServiceEndFrom.Text) Then
    '    qry = qry + " and ServiceEnd  >= '" + Convert.ToDateTime(txtSearchServiceEndFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchServiceEndTo.Text) = False And IsDate(txtSearchServiceEndTo.Text) Then
    '    qry = qry + " and ServiceEnd  <= '" + Convert.ToDateTime(txtSearchServiceEndTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchActualEndFrom.Text) = False And IsDate(txtSearchActualEndFrom.Text) Then
    '    qry = qry + " and ActualEnd  >= '" + Convert.ToDateTime(txtSearchActualEndFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchActualEndTo.Text) = False And IsDate(txtSearchActualEndTo.Text) Then
    '    qry = qry + " and ActualEnd  <= '" + Convert.ToDateTime(txtSearchActualEndTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If


    'If String.IsNullOrEmpty(txtSearchActualEndFrom.Text) = False And IsDate(txtSearchActualEndFrom.Text) Then
    '    qry = qry + " and ActualEnd  >= '" + Convert.ToDateTime(txtSearchActualEndFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchActualEndTo.Text) = False And IsDate(txtSearchActualEndTo.Text) Then
    '    qry = qry + " and ActualEnd  <= '" + Convert.ToDateTime(txtSearchStartDateFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If



    'If String.IsNullOrEmpty(txtSearchEntryDateFrom.Text) = False And IsDate(txtSearchEntryDateFrom.Text) Then
    '    qry = qry + " and EntryDate  >= '" + Convert.ToDateTime(txtSearchEntryDateFrom.Text).ToString("yyyy-MM-dd") & "'"
    'End If

    'If String.IsNullOrEmpty(txtSearchEntryDateTo.Text) = False And IsDate(txtSearchEntryDateTo.Text) Then
    '    qry = qry + " and EntryDate  <= '" + Convert.ToDateTime(txtSearchEntryDateTo.Text).ToString("yyyy-MM-dd") & "'"
    'End If



    'qry = qry + " order by custname;"
    'txt.Text = qry

    'SQLDSInvoice.SelectCommand = qry
    'SQLDSInvoice.DataBind()
    'GridView1.DataBind()
    'txtSearchID.Text = ""
    'GridSelected = "SQLDSContract"

    'txtSearchCustCode.Text = ""
    'txtSearchCompany.Text = ""
    'txtSearchAddress.Text = ""
    'txtSearchContact.Text = ""
    'txtSearchContactNo.Text = ""
    'txtSearchPostal.Text = ""

    'txtSearchOurRef.Text = ""
    'txtSearchYourRef.Text = ""

    'ddlSearchSalesman.ClearSelection()
    'ddlSearchScheduler.ClearSelection()
    'ddlSearchStatus.ClearSelection()
    'ddlSearchContractGroup.ClearSelection()
    'ddlSearchCompanyGroup.ClearSelection()
    'ddlSearchLocationGroup.ClearSelection()
    'ddlSearchRenewalStatus.ClearSelection()
    'ddlSearchInChargeId.ClearSelection()

    'txtSearchContractDateFrom.Text = ""
    'txtSearchContractDateTo.Text = ""
    'txtSearchServiceStartFrom.Text = ""
    'txtSearchServiceStartTo.Text = ""
    'txtSearchServiceEndFrom.Text = ""
    'txtSearchServiceEndTo.Text = ""
    'txtSearchActualEndFrom.Text = ""
    'txtSearchActualEndTo.Text = ""

    'txtSearchStartDateFrom.Text = ""
    'txtSearchStartDateTo.Text = ""

    'txtSearchEndDateFrom.Text = ""
    'txtSearchEndDateTo.Text = ""
    'txtSearchEntryDateFrom.Text = ""
    'txtSearchEntryDateTo.Text = ""

    'End Sub


    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Response.Redirect("Home.aspx")

        If String.IsNullOrEmpty(txtFrom.Text) = True Then
            Response.Redirect("Home.aspx")
        End If

        'If txtFrom.Text = "schedule" Then
        '    Session("invoiceno") = lblInvoiceId1.Text
        '    Response.Redirect("InvoiceSchedule.aspx")
        'End If

        If txtFrom.Text = "Corporate" Then
            Session.Add("customerfrom", "Corporate")
            Session.Add("armodule", "armodule")
            Response.Redirect("Company.aspx")
        End If

        If txtFrom.Text = "Residential" Then
            Session.Add("customerfrom", "Residential")
            Session.Add("armodule", "armodule")
            Response.Redirect("Person.aspx")
        End If
    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex


        'txt.Text = strsql

        SQLDSCN.SelectCommand = txt.Text
        SQLDSCN.DataBind()
        GridView1.DataBind()
        'If GridSelected = "SQLDSContract" Then
        '    SQLDSInvoice.SelectCommand = txt.Text
        '    SQLDSInvoice.DataBind()
        'ElseIf GridSelected = "SQLDSContractClientId" Then
        '    'SqlDataSource1.SelectCommand = txt.Text
        '    'SQLDSContractClientId.DataBind()
        'ElseIf GridSelected = "SQLDSContractNo" Then
        '    ''SqlDataSource1.SelectCommand = txt.Text
        '    'SqlDSContractNo.DataBind()
        'End If

        GridView1.DataBind()
    End Sub


    'Protected Sub BtnChSt_Click(sender As Object, e As EventArgs) Handles BtnChSt.Click
    'Try


    '            Dim confirmValue As String
    '            confirmValue = ""

    '            confirmValue = Request.Form("confirm_valueChSt")
    '            If Right(confirmValue, 3) = "Yes" Then

    '                Dim strdate As DateTime
    '                Dim conn As MySqlConnection = New MySqlConnection()

    '                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '                'Dim conn As MySqlConnection = New MySqlConnection(constr)
    '                conn.Open()


    '                ''''''''''''''''''E
    '                If Left(ddlStatusChSt.Text, 1) = "E" Then
    '                    Dim commandE1 As MySqlCommand = New MySqlCommand

    '                    commandE1.CommandType = CommandType.Text
    '                    commandE1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                    commandE1.Connection = conn

    '                    Dim drservice As MySqlDataReader = commandE1.ExecuteReader()
    '                    Dim dtservice As New DataTable
    '                    dtservice.Load(drservice)

    '                    If dtservice.Rows.Count > 0 Then
    '                        Dim commandE2 As MySqlCommand = New MySqlCommand
    '                        commandE2.CommandType = CommandType.Text
    '                        'Exit Sub
    '                        commandE2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                        commandE2.Connection = conn
    '                        commandE2.ExecuteNonQuery()

    '                    End If
    '                    '''

    '                    Dim commandE As MySqlCommand = New MySqlCommand
    '                    commandE.CommandType = CommandType.Text

    '                    Dim qry As String = "UPDATE tblContract SET  Status ='E', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


    '                    commandE.CommandText = qry
    '                    commandE.Parameters.Clear()


    '                    If txtActualEndChSt.Text = "" Then
    '                        commandE.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    Else
    '                        strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
    '                        commandE.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
    '                    End If

    '                    commandE.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
    '                    commandE.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
    '                    commandE.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
    '                    commandE.Parameters.AddWithValue("@Rcno", txtRcno.Text)
    '                    commandE.Connection = conn
    '                    commandE.ExecuteNonQuery()


    '                    'MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")


    '                    conn.Close()
    '                    'Dim message As String = "alert('Contract Updated successfully!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


    '                    ''''''
    '                    GridView1.DataBind()
    '                    'txtServInst.Text = txtCommentChSt.Text
    '                    txtStatus.Text = "E"

    '                    '''''''''''''''''''E

    '                    '''''''''''''''''''T
    '                ElseIf Left(ddlStatusChSt.Text, 1) = "T" Then
    '                    Dim commandT1 As MySqlCommand = New MySqlCommand

    '                    commandT1.CommandType = CommandType.Text
    '                    commandT1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                    commandT1.Connection = conn

    '                    Dim drserviceT As MySqlDataReader = commandT1.ExecuteReader()
    '                    Dim dtserviceT As New DataTable
    '                    dtserviceT.Load(drserviceT)

    '                    If dtserviceT.Rows.Count > 0 Then
    '                        Dim commandT2 As MySqlCommand = New MySqlCommand
    '                        commandT2.CommandType = CommandType.Text
    '                        'Exit Sub
    '                        commandT2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                        commandT2.Connection = conn
    '                        commandT2.ExecuteNonQuery()
    '                    End If

    '                    Dim commandT As MySqlCommand = New MySqlCommand
    '                    commandT.CommandType = CommandType.Text

    '                    Dim qryT As String = "UPDATE tblContract SET  Status ='T', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


    '                    commandT.CommandText = qryT
    '                    commandT.Parameters.Clear()


    '                    If txtActualEndChSt.Text = "" Then
    '                        commandT.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    Else
    '                        strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
    '                        commandT.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
    '                    End If


    '                    commandT.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)

    '                    commandT.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
    '                    commandT.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
    '                    commandT.Parameters.AddWithValue("@Rcno", txtRcno.Text)
    '                    commandT.Connection = conn
    '                    commandT.ExecuteNonQuery()

    '                    'Dim message As String = "alert('Contract Terminated successfully!!!!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


    '                    conn.Close()
    '                    'Dim messageT As String = "alert('Contract Updated successfully!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageT, True)

    '                    GridView1.DataBind()
    '                    'txtServInst.Text = txtCommentChSt.Text
    '                    txtStatus.Text = "T"

    '                    ''''''''''''''''''T

    '                    '''''''''''''''''''X
    '                ElseIf Left(ddlStatusChSt.Text, 1) = "X" Then
    '                    Dim commandX1 As MySqlCommand = New MySqlCommand

    '                    commandX1.CommandType = CommandType.Text
    '                    commandX1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                    commandX1.Connection = conn

    '                    Dim drserviceX As MySqlDataReader = commandX1.ExecuteReader()
    '                    Dim dtserviceX As New DataTable
    '                    dtserviceX.Load(drserviceX)

    '                    If dtserviceX.Rows.Count > 0 Then
    '                        Dim commandX2 As MySqlCommand = New MySqlCommand
    '                        commandX2.CommandType = CommandType.Text
    '                        'Exit Sub
    '                        commandX2.CommandText = "update  tblServiceRecord set Status='V' where (Status='O' or Status='' or Status is null)  and SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                        commandX2.Connection = conn
    '                        commandX2.ExecuteNonQuery()
    '                    End If

    '                    Dim commandX As MySqlCommand = New MySqlCommand
    '                    commandX.CommandType = CommandType.Text

    '                    Dim qryX As String = "UPDATE tblContract SET  Status ='X', ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "

    '                    commandX.CommandText = qryX
    '                    commandX.Parameters.Clear()


    '                    If txtActualEndChSt.Text = "" Then
    '                        commandX.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    Else
    '                        strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
    '                        commandX.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
    '                    End If

    '                    commandX.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
    '                    commandX.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
    '                    commandX.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
    '                    commandX.Parameters.AddWithValue("@Rcno", txtRcno.Text)
    '                    commandX.Connection = conn
    '                    commandX.ExecuteNonQuery()
    '                    'Dim message As String = "alert('Contract Cancelled successfully!!!!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

    '                    conn.Close()
    '                    'Dim messageX As String = "alert('Contract Updated successfully!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageX, True)

    '                    GridView1.DataBind()
    '                    'txtServInst.Text = txtCommentChSt.Text
    '                    txtStatus.Text = "X"


    '                    ''''''''''''''''''X

    '                Else
    '                    Dim command1 As MySqlCommand = New MySqlCommand

    '                    command1.CommandType = CommandType.Text
    '                    command1.CommandText = "select 1 from tblServiceRecord where (Status='O' or Status='' or Status is null) and (SchServiceDate)> " & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "  and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"
    '                    command1.Connection = conn

    '                    Dim drserviceE As MySqlDataReader = command1.ExecuteReader()
    '                    Dim dtserviceE As New DataTable
    '                    dtserviceE.Load(drserviceE)

    '                    If dtserviceE.Rows.Count > 0 Then
    '                        Dim command2 As MySqlCommand = New MySqlCommand
    '                        command2.CommandType = CommandType.Text

    '                        Dim qry1 As String = "update  tblServiceRecord set Status= @status where  SchServiceDate>'" & Convert.ToDateTime(txtActualEndChSt.Text).ToString("yyyy-MM-dd") & "' and recordNo in (select recordNo from tblServiceRecordDet where ContractNo ='" & txtContractNo.Text & "')"

    '                        command2.CommandText = qry1
    '                        command2.Parameters.Clear()

    '                        If ddlStatusChSt.SelectedIndex <> "-1" Then
    '                            command2.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
    '                        Else
    '                            command2.Parameters.AddWithValue("@Status", txtStatus.Text)
    '                        End If

    '                        command2.Connection = conn
    '                        command2.ExecuteNonQuery()
    '                    End If



    '                    ''Contract

    '                    Dim command As MySqlCommand = New MySqlCommand
    '                    command.CommandType = CommandType.Text

    '                    Dim qryE As String = "UPDATE tblContract SET  Status = @status, RenewalSt = @RenewalSt, ActualStaff = @ActualStaff, ActualEnd=@ActualEnd ,CommentsStatus = @CommentsStatus where rcno= @rcno "


    '                    command.CommandText = qryE
    '                    command.Parameters.Clear()

    '                    If ddlStatusChSt.SelectedIndex <> "-1" Then
    '                        command.Parameters.AddWithValue("@Status", Left(ddlStatusChSt.Text, 1))
    '                    Else
    '                        command.Parameters.AddWithValue("@Status", txtStatus.Text)
    '                    End If

    '                    If ddlRenewalStatus.SelectedIndex <> "0" Then
    '                        command.Parameters.AddWithValue("@RenewalSt", Left(ddlRenewalStatus.Text, 1))
    '                    Else
    '                        command.Parameters.AddWithValue("@RenewalSt", txtRs.Text)
    '                    End If



    '                    If txtActualEndChSt.Text = "" Then
    '                        command.Parameters.AddWithValue("@ActualEnd", DBNull.Value)
    '                    Else
    '                        strdate = DateTime.Parse(txtActualEndChSt.Text.Trim)
    '                        command.Parameters.AddWithValue("@ActualEnd", strdate.ToString("yyyy-MM-dd"))
    '                    End If


    '                    command.Parameters.AddWithValue("@CommentsStatus", txtCommentChSt.Text.Trim)
    '                    command.Parameters.AddWithValue("@ActualStaff", txtLastModifiedBy.Text)
    '                    command.Parameters.AddWithValue("@LastModifiedOn", DateAndTime.Now.ToString)
    '                    command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
    '                    command.Connection = conn
    '                    command.ExecuteNonQuery()
    '                    ''Dim message As String = "alert('Contract Status Changed successfully!!!!!!')"
    '                    ''ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

    '                    conn.Close()
    '                    'Dim messageE As String = "alert('Contract Updated successfully!!!')"
    '                    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", messageE, True)


    '                    GridView1.DataBind()
    '                    'txtServInst.Text = txtCommentChSt.Text
    '                    txtStatus.Text = Left(ddlStatusChSt.Text, 1)

    '                    If ddlRenewalStatus.SelectedIndex <> "0" Then
    '                        txtRs.Text = Left(ddlRenewalStatus.Text, 1)
    '                    End If



    '                End If '  If Left(ddlStatusChSt.Text, 1) = "E" Then

    '                ddlStatusChSt.SelectedIndex = 0
    '                txtActualEndChSt.Text = ""
    '                txtCommentChSt.Text = ""

    '                GridView1.DataBind()
    '                ModalPopupExtender5.Hide()
    '                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
    '                lblAlert.Text = ""
    '            Else
    '                ModalPopupExtender5.Show()
    '            End If

    'InvalidStatus:
    '            If txtChangeStatus.Text = "000" Then
    '                lblAlert.Text = "SELECTED STATUS IS SAME AS CURRENT STATUS"
    '            ElseIf txtChangeStatus.Text = "001" Then
    '                lblAlert.Text = "CONTRACT STATUS SHOULD BE [O-OPEN]"
    '            ElseIf txtChangeStatus.Text = "002" Then
    '                lblAlert.Text = "PLEASE SELECT VALID STATUS"
    '            ElseIf txtChangeStatus.Text = "003" Then
    '                lblAlert.Text = "ACTUAL END DATE CANNOT BE BLANK"

    '            End If

    '            ModalPopupExtender5.Hide()
    '        Catch ex As Exception
    '            Dim exstr As String
    '            exstr = ex.Message.ToString
    '            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '        End Try
    'End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'lblAlert.Text = ""
        'Try
        '    If txtPostStatus.Text = "P" Then
        '        lblAlert.Text = "Voucher has already been POSTED.. Cannot be DELETED"
        '        'Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
        '        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

        '        Exit Sub
        '    End If

        '    If txtPostStatus.Text = "V" Then
        '        lblAlert.Text = "Voucher is VOID.. Cannot be DELETED"
        '        'Dim message2 As String = "alert('Contract is VOID.. Cannot be DELETED!!!')"
        '        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message2, True)

        '        Exit Sub
        '    End If

        '    lblAlert.Text = ""
        '    lblMessage.Text = "ACTION: DELETE RECORD"


        '    Dim confirmValue As String
        '    confirmValue = ""

        '    confirmValue = Request.Form("confirm_value")
        '    If Right(confirmValue, 3) = "Yes" Then

        '        Dim conn As MySqlConnection = New MySqlConnection()

        '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '        conn.Open()

        '        Dim command1 As MySqlCommand = New MySqlCommand
        '        command1.CommandType = CommandType.Text

        '        Dim qry1 As String = "DELETE from tblCN where Rcno= @Rcno "

        '        command1.CommandText = qry1
        '        command1.Parameters.Clear()

        '        command1.Parameters.AddWithValue("@Rcno", txtRcno.Text)
        '        command1.Connection = conn
        '        command1.ExecuteNonQuery()



        '        'Dim command3 As MySqlCommand = New MySqlCommand
        '        'command3.CommandType = CommandType.Text

        '        'Dim qry3 As String = "DELETE from tblCNDet where ReceiptNumber= @ReceiptNumber "

        '        'command3.CommandText = qry3
        '        'command3.Parameters.Clear()

        '        'command3.Parameters.AddWithValue("@ReceiptNumber", txtCNNo.Text)
        '        'command3.Connection = conn
        '        'command3.ExecuteNonQuery()


        '        'Start:Loop thru' Credit values

        '        Dim commandValues As MySqlCommand = New MySqlCommand

        '        commandValues.CommandType = CommandType.Text
        '        commandValues.CommandText = "SELECT *  FROM tblcndet where CNNumber ='" & txtCNNo.Text.Trim & "'"
        '        commandValues.Connection = conn

        '        Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
        '        Dim dtValues As New DataTable
        '        dtValues.Load(drValues)

        '        Dim lTotalReceiptAmt As Decimal
        '        Dim lInvoiceAmt As Decimal
        '        'Dim lReceptAmtAdjusted As Decimal

        '        lTotalReceiptAmt = 0.0
        '        lInvoiceAmt = 0.0

        '        lTotalReceiptAmt = dtValues.Rows(0)("CNValue").ToString
        '        Dim rowindex = 0

        '        For Each row As DataRow In dtValues.Rows

        '            Dim lContractNo As String
        '            Dim lServiceRecordNo As String
        '            Dim lServiceDate As String

        '            If Convert.ToDecimal(row("CNValue")) > 0.0 Then

        '                Dim commandUpdateInvoiceValue As MySqlCommand = New MySqlCommand

        '                commandUpdateInvoiceValue.CommandType = CommandType.Text
        '                'commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblservicebillingdetailitem where rcno = " & row("RcnoServiceBillingItem")
        '                commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblSalesDetail where rcno = " & row("RcnoServiceBillingItem")

        '                commandUpdateInvoiceValue.Connection = conn

        '                Dim drInvoiceValue As MySqlDataReader = commandUpdateInvoiceValue.ExecuteReader()
        '                Dim dtInvoiceValue As New DataTable
        '                dtInvoiceValue.Load(drInvoiceValue)

        '                lContractNo = ""
        '                lServiceRecordNo = ""
        '                lServiceDate = ""

        '                For Each row1 As DataRow In dtInvoiceValue.Rows

        '                    Dim command3 As MySqlCommand = New MySqlCommand
        '                    command3.CommandType = CommandType.Text

        '                    Dim qry3 As String = "DELETE from tblCNDet where rcno= @rcno "

        '                    command3.CommandText = qry3
        '                    command3.Parameters.Clear()

        '                    command3.Parameters.AddWithValue("@rcno", row("Rcno"))
        '                    command3.Connection = conn
        '                    command3.ExecuteNonQuery()

        '                    'Updatetblservicebillingdetailitem(row("RcnoServiceBillingItem"), row("ServiceRecordNo"))



        '                Next row1
        '            End If
        '        Next row

        '        ''''''''''''''''''''''''''''''''''

        '        conn.Close()
        '        conn.Dispose()
        '        calculateTotalCN()

        '        'conn.Close()



        '        'btnADD_Click(sender, e)

        '        'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
        '        SQLDSCN.SelectCommand = txt.Text
        '        SQLDSCN.DataBind()
        '        'GridView1.DataSourceID = "SqlDSContract"

        '        lblMessage.Text = "DELETE: CREDIT NOTE SUCCESSFULLY DELETED"
        '        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INVOICE", txtCNNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)


        '        MakeMeNull()
        '        MakeMeNullBillingDetails()
        '        FirstGridViewRowGL()
        '        GridView1.DataBind()

        '        'Dim message As String = "alert('Contract is deleted Successfully!!!')"
        '        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

        '        updPnlMsg.Update()
        '        updPnlSearch.Update()
        '        'updpnlServiceRecs.Update()
        '        'GridView1.DataBind()
        '    End If
        'Catch ex As Exception
        '    Dim exstr As String
        '    exstr = ex.Message.ToString

        '    lblAlert.Text = exstr
        '    InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnDelete_Click", ex.Message.ToString, "")
        '    Exit Sub


        'End Try
    End Sub


    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting

        'If GridSelected = "SQLDSContract" Then
        '    SQLDSInvoice.SelectCommand = txt.Text
        '    SQLDSInvoice.DataBind()
        'ElseIf GridSelected = "SQLDSContractClientId" Then
        '    'SqlDataSource1.SelectCommand = txt.Text
        '    'SQLDSContractClientId.DataBind()
        'ElseIf GridSelected = "SQLDSContractNo" Then
        '    ''SqlDataSource1.SelectCommand = txt.Text
        '    'SqlDSContractNo.DataBind()
        'End If

        GridView1.DataBind()
    End Sub




    ''''''' Start: Service Details



    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Session("serviceschedulefrom") = ""
        'Session("contractno") = ""
        'Session("contractdetailfrom") = ""

        'txt.Text = "SELECT * From tblCN where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"

        txtAccountIdSearch.Text = ""
        'txtBillingPeriodSearch.Text = ""
        'txtInvoiceNoSearch.Text = ""
        txtClientNameSearch.Text = ""
        'txtBillingPeriodSearch.Text = ""
        'ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O,P"
        ddlCompanyGrpSearch.SelectedIndex = 0
        'btnSearch1Status_Click(sender, e)
        'SQLDSInvoice.SelectCommand = txt.Text
        'SQLDSInvoice.DataBind()
        btnQuickSearch_Click(sender, e)

        'GridView1.DataSourceID = "SQLDSCN"

        'GridView1.DataBind()


    End Sub


    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        lblMessage.Text = ""
        lblAlert.Text = ""
        'If txtStatus.Text = "O" Then
        '    'btnSave.Enabled = True
        'Else
        '    'btnSave.Enabled = False
        '    lblAlert.Text = "CONTRACT STATUS SHOULD BE [O-OPEN]"
        '    Exit Sub
        'End If
        grvBillingDetailsNew.Enabled = True
        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return
        End If

        lblMessage.Text = "ACTION: EDIT RECORD"

        txtMode.Text = "EDIT"

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        btnPost.Enabled = False
        btnPost.ForeColor = System.Drawing.Color.Gray
        EnableControls()


        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        grvBillingDetails.Visible = True



        If grvBillingDetailsNew.Rows.Count = 0 Then
            'grvBillingDetailsNew.Visible = False
            grvBillingDetails.ShowHeader = True

        Else
            grvBillingDetailsNew.Visible = True
            grvBillingDetails.ShowHeader = False

        End If
        updPanelCN.Update()
    End Sub


    Protected Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click

        'lblMessage.Text = ""
        'If txtRcno.Text = "" Then
        '    lblAlert.Text = "SELECT RECORD TO COPY"
        '    Return

        'End If
        ''DisableControls()
        ''txtMode.Text = "EDIT"
        'lblMessage.Text = "ACTION: COPY RECORD"

        'txtMode.Text = "NEW"
        'EnableControls()
        ''btnDelete.Enabled = True
        ''btnDelete.ForeColor = System.Drawing.Color.Black
        'btnQuit.Enabled = True
        'btnQuit.ForeColor = System.Drawing.Color.Black


        ''If Not (txtStatus.Text = "O" Or txtStatus.Text = "P" Or txtStatus.Text = "E" Or txtStatus.Text = "T" Or txtStatus.Text = "C") Then
        ''    Dim message As String = "alert('Contract Status should be [O/C/E/P/T]!!!')"
        ''    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        ''    Exit Sub
        ''End If



        'txtContractNo.Text = ""
        'txtContractDate.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtConDetVal.Text = "0.00"
        'txtAgreeVal.Text = "0.00"
        'txtContractStart.Text = Now.Date.ToString("dd/MM/yyyy")

        ''txtContractEnd.Text = ""
        'txtServStart.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtServStartDay.Text = DateTime.Parse(txtServStart.Text).DayOfWeek.ToString()
        ''txtServEnd.Text = ""
        ''txtServEndDay.Text = ""
        ''TxtServEndDay.Text = DateTime.Parse(txtServEnd.Text).DayOfWeek.ToString()
        'txtServTimeIn.Text = ""
        'txtServTimeOut.Text = ""
        'txtAllocTime.Text = "0"
        ' ''txtWarrStart.Text = Now.Date.ToString("dd/MM/yyyy")
        ''txtWarrEnd.Text = ""
        'txtValPerMnth.Text = ""
        ''txtContractNotes.Text = ""
        ''txtServInst.Text = ""
        'txtStatus.Text = "O"
        'txtRs.Text = "O"
        'txtNS.Text = "O"
        'txtProspectId.Text = ""
        'txtGS.Text = "O"
        ''txtRemarks.Text = ""
        'txtPrintBody.Text = ""
        ''txtClient.Text = ""
        ''txtContactPerson.Text = ""


    End Sub

    Protected Sub btnQuickSearch_Click(sender As Object, e As EventArgs) Handles btnQuickSearch.Click
        Dim strsql As String = ""
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            txtTotalInvoiceAmount.Text = "0.00"
            txtCondition.Text = ""

            'strsql = "SELECT distinct a.rcno, a.VoucherNumber, a.GLPeriod, a.PostStatus, a.JournalDate, a.DebitBase, a.CreditBase, a.Comments, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, b.Location, b.AccountId, b.CustName, b.RefType FROM tbljrnv a, tbljrnvdet b WHERE 1=1   "
            strsql = "SELECT distinct a.rcno, a.VoucherNumber, a.GLPeriod, a.PostStatus, a.JournalDate, a.DebitBase, a.CreditBase, a.Comments, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn FROM tbljrnv a, tbljrnvdet b WHERE 1=1   "

            txtCondition.Text = ""
            txtCondition1.Text = ""
            txtCondition1.Text = " and a.voucherNumber = b.voucherNumber "

            txtConditionTotal.Text = ""

            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                txtCondition.Text = txtCondition.Text & " and a.PostStatus in (" + YrStr + ")"
                'txtConditionTotal.Text = txtConditionTotal.Text & " and a.PostStatus in (" + YrStr + ")"
            End If

            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    txtCondition.Text = txtCondition.Text + " and Location = '" & txtLocation.Text & "'"
            'End If

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                txtCondition.Text = txtCondition.Text & " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            End If
            'If ddlCompanyGrpSearch.SelectedIndex > 0 Then
            '    If String.IsNullOrEmpty(ddlCompanyGrpSearch.Text) = False Then
            '        txtCondition.Text = txtCondition.Text & " and CompanyGroup = '" & ddlCompanyGrpSearch.Text.Trim + "'"
            '    End If
            'End If

            If String.IsNullOrEmpty(txtInvoiceDateFromSearch.Text) = False And txtInvoiceDateFromSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and a.JournalDate >= '" + Convert.ToDateTime(txtInvoiceDateFromSearch.Text).ToString("yyyy-MM-dd") + "'"
                'txtConditionTotal.Text = txtConditionTotal.Text + " and a.JournalDate >= '" + Convert.ToDateTime(txtInvoiceDateFromSearch.Text).ToString("yyyy-MM-dd") + "'"
            End If

            If String.IsNullOrEmpty(txtInvoiceDateToSearch.Text) = False And txtInvoiceDateToSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and a.JournalDate <= '" + Convert.ToDateTime(txtInvoiceDateToSearch.Text).ToString("yyyy-MM-dd") + "'"
                'txtConditionTotal.Text = txtConditionTotal.Text + " and a.JournalDate <= '" + Convert.ToDateTime(txtInvoiceDateToSearch.Text).ToString("yyyy-MM-dd") + "'"

            End If


            If String.IsNullOrEmpty(txtReceiptnoSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and a.VoucherNumber like '%" & txtReceiptnoSearch.Text.Trim + "%'"
                'txtConditionTotal.Text = txtConditionTotal.Text & " and a.VoucherNumber like '%" & txtReceiptnoSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and b.AccountId like '%" & txtAccountIdSearch.Text.Trim & "%'"
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and b.CustName like ""%" & txtClientNameSearch.Text.Trim & "%"""
            End If


            If String.IsNullOrEmpty(txtInvoiceNoSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and B.RefType like '%" & txtInvoiceNoSearch.Text.Trim & "%'"
            End If

            If String.IsNullOrEmpty(txtLedgerSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and b.LedgerCode like '%" & txtLedgerSearch.Text.Trim & "%'"
            End If


            If String.IsNullOrEmpty(txtAmountSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and A.DebitBase =" & txtAmountSearch.Text
                'txtConditionTotal.Text = txtConditionTotal.Text & " and A.DebitBase =" & txtAmountSearch.Text
            End If

            txtOrderBy.Text = " order by a.rcno desc "
            txt.Text = strsql
            'txtComments.Text = strsql

            'SQLDSCN.SelectCommand = strsql
            'SQLDSCN.DataBind()
            'GridView1.DataBind()

            strsql = strsql + txtCondition1.Text + txtCondition.Text + txtOrderBy.Text + " limit " & txtLimit.Text
            txt.Text = strsql

            SQLDSCN.SelectCommand = strsql
            SQLDSCN.DataBind()
            GridView1.DataBind()

            CalculateTotal()

            If GridView1.Rows.Count > 0 Then
                Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
                MakeMeNull()
                MakeMeNullBillingDetails()
                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
                GridView1.SelectedIndex = 0
                'GridView1_SelectedIndexChanged(sender, e)
            End If

            lblMessage.Text = "NUMBER OF RECORDS FOUND : " + txtRowCount.Text + "   [DISPLAYING TOP " + txtLimit.Text + " RECORDS]"
            updPnlMsg.Update()

            'GridSelected = "SQLDSContract"
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnQuickSearch_Click", ex.Message.ToString, Left(strsql, 10000))
            Exit Sub
        End Try
    End Sub



    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs)
        lblAlert.Text = ""

        'txtContType1.Text = "CORPORATE"
        'txtContType2.Text = "COMPANY"
        'txtContType3.Text = "RESIDENTIAL"
        'txtContType4.Text = "PERSON"

        'mdlPopUpClient.Show()
    End Sub

    Protected Sub btnShowRecords_Click(sender As Object, e As EventArgs)
        'PopulateServiceGrid()
        'btnSave.Enabled = True
        'updPanelSave.Update()
    End Sub

    Private Sub PopulateServiceGrid()

        Try
            Dim sql As String
            sql = ""
            'If ddlContactTypeIS.SelectedIndex = 1 Then
            '    'sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '    'sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, "
            '    'sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '    'sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '    'sql = sql + " FROM tblservicerecord A, tblCompany B, tblContract C "
            '    'sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup <> 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"

            '    If ddlServiceFrequency.Text.Trim = "-1" Then
            '        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '        sql = sql + " (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) as Billamount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup,  A.ServiceBy, A.ServiceLocationGroup,"
            '        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '        sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblCompany B"
            '        sql = sql + " where 1 = 1 and (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) > 0 and  (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId   and A.ContactType = '" & ddlContactType.Text.Trim & "'"
            '    Else
            '        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '        sql = sql + " (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) as Billamount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup,  A.ServiceBy, A.ServiceLocationGroup,"
            '        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '        sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblCompany B, tblcontractdet D "
            '        sql = sql + " where 1 = 1 and (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) > 0 and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and C.ContractNo = D.ContractNo  and A.ContactType = '" & ddlContactType.Text.Trim & "'"
            '    End If

            'ElseIf ddlContactTypeIS.SelectedIndex = 2 Then
            '    'sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '    'sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, "
            '    'sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '    'sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '    'sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C "
            '    'sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and C.ConTractGroup <> 'ST' and A.ContactType = '" & ddlContactType.Text.Trim & "'"

            '    If ddlServiceFrequency.Text.Trim = "-1" Then
            '        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '        sql = sql + " (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) as Billamount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup,  A.ServiceBy, A.ServiceLocationGroup, "
            '        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '        sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblPerson B "
            '        sql = sql + " where 1 = 1 and (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) > 0  and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and A.ContactType = '" & ddlContactType.Text.Trim & "'"
            '    Else
            '        sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
            '        sql = sql + " (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) as Billamount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.ServiceBy, A.ServiceLocationGroup, "
            '        sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
            '        sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup  "
            '        sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblPerson B, tblcontractdet D "
            '        sql = sql + " where 1 = 1 and (A.BillAmount - A.BilledAmt - A.TotalCreditAmount) > 0  and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and C.ContractNo = D.ContractNo and  A.ContactType = '" & ddlContactType.Text.Trim & "'"

            '    End If

            'End If



            sql = sql + "Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount, A.ServiceDate, A.BilledAmt  FROM tblServiceRecord A  where (Billno is not null and Billno <> '')  "

            If String.IsNullOrEmpty(txtAccountId.Text) = False Then
                sql = sql + " and  A.AccountID = '" & txtAccountId.Text & "'"
            Else
                If String.IsNullOrEmpty(txtClientName.Text) = False Then
                    sql = sql + " and  A.ServiceName like ""%" & txtClientName.Text & "%"""
                End If
            End If

            If ddlCompanyGrp.Text.Trim <> "--SELECT--" Then
                sql = sql + " and   A.CompanyGroup = '" & ddlCompanyGrp.Text.Trim & "'"
            End If

            'If ddlContractNo.Text.Trim <> "-1" Then
            If String.IsNullOrEmpty(ddlContractNo.Text) = False Then
                sql = sql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
            End If

            If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = True Then
                sql = sql + " and   A.ServiceDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateTo.Text) = False And String.IsNullOrEmpty(txtDateFrom.Text) = True Then
                sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                sql = sql + " and   A.ServiceDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtLocationId.Text) = False Then
                sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
            End If

            If String.IsNullOrEmpty(txtInvoiceNo.Text) = False Then
                sql = sql + " and   A.BillNo = '" & txtInvoiceNo.Text & "'"
            End If

            'If ddlServiceFrequency.Text.Trim <> "-1" Then
            '    sql = sql + " and   D.Frequency = '" & ddlServiceFrequency.Text.Trim & "'"
            'End If

            'If ddlBillingFrequency.Text.Trim <> "-1" Then
            '    sql = sql + " and   C.BillingFrequency = '" & ddlBillingFrequency.Text.Trim & "'"
            'End If

            'If ddlContractGroup.Text.Trim <> "-1" Then
            '    sql = sql + " and   C.ContractGroup = '" & ddlContractGroup.Text.Trim & "'"
            'End If


            'If String.IsNullOrEmpty(txtServiceBy.Text) = False Then
            '    sql = sql + " and   A.ServiceBy like '%" & txtServiceBy.Text & "%'"
            'End If

            If rdbCompleted.Checked = True Then
                sql = sql + " and  A.Status = 'P' "
            End If

            If rdbNotCompleted.Checked = True Then
                sql = sql + " and  A.Status = 'O' "
            End If


            SqlDSServices.SelectCommand = sql
            grvServiceRecDetails.DataSourceID = "SqlDSServices"
            grvServiceRecDetails.DataBind()

            'If ChkServicesAll.Checked = True Then
            '    sql = sql + " and  A.Status = 'C'"
            'End If

            'If rbtServicestatus.SelectedValue = "C" Then
            '    sql = sql + " and  A.Status = 'P'"
            'End If

            'If rbtServicestatus.SelectedValue = "N" Then
            '    sql = sql + " and  A.Status = 'O'"
            'End If
            'txtInchargeSearch.Text = sql

            'End If

            'If rdbGrouping.SelectedIndex = 0 Then
            '    Sql = Sql + " order by A.ContractNo, A.ServiceDate"
            'ElseIf rdbGrouping.SelectedIndex = 1 Then
            '    Sql = Sql + " order by A.AccountID, A.LocationId"
            'ElseIf rdbGrouping.SelectedIndex = 2 Then
            '    Sql = Sql + " order by A.AccountID, A.ServiceDate"
            'ElseIf rdbGrouping.SelectedIndex = 3 Then
            'End If
            'End: Service Recods


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PopulateServiceGrid", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    '''''''''' Start: Service Record




    'Start: Billing Details Grid


    Private Sub FirstGridViewRowBillingDetailsRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing

            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            'dt.Columns.Add(New DataColumn("SelRec", GetType(String)))

            dt.Columns.Add(New DataColumn("ItemType", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("Description", GetType(String)))
            dt.Columns.Add(New DataColumn("OtherCode", GetType(String)))
            dt.Columns.Add(New DataColumn("GLDescription", GetType(String)))

            dt.Columns.Add(New DataColumn("UOM", GetType(String)))
            dt.Columns.Add(New DataColumn("Qty", GetType(String)))
          
            dt.Columns.Add(New DataColumn("ContactType", GetType(String)))
            dt.Columns.Add(New DataColumn("CustCode", GetType(String)))
            dt.Columns.Add(New DataColumn("AccountID", GetType(String)))
            dt.Columns.Add(New DataColumn("CustName", GetType(String)))
            dt.Columns.Add(New DataColumn("DebitBase", GetType(String)))
            dt.Columns.Add(New DataColumn("CreditBase", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoJournalDetail", GetType(String)))
            dt.Columns.Add(New DataColumn("LocationId", GetType(String)))
            dt.Columns.Add(New DataColumn("CompanyGroup", GetType(String)))
            dt.Columns.Add(New DataColumn("Location", GetType(String)))

            dr = dt.NewRow()

            dr("ItemType") = String.Empty
            dr("InvoiceNo") = String.Empty
            dr("Description") = String.Empty
            dr("OtherCode") = 0
            dr("GLDescription") = String.Empty

            dr("UOM") = String.Empty
            dr("Qty") = 0
            dr("ContactType") = ""
            dr("CustCode") = ""
            dr("AccountID") = ""
            dr("CustName") = ""
            dr("DebitBase") = 0
            dr("CreditBase") = 0
            dr("RcnoJournalDetail") = 0
            dr("LocationId") = String.Empty
            dr("CompanyGroup") = String.Empty
            dr("Location") = String.Empty

            dt.Rows.Add(dr)

            ViewState("CurrentTableBillingDetailsRec") = dt

            grvBillingDetails.DataSource = dt
            grvBillingDetails.DataBind()

            Dim btnAdd As Button = CType(grvBillingDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "FirstGridViewRowBillingDetailsRecs", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub AddNewRowBillingDetailsRecs()
        Try
            Dim rowIndex As Integer = 0
            Dim Query As String

            If ViewState("CurrentTableBillingDetailsRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        'Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDescriptionGV"), TextBox)
                        Dim TextBoxUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), TextBox)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxContactType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContactTypeGV"), DropDownList)
                        Dim TextBoxCustCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustCodeGV"), TextBox)
                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                        Dim TextBoxCustName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustNameGV"), TextBox)
                        Dim TextBoxDebitBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDebitBaseGV"), TextBox)
                        Dim TextBoxCreditBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCreditBaseGV"), TextBox)
                        Dim TextBoxRcnoJournalDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoJournalDetailGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocationID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        'TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N2")

                        If String.IsNullOrEmpty(TextBoxQty.Text.Trim) = True Then
                            TextBoxQty.Text = "1"
                        End If

                      
                        TextBoxQty.Text = "1.00"
                      
                        'TextBoxOtherCode.Text = txtGLCodeIS.Text
                        'TextBoxGLDescription.Text = txtLedgerNameIS.Text

                        'dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("Description") = TextBoxDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CustCode") = TextBoxCustCode.Text
                        dtCurrentTable.Rows(i - 1)("AccountID") = TextBoxAccountID.Text
                        dtCurrentTable.Rows(i - 1)("CustName") = TextBoxCustName.Text
                        dtCurrentTable.Rows(i - 1)("DebitBase") = TextBoxDebitBase.Text
                        dtCurrentTable.Rows(i - 1)("CreditBase") = TextBoxCreditBase.Text
                        dtCurrentTable.Rows(i - 1)("RcnoJournalDetail") = TextBoxRcnoJournalDetail.Text

                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("LocationID") = TextBoxLocationID.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text

                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable

                    grvBillingDetails.DataSource = dtCurrentTable
                    grvBillingDetails.DataBind()


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub SetPreviousDataBillingDetailsRecs()
        Try
            Dim rowIndex As Integer = 0

            'Dim Query As String

            If ViewState("CurrentTableBillingDetailsRec") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                         'Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDescriptionGV"), TextBox)
                        Dim TextBoxUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), TextBox)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxContactType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContactTypeGV"), DropDownList)
                        Dim TextBoxCustCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustCodeGV"), TextBox)
                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                        Dim TextBoxCustName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustNameGV"), TextBox)
                        Dim TextBoxDebitBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDebitBaseGV"), TextBox)
                        Dim TextBoxCreditBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCreditBaseGV"), TextBox)
                        Dim TextBoxRcnoJournalDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoJournalDetailGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocationID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)


                        TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        TextBoxInvoiceNo.Text = dt.Rows(i)("InvoiceNo").ToString()
                        TextBoxDescription.Text = dt.Rows(i)("Description").ToString()
                        TextBoxUOM.Text = dt.Rows(i)("UOM").ToString().ToString()
                        TextBoxQty.Text = dt.Rows(i)("Qty").ToString().ToString()
                        TextBoxContactType.Text = dt.Rows(i)("ContactType").ToString()
                        TextBoxCustCode.Text = dt.Rows(i)("CustCode").ToString()
                        TextBoxAccountID.Text = dt.Rows(i)("AccountID").ToString()
                        TextBoxCustName.Text = dt.Rows(i)("CustName").ToString()
                        TextBoxDebitBase.Text = dt.Rows(i)("DebitBase").ToString()
                        TextBoxCreditBase.Text = dt.Rows(i)("CreditBase").ToString()
                        TextBoxRcnoJournalDetail.Text = dt.Rows(i)("RcnoJournalDetail").ToString()

                        TextBoxOtherCode.Text = dt.Rows(i)("OtherCode").ToString()
                        TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
                        TextBoxLocationID.Text = (dt.Rows(i)("LocationID").ToString())
                        TextBoxCompanyGroup.Text = dt.Rows(i)("CompanyGroup").ToString
                        TextBoxLocation.Text = dt.Rows(i)("Location").ToString
                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetRowDataBillingDetailsRecs()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTableBillingDetailsRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                       'Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDescriptionGV"), TextBox)
                        Dim TextBoxUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), TextBox)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxContactType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContactTypeGV"), DropDownList)
                        Dim TextBoxCustCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustCodeGV"), TextBox)
                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                        Dim TextBoxCustName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustNameGV"), TextBox)
                        Dim TextBoxDebitBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDebitBaseGV"), TextBox)
                        Dim TextBoxCreditBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCreditBaseGV"), TextBox)
                        Dim TextBoxRcnoJournalDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoJournalDetailGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocationID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                       dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("Description") = TextBoxDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CustCode") = TextBoxCustCode.Text
                        dtCurrentTable.Rows(i - 1)("AccountID") = TextBoxAccountID.Text
                        dtCurrentTable.Rows(i - 1)("CustName") = TextBoxCustName.Text
                        dtCurrentTable.Rows(i - 1)("DebitBase") = TextBoxDebitBase.Text
                        dtCurrentTable.Rows(i - 1)("CreditBase") = TextBoxCreditBase.Text
                        dtCurrentTable.Rows(i - 1)("RcnoJournalDetail") = TextBoxRcnoJournalDetail.Text

                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("LocationID") = TextBoxLocationID.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable
                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'End: Biling Details Grid

    'Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs)
    '    lblAlert.Text = ""
    '    If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
    '        '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
    '        lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
    '        Exit Sub
    '    End If


    '    If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
    '        txtPopUpClient.Text = txtAccountId.Text
    '        txtPopupClientSearch.Text = txtPopUpClient.Text

    '        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
    '        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like '%" + txtPopupClientSearch.Text.Trim.ToUpper + "%' or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
    '        End If

    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '    Else

    '        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '            SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1  order by name"
    '        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '            SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1  order by name"
    '        End If


    '        SqlDSClient.DataBind()
    '        gvClient.DataBind()
    '    End If
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub btnPopUpClientSearch_Click(sender As Object, e As ImageClickEventArgs)
    'If txtPopUpClient.Text.Trim = "" Then
    '    MessageBox.Message.Alert(Page, "Please enter client name", "str")
    'Else
    '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
    '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"

    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End If
    'End Sub

    'Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging

    '    gvClient.PageIndex = e.NewPageIndex




    '    If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
    '        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany order by name"
    '        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON order by name"
    '        End If
    '    Else
    '        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
    '        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '            SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
    '        End If

    '        'SqlDSClient.DataBind()
    '        'gvClient.DataBind()
    '        'mdlPopUpClient.Show()
    '    End If

    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    '    'End If

    'End Sub



    Private Sub CalculatePrice()
        'Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
        'Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGV"), TextBox)
        'Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceGV"), TextBox)

        'Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGV"), TextBox)
        'Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGV"), TextBox)
        'Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGV"), TextBox)

        'Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
        'Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTAmtGV"), TextBox)
        'Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceWithGSTGV"), TextBox)

        'Dim dblQty As String
        'Dim dblPricePerUOM As String
        'Dim dblTotalPrice As String

        'Dim dblDiscPerc As String
        'Dim dblDisAmount As String
        'Dim dblPriceWithDisc As String

        'Dim dblGSTPerc As String
        'Dim dblGSTAmt As String
        'Dim dblTotalPriceWithGST As String


        'If String.IsNullOrEmpty(lblid1.Text) = True Then
        '    lblid1.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid2.Text) = True Then
        '    lblid2.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid3.Text) = True Then
        '    lblid3.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid4.Text) = True Then
        '    lblid4.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid5.Text) = True Then
        '    lblid5.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid6.Text) = True Then
        '    lblid6.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid7.Text) = True Then
        '    lblid7.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid8.Text) = True Then
        '    lblid8.Text = "0.00"
        'End If

        'If String.IsNullOrEmpty(lblid9.Text) = True Then
        '    lblid9.Text = "0.00"
        'End If


        'dblQty = (lblid1.Text)
        'dblPricePerUOM = (lblid2.Text)
        'dblTotalPrice = (lblid3.Text)

        'dblDiscPerc = (lblid4.Text)
        'dblDisAmount = (lblid5.Text)
        'dblPriceWithDisc = (lblid6.Text)

        'dblGSTPerc = (lblid7.Text)
        'dblGSTAmt = (lblid8.Text)
        'dblTotalPriceWithGST = (lblid9.Text)

        'lblid3.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
        'lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
        'lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
        'lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid6.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
        'lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid6.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

        CalculateTotalPrice()


    End Sub

    Private Sub CalculateTotalPrice()
        Try

            Dim TotalAmt As Decimal = 0
            'Dim TotalDiscAmt As Decimal = 0
            'Dim TotalWithDiscAmt As Decimal = 0
            'Dim TotalGSTAmt As Decimal = 0
            'Dim TotalAmtWithGST As Decimal = 0
            'Dim GSTableGVB As Decimal = 0.0
            'Dim GSTGVB As Decimal = 0.0
            'Dim GSTGV As Decimal = 0.0
            'Dim TotalPrice As Decimal = 0.0

            'If txtMode.Text = "EDIT" Then
            'txtCreditAmount.Text = "0.00"
            'txtDiscountAmount.Text = "0.00"
            'txtAmountWithDiscount.Text = "0.00"
            txtDebitAmount.Text = "0.00"
            txtCreditAmount.Text = "0.00"
            'End If

            Dim lGSTadjustedRecNo As Integer
            Dim lGSTadjustedRecNoNew As Integer

            lGSTadjustedRecNo = 0
            lGSTadjustedRecNoNew = 0


            ' ''''''''''''''''''''''''''''''''start Modification'''''''''''''''''''''''''''''''''''''

            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim GSTable As Decimal = 0.0

            If (table.Rows.Count > 0) Then

                For i As Integer = (table.Rows.Count) - 1 To 0 Step -1
                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtItemTypeGV"), DropDownList)

                    If TextBoxItemType.SelectedValue <> "-1" Then
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtQtyGV"), TextBox)
                        'Dim TextBoxPricePerUOMGV As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                        'Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                        'Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                        'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxDebitBase As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDebitBaseGV"), TextBox)
                        Dim TextBoxCreditBase As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtCreditBaseGV"), TextBox)

                        txtDebitAmount.Text = (Convert.ToDecimal(txtDebitAmount.Text) + Convert.ToDecimal(TextBoxDebitBase.Text)).ToString("N2")
                        txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxCreditBase.Text)).ToString("N2")


                        'Dim TextBoxTaxCode As DropDownList = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTaxTypeGV"), DropDownList)

                        'If ddlDocType.Text = "ARCN" Then
                        '    If String.IsNullOrEmpty(TextBoxQty.Text) = True Then
                        '        TextBoxQty.Text = "-1"
                        '    End If
                        'Else
                        '    If String.IsNullOrEmpty(TextBoxQty.Text) = True Then
                        '        TextBoxQty.Text = "1"
                        '    End If
                        'End If

                        'If String.IsNullOrEmpty(TextBoxPricePerUOMGV.Text) = True Then
                        '    TextBoxPricePerUOMGV.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPrice.Text) = True Then
                        '    TextBoxTotalPrice.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxDiscAmount.Text) = True Then
                        '    TextBoxDiscAmount.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxDiscPerc.Text) = True Then
                        '    TextBoxDiscPerc.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxPriceWithDisc.Text) = True Then
                        '    TextBoxPriceWithDisc.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPrice.Text) = False Then
                        '    TextBoxTotalPrice.Text = Convert.ToDecimal(TextBoxQty.Text) * Convert.ToDecimal(TextBoxPricePerUOMGV.Text)
                        'End If

                        'If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                        '    TextBoxGSTAmt.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPriceWithGST.Text) = True Then
                        '    TextBoxTotalPriceWithGST.Text = "0.00"
                        'End If

                        'If TextBoxUOM.SelectedIndex = 0 Then
                        '    TextBoxUOM.Text = "NO"
                        'End If

                        'If TextBoxTaxCode.Text = "SR" Then
                        '    GSTable = GSTable + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

                        '    If lGSTadjustedRecNo = 0 Then
                        '        lGSTadjustedRecNo = i
                        '    End If
                        'End If

                      
                        'txtInvoiceAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(TextBoxTotalPrice.Text)).ToString("N2")
                        'txtDiscountAmount.Text = (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(TextBoxDiscAmount.Text)).ToString("N2")

                        'TotalPrice = TotalPrice + (Convert.ToDecimal(TextBoxTotalPrice.Text))
                        'GSTGV = (Convert.ToDecimal(GSTable) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")

                        'txtNetAmount.Text = (Convert.ToDecimal(txtAmountWithDiscount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")
                        'UpdatePanel3.Update()
                    End If
                Next i
            End If



            ' '' start of GVB
            If txtMode.Text = "EDIT" Then
                Dim gvbRecords, j As Long
                gvbRecords = grvBillingDetailsNew.Rows.Count()

                For j = gvbRecords - 1 To 0 Step -1


                    Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtItemTypeGVB"), TextBox)
                    Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtOtherCodeGVB"), TextBox)


                    If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then
                        'Dim TextBoxUOMGVB As DropDownList = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtUOMGVB"), DropDownList)
                        'Dim TextBoxQtyGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtQtyGVB"), TextBox)
                        'Dim TextBoxPricePerUOMGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtPricePerUOMGVB"), TextBox)
                        'Dim TextBoxTotalPriceGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceGVB"), TextBox)
                        'Dim TextBoxDiscAmountGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtDiscAmountGVB"), TextBox)
                        'Dim TextBoxGSTAmtGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtGSTAmtGVB"), TextBox)
                        'Dim TextBoxTotalPriceWithGSTGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceWithGSTGVB"), TextBox)
                        'Dim TextBoxDiscPercGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtDiscPercGVB"), TextBox)
                        'Dim TextBoxPriceWithDiscGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtPriceWithDiscGVB"), TextBox)
                        'Dim TextBoxTaxCodeGVB As DropDownList = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTaxTypeGVB"), DropDownList)

                        Dim TextBoxDebitBase As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtDebitBaseGVB"), TextBox)
                        Dim TextBoxCreditBase As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtCreditBaseGVB"), TextBox)

                        txtDebitAmount.Text = (Convert.ToDecimal(txtDebitAmount.Text) + Convert.ToDecimal(TextBoxDebitBase.Text)).ToString("N2")
                        txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxCreditBase.Text)).ToString("N2")



                        'If String.IsNullOrEmpty(TextBoxQtyGVB.Text) = True Then
                        '    TextBoxQtyGVB.Text = "1"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxPricePerUOMGVB.Text) = True Then
                        '    TextBoxPricePerUOMGVB.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPriceGVB.Text) = True Then
                        '    TextBoxTotalPriceGVB.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxDiscAmountGVB.Text) = True Then
                        '    TextBoxDiscAmountGVB.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxDiscPercGVB.Text) = True Then
                        '    TextBoxDiscPercGVB.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxPriceWithDiscGVB.Text) = True Then
                        '    TextBoxPriceWithDiscGVB.Text = "0"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPriceGVB.Text) = False Then
                        '    TextBoxTotalPriceGVB.Text = (Convert.ToDecimal(TextBoxQtyGVB.Text) * Convert.ToDecimal(TextBoxPricePerUOMGVB.Text)).ToString("N2")
                        'End If

                        'If String.IsNullOrEmpty(TextBoxGSTAmtGVB.Text) = True Then
                        '    TextBoxGSTAmtGVB.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalPriceWithGSTGVB.Text) = True Then
                        '    TextBoxTotalPriceWithGSTGVB.Text = "0.00"
                        'End If

                        'If TextBoxUOMGVB.SelectedIndex = 0 Then
                        '    TextBoxUOMGVB.Text = "NO"
                        'End If

                        'If TextBoxTaxCodeGVB.Text = "SR" Then
                        '    GSTableGVB = GSTableGVB + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)

                        '    If lGSTadjustedRecNoNew = 0 And lGSTadjustedRecNo = 0 Then
                        '        lGSTadjustedRecNoNew = j
                        '    End If
                        'End If

                        'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)).ToString("N2")
                        ''txtDiscountAmount.Text = (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(TextBoxDiscAmountGVB.Text)).ToString("N2")

                        ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
                        ''txtGSTAmount.Text = (Convert.ToDecimal(txtGSTAmount.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")
                        ''txtNetAmount.Text = (Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)).ToString("N2")

                        ''txtGSTAmount.Text = (Convert.ToDecimal(GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
                        'GSTGVB = (Convert.ToDecimal(GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")

                        ''txtNetAmount.Text = (Convert.ToDecimal(txtAmountWithDiscount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")
                        'GSTGVB = Convert.ToDecimal(txtGSTAmount.Text)

                        'txtCreditAmount.Text = Convert.ToDecimal(TotalPrice.ToString("N2"))
                        'txtCNGSTAmount.Text = GSTGVB + GSTGV
                        'txtCreditAmount.Text = Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtCNGSTAmount.Text)

                        'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)).ToString("N2")
                    End If
                Next
            End If



            '' '' end of GVB
            ' ''''''''''''''''''''''''''''''''''''end Modification ''''''''''''''''''''''''''''''''


            ''''''''''''''''''''''''''''''''''''''  ORIGINAL ''''''''''''''''''''''''''''''''''''''''
            ' '' GVB
            'If txtMode.Text = "EDIT" Then
            '    Dim gvbRecords, j As Long
            '    gvbRecords = grvBillingDetailsNew.Rows.Count()

            '    For j = 0 To gvbRecords - 1


            '        Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtItemTypeGVB"), TextBox)
            '        Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtOtherCodeGVB"), TextBox)


            '        If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then
            '            Dim TextBoxUOMGVB As DropDownList = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtUOMGVB"), DropDownList)
            '            Dim TextBoxQtyGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtQtyGVB"), TextBox)
            '            Dim TextBoxPricePerUOMGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtPricePerUOMGVB"), TextBox)
            '            Dim TextBoxTotalPriceGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceGVB"), TextBox)
            '            Dim TextBoxDiscAmountGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtDiscAmountGVB"), TextBox)
            '            Dim TextBoxGSTAmtGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtGSTAmtGVB"), TextBox)
            '            Dim TextBoxTotalPriceWithGSTGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceWithGSTGVB"), TextBox)
            '            Dim TextBoxDiscPercGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtDiscPercGVB"), TextBox)
            '            Dim TextBoxPriceWithDiscGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtPriceWithDiscGVB"), TextBox)
            '            Dim TextBoxTaxCodeGVB As DropDownList = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTaxTypeGVB"), DropDownList)

            '            If String.IsNullOrEmpty(TextBoxQtyGVB.Text) = True Then
            '                TextBoxQtyGVB.Text = "1"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxPricePerUOMGVB.Text) = True Then
            '                TextBoxPricePerUOMGVB.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPriceGVB.Text) = True Then
            '                TextBoxTotalPriceGVB.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxDiscAmountGVB.Text) = True Then
            '                TextBoxDiscAmountGVB.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxDiscPercGVB.Text) = True Then
            '                TextBoxDiscPercGVB.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxPriceWithDiscGVB.Text) = True Then
            '                TextBoxPriceWithDiscGVB.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPriceGVB.Text) = False Then
            '                TextBoxTotalPriceGVB.Text = (Convert.ToDecimal(TextBoxQtyGVB.Text) * Convert.ToDecimal(TextBoxPricePerUOMGVB.Text)).ToString("N2")
            '            End If

            '            If String.IsNullOrEmpty(TextBoxGSTAmtGVB.Text) = True Then
            '                TextBoxGSTAmtGVB.Text = "0.00"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPriceWithGSTGVB.Text) = True Then
            '                TextBoxTotalPriceWithGSTGVB.Text = "0.00"
            '            End If

            '            If TextBoxUOMGVB.SelectedIndex = 0 Then
            '                TextBoxUOMGVB.Text = "NO"
            '            End If

            '            If TextBoxTaxCodeGVB.Text = "SR" Then
            '                GSTableGVB = GSTableGVB + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)
            '            End If

            '            'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)).ToString("N2")
            '            ''txtDiscountAmount.Text = (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(TextBoxDiscAmountGVB.Text)).ToString("N2")

            '            ''txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
            '            ''txtGSTAmount.Text = (Convert.ToDecimal(txtGSTAmount.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")
            '            ''txtNetAmount.Text = (Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)).ToString("N2")

            '            ''txtGSTAmount.Text = (Convert.ToDecimal(GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
            '            GSTGVB = (Convert.ToDecimal(GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")

            '            ''txtNetAmount.Text = (Convert.ToDecimal(txtAmountWithDiscount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")
            '            'GSTGVB = Convert.ToDecimal(txtGSTAmount.Text)

            '            'txtCreditAmount.Text = Convert.ToDecimal(TotalPrice.ToString("N2"))
            '            'txtCNGSTAmount.Text = GSTGVB + GSTGV
            '            'txtCreditAmount.Text = Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtCNGSTAmount.Text)

            '            'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)).ToString("N2")
            '        End If
            '    Next
            'End If



            ' '' GVB

            'Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            'Dim GSTable As Decimal = 0.0

            'If (table.Rows.Count > 0) Then

            '    For i As Integer = 0 To (table.Rows.Count) - 1
            '        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtItemTypeGV"), DropDownList)

            '        If TextBoxItemType.SelectedValue <> "-1" Then
            '            Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtUOMGV"), DropDownList)
            '            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtQtyGV"), TextBox)
            '            Dim TextBoxPricePerUOMGV As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
            '            Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
            '            Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
            '            Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
            '            Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
            '            Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDiscPercGV"), TextBox)
            '            Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
            '            Dim TextBoxTaxCode As DropDownList = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTaxTypeGV"), DropDownList)

            '            'If ddlDocType.Text = "ARCN" Then
            '            '    If String.IsNullOrEmpty(TextBoxQty.Text) = True Then
            '            '        TextBoxQty.Text = "-1"
            '            '    End If
            '            'Else
            '            '    If String.IsNullOrEmpty(TextBoxQty.Text) = True Then
            '            TextBoxQty.Text = "1"
            '            '    End If
            '            'End If

            '            If String.IsNullOrEmpty(TextBoxPricePerUOMGV.Text) = True Then
            '                TextBoxPricePerUOMGV.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPrice.Text) = True Then
            '                TextBoxTotalPrice.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxDiscAmount.Text) = True Then
            '                TextBoxDiscAmount.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxDiscPerc.Text) = True Then
            '                TextBoxDiscPerc.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxPriceWithDisc.Text) = True Then
            '                TextBoxPriceWithDisc.Text = "0"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPrice.Text) = False Then
            '                TextBoxTotalPrice.Text = Convert.ToDecimal(TextBoxQty.Text) * Convert.ToDecimal(TextBoxPricePerUOMGV.Text)
            '            End If

            '            If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
            '                TextBoxGSTAmt.Text = "0.00"
            '            End If

            '            If String.IsNullOrEmpty(TextBoxTotalPriceWithGST.Text) = True Then
            '                TextBoxTotalPriceWithGST.Text = "0.00"
            '            End If

            '            If TextBoxUOM.SelectedIndex = 0 Then
            '                TextBoxUOM.Text = "NO"
            '            End If

            '            If TextBoxTaxCode.Text = "SR" Then
            '                GSTable = GSTable + Convert.ToDecimal(TextBoxPriceWithDisc.Text)
            '            End If

            '            txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TextBoxTotalPrice.Text)).ToString("N2")
            '            'txtInvoiceAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(TextBoxTotalPrice.Text)).ToString("N2")
            '            'txtDiscountAmount.Text = (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(TextBoxDiscAmount.Text)).ToString("N2")

            '            TotalPrice = TotalPrice + (Convert.ToDecimal(TextBoxTotalPrice.Text))
            '            GSTGV = (Convert.ToDecimal(GSTable) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")

            '            'txtNetAmount.Text = (Convert.ToDecimal(txtAmountWithDiscount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")
            '            'UpdatePanel3.Update()
            '        End If

            '        ''''''''''''''''''''''''''''''
            '    Next i

            'End If
            ''''''''''''''''''''''''''''''''''''''  ORIGINAL ''''''''''''''''''''''''''''''''''''''''
            'txtGSTAmount.Text = GSTGVB + txtGSTAmount.Text
            'txtCreditAmount.Text = Convert.ToDecimal(TotalPrice.ToString("N2"))

            'txtCNGSTAmount.Text = GSTGV
            'txtCNNetAmount.Text = Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtCNGSTAmount.Text)

            'txtDebitAmount.Text = GSTGVB + GSTGV

            ' ''''''''''''''''''''''''''
            ''txtComments.Text = lGSTadjustedRecNo & lGSTadjustedRecNoNew

            'Dim GSTDiff As Decimal
            'GSTDiff = 0.0
            'GSTDiff = ((GSTable + GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01) - Convert.ToDecimal(txtCNGSTAmount.Text)

            'If GSTDiff <> 0.0 Then
            '    'txtComments.Text = txtComments.Text & "-" & GSTDiff

            '    txtCNGSTAmount.Text = Convert.ToDecimal(Convert.ToDecimal(txtCNGSTAmount.Text) + GSTDiff.ToString("N2")).ToString("N2")

            '    If lGSTadjustedRecNo > 0 Then
            '        Dim adjGST As TextBox = CType(grvBillingDetails.Rows(lGSTadjustedRecNo).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
            '        Dim adjTotal As TextBox = CType(grvBillingDetails.Rows(lGSTadjustedRecNo).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)

            '        adjGST.Text = Convert.ToDecimal(Convert.ToDecimal(adjGST.Text).ToString("N2") + GSTDiff).ToString("N2")
            '        adjTotal.Text = Convert.ToDecimal(Convert.ToDecimal(adjTotal.Text).ToString("N2") + GSTDiff).ToString("N2")

            '    ElseIf lGSTadjustedRecNoNew > 0 Then
            '        Dim adjGSTNew As TextBox = CType(grvBillingDetailsNew.Rows(lGSTadjustedRecNoNew).Cells(0).FindControl("txtTotalPriceWithGSTGVB"), TextBox)
            '        Dim adjTotalNew As TextBox = CType(grvBillingDetailsNew.Rows(lGSTadjustedRecNoNew).Cells(0).FindControl("txtGSTAmtGVB"), TextBox)

            '        adjGSTNew.Text = Convert.ToDecimal(Convert.ToDecimal(adjGSTNew.Text).ToString("N2") + GSTDiff).ToString("N2")
            '        adjTotalNew.Text = Convert.ToDecimal(Convert.ToDecimal(adjTotalNew.Text).ToString("N2") + GSTDiff).ToString("N2")
            '    End If
            'End If

            ' ''''''''''''''''''''''''''''

            'txtCreditAmount.Text = Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtDebitAmount.Text)


            'UpdatePanel3.Update()

            updPnlBillingRecs.Update()
            'txtTotal.Text = TotalAmt.ToString
            'txtTotalWithGST.Text = TotalAmtWithGST.ToString

            'txtTotalDiscAmt.Text = TotalDiscAmt.ToString
            'txtTotalGSTAmt.Text = TotalGSTAmt.ToString

            'txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

            'DisplayGLGrid()
            'UpdatePanel3.Update()
            updPanelSave.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "CalculateTotalPrice", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtQtyGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        'If ddlDocType.Text = "ARCN" Then
        '    If btn1.Text > 0 Then
        '        btn1.Text = btn1.Text * (-1)
        '    End If
        'End If
        CalculatePrice()
    End Sub

    Protected Sub txtPricePerUOMGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        CalculatePrice()
    End Sub

    Protected Sub txtDiscAmountGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        'CalculatePrice()
    End Sub

    Protected Sub txtDiscPercGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        'CalculatePrice()
    End Sub


    Private Sub MakeMeNullBillingDetails()
        'ddlDocType.SelectedIndex = 0
        txtCNNo.Text = ""
        'txtCNNoSelected.Text = ""
        txtPostStatus.Text = ""
        'ddlContactType.SelectedIndex = 0
        'txtBillAddress.Text = ""
        'txtBillBuilding.Text = ""
        'txtBillStreet.Text = ""
        'txtContactPerson.Text = ""

        'txtBillCountry.Text = ""
        'txtBillPostal.Text = ""
        'txtCreditDays.Text = ""
        'ddlCreditTerms.SelectedIndex = 0
        txtComments.Text = ""

        'txtInvoiceDate.Text = ""
        'txtAccountIdBilling.Text = ""
        'txtAccountName.Text = ""
        txtCompanyGroup.Text = ""
        'ddlBankCode.SelectedIndex = 0
        'txtBankName.Text = ""
        'txtBankGLCode.Text = ""
        grvBillingDetails.ShowHeader = True

        txtTotalWithGST.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        'txtTotalDiscAmt.Text = "0.00"
        txtTotalWithDiscAmt.Text = "0.00"
        txtTotalCNAmt.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        txtCreditAmount.Text = "0.00"
        'ddlContractNoCN.Items.Clear()
        'ddlContractNoCN.Items.Add("--SELECT--")
        txtCreditAmount.Text = "0.00"
        txtDebitAmount.Text = "0.00"
        'ddlSalesmanBilling.SelectedIndex = 0

        FirstGridViewRowGL()
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
        'updPnlBillingRecs.Update()
        'updPnlBillingRecs.Update()
    End Sub

    Protected Sub btnAddDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        'If TotDetailRecords > 0 Then
        '    AddNewRowWithDetailRecBillingDetailsRecs()
        'Else
        AddNewRowBillingDetailsRecs()
        'End If
    End Sub


    Protected Sub PopulateGLCodes()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim sql As String
            sql = ""

            Dim command21 As MySqlCommand = New MySqlCommand

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
            sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"
            'Dim command1 As MySqlCommand = New MySqlCommand
            command21.CommandType = CommandType.Text
            command21.CommandText = sql
            command21.Connection = conn

            Dim dr21 As MySqlDataReader = command21.ExecuteReader()

            Dim dt21 As New DataTable
            dt21.Load(dr21)

            If dt21.Rows.Count > 0 Then
                If dt21.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt21.Rows(0)("COACode").ToString : End If
                If dt21.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt21.Rows(0)("Description").ToString : End If
            End If

            '''''''''''''''''''''''''''''''''''
            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"
            sql = "Select COACode, Description from tblchartofaccounts where GLType='GST OUTPUT'"
            Dim command23 As MySqlCommand = New MySqlCommand
            command23.CommandType = CommandType.Text
            command23.CommandText = sql
            command23.Connection = conn

            Dim dr23 As MySqlDataReader = command23.ExecuteReader()

            Dim dt23 As New DataTable
            dt23.Load(dr23)

            If dt23.Rows.Count > 0 Then
                If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
                If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
            End If

            updPnlBillingRecs.Update()

            conn.Close()
            conn.Dispose()
            command21.Dispose()
            command23.Dispose()
            dt21.Dispose()
            dt23.Dispose()
            dr21.Close()
            dr23.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PopulateGLCode", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub InsertIntoTblJournal()

        Dim rowselected As Integer
        rowselected = 0

        Dim ToBillAmt As Decimal
        Dim DiscAmount As Decimal
        Dim GSTAmount As Decimal
        Dim NetAmount As Decimal
        'Dim ToBillAmt As Decimal

        ToBillAmt = 0.0
        DiscAmount = 0.0
        GSTAmount = 0.0
        NetAmount = 0.0

        Try

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim qry As String
            qry = ""

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()


            '''''''''tblSales
            If txtMode.Text = "NEW" Then
                qry = "INSERT INTO tblJrnv(VoucherNumber, CalendarPeriod, GLPeriod,   "
                qry = qry + "  JournalDate, DebitBase, DebitOriginal, Creditbase, CreditOriginal, DifferenceBase,   "
                qry = qry + "  Comments, CompanyGroup, Location,  "
                qry = qry + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                qry = qry + " (@VoucherNumber, @CalendarPeriod, @GLPeriod,   "
                qry = qry + " @JournalDate, @DebitBase, @DebitOriginal, @Creditbase, @CreditOriginal, @DifferenceBase, "
                qry = qry + "   @Comments,  @CompanyGroup, @Location,   "
                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@CalendarPeriod", txtReceiptPeriod.Text)
                command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)

                If String.IsNullOrEmpty(txtCNDate.Text.Trim) = True Then
                    command.Parameters.AddWithValue("@JournalDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@JournalDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                End If


                If String.IsNullOrEmpty(txtDebitAmount.Text) = True Then
                    command.Parameters.AddWithValue("@DebitBase", 0.0)
                Else
                    command.Parameters.AddWithValue("@DebitBase", Convert.ToDecimal(txtDebitAmount.Text))
                End If

                If String.IsNullOrEmpty(txtDebitAmount.Text) = True Then
                    command.Parameters.AddWithValue("@DebitOriginal", 0.0)
                Else
                    command.Parameters.AddWithValue("@DebitOriginal", Convert.ToDecimal(txtDebitAmount.Text))
                End If

                If String.IsNullOrEmpty(txtCreditAmount.Text) = True Then
                    command.Parameters.AddWithValue("@Creditbase", 0.0)
                Else
                    command.Parameters.AddWithValue("@Creditbase", Convert.ToDecimal(txtCreditAmount.Text))
                End If

                If String.IsNullOrEmpty(txtCreditAmount.Text) = True Then
                    command.Parameters.AddWithValue("@CreditOriginal", 0.0)
                Else
                    command.Parameters.AddWithValue("@CreditOriginal", Convert.ToDecimal(txtCreditAmount.Text))
                End If

                'If String.IsNullOrEmpty(txtTotalWithDiscAmt.Text) = True Then
                command.Parameters.AddWithValue("@DifferenceBase", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@DifferenceBase", Convert.ToDecimal(txtCreditAmount.Text))
                'End If

                command.Parameters.AddWithValue("@PostStatus", txtPostStatus.Text)
                command.Parameters.AddWithValue("@GLStatus", txtPostStatus.Text)
                command.Parameters.AddWithValue("@Comments", txtComments.Text)
                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                command.Parameters.AddWithValue("@Location", txtLocation.Text)
                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                If String.IsNullOrEmpty(txtCNNo.Text) = True Then
                    GenerateJournalNo()
                End If
                command.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                command.Connection = conn
                command.ExecuteNonQuery()

                Dim sqlLastId As String
                sqlLastId = "SELECT last_insert_id() from tbljrnv"

                Dim commandRcno As MySqlCommand = New MySqlCommand
                commandRcno.CommandType = CommandType.Text
                commandRcno.CommandText = sqlLastId
                commandRcno.Parameters.Clear()
                commandRcno.Connection = conn
                txtRcno.Text = commandRcno.ExecuteScalar()


            Else
                qry = "Update tblJrnv set VoucherNumber =@VoucherNumber, CalendarPeriod=@CalendarPeriod, GLPeriod =@GLPeriod,   "
                qry = qry + " JournalDate =@JournalDate, DebitBase=@DebitBase, DebitOriginal=@DebitOriginal, Creditbase=@Creditbase, CreditOriginal=@CreditOriginal, DifferenceBase=@DifferenceBase,   "
                qry = qry + " Comments =@Comments,  CompanyGroup =@CompanyGroup,  Location =@Location,"
                qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                qry = qry + " where Rcno = @Rcno;"


                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                command.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                command.Parameters.AddWithValue("@CalendarPeriod", txtReceiptPeriod.Text)
                command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)

                If String.IsNullOrEmpty(txtCNDate.Text.Trim) = True Then
                    command.Parameters.AddWithValue("@JournalDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@JournalDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                End If


                If String.IsNullOrEmpty(txtDebitAmount.Text) = True Then
                    command.Parameters.AddWithValue("@DebitBase", 0.0)
                Else
                    command.Parameters.AddWithValue("@DebitBase", Convert.ToDecimal(txtDebitAmount.Text))
                End If

                If String.IsNullOrEmpty(txtDebitAmount.Text) = True Then
                    command.Parameters.AddWithValue("@DebitOriginal", 0.0)
                Else
                    command.Parameters.AddWithValue("@DebitOriginal", Convert.ToDecimal(txtDebitAmount.Text))
                End If

                If String.IsNullOrEmpty(txtCreditAmount.Text) = True Then
                    command.Parameters.AddWithValue("@Creditbase", 0.0)
                Else
                    command.Parameters.AddWithValue("@Creditbase", Convert.ToDecimal(txtCreditAmount.Text))
                End If

                If String.IsNullOrEmpty(txtCreditAmount.Text) = True Then
                    command.Parameters.AddWithValue("@CreditOriginal", 0.0)
                Else
                    command.Parameters.AddWithValue("@CreditOriginal", Convert.ToDecimal(txtCreditAmount.Text))
                End If

                'If String.IsNullOrEmpty(txtTotalWithDiscAmt.Text) = True Then
                command.Parameters.AddWithValue("@DifferenceBase", 0.0)
                'Else
                '    command.Parameters.AddWithValue("@DifferenceBase", Convert.ToDecimal(txtCreditAmount.Text))
                'End If

                command.Parameters.AddWithValue("@PostStatus", txtPostStatus.Text)
                command.Parameters.AddWithValue("@GLStatus", txtPostStatus.Text)
                command.Parameters.AddWithValue("@Comments", txtComments.Text)
                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                command.Parameters.AddWithValue("@Location", txtLocation.Text)

                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                command.Connection = conn
                command.ExecuteNonQuery()
            End If


            ''''''''tblSales

            'Dim commandtblSalesDetail As MySqlCommand = New MySqlCommand

            'commandtblSalesDetail.CommandType = CommandType.Text
            ''Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
            'Dim qrycommandtblSalesDetail As String = "DELETE from tblJrnvDet where VoucherNumber = '" & txtCNNo.Text & "'"

            'commandtblSalesDetail.CommandText = qrycommandtblSalesDetail
            'commandtblSalesDetail.Parameters.Clear()
            'commandtblSalesDetail.Connection = conn
            'commandtblSalesDetail.ExecuteNonQuery()
            'updPanelCN.Update()


            '' GVB----------------------------

            Dim gvbRecords, i As Long
            gvbRecords = grvBillingDetailsNew.Rows.Count()

            For i = 0 To gvbRecords - 1
                ''Dim lblItemCodeGVB As DropDownList = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemCodeGVB"), DropDownList)
                'Dim contractNoGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtContractNoGVB"), TextBox)
                'txtComments.Text = contractNoGVB.Text


                Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemTypeGVB"), TextBox)
                Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtOtherCodeGVB"), TextBox)
                Dim lblid14 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtRcnoJournalDetailGVB"), TextBox)

                If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then
                    Dim lblGLDescriptionGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGLDescriptionGVB"), TextBox)
                    'Dim lblidUnitMs As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtUOMGV"), DropDownList)
                    Dim lblidQtyGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtQtyGVB"), TextBox)
                    Dim lblidRefTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtInvoiceNoGVB"), TextBox)
                    Dim lblidDescriptionGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDescriptionGVB"), TextBox)
                    Dim lblidDebitBaseGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDebitBaseGVB"), TextBox)
                    Dim lblidCreditBaseGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtCreditBaseGVB"), TextBox)
                    Dim lblidContactTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtContactTypeGVB"), TextBox)
                    Dim lblidCustCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtCustCodeGVB"), TextBox)
                    Dim lblidAccountIDGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtAccountIDGVB"), TextBox)
                    Dim lblidLocationIDGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtLocationIdGVB"), TextBox)
                    Dim lblidCustNameGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtCustNameGVB"), TextBox)
                    Dim lblidLocationGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtLocationGVB"), TextBox)

                    'Dim lContractGroup As String
                    'lContractGroup = ""

                    'If String.IsNullOrEmpty(lblidContractNoGVB.Text) = False Then
                    '    Dim commandCG As MySqlCommand = New MySqlCommand
                    '    commandCG.CommandType = CommandType.Text

                    '    commandCG.CommandText = "SELECT ContractGroup FROM tblContract where  ContractNo = '" & lblidContractNoGVB.Text & "'"
                    '    'command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
                    '    commandCG.Connection = conn

                    '    Dim drCG As MySqlDataReader = commandCG.ExecuteReader()
                    '    Dim dtCG As New DataTable
                    '    dtCG.Load(drCG)


                    '    lContractGroup = dtCG.Rows(0)("ContractGroup").ToString
                    'End If

                    Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                    commandSalesDetail.CommandType = CommandType.Text


                    'Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
                    'qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
                    'qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
                    'qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, ServiceBy, ServiceDate, BillingFrequency, InvoiceTYpe, ItemDescription, ContractGroup, SourceInvoice,  "
                    'qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    'qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                    'qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                    'qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                    'qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @ServiceBy, @ServiceDate, @BillingFrequency, @InvoiceType, @ItemDescription, @ContractGroup, @SourceInvoice, "
                    'qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    Dim qryDetail As String = "UPDATE tblJrnvDet SET ItemType=ItemType, VoucherNumber=VoucherNumber,  LedgerCode=@LedgerCode, LedgerName=@LedgerName, Reftype=@Reftype, Description=@Description, Currency=@Currency, ExchangeRate=@ExchangeRate, Quantity=@Quantity,    "
                    qryDetail = qryDetail + " Debitbase=@Debitbase, DebitOriginal=@DebitOriginal, CreditBase=@CreditBase, CreditOriginal=@CreditOriginal,  "
                    qryDetail = qryDetail + " ContactType=@ContactType, CustCode=@CustCode, AccountID=@AccountID, LocationID=@LocationID, CustName=@CustName, Location=@Location, "
                    qryDetail = qryDetail + " LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn  "
                    qryDetail = qryDetail + " where Rcno = " & lblid14.Text
                    'qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                    'qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                    'qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                    'qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @ServiceBy, @ServiceDate, @BillingFrequency, @InvoiceType, @ItemDescription, @ContractGroup, @SourceInvoice, "
                    'qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandSalesDetail.CommandText = qryDetail
                    commandSalesDetail.Parameters.Clear()

                    commandSalesDetail.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@ItemType", lblidItemTypeGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@LedgerCode", lblidOtherCodeGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@LedgerName", lblGLDescriptionGVB.Text.ToUpper.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@RefType", lblidRefTypeGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@Description", lblidDescriptionGVB.Text.ToUpper.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                    commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                    commandSalesDetail.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(lblidQtyGVB.Text))
                    commandSalesDetail.Parameters.AddWithValue("@UnitMs", "")
                    commandSalesDetail.Parameters.AddWithValue("@DebitBase", Convert.ToDecimal(lblidDebitBaseGVB.Text))
                    commandSalesDetail.Parameters.AddWithValue("@DebitOriginal", Convert.ToDecimal(lblidDebitBaseGVB.Text))
                    commandSalesDetail.Parameters.AddWithValue("@CreditBase", Convert.ToDecimal(lblidCreditBaseGVB.Text))
                    commandSalesDetail.Parameters.AddWithValue("@CreditOriginal", Convert.ToDecimal(lblidCreditBaseGVB.Text))

                    If (lblidContactTypeGVB.Text.Trim) = "-1" Then
                        commandSalesDetail.Parameters.AddWithValue("@ContactType", "")
                    Else
                        commandSalesDetail.Parameters.AddWithValue("@ContactType", lblidContactTypeGVB.Text)
                    End If

                    commandSalesDetail.Parameters.AddWithValue("@CustCode", lblidCustCodeGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@AccountID", lblidAccountIDGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@LocationID", lblidLocationIDGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@CustName", lblidCustNameGVB.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@Location", lblidLocationGVB.Text.Trim)

                    'commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    commandSalesDetail.Connection = conn
                    commandSalesDetail.ExecuteNonQuery()
                End If


            Next



            ''GVB -----------------------------

            ''''''''''''''''''
            SetRowDataBillingDetailsRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                PopulateGLCodes()
                ''''''''''''''''''''''''''''''''''''''''''''''''
                For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1

                    Dim lblidItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemTypeGV"), DropDownList)
                    Dim lblidOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtOtherCodeGV"), TextBox)

                    If (lblidItemType.SelectedValue) <> "-1" And String.IsNullOrEmpty(lblidOtherCode.Text) = False Then
                        Dim lblGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGLDescriptionGV"), TextBox)
                        'Dim lblidUnitMs As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtUOMGV"), DropDownList)
                        Dim lblidQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtQtyGV"), TextBox)
                        Dim lblidRefType As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim lblidDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDescriptionGV"), TextBox)
                        Dim lblidDebitBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDebitBaseGV"), TextBox)
                        Dim lblidCreditBase As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtCreditBaseGV"), TextBox)
                        Dim lblidContactType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContactTypeGV"), DropDownList)
                        Dim lblidCustCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtCustCodeGV"), TextBox)
                        Dim lblidAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtAccountIDGV"), TextBox)
                        Dim lblidLocationID As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtLocationIdGV"), TextBox)
                        Dim lblidCustName As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtCustNameGV"), TextBox)
                        Dim lblidLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtLocationGV"), TextBox)


                        Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                        commandSalesDetail.CommandType = CommandType.Text
                       
                        Dim qryDetail As String = "INSERT INTO tblJrnvDet(ItemType, VoucherNumber,  LedgerCode, LedgerName, Reftype, Description, Currency, ExchangeRate, Quantity,    "
                        qryDetail = qryDetail + " Debitbase, DebitOriginal, CreditBase, CreditOriginal,  "
                        qryDetail = qryDetail + " ContactType, CustCode, AccountID, LocationID, CustName, Location, "
                        qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        qryDetail = qryDetail + "(@ItemType, @VoucherNumber,  @LedgerCode, @LedgerName,  @Reftype, @Description, @Currency, @ExchangeRate, @Quantity,  "
                        qryDetail = qryDetail + " @Debitbase, @DebitOriginal, @CreditBase, @CreditOriginal, "
                        qryDetail = qryDetail + " @ContactType, @CustCode, @AccountID, @LocationID, @CustName, @Location, "
                        qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        commandSalesDetail.CommandText = qryDetail
                        commandSalesDetail.Parameters.Clear()

                        commandSalesDetail.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@ItemType", lblidItemType.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@LedgerCode", lblidOtherCode.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@LedgerName", lblGLDescription.Text.ToUpper.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@RefType", lblidRefType.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@Description", lblidDescription.Text.ToUpper.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                        commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                        commandSalesDetail.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(lblidQty.Text))
                        commandSalesDetail.Parameters.AddWithValue("@UnitMs", "")
                        commandSalesDetail.Parameters.AddWithValue("@DebitBase", Convert.ToDecimal(lblidDebitBase.Text))
                        commandSalesDetail.Parameters.AddWithValue("@DebitOriginal", Convert.ToDecimal(lblidDebitBase.Text))
                        commandSalesDetail.Parameters.AddWithValue("@CreditBase", Convert.ToDecimal(lblidCreditBase.Text))
                        commandSalesDetail.Parameters.AddWithValue("@CreditOriginal", Convert.ToDecimal(lblidCreditBase.Text))

                        If (lblidContactType.Text.Trim) = "-1" Then
                            commandSalesDetail.Parameters.AddWithValue("@ContactType", "")
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@ContactType", lblidContactType.Text)
                        End If

                        commandSalesDetail.Parameters.AddWithValue("@CustCode", lblidCustCode.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@AccountID", lblidAccountID.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@LocationID", lblidLocationID.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@CustName", lblidCustName.Text.Trim)
                        commandSalesDetail.Parameters.AddWithValue("@Location", lblidLocation.Text.Trim)


                        commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        commandSalesDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        commandSalesDetail.Connection = conn
                        commandSalesDetail.ExecuteNonQuery()

                    End If
                Next rowIndex

                grvBillingDetailsNew.DataBind()
                updPanelCN.Update()

                DisplayGLGrid()

            End If

            conn.Close()
            conn.Dispose()

            'InsertNewLog()
            'InsertNewLogDetail()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "InsertIntoSales", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub


    'Private Function FindAccountId() As Boolean
    '    Dim IsAccountId As Boolean
    '    IsAccountId = False

    '    Dim connIsAccountId As MySqlConnection = New MySqlConnection()

    '    connIsAccountId.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    connIsAccountId.Open()

    '    Dim commandIsAccountId As MySqlCommand = New MySqlCommand
    '    commandIsAccountId.CommandType = CommandType.Text

    '    If ddlContactType.Text = "COMPANY" Or ddlContactType.Text = "CORPORATE" Then
    '        commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblCompany where AccountId ='" & txtAccountIdBilling.Text & "'"
    '    ElseIf ddlContactType.Text = "PERSON" Or ddlContactType.Text = "RESIDENTIAL" Then
    '        commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblPerson where AccountId ='" & txtAccountIdBilling.Text & "'"
    '    End If

    '    commandIsAccountId.Connection = connIsAccountId

    '    Dim drIsAccountId As MySqlDataReader = commandIsAccountId.ExecuteReader()
    '    Dim dtIsAccountId As New DataTable
    '    dtIsAccountId.Load(drIsAccountId)

    '    If dtIsAccountId.Rows.Count > 0 Then
    '        If dtIsAccountId.Rows(0)("CountAccountId").ToString > 0 Then
    '            IsAccountId = True
    '        End If
    '    End If

    '    commandIsAccountId.Dispose()
    '    connIsAccountId.Close()
    '    connIsAccountId.Dispose()
    '    Return IsAccountId
    'End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim rowselected As Integer
        rowselected = 0
        lblAlert.Text = ""

        btnSave.Enabled = False

        If String.IsNullOrEmpty(txtCNDate.Text) = True Then
            lblAlert.Text = "PLEASE ENTER VOUCHER DATE"
            updPnlMsg.Update()
            txtCNDate.Focus()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Dim IsLock = FindJNPeriod(txtReceiptPeriod.Text)
        If IsLock = "Y" Then
            lblAlert.Text = "PERIOD IS LOCKED"
            updPnlMsg.Update()
            txtCNDate.Focus()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If




        '''''''''''''''''''''

        'Dim IsAccountIdExist As Boolean = FindAccountId()

        'If IsAccountIdExist = False Then
        '    lblAlert.Text = "INVALID ACCOUNT ID"
        '    updPnlMsg.Update()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If
        '''''''''''''''''''''



        If Convert.ToDecimal(txtCreditAmount.Text) <> Convert.ToDecimal(txtDebitAmount.Text) Then
            lblAlert.Text = "DEBIT AMOUNT AND CREDIT AMOUNT SHOULD BE SAME"
            updPnlMsg.Update()
            'txtCNDate.Focus()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Try

            'PopulateArCode()
            'Dim conn As MySqlConnection = New MySqlConnection()
            Dim totalRows, totalRows2 As Long
            totalRows = 0
            totalRows2 = 0

            Dim totalRows1 As Long
            totalRows1 = 0
            totalRows1 = grvBillingDetailsNew.Rows.Count

            'SetRowDataBillingDetailsRecs()

            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then

                If tableAdd.Rows.Count > 0 Then
                    Dim lblidOtherCode1 As TextBox = CType(grvBillingDetails.Rows(0).FindControl("txtOtherCodeGV"), TextBox)
                    Dim lblidItemType1 As DropDownList = CType(grvBillingDetails.Rows(0).FindControl("txtItemTypeGV"), DropDownList)

                    If String.IsNullOrEmpty(lblidOtherCode1.Text) = True And lblidItemType1.Text = "-1" And totalRows1 = 0 Then
                        totalRows = totalRows + 1
                        lblAlert.Text = "PLEASE ENTER DETAILS RECORD"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        btnSave.Enabled = True
                        'btnShowInvoices.Enabled = False
                        updPnlMsg.Update()

                        Exit Sub
                    End If

                End If


                totalRows = 0

                Dim tableAdd1 As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

                If tableAdd1 IsNot Nothing Then

                    For rowIndex1 As Integer = 0 To tableAdd1.Rows.Count - 1
                        Dim lblidOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtOtherCodeGV"), TextBox)
                        Dim lblidItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim lblidServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtServiceRecordGV"), TextBox)
                        'Dim lblidTaxTypeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtTaxTypeGV"), DropDownList)


                        'If lblidItemType.Text = "SERVICE" And String.IsNullOrEmpty(lblidServiceRecordNo.Text.Trim) = True Then
                        '    lblAlert.Text = "PLEASE ENTER SERVICE RECORD NO. FOR ALL SERVICE ITEM TYPE"
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        '    updPnlMsg.Update()
                        '    btnSave.Enabled = True
                        '    Exit Sub
                        'End If

                        If String.IsNullOrEmpty(lblidOtherCode.Text) = True And lblidItemType.Text <> "-1" Then
                            totalRows = totalRows + 1
                        End If

                        'If ((String.IsNullOrEmpty(lblidTaxTypeGV.Text) = True)) And lblidItemType.Text <> "-1" Then
                        '    totalRows2 = totalRows2 + 1
                        'End If
                    Next rowIndex1
                End If


                If totalRows > 0 Then
                    lblAlert.Text = "PLEASE SELECT GL CODE "
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    lblAlert.Focus()
                    updPnlMsg.Update()
                    btnSave.Enabled = True
                    Exit Sub
                End If


                'If totalRows2 > 0 Then
                '    lblAlert.Text = "PLEASE SELECT GST CODE "
                '    lblAlert.Focus()
                '    updPnlMsg.Update()
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                '    btnSave.Enabled = True
                '    Exit Sub
                'End If

                '''''''''''''''''''

                CalculateTotalPrice()
                InsertIntoTblJournal()

                'CalculateTotal()

            End If
            'End If



            'conn.Close()

            DisableControls()

            'If rowselected = 0 Then
            '    lblAlert.Text = "PLEASE SELECT A RECORD"
            '    btnShowInvoices.Enabled = False
            '    updPnlMsg.Update()
            '    Exit Sub
            'End If

            If txtMode.Text = "NEW" Then
                lblMessage.Text = "ADD: ADJUSTMENT NOTE RECORD SUCCESSFULLY ADDED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJNOTE", txtCNNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), txtDebitAmount.Text, 0, txtCreditAmount.Text, "", "", txtRcno.Text)



                '''''''''''''''''''''''''
                If txtPostUponSave.Text = "1" Then
                    IsSuccess = PostCN()

                    If IsSuccess = True Then

                        lblAlert.Text = ""
                        updPnlSearch.Update()
                        updPnlMsg.Update()
                        updpnlBillingDetails.Update()
                        'updpnlServiceRecs.Update()
                        updpnlBillingDetails.Update()

                        btnQuickSearch_Click(sender, e)
                        lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
                        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJNOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

                        btnReverse.Enabled = True
                        btnReverse.ForeColor = System.Drawing.Color.Black

                        btnEdit.Enabled = False
                        btnEdit.ForeColor = System.Drawing.Color.Gray

                        btnDelete.Enabled = False
                        btnDelete.ForeColor = System.Drawing.Color.Gray

                        btnPost.Enabled = False
                        btnPost.ForeColor = System.Drawing.Color.Gray

                        'InsertNewLog()
                    End If

                Else
                    mdlPopupConfirmSavePost.Show()
                End If


                '''''''''''''''''''''''''''


            Else
                lblMessage.Text = "EDIT: ADJUSTMENT NOTE RECORD SUCCESSFULLY UPDATED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJNOTE", txtCNNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), txtDebitAmount.Text, 0, txtCreditAmount.Text, "", "", txtRcno.Text)

            End If


            If String.IsNullOrEmpty(txt.Text.Trim) = True Then
                txt.Text = "SELECT * FROM tblJrnv  WHERE 1=1 and VoucherNumber = '" & txtCNNo.Text & "'"
            End If

            SQLDSCN.SelectCommand = txt.Text
            SQLDSCN.DataBind()
            GridView1.DataSourceID = "SQLDSCN"
            GridView1.DataBind()

            txtMode.Text = "View"

            lblAlert.Text = ""
            btnPost.Enabled = True
            grvBillingDetails.Visible = False


            'grvBillingDetails.Visible = False
            FirstGridViewRowBillingDetailsRecs()


            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black
            'txtRcno.Text = command.LastInsertedId
            ' End If

            'FirstGridViewRowServiceRecs()

            updPnlMsg.Update()
            updPnlSearch.Update()
            updPnlBillingRecs.Update()
            'updpnlServiceRecs.Update()

            Session.Add("RecordNo", txtCNNo.Text)

            'InsertNewLog()
            'Session.Add("Title", ddlDocType.SelectedItem.Text)

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnSave_Click", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub

    Private Sub InsertNewLog()
        Try
            ' Start: Insert NEW Log table

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT EnableLogforJournal FROM tblservicerecordmastersetup where rcno=1"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
                If dt.Rows(0)("EnableLogforJournal").ToString = "1" Then
                    Dim connLog As MySqlConnection = New MySqlConnection()

                    connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
                    If connLog.State = ConnectionState.Open Then
                        connLog.Close()
                        connLog.Dispose()
                    End If
                    connLog.Open()

                    Dim commandInsertLog As MySqlCommand = New MySqlCommand
                    commandInsertLog.CommandType = CommandType.StoredProcedure
                    commandInsertLog.CommandText = "InsertLog_sitadatadb"

                    commandInsertLog.Parameters.Clear()

                    commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Journal")
                    commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtCNNo.Text.Trim)

                    commandInsertLog.Connection = connLog
                    commandInsertLog.ExecuteScalar()

                    connLog.Close()
                    commandInsertLog.Dispose()
                End If
            End If

            ' End: Insert NEW Log table
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "FUNCTION InsertNewLog", ex.Message.ToString, txtInvoiceNo.Text)
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        End Try
    End Sub

    Private Sub InsertNewLogDetail()
        Try
            ' Start: Insert NEW Log table

            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            command.CommandType = CommandType.Text
            command.CommandText = "SELECT EnableLogforJournal FROM tblservicerecordmastersetup where rcno=1"
            command.Connection = conn

            Dim dr As MySqlDataReader = command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
                If dt.Rows(0)("EnableLogforJournal").ToString = "1" Then
                    Dim connLog As MySqlConnection = New MySqlConnection()

                    connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
                    If connLog.State = ConnectionState.Open Then
                        connLog.Close()
                        connLog.Dispose()
                    End If
                    connLog.Open()

                    Dim commandInsertLog As MySqlCommand = New MySqlCommand
                    commandInsertLog.CommandType = CommandType.StoredProcedure
                    commandInsertLog.CommandText = "InsertLogDetail_sitadatadb"

                    commandInsertLog.Parameters.Clear()

                    commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Journal")
                    commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtCNNo.Text.Trim)
                    commandInsertLog.Parameters.AddWithValue("@pr_KeyValueDetail", "")
                    commandInsertLog.Connection = connLog
                    commandInsertLog.ExecuteScalar()

                    connLog.Close()
                    commandInsertLog.Dispose()
                End If
            End If

            ' End: Insert NEW Log table
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "FUNCTION InsertNewLogDetail", ex.Message.ToString, txtInvoiceNo.Text)
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        End Try
    End Sub
    Protected Sub Updatetblservicebillingdetailitem(TextBoxRcnoInvoice As Long, TextBoxServiceRecordNo As String)
        Try
            Dim lRcoInvoice As Long = TextBoxRcnoInvoice
            Dim lServiceRecordNo As String = TextBoxServiceRecordNo

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            'update  tblSalesDetail
            Dim cmdCNSum As MySqlCommand = New MySqlCommand
            cmdCNSum.CommandType = CommandType.Text
            cmdCNSum.CommandText = "SELECT sum(CNValue) as cnsum  FROM tblcndet where  RcnoServiceBillingItem = " & Convert.ToInt64(lRcoInvoice)
            cmdCNSum.Connection = conn

            Dim drCNSum As MySqlDataReader = cmdCNSum.ExecuteReader()
            Dim dtCNSum As New DataTable
            dtCNSum.Load(drCNSum)

            Dim lReceiptSum As Decimal

            If dtCNSum.Rows.Count = 0 Then
                lReceiptSum = 0.0
            ElseIf IsDBNull(dtCNSum.Rows(0)("cnsum")) = True Then
                lReceiptSum = 0.0
            Else
                lReceiptSum = Convert.ToDecimal(dtCNSum.Rows(0)("cnsum"))
            End If



            Dim commandUpdateInvoiceValue1 As MySqlCommand = New MySqlCommand
            commandUpdateInvoiceValue1.CommandType = CommandType.Text
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & Convert.ToDecimal(lReceptAmtAdjusted) & " where Rcno = " & row1("Rcno")
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set CreditAmount  = " & lReceiptSum & " where Rcno = " & Convert.ToInt64(lRcoInvoice)
            Dim sqlUpdateInvoiceValue1 As String = "Update tblSalesDetail set CreditAmount  = " & lReceiptSum & " where Rcno = " & Convert.ToInt64(lRcoInvoice)
            commandUpdateInvoiceValue1.CommandText = sqlUpdateInvoiceValue1
            commandUpdateInvoiceValue1.Parameters.Clear()
            commandUpdateInvoiceValue1.Connection = conn
            commandUpdateInvoiceValue1.ExecuteNonQuery()

            'End: Update  tblServiceRecord

            'update  tblServiceRecord
            Dim cmdCNSumServiceRecord As MySqlCommand = New MySqlCommand
            cmdCNSumServiceRecord.CommandType = CommandType.Text
            cmdCNSumServiceRecord.CommandText = "SELECT sum(CNValue) as cnsum  FROM tblcndet where ServiceRecordNo = '" & lServiceRecordNo & "'"
            cmdCNSumServiceRecord.Connection = conn

            Dim drCNSumServiceRecord As MySqlDataReader = cmdCNSumServiceRecord.ExecuteReader()
            Dim dtCNSumServiceRecord As New DataTable
            dtCNSumServiceRecord.Load(drCNSumServiceRecord)

            Dim lReceiptSumServiceRecord As Decimal

            If dtCNSumServiceRecord.Rows.Count = 0 Then
                lReceiptSumServiceRecord = 0.0
            ElseIf IsDBNull(dtCNSumServiceRecord.Rows(0)("cnsum")) = True Then
                lReceiptSumServiceRecord = 0.0
            Else
                lReceiptSumServiceRecord = Convert.ToDecimal(dtCNSumServiceRecord.Rows(0)("cnsum"))
            End If



            Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
            commandUpdateServiceRecord.CommandType = CommandType.Text
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & Convert.ToDecimal(lReceptAmtAdjusted) & " where Rcno = " & row1("Rcno")
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set CreditAmount  = " & lReceiptSum & " where Rcno = " & Convert.ToInt64(lRcoInvoice)
            Dim sqlUpdateServiceRecord As String = "Update tblServiceRecord set TotalCreditAmount  = " & lReceiptSumServiceRecord & " where RecordNo = '" & lServiceRecordNo & "'"
            commandUpdateServiceRecord.CommandText = sqlUpdateServiceRecord
            commandUpdateServiceRecord.Parameters.Clear()
            commandUpdateServiceRecord.Connection = conn
            commandUpdateServiceRecord.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            cmdCNSumServiceRecord.Dispose()
            commandUpdateServiceRecord.Dispose()
            dtCNSumServiceRecord.Dispose()
            drCNSumServiceRecord.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "Updatetblservicebillingdetailitem", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnShowInvoices_Click(sender As Object, e As EventArgs) Handles btnShowInvoices.Click
        Try
            lblAlert.Text = ""
            updPnlMsg.Update()
            'lblMessage.Text = ""
            'If String.IsNullOrEmpty(txtAccountIdBilling.Text) = True Then
            '    lblAlert.Text = "Please Select Account Id"
            '    updPnlMsg.Update()
            '    Exit Sub
            'End If

            'If ddlDocType.SelectedIndex = 0 Then
            '    lblAlert.Text = "PLEASE SELECT VOUCHER TYPE "
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    lblAlert.Focus()

            '    updPnlMsg.Update()
            '    Exit Sub
            'End If



            'If String.IsNullOrEmpty(txtCompanyGroup.Text.Trim) = False Then
            '    ddlCompanyGrpII.Text = txtCompanyGroup.Text
            'End If

            'If String.IsNullOrEmpty(ddlContactType.Text.Trim) = False Then
            '    ddlContactTypeIS.Text = ddlContactType.Text
            'End If
            'txtAccountIdII.Text = txtAccountIdBilling.Text
            'txtClientNameII.Text = txtAccountName.Text
            ''''''''''''''''''
            mdlImportInvoices.Show()
            'Start: Billing Details

            'Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            'Dim drCurrentRowLoc As DataRow = Nothing

            'For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
            '    dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
            '    drCurrentRowLoc = dtScdrLoc.NewRow()
            '    ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
            '    grvBillingDetails.DataSource = dtScdrLoc
            '    grvBillingDetails.DataBind()

            '    SetPreviousDataBillingDetailsRecs()
            'Next i

            'FirstGridViewRowBillingDetailsRecs()

            ''Start: From tblBillingDetailItem

            'Dim Total As Decimal
            'Dim TotalWithGST As Decimal
            'Dim TotalDiscAmt As Decimal
            'Dim TotalGSTAmt As Decimal
            'Dim TotalPriceWithDiscountAmt As Decimal


            'Total = 0.0
            'TotalWithGST = 0.0
            'TotalDiscAmt = 0.0
            'TotalGSTAmt = 0.0
            'TotalPriceWithDiscountAmt = 0.0

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
            'cmdServiceBillingDetailItem.CommandType = CommandType.Text
            ''cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblsales where Rcnoservicebillingdetail=" & Convert.ToInt32(txtRcnoservicebillingdetail.Text)

            ''If String.IsNullOrEmpty(ddlContractNo.Text) = True Then
            ''    cmdServiceBillingDetailItem.CommandText = "SELECT ContractNo, ContractDate, AgreeValue, RetentionValue FROM tblContract where Status = 'O' and CompanyGroup ='" & txtCompanyGroup.Text & "' and ContractGroup <> 'ST'  and AccountId = '" & txtAccountIdBilling.Text & "'"
            ''Else
            ''    cmdServiceBillingDetailItem.CommandText = "SELECT ContractNo, ContractDate, AgreeValue, RetentionValue FROM tblContract where Status = 'O' and CompanyGroup ='" & txtCompanyGroup.Text & "' and ContractGroup <> 'ST'  and AccountId = '" & txtAccountIdBilling.Text & "' and ContractNo ='" & ddlContractNo.Text & "'"
            ''End If

            ''cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblsales a, tblservicebillingdetailitem b where a.rcnoServiceRecord = b.RcnoServiceRecord and a.PaidStatus <> 'F' and a.GLStatus = 'P' and a.CompanyGroup ='" & txtCompanyGroup.Text & "' and a.ContactType = '" & ddlContactType.Text & "'  and a.AccountId = '" & txtAccountIdBilling.Text & "' order by a.Salesdate"

            ''cmdServiceBillingDetailItem.CommandText = "SELECT a.InvoiceNumber, a.SalesDate, b.TotalPriceWithGST, a.TotalReceiptAmount, a.TotalCNAmount, b.ItemType, b.ItemCode, b.ItemDescription, b.OtherCode, b.GLDescription, b.rcno, b.ReceiptAmount, b.CreditAmount FROM tblsales a, tblservicebillingdetailitem b where a.rcnoServiceRecord = b.RcnoServiceRecord and b.Contractno = '" & ddlContractNo.Text & "' and  a.PaidStatus <> 'F' and a.GLStatus = 'P' and STBilling = 'N' and a.CompanyGroup ='" & txtCompanyGroup.Text & "' and a.AccountId = '" & txtAccountIdBilling.Text & "' order by a.Salesdate, b.rcno"
            ''cmdServiceBillingDetailItem.CommandText = "SELECT a.InvoiceNumber, a.SalesDate, b.TotalPriceWithGST, a.TotalReceiptAmount, a.TotalCNAmount, b.ServiceRecordNo, b.ContractNo, b.TaxType, b.GSTPerc, b.GSTAmt, b.PriceWithDisc, b.InvoiceType,  b.ItemType, b.ItemCode, b.ItemDescription, b.OtherCode, b.GLDescription, b.rcno, b.ReceiptAmount, b.CreditAmount FROM tblsales a, tblservicebillingdetailitem b where a.BatchNo = b.BatchNo and b.Contractno = '" & ddlContractNo.Text & "' and  a.PaidStatus <> 'F' and a.GLStatus = 'P' and STBilling = 'N' and a.CompanyGroup ='" & txtCompanyGroup.Text & "' and a.AccountId = '" & txtAccountIdBilling.Text & "' order by a.Salesdate, b.rcno"
            ''cmdServiceBillingDetailItem.CommandText = "SELECT a.InvoiceNumber, a.SalesDate, b.TotalPriceWithGST, a.TotalReceiptAmount, a.TotalCNAmount, b.ServiceRecordNo, b.ContractNo, b.TaxType, b.GSTPerc, b.GSTAmt, b.PriceWithDisc, b.InvoiceType,  b.ItemType, b.ItemCode, b.ItemDescription, b.OtherCode, b.GLDescription, b.rcno, b.ReceiptAmount, b.CreditAmount FROM tblsales a, tblservicebillingdetailitem b where a.BatchNo = b.BatchNo and b.Contractno = '" & ddlContractNo.Text & "' and  a.PaidStatus <> 'F' and a.GLStatus = 'P' and STBilling = 'N' and a.CompanyGroup ='" & txtCompanyGroup.Text & "' and a.AccountId = '" & txtAccountIdBilling.Text & "' order by a.Salesdate, b.rcno"
            'cmdServiceBillingDetailItem.CommandText = "SELECT a.InvoiceNumber, a.SalesDate, b.AppliedBase, a.TotalReceiptAmount, a.TotalCNAmount, b.RefType, b.CostCode, b.GST, b.GSTRate, b.GSTBase, b.ValueBase,   b.SubCode,  b.Description, b.LedgerCode, b.LedgerName, b.InvoiceType, b.ReceiptAmount, b.CreditAmount, b.rcno FROM tblsales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber  and  a.PaidStatus <> 'F' and b.Costcode = '" & ddlContractNo.Text & "' and a.GLStatus = 'P' and a.CompanyGroup ='" & txtCompanyGroup.Text & "' and a.AccountId = '" & txtAccountIdBilling.Text & "' order by a.Salesdate, b.rcno"

            'cmdServiceBillingDetailItem.Connection = conn

            'Dim drcmdServiceBillingDetailItem As MySqlDataReader = cmdServiceBillingDetailItem.ExecuteReader()
            'Dim dtServiceBillingDetailItem As New DataTable
            'dtServiceBillingDetailItem.Load(drcmdServiceBillingDetailItem)

            'Dim TotDetailRecordsLoc = dtServiceBillingDetailItem.Rows.Count
            'If dtServiceBillingDetailItem.Rows.Count > 0 Then

            '    Dim rowIndex = 0

            '    'PopulateArCode()
            '    For Each row As DataRow In dtServiceBillingDetailItem.Rows
            '        If (TotDetailRecordsLoc > (rowIndex + 1)) Then
            '            AddNewRowBillingDetailsRecs()
            '            'AddNewRow()
            '        End If


            '        Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
            '        TextBoxtxtInvoiceNoGV.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("InvoiceNumber"))

            '        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
            '        'TextBoxInvoiceDate.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("SalesDate"))
            '        TextBoxInvoiceDate.Text = Convert.ToDateTime(dtServiceBillingDetailItem.Rows(rowIndex)("SalesDate")).ToString("dd/MM/yyyy")
            '        'Convert.ToDateTime(txtContractDate.Text).ToString("yyyy-MM-dd")

            '        Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
            '        'TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("NetAmount"))
            '        TextBoxTotalPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ValueBase"))

            '        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
            '        'TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("NetAmount"))
            '        TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTBase"))

            '        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
            '        TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("AppliedBase"))

            '        Dim TextBoxtxtServiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceNoGV"), TextBox)
            '        TextBoxtxtServiceNoGV.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("RefType"))

            '        Dim TextBoxTotalTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
            '        TextBoxTotalTotalReceiptAmt.Text = Convert.ToString(Convert.ToDecimal(dtServiceBillingDetailItem.Rows(rowIndex)("ReceiptAmount")))



            '        Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
            '        TextBoxBalanceAmt.Text = Convert.ToString(Convert.ToDecimal(dtServiceBillingDetailItem.Rows(rowIndex)("CreditAmount")))


            '        'Dim TextBoxItemType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), TextBox)
            '        'TextBoxItemType.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemType")))

            '        'Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
            '        'TextBoxItemCode.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode")))

            '        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
            '        'TextBoxItemDescription.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemDescription")))

            '        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
            '        'TextBoxOtherCode.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("LedgerCode")))
            '        TextBoxOtherCode.Text = txtARCode10.Text


            '        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            '        TextBoxGLDescription.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("LedgerName")))



            '        ''''''''''''''''''''''''''''''
            '        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
            '        TextBoxItemCode.Text = txtARProductCode10.Text

            '        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
            '        TextBoxItemDescription.Text = txtARDescription10.Text

            '        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
            '        'TextBoxOtherCode.Text = txtARCode10.Text

            '        'Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            '        'TextBoxGLDescription.Text = txtARDescription10.Text


            '        '''''''''''''''''''''''''''''''

            '        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
            '        TextBoxRcnoInvoice.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Rcno")))

            '        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTaxTypeGV"), TextBox)
            '        'TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("NetAmount"))
            '        TextBoxTaxType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GST"))

            '        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTPercGV"), TextBox)
            '        'TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("NetAmount"))
            '        TextBoxGSTPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTRate"))

            '        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)

            '        If Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("SubCode"))) = "SERV" Then
            '            TextBoxInvoiceType.Text = "SERVICE"
            '            'TextBoxItemType.Enabled = False
            '        ElseIf Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("SubCode"))) = "STCK" Then
            '            TextBoxInvoiceType.Text = "STOCK"
            '            'TextBoxItemType.Enabled = False
            '        ElseIf Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("SubCode"))) = "DIST" Then
            '            TextBoxInvoiceType.Text = "OTHERS"
            '            'TextBoxItemType.Enabled = False
            '        End If

            '        'TextBoxInvoiceType.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("InvoiceType")))


            '        'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
            '        'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
            '        'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
            '        ''txtAmountWithDiscount.Text = Total - TotalDiscAmt
            '        'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
            '        'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

            '        'Dim Query As String

            '        'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
            '        'Query = "Select * from tblbillingproducts"
            '        'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

            '        'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
            '        'Query = "Select * from tblunitms"
            '        'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


            '        'Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
            '        'Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

            '        'If TextBoxItemType1.Text = "SERVICE" Then
            '        '    TextBoxQty1.Enabled = False
            '        '    TextBoxQty1.Text = 1
            '        '    TextBoxItemType1.Enabled = False
            '        'End If

            '        rowIndex += 1

            '    Next row

            '    'txtTotal.Text = Total.ToString("N2")

            '    'txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")


            '    'txtTotalDiscAmt.Text = 0.0
            '    'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
            '    'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
            '    'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
            'Else
            '    FirstGridViewRowBillingDetailsRecs()
            '    'FirstGridViewRowTarget()
            '    'Dim Query As String
            '    'Dim TextBoxTargetDesc As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
            '    'Query = "Select * from tblTarget"

            '    'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
            'End If

            ''End: Detail Records


            ''End: From tblBillingDetailItem
            ''End If

            ''AddNewRowBillingDetailsRecs()

            btnSave.Enabled = True
            'updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()
            'End: Billing Details
            updPanelSave.Update()
            'End: Populate the grid
            updPnlBillingRecs.Update()


            '''''''''''''''''''
            'Dim rowselected As Integer
            'rowselected = 0
            '

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnShowInvoices_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub txtTaxTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
            xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)


            'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(ddl1.FindControl("txtTaxTypeGV"), DropDownList)
            Dim lblid2 As TextBox = CType(ddl1.FindControl("txtGSTPercGV"), TextBox)


            'lTargetDesciption = lblid1.Text

            'Dim rowindex1 As Integer = ddl1.RowIndex

            Dim conn1 As MySqlConnection = New MySqlConnection(constr)
            conn1.Open()

            Dim commandGST As MySqlCommand = New MySqlCommand
            commandGST.CommandType = CommandType.Text
            commandGST.CommandText = "SELECT TaxRatePct FROM tbltaxtype where TaxType='" & lblid1.Text & "'"
            commandGST.Connection = conn1

            Dim drGST As MySqlDataReader = commandGST.ExecuteReader()
            Dim dtGST As New DataTable
            dtGST.Load(drGST)

            If dtGST.Rows.Count > 0 Then
                lblid2.Text = dtGST.Rows(0)("TaxRatePct").ToString

                CalculatePrice()
                'If dtGST.Rows(0)("GST").ToString = "P" Then
                '    lblAlert.Text = "SCHEUDLE HAS ALREADY BEEN GENERATED"
                '    conn1.Close()
                '    Exit Sub
                'End If
            End If

            conn1.Close()
            conn1.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtTaxTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub txtItemTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            lblAlert.Text = ""
            updPnlMsg.Update()

            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)



            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("txtItemTypeGV"), DropDownList)

            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtInvoiceNoGV"), TextBox)
            'Dim lblid3 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
            'Dim lblid4 As TextBox = CType(xrow1.FindControl("txtServiceRecordGV"), TextBox)

            'If (ddlDocType.SelectedIndex = 0) Then
            '    lblAlert.Text = "PLEASE ENTER VOUCHER TYPE"
            '    updPnlMsg.Update()
            '    ddlDocType.Focus()
            '    lblid1.SelectedIndex = 0
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If


            If lblid1.Text = "SERVICE" Then
                lblid2.Enabled = True
                'lblid3.Enabled = True
                'lblid4.Enabled = True
            Else
                lblid2.Text = ""
                'lblid3.Text = ""
                'lblid4.Text = ""
                'lblid2.Enabled = False
                'lblid3.Enabled = False
                'lblid4.Enabled = False
            End If

            Dim rowindex1 As Integer = xrow1.RowIndex
            If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
                btnAddDetail_Click(sender, e)
                'txtRecordAdded.Text = "Y"
            End If

            'CalculateTotalPrice()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtItemTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try

    End Sub

    Protected Sub txtItemCodeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            'Dim xgrvBillingDetails As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)

            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
            xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)

            Dim lblid1 As DropDownList = CType(xgrvBillingDetails.FindControl("txtItemCodeGV"), DropDownList)
            Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtItemDescriptionGV"), TextBox)
            Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGV"), TextBox)
            Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
            Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtOtherCodeGV"), TextBox)
            Dim lblid6 As DropDownList = CType(xgrvBillingDetails.FindControl("txtTaxTypeGV"), DropDownList)
            Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)

            'lTargetDesciption = lblid1.Text

            Dim rowindex1 As Integer = xgrvBillingDetails.RowIndex

            'Get Item desc, price Id

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT * FROM tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = '" & lblid1.Text & "'"
            command1.CommandText = "SELECT * FROM tblbillingproducts  where   ProductCode = '" & lblid1.Text & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lblid2.Text = dt.Rows(0)("Description").ToString
                lblid3.Text = dt.Rows(0)("Price").ToString
                lblid4.Text = 1
                lblid5.Text = dt.Rows(0)("COACode").ToString
                lblid6.Text = dt.Rows(0)("TaxType").ToString
                lblid7.Text = dt.Rows(0)("TaxRate").ToString
                'CalculatePrice()
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

            'If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
            '    btnAddDetail_Click(sender, e)
            '    'txtRecordAdded.Text = "Y"
            'End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtItemCodeGV_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub grvBillingDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grvBillingDetails.RowDeleting
        Try

            'If txtRecordDeleted.Text = "Y" Then
            '    txtRecordDeleted.Text = "N"
            '    Exit Sub
            'End If

            lblAlert.Text = ""
            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

                SetRowDataBillingDetailsRecs()
                Dim dt As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)


                'Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                'Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceGV"), TextBox)

                'InvoiceNumber()


                'Dim TextBoxRcnoCNDet As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)

                'Dim conn As MySqlConnection = New MySqlConnection(constr)
                ''Dim TextBoxRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                'If (String.IsNullOrEmpty(TextBoxRcnoInvoice.Text) = False) Then
                '    If (Convert.ToInt32(TextBoxRcnoInvoice.Text) > 0) Then

                '        'Dim conn As MySqlConnection = New MySqlConnection(constr)
                '        conn.Open()

                '        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                '        commandUpdGS.CommandType = CommandType.Text
                '        commandUpdGS.CommandText = "Delete from tblSalesDetail where rcno = " & TextBoxInvoiceNo.Text
                '        commandUpdGS.Connection = conn
                '        commandUpdGS.ExecuteNonQuery()



                '        'Updatetblservicebillingdetailitem(Convert.ToInt64(TextBoxRcnoInvoice.Text))
                '    End If
                'End If
                'conn.Close()
                If dt.Rows.Count > 0 Then
                    dt.Rows.Remove(dt.Rows(rowIndex))
                    drCurrentRow = dt.NewRow()
                    ViewState("CurrentTableBillingDetailsRec") = dt
                    grvBillingDetails.DataSource = dt
                    grvBillingDetails.DataBind()

                    rowdeleted = "Y"
                    SetPreviousDataBillingDetailsRecs()

                    calculateTotalCN()
                    rowdeleted = "N"

                    If dt.Rows.Count = 0 Then
                        FirstGridViewRowBillingDetailsRecs()
                    End If

                    'Dim commandUpdRecv As MySqlCommand = New MySqlCommand
                    'commandUpdRecv.CommandType = CommandType.Text
                    'commandUpdRecv.CommandText = "Update tblcn set NetAmount =" & Convert.ToDecimal(txtCreditAmount.Text) & " where CNNumber = '" & txtCNNo.Text & "'"
                    'commandUpdRecv.Connection = conn
                    'commandUpdRecv.ExecuteNonQuery()

                    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                End If

                'CalculateTotalPrice()

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "grvBillingDetails_RowDeleting", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub grvBillingDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvBillingDetails.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' Delete

                For Each cell As DataControlFieldCell In e.Row.Cells
                    ' check all cells in one row
                    For Each control As Control In cell.Controls

                        Dim button As ImageButton = TryCast(control, ImageButton)
                        If button IsNot Nothing AndAlso button.CommandName = "Delete" Then
                            'button.OnClientClick = "if (!confirm('Are you sure to DELETE this record?')) return;"
                            button.OnClientClick = "Confirm()"
                        End If
                    Next control
                Next cell

            End If



        Catch ex As Exception

            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "grvBillingDetails_RowDataBound", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
        mdlPopupStatusSearch.Show()
    End Sub

    Protected Sub btnStatusSearch_Click(sender As Object, e As EventArgs)
        'Try
        '    Dim YrStrList As List(Of [String]) = New List(Of String)()

        '    'If rdbStatusSearch.SelectedValue = "ALL" Then
        '    '    For Each item As ListItem In chkStatusSearch.Items
        '    '        YrStrList.Add(item.Value)
        '    '    Next
        '    'Else
        '    For Each item As ListItem In chkStatusSearch.Items
        '        If item.Selected Then
        '            YrStrList.Add(item.Value)
        '        End If
        '    Next

        '    Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

        '    txtSearch1Status.Text = YrStr
        'Catch ex As Exception
        '    Dim exstr As String
        '    exstr = ex.Message.ToString
        '    'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        '    lblAlert.Text = exstr
        '    'Dim message As String = "alert('" + exstr + "')"
        '    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        'End Try
    End Sub

    Protected Sub txtAccountId_TextChanged(sender As Object, e As EventArgs)
        'Dim query As String
        'query = "Select ContractNo from tblContract where AccountId = '" & txtAccountId.Text & "'"
        'PopulateDropDownList(query, "ContractNo", "ContractNo", ddlContractNo)
    End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        txtReceiptnoSearch.Text = ""
        txtAccountIdSearch.Text = ""
        'txtBillingPeriodSearch.Text = ""
        txtClientNameSearch.Text = ""
        ddlCompanyGrpSearch.SelectedIndex = 0
        'ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O,P"
        'ddlCompanyGrpSearch.SelectedIndex = 0
        txtLedgerSearch.Text = ""


        'txtSearch1Status.Text = "O,P"

        txtLedgerSearch.Text = ""
        'txtAccountIdSearch.Text = ""
        'txtClientNameSearch.Text = ""
        txtReceiptnoSearch.Text = ""

        txtInvoiceDateFromSearch.Text = ""
        txtInvoiceDateToSearch.Text = ""

        txtAmountSearch.Text = ""
        txtInvoiceNoSearch.Text = ""

        ddlContactTypeSearch.SelectedIndex = 0

        'ddlCompanyGrpSearch.SelectedIndex = 0

        'btnQuickSearch_Click(sender, e)


        'btnSearch1Status_Click(sender, e)
    End Sub

    Protected Sub chkSelectGV_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
            xgrvBillingDetails = CType(chk1.NamingContainer, GridViewRow)


            Dim lblid1 As TextBox = CType(chk1.FindControl("txtPriceWithDiscGV"), TextBox)
            Dim lblid2 As TextBox = CType(chk1.FindControl("txtTotalReceiptAmtGV"), TextBox)
            Dim lblid3 As TextBox = CType(chk1.FindControl("txtTotalCreditNoteAmtGV"), TextBox)
            Dim lblid4 As TextBox = CType(chk1.FindControl("txtReceiptAmtGV"), TextBox)
            Dim lblid5 As TextBox = CType(chk1.FindControl("txtCNGSTAmtGV"), TextBox)
            Dim lblid6 As TextBox = CType(chk1.FindControl("txtGSTPercGV"), TextBox)


            'If chk1.Checked = True Then
            '    lblid3.Text = lblid2.Text
            'Else
            '    lblid3.Text = "0.00"
            'End If
            ''lblid2.Text = lblid1.Text


            If String.IsNullOrEmpty(lblid1.Text) = True Then
                lblid1.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid2.Text) = True Then
                lblid2.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid3.Text) = True Then
                lblid3.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid4.Text) = True Then
                lblid4.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid5.Text) = True Then
                lblid5.Text = "0.00"
            End If



            If chk1.Checked = True Then
                lblid4.Text = lblid1.Text - lblid2.Text - lblid3.Text
            Else
                lblid4.Text = "0.00"
            End If

            lblid4.Text = Convert.ToDecimal(lblid4.Text).ToString("N2")

            lblid5.Text = Convert.ToDecimal(Convert.ToDecimal(lblid4.Text).ToString("N2") * Convert.ToDecimal(lblid6.Text).ToString("N2") * 0.01).ToString("N2")
            calculateTotalCN()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "chkSelectGV_CheckedChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub calculateTotalCN()
        Try
            Dim TotalCNAmt As Decimal = 0
            Dim TotalGSTAmt As Decimal = 0
            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


            If (table.Rows.Count > 0) Then

                For i As Integer = 0 To (table.Rows.Count) - 1

                    Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)
                    Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceGV"), TextBox)
                    Dim TextBoxCNGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtGSTAmtGV"), TextBox)

                    If String.IsNullOrEmpty(TextBoxReceiptAmt.Text) = True Then
                        TextBoxReceiptAmt.Text = "0.00"
                    End If

                    If String.IsNullOrEmpty(TextBoxCNGSTAmt.Text) = True Then
                        TextBoxCNGSTAmt.Text = "0.00"
                    End If


                    'If TextBoxSelect.Checked = True Then
                    TotalCNAmt = TotalCNAmt + Convert.ToDecimal(TextBoxReceiptAmt.Text)
                    txtCreditAmount.Text = TotalCNAmt

                    TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxCNGSTAmt.Text)
                    txtCreditAmount.Text = TotalGSTAmt
                    'End If
                Next i

            End If


            txtTotalCNAmt.Text = TotalCNAmt.ToString("N2")
            txtCreditAmount.Text = TotalCNAmt.ToString("N2")



            txtTotalCNGSTAmt.Text = TotalGSTAmt.ToString("N2")
            txtDebitAmount.Text = TotalGSTAmt.ToString("N2")

            txtCreditAmount.Text = (TotalCNAmt + TotalGSTAmt).ToString("N2")
            updPanelSave.Update()
            updPnlBillingRecs.Update()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "calculateTotalCN", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub

    Protected Sub txtReceiptAmtGV_TextChanged(sender As Object, e As EventArgs)

        Dim txt1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

        Dim lblid1 As CheckBox = CType(txt1.FindControl("chkSelectGV"), CheckBox)
        Dim lblid2 As TextBox = CType(txt1.FindControl("txtReceiptAmtGV"), TextBox)
        'Dim lblid3 As TextBox = CType(txt1.FindControl("txtBalanceAmtGV"), TextBox)

        Dim lblid5 As TextBox = CType(txt1.FindControl("txtCNGSTAmtGV"), TextBox)
        Dim lblid6 As TextBox = CType(txt1.FindControl("txtGSTPercGV"), TextBox)


        If Convert.ToDecimal(lblid2.Text) > 0.0 Then
            lblid1.Checked = True
        Else
            lblid1.Checked = False

        End If

        lblid5.Text = Convert.ToDecimal(Convert.ToDecimal(lblid2.Text).ToString("N2") * Convert.ToDecimal(lblid6.Text).ToString("N2") * 0.01).ToString("N2")
        'If Convert.ToDecimal(lblid2.Text) > Convert.ToDecimal(lblid3.Text) Then
        '    lblid2.Text = lblid3.Text
        'End If

        lblid2.Text = Convert.ToDecimal(lblid2.Text).ToString("N2")
        calculateTotalCN()
    End Sub


    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientSearch.Click
        'If txtPopUpClient.Text.Trim = "" Then
        '    MessageBox.Message.Alert(Page, "Please enter client name", "str")
        'Else
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
        '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"

        '    SqlDSClient.DataBind()
        '    gvClient.DataBind()
        '    mdlPopUpClient.Show()
        'End If
    End Sub

    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        Try
            gvClient.PageIndex = e.NewPageIndex

            'If txtPopUpClient.Text.Trim = "" Then
            '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster "
            'Else
            '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            'End If
            'SqlDSClient.DataBind()
            'gvClient.DataBind()
            'mdlPopUpClient.Show()


            'If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and  (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
            '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and  (Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"


            'Else
            '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "')"
            '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
            '    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and (upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like '" + txtPopUpClient.Text + "%' or contperson like '%" + txtPopUpClient.Text + "%') and (Upper(ContType) = '" + txtContType2.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"
            'End If


            'If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
            '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany order by name"
            '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON order by name"
            '    End If
            'Else
            '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
            '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1 and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
            '    End If

            '    'SqlDSClient.DataBind()
            '    'gvClient.DataBind()
            '    'mdlPopUpClient.Show()
            'End If

            If txtSearch.Text = "ImportService" Or txtSearch.Text = "ImportInvoice" Then
                SqlDSClient.SelectCommand = txtImportService.Text
            End If

            If txtSearch.Text = "CustomerSearch" Then
                SqlDSClient.SelectCommand = txtCustomerSearch.Text
            End If

            If txtSearch.Text = "InvoiceSearch" Then
                SqlDSClient.SelectCommand = txtInvoiceSearch.Text
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "gvClient_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



    Protected Sub ClientAlphabet_Click(sender As Object, e As EventArgs)
        ''please check when user enter search criteria for one alphabet and then without clearing the textPoPUp client
        ''select another alphabet ---records are not selected

        'Dim lnkAlphabet As LinkButton = DirectCast(sender, LinkButton)
        'ViewState("ClientCurrentAlphabet") = lnkAlphabet.Text
        'Me.GenerateClientAlphabets()
        'gvClient.PageIndex = 0
        ''If txtPopUpClient.Text.Trim = "" Then
        ''    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 And ContName Like '" + lnkAlphabet.Text + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        ''Else
        ''    SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + lnkAlphabet.Text + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        ''End If

        'If txtPopUpClient.Text.Trim = "" Then
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 And ContName Like '" + lnkAlphabet.Text + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        '    If ddlContactType.SelectedValue.ToString = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where 1=1 And Name Like '" + lnkAlphabet.Text + "%'"
        '    Else
        '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPerson where 1=1 And Name Like '" + lnkAlphabet.Text + "%'"
        '    End If

        'Else
        '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + lnkAlphabet.Text + "%' And upper(ContName) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' and Upper(ContType) = '" + ddlContactType.SelectedValue.ToString + "'"
        '    If ddlContactType.SelectedValue.ToString = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT distinct * From tblCompany where 1=1 and Name like '" + lnkAlphabet.Text + "%' And upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
        '    Else
        '        SqlDSClient.SelectCommand = "SELECT distinct * From tblPerson where 1=1 and Name like '" + lnkAlphabet.Text + "%' And upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%'"
        '    End If
        'End If

        'SqlDSClient.DataBind()
        'gvClient.DataBind()
        'mdlPopUpClient.Show()
    End Sub

    'Protected Sub txtAccountIdBilling_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIdBilling.TextChanged
    '    If Len(txtAccountIdBilling.Text) > 2 Then
    '        btnClient_Click(sender, New ImageClickEventArgs(0, 0))
    '    End If
    'End Sub

    'Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
    '    Try
    '        lblAlert.Text = ""
    '        txtSearch.Text = ""
    '        txtPopUpClient.Text = ""

    '        txtSearch.Text = "CustomerSearch"

    '        If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = False Then
    '            txtPopUpClient.Text = txtAccountIdBilling.Text
    '            txtPopupClientSearch.Text = txtPopUpClient.Text

    '            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
    '            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '                SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
    '            Else
    '                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"

    '            End If

    '            SqlDSClient.DataBind()
    '            gvClient.DataBind()
    '        Else



    '            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
    '            ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '                SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
    '            Else
    '                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"

    '            End If

    '            SqlDSClient.DataBind()
    '            gvClient.DataBind()
    '        End If
    '        txtCustomerSearch.Text = SqlDSClient.SelectCommand
    '        mdlPopUpClient.Show()
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnClient_Click", ex.Message.ToString, "")
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub UpdateTblSales(strItemType As String, strRefType As String, dblDebitAmt As Decimal, dblCreditAmt As Decimal)
        Try
            Dim lTotalReceipt As Decimal
            Dim lInvoiceAmount As Decimal
            Dim lTotalcn As Decimal
            lTotalReceipt = 0.0
            lInvoiceAmount = 0.0
            lTotalcn = 0.0
            'Get Item desc, price Id

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            If strItemType = "INVOICE" Then

                Dim commandCN As MySqlCommand = New MySqlCommand
                commandCN.CommandType = CommandType.Text


                commandCN.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblSalesDetail A, tblSales B WHERE " & _
                    "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and A.SourceInvoice = '" & strRefType & "'"

                commandCN.Connection = conn


                Dim dr2 As MySqlDataReader = commandCN.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    lTotalcn = dt2.Rows(0)("totalcn").ToString
                End If
                'lTotalcn = lTotalcn * (-1)

                commandCN.Dispose()
                ''''''''''''''''''''''''


                '''''''''''''' Journal

                Dim lTotalJV As Decimal
                Dim commandJournal As MySqlCommand = New MySqlCommand
                commandJournal.CommandType = CommandType.Text

                commandJournal.CommandText = "SELECT   ifnull(SUM(ifnull(A.DebitBase,0)),0) as debitbase, ifnull(SUM(ifnull(A.CreditBase,0)),0) as creditbase  FROM tbljrnvdet A, tbljrnv B WHERE " & _
                   "A.VoucherNumber=B.VoucherNumber AND  B.PostStatus = 'P'  and A.RefType = '" & strRefType & "' "

                commandJournal.Connection = conn

                Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
                Dim dtJournal As New DataTable
                dtJournal.Load(drJournal)

                If dtJournal.Rows.Count > 0 Then
                    lTotalJV = Convert.ToDecimal(dtJournal.Rows(0)("debitbase").ToString - dtJournal.Rows(0)("creditbase").ToString)
                End If

                ''''''''''''''' Journal

                Dim lbalance As Decimal
                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text
                command3.CommandText = "SELECT ValueBase, GSTBase , AppliedBase , ReceiptBase, CreditBase, CreditBase FROM tblSales where InvoiceNumber = '" & strRefType & "'"

                command3.Connection = conn
                command3.ExecuteNonQuery()

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New DataTable
                dt3.Load(dr3)

                If dt3.Rows.Count > 0 Then


                    If String.IsNullOrEmpty(dt3.Rows(0)("AppliedBase").ToString) = False Then
                        lbalance = dt3.Rows(0)("AppliedBase").ToString
                    Else
                        lbalance = 0.0
                    End If

                    If String.IsNullOrEmpty(dt3.Rows(0)("ReceiptBase").ToString) = False Then
                        lbalance = lbalance - dt3.Rows(0)("ReceiptBase").ToString
                    Else
                        'lbalance = 0.0
                    End If

                    lbalance = lbalance + lTotalcn + lTotalJV
                    'If String.IsNullOrEmpty(dt3.Rows(0)("CreditBase").ToString) = False Then
                    '    lbalance = lbalance - dt3.Rows(0)("CreditBase").ToString
                    'Else
                    '    'lbalance = 0.0
                    'End If

                End If

                ''''''''''' Journal




                Dim lcredbal As Decimal
                lcredbal = 0.0
                lcredbal = (lTotalcn + lTotalJV) * (-1)

                Dim command4 As MySqlCommand = New MySqlCommand
                command4.CommandType = CommandType.Text

                Dim qry4 As String = "Update tblSales Set CreditBase = " & lcredbal & ", BalanceBase = " & lbalance & " where InvoiceNumber = @InvoiceNumber "

                command4.CommandText = qry4
                command4.Parameters.Clear()

                command4.Parameters.AddWithValue("@InvoiceNumber", strRefType)
                command4.Connection = conn
                command4.ExecuteNonQuery()

                '    'End: Update tblSales

                '''''''''''''' 01.12.18
            End If





            'If strItemType = "RECEIPT" Then

            '    Dim commandCN As MySqlCommand = New MySqlCommand
            '    commandCN.CommandType = CommandType.Text


            '    commandCN.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblRecvDet A, tblRecv B WHERE " & _
            '        "A.ReceiptNumber=B.ReceiptNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and A.SourceInvoice = '" & strRefType & "'"

            '    commandCN.Connection = conn


            '    Dim dr2 As MySqlDataReader = commandCN.ExecuteReader()
            '    Dim dt2 As New DataTable
            '    dt2.Load(dr2)

            '    If dt2.Rows.Count > 0 Then
            '        lTotalcn = dt2.Rows(0)("totalcn").ToString
            '    End If
            '    'lTotalcn = lTotalcn * (-1)

            '    commandCN.Dispose()
            '    ''''''''''''''''''''''''


            '    '''''''''''''' Journal

            '    Dim lTotalJV As Decimal
            '    Dim commandJournal As MySqlCommand = New MySqlCommand
            '    commandJournal.CommandType = CommandType.Text

            '    commandJournal.CommandText = "SELECT ifnull(SUM(ifnull(A.DebitBase,0)),0) as debitbase, ifnull(SUM(ifnull(A.CreditBase,0)),0) as creditbase  FROM tbljrnvdet A, tbljrnv B WHERE " & _
            '       "A.VoucherNumber=B.VoucherNumber AND  B.PostStatus = 'P'  and A.RefType = '" & strRefType & "' "

            '    commandJournal.Connection = conn

            '    Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
            '    Dim dtJournal As New DataTable
            '    dtJournal.Load(drJournal)

            '    If dtJournal.Rows.Count > 0 Then
            '        lTotalJV = Convert.ToDecimal(dtJournal.Rows(0)("debitbase").ToString - dtJournal.Rows(0)("creditbase").ToString)
            '    End If

            '    ''''''''''''''' Journal

            '    Dim lbalance As Decimal
            '    Dim command3 As MySqlCommand = New MySqlCommand
            '    command3.CommandType = CommandType.Text
            '    command3.CommandText = "SELECT  AppliedBase FROM tblRecv where ReceiptNumber = '" & strRefType & "'"

            '    command3.Connection = conn
            '    command3.ExecuteNonQuery()

            '    Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            '    Dim dt3 As New DataTable
            '    dt3.Load(dr3)

            '    If dt3.Rows.Count > 0 Then


            '        If String.IsNullOrEmpty(dt3.Rows(0)("AppliedBase").ToString) = False Then
            '            lbalance = dt3.Rows(0)("AppliedBase").ToString
            '        Else
            '            lbalance = 0.0
            '        End If

            '        'If String.IsNullOrEmpty(dt3.Rows(0)("ReceiptBase").ToString) = False Then
            '        '    lbalance = lbalance - dt3.Rows(0)("ReceiptBase").ToString
            '        'Else
            '        '    'lbalance = 0.0
            '        'End If

            '        lbalance = lbalance + lTotalcn + lTotalJV
            '        'If String.IsNullOrEmpty(dt3.Rows(0)("CreditBase").ToString) = False Then
            '        '    lbalance = lbalance - dt3.Rows(0)("CreditBase").ToString
            '        'Else
            '        '    'lbalance = 0.0
            '        'End If

            '    End If

            '    ''''''''''' Journal




            '    Dim lcredbal As Decimal
            '    lcredbal = 0.0
            '    lcredbal = (lTotalcn + lTotalJV) * (-1)

            '    Dim command4 As MySqlCommand = New MySqlCommand
            '    command4.CommandType = CommandType.Text

            '    Dim qry4 As String = "Update tblRecv Set BalanceBase = " & lbalance & " where ReceiptNumber = @ReceiptNumber "

            '    command4.CommandText = qry4
            '    command4.Parameters.Clear()

            '    command4.Parameters.AddWithValue("@ReceiptNumber", strRefType)
            '    command4.Connection = conn
            '    command4.ExecuteNonQuery()

            '    '    'End: Update tblSales

            '    '''''''''''''' 01.12.18
            'End If
         
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "UpdateTblSales", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub

    Private Function PostCN() As Boolean
        Try

            Dim IsSuccess As Boolean = False
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblJrnv set PostStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()

            '''''''''''''''''''''

            Dim command5 As MySqlCommand = New MySqlCommand
            command5.CommandType = CommandType.Text

            Dim qry5 As String = "DELETE from tblAR where VoucherNumber = '" & txtCNNo.Text & "'"

            command5.CommandText = qry5
            'command1.Parameters.Clear()
            command5.Connection = conn
            command5.ExecuteNonQuery()

            Dim qryAR As String
            Dim commandAR As MySqlCommand = New MySqlCommand
            commandAR.CommandType = CommandType.Text

            If Convert.ToDecimal(txtCreditAmount.Text) > 0.0 Then
                'qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, BillingPeriod, "
                'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @BillingPeriod,"
                'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                'commandAR.CommandText = qryAR
                'commandAR.Parameters.Clear()
                'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                'If txtCNDate.Text.Trim = "" Then
                '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                'Else
                '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                'End If
                'commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
                'commandAR.Parameters.AddWithValue("@ContractNo", "")
                'commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
                'commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                ''commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtReceivedAmount.Text))
                'commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                'commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                'commandAR.Connection = conn
                'commandAR.ExecuteNonQuery()
            End If


            'Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            commandValues.CommandText = "SELECT *  FROM tbljrnvdet where VoucherNumber ='" & txtCNNo.Text.Trim & "'"
            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)


            Dim lTotalReceiptAmt As Decimal
            Dim lInvoiceAmt As Decimal
            Dim lReceptAmtAdjusted As Decimal

            lTotalReceiptAmt = 0.0
            lInvoiceAmt = 0.0

            'lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            Dim rowindex = 0


            For Each row As DataRow In dtValues.Rows



                'qryAR = "INSERT INTO tblAR(VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, ServiceRecordNo, ServiceDate, BillingPeriod, Salesman, InvoiceType, GLType,  "
                'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @ServiceRecordNo, @ServiceDate,  @BillingPeriod, @Salesman, @InvoiceType, @GLType,"
                'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                'commandAR.CommandText = qryAR
                'commandAR.Parameters.Clear()
                'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                'If txtCNDate.Text.Trim = "" Then
                '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                'Else
                '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                'End If
                'commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
                'commandAR.Parameters.AddWithValue("@ContractNo", row("ContractNo"))
                'commandAR.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))
                'commandAR.Parameters.AddWithValue("@GLCode", row("LedgerCode"))
                'commandAR.Parameters.AddWithValue("@GLDescription", row("LedgerName"))
                'commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)

                ''commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("ReceiptValue")))
                'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(dtValues.Rows(rowindex)("ReceiptValue").ToString))

                'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                'commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                'commandAR.Parameters.AddWithValue("@GLType", "Debtor")
                'commandAR.Parameters.AddWithValue("@ServiceRecordNo", row("ServiceRecordNo"))

                'commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                ''Else
                ''commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lServiceDate).ToString("yyyy-MM-dd"))
                ''End If


                ''commandAR.Parameters.AddWithValue("@Salesman", txtSalesman.Text)
                'commandAR.Parameters.AddWithValue("@InvoiceType", row("InvoiceType"))

                'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                'commandAR.Connection = conn
                'commandAR.ExecuteNonQuery()


                ''Start: Update tblSales

                ' ''''''''''''''''''''

                If String.IsNullOrEmpty(row("RefType")) = False Then
                    'UpdateTblSales(row("SourceInvoice"), row("RefType"), row("ValueBase") * (-1))
                    UpdateTblSales(row("ItemType"), row("RefType"), row("Debitbase"), row("CreditBase"))
                End If



                rowindex = rowindex + 1
            Next row


            GridView1.DataBind()
            conn.Close()
            conn.Dispose()
            command5.Dispose()
            commandValues.Dispose()

            IsSuccess = True
            Return IsSuccess
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PostAN", ex.Message.ToString, txtCNNo.Text)
            IsSuccess = False
            Return IsSuccess
            Exit Function
        End Try

    End Function

    Private Function ReverseCN() As Boolean
        Try
            'Dim confirmValue As String
            'confirmValue = ""


            'confirmValue = Request.Form("confirm_value")
            'If Right(confirmValue, 3) = "Yes" Then
            ''''''''''''''' Insert tblAR

            ''''''''''''''''''''
            'PopulateArCode()

            '''''''''''''''''''''''''
            Dim IsSuccess As Boolean = False

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command5 As MySqlCommand = New MySqlCommand
            command5.CommandType = CommandType.Text

            Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtCNNo.Text & "'"

            command5.CommandText = qry5
            'command1.Parameters.Clear()
            command5.Connection = conn
            command5.ExecuteNonQuery()


            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblJrnv set PostStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()


            ''''''''''''''''''''''''''''''''

            ''Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            'commandValues.CommandText = "SELECT *  FROM tblSalesdetail where InvoiceNumber ='" & txtCNNo.Text.Trim & "'"
            commandValues.CommandText = "SELECT *  FROM tbljrnvdet where VoucherNumber ='" & txtCNNo.Text.Trim & "'"
            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)


            'Dim lTotalReceiptAmt As Decimal
            'Dim lInvoiceAmt As Decimal
            'Dim lReceptAmtAdjusted As Decimal

            'lTotalReceiptAmt = 0.0
            'lInvoiceAmt = 0.0

            'lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            'Dim rowindex = 0

            For Each row As DataRow In dtValues.Rows

                ''Start: Update tblSales

                ' ''''''''''''''''''''

                If String.IsNullOrEmpty(row("RefType")) = False Then
                    'UpdateTblSales(row("SourceInvoice"), row("RefType"), row("ValueBase") * (-1))
                    UpdateTblSales(row("ItemType"), row("RefType"), row("Debitbase") * (-1), row("CreditBase") * (-1))
                End If
                'If IsDBNull(row("SourceInvoice")) = False Then
                '    If String.IsNullOrEmpty(row("SourceInvoice")) = False Then
                '        UpdateTblSales(row("SourceInvoice"), row("RefType"), row("ValueBase"))
                '    End If
                'End If

            Next

            conn.Close()
            conn.Dispose()

            command5.Dispose()
            commandUpdateInvoice.Dispose()

            '''''''''''''''''''''''''''''''''
            'GridView1.DataBind()


            'lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'lblAlert.Text = ""
            'updPnlSearch.Update()
            'updPnlMsg.Update()
            'updpnlBillingDetails.Update()
            ''updpnlServiceRecs.Update()
            'updpnlBillingDetails.Update()

            'btnQuickSearch_Click(sender, e)

            'btnChangeStatus.Enabled = False
            'btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            'btnEdit.Enabled = True
            'btnEdit.ForeColor = System.Drawing.Color.Black

            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black

            'btnPost.Enabled = True
            'btnPost.ForeColor = System.Drawing.Color.Black
            'End If


            'End: Loop thru' Credit Values


            ''''''''''''''' Insert tblAR

            IsSuccess = True
            Return IsSuccess
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "ReverseAN", ex.Message.ToString, txtCNNo.Text)
            Dim IsSuccess As Boolean
            IsSuccess = False
            Return IsSuccess
            Exit Function
        End Try
    End Function

    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""


            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            Dim IsLock = FindJNPeriod(txtReceiptPeriod.Text)
            If IsLock = "Y" Then
                lblAlert.Text = "PERIOD IS LOCKED"
                updPnlMsg.Update()
                txtCNDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            mdlPopupConfirmPost.Show()


        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnPost_Click", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMode.Text = ""
        'txtMode.Text = "View"
        MakeMeNullBillingDetails()
        MakeMeNull()
        DisableControls()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Session("receiptfrom") = "invoice"

        Response.Redirect("Invoice.aspx")
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        Dim strsql As String
        strsql = ""


        If txtPopUpClient.Text.Trim = "" Then
            MessageBox.Message.Alert(Page, "Please enter client name", "str")
            Exit Sub
        End If

        Try

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            Else
                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            End If
            'If txtSearch.Text = "ImportService" Then


            '    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    Else
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            '    End If
            '    txtImportService.Text = SqlDSClient.SelectCommand
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
            '    mdlPopUpClient.Show()
            '    txtIsPopup.Text = "Client"
            'End If

            'If txtSearch.Text = "ImportInvoice" Then


            '    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by ServiceName"
            '    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    Else
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            '    End If
            '    txtImportService.Text = SqlDSClient.SelectCommand
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
            '    mdlPopUpClient.Show()
            '    txtIsPopup.Text = "Client"
            'End If

            'If txtSearch.Text = "CustomerSearch" Then


            '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    Else
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            '    End If
            '    txtCustomerSearch.Text = SqlDSClient.SelectCommand
            '    SqlDSClient.DataBind()
            '    gvClient.DataBind()
            '    mdlPopUpClient.Show()
            '    txtIsPopup.Text = "Client"

            'End If

            'If txtSearch.Text = "InvoiceSearch" Then



            '    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
            ''        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
            '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like '%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
            '    Else
            '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
            '    End If
            '    txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"

            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub DisplayGLGrid()
        Try

            ''''''''''''''''' Start: Display GL Grid

            FirstGridViewRowGL()

            updPnlBillingRecs.Update()

            'Start: GL Details

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvGL.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableGL") = dtScdrLoc
                grvGL.DataSource = dtScdrLoc
                grvGL.DataBind()


                SetPreviousDataGL()
            Next i

            FirstGridViewRowGL()

            Dim rowIndex3 = 0

            ''AR Values

            'AddNewRowGL()




            'rowIndex3 += 1
            AddNewRowGL()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            Dim cmdGL As MySqlCommand = New MySqlCommand
            cmdGL.CommandType = CommandType.Text
            'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblsalesDetail where InvoiceNumber ='" & txtInvoiceNo.Text.Trim & "' and InvoiceNo ='" & txtInvoiceNo.Text & "' order by OtherCode"
            cmdGL.CommandText = "SELECT LedgerCode, LedgerName, debitbase, CreditBase   FROM tbljrnvdet where VoucherNumber ='" & txtCNNo.Text.Trim & "' order by LedgerCode"

            'cmdGL.CommandText = "SELECT * FROM tblAR where BatchNo ='" & txtBatchNo.Text.Trim & "'"
            cmdGL.Connection = conn

            Dim drcmdGL As MySqlDataReader = cmdGL.ExecuteReader()
            Dim dtGL As New DataTable
            dtGL.Load(drcmdGL)

            'FirstGridViewRowGL()


            Dim TotDetailRecordsLoc = dtGL.Rows.Count
            If dtGL.Rows.Count > 0 Then

                lGLCode = ""
                lGLDescription = ""
                lCreditAmount = 0.0
                lDebitAmount = 0.0

                lGLCode = dtGL.Rows(0)("LedgerCode").ToString()
                lGLDescription = dtGL.Rows(0)("LedgerName").ToString()
                lCreditAmount = 0.0
                lDebitAmount = 0.0

                Dim rowIndex4 = 0

                For Each row As DataRow In dtGL.Rows

                    If lGLCode = row("LedgerCode") Then
                        lCreditAmount = lCreditAmount + row("CreditBase")
                        lDebitAmount = lDebitAmount + row("debitbase")
                    Else


                        If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
                            AddNewRowGL()
                        End If

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        TextBoxGLCode.Text = lGLCode

                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        TextBoxGLDescription.Text = lGLDescription

                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                        TextBoxDebitAmount.Text = lDebitAmount.ToString("N2")

                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
                        TextBoxCreditAmount.Text = lCreditAmount.ToString("N2")

                        lGLCode = ""
                        lGLDescription = ""

                        lCreditAmount = 0.0
                        lDebitAmount = 0.0


                        lGLCode = row("LedgerCode")
                        lGLDescription = row("LedgerName")

                        lCreditAmount = lCreditAmount + row("CreditBase")
                        lDebitAmount = lDebitAmount + row("debitbase")

                        rowIndex3 += 1
                        'rowIndex4 += 1
                    End If
                Next row

            End If


            AddNewRowGL()

            Dim TextBoxGLCode1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCode1.Text = lGLCode

            Dim TextBoxGLDescription1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescription1.Text = lGLDescription

            Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmount1.Text = lDebitAmount.ToString("N2")

            Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")


            SetRowDataGL()
            Dim dtScdrLoc1 As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
            Dim drCurrentRowLoc1 As DataRow = Nothing

            dtScdrLoc1.Rows.Remove(dtScdrLoc1.Rows(rowIndex3 + 1))
            drCurrentRowLoc1 = dtScdrLoc1.NewRow()
            ViewState("CurrentTableGL") = dtScdrLoc1
            grvGL.DataSource = dtScdrLoc1
            grvGL.DataBind()
            SetPreviousDataGL()
            'conn.Close()
            conn.Close()
            conn.Dispose()
            cmdGL.Dispose()
            dtGL.Dispose()
            dtScdrLoc.Dispose()
            dtScdrLoc1.Dispose()
            drcmdGL.Close()
            ''''''''''''''''' End: Display GL Grid
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "DisplayGLGrid", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub FirstGridViewRowGL()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("GLCode", GetType(String)))
            dt.Columns.Add(New DataColumn("GLDescription", GetType(String)))
            dt.Columns.Add(New DataColumn("DebitAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("CreditAmount", GetType(String)))

            dr = dt.NewRow()

            dr("GLCode") = String.Empty
            dr("GLDescription") = String.Empty
            dr("DebitAmount") = String.Empty
            dr("CreditAmount") = String.Empty
            dt.Rows.Add(dr)

            ViewState("CurrentTableGL") = dt

            grvGL.DataSource = dt
            grvGL.DataBind()

            'Dim btnAdd As Button = CType(grvServiceRecDetails.FooterRow.Cells(1).FindControl("btnViewEdit"), Button)
            'Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "FirstGridViewRowGL", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub




    Private Sub AddNewRowGL()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String

            If ViewState("CurrentTableGL") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)
                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
                        dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text

                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableGL") = dtCurrentTable

                    grvGL.DataSource = dtCurrentTable
                    grvGL.DataBind()


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataGL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewRowWithDetailRecGL()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String
            If ViewState("CurrentTableGL") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If TotDetailRecords > 0 Then
                    For i As Integer = 1 To (dtCurrentTable.Rows.Count)

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
                        dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text

                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableGL") = dtCurrentTable

                    grvBillingDetails.DataSource = dtCurrentTable
                    grvBillingDetails.DataBind()
                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataGL()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetPreviousDataGL()
        Try
            Dim rowIndex As Integer = 0

            'Dim Query As String

            If ViewState("CurrentTableGL") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

                        TextBoxGLCode.Text = dt.Rows(i)("GLCode").ToString()
                        TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
                        TextBoxDebitAmount.Text = dt.Rows(i)("DebitAmount").ToString()
                        TextBoxCreditAmount.Text = dt.Rows(i)("CreditAmount").ToString()


                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetRowDataGL()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTableGL") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
                        dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text


                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableGL") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataGL()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Protected Sub PopulateArCode()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim sql As String
            sql = ""

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
            sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"
            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr1 As MySqlDataReader = command1.ExecuteReader()

            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt1.Rows(0)("COACode").ToString : End If
                If dt1.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt1.Rows(0)("Description").ToString : End If
            End If


            '''''''''''''''''''''''''''''''''''

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"

            'Dim command122 As MySqlCommand = New MySqlCommand
            'command122.CommandType = CommandType.Text
            'command122.CommandText = sql
            'command122.Connection = conn

            'Dim dr22 As MySqlDataReader = command122.ExecuteReader()

            'Dim dt22 As New DataTable
            'dt22.Load(dr22)

            'If dt22.Rows.Count > 0 Then
            '    If dt22.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt22.Rows(0)("COACode").ToString : End If
            '    If dt22.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt22.Rows(0)("Description").ToString : End If
            'End If


            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()


            'Dim sql As String
            sql = ""
            'sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode = 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"
            sql = "Select ProductCode, Description, COACode, COADescription from tblbillingproducts where ProductCode = 'IN-CRN'"

            Dim command10 As MySqlCommand = New MySqlCommand
            command10.CommandType = CommandType.Text
            command10.CommandText = sql
            command10.Connection = conn

            Dim dr10 As MySqlDataReader = command10.ExecuteReader()

            Dim dt10 As New DataTable
            dt10.Load(dr10)

            If dt10.Rows.Count > 0 Then
                If dt10.Rows(0)("ProductCode").ToString <> "" Then : txtARProductCode10.Text = dt10.Rows(0)("ProductCode").ToString : End If
                If dt10.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt10.Rows(0)("Description").ToString : End If
                If dt10.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt10.Rows(0)("COACode").ToString : End If
            End If

            '''''''''''''''''''''''''''''''''''
            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"
            sql = "Select COACode, Description from tblchartofaccounts where  GLType='GST OUTPUT'"
            Dim command23 As MySqlCommand = New MySqlCommand
            command23.CommandType = CommandType.Text
            command23.CommandText = sql
            command23.Connection = conn

            Dim dr23 As MySqlDataReader = command23.ExecuteReader()

            Dim dt23 As New DataTable
            dt23.Load(dr23)

            If dt23.Rows.Count > 0 Then
                If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
                If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
            End If

            updPnlBillingRecs.Update()

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            command10.Dispose()
            command23.Dispose()

            dt1.Dispose()
            dt10.Dispose()
            dt23.Dispose()

            dr1.Close()
            dr10.Close()
            dr23.Close()

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "PopulateARCode", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_Sorted(sender As Object, e As EventArgs) Handles GridView1.Sorted

    End Sub

    Protected Sub txtCNDate_TextChanged(sender As Object, e As EventArgs) Handles txtCNDate.TextChanged
        txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtCNDate.Text)) & Format(Month(Convert.ToDateTime(txtCNDate.Text)), "00")
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        Try
         
            'SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
          
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
            Else
                SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
            End If
            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Session.Add("RecordNo", txtCNNo.Text)
        'Session.Add("Title", ddlDocType.SelectedItem.Text)

        Response.Redirect("RV_CreditNote.aspx")


    End Sub

    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        lblAlert.Text = ""
        txtSearch.Text = ""
        txtPopUpClient.Text = ""

        'If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
        '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
        '    Exit Sub
        'End If

        Try
            txtSearch.Text = "InvoiceSearch"
            If String.IsNullOrEmpty(txtAccountIdSearch.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIdSearch.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                'If ddlContactTypeSearch.Text = "COMPANY" Or ddlContactTypeSearch.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                'ElseIf ddlContactTypeSearch.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '%" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                'End If

                If ddlContactTypeSearch.Text = "COMPANY" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%""  or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "PERSON" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"

                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()

                updPanelCN.Update()
            Else

                'If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' order by name"
                'ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' order by name"
                'End If

                If ddlContactTypeSearch.Text = "COMPANY" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "PERSON" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by ServiceName"
                End If
                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelCN.Update()
            End If
            txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnClientSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        Try



            '''''''''''''''''''''''''
            'If txtSearch.Text = "InvoiceFilter" Then
            '    txtAccountId.Text = ""
            '    txtAccountId.Text = ""


            '    'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
            '    '    ddlSearchCompanyGrp.Text = ""
            '    'Else
            '    '    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(21).Text.Trim
            '    'End If


            '    If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '        ddlSearchContactType.Text = ""
            '    Else
            '        ddlSearchContactType.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            '    End If

            '    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '        txtSearchAccountID.Text = ""
            '    Else
            '        txtSearchAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
            '    End If

            '    If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
            '        txtSearchClientName.Text = ""
            '    Else
            '        txtSearchClientName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
            '    End If


            '    'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            '    '    txtLocationId.Text = ""
            '    'Else
            '    '    txtLocationId.Text = gvClient.SelectedRow.Cells(3).Text.Trim
            '    'End If

            '    txtSearch.Text = "N"
            '    'mdlImportServices.Show()
            '    mdlPopupSearch.Show()

            'End If


            ''''''''''''''''''''''''''''
            If txtSearch.Text <> "InvoiceSearch" Then

                lblAlert.Text = ""

                txtIsPopup.Text = ""

                Dim xrowNumber1 = txtxRow.Text
                Dim lblid1 As DropDownList = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtContactTypeGV"), DropDownList)
                Dim lblid2 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtCustCodeGV"), TextBox)
                Dim lblid3 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtAccountIDGV"), TextBox)
                Dim lblid4 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtLocationIDGV"), TextBox)
                Dim lblid5 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtCustNameGV"), TextBox)
                Dim lblid6 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtCompanyGroupGV"), TextBox)
                Dim lblid7 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtLocationGV"), TextBox)

                'txtComments.Text = gvClient.SelectedRow.Cells(1).Text
                If gvClient.SelectedRow.Cells(1).Text = "&nbsp;" Then
                    lblid1.Text = " "
                Else
                    lblid1.Text = gvClient.SelectedRow.Cells(1).Text
                End If

                If gvClient.SelectedRow.Cells(4).Text = "&nbsp;" Then
                    lblid2.Text = " "
                Else
                    lblid2.Text = gvClient.SelectedRow.Cells(4).Text
                End If

                If gvClient.SelectedRow.Cells(2).Text = "&nbsp;" Then
                    lblid3.Text = " "
                Else
                    lblid3.Text = gvClient.SelectedRow.Cells(2).Text
                End If

                If gvClient.SelectedRow.Cells(3).Text = "&nbsp;" Then
                    lblid4.Text = " "
                Else
                    lblid4.Text = gvClient.SelectedRow.Cells(3).Text
                End If

                If gvClient.SelectedRow.Cells(5).Text = "&nbsp;" Then
                    lblid5.Text = " "
                Else
                    lblid5.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text)
                End If

                'If gvClient.SelectedRow.Cells(21).Text = "&nbsp;" Then
                '    lblid6.Text = " "
                'Else
                '    lblid6.Text = gvClient.SelectedRow.Cells(21).Text
                'End If

                If gvClient.SelectedRow.Cells(22).Text = "&nbsp;" Then
                    lblid6.Text = " "
                Else
                    lblid6.Text = gvClient.SelectedRow.Cells(22).Text
                End If

                If gvClient.SelectedRow.Cells(23).Text = "&nbsp;" Then
                    lblid7.Text = " "
                Else
                    lblid7.Text = gvClient.SelectedRow.Cells(23).Text
                End If


                If String.IsNullOrEmpty(txtLocation.Text) = True Then
                    txtLocation.Text = lblid7.Text
                End If

            Else

                'txtSearch.Text = ""

                txtAccountIdSearch.Text = ""
                txtClientNameSearch.Text = ""
                'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                '    txtAccountIdSearch.Text = ""
                'Else
                '    txtAccountIdSearch.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                'End If


                'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                '    txtClientNameSearch.Text = ""
                'Else
                '    txtClientNameSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
                'End If
                'txtSearch.Text = ""

                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    ddlContactTypeSearch.Text = ""
                Else
                    ddlContactTypeSearch.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                    txtAccountIdSearch.Text = ""
                Else
                    txtAccountIdSearch.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                End If


                If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                    txtClientNameSearch.Text = ""
                Else
                    txtClientNameSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                End If

                If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                    ddlCompanyGrpSearch.Text = ""
                Else
                    ddlCompanyGrpSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
                End If
                updPnlSearch.Update()
            End If
            ''End If
            ''PopulateArCode()


            ''Dim conn As MySqlConnection = New MySqlConnection()

            ''conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            ''conn.Open()

            ''Dim sql As String
            ''sql = ""

            ' ''sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
            ''sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"
            ''Dim command1 As MySqlCommand = New MySqlCommand
            ''command1.CommandType = CommandType.Text
            ''command1.CommandText = sql
            ''command1.Connection = conn

            ''Dim dr1 As MySqlDataReader = command1.ExecuteReader()

            ''Dim dt1 As New DataTable
            ''dt1.Load(dr1)

            ''If dt1.Rows.Count > 0 Then
            ''    If dt1.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt1.Rows(0)("COACode").ToString : End If
            ''    If dt1.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt1.Rows(0)("Description").ToString : End If
            ''End If


            ' '''''''''''''''''''''''''''''''''''

            ''sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='OTHER DEBTORS'"

            ''Dim command122 As MySqlCommand = New MySqlCommand
            ''command122.CommandType = CommandType.Text
            ''command122.CommandText = sql
            ''command122.Connection = conn

            ''Dim dr22 As MySqlDataReader = command122.ExecuteReader()

            ''Dim dt22 As New DataTable
            ''dt22.Load(dr22)

            ''If dt22.Rows.Count > 0 Then
            ''    If dt22.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt22.Rows(0)("COACode").ToString : End If
            ''    If dt22.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt22.Rows(0)("Description").ToString : End If
            ''End If


            ''Dim conn As MySqlConnection = New MySqlConnection()

            ''conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            ''conn.Open()


            ' ''Dim sql As String
            ''sql = ""
            ''sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode <> 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"

            ''Dim command10 As MySqlCommand = New MySqlCommand
            ''command10.CommandType = CommandType.Text
            ''command10.CommandText = sql
            ''command10.Connection = conn

            ''Dim dr10 As MySqlDataReader = command10.ExecuteReader()

            ''Dim dt10 As New DataTable
            ''dt10.Load(dr10)

            ''If dt10.Rows.Count > 0 Then
            ''    If dt10.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt10.Rows(0)("Description").ToString : End If
            ''    If dt10.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt10.Rows(0)("COACode").ToString : End If
            ''End If

            ' '''''''''''''''''''''''''''''''''''
            ''Dim query As String

            ''ddlContractNo.Items.Clear()
            ''ddlContractNo.Items.Add("--SELECT--")

            ''sql = ""
            ' ''sql = "Select ContractNo from tblContract where Status = 'O' and GST = 'P' and CompanyGroup ='" & txtCompanyGroup.Text & "' and AccountId = '" & txtAccountIdBilling.Text & "' and ContractGroup <> 'ST'"

            ''sql = "Select ContractNo from tblContract where   CompanyGroup ='" & txtCompanyGroup.Text & "' and AccountId = '" & txtAccountIdBilling.Text & "'"

            ''Dim command11 As MySqlCommand = New MySqlCommand
            ''command11.CommandType = CommandType.Text
            ''command11.CommandText = sql
            ''command11.Connection = conn

            ''Dim dr11 As MySqlDataReader = command11.ExecuteReader()

            ''Dim dt11 As New DataTable
            ''dt11.Load(dr11)

            ''If dt11.Rows.Count > 0 Then

            ''    'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
            ''    PopulateDropDownList(sql, "ContractNo", "ContractNo", ddlContractNo)

            ''    'If dt11.Rows(0)("ContractNo").ToString <> "" Then : ddlContractNo.Text = dt11.Rows(0)("ContractNo").ToString : End If
            ''    'If dt11.Rows(0)("ContractNo").ToString <> "" Then : txtComments.Text = dt11.Rows(0)("ContractNo").ToString : End If
            ''    'If dt11.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt11.Rows(0)("COACode").ToString : End If
            ''End If

            ' '''''''''''''''''''''''''''''''''''

            ''ddlItemCode.Items.Add("CN-RET")
            ''ddlItemCode.Items.Clear()
            ''query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode <> 'CN-RET'"
            ''PopulateDropDownList(query, "ProductCode", "ProductCode", ddlItemCode)

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub btnGLGVB_Click(sender As Object, e As EventArgs)
        txtGLFrom.Text = "InvoiceB"
        Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        'Dim lblid1 As TextBox = CType(xrow1.FindControl("txtOtherCodeGV"), TextBox)

        Dim rowindex1 As Integer = xrow1.RowIndex
        txtxRow.Text = rowindex1
        updPanelSave.Update()
        mdlPopupGL.Show()
    End Sub

    Protected Sub btnGL_Click(sender As Object, e As EventArgs)
        txtGLFrom.Text = "Invoice"
        Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        'Dim lblid1 As TextBox = CType(xrow1.FindControl("txtOtherCodeGV"), TextBox)

        Dim rowindex1 As Integer = xrow1.RowIndex
        txtxRow.Text = rowindex1
        updPanelSave.Update()
        mdlPopupGL.Show()
    End Sub


    Protected Sub BtnAccountIDGVL_Click(sender As Object, e As EventArgs)
        Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        'Dim lblid1 As TextBox = CType(xrow1.FindControl("txtAccountIDGV"), TextBox)

        Dim rowindex1 As Integer = xrow1.RowIndex
        txtxRow.Text = rowindex1
        updPanelSave.Update()

        Dim lblid1 As TextBox = CType(grvBillingDetails.Rows(rowindex1).FindControl("txtAccountIDGV"), TextBox)
        Dim lblid2 As DropDownList = CType(grvBillingDetails.Rows(rowindex1).FindControl("txtContactTypeGV"), DropDownList)

        ''''''''''''''''''''''''''''''
        Try
            lblAlert.Text = ""
            txtSearch.Text = ""
            txtPopUpClient.Text = ""

            'txtSearch.Text = "CustomerSearch"

            If String.IsNullOrEmpty(lblid1.Text.Trim) = False Then
                txtPopUpClient.Text = lblid1.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If lblid2.Text = "CORPORATE" Or lblid2.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    ElseIf lblid2.SelectedItem.Text = "RESIDENTIAL" Or lblid2.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    End If
                Else
                    If lblid2.Text = "CORPORATE" Or lblid2.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    ElseIf lblid2.SelectedItem.Text = "RESIDENTIAL" Or lblid2.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by ServiceName"
                    End If
                End If
               

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If lblid2.Text = "CORPORATE" Or lblid2.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf lblid2.SelectedItem.Text = "RESIDENTIAL" Or lblid2.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"

                    End If
                Else
                    If lblid2.Text = "CORPORATE" Or lblid2.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf lblid2.SelectedItem.Text = "RESIDENTIAL" Or lblid2.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"

                    End If
                End If
                

                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If
            txtCustomerSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
            updPanelCN.Update()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "BtnAccountIDGVL_Click", ex.Message.ToString, "")
            Exit Sub
        End Try




        '''''''''''''''''''''''''''''''
        'updPanelSave.Update()
        'mdlPopupGL.Show()
    End Sub

    Protected Sub GrdViewGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        Try

            If txtGLFrom.Text = "InvoiceSearch" Then
                txtGLCodeII.Text = GrdViewGL.SelectedRow.Cells(1).Text
                txtLedgerNameII.Text = GrdViewGL.SelectedRow.Cells(2).Text
                txtGLFrom.Text = ""
                mdlPopupGL.Hide()
                mdlImportInvoices.Show()
            ElseIf txtGLFrom.Text = "ServiceSearch" Then
                txtGLCodeIS.Text = GrdViewGL.SelectedRow.Cells(1).Text
                txtLedgerNameIS.Text = GrdViewGL.SelectedRow.Cells(2).Text
                txtGLFrom.Text = ""
                mdlPopupGL.Hide()
                mdlImportServices.Show()
            ElseIf txtGLFrom.Text = "LedgerSearch" Then
                txtLedgerSearch.Text = GrdViewGL.SelectedRow.Cells(1).Text
                'txtGLCodeIS.Text = GrdViewGL.SelectedRow.Cells(1).Text
                'txtLedgerNameIS.Text = GrdViewGL.SelectedRow.Cells(2).Text
                txtGLFrom.Text = ""
                mdlPopupGL.Hide()
                'mdlImportServices.Show()
            Else

                If txtGLFrom.Text = "InvoiceB" Then
                    Dim xrowNumber1 = txtxRow.Text
                    Dim lblid1 As TextBox = CType(grvBillingDetailsNew.Rows(xrowNumber1).FindControl("txtOtherCodeGVB"), TextBox)
                    Dim lblid2 As TextBox = CType(grvBillingDetailsNew.Rows(xrowNumber1).FindControl("txtGLDescriptionGVB"), TextBox)

                    If GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;" Then
                        lblid1.Text = " "
                    Else
                        lblid1.Text = GrdViewGL.SelectedRow.Cells(1).Text
                    End If

                    If GrdViewGL.SelectedRow.Cells(2).Text = "&nbsp;" Then
                        lblid2.Text = " "
                    Else
                        lblid2.Text = GrdViewGL.SelectedRow.Cells(2).Text
                    End If

                Else
                    Dim xrowNumber1 = txtxRow.Text
                    Dim lblid1 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtOtherCodeGV"), TextBox)
                    Dim lblid2 As TextBox = CType(grvBillingDetails.Rows(xrowNumber1).FindControl("txtGLDescriptionGV"), TextBox)

                    If GrdViewGL.SelectedRow.Cells(1).Text = "&nbsp;" Then
                        lblid1.Text = " "
                    Else
                        lblid1.Text = GrdViewGL.SelectedRow.Cells(1).Text
                    End If

                    If GrdViewGL.SelectedRow.Cells(2).Text = "&nbsp;" Then
                        lblid2.Text = " "
                    Else
                        lblid2.Text = GrdViewGL.SelectedRow.Cells(2).Text
                    End If
                End If

            End If
            'txtIsPopup.Text = "Location"
            'mdlImportServices.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "GrdViewGL_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub CheckUncheckAllInvoice(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(grvInvoiceRecDetails.HeaderRow.Cells(0).FindControl("chkSelectInvoiceGV"), CheckBox)
        For Each row As GridViewRow In grvInvoiceRecDetails.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelectGVII"), CheckBox)
            chk.Checked = chk1.Checked
        Next
    End Sub


    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(grvServiceRecDetails.HeaderRow.Cells(0).FindControl("chkSelectGV"), CheckBox)
        For Each row As GridViewRow In grvServiceRecDetails.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelectGV"), CheckBox)
            chk.Checked = chk1.Checked
        Next
    End Sub
    Protected Sub CheckUncheckAllservice(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(grvServiceRecDetails.HeaderRow.Cells(0).FindControl("chkSelectServiceGV"), CheckBox)
        For Each row As GridViewRow In grvServiceRecDetails.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelectGV"), CheckBox)
            chk.Checked = chk1.Checked
        Next
    End Sub
    Protected Sub btnShowServices_Click(sender As Object, e As EventArgs) Handles btnShowServices.Click
        lblAlert.Text = ""
        updPnlMsg.Update()

        'If ddlDocType.SelectedIndex = 0 Then
        '    lblAlert.Text = "PLEASE SELECT VOUCHER TYPE "
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    lblAlert.Focus()
        '    updPnlMsg.Update()
        '    Exit Sub
        'End If

        If String.IsNullOrEmpty(txtCompanyGroup.Text.Trim) = False Then
            ddlCompanyGrp.Text = txtCompanyGroup.Text
        End If

        'If String.IsNullOrEmpty(ddlContactType.Text.Trim) = False Then
        '    ddlContactTypeIS.Text = ddlContactType.Text
        'End If

        'txtAccountId.Text = txtAccountIdBilling.Text
        'txtClientName.Text = txtAccountName.Text
        mdlImportServices.Show()
    End Sub

    Protected Sub btnShowRecordsII_Click(sender As Object, e As EventArgs) Handles btnShowRecordsII.Click
        Try
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim strsql As String

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand

            'strsql = "SELECT a.rcno, a.CompanyGroup, a.AccountId, a.Custname,  a.InvoiceNumber, a.SalesDate, b.LocationID, a.valuebase, a.gstbase, "
            'strsql = strsql + " a.AppliedBase, a.TotalReceiptAmount, a.TotalCNAmount, a.Terms, (a.AppliedBase - a.TotalReceiptAmount - a.TotalCNAmount) as OSAmount "
            'strsql = strsql + " FROM tblsales a Left join tblSalesDetail B on  a.InvoiceNumber = b.InvoiceNumber "
            'strsql = strsql + " where  a.PaidStatus <> 'F' and a.GLStatus = 'P' and a.CompanyGroup ='" & ddlCompanyGrp.Text & "'"
            'strsql = strsql + " and (a.AppliedBase - a.TotalReceiptAmount - a.TotalCNAmount) > 0"

            strsql = "SELECT a.rcno, a.CompanyGroup, a.AccountId, a.Custname,  a.InvoiceNumber, a.SalesDate,  a.valuebase, a.gstbase,  a.Receiptbase, a.Creditbase, "
            strsql = strsql + " a.AppliedBase, a.Terms, a.BalanceBase as OSAmount "
            strsql = strsql + " FROM tblsales a  "
            strsql = strsql + " where  DocType = 'ARIN' and a.PostStatus = 'P' and a.CompanyGroup ='" & ddlCompanyGrpII.Text & "'"
            strsql = strsql + " and a.BalanceBase > 0"

            If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
                strsql = strsql + " and a.AccountId = '" & txtAccountIdII.Text & "' "
            End If

            If String.IsNullOrEmpty(txtClientNameII.Text.Trim) = False Then
                strsql = strsql + " and a.CustName = '" & txtClientNameII.Text & "' "
            End If


            If String.IsNullOrEmpty(txtDateFromII.Text) = False Then
                strsql = strsql + " and   A.Salesdate >= '" & Convert.ToDateTime(txtDateFromII.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateToII.Text) = False Then
                strsql = strsql + " and   A.Salesdate <= '" & Convert.ToDateTime(txtDateToII.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateFromII.Text) = False And String.IsNullOrEmpty(txtDateToII.Text) = False Then
                strsql = strsql + " and   A.Salesdate between '" & Convert.ToDateTime(txtDateFromII.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateToII.Text).ToString("yyyy-MM-dd") & "'"
            End If


            'If ddlContractNo.Text.Trim <> "-1" Then
            '    strsql = strsql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
            'End If

            'If String.IsNullOrEmpty(txtLocationId.Text) = False Then
            '    strsql = strsql + " and   b.LocationId = '" & txtLocationId.Text & "'"
            'End If

            strsql = strsql + " order by a.Salesdate"


            cmdServiceBillingDetailItem.CommandText = strsql

            SqlDSOSInvoice.SelectCommand = strsql
            grvInvoiceRecDetails.DataSourceID = "SqlDSOSInvoice"
            grvInvoiceRecDetails.DataBind()


            conn.Close()


            btnImportInvoiceII.Enabled = True

            mdlImportInvoices.Show()

            'TextBoxCompanyGroup.Text = Convert.ToString(dt.Rows(rowIndex)("CompanyGroup"))

            If grvInvoiceRecDetails.Rows.Count > 0 Then
                Dim TextBoxCompanyGroup1 As TextBox = CType(grvInvoiceRecDetails.Rows(0).Cells(0).FindControl("txtCompanyGroupGVII"), TextBox)
                If String.IsNullOrEmpty(TextBoxCompanyGroup1.Text) = True Then
                    Label43.Text = "INVOICE DETAILS : Total Records : " & grvInvoiceRecDetails.Rows.Count.ToString - 1
                Else
                    Label43.Text = "INVOICE DETAILS : Total Records : " & grvInvoiceRecDetails.Rows.Count.ToString
                End If
            Else
                Label43.Text = "INVOICE DETAILS : Total Records : " & grvInvoiceRecDetails.Rows.Count.ToString
            End If

            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnShowRecordsII_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Try
            lblAlert1.Text = ""
            txtSearch.Text = "ImportInvoice"
            'If String.IsNullOrEmpty(ddlContactTypeII.Text) Or ddlContactTypeII.Text = "--SELECT--" Then
            '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            '    lblAlert1.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            '    mdlImportInvoices.Show()
            '    Exit Sub
            'End If

            txtClientFrom.Text = "ImportInvoice"
            If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIdII.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text


                If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                ElseIf ddlContactTypeII.SelectedItem.Text = "PERSON" Or ddlContactTypeII.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                ElseIf ddlContactTypeII.SelectedItem.Text = "PERSON" Or ddlContactTypeII.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If
            txtImportService.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "ImageButton1_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        txtClientFrom.Text = ""
        mdlImportServices.Hide()
    End Sub

    Protected Sub ImageButton2_Click1(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        lblAlert.Text = ""
        lblAlert1.Text = ""
        txtSearch.Text = "ImportService"
        Try
            'If String.IsNullOrEmpty(ddlContactTypeIS.Text) Or ddlContactTypeIS.Text = "--SELECT--" Then
            '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            '    lblAlert2.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            '    mdlImportServices.Show()
            '    Exit Sub
            'End If


            If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
                txtPopUpClient.Text = ""
                txtPopUpClient.Text = txtAccountId.Text.Trim
                txtPopupClientSearch.Text = txtPopUpClient.Text
                'UpdatePanel1.Update()

                'If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman,  BillAddress1, BillStreet, BillBuilding, BillCountry, BillPostal From tblCompany where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contactperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                'ElseIf ddlContactTypeIS.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman,  BillAddress1, BillStreet, BillBuilding, BillCountry, BillPostal From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopupClientSearch.Text + "%' or contACTperson like '%" + txtPopupClientSearch.Text + "%') order by name"
                'End If

                If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                ElseIf ddlContactTypeIS.SelectedItem.Text = "PERSON" Or ddlContactTypeIS.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            Else
                txtPopUpClient.Text = ""
                'If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                '    SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman,  BillAddress1, BillStreet, BillBuilding, BillCountry, BillPostal From tblCompany where 1=1 and status = 'O' order by name"
                'ElseIf ddlContactTypeIS.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                '    SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman,  BillAddress1, BillStreet, BillBuilding, BillCountry, BillPostal From tblPERSON where 1=1 and status = 'O' order by name"
                'End If


                If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                ElseIf ddlContactTypeIS.SelectedItem.Text = "PERSON" Or ddlContactTypeIS.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            End If
            txtImportService.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "ImageButton2_Click1", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnShowRecords_Click1(sender As Object, e As EventArgs) Handles btnShowRecords.Click

        lblAlert.Text = ""

        'FirstGridViewRowServiceRecs()


        'updPanelInvoice.Update()

        PopulateServiceGrid()

        btnImportService.Enabled = True

        mdlImportServices.Show()
    End Sub

    Protected Sub btnImportInvoiceII_Click(sender As Object, e As EventArgs) Handles btnImportInvoiceII.Click
        Try
            txtClientFrom.Text = ""
            Dim totalRows As Long
            totalRows = 0


            For rowIndex1 As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
                Dim TextBoxchkSelect1 As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGVII"), CheckBox)
                If (TextBoxchkSelect1.Checked = True) Then
                    totalRows = totalRows + 1
                    GoTo insertRec1
                End If
            Next rowIndex1



            If totalRows = 0 Then
                mdlImportInvoices.Show()
                Dim message As String = "alert('PLEASE SELECT A RECORD')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                'MessageBox.Message.Alert(Page, "PLEASE SELECT A RECORD", "str")
                'lblAlert.Text = "PLEASE SELECT A RECORD"
                'lblAlert.Focus()
                'updPnlMsg.Update()

                Exit Sub
            End If

insertRec1:
            'If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = True Then
            '    txtCompanyGroup.Text = ddlCompanyGrpII.Text
            '    ddlContactType.Text = ddlContactTypeII.Text
            '    txtAccountIdBilling.Text = txtAccountIdII.Text
            '    txtAccountName.Text = txtClientNameII.Text

            '    ddlCompanyGrpII.Enabled = False
            '    ddlContactTypeII.Enabled = False
            '    txtAccountIdII.Enabled = False
            '    txtClientNameII.Enabled = False
            '    btnClient.Visible = False
            'Else
            '    'ddlCompanyGrp.Text = txtCompanyGroup.Text
            '    'txtAccountId.Text = txtAccountIdBilling.Text
            '    'txtClientName.Text = txtAccountName.Text

            '    'ddlCompanyGrp.Enabled = True
            '    'ddlContactType.Enabled = True
            '    'txtAccountId.Enabled = True
            '    'txtClientName.Enabled = True
            '    'btnClient.Visible = True

            'End If
            'System.Threading.Thread.Sleep(5000)

            'Start: Billing Details

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                grvBillingDetails.DataSource = dtScdrLoc
                grvBillingDetails.DataBind()

                SetPreviousDataBillingDetailsRecs()
            Next i

            FirstGridViewRowBillingDetailsRecs()

            'Start: From tblBillingDetailItem

            txtCreditAmount.Text = "0.00"
            txtDebitAmount.Text = "0.00"
            txtCreditAmount.Text = "0.00"

            Dim rowselected As Integer
            rowselected = 0

            For rowIndex As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGVII"), CheckBox)

                If (TextBoxchkSelect.Checked = True) Then
                    AddNewRowBillingDetailsRecs()
                    Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text
                    'Header
                    rowselected = rowselected + 1

                    Dim lblid13 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGVII"), TextBox)
                    Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGVII"), TextBox)
                    Dim InvoiceDate As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtSalesDateGVII"), TextBox)

                    Dim InvoiceAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtValueBaseGVII"), TextBox)
                    'Dim InvoiceAmmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAppliedBaseGVII"), TextBox)
                    Dim TotalReceiptAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmountGVII"), TextBox)
                    Dim TotalCNAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCNAmountGVII"), TextBox)
                    Dim TotalOSAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtOSAmountGVII"), TextBox)
                    Dim lCustName As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtClientNameGVII"), TextBox)
                    Dim lLocationID As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIDGVII"), TextBox)
                 
                    'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    'TextBoxItemType.Text = "INVOICE"


                    'Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    'TextBoxtxtInvoiceNoGV.Text = Convert.ToString(InvoiceNumber.Text)

                    ''Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    ''TextBoxUOM.Text = "NO"

                    'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    ''TextBoxOtherCode.Text = txtARCode.Text
                    'TextBoxOtherCode.Text = txtGLCodeII.Text

                    'Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    ''TextBoxGLDescription.Text = txtARDescription.Text
                    'TextBoxGLDescription.Text = txtLedgerNameII.Text

                    Dim TextBoxContactType As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContactTypeGV"), TextBox)
                    TextBoxContactType.Text = "0.00"

                    Dim TextBoxCustCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCustCodeGV"), TextBox)
                    TextBoxCustCode.Text = "0.00"

                    Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                    TextBoxAccountID.Text = lblid13.Text

                    Dim TextBoxLocationID As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtLocationIDGV"), TextBox)
                    TextBoxLocationID.Text = lLocationID.Text

                    Dim TextBoxCustName As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCustNameGV"), TextBox)
                    TextBoxCustName.Text = lCustName.Text

                    Dim TextBoxDebitBase As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtDebitBaseGV"), TextBox)
                    TextBoxDebitBase.Text = "0.00"

                    Dim TextBoxCreditBase As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCreditBaseGV"), TextBox)
                    TextBoxCreditBase.Text = TotalOSAmount.Text


                    'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                    'TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N2")

                    'Dim gstCalc As Decimal
                    'Dim CNDNAmt As Decimal



                    'Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                    'TextBoxTotalPricePerUOM.Text = Convert.ToString(InvoiceAmount.Text)

                    'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtQtyGV"), TextBox)
                    'TextBoxQty.Text = (Convert.ToDecimal(1).ToString("N2"))

                    'Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                    'TextBoxTotalPrice.Text = (Convert.ToDecimal(InvoiceAmount.Text).ToString("N2")) * Convert.ToDecimal(TextBoxQty.Text).ToString("N2")

                    'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    'TextBoxTotalPriceWithDisc.Text = Convert.ToDecimal(TextBoxTotalPrice.Text).ToString("N2")
                    'CNDNAmt = TextBoxTotalPriceWithDisc.Text

                    'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    'TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(InvoiceAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))
                    'gstCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

                    'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    'TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)))


                    'End If



                    ''''''''''''''''''''''''''''''
                    'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(CNDNAmt)).ToString("N2")
                    'txtDebitAmount.Text = (Convert.ToDecimal(txtDebitAmount.Text) + Convert.ToDecimal(gstCalc)).ToString("N2")
                    'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtDebitAmount.Text)).ToString("N2")

                    ''Dim TextBoxItemCodeGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
                    ''TextBoxItemCodeGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode")))

                    'Dim TextBoxTermsGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtARCodeGV"), TextBox)
                    'TextBoxTermsGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Terms")))



                    'rowIndex += 1

                    'Next row


                    'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
                    'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
                    'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
                    ''Else
                    'FirstGridViewRowBillingDetailsRecs()

                End If

                'End: Detail Records
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString

                'Button1.Enabled = False
                'Timer1.Enabled = True
                'Thread.Sleep(1000)
                'Dim workerThread As New Thread(New ThreadStart(AddressOf ProcessRecords))
                'workerThread.Start()


                'textbox6.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
            Next

            btnSave.Enabled = True
            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnImportInvoiceII_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnImportService_Click(sender As Object, e As EventArgs) Handles btnImportService.Click
        Try
            txtClientFrom.Text = ""
            Dim totalRows As Long
            totalRows = 0


            For rowIndex1 As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
                If (TextBoxchkSelect1.Checked = True) Then
                    totalRows = totalRows + 1
                    GoTo insertRec2
                End If
            Next rowIndex1



            If totalRows = 0 Then
                mdlImportServices.Show()
                Dim message As String = "alert('PLEASE SELECT A RECORD')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                'MessageBox.Message.Alert(Page, "PLEASE SELECT A RECORD", "str")
                'lblAlert.Text = "PLEASE SELECT A RECORD"
                'lblAlert.Focus()
                'updPnlMsg.Update()

                Exit Sub
            End If


insertRec2:


            'If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = True Then
            '    txtCompanyGroup.Text = ddlCompanyGrpII.Text
            '    'ddlContactType.Text = txtAccountType.Text
            '    txtAccountIdBilling.Text = txtAccountIdII.Text
            '    txtAccountName.Text = txtClientNameII.Text

            '    ddlCompanyGrpII.Enabled = False
            '    ddlContactType.Enabled = False
            '    txtAccountIdII.Enabled = False
            '    txtClientNameII.Enabled = False
            '    btnClient.Visible = False
            'Else
            '    'ddlCompanyGrp.Text = txtCompanyGroup.Text
            '    'txtAccountId.Text = txtAccountIdBilling.Text
            '    'txtClientName.Text = txtAccountName.Text

            '    'ddlCompanyGrp.Enabled = True
            '    'ddlContactType.Enabled = True
            '    'txtAccountId.Enabled = True
            '    'txtClientName.Enabled = True
            '    'btnClient.Visible = True

            'End If
            'System.Threading.Thread.Sleep(5000)

            'Start: Billing Details

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                grvBillingDetails.DataSource = dtScdrLoc
                grvBillingDetails.DataBind()

                SetPreviousDataBillingDetailsRecs()
            Next i

            FirstGridViewRowBillingDetailsRecs()

            'Start: From tblBillingDetailItem

            txtCreditAmount.Text = "0.00"
            txtDebitAmount.Text = "0.00"
            txtCreditAmount.Text = "0.00"

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim rowselected As Integer
            rowselected = 0

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

                If (TextBoxchkSelect.Checked = True) Then

                    Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text

                    Dim ServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)

                    '''''''''''''''''''''''''
                    If String.IsNullOrEmpty(txtCNNo.Text.Trim) = False Then
                        'Dim conn As MySqlConnection = New MySqlConnection()

                        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn.Open()
                        Dim command11 As MySqlCommand = New MySqlCommand

                        command11.CommandType = CommandType.Text

                        'command1.CommandText = "SELECT * FROM tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = '" & lblid1.Text & "'"
                        command11.CommandText = "SELECT count(*) as totcount FROM tblSalesDetail where RefType = '" & ServiceRecordNo.Text & "' and  InvoiceNumber = '" & txtCNNo.Text & "'"
                        command11.Connection = conn

                        Dim dr As MySqlDataReader = command11.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("totcount").ToString() > 0 Then
                                GoTo nextrec
                            End If
                        End If

                        'conn.Close()
                        command11.Dispose()
                        dt.Dispose()
                    End If

                    ''''''''''''''''''''''''''

                    AddNewRowBillingDetailsRecs()
                    'Header
                    rowselected = rowselected + 1

                    Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                    Dim InvoiceNumber As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    'Dim ServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
                    Dim ContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    Dim BillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
                    Dim LocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)


                    'Dim TotalReceiptAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmountGVII"), TextBox)
                    'Dim TotalCNAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCNAmountGVII"), TextBox)
                    'Dim TotalOSAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtOSAmountGVII"), TextBox)

                    ''Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)
                    ''Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)

                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    TextBoxItemType.Text = "SERVICE"

                    Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    TextBoxUOM.Text = "NO"

                    Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    TextBoxtxtInvoiceNoGV.Text = Convert.ToString(InvoiceNumber.Text)


                    Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
                    TextBoxServiceRecord.Text = Convert.ToString(ServiceRecordNo.Text)
                    TextBoxServiceRecord.Enabled = False

                    Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    TextBoxContractNo.Text = Convert.ToString(ContractNo.Text)

                    Dim TextBoxLocationId As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                    TextBoxLocationId.Text = Convert.ToString(LocationId.Text)

                    'Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    'TextBoxInvoiceDate.Text = Convert.ToDateTime(InvoiceDate.Text).ToString("dd/MM/yyyy")

                    Dim GSTCalc As Decimal
                    Dim CNDNAmt As Decimal

                    Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                    TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N2")

                    'If ddlDocType.Text = "ARCN" Then
                    '    Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                    '    TextBoxTotalPricePerUOM.Text = (Convert.ToDecimal(BillAmt.Text).ToString("N2"))

                    '    Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtQtyGV"), TextBox)
                    '    TextBoxQty.Text = (Convert.ToDecimal(-1).ToString("N2"))

                    '    Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                    '    TextBoxTotalPrice.Text = (Convert.ToDecimal(BillAmt.Text).ToString("N2")) * Convert.ToDecimal(TextBoxQty.Text).ToString("N2")

                    '    Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    '    TextBoxTotalPriceWithDisc.Text = Convert.ToDecimal(TextBoxTotalPrice.Text).ToString("N2")
                    '    CNDNAmt = TextBoxTotalPriceWithDisc.Text

                    '    Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    '    TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(BillAmt.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01 * (-1)).ToString("N2"))
                    '    GSTCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

                    '    Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    '    TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)))

                    'Else
                    Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
                    TextBoxTotalPricePerUOM.Text = (Convert.ToDecimal(BillAmt.Text).ToString("N2"))

                    Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtQtyGV"), TextBox)
                    TextBoxQty.Text = (Convert.ToDecimal(1).ToString("N2"))

                    Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
                    TextBoxTotalPrice.Text = (Convert.ToDecimal(BillAmt.Text).ToString("N2")) * Convert.ToDecimal(TextBoxQty.Text).ToString("N2")

                    Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    TextBoxTotalPriceWithDisc.Text = Convert.ToDecimal(TextBoxTotalPrice.Text).ToString("N2")
                    CNDNAmt = TextBoxTotalPriceWithDisc.Text

                    Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(BillAmt.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))
                    GSTCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

                    Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)))


                    'End If


                    txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(CNDNAmt)).ToString("N2")
                    txtDebitAmount.Text = (Convert.ToDecimal(txtDebitAmount.Text) + Convert.ToDecimal(GSTCalc)).ToString("N2")
                    txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(txtDebitAmount.Text)).ToString("N2")

                    'TextBoxTotalPriceWithGST.Text = "0.00"

                    'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    'TextBoxTotalPriceWithGST.Text = (Convert.ToDecimal(InvoiceAmmount.Text)).ToString("N2")

                    ''Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    ''TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTBase"))


                    'Dim TextBoxTotalTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    'TextBoxTotalTotalReceiptAmt.Text = (Convert.ToDecimal(TotalReceiptAmount.Text)).ToString("N2")

                    'Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    'TextBoxTotalCNAmt.Text = (Convert.ToDecimal(TotalCNAmount.Text)).ToString("N2")

                    ''Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                    ''TextBoxRcnoInvoice.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Rcno")))

                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    TextBoxOtherCode.Text = txtGLCodeIS.Text

                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    TextBoxGLDescription.Text = txtLedgerNameIS.Text

                    'Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    'TextBoxReceiptAmt.Text = TotalOSAmount.Text


                    'txtCreditAmount.Text = (Convert.ToDecimal(txtCreditAmount.Text) + Convert.ToDecimal(TotalOSAmount.Text)).ToString("N2")
                    ' ''Dim TextBoxItemCodeGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
                    ' ''TextBoxItemCodeGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode")))

                    ''Dim TextBoxTermsGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtARCodeGV"), TextBox)
                    ''TextBoxTermsGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Terms")))



                    'rowIndex += 1

                    'Next row


                    'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
                    'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
                    'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
                    ''Else
                    'FirstGridViewRowBillingDetailsRecs()

                End If
nextrec:

                'End: Detail Records
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString

                'Button1.Enabled = False
                'Timer1.Enabled = True
                'Thread.Sleep(1000)
                'Dim workerThread As New Thread(New ThreadStart(AddressOf ProcessRecords))
                'workerThread.Start()


                'textbox6.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
            Next

            conn.Close()
            conn.Dispose()
            CalculateTotalPrice()
            grvBillingDetails.Visible = True

            btnSave.Enabled = True
            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()



        Catch ex As Exception
            lblAlert1.Text = ex.Message
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnImportService_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub





    '' GVB

    Protected Sub txtQtyGVB_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        CalculatePriceGVB()
    End Sub


    Protected Sub txtDebitBaseGVB_TextChanged(sender As Object, e As EventArgs)
        CalculateTotalPrice()
    End Sub

    Protected Sub txtCreditBaseGVB_TextChanged(sender As Object, e As EventArgs)
        CalculateTotalPrice()
    End Sub

    Protected Sub txtDebitBaseGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtDebitBaseGV"), TextBox)
        Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtCreditBaseGV"), TextBox)


        If Convert.ToDecimal(lblid1.Text) > 0.0 Then
            lblid2.Text = "0.00"
        End If

        Dim rowindex1 As Integer = xgrvBillingDetails.RowIndex

        CalculatePrice()
    End Sub

    Protected Sub txtCreditBaseGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtDebitBaseGV"), TextBox)
        Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtCreditBaseGV"), TextBox)


        If String.IsNullOrEmpty(lblid1.Text) = True Then
            lblid1.Text = "0.00"
        End If
        If String.IsNullOrEmpty(lblid2.Text) = True Then
            lblid2.Text = "0.00"
        End If


        If Convert.ToDecimal(lblid2.Text) > 0.0 Then
            lblid1.Text = "0.00"
        End If


        CalculatePrice()
    End Sub



    Private Sub CalculatePriceGVB()
        Try
            Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGVB"), TextBox)
            Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGVB"), TextBox)
            Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceGVB"), TextBox)

            Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGVB"), TextBox)
            Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGVB"), TextBox)
            Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGVB"), TextBox)

            Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGVB"), TextBox)
            Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTAmtGVB"), TextBox)
            Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceWithGSTGVB"), TextBox)

            Dim dblQty As String
            Dim dblPricePerUOM As String
            Dim dblTotalPrice As String

            Dim dblDiscPerc As String
            Dim dblDisAmount As String
            Dim dblPriceWithDisc As String

            Dim dblGSTPerc As String
            Dim dblGSTAmt As String
            Dim dblTotalPriceWithGST As String


            If String.IsNullOrEmpty(lblid1.Text) = True Then
                lblid1.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid2.Text) = True Then
                lblid2.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid3.Text) = True Then
                lblid3.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid4.Text) = True Then
                lblid4.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid5.Text) = True Then
                lblid5.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid6.Text) = True Then
                lblid6.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid7.Text) = True Then
                lblid7.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid8.Text) = True Then
                lblid8.Text = "0.00"
            End If

            If String.IsNullOrEmpty(lblid9.Text) = True Then
                lblid9.Text = "0.00"
            End If


            dblQty = (lblid1.Text)
            dblPricePerUOM = (lblid2.Text)
            dblTotalPrice = (lblid3.Text)

            dblDiscPerc = (lblid4.Text)
            dblDisAmount = (lblid5.Text)
            dblPriceWithDisc = (lblid6.Text)

            dblGSTPerc = (lblid7.Text)
            dblGSTAmt = (lblid8.Text)
            dblTotalPriceWithGST = (lblid9.Text)

            lblid3.Text = (Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
            lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
            lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
            lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid6.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
            lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid6.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

            CalculateTotalPrice()

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "CalculatePriceGVB", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub


    Protected Sub txtTaxTypeGVB_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
            xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)


            'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(ddl1.FindControl("txtTaxTypeGVB"), DropDownList)
            Dim lblid2 As TextBox = CType(ddl1.FindControl("txtGSTPercGVB"), TextBox)


            'lTargetDesciption = lblid1.Text

            'Dim rowindex1 As Integer = ddl1.RowIndex

            Dim conn1 As MySqlConnection = New MySqlConnection(constr)
            conn1.Open()

            Dim commandGST As MySqlCommand = New MySqlCommand
            commandGST.CommandType = CommandType.Text
            commandGST.CommandText = "SELECT TaxRatePct FROM tbltaxtype where TaxType='" & lblid1.Text & "'"
            commandGST.Connection = conn1

            Dim drGST As MySqlDataReader = commandGST.ExecuteReader()
            Dim dtGST As New DataTable
            dtGST.Load(drGST)

            If dtGST.Rows.Count > 0 Then
                lblid2.Text = dtGST.Rows(0)("TaxRatePct").ToString
                lblid2.Text = Convert.ToDecimal(lblid2.Text).ToString("N2")
                CalculatePriceGVB()
                'If dtGST.Rows(0)("GST").ToString = "P" Then
                '    lblAlert.Text = "SCHEUDLE HAS ALREADY BEEN GENERATED"
                '    conn1.Close()
                '    Exit Sub
                'End If
            End If

            conn1.Close()
            conn1.Dispose()
            commandGST.Dispose()
            dtGST.Dispose()
            drGST.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtTaxTypeGVB_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    ' GVB

    Protected Sub ImageButton6_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton6.Click
        txtGLFrom.Text = ""
        txtGLFrom.Text = "InvoiceSearch"
        mdlPopupGL.Show()
    End Sub



    Protected Sub ImageButton7_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton7.Click
        txtGLFrom.Text = ""
        txtGLFrom.Text = "ServiceSearch"
        mdlPopupGL.Show()
    End Sub

    Protected Sub OnRowDataBoundg1(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedg1(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView1.Rows
            'If row.RowIndex = GridView1.SelectedIndex Then
            '    row.BackColor = ColorTranslator.FromHtml("#738A9C")
            '    row.ToolTip = String.Empty
            'Else
            '    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
            '    row.ToolTip = "Click to select this row."
            'End If
            If row.RowIndex = GridView1.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#AEE4FF")
                row.ToolTip = String.Empty
            Else
                If row.RowIndex Mod 2 = 0 Then
                    row.BackColor = ColorTranslator.FromHtml("#EFF3FB")
                    row.ToolTip = "Click to select this row."
                Else
                    row.BackColor = ColorTranslator.FromHtml("#ffffff")
                    row.ToolTip = "Click to select this row."
                End If


            End If
        Next
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        IsSuccess = PostCN()

        If IsSuccess = True Then

            lblAlert.Text = ""
            updPnlSearch.Update()
            updPnlMsg.Update()
            updpnlBillingDetails.Update()
            'updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()

            btnQuickSearch_Click(sender, e)
            lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJNOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

            btnReverse.Enabled = True
            btnReverse.ForeColor = System.Drawing.Color.Black

            btnEdit.Enabled = False
            btnEdit.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnPost.Enabled = False
            btnPost.ForeColor = System.Drawing.Color.Gray

            'InsertNewLog()
        End If
    End Sub

    Protected Sub btnConfirmYesReverse_Click(sender As Object, e As EventArgs) Handles btnConfirmYesReverse.Click
        IsSuccess = ReverseCN()

        If IsSuccess = True Then
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

            lblAlert.Text = ""
            updPnlSearch.Update()
            updPnlMsg.Update()
            updpnlBillingDetails.Update()
            'updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()

            btnQuickSearch_Click(sender, e)

            lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"

            btnReverse.Enabled = False
            btnReverse.ForeColor = System.Drawing.Color.Gray

            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black

            btnDelete.Enabled = True
            btnDelete.ForeColor = System.Drawing.Color.Black

            btnPost.Enabled = True
            btnPost.ForeColor = System.Drawing.Color.Black

            'InsertNewLog()
        End If
    End Sub

    Protected Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""


            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If


            mdlPopupConfirmReverse.Show()


            Dim confirmValue As String
            confirmValue = ""


            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                ''''''''''''''' Insert tblAR

                'PopulateArCode()
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command5 As MySqlCommand = New MySqlCommand
                command5.CommandType = CommandType.Text

                Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtCNNo.Text & "'"

                command5.CommandText = qry5
                'command1.Parameters.Clear()
                command5.Connection = conn
                command5.ExecuteNonQuery()


                lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJUSTMENT", txtCNNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()

                conn.Close()
                conn.Dispose()
                command5.Dispose()
                'commandUpdateCN.Dispose()

                'InsertNewLog()
                btnQuickSearch_Click(sender, e)
            End If

            ''''''''''''''' Insert tblAR
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnReverse_Click", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        lblAlertStatus.Text = ""
        mdlPopupStatus.Show()
    End Sub

    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        lblAlertStatus.Text = ""

        If ddlNewStatus.SelectedIndex = 0 Then
            lblAlertStatus.Text = "PLEASE SELECT STATUS"
            mdlPopupStatus.Show()
            Return

        End If


        If ddlNewStatus.Text = txtDDLText.Text Then
            lblAlertStatus.Text = "SELECT NEW STATUS"
            mdlPopupStatus.Show()
            Return
        End If

        If ddlNewStatus.Text = txtPostStatus.Text Then
            lblAlertStatus.Text = "STATUS ALREADY UPDATED"
            mdlPopupStatus.Show()
            Return
        End If


        If String.IsNullOrEmpty(txtReasonChSt.Text) = True Then
            lblAlertStatus.Text = "PLEASE ENTER REASON"
            txtReasonChSt.Focus()
            mdlPopupStatus.Show()
            Return
        End If

        Try
            If ddlNewStatus.SelectedIndex > 0 Then
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.CommandText = "UPDATE tbljrnv SET PostStatus='" + ddlNewStatus.SelectedValue + "', ReasonChSt ='" & txtReasonChSt.Text.Trim & "'  where rcno=" & Convert.ToInt32(txtRcno.Text)
                command.Connection = conn
                command.ExecuteNonQuery()

                '   UpdateContractActSvcDate(conn)

                conn.Close()
                conn.Dispose()
                command.Dispose()

                'ddlStatus.Text = ddlNewStatus.Text
                txtPostStatus.Text = ddlNewStatus.SelectedValue
                ddlNewStatus.SelectedIndex = 0

                lblMessage.Text = "ACTION: STATUS UPDATED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtCNNo.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)


                SQLDSCN.SelectCommand = txt.Text
                SQLDSCN.DataBind()
                GridView1.DataBind()

                'GridView1.DataSourceID = "SqlDataSource1"
                mdlPopupStatus.Hide()
            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnUpdateStatus_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub SQLDSCN_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SQLDSCN.Selected
        txtRowCount.Text = e.AffectedRows.ToString
    End Sub

    Protected Sub OnRowDataBoundgClient(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvClient, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        For Each row As GridViewRow In gvClient.Rows
            If row.RowIndex = gvClient.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#738A9C")
                row.ToolTip = String.Empty
            Else
                row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                row.ToolTip = "Click to select this row."
            End If
        Next
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

    Protected Sub OnSelectedIndexChangedgGL(sender As Object, e As EventArgs)
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

    Protected Sub txtReceiptnoSearch_TextChanged(sender As Object, e As EventArgs) Handles txtReceiptnoSearch.TextChanged
        If Len(txtReceiptnoSearch.Text.Trim) > 2 Then
            btnQuickSearch_Click(sender, e)

            'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            'MakeMeNull()
            'MakeMeNullBillingDetails()

            'If GridView1.Rows.Count > 0 Then
            '    txtMode.Text = "View"
            '    txtRcno.Text = GridView1.Rows(0).Cells(1).Text
            '    PopulateRecord()
            '    'GridView1_SelectedIndexChanged(sender, e)
            'End If
        End If
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)

        GridView1.DataBind()
    End Sub


    Protected Sub btnConfirmYesSavePost_Click(sender As Object, e As EventArgs) Handles btnConfirmYesSavePost.Click
        IsSuccess = PostCN()

        If IsSuccess = True Then

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "ADJUSTMENT NOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

            lblAlert.Text = ""
            updPnlSearch.Update()
            updPnlMsg.Update()

            'updpnlServiceRecs.Update()


            btnQuickSearch_Click(sender, e)

            lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
            updPnlMsg.Update()

            btnReverse.Enabled = True
            btnReverse.ForeColor = System.Drawing.Color.Black

            btnEdit.Enabled = False
            btnEdit.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnPost.Enabled = False
            btnPost.ForeColor = System.Drawing.Color.Gray
            'InsertNewLog()
        End If
    End Sub

    Public Function FindJNPeriod(BillingPeriod As String) As String
        Dim IsLock As String
        IsLock = "Y"

        Dim connPeriod As MySqlConnection = New MySqlConnection()

        connPeriod.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connPeriod.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text


        If txtMode.Text = "NEW" Then
            If txtDisplayRecordsLocationwise.Text = "N" Then
                command1.CommandText = "SELECT JNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            Else
                command1.CommandText = "SELECT JNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            End If
        Else
            If txtDisplayRecordsLocationwise.Text = "N" Then
                command1.CommandText = "SELECT JNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            Else
                command1.CommandText = "SELECT JNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            End If

        End If

        'If txtMode.Text = "NEW" Then
        '    command1.CommandText = "SELECT JNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        'Else
        '    command1.CommandText = "SELECT JNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        'End If

        command1.Connection = connPeriod

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)


        'If dt.Rows.Count > 0 Then
        '    IsLock = dt.Rows(0)("JNLock").ToString
        'End If


        If dt.Rows.Count > 0 Then
            If txtMode.Text = "NEW" Then
                IsLock = dt.Rows(0)("JNLock").ToString
            Else
                IsLock = dt.Rows(0)("JNLocke").ToString
            End If
            'IsLock = dt.Rows(0)("RVLock").ToString
        End If

        connPeriod.Close()
        connPeriod.Dispose()
        command1.Dispose()
        dt.Dispose()
        dr.Close()
        Return IsLock
    End Function

    Protected Sub txtAccountIdII_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIdII.TextChanged
        If Len(txtAccountIdII.Text) > 2 Then
            ImageButton1_Click(sender, New ImageClickEventArgs(0, 0))
        End If
    End Sub

    Protected Sub txtAccountId_TextChanged1(sender As Object, e As EventArgs) Handles txtAccountId.TextChanged
        If Len(txtAccountId.Text) > 2 Then
            ImageButton2_Click1(sender, New ImageClickEventArgs(0, 0))
        End If
    End Sub

    Protected Sub txtAccountIdSearch_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIdSearch.TextChanged
        If Len(txtAccountIdSearch.Text.Trim) > 2 Then
            btnQuickSearch_Click(sender, e)

            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            If GridView1.Rows.Count > 0 Then
                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
                'UpdatePanel2.Update()
                updPanelSave.Update()
                'UpdatePanel3.Update()

                'GridView1_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub


    Protected Sub btnDeleteDetail_Click(sender As Object, e As EventArgs)
        Try
            Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex

            lblMessage.Text = ""
            lblAlert.Text = ""

            Dim TextBoxRcno As TextBox = CType(grvBillingDetailsNew.Rows(rowindex1).Cells(5).FindControl("txtRcnoJournalDetailGVB"), TextBox)

            If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                    Dim conn As MySqlConnection = New MySqlConnection(constr)
                    conn.Open()

                    Dim command4 As MySqlCommand = New MySqlCommand
                    command4.CommandType = CommandType.Text

                    'Dim qry4 As String = "Update tblservicerecord Set BilledAmt = " & Convert.ToDecimal(row("PriceWithDisc")) & ", BillNo = '' where Rcno= @Rcno "
                    Dim qry4 As String = "Delete from tbljrnvdet where rcno = " & TextBoxRcno.Text
                    'Dim qry4 As String = "Delete from tblsalesdetail "
                    command4.CommandText = qry4
                    command4.Connection = conn
                    command4.ExecuteNonQuery()
                    command4.Dispose()

                    SqlDSSalesDetail.DataBind()
                    grvBillingDetailsNew.DataSourceID = "SqlDSSalesDetail"
                    grvBillingDetailsNew.DataBind()

                    '''''''''''''''''''''''''
                    CalculateTotal()
                    CalculateTotalPrice()
                    updPanelCN.Update()

                    'Exit Sub
                    Dim command5 As MySqlCommand = New MySqlCommand
                    command5.CommandType = CommandType.Text

                    Dim qry As String
                    qry = "Update tblJrnv set DebitBase = @DebitBase, DebitOriginal =@DebitOriginal, CreditBase=@CreditBase, CreditOriginal=@CreditOriginal "
                    'qry = qry + "  "
                    'qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                    qry = qry + " where Rcno = @Rcno;"

                    command5.CommandText = qry
                    command5.Parameters.Clear()

                    command5.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))

                    command5.Parameters.AddWithValue("@DebitBase", Convert.ToDecimal(txtDebitAmount.Text))
                    command5.Parameters.AddWithValue("@DebitOriginal", Convert.ToDecimal(txtDebitAmount.Text))
                    command5.Parameters.AddWithValue("@CreditBase", Convert.ToDecimal(txtCreditAmount.Text))
                    command5.Parameters.AddWithValue("@CreditOriginal", Convert.ToDecimal(txtCreditAmount.Text))
                    'command5.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command5.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command5.Connection = conn
                    command5.ExecuteNonQuery()

                    '''''''''''''''''''''''
                    conn.Close()
                    conn.Dispose()
                    command5.Dispose()

                    DisplayGLGrid()
                    'CalculatePrice()
                    IsDetailBlank()
                    updPanelCN.Update()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnDeleteDetail_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


   
    Protected Sub ImageButton8_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton8.Click
        lblAlert.Text = ""
        txtIsPopup.Text = ""
        txtIsPopup.Text = "ContractNo"
        Try

            If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                txtPopUpContractNo.Text = ddlContractNo.Text

                If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"
                Else
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
                End If

                SqlDSClient.DataBind()
                gvPopUpContractNo.DataBind()
                updPanelCN.Update()
            ElseIf String.IsNullOrEmpty(ddlContractNo.Text.Trim) = True Then
                txtPopUpContractNo.Text = ddlContractNo.Text

                If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') order by ContractNo"
                Else
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
                End If
                'SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName  FROM tblcontract WHERE (Status = 'O' or Status = 'P') order by ContractNo"

                SqlDSContractNo.DataBind()
                gvPopUpContractNo.DataBind()
                updPanelCN.Update()
            End If

            mdlPopUpContractNo.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "ImageButton8_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtPopUpContractNo_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpContractNo.TextChanged
        Try
            If txtPopUpContractNo.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter ContractNo/CustName", "str")
            Else
                SqlDSContractNo.SelectCommand = "SELECT  ContractNo, CustName from tblContract where (ContractNo like '%" & txtPopUpContractNo.Text & "%' or CustName like '%" & txtPopUpContractNo.Text & "%') and  (Status = 'O' or Status = 'P') order by ContractNo"


                'txtImportService.Text = SqlDSClient.SelectCommand
                SqlDSContractNo.DataBind()
                gvPopUpContractNo.DataBind()
                mdlPopUpContractNo.Show()
                txtIsPopup.Text = "ContractNo"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtPopUpContractNo_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvPopUpContractNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPopUpContractNo.SelectedIndexChanged

        Try

      
        txtIsPopup.Text = ""
        If (gvPopUpContractNo.SelectedRow.Cells(1).Text = "&nbsp;") Then
            ddlContractNo.Text = ""
        Else
            ddlContractNo.Text = gvPopUpContractNo.SelectedRow.Cells(1).Text.Trim
        End If

        txtIsPopup.Text = "N"
        mdlPopUpContractNo.Hide()
            mdlImportServices.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "gvPopUpContractNo_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvPopUpContractNo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPopUpContractNo.PageIndexChanging
        Try
            gvPopUpContractNo.PageIndex = e.NewPageIndex

            If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"
            Else
                SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
            End If

            SqlDSClient.DataBind()
            gvPopUpContractNo.DataBind()
            updPanelCN.Update()

            mdlPopUpContractNo.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "gvPopUpContractNo_PageIndexChanging", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ImageButton10_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton10.Click
        Try
            'txtPopUpContractNo.Text = ""
            'SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where (Status = 'O' or Status = 'P')"
            'SqlDSContractNo.DataBind()
            'gvPopUpContractNo.DataBind()
            'mdlPopUpContractNo.Show()

            'If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
            'txtPopUpContractNo.Text = ddlContractNo.Text
            'txtIsPopup.Text = ""
            txtPopUpContractNo.Text = ""
            If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P')  order by ContractNo"
            Else
                SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
            End If

            SqlDSContractNo.DataBind()
            gvPopUpContractNo.DataBind()
            updPanelCN.Update()
            mdlPopUpContractNo.Show()
            'ElseIf String.IsNullOrEmpty(ddlContractNo.Text.Trim) = True Then
            'txtPopUpContractNo.Text = ddlContractNo.Text

            'If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
            '    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') order by ContractNo"
            'Else
            '    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
            'End If
            ''SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName  FROM tblcontract WHERE (Status = 'O' or Status = 'P') order by ContractNo"

            'SqlDSContractNo.DataBind()
            'gvPopUpContractNo.DataBind()
            'updPanelInvoice.Update()
            'End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "ImageButton10_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Public Sub InsertIntoTblWebEventLog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", LoginID)
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtInvoiceNoGV_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)


            Dim lblid1 As TextBox = CType(txt1.FindControl("txtInvoiceNoGV"), TextBox)
            Dim lblid0 As DropDownList = CType(txt1.FindControl("txtItemTypeGV"), DropDownList)


            Dim lblid2 As DropDownList = CType(txt1.FindControl("txtContactTypeGV"), DropDownList)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtCustCodeGV"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtAccountIDGV"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtLocationIDGV"), TextBox)
            Dim lblid6 As TextBox = CType(txt1.FindControl("txtCustNameGV"), TextBox)
            Dim lblid7 As TextBox = CType(txt1.FindControl("txtCompanyGroupGV"), TextBox)

            lblAlert.Text = ""
            updPnlMsg.Update()


            '''''''''''''''''''''''''''''''''''
            If lblid0.Text = "INVOICE" Then
                'lblid0.Text = 0.0
                'lblid1.Text = 0.0
                'lblid8.Text = 0.0


                Dim IsInvDet As Boolean
                IsInvDet = False
                Dim lService As String
                lService = ""

                Dim connIsInvDet As MySqlConnection = New MySqlConnection()

                connIsInvDet.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connIsInvDet.Open()

                Dim commandIsInvDet As MySqlCommand = New MySqlCommand
                commandIsInvDet.CommandType = CommandType.Text
                'commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where InvoiceNumber = '" & lblid0.Text.Trim & "'"
                commandIsInvDet.CommandText = "SELECT  InvoiceNumber, ContactType, CustCode,  CustName, AccountID,  CompanyGroup, Location from tblsales where PostStatus = 'P' and InvoiceNumber = '" & lblid1.Text.Trim & "'"
                commandIsInvDet.Connection = connIsInvDet

                Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                Dim dtIsInvDet As New DataTable
                dtIsInvDet.Load(drIsInvDet)

                If dtIsInvDet.Rows.Count > 0 Then

                    lblid2.Text = dtIsInvDet.Rows(0)("ContactType").ToString
                    lblid3.Text = dtIsInvDet.Rows(0)("CustCode").ToString
                    lblid4.Text = dtIsInvDet.Rows(0)("AccountID").ToString
                    'Dim lblid4 As TextBox = dtIsInvDet.Rows(0)("InvoiceNumber").ToStringCType(txt1.FindControl("txtLocationIDGV"), TextBox)
                    lblid6.Text = dtIsInvDet.Rows(0)("CustName").ToString
                    lblid7.Text = dtIsInvDet.Rows(0)("CompanyGroup").ToString


                    If String.IsNullOrEmpty(txtLocation.Text) = True Then
                        txtLocation.Text = dtIsInvDet.Rows(0)("Location").ToString
                    End If
                    updPnlBillingRecs.Update()
                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''


                Else
                    'lblid1.Text = ""
                    lblAlert.Text = "INVALID INVOICE NUMBER"
                    updPnlMsg.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub

                End If

                'commandIsServiceRecord.Dispose()
                'connIsServiceRecord.Close()
                'connIsServiceRecord.Dispose()
            End If

            If lblid0.Text = "RECEIPT" Then
                'lblid0.Text = 0.0
                'lblid1.Text = 0.0
                'lblid8.Text = 0.0


                Dim IsInvDet As Boolean
                IsInvDet = False
                Dim lService As String
                lService = ""

                Dim connIsInvDet As MySqlConnection = New MySqlConnection()

                connIsInvDet.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connIsInvDet.Open()

                Dim commandIsInvDet As MySqlCommand = New MySqlCommand
                commandIsInvDet.CommandType = CommandType.Text
                'commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where InvoiceNumber = '" & lblid0.Text.Trim & "'"
                commandIsInvDet.CommandText = "SELECT  ReceiptNumber, ContactType, CustomerCode,  ReceiptFrom, AccountID,  CompanyGroup, Location from tblRecv where PostStatus = 'P' and ReceiptNumber = '" & lblid1.Text.Trim & "'"
                commandIsInvDet.Connection = connIsInvDet

                Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                Dim dtIsInvDet As New DataTable
                dtIsInvDet.Load(drIsInvDet)

                If dtIsInvDet.Rows.Count > 0 Then

                    lblid2.Text = dtIsInvDet.Rows(0)("ContactType").ToString
                    lblid3.Text = dtIsInvDet.Rows(0)("CustomerCode").ToString
                    lblid4.Text = dtIsInvDet.Rows(0)("AccountID").ToString
                    'Dim lblid4 As TextBox = dtIsInvDet.Rows(0)("InvoiceNumber").ToStringCType(txt1.FindControl("txtLocationIDGV"), TextBox)
                    lblid6.Text = dtIsInvDet.Rows(0)("ReceiptFrom").ToString
                    lblid7.Text = dtIsInvDet.Rows(0)("CompanyGroup").ToString

                    If String.IsNullOrEmpty(txtLocation.Text) = True Then
                        txtLocation.Text = dtIsInvDet.Rows(0)("Location").ToString
                    End If

                    updPnlBillingRecs.Update()
                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''


                Else
                    'lblid1.Text = ""
                    lblAlert.Text = "INVALID RECEIPT NUMBER"
                    updPnlMsg.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub

                End If

                'commandIsServiceRecord.Dispose()
                'connIsServiceRecord.Close()
                'connIsServiceRecord.Dispose()
            End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtInvoiceNoGV_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        mdlPopupSearch.Show()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblMessage.Text = ""
            txtTotalInvoiceAmount.Text = "0.00"
            txtCondition.Text = ""

            Dim strsql As String

            ''strsql = SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, 
            'strsql = "Select PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, Billcity,  "
            ''strsql = strsql & "AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , CreditDays, RecurringInvoice, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno
            'strsql = strsql & " ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, yourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , TermsDay, RecurringInvoice,  BillSchedule, "
            'strsql = strsql & " ReceiptBase, CreditBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CustAddStreet, custAddBuilding,  CustAddCity, PrintCounter,"
            'strsql = strsql & " CreatedBy,   CreatedOn, LastModifiedBy, LastModifiedOn, Rcno "
            'strsql = strsql & " from tblSales where 1=1 "


            strsql = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, LedgerCode, LedgerName, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule FROM tblsales WHERE 1=1   "

            'txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  "

            txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  "

            Dim YrStrList As List(Of [String]) = New List(Of String)()

            'If rdbStatusSearch.SelectedValue = "ALL" Then
            '    For Each item As ListItem In chkStatusSearch.Items
            '        YrStrList.Add(item.Value)
            '    Next
            'Else
            For Each item As ListItem In chkStatusSearch0.Items
                If item.Selected Then
                    YrStrList.Add("'" & item.Value & "'")
                End If
            Next

            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                txtCondition.Text = txtCondition.Text + " and Location = '" & txtLocation.Text & "'"
            End If


            txtCondition.Text = txtCondition.Text & " and PostStatus in (" & (YrStr) & ") "


            If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and InvoiceNumber like '%" & txtSearchInvoiceNo.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtSearchAccountID.Text) = False Then
                'strsql = strsql & " and (AccountId like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
                txtCondition.Text = txtCondition.Text & " and (AccountId like '%" & txtSearchAccountID.Text.Trim & "%')"

            End If

            If String.IsNullOrEmpty(txtSearchClientName.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and CustName like ""%" & txtSearchClientName.Text.Trim & "%"""
            End If


            If String.IsNullOrEmpty(txtSearchOurRef.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and OurRef like ""%" & txtSearchOurRef.Text.Trim & "%"""
            End If

            If String.IsNullOrEmpty(txtSearchYourRef.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and YourRef like ""%" & txtSearchYourRef.Text.Trim & "%"""
            End If

            If String.IsNullOrEmpty(txtSearchPONo.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and PoNo like ""%" & txtSearchPONo.Text.Trim & "%"""
            End If

            'If (ddlCompanyGrpSearch.SelectedIndex > 0) Then
            '    txtCondition.Text = txtCondition.Text & " and CompanyGroup like '%" & ddlCompanyGrpSearch.Text.Trim + "%'"
            'End If

            'If String.IsNullOrEmpty(txtBillSchedule.Text) = False Then
            '    strsql = strsql & " and BillSchedule like '%" & txtBillSchedule.Text.Trim + "%'"
            'End If


            If (ddlSearchSalesman.SelectedIndex > 0) Then
                txtCondition.Text = txtCondition.Text & " and StaffCode like '%" & ddlSearchSalesman.Text.Trim + "%'"
            End If


            If rdbSearchPaidStatus0.SelectedItem.Value = "Fully Paid" Then
                txtCondition.Text = txtCondition.Text + " and BalanceBase = 0 and ValueBase <> 0 "
            ElseIf rdbSearchPaidStatus0.SelectedItem.Value = "O/S" Then
                txtCondition.Text = txtCondition.Text + " and BalanceBase <>  0"
            End If


            If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and SalesDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and SalesDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and SalesDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and SalesDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtSearchEntryDateFrom.Text) = False And txtSearchEntryDateFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and CreatedOn >= '" + Convert.ToDateTime(txtSearchEntryDateFrom.Text).ToString("yyyy-MM-dd") + " 00:00:00'"
            End If
            If String.IsNullOrEmpty(txtSearchEntryDateTo.Text) = False And txtSearchEntryDateTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and CreatedOn <= '" + Convert.ToDateTime(txtSearchEntryDateTo.Text).ToString("yyyy-MM-dd") + " 00:00:000'"
            End If


            If String.IsNullOrEmpty(txtSearchEditEndFrom.Text) = False And txtSearchEditEndFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and LastModifiedOn >= '" + Convert.ToDateTime(txtSearchEditEndFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtSearchEditEndTo.Text) = False And txtSearchEditEndTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and LastModifiedOn <= '" + Convert.ToDateTime(txtSearchEditEndTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If (ddlSearchEditedBy.SelectedIndex > 0) Then
                txtCondition.Text = txtCondition.Text & " and LastModifiedBy = '" & ddlSearchEditedBy.Text.Trim + "'"
            End If


            If (ddlSearchCreatedBy.SelectedIndex > 0) Then
                txtCondition.Text = txtCondition.Text & " and CreatedBy = '" & ddlSearchCreatedBy.Text.Trim + "'"
            End If

            If String.IsNullOrEmpty(txtSearchComments.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and Comments like '%" & txtSearchComments.Text.Trim & "%'"
            End If

            txtOrderBy.Text = " order by rcno desc, custname "

            strsql = strsql + txtCondition.Text + txtOrderBy.Text + " limit " & txtLimit.Text
            txt.Text = strsql
            'txtComments.Text = strsql
            SQLDSCN.SelectCommand = strsql
            SQLDSCN.DataBind()
            GridView1.DataBind()

            CalculateTotal()

            ''''''''''''''''''''''''''''''
            If Convert.ToInt32(txtRowCount.Text) > 0 Then

                'btnQuickSearch_Click(sender, e)

                Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo


                MakeMeNull()
                MakeMeNullBillingDetails()

                If GridView1.Rows.Count > 0 Then
                    txtMode.Text = "View"

                    If String.IsNullOrEmpty(txtRcnoSelected.Text.Trim) = False Then
                        If txtRcnoSelected.Text > 0 Then
                            txtRcno.Text = txtRcnoSelected.Text
                            txtRcnoSelected.Text = 0
                        Else
                            txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                        End If
                    Else
                        txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                    End If

                    'txtRcno.Text = GridView1.Rows(0).Cells(1).Text

                    PopulateRecord()
                    'UpdatePanel2.Update()
                    GridView1.SelectedIndex = 0
                    updPanelSave.Update()
                    'UpdatePanel3.Update()

                    'GridView1_SelectedIndexChanged(sender, e)
                Else
                    MakeMeNull()
                    MakeMeNullBillingDetails()
                End If
            End If

            lblMessage.Text = "NUMBER OF RECORDS FOUND : " + txtRowCount.Text + "   [DISPLAYING TOP " + txtLimit.Text + " RECORDS]"
            '+ "   [DISPLAYING TOP " + txtLimit.Text + " RECORDS]"
            '''''''''''''''''''''''''''''


            updPnlMsg.Update()
            updPanelCN.Update()
            'SqlDSMultiPrint.SelectCommand = SQLDSInvoice.SelectCommand
            'GridSelected = "SQLDSContract"



            txtSearchAccountID.Text = ""
            txtSearchClientName.Text = ""
            'txtSearchAddress.Text = ""
            'txtSearchContact.Text = ""
            'txtSearchContactNo.Text = ""
            'txtSearchPostal.Text = ""

            txtSearchOurRef.Text = ""
            txtSearchYourRef.Text = ""

            ddlSearchSalesman.ClearSelection()
            'ddlSearchScheduler.ClearSelection()
            'ddlSearchStatus.ClearSelection()
            'ddlSearchContractGroup.ClearSelection()
            'ddlSearchCompanyGroup.ClearSelection()
            'ddlSearchLocationGroup.ClearSelection()
            'ddlSearchRenewalStatus.ClearSelection()
            'ddlSearchInChargeId.ClearSelection()

            txtInvoiceDateSearchFrom.Text = ""
            txtInvoiceDateSearchTo.Text = ""
            'txtSearchServiceStartFrom.Text = ""
            'txtSearchServiceStartTo.Text = ""
            'txtSearchServiceEndFrom.Text = ""
            'txtSearchServiceEndTo.Text = ""
            txtSearchEditEndFrom.Text = ""
            txtSearchEditEndTo.Text = ""

            'txtSearchStartDateFrom.Text = ""
            'txtSearchStartDateTo.Text = ""

            'txtSearchEndDateFrom.Text = ""
            'txtSearchEndDateTo.Text = ""
            txtSearchEntryDateFrom.Text = ""
            txtSearchEntryDateTo.Text = ""

        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnClient2_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient2.Click
        lblAlert.Text = ""
        lblAlert1.Text = ""
        txtSearch.Text = ""
        txtClientFrom.Text = ""

        txtClientFrom.Text = "InvoiceFilter"
        txtSearch.Text = "InvoiceFilter"
        Try

            If String.IsNullOrEmpty(txtSearchAccountID.Text.Trim) = False Then
                txtPopUpClient.Text = ""
                txtPopUpClient.Text = txtSearchAccountID.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text
                'UpdatePanel1.Update()


                If ddlSearchContactType.Text = "CORPORATE" Or ddlSearchContactType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"" or  B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlSearchContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlSearchContactType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"" or D.PersonID like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"" or  B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"" or  D.PersonID like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelCN.Update()
            Else
                txtPopUpClient.Text = ""


                If ddlSearchContactType.Text = "CORPORATE" Or ddlSearchContactType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlSearchContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlSearchContactType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')   order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by AccountID,  LocationId, ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelCN.Update()
            End If

            'txtImportService.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
            'txtImportService.Text = SqlDSClient.SelectCommand
            'mdlPopupSearch.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "btnClient2_Click", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        End Try
    End Sub

    Public Sub FindLocation()
        Dim IsLock As String
        IsLock = ""

        Dim connLocation As MySqlConnection = New MySqlConnection()

        connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connLocation.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text
        command1.CommandText = "SELECT LocationID, Location FROM tblstaff where StaffId='" & txtCreatedBy.Text.ToUpper & "'"
        command1.Connection = connLocation

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            txtLocation.Text = dt.Rows(0)("LocationID").ToString
        End If

        connLocation.Close()
        connLocation.Dispose()
        command1.Dispose()
        dt.Dispose()
    End Sub

    Protected Sub ddlContactTypeSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContactTypeSearch.SelectedIndexChanged

    End Sub

    Protected Sub btnStatusSearch_Click1(sender As Object, e As EventArgs) Handles btnStatusSearch.Click
        Try
            Dim YrStrList As List(Of [String]) = New List(Of String)()

            'If rdbStatusSearch.SelectedValue = "ALL" Then
            '    For Each item As ListItem In chkStatusSearch.Items
            '        YrStrList.Add(item.Value)
            '    Next
            'Else
            For Each item As ListItem In chkStatusSearch.Items
                If item.Selected Then
                    YrStrList.Add(item.Value)
                End If
            Next

            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

            txtSearch1Status.Text = YrStr
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub

    Protected Sub txtPopUpGL_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpGL.TextChanged
        Try
            If txtPopUpGL.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter Team name", "str")
            Else
                'SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and TeamName like '" + ViewState("TeamCurrentAlphabet") + "%' And upper(TeamName) Like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%'"
                SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts where  COACode like '%" + txtPopUpGL.Text.Trim.ToUpper + "%' order by COACode "

                SqlDSGL.DataBind()
                grvGL.DataBind()
                'im()
                'imgBtnGL_ModalPopupExtender.show()
                'imgBtnGL_ModalPopupExtender.mdl imgBtnGL_ModalPopupExtender.mdlPopUpTeam.Show()
                txtIsPopup.Text = "GL"
                'IsPopUpTeam = "Y"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("ADJUSTMENT - " + Session("UserID"), "txtPopUpGL_TextChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnPopUpGLReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpGLReset.Click
        txtPopUpGL.Text = "Search Here for GL Code or Description"
        SqlDSGL.SelectCommand = "SELECT * From tblChartOfAccounts order by COACode"
        SqlDSGL.DataBind()
        grvGL.DataBind()

        mdlPopupGL.Show()
        txtIsPopup.Text = "GL"
    End Sub

    Protected Sub btnLedgerCode_Click(sender As Object, e As ImageClickEventArgs) Handles btnLedgerCode.Click
        txtGLFrom.Text = "LedgerSearch"
        Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

        'Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        ''Dim lblid1 As TextBox = CType(xrow1.FindControl("txtOtherCodeGV"), TextBox)

        'Dim rowindex1 As Integer = xrow1.RowIndex
        'txtxRow.Text = rowindex1
        'updPanelSave.Update()
        mdlPopupGL.Show()
    End Sub

    Protected Sub tb1_ActiveTabChanged(sender As Object, e As EventArgs) Handles tb1.ActiveTabChanged
        lblAlert.Text = ""
        '   lblAlert.Text = tb1.ActiveTabIndex.ToString

        If tb1.ActiveTabIndex = 1 Then
            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Then
                lblAlert.Text = "Cannot switch tabs in Add or Edit Mode!! Save or Cancel the record to proceed!!"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtCNNo.Text) Then
                lblAlert.Text = "Select an Adjustment to Proceed"

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                tb1.ActiveTabIndex = 0
                Exit Sub
            End If

            'If GridView1.Rows.Count = 0 Then
            '    txtSelectedIndex.Text = "-1"
            'Else
            '    txtSelectedIndex.Text = "0"
            'End If

            'GridView1.SelectedIndex = 0
            'GridView1_SelectedIndexChanged(New Object(), New EventArgs)
            'OnSelectedIndexChangedg1(New Object(), New EventArgs)
        End If

        If tb1.ActiveTabIndex = 1 Then

            lblFileUploadVoucherNo.Text = txtCNNo.Text
            ' lblFileUploadAccountID.Text = txtAccountId.Text
            lblFileUploadName.Text = txtClientName.Text

            iframeid.Attributes.Add("src", "about:blank")

            'View Uploaded files

            'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
            'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            'Dim files As List(Of ListItem) = New List(Of ListItem)

            'For Each foundFile As FileInfo In filesInDir
            '    Dim fullName As String = foundFile.FullName
            '    files.Add(New ListItem(foundFile.Name))
            'Next


            'gvUpload.DataSource = files
            'gvUpload.DataBind()

            SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtCNNo.Text + "'"
            gvUpload.DataSourceID = "SqlDSUpload"
            gvUpload.DataBind()

            lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
        End If

    End Sub

    Protected Sub UploadFile(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(lblFileUploadVoucherNo.Text) Then
                lblAlert.Text = "SELECT RECEIPT TO UPLOAD FILE"
                Return

            End If

            If String.IsNullOrEmpty(txtFileDescription.Text) Then
                lblAlert.Text = "ENTER FILE DESCRIPTION TO UPLOAD FILE"
                Exit Sub

            End If
            InsertIntoTblWebEventLog("CN - UPLOAD1", "BTNUPLOAD", FileUpload1.HasFile.ToString, txtCNNo.Text)

            If FileUpload1.HasFile Then

                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim ext As String = Path.GetExtension(fileName)

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                InsertIntoTblWebEventLog("RECEIPT - UPLOAD2", "BTNUPLOAD", ext, txtCNNo.Text)

                If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then

                    Dim strFilePath As String = Server.MapPath("~/Uploads/Accounts/Journal/")

                    strFilePath = strFilePath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")

                    If File.Exists(strFilePath + txtCNNo.Text + "_" + fileName) Then

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", txtCNNo.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            '  MessageBox.Message.Alert(Page, "Record already exists!!!", "str")
                            lblAlert.Text = "FILE ALREADY EXISTS"
                            conn.Close()

                            Exit Sub
                        End If
                    Else
                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", txtCNNo.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            Dim command2 As MySqlCommand = New MySqlCommand

                            command2.CommandType = CommandType.Text

                            command2.CommandText = "delete from fileupload where filenamelink='" + txtCNNo.Text + "_" + fileName + "'"

                            command2.Connection = conn

                            command2.ExecuteNonQuery()
                        End If
                    End If
                    FileUpload1.PostedFile.SaveAs(strFilePath + txtCNNo.Text + "_" + fileName)

                    'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
                    'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
                    'Dim files As List(Of ListItem) = New List(Of ListItem)

                    'For Each foundFile As FileInfo In filesInDir
                    '    Dim fullName As String = foundFile.FullName
                    '    files.Add(New ListItem(foundFile.Name))
                    'Next
                    ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
                    ''For Each filePath As String In filePaths
                    ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
                    ''Next

                    'ADD FILE UPLOAD INFORMATION TO DATABASE INORDER TO STORE FILES WITH DESCRIPTION - 20170930

                    InsertIntoTblWebEventLog("JOURNAL - UPLOAD3", "BTNUPLOAD", ext, txtCNNo.Text)


                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@FileGroup,@FileRef,@FileName,@FileDescription,@FileType,@FileNameLink,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"


                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                        command.Parameters.AddWithValue("@FileGroup", "JOURNAL")

                        command.Parameters.AddWithValue("@FileRef", txtCNNo.Text)
                        command.Parameters.AddWithValue("@FileName", fileName.ToUpper)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtCNNo.Text + "_" + fileName.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@FileGroup", "JOURNAL")

                        command.Parameters.AddWithValue("@FileRef", txtCNNo.Text)
                        command.Parameters.AddWithValue("@FileName", fileName)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtCNNo.Text + "_" + fileName)

                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    conn.Close()
                    conn.Dispose()
                    command.Dispose()

                    InsertIntoTblWebEventLog("JOURNAL - UPLOAD4", "BTNUPLOAD", ext, txtCNNo.Text)


                    SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtCNNo.Text + "'"
                    gvUpload.DataSourceID = "SqlDSUpload"
                    gvUpload.DataBind()
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtCNNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", txtCNNo.Text + "_" + fileName, txtCNNo.Text)

                    '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAccountID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtAccountID.Text + "_" + fileName)

                    txtFileDescription.Text = ""

                    lblMessage.Text = "FILE UPLOADED"
                    lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
                    InsertIntoTblWebEventLog("JOURNAL - UPLOAD5", "BTNUPLOAD", ext, txtCNNo.Text)

                Else
                    lblAlert.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
                    Return
                End If
            Else
                lblAlert.Text = "SELECT FILE TO UPLOAD"
            End If
            '  Response.Redirect(Request.Url.AbsoluteUri)
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL - " + txtCreatedBy.Text, "Upload File", ex.Message.ToString, txtCNNo.Text + "-" + FileUpload1.PostedFile.FileName)
        End Try
    End Sub

    'Protected Sub btnEditFileDesc_Click(sender As Object, e As EventArgs)
    '    Dim btn1 As ImageButton = DirectCast(sender, ImageButton)

    '    Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
    '    Dim rowindex1 As Integer = xrow1.RowIndex



    '    Dim rRECEIPTofile = DirectCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text
    '    txtfilercno.Text = rcnofile.ToString

    '    lblMessage.Text = ""
    '    lblAlert.Text = ""

    '    txtEditFileName.Text = GridView1.Rows(rowindex1).Cells(0).Text
    '    txtEditFileDescription.Text = GridView1.Rows(rowindex1).Cells(1).Text

    '    mdlPopupEditFileDesc.Show()


    'End Sub


    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            filePath = Server.MapPath("~/Uploads/Accounts/Journal/") + filePath
            filePath = filePath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")

            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL - " + txtCreatedBy.Text, "DownloadFile", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            txtFileLink.Text = filePath

            filePath = Server.MapPath("~/Uploads/Accounts/Journal/") + filePath
            filePath = filePath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")

            txtDeleteUploadedFile.Text = filePath
            iframeid.Attributes.Add("src", "about:blank")

            lblQuery.Text = "Are you sure to DELETE the File? <br><br> File Name : " + txtFileLink.Text

            mdlPopupDeleteUploadedFile.Show()

            'File.Delete(filePath)
            ''  Response.Redirect(Request.Url.AbsoluteUri)
            'lblMessage.Text = "FILE DELETED"
            'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/"))
            'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            'Dim files As List(Of ListItem) = New List(Of ListItem)

            'For Each foundFile As FileInfo In filesInDir
            '    Dim fullName As String = foundFile.FullName
            '    files.Add(New ListItem(foundFile.Name))
            'Next
            ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
            ''For Each filePath As String In filePaths
            ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            ''Next
            'gvUpload.DataSource = files
            'gvUpload.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL - " + txtCreatedBy.Text, "DeleteFile", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If ConfigurationManager.AppSettings("DomainName") = "MALAYSIA PRE-PRODUCTION" Then
                Dim filepath1 As String = Server.MapPath("Uploads\Accounts\Journal\") + txtFileLink.Text
                Dim filepath2 As String = "E:\WEBSITE FILES\MalaysiaPreproduction\Uploads\Accounts\Journal\" + txtFileLink.Text

                File.Copy(filepath1, filepath2)

            End If

            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            Dim ext As String = Path.GetExtension(filePath)
            filePath = "Uploads/Accounts/Journal/" + filePath
            ext = ext.ToLower

            '  filePath = Server.MapPath("~/Uploads/") + filePath
            '    frmWord.Attributes["src"] = http://localhost/MyApp/resume.doc;
            ' iframeid.Attributes.Add("src", Server.HtmlDecode("D:\1_CWBInfotech\A_Sitapest\Program\Sitapest\Uploads\10000145_photo (1).JPG"))
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Accounts\Journal\")
                Dim strFile As String = CType(sender, LinkButton).CommandArgument
                Dim File As String() = strFile.Split("."c)
                Dim strExtension As String = ext
                Dim strUrl As String = "http://" + Request.Url.Authority + "/WordinIFrame/ConvertedLocation/"

                Dim Filename As String = strFilePath + strFile.Split("."c)(0) & Convert.ToString(".html")

                If System.IO.File.Exists(Filename) Then
                    System.IO.File.Delete(Filename)
                End If

                If ext = ".doc" Or ext = ".docx" Then
                    ConvertHTMLFromWord(strFilePath & strFile, strFilePath + "A" + strFile.Split("."c)(0) & Convert.ToString(".html"))

                ElseIf ext = ".xls" Or ext = ".xlsx" Then
                    ConvertHtmlFromExcel(strFilePath + strFile, strFilePath + "A" + strFile.Split("."c)(0) + ".html")
                End If

                iframeid.Attributes("src") = "Uploads/Accounts/Journal/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                iframeid.Attributes.Add("src", filePath)

            End If
            '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL - " + txtCreatedBy.Text, "PreviewFile", ex.Message.ToString, "")
        End Try
    End Sub

    Public Sub ConvertHTMLFromWord(Source As Object, Target As Object)
        If Word Is Nothing Then
            ' Check for the prior instance of the OfficeWord Object
            Word = New Microsoft.Office.Interop.Word.ApplicationClass()
        End If

        Try
            ' To suppress window display the following code will help
            Word.Visible = False
            Word.Application.Visible = False
            Word.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize



            Word.Documents.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML

            Word.ActiveDocument.SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS
            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Word IsNot Nothing Then
                Word.Documents.Close(Unknown, Unknown, Unknown)
                Word.Quit(Unknown, Unknown, Unknown)
            End If
        End Try
    End Sub

    Public Sub ConvertHtmlFromExcel(Source As String, Target As String)
        If Excel Is Nothing Then
            Excel = New Microsoft.Office.Interop.Excel.ApplicationClass()
        End If

        Try
            Excel.Visible = False
            Excel.Application.Visible = False
            Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized

            Excel.Workbooks.Open(Source, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown, Unknown, Unknown, Unknown, _
                Unknown, Unknown, Unknown)

            Dim format As Object = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml

            Excel.Workbooks(1).SaveAs(Target, format, Unknown, Unknown, Unknown, Unknown, _
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Unknown, Unknown, Unknown, Unknown, Unknown)

            Status = StatusType.SUCCESS

            Message = Status.ToString()
        Catch e As Exception
            Message = "Error :" + e.Message.ToString().Trim()
        Finally
            If Excel IsNot Nothing Then
                Excel.Workbooks.Close()
                Excel.Quit()
            End If
        End Try
    End Sub

    'Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim filePath As String = CType(sender, LinkButton).CommandArgument
    '        filePath = Server.MapPath("~/Uploads/Service/") + filePath
    '        'lblMessage.Text = filePath
    '        'Return

    '        Response.ContentType = ContentType
    '        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
    '        Response.WriteFile(filePath)
    '        Response.End()
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("Invoice", "DownloadFile", ex.Message.ToString, txtInvoiceNo.Text)
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try
    'End Sub

    Protected Sub EmailFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            Session.Add("FileName", filePath)
            filePath = Server.MapPath("~/Uploads/Accounts/Journal/") + filePath
            'lblMessage.Text = filePath
            'Return
            Session.Add("FilePath", filePath)

            '    Response.Redirect("Email.aspx?Type=FileUpload")
            Dim Url As String = "Email.aspx?Type=FileUpload"
            Response.Write("<script language='javascript'>window.open('" & Url & "','_blank','');")
            Response.Write("</script>")
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL", "EmailFile", ex.Message.ToString, txtCNNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        ' InsertIntoTblWebEventLog("FILEDELETE1", "ConfirmDelete", "1", txtInvoiceNo.Text)

        Dim deletefilepath1 As String = Server.MapPath("~/Uploads/Accounts/Journal/DeletedFiles/") + txtFileLink.Text
        Dim deletefilepath As String = Server.MapPath("~/Uploads/Accounts/Journal/DeletedFiles/") + Path.GetFileNameWithoutExtension(deletefilepath1) + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("ssmmhh") + Path.GetExtension(deletefilepath1)

        deletefilepath = deletefilepath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")
        deletefilepath1 = deletefilepath1.Replace("MalaysiaPreProduction", "AnticimexMalaysia")


        File.Move(txtDeleteUploadedFile.Text, deletefilepath)


        'File.Delete(txtDeleteUploadedFile.Text)
        '  Response.Redirect(Request.Url.AbsoluteUri)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblfileupload where filenamelink='" + txtFileLink.Text + "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String = "delete from tblfileupload where filenamelink='" + txtFileLink.Text + "'"

                command.CommandText = qry

                command.Connection = conn

                command.ExecuteNonQuery()
                command.Dispose()

                '  MessageBox.Message.Alert(Page, "Record deleted successfully!!!", "str")
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOADDELETE", txtFileLink.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtFileLink.Text)

            End If
            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()
            lblMessage.Text = "FILE DELETED"

            SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtCNNo.Text + "'"
            gvUpload.DataSourceID = "SqlDSUpload"
            gvUpload.DataBind()

            'Dim myDir As New DirectoryInfo(Server.MapPath("~/Uploads/Customer/"))
            'Dim filesInDir As FileInfo() = myDir.GetFiles((Convert.ToString(txtAccountID.Text + "_")) + "*.*")
            'Dim files As List(Of ListItem) = New List(Of ListItem)

            'For Each foundFile As FileInfo In filesInDir
            '    Dim fullName As String = foundFile.FullName
            '    files.Add(New ListItem(foundFile.Name))
            'Next
            ''Dim filePaths() As String = Directory.GetFiles(Server.MapPath("~/Uploads/") + txtAccountID.Text + "_*")
            ''For Each filePath As String In filePaths
            ''    files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            ''Next
            'gvUpload.DataSource = files
            'gvUpload.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("JOURNAL - " + txtCreatedBy.Text, "FILE DELETE", ex.Message.ToString, txtCNNo.Text + " " + txtFileLink.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

End Class
