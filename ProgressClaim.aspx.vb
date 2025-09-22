Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Drawing

' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading

Partial Class ProgressClaim
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
    Public lCreditAmount As Decimal

    Public IsSuccess As Boolean


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
            txtAccountName.Attributes.Add("readonly", "readonly")
            'txtContactPerson.Attributes.Add("readonly", "readonly")

            txtBillAddress.Attributes.Add("readonly", "readonly")
            txtBillBuilding.Attributes.Add("readonly", "readonly")
            txtBillStreet.Attributes.Add("readonly", "readonly")
            txtBillCity.Attributes.Add("readonly", "readonly")
            txtBillState.Attributes.Add("readonly", "readonly")

            txtBillCountry.Attributes.Add("readonly", "readonly")
            txtBillPostal.Attributes.Add("readonly", "readonly")
            'txtTotal.Attributes.Add("readonly", "readonly")
            'txtTaxRatePct.Attributes.Add("readonly", "readonly")

            txtCNNo.Attributes.Add("readonly", "readonly")
            txtReceiptPeriod.Attributes.Add("readonly", "readonly")
            txtCompanyGroup.Attributes.Add("readonly", "readonly")
            ddlContactType.Attributes.Add("readonly", "readonly")
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


            txtCNDate.Attributes.Add("readonly", "readonly")

          

            btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")
        
            If Not Page.IsPostBack Then
                mdlPopUpClient.Hide()
                'mdlImportInvoices.Hide()
                'mdlImportServices.Hide()
                mdlPopupGL.Hide()

                Dim Query As String


                Query = "SELECT companygroup FROM tblcompanygroup order by companygroup"
                PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrp)
                PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrpSearch)
                'PopulateDropDownList(Query, "companygroup", "companygroup", ddlCompanyGrpII)

                Query = "Select StaffID from tblStaff where Status = 'O' and Roles <> 'TECHNICAL'"
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchEditedBy)
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchCreatedBy)


                'SELECT StaffId FROM tblstaff where roles= 'SALES MAN' ORDER BY STAFFID
                Query = "SELECT contractgroup FROM tblcontractgroup ORDER BY contractgroup"

                'PopulateDropDownList(Query, "contractgroup", "contractgroup", ddlContractGroup)
                PopulateDropDownList(Query, "contractgroup", "contractgroup", ddlContractGroupIS)

                'SELECT UPPER(contractgroup) FROM tblcontractgroup ORDER BY contractgroup
                Query = "SELECT Frequency FROM tblServiceFrequency order by Frequency"
                PopulateDropDownList(Query, "Frequency", "Frequency", ddlServiceFrequency)


                Query = "SELECT Frequency FROM tblFrequency  order by Frequency "
                PopulateDropDownList(Query, "Frequency", "Frequency", ddlBillingFrequency)


                SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
                SqlDSGL.DataBind()

                'SELECT Frequency FROM tblFrequency  order by Frequency 
                'SELECT Frequency FROM tblServiceFrequency order by Frequency 

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

              
                Dim sql As String


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
                    'If dt2.Rows(0)("COACode").ToString <> "" Then : txtGLCodeII.Text = dt2.Rows(0)("COACode").ToString : End If

                    If dt2.Rows(0)("COADescription").ToString <> "" Then : txtLedgerNameIS.Text = dt2.Rows(0)("COADescription").ToString : End If
                    'If dt2.Rows(0)("COADescription").ToString <> "" Then : txtLedgerNameII.Text = dt2.Rows(0)("COADescription").ToString : End If
                End If



                ''''''''''''''''''''''''''''''''''''''''''''''''
                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                commandServiceRecordMasterSetup.CommandText = "SELECT ShowCNOnScreenLoad, CNRecordMaxRec,DisplayRecordsLocationWise,PostCN, CreditNoteOnlyEditableByCreator, DebitNoteOnlyEditableByCreator, DefaultTaxCode FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("CNRecordMaxRec")
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostCN").ToString
                txtOnlyEditableByCreatorCN.Text = dtServiceRecordMasterSetup.Rows(0)("CreditNoteOnlyEditableByCreator").ToString
                txtOnlyEditableByCreatorDN.Text = dtServiceRecordMasterSetup.Rows(0)("DebitNoteOnlyEditableByCreator").ToString
                txtDefaultTaxCode.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString


                '''''''''''''''

                sql = ""


                sql = "Select TaxRatePct from tbltaxtype where TaxType = '" & txtDefaultTaxCode.Text & "'"

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


                '''''''''''''


                conn.Close()
                conn.Dispose()

                command1.Dispose()
                command2.Dispose()
                dt.Dispose()
                dt2.Dispose()
                dtServiceRecordMasterSetup.Dispose()

                dr.Close()
                dr2.Close()
                ''''''''''''''''''''''''''''''''''''''''''
                'conn.Close()

                '''''''''''''''''''''''

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    lblBranch1.Visible = True
                    txtLocation.Visible = True

                    lblBranch.Visible = True
                    ddlBranch.Visible = True
                Else
                    lblBranch1.Visible = False
                    txtLocation.Visible = False

                    lblBranch.Visible = False
                    ddlBranch.Visible = False
                End If


                ''''''''''''''''''''''''''''''''''
                MakeMeNull()
                DisableControls()
                PopulateArCode()
                txt.Text = SQLDSCN.SelectCommand

                txtGroupAuthority.Text = Session("SecGroupAuthority")
                txtSearch1Status.Text = "O,P"
                '''''''''''''''''''''''
                Session.Add("customerfrom", Request.QueryString("CustomerFrom"))
                If String.IsNullOrEmpty(Session("customerfrom")) = False Then
                    Session.Add("invoiceno", Request.QueryString("VoucherNumber"))
                    If Request.QueryString("VoucherNumber") <> "" Then
                        txtReceiptnoSearch.Text = Session("invoiceno")
                        txtFrom.Text = Session("customerfrom")

                        btnQuickSearch_Click(sender, e)
                        Session.Remove("invoiceno")
                        Session.Remove("customerfrom")

                        btnQuit.Text = "BACK"
                        ''''' Retrieve rcno for the Invoice 

                        ''''' Retrieve rcno for the Invoice 
                        GridView1_SelectedIndexChanged(New Object(), New EventArgs)

                    End If
                    Exit Sub
                End If

                ''''''''''''''''''''''''''
                If Session("receiptfrom") = "invoice" Then
                    'txtInvoiceNoSearch.Text = Session("invoiceno")
                    'sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.CNNumber, A.CNDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.CustomerName,  A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno FROM tblCN A, tblCNdet B where A.CNNumber = B.CNNumber and B.InvoiceNo = '" & txtReceiptnoSearch.Text & "' ORDER BY Rcno DESC, CustomerName"
                    'SQLDSCN.SelectCommand = sqltext
                    'btnBack.Visible = True
                    'btnQuit.Visible = False
                ElseIf Session("cnfrom") = "invoice" Then
                    txtInvoiceNoSearch.Text = Session("invoiceno")
                    'txt.Text = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo,   Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule, LedgerCode, LedgerName, PrintCounter FROM tblsales  WHERE 1=1  "
                    'txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  AND (PostStatus = 'O' OR PostStatus = 'P') and  InvoiceNumber = '" & txtReceiptnoSearch.Text & "'"


                    txt.Text = "SELECT distinct a.PostStatus, a.PaidStatus, a.GlStatus, a.InvoiceNumber, a.SalesDate, a.AccountId, a.CustName, a.BillAddress1,    "
                    txt.Text = txt.Text + " a.BillBuilding, a.BillStreet, a.BillCountry, a.BillPostal, a.ValueBase, a.ValueOriginal, a.GstBase, a.GstOriginal, a.AppliedBase,    "
                    txt.Text = txt.Text + " a.AppliedOriginal, a.BalanceBase, a.BalanceOriginal, a.Salesman, a.PoNo, a.OurRef, a.YourRef, a.Terms, a.DiscountAmount,    "
                    txt.Text = txt.Text + " a.GSTAmount, a.NetAmount, a.GlPeriod, a.CompanyGroup, a.ContactType, a.BatchNo, a.Comments, a.AmountWithDiscount,    "
                    txt.Text = txt.Text + " a.TermsDay, a.RecurringInvoice, a.ReceiptBase, a.CreditBase, a.BalanceBase, a.StaffCode, a.CustAddress1, a.CustAddCountry,    "
                    txt.Text = txt.Text + " a.CustAddPostal, a.LedgerCode, a.LedgerName, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno, a.BillSchedule, a.PrintCounter, a.Location   "
                    'txt.Text = txt.Text + " FROM tblsales a, tblSalesDetail b WHERE 1=1   "
                    txt.Text = txt.Text + " FROM tblsales a Left OUTER join  tblSalesDetail b on a.InvoiceNumber=b.InvoiceNumber WHERE 1=1   "

                    txtCondition.Text = " and (a.DocType='ARCN' or a.DocType='ARDN')  "
                    txtCondition.Text = txtCondition.Text & " and a.InvoiceNumber = b.InvoiceNumber  "
                    txtCondition.Text = txtCondition.Text & " AND (a.PostStatus = 'O' OR a.PostStatus = 'P') and "
                    txtCondition.Text = txtCondition.Text & " AND b.SourceInvoice= '" & txtInvoiceNoSearch.Text & "' "

                    If txtDisplayRecordsLocationwise.Text = "Y" Then
                        txtCondition.Text = txtCondition.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') "

                    End If
                    txtOrderBy.Text = " ORDER BY Rcno DESC, CustName "

                    txt.Text = txt.Text + txtCondition.Text + txtOrderBy.Text
                    sqltext = txt.Text


                    txtFrom.Text = Session("cnfrom")

                    btnQuickSearch_Click(sender, e)
                    Session.Remove("invoiceno")
                    Session.Remove("cnfrom")

                    btnQuit.Text = "BACK"
                    ''''' Retrieve rcno for the Invoice 

                    ''''' Retrieve rcno for the Invoice 
                    GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                Else
                    'sqltext = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule, LedgerCode, LedgerName FROM tblsales WHERE (DocType='ARCN' or DocType='ARDN')  AND (PostStatus = 'O' OR PostStatus = 'P') and  GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) ORDER BY Rcno DESC, CustName;"
                    'SQLDSCN.SelectCommand = sqltext
                    If Convert.ToBoolean(dtServiceRecordMasterSetup.Rows(0)("ShowCNOnScreenLoad")) = False Then
                        txt.Text = ""
                        SQLDSCN.SelectCommand = ""
                        GridView1.DataSourceID = "SQLDSCN"
                        GridView1.DataBind()
                    Else
                        txt.Text = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule, LedgerCode, LedgerName, PrintCounter, Location FROM tblsales a WHERE 1=1  "

                        'txt.Text = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule, LedgerCode, LedgerName, PrintCounter, Location   "
                        'txt.Text = txt.Text + " FROM tblsales a Left OUTER join  tblSalesDetail b on a.InvoiceNumber=b.InvoiceNumber WHERE 1=1   "

                        txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  AND (PostStatus = 'O' OR PostStatus = 'P') and  GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) "

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            txtCondition.Text = txtCondition.Text & " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') "

                        End If
                        txtOrderBy.Text = " ORDER BY Rcno DESC, CustName "

                        txt.Text = txt.Text + txtCondition.Text + txtOrderBy.Text
                        sqltext = txt.Text

                        SQLDSCN.SelectCommand = sqltext
                        CalculateTotal()
                    End If

                End If

                SQLDSCN.DataBind()
                GridView1.DataBind()



                If String.IsNullOrEmpty(txt.Text) = True And String.IsNullOrEmpty(txt.Text) = True Then
                    SqlDSMultiPrint.SelectCommand = ""

                    grdViewMultiPrint.DataSourceID = "SqlDSMultiPrint"
                    grdViewMultiPrint.DataBind()

                Else
                    SqlDSMultiPrint.SelectCommand = txt.Text

                    grdViewMultiPrint.DataSourceID = "SqlDSMultiPrint"
                    grdViewMultiPrint.DataBind()

                End If


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

                If txtSearch.Text = "ImportService" Then
                    mdlPopUpClient.Show()
                End If

                If txtIsPopup.Text = "ContractNo" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpContractNo.Show()
                End If

                If txtSearch.Text = "ImportInvoice" Then
                    mdlPopUpClient.Show()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub CalculateTotal()
        Try
            Dim sqlstr As String
            sqlstr = ""

            'sqlstr = "SELECT ifnull(Sum(b.AppliedBase),0) as totalamount FROM tblSales a, tblSalesDetail b where 1=1 " + txtCondition.Text

            sqlstr = "SELECT ifnull(Sum(a.AppliedBase),0) as totalamount FROM tblsales a WHERE 1=1  " + txtCondition.Text

            'strsql = strsql + " FROM tblsales a Left OUTER join  tblSalesDetail b on a.InvoiceNumber=b.InvoiceNumber WHERE 1=1   "

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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "CalculateTotal", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    If e.Row.Cells(9).Text = "COMPANY" Then
        '        e.Row.Cells(9).Text = "CORPORATE"
        '    Else
        '        e.Row.Cells(9).Text = "RESIDENTIAL"
        '    End If
        'End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged


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


            Dim editindex As Integer = GridView1.SelectedIndex

            '
            txtRcno.Text = 0

            If txtFrom.Text = "Corporate" Or txtFrom.Text = "Residential" Then
                'txtRcno.Text = Session("rcnoinv")
                'Session.Remove("rcnoinv")
                ''txtRcno.Text = Session("rcnoinv")
                FindRcno(txtReceiptnoSearch.Text)
                'Session.Remove("customerfrom")
            
            Else
                'rcno = DirectCast(GridView1.Rows(editindex).FindControl("Label1"), Label).Text
                'txtComments.Text = GridView1.SelectedRow.Cells(1).Text.Trim


                If GridView1.Rows.Count > 0 Then
                    txtRcno.Text = GridView1.SelectedRow.Cells(1).Text.Trim
                Else
                    Exit Sub
                End If
                'txtRcno.Text = GridView1.SelectedRow.Cells(1).Text.Trim
            End If
            


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
            updPanelCN.Update()

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "GridView1_SelectedIndexChanged", ex.Message.ToString, "")
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
            TextBoxItemTypeNew = CType(grvBillingDetailsNew.Rows(0).Cells(0).FindControl("txtContractTypeGVB"), TextBox)
            rownew = String.IsNullOrEmpty(TextBoxItemTypeNew.Text)
            If String.IsNullOrEmpty(rownew) = "False" Then
                rownew = "1"
            End If
        End If

        If grvBillingDetails.Rows.Count > 0 Then
            TextBoxItemType = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtContractTypeGV"), DropDownList)
            rowoldstr = TextBoxItemType.Text
            If rowoldstr = "-1" Then
                rowold = 0
            Else
                rowold = 1
            End If
        End If
        'txtComments.Text = rownew & rowold

        'If rownew = "0" And rowold = 0 Then
        '    'grvBillingDetails.ShowHeader = True
        '    grvBillingDetailsNew.Visible = False
        '    grvBillingDetails.Visible = True
        '    FirstGridViewRowBillingDetailsRecs()
        '    grvBillingDetails.ShowHeader = True
        '    updPanelCN.Update()
        'ElseIf rownew = "1" And rowold = 0 Then
        '    'grvBillingDetails.ShowHeader = True
        '    grvBillingDetailsNew.Visible = True
        '    grvBillingDetails.Visible = False
        '    updPanelCN.Update()
        '    'FirstGridViewRowBillingDetailsRecs()
        'ElseIf rownew = "0" And rowold = 1 Then
        '    'grvBillingDetails.ShowHeader = True
        '    grvBillingDetailsNew.Visible = False
        '    FirstGridViewRowBillingDetailsRecs()
        '    grvBillingDetails.Visible = True
        '    updPanelCN.Update()
        '    'FirstGridViewRowBillingDetailsRecs()
        'ElseIf rownew = "1" And rowold = 1 Then
        '    'grvBillingDetails.ShowHeader = True
        '    grvBillingDetailsNew.Visible = True
        '    grvBillingDetails.Visible = True
        '    'FirstGridViewRowBillingDetailsRecs()
        '    updPanelCN.Update()
        'End If

        If rownew = "1" And rowold = 0 Then
            'grvBillingDetails.ShowHeader = False
            grvBillingDetails.Visible = False
            'FirstGridViewRowBillingDetailsRecs()
            'grvBillingDetails.ShowHeader = True

            'UpdatePanel2.Update()
            updPanelCN.Update()
            'updPanelInvoice.Update()
        End If

        If rownew = "0" And rowold = 0 Then
            grvBillingDetails.ShowHeader = True
            grvBillingDetails.Visible = True
            'FirstGridViewRowBillingDetailsRecs()
            grvBillingDetails.ShowHeader = True
            'updPanelInvoice.Update()
        End If

    End Sub

    Private Sub FindRcno(source As String)

        Dim sqlstr As String
        sqlstr = ""

        sqlstr = "SELECT Rcno FROM tblSales where InvoiceNumber ='" & source & "'"

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

        If dt.Rows.Count > 0 Then
            txtRcno.Text = dt.Rows(0)("Rcno").ToString()
        End If
        conn.Close()
        conn.Dispose()
        command.Dispose()
        dt.Dispose()
        dr.Close()
    End Sub

    Private Sub PopulateRecord()
        Try
            grvBillingDetails.ShowHeader = False
            grvBillingDetails.Visible = False

            lblAlertBillingDetails.Text = ""
            lblAlertBillingName.Text = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()


            Dim sql As String
            sql = ""
            'sql = "Select PostStatus, DocType,  GLStatus, InvoiceNumber, SalesDate, GLPeriod, CompanyGroup, AccountId, ContactType, CustName, BillAddress1, BillBuilding, BillStreet, BillCity, BillState,  BillPostal, BillCountry, "
            'sql = sql + " Salesman, PONo, OurRef, YourRef, Terms, TermsDay, BatchNo, Comments, RecurringInvoice, BillSchedule, ContractGroup, "
            'sql = sql + " ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, DiscountAmount, AmountWithDiscount, GSTAmount, NetAmount, InvoiceType, "
            'sql = sql + " Receiptbase, Creditbase, balanceBase, CustAddress1, StaffCode, CustAddBuilding, CustAddStreet, CustAddCity, CustAddState,  CustAddCountry, CustAddPostal, CustAttention, GST, GSTRate, Location,  "
            'sql = sql + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn "
            'sql = sql + " FROM tblsales "

            sql = "SELECT  a.ClaimID, a.ProjectCode, a.ClaimNumber, a.ClaimDate, a.Status, a.AccountId, a.ContactType,  a.CustName,    "
            sql = sql + "   a.GLPeriod, a.CustAttention, a.CompanyGroup,    "
            'strsql = strsql + " a.AppliedOriginal, a.BalanceBase, a.BalanceOriginal, a.Salesman, a.PoNo, a.OurRef, a.YourRef, a.Terms, a.DiscountAmount,    "
            'strsql = strsql + " a.GSTAmount, a.NetAmount, a.GlPeriod, a.CompanyGroup, a.ContactType, a.BatchNo, a.Comments, a.AmountWithDiscount,    "
            'strsql = strsql + " a.TermsDay, a.RecurringInvoice, a.ReceiptBase, a.CreditBase, a.BalanceBase, a.StaffCode, a.CustAddress1, a.CustAddCountry,    "
            'strsql = strsql + " a.CustAddPostal, a.LedgerCode, a.LedgerName, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno, a.BillSchedule, a.PrintCounter, a.Location   "
            sql = sql + " a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno   "
            sql = sql + " FROM tblprogressclaim a WHERE 1=1   "
            sql = sql + " and  a.rcno = " & Convert.ToInt64(txtRcno.Text)

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

             

                'ddlDocType.Text = dt.Rows(0)("DocType").ToString
                If dt.Rows(0)("ClaimNumber").ToString <> "" Then : txtClaimNo.Text = dt.Rows(0)("ClaimNumber").ToString : End If
                If dt.Rows(0)("Status").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("Status").ToString : End If

                If dt.Rows(0)("ProjectCode").ToString <> "" Then : txtProjectCode.Text = dt.Rows(0)("ProjectCode").ToString : End If
                If dt.Rows(0)("ClaimID").ToString <> "" Then : txtCNNo.Text = dt.Rows(0)("ClaimID").ToString : End If
                'If dt.Rows(0)("InvoiceNumber").ToString <> "" Then : txtCNNoSelected.Text = dt.Rows(0)("InvoiceNumber").ToString : End If
                If dt.Rows(0)("ClaimDate").ToString <> "" Then : txtCNDate.Text = Convert.ToDateTime(dt.Rows(0)("ClaimDate")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtReceiptPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then : txtCompanyGroup.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountIdBilling.Text = dt.Rows(0)("AccountId").ToString : End If
                'If dt.Rows(0)("ContactType").ToString <> "" Then : txtAccountType.Text = dt.Rows(0)("ContactType").ToString : End If
                If dt.Rows(0)("ContactType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContactType").ToString : End If
                If dt.Rows(0)("CustName").ToString <> "" Then : txtAccountName.Text = dt.Rows(0)("CustName").ToString : End If
                txtBillingNameEdit.Text = txtAccountName.Text


                'If dt.Rows(0)("CustAddress1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("CustAddress1").ToString : End If
                'If dt.Rows(0)("CustAddStreet").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("CustAddStreet").ToString : End If
                'If dt.Rows(0)("CustAddBuilding").ToString <> "" Then : txtBillBuilding.Text = dt.Rows(0)("CustAddBuilding").ToString : End If

                'If dt.Rows(0)("CustAddCity").ToString <> "" Then : txtBillCity.Text = dt.Rows(0)("CustAddCity").ToString : End If
                'If dt.Rows(0)("CustAddState").ToString <> "" Then : txtBillState.Text = dt.Rows(0)("CustAddState").ToString : End If

                'If dt.Rows(0)("CustAddCountry").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("CustAddCountry").ToString : End If
                'If dt.Rows(0)("CustAddPostal").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("CustAddPostal").ToString : End If
                If dt.Rows(0)("CustAttention").ToString <> "" Then : txtContactPerson.Text = dt.Rows(0)("CustAttention").ToString : End If


               
                'If dt.Rows(0)("OurRef").ToString <> "" Then : txtOurReference.Text = dt.Rows(0)("OurRef").ToString : End If
                'If dt.Rows(0)("YourRef").ToString <> "" Then : txtYourReference.Text = dt.Rows(0)("YourRef").ToString : End If


                'If dt.Rows(0)("BatchNo").ToString <> "" Then : txtBatchNo.Text = dt.Rows(0)("BatchNo").ToString : End If
                'If dt.Rows(0)("Comments").ToString <> "" Then : txtComments.Text = dt.Rows(0)("Comments").ToString : End If
                'If dt.Rows(0)("TermsDay").ToString <> "" Then : txtCreditDays.Text = dt.Rows(0)("TermsDay").ToString : End If
                ''If dt.Rows(0)("RecurringInvoice").ToString = "N" Then : chkRecurringInvoice.Text = dt.Rows(0)("RecurringInvoice").ToString : End If

                'If dt.Rows(0)("ValueBase").ToString <> "" Then : txtCNAmount.Text = dt.Rows(0)("ValueBase").ToString : End If
                ''If dt.Rows(0)("ValueOriginal").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("ValueOriginal").ToString : End If
                'If dt.Rows(0)("GSTBase").ToString <> "" Then : txtCNGSTAmount.Text = dt.Rows(0)("GSTBase").ToString : End If
                ''If dt.Rows(0)("GSTOriginal").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("GSTOriginal").ToString : End If
                'If dt.Rows(0)("AppliedBase").ToString <> "" Then : txtCNNetAmount.Text = dt.Rows(0)("AppliedBase").ToString : End If
                'If dt.Rows(0)("GST").ToString <> "" Then : txtGST.Text = dt.Rows(0)("GST").ToString : End If
                ''If dt.Rows(0)("GST").ToString <> "" Then : txtGST.Text = dt.Rows(0)("GST").ToString : End If
                'If dt.Rows(0)("GSTRate").ToString <> "" Then : txtGST1.Text = dt.Rows(0)("GSTRate").ToString : End If

                'If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If
                'txtRecordCreatedBy.Text = dt.Rows(0)("CreatedBy").ToString

                'txtContactPersonEdit.Text = txtContactPerson.Text
                'txtBillAddressEdit.Text = txtBillAddress.Text
                'txtBillBuildingEdit.Text = txtBillBuilding.Text
                'txtBillStreetEdit.Text = txtBillStreet.Text

                'txtBillCityEdit.Text = txtBillCity.Text
                'txtBillStateEdit.Text = txtBillState.Text

                'txtBillPostalEdit.Text = txtBillPostal.Text
                'txtBillCountryEdit.Text = txtBillCountry.Text
            End If


            ''''''''''''''''''''''''''

            'Dim commandDetailTotal As MySqlCommand = New MySqlCommand

            'commandDetailTotal.CommandType = CommandType.Text
            'commandDetailTotal.CommandText = "SELECT sum(ValueBase) as TotalwithDisc, sum(GSTBase) as TotalGST, sum(AppliedBase) as Totalappliedbase FROM tblSalesDetail  where InvoiceNumber ='" & txtCNNo.Text & "'"
            'commandDetailTotal.Connection = conn

            'Dim drDetailTotal As MySqlDataReader = commandDetailTotal.ExecuteReader()
            'Dim dtDetailTotal As New DataTable
            'dtDetailTotal.Load(drDetailTotal)

            'If dtDetailTotal.Rows.Count > 0 Then
            '    txtTotalWithDiscAmt.Text = dtDetailTotal.Rows(0)("TotalwithDisc").ToString
            '    txtTotalGSTAmt.Text = dtDetailTotal.Rows(0)("TotalGST").ToString
            '    txtTotalWithGST.Text = dtDetailTotal.Rows(0)("Totalappliedbase").ToString

            'End If
            'commandDetailTotal.Dispose()
            'dtDetailTotal.Dispose()

            ''''''''''''''''''''''''
            conn.Close()
            conn.Dispose()


            '
            'updpnlBillingDetails.Update()
        

            'If txtPostStatus.Text = "P" Then
            '    btnEdit.Enabled = False
            '    btnEdit.ForeColor = System.Drawing.Color.Gray
            '    btnCopy.Enabled = True
            '    btnReverse.Enabled = True
            '    btnReverse.ForeColor = System.Drawing.Color.Black

            '    btnDelete.Enabled = False
            '    btnDelete.ForeColor = System.Drawing.Color.Gray

            '    btnPrint.Enabled = True
            '    btnPrint.ForeColor = System.Drawing.Color.Black

            '    btnMultiPrint.Enabled = True
            '    btnMultiPrint.ForeColor = System.Drawing.Color.Black

            '    btnPost.Enabled = False
            '    btnPost.ForeColor = System.Drawing.Color.Gray



            'Else
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
            btnPrint.ForeColor = System.Drawing.Color.Black

            btnMultiPrint.Enabled = True
            btnMultiPrint.ForeColor = System.Drawing.Color.Black

            btnPost.Enabled = True
            btnPost.ForeColor = System.Drawing.Color.Black


            'End If

            btnEditBillingName.Visible = True
            btnEditBillingDetails.Visible = True



            updPnlMsg.Update()

            '          CompletedValue, BalanceValue, PreviousClaimedAmount, CurrentClaimAmount, RetentionPerc, 
            '`RetentionAmount` decimal(18,2) DEFAULT NULL,
            '`TotalCurrentClaim` decimal(18,2) DEFAULT NULL,
            '`BilledAmount` decimal(18,2) DEFAULT NULL,
            '`BalanceNotClaimed

            'SqlDSSalesDetail.SelectCommand = "SELECT ContractType, ClaimID, ContractNo, ContractAmount, CompletedValue, BalanceValue, PreviousClaimedAmount, CurrentClaimAmount, RetentionPerc, RetentionAmount, TotalCurrentClaim, BilledAmount, BalanceNotClaimed from tblsalesdetail where ClaimID= @InvoiceNumber"
            grvBillingDetailsNew.DataSourceID = "SqlDSSalesDetail"
            grvBillingDetailsNew.DataBind()
            IsDetailBlank()

            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()
            'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
            '    AccessControl()
            'End If


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

            'Dim IsLock = FindCNPeriod(txtReceiptPeriod.Text)

            'If IsLock = "Y" Then
            '    'lblAlert.Text = "PERIOD IS LOCKED"
            '    'updPnlMsg.Update()
            '    'txtInvoiceDate.Focus()
            '    btnEdit.Enabled = False
            '    btnEdit.ForeColor = System.Drawing.Color.Gray
            '    'btnSaveInvoice.Enabled = False
            '    'btnSaveInvoice.ForeColor = System.Drawing.Color.Gray
            '    btnPost.Enabled = False
            '    btnPost.ForeColor = System.Drawing.Color.Gray
            '    btnReverse.Enabled = False
            '    btnReverse.ForeColor = System.Drawing.Color.Gray
            '    btnChangeStatus.Enabled = False
            '    btnChangeStatus.ForeColor = System.Drawing.Color.Gray
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PopulateRecord", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Function

    Private Sub GenerateClaimNo()
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
            '    lInvoiceNo = "DD" + lPrefix + "-"
            '    lMonth = Right(lPrefix, 2)
            '    lYear = Left(lPrefix, 4)
            '    lPrefix = "DD" + lYear
            'End If

            lInvoiceNo = "CL" + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)
            lPrefix = "CL" + lYear

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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "GenerateCNNo", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    Public Sub MakeMeNull()
        Try

            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            'txtSearch1Status.Text = "O,P"
            'txtMode.Text = "NEW"
            txtRcno.Text = "0"
            txtLogDocNo.Text = ""


            txtPopUpClient.Text = ""
            'ddlCompanyGrpII.SelectedIndex = 0
            ddlCompanyGrp.SelectedIndex = 0

            grvBillingDetails.ShowHeader = True
            grvBillingDetails.Visible = True

            grvServiceRecDetails.DataSourceID = ""
            grvServiceRecDetails.DataBind()

           

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
                'command.CommandText = "SELECT X0256,  X0256Add, X0256Edit, X0256Delete, X0256Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT X0256,  X0256Add, X0256Edit, X0256Delete, X0256Print, X0256Post, X0256Reverse, x0256EditCompanyName, x0256EditBillingAddress FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("X0256")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("X0256"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("X0256")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X0256Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0256Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("X0256Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("X0256Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0256Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("X0256Edit").ToString()
                                btnEditBillingName.Enabled = dt.Rows(0)("X0256Edit").ToString()
                                btnEditBillingDetails.Enabled = dt.Rows(0)("X0256Edit").ToString()

                                btnEditBillingName.Visible = dt.Rows(0)("x0256EditCompanyName").ToString()
                                btnEditBillingDetails.Visible = dt.Rows(0)("x0256EditBillingAddress").ToString()
                                'btnEditPONo.Enabled = dt.Rows(0)("x0252EditOurRef").ToString()
                                'btnEditSalesman.Enabled = dt.Rows(0)("x0252EditSalesman").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0256Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0256Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("X0256Delete").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0256Print")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0256Print")) = False Then
                                Me.btnPrint.Enabled = dt.Rows(0)("X0256Print").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0256Post")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0256Post")) = False Then
                                Me.btnPost.Enabled = dt.Rows(0)("X0256Post").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0256Reverse")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0256Reverse")) = False Then
                                Me.btnReverse.Enabled = dt.Rows(0)("X0256Reverse").ToString()
                            End If
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
                InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "AccessControl", ex.Message.ToString, "")
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


        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        'btnClient.Visible = False
        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        ddlDocType.Enabled = False

        txtCNNo.Enabled = False
        txtCNDate.Enabled = False
        txtReceiptPeriod.Enabled = False
        txtCompanyGroup.Enabled = False
        txtAccountIdBilling.Enabled = False
        ddlContactType.Enabled = False
        txtAccountName.Enabled = False
        txtContactPerson.Enabled = False
        'txtBankGLCode.Enabled = False

    

        txtOurReference.Enabled = False
        txtYourReference.Enabled = False
      
        txtComments.Enabled = False


        btnShowInvoices.Enabled = False
        btnShowServices.Enabled = False

        btnSave.Enabled = False
        'btnShowRecords.Enabled = False

        grvBillingDetails.Enabled = False
        grvBillingDetailsNew.Enabled = False
        btnDelete.Enabled = False
        btnClient.Visible = False


        txtReceiptnoSearch.Enabled = True
        txtInvoiceNoSearch.Enabled = True
        txtInvoiceDateFromSearch.Enabled = True
        txtInvoiceDateToSearch.Enabled = True
        ddlCompanyGrpSearch.Enabled = True
        ddlContactTypeSearch.Enabled = True
        txtAccountIdSearch.Enabled = True
        txtSearch1Status.Enabled = True
        btnQuickSearch.Enabled = True
        btnQuickReset.Enabled = True
        'txtCommentsSearch.Enabled = True
        txtClientNameSearch.Enabled = True
        btnSearch1Status.Enabled = True
        'btnClientSearch0.Enabled = True
        btnClientSearch.Enabled = True
        'rdbSearchPaidStatus.Enabled = True
        'ddlBankIDSearch.Enabled = True
        'txtChequeNoSearch.Enabled = True


        btnEditBillingName.Visible = False
        btnEditBillingDetails.Visible = False
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

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        btnMultiPrint.Enabled = False
        btnMultiPrint.ForeColor = System.Drawing.Color.Gray

        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray

      
        ddlDocType.Enabled = True
        txtCNNo.Enabled = True
        txtCNDate.Enabled = True
        txtReceiptPeriod.Enabled = True
        txtCompanyGroup.Enabled = True
        'txtAccountId.Enabled = True
        ddlContactType.Enabled = True
        txtAccountIdBilling.Enabled = True
        txtAccountName.Enabled = True
        txtContactPerson.Enabled = True
        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True

        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True

        txtOurReference.Enabled = True
        txtYourReference.Enabled = True
       
        txtComments.Enabled = True

        btnShowInvoices.Enabled = True
        btnShowServices.Enabled = True
        btnSave.Enabled = True

        btnDelete.Enabled = True

        txtReceiptnoSearch.Enabled = False
        txtInvoiceNoSearch.Enabled = False
        txtInvoiceDateFromSearch.Enabled = False
        txtInvoiceDateToSearch.Enabled = False

        If Left(ConfigurationManager.AppSettings("DomainName").ToString(), 9) <> "SINGAPORE" Then
            'txtBillCity.Enabled = True
            'txtBillState.Enabled = True

            txtBillCityEdit.Enabled = True
            txtBillStateEdit.Enabled = True
        End If

        ddlCompanyGrpSearch.Enabled = False
        ddlContactTypeSearch.Enabled = False
        txtAccountIdSearch.Enabled = False
        txtSearch1Status.Enabled = False
        btnQuickSearch.Enabled = False
        btnQuickReset.Enabled = False
        'txtCommentsSearch.Enabled = True
        txtClientNameSearch.Enabled = False
        btnSearch1Status.Enabled = False
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

        btnClient.Visible = True
        btnEditBillingName.Visible = False
        btnEditBillingDetails.Visible = False
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnAdd_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub



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
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PopulateDropDownList", ex.Message.ToString, "")
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

        If txtFrom.Text = "invoice" Then
            'Session.Add("customerfrom", "Residential")
            'Session.Add("armodule", "armodule")
            'Response.Redirect("Person.aspx")

            Session("cnfrom") = "invoice"
            Response.Redirect("Invoice.aspx")
        End If

      
    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

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

        SQLDSCN.SelectCommand = txt.Text
        'GridView1.DataBind()
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
        lblAlert.Text = ""
        Try
            If txtPostStatus.Text = "P" Then
                lblAlert.Text = "Credit Note has already been POSTED.. Cannot be DELETED"
                'Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

                Exit Sub
            End If

            If txtPostStatus.Text = "V" Then
                lblAlert.Text = "Credit Note is VOID.. Cannot be DELETED"
                'Dim message2 As String = "alert('Contract is VOID.. Cannot be DELETED!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message2, True)

                Exit Sub
            End If

            lblAlert.Text = ""
            lblMessage.Text = "ACTION: DELETE RECORD"


            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text

                Dim qry1 As String = "DELETE from tblCN where Rcno= @Rcno "

                command1.CommandText = qry1
                command1.Parameters.Clear()

                command1.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                command1.Connection = conn
                command1.ExecuteNonQuery()



                'Dim command3 As MySqlCommand = New MySqlCommand
                'command3.CommandType = CommandType.Text

                'Dim qry3 As String = "DELETE from tblCNDet where ReceiptNumber= @ReceiptNumber "

                'command3.CommandText = qry3
                'command3.Parameters.Clear()

                'command3.Parameters.AddWithValue("@ReceiptNumber", txtCNNo.Text)
                'command3.Connection = conn
                'command3.ExecuteNonQuery()


                'Start:Loop thru' Credit values

                Dim commandValues As MySqlCommand = New MySqlCommand

                commandValues.CommandType = CommandType.Text
                commandValues.CommandText = "SELECT *  FROM tblcndet where CNNumber ='" & txtCNNo.Text.Trim & "'"
                commandValues.Connection = conn

                Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                Dim dtValues As New DataTable
                dtValues.Load(drValues)

                Dim lTotalReceiptAmt As Decimal
                Dim lInvoiceAmt As Decimal
                'Dim lReceptAmtAdjusted As Decimal

                lTotalReceiptAmt = 0.0
                lInvoiceAmt = 0.0

                lTotalReceiptAmt = dtValues.Rows(0)("CNValue").ToString
                Dim rowindex = 0

                For Each row As DataRow In dtValues.Rows

                    Dim lContractNo As String
                    Dim lServiceRecordNo As String
                    Dim lServiceDate As String

                    If Convert.ToDecimal(row("CNValue")) > 0.0 Then

                        Dim commandUpdateInvoiceValue As MySqlCommand = New MySqlCommand

                        commandUpdateInvoiceValue.CommandType = CommandType.Text
                        'commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblservicebillingdetailitem where rcno = " & row("RcnoServiceBillingItem")
                        commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblSalesDetail where rcno = " & row("RcnoServiceBillingItem")

                        commandUpdateInvoiceValue.Connection = conn

                        Dim drInvoiceValue As MySqlDataReader = commandUpdateInvoiceValue.ExecuteReader()
                        Dim dtInvoiceValue As New DataTable
                        dtInvoiceValue.Load(drInvoiceValue)

                        lContractNo = ""
                        lServiceRecordNo = ""
                        lServiceDate = ""

                        For Each row1 As DataRow In dtInvoiceValue.Rows

                            Dim command3 As MySqlCommand = New MySqlCommand
                            command3.CommandType = CommandType.Text

                            Dim qry3 As String = "DELETE from tblCNDet where rcno= @rcno "

                            command3.CommandText = qry3
                            command3.Parameters.Clear()

                            command3.Parameters.AddWithValue("@rcno", row("Rcno"))
                            command3.Connection = conn
                            command3.ExecuteNonQuery()

                            'Updatetblservicebillingdetailitem(row("RcnoServiceBillingItem"), row("ServiceRecordNo"))



                        Next row1
                    End If
                Next row

                ''''''''''''''''''''''''''''''''''

                conn.Close()
                conn.Dispose()
               

                'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
                SQLDSCN.SelectCommand = txt.Text
                SQLDSCN.DataBind()
                'GridView1.DataSourceID = "SqlDSContract"

                lblMessage.Text = "DELETE: CREDIT NOTE SUCCESSFULLY DELETED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)


                MakeMeNull()
                MakeMeNullBillingDetails()
                'FirstGridViewRowGL()
                GridView1.DataBind()

                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

                updPnlMsg.Update()
                updPnlSearch.Update()
                'updpnlServiceRecs.Update()
                'GridView1.DataBind()
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString

            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnDelete_Click", ex.Message.ToString, "")
            Exit Sub


        End Try
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

        'GridView1.DataBind()

        SQLDSCN.SelectCommand = txt.Text
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

        'btnQuit.Enabled = False
        'btnQuit.ForeColor = System.Drawing.Color.Gray

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
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            txtTotalInvoiceAmount.Text = "0.00"
            txtCondition.Text = ""

            Dim strsql As String

            'strsql = "SELECT distinct a.PostStatus, a.PaidStatus, a.GlStatus, a.InvoiceNumber, a.SalesDate, a.AccountId, a.CustName, a.BillAddress1, a.BillBuilding, a.BillStreet, a.BillCountry, a.BillPostal, a.ValueBase, a.ValueOriginal, a.GstBase, a.GstOriginal, a.AppliedBase, a.AppliedOriginal, a.BalanceBase, a.BalanceOriginal, a.Salesman, a.PoNo, a.OurRef, a.YourRef, a.Terms, a.DiscountAmount, a.GSTAmount, a.NetAmount, a.GlPeriod, a.CompanyGroup, a.ContactType, a.BatchNo, a.Salesman a.Comments, a.AmountWithDiscount, a.TermsDay, a.RecurringInvoice, a.ReceiptBase, a.CreditBase, a.BalanceBase, a.StaffCode, a.CustAddress1, a.CustAddCountry, a.CustAddPostal, a.LedgerCode, a.LedgerName, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno, a.BillSchedule, a.PrintCounter FROM tblsales a WHERE 1=1   "

            strsql = "SELECT  a.ClaimID, a.ProjectCode, a.ClaimNumber, a.ClaimDate, a.Status, a.AccountId, a.ContactType,  a.CustName, a.CompanyGroup,   "
            'strsql = strsql + " a.BillBuilding, a.BillStreet, a.BillCountry, a.BillPostal, a.ValueBase, a.ValueOriginal, a.GstBase, a.GstOriginal, a.AppliedBase,    "
            'strsql = strsql + " a.AppliedOriginal, a.BalanceBase, a.BalanceOriginal, a.Salesman, a.PoNo, a.OurRef, acliam.YourRef, a.Terms, a.DiscountAmount,    "
            'strsql = strsql + " a.GSTAmount, a.NetAmount, a.GlPeriod, a.CompanyGroup, a.ContactType, a.BatchNo, a.Comments, a.AmountWithDiscount,    "
            'strsql = strsql + " a.TermsDay, a.RecurringInvoice, a.ReceiptBase, a.CreditBase, a.BalanceBase, a.StaffCode, a.CustAddress1, a.CustAddCountry,    "
            'strsql = strsql + " a.CustAddPostal, a.LedgerCode, a.LedgerName, a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno, a.BillSchedule, a.PrintCounter, a.Location   "
            strsql = strsql + " a.CreatedBy, a.CreatedOn, a.LastModifiedBy, a.LastModifiedOn, a.rcno   "

            strsql = strsql + " FROM tblprogressclaim a Left OUTER join  tblprogressclaimdetail b on a.ClaimID=b.ClaimID WHERE 1=1   "

            'txtCondition.Text = " and (a.DocType='ARCN' or a.DocType='ARDN')  "
          

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'strsql = strsql + " and Location = '" & txtLocation.Text & "'"
                txtCondition.Text = txtCondition.Text & " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & Session("SecGroupAuthority") & "') "
                If ddlBranch.SelectedIndex > 0 Then
                    If String.IsNullOrEmpty(ddlBranch.Text) = False Then
                        txtCondition.Text = txtCondition.Text & " and a.location = '" & ddlBranch.Text.Trim + "'"
                    End If
                End If
            End If

            'If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
            '    Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
            '    Dim YrStrList As List(Of [String]) = New List(Of String)()

            '    For Each str As String In stringList
            '        str = "'" + str + "'"
            '        YrStrList.Add(str.ToUpper)
            '    Next

            '    Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
            '    txtCondition.Text = txtCondition.Text & " and a.PostStatus in (" + YrStr + ")"

            'End If


            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    'txtCondition.Text = txtCondition.Text + " and Location = '" & txtLocation.Text & "'"
            '    txtCondition.Text = txtCondition.Text & " and a.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            'End If

            'If String.IsNullOrEmpty(txtInvoiceNoSearch.Text) = False Then
            '    txtCondition.Text = txtCondition.Text & " and b.SourceInvoice = '" & txtInvoiceNoSearch.Text.Trim + "'"
            'End If

            'If ddlCompanyGrpSearch.SelectedIndex > 0 Then
            '    If String.IsNullOrEmpty(ddlCompanyGrpSearch.Text) = False Then
            '        txtCondition.Text = txtCondition.Text & " and a.CompanyGroup = '" & ddlCompanyGrpSearch.Text.Trim + "'"
            '    End If
            'End If

            If String.IsNullOrEmpty(txtInvoiceDateFromSearch.Text) = False And txtInvoiceDateFromSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and a.ClaimDate >= '" + Convert.ToDateTime(txtInvoiceDateFromSearch.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateToSearch.Text) = False And txtInvoiceDateToSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and a.ClaimDate <= '" + Convert.ToDateTime(txtInvoiceDateToSearch.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtReceiptnoSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and a.ClaimID like '%" & txtReceiptnoSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and (a.AccountId like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and a.CustName like ""%" & txtClientNameSearch.Text & "%"""
            End If


            txtOrderBy.Text = " order by rcno desc, CustName "
            txt.Text = strsql
            'txtComments.Text = strsql

            'SQLDSCN.SelectCommand = strsql
            'SQLDSCN.DataBind()
            'GridView1.DataBind()

            strsql = strsql + txtCondition.Text + txtOrderBy.Text + " limit " & txtLimit.Text
            txt.Text = strsql

            SQLDSCN.SelectCommand = strsql
            SQLDSCN.DataBind()

            GridView1.DataSourceID = "SQLDSCN"
            GridView1.DataBind()

            'CalculateTotal()

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


            'SqlDSMultiPrint.SelectCommand = ""
            'SqlDSMultiPrint.SelectCommand = txt.Text


            'grdViewMultiPrint.DataSourceID = "SqlDSMultiPrint"
            'grdViewMultiPrint.DataBind()



            'GridSelected = "SQLDSContract"
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnQuickSearch_Click", ex.Message.ToString, "")
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

            'sql = sql + "Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount, A.ServiceDate, A.BilledAmt  FROM tblServiceRecord A  where (Billno is not null and Billno <> '')  "

            'If ddlDocType.Text = "ARCN" Then
            '    sql = sql + "Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount, A.ServiceDate, A.BilledAmt  FROM tblServiceRecord A  where (Billno is not null and Billno <> '')  "
            'Else
            '    sql = sql + "Select A.Rcno,  A.RecordNo, A.AccountID, A.ContractNo, A.CustName,   A.BillNo,  A.CompanyGroup, A.LocationID , A.BillAmount, A.ServiceDate, A.BilledAmt  FROM tblServiceRecord A  where 1 = 1  "
            'End If

            sql = sql + "Select B.Rcno,  B.RecordNo, B.AccountID, B.ContractNo, B.CustName, B.LocationID , B.Address1, B.BillAmount,  A.AgreeValue, A.StartDate, A.EndDate  FROM tblContract A, tblServiceRecord b  where A.ContractNo = B.ContractNo "
            '     sql = sql + " and B.Status ='P'"
            sql = sql + "  and A.BillingFrequency = 'PROGRESS BILLING'"


            If String.IsNullOrEmpty(txtAccountId.Text) = False Then
                sql = sql + " and  A.AccountID = '" & txtAccountId.Text & "'"
            Else
                If String.IsNullOrEmpty(txtClientName.Text) = False Then
                    sql = sql + " and  A.ServiceName like ""%" & txtClientName.Text & "%"""
                End If
            End If

            If ddlCompanyGrp.Text.Trim <> "-1" Then
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

            If String.IsNullOrEmpty(txtInvoiceNo.Text) = False Then
                sql = sql + " order by  A.ContractNo"
            End If

            SqlDSServices.SelectCommand = sql
            grvServiceRecDetails.DataSourceID = "SqlDSServices"
            grvServiceRecDetails.DataBind()

            Label6.Text = "SERVICE BILLING DETAILS : Total Records: " & grvServiceRecDetails.Rows.Count.ToString
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


            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PopulateServiceGrid", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    '''''''''' Start: Service Record




   

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


    'Protected Sub txtGSTAmtGV_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        lblAlert.Text = ""
    '        updPnlMsg.Update()

    '        Dim txt1 As TextBox = DirectCast(sender, TextBox)
    '        xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

    '        Dim lblid1 As TextBox = CType(txt1.FindControl("txtGSTAmtGV"), TextBox)
    '        Dim lblid2 As TextBox = CType(txt1.FindControl("txtPriceWithDiscGV"), TextBox)
    '        Dim lblid3 As TextBox = CType(txt1.FindControl("txtTotalPriceWithGSTGV"), TextBox)

    '        lblid3.Text = Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) + Convert.ToDecimal(lblid2.Text)).ToString("N2")

    '        'CalculateTotalGSTNetPrice()
    '        'updpnlBillingDetails.Update()


    '        updPanelCN.Update()

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("CN - " + Session("UserID"), "txtGSTAmtGV_TextChanged", ex.Message.ToString, "")
    '    End Try
    'End Sub


    'Protected Sub txtGSTAmtGVB_TextChanged(sender As Object, e As EventArgs)
    '    Try
    '        lblAlert.Text = ""
    '        updPnlMsg.Update()

    '        Dim txt1 As TextBox = DirectCast(sender, TextBox)
    '        xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

    '        Dim lblid1 As TextBox = CType(txt1.FindControl("txtGSTAmtGVB"), TextBox)
    '        Dim lblid2 As TextBox = CType(txt1.FindControl("txtPriceWithDiscGVB"), TextBox)
    '        Dim lblid3 As TextBox = CType(txt1.FindControl("txtTotalPriceWithGSTGVB"), TextBox)

    '        lblid3.Text = Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) + Convert.ToDecimal(lblid2.Text)).ToString("N2")

    '        CalculateTotalGSTNetPrice()
    '        'updpnlBillingDetails.Update()
    '        updPanelCN.Update()

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("CN - " + Session("UserID"), "txtGSTAmtGVB_TextChanged", ex.Message.ToString, "")
    '    End Try
    'End Sub

    Protected Sub txtRetentionAmountGV_TextChanged(sender As Object, e As EventArgs)
        Try
            lblAlert.Text = ""
            updPnlMsg.Update()

            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

            Dim lblid1 As TextBox = CType(txt1.FindControl("txtRetentionAmountGV"), TextBox)
            Dim lblid2 As TextBox = CType(txt1.FindControl("txtCurrentClaimedAmountGV"), TextBox)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtTotalCurrentClaimGV"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtBalanceNotClaimedGV"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtBalanceValueGV"), TextBox)

            lblid3.Text = Convert.ToDecimal(Convert.ToDecimal(lblid2.Text) - Convert.ToDecimal(lblid1.Text)).ToString("N2")
            lblid4.Text = Convert.ToDecimal(Convert.ToDecimal(lblid5.Text) - Convert.ToDecimal(lblid3.Text)).ToString("N2")

            'CalculateTotalGSTNetPrice()
            'updpnlBillingDetails.Update()


            updPanelCN.Update()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CN - " + Session("UserID"), "txtGSTAmtGV_TextChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Private Sub CalculateTotalGSTNetPrice()
        Try


            Dim TotalGSTAmt As Decimal = 0
            Dim TotalAmtWithGST As Decimal = 0
            Dim GSTGVB As Decimal = 0.0
            Dim GSTGV As Decimal = 0.0

            'txtCNGSTAmount.Text = "0.00"
            'txtCNNetAmount.Text = "0.00"
            'End If

            ''''''''''''''''''''''''''''''''start Modification'''''''''''''''''''''''''''''''''''''

            'SetRowDataBillingDetailsRecs()
            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim GSTable As Decimal = 0.0

            If (table.Rows.Count > 0) Then

                For i As Integer = (table.Rows.Count) - 1 To 0 Step -1
                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtItemTypeGV"), DropDownList)

                    If TextBoxItemType.SelectedValue <> "-1" Then
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)


                        If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                            TextBoxGSTAmt.Text = "0.00"
                        End If

                        If String.IsNullOrEmpty(TextBoxTotalPriceWithGST.Text) = True Then
                            TextBoxTotalPriceWithGST.Text = "0.00"
                        End If

                        GSTGV = GSTGV + Convert.ToDecimal(TextBoxGSTAmt.Text)


                    End If
                Next i
            End If



            '' start of GVB
            Dim gvbRecords, j As Long
            gvbRecords = 0

            If txtMode.Text = "EDIT" Then

                gvbRecords = grvBillingDetailsNew.Rows.Count()

                For j = gvbRecords - 1 To 0 Step -1


                    Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtItemTypeGVB"), TextBox)
                    Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtOtherCodeGVB"), TextBox)


                    If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then
                        Dim TextBoxGSTAmtGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtGSTAmtGVB"), TextBox)
                        Dim TextBoxTotalPriceWithGSTGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceWithGSTGVB"), TextBox)


                        If String.IsNullOrEmpty(TextBoxGSTAmtGVB.Text) = True Then
                            TextBoxGSTAmtGVB.Text = "0.00"
                        End If

                        If String.IsNullOrEmpty(TextBoxTotalPriceWithGSTGVB.Text) = True Then
                            TextBoxTotalPriceWithGSTGVB.Text = "0.00"
                        End If


                        GSTGVB = GSTGVB + Convert.ToDecimal(TextBoxGSTAmtGVB.Text)

                    End If
                Next
            End If

            'txtCNGSTAmount.Text = Convert.ToDecimal(GSTGVB + GSTGV).ToString("N2")
            txtTotalGSTAmt.Text = Convert.ToDecimal(GSTGVB + GSTGV).ToString("N2")
            ''''''''''''''''''''''''''

            'Dim GSTDiff As Decimal
            'GSTDiff = 0.0

            'Dim GSTCalc As Decimal
            'GSTCalc = 0.0
            'GSTCalc = Convert.ToDecimal((GSTable + GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
            'GSTDiff = GSTCalc - Convert.ToDecimal(txtGSTAmount.Text)
            ''GSTDiff = ((GSTable + GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01) - Convert.ToDecimal(txtGSTAmount.Text)

            'If GSTDiff <> 0.0 Then

            '    txtGSTAmount.Text = Convert.ToDecimal(Convert.ToDecimal(txtGSTAmount.Text) + GSTDiff.ToString("N2")).ToString("N2")

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

            ''''''''''''''''''''''''''''

            'txtCNNetAmount.Text = Convert.ToDecimal(txtCNAmount.Text) + Convert.ToDecimal(txtCNGSTAmount.Text)
            txtTotalWithGST.Text = Convert.ToDecimal(txtTotalWithDiscAmt.Text) + Convert.ToDecimal(txtTotalGSTAmt.Text)
            UpdatePanel2.Update()
            updPanelSave.Update()
            table.Dispose()
            'updpnlServiceRecs.Update()

            'lbltotalservices.Text = Convert.ToInt32(totalrecords)
            'UpdatePanel3.Update()

            'updPanelInvoice.Update()
            'updpnlBillingDetails.Update()
            'txtTotal.Text = TotalAmt.ToString
            'txtTotalWithGST.Text = TotalAmtWithGST.ToString

            'txtTotalDiscAmt.Text = TotalDiscAmt.ToString
            'txtTotalGSTAmt.Text = TotalGSTAmt.ToString

            'txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

            'DisplayGLGrid()
            'UpdatePanel3.Update()
            'updPanelSave.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr

            InsertIntoTblWebEventLog("CN - " + Session("UserID"), "FUNCTION CalculateTotalPrice", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
        End Try
    End Sub


    Protected Sub txtQtyGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        If ddlDocType.Text = "ARCN" Then
            If btn1.Text > 0 Then
                btn1.Text = btn1.Text * (-1)
            End If
        End If

    End Sub

    Protected Sub txtPricePerUOMGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)


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
        ddlDocType.SelectedIndex = 0
        txtCNNo.Text = ""
        'txtCNNoSelected.Text = ""
        txtPostStatus.Text = ""
        ddlContactType.SelectedIndex = 0
        txtBillAddress.Text = ""
        txtBillBuilding.Text = ""
        txtBillStreet.Text = ""

        txtBillCity.Text = ""
        txtBillState.Text = ""

        txtContactPerson.Text = ""

        txtBillCountry.Text = ""
        txtBillPostal.Text = ""
      
        txtComments.Text = ""

        'txtInvoiceDate.Text = ""
        txtAccountIdBilling.Text = ""
        txtAccountName.Text = ""
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
      
        txtLocation.Text = ""

        
    End Sub

    Protected Sub btnAddDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        'If TotDetailRecords > 0 Then
        '    AddNewRowWithDetailRecBillingDetailsRecs()
        'Else
        'AddNewRowBillingDetailsRecs()
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
            dt21.Dispose()
            dr21.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PopulateGLCode", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub InsertIntoTblProgressClaim()

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

                qry = "INSERT INTO tblProgressClaim "
                qry = qry + " (ClaimID,ProjectCode,ClaimNumber, DocType, DocPrefix, CalendarPeriod, GlPeriod, Status, GlStatus, PaidStatus, ContactType, "
                qry = qry + " CustAttention, CustName, AccountId, CompanyGroup,    "
                qry = qry + " ClaimDate, OurRef,YourRef,  "
                qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                qry = qry + " (@ClaimID, @ProjectCode, @ClaimNumber, @DocType, @DocPrefix, @CalendarPeriod, @GlPeriod, @Status, @GlStatus, @PaidStatus, @ContactType, "
                qry = qry + " @CustAttention, @ClientName, @AccountId, @CompanyGroup, "
                qry = qry + " @ClaimDate, @ourRef, @YourRef, "
                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                'command.Parameters.AddWithValue("@CliamID", ddlDocType.Text)
                command.Parameters.AddWithValue("@ProjectCode", txtProjectCode.Text.Trim)
                command.Parameters.AddWithValue("@DocType", "PRCL")
                If String.IsNullOrEmpty(txtClaimNo.Text) = True Then
                    command.Parameters.AddWithValue("@ClaimNumber", 0)
                Else
                    command.Parameters.AddWithValue("@ClaimNumber", txtClaimNo.Text)
                End If

                command.Parameters.AddWithValue("@DocPrefix", "")
                command.Parameters.AddWithValue("@CalendarPeriod", txtReceiptPeriod.Text)
                command.Parameters.AddWithValue("@GlPeriod", txtReceiptPeriod.Text)
                command.Parameters.AddWithValue("@Status", "OPEN")
                command.Parameters.AddWithValue("@GlStatus", "O")
                command.Parameters.AddWithValue("@PaidStatus", "O")

                command.Parameters.AddWithValue("@ContactType", ddlContactType.Text)

                command.Parameters.AddWithValue("@CustAttention", txtContactPerson.Text)
                command.Parameters.AddWithValue("@ClientName", txtAccountName.Text.ToUpper)
                command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)


                'command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                'command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                'command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)

                'command.Parameters.AddWithValue("@BillCity", txtBillCity.Text.ToUpper)
                'command.Parameters.AddWithValue("@BillState", txtBillState.Text.ToUpper)

                'command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)
                'command.Parameters.AddWithValue("@BillCountry", txtBillCountry.Text)
                If txtCNDate.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@ClaimDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@ClaimDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                End If

                command.Parameters.AddWithValue("@OurRef", "")
                command.Parameters.AddWithValue("@YourRef", "")
                command.Parameters.AddWithValue("@Comments", "")

           
                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                GenerateClaimNo()

                If String.IsNullOrEmpty(txtCNNo.Text.Trim) = True Then
                    lblAlert.Text = "CLAIM ID CANNOT BE BLANK"
                    updPnlMsg.Update()
                    Exit Sub
                End If

                command.Parameters.AddWithValue("@ClaimID", txtCNNo.Text)

                command.Connection = conn
                command.ExecuteNonQuery()

                Dim sqlLastId As String
                sqlLastId = "SELECT last_insert_id() from tblProgressClaim"

                Dim commandRcno As MySqlCommand = New MySqlCommand
                commandRcno.CommandType = CommandType.Text
                commandRcno.CommandText = sqlLastId
                commandRcno.Parameters.Clear()
                commandRcno.Connection = conn
                txtRcno.Text = commandRcno.ExecuteScalar()
                'txtCNNoSelected.Text = txtCNNo.Text
            Else

                'qry = "Update tblSales set InvoiceNumber = @InvoiceNumber, CustAttention = @CustAttention, CustName =@ClientName, AccountId =@AccountId, BillAddress1 =@BillAddress1, BillBuilding =@BillBuilding,   "
                'qry = qry + " BillStreet = @BillStreet, BillPostal= @BillPostal, BillCountry= @BillCountry, ServiceRecordNo = @ServiceRecordNo,  SalesDate =@SalesDate,   "
                'qry = qry + " OurRef = @ourRef, YourRef =@YourRef, PONo = @PONo,   StaffCode = @StaffCode,  Terms =@Terms, TermsDay =@TermsDay, "
                'qry = qry + " ValueBase = @ValueBase, ValueOriginal =@ValueOriginal, GSTBase=@GSTBase, GSTOriginal=@GSTOriginal, AppliedBase = @AppliedBase, AppliedOriginal=@AppliedOriginal, BalanceBase=@BalanceBase, BalanceOriginal=@BalanceOriginal, "
                'qry = qry + " DiscountAmount = @DiscountAmount, GSTAmount = @GSTAmount, NetAmount = @NetAmount, Comments = @Comments, ContactType = @ContactType, CompanyGroup = @CompanyGroup,  GLPeriod = @GLPeriod, AmountWithDiscount = @AmountWithDiscount, RecurringInvoice = @RecurringInvoice, "
                'qry = qry + " CustAddress1 = @CustAddress1,   CustAddStreet = @CustAddStreet, CustAddBuilding=@CustAddBuilding,  CustAddCity = @CustAddCity, CustAddState=@CustAddState,  CustAddCountry = @CustAddCountry, CustAddPostal=@CustAddPostal, GST=@GST, GSTrate=@GSTrate, ContractGroup =@ContractGroup, "
                'qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                'qry = qry + " where Rcno = @Rcno;"

                'command.CommandText = qry
                'command.Parameters.Clear()

                'command.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                'command.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                'command.Parameters.AddWithValue("@CustAttention", txtContactPerson.Text)
                'command.Parameters.AddWithValue("@ClientName", txtAccountName.Text.ToUpper)
                'command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                'command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                'command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                'command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)

                'command.Parameters.AddWithValue("@BillCity", txtBillCity.Text.ToUpper)
                'command.Parameters.AddWithValue("@BillState", txtBillState.Text.ToUpper)

                'command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)
                'command.Parameters.AddWithValue("@BillCountry", txtBillCountry.Text)
                'command.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)

                'If txtCNDate.Text.Trim = "" Then
                '    command.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                'Else
                '    command.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                'End If

                'command.Parameters.AddWithValue("@OurRef", txtOurReference.Text.ToUpper)
                'command.Parameters.AddWithValue("@YourRef", txtYourReference.Text.ToUpper)
                'command.Parameters.AddWithValue("@PoNo", txtPONo.Text.ToUpper)
                'If ddlSalesmanBilling.SelectedIndex = 0 Then
                '    command.Parameters.AddWithValue("@StaffCode", "")
                'Else
                '    command.Parameters.AddWithValue("@StaffCode", ddlSalesmanBilling.Text)
                'End If


                'If ddlCreditTerms.Text = "-1" Then
                '    command.Parameters.AddWithValue("@Terms", "")
                'Else
                '    command.Parameters.AddWithValue("@Terms", ddlCreditTerms.Text)
                'End If

                ''If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                ''    command.Parameters.AddWithValue("@TermsDay", txtCreditDays.Text)
                ''Else
                'command.Parameters.AddWithValue("@TermsDay", txtCreditDays.Text)

                ''command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                ''command.Parameters.AddWithValue("@RcnoServiceRecord", 0)

                'command.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(txtCNAmount.Text))
                'command.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(txtCNAmount.Text))
                'command.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(txtCNGSTAmount.Text))
                'command.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(txtCNGSTAmount.Text))
                'command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtCNNetAmount.Text))
                'command.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(txtCNNetAmount.Text))

                'command.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(txtCNNetAmount.Text))
                'command.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(txtCNNetAmount.Text))

                ''command.Parameters.AddWithValue("@BalanceBase", 0.0)
                ''command.Parameters.AddWithValue("@BalanceOriginal", 0.0)
                'command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                'command.Parameters.AddWithValue("@AmountWithDiscount", 0.0)
                'command.Parameters.AddWithValue("@GSTAmount", 0.0)
                'command.Parameters.AddWithValue("@NetAmount", 0.0)

                'command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper)
                'command.Parameters.AddWithValue("@ContactType", ddlContactType.Text)
                'command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                'command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)
                ''command.Parameters.AddWithValue("@BatchNo", "")

                'command.Parameters.AddWithValue("@CustAddress1", txtBillAddress.Text)
                'command.Parameters.AddWithValue("@CustAddBuilding", txtBillBuilding.Text)
                'command.Parameters.AddWithValue("@CustAddStreet", txtBillStreet.Text)

                'command.Parameters.AddWithValue("@CustAddCity", txtBillCity.Text.ToUpper)
                'command.Parameters.AddWithValue("@CustAddState", txtBillState.Text.ToUpper)

                'command.Parameters.AddWithValue("@CustAddPostal", txtBillPostal.Text)
                'command.Parameters.AddWithValue("@CustAddCountry", txtBillCountry.Text)
                ''End If
                ''If chkRecurringInvoice.Checked = True Then
                ''    command.Parameters.AddWithValue("@RecurringInvoice", "Y")
                ''Else
                'command.Parameters.AddWithValue("@RecurringInvoice", "")
                'command.Parameters.AddWithValue("@GST", txtGST.Text)

                ''If txtGST.Text = "SR" Then
                ''    command.Parameters.AddWithValue("@GSTRate", txtTaxRatePct.Text)
                ''Else
                ''    command.Parameters.AddWithValue("@GSTRate", 0.0)
                ''End If
                ''End If

                ''If rbtInvoiceType.SelectedIndex = 0 Then
                ''    command.Parameters.AddWithValue("@InvoiceType", "M")
                ''Else
                ''command.Parameters.AddWithValue("@InvoiceType", "")
                ''End If

                'command.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtGST1.Text))
                'command.Parameters.AddWithValue("@ContractGroup", ddlContractGroupBilling.Text)
                'command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                'command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                'command.Connection = conn
                'command.ExecuteNonQuery()
            End If


            ''''''''tblSales

            'Dim commandtblSalesDetail As MySqlCommand = New MySqlCommand

            'commandtblSalesDetail.CommandType = CommandType.Text
            ''Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
            'Dim qrycommandtblSalesDetail As String = "DELETE from tblSalesdetail where invoiceNumber = '" & txtCNNo.Text & "'"

            'commandtblSalesDetail.CommandText = qrycommandtblSalesDetail
            'commandtblSalesDetail.Parameters.Clear()
            'commandtblSalesDetail.Connection = conn
            'commandtblSalesDetail.ExecuteNonQuery()

            ''txtCNNoSelected.Text = ""
            'updPanelCN.Update()


            '' GVB----------------------------

            'Dim gvbRecords, i As Long
            'gvbRecords = grvBillingDetailsNew.Rows.Count()

            'For i = 0 To gvbRecords - 1
            '    ''Dim lblItemCodeGVB As DropDownList = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemCodeGVB"), DropDownList)
            '    'Dim contractNoGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtContractNoGVB"), TextBox)
            '    'txtComments.Text = contractNoGVB.Text


            '    Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemTypeGVB"), TextBox)
            '    Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtOtherCodeGVB"), TextBox)
            '    Dim lblid14 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtRcnoInvoiceGVB"), TextBox)

            '    If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then
            '        Dim lblidItemCodeGVB As DropDownList = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemCodeGVB"), DropDownList)
            '        Dim lblidServiceRecordGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceRecordGVB"), TextBox)
            '        Dim lblItemDescriptionGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtItemDescriptionGVB"), TextBox)
            '        Dim lblDescriptionGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDescriptionGVB"), TextBox)
            '        Dim lblGLDescriptionGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGLDescriptionGVB"), TextBox)

            '        'Dim lblid6 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtBillingFrequencyGV"), TextBox)
            '        Dim lblidRcnoServiceRecordGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtRcnoServiceRecordGVB"), TextBox)

            '        If String.IsNullOrEmpty(lblidRcnoServiceRecordGVB.Text) = True Then
            '            lblidRcnoServiceRecordGVB.Text = 0
            '        End If

            '        'Dim lblid8 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDeptGV"), TextBox)
            '        Dim lblidServiceStatusGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceStatusGVB"), TextBox)

            '        Dim lblidContractNoGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtContractNoGVB"), TextBox)
            '        'Dim lblid12 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceAddressGV"), TextBox)
            '        'Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)

            '        Dim lblidUnitMsGVB As DropDownList = CType(grvBillingDetailsNew.Rows(i).FindControl("txtUOMGVB"), DropDownList)
            '        Dim lblidQtyGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtQtyGVB"), TextBox)
            '        Dim lblidPricePerUOMGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtPricePerUOMGVB"), TextBox)
            '        Dim lblidTotalPriceGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtTotalPriceGVB"), TextBox)
            '        Dim lblidDiscPercGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDiscPercGVB"), TextBox)
            '        'Dim lblidDiscAmountGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtDiscAmountGVB"), TextBox)
            '        Dim lblidPriceWithDiscGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtPriceWithDiscGVB"), TextBox)

            '        Dim lblidGSTGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtTaxTypeGVB"), TextBox)
            '        Dim lblidGSTRate As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGSTPercGVB"), TextBox)
            '        Dim lblidGSTBaseGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGSTAmtGVB"), TextBox)
            '        'Dim lblidGSTAmt As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGSTAmtGV"), TextBox)
            '        Dim lblidNetAmountGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtTotalPriceWithGSTGVB"), TextBox)

            '        'Dim lblidServiceStatus As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceStatusGV"), TextBox)
            '        Dim lblidLocationIdGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtLocationIdGVB"), TextBox)
            '        Dim lblidServiceLocationGroupGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceLocationGroupGVB"), TextBox)
            '        'Dim lblid14 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtBillingFrequencyGV"), TextBox)
            '        'Dim lblid14 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceByGV"), TextBox)
            '        'Dim lblid14 As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtRcnoInvoiceGV"), TextBox)
            '        'Dim lblid1GLDescription As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtGLDescriptionGV"), TextBox)


            '        Dim lblidBillingFrequencyGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtBillingFrequencyGVB"), TextBox)
            '        Dim lblidServiceByGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceByGVB"), TextBox)
            '        Dim lblidServiceDateGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtServiceDateGVB"), TextBox)
            '        Dim lblSourceInvoiceGVB As TextBox = CType(grvBillingDetailsNew.Rows(i).FindControl("txtInvoiceNoGVB"), TextBox)

            '        Dim lContractGroup As String
            '        lContractGroup = ""

            '        If String.IsNullOrEmpty(lblidContractNoGVB.Text) = False Then
            '            Dim commandCG As MySqlCommand = New MySqlCommand
            '            commandCG.CommandType = CommandType.Text

            '            commandCG.CommandText = "SELECT ContractGroup FROM tblContract where  ContractNo = '" & lblidContractNoGVB.Text & "'"
            '            'command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
            '            commandCG.Connection = conn

            '            Dim drCG As MySqlDataReader = commandCG.ExecuteReader()
            '            Dim dtCG As New DataTable
            '            dtCG.Load(drCG)


            '            lContractGroup = dtCG.Rows(0)("ContractGroup").ToString
            '        End If

            '        Dim commandSalesDetail As MySqlCommand = New MySqlCommand

            '        commandSalesDetail.CommandType = CommandType.Text


            '        'Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
            '        'qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
            '        'qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
            '        'qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, ServiceBy, ServiceDate, BillingFrequency, InvoiceTYpe, ItemDescription, ContractGroup, SourceInvoice,  "
            '        'qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '        'qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
            '        'qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
            '        'qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
            '        'qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @ServiceBy, @ServiceDate, @BillingFrequency, @InvoiceType, @ItemDescription, @ContractGroup, @SourceInvoice, "
            '        'qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '        Dim qryDetail As String = "UPDATE tblSalesDetail SET InvoiceNumber = @InvoiceNumber, Sequence=@Sequence, SubCode=@SubCode, LedgerCode=@LedgerCode, LedgerName=@LedgerName, SubLedgerCode=@SubLedgerCode, SONUMBER=@SONUMBER, RefType=@RefType, Gst=@Gst, "
            '        qryDetail = qryDetail + " GstRate=@GstRate, ExchangeRate=@ExchangeRate, Currency=@Currency, Quantity=@Quantity, UnitMs=@UnitMs, UnitOriginal=@UnitOriginal, UnitBase=@UnitBase,  DiscP=@DiscP, TaxBase=@TaxBase, GstOriginal=@GstOriginal,"
            '        qryDetail = qryDetail + " GstBase=@GstBase, ValueOriginal=@ValueOriginal, ValueBase=@ValueBase, AppliedOriginal=@AppliedOriginal, AppliedBase=@AppliedBase, Description=@Description, Comments=@Comments, GroupId=@GroupId, "
            '        qryDetail = qryDetail + " DetailID=@DetailID, GrpDetName=@GrpDetName, SoCode=@SoCode, ItemCode=@ItemCode, AvgCost=@AvgCost, CostValue=@CostValue, COSTCODE=@COSTCODE, ServiceStatus=@ServiceStatus, DiscAmount=@DiscAmount, TotalPrice=@TotalPrice, LocationId=@LocationId, ServiceLocationGroup=@ServiceLocationGroup, RcnoServiceRecord=@RcnoServiceRecord, ServiceBy=@ServiceBy, ServiceDate=@ServiceDate, BillingFrequency=@BillingFrequency, InvoiceTYpe=@InvoiceTYpe, ItemDescription=@ItemDescription, ContractGroup=@ContractGroup, SourceInvoice=@SourceInvoice,  "
            '        qryDetail = qryDetail + " LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn "
            '        qryDetail = qryDetail + " where Rcno = " & lblid14.Text
            '        'qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
            '        'qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
            '        'qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
            '        'qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @ServiceBy, @ServiceDate, @BillingFrequency, @InvoiceType, @ItemDescription, @ContractGroup, @SourceInvoice, "
            '        'qryDetail = qryDetail + " @LastModifiedBy, @LastModifiedOn "

            '        commandSalesDetail.CommandText = qryDetail
            '        commandSalesDetail.Parameters.Clear()

            '        commandSalesDetail.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@Sequence", 0)
            '        commandSalesDetail.Parameters.AddWithValue("@SubCode", lblidItemTypeGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@LedgerCode", lblidOtherCodeGVB.Text)
            '        'commandSalesDetail.Parameters.AddWithValue("@LedgerName", lLedgerName.ToUpper)
            '        commandSalesDetail.Parameters.AddWithValue("@LedgerName", lblGLDescriptionGVB.Text.ToUpper)

            '        commandSalesDetail.Parameters.AddWithValue("@SubLedgerCode", "")
            '        commandSalesDetail.Parameters.AddWithValue("@SONUMBER", "")

            '        'If (lblidServiceRecordGVB.SelectedIndex) = 0 Then
            '        '    'If String.IsNullOrEmpty(lblidServiceRecord.Text) = "--SELECT--" Then
            '        '    commandSalesDetail.Parameters.AddWithValue("@RefType", "")
            '        'Else
            '        commandSalesDetail.Parameters.AddWithValue("@RefType", lblidServiceRecordGVB.Text.Trim)
            '        'End If

            '        commandSalesDetail.Parameters.AddWithValue("@Gst", lblidGSTGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(lblidGSTRate.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
            '        commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
            '        commandSalesDetail.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(lblidQtyGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@UnitMs", lblidUnitMsGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(lblidPricePerUOMGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(lblidPricePerUOMGVB.Text))
            '        'commandSalesDetail.Parameters.AddWithValue("@DiscP", lblidDiscPercGVB.Text)

            '        'If String.IsNullOrEmpty(lblidDiscPercGVB.Text) = True Then
            '        commandSalesDetail.Parameters.AddWithValue("@DiscP", 0.0)
            '        'Else
            '        '    commandSalesDetail.Parameters.AddWithValue("@DiscP", lblidDiscPercGVB.Text)
            '        'End If

            '        commandSalesDetail.Parameters.AddWithValue("@TaxBase", Convert.ToDecimal(lblidGSTBaseGVB.Text))

            '        commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(lblidTotalPriceGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(lblidTotalPriceGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@GstOriginal", Convert.ToDecimal(lblidGSTBaseGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@GstBase", Convert.ToDecimal(lblidGSTBaseGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(lblidNetAmountGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(lblidNetAmountGVB.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(lblidTotalPriceGVB.Text))

            '        commandSalesDetail.Parameters.AddWithValue("@ItemCode", lblItemDescriptionGVB.Text.ToUpper.Trim)
            '        commandSalesDetail.Parameters.AddWithValue("@ItemDescription", lblidItemCodeGVB.Text.ToUpper)
            '        commandSalesDetail.Parameters.AddWithValue("@Description", lblDescriptionGVB.Text.ToUpper)
            '        commandSalesDetail.Parameters.AddWithValue("@Comments", "")
            '        commandSalesDetail.Parameters.AddWithValue("@GroupId", "")
            '        commandSalesDetail.Parameters.AddWithValue("@DetailID", "")
            '        commandSalesDetail.Parameters.AddWithValue("@GrpDetName", "")

            '        commandSalesDetail.Parameters.AddWithValue("@SoCode", 0.0)
            '        commandSalesDetail.Parameters.AddWithValue("@AvgCost", 0.0)
            '        commandSalesDetail.Parameters.AddWithValue("@CostValue", 0.0)
            '        commandSalesDetail.Parameters.AddWithValue("@COSTCODE", lblidContractNoGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@ServiceStatus", lblidServiceStatusGVB.Text)

            '        commandSalesDetail.Parameters.AddWithValue("@DiscAmount", 0.0)
            '        commandSalesDetail.Parameters.AddWithValue("@PriceWithDisc", lblidPriceWithDiscGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@LocationId", lblidLocationIdGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@ServiceLocationGroup", lblidServiceLocationGroupGVB.Text)

            '        commandSalesDetail.Parameters.AddWithValue("@RcnoServiceRecord", lblidRcnoServiceRecordGVB.Text)

            '        commandSalesDetail.Parameters.AddWithValue("@ServiceBy", lblidServiceByGVB.Text)

            '        If String.IsNullOrEmpty(lblidServiceDateGVB.Text.Trim) = True Then
            '            commandSalesDetail.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '        Else
            '            commandSalesDetail.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblidServiceDateGVB.Text).ToString("yyyy-MM-dd"))
            '        End If

            '        'commandSalesDetail.Parameters.AddWithValue("@ServiceDate", lblidServiceDate.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@BillingFrequency", lblidBillingFrequencyGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@InvoiceType", lblidItemTypeGVB.Text)
            '        commandSalesDetail.Parameters.AddWithValue("@ContractGroup", lContractGroup)
            '        commandSalesDetail.Parameters.AddWithValue("@SourceInvoice", lblSourceInvoiceGVB.Text.Trim)

            '        'commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '        'commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '        commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '        commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            '        commandSalesDetail.Connection = conn
            '        commandSalesDetail.ExecuteNonQuery()
            '    End If


            'Next

            ''Exit Sub

            ''GVB -----------------------------

            ' ''''''''''''''''''
            SetRowDataBillingDetailsRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''
                PopulateGLCodes()
                ''''''''''''''''''''''''''''''''''''''''''''''''
                For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1

                    Dim lblContractType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractTypeGV"), DropDownList)
                    Dim lblContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)

                    Dim lblContractAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractAmountGV"), TextBox)
                    Dim lblCompletedValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtCompletedValueGV"), TextBox)
                    Dim lblBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtBalanceValueGV"), TextBox)
                    Dim lblPreviousClaimAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                    Dim lblCurrentClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtCurrentClaimedAmountGV"), TextBox)
                    Dim lblRetentionPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtRetentionPercGV"), TextBox)

                    Dim lblRetentionAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtRetentionAmountGV"), TextBox)
                    Dim lblTotalCurrentClaim As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalCurrentClaimGV"), TextBox)
                    Dim lblBilledAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtBilledAmountGV"), TextBox)
                    Dim lblBalanceNotClaimed As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtBalanceNotClaimedGV"), TextBox)
                 

                    Dim lContractGroup As String
                    lContractGroup = ""


                    Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                    commandSalesDetail.CommandType = CommandType.Text


                    Dim qryDetail As String = "INSERT INTO tblprogressclaimdetail(ClaimID, ContractNo, Currency, Quantity, UnitMs, ExchangeRate, "
                    qryDetail = qryDetail + " ContractAmount, CompletedValue, BalanceValue, PreviousClaimedAmount, CurrentClaimAmount,"
                    qryDetail = qryDetail + " RetentionPerc, RetentionAmount, TotalCurrentClaim, BilledAmount, BalanceNotClaimed, "
                    qryDetail = qryDetail + " LedgerCode, LedgerName, LocationID, "
                    qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qryDetail = qryDetail + "(@ClaimID, @ContractNo, @Currency, @Quantity, @UnitMs, @ExchangeRate, "
                    qryDetail = qryDetail + " @ContractAmount, @CompletedValue, @BalanceValue, @PreviousClaimedAmount, @CurrentClaimAmount, "
                    qryDetail = qryDetail + " @RetentionPerc, @RetentionAmount, @TotalCurrentClaim, @BilledAmount, @BalanceNotClaimed, "
                    qryDetail = qryDetail + " @LedgerCode, @LedgerName, @LocationID, "
                    qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    commandSalesDetail.CommandText = qryDetail
                    commandSalesDetail.Parameters.Clear()

                    commandSalesDetail.Parameters.AddWithValue("@ClaimID", txtCNNo.Text)
                    commandSalesDetail.Parameters.AddWithValue("@ContractNo", lblContractNo.Text.Trim)
                    commandSalesDetail.Parameters.AddWithValue("@Currency", "")
                    commandSalesDetail.Parameters.AddWithValue("@Quantity", 1)
                    commandSalesDetail.Parameters.AddWithValue("@UnitMs", "")
                    commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                    commandSalesDetail.Parameters.AddWithValue("@ContractAmount", Convert.ToDecimal(lblContractAmt.Text))
                    commandSalesDetail.Parameters.AddWithValue("@CompletedValue", Convert.ToDecimal(lblCompletedValue.Text.Trim))
                    commandSalesDetail.Parameters.AddWithValue("@BalanceValue", Convert.ToDecimal(lblBalanceValue.Text))
                    commandSalesDetail.Parameters.AddWithValue("@PreviousClaimedAmount", Convert.ToDecimal(lblPreviousClaimAmount.Text))
                    commandSalesDetail.Parameters.AddWithValue("@CurrentClaimAmount", Convert.ToDecimal(lblCurrentClaimedAmount.Text))
                    commandSalesDetail.Parameters.AddWithValue("@RetentionPerc", Convert.ToDecimal(lblRetentionPerc.Text))
                    commandSalesDetail.Parameters.AddWithValue("@RetentionAmount", Convert.ToDecimal(lblRetentionAmount.Text))
                    commandSalesDetail.Parameters.AddWithValue("@TotalCurrentClaim", Convert.ToDecimal(lblTotalCurrentClaim.Text))
                    commandSalesDetail.Parameters.AddWithValue("@BilledAmount", Convert.ToDecimal(lblBilledAmount.Text))
                    commandSalesDetail.Parameters.AddWithValue("@BalanceNotClaimed", Convert.ToDecimal(lblBalanceNotClaimed.Text))

                    commandSalesDetail.Parameters.AddWithValue("@LedgerCode", "")
                    commandSalesDetail.Parameters.AddWithValue("@LedgerName", "")
                    commandSalesDetail.Parameters.AddWithValue("@LocationID", "")

                    commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    commandSalesDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    commandSalesDetail.Connection = conn
                    commandSalesDetail.ExecuteNonQuery()




                    '            'ToBillAmtTot = ToBillAmtTot + lblidTotalPrice()

                    '            'End: Update tblServiceRecord

                    '            'command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                    '            'command.Parameters.AddWithValue("@Itemtype", "SERVICE")
                    '            'command.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                    '            'command.Parameters.AddWithValue("@Itemdescription", dt1.Rows(0)("Description").ToString())
                    '            'command.Parameters.AddWithValue("@UOM", "")
                    '            'command.Parameters.AddWithValue("@Qty", 1)
                    '            'command.Parameters.AddWithValue("@PricePerUOM", Convert.ToDecimal(lblid23.Text))
                    '            ''command.Parameters.AddWithValue("@BillAmount", 0.0)
                    '            'command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(lblid23.Text))
                    '            'command.Parameters.AddWithValue("@DiscPerc", 0.0)
                    '            'command.Parameters.AddWithValue("@DiscAmount", 0.0)
                    '            'command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(lblid23.Text))
                    '            'command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(txtTaxRatePct.Text))
                    '            'command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                    '            'command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                    '            'command.Parameters.AddWithValue("@TaxType", "SR")
                    '            'command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                    '            'command.Parameters.AddWithValue("@ARCode", "")
                    '            'command.Parameters.AddWithValue("@GSTCode", "")
                    '            'command.Parameters.AddWithValue("@OtherCode", dt1.Rows(0)("COACode").ToString())
                    '            'command.Parameters.AddWithValue("@GLDescription", dt1.Rows(0)("COADescription").ToString())
                    '            'command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                    '            ''command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                    '            'command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                    '            'command.Parameters.AddWithValue("@ServiceStatus", lblid9.Text)
                    '            'command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    '            'command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                    '            'command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                    '            'command.Parameters.AddWithValue("@InvoiceType", "SERVICE")
                    '            'If lblid5.Text.Trim = "" Then
                    '            '    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                    '            'Else
                    '            '    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                    '            'End If
                    '            'command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    '            ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    '            'command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    '            ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    '            'command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    '            'command.Connection = conn
                    '            'command.ExecuteNonQuery()
                    'End If
                Next rowIndex



                '    'txtCNNoSelected.Text = txtCNNo.Text

                '    grvBillingDetailsNew.DataBind()
                '    updPanelCN.Update()
                '    'txtInvoiceAmount.Text = Convert.ToDecimal(ToBillAmt)
                '    'txtDiscountAmount.Text = Convert.ToDecimal(DiscAmount)
                '    'txtAmountWithDiscount.Text = (Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount)).ToString("N2")
                '    'txtGSTAmount.Text = Convert.ToDecimal(GSTAmount)
                '    'txtNetAmount.Text = Convert.ToDecimal(NetAmount)

                '    'DisplayGLGrid()
                '    'UpdatePanel3.Update()
            End If

            conn.Close()
            conn.Dispose()
            '            ToBillAmt = ToBillAmt + Convert.ToDecimal(lblidTotalPrice.Text)
            '            DiscAmount = DiscAmount + Convert.ToDecimal(lblidDiscAmount.Text)
            '            GSTAmount = GSTAmount + Convert.ToDecimal(lblidGSTBase.Text)
            '            NetAmount()

            'txtInvoiceAmount.Text = Convert.ToDecimal(ToBillAmt)
            'txtDiscountAmount.Text = Convert.ToDecimal(DiscAmount)
            'txtAmountWithDiscount.Text = (Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount)).ToString("N2")
            'txtGSTAmount.Text = Convert.ToDecimal(GSTAmount)
            'txtNetAmount.Text = Convert.ToDecimal(NetAmount)
            ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString
            'UpdatePanel3.Update()

            'InsertNewLogDetail()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "InsertIntoSales", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Function FindAccountId() As Boolean
        Dim IsAccountId As Boolean
        IsAccountId = False

        Dim connIsAccountId As MySqlConnection = New MySqlConnection()

        connIsAccountId.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connIsAccountId.Open()

        Dim commandIsAccountId As MySqlCommand = New MySqlCommand
        commandIsAccountId.CommandType = CommandType.Text

        If ddlContactType.Text = "COMPANY" Or ddlContactType.Text = "CORPORATE" Then
            commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblCompany where AccountId ='" & txtAccountIdBilling.Text & "'"
        ElseIf ddlContactType.Text = "PERSON" Or ddlContactType.Text = "RESIDENTIAL" Then
            commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblPerson where AccountId ='" & txtAccountIdBilling.Text & "'"
        End If

        'If txtAccountType.Text = "COMPANY" Or txtAccountType.Text = "CORPORATE" Then
        '    commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblCompany where AccountId ='" & txtAccountIdBilling.Text & "'"
        'ElseIf txtAccountType.Text = "PERSON" Or txtAccountType.Text = "RESIDENTIAL" Then
        '    commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblPerson where AccountId ='" & txtAccountIdBilling.Text & "'"
        'End If


        commandIsAccountId.Connection = connIsAccountId

        Dim drIsAccountId As MySqlDataReader = commandIsAccountId.ExecuteReader()
        Dim dtIsAccountId As New DataTable
        dtIsAccountId.Load(drIsAccountId)

        If dtIsAccountId.Rows.Count > 0 Then
            If dtIsAccountId.Rows(0)("CountAccountId").ToString > 0 Then
                IsAccountId = True
            End If
        End If

        commandIsAccountId.Dispose()
        connIsAccountId.Close()
        connIsAccountId.Dispose()
        dtIsAccountId.Dispose()
        drIsAccountId.Close()
        Return IsAccountId
    End Function

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

        'Dim IsLock = FindCNPeriod(txtReceiptPeriod.Text)
        'If IsLock = "Y" Then
        '    lblAlert.Text = "PERIOD IS LOCKED"
        '    updPnlMsg.Update()
        '    txtCNDate.Focus()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If

        If String.IsNullOrEmpty(txtAccountIdBilling.Text) = True Then
            lblAlert.Text = "PLEASE SELECT ACCOUNT ID"
            updPnlMsg.Update()
            txtAccountIdBilling.Focus()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtCompanyGroup.Text.Trim) = True Then
            lblAlert.Text = "INVALID COMPANY GROUP"
            updPnlMsg.Update()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If
        '''''''''''''''''''''

        Dim IsAccountIdExist As Boolean = FindAccountId()

        If IsAccountIdExist = False Then
            lblAlert.Text = "INVALID ACCOUNT ID"
            updPnlMsg.Update()
            btnSave.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If
        '''''''''''''''''''''

        'If (ddlCreditTerms.SelectedIndex) = 0 Then
        '    lblAlert.Text = "PLEASE SELECT CREDIT TERMS"
        '    updPnlMsg.Update()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If

        'If (ddlSalesmanBilling.SelectedIndex) = 0 Then
        '    lblAlert.Text = "PLEASE SELECT SALESMAN"
        '    updPnlMsg.Update()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If


        'If IsNumeric(txtCreditDays.Text) = False Then
        '    lblAlert.Text = "CREDIT DAYS SHOULD BE NUMERIC ONLY"
        '    updPnlMsg.Update()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If


        'If Convert.ToDecimal(txtTotalGSTAmt.Text.Trim).ToString("N2") <> Convert.ToDecimal(txtCNGSTAmount.Text.Trim).ToString("N2") Then
        '    AdjustGStAmount()
        'End If



        'If Convert.ToDecimal(txtTotalGSTAmt.Text.Trim).ToString("N2") <> Convert.ToDecimal(txtCNGSTAmount.Text.Trim).ToString("N2") Then
        '    lblAlert.Text = "GST AMOUNTS DO NOT MATCH"
        '    updPnlMsg.Update()
        '    btnSave.Enabled = True
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    Exit Sub
        'End If

        Try

          
            Dim totalRows, totalRows2, totalRows3 As Long
            totalRows = 0
            totalRows2 = 0
            totalRows3 = 0

            Dim totalRows1 As Long
            totalRows1 = 0
            totalRows1 = grvBillingDetailsNew.Rows.Count

            SetRowDataBillingDetailsRecs()
            'SetRowDataServiceRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then

                If tableAdd.Rows.Count > 0 Then
                    'Dim lblidOtherCode1 As TextBox = CType(grvBillingDetails.Rows(0).FindControl("txtOtherCodeGV"), TextBox)
                    'Dim lblidItemType1 As DropDownList = CType(grvBillingDetails.Rows(0).FindControl("txtItemTypeGV"), DropDownList)

                    'If String.IsNullOrEmpty(lblidOtherCode1.Text) = True And lblidItemType1.Text = "-1" And totalRows1 = 0 Then
                    '    totalRows = totalRows + 1
                    '    lblAlert.Text = "PLEASE ENTER DETAILS RECORD"
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '    btnSave.Enabled = True
                    '    updPnlMsg.Update()

                    '    Exit Sub
                    'End If

                End If

                'SetRowDataServiceRecs()
                totalRows = 0

                Dim tableAdd1 As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

                If tableAdd1 IsNot Nothing Then

                    For rowIndex1 As Integer = 0 To tableAdd1.Rows.Count - 1
                        'Dim lblidOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim lblidItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim lblidServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtServiceRecordGV"), TextBox)
                        'Dim lblidTaxTypeGV As TextBox = CType(grvBillingDetails.Rows(rowIndex1).FindControl("txtTaxTypeGV"), TextBox)


                        'If lblidItemType.Text = "SERVICE" And String.IsNullOrEmpty(lblidServiceRecordNo.Text.Trim) = True Then
                        '    lblAlert.Text = "PLEASE ENTER SERVICE RECORD NO. FOR ALL SERVICE ITEM TYPE"
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        '    updPnlMsg.Update()
                        '    btnSave.Enabled = True
                        '    Exit Sub
                        'End If

                        'If String.IsNullOrEmpty(lblidOtherCode.Text) = True And lblidItemType.Text <> "-1" Then
                        '    totalRows = totalRows + 1
                        'End If

                        'If ((String.IsNullOrEmpty(lblidTaxTypeGV.Text) = True)) And lblidItemType.Text <> "-1" Then
                        '    totalRows2 = totalRows2 + 1
                        'End If

                        ''If lblidItemType.Text <> "-1" Then
                        ''    If ((lblidTaxTypeGV.Text) <> (txtGST.Text)) Then
                        ''        totalRows3 = totalRows3 + 1
                        ''    End If
                        ''End If

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


                If totalRows2 > 0 Then
                    lblAlert.Text = "PLEASE SELECT GST CODE "
                    lblAlert.Focus()
                    updPnlMsg.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    btnSave.Enabled = True
                    Exit Sub
                End If

                If totalRows3 > 0 Then
                    lblAlert.Text = "HEADER AND DETAIL TAX CODE DO NOT MATCH "
                    lblAlert.Focus()
                    updPnlMsg.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    btnSave.Enabled = True
                    Exit Sub
                End If

                '''''''''''''''''''
                InsertIntoTblProgressClaim()

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

            'InsertNewLogDetail()

            If txtMode.Text = "NEW" Then
                lblMessage.Text = "ADD: PROGRESS CLAIM RECORD SUCCESSFULLY ADDED"
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)


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
                        'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

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
                lblMessage.Text = "EDIT: PROGRESS CLAIM RECORD SUCCESSFULLY UPDATED"
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

            End If


            If String.IsNullOrEmpty(txt.Text.Trim) = True Then
                txt.Text = "SELECT ClaimID, ProjectCode, ClaimDate, Status, AccountId, AccountId, CustName FROM tblprogressclaim  WHERE 1=1 and ClaimID = '" & txtCNNo.Text & "'"
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
            Session.Add("Title", ddlDocType.SelectedItem.Text)



        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnSave_Click", ex.Message.ToString, txtCNNo.Text)
            Exit Sub
        End Try
    End Sub

    'Private Sub InsertNewLog()

    '    Try

    '        ' Start: Insert NEW Log table

    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforCNDN FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforCNDN").ToString = "1" Then
    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLog_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "CNDN")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtCNNo.Text.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()
    '            End If
    '        End If

    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "FUNCTION InsertNewLog", ex.Message.ToString, txtInvoiceNo.Text)
    '        'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '    End Try
    'End Sub


    'Private Sub InsertNewLogDetail()

    '    Try

    '        ' Start: Insert NEW Log table

    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        Dim command As MySqlCommand = New MySqlCommand

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text
    '        command.CommandText = "SELECT EnableLogforCNDN FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforCNDN").ToString = "1" Then


    '                Dim connLog As MySqlConnection = New MySqlConnection()

    '                connLog.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataLogConnectionString").ConnectionString
    '                If connLog.State = ConnectionState.Open Then
    '                    connLog.Close()
    '                    connLog.Dispose()
    '                End If
    '                connLog.Open()

    '                Dim commandInsertLog As MySqlCommand = New MySqlCommand
    '                commandInsertLog.CommandType = CommandType.StoredProcedure
    '                commandInsertLog.CommandText = "InsertLogDetail_sitadatadb"

    '                commandInsertLog.Parameters.Clear()

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "CNDN")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtCNNo.Text.Trim)
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValueDetail", "")

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()

    '            End If
    '        End If
    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("COMPANY - " + Session("UserID"), "FUNCTION InsertLogDetail", ex.Message.ToString, txtAccountId.Text)
    '        'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '    End Try
    'End Sub

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
            commandUpdateInvoiceValue1.Dispose()
            cmdCNSum.Dispose()
            dtCNSumServiceRecord.Dispose()
            dtCNSum.Dispose()
            drCNSum.Close()
            drCNSumServiceRecord.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "Updatetblservicebillingdetailitem", ex.Message.ToString, "")
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

          

            If String.IsNullOrEmpty(ddlContactType.Text.Trim) = False Then
                ddlContactTypeIS.Text = ddlContactType.Text
            End If
          
            ''''''''''''''''''
            'mdlImportInvoices.Show()
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnShowInvoices_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    'Protected Sub txtTaxTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
    '        xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)


    '        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

    '        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
    '        Dim lblid1 As DropDownList = CType(ddl1.FindControl("txtTaxTypeGV"), DropDownList)
    '        Dim lblid2 As TextBox = CType(ddl1.FindControl("txtGSTPercGV"), TextBox)


    '        'lTargetDesciption = lblid1.Text

    '        'Dim rowindex1 As Integer = ddl1.RowIndex

    '        Dim conn1 As MySqlConnection = New MySqlConnection(constr)
    '        conn1.Open()

    '        Dim commandGST As MySqlCommand = New MySqlCommand
    '        commandGST.CommandType = CommandType.Text
    '        commandGST.CommandText = "SELECT TaxRatePct FROM tbltaxtype where TaxType='" & lblid1.Text & "'"
    '        commandGST.Connection = conn1

    '        Dim drGST As MySqlDataReader = commandGST.ExecuteReader()
    '        Dim dtGST As New DataTable
    '        dtGST.Load(drGST)

    '        If dtGST.Rows.Count > 0 Then
    '            lblid2.Text = dtGST.Rows(0)("TaxRatePct").ToString

    '            CalculatePrice()
    '            'If dtGST.Rows(0)("GST").ToString = "P" Then
    '            '    lblAlert.Text = "SCHEUDLE HAS ALREADY BEEN GENERATED"
    '            '    conn1.Close()
    '            '    Exit Sub
    '            'End If
    '        End If

    '        conn1.Close()
    '        conn1.Dispose()
    '        commandGST.Dispose()
    '        dtGST.Dispose()
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtTaxTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
    '        Exit Sub
    '    End Try
    'End Sub


    Protected Sub txtItemTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            lblAlert.Text = ""
            updPnlMsg.Update()

            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)



            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("txtItemTypeGV"), DropDownList)

            Dim lblid2 As TextBox = CType(xrow1.FindControl("txtInvoiceNoGV"), TextBox)
            Dim lblid3 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
            Dim lblid4 As TextBox = CType(xrow1.FindControl("txtServiceRecordGV"), TextBox)

            'If (ddlDocType.SelectedIndex = 0) Then
            '    lblAlert.Text = "PLEASE ENTER VOUCHER TYPE"
            '    updPnlMsg.Update()
            '    ddlDocType.Focus()
            '    lblid1.SelectedIndex = 0
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If


            'If lblid1.Text = "SERVICE" Then
            '    lblid2.Enabled = True
            '    lblid3.Enabled = True
            '    lblid4.Enabled = True
            'Else
            '    lblid2.Text = ""
            '    lblid3.Text = ""
            '    lblid4.Text = ""
            '    'lblid2.Enabled = False
            '    lblid3.Enabled = False
            '    lblid4.Enabled = False
            'End If

            Dim rowindex1 As Integer = xrow1.RowIndex
            If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
                btnAddDetail_Click(sender, e)
                'txtRecordAdded.Text = "Y"
            End If

            'CalculateTotalPrice()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtItemTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
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
            Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtTaxTypeGV"), TextBox)
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtItemCodeGV_SelectedIndexChanged", ex.Message.ToString, "")
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

                'SetRowDataBillingDetailsRecs()
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
                    'SetPreviousDataBillingDetailsRecs()

                
                    rowdeleted = "N"

                    'If dt.Rows.Count = 0 Then
                    '    FirstGridViewRowBillingDetailsRecs()
                    'End If

                    'Dim commandUpdRecv As MySqlCommand = New MySqlCommand
                    'commandUpdRecv.CommandType = CommandType.Text
                    'commandUpdRecv.CommandText = "Update tblcn set NetAmount =" & Convert.ToDecimal(txtCNAmount.Text) & " where CNNumber = '" & txtCNNo.Text & "'"
                    'commandUpdRecv.Connection = conn
                    'commandUpdRecv.ExecuteNonQuery()

                    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                End If

                'CalculateTotalPrice()
                'CalculatePrice()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "grvBillingDetails_RowDeleting", ex.Message.ToString, "")
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

            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "grvBillingDetails_RowDataBound", ex.Message.ToString, "")
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
        ddlCompanyGrpSearch.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0

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

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "chkSelectGV_CheckedChanged", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "gvClient_PageIndexChanging", ex.Message.ToString, "")
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

    Protected Sub txtAccountIdBilling_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIdBilling.TextChanged
        If Len(txtAccountIdBilling.Text) > 2 Then
            btnClient_Click(sender, New ImageClickEventArgs(0, 0))
        End If
    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        Try
            lblAlert.Text = ""
            txtSearch.Text = ""
            txtPopUpClient.Text = ""

            txtSearch.Text = "CustomerSearch"

            If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIdBilling.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID,  LocationId, ServiceName"

                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text.Trim + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text.Trim + "%"") order by AccountID,  LocationId, ServiceName"

                    End If
                End If
               

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"

                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"

                    End If
                End If
                

                SqlDSClient.DataBind()
                gvClient.DataBind()
            End If
            txtCustomerSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnClient_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub UpdateTblSales(strInvoiceNo As String, strRecordNo As String, dblCNAmt As Decimal, strSubCode As String)
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
            'Dim command1 As MySqlCommand = New MySqlCommand

            'command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT AppliedBase FROM tblSales where InvoiceNumber = '" & strInvoiceNo & "'"
            'command1.Connection = conn

            'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            'Dim dt1 As New DataTable
            'dt1.Load(dr1)

            'If dt1.Rows.Count > 0 Then
            '    lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString
            'End If

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()


            'Dim command2 As MySqlCommand = New MySqlCommand
            'command2.CommandType = CommandType.Text

            ''command2.CommandText = "UPDATE tblSales SET CreditBase = (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblSalesDetail A, tblSales B WHERE " & _
            ''      "A.InvoiceNumber=B.InvoiceNumber AND A.SubCode = 'ARIN'  AND A.SourceInvoice = tblSales.InvoiceNumber AND " & _
            ''      "B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & strInvoiceNo & "' "

            'command2.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblSalesDetail A, tblSales B WHERE " & _
            '    "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and A.SourceInvoice = '" & strInvoiceNo & "'"

            'command2.Connection = conn

            ''command4.Connection = conn
            ''command2.ExecuteNonQuery()

            'Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            'Dim dt2 As New DataTable
            'dt2.Load(dr2)

            'If dt2.Rows.Count > 0 Then
            '    lTotalReceipt = dt2.Rows(0)("totalcn").ToString
            'End If
            ' ''''''''''''''''''''''''


            'Dim command21 As MySqlCommand = New MySqlCommand
            'command21.CommandType = CommandType.Text

            'command21.CommandText = "UPDATE tblSales SET CreditBase = '" & lTotalReceipt * (-1) & "' WHERE InvoiceNumber = '" & strInvoiceNo & "'"
            'command21.Connection = conn

            ''command4.Connection = conn
            'command21.ExecuteNonQuery()
            ' ''''''''''''''''''''''''
            'Dim lbalance As Decimal
            'Dim command3 As MySqlCommand = New MySqlCommand
            'command3.CommandType = CommandType.Text
            'command3.CommandText = "SELECT ValueBase, GSTBase , ReceiptBase, CreditBase, CreditBase FROM tblSales where InvoiceNumber = '" & strInvoiceNo & "'"
            ''command3.CommandText = "UPDATE tblSales SET ReceiptOriginal = (SELECT SUM(A.ValueOriginal+A.GstOriginal) FROM tblRecvDet A, tblRecv B WHERE " & _
            ''"A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN' AND A.RefType = tblSales.InvoiceNumber AND " & _
            ''"B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & strInvoiceNo & "' "
            'command3.Connection = conn

            ''command4.Connection = conn
            'command3.ExecuteNonQuery()

            'Dim dr3 As MySqlDataReader = command3.ExecuteReader()
            'Dim dt3 As New DataTable
            'dt3.Load(dr3)

            'If dt3.Rows.Count > 0 Then

            '    If String.IsNullOrEmpty(dt3.Rows(0)("ValueBase").ToString) = False Then
            '        lbalance = dt3.Rows(0)("ValueBase").ToString
            '    Else
            '        lbalance = 0.0
            '    End If

            '    If String.IsNullOrEmpty(dt3.Rows(0)("GSTBase").ToString) = False Then
            '        lbalance = lbalance + dt3.Rows(0)("GSTBase").ToString
            '    Else
            '        'lbalance = 0.0
            '    End If

            '    If String.IsNullOrEmpty(dt3.Rows(0)("ReceiptBase").ToString) = False Then
            '        lbalance = lbalance - dt3.Rows(0)("ReceiptBase").ToString
            '    Else
            '        'lbalance = 0.0
            '    End If

            '    If String.IsNullOrEmpty(dt3.Rows(0)("CreditBase").ToString) = False Then
            '        lbalance = lbalance - dt3.Rows(0)("CreditBase").ToString
            '    Else
            '        'lbalance = 0.0
            '    End If
            '    'lbalance = Convert.ToDecimal(dt3.Rows(0)("ValueBase").ToString) + Convert.ToDecimal(dt3.Rows(0)("GSTBase").ToString) - Convert.ToDecimal(dt3.Rows(0)("ReceiptBase").ToString)
            '    'If String.IsNullOrEmpty(dt3.Rows(0)("totalcn").ToString) = True Then
            '    '    lTotalcn = 0.0
            '    'Else
            '    '    lTotalcn = dt3.Rows(0)("totalcn").ToString
            '    'End If
            'End If




            'Dim command4 As MySqlCommand = New MySqlCommand
            'command4.CommandType = CommandType.Text

            ' ''Dim qry4 As String = "Update tblSales Set PaidStatus = '" & lstatus & "', TotalReceiptAmount = " & lTotalReceipt & " where InvoiceNumber = @InvoiceNumber "
            'Dim qry4 As String = "Update tblSales Set BalanceBase = " & lbalance & " where InvoiceNumber = @InvoiceNumber "

            'command4.CommandText = qry4
            'command4.Parameters.Clear()

            'command4.Parameters.AddWithValue("@InvoiceNumber", strInvoiceNo)
            'command4.Connection = conn
            'command4.ExecuteNonQuery()

            '    'End: Update tblSales


            '''''''''''''' 01.12.18

            Dim commandCN As MySqlCommand = New MySqlCommand
            commandCN.CommandType = CommandType.Text


            commandCN.CommandText = "SELECT  ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) as totalcn FROM tblSalesDetail A, tblSales B WHERE " & _
                "A.InvoiceNumber=B.InvoiceNumber AND (B.DocType = 'ARCN' or B.DocType = 'ARDN')  and B.PostStatus = 'P' and A.SourceInvoice = '" & strInvoiceNo.Trim & "'"

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
               "A.VoucherNumber=B.VoucherNumber AND  B.PostStatus = 'P'  and A.RefType = '" & strInvoiceNo.Trim & "' "

            commandJournal.Connection = conn

            Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
            Dim dtJournal As New DataTable
            dtJournal.Load(drJournal)

            If dtJournal.Rows.Count > 0 Then
                'If dtJournal.Rows(0)("debitbase").ToString > 0.0 Then
                '    lTotalJV = dtJournal.Rows(0)("debitbase").ToString
                'Else
                '    lTotalJV = dtJournal.Rows(0)("creditbase").ToString
                'End If
                lTotalJV = Convert.ToDecimal(dtJournal.Rows(0)("debitbase").ToString - dtJournal.Rows(0)("creditbase").ToString)
            End If

            ''''''''''''''' Journal

            Dim lbalance As Decimal
            Dim command3 As MySqlCommand = New MySqlCommand
            command3.CommandType = CommandType.Text
            command3.CommandText = "SELECT ValueBase, GSTBase , AppliedBase , ReceiptBase, CreditBase, CreditBase FROM tblSales where InvoiceNumber = '" & strInvoiceNo.Trim & "'"

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

            command4.Parameters.AddWithValue("@InvoiceNumber", strInvoiceNo)
            command4.Connection = conn
            command4.ExecuteNonQuery()

            '    'End: Update tblSales

            '''''''''''''' 01.12.18

            ''Update tblServiceRecord
            'If String.IsNullOrEmpty(strRecordNo) = False Then

            '    '''''''''''''''''''
            '    Dim command22 As MySqlCommand = New MySqlCommand
            '    command22.CommandType = CommandType.Text
            '    command22.CommandText = "SELECT BilledAmt FROM tblServiceRecord where RecordNo = '" & strRecordNo & "'"
            '    command22.Connection = conn

            '    command22.ExecuteNonQuery()

            '    Dim dr22 As MySqlDataReader = command22.ExecuteReader()
            '    Dim dt22 As New DataTable
            '    dt22.Load(dr22)
            '    Dim lBilledamt As Decimal = 0.0
            '    If dt22.Rows.Count > 0 Then


            '        If String.IsNullOrEmpty(dt22.Rows(0)("BilledAmt").ToString) = False Then
            '            lBilledamt = dt22.Rows(0)("BilledAmt").ToString
            '            'Else
            '            '    lbalance = 0.0
            '        End If
            '    End If

            '    '''''''''''''''''''
            '    Dim command23 As MySqlCommand = New MySqlCommand
            '    command23.CommandType = CommandType.Text

            '    If lBilledamt = dblCNAmt Then
            '        command23.CommandText = "UPDATE tblServiceRecord SET BillNo = '', BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & strRecordNo & "'"
            '    Else
            '        command23.CommandText = "UPDATE tblServiceRecord SET BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & strRecordNo & "'"
            '    End If
            '    command23.Connection = conn

            '    command23.ExecuteNonQuery()

            '    command22.Dispose()
            '    command23.Dispose()
            'End If

            ''End Update Service Record
            conn.Close()
            conn.Dispose()

            'command1.Dispose()
            'command2.Dispose()
            'command21.Dispose()
            'command3.Dispose()
            'command4.Dispose()

            'dt1.Dispose()
            'dt2.Dispose()
            'dt3.Dispose()

            'dr1.Close()
            'dr2.Close()
            'dr3.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "UpdateTblSales", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Function PostCN() As Boolean
        Try

            Dim IsSuccess As Boolean = False
            Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()



            'Dim conn As MySqlConnection = New MySqlConnection()
            '''''''''''''''''''''''''''''''''''''''

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''
            Dim lStatusIsHeaderDetailEqual As String
            Dim commandIsHeaderDetailEqual As MySqlCommand = New MySqlCommand

            commandIsHeaderDetailEqual.CommandType = CommandType.StoredProcedure
            commandIsHeaderDetailEqual.CommandText = "IsHeaderDetailEqual"
            'commandIsExists.Connection = conn
            commandIsHeaderDetailEqual.Parameters.Clear()

            commandIsHeaderDetailEqual.Parameters.AddWithValue("@pr_Module", "CN")
            commandIsHeaderDetailEqual.Parameters.AddWithValue("@pr_DocumentNo", txtCNNo.Text.Trim)

            commandIsHeaderDetailEqual.Parameters.Add(New MySqlParameter("@pr_status", MySqlDbType.String))
            commandIsHeaderDetailEqual.Parameters("@pr_status").Direction = ParameterDirection.Output

            commandIsHeaderDetailEqual.Connection = conn
            commandIsHeaderDetailEqual.ExecuteScalar()

            lStatusIsHeaderDetailEqual = commandIsHeaderDetailEqual.Parameters("@pr_status").Value

            commandIsHeaderDetailEqual.Dispose()
            'conn.Close()
            'conn.Dispose()

            If lStatusIsHeaderDetailEqual = "F" Then
                conn.Close()
                conn.Dispose()
                lblAlert.Text = "CREDIT NOTE AMOUNT AND SUM OF APPLIED CREDIT NOTES SHOULD BE EQUAL.. CANNOT BE POSTED"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                'Exit Function
                'btnSaveInvoice.Enabled = True
                IsSuccess = False
                Return IsSuccess
            End If


            If lStatusIsHeaderDetailEqual = "V" Then
                conn.Close()
                conn.Dispose()
                lblAlert.Text = "THERE SHOULD BE DETAIL RECORDS.. CANNOT BE POSTED"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                'Exit Function
                'btnSaveInvoice.Enabled = True
                IsSuccess = False
                Return IsSuccess
            End If
            ''''''''''''''''''''''''''''''''''''''''''

            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblSales set PostStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()

            '''''''''''''''''''''

            'Dim command5 As MySqlCommand = New MySqlCommand
            'command5.CommandType = CommandType.Text

            'Dim qry5 As String = "DELETE from tblAR where VoucherNumber = '" & txtCNNo.Text & "'"

            'command5.CommandText = qry5
            ''command1.Parameters.Clear()
            'command5.Connection = conn
            'command5.ExecuteNonQuery()

            Dim qryAR As String
            Dim commandAR As MySqlCommand = New MySqlCommand
            commandAR.CommandType = CommandType.Text



            'Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            commandValues.CommandText = "SELECT SourceInvoice, RefType, ValueBase, SubCode  FROM tblSalesDetail where InvoiceNumber ='" & txtCNNo.Text.Trim & "'"
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

                'If String.IsNullOrEmpty(row("SourceInvoice")) = False Then
                '    UpdateTblSales(row("SourceInvoice"), row("RefType"), row("ValueBase") * (-1), row("SubCode"))
                'End If



                ''''''''''''''''''''
             
                'Dim dblCNAmt As Decimal
                'dblCNAmt = row("ValueBase") * (-1)
                'If String.IsNullOrEmpty(row("RefType")) = False Then

                '    '''''''''''''''''''
                '    Dim command22 As MySqlCommand = New MySqlCommand
                '    command22.CommandType = CommandType.Text
                '    command22.CommandText = "SELECT BilledAmt FROM tblServiceRecord where RecordNo = '" & row("RefType") & "'"
                '    command22.Connection = conn

                '    command22.ExecuteNonQuery()

                '    Dim dr22 As MySqlDataReader = command22.ExecuteReader()
                '    Dim dt22 As New DataTable
                '    dt22.Load(dr22)
                '    Dim lBilledamt As Decimal = 0.0
                '    If dt22.Rows.Count > 0 Then


                '        If String.IsNullOrEmpty(dt22.Rows(0)("BilledAmt").ToString) = False Then
                '            lBilledamt = dt22.Rows(0)("BilledAmt").ToString
                '            'Else
                '            '    lbalance = 0.0
                '        End If
                '    End If

                '''''''''''''''''''
                'Dim command23 As MySqlCommand = New MySqlCommand
                'command23.CommandType = CommandType.Text

                'If lBilledamt = dblCNAmt Then
                '    command23.CommandText = "UPDATE tblServiceRecord SET BillNo = '', BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & row("RefType") & "'"
                'Else
                '    command23.CommandText = "UPDATE tblServiceRecord SET BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & row("RefType") & "'"
                'End If
                'command23.Connection = conn

                'command23.ExecuteNonQuery()

                'command22.Dispose()
                'command23.Dispose()
                'End If

                'End

                ''''''''''''''''''''
                rowindex = rowindex + 1
            Next row


            '''''''''''''''''''''''''''''''''''''''
            'Dim cmdUpdateCNBalance As MySqlCommand = New MySqlCommand
            'cmdUpdateCNBalance.CommandType = CommandType.Text

            ' ''cmdUpdateCNBalance.CommandText = "UPDATE tblSales SET BalanceBase = " & Convert.ToDecimal(txtCNNetAmount.Text) & " - (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblSalesDetail A  " & _
            ' ''      " WHERE A.SubCode = 'SERVICE' AND InvoiceNumber = '" & txtCNNo.Text & "') WHERE PostStatus = 'P' and InvoiceNumber = '" & txtCNNo.Text & "'"

            'cmdUpdateCNBalance.CommandText = "UPDATE tblSales SET BalanceBase = " & Convert.ToDecimal(txtCNNetAmount.Text) & " - (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblSalesDetail A  " & _
            '" WHERE InvoiceNumber = '" & txtCNNo.Text & "' and ((SourceInvoice is not null) and (SourceInvoice <>''))) WHERE PostStatus = 'P' and InvoiceNumber = '" & txtCNNo.Text & "'"

            'cmdUpdateCNBalance.Connection = conn
            'cmdUpdateCNBalance.ExecuteNonQuery()
            'cmdUpdateCNBalance.Dispose()

            '''''''''''''''''''''''''''''''''''''''

            GridView1.DataBind()
            conn.Close()
            conn.Dispose()

            commandAR.Dispose()
            commandUpdateInvoice.Dispose()
            commandValues.Dispose()

            IsSuccess = True
            Return IsSuccess
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PostCN", ex.Message.ToString, txtCNNo.Text)
            IsSuccess = False
            Return IsSuccess
            Exit Function
        End Try

    End Function

    Private Function ReverseCN() As Boolean
        Try
            ''Dim confirmValue As String
            ''confirmValue = ""


            ''confirmValue = Request.Form("confirm_value")
            ''If Right(confirmValue, 3) = "Yes" Then
            ' ''''''''''''''' Insert tblAR

            ' ''''''''''''''''''''
            ''PopulateArCode()

            ' '''''''''''''''''''''''''
            'Dim IsSuccess As Boolean = False

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim command5 As MySqlCommand = New MySqlCommand
            'command5.CommandType = CommandType.Text

            'Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtCNNo.Text & "'"

            'command5.CommandText = qry5
            ''command1.Parameters.Clear()
            'command5.Connection = conn
            'command5.ExecuteNonQuery()


            'Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            'commandUpdateInvoice.CommandType = CommandType.Text
            'Dim sqlUpdateInvoice As String = "Update tblSales set PostStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            'commandUpdateInvoice.CommandText = sqlUpdateInvoice
            'commandUpdateInvoice.Parameters.Clear()
            'commandUpdateInvoice.Connection = conn
            'commandUpdateInvoice.ExecuteNonQuery()


            ' ''''''''''''''''''''''''''''''''

            ''Start:Loop thru' Credit values

            'Dim commandValues As MySqlCommand = New MySqlCommand

            'commandValues.CommandType = CommandType.Text
            'commandValues.CommandText = "SELECT *  FROM tblSalesdetail where InvoiceNumber ='" & txtCNNo.Text.Trim & "'"
            'commandValues.Connection = conn

            'Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            'Dim dtValues As New DataTable
            'dtValues.Load(drValues)


            ''Dim lTotalReceiptAmt As Decimal
            ''Dim lInvoiceAmt As Decimal
            ''Dim lReceptAmtAdjusted As Decimal

            ''lTotalReceiptAmt = 0.0
            ''lInvoiceAmt = 0.0

            ''lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            ''Dim rowindex = 0

            'For Each row As DataRow In dtValues.Rows

            '    ''Start: Update tblSales

            '    ' ''''''''''''''''''''
            '    If IsDBNull(row("SourceInvoice")) = False Then
            '        If String.IsNullOrEmpty(row("SourceInvoice")) = False Then
            '            UpdateTblSales(row("SourceInvoice"), row("RefType"), row("ValueBase"), row("SubCode"))
            '        End If
            '    End If

            '    ''''''''''''''''''''
            '    'Private Sub UpdateTblSales(strInvoiceNo As String, strRecordNo As String, dblCNAmt As Decimal, strSubCode As String)
            '    'Update tblServiceRecord
            '    Dim dblCNAmt As Decimal
            '    dblCNAmt = row("ValueBase")
            '    If String.IsNullOrEmpty(row("RefType")) = False Then

            '        '''''''''''''''''''
            '        Dim command22 As MySqlCommand = New MySqlCommand
            '        command22.CommandType = CommandType.Text
            '        command22.CommandText = "SELECT BilledAmt FROM tblServiceRecord where RecordNo = '" & row("RefType") & "'"
            '        command22.Connection = conn

            '        command22.ExecuteNonQuery()

            '        Dim dr22 As MySqlDataReader = command22.ExecuteReader()
            '        Dim dt22 As New DataTable
            '        dt22.Load(dr22)
            '        Dim lBilledamt As Decimal = 0.0
            '        If dt22.Rows.Count > 0 Then


            '            If String.IsNullOrEmpty(dt22.Rows(0)("BilledAmt").ToString) = False Then
            '                lBilledamt = dt22.Rows(0)("BilledAmt").ToString
            '                'Else
            '                '    lbalance = 0.0
            '            End If
            '        End If

            '        '''''''''''''''''''
            '        Dim command23 As MySqlCommand = New MySqlCommand
            '        command23.CommandType = CommandType.Text

            '        If lBilledamt = dblCNAmt Then
            '            command23.CommandText = "UPDATE tblServiceRecord SET BillNo = '', BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & row("RefType") & "'"
            '        Else
            '            command23.CommandText = "UPDATE tblServiceRecord SET BilledAmt = '" & lBilledamt - dblCNAmt & "' WHERE RecordNo = '" & row("RefType") & "'"
            '        End If
            '        command23.Connection = conn

            '        command23.ExecuteNonQuery()

            '        command22.Dispose()
            '        command23.Dispose()
            '    End If

            '    'End

            'Next


            ' '''''''''''''''''''''''''''''''''''''''
            'Dim cmdUpdateCNBalance As MySqlCommand = New MySqlCommand
            'cmdUpdateCNBalance.CommandType = CommandType.Text

            'cmdUpdateCNBalance.CommandText = "UPDATE tblSales SET BalanceBase = " & Convert.ToDecimal(txtCNNetAmount.Text) & " WHERE InvoiceNumber = '" & txtCNNo.Text & "'"

            'cmdUpdateCNBalance.Connection = conn
            'cmdUpdateCNBalance.ExecuteNonQuery()
            'cmdUpdateCNBalance.Dispose()


            ' '''''''''''''''''''''''''''''''''''''''

            'conn.Close()
            'conn.Dispose()
            'command5.Dispose()
            ' '''''''''''''''''''''''''''''''''
            ''GridView1.DataBind()


            ''lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
            ''CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            ''lblAlert.Text = ""
            ''updPnlSearch.Update()
            ''updPnlMsg.Update()
            ''updpnlBillingDetails.Update()
            ' ''updpnlServiceRecs.Update()
            ''updpnlBillingDetails.Update()

            ''btnQuickSearch_Click(sender, e)

            ''btnChangeStatus.Enabled = False
            ''btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            ''btnEdit.Enabled = True
            ''btnEdit.ForeColor = System.Drawing.Color.Black

            ''btnDelete.Enabled = True
            ''btnDelete.ForeColor = System.Drawing.Color.Black

            ''btnPost.Enabled = True
            ''btnPost.ForeColor = System.Drawing.Color.Black
            ''End If


            ''End: Loop thru' Credit Values


            ' ''''''''''''''' Insert tblAR

            'IsSuccess = True
            'Return IsSuccess
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "ReverseCN", ex.Message.ToString, txtCNNo.Text)

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

            Dim IsLock = FindCNPeriod(txtReceiptPeriod.Text)
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnPost_Click", ex.Message.ToString, "")
            Exit Sub
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        '''
        If String.IsNullOrEmpty(txtCNNo.Text) = False Then
            Dim totalRows, totalRows1 As Long
            totalRows = 0
            totalRows1 = 0
            totalRows1 = grvBillingDetailsNew.Rows.Count

            'SetRowDataBillingDetailsRecs()
            'SetRowDataServiceRecs()
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
            End If
        End If


        ''''
        txtMode.Text = ""
        'txtMode.Text = "View"
        MakeMeNullBillingDetails()
        MakeMeNull()
        DisableControls()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Session("receiptfrom") = "invoice"
        'Session("cnfrom") = "invoice"
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

            If txtSearch.Text = "ImportService" Then

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If
               
                txtImportService.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If

            If txtSearch.Text = "ImportInvoice" Then

                'If txtDisplayRecordsLocationwise.Text = "Y" Then
                '    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                '    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                '    Else
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                '    End If
                'Else
                '    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                '    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                '    Else
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                '    End If
                'End If
               
                txtImportService.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If

            If txtSearch.Text = "CustomerSearch" Then

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
                    End If
                End If
              
                txtCustomerSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"

            End If

            If txtSearch.Text = "InvoiceSearch" Then

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like '%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like '%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by ServiceName"
                    End If
                End If
               
                txtInvoiceSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    'Private Sub DisplayGLGrid()
    '    Try

    '        ''''''''''''''''' Start: Display GL Grid

    '        FirstGridViewRowGL()

    '        updPnlBillingRecs.Update()

    '        'Start: GL Details

    '        Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '        Dim drCurrentRowLoc As DataRow = Nothing

    '        For i As Integer = 0 To grvGL.Rows.Count - 1
    '            dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
    '            drCurrentRowLoc = dtScdrLoc.NewRow()
    '            ViewState("CurrentTableGL") = dtScdrLoc
    '            grvGL.DataSource = dtScdrLoc
    '            grvGL.DataBind()


    '            SetPreviousDataGL()
    '        Next i

    '        FirstGridViewRowGL()

    '        Dim rowIndex3 = 0

    '        ''AR Values

    '        AddNewRowGL()


    '        ''AR values

    '        Dim TextBoxGLCodeAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '        TextBoxGLCodeAR.Text = txtARCode.Text

    '        Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '        TextBoxGLDescriptionAR.Text = txtARDescription.Text

    '        If ddlDocType.Text = "ARCN" Then

    '            Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmountAR.Text = (0.0).ToString("N2")

    '            Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmountAR.Text = Convert.ToDecimal(txtCNNetAmount.Text).ToString("N2")
    '        Else
    '            Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtCNNetAmount.Text).ToString("N2")

    '            Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmountAR.Text = (0.0).ToString("N2")
    '        End If



    '        ''GST values

    '        rowIndex3 += 1
    '        AddNewRowGL()
    '        Dim TextBoxGLCodeGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '        TextBoxGLCodeGST.Text = txtGSTOutputCode.Text

    '        Dim TextBoxGLDescriptionGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '        TextBoxGLDescriptionGST.Text = txtGSTOutputDescription.Text

    '        If ddlDocType.Text = "ARCN" Then
    '            Dim TextBoxDebitAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmountGST.Text = Convert.ToDecimal(txtCNGSTAmount.Text).ToString("N2")

    '            Dim TextBoxCreditAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmountGST.Text = (0.0).ToString("N2")

    '        Else
    '            Dim TextBoxDebitAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmountGST.Text = (0.0).ToString("N2")

    '            Dim TextBoxCreditAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmountGST.Text = Convert.ToDecimal(txtCNGSTAmount.Text).ToString("N2")
    '        End If

    '        ''GST Values



    '        rowIndex3 += 1
    '        AddNewRowGL()
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '        End If
    '        conn.Open()

    '        Dim cmdGL As MySqlCommand = New MySqlCommand
    '        cmdGL.CommandType = CommandType.Text
    '        'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblsalesDetail where InvoiceNumber ='" & txtInvoiceNo.Text.Trim & "' and InvoiceNo ='" & txtInvoiceNo.Text & "' order by OtherCode"
    '        cmdGL.CommandText = "SELECT LedgerCode, LedgerName, ValueBase  FROM tblsalesDetail where InvoiceNumber ='" & txtCNNo.Text.Trim & "' order by LedgerCode"

    '        'cmdGL.CommandText = "SELECT * FROM tblAR where BatchNo ='" & txtBatchNo.Text.Trim & "'"
    '        cmdGL.Connection = conn

    '        Dim drcmdGL As MySqlDataReader = cmdGL.ExecuteReader()
    '        Dim dtGL As New DataTable
    '        dtGL.Load(drcmdGL)

    '        'FirstGridViewRowGL()


    '        Dim TotDetailRecordsLoc = dtGL.Rows.Count
    '        If dtGL.Rows.Count > 0 Then

    '            lGLCode = ""
    '            lGLDescription = ""
    '            lCreditAmount = 0.0


    '            lGLCode = dtGL.Rows(0)("LedgerCode").ToString()
    '            lGLDescription = dtGL.Rows(0)("LedgerName").ToString()
    '            lCreditAmount = 0.0

    '            Dim rowIndex4 = 0

    '            For Each row As DataRow In dtGL.Rows

    '                If lGLCode = row("LedgerCode") Then
    '                    lCreditAmount = lCreditAmount + row("ValueBase")
    '                Else


    '                    If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
    '                        AddNewRowGL()
    '                    End If

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    TextBoxGLCode.Text = lGLCode

    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '                    TextBoxGLDescription.Text = lGLDescription

    '                    If ddlDocType.Text = "ARCN" Then
    '                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '                        TextBoxDebitAmount.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

    '                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '                        TextBoxCreditAmount.Text = (0.0).ToString("N2")
    '                    Else
    '                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '                        TextBoxDebitAmount.Text = (0.0).ToString("N2")

    '                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '                        TextBoxCreditAmount.Text = (0.0).ToString("N2")
    '                    End If

    '                    lGLCode = row("LedgerCode")
    '                    lGLDescription = row("LedgerName")
    '                    lCreditAmount = row("ValueBase")

    '                    rowIndex3 += 1
    '                    rowIndex4 += 1
    '                End If
    '            Next row

    '        End If


    '        'AddNewRowGL()

    '        Dim TextBoxGLCode1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '        TextBoxGLCode1.Text = lGLCode

    '        Dim TextBoxGLDescription1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '        TextBoxGLDescription1.Text = lGLDescription

    '        If ddlDocType.Text = "ARCN" Then
    '            Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

    '            Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmount1.Text = (0.0).ToString("N2")
    '        Else
    '            Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '            TextBoxDebitAmount1.Text = (0.0).ToString("N2")

    '            Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '            TextBoxCreditAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")
    '        End If


    '        SetRowDataGL()
    '        Dim dtScdrLoc1 As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '        Dim drCurrentRowLoc1 As DataRow = Nothing

    '        dtScdrLoc1.Rows.Remove(dtScdrLoc1.Rows(rowIndex3 + 1))
    '        drCurrentRowLoc1 = dtScdrLoc1.NewRow()
    '        ViewState("CurrentTableGL") = dtScdrLoc1
    '        grvGL.DataSource = dtScdrLoc1
    '        grvGL.DataBind()
    '        SetPreviousDataGL()
    '        conn.Close()
    '        conn.Dispose()


    '        cmdGL.Dispose()
    '        dtGL.Dispose()
    '        drcmdGL.Close()
    '        ''''''''''''''''' End: Display GL Grid
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.Message.ToString
    '        lblAlert.Text = exstr
    '        InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "DisplayGLGrid", ex.Message.ToString, "")
    '        Exit Sub
    '    End Try
    'End Sub

  

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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "PopulateARCode", ex.Message.ToString, "")
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
            txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"


            If txtSearch.Text = "InvoiceSearch" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
              
                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                End If
              
            End If

            If txtSearch.Text = "CustomerSearch" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
               
                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                    End If
                End If
             
            End If

            If txtClientFrom.Text = "ImportInvoice" Then
                'txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"

                'If txtDisplayRecordsLocationwise.Text = "Y" Then
                '    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                '    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                '    Else
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                '    End If
                'Else
                '    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                '    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                '        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                '    Else
                '        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                '    End If
                'End If
              
            End If


            If txtSearch.Text = "ImportService" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
               
                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                    End If
                Else
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by ServiceName"
                    End If
                End If
               
            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Session.Add("RecordNo", txtCNNo.Text)
        Session.Add("Title", ddlDocType.SelectedItem.Text)
        Session.Add("PrintType", "Print")
        'Response.Redirect("RV_CreditNote.aspx")
        mdlConfirmMultiPrint.Show()


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

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%""  or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"

                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%""  or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False  and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"

                    End If
                End If
             

                SqlDSClient.DataBind()
                gvClient.DataBind()

                updPanelCN.Update()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If
                
                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelCN.Update()
            End If
            txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnClientSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        Try
            lblAlert.Text = ""

            txtIsPopup.Text = ""
            'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            'txtAccountIdBilling.Text = ""


            If txtSearch.Text = "InvoiceFilter" Then
                txtSearchAccountID.Text = ""
                'txtAccountId.Text = ""


                'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                '    ddlSearchCompanyGrp.Text = ""
                'Else
                '    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(21).Text.Trim
                'End If


                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    ddlSearchContactType.Text = ""
                Else
                    ddlSearchContactType.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                    txtSearchAccountID.Text = ""
                Else
                    txtSearchAccountID.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                    txtSearchClientName.Text = ""
                Else
                    txtSearchClientName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                End If


                'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                '    txtLocationId.Text = ""
                'Else
                '    txtLocationId.Text = gvClient.SelectedRow.Cells(3).Text.Trim
                'End If

                updPanelCN.Update()
                txtSearch.Text = "N"
                'mdlImportServices.Show()
                mdlPopupSearch.Show()

            End If

            If txtClientFrom.Text = "ImportInvoice" Then
                'txtAccountIdII.Text = ""
                'txtAccountIdII.Text = ""

                'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                '    ddlContactTypeII.Text = ""
                'Else
                '    ddlContactTypeII.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                'End If

                'If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                '    txtAccountIdII.Text = ""
                'Else
                '    txtAccountIdII.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                'End If


                'If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                '    txtClientNameII.Text = ""
                'Else
                '    txtClientNameII.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                'End If

                ''If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                ''    ddlCompanyGrpII.Text = ""
                ''Else
                ''    ddlCompanyGrpII.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
                ''End If

                'If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                '    ddlCompanyGrpII.Text = ""
                'Else
                '    ddlCompanyGrpII.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
                'End If


                'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                '    txtAccountIdII.Text = ""
                'Else
                '    txtAccountIdII.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                'End If

                'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                '    txtClientNameII.Text = ""
                'Else
                '    txtClientNameII.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
                'End If


                'If (gvClient.SelectedRow.Cells(9).Text = "&nbsp;") Then
                '    ddlCompanyGrpII.Text = ""
                'Else
                '    ddlCompanyGrpII.Text = gvClient.SelectedRow.Cells(9).Text.Trim
                'End If
                txtSearch.Text = ""
                'mdlImportInvoices.Show()
                'If (gvClient.SelectedRow.Cells(23).Text = "&nbsp;") Then
                '    txtSalesman.Text = ""
                'Else
                '    txtSalesman.Text = gvClient.SelectedRow.Cells(23).Text.Trim
                'End If

            End If



            If txtSearch.Text = "ImportService" Then
                txtAccountId.Text = ""
                txtClientName.Text = ""


                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    ddlContactTypeIS.Text = ""
                Else
                    ddlContactTypeIS.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                    txtAccountId.Text = ""
                Else
                    txtAccountId.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                End If


                If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                    txtClientName.Text = ""
                Else
                    txtClientName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                End If

                'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                '    ddlCompanyGrp.Text = ""
                'Else
                '    ddlCompanyGrp.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
                'End If

                If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                    ddlCompanyGrp.Text = ""
                Else
                    ddlCompanyGrp.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
                End If


                'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                '    txtAccountId.Text = ""
                'Else
                '    txtAccountId.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                'End If


                'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                '    txtClientName.Text = ""
                'Else
                '    txtClientName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
                'End If

                'If (gvClient.SelectedRow.Cells(9).Text = "&nbsp;") Then
                '    ddlCompanyGrp.Text = ""
                'Else
                '    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(9).Text.Trim
                'End If
                txtSearch.Text = ""
                mdlImportServices.Show()
            End If

            If txtClientFrom.Text = "" Then
                If txtSearch.Text = "CustomerSearch" Then
                    txtAccountIdBilling.Text = ""
                    txtAccountIdBilling.Text = ""

                    If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                        ddlContactType.Text = ""
                    Else
                        ddlContactType.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                    End If


                    If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                        txtAccountIdBilling.Text = ""
                    Else
                        txtAccountIdBilling.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                    End If


                    If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                        txtAccountName.Text = ""
                    Else
                        txtAccountName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                    End If

                    'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                    '    txtCompanyGroup.Text = ""
                    'Else
                    '    txtCompanyGroup.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
                    'End If

                    If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                        txtCompanyGroup.Text = ""
                    Else
                        txtCompanyGroup.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
                    End If


                    If (gvClient.SelectedRow.Cells(7).Text = "&nbsp;") Then
                        txtContactPerson.Text = ""
                    Else
                        txtContactPerson.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(7).Text.Trim)
                    End If


                    If (gvClient.SelectedRow.Cells(9).Text = "&nbsp;") Then
                        txtBillAddress.Text = ""
                    Else
                        txtBillAddress.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(9).Text.Trim)
                    End If

                    'If (gvClient.SelectedRow.Cells(16).Text = "&nbsp;") Then
                    '    txtBillStreet.Text = ""
                    'Else
                    '    txtBillStreet.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(16).Text.Trim)
                    'End If

                    'If (gvClient.SelectedRow.Cells(17).Text = "&nbsp;") Then
                    '    txtBillBuilding.Text = ""
                    'Else
                    '    txtBillBuilding.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(17).Text.Trim)
                    'End If


                    If (gvClient.SelectedRow.Cells(14).Text = "&nbsp;") Then
                        txtBillStreet.Text = ""
                    Else
                        txtBillStreet.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(14).Text.Trim)
                    End If

                    If (gvClient.SelectedRow.Cells(15).Text = "&nbsp;") Then
                        txtBillBuilding.Text = ""
                    Else
                        txtBillBuilding.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(15).Text.Trim)
                    End If



                    ''''''''''''
                    If (gvClient.SelectedRow.Cells(16).Text = "&nbsp;") Then
                        txtBillCity.Text = ""
                    Else
                        txtBillCity.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(16).Text.Trim)
                    End If


                    If (gvClient.SelectedRow.Cells(17).Text = "&nbsp;") Then
                        txtBillState.Text = ""
                    Else
                        txtBillState.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(17).Text.Trim)
                    End If

                    '''''''''''''
                    If (gvClient.SelectedRow.Cells(18).Text = "&nbsp;") Then
                        txtBillCountry.Text = ""
                    Else
                        txtBillCountry.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(18).Text.Trim)
                    End If

                    If (gvClient.SelectedRow.Cells(19).Text = "&nbsp;") Then
                        txtBillPostal.Text = ""
                    Else
                        txtBillPostal.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(19).Text.Trim)
                    End If


                    If (gvClient.SelectedRow.Cells(23).Text = "&nbsp;") Then
                        txtLocation.Text = ""
                    Else
                        txtLocation.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(23).Text.Trim)
                    End If

                    'If (gvClient.SelectedRow.Cells(13).Text = "&nbsp;") Then
                    '    ddlSalesmanBilling.SelectedIndex = 0
                    'Else
                    '    ddlSalesmanBilling.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(13).Text.Trim)
                    'End If

                  



                    'txtSearch.Text = ""
                ElseIf txtSearch.Text = "InvoiceSearch" Then
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

                    'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                    '    ddlCompanyGrpSearch.Text = ""
                    'Else
                    '    ddlCompanyGrpSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(21).Text.Trim)
                    'End If

                    If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                        ddlCompanyGrpSearch.Text = ""
                    Else
                        ddlCompanyGrpSearch.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
                    End If

                    updPnlSearch.Update()
                End If
            End If
            'PopulateArCode()


            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()

            'Dim sql As String
            'sql = ""

            ''sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
            'sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"
            'Dim command1 As MySqlCommand = New MySqlCommand
            'command1.CommandType = CommandType.Text
            'command1.CommandText = sql
            'command1.Connection = conn

            'Dim dr1 As MySqlDataReader = command1.ExecuteReader()

            'Dim dt1 As New DataTable
            'dt1.Load(dr1)

            'If dt1.Rows.Count > 0 Then
            '    If dt1.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt1.Rows(0)("COACode").ToString : End If
            '    If dt1.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt1.Rows(0)("Description").ToString : End If
            'End If


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


            ''Dim sql As String
            'sql = ""
            'sql = "Select Description, COACode, COADescription from tblbillingproducts where ProductCode <> 'CN-RET' and CompanyGroup = '" & txtCompanyGroup.Text & "'"

            'Dim command10 As MySqlCommand = New MySqlCommand
            'command10.CommandType = CommandType.Text
            'command10.CommandText = sql
            'command10.Connection = conn

            'Dim dr10 As MySqlDataReader = command10.ExecuteReader()

            'Dim dt10 As New DataTable
            'dt10.Load(dr10)

            'If dt10.Rows.Count > 0 Then
            '    If dt10.Rows(0)("Description").ToString <> "" Then : txtARDescription10.Text = dt10.Rows(0)("Description").ToString : End If
            '    If dt10.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt10.Rows(0)("COACode").ToString : End If
            'End If

            '''''''''''''''''''''''''''''''''''
            'Dim query As String

            'ddlContractNo.Items.Clear()
            'ddlContractNo.Items.Add("--SELECT--")

            'sql = ""
            ''sql = "Select ContractNo from tblContract where Status = 'O' and GST = 'P' and CompanyGroup ='" & txtCompanyGroup.Text & "' and AccountId = '" & txtAccountIdBilling.Text & "' and ContractGroup <> 'ST'"

            'sql = "Select ContractNo from tblContract where   CompanyGroup ='" & txtCompanyGroup.Text & "' and AccountId = '" & txtAccountIdBilling.Text & "'"

            'Dim command11 As MySqlCommand = New MySqlCommand
            'command11.CommandType = CommandType.Text
            'command11.CommandText = sql
            'command11.Connection = conn

            'Dim dr11 As MySqlDataReader = command11.ExecuteReader()

            'Dim dt11 As New DataTable
            'dt11.Load(dr11)

            'If dt11.Rows.Count > 0 Then

            '    'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
            '    PopulateDropDownList(sql, "ContractNo", "ContractNo", ddlContractNo)

            '    'If dt11.Rows(0)("ContractNo").ToString <> "" Then : ddlContractNo.Text = dt11.Rows(0)("ContractNo").ToString : End If
            '    'If dt11.Rows(0)("ContractNo").ToString <> "" Then : txtComments.Text = dt11.Rows(0)("ContractNo").ToString : End If
            '    'If dt11.Rows(0)("COACode").ToString <> "" Then : txtARCode10.Text = dt11.Rows(0)("COACode").ToString : End If
            'End If

            '''''''''''''''''''''''''''''''''''

            'ddlItemCode.Items.Add("CN-RET")
            'ddlItemCode.Items.Clear()
            'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode <> 'CN-RET'"
            'PopulateDropDownList(query, "ProductCode", "ProductCode", ddlItemCode)

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
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

    Protected Sub GrdViewGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdViewGL.SelectedIndexChanged
        Try

            If txtGLFrom.Text = "InvoiceSearch" Then
                'txtGLCodeII.Text = GrdViewGL.SelectedRow.Cells(1).Text
                'txtLedgerNameII.Text = GrdViewGL.SelectedRow.Cells(2).Text
                txtGLFrom.Text = ""
                mdlPopupGL.Hide()
                'mdlImportInvoices.Show()
            ElseIf txtGLFrom.Text = "ServiceSearch" Then
                txtGLCodeIS.Text = GrdViewGL.SelectedRow.Cells(1).Text
                txtLedgerNameIS.Text = GrdViewGL.SelectedRow.Cells(2).Text
                txtGLFrom.Text = ""
                mdlPopupGL.Hide()
                mdlImportServices.Show()
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "GrdViewGL_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
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

        If String.IsNullOrEmpty(ddlContactType.Text.Trim) = False Then
            ddlContactTypeIS.Text = ddlContactType.Text
        End If

        txtAccountId.Text = txtAccountIdBilling.Text
        txtClientName.Text = txtAccountName.Text
        mdlImportServices.Show()
    End Sub

  

   

    Protected Sub ImageButton2_Click1(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        lblAlert.Text = ""
        'lblAlert1.Text = ""
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

           
                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    End If
                Else
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    End If
                End If
              

                SqlDSClient.DataBind()
                gvClient.DataBind()
                'updPanelInvoice.Update()
            Else
                txtPopUpClient.Text = ""
           

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    End If
                Else
                    If ddlContactTypeIS.Text = "CORPORATE" Or ddlContactTypeIS.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    ElseIf ddlContactTypeIS.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeIS.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by ServiceName"
                    End If
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "ImageButton2_Click1", ex.Message.ToString, "")
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

    '    Protected Sub btnImportInvoiceII_Click(sender As Object, e As EventArgs) Handles btnImportInvoiceII.Click
    '        Try
    '            txtClientFrom.Text = ""
    '            Dim totalRows As Long
    '            totalRows = 0


    '            For rowIndex1 As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
    '                Dim TextBoxchkSelect1 As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGVII"), CheckBox)
    '                If (TextBoxchkSelect1.Checked = True) Then
    '                    totalRows = totalRows + 1
    '                    GoTo insertRec1
    '                End If
    '            Next rowIndex1



    '            If totalRows = 0 Then
    '                mdlImportInvoices.Show()
    '                Dim message As String = "alert('PLEASE SELECT A RECORD')"
    '                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
    '                'MessageBox.Message.Alert(Page, "PLEASE SELECT A RECORD", "str")
    '                'lblAlert.Text = "PLEASE SELECT A RECORD"
    '                'lblAlert.Focus()
    '                'updPnlMsg.Update()

    '                Exit Sub
    '            End If

    'insertRec1:
    '            If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = True Then
    '                'txtCompanyGroup.Text = ddlCompanyGrpII.Text
    '                'ddlContactType.Text = ddlContactTypeII.Text
    '                'txtAccountIdBilling.Text = txtAccountIdII.Text
    '                'txtAccountName.Text = txtClientNameII.Text

    '                'ddlCompanyGrpII.Enabled = False
    '                'ddlContactTypeII.Enabled = False
    '                'txtAccountIdII.Enabled = False
    '                'txtClientNameII.Enabled = False
    '                btnClient.Visible = False
    '            Else
    '                'ddlCompanyGrp.Text = txtCompanyGroup.Text
    '                'txtAccountId.Text = txtAccountIdBilling.Text
    '                'txtClientName.Text = txtAccountName.Text

    '                'ddlCompanyGrp.Enabled = True
    '                'ddlContactType.Enabled = True
    '                'txtAccountId.Enabled = True
    '                'txtClientName.Enabled = True
    '                'btnClient.Visible = True

    '            End If
    '            'System.Threading.Thread.Sleep(5000)

    '            'Start: Billing Details

    '            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
    '            Dim drCurrentRowLoc As DataRow = Nothing

    '            For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
    '                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
    '                drCurrentRowLoc = dtScdrLoc.NewRow()
    '                ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
    '                grvBillingDetails.DataSource = dtScdrLoc
    '                grvBillingDetails.DataBind()

    '                SetPreviousDataBillingDetailsRecs()
    '            Next i

    '            FirstGridViewRowBillingDetailsRecs()

    '            'Start: From tblBillingDetailItem



    '            Dim rowselected As Integer
    '            rowselected = 0

    '            For rowIndex As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
    '                'string txSpareId = row.ItemArray[0] as string;
    '                Dim TextBoxchkSelect As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGVII"), CheckBox)

    '                If (TextBoxchkSelect.Checked = True) Then
    '                    AddNewRowBillingDetailsRecs()
    '                    Dim qry As String
    '                    qry = ""

    '                    Dim command As MySqlCommand = New MySqlCommand
    '                    command.CommandType = CommandType.Text
    '                    'Header
    '                    rowselected = rowselected + 1

    '                    Dim lblid13 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGVII"), TextBox)
    '                    Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGVII"), TextBox)
    '                    Dim InvoiceDate As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtSalesDateGVII"), TextBox)

    '                    Dim InvoiceAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtValueBaseGVII"), TextBox)
    '                    'Dim InvoiceAmmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAppliedBaseGVII"), TextBox)
    '                    Dim TotalReceiptAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmountGVII"), TextBox)
    '                    Dim TotalCNAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCNAmountGVII"), TextBox)
    '                    Dim TotalOSAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtOSAmountGVII"), TextBox)

    '                    'Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)
    '                    'Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)

    '                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
    '                    TextBoxItemType.Text = "INVOICE"

    '                    Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
    '                    TextBoxtxtInvoiceNoGV.Text = Convert.ToString(InvoiceNumber.Text)

    '                    Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtUOMGV"), DropDownList)
    '                    TextBoxUOM.Text = "UNIT"

    '                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
    '                    'TextBoxOtherCode.Text = txtARCode.Text
    '                    TextBoxOtherCode.Text = txtGLCodeII.Text

    '                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '                    'TextBoxGLDescription.Text = txtARDescription.Text
    '                    TextBoxGLDescription.Text = txtLedgerNameII.Text

    '                    Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTPercGV"), TextBox)
    '                    TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N2")

    '                    Dim gstCalc As Decimal
    '                    Dim CNDNAmt As Decimal

    '                    If ddlDocType.Text = "ARCN" Then

    '                        'Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
    '                        'TextBoxTotalPricePerUOM.Text = Convert.ToString(TotalOSAmount.Text) * (-1)

    '                        'Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
    '                        'TextBoxTotalPrice.Text = Convert.ToString(TotalOSAmount.Text) * (-1)

    '                        'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
    '                        'TextBoxTotalPriceWithDisc.Text = Convert.ToString(TotalOSAmount.Text) * (-1)

    '                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
    '                        'TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(TotalOSAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")) * (-1)
    '                        'gstCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

    '                        'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '                        'TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TotalOSAmount.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)) * (-1))


    '                        Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
    '                        'TextBoxTotalPricePerUOM.Text = Convert.ToString(TotalOSAmount.Text)
    '                        TextBoxTotalPricePerUOM.Text = Convert.ToString(InvoiceAmount.Text)

    '                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtQtyGV"), TextBox)
    '                        TextBoxQty.Text = (Convert.ToDecimal(-1).ToString("N2"))

    '                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
    '                        TextBoxTotalPrice.Text = (Convert.ToDecimal(InvoiceAmount.Text).ToString("N2")) * Convert.ToDecimal(TextBoxQty.Text).ToString("N2")

    '                        Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
    '                        TextBoxTotalPriceWithDisc.Text = Convert.ToDecimal(TextBoxTotalPrice.Text).ToString("N2")
    '                        CNDNAmt = TextBoxTotalPriceWithDisc.Text

    '                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
    '                        TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(InvoiceAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01 * (-1)).ToString("N2"))
    '                        gstCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

    '                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '                        TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)))

    '                    Else
    '                        'Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
    '                        'TextBoxTotalPricePerUOM.Text = Convert.ToString(TotalOSAmount.Text)

    '                        'Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
    '                        'TextBoxTotalPrice.Text = Convert.ToString(TotalOSAmount.Text)

    '                        'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
    '                        'TextBoxTotalPriceWithDisc.Text = Convert.ToString(TotalOSAmount.Text)

    '                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
    '                        'TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(TotalOSAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))
    '                        'gstCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

    '                        'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '                        'TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal(TotalOSAmount.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text))


    '                        Dim TextBoxTotalPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
    '                        TextBoxTotalPricePerUOM.Text = Convert.ToString(InvoiceAmount.Text)

    '                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtQtyGV"), TextBox)
    '                        TextBoxQty.Text = (Convert.ToDecimal(1).ToString("N2"))

    '                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
    '                        TextBoxTotalPrice.Text = (Convert.ToDecimal(InvoiceAmount.Text).ToString("N2")) * Convert.ToDecimal(TextBoxQty.Text).ToString("N2")

    '                        Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
    '                        TextBoxTotalPriceWithDisc.Text = Convert.ToDecimal(TextBoxTotalPrice.Text).ToString("N2")
    '                        CNDNAmt = TextBoxTotalPriceWithDisc.Text

    '                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
    '                        TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(InvoiceAmount.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))
    '                        gstCalc = Convert.ToDecimal(TextBoxGSTAmt.Text)

    '                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '                        TextBoxTotalPriceWithGST.Text = Convert.ToString((Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)))


    '                    End If


    '                    ''''''''''''''''''''''''''''''
    '                    'txtCNAmount.Text = (Convert.ToDecimal(txtCNAmount.Text) + Convert.ToDecimal(CNDNAmt)).ToString("N2")
    '                    'txtCNGSTAmount.Text = (Convert.ToDecimal(txtCNGSTAmount.Text) + Convert.ToDecimal(gstCalc)).ToString("N2")
    '                    'txtCNNetAmount.Text = (Convert.ToDecimal(txtCNAmount.Text) + Convert.ToDecimal(txtCNGSTAmount.Text)).ToString("N2")

    '                    ''Dim TextBoxItemCodeGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
    '                    ''TextBoxItemCodeGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode")))

    '                    'Dim TextBoxTermsGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtARCodeGV"), TextBox)
    '                    'TextBoxTermsGV.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Terms")))



    '                    'rowIndex += 1

    '                    'Next row


    '                    'txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
    '                    'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
    '                    'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
    '                    ''Else
    '                    'FirstGridViewRowBillingDetailsRecs()

    '                End If

    '                'End: Detail Records
    '                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString

    '                'Button1.Enabled = False
    '                'Timer1.Enabled = True
    '                'Thread.Sleep(1000)
    '                'Dim workerThread As New Thread(New ThreadStart(AddressOf ProcessRecords))
    '                'workerThread.Start()


    '                'textbox6.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
    '                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.Message.ToString
    '            Next

    '            btnSave.Enabled = True
    '            updpnlBillingDetails.Update()
    '            updPanelSave.Update()
    '            updPnlBillingRecs.Update()

    '        Catch ex As Exception
    '            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnImportInvoiceII_Click", ex.Message.ToString, "")
    '            Exit Sub
    '        End Try
    '    End Sub

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

            'txtCNAmount.Text = "0.00"
            'txtCNGSTAmount.Text = "0.00"
            'txtCNNetAmount.Text = "0.00"

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim rowselected As Integer
            rowselected = 0

            Dim lContractNo As String
            lContractNo = ""

            Dim lContractAmount As Double
            lContractAmount = 0.0

            Dim lTotalClaimAmount, lCurrentClaimAmount As Double
            lCurrentClaimAmount = 0.0
            lTotalClaimAmount = 0.0

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

                Dim ContractNoFirst As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                Dim ContractAmountFirst As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractAmountGV"), TextBox)
                Dim BillAmountFirst As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillAmountGV"), TextBox)


                If rowIndex = 0 Then
                    lContractNo = ContractNoFirst.Text.Trim
                    lContractAmount = ContractAmountFirst.Text.Trim
                    'lTotalClaimAmount = BillAmountFirst.Text.Trim
                End If


                If (TextBoxchkSelect.Checked = True) Then

                    Dim qry As String
                    qry = ""

                    Dim ContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    Dim ContractAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractAmountGV"), TextBox)
                    Dim BillAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillAmountGV"), TextBox)
                    'lCurrentClaimAmount = BillAmount.Text

                    If lContractNo <> ContractNo.Text.Trim Then

                        'Start: Add Record in grvBillingDetails

                        AddNewRowBillingDetailsRecs()
                        'Header
                        rowselected = rowselected + 1


                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        TextBoxContractNo.Text = Convert.ToString(lContractNo)

                        Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractAmountGV"), TextBox)
                        TextBoxContractAmount.Text = Convert.ToString(lContractAmount)

                        Dim TextBoxtxtPreviousClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                        TextBoxtxtPreviousClaimedAmount.Text = "0.00"

                        Dim TextBoxtxtCurrentClaimedAmountGV As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCurrentClaimedAmountGV"), TextBox)
                        TextBoxtxtCurrentClaimedAmountGV.Text = lTotalClaimAmount


                        Dim TextBoxCompletedAmount As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCompletedValueGV"), TextBox)

                        Dim command As MySqlCommand = New MySqlCommand
                        command.CommandType = CommandType.Text


                        If String.IsNullOrEmpty(lContractNo.Trim) = False Then

                            command.CommandText = "SELECT SUM(BillAmount) as completedvalue FROM tblServiceRecord where ContractNo = '" & lContractNo.Trim & "' and  Status = 'P'"
                            command.Connection = conn

                            Dim dr As MySqlDataReader = command.ExecuteReader()
                            Dim dt As New DataTable
                            dt.Load(dr)


                            If String.IsNullOrEmpty(dt.Rows(0)("completedvalue").ToString()) = False Then
                                TextBoxCompletedAmount.Text = (dt.Rows(0)("completedvalue").ToString())
                            Else
                                TextBoxCompletedAmount.Text = "0.00"
                            End If

                            dt.Dispose()
                        End If


                        Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtBalanceValueGV"), TextBox)
                        TextBoxBalanceValue.Text = (Convert.ToDecimal(TextBoxContractAmount.Text) - Convert.ToDecimal(TextBoxCompletedAmount.Text)).ToString("N2")

                        lContractNo = ContractNo.Text.Trim
                        lContractAmount = ContractAmount.Text.Trim
                        lTotalClaimAmount = 0.0
                        lCurrentClaimAmount = 0.0
                        lTotalClaimAmount = BillAmount.Text

                    Else
                        lTotalClaimAmount = lTotalClaimAmount + BillAmount.Text
                    End If

                    'End: Add Record In grvBillingDetails

                Else



                End If

                '    AddNewRowBillingDetailsRecs()
                '    'Header
                '    rowselected = rowselected + 1


                '    Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractNoGV"), TextBox)
                '    TextBoxContractNo.Text = Convert.ToString(ContractNo.Text)

                '    Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractAmountGV"), TextBox)
                '    TextBoxContractAmount.Text = Convert.ToString(ContractAmount.Text)

                '    Dim TextBoxCompletedAmount As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCompletedValueGV"), TextBox)

                '    Dim command As MySqlCommand = New MySqlCommand
                '    command.CommandType = CommandType.Text


                '    If String.IsNullOrEmpty(ContractNo.Text.Trim) = False Then

                '        command.CommandText = "SELECT SUM(BillAmount) as completedvalue FROM tblServiceRecord where ContractNo = '" & ContractNo.Text & "' and  Status = 'P'"
                '        command.Connection = conn

                '        Dim dr As MySqlDataReader = command.ExecuteReader()
                '        Dim dt As New DataTable
                '        dt.Load(dr)

                '        If dt.Rows.Count > 0 Then

                '            TextBoxCompletedAmount.Text = (dt.Rows(0)("completedvalue").ToString())
                '        Else
                '            TextBoxCompletedAmount.Text = "0.00"
                '        End If

                '        dt.Dispose()
                '    End If


                '    Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtBalanceValueGV"), TextBox)
                '    TextBoxBalanceValue.Text = (Convert.ToDecimal(TextBoxContractAmount.Text) - Convert.ToDecimal(TextBoxCompletedAmount.Text)).ToString("N2")

                'End If
nextrec:



            Next


            'Last Record


            AddNewRowBillingDetailsRecs()
            'Header
            rowselected = rowselected + 1


            Dim TextBoxContractNoL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractNoGV"), TextBox)
            TextBoxContractNoL.Text = Convert.ToString(lContractNo)

            Dim TextBoxContractAmountL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtContractAmountGV"), TextBox)
            TextBoxContractAmountL.Text = Convert.ToString(lContractAmount)

            Dim TextBoxtxtPreviousClaimedAmountL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtPreviousClaimedAmountGV"), TextBox)
            TextBoxtxtPreviousClaimedAmountL.Text = "0.00"

            Dim TextBoxtxtCurrentClaimedAmountGVL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCurrentClaimedAmountGV"), TextBox)
            TextBoxtxtCurrentClaimedAmountGVL.Text = lTotalClaimAmount


            Dim TextBoxCompletedAmountL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCompletedValueGV"), TextBox)

            Dim commandL As MySqlCommand = New MySqlCommand
            commandL.CommandType = CommandType.Text


            If String.IsNullOrEmpty(lContractNo.Trim) = False Then

                commandL.CommandText = "SELECT SUM(BillAmount) as completedvalue FROM tblServiceRecord where ContractNo = '" & lContractNo.Trim & "' and  Status = 'P'"
                commandL.Connection = conn

                Dim drL As MySqlDataReader = commandL.ExecuteReader()
                Dim dtL As New DataTable
                dtL.Load(drL)


                If String.IsNullOrEmpty(dtL.Rows(0)("completedvalue").ToString()) = False Then
                    TextBoxCompletedAmountL.Text = (dtL.Rows(0)("completedvalue").ToString())
                Else
                    TextBoxCompletedAmountL.Text = "0.00"
                End If

                dtL.Dispose()
            End If


            Dim TextBoxBalanceValueL As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtBalanceValueGV"), TextBox)
            TextBoxBalanceValueL.Text = (Convert.ToDecimal(TextBoxContractAmountL.Text) - Convert.ToDecimal(TextBoxCompletedAmountL.Text)).ToString("N2")


            'Last Record


            conn.Close()
            conn.Dispose()
          


            grvBillingDetails.Visible = True

            btnSave.Enabled = True
            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()



        Catch ex As Exception
            'lblAlert1.Text = ex.Message
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnImportService_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub ddlDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDocType.SelectedIndexChanged
        If ddlDocType.Text = "ARCN" Then
            Label47.Text = "CREDIT NOTE INFORMATION"
            Label5.Text = "CREDIT NOTE DETAILS"
        Else
            Label47.Text = "DEBIT NOTE INFORMATION"
            Label5.Text = "DEBIT NOTE DETAILS"
        End If
    End Sub

  

    '' GVB

    Protected Sub txtQtyGVB_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        CalculatePriceGVB()
    End Sub

    Protected Sub txtPricePerUOMGVB_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        CalculatePriceGVB()
    End Sub

    Protected Sub txtDiscAmountGVB_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        CalculatePriceGVB()
    End Sub

    Protected Sub txtDiscPercGVB_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        CalculatePriceGVB()
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

            'CalculateTotalPrice()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "CalculatePriceGVB", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    'Protected Sub txtTaxTypeGVB_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
    '        xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)


    '        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

    '        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
    '        Dim lblid1 As DropDownList = CType(ddl1.FindControl("txtTaxTypeGVB"), DropDownList)
    '        Dim lblid2 As TextBox = CType(ddl1.FindControl("txtGSTPercGVB"), TextBox)


    '        'lTargetDesciption = lblid1.Text

    '        'Dim rowindex1 As Integer = ddl1.RowIndex

    '        Dim conn1 As MySqlConnection = New MySqlConnection(constr)
    '        conn1.Open()

    '        Dim commandGST As MySqlCommand = New MySqlCommand
    '        commandGST.CommandType = CommandType.Text
    '        commandGST.CommandText = "SELECT TaxRatePct FROM tbltaxtype where TaxType='" & lblid1.Text & "'"
    '        commandGST.Connection = conn1

    '        Dim drGST As MySqlDataReader = commandGST.ExecuteReader()
    '        Dim dtGST As New DataTable
    '        dtGST.Load(drGST)

    '        If dtGST.Rows.Count > 0 Then
    '            lblid2.Text = dtGST.Rows(0)("TaxRatePct").ToString
    '            lblid2.Text = Convert.ToDecimal(lblid2.Text).ToString("N2")
    '            CalculatePriceGVB()
    '            'If dtGST.Rows(0)("GST").ToString = "P" Then
    '            '    lblAlert.Text = "SCHEUDLE HAS ALREADY BEEN GENERATED"
    '            '    conn1.Close()
    '            '    Exit Sub
    '            'End If
    '        End If

    '        conn1.Close()
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.Message.ToString
    '        lblAlert.Text = exstr
    '        InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtTaxTypeGVB_SelectedIndexChanged", ex.Message.ToString, "")
    '        Exit Sub
    '    End Try
    'End Sub
    ' GVB


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

            'If txtDisplayRecordsLocationwise.Text = "N" Then
            '    e.Row.Cells(18).Visible = False
            '    GridView1.HeaderRow.Cells(18).Visible = False
            'ElseIf txtDisplayRecordsLocationwise.Text = "Y" Then
            '    e.Row.Cells(18).Visible = True
            '    GridView1.HeaderRow.Cells(18).Visible = True
            'End If
        End If
    End Sub

    Protected Sub OnSelectedIndexChangedg1(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
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
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

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
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

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

                'Dim qryAR As String
                'Dim commandAR As MySqlCommand = New MySqlCommand
                'commandAR.CommandType = CommandType.Text

                'If Convert.ToDecimal(txtCNAmount.Text) > 0.0 Then

                '    '''''''''''''''''''''''''''''

                '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName,  "
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName,"
                '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"


                '    'Dim conn As MySqlConnection = New MySqlConnection()
                '    'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                '    'conn.Open()

                '    Dim cmdGL As MySqlCommand = New MySqlCommand
                '    cmdGL.CommandType = CommandType.Text
                '    cmdGL.CommandText = "SELECT LedgerCode, LedgerName, CNValue, InvoiceNo   FROM tblcndet where CNNumber ='" & txtCNNo.Text.Trim & "' order by LedgerCode"
                '    cmdGL.Connection = conn

                '    Dim drcmdGL As MySqlDataReader = cmdGL.ExecuteReader()
                '    Dim dtGL As New DataTable
                '    dtGL.Load(drcmdGL)

                '    'FirstGridViewRowGL()


                '    Dim TotDetailRecordsLoc = dtGL.Rows.Count
                '    If dtGL.Rows.Count > 0 Then

                '        lGLCode = ""
                '        lGLDescription = ""
                '        lCreditAmount = 0.0


                '        lGLCode = dtGL.Rows(0)("LedgerCode").ToString()
                '        lGLDescription = dtGL.Rows(0)("LedgerName").ToString()
                '        lCreditAmount = 0.0

                '        Dim rowIndex4 = 0

                '        For Each row As DataRow In dtGL.Rows

                '            'If lGLCode = row("LedgerCode") Then
                '            '    lCreditAmount = lCreditAmount + row("CNValue")
                '            'Else

                '            commandAR.CommandText = qryAR
                '            commandAR.Parameters.Clear()
                '            commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '            commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '            commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '            If txtCNDate.Text.Trim = "" Then
                '                commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '            Else
                '                commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '            End If
                '            commandAR.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '            commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
                '            commandAR.Parameters.AddWithValue("@GLCode", dtGL.Rows(rowIndex4)("LedgerCode").ToString())
                '            commandAR.Parameters.AddWithValue("@GLDescription", dtGL.Rows(rowIndex4)("LedgerName").ToString())
                '            commandAR.Parameters.AddWithValue("@DebitAmount", (0.0).ToString("N2"))
                '            commandAR.Parameters.AddWithValue("@CreditAmount", dtGL.Rows(rowIndex4)("CNValue").ToString())
                '            commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '            commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '            commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '            commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '            commandAR.Connection = conn
                '            commandAR.ExecuteNonQuery()



                '            commandAR.CommandText = qryAR
                '            commandAR.Parameters.Clear()
                '            commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '            commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '            commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '            If txtCNDate.Text.Trim = "" Then
                '                commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '            Else
                '                commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '            End If
                '            commandAR.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '            commandAR.Parameters.AddWithValue("@InvoiceNumber", dtGL.Rows(rowIndex4)("InvoiceNo").ToString())
                '            commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                '            commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                '            'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(lCreditAmount).ToString("N2"))
                '            'commandAR.Parameters.AddWithValue("@CreditAmount", (0.0).ToString("N2"))
                '            commandAR.Parameters.AddWithValue("@DebitAmount", dtGL.Rows(rowIndex4)("CNValue").ToString())
                '            commandAR.Parameters.AddWithValue("@CreditAmount", (0.0).ToString("N2"))
                '            commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '            commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '            commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '            commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '            commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '            commandAR.Connection = conn
                '            commandAR.ExecuteNonQuery()
                '            'If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
                '            '    AddNewRowGL()
                '            'End If



                '            'lGLCode = row("LedgerCode")
                '            'lGLDescription = row("LedgerName")
                '            'lCreditAmount = row("CNValue")

                '            rowIndex4 += 1
                '            'rowIndex4 += 1
                '            'End If
                '        Next row


                '        'commandAR.CommandText = qryAR
                '        'commandAR.Parameters.Clear()
                '        'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '        'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '        'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '        'If txtCNDate.Text.Trim = "" Then
                '        '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '        'Else
                '        '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '        'End If
                '        'commandAR.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '        'commandAR.Parameters.AddWithValue("@InvoiceNumber", dtGL.Rows(0)("InvoiceNo").ToString())
                '        'commandAR.Parameters.AddWithValue("@GLCode", lGLCode)
                '        'commandAR.Parameters.AddWithValue("@GLDescription", lGLDescription)
                '        'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(lCreditAmount).ToString("N2"))
                '        'commandAR.Parameters.AddWithValue("@CreditAmount", (0.0).ToString("N2"))
                '        'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '        'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '        'commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '        'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '        ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '        'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '        'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '        ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '        'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '        'commandAR.Connection = conn
                '        'commandAR.ExecuteNonQuery()
                '    End If


                '    'AddNewRowGL()

                '    'Dim TextBoxGLCode1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                '    'TextBoxGLCode1.Text = lGLCode

                '    'Dim TextBoxGLDescription1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                '    'TextBoxGLDescription1.Text = lGLDescription

                '    'Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                '    'TextBoxDebitAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

                '    'Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
                '    'TextBoxCreditAmount1.Text = (0.0).ToString("N2")

                '    'commandAR.CommandText = qryAR
                '    'commandAR.Parameters.Clear()
                '    'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    'If txtCNDate.Text.Trim = "" Then
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    'Else
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    'End If
                '    'commandAR.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                '    'commandAR.Parameters.AddWithValue("@InvoiceNumber", dtGL.Rows(0)("InvoiceNo").ToString())
                '    'commandAR.Parameters.AddWithValue("@GLCode", lGLCode)
                '    'commandAR.Parameters.AddWithValue("@GLDescription", lGLDescription)
                '    'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(lCreditAmount).ToString("N2"))
                '    'commandAR.Parameters.AddWithValue("@CreditAmount", (0.0).ToString("N2"))
                '    'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    'commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '    'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    'commandAR.Connection = conn
                '    'commandAR.ExecuteNonQuery()

                '    ''''''''''''''''''''

                '    ''AR values

                '    'Dim TextBoxGLCodeAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                '    'TextBoxGLCodeAR.Text = txtARCode.Text

                '    'Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                '    'TextBoxGLDescriptionAR.Text = txtARDescription.Text

                '    'Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                '    'TextBoxDebitAmountAR.Text = (0.0).ToString("N2")

                '    'Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
                '    'TextBoxCreditAmountAR.Text = Convert.ToDecimal(txtCNAmount.Text).ToString("N2")


                '    'commandAR.CommandText = qryAR
                '    'commandAR.Parameters.Clear()
                '    'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    'If txtCNDate.Text.Trim = "" Then
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    'Else
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    'End If
                '    'commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    'commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
                '    'commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                '    'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                '    'commandAR.Parameters.AddWithValue("@DebitAmount", (0.0).ToString("N2"))
                '    'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtCNAmount.Text).ToString("N2"))
                '    'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    'commandAR.Parameters.AddWithValue("@ModuleName", "CN")
                '    'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    'commandAR.Connection = conn
                '    'commandAR.ExecuteNonQuery()



                '    'Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableGL"), DataTable)

                '    'If tableAdd IsNot Nothing Then

                '    '    For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1

                '    '        Dim TextBoxGLCodeAR As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                '    '        Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                '    '        Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                '    '        Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtCreditAmountGV"), TextBox)

                '    '        commandAR.CommandText = qryAR
                '    '        commandAR.Parameters.Clear()
                '    '        commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    '        commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    '        commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    '        If txtCNDate.Text.Trim = "" Then
                '    '            commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    '        Else
                '    '            commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    '        End If
                '    '        commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    '        commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                '    '        commandAR.Parameters.AddWithValue("@GLCode", TextBoxGLCodeAR.Text)
                '    '        commandAR.Parameters.AddWithValue("@GLDescription", TextBoxGLDescriptionAR.Text)
                '    '        commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(TextBoxDebitAmountAR.Text))
                '    '        commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(TextBoxCreditAmountAR.Text))
                '    '        commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    '        commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    '        commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
                '    '        commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    '        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    '        commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    '        commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    '        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    '        commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    '        commandAR.Connection = conn
                '    '        commandAR.ExecuteNonQuery()
                '    '    Next
                '    'End If


                '    ''''''''''''''''''''

                '    'qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName,  "
                '    'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName,"
                '    'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    'commandAR.CommandText = qryAR
                '    'commandAR.Parameters.Clear()
                '    'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    'If txtCNDate.Text.Trim = "" Then
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    'Else
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    'End If
                '    'commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    'commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@GLCode", txtARCode10.Text)
                '    'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription10.Text)
                '    'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtReceiptAmt.Text))
                '    'commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                '    'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    'commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
                '    'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    'commandAR.Connection = conn
                '    'commandAR.ExecuteNonQuery()



                '    'qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, GLtype, "
                '    'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @GLtype,"
                '    'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    'commandAR.CommandText = qryAR
                '    'commandAR.Parameters.Clear()
                '    'commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    'If txtCNDate.Text.Trim = "" Then
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    'Else
                '    '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '    'End If
                '    'commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    'commandAR.Parameters.AddWithValue("@InvoiceNumber", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                '    'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                '    'commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                '    'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtReceiptAmt.Text))
                '    'commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '    'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    'commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
                '    'commandAR.Parameters.AddWithValue("@GLType", "Debtor")

                '    'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    'commandAR.Connection = conn
                '    'commandAR.ExecuteNonQuery()
                'End If



                Dim commandUpdateCN As MySqlCommand = New MySqlCommand
                commandUpdateCN.CommandType = CommandType.Text
                Dim sqlUpdateCN As String = "Update tblCN set PostStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateCN.CommandText = sqlUpdateCN
                commandUpdateCN.Parameters.Clear()
                commandUpdateCN.Connection = conn
                commandUpdateCN.ExecuteNonQuery()

                GridView1.DataBind()

                '    'Start:Loop thru' Credit values

                '    Dim commandValues As MySqlCommand = New MySqlCommand

                '    commandValues.CommandType = CommandType.Text
                '    commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtCNNo.Text.Trim & "'"
                '    commandValues.Connection = conn

                '    Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                '    Dim dtValues As New DataTable
                '    dtValues.Load(drValues)


                '    Dim lTotalReceiptAmt As Decimal
                '    Dim lInvoiceAmt As Decimal
                '    Dim lReceptAmtAdjusted As Decimal

                '    lTotalReceiptAmt = 0.0
                '    lInvoiceAmt = 0.0

                '    lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString


                '    For Each row As DataRow In dtValues.Rows

                '        If Convert.ToDecimal(row("ReceiptValue")) > 0.0 Then

                '            ''''''''''''''''''''

                '            Dim commandUpdateInvoiceValue As MySqlCommand = New MySqlCommand

                '            commandUpdateInvoiceValue.CommandType = CommandType.Text
                '            commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblservicebillingdetailitem where InvoiceNo ='" & row("InvoiceNo") & "' order by ServiceDate"
                '            commandUpdateInvoiceValue.Connection = conn

                '            Dim drInvoiceValue As MySqlDataReader = commandUpdateInvoiceValue.ExecuteReader()
                '            Dim dtInvoiceValue As New DataTable
                '            dtInvoiceValue.Load(drInvoiceValue)

                '            For Each row1 As DataRow In dtInvoiceValue.Rows

                '                '''''''''''''''''''''''''''

                '                lInvoiceAmt = row1("TotalPriceWithGST")

                '                If lTotalReceiptAmt = lInvoiceAmt Then
                '                    lReceptAmtAdjusted = lInvoiceAmt
                '                ElseIf lTotalReceiptAmt > lInvoiceAmt Then
                '                    lReceptAmtAdjusted = lInvoiceAmt
                '                ElseIf lTotalReceiptAmt < lInvoiceAmt Then
                '                    lReceptAmtAdjusted = lInvoiceAmt - lTotalReceiptAmt
                '                End If

                '                lTotalReceiptAmt = lTotalReceiptAmt - lReceptAmtAdjusted

                '                Dim commandUpdateInvoiceValue1 As MySqlCommand = New MySqlCommand
                '                commandUpdateInvoiceValue1.CommandType = CommandType.Text
                '                Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & Convert.ToDecimal(lReceptAmtAdjusted) & " where Rcno = " & row1("Rcno")

                '                commandUpdateInvoiceValue1.CommandText = sqlUpdateInvoiceValue1
                '                commandUpdateInvoiceValue1.Parameters.Clear()
                '                commandUpdateInvoiceValue1.Connection = conn
                '                commandUpdateInvoiceValue1.ExecuteNonQuery()

                '                ''''''''''''''''''''''''''''''


                '                '''''''''''''''''''''

                '                qryAR = "INSERT INTO tblAR(VoucherNumber,  AccountId, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, ServiceRecordNo, ServiceDate,  "
                '                qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '                qryAR = qryAR + " (@VoucherNumber, @AccountId, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @ServiceRecordNo, @ServiceDate, "
                '                qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '                commandAR.CommandText = qryAR
                '                commandAR.Parameters.Clear()
                '                commandAR.Parameters.AddWithValue("@VoucherNumber", txtCNNo.Text)
                '                commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)

                '                If txtCNDate.Text.Trim = "" Then
                '                    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '                Else
                '                    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtCNDate.Text).ToString("yyyy-MM-dd"))
                '                End If

                '                commandAR.Parameters.AddWithValue("@ContractNo", row1("ContractNo"))
                '                commandAR.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))
                '                commandAR.Parameters.AddWithValue("@GLCode", row("LedgerCode"))
                '                commandAR.Parameters.AddWithValue("@GLDescription", row("LedgerName"))
                '                commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)

                '                'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("ReceiptValue")))
                '                commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(lReceptAmtAdjusted))

                '                commandAR.Parameters.AddWithValue("@BatchNo", txtCNNo.Text)
                '                commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '                commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")

                '                commandAR.Parameters.AddWithValue("@ServiceRecordNo", row1("ServiceRecordNo"))


                '                'commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                '                'Else
                '                commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(row1("ServiceDate")).ToString("yyyy-MM-dd"))
                '                'End If

                '                commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '                commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '                commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '                commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '                commandAR.Connection = conn
                '                commandAR.ExecuteNonQuery()

                '            Next row1
                '        End If

                '    Next row
                ''End: Loop thru' Credit Values


                'Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                'commandUpdateInvoice.CommandType = CommandType.Text
                'Dim sqlUpdateInvoice As String = "Update tblcn set GLStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                'commandUpdateInvoice.CommandText = sqlUpdateInvoice
                'commandUpdateInvoice.Parameters.Clear()
                'commandUpdateInvoice.Connection = conn
                'commandUpdateInvoice.ExecuteNonQuery()

                'GridView1.DataBind()

                lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()

                btnQuickSearch_Click(sender, e)

                conn.Close()
                conn.Dispose()


                command5.Dispose()
                commandUpdateCN.Dispose()
            End If

            ''''''''''''''' Insert tblAR
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnReverse_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        mdlPopupStatus.Show()
    End Sub

    Protected Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
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


        Try
            If ddlNewStatus.SelectedIndex > 0 Then
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                command.CommandText = "UPDATE tblSales SET PostStatus='" + ddlNewStatus.SelectedValue + "' where rcno=" & Convert.ToInt32(txtRcno.Text)
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
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)


                SQLDSCN.SelectCommand = txt.Text
                SQLDSCN.DataBind()
                GridView1.DataBind()

                'InsertNewLog()
                'GridView1.DataSourceID = "SqlDataSource1"
                mdlPopupStatus.Hide()
            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnUpdateStatus_Click", ex.Message.ToString, "")
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

            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtCNAmount.Text, txtCNGSTAmount.Text, txtCNNetAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

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

    Public Function FindCNPeriod(BillingPeriod As String) As String
        Dim IsLock As String
        IsLock = "Y"

        Dim connPeriod As MySqlConnection = New MySqlConnection()

        connPeriod.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connPeriod.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        ''''''''''''
        If txtMode.Text = "NEW" Then
            If txtDisplayRecordsLocationwise.Text = "N" Then
                command1.CommandText = "SELECT CNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            Else
                command1.CommandText = "SELECT CNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            End If
        Else
            If txtDisplayRecordsLocationwise.Text = "N" Then
                command1.CommandText = "SELECT CNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            Else
                command1.CommandText = "SELECT CNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            End If

        End If


        '''''''''''''

        'If txtMode.Text = "NEW" Then
        '    command1.CommandText = "SELECT CNLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        'Else
        '    command1.CommandText = "SELECT CNLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        'End If
        'command1.CommandText = "SELECT ARLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        command1.Connection = connPeriod

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)


        If dt.Rows.Count > 0 Then
            If txtMode.Text = "NEW" Then
                IsLock = dt.Rows(0)("CNLock").ToString
            Else
                IsLock = dt.Rows(0)("CNLocke").ToString
            End If
        End If

        connPeriod.Close()
        connPeriod.Dispose()
        command1.Dispose()
        dt.Dispose()

        Return IsLock
    End Function

   

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

            Dim TextBoxRcno As TextBox = CType(grvBillingDetailsNew.Rows(rowindex1).Cells(5).FindControl("txtRcnoInvoiceGVB"), TextBox)

            If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                    Dim conn As MySqlConnection = New MySqlConnection(constr)
                    conn.Open()

                    Dim command4 As MySqlCommand = New MySqlCommand
                    command4.CommandType = CommandType.Text

                    'Dim qry4 As String = "Update tblservicerecord Set BilledAmt = " & Convert.ToDecimal(row("PriceWithDisc")) & ", BillNo = '' where Rcno= @Rcno "
                    Dim qry4 As String = "Delete from tblsalesdetail where rcno = " & TextBoxRcno.Text
                    'Dim qry4 As String = "Delete from tblsalesdetail "
                    command4.CommandText = qry4
                    command4.Connection = conn
                    command4.ExecuteNonQuery()
                    command4.Dispose()

                    'SqlDSSalesDetail.DataBind()
                    grvBillingDetailsNew.DataSourceID = "SqlDSSalesDetail"
                    grvBillingDetailsNew.DataBind()

                    '''''''''''''''''''''''''
                    'CalculateTotal()
                    'CalculateTotalPrice()

                    'calculateTotalCN()

                    'calculateTotalCN()
                    updPanelCN.Update()

                    'Exit Sub
                    Dim command5 As MySqlCommand = New MySqlCommand
                    command5.CommandType = CommandType.Text

                    Dim qry As String
                    qry = "Update tblSales set ValueBase = @ValueBase, ValueOriginal =@ValueOriginal, GSTBase=@GSTBase, GSTOriginal=@GSTOriginal, AppliedBase = @AppliedBase, AppliedOriginal=@AppliedOriginal, BalanceBase=@BalanceBase, BalanceOriginal=@BalanceOriginal, "
                    qry = qry + " DiscountAmount = @DiscountAmount, GSTAmount = @GSTAmount, NetAmount = @NetAmount  "
                    'qry = qry + " LastModifiedBy = @LastModifiedBy, LastModifiedOn = @LastModifiedOn "
                    qry = qry + " where Rcno = @Rcno;"

                    command5.CommandText = qry
                    command5.Parameters.Clear()

                    command5.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))

                    'command5.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(txtCNAmount.Text))
                    'command5.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(txtCNAmount.Text))
                    'command5.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(txtCNGSTAmount.Text))
                    'command5.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(txtCNGSTAmount.Text))
                    'command5.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtCNNetAmount.Text))
                    'command5.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(txtCNNetAmount.Text))
                    command5.Parameters.AddWithValue("@BalanceBase", 0.0)
                    command5.Parameters.AddWithValue("@BalanceOriginal", 0.0)
                    command5.Parameters.AddWithValue("@DiscountAmount", 0.0)
                    command5.Parameters.AddWithValue("@AmountWithDiscount", 0.0)
                    command5.Parameters.AddWithValue("@GSTAmount", 0.0)
                    command5.Parameters.AddWithValue("@NetAmount", 0.0)

                    'command5.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command5.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command5.Connection = conn
                    command5.ExecuteNonQuery()

                    '''''''''''''''''''''''
                    conn.Close()
                    conn.Dispose()
                    command5.Dispose()

                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtCNNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", TextBoxRcno.Text)

                    GridView1.DataBind()

                    'DisplayGLGrid()
                    'CalculatePrice()
                    IsDetailBlank()
                    updPanelCN.Update()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnDeleteDetail_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub txtInvoiceNoGV_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)


            Dim lblid0 As TextBox = CType(txt1.FindControl("txtInvoiceNoGV"), TextBox)
            Dim lblid1 As TextBox = CType(txt1.FindControl("txtServiceRecordGV"), TextBox)
            Dim lblid2 As DropDownList = CType(txt1.FindControl("txtItemTypeGV"), DropDownList)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtPricePerUOMGV"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtServiceStatusGV"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtContractNoGV"), TextBox)
            Dim lblid6 As TextBox = CType(txt1.FindControl("txtLocationIdGV"), TextBox)
            Dim lblid7 As TextBox = CType(txt1.FindControl("txtServiceDateGV"), TextBox)
            'Dim lblid8 As TextBox = CType(txt1.FindControl("txtOriginalBillAmountGV"), TextBox)
            Dim lblid9 As TextBox = CType(txt1.FindControl("txtTotalPriceGV"), TextBox)
            Dim lblid10 As TextBox = CType(txt1.FindControl("txtPriceWithDiscGV"), TextBox)

            Dim lblid11 As DropDownList = CType(txt1.FindControl("txtItemCodeGV"), DropDownList)
            Dim lblid12 As TextBox = CType(txt1.FindControl("txtItemDescriptionGV"), TextBox)
            Dim lblid13 As TextBox = CType(txt1.FindControl("txtOtherCodeGV"), TextBox)
            Dim lblid14 As TextBox = CType(txt1.FindControl("txtGLDescriptionGV"), TextBox)
            Dim lblid15 As TextBox = CType(txt1.FindControl("txtDescriptionGV"), TextBox)


            Dim lblid16 As TextBox = CType(txt1.FindControl("txtQtyGV"), TextBox)
            Dim lblid18 As TextBox = CType(txt1.FindControl("txtGSTAmtGV"), TextBox)
            Dim lblid19 As TextBox = CType(txt1.FindControl("txtTotalPriceWithGSTGV"), TextBox)
            Dim lblid20 As DropDownList = CType(txt1.FindControl("txtUOMGV"), DropDownList)

            Dim lblid21 As TextBox = CType(txt1.FindControl("txtTaxTypeGV"), TextBox)
            Dim lblid22 As TextBox = CType(txt1.FindControl("txtGSTPercGV"), TextBox)

            lblAlert.Text = ""
            updPnlMsg.Update()


            '''''''''''''''''''''''''''''''''''

            If String.IsNullOrEmpty(lblid0.Text) = False Then
                If lblid2.Text = "SERVICE" Then
                    'lblid0.Text = 0.0
                    lblid1.Text = 0.0
                    lblid3.Text = 0.0
                    'lblid8.Text = 0.0
                    lblid9.Text = 0.0
                    lblid10.Text = 0.0
                    lblid4.Text = ""
                    lblid5.Text = ""
                    lblid6.Text = ""
                    'lblid16.Text = ""
                    lblid7.Text = ""

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
                    commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where RefType = '" & lblid1.Text.Trim & "'"
                    commandIsInvDet.Connection = connIsInvDet

                    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                    Dim dtIsInvDet As New DataTable
                    dtIsInvDet.Load(drIsInvDet)

                    If dtIsInvDet.Rows.Count > 0 Then
                        lService = dtIsInvDet.Rows(0)("InvoiceNumber").ToString
                        lblid0.Text = lService.Trim
                    End If

                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''

                    Dim IsServiceRecord As Boolean
                    IsServiceRecord = False

                    Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

                    connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    connIsServiceRecord.Open()

                    Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
                    commandIsServiceRecord.CommandType = CommandType.Text
                    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                    commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lblid1.Text.Trim & "'"

                    commandIsServiceRecord.Connection = connIsServiceRecord

                    Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
                    Dim dtIsServiceRecord As New DataTable
                    dtIsServiceRecord.Load(drIsServiceRecord)

                    If dtIsServiceRecord.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("RecordNo").ToString) = True Then
                            lblid1.Text = ""
                            lblAlert.Text = "SERVICE RECORD NUMBER NOT FOUND"
                            updPnlMsg.Update()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                            Exit Sub
                        Else
                            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                                lblid1.Text = ""
                                lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
                                updPnlMsg.Update()
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If

                            lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
                            lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
                            lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
                            lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                            'CalculatePrice()
                            updpnlBillingDetails.Update()
                        End If
                    Else
                        lblid1.Text = ""
                        lblAlert.Text = "INVALID INVOICE NUMBER"
                        updPnlMsg.Update()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub

                    End If

                    commandIsServiceRecord.Dispose()
                    connIsServiceRecord.Close()
                    connIsServiceRecord.Dispose()
                End If


                If lblid2.Text = "OTHERS" Then

                    'lblid0.Text = 0.0
                    lblid1.Text = ""
                    lblid3.Text = 0.0
                    'lblid8.Text = 0.0
                    lblid9.Text = 0.0
                    lblid10.Text = 0.0
                    lblid4.Text = ""
                    lblid5.Text = ""
                    lblid6.Text = ""
                    'lblid16.Text = ""
                    lblid7.Text = ""

                    lblid18.Text = 0.0
                    lblid19.Text = 0.0

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
                    commandIsInvDet.CommandText = "SELECT InvoiceNumber, ValueBase from tblsales where accountid ='" & txtAccountIdBilling.Text & "' and  InvoiceNumber = '" & lblid0.Text.Trim & "'  and PostStatus ='P'"
                    commandIsInvDet.Connection = connIsInvDet

                    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                    Dim dtIsInvDet As New DataTable
                    dtIsInvDet.Load(drIsInvDet)

                    If dtIsInvDet.Rows.Count > 0 Then
                        lblid3.Text = dtIsInvDet.Rows(0)("ValueBase").ToString
                        lblid9.Text = Convert.ToDecimal(lblid16.Text) * dtIsInvDet.Rows(0)("ValueBase").ToString
                        lblid10.Text = Convert.ToDecimal(lblid16.Text) * dtIsInvDet.Rows(0)("ValueBase").ToString

                        lblid20.Text = "UNIT"

                      

                        lblid18.Text = Convert.ToDecimal(Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(lblid9.Text) * 0.01).ToString("N2")
                        lblid19.Text = Convert.ToDecimal(Convert.ToDecimal(lblid18.Text) + Convert.ToDecimal(lblid9.Text)).ToString("N2")

                        'lblid0.Text = lService.Trim
                    Else
                        lblAlert.Text = "INVALID INVOICE NUMBER"
                        lblid0.Text = ""

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub
                    End If


                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''

                    'Dim IsServiceRecord As Boolean
                    'IsServiceRecord = False

                    'Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

                    'connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    'connIsServiceRecord.Open()

                    'Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
                    'commandIsServiceRecord.CommandType = CommandType.Text
                    ''commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lblid1.Text.Trim & "'"

                    'commandIsServiceRecord.Connection = connIsServiceRecord

                    'Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
                    'Dim dtIsServiceRecord As New DataTable
                    'dtIsServiceRecord.Load(drIsServiceRecord)

                    'If dtIsServiceRecord.Rows.Count > 0 Then
                    '    If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("RecordNo").ToString) = True Then
                    '        lblid1.Text = ""
                    '        lblAlert.Text = "SERVICE RECORD NUMBER NOT FOUND"
                    '        updPnlMsg.Update()
                    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '        Exit Sub
                    '    Else
                    '        If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                    '            lblid1.Text = ""
                    '            lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
                    '            updPnlMsg.Update()
                    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '            Exit Sub
                    '        End If

                    '        lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
                    '        lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
                    '        lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
                    '        lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                    '        CalculatePrice()
                    '        updpnlBillingDetails.Update()
                    '    End If
                    'Else
                    '    lblid1.Text = ""
                    '    lblAlert.Text = "INVALID INVOICE NUMBER"
                    '    updPnlMsg.Update()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '    Exit Sub

                    'End If

                    'commandIsServiceRecord.Dispose()
                    'connIsServiceRecord.Close()
                    'connIsServiceRecord.Dispose()
                End If
                '''''''''''''''''''''''''''''''''''''
            End If


            'If lblid2.Text = "SERVICE" Then
            '    'lblid0.Text = 0.0
            '    lblid1.Text = 0.0
            '    lblid3.Text = 0.0
            '    'lblid8.Text = 0.0
            '    lblid9.Text = 0.0
            '    lblid10.Text = 0.0
            '    lblid4.Text = ""
            '    lblid5.Text = ""
            '    lblid6.Text = ""
            '    'lblid16.Text = ""
            '    lblid7.Text = ""


            '    ''''''''''''''''''''''
            '    Dim IsInvDet As Boolean
            '    IsInvDet = False
            '    Dim lService As String
            '    lService = ""

            '    Dim connIsInvDet As MySqlConnection = New MySqlConnection()

            '    connIsInvDet.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    connIsInvDet.Open()

            '    Dim commandIsInvDet As MySqlCommand = New MySqlCommand
            '    commandIsInvDet.CommandType = CommandType.Text
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text & " and AccountId ='" & txtAccountIdBilling.Text & "'"
            '    commandIsInvDet.CommandText = "SELECT RefType from tblsalesdetail where InvoiceNumber = '" & lblid0.Text.Trim & "'"
            '    'commandIsServiceRecord.CommandText = "SELECT InvoiceNumber, ValueBase, RefType, CostCode, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text.Trim & "'"

            '    commandIsInvDet.Connection = connIsInvDet

            '    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
            '    Dim dtIsInvDet As New DataTable
            '    dtIsInvDet.Load(drIsInvDet)

            '    If dtIsInvDet.Rows.Count > 0 Then
            '        lService = dtIsInvDet.Rows(0)("RefType").ToString
            '    End If

            '    connIsInvDet.Close()
            '    connIsInvDet.Dispose()
            '    ''''''''''''''''''''''

            '    Dim IsServiceRecord As Boolean
            '    IsServiceRecord = False

            '    Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

            '    connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    connIsServiceRecord.Open()

            '    Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
            '    commandIsServiceRecord.CommandType = CommandType.Text
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text & " and AccountId ='" & txtAccountIdBilling.Text & "'"
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text.Trim & "'"
            '    commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"

            '    commandIsServiceRecord.Connection = connIsServiceRecord

            '    Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
            '    Dim dtIsServiceRecord As New DataTable
            '    dtIsServiceRecord.Load(drIsServiceRecord)

            '    If dtIsServiceRecord.Rows.Count > 0 Then
            '        If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("BillNo").ToString) = True Then
            '            lblid1.Text = ""
            '            lblAlert.Text = "INVOICE NUMBER NOT FOUND FOR THE SERVICE RECORD"
            '            updPnlMsg.Update()
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            Exit Sub

            '        Else
            '            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
            '                lblid1.Text = ""
            '                lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
            '                updPnlMsg.Update()
            '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '                Exit Sub
            '            End If

            '            'If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("ContractNo").ToString.Trim) = True Then
            '            '    lblid1.Text = ""
            '            '    lblAlert.Text = "INVALID CONTRACT NUMBER"
            '            '    updPnlMsg.Update()
            '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '    Exit Sub
            '            'Else
            '            '    Dim commandIsContract As MySqlCommand = New MySqlCommand
            '            '    commandIsContract.CommandType = CommandType.Text
            '            '    commandIsContract.CommandText = "SELECT ContractGroup from tblContract where ContractNo ='" & dtIsServiceRecord.Rows(0)("ContractNo").ToString & "'"
            '            '    commandIsContract.Connection = connIsServiceRecord
            '            '    Dim drIsContract As MySqlDataReader = commandIsContract.ExecuteReader()
            '            '    Dim dtIsContract As New DataTable
            '            '    dtIsContract.Load(drIsContract)

            '            '    If dtIsContract.Rows.Count > 0 Then
            '            '        If dtIsContract.Rows(0)("ContractGroup").ToString <> ddlContractGroupBilling.Text.Trim Then
            '            '            lblAlert.Text = "SERVICE RECORD [" & lblid1.Text & "] DOES NOT BELONG TO CONTRACT GROUP [" & ddlContractGroupBilling.Text & "]"
            '            '            updPnlMsg.Update()
            '            '            lblid1.Text = ""
            '            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '            Exit Sub
            '            '        End If
            '            '    Else
            '            '        lblid1.Text = ""
            '            '        lblAlert.Text = "INVALID CONTRACT GROUP"
            '            '        updPnlMsg.Update()
            '            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '        Exit Sub
            '            '    End If
            '            'End If

            '            lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
            '            lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            'lblid8.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            'lblid4.Text = dtIsServiceRecord.Rows(0)("Status").ToString
            '            lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
            '            lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
            '            'lblid16.Text = dtIsServiceRecord.Rows(0)("ServiceBy").ToString
            '            lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
            '            CalculatePrice()
            '            updpnlBillingDetails.Update()

            '            '''''''''''''''''''''''
            '            'Dim commandOtherInfo As MySqlCommand = New MySqlCommand
            '            'If lblid4.Text = "P" Then
            '            '    commandOtherInfo.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
            '            'ElseIf lblid4.Text = "O" Then
            '            '    commandOtherInfo.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
            '            'End If
            '            'commandOtherInfo.Connection = connIsServiceRecord

            '            'Dim dr1 As MySqlDataReader = commandOtherInfo.ExecuteReader()
            '            'Dim dt1 As New DataTable
            '            'dt1.Load(dr1)

            '            'lblid11.Text = dt1.Rows(0)("Description").ToString()
            '            'lblid12.Text = dt1.Rows(0)("ProductCode").ToString()
            '            'lblid13.Text = dt1.Rows(0)("COACode").ToString()
            '            'lblid14.Text = dt1.Rows(0)("COADescription").ToString()
            '            'lblid15.Text = lblid1.Text & ", " & lblid7.Text & ", " & dtIsServiceRecord.Rows(0)("Notes").ToString()

            '            '''''''''''''''''''''''
            '            'End If
            '        End If
            '    Else
            '        lblid1.Text = ""
            '        lblAlert.Text = "INVALID INVOICE NUMBER"
            '        updPnlMsg.Update()
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '        Exit Sub

            '    End If

            '    commandIsServiceRecord.Dispose()
            '    connIsServiceRecord.Close()
            '    connIsServiceRecord.Dispose()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtInvoiceNoGV_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    ''
    Protected Sub txtInvoiceNoGVB_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)


            Dim lblid0 As TextBox = CType(txt1.FindControl("txtInvoiceNoGVB"), TextBox)
            Dim lblid1 As TextBox = CType(txt1.FindControl("txtServiceRecordGVB"), TextBox)
            Dim lblid2 As TextBox = CType(txt1.FindControl("txtItemTypeGVB"), TextBox)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtPricePerUOMGVB"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtServiceStatusGVB"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtContractNoGVB"), TextBox)
            Dim lblid6 As TextBox = CType(txt1.FindControl("txtLocationIdGVB"), TextBox)
            Dim lblid7 As TextBox = CType(txt1.FindControl("txtServiceDateGVB"), TextBox)
            'Dim lblid8 As TextBox = CType(txt1.FindControl("txtOriginalBillAmountGV"), TextBox)
            Dim lblid9 As TextBox = CType(txt1.FindControl("txtTotalPriceGVB"), TextBox)
            Dim lblid10 As TextBox = CType(txt1.FindControl("txtPriceWithDiscGVB"), TextBox)

            Dim lblid11 As DropDownList = CType(txt1.FindControl("txtItemCodeGVB"), DropDownList)
            Dim lblid12 As TextBox = CType(txt1.FindControl("txtItemDescriptionGVB"), TextBox)
            Dim lblid13 As TextBox = CType(txt1.FindControl("txtOtherCodeGVB"), TextBox)
            Dim lblid14 As TextBox = CType(txt1.FindControl("txtGLDescriptionGVB"), TextBox)
            Dim lblid15 As TextBox = CType(txt1.FindControl("txtDescriptionGVB"), TextBox)


            Dim lblid16 As TextBox = CType(txt1.FindControl("txtQtyGVB"), TextBox)
            Dim lblid18 As TextBox = CType(txt1.FindControl("txtGSTAmtGVB"), TextBox)
            Dim lblid19 As TextBox = CType(txt1.FindControl("txtTotalPriceWithGSTGVB"), TextBox)
            Dim lblid20 As DropDownList = CType(txt1.FindControl("txtUOMGVB"), DropDownList)

            Dim lblid21 As TextBox = CType(txt1.FindControl("txtTaxTypeGVB"), TextBox)
            Dim lblid22 As TextBox = CType(txt1.FindControl("txtGSTPercGVB"), TextBox)

            lblAlert.Text = ""
            updPnlMsg.Update()


            '''''''''''''''''''''''''''''''''''

            If String.IsNullOrEmpty(lblid0.Text) = False Then
                If lblid2.Text = "SERVICE" Then
                    'lblid0.Text = 0.0
                    lblid1.Text = 0.0
                    lblid3.Text = 0.0
                    'lblid8.Text = 0.0
                    lblid9.Text = 0.0
                    lblid10.Text = 0.0
                    lblid4.Text = ""
                    lblid5.Text = ""
                    lblid6.Text = ""
                    'lblid16.Text = ""
                    lblid7.Text = ""

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
                    commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where RefType = '" & lblid1.Text.Trim & "'"
                    commandIsInvDet.Connection = connIsInvDet

                    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                    Dim dtIsInvDet As New DataTable
                    dtIsInvDet.Load(drIsInvDet)

                    If dtIsInvDet.Rows.Count > 0 Then
                        lService = dtIsInvDet.Rows(0)("InvoiceNumber").ToString
                        lblid0.Text = lService.Trim
                    End If

                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''

                    Dim IsServiceRecord As Boolean
                    IsServiceRecord = False

                    Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

                    connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    connIsServiceRecord.Open()

                    Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
                    commandIsServiceRecord.CommandType = CommandType.Text
                    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                    commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lblid1.Text.Trim & "'"

                    commandIsServiceRecord.Connection = connIsServiceRecord

                    Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
                    Dim dtIsServiceRecord As New DataTable
                    dtIsServiceRecord.Load(drIsServiceRecord)

                    If dtIsServiceRecord.Rows.Count > 0 Then
                        If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("RecordNo").ToString) = True Then
                            lblid1.Text = ""
                            lblAlert.Text = "SERVICE RECORD NUMBER NOT FOUND"
                            updPnlMsg.Update()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                            Exit Sub
                        Else
                            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                                lblid1.Text = ""
                                lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
                                updPnlMsg.Update()
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If

                            lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
                            lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                            lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
                            lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
                            lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                            'CalculatePrice()
                            updpnlBillingDetails.Update()
                        End If
                    Else
                        lblid1.Text = ""
                        lblAlert.Text = "INVALID INVOICE NUMBER"
                        updPnlMsg.Update()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub

                    End If

                    commandIsServiceRecord.Dispose()
                    connIsServiceRecord.Close()
                    connIsServiceRecord.Dispose()
                End If


                If lblid2.Text = "OTHERS" Then

                    'lblid0.Text = 0.0
                    lblid1.Text = 0.0
                    lblid3.Text = 0.0
                    'lblid8.Text = 0.0
                    lblid9.Text = 0.0
                    lblid10.Text = 0.0
                    lblid4.Text = ""
                    lblid5.Text = ""
                    lblid6.Text = ""
                    'lblid16.Text = ""
                    lblid7.Text = ""

                    lblid18.Text = 0.0
                    lblid19.Text = 0.0

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
                    commandIsInvDet.CommandText = "SELECT InvoiceNumber, ValueBase from tblsales where accountid ='" & txtAccountIdBilling.Text & "' and  InvoiceNumber = '" & lblid0.Text.Trim & "' and PostStatus ='P'"
                    commandIsInvDet.Connection = connIsInvDet

                    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                    Dim dtIsInvDet As New DataTable
                    dtIsInvDet.Load(drIsInvDet)

                    If dtIsInvDet.Rows.Count > 0 Then
                        lblid3.Text = dtIsInvDet.Rows(0)("ValueBase").ToString
                        lblid9.Text = Convert.ToDecimal(lblid16.Text) * dtIsInvDet.Rows(0)("ValueBase").ToString
                        lblid10.Text = Convert.ToDecimal(lblid16.Text) * dtIsInvDet.Rows(0)("ValueBase").ToString

                        lblid20.Text = "UNIT"

                      

                        lblid18.Text = Convert.ToDecimal(Convert.ToDecimal(lblid22.Text) * Convert.ToDecimal(lblid9.Text) * 0.01).ToString("N2")
                        lblid19.Text = Convert.ToDecimal(Convert.ToDecimal(lblid18.Text) + Convert.ToDecimal(lblid9.Text)).ToString("N2")

                        'lblid0.Text = lService.Trim
                    Else
                        lblAlert.Text = "INVALID INVOICE NUMBER"
                        lblid0.Text = ""

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub
                    End If
                    CalculatePriceGVB()
                    'CalculatePrice()
                    connIsInvDet.Close()
                    connIsInvDet.Dispose()
                    ''''''''''''''''''''''

                    'Dim IsServiceRecord As Boolean
                    'IsServiceRecord = False

                    'Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

                    'connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                    'connIsServiceRecord.Open()

                    'Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
                    'commandIsServiceRecord.CommandType = CommandType.Text
                    ''commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lblid1.Text.Trim & "'"

                    'commandIsServiceRecord.Connection = connIsServiceRecord

                    'Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
                    'Dim dtIsServiceRecord As New DataTable
                    'dtIsServiceRecord.Load(drIsServiceRecord)

                    'If dtIsServiceRecord.Rows.Count > 0 Then
                    '    If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("RecordNo").ToString) = True Then
                    '        lblid1.Text = ""
                    '        lblAlert.Text = "SERVICE RECORD NUMBER NOT FOUND"
                    '        updPnlMsg.Update()
                    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '        Exit Sub
                    '    Else
                    '        If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                    '            lblid1.Text = ""
                    '            lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
                    '            updPnlMsg.Update()
                    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '            Exit Sub
                    '        End If

                    '        lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
                    '        lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                    '        lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
                    '        lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
                    '        lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
                    '        CalculatePrice()
                    '        updpnlBillingDetails.Update()
                    '    End If
                    'Else
                    '    lblid1.Text = ""
                    '    lblAlert.Text = "INVALID INVOICE NUMBER"
                    '    updPnlMsg.Update()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    '    Exit Sub

                    'End If

                    'commandIsServiceRecord.Dispose()
                    'connIsServiceRecord.Close()
                    'connIsServiceRecord.Dispose()
                End If
                '''''''''''''''''''''''''''''''''''''
            End If


            'If lblid2.Text = "SERVICE" Then
            '    'lblid0.Text = 0.0
            '    lblid1.Text = 0.0
            '    lblid3.Text = 0.0
            '    'lblid8.Text = 0.0
            '    lblid9.Text = 0.0
            '    lblid10.Text = 0.0
            '    lblid4.Text = ""
            '    lblid5.Text = ""
            '    lblid6.Text = ""
            '    'lblid16.Text = ""
            '    lblid7.Text = ""


            '    ''''''''''''''''''''''
            '    Dim IsInvDet As Boolean
            '    IsInvDet = False
            '    Dim lService As String
            '    lService = ""

            '    Dim connIsInvDet As MySqlConnection = New MySqlConnection()

            '    connIsInvDet.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    connIsInvDet.Open()

            '    Dim commandIsInvDet As MySqlCommand = New MySqlCommand
            '    commandIsInvDet.CommandType = CommandType.Text
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text & " and AccountId ='" & txtAccountIdBilling.Text & "'"
            '    commandIsInvDet.CommandText = "SELECT RefType from tblsalesdetail where InvoiceNumber = '" & lblid0.Text.Trim & "'"
            '    'commandIsServiceRecord.CommandText = "SELECT InvoiceNumber, ValueBase, RefType, CostCode, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text.Trim & "'"

            '    commandIsInvDet.Connection = connIsInvDet

            '    Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
            '    Dim dtIsInvDet As New DataTable
            '    dtIsInvDet.Load(drIsInvDet)

            '    If dtIsInvDet.Rows.Count > 0 Then
            '        lService = dtIsInvDet.Rows(0)("RefType").ToString
            '    End If

            '    connIsInvDet.Close()
            '    connIsInvDet.Dispose()
            '    ''''''''''''''''''''''

            '    Dim IsServiceRecord As Boolean
            '    IsServiceRecord = False

            '    Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

            '    connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    connIsServiceRecord.Open()

            '    Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
            '    commandIsServiceRecord.CommandType = CommandType.Text
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text & " and AccountId ='" & txtAccountIdBilling.Text & "'"
            '    'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where BillNo = '" & lblid0.Text.Trim & "'"
            '    commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"

            '    commandIsServiceRecord.Connection = connIsServiceRecord

            '    Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
            '    Dim dtIsServiceRecord As New DataTable
            '    dtIsServiceRecord.Load(drIsServiceRecord)

            '    If dtIsServiceRecord.Rows.Count > 0 Then
            '        If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("BillNo").ToString) = True Then
            '            lblid1.Text = ""
            '            lblAlert.Text = "INVOICE NUMBER NOT FOUND FOR THE SERVICE RECORD"
            '            updPnlMsg.Update()
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            Exit Sub

            '        Else
            '            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
            '                lblid1.Text = ""
            '                lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
            '                updPnlMsg.Update()
            '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '                Exit Sub
            '            End If

            '            'If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("ContractNo").ToString.Trim) = True Then
            '            '    lblid1.Text = ""
            '            '    lblAlert.Text = "INVALID CONTRACT NUMBER"
            '            '    updPnlMsg.Update()
            '            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '    Exit Sub
            '            'Else
            '            '    Dim commandIsContract As MySqlCommand = New MySqlCommand
            '            '    commandIsContract.CommandType = CommandType.Text
            '            '    commandIsContract.CommandText = "SELECT ContractGroup from tblContract where ContractNo ='" & dtIsServiceRecord.Rows(0)("ContractNo").ToString & "'"
            '            '    commandIsContract.Connection = connIsServiceRecord
            '            '    Dim drIsContract As MySqlDataReader = commandIsContract.ExecuteReader()
            '            '    Dim dtIsContract As New DataTable
            '            '    dtIsContract.Load(drIsContract)

            '            '    If dtIsContract.Rows.Count > 0 Then
            '            '        If dtIsContract.Rows(0)("ContractGroup").ToString <> ddlContractGroupBilling.Text.Trim Then
            '            '            lblAlert.Text = "SERVICE RECORD [" & lblid1.Text & "] DOES NOT BELONG TO CONTRACT GROUP [" & ddlContractGroupBilling.Text & "]"
            '            '            updPnlMsg.Update()
            '            '            lblid1.Text = ""
            '            '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '            Exit Sub
            '            '        End If
            '            '    Else
            '            '        lblid1.Text = ""
            '            '        lblAlert.Text = "INVALID CONTRACT GROUP"
            '            '        updPnlMsg.Update()
            '            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '            '        Exit Sub
            '            '    End If
            '            'End If

            '            lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString
            '            lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            'lblid8.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
            '            'lblid4.Text = dtIsServiceRecord.Rows(0)("Status").ToString
            '            lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
            '            lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
            '            'lblid16.Text = dtIsServiceRecord.Rows(0)("ServiceBy").ToString
            '            lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")
            '            CalculatePrice()
            '            updpnlBillingDetails.Update()

            '            '''''''''''''''''''''''
            '            'Dim commandOtherInfo As MySqlCommand = New MySqlCommand
            '            'If lblid4.Text = "P" Then
            '            '    commandOtherInfo.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
            '            'ElseIf lblid4.Text = "O" Then
            '            '    commandOtherInfo.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
            '            'End If
            '            'commandOtherInfo.Connection = connIsServiceRecord

            '            'Dim dr1 As MySqlDataReader = commandOtherInfo.ExecuteReader()
            '            'Dim dt1 As New DataTable
            '            'dt1.Load(dr1)

            '            'lblid11.Text = dt1.Rows(0)("Description").ToString()
            '            'lblid12.Text = dt1.Rows(0)("ProductCode").ToString()
            '            'lblid13.Text = dt1.Rows(0)("COACode").ToString()
            '            'lblid14.Text = dt1.Rows(0)("COADescription").ToString()
            '            'lblid15.Text = lblid1.Text & ", " & lblid7.Text & ", " & dtIsServiceRecord.Rows(0)("Notes").ToString()

            '            '''''''''''''''''''''''
            '            'End If
            '        End If
            '    Else
            '        lblid1.Text = ""
            '        lblAlert.Text = "INVALID INVOICE NUMBER"
            '        updPnlMsg.Update()
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '        Exit Sub

            '    End If

            '    commandIsServiceRecord.Dispose()
            '    connIsServiceRecord.Close()
            '    connIsServiceRecord.Dispose()
            'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtInvoiceNoGVB_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Protected Sub txtServiceRecordGV_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)


            Dim lblid0 As TextBox = CType(txt1.FindControl("txtInvoiceNoGV"), TextBox)
            Dim lblid1 As TextBox = CType(txt1.FindControl("txtServiceRecordGV"), TextBox)
            Dim lblid2 As DropDownList = CType(txt1.FindControl("txtItemTypeGV"), DropDownList)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtPricePerUOMGV"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtServiceStatusGV"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtContractNoGV"), TextBox)
            Dim lblid6 As TextBox = CType(txt1.FindControl("txtLocationIdGV"), TextBox)
            Dim lblid7 As TextBox = CType(txt1.FindControl("txtServiceDateGV"), TextBox)
            'Dim lblid8 As TextBox = CType(txt1.FindControl("txtOriginalBillAmountGV"), TextBox)
            Dim lblid9 As TextBox = CType(txt1.FindControl("txtTotalPriceGV"), TextBox)
            Dim lblid10 As TextBox = CType(txt1.FindControl("txtPriceWithDiscGV"), TextBox)

            Dim lblid11 As DropDownList = CType(txt1.FindControl("txtItemCodeGV"), DropDownList)
            Dim lblid12 As TextBox = CType(txt1.FindControl("txtItemDescriptionGV"), TextBox)
            Dim lblid13 As TextBox = CType(txt1.FindControl("txtOtherCodeGV"), TextBox)
            Dim lblid14 As TextBox = CType(txt1.FindControl("txtGLDescriptionGV"), TextBox)
            Dim lblid15 As TextBox = CType(txt1.FindControl("txtDescriptionGV"), TextBox)
            'Dim lblid16 As TextBox = CType(txt1.FindControl("txtServiceByGV"), TextBox)

            Dim lblid16 As TextBox = CType(txt1.FindControl("txtTaxTypeGV"), TextBox)
            Dim lblid17 As TextBox = CType(txt1.FindControl("txtGSTPercGV"), TextBox)

            lblAlert.Text = ""
            updPnlMsg.Update()



            '''''''''''''''''''''''''''''''''''
            If lblid2.Text = "SERVICE" Then
                'lblid0.Text = 0.0
                'lblid1.Text = 0.0
                lblid3.Text = 0.0
                'lblid8.Text = 0.0
                lblid9.Text = 0.0
                lblid10.Text = 0.0
                lblid4.Text = ""
                lblid5.Text = ""
                lblid6.Text = ""
                'lblid16.Text = ""
                lblid7.Text = ""

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
                commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where RefType = '" & lblid1.Text.Trim & "' order by rcno desc"
                commandIsInvDet.Connection = connIsInvDet

                Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                Dim dtIsInvDet As New DataTable
                dtIsInvDet.Load(drIsInvDet)

                If dtIsInvDet.Rows.Count > 0 Then
                    lService = dtIsInvDet.Rows(0)("InvoiceNumber").ToString
                    lblid0.Text = lService.Trim
                End If

                connIsInvDet.Close()
                connIsInvDet.Dispose()
                ''''''''''''''''''''''

                Dim IsServiceRecord As Boolean
                IsServiceRecord = False

                Dim connIsServiceRecord As MySqlConnection = New MySqlConnection()

                connIsServiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connIsServiceRecord.Open()

                Dim commandIsServiceRecord As MySqlCommand = New MySqlCommand
                commandIsServiceRecord.CommandType = CommandType.Text
                'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                commandIsServiceRecord.CommandText = "SELECT BillAmount,BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lblid1.Text.Trim & "'"

                commandIsServiceRecord.Connection = connIsServiceRecord

                Dim drIsServiceRecord As MySqlDataReader = commandIsServiceRecord.ExecuteReader()
                Dim dtIsServiceRecord As New DataTable
                dtIsServiceRecord.Load(drIsServiceRecord)

                If dtIsServiceRecord.Rows.Count > 0 Then
                    If String.IsNullOrEmpty(dtIsServiceRecord.Rows(0)("RecordNo").ToString) = True Then
                        lblid1.Text = ""
                        lblAlert.Text = "SERVICE RECORD NUMBER NOT FOUND"
                        updPnlMsg.Update()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub
                    Else
                        If ddlDocType.Text = "ARCN" Then
                            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                                lblid1.Text = ""
                                lblAlert.Text = "SERVICE RECORD HAS NOT BEEN BILLED"
                                updPnlMsg.Update()
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                                Exit Sub
                            End If
                        ElseIf ddlDocType.Text = "ARDN" Then
                            If Convert.ToDecimal(dtIsServiceRecord.Rows(0)("BilledAmt").ToString) = 0.0 Then
                                lblid3.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                                lblid9.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                                lblid10.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                                GoTo populateOtherInfo
                            End If


                        End If
                        lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                        lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                        lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
populateOtherInfo:

                        lblid1.Text = dtIsServiceRecord.Rows(0)("RecordNo").ToString

                       

                        'If ddlDocType.Text = "CREDIT NOTE" Then
                        '    lblid3.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                        '    lblid9.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                        '    lblid10.Text = dtIsServiceRecord.Rows(0)("BilledAmt").ToString
                        'Else
                        '    lblid3.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                        '    lblid9.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                        '    lblid10.Text = dtIsServiceRecord.Rows(0)("BillAmount").ToString
                        'End If
                    
                        lblid5.Text = dtIsServiceRecord.Rows(0)("ContractNo").ToString
                        lblid6.Text = dtIsServiceRecord.Rows(0)("LocationID").ToString
                        lblid7.Text = Convert.ToDateTime(dtIsServiceRecord.Rows(0)("ServiceDate")).ToString("dd/MM/yyyy")

                        updpnlBillingDetails.Update()
                    End If
                Else
                    lblid1.Text = ""
                    lblAlert.Text = "INVALID INVOICE NUMBER"
                    updPnlMsg.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                    Exit Sub

                End If

                commandIsServiceRecord.Dispose()
                connIsServiceRecord.Close()
                connIsServiceRecord.Dispose()
            End If

        
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtInvoiceNoGV_TextChanged", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "ImageButton8_Click", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "txtPopUpContractNo_TextChanged", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "gvPopUpContractNo_SelectedIndexChanged", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "gvPopUpContractNo_PageIndexChanging", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "ImageButton10_Click", ex.Message.ToString, "")
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
        Catch ex As Exception

        End Try
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


            strsql = "SELECT ClaimID, ProjectCode, ClaimDate, Status, AccountId,  CustName FROM tblprogressclaim FROM tblsales WHERE 1=1   "

            'txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  "

            'txtCondition.Text = " and (DocType='ARCN' or DocType='ARDN')  "


            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'txtCondition.Text = txtCondition.Text + " and Location = '" & txtLocation.Text & "'"
                txtCondition.Text = txtCondition.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            End If


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


            txtCondition.Text = txtCondition.Text & " and PostStatus in (" & (YrStr) & ") "


            If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and ClaimID like '%" & txtSearchInvoiceNo.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtSearchAccountID.Text) = False Then
                'strsql = strsql & " and (AccountId like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
                txtCondition.Text = txtCondition.Text & " and (AccountId like '%" & txtSearchAccountID.Text.Trim & "%')"

            End If

            If String.IsNullOrEmpty(txtSearchClientName.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and CustName like ""%" & txtSearchClientName.Text.Trim & "%"""
            End If


            'If String.IsNullOrEmpty(txtSearchOurRef.Text) = False Then
            '    txtCondition.Text = txtCondition.Text & " and OurRef like ""%" & txtSearchOurRef.Text.Trim & "%"""
            'End If

            'If String.IsNullOrEmpty(txtSearchYourRef.Text) = False Then
            '    txtCondition.Text = txtCondition.Text & " and YourRef like ""%" & txtSearchYourRef.Text.Trim & "%"""
            'End If

            'If String.IsNullOrEmpty(txtSearchPONo.Text) = False Then
            '    txtCondition.Text = txtCondition.Text & " and PoNo like ""%" & txtSearchPONo.Text.Trim & "%"""
            'End If

            'If (ddlCompanyGrpSearch.SelectedIndex > 0) Then
            '    txtCondition.Text = txtCondition.Text & " and CompanyGroup like '%" & ddlCompanyGrpSearch.Text.Trim + "%'"
            'End If

            'If String.IsNullOrEmpty(txtBillSchedule.Text) = False Then
            '    strsql = strsql & " and BillSchedule like '%" & txtBillSchedule.Text.Trim + "%'"
            'End If


            'If (ddlSearchSalesman.SelectedIndex > 0) Then
            '    txtCondition.Text = txtCondition.Text & " and StaffCode like '%" & ddlSearchSalesman.Text.Trim + "%'"
            'End If


            'If rdbSearchPaidStatus0.SelectedItem.Value = "Fully Paid" Then
            '    txtCondition.Text = txtCondition.Text + " and BalanceBase = 0 and ValueBase <> 0 "
            'ElseIf rdbSearchPaidStatus0.SelectedItem.Value = "O/S" Then
            '    txtCondition.Text = txtCondition.Text + " and BalanceBase <>  0"
            'End If


            If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ClaimDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ClaimDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ClaimDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ClaimDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtSearchEntryDateFrom.Text) = False And txtSearchEntryDateFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and CreatedOn >= '" + Convert.ToDateTime(txtSearchEntryDateFrom.Text).ToString("yyyy-MM-dd") + " 00:00:00'"
            End If
            If String.IsNullOrEmpty(txtSearchEntryDateTo.Text) = False And txtSearchEntryDateTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and CreatedOn <= '" + Convert.ToDateTime(txtSearchEntryDateTo.Text).ToString("yyyy-MM-dd") + " 23:59:59'"
            End If


            If String.IsNullOrEmpty(txtSearchEditEndFrom.Text) = False And txtSearchEditEndFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and LastModifiedOn >= '" + Convert.ToDateTime(txtSearchEditEndFrom.Text).ToString("yyyy-MM-dd") + " 00:00:00'"
            End If
            If String.IsNullOrEmpty(txtSearchEditEndTo.Text) = False And txtSearchEditEndTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and LastModifiedOn <= '" + Convert.ToDateTime(txtSearchEditEndTo.Text).ToString("yyyy-MM-dd") + " 23:59:59'"
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
            InsertIntoTblWebEventLog("CN - " + Session("UserID"), "btnSearch_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnClient2_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient2.Click
        lblAlert.Text = ""
        'lblAlert1.Text = ""
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
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"" or  B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlSearchContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlSearchContactType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"" or D.PersonID like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"" or  B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Status = 'O' and C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"" or  D.PersonID like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                End If

                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelCN.Update()
            Else
                txtPopUpClient.Text = ""


                If ddlSearchContactType.Text = "CORPORATE" Or ddlSearchContactType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlSearchContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlSearchContactType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')   order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '')  UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by AccountID,  LocationId, ServiceName"
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
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "btnClient2_Click", ex.Message.ToString, "")
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


    

    Protected Sub grvBillingDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grvBillingDetails.SelectedIndexChanged

    End Sub

    Protected Sub btnMultiPrint_Click(sender As Object, e As EventArgs) Handles btnMultiPrint.Click
        Dim strsql As String
        strsql = ""

        strsql = strsql + txt.Text
        'txt.Text = strsql

        SqlDSMultiPrint.SelectCommand = strsql

        'SqlDSMultiPrint.SelectCommand = txt.Text


        SqlDSMultiPrint.DataBind()
        grdViewMultiPrint.DataSourceID = "SqlDSMultiPrint"
        grdViewMultiPrint.DataBind()

        mdlPopupMultiPrint.Show()
    End Sub

    Protected Sub txtClientNameSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientNameSearch.TextChanged

    End Sub

    Protected Sub chkShowUnprinted_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowUnprinted.CheckedChanged
        Dim strsql As String
        strsql = ""
        'If chkShowUnprinted.Checked = True Then
        '    strsql = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, Billcity, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman AS Expr1, Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, custAddBuilding, CustAddCountry, CustAddPostal, CustAddCity, CustAddStreet, PrintCounter, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno, BillSchedule FROM tblsales WHERE 1=1  "
        '    strsql = strsql + " and (DocType ='ARIN') AND PrintCounter > 0 AND  (PostStatus = 'O' OR PostStatus = 'P') and GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) "
        '    strsql = strsql + " ORDER BY Rcno DESC, CustName"
        '    strsql = strsql + " limit " & txtLimit.Text
        'Else
        '    'strsql = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, Billcity, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman AS Expr1, Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, custAddBuilding, CustAddCountry, CustAddPostal, CustAddCity, CustAddStreet, PrintCounter, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno, BillSchedule FROM tblsales WHERE 1=1  "
        '    'strsql = strsql + " and (DocType ='ARIN') AND ((PrintCounter = '0') or (PrintCounter is null) or (PrintCounter = '')) AND (PostStatus = 'O' OR PostStatus = 'P')  "
        '    'strsql = strsql + " ORDER BY Rcno DESC, CustName"
        '    'strsql = strsql + " limit " & txtLimit.Text

        '    strsql = txt.Text

        'End If

        '''''''''''''''''''

        strsql = "Select PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, Billcity,  "
        'strsql = strsql & "AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , CreditDays, RecurringInvoice, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno
        strsql = strsql & " ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, yourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , TermsDay, RecurringInvoice,  BillSchedule, "
        strsql = strsql & " ReceiptBase, CreditBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, CustAddStreet, custAddBuilding,  CustAddCity, PrintCounter,"
        strsql = strsql & " CreatedBy,   CreatedOn, LastModifiedBy, LastModifiedOn, Rcno, Description "
        strsql = strsql & " from tblSales where 1=1 "

        strsql = strsql & " and ((DocType='ARCN') or (DocType='ARDN'))  "

        'If chkShowUnprinted.Checked = False Then

        '    strsql = strsql & txtCondition.Text & " AND PrintCounter > 0 "

        'Else

        '    strsql = strsql & txtCondition.Text & " AND ((PrintCounter = '0') or (PrintCounter is null) or (PrintCounter = ''))  "
        'End If


        strsql = strsql + txtOrderBy.Text + " limit " & txtLimit.Text


        ''''''''''''''''''


        SqlDSMultiPrint.SelectCommand = strsql
        grdViewMultiPrint.DataSourceID = "SqlDSMultiPrint"
        grdViewMultiPrint.DataBind()

        mdlPopupMultiPrint.Show()
    End Sub

    Protected Sub btnPrintMultiPrint_Click(sender As Object, e As EventArgs) Handles btnPrintMultiPrint.Click
        lblAlert.Text = ""
        Dim invcheck As String = ""
        Session("PrintType") = "MultiPrint"

        Session("gridsqlinvoiceprint") = txt.Text
        Session("servicefrom") = "serviceprint"
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        If grdViewMultiPrint.Rows.Count > 0 Then
            For Each row As GridViewRow In grdViewMultiPrint.Rows

                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelectMultiPrintGV"), CheckBox)
                    If chkRow.Checked Then
                        YrStrList.Add("""" + TryCast(row.Cells(1).FindControl("lblInvNo"), Label).Text() + """")

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'Dim conn As MySqlConnection = New MySqlConnection()

                        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn.Open()

                        'Dim sql As String
                        'sql = ""
                        'sql = "Select PrintCounter from tblSales where InvoiceNumber = '" & TryCast(row.Cells(1).FindControl("lblInvNo"), Label).Text() & "'"

                        'Dim command1 As MySqlCommand = New MySqlCommand
                        'command1.CommandType = CommandType.Text
                        'command1.CommandText = sql
                        'command1.Connection = conn

                        'Dim dr As MySqlDataReader = command1.ExecuteReader()

                        'Dim dt As New DataTable
                        'dt.Load(dr)

                        'Dim lPrincounter As Integer
                        'lPrincounter = 0

                        'If dt.Rows.Count > 0 Then
                        '    If dt.Rows(0)("PrintCounter").ToString <> "" Then : lPrincounter = Convert.ToInt32(dt.Rows(0)("PrintCounter").ToString) : End If
                        'End If

                        'command1.Dispose()


                        ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'lPrincounter = lPrincounter + 1

                        'Dim qry As String
                        'qry = ""
                        ''Dim conn As MySqlConnection = New MySqlConnection()
                        'Dim command As MySqlCommand = New MySqlCommand
                        'command.CommandType = CommandType.Text

                        ''conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        ''conn.Open()

                        'qry = "Update tblSales set    "
                        ''qry = qry + " PrintCounter = @PrintCounter, LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn "
                        'qry = qry + " PrintCounter = @PrintCounter "
                        'qry = qry + " where InvoiceNumber = '" & TryCast(row.Cells(1).FindControl("lblInvNo"), Label).Text() & "'"

                        'command.CommandText = qry
                        'command.Parameters.Clear()
                        ''command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
                        'command.Parameters.AddWithValue("@PrintCounter", lPrincounter)
                        ''command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        ''command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Connection = conn
                        'command.ExecuteNonQuery()

                        'conn.Close()
                        'conn.Dispose()
                        'command.Dispose()
                        ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        'SQLDSInvoice.SelectCommand = txt.Text
                        'GridView1.DataSourceID = "SQLDSInvoice"
                        'GridView1.DataBind()
                        '''''''''''''''''''''''''''''''''''''''''''''''''''

                    End If
                End If
            Next
            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
            If String.IsNullOrEmpty(YrStr) = False Then
                invcheck = YrStr
            Else
                lblAlert.Text = "SELECT RECORD TO PRINT"
                Return

            End If
            Session("RecordNo") = invcheck

        End If
        'Session("InvoiceNo") = "CN/DN"


        'Session.Add("RecordNo", txtCNNo.Text)
        Session.Add("Title", ddlDocType.SelectedItem.Text)
        mdlConfirmMultiPrint.Show()


        ' btn.Attributes.Add("onclick")
        ''mdlPopupPrint.Show()
    End Sub

  
   
    Protected Sub grvServiceRecDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grvServiceRecDetails.SelectedIndexChanged

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

    Protected Sub btnEditBillingNameSave_Click(sender As Object, e As EventArgs) Handles btnEditBillingNameSave.Click
        Try

            Dim qry As String
            qry = ""
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            qry = "Update tblSales set    "
            qry = qry + " CustName = @CustName, LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn "
            qry = qry + " where Rcno = @Rcno;"

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
            command.Parameters.AddWithValue("@CustName", txtBillingNameEdit.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Connection = conn
            command.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            command.Dispose()

            SQLDSCN.SelectCommand = txt.Text
            GridView1.DataSourceID = "SQLDSCN"
            GridView1.DataBind()

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtInvoiceNo.Text, "EDITBILLINGNAME", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, txtAccountName.Text + " - " + txtBillingNameEdit.Text.ToUpper, txtRcno.Text)
            txtAccountName.Text = txtBillingNameEdit.Text.ToUpper

            'InsertNewLog()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnEditBillingNameSave_Click", ex.Message.ToString, txtInvoiceNo.Text)
        End Try
    End Sub

    Protected Sub btnEditBillingDetailsSave_Click(sender As Object, e As EventArgs) Handles btnEditBillingDetailsSave.Click
        Try

            If String.IsNullOrEmpty(txtBillAddressEdit.Text.Trim) = True Then
                lblAlertBillingDetails.Text = "PLEASE ENTER BILLING ADDRESS (1ST LINE)"
                updPnlMsg.Update()
                'btnSaveInvoice.Enabled = True
                mdlPopupEditBillingDetails.Show()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtBillCountryEdit.Text.Trim) = True Then
                lblAlertBillingDetails.Text = "PLEASE ENTER BILL COUNTRY"
                updPnlMsg.Update()
                'btnSaveInvoice.Enabled = True
                mdlPopupEditBillingDetails.Show()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            Dim qry As String
            qry = ""
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            qry = "Update tblSales set    "
            qry = qry + " CustAttention = @CustAttention,   "
            qry = qry + " CustAddress1 = @CustAddress1,   CustAddStreet = @CustAddStreet, CustAddBuilding=@CustAddBuilding, CustAddCountry = @CustAddCountry, CustAddPostal=@CustAddPostal, LastModifiedBy = @LastModifiedBy, LastModifiedOn=@LastModifiedOn "
            qry = qry + " where Rcno = @Rcno;"

            command.CommandText = qry
            command.Parameters.Clear()
            command.Parameters.AddWithValue("@Rcno", txtRcno.Text)
            command.Parameters.AddWithValue("@CustAttention", txtContactPersonEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@CustAddress1", txtBillAddressEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@CustAddBuilding", txtBillBuildingEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@CustAddStreet", txtBillStreetEdit.Text.ToUpper())

            command.Parameters.AddWithValue("@CustAddCity", txtBillCityEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@CustAddState", txtBillStateEdit.Text.ToUpper())

            command.Parameters.AddWithValue("@CustAddPostal", txtBillPostalEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@CustAddCountry", txtBillCountryEdit.Text.ToUpper())
            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
            command.Connection = conn
            command.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()
            command.Dispose()
            txtContactPerson.Text = txtContactPersonEdit.Text.ToUpper()
            txtBillAddress.Text = txtBillAddressEdit.Text.ToUpper()
            txtBillBuilding.Text = txtBillBuildingEdit.Text.ToUpper()
            txtBillStreet.Text = txtBillStreetEdit.Text.ToUpper()

            txtBillCity.Text = txtBillCityEdit.Text.ToUpper()
            txtBillState.Text = txtBillStateEdit.Text.ToUpper()

            txtBillPostal.Text = txtBillPostalEdit.Text.ToUpper()
            txtBillCountry.Text = txtBillCountryEdit.Text.ToUpper()

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CNOTE", txtInvoiceNo.Text, "EDITBILLINGDETAILS", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'InsertNewLog()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "btnEditBillingDetailsSave_Click", ex.Message.ToString, txtInvoiceNo.Text)
        End Try
    End Sub

    Protected Sub btnEditBillingDetails_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditBillingDetails.Click
        lblMessage.Text = ""
        lblAlert.Text = ""

        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT BILLING DEATAILS"
            Return

        End If
        mdlPopupEditBillingDetails.Show()
    End Sub

    Protected Sub btnEditBillingName_Click(sender As Object, e As ImageClickEventArgs) Handles btnEditBillingName.Click
        lblMessage.Text = ""
        lblAlert.Text = ""

        If txtRcno.Text = "" Then
            ' MessageBox.Message.Alert(Page, "Select a record to delete!!!", "str")
            lblAlert.Text = "SELECT RECORD TO EDIT BILLING DEATAILS"
            Return

        End If
        mdlPopupEditBillingName.Show()
    End Sub

    Private Sub UpdateGStCode()
        Try

            Dim TotalAmt As Decimal = 0
            Dim TotalDiscAmt As Decimal = 0
            Dim TotalWithDiscAmt As Decimal = 0
            Dim TotalGSTAmt As Decimal = 0
            Dim TotalAmtWithGST As Decimal = 0
            Dim GSTableGVB As Decimal = 0.0
            Dim GSTGVB As Decimal = 0.0
            Dim GSTGV As Decimal = 0.0

            Dim lGSTadjustedRecNo As Integer
            Dim lGSTadjustedRecNoNew As Integer

            Dim totalrecords As Integer = 0

            Dim GSTGVBNew As Decimal = 0.0
            Dim GSTGVNew As Decimal = 0.0


            lGSTadjustedRecNo = 0
            lGSTadjustedRecNoNew = 0

            'If txtMode.Text = "EDIT" Then
            'txtInvoiceAmount.Text = "0.00"
            'txtDiscountAmount.Text = "0.00"
            'txtAmountWithDiscount.Text = "0.00"
            'txtGSTAmount.Text = "0.00"
            'txtNetAmount.Text = "0.00"
            'End If

            ''''''''''''''''''''''''''''''''start Modification'''''''''''''''''''''''''''''''''''''

            'SetRowDataBillingDetailsRecs()
            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim GSTable As Decimal = 0.0

            If (table.Rows.Count > 0) Then

                For i As Integer = (table.Rows.Count) - 1 To 0 Step -1
                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtItemTypeGV"), DropDownList)

                    If TextBoxItemType.SelectedValue <> "-1" Then

                        Dim TextBoxTaxCode As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTaxTypeGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtGSTPercGV"), TextBox)

                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        'Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)

                        TextBoxGSTAmt.Text = (Convert.ToDecimal(TextBoxPriceWithDisc.Text) * Convert.ToDecimal(TextBoxGSTPerc.Text) * 0.01).ToString("N2")
                        TextBoxTotalPriceWithGST.Text = (Convert.ToDecimal(TextBoxPriceWithDisc.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")

                        GSTGV = GSTGV + Convert.ToDecimal(TextBoxGSTAmt.Text)

                        'totalrecords = totalrecords + 1
                    End If
                Next i
            End If



            '' start of GVB
            Dim gvbRecords, j As Long
            gvbRecords = 0

            If txtMode.Text = "EDIT" Then

                gvbRecords = grvBillingDetailsNew.Rows.Count()

                For j = gvbRecords - 1 To 0 Step -1


                    Dim lblidItemTypeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtItemTypeGVB"), TextBox)
                    Dim lblidOtherCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).FindControl("txtOtherCodeGVB"), TextBox)


                    If String.IsNullOrEmpty(lblidOtherCodeGVB.Text) = False Then

                        Dim TextBoxTaxCodeGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTaxTypeGVB"), TextBox)
                        Dim TextBoxGSTPercGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtGSTPercGVB"), TextBox)
                        Dim TextBoxGSTAmtGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtGSTAmtGVB"), TextBox)
                        Dim TextBoxTotalPriceWithGSTGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtTotalPriceWithGSTGVB"), TextBox)
                        Dim TextBoxPriceWithDiscGVB As TextBox = CType(grvBillingDetailsNew.Rows(j).Cells(0).FindControl("txtPriceWithDiscGVB"), TextBox)


                        If String.IsNullOrEmpty(TextBoxGSTAmtGVB.Text) = True Then
                            TextBoxGSTAmtGVB.Text = "0.00"
                        End If

                        If String.IsNullOrEmpty(TextBoxTotalPriceWithGSTGVB.Text) = True Then
                            TextBoxTotalPriceWithGSTGVB.Text = "0.00"
                        End If


                        TextBoxGSTAmtGVB.Text = (Convert.ToDecimal(TextBoxPriceWithDiscGVB.Text) * Convert.ToDecimal(TextBoxGSTPercGVB.Text) * 0.01).ToString("N2")
                        TextBoxTotalPriceWithGSTGVB.Text = (Convert.ToDecimal(TextBoxPriceWithDiscGVB.Text) + Convert.ToDecimal(TextBoxGSTAmtGVB.Text)).ToString("N2")


                        GSTGVBNew = GSTGVBNew + Convert.ToDecimal(TextBoxGSTAmtGVB.Text)

                        'txtInvoiceAmount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(TextBoxTotalPriceGVB.Text)).ToString("N2")
                        'txtDiscountAmount.Text = (Convert.ToDecimal(txtDiscountAmount.Text) + Convert.ToDecimal(TextBoxDiscAmountGVB.Text)).ToString("N2")
                        'txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
                        'GSTGVB = GSTGVB + Convert.ToDecimal(TextBoxGSTAmtGVB.Text)

                        'totalrecords = totalrecords + 1
                    End If
                Next
            End If

            txtTotalGSTAmt.Text = (Convert.ToDecimal(GSTGV + GSTGVBNew)).ToString("N2")
            txtTotalWithGST.Text = (Convert.ToDecimal(txtTotalWithDiscAmt.Text) + txtTotalGSTAmt.Text).ToString("N2")

            ''totalrecords = totalrecords + gvbRecords

            '' '' end of GVB
            ' ''''''''''''''''''''''''''''''''''''end Modification ''''''''''''''''''''''''''''''''


            ''txtGSTAmount.Text = Convert.ToDecimal(GSTGVB + GSTGV).ToString("N2")

            'txtGSTAmount.Text = Convert.ToDecimal(Convert.ToDecimal(txtAmountWithDiscount.Text) * Convert.ToDecimal(txtGST1.Text) * 0.01).ToString("N2")
            'txtNetAmount.Text = Convert.ToDecimal(txtAmountWithDiscount.Text) + Convert.ToDecimal(txtGSTAmount.Text)

            ''txtGSTAmount.Text = Convert.ToDecimal(GSTGVBNew + GSTGVNew).ToString("N2")
            ' ''''''''''''''''''''''''''

            'Dim GSTDiff As Decimal
            'GSTDiff = 0.0

            'Dim GSTCalc As Decimal
            'GSTCalc = 0.0
            ''GSTCalc = Convert.ToDecimal((GSTable + GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")

            'GSTCalc = GSTGVB + GSTGV
            'GSTDiff = Convert.ToDecimal(txtGSTAmount.Text) - GSTCalc
            ''GSTDiff = ((GSTable + GSTableGVB) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01) - Convert.ToDecimal(txtGSTAmount.Text)

            'If GSTDiff <> 0.0 Then

            '    'txtGSTAmount.Text = Convert.ToDecimal(Convert.ToDecimal(txtGSTAmount.Text) + GSTDiff.ToString("N2")).ToString("N2")

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

            '    txtTotalGSTAmt.Text = (Convert.ToDecimal(txtTotalGSTAmt.Text) + GSTDiff).ToString("N2")
            '    txtTotalWithGST.Text = (Convert.ToDecimal(txtTotalWithGST.Text) + GSTDiff).ToString("N2")
            'End If

            ''''''''''''''''''''''''''''


            'UpdatePanel3.Update()

            UpdatePanel2.Update()
            'UpdatePanel5.Update()
            updpnlBillingDetails.Update()

            updPanelSave.Update()

            table.Dispose()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr

            InsertIntoTblWebEventLog("CN - " + Session("UserID"), "FUNCTION UpdateGStCode", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnConfirmYesUpdateGSTCode_Click(sender As Object, e As EventArgs) Handles btnConfirmYesUpdateGSTCode.Click
        Dim sql As String
        sql = ""
        'sql = "Select TaxRatePct from tbltaxtype where TaxType = '" & txtGST.Text & "'"

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = sql
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(dr)

     

        UpdateGStCode()

        conn.Close()
        conn.Dispose()
        command1.Dispose()
        dt.Dispose()
        dr.Close()
    End Sub

    Public Sub UpdateCNBal(InvNo As String)
        Try


            Dim conn As MySqlConnection = New MySqlConnection()
            '''''''''''''''''''''''''''''''''''''''

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim qryAR As String
            Dim commandAR As MySqlCommand = New MySqlCommand
            commandAR.CommandType = CommandType.Text


            'Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            commandValues.CommandText = "SELECT SourceInvoice, RefType, ValueBase, SubCode  FROM tblSalesDetail where InvoiceNumber ='" & InvNo.Trim & "'"
            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)

            Dim lTotalReceiptAmt As Decimal
            Dim lInvoiceAmt As Decimal
            'Dim lReceptAmtAdjusted As Decimal

            lTotalReceiptAmt = 0.0
            lInvoiceAmt = 0.0

            '''''''''''''''''''''''''''''''''''''''
            Dim cmdUpdateCNBalance As MySqlCommand = New MySqlCommand
            cmdUpdateCNBalance.CommandType = CommandType.Text

            ''cmdUpdateCNBalance.CommandText = "UPDATE tblSales SET BalanceBase = " & Convert.ToDecimal(txtCNNetAmount.Text) & " - (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblSalesDetail A  " & _
            ''      " WHERE A.SubCode = 'SERVICE' AND InvoiceNumber = '" & InvNo & "') WHERE PostStatus = 'P' and InvoiceNumber = '" & InvNo & "'"

            'cmdUpdateCNBalance.CommandText = "UPDATE tblSales SET BalanceBase = " & Convert.ToDecimal(txtCNNetAmount.Text) & " - (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblSalesDetail A  " & _
            ' " WHERE  InvoiceNumber = '" & InvNo & "' and ((SourceInvoice is not null) and (SourceInvoice <>''))) WHERE PostStatus = 'P' and InvoiceNumber = '" & InvNo & "'"

            cmdUpdateCNBalance.Connection = conn
            cmdUpdateCNBalance.ExecuteNonQuery()
            cmdUpdateCNBalance.Dispose()

            '''''''''''''''''''''''''''''''''''''''

            'GridView1.DataBind()
            conn.Close()
            conn.Dispose()

            commandAR.Dispose()
            cmdUpdateCNBalance.Dispose()
            commandValues.Dispose()


        Catch ex As Exception
            'InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "ReCalculate", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
   

    Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
        Try


            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
                lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
                Return
            End If

            lblMessage.Text = ""
            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""

            Dim btn1 As Button = DirectCast(sender, Button)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex

            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno

            GridView1.SelectedIndex = rowindex1

            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(4).Text
            txtLogDocNo.Text = strRecordNo
         
            sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()

            mdlViewEditHistory.Show()

            updPanelCN.Update()
        Catch ex As Exception
            InsertIntoTblWebEventLog("CN - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, txtRcno.Text)

        End Try

    End Sub

    Protected Sub grdViewEditHistory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdViewEditHistory.PageIndexChanging
        Try
            grdViewEditHistory.PageIndex = e.NewPageIndex

            sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & txtLogDocNo.Text & "' order by logdate desc"
            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()
            mdlViewEditHistory.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "GridView1_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub


    'Start: Billing Details Grid


    Private Sub FirstGridViewRowBillingDetailsRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing

            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            'dt.Columns.Add(New DataColumn("SelRec", GetType(String)))

            dt.Columns.Add(New DataColumn("ContractType", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractNo", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("CompletedValue", GetType(String)))
            dt.Columns.Add(New DataColumn("BalanceValue", GetType(String)))
            dt.Columns.Add(New DataColumn("PreviousClaimedAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("CurrentClaimedAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("RetentionPerc", GetType(String)))
            dt.Columns.Add(New DataColumn("RetentionAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalCurrentClaim", GetType(String)))
            dt.Columns.Add(New DataColumn("BilledAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("BalanceNotClaimed", GetType(String)))

            dr = dt.NewRow()

            dr("ContractType") = String.Empty
            dr("ContractNo") = String.Empty
            dr("ContractAmount") = 0
            dr("CompletedValue") = 0.0
            dr("BalanceValue") = 0
            dr("PreviousClaimedAmount") = 0.0
            dr("CurrentClaimedAmount") = 0.0
            dr("RetentionPerc") = 0
            dr("RetentionAmount") = 0
            dr("TotalCurrentClaim") = String.Empty
            dr("BilledAmount") = 0.0
            dr("BalanceNotClaimed") = 0

            dt.Rows.Add(dr)

            ViewState("CurrentTableBillingDetailsRec") = dt

            grvBillingDetails.DataSource = dt
            grvBillingDetails.DataBind()

            Dim btnAdd As Button = CType(grvBillingDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "FirstGridViewRowBillingDetailsRecs", ex.Message.ToString, "")
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
                        Dim TextBoxContractType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractTypeGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtContractAmountGV"), TextBox)
                        Dim TextBoxCompletedValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtCompletedValueGV"), TextBox)
                        Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtBalanceValueGV"), TextBox)
                        Dim TextBoxPreviousClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                        Dim TextBoxCurrentClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtCurrentClaimedAmountGV"), TextBox)
                        Dim TextBoxRetentionPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtRetentionPercGV"), TextBox)
                        Dim TextBoxRetentionAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRetentionAmountGV"), TextBox)
                        Dim TextBoxTotalCurrentClaim As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalCurrentClaimGV"), TextBox)
                        Dim TextBoxBilledAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtBilledAmountGV"), TextBox)
                        Dim TextBoxBalanceNotClaimed As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtBalanceNotClaimedGV"), TextBox)
                      
                        drCurrentRow = dtCurrentTable.NewRow()

                        TextBoxRetentionPerc.Text = "0.00"

                        dtCurrentTable.Rows(i - 1)("ContractType") = TextBoxContractType.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractAmount") = TextBoxContractAmount.Text
                        dtCurrentTable.Rows(i - 1)("CompletedValue") = TextBoxCompletedValue.Text
                        dtCurrentTable.Rows(i - 1)("BalanceValue") = TextBoxBalanceValue.Text
                        dtCurrentTable.Rows(i - 1)("PreviousClaimedAmount") = TextBoxPreviousClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("CurrentClaimedAmount") = TextBoxCurrentClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("RetentionPerc") = TextBoxRetentionPerc.Text
                        dtCurrentTable.Rows(i - 1)("RetentionAmount") = TextBoxTotalCurrentClaim.Text
                        dtCurrentTable.Rows(i - 1)("TotalCurrentClaim") = TextBoxBilledAmount.Text
                        dtCurrentTable.Rows(i - 1)("BilledAmount") = TextBoxBalanceNotClaimed.Text
                        dtCurrentTable.Rows(i - 1)("BalanceNotClaimed") = TextBoxBalanceNotClaimed.Text

                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable

                    grvBillingDetails.DataSource = dtCurrentTable
                    grvBillingDetails.DataBind()

                    Dim rowIndex2 As Integer = 0
                    Dim j As Integer = 1
                    'Do While j <= (rowIndex)

                    '    'Dim TextBoxTargetDesc1 As TextBox = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtTaxTypeGV"), TextBox)

                    '    'Query = "Select TaxType from tbltaxtype"
                    '    'PopulateDropDownList(Query, "TaxType", "TaxType", TextBoxTargetDesc1)

                    '    Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtItemCodeGV"), DropDownList)

                    '    TextBoxItemCode1.Items.Clear()
                    '    Query = "Select * from tblbillingproducts  "
                    '    PopulateDropDownList(Query, "Description", "Description", TextBoxItemCode1)

                    '    'Dim Query As String
                    '    'Query = "Select * from tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                    '    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                    '    Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtUOMGV"), DropDownList)
                    '    Query = "Select * from tblunitms order by UnitMs"
                    '    PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


                    '    'Dim TextBoxRecordNo As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtServiceRecordGV"), DropDownList)
                    '    'Query = "Select RecordNo from tblServiceRecord where  AccountId = '" & txtAccountIdBilling.Text & "' and (Status ='O' or Status ='P') order by RecordNo"
                    '    'PopulateDropDownList(Query, "RecordNo", "RecordNo", TextBoxRecordNo)

                    '    Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    '    Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                    '    Dim TextBoxItemDescription1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)

                    '    Dim TextBoxInvoiceNo1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    '    Dim TextBoxContractNo1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtContractNoGV"), TextBox)
                    '    Dim TextBoxItemServiceNo1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceRecordGV"), TextBox)


                    '    'If TextBoxItemType1.Text = "SERVICE" Then
                    '    '    TextBoxQty1.Enabled = False
                    '    '    TextBoxItemCode1.Enabled = False

                    '    '    TextBoxInvoiceNo1.Enabled = True
                    '    '    TextBoxContractNo1.Enabled = True
                    '    '    TextBoxItemServiceNo1.Enabled = True
                    '    'Else
                    '    '    TextBoxInvoiceNo1.Enabled = False
                    '    '    TextBoxContractNo1.Enabled = False
                    '    '    TextBoxItemServiceNo1.Enabled = False
                    '    'End If

                    '    'If TextBoxItemType1.Text = "SERVICE" Then
                    '    '    TextBoxQty1.Enabled = False
                    '    '    TextBoxItemCode1.Enabled = False

                    '    '    TextBoxInvoiceNo1.Enabled = True
                    '    '    TextBoxContractNo1.Enabled = True
                    '    '    TextBoxItemServiceNo1.Enabled = True
                    '    'Else
                    '    '    TextBoxInvoiceNo1.Enabled = False
                    '    '    TextBoxContractNo1.Enabled = False
                    '    '    TextBoxItemServiceNo1.Enabled = False
                    '    'End If

                    '    'If ddl1.Text = "OTHERS" Then
                    '    '    lblid2.Enabled = False
                    '    '    lblid3.Enabled = False
                    '    '    lblid4.Enabled = False
                    '    'Else
                    '    '    lblid2.Enabled = True
                    '    '    lblid3.Enabled = True
                    '    '    lblid4.Enabled = True
                    '    'End If
                    '    rowIndex2 += 1
                    '    j += 1
                    'Loop

                    'Dim TextBoxTargetDesc2 As DropDownList = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    'Query = "Select TargetId, descrip1 from tblTarget"
                    'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddNewRowWithDetailRecBillingDetailsRecs()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String
            If ViewState("CurrentTableBillingDetailsRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If TotDetailRecords > 0 Then
                    For i As Integer = 1 To (dtCurrentTable.Rows.Count)

                        Dim TextBoxContractType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractTypeGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtContractAmountGV"), TextBox)
                        Dim TextBoxCompletedValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtCompletedValueGV"), TextBox)
                        Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtBalanceValueGV"), TextBox)
                        Dim TextBoxPreviousClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                        Dim TextBoxCurrentClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtCurrentClaimedAmountGV"), TextBox)

                        Dim TextBoxRetentionPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtRetentionPercGV"), TextBox)
                        Dim TextBoxRetentionAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRetentionAmountGV"), TextBox)
                        Dim TextBoxTotalCurrentClaim As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalCurrentClaimGV"), TextBox)

                        Dim TextBoxBilledAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtBilledAmountGV"), TextBox)
                        Dim TextBoxBalanceNotClaimed As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtBalanceNotClaimedGV"), TextBox)



                        drCurrentRow = dtCurrentTable.NewRow()

                         dtCurrentTable.Rows(i - 1)("ContractType") = TextBoxContractType.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractAmount") = TextBoxContractAmount.Text
                        dtCurrentTable.Rows(i - 1)("CompletedValue") = TextBoxCompletedValue.Text
                        dtCurrentTable.Rows(i - 1)("BalanceValue") = TextBoxBalanceValue.Text
                        dtCurrentTable.Rows(i - 1)("PreviousClaimedAmount") = TextBoxPreviousClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("CurrentClaimedAmount") = TextBoxCurrentClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("RetentionPerc") = TextBoxRetentionPerc.Text
                        dtCurrentTable.Rows(i - 1)("RetentionAmount") = TextBoxTotalCurrentClaim.Text
                        dtCurrentTable.Rows(i - 1)("TotalCurrentClaim") = TextBoxBilledAmount.Text
                        dtCurrentTable.Rows(i - 1)("BilledAmount") = TextBoxBalanceNotClaimed.Text
                        dtCurrentTable.Rows(i - 1)("BalanceNotClaimed") = TextBoxBalanceNotClaimed.Text
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

                        Dim TextBoxContractType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractTypeGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtContractAmountGV"), TextBox)
                        Dim TextBoxCompletedValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtCompletedValueGV"), TextBox)
                        Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtBalanceValueGV"), TextBox)
                        Dim TextBoxPreviousClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                        Dim TextBoxCurrentClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtCurrentClaimedAmountGV"), TextBox)

                        Dim TextBoxRetentionPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtRetentionPercGV"), TextBox)
                        Dim TextBoxRetentionAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRetentionAmountGV"), TextBox)
                        Dim TextBoxTotalCurrentClaim As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalCurrentClaimGV"), TextBox)

                        Dim TextBoxBilledAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtBilledAmountGV"), TextBox)
                        Dim TextBoxBalanceNotClaimed As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtBalanceNotClaimedGV"), TextBox)


                        'TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()

                        TextBoxContractType.Text = dt.Rows(i)("ContractType").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxContractAmount.Text = dt.Rows(i)("ContractAmount").ToString()
                        TextBoxCompletedValue.Text = dt.Rows(i)("CompletedValue").ToString()

                        TextBoxBalanceValue.Text = dt.Rows(i)("BalanceValue").ToString()
                        TextBoxPreviousClaimedAmount.Text = dt.Rows(i)("PreviousClaimedAmount").ToString()
                        TextBoxCurrentClaimedAmount.Text = dt.Rows(i)("CurrentClaimedAmount").ToString()
                        TextBoxRetentionPerc.Text = dt.Rows(i)("RetentionPerc").ToString()
                        TextBoxTotalCurrentClaim.Text = dt.Rows(i)("RetentionAmount").ToString()
                        TextBoxBilledAmount.Text = dt.Rows(i)("TotalCurrentClaim").ToString()
                        TextBoxBalanceNotClaimed.Text = dt.Rows(i)("BilledAmount").ToString()
                        TextBoxBalanceNotClaimed.Text = dt.Rows(i)("BalanceNotClaimed").ToString()


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

                        Dim TextBoxContractType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractTypeGV"), DropDownList)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxContractAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtContractAmountGV"), TextBox)
                        Dim TextBoxCompletedValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtCompletedValueGV"), TextBox)
                        Dim TextBoxBalanceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtBalanceValueGV"), TextBox)
                        Dim TextBoxPreviousClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtPreviousClaimedAmountGV"), TextBox)
                        Dim TextBoxCurrentClaimedAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtCurrentClaimedAmountGV"), TextBox)

                        Dim TextBoxRetentionPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtRetentionPercGV"), TextBox)
                        Dim TextBoxRetentionAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtRetentionAmountGV"), TextBox)
                        Dim TextBoxTotalCurrentClaim As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalCurrentClaimGV"), TextBox)

                        Dim TextBoxBilledAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtBilledAmountGV"), TextBox)
                        Dim TextBoxBalanceNotClaimed As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtBalanceNotClaimedGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                           dtCurrentTable.Rows(i - 1)("ContractType") = TextBoxContractType.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractAmount") = TextBoxContractAmount.Text
                        dtCurrentTable.Rows(i - 1)("CompletedValue") = TextBoxCompletedValue.Text
                        dtCurrentTable.Rows(i - 1)("BalanceValue") = TextBoxBalanceValue.Text
                        dtCurrentTable.Rows(i - 1)("PreviousClaimedAmount") = TextBoxPreviousClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("CurrentClaimedAmount") = TextBoxCurrentClaimedAmount.Text
                        dtCurrentTable.Rows(i - 1)("RetentionPerc") = TextBoxRetentionPerc.Text
                        dtCurrentTable.Rows(i - 1)("RetentionAmount") = TextBoxTotalCurrentClaim.Text
                        dtCurrentTable.Rows(i - 1)("TotalCurrentClaim") = TextBoxBilledAmount.Text
                        dtCurrentTable.Rows(i - 1)("BilledAmount") = TextBoxBalanceNotClaimed.Text
                        dtCurrentTable.Rows(i - 1)("BalanceNotClaimed") = TextBoxBalanceNotClaimed.Text
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

End Class
