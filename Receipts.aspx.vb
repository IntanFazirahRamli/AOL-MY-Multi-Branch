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

Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel




Partial Class Receipts
    Inherits System.Web.UI.Page
    Public rcno As String
    Private Shared GridSelected As String = String.Empty
    Private Shared gScheduler, gddlvalue As String
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Dim client As String

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

    Protected Shared content As String
    Protected Shared inProcess As Boolean = False
    Protected Shared processComplete As Boolean = False
    Protected Shared processCompleteMsg As String = "Finished Processing All Records."

    Public IsSuccess As Boolean

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

    Protected Sub PopulateArCode()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim sql As String
            sql = ""

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
            sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"

            Dim commandAR1 As MySqlCommand = New MySqlCommand
            commandAR1.CommandType = CommandType.Text
            commandAR1.CommandText = sql
            commandAR1.Connection = conn

            Dim drAR1 As MySqlDataReader = commandAR1.ExecuteReader()

            Dim dtAR1 As New DataTable
            dtAR1.Load(drAR1)

            If dtAR1.Rows.Count > 0 Then
                If dtAR1.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dtAR1.Rows(0)("COACode").ToString : End If
                If dtAR1.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dtAR1.Rows(0)("Description").ToString : End If
            End If

            conn.Close()
            conn.Dispose()
            commandAR1.Dispose()
            dtAR1.Dispose()
            drAR1.Close()

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateArCode", ex.Message.ToString, "")
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            
            'Restrict users manual date entries

            txtReceiptNo.Attributes.Add("readonly", "readonly")
            txtAccountName.Attributes.Add("readonly", "readonly")

            txtReceiptPeriod.Attributes.Add("readonly", "readonly")
            'txtCompanyGroup.Attributes.Add("readonly", "readonly")
            ddlContactType.Attributes.Add("readonly", "readonly")
            txtBankGLCode.Attributes.Add("readonly", "readonly")

            txtSalesman.Attributes.Add("readonly", "readonly")
            txtBalance.Attributes.Add("readonly", "readonly")

            txtReceiptDate.Attributes.Add("readonly", "readonly")

            btnTop.Attributes.Add("onclick", "javascript:scroll(0,0);return false;")

            If Not Page.IsPostBack Then
                mdlPopUpClient.Hide()
                mdlImportServices.Hide()
                'mdlPopupLocation.Hide()
                mdlPopupGL.Hide()

                MakeMeNull()
                DisableControls()

                tb1.ActiveTabIndex = 0

                txtGroupAuthority.Text = Session("SecGroupAuthority")
                txt.Text = SQLDSReceipt.SelectCommand
                PopulateArCode()
                Dim Query As String

                'Dim Query As String
                Query = "Select StaffID from tblStaff where Status = 'O' and Roles <> 'TECHNICAL'"
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchEditedBy)
                PopulateDropDownList(Query, "StaffID", "StaffID", ddlSearchCreatedBy)

                'If txtDisplayRecordsLocationwise.Text = "N" Then
                '    Query = "Select BankID from tblBank order by BankID"
                '    PopulateDropDownList(Query, "BankID", "BankID", ddlBankIDSearch)
                '    PopulateDropDownList(Query, "BankID", "BankID", ddlSearchBankID)
                'Else
                '    Query = "Select BankID from tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
                '    PopulateDropDownList(Query, "BankID", "BankID", ddlBankIDSearch)
                '    PopulateDropDownList(Query, "BankID", "BankID", ddlSearchBankID)
                'End If

                Query = "Select concat(COACode, ' - ', Description) as CCode from tblChartofAccounts where ((GLType is null) or (GLType <> 'BANK')) order by COACode"
                PopulateDropDownList(Query, "CCode", "CCode", ddlCOACode)

                SqlDSGL.SelectCommand = "Select COACode, Description, GLType from tblchartofaccounts order by COACode"
                SqlDSGL.DataBind()

                txtSearch1Status.Text = "O,P"

                ''''''''''''''''''''''''''''''''''''''''''''''''
                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()
                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                commandServiceRecordMasterSetup.CommandText = "SELECT ShowReceiptOnScreenLoad, ReceiptRecordMaxRec,DisplayRecordsLocationWise,PostReceipt, ReceiptOnlyEditableByCreator,AutoEmailReceipt FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("ReceiptRecordMaxRec")
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtPostUponSave.Text = dtServiceRecordMasterSetup.Rows(0)("PostReceipt").ToString
                txtOnlyEditableByCreator.Text = dtServiceRecordMasterSetup.Rows(0)("ReceiptOnlyEditableByCreator").ToString
                If dtServiceRecordMasterSetup.Rows(0)("AutoEmailReceipt").ToString = "1" Then
                    txtAutoEmailReceipt.Text = "True"
                Else
                    txtAutoEmailReceipt.Text = "False"
                End If

                conn.Close()
                conn.Dispose()
                commandServiceRecordMasterSetup.Dispose()
                dtServiceRecordMasterSetup.Dispose()
                drServiceRecordMasterSetup.Close()
                ''''''''''''''''''''''''''''''''''''''''''

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    Query = "select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "' order by LocationID"
                    'PopulateDropDownList(query, "locationID", "locationID", txtLocation)
                    PopulateComboBoxSaplin(Query, "locationID", "locationID", ddlLocationSearch)

                    lblBranch1.Visible = True
                    txtLocation.Visible = True
                    lblBranch.Visible = True
                    ddlBranch.Visible = False
                    ddlLocationSearch.Visible = True
                    Query = "Select BankID from tblBank where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by BankID"
                    PopulateDropDownList(Query, "BankID", "BankID", ddlBankIDSearch)
                    PopulateDropDownList(Query, "BankID", "BankID", ddlSearchBankID)
                Else
                    lblBranch1.Visible = False
                    txtLocation.Visible = False
                    lblBranch.Visible = False
                    ddlBranch.Visible = False
                    ddlLocationSearch.Visible = False
                    Query = "Select BankID from tblBank order by BankID"
                    PopulateDropDownList(Query, "BankID", "BankID", ddlBankIDSearch)
                    PopulateDropDownList(Query, "BankID", "BankID", ddlSearchBankID)
                End If


                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    Query = "Select SettleType from tblsettletype where ((DefaultBank is not null) or (DefaultBank <> '')) and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') order by SettleType"
                Else
                    Query = "Select SettleType from tblsettletype where ((DefaultBank is not null) or (DefaultBank <> '')) order by sortorder"

                End If

                PopulateDropDownList(Query, "SettleType", "SettleType", ddlPaymentMode)
                PopulateDropDownList(Query, "SettleType", "SettleType", ddlPaymentModeSearch)
                PopulateDropDownList(Query, "SettleType", "SettleType", ddlSearchPaymentMode)
                '''''''''''''''''''''''''''''''''''
                '''''''''''''''''''''''''''''
                '07/07/2017 - CODE TO RETRIEVE DATA FROM TRANSACTIONS - SASI
                '''''''''''''''''''''''''''
                Session.Add("customerfrom", Request.QueryString("CustomerFrom"))
                Session.Add("receiptno", Request.QueryString("VoucherNumber"))
                If Request.QueryString("VoucherNumber") <> "" Then
                    '   btnADD_Click(sender, e)
                    '  txtReceiptNo.Text = Session("receiptno")
                    txtReceiptNoSearch.Text = Session("receiptno")
                    txtFrom.Text = Session("customerfrom")
                    btnBack.Visible = True
                    btnQuit.Visible = False
                    'txtSearch1Status.Text = "O,P"
                    btnQuickSearch_Click(sender, e)
                    '  GridView1.SelectedIndex = 0
                    '  RetrieveData()

                    'lblInvoiceId.Visible = True
                    'lblInvoiceId1.Text = Session("invoiceno")
                    Session.Remove("receiptno")
                    Session.Remove("customerfrom")
                    GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                Else
                    '''''''''''''''''''''''''''''
                    'END - 07/07/2017
                    '''''''''''''''''''''''
                    If Session("receiptfrom") = "invoice" Or Session("receiptfrom") = "invoicePB" Then
                        txtFrom.Text = Session("receiptfrom")
                        Session.Remove("receiptfrom")
                        lblInvoiceNo.Visible = True
                        lblInvoiceNo1.Text = Session("invoiceno")
                        txtInvoicenoSearch.Text = Session("invoiceno")
                        'sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman,  A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno FROM tblrecv A, tblrecvdet B where A.ReceiptNumber = B.ReceiptNumber and B.RefType  = '" & txtInvoicenoSearch.Text & "' ORDER BY Rcno DESC, ReceiptFrom"

                        sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, "
                        sqltext = sqltext + " A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, "
                        sqltext = sqltext + " A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman, A.Location, "
                        sqltext = sqltext + " A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno, A.Location, A.ChequeReturned "
                        sqltext = sqltext + " FROM tblrecv A, tblrecvdet B where A.ReceiptNumber = B.ReceiptNumber and B.RefType  = '" & txtInvoicenoSearch.Text & "'"

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            'sqltext = sqltext & " and Location = '" & txtLocation.Text & "'"
                            sqltext = sqltext & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                        End If

                        sqltext = sqltext + " ORDER BY Rcno DESC, ReceiptFrom"

                        SQLDSReceipt.SelectCommand = sqltext
                        btnBack.Visible = True
                        btnQuit.Visible = False

                        txtAccountIdBilling.Text = Session("AccountId")
                        txtAccountName.Text = Session("AccountName")
                        ddlContactType.Text = Session("ContactType")
                        txtCompanyGroup.Text = Session("CompanyGroup")
                        txtSalesman.Text = Session("Salesman")

                        'btnShowInvoices_Click(sender, e)
                        txtSearch1Status.Text = "O,P"
                        btnQuickSearch_Click(sender, e)
                    ElseIf Session("receiptfrom") = "invoiceQR" Then
                        txtFrom.Text = Session("receiptfrom")
                        Session.Remove("receiptfrom")
                        lblInvoiceNo.Visible = True
                        lblInvoiceNo1.Text = Session("invoiceno")

                        txtReceiptNoSearch.Text = Session("receiptno1")
                        'txtInvoicenoSearch.Text = Session("invoiceno")
                        sqltext = "SELECT A.PostStatus, A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, "
                        sqltext = sqltext + " A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, "
                        sqltext = sqltext + " A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman,  "
                        sqltext = sqltext + " A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno, A.Location, A.ChequeReturned "
                        sqltext = sqltext + " FROM tblrecv A where 1=1 "
                        sqltext = sqltext + " and A.ReceiptNumber  = '" & txtReceiptNoSearch.Text & "' "

                        If txtDisplayRecordsLocationwise.Text = "Y" Then
                            sqltext = sqltext & " and Location = '" & txtLocation.Text & "'"
                        End If

                        sqltext = sqltext + "  ORDER BY Rcno DESC, ReceiptFrom"

                        SQLDSReceipt.SelectCommand = sqltext
                        btnBack.Visible = True
                        btnQuit.Visible = False

                        txtAccountIdBilling.Text = Session("AccountId")
                        txtAccountName.Text = Session("AccountName")
                        ddlContactType.Text = Session("ContactType")
                        txtCompanyGroup.Text = Session("CompanyGroup")
                        txtSalesman.Text = Session("Salesman")

                        'btnShowInvoices_Click(sender, e)
                        txtSearch1Status.Text = "O,P"
                        btnQuickSearch_Click(sender, e)
                    Else

                        If Convert.ToBoolean(dtServiceRecordMasterSetup.Rows(0)("ShowReceiptOnScreenLoad")) = False Then
                            txt.Text = ""
                            SQLDSReceipt.SelectCommand = ""
                            GridView1.DataSourceID = "SQLDSReceipt"
                            GridView1.DataBind()
                        Else
                            txt.Text = "SELECT PostStatus, BankStatus, GlStatus, ReceiptNumber, ReceiptDate, AccountId, AppliedBase, GSTAmount, BaseAmount, ReceiptFrom, ReceiptDate, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, BankId,  LedgerCode, LedgerName, Comments, PaymentType, Salesman, Location, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno, ChequeReturned FROM tblrecv where 1=1 "
                            txtGrid.Text = "SELECT PostStatus,  ReceiptNumber, ReceiptDate, Cheque, AccountId, ContactType, ReceiptFrom, AppliedBase, BankId,  PaymentType, GlPeriod, CompanyGroup, ChequeDate, ChequeReturned, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn  FROM tblrecv where  1=1"


                            txtCondition.Text = " and (PostStatus = 'O' or PostStatus ='P') and  GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now())))  "

                            If txtDisplayRecordsLocationwise.Text = "Y" Then
                                txtCondition.Text = txtCondition.Text & " and Location = '" & txtLocation.Text & "'"
                            End If

                            txtOrderBy.Text = " ORDER BY Rcno DESC, ReceiptFrom"
                            txt.Text = txt.Text + txtCondition.Text + txtOrderBy.Text
                            sqltext = txt.Text
                            SQLDSReceipt.SelectCommand = sqltext
                            txtGrid.Text = txtGrid.Text + txtCondition.Text + txtOrderBy.Text
                            CalculateTotal()
                        End If
                    End If
                    txt.Text = sqltext

                    SQLDSReceipt.DataBind()
                    GridView1.DataBind()
                End If

                txtLocation.Attributes.Add("disabled", "true")
                txtCreatedBy.Text = Session("userid")
                'FindLocation()

                updPnlBillingRecs.Update()
            Else
                If txtIsPopup.Text = "Team" Then
                    txtIsPopup.Text = "N"
                    'mdlPopUpTeam.Show()
                ElseIf txtIsPopup.Text = "Client" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpClient.Show()
                End If

                If txtClientFrom.Text = "ImportClient" Then
                    'txtIsPopup.Text = "N"
                    mdlPopUpClient.Show()
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "Page_Load", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub CalculateTotal()
        Dim sqlstr As String
        sqlstr = ""
        Try

            If String.IsNullOrEmpty(txtInvoicenoSearch.Text) = True Then
                sqlstr = "SELECT ifnull(Sum(BaseAmount),0) as totalamount FROM tblrecv A where 1=1  " + txtCondition.Text
            Else
                sqlstr = "SELECT ifnull(Sum(BaseAmount),0) as totalamount FROM tblrecv A, tblRecvDet B where 1=1 and A.ReceiptNumber = B.ReceiptNumber " + txtCondition.Text
                'sqlstr = "SELECT ifnull(Sum(AppliedBase),0) as totalamount FROM tblrecvDet A where 1=1  and Reftype = '" & txtInvoicenoSearch.Text & "'"
            End If

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
            'conn.Close()
            conn.Close()
            conn.Dispose()
            command.Dispose()
            dt.Dispose()
            dr.Close()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION CalculateTotal", ex.Message.ToString, Left(sqlstr, 2000))
        End Try
    End Sub

    Private Sub RetrieveData()
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo
        'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
        Try
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            txtMode.Text = "View"
            'btnSvcEdit.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcEdit.Enabled = False
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.Enabled = False
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            '''''''''''''''''''''''''
            Dim sql As String
            sql = ""
            sql = "Select rcno,PostStatus, GLStatus, ReceiptNumber, ReceiptDate, GLPeriod, CompanyGroup, AccountId, ContactType, ReceiptFrom,  "
            sql = sql + " Comments, BankId, PaymentType, Cheque, ChequeDate, NetAmount, LedgerCode, LedgerName, BankName, Bank, Location,   "
            sql = sql + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, ChequeReturned "
            sql = sql + " FROM tblRecv "
            sql = sql + "where ReceiptNumber = '" + txtReceiptNoSearch.Text + "'"

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            'command1.CommandText = "SELECT * FROM tblcontract where rcno=" & Convert.ToInt64(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Try

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try

            Dim dt As New DataTable
            dt.Load(dr)
            'Convert.ToDateTime(dt.Rows(0)("ContractDate")).ToString("dd/MM/yyyy")
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("rcno").ToString <> "" Then : txtRcno.Text = dt.Rows(0)("rcno").ToString : End If

                If dt.Rows(0)("PostStatus").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("PostStatus").ToString : End If
                If dt.Rows(0)("ReceiptNumber").ToString <> "" Then : txtReceiptNo.Text = dt.Rows(0)("ReceiptNumber").ToString : End If
                If dt.Rows(0)("ReceiptDate").ToString <> "" Then : txtReceiptDate.Text = Convert.ToDateTime(dt.Rows(0)("ReceiptDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtReceiptPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then : txtCompanyGroup.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountIdBilling.Text = dt.Rows(0)("AccountId").ToString : End If
                If dt.Rows(0)("ContactType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContactType").ToString : End If
                If dt.Rows(0)("ReceiptFrom").ToString <> "" Then : txtAccountName.Text = dt.Rows(0)("ReceiptFrom").ToString : End If
                If dt.Rows(0)("NetAmount").ToString <> "" Then : txtReceivedAmount.Text = dt.Rows(0)("NetAmount").ToString : End If

                If dt.Rows(0)("PaymentType").ToString <> "" Then : ddlPaymentMode.Text = dt.Rows(0)("PaymentType").ToString : End If
                If dt.Rows(0)("Cheque").ToString <> "" Then : txtChequeNo.Text = dt.Rows(0)("Cheque").ToString : End If
                If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If

                'ddlBankCode.Items.Clear()
                'ddlBankCode.DataSourceID = "SqlDSBankCode"
                'ddlBankCode.DataBind()

                If dt.Rows(0)("Bank").ToString <> "" Then : ddlBankCode.Text = dt.Rows(0)("Bank").ToString : End If
                If dt.Rows(0)("BankId").ToString <> "" Then : txtBankID.Text = dt.Rows(0)("BankId").ToString : End If
                If dt.Rows(0)("LedgerCode").ToString <> "" Then : txtBankGLCode.Text = dt.Rows(0)("LedgerCode").ToString : End If
                If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtBankName.Text = Right(ddlBankCode.Text, Len(ddlBankCode.Text) - (Len(txtBankGLCode.Text) + 3)) : End If
                'If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If
                If dt.Rows(0)("Comments").ToString <> "" Then : txtComments.Text = dt.Rows(0)("Comments").ToString : End If
                If dt.Rows(0)("BankName").ToString <> "" Then : txtBankName.Text = dt.Rows(0)("BankName").ToString : End If
                If dt.Rows(0)("LedgerName").ToString <> "" Then : txtLedgerName.Text = dt.Rows(0)("LedgerName").ToString : End If
            End If

            conn.Close()
            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()
         

            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnCopy.Enabled = True
                btnReverse.Enabled = True
                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray
                btnPrint.Enabled = True
                btnPost.Enabled = False
                'btnDelete.Enabled = True
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnReverse.Enabled = False

                btnChangeStatus.Enabled = True
                btnChangeStatus.ForeColor = System.Drawing.Color.Black

                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPost.Enabled = True
                'btnDelete.Enabled = True
            End If


            updPnlMsg.Update()

            'PopulateArCode()
            DisplayGLGrid()

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION RetrieveData", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub FindRcno(source As String)
        Try
            Dim sqlstr As String
            sqlstr = ""

            sqlstr = "SELECT Rcno FROM tblRecv where ReceiptNumber ='" & source & "'"

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
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION FindRcno", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(10).Text = "COMPANY" Then
                    e.Row.Cells(10).Text = "CORPORATE"
                Else
                    e.Row.Cells(10).Text = "RESIDENTIAL"
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "GridView1_RowDataBound", ex.Message.ToString, "")
        End Try
    End Sub

    
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo
        'Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo

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
            'btnSvcEdit.Enabled = False
            'btnSvcDelete.Enabled = False

            'btnSvcEdit.Enabled = False
            'btnSvcEdit.ForeColor = System.Drawing.Color.Gray
            'btnSvcDelete.Enabled = False
            'btnSvcDelete.ForeColor = System.Drawing.Color.Gray

            Dim editindex As Integer = GridView1.SelectedIndex
            '
            txtRcno.Text = 0

            If txtFrom.Text = "Corporate" Or txtFrom.Text = "Residential" Then

                ddlPaymentMode.SelectedIndex = -1
                ddlBankCode.SelectedIndex = -1
                'txtRcno.Text = Session("rcnoinv")
                'Session.Remove("rcnoinv")
                ''txtRcno.Text = Session("rcnoinv")
                FindRcno(txtReceiptNoSearch.Text)
                'Session.Remove("customerfrom")

                'ddlBankCode.SelectedIndex = -1
                'ddlPaymentMode.SelectedIndex = -1
            Else

                txtRcno.Text = GridView1.SelectedRow.Cells(1).Text.Trim
            End If


            PopulateRecord()
            ' InsertIntoTblWebEventLog("Receipt", "Gridview1", tb1.ActiveTabIndex.ToString, txtCreatedBy.Text)

            If tb1.ActiveTabIndex = 1 Then
                tb1.ActiveTabIndex = 0
            End If
            updPanelReceipt.Update()

            '  InsertIntoTblWebEventLog("Receipt", "Gridview1", tb1.ActiveTabIndex.ToString, txtCreatedBy.Text)

            lblFileUploadCount.Text = "File Upload"

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "GridView1_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Private Sub PopulateRecord()
        Try
            lblAlertStatus.Text = ""
            'lblAlert.Text = ""

            Dim conn As MySqlConnection = New MySqlConnection()


            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            '''''''''''''''''''''''''
            Dim sql As String
            sql = ""
            sql = "Select PostStatus, GLStatus, ReceiptNumber, ReceiptDate, GLPeriod, CompanyGroup, AccountId, ContactType, ReceiptFrom,  "
            sql = sql + " Comments, BankId, PaymentType, Cheque, ChequeDate, NetAmount, LedgerCode, LedgerName, BankName, Bank, BaseAmount,  "
            sql = sql + " AppliedBase, Addr1, Addr2, Addr3, Addr4, Location, AddPostal, AddCity, AddState, AddCountry, ChequeReturned, ReasonChSt, "
            sql = sql + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn "
            sql = sql + " FROM tblRecv "
            sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            'command1.CommandText = "SELECT * FROM tblcontract where rcno=" & Convert.ToInt64(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Try

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try

            Dim dt As New DataTable
            dt.Load(dr)
            'Convert.ToDateTime(dt.Rows(0)("ContractDate")).ToString("dd/MM/yyyy")
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("PostStatus").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("PostStatus").ToString : End If
               
                If dt.Rows(0)("ReceiptNumber").ToString <> "" Then : txtReceiptNo.Text = dt.Rows(0)("ReceiptNumber").ToString : End If
                If dt.Rows(0)("ReceiptDate").ToString <> "" Then : txtReceiptDate.Text = Convert.ToDateTime(dt.Rows(0)("ReceiptDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtReceiptPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then : txtCompanyGroup.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountIdBilling.Text = dt.Rows(0)("AccountId").ToString : End If
                If dt.Rows(0)("ContactType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContactType").ToString : End If
                If dt.Rows(0)("ReceiptFrom").ToString <> "" Then : txtAccountName.Text = dt.Rows(0)("ReceiptFrom").ToString : End If
                'If dt.Rows(0)("BaseAmount").ToString <> "" Then : txtReceivedAmount.Text = dt.Rows(0)("BaseAmount").ToString : End If
                If dt.Rows(0)("AppliedBase").ToString <> "" Then : txtReceivedAmount.Text = dt.Rows(0)("AppliedBase").ToString : End If

                If dt.Rows(0)("PaymentType").ToString <> "" Then
                    'Dim gSalesman As String

                    gddlvalue = dt.Rows(0)("PaymentType").ToString.ToUpper()

                    If ddlPaymentMode.Items.FindByValue(gddlvalue) Is Nothing Then
                        ddlPaymentMode.Items.Add(gddlvalue)
                        ddlPaymentMode.Text = gddlvalue
                    Else
                        ddlPaymentMode.Text = dt.Rows(0)("PaymentType").ToString.Trim().ToUpper()
                    End If
                End If
                'If dt.Rows(0)("PaymentType").ToString <> "" Then : ddlPaymentMode.Text = dt.Rows(0)("PaymentType").ToString : End If
                If dt.Rows(0)("Cheque").ToString <> "" Then : txtChequeNo.Text = dt.Rows(0)("Cheque").ToString : End If
                'If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If

                If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = Convert.ToDateTime(dt.Rows(0)("ChequeDate")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("AddPostal").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("AddPostal").ToString : End If
                If dt.Rows(0)("AddCity").ToString <> "" Then : txtBillCity.Text = dt.Rows(0)("AddCity").ToString : End If
                If dt.Rows(0)("AddState").ToString <> "" Then : txtBillState.Text = dt.Rows(0)("AddState").ToString : End If
                If dt.Rows(0)("AddCountry").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("AddCountry").ToString : End If

                ''''''''''''''''''''''
                txtReasonChSt.Text = dt.Rows(0)("ReasonChSt").ToString.Trim
                '''''''''''''''
                Dim lBankId As String = ""

                Dim sqlBank As String
                sqlBank = ""
                sqlBank = "Select DefaultBank from tblSettleType where SettleType = '" & ddlPaymentMode.Text & "'"

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text
                command2.CommandText = sqlBank
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("DefaultBank").ToString <> "" Then : lBankId = dt2.Rows(0)("DefaultBank").ToString : End If
                End If
                'conn.Close()
                Dim Query As String

                ddlBankCode.Items.Clear()
                ddlBankCode.Items.Add("--SELECT--")

                ddlBankCode.SelectedIndex = -1
                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank  where BankId = '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)


                '''''''''''''''''''''''

                If dt.Rows(0)("Bank").ToString <> "" Then
                    'Dim gSalesman As String

                    gddlvalue = dt.Rows(0)("Bank").ToString.ToUpper()

                    If ddlBankCode.Items.FindByValue(gddlvalue) Is Nothing Then
                        ddlBankCode.Items.Add(gddlvalue)
                        ddlBankCode.Text = gddlvalue
                    Else
                        ddlBankCode.Text = dt.Rows(0)("Bank").ToString.Trim().ToUpper()
                    End If
                End If

                'If dt.Rows(0)("Bank").ToString <> "" Then : ddlBankCode.Text = dt.Rows(0)("Bank").ToString : End If
                If dt.Rows(0)("BankId").ToString <> "" Then : txtBankID.Text = dt.Rows(0)("BankId").ToString : End If
                If dt.Rows(0)("LedgerCode").ToString <> "" Then : txtBankGLCode.Text = dt.Rows(0)("LedgerCode").ToString : End If
                If dt.Rows(0)("BankId").ToString <> "" Then : txtBankName.Text = Right(ddlBankCode.Text, Len(ddlBankCode.Text) - (Len(txtBankGLCode.Text) + 3)) : End If
                'If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If
                If dt.Rows(0)("Comments").ToString <> "" Then : txtComments.Text = dt.Rows(0)("Comments").ToString : End If
                If dt.Rows(0)("BankName").ToString <> "" Then : txtBankName.Text = dt.Rows(0)("BankName").ToString : End If
                If dt.Rows(0)("LedgerName").ToString <> "" Then : txtLedgerName.Text = dt.Rows(0)("LedgerName").ToString : End If

                If dt.Rows(0)("Addr1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("Addr1").ToString : End If
                If dt.Rows(0)("Addr2").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("Addr2").ToString : End If
                If dt.Rows(0)("Addr3").ToString <> "" Then : txtBillBuilding.Text = dt.Rows(0)("Addr3").ToString : End If
                If dt.Rows(0)("Addr4").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("Addr4").ToString : End If
                'If dt.Rows(0)("Addr4").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("Addr1").ToString : End If
                If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If
                chkChequeReturned.Checked = dt.Rows(0)("ChequeReturned").ToString

                txtRecordCreatedBy.Text = dt.Rows(0)("CreatedBy").ToString

                Dim sqlbal As String

                'If ddlContactType.Text = "COMPANY" Or ddlContactType.Text = "CORPORATE" Then
                Dim salescn As Decimal = 0.0
                Dim rcpt As Decimal = 0.0

                sqlbal = ""
                sqlbal = "Select ifnull(sum(appliedbase),0) as salescnbal from tblSales where AccountId = '" & txtAccountIdBilling.Text & "' and Poststatus ='P'"

                Dim commandBal As MySqlCommand = New MySqlCommand
                commandBal.CommandType = CommandType.Text
                commandBal.CommandText = sqlbal
                commandBal.Connection = conn

                Dim drbal As MySqlDataReader = commandBal.ExecuteReader()

                Dim dtbal As New DataTable
                dtbal.Load(drbal)

                If dtbal.Rows.Count > 0 Then
                    If dtbal.Rows(0)("salescnbal").ToString <> "" Then : salescn = dtbal.Rows(0)("salescnbal").ToString : End If
                End If


                sqlbal = ""
                sqlbal = "Select ifnull(sum(appliedbase),0) as recvbal from tblRecv where AccountId = '" & txtAccountIdBilling.Text & "' and Poststatus ='P'"

                Dim commandBal1 As MySqlCommand = New MySqlCommand
                commandBal1.CommandType = CommandType.Text
                commandBal1.CommandText = sqlbal
                commandBal1.Connection = conn

                Dim drbal1 As MySqlDataReader = commandBal1.ExecuteReader()

                Dim dtbal1 As New DataTable
                dtbal1.Load(drbal1)

                If dtbal1.Rows.Count > 0 Then
                    If dtbal1.Rows(0)("recvbal").ToString <> "" Then : rcpt = dtbal1.Rows(0)("recvbal").ToString : End If
                End If

                txtBalance.Text = (salescn - rcpt).ToString("N2")

                Dim lTotJournal As Integer
                Dim lTotReceipt As Integer
                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Contra Records
                Dim commandService As MySqlCommand = New MySqlCommand

                commandService.CommandType = CommandType.Text
                commandService.CommandText = "SELECT count(a.receiptnumber) as totreceiptrec FROM tblrecv a, tblrecvdet b where a.receiptnumber = b.receiptnumber and a.PostStatus <> 'V' and b.reftype ='" & txtReceiptNo.Text & "'"
                commandService.Connection = conn

                Dim drService As MySqlDataReader = commandService.ExecuteReader()
                Dim dtService As New DataTable
                dtService.Load(drService)

                If dtService.Rows.Count > 0 Then

                    lTotReceipt = Val(dtService.Rows(0)("totreceiptrec").ToString)
                    'txtTotReceipts.Text = Val(dtService.Rows(0)("totreceiptrec").ToString).ToString
                    'btnReceipts.Text = "RECEIPTS [" + Val(dtService.Rows(0)("totreceiptrec").ToString).ToString + "]"
                    'txtTotalReceipts.Text = Val(dtService.Rows(0)("totreceiptrec").ToString).ToString
                    ''btnServiceRecords.Enabled = True
                    ''btnServiceRecords.ForeColor = System.Drawing.Color.Black
                End If

                'End:Retrieve Contra Records
                ''''''''''''''''''''''''''

                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Journal Records
                Dim commandJournal As MySqlCommand = New MySqlCommand

                commandJournal.CommandType = CommandType.Text
                commandJournal.CommandText = "SELECT count(a.VoucherNumber) as totjournalrec FROM tblJrnv a, tblJrnvDet b where a.VoucherNumber = b.VoucherNumber and a.PostStatus <> 'V' and b.RefTYpe ='" & txtReceiptNo.Text & "'"
                commandJournal.Connection = conn

                Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
                Dim dtJournal As New DataTable
                dtJournal.Load(drJournal)

                If dtJournal.Rows.Count > 0 Then

                    lTotJournal = Val(dtJournal.Rows(0)("totjournalrec").ToString)
                    'txtTotReceipts.Text = Val(dtCNDN.Rows(0)("totreceiptrec").ToString).ToString
                    'btnJournal.Text = "JOURNAL & CONTRA [" + Val(dtService.Rows(0)("totreceiptrec").ToString).ToString + Val(dtJournal.Rows(0)("totjournalrec").ToString).ToString + "]"
                    btnJournal.Text = "JOURNAL & CONTRA [" & Val(lTotReceipt + lTotJournal).ToString & "]"

                End If

                'End:Retrieve Journal Records
                ''''''''''''''''''''''''''
                btnJournal.Enabled = True
                btnJournal.ForeColor = System.Drawing.Color.Black
                'conn.Close()

                'ElseIf ddlContactType.Text = "PERSON" Or ddlContactType.Text = "RESIDENTIAL" Then

                'End If


            End If

            conn.Close()
            conn.Dispose()

            command1.Dispose()
            dt.Dispose()
            dr.Close()

            PopulateServiceGrid()

            updpnlBillingDetails.Update()

            updPnlBillingRecs.Update()

            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnCopy.Enabled = True
                btnCopy.ForeColor = System.Drawing.Color.Black
                btnReverse.Enabled = True
                btnReverse.ForeColor = System.Drawing.Color.Black

                BtnDelete.Enabled = False
                BtnDelete.ForeColor = System.Drawing.Color.Gray

                btnPrint.Enabled = True
                btnPrint.ForeColor = System.Drawing.Color.Black
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
                'btnDelete.Enabled = True
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnCopy.ForeColor = System.Drawing.Color.Black
                btnReverse.Enabled = False
                btnReverse.ForeColor = System.Drawing.Color.Gray

                btnChangeStatus.Enabled = True
                btnChangeStatus.ForeColor = System.Drawing.Color.Black

                BtnDelete.Enabled = True
                BtnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPrint.ForeColor = System.Drawing.Color.Black
                btnPost.Enabled = True
                btnPost.ForeColor = System.Drawing.Color.Black
                'btnDelete.Enabled = True
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

            lbltotalservices.Text = (grvBillingDetails.Rows.Count).ToString & " out of " & txtTotDetRec.Text
            updPnlMsg.Update()

            'PopulateArCode()
            DisplayGLGrid()

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If

            If Right(txtChequeNo.Text, 10) <> "(RETURNED)" Then
                FindEnableReturn()
            End If

            'If Right(txtChequeNo.Text, 10) <> "(RETURNED)" Then
            '    FindEnableReturn()
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

            Session.Add("ReceiptNumber", txtReceiptNo.Text)

            Dim IsLock = FindRVPeriod(txtReceiptPeriod.Text)
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
                'Exit Sub
            End If


            'Dim ErrOtLo As String = "C:\temp\SitaPestErrorLogger.txt"
            'Using w As StreamWriter = File.AppendText(ErrOtLo)
            '    w.WriteLine("RCPT 1" + vbLf & vbLf)
            'End Using

            'If Right(txtChequeNo.Text, 10) <> "(RETURNED)" Then
            '    FindEnableReturn()
            'End If

            'Using w As StreamWriter = File.AppendText(ErrOtLo)
            '    w.WriteLine("RCPT 2" + vbLf & vbLf)
            'End Using


            'Session.Add("ReceiptNumber", txtReceiptNo.Text)
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateRecord", ex.Message.ToString, "")
        End Try
    End Sub



    'Cheque Return

    Private Sub PopulateRecordChequeReturn()
        Try
            Dim conn As MySqlConnection = New MySqlConnection()


            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            '''''''''''''''''''''''''
            Dim sql As String
            sql = ""
            sql = "Select PostStatus, GLStatus, ReceiptNumber, ReceiptDate, GLPeriod, CompanyGroup, AccountId, ContactType, ReceiptFrom,  "
            sql = sql + " Comments, BankId, PaymentType, Cheque, ChequeDate, NetAmount, LedgerCode, LedgerName, BankName, Bank, BaseAmount, AppliedBase, Addr1, Addr2, Addr3, Addr4, Location, ChequeReturned"
            sql = sql + " CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn "
            sql = sql + " FROM tblRecv "
            sql = sql + "where rcno = " & Convert.ToInt64(txtRcno.Text)

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            'command1.CommandText = "SELECT * FROM tblcontract where rcno=" & Convert.ToInt64(txtRcno.Text)
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Try

            Catch ex As Exception
                lblAlert.Text = ex.Message.ToString
            End Try

            Dim dt As New DataTable
            dt.Load(dr)
            'Convert.ToDateTime(dt.Rows(0)("ContractDate")).ToString("dd/MM/yyyy")
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("PostStatus").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("PostStatus").ToString : End If
                If dt.Rows(0)("ReceiptNumber").ToString <> "" Then : txtReceiptNo.Text = dt.Rows(0)("ReceiptNumber").ToString : End If
                If dt.Rows(0)("ReceiptDate").ToString <> "" Then : txtReceiptDate.Text = Convert.ToDateTime(dt.Rows(0)("ReceiptDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtReceiptPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                If dt.Rows(0)("CompanyGroup").ToString <> "" Then : txtCompanyGroup.Text = dt.Rows(0)("CompanyGroup").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountIdBilling.Text = dt.Rows(0)("AccountId").ToString : End If
                If dt.Rows(0)("ContactType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContactType").ToString : End If
                If dt.Rows(0)("ReceiptFrom").ToString <> "" Then : txtAccountName.Text = dt.Rows(0)("ReceiptFrom").ToString : End If
                'If dt.Rows(0)("BaseAmount").ToString <> "" Then : txtReceivedAmount.Text = dt.Rows(0)("BaseAmount").ToString : End If
                If dt.Rows(0)("AppliedBase").ToString <> "" Then : txtReceivedAmount.Text = dt.Rows(0)("AppliedBase").ToString : End If

                If dt.Rows(0)("PaymentType").ToString <> "" Then
                    'Dim gSalesman As String

                    gddlvalue = dt.Rows(0)("PaymentType").ToString.ToUpper()

                    If ddlPaymentMode.Items.FindByValue(gddlvalue) Is Nothing Then
                        ddlPaymentMode.Items.Add(gddlvalue)
                        ddlPaymentMode.Text = gddlvalue
                    Else
                        ddlPaymentMode.Text = dt.Rows(0)("PaymentType").ToString.Trim().ToUpper()
                    End If
                End If
                'If dt.Rows(0)("PaymentType").ToString <> "" Then : ddlPaymentMode.Text = dt.Rows(0)("PaymentType").ToString : End If
                If dt.Rows(0)("Cheque").ToString <> "" Then : txtChequeNo.Text = dt.Rows(0)("Cheque").ToString : End If
                'If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If

                If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = Convert.ToDateTime(dt.Rows(0)("ChequeDate")).ToString("dd/MM/yyyy") : End If

                ''''''''''''''''''''''

                '''''''''''''''
                Dim lBankId As String = ""

                Dim sqlBank As String
                sqlBank = ""
                sqlBank = "Select DefaultBank from tblSettleType where SettleType = '" & ddlPaymentMode.Text & "'"

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text
                command2.CommandText = sqlBank
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("DefaultBank").ToString <> "" Then : lBankId = dt2.Rows(0)("DefaultBank").ToString : End If
                End If
                'conn.Close()
                Dim Query As String

                ddlBankCode.Items.Clear()
                ddlBankCode.Items.Add("--SELECT--")

                ddlBankCode.SelectedIndex = -1
                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank  where BankId = '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)


                '''''''''''''''''''''''

                If dt.Rows(0)("Bank").ToString <> "" Then
                    'Dim gSalesman As String

                    gddlvalue = dt.Rows(0)("Bank").ToString.ToUpper()

                    If ddlBankCode.Items.FindByValue(gddlvalue) Is Nothing Then
                        ddlBankCode.Items.Add(gddlvalue)
                        ddlBankCode.Text = gddlvalue
                    Else
                        ddlBankCode.Text = dt.Rows(0)("Bank").ToString.Trim().ToUpper()
                    End If
                End If

                'If dt.Rows(0)("Bank").ToString <> "" Then : ddlBankCode.Text = dt.Rows(0)("Bank").ToString : End If
                If dt.Rows(0)("BankId").ToString <> "" Then : txtBankID.Text = dt.Rows(0)("BankId").ToString : End If
                If dt.Rows(0)("LedgerCode").ToString <> "" Then : txtBankGLCode.Text = dt.Rows(0)("LedgerCode").ToString : End If
                If dt.Rows(0)("BankId").ToString <> "" Then : txtBankName.Text = Right(ddlBankCode.Text, Len(ddlBankCode.Text) - (Len(txtBankGLCode.Text) + 3)) : End If
                'If dt.Rows(0)("ChequeDate").ToString <> "" Then : txtChequeDate.Text = dt.Rows(0)("ChequeDate").ToString : End If
                If dt.Rows(0)("Comments").ToString <> "" Then : txtComments.Text = dt.Rows(0)("Comments").ToString : End If
                If dt.Rows(0)("BankName").ToString <> "" Then : txtBankName.Text = dt.Rows(0)("BankName").ToString : End If
                If dt.Rows(0)("LedgerName").ToString <> "" Then : txtLedgerName.Text = dt.Rows(0)("LedgerName").ToString : End If

                If dt.Rows(0)("Addr1").ToString <> "" Then : txtBillAddress.Text = dt.Rows(0)("Addr1").ToString : End If
                If dt.Rows(0)("Addr2").ToString <> "" Then : txtBillStreet.Text = dt.Rows(0)("Addr2").ToString : End If
                If dt.Rows(0)("Addr3").ToString <> "" Then : txtBillBuilding.Text = dt.Rows(0)("Addr3").ToString : End If
                If dt.Rows(0)("Addr4").ToString <> "" Then : txtBillCountry.Text = dt.Rows(0)("Addr4").ToString : End If
                'If dt.Rows(0)("Addr4").ToString <> "" Then : txtBillPostal.Text = dt.Rows(0)("Addr1").ToString : End If
                If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If
                txtRecordCreatedBy.Text = dt.Rows(0)("CreatedBy").ToString

                Dim sqlbal As String

                'If ddlContactType.Text = "COMPANY" Or ddlContactType.Text = "CORPORATE" Then
                Dim salescn As Decimal = 0.0
                Dim rcpt As Decimal = 0.0

                sqlbal = ""
                sqlbal = "Select ifnull(sum(appliedbase),0) as salescnbal from tblSales where AccountId = '" & txtAccountIdBilling.Text & "' and Poststatus ='P'"

                Dim commandBal As MySqlCommand = New MySqlCommand
                commandBal.CommandType = CommandType.Text
                commandBal.CommandText = sqlbal
                commandBal.Connection = conn

                Dim drbal As MySqlDataReader = commandBal.ExecuteReader()

                Dim dtbal As New DataTable
                dtbal.Load(drbal)

                If dtbal.Rows.Count > 0 Then
                    If dtbal.Rows(0)("salescnbal").ToString <> "" Then : salescn = dtbal.Rows(0)("salescnbal").ToString : End If
                End If


                sqlbal = ""
                sqlbal = "Select ifnull(sum(appliedbase),0) as recvbal from tblRecv where AccountId = '" & txtAccountIdBilling.Text & "' and Poststatus ='P'"

                Dim commandBal1 As MySqlCommand = New MySqlCommand
                commandBal1.CommandType = CommandType.Text
                commandBal1.CommandText = sqlbal
                commandBal1.Connection = conn

                Dim drbal1 As MySqlDataReader = commandBal1.ExecuteReader()

                Dim dtbal1 As New DataTable
                dtbal1.Load(drbal1)

                If dtbal1.Rows.Count > 0 Then
                    If dtbal1.Rows(0)("recvbal").ToString <> "" Then : rcpt = dtbal1.Rows(0)("recvbal").ToString : End If
                End If

                txtBalance.Text = (salescn - rcpt).ToString("N2")

                Dim lTotJournal As Integer
                Dim lTotReceipt As Integer
                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Contra Records
                Dim commandService As MySqlCommand = New MySqlCommand

                commandService.CommandType = CommandType.Text
                commandService.CommandText = "SELECT count(a.receiptnumber) as totreceiptrec FROM tblrecv a, tblrecvdet b where a.receiptnumber = b.receiptnumber and a.PostStatus <> 'V' and b.reftype ='" & txtReceiptNo.Text & "'"
                commandService.Connection = conn

                Dim drService As MySqlDataReader = commandService.ExecuteReader()
                Dim dtService As New DataTable
                dtService.Load(drService)

                If dtService.Rows.Count > 0 Then

                    lTotReceipt = Val(dtService.Rows(0)("totreceiptrec").ToString)
                    'txtTotReceipts.Text = Val(dtService.Rows(0)("totreceiptrec").ToString).ToString
                    'btnReceipts.Text = "RECEIPTS [" + Val(dtService.Rows(0)("totreceiptrec").ToString).ToString + "]"
                    'txtTotalReceipts.Text = Val(dtService.Rows(0)("totreceiptrec").ToString).ToString
                    ''btnServiceRecords.Enabled = True
                    ''btnServiceRecords.ForeColor = System.Drawing.Color.Black
                End If

                'End:Retrieve Contra Records
                ''''''''''''''''''''''''''

                ''''''''''''''''''''''''''''''''''''''''
                'Start:Retrive Journal Records
                Dim commandJournal As MySqlCommand = New MySqlCommand

                commandJournal.CommandType = CommandType.Text
                commandJournal.CommandText = "SELECT count(a.VoucherNumber) as totjournalrec FROM tblJrnv a, tblJrnvDet b where a.VoucherNumber = b.VoucherNumber and a.PostStatus <> 'V' and b.RefTYpe ='" & txtReceiptNo.Text & "'"
                commandJournal.Connection = conn

                Dim drJournal As MySqlDataReader = commandJournal.ExecuteReader()
                Dim dtJournal As New DataTable
                dtJournal.Load(drJournal)

                If dtJournal.Rows.Count > 0 Then

                    lTotJournal = Val(dtJournal.Rows(0)("totjournalrec").ToString)
                    'txtTotReceipts.Text = Val(dtCNDN.Rows(0)("totreceiptrec").ToString).ToString
                    'btnJournal.Text = "JOURNAL & CONTRA [" + Val(dtService.Rows(0)("totreceiptrec").ToString).ToString + Val(dtJournal.Rows(0)("totjournalrec").ToString).ToString + "]"
                    btnJournal.Text = "JOURNAL & CONTRA [" & Val(lTotReceipt + lTotJournal).ToString & "]"

                End If

                'End:Retrieve Journal Records
                ''''''''''''''''''''''''''
                btnJournal.Enabled = True
                btnJournal.ForeColor = System.Drawing.Color.Black
                'conn.Close()

                'ElseIf ddlContactType.Text = "PERSON" Or ddlContactType.Text = "RESIDENTIAL" Then

                'End If
            End If

            conn.Close()
            conn.Dispose()

            command1.Dispose()
            dt.Dispose()
            dr.Close()

            PopulateServiceGrid()

            updpnlBillingDetails.Update()

            updPnlBillingRecs.Update()

            If txtPostStatus.Text = "P" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnCopy.Enabled = True
                btnCopy.ForeColor = System.Drawing.Color.Black
                btnReverse.Enabled = True
                btnReverse.ForeColor = System.Drawing.Color.Black



                BtnDelete.Enabled = False
                BtnDelete.ForeColor = System.Drawing.Color.Gray

                btnPrint.Enabled = True
                btnPrint.ForeColor = System.Drawing.Color.Black
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
                'btnDelete.Enabled = True
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                btnCopy.Enabled = True
                btnCopy.ForeColor = System.Drawing.Color.Black
                btnReverse.Enabled = False
                btnReverse.ForeColor = System.Drawing.Color.Gray

                btnChangeStatus.Enabled = True
                btnChangeStatus.ForeColor = System.Drawing.Color.Black

                BtnDelete.Enabled = True
                BtnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPrint.ForeColor = System.Drawing.Color.Black
                btnPost.Enabled = True
                btnPost.ForeColor = System.Drawing.Color.Black
                'btnDelete.Enabled = True
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

            Session.Add("ReceiptNumber", txtReceiptNo.Text)

            Dim IsLock = FindRVPeriod(txtReceiptPeriod.Text)
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
            'Session.Add("ReceiptNumber", txtReceiptNo.Text)
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateRecord", ex.Message.ToString, "")
        End Try
    End Sub



    Private Sub PopulateServiceGridChequeReturn()

        Try

            'Start: Service Recods

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                grvBillingDetails.DataSource = dtScdrLoc
                grvBillingDetails.DataBind()

                SetPreviousDataBillingDetailsRecs()
                'SetPreviousDataServiceRecs()
            Next i

            FirstGridViewRowBillingDetailsRecs()
            'FirstGridViewRowServiceRecs()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim sql As String
            sql = ""

            If txtMode.Text = "View" Then

                sql = "Select A.Rcno, A.InvoiceNo, A.InvoicePrice, A.InvoiceGST, A.InvoiceValue, a.Description, A.SubCode, "
                sql = sql + "  A.ReceiptValue, A.InvoiceDate, A.RcnoServiceBillingItem , A.ContractNo, A.ServiceRecordNo, A.InvoiceType, A.TaxType, A.LedgerCode, A.LedgerName, A.AppliedBase, A.RefType, DocTYpe, AccountId, AccountName  "
                sql = sql + " FROM tblrecvdet A "
                sql = sql + " where 1 = 1 "
                sql = sql + " and ReceiptNumber = '" & txtReceiptNo.Text & "'"

            Else
                If ddlContactType.SelectedIndex = 1 Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, "
                    sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, B.Address1, "
                    sql = sql + " B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, A.ContactType, A.CompanyGroup, A.ChequeReturned  "
                    sql = sql + " FROM tblrecv A, tblCompany B, tblContract C "
                    sql = sql + " where 1 = 1  and A.BillYN= 'N' and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                ElseIf ddlContactType.SelectedIndex = 2 Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, "
                    sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, B.Address1, "
                    sql = sql + " B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, A.ContactType, A.CompanyGroup  "
                    sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C "
                    sql = sql + " where 1 = 1  and A.BillYN= 'N' and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and A.ContactType = '" & ddlContactType.Text.Trim & "'"

                End If
            End If


            txtMode.Text = "NEW"

            Dim Total As Decimal
            Dim TotalWithGST As Decimal
            Dim TotalDiscAmt As Decimal
            Dim TotalGSTAmt As Decimal
            Dim TotalPriceWithDiscountAmt As Decimal
            Dim TotalReceiptAmt As Decimal
            Dim ReceiptAmt As Decimal


            Total = 0.0
            TotalWithGST = 0.0
            TotalDiscAmt = 0.0
            TotalGSTAmt = 0.0
            TotalPriceWithDiscountAmt = 0.0
            TotalReceiptAmt = 0.0
            ReceiptAmt = 0.0

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)

            Dim TotDetailRecordsServiceRec = dt.Rows.Count
            If dt.Rows.Count > 0 Then

                Dim rowIndex = 0

                For Each row As DataRow In dt.Rows
                    If (TotDetailRecordsServiceRec > (rowIndex + 1)) Then
                        'AddNewRowLoc()
                        AddNewRowBillingDetailsRecs()
                        'AddNewRowServiceRecs()
                    End If

                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    TextBoxItemType.Text = Convert.ToString(dt.Rows(rowIndex)("SubCode"))

                    Dim TextBoxRcnoRecvDet As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoReceiptGV"), TextBox)
                    TextBoxRcnoRecvDet.Text = Convert.ToString(dt.Rows(rowIndex)("Rcno"))


                    Dim TextBoxTotalInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    'TextBoxTotalInvoiceNo.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceNo"))
                    TextBoxTotalInvoiceNo.Text = Convert.ToString(dt.Rows(rowIndex)("RefType"))

                    Dim TextBoxTotalInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    If String.IsNullOrEmpty(dt.Rows(rowIndex)("InvoiceDate").ToString) = False Then
                        TextBoxTotalInvoiceDate.Text = Convert.ToDateTime(dt.Rows(rowIndex)("InvoiceDate")).ToString("dd/MM/yyyy")
                    Else
                        TextBoxTotalInvoiceDate.Text = ""
                    End If


                    'Convert.ToDateTime(dtServiceBillingDetailItem.Rows(rowIndex)("SalesDate")).ToString("dd/MM/yyyy")
                    Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    TextBoxTotalPriceWithDisc.Text = Convert.ToString(dt.Rows(rowIndex)("InvoicePrice"))

                    Dim TextBoxTotalInvoiceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    TextBoxTotalInvoiceValue.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceGST"))


                    Dim TextBoxTotalTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalTotalPriceWithGST.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceValue"))

                    Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    TextBoxContractNo.Text = Convert.ToString(dt.Rows(rowIndex)("ContractNo"))

                    Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceNoGV"), TextBox)
                    TextBoxServiceRecordNo.Text = Convert.ToString(dt.Rows(rowIndex)("ServiceRecordNo"))

                    Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)
                    TextBoxInvoiceType.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceType"))

                    Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTaxTypeGV"), TextBox)
                    TextBoxTaxType.Text = Convert.ToString(dt.Rows(rowIndex)("TaxType"))


                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    TextBoxOtherCode.Text = Convert.ToString(dt.Rows(rowIndex)("LedgerCode"))

                    Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRemarksGV"), TextBox)
                    TextBoxRemarks.Text = Convert.ToString(dt.Rows(rowIndex)("Description"))

                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    TextBoxGLDescription.Text = Convert.ToString(dt.Rows(rowIndex)("LedgerName"))

                    Dim TextBoxDocType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)
                    TextBoxDocType.Text = Convert.ToString(dt.Rows(rowIndex)("DocTYpe"))


                    Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                    TextBoxAccountID.Text = Convert.ToString(dt.Rows(rowIndex)("AccountID"))

                    Dim TextBoxAccountName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustomerNameGV"), TextBox)
                    TextBoxAccountName.Text = Convert.ToString(dt.Rows(rowIndex)("AccountName"))


                    'quoted on 04.07.17
                    'Dim RcnoServiceBillingItemx As Long

                    'RcnoServiceBillingItemx = dt.Rows(rowIndex)("RcnoServiceBillingItem")
                    ' '''''''''''''''''''''''''''''''''''''''
                    'sql = "Select ReceiptAmount as Totalreceipt from tblSalesDetail where  rcno =" & Convert.ToInt64(RcnoServiceBillingItemx)

                    'Dim command23 As MySqlCommand = New MySqlCommand
                    'command23.CommandType = CommandType.Text
                    'command23.CommandText = sql
                    'command23.Connection = conn

                    'Dim dr23 As MySqlDataReader = command23.ExecuteReader()

                    'Dim dt23 As New DataTable
                    'dt23.Load(dr23)


                    ' ''''''''''''''''''''''''''''''''''''''''''
                    'Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    'If dt23.Rows.Count > 0 Then

                    '    TextBoxTotalReceiptAmt.Text = Convert.ToString(dt23.Rows(0)("Totalreceipt").ToString)

                    'End If

                    ' '''''''''''''''''''''''''''''''''''''''
                    'sql = "Select CreditAmount as TotalCN from tblSalesDetail where rcno =" & Convert.ToInt64(RcnoServiceBillingItemx)

                    'Dim command24 As MySqlCommand = New MySqlCommand
                    'command24.CommandType = CommandType.Text
                    'command24.CommandText = sql
                    'command24.Connection = conn

                    'Dim dr24 As MySqlDataReader = command24.ExecuteReader()

                    'Dim dt24 As New DataTable
                    'dt24.Load(dr24)


                    ' ''''''''''''''''''''''''''''''''''''''''''
                    'Dim totalCNAmount As Decimal
                    'totalCNAmount = 0.0

                    'If dt24.Rows.Count > 0 Then
                    '    If String.IsNullOrEmpty(dt24.Rows(0)("TotalCN").ToString) = True Then
                    '        totalCNAmount = 0.0
                    '    Else
                    '        totalCNAmount = Convert.ToDecimal(dt24.Rows(0)("TotalCN").ToString)
                    '    End If
                    'End If

                    'Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    'TextBoxTotalCNAmt.Text = (Convert.ToDecimal(totalCNAmount)).ToString("N2")

                    'quoted on 04.07.17


                    'Start: 04.07.17

                    Dim RcnoServiceBillingItemx As Long

                    RcnoServiceBillingItemx = dt.Rows(rowIndex)("RcnoServiceBillingItem")
                    '''''''''''''''''''''''''''''''''''''''
                    Dim totalCNAmount As Decimal
                    totalCNAmount = 0.0

                    Dim totalReceiptAmount As Decimal
                    totalReceiptAmount = 0.0
                    sql = "Select ReceiptBase as Totalreceipt, CreditBase as TotalCN from tblSales where  InvoiceNumber = '" & Convert.ToString(dt.Rows(rowIndex)("RefType")) & "'"

                    Dim command23 As MySqlCommand = New MySqlCommand
                    command23.CommandType = CommandType.Text
                    command23.CommandText = sql
                    command23.Connection = conn

                    Dim dr23 As MySqlDataReader = command23.ExecuteReader()

                    Dim dt23 As New DataTable
                    dt23.Load(dr23)


                    ''''''''''''''''''''''''''''''''''''''''''

                    If dt23.Rows.Count > 0 Then



                        If String.IsNullOrEmpty(dt23.Rows(0)("Totalreceipt").ToString) = True Then
                            totalReceiptAmount = 0.0
                        Else
                            totalReceiptAmount = Convert.ToDecimal(dt23.Rows(0)("Totalreceipt").ToString)
                        End If

                        If String.IsNullOrEmpty(dt23.Rows(0)("TotalCN").ToString) = True Then
                            totalCNAmount = 0.0
                        Else
                            totalCNAmount = Convert.ToDecimal(dt23.Rows(0)("TotalCN").ToString)
                        End If
                    End If

                  
                    Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    TextBoxTotalReceiptAmt.Text = (Convert.ToDecimal(totalReceiptAmount)).ToString("N2") * (-1)

                    Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    TextBoxTotalCNAmt.Text = (Convert.ToDecimal(totalCNAmount)).ToString("N2") * (-1)


                    'End: 04.07.17

                    Dim TextBoxTotalReceipt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    'TextBoxTotalReceipt.Text = Convert.ToString(dt.Rows(rowIndex)("ReceiptValue"))
                    TextBoxTotalReceipt.Text = Convert.ToString(dt.Rows(rowIndex)("AppliedBase")) * (-1)


                    'ReceiptAmt = ReceiptAmt + Convert.ToDecimal(Convert.ToString(dt.Rows(rowIndex)("ReceiptValue")))
                    ReceiptAmt = ReceiptAmt + Convert.ToDecimal(Convert.ToString(dt.Rows(rowIndex)("AppliedBase"))) * (-1)


                    Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                    TextBoxRcnoInvoice.Text = Convert.ToString(Convert.ToString(dt.Rows(rowIndex)("RcnoServiceBillingItem")))



                    'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                    'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalTotalPriceWithGST.Text)
                    'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                    ''txtAmountWithDiscount.Text = Total - TotalDiscAmt
                    'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxTotalInvoiceValue.Text)
                    'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text)
                    'TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxTotalReceiptAmt.Text)
                    TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxTotalReceipt.Text)

                    rowIndex += 1

                Next row

                'AddNewRowServiceRecs()
                'SetPreviousDataServiceRecs()
                'AddNewRowLoc()
                'SetPreviousDataLoc()
            Else
                'FirstGridViewRowServiceRecs()
                FirstGridViewRowBillingDetailsRecs()
            End If
            'End If

            'updpnlServiceRecs.Update()

            txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
            txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
            txtTotalWithGST.Text = TotalWithGST.ToString("N2")
            'txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

            'txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

            txtReceiptAmt.Text = ReceiptAmt.ToString("N2")

            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If txtMode.Text = "View" Then

                If (table.Rows.Count > 0) Then
                    For i As Integer = 0 To (table.Rows.Count) - 1
                        Dim TextBoxTotalInvoiceNo1 As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxSel As CheckBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)

                        If String.IsNullOrEmpty(TextBoxTotalInvoiceNo1.Text) = False Then
                            TextBoxSel.Enabled = False
                            TextBoxSel.Checked = True
                        End If

                    Next i
                End If
            End If
            'End: Service Recods



            'txtReceiptNo.Text = ""
            txtReceiptNo.Text = txtReceiptNo.Text + "-X"
            txtPostStatus.Text = ""
            txtReceivedAmount.Text = Convert.ToDecimal(txtReceivedAmount.Text) * (-1)
            txtChequeNo.Text = txtChequeNo.Text + " (RETURNED)"
            chkChequeReturned.Checked = True

            txtComments.Enabled = True
            txtComments.Text = ""

            'txtReceiptDate.Attributes.Remove("readonly")
            txtReceiptDate.Enabled = True
            btnSave.Enabled = True
            btnSave.ForeColor = Color.Black

            btnCancel.Enabled = True
            btnCancel.ForeColor = Color.Black

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateServiceGrid", ex.Message.ToString, "")
        End Try
    End Sub
    'Cheque Return

    'Function

    Private Sub GenerateReceiptNo()
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
            If Date.TryParseExact(txtReceiptDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                strdate = d.ToShortDateString
            End If


            lPrefix = Format(CDate(strdate), "yyyyMM")
            'lInvoiceNo = "RCPT" + lPrefix + "-"
            lInvoiceNo = txtRecvPrefix.Text + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)

            'lPrefix = "RCPT" + lYear
            lSuffixVal = 0


            ''''''''''''''
            lInvoiceNo = txtRecvPrefix.Text + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)

            lPrefix = txtRecvPrefix.Text + lYear
            lPrefix = Right(lPrefix, 6)
            lSuffixVal = 0

            '''''''''''''''

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

                'If lMonth = "01" Then
                lSuffixVal = dt.Rows(0)("Period01").ToString + 1
                lSetWidth = dt.Rows(0)("Width").ToString

                strUpdate = " Update tbldoccontrol set Period01 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "02" Then
                '    lSuffixVal = dt.Rows(0)("Period02").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString
                '    strUpdate = " Update tbldoccontrol set Period02 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "03" Then
                '    lSuffixVal = dt.Rows(0)("Period03").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period03 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "04" Then
                '    lSuffixVal = dt.Rows(0)("Period04").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period04 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "05" Then
                '    lSuffixVal = dt.Rows(0)("Period05").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period05 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "06" Then
                '    lSuffixVal = dt.Rows(0)("Period06").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period06 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "07" Then
                '    lSuffixVal = dt.Rows(0)("Period07").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString
                '    strUpdate = " Update tbldoccontrol set Period07 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "08" Then
                '    lSuffixVal = dt.Rows(0)("Period08").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString
                '    strUpdate = " Update tbldoccontrol set Period08 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "09" Then
                '    lSuffixVal = dt.Rows(0)("Period09").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period09 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "10" Then
                '    lSuffixVal = dt.Rows(0)("Period10").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period10 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "11" Then
                '    lSuffixVal = dt.Rows(0)("Period11").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString

                '    strUpdate = " Update tbldoccontrol set Period11 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'ElseIf lMonth = "12" Then
                '    lSuffixVal = dt.Rows(0)("Period12").ToString + 1
                '    lSetWidth = dt.Rows(0)("Width").ToString
                '    strUpdate = " Update tbldoccontrol set Period12 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                'End If

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
            conn.Close()
            conn.Dispose()

            commandDocControl.Dispose()
            dt.Dispose()
            dr.Close()
            lSuffix = lSetZeroes + lSuffixVal.ToString()
            txtReceiptNo.Text = lInvoiceNo + lSuffix
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION GenerateReceiptNo", ex.Message.ToString, "")
        End Try
    End Sub
    Public Sub MakeMeNull()

        Try

            lblMessage.Text = ""
            lblAlert.Text = ""
            txtMode.Text = ""
            'txtSearch1Status.Text = "O,P"
            'txtMode.Text = "NEW"

            'ddlBankCode.Items.Clear()
            'ddlBankCode.Items.Add("--SELECT--")

            'If txtFrom.Text = "invoice" Or txtFrom.Text = "invoicePB" Then
            '    'ddlBankCode.Items.Clear()
            '    ddlBankCode.DataSourceID = "SqlDSBankCode"
            '    ddlBankCode.DataBind()
            '    'Else
            '    '    ddlBankCode.Items.Clear()
            '    '    ddlBankCode.Items.Add("--SELECT--")
            'End If

            txtReceivedAmount.Text = "0.00"
            ddlPaymentMode.SelectedIndex = 0
            txtBankGLCode.Text = ""
            txtChequeDate.Text = ""
            txtChequeNo.Text = ""
            txtComments.Text = ""
            txtLedgerName.Text = ""
            txtBankID.Text = ""
            txtPopUpClient.Text = ""


            txtBillPostal.Text = ""
            txtBillCity.Text = ""
            txtBillState.Text = ""
            txtBillCountry.Text = ""
            txtLogDocNo.Text = ""


            'SqlDSOSInvoice.SelectCommand = strsql
            grvInvoiceRecDetails.DataSourceID = "SqlDSOSInvoice"
            grvInvoiceRecDetails.DataBind()

            'btnEdit.Enabled = False
            'btnCopy.Enabled = False
            'btnReverse.Enabled = False

            'btnChangeStatus.Enabled = False
            'btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            'btnDelete.Enabled = False
            'btnPrint.Enabled = False
            'btnPost.Enabled = False
            'btnDelete.Enabled = False

            btnEdit.Enabled = False
            btnEdit.ForeColor = System.Drawing.Color.Gray
            'btnCopy.Enabled = False
            btnReverse.Enabled = False
            btnReverse.ForeColor = System.Drawing.Color.Gray

            btnChangeStatus.Enabled = False
            btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            BtnDelete.Enabled = False
            BtnDelete.ForeColor = System.Drawing.Color.Gray

            btnPrint.Enabled = False
            btnPrint.ForeColor = System.Drawing.Color.Gray

            btnPost.Enabled = False
            btnPost.ForeColor = System.Drawing.Color.Gray

            'btnChequeReturn.Enabled = False
            'btnChequeReturn.ForeColor = System.Drawing.Color.Gray

            updPnlMsg.Update()

            DisableControls()

            updPanelSave.Update()

            If txtFrom.Text <> "invoice" And txtFrom.Text <> "invoicePB" Then
                FirstGridViewRowBillingDetailsRecs()
                'FirstGridViewRowServiceRecs()
                FirstGridViewRowGL()
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
            'updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION MakeMeNull", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub AccessControl()
        Try
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT X0255,  X0255Add, X0255Edit, X0255Delete, X0255Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT X0255,  X0255Add, X0255Edit, X0255Delete, X0255Print, X0255Post, X0255Reverse, x0255ChequeReturn,x0255FileUpload FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("X0255")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("X0255"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("X0255")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X0255Add")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0255Add")) = False Then
                            Me.btnADD.Enabled = dt.Rows(0)("X0255Add").ToString()
                        End If
                    End If


                    If txtMode.Text = "View" Then
                        If Not IsDBNull(dt.Rows(0)("X0255Edit")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0255Edit")) = False Then
                                Me.btnEdit.Enabled = dt.Rows(0)("X0255Edit").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0255Delete")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0255Delete")) = False Then
                                Me.btnDelete.Enabled = dt.Rows(0)("X0255Delete").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0255Print")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0255Print")) = False Then
                                Me.btnPrint.Enabled = dt.Rows(0)("X0255Print").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0255Post")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0255Post")) = False Then
                                Me.btnPost.Enabled = dt.Rows(0)("X0255Post").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("X0255Reverse")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("X0255Reverse")) = False Then
                                Me.btnReverse.Enabled = dt.Rows(0)("X0255Reverse").ToString()
                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0255ChequeReturn")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0255ChequeReturn")) = False Then
                                If dt.Rows(0)("x0255ChequeReturn").ToString() = False Then
                                    Me.btnChequeReturn.Enabled = False
                                End If
                                'Me.btnChequeReturn.Enabled = dt.Rows(0)("x0255ChequeReturn").ToString()

                            End If
                        End If

                        If Not IsDBNull(dt.Rows(0)("x0255FileUpload")) Then
                            If String.IsNullOrEmpty(dt.Rows(0)("x0255FileUpload")) = False Then
                                txtFileUpload.Text = dt.Rows(0)("x0255FileUpload").ToString()
                                btnUpload.Enabled = dt.Rows(0)("x0255FileUpload").ToString()
                                FileUpload1.Enabled = dt.Rows(0)("x0255FileUpload").ToString()
                            Else
                                txtFileUpload.Text = "0"
                            End If
                        Else
                            txtFileUpload.Text = "0"
                        End If

                    Else
                        Me.btnEdit.Enabled = False
                        Me.BtnDelete.Enabled = False
                        Me.btnPrint.Enabled = False
                        Me.btnPost.Enabled = False
                        Me.btnReverse.Enabled = False
                        'Me.btnChequeReturn.Enabled = False
                    End If

                    'If String.IsNullOrEmpty(dt.Rows(0)("X0255Print")) = False Then
                    '    Me.btnDelete.Enabled = dt.Rows(0)("X0255Print").ToString()
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

                    If BtnDelete.Enabled = True Then
                        BtnDelete.ForeColor = System.Drawing.Color.Black
                    Else
                        BtnDelete.ForeColor = System.Drawing.Color.Gray
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

                    If btnChequeReturn.Enabled = True Then
                        btnChequeReturn.ForeColor = System.Drawing.Color.Black
                    Else
                        btnChequeReturn.ForeColor = System.Drawing.Color.Gray
                    End If

                End If
                conn.Close()
                conn.Dispose()
                command.Dispose()
                dt.Dispose()
                dr.Close()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION AccessControl", ex.Message.ToString, "")
        End Try
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


        btnChequeReturn.Enabled = False
        btnChequeReturn.ForeColor = System.Drawing.Color.Gray


        txtReceiptNo.Enabled = False
        txtReceiptDate.Enabled = False
        txtReceiptPeriod.Enabled = False
        txtCompanyGroup.Enabled = False
        txtAccountIdBilling.Enabled = False
        ddlContactType.Enabled = False
        txtAccountName.Enabled = False
        txtBankGLCode.Enabled = False

        'ddlBankCode.Enabled = True
        'txtBankGLCode.Enabled = True
        'ddlPaymentMode.Enabled = True
        'txtChequeNo.Enabled = True
        'txtChequeDate.Enabled = True

        txtComments.Enabled = False

        txtBankName.Enabled = False
        txtChequeDate.Enabled = False
        txtChequeNo.Enabled = False
        ddlBankCode.Enabled = False
        ddlPaymentMode.Enabled = False
        chkChequeReturned.Enabled = False

        txtReceivedAmount.Enabled = False

        txtLedgerName.Enabled = False
        txtBankID.Enabled = False

        btnShowInvoices.Enabled = False
        btnSave.Enabled = False


        grvBillingDetails.Enabled = False

        btnDelete.Enabled = False
        btnClient.Visible = False

        txtReceiptNoSearch.Enabled = True
        txtInvoicenoSearch.Enabled = True
        txtReceiptDateFromSearch.Enabled = True
        txtReceiptDateToSearch.Enabled = True
        ddlCompanyGrpSearch.Enabled = True
        ddlContactTypeSearch.Enabled = True
        txtAccountIdSearch.Enabled = True
        txtSearch1Status.Enabled = True
        btnQuickSearch.Enabled = True
        btnQuickReset.Enabled = True
        txtCommentsSearch.Enabled = True
        txtClientNameSearch.Enabled = True
        btnSearch1Status.Enabled = True
        'btnClientSearch0.Enabled = True
        btnClientSearch.Enabled = True
        'rdbSearchPaidStatus.Enabled = True
        ddlBankIDSearch.Enabled = True
        txtChequeNoSearch.Enabled = True


        btnFilter.Enabled = True
        btnJournal.Enabled = False

        Label60.Visible = False

        btnJournal.ForeColor = System.Drawing.Color.Gray

        btnJournal.Text = "JOURNAL & CONTRA"
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

        'btnChequeReturn.Enabled = False
        'btnChequeReturn.ForeColor = System.Drawing.Color.Gray
        'btnDelete.Enabled = False
        'btnDelete.ForeColor = System.Drawing.Color.Gray

        'rdbAll.Attributes.Remove("disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")

        'rdbAll.Enabled = True
        'rdbCompleted.Enabled = True
        'rdbNotCompleted.Enabled = True

        'If txtFrom.Text = "invoice" Or txtFrom.Text = "invoicePB" Then
        '    btnClient.Visible = False
        '    txtAccountIdBilling.Enabled = False
        '    txtAccountName.Enabled = False
        '    ddlContactType.Enabled = False
        'Else
        '    btnClient.Visible = True
        '    txtAccountIdBilling.Enabled = True
        '    txtAccountName.Enabled = True
        '    ddlContactType.Enabled = True
        'End If

        btnClient.Visible = True
        txtAccountIdBilling.Enabled = True
        txtAccountName.Enabled = True
        ddlContactType.Enabled = True

        txtReceiptNo.Enabled = True
        txtReceiptDate.Enabled = True
        txtReceiptPeriod.Enabled = True
        txtCompanyGroup.Enabled = True
        'txtAccountId.Enabled = True



        ddlBankCode.Enabled = True
        txtBankGLCode.Enabled = True
        ddlPaymentMode.Enabled = True

        txtChequeNo.Enabled = True
        txtChequeDate.Enabled = True

        'If ddlPaymentMode.Text = "CHEQUE" Then
        '    txtChequeNo.Attributes.Remove("readonly")
        '    txtChequeDate.Attributes.Remove("readonly")
        'Else
        '    txtChequeNo.Attributes.Add("readonly", "readonly")
        '    txtChequeDate.Attributes.Add("readonly", "readonly")
        'End If
        txtComments.Enabled = True

        txtBankName.Enabled = True
        txtChequeDate.Enabled = True
        'txtChequeNo.Enabled = True
        ddlBankCode.Enabled = True
        ddlPaymentMode.Enabled = True
        'chkChequeReturned.Enabled = True
        txtReceivedAmount.Enabled = True

        txtLedgerName.Enabled = True
        txtBankID.Enabled = True

        btnShowInvoices.Enabled = True
        btnSave.Enabled = True

        txtReceiptNoSearch.Enabled = False
        txtInvoicenoSearch.Enabled = False
        txtReceiptDateFromSearch.Enabled = False
        txtReceiptDateToSearch.Enabled = False
        ddlCompanyGrpSearch.Enabled = False
        ddlContactTypeSearch.Enabled = False
        txtAccountIdSearch.Enabled = False
        txtSearch1Status.Enabled = False
        btnQuickSearch.Enabled = False
        btnQuickReset.Enabled = False
        txtCommentsSearch.Enabled = False
        txtClientNameSearch.Enabled = False
        btnSearch1Status.Enabled = False
        'btnClientSearch0.Enabled = True
        btnClientSearch.Enabled = False
        'rdbSearchPaidStatus.Enabled = True
        ddlBankIDSearch.Enabled = False
        txtChequeNoSearch.Enabled = False


        btnFilter.Enabled = False
        btnJournal.Enabled = False
        btnJournal.ForeColor = System.Drawing.Color.Gray
        btnJournal.Text = "JOURNAL & CONTRA"
        updPnlSearch.Update()
        btnPrint.Enabled = False

        'btnEdit.Enabled = True
        'btnCopy.Enabled = True
        'btnChangeStatus.Enabled = True
        'btnDelete.Enabled = True
        'btnPrint.Enabled = True
        'btnPost.Enabled = True
        'btnDelete.Enabled = True

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

        'btnDelete.Enabled = True

        grvBillingDetails.Enabled = True
        'grvServiceRecDetails.Enabled = True
        updPnlBillingRecs.Update()
        'updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        'updPanelSearchServiceRec.Update()


        'BtnLocation.Visible = True

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
            txtSearch1Status.Text = "O,P"
            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtReceiptDate.Text)) & Format(Month(Convert.ToDateTime(txtReceiptDate.Text)), "00")

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), Constants.OpenConfirm, "confirm('Data Already Exists, do you Wish to Overwrite the existing record?')", True)


            'Me.Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Javascript", " return ConfirmPost()", True)


            ' '''''''''''''''''
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ConfirmPost();</Script>", False)

            ' '''''''''''''''''ss

            'Dim confirmValue As String
            'confirmValue = ""

            'confirmValue = Request.Form("confirm_value")
            'If Right(confirmValue, 3) = "Yes" Then
            '    btnPost_Click(sender, e)
            '    '''''''''''''ss
            'End If

            ' ''''''''''''''''
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnADD_Click", ex.Message.ToString, "")
        End Try
    End Sub

    'Button clic


    'Pop-up


    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Dim con As MySqlConnection = New MySqlConnection()
        Try
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
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString

            lblAlert.Text = exstr
            updPnlMsg.Update()
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateDropDownList", ex.Message.ToString, textField)
        End Try
        'End Using
    End Sub


    Public Sub PopulateComboBox(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As AjaxControlToolkit.ComboBox)
        Dim con As MySqlConnection = New MySqlConnection()
        Try
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
                con.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString

            lblAlert.Text = exstr
            updPnlMsg.Update()
        End Try
    End Sub

    Public Sub PopulateComboBoxSaplin(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As Saplin.Controls.DropDownCheckBoxes)
        Dim con As MySqlConnection = New MySqlConnection()

        con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'Using con As New MySqlConnection(constr)
        Using cmd As New MySqlCommand(query)
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            cmd.CommandTimeout = 0
            con.Open()
            cmb.DataSource = cmd.ExecuteReader()
            cmb.DataTextField = textField.Trim()
            cmb.DataValueField = valueField.Trim()
            cmb.DataBind()
            con.Close()
        End Using
        'End Using
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")
    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
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

            SQLDSReceipt.SelectCommand = txt.Text
            SQLDSReceipt.DataBind()
            GridView1.DataBind()
            'GridView1.DataBind()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "GridView1_PageIndexChanging", ex.Message.ToString, "")
        End Try
    End Sub


    'Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    lblAlert.Text = ""
    '    Try
    '        If txtPostStatus.Text = "P" Then
    '            lblAlert.Text = "Receipt has already been POSTED.. Cannot be DELETED"
    '            'Dim message1 As String = "alert('Contract has already been POSTED.. Cannot be DELETED!!!')"
    '            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message1, True)

    '            Exit Sub
    '        End If

    '        If txtPostStatus.Text = "V" Then
    '            lblAlert.Text = "Receipt is VOID.. Cannot be DELETED"
    '            'Dim message2 As String = "alert('Contract is VOID.. Cannot be DELETED!!!')"
    '            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message2, True)

    '            Exit Sub
    '        End If

    '        lblAlert.Text = ""
    '        lblMessage.Text = "ACTION: DELETE RECORD"


    '        Dim confirmValue As String
    '        confirmValue = ""

    '        confirmValue = Request.Form("confirm_value")
    '        If Right(confirmValue, 3) = "Yes" Then

    '            Dim conn As MySqlConnection = New MySqlConnection()

    '            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '            conn.Open()

    '            Dim command1 As MySqlCommand = New MySqlCommand
    '            command1.CommandType = CommandType.Text

    '            Dim qry1 As String = "DELETE from tblRecv where Rcno= @Rcno "

    '            command1.CommandText = qry1
    '            command1.Parameters.Clear()

    '            command1.Parameters.AddWithValue("@Rcno", txtRcno.Text)
    '            command1.Connection = conn
    '            command1.ExecuteNonQuery()



    '            'Dim command3 As MySqlCommand = New MySqlCommand
    '            'command3.CommandType = CommandType.Text

    '            'Dim qry3 As String = "DELETE from tblRecvDet where ReceiptNumber= @ReceiptNumber "

    '            'command3.CommandText = qry3
    '            'command3.Parameters.Clear()

    '            'command3.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)
    '            'command3.Connection = conn
    '            'command3.ExecuteNonQuery()


    '            ''''''''''''''''''''''''''''''''''

    '            'Start:Loop thru' Credit values

    '            Dim commandValues As MySqlCommand = New MySqlCommand

    '            commandValues.CommandType = CommandType.Text
    '            commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
    '            commandValues.Connection = conn

    '            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
    '            Dim dtValues As New DataTable
    '            dtValues.Load(drValues)

    '            Dim lTotalReceiptAmt As Decimal
    '            Dim lInvoiceAmt As Decimal
    '            'Dim lReceptAmtAdjusted As Decimal

    '            lTotalReceiptAmt = 0.0
    '            lInvoiceAmt = 0.0

    '            lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
    '            Dim rowindex = 0

    '            For Each row As DataRow In dtValues.Rows

    '                Dim lContractNo As String
    '                Dim lServiceRecordNo As String
    '                Dim lServiceDate As String

    '                If Convert.ToDecimal(row("ReceiptValue")) > 0.0 Then

    '                    Dim commandUpdateInvoiceValue As MySqlCommand = New MySqlCommand

    '                    commandUpdateInvoiceValue.CommandType = CommandType.Text
    '                    'commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblservicebillingdetailitem where rcno = " & row("RcnoServiceBillingItem")
    '                    commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblSalesDetail where rcno = " & row("RcnoServiceBillingItem")
    '                    commandUpdateInvoiceValue.Connection = conn

    '                    Dim drInvoiceValue As MySqlDataReader = commandUpdateInvoiceValue.ExecuteReader()
    '                    Dim dtInvoiceValue As New DataTable
    '                    dtInvoiceValue.Load(drInvoiceValue)

    '                    lContractNo = ""
    '                    lServiceRecordNo = ""
    '                    lServiceDate = ""

    '                    For Each row1 As DataRow In dtInvoiceValue.Rows

    '                        Dim command3 As MySqlCommand = New MySqlCommand
    '                        command3.CommandType = CommandType.Text

    '                        Dim qry3 As String = "DELETE from tblRecvDet where rcno= @rcno "

    '                        command3.CommandText = qry3
    '                        command3.Parameters.Clear()

    '                        command3.Parameters.AddWithValue("@rcno", row("Rcno"))
    '                        command3.Connection = conn
    '                        command3.ExecuteNonQuery()

    '                        Updatetblservicebillingdetailitem(row("RcnoServiceBillingItem"))

    '                    Next row1
    '                End If
    '            Next row

    '            ''''''''''''''''''''''''''''''''''

    '            conn.Close()
    '            conn.Dispose()
    '            calculateTotalReceipt()

    '            'btnADD_Click(sender, e)

    '            'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
    '            SQLDSReceipt.SelectCommand = txt.Text
    '            SQLDSReceipt.DataBind()
    '            'GridView1.DataSourceID = "SqlDSContract"

    '            MakeMeNull()
    '            MakeMeNullBillingDetails()
    '            FirstGridViewRowGL()
    '            GridView1.DataBind()

    '            'Dim message As String = "alert('Contract is deleted Successfully!!!')"
    '            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
    '            lblMessage.Text = "DELETE: RECEIPT SUCCESSFULLY DELETED"
    '            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

    '            updPnlMsg.Update()
    '            updPnlSearch.Update()
    '            'updpnlServiceRecs.Update()
    '            'GridView1.DataBind()
    '        End If
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.Message.ToString

    '        lblAlert.Text = exstr
    '        InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnDelete_Click", ex.Message.ToString, "")

    '    End Try
    'End Sub


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



    Private Function PostReceipt() As Boolean
        Try
            lblAlert.Text = ""
            Dim IsSuccess As Boolean = False


            If Convert.ToDecimal(txtReceiptAmt.Text) <> Convert.ToDecimal(txtReceivedAmount.Text) Then
                lblAlert.Text = "RECEIPT AMOUNT AND SUM OF APPLIED RECEIPTS SHOULD BE EQUAL.. CANNOT BE POSTED"
                updPnlMsg.Update()
                btnSave.Enabled = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                IsSuccess = False
                Return IsSuccess
                Exit Function
            End If


            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()



            '''''''''''''''''''''''''''''''''''''''''''
            Dim lStatusIsHeaderDetailEqual As String
            Dim commandIsHeaderDetailEqual As MySqlCommand = New MySqlCommand

            commandIsHeaderDetailEqual.CommandType = CommandType.StoredProcedure
            commandIsHeaderDetailEqual.CommandText = "IsHeaderDetailEqual"
            'commandIsExists.Connection = conn
            commandIsHeaderDetailEqual.Parameters.Clear()

            commandIsHeaderDetailEqual.Parameters.AddWithValue("@pr_Module", "RCT")
            commandIsHeaderDetailEqual.Parameters.AddWithValue("@pr_DocumentNo", txtReceiptNo.Text.Trim)

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
                lblAlert.Text = "RECEIPT AMOUNT AND SUM OF APPLIED RECEIPTS SHOULD BE EQUAL.. CANNOT BE POSTED"
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

            '''''''''''''''''''
            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblrecv set PostStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()

            '''''''''''''''''''''

            'Dim command5 As MySqlCommand = New MySqlCommand
            'command5.CommandType = CommandType.Text

            'Dim qry5 As String = "DELETE from tblAR where VoucherNumber = '" & txtReceiptNo.Text & "'"

            'command5.CommandText = qry5
            ''command1.Parameters.Clear()
            'command5.Connection = conn
            'command5.ExecuteNonQuery()

            Dim qryAR As String
            Dim commandAR As MySqlCommand = New MySqlCommand
            commandAR.CommandType = CommandType.Text

            'If Convert.ToDecimal(txtReceiptAmt.Text) > 0.0 Then
            '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, BillingPeriod, "
            '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @BillingPeriod,"
            '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    commandAR.CommandText = qryAR
            '    commandAR.Parameters.Clear()
            '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    If txtReceiptDate.Text.Trim = "" Then
            '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    Else
            '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    End If
            '    commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
            '    commandAR.Parameters.AddWithValue("@ContractNo", "")
            '    commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
            '    commandAR.Parameters.AddWithValue("@GLCode", txtBankGLCode.Text)
            '    commandAR.Parameters.AddWithValue("@GLDescription", txtBankName.Text)
            '    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtReceivedAmount.Text))
            '    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
            '    commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
            '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    commandAR.Connection = conn
            '    commandAR.ExecuteNonQuery()
            'End If


            'Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            commandValues.CommandText = "SELECT DocType, RefType, SourceRcno, AppliedBase, ContractNo, ServiceRecordNo  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)


            Dim lTotalReceiptAmt As Decimal
            Dim lInvoiceAmt As Decimal
            'Dim lReceptAmtAdjusted As Decimal

            lTotalReceiptAmt = 0.0
            lInvoiceAmt = 0.0

            lTotalReceiptAmt = dtValues.Rows(0)("AppliedBase").ToString
            Dim rowindex = 0

            For Each row As DataRow In dtValues.Rows

                ''Start: Update tblSales

                ''''''''''''''''''''''
                If String.IsNullOrEmpty(row("RefType")) = False Then
                    UpdateTblSales(row("RefType"), row("DocType"), row("SourceRcno"), row("AppliedBase"))
                End If

                'UpdateTblSales(row("RefType"), row("DocType"), row("SourceRcno"))

                ''''''''''''''''''''''''' Sales Commission

                ''''''''''' Sales Commission
                If Not IsDBNull(row("ContractNo")) Then
                    'If IsDBNull(row("ContractNo") = False) Then
                    If String.IsNullOrEmpty(row("ContractNo")) = False Then
                        Dim sqlCommissionPct As String
                        sqlCommissionPct = ""

                        sqlCommissionPct = "Select SalesCommissionPerc, TechCommissionPerc from tblContract where Contractno = '" & row("ContractNo") & "'"

                        Dim commandCommissionPct As MySqlCommand = New MySqlCommand
                        commandCommissionPct.CommandType = CommandType.Text
                        commandCommissionPct.CommandText = sqlCommissionPct
                        commandCommissionPct.Connection = conn

                        Dim drCommissionPct As MySqlDataReader = commandCommissionPct.ExecuteReader()

                        Dim dtCommissionPct As New DataTable
                        dtCommissionPct.Load(drCommissionPct)

                        If Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString) > 0 Then

                            ''''''''''' Sales Commission

                            Dim qrySalesCommission As String

                            'Dim qryAR As String
                            Dim commandSalesCommission As MySqlCommand = New MySqlCommand
                            commandSalesCommission.CommandType = CommandType.Text

                            qrySalesCommission = "INSERT INTO tblsalesCommission( CompanyGroup, AccountId, CustomerName, SalesDate,  InvoiceNumber,  DatePaid, ReceiptNumber, Terms, Salesman, ValueBase, ValueOriginal,   SalesCommissionPerc, SalesCommissionAmt, TechCommissionPerc, TechCommissionAmt, ServiceGroup, ContractGroup, ContractNo, GLPeriod, ItemCode, TechnicianId, TechnicianName, InvoiceAmount, PaymentType, "
                            qrySalesCommission = qrySalesCommission + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                            qrySalesCommission = qrySalesCommission + " ( @CompanyGroup, @AccountId, @CustomerName, @SalesDate,  @InvoiceNumber,  @DatePaid, @ReceiptNumber, @Terms, @Salesman, @ValueBase, @ValueOriginal,  @SalesCommissionPerc, @SalesCommissionAmt, @TechCommissionPerc, @TechCommissionAmt,  @ServiceGroup, @ContractGroup, @ContractNo, @GLPeriod, @ItemCode, @TechnicianId, @TechnicianName, @InvoiceAmount, @PaymentType, "
                            qrySalesCommission = qrySalesCommission + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                            commandSalesCommission.CommandText = qrySalesCommission
                            commandSalesCommission.Parameters.Clear()

                            commandSalesCommission.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                            commandSalesCommission.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                            commandSalesCommission.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)

                            If row("InvoiceDate").ToString = "" Then
                                commandSalesCommission.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                            Else
                                commandSalesCommission.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(row("InvoiceDate")).ToString("yyyy-MM-dd"))
                            End If
                            commandSalesCommission.Parameters.AddWithValue("@InvoiceNumber", row("RefType"))

                            If txtReceiptDate.Text.Trim = "" Then
                                commandSalesCommission.Parameters.AddWithValue("@DatePaid", DBNull.Value)
                            Else
                                commandSalesCommission.Parameters.AddWithValue("@DatePaid", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
                            End If
                            commandSalesCommission.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

                            commandSalesCommission.Parameters.AddWithValue("@Terms", row("Terms"))
                            commandSalesCommission.Parameters.AddWithValue("@Salesman", txtSalesman.Text)

                            commandSalesCommission.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)
                            commandSalesCommission.Parameters.AddWithValue("@ContractNo", row("ContractNo"))

                            commandSalesCommission.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(row("ValueBase")))
                            commandSalesCommission.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(row("ValueBase")))
                            commandSalesCommission.Parameters.AddWithValue("@SalesCommissionPerc", Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString))
                            commandSalesCommission.Parameters.AddWithValue("@SalesCommissionAmt", Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString) * 0.01 * Convert.ToDecimal(row("ValueBase")))

                            commandSalesCommission.Parameters.AddWithValue("@TechCommissionPerc", 0.0)
                            commandSalesCommission.Parameters.AddWithValue("@TechCommissionAmt", 0.0)

                            commandSalesCommission.Parameters.AddWithValue("@ServiceGroup", row("InvoiceType"))
                            commandSalesCommission.Parameters.AddWithValue("@ContractGroup", row("ContractGroup"))
                            commandSalesCommission.Parameters.AddWithValue("@ItemCode", row("ItemCode"))

                            commandSalesCommission.Parameters.AddWithValue("@TechnicianId", "")
                            commandSalesCommission.Parameters.AddWithValue("@TechnicianName", "")
                            commandSalesCommission.Parameters.AddWithValue("@InvoiceAmount", row("InvoiceValue"))

                            If ddlPaymentMode.Text = "--SELECT--" Then
                                commandSalesCommission.Parameters.AddWithValue("@PaymentType", "")
                            Else
                                commandSalesCommission.Parameters.AddWithValue("@PaymentType", ddlPaymentMode.Text)
                            End If


                            commandSalesCommission.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                            commandSalesCommission.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                            commandSalesCommission.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                            commandSalesCommission.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                            commandSalesCommission.Connection = conn
                            commandSalesCommission.ExecuteNonQuery()
                        End If





                        '''''''''''''''''''''''''' Sales Commission


                        ''''''''''''''''''''''''' Technician Commission

                        If String.IsNullOrEmpty(row("ServiceRecordNo")) = False Then
                            If Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString) > 0 Then


                                Dim sqlTechnician As String
                                sqlTechnician = ""

                                sqlTechnician = "Select StaffId, StaffName from tblServiceRecordStaff where RecordNo = '" & row("ServiceRecordNo") & "'"

                                Dim commandTechnician As MySqlCommand = New MySqlCommand
                                commandTechnician.CommandType = CommandType.Text
                                commandTechnician.CommandText = sqlTechnician
                                commandTechnician.Connection = conn

                                Dim drTechnician As MySqlDataReader = commandTechnician.ExecuteReader()

                                Dim dtTechnician As New DataTable
                                dtTechnician.Load(drTechnician)

                                For Each row1 As DataRow In dtTechnician.Rows


                                    If String.IsNullOrEmpty(dtTechnician.Rows(0)("StaffId").ToString) = False Then

                                        Dim qrySalesCommission As String
                                        Dim commandSalesCommission As MySqlCommand = New MySqlCommand
                                        commandSalesCommission.CommandType = CommandType.Text

                                        qrySalesCommission = "INSERT INTO tblsalesCommission( CompanyGroup, AccountId, CustomerName, SalesDate,  InvoiceNumber,  DatePaid, ReceiptNumber, Terms, Salesman, ValueBase, ValueOriginal,   SalesCommissionPerc, SalesCommissionAmt, TechCommissionPerc, TechCommissionAmt, ServiceGroup, ContractGroup, ContractNo, GLPeriod, ItemCode, TechnicianId, TechnicianName,  "
                                        qrySalesCommission = qrySalesCommission + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                        qrySalesCommission = qrySalesCommission + " ( @CompanyGroup, @AccountId, @CustomerName, @SalesDate,  @InvoiceNumber,  @DatePaid, @ReceiptNumber, @Terms, @Salesman, @ValueBase, @ValueOriginal,  @SalesCommissionPerc, @SalesCommissionAmt, @TechCommissionPerc, @TechCommissionAmt,  @ServiceGroup, @ContractGroup, @ContractNo, @GLPeriod, @ItemCode, @TechnicianId, @TechnicianName, "
                                        qrySalesCommission = qrySalesCommission + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                        commandSalesCommission.CommandText = qrySalesCommission
                                        commandSalesCommission.Parameters.Clear()

                                        commandSalesCommission.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                        commandSalesCommission.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                                        commandSalesCommission.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)

                                        If row("InvoiceDate").ToString = "" Then
                                            commandSalesCommission.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                                        Else
                                            commandSalesCommission.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(row("InvoiceDate")).ToString("yyyy-MM-dd"))
                                        End If
                                        commandSalesCommission.Parameters.AddWithValue("@InvoiceNumber", row("RefType"))

                                        If txtReceiptDate.Text.Trim = "" Then
                                            commandSalesCommission.Parameters.AddWithValue("@DatePaid", DBNull.Value)
                                        Else
                                            commandSalesCommission.Parameters.AddWithValue("@DatePaid", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
                                        End If
                                        commandSalesCommission.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

                                        commandSalesCommission.Parameters.AddWithValue("@Terms", row("Terms"))
                                        commandSalesCommission.Parameters.AddWithValue("@Salesman", "")

                                        commandSalesCommission.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)
                                        commandSalesCommission.Parameters.AddWithValue("@ContractNo", row("ContractNo"))

                                        commandSalesCommission.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(row("ValueBase")))
                                        commandSalesCommission.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(row("ValueBase")))
                                        commandSalesCommission.Parameters.AddWithValue("@SalesCommissionPerc", 0.0)
                                        commandSalesCommission.Parameters.AddWithValue("@SalesCommissionAmt", 0.0)

                                        commandSalesCommission.Parameters.AddWithValue("@TechCommissionPerc", Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString))
                                        commandSalesCommission.Parameters.AddWithValue("@TechCommissionAmt", Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString) * 0.01 * Convert.ToDecimal(row("ValueBase")))

                                        commandSalesCommission.Parameters.AddWithValue("@ServiceGroup", row("InvoiceType"))
                                        commandSalesCommission.Parameters.AddWithValue("@ContractGroup", txtCompanyGroup.Text)
                                        commandSalesCommission.Parameters.AddWithValue("@ItemCode", row("ItemCode"))

                                        commandSalesCommission.Parameters.AddWithValue("@TechnicianId", row1("StaffId").ToString)
                                        commandSalesCommission.Parameters.AddWithValue("@TechnicianName", row1("StaffName").ToString)

                                        commandSalesCommission.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                        commandSalesCommission.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                                        commandSalesCommission.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                        commandSalesCommission.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                                        commandSalesCommission.Connection = conn
                                        commandSalesCommission.ExecuteNonQuery()
                                    End If
                                Next
                            End If
                        End If
                    End If

                End If


                ''''''''''''''''''''''' Technician Commission
                rowindex = rowindex + 1
            Next row

            GridView1.DataBind()
            conn.Close()
            conn.Dispose()

            commandAR.Dispose()
            commandUpdateInvoice.Dispose()
            commandValues.Dispose()

            'lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'lblAlert.Text = ""
            'updPnlSearch.Update()
            'updPnlMsg.Update()
            'updpnlBillingDetails.Update()
            ''updpnlServiceRecs.Update()
            'updpnlBillingDetails.Update()

            'btnQuickSearch_Click(sender, e)

            'btnChangeStatus.Enabled = True
            'btnChangeStatus.ForeColor = System.Drawing.Color.Black

            'btnEdit.Enabled = False
            'btnEdit.ForeColor = System.Drawing.Color.Gray

            'btnDelete.Enabled = False
            'btnDelete.ForeColor = System.Drawing.Color.Gray

            'btnPost.Enabled = False
            'btnPost.ForeColor = System.Drawing.Color.Gray

            'End If


            'End: Loop thru' Credit Values


            ''''''''''''''' Insert tblAR

            IsSuccess = True
            Return IsSuccess
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PostReceipt", ex.Message.ToString, txtReceiptNo.Text)
        End Try

    End Function

    Private Function ReverseReceipt() As Boolean
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

            Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtReceiptNo.Text & "'"

            command5.CommandText = qry5
            'command1.Parameters.Clear()
            command5.Connection = conn
            command5.ExecuteNonQuery()


            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblrecv set PostStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()


            ''''''''''''''''''''''''''''''''

            'Start:Loop thru' Credit values

            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)


            For Each row As DataRow In dtValues.Rows

                ''Start: Update tblSales

                ' ''''''''''''''''''''
                'If String.IsNullOrEmpty(row("InvoiceNo")) = False Then
                '    UpdateTblSales(row("InvoiceNo"))
                'End If

                If String.IsNullOrEmpty(row("RefType")) = False Then
                    UpdateTblSales(row("RefType"), row("DocTYpe"), row("SourceRcno"), row("AppliedBase"))
                End If
            Next

            conn.Close()
            conn.Dispose()
            commandValues.Dispose()
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
            Dim IsSuccess As Boolean

            IsSuccess = False
            Return IsSuccess
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION ReverseReceipt", ex.Message.ToString, txtReceiptNo.Text)
        End Try
    End Function

    ''''''' Start: Service Details



    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Session("serviceschedulefrom") = ""
        'Session("contractno") = ""
        'Session("contractdetailfrom") = ""

        'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"

        'txtAccountIdSearch.Text = ""
        'txtBillingPeriodSearch.Text = ""
        'txtInvoicenoSearch.Text = ""
        'txtClientNameSearch.Text = ""
        ''txtBillingPeriodSearch.Text = ""
        'ddlSalesmanSearch.SelectedIndex = 0
        'txtSearch1Status.Text = "O"
        'ddlCompanyGrpSearch.SelectedIndex = 0
        'btnSearch1Status_Click(sender, e)
        'SQLDSInvoice.SelectCommand = txt.Text
        'SQLDSInvoice.DataBind()
        'GridView1.DataSourceID = "SqlDSContract"

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

        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO EDIT"
            Return
        End If

        lblMessage.Text = "ACTION: EDIT RECORD"

        txtMode.Text = "EDIT"
        EnableControls()

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnChangeStatus.Enabled = False
        btnChangeStatus.ForeColor = System.Drawing.Color.Gray

        btnPost.Enabled = False
        btnPost.ForeColor = System.Drawing.Color.Gray
        AddNewRowBillingDetailsRecs()

        'btnDelete.Enabled = True
        'btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
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
        Dim strsql As String
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""

            txtTotalInvoiceAmount.Text = "0.00"
            txtCondition.Text = ""
            strsql = ""

            If String.IsNullOrEmpty(txtInvoicenoSearch.Text) = True Then
                strsql = "SELECT A.PostStatus,  A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman, A.Location, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno, A.ChequeReturned FROM tblrecv A where  1=1  "
                txtGrid.Text = "SELECT A.PostStatus,  A.ReceiptNumber, A.ReceiptDate, A.Cheque, A.AccountId, A.ContactType, A.ReceiptFrom, A.AppliedBase, A.BankId,  A.PaymentType, A.GlPeriod, A.CompanyGroup,   A.ChequeDate, A.ChequeReturned, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn  FROM tblrecv A where  1=1"
            Else
                strsql = "SELECT  A.PostStatus,  A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman, A.Location, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno, A.ChequeReturned FROM tblrecv A, tblRecvDet B where A.ReceiptNumber = B.ReceiptNumber and  1=1  "
                txtGrid.Text = "SELECT A.PostStatus,  A.ReceiptNumber, A.ReceiptDate, A.Cheque, A.AccountId, A.ContactType, A.ReceiptFrom, A.AppliedBase, A.BankId,  A.PaymentType, A.GlPeriod, A.CompanyGroup,   A.ChequeDate, A.ChequeReturned, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn FROM tblrecv A, tblRecvDet B where A.ReceiptNumber = B.ReceiptNumber and  1=1 "

            End If

            'txtCondition.Text = " and GLPeriod = concat(year(now()), if(length(month(now()))=1, concat('0', month(now())),month(now()))) "

            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                txtCondition.Text = txtCondition.Text & " and A.PostStatus in (" + YrStr + ")"
                'txtCondition.Text = txtCondition.Text & " and Location = '" & txtLocation.Text & "'"
            End If


            If txtDisplayRecordsLocationwise.Text = "Y" Then

                txtCondition.Text = txtCondition.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"


                'If ddlBranch.SelectedIndex > 0 Then
                '    If String.IsNullOrEmpty(ddlBranch.Text) = False Then
                '        txtCondition.Text = txtCondition.Text & " and location = '" & ddlBranch.Text.Trim + "'"
                '    End If
                'End If


                '17.01.24


                Dim YrStrList1 As List(Of [String]) = New List(Of String)()

                For Each item As ListItem In ddlLocationSearch.Items
                    If item.Selected Then

                        YrStrList1.Add("""" + item.Value + """")

                    End If
                Next

                If YrStrList1.Count > 0 Then

                    Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                    txtCondition.Text = txtCondition.Text + " and a.location in (" + YrStr + ")"

                Else
                    txtCondition.Text = txtCondition.Text & " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                End If


                '17.01.24
            End If

            If ddlCompanyGrpSearch.SelectedIndex > 0 Then
                If String.IsNullOrEmpty(ddlCompanyGrpSearch.Text) = False Then
                    txtCondition.Text = txtCondition.Text & " and CompanyGroup = '" & ddlCompanyGrpSearch.Text.Trim + "'"
                End If
            End If

            'If String.IsNullOrEmpty(txtBillingPeriodSearch.Text) = False Then
            '    strsql = strsql & " and A.GLPeriod like '%" & txtBillingPeriodSearch.Text.Trim + "%'"
            'End If


            If String.IsNullOrEmpty(txtReceiptNoSearch.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and A.ReceiptNumber like '%" & txtReceiptNoSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtAccountIdSearch.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and (A.AccountId like '%" & txtAccountIdSearch.Text & "%' or A.AccountId like '%" & txtAccountIdSearch.Text & "%')"
            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and A.ReceiptFrom like ""%" & txtClientNameSearch.Text & "%"""
            End If

            If String.IsNullOrEmpty(txtInvoicenoSearch.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and B.RefType ='" & txtInvoicenoSearch.Text.Trim + "'"
            End If

            If (ddlCompanyGrpSearch.SelectedIndex > 0) Then
                txtCondition.Text = txtCondition.Text & " and A.CompanyGroup like '%" & ddlCompanyGrpSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtReceiptDateFromSearch.Text) = False And txtReceiptDateFromSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ReceiptDate >= '" + Convert.ToDateTime(txtReceiptDateFromSearch.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtReceiptDateToSearch.Text.Trim) = False And txtReceiptDateToSearch.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ReceiptDate <= '" + Convert.ToDateTime(txtReceiptDateToSearch.Text).ToString("yyyy-MM-dd") + "'"
            End If


            If String.IsNullOrEmpty(txtCommentsSearch.Text.Trim.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and Comments like '%" & txtCommentsSearch.Text & "%'"
            End If


            If String.IsNullOrEmpty(txtChequeNoSearch.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and Cheque like '%" & txtChequeNoSearch.Text & "%'"
            End If

            If ddlBankIDSearch.SelectedIndex > 0 Then
                txtCondition.Text = txtCondition.Text & " and Bankid like '%" & ddlBankIDSearch.Text & "%'"
            End If

            If ddlPaymentModeSearch.SelectedIndex > 0 Then
                txtCondition.Text = txtCondition.Text & " and PaymentType = '" & ddlPaymentModeSearch.Text & "'"
            End If


            'If (ddlSalesmanSearch.SelectedIndex > 0) Then
            '    strsql = strsql & " and Salesman like '%" & ddlSalesmanSearch.Text.Trim + "%'"
            'End If
            'ORDER BY Rcno DESC, ReceiptFrom"


            txtOrderBy.Text = " order by A.Rcno desc, A.ReceiptFrom" + " limit " & txtLimit.Text
            'txtOrderBy.Text = " order by A.Rcno desc, A.ReceiptFrom"
            strsql = strsql + txtCondition.Text + txtOrderBy.Text
            txt.Text = strsql

            txtGrid.Text = txtGrid.Text + txtCondition.Text + txtOrderBy.Text

            'strsql = strsql + " order by A.Rcno desc, A.ReceiptFrom;"
            'txt.Text = strsql

            SQLDSReceipt.SelectCommand = strsql
            SQLDSReceipt.DataBind()
            GridView1.DataBind()

            CalculateTotal()



            If Convert.ToInt32(txtRowCount.Text) > 0 Then

                'btnQuickSearch_Click(sender, e)

                Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
                MakeMeNull()
                MakeMeNullBillingDetails()

                If GridView1.Rows.Count > 0 Then
                    txtMode.Text = "View"
                    txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                    PopulateRecord()
                    'UpdatePanel2.Update()

                    'updPanelSave.Update()
                    'UpdatePanel3.Update()

                    'GridView1_SelectedIndexChanged(sender, e)
                End If
            End If

            lblMessage.Text = "NUMBER OF RECORDS FOUND : " + txtRowCount.Text + "   [DISPLAYING TOP " + txtLimit.Text + " RECORDS]"

            updPnlMsg.Update()
            'GridSelected = "SQLDSContract"
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnQuickSearch_Click", ex.Message.ToString, strsql)
        End Try
    End Sub


    Protected Sub btnShowRecords_Click(sender As Object, e As EventArgs)
        'PopulateServiceGrid()
        'btnSave.Enabled = True
        'updPanelSave.Update()
    End Sub

    Private Sub PopulateServiceGrid()

        Try

            'Start: Service Recods

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                grvBillingDetails.DataSource = dtScdrLoc
                grvBillingDetails.DataBind()

                SetPreviousDataBillingDetailsRecs()
                'SetPreviousDataServiceRecs()
            Next i

            FirstGridViewRowBillingDetailsRecs()
            'FirstGridViewRowServiceRecs()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim sql As String
            sql = ""

            If txtMode.Text = "View" Then

                sql = "Select A.Rcno, A.InvoiceNo, A.InvoicePrice, A.InvoiceGST, A.InvoiceValue, a.Description, A.SubCode, "
                sql = sql + "  A.ReceiptValue, A.InvoiceDate, A.RcnoServiceBillingItem , A.ContractNo, A.ServiceRecordNo, A.InvoiceType, A.TaxType, A.LedgerCode, A.LedgerName, A.AppliedBase, A.RefType, DocTYpe, AccountId, AccountName  "
                sql = sql + " FROM tblrecvdet A "
                sql = sql + " where 1 = 1 "
                sql = sql + " and ReceiptNumber = '" & txtReceiptNo.Text & "' Limit " & ddlViewServiceRecs.Text

                txtsqldetail.Text = sql
            Else
                If ddlContactType.SelectedIndex = 0 Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, "
                    sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, B.Address1, "
                    sql = sql + " B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, A.ContactType, A.CompanyGroup, A.ChequeReturned  "
                    sql = sql + " FROM tblrecv A, tblCompany B, tblContract C "
                    sql = sql + " where 1 = 1  and A.BillYN= 'N' and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and A.ContactType = '" & ddlContactType.Text.Trim & "'"
                ElseIf ddlContactType.SelectedIndex = 1 Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, "
                    sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, B.Address1, "
                    sql = sql + " B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, A.ContactType, A.CompanyGroup  "
                    sql = sql + " FROM tblservicerecord A, tblPerson B, tblContract C "
                    sql = sql + " where 1 = 1  and A.BillYN= 'N' and  A.AccountId = B.AccountId and A.ContractNo = C.ContractNo and A.ContactType = '" & ddlContactType.Text.Trim & "'"

                End If
            End If


            Dim Total As Decimal
            Dim TotalWithGST As Decimal
            Dim TotalDiscAmt As Decimal
            Dim TotalGSTAmt As Decimal
            Dim TotalPriceWithDiscountAmt As Decimal
            Dim TotalReceiptAmt As Decimal
            Dim ReceiptAmt As Decimal


            Total = 0.0
            TotalWithGST = 0.0
            TotalDiscAmt = 0.0
            TotalGSTAmt = 0.0
            TotalPriceWithDiscountAmt = 0.0
            TotalReceiptAmt = 0.0
            ReceiptAmt = 0.0

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)

            Dim TotDetailRecordsServiceRec = dt.Rows.Count
            If dt.Rows.Count > 0 Then

                Dim rowIndex = 0

                For Each row As DataRow In dt.Rows
                    If (TotDetailRecordsServiceRec > (rowIndex + 1)) Then
                        'AddNewRowLoc()
                        AddNewRowBillingDetailsRecs()
                        'AddNewRowServiceRecs()
                    End If



                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    TextBoxItemType.Text = Convert.ToString(dt.Rows(rowIndex)("SubCode"))

                    Dim TextBoxRcnoRecvDet As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoReceiptGV"), TextBox)
                    TextBoxRcnoRecvDet.Text = Convert.ToString(dt.Rows(rowIndex)("Rcno"))


                    Dim TextBoxTotalInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    'TextBoxTotalInvoiceNo.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceNo"))
                    TextBoxTotalInvoiceNo.Text = Convert.ToString(dt.Rows(rowIndex)("RefType"))

                    Dim TextBoxTotalInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    If String.IsNullOrEmpty(dt.Rows(rowIndex)("InvoiceDate").ToString) = False Then
                        TextBoxTotalInvoiceDate.Text = Convert.ToDateTime(dt.Rows(rowIndex)("InvoiceDate")).ToString("dd/MM/yyyy")
                    Else
                        TextBoxTotalInvoiceDate.Text = ""
                    End If


                    'Convert.ToDateTime(dtServiceBillingDetailItem.Rows(rowIndex)("SalesDate")).ToString("dd/MM/yyyy")
                    Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    TextBoxTotalPriceWithDisc.Text = Convert.ToString(dt.Rows(rowIndex)("InvoicePrice"))

                    Dim TextBoxTotalInvoiceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    TextBoxTotalInvoiceValue.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceGST"))


                    Dim TextBoxTotalTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalTotalPriceWithGST.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceValue"))

                    Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                    TextBoxContractNo.Text = Convert.ToString(dt.Rows(rowIndex)("ContractNo"))

                    Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceNoGV"), TextBox)
                    TextBoxServiceRecordNo.Text = Convert.ToString(dt.Rows(rowIndex)("ServiceRecordNo"))

                    Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)
                    TextBoxInvoiceType.Text = Convert.ToString(dt.Rows(rowIndex)("InvoiceType"))

                    Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTaxTypeGV"), TextBox)
                    TextBoxTaxType.Text = Convert.ToString(dt.Rows(rowIndex)("TaxType"))


                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    TextBoxOtherCode.Text = Convert.ToString(dt.Rows(rowIndex)("LedgerCode"))

                    Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRemarksGV"), TextBox)
                    TextBoxRemarks.Text = Convert.ToString(dt.Rows(rowIndex)("Description"))

                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    TextBoxGLDescription.Text = Convert.ToString(dt.Rows(rowIndex)("LedgerName"))

                    Dim TextBoxDocType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)
                    TextBoxDocType.Text = Convert.ToString(dt.Rows(rowIndex)("DocTYpe"))


                    Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIDGV"), TextBox)
                    TextBoxAccountID.Text = Convert.ToString(dt.Rows(rowIndex)("AccountID"))

                    Dim TextBoxAccountName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustomerNameGV"), TextBox)
                    TextBoxAccountName.Text = Convert.ToString(dt.Rows(rowIndex)("AccountName"))


                    'quoted on 04.07.17
                    'Dim RcnoServiceBillingItemx As Long

                    'RcnoServiceBillingItemx = dt.Rows(rowIndex)("RcnoServiceBillingItem")
                    ' '''''''''''''''''''''''''''''''''''''''
                    'sql = "Select ReceiptAmount as Totalreceipt from tblSalesDetail where  rcno =" & Convert.ToInt64(RcnoServiceBillingItemx)

                    'Dim command23 As MySqlCommand = New MySqlCommand
                    'command23.CommandType = CommandType.Text
                    'command23.CommandText = sql
                    'command23.Connection = conn

                    'Dim dr23 As MySqlDataReader = command23.ExecuteReader()

                    'Dim dt23 As New DataTable
                    'dt23.Load(dr23)


                    ' ''''''''''''''''''''''''''''''''''''''''''
                    'Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    'If dt23.Rows.Count > 0 Then

                    '    TextBoxTotalReceiptAmt.Text = Convert.ToString(dt23.Rows(0)("Totalreceipt").ToString)

                    'End If

                    ' '''''''''''''''''''''''''''''''''''''''
                    'sql = "Select CreditAmount as TotalCN from tblSalesDetail where rcno =" & Convert.ToInt64(RcnoServiceBillingItemx)

                    'Dim command24 As MySqlCommand = New MySqlCommand
                    'command24.CommandType = CommandType.Text
                    'command24.CommandText = sql
                    'command24.Connection = conn

                    'Dim dr24 As MySqlDataReader = command24.ExecuteReader()

                    'Dim dt24 As New DataTable
                    'dt24.Load(dr24)


                    ' ''''''''''''''''''''''''''''''''''''''''''
                    'Dim totalCNAmount As Decimal
                    'totalCNAmount = 0.0

                    'If dt24.Rows.Count > 0 Then
                    '    If String.IsNullOrEmpty(dt24.Rows(0)("TotalCN").ToString) = True Then
                    '        totalCNAmount = 0.0
                    '    Else
                    '        totalCNAmount = Convert.ToDecimal(dt24.Rows(0)("TotalCN").ToString)
                    '    End If
                    'End If

                    'Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    'TextBoxTotalCNAmt.Text = (Convert.ToDecimal(totalCNAmount)).ToString("N2")

                    'quoted on 04.07.17


                    'Start: 04.07.17

                    Dim RcnoServiceBillingItemx As Long

                    RcnoServiceBillingItemx = dt.Rows(rowIndex)("RcnoServiceBillingItem")
                    '''''''''''''''''''''''''''''''''''''''
                    Dim totalCNAmount As Decimal
                    totalCNAmount = 0.0

                    Dim totalReceiptAmount As Decimal
                    totalReceiptAmount = 0.0
                    sql = "Select ReceiptBase as Totalreceipt, CreditBase as TotalCN from tblSales where  InvoiceNumber = '" & Convert.ToString(dt.Rows(rowIndex)("RefType")) & "'"

                    Dim command23 As MySqlCommand = New MySqlCommand
                    command23.CommandType = CommandType.Text
                    command23.CommandText = sql
                    command23.Connection = conn

                    Dim dr23 As MySqlDataReader = command23.ExecuteReader()

                    Dim dt23 As New DataTable
                    dt23.Load(dr23)


                    ''''''''''''''''''''''''''''''''''''''''''

                    If dt23.Rows.Count > 0 Then



                        If String.IsNullOrEmpty(dt23.Rows(0)("Totalreceipt").ToString) = True Then
                            totalReceiptAmount = 0.0
                        Else
                            totalReceiptAmount = Convert.ToDecimal(dt23.Rows(0)("Totalreceipt").ToString)
                        End If

                        If String.IsNullOrEmpty(dt23.Rows(0)("TotalCN").ToString) = True Then
                            totalCNAmount = 0.0
                        Else
                            totalCNAmount = Convert.ToDecimal(dt23.Rows(0)("TotalCN").ToString)
                        End If
                    End If

                    '''''''''''''''''''''''''''''''''''''''
                    'sql = "Select CreditAmount as TotalCN from tblSalesDetail where rcno =" & Convert.ToInt64(RcnoServiceBillingItemx)

                    'Dim command24 As MySqlCommand = New MySqlCommand
                    'command24.CommandType = CommandType.Text
                    'command24.CommandText = sql
                    'command24.Connection = conn

                    'Dim dr24 As MySqlDataReader = command24.ExecuteReader()

                    'Dim dt24 As New DataTable
                    'dt24.Load(dr24)


                    ' ''''''''''''''''''''''''''''''''''''''''''


                    'If dt23.Rows.Count > 0 Then

                    'End If
                    Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    TextBoxTotalReceiptAmt.Text = (Convert.ToDecimal(totalReceiptAmount)).ToString("N2")

                    Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    TextBoxTotalCNAmt.Text = (Convert.ToDecimal(totalCNAmount)).ToString("N2")


                    'End: 04.07.17

                    Dim TextBoxTotalReceipt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    'TextBoxTotalReceipt.Text = Convert.ToString(dt.Rows(rowIndex)("ReceiptValue"))
                    TextBoxTotalReceipt.Text = Convert.ToString(dt.Rows(rowIndex)("AppliedBase"))


                    'ReceiptAmt = ReceiptAmt + Convert.ToDecimal(Convert.ToString(dt.Rows(rowIndex)("ReceiptValue")))
                    ReceiptAmt = ReceiptAmt + Convert.ToDecimal(Convert.ToString(dt.Rows(rowIndex)("AppliedBase")))

                    'InsertIntoTblWebEventLog("RECEIPTIssue - " + Session("UserID"), Convert.ToDecimal(Convert.ToString(dt.Rows(rowIndex)("AppliedBase"))), ReceiptAmt, "")

                    Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                    TextBoxRcnoInvoice.Text = Convert.ToString(Convert.ToString(dt.Rows(rowIndex)("RcnoServiceBillingItem")))



                    'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
                    'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalTotalPriceWithGST.Text)
                    'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
                    ''txtAmountWithDiscount.Text = Total - TotalDiscAmt
                    'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxTotalInvoiceValue.Text)
                    'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text)
                    'TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxTotalReceiptAmt.Text)
                    TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxTotalReceipt.Text)

                    rowIndex += 1

                Next row

                'AddNewRowServiceRecs()
                'SetPreviousDataServiceRecs()
                'AddNewRowLoc()
                'SetPreviousDataLoc()
            Else
                'FirstGridViewRowServiceRecs()
                FirstGridViewRowBillingDetailsRecs()
            End If
            'End If

            'updpnlServiceRecs.Update()

            txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
            txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
            txtTotalWithGST.Text = TotalWithGST.ToString("N2")
            'txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

            'txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

            txtReceiptAmt.Text = ReceiptAmt.ToString("N2")

            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If txtMode.Text = "View" Then

                If (table.Rows.Count > 0) Then
                    For i As Integer = 0 To (table.Rows.Count) - 1
                        Dim TextBoxTotalInvoiceNo1 As TextBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxSel As CheckBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)

                        If String.IsNullOrEmpty(TextBoxTotalInvoiceNo1.Text) = False Then
                            TextBoxSel.Enabled = False
                            TextBoxSel.Checked = True
                        End If

                    Next i
                End If
            End If
            'End: Service Recods

            Dim commandDetailTotal As MySqlCommand = New MySqlCommand

            commandDetailTotal.CommandType = CommandType.Text
            commandDetailTotal.CommandText = "SELECT count(rcno) as TotDetRec FROM tblRecvDet  where ReceiptNumber ='" & txtReceiptNo.Text & "'"
            commandDetailTotal.Connection = conn

            Dim drDetailTotal As MySqlDataReader = commandDetailTotal.ExecuteReader()
            Dim dtDetailTotal As New DataTable
            dtDetailTotal.Load(drDetailTotal)

            If dtDetailTotal.Rows.Count > 0 Then
                'txtTotalWithDiscAmt.Text = dtDetailTotal.Rows(0)("TotalwithDisc").ToString
                'txtTotalGSTAmt.Text = dtDetailTotal.Rows(0)("TotalGST").ToString
                'txtTotalWithGST.Text = dtDetailTotal.Rows(0)("Totalappliedbase").ToString
                txtTotDetRec.Text = dtDetailTotal.Rows(0)("TotDetRec").ToString
            End If
            commandDetailTotal.Dispose()
            dtDetailTotal.Dispose()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION PopulateServiceGrid", ex.Message.ToString, "")
        End Try
    End Sub



    'Start: Billing Details Grid


    Private Sub FirstGridViewRowBillingDetailsRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("SelRec", GetType(String)))
            dt.Columns.Add(New DataColumn("ItemType", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceDate", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractNo", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("OtherCode", GetType(String)))
            dt.Columns.Add(New DataColumn("Location", GetType(String)))
            'dt.Columns.Add(New DataColumn("ItemType", GetType(String)))
            dt.Columns.Add(New DataColumn("ItemCode", GetType(String)))
            'dt.Columns.Add(New DataColumn("ItemDescription", GetType(String)))
            'dt.Columns.Add(New DataColumn("UOM", GetType(String)))
            'dt.Columns.Add(New DataColumn("Qty", GetType(String)))
            dt.Columns.Add(New DataColumn("PriceWithDisc", GetType(String)))
            dt.Columns.Add(New DataColumn("TaxType", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTPerc", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalPriceWithGST", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalReceiptAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalCrediteNoteAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("ReceiptAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoReceipt", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoInvoice", GetType(String)))
            dt.Columns.Add(New DataColumn("ARCode", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTCode", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceType", GetType(String)))
            dt.Columns.Add(New DataColumn("Remarks", GetType(String)))
            dt.Columns.Add(New DataColumn("GLDescription", GetType(String)))
            dt.Columns.Add(New DataColumn("SourceRcno", GetType(String)))

            dt.Columns.Add(New DataColumn("AccountID", GetType(String)))
            dt.Columns.Add(New DataColumn("CustomerName", GetType(String)))
            dr = dt.NewRow()

            dr("SelRec") = String.Empty
            dr("ItemType") = String.Empty
            dr("InvoiceNo") = String.Empty
            dr("InvoiceDate") = String.Empty
            dr("ContractNo") = String.Empty
            dr("ServiceNo") = String.Empty
            dr("OtherCode") = String.Empty
            'dr("ItemType") = String.Empty
            dr("ItemCode") = String.Empty
            'dr("ItemDescription") = String.Empty
            'dr("UOM") = String.Empty
            'dr("Qty") = 0
            dr("PriceWithDisc") = 0
            dr("TaxType") = String.Empty
            dr("GSTPerc") = 0.0
            dr("GSTAmt") = 0
            dr("TotalPriceWithGST") = 0
            dr("TotalReceiptAmt") = 0
            dr("TotalCrediteNoteAmt") = 0
            dr("ReceiptAmt") = 0
            dr("RcnoReceipt") = 0
            dr("RcnoInvoice") = 0
            dr("ARCode") = String.Empty
            dr("GSTCode") = 0
            dr("InvoiceType") = 0
            dr("Remarks") = String.Empty
            dr("GLDescription") = String.Empty
            dr("Location") = String.Empty
            dr("SourceRcno") = String.Empty

            dr("AccountID") = String.Empty
            dr("CustomerName") = String.Empty

          
            dt.Rows.Add(dr)

            ViewState("CurrentTableBillingDetailsRec") = dt

            grvBillingDetails.DataSource = dt
            grvBillingDetails.DataBind()

            Dim btnAdd As Button = CType(grvBillingDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FirstGridViewRowBillingDetailsRecs", ex.Message.ToString, "")
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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceNoGV"), TextBox)
                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceTypeGV"), TextBox)

                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRemarksGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)

                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtLocationGV"), TextBox)
                        Dim TextBoxSourceRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtSourceRcnoGV"), TextBox)

                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxCustomerName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtCustomerNameGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                        'TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text

                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceNo") = TextBoxServiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceType") = TextBoxInvoiceType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text                 
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text

                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("SourceRcno") = TextBoxSourceRcno.Text

                        dtCurrentTable.Rows(i - 1)("AccountID") = TextBoxAccountID.Text
                        dtCurrentTable.Rows(i - 1)("CustomerName") = TextBoxCustomerName.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable

                    grvBillingDetails.DataSource = dtCurrentTable
                    grvBillingDetails.DataBind()

                    Dim rowIndex2 As Integer = 0
                    Dim j As Integer = 1
                    Do While j <= (rowIndex)

                        'Dim TextBoxTargetDesc1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtTaxTypeGV"), DropDownList)
                        'Query = "Select TaxType from tbltaxtype"
                        'PopulateDropDownList(Query, "TaxType", "TaxType", TextBoxTargetDesc1)


                        'Dim Query As String

                        'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'Query = "Select * from tblbillingproducts"
                        'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        'Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        'Query = "Select * from tblunitms"
                        'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


                        'Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                        'If TextBoxItemType1.Text = "SERVICE" Then
                        '    TextBoxQty1.Enabled = False
                        '    TextBoxQty1.Text = 1

                        'End If

                        'If rowIndex2 = 0 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If

                        rowIndex2 += 1
                        j += 1
                    Loop

                    'Dim TextBoxTargetDesc2 As DropDownList = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
                    'Query = "Select TargetId, descrip1 from tblTarget"
                    'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc2)


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "AddNewRowBillingDetailsRecs", ex.Message.ToString, "")
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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceNoGV"), TextBox)
                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceTypeGV"), TextBox)

                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRemarksGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtLocationGV"), TextBox)
                        Dim TextBoxSourceRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtSourceRcnoGV"), TextBox)

                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxCustomerName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtCustomerNameGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text

                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceNo") = TextBoxServiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceType") = TextBoxInvoiceType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text
                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("SourceRcno") = TextBoxSourceRcno.Text

                        dtCurrentTable.Rows(i - 1)("AccountID") = TextBoxAccountID.Text
                        dtCurrentTable.Rows(i - 1)("CustomerName") = TextBoxCustomerName.Text
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "AddNewRowWithDetailRecBillingDetailsRecs", ex.Message.ToString, "")
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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceNoGV"), TextBox)
                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceTypeGV"), TextBox)

                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRemarksGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtLocationGV"), TextBox)
                        Dim TextBoxSourceRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtSourceRcnoGV"), TextBox)

                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxCustomerName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtCustomerNameGV"), TextBox)



                        TextBoxSelect.Text = dt.Rows(i)("SelRec").ToString()
                        TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()

                        TextBoxInvoiceNo.Text = dt.Rows(i)("InvoiceNo").ToString()
                        TextBoxInvoiceDate.Text = dt.Rows(i)("InvoiceDate").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxServiceNo.Text = dt.Rows(i)("ServiceNo").ToString()
                        TextBoxInvoiceType.Text = dt.Rows(i)("InvoiceType").ToString()
                        'TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()
                        TextBoxItemCode.Text = dt.Rows(i)("ItemCode").ToString()
                        'TextBoxItemDescription.Text = dt.Rows(i)("ItemDescription").ToString()
                        'TextBoxUOM.Text = dt.Rows(i)("UOM").ToString()
                        'TextBoxQty.Text = dt.Rows(i)("Qty").ToString()

                        TextBoxPriceWithDisc.Text = dt.Rows(i)("PriceWithDisc").ToString()
                        TextBoxTaxType.Text = dt.Rows(i)("TaxType").ToString()
                        TextBoxGSTPerc.Text = dt.Rows(i)("GSTPerc").ToString()
                        TextBoxGSTAmt.Text = dt.Rows(i)("GSTAmt").ToString()
                        TextBoxTotalPriceWithGST.Text = dt.Rows(i)("TotalPriceWithGST").ToString()

                        TextBoxTotalReceiptAmt.Text = dt.Rows(i)("TotalReceiptAmt").ToString()
                        TextBoxBalanceAmt.Text = dt.Rows(i)("TotalCrediteNoteAmt").ToString().ToString()

                        'dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text

                        TextBoxReceiptAmt.Text = dt.Rows(i)("ReceiptAmt").ToString()
                        TextBoxRcnoReceipt.Text = dt.Rows(i)("RcnoReceipt").ToString()
                        TextBoxRcnoInvoice.Text = dt.Rows(i)("RcnoInvoice").ToString()
                        TextBoxARCode.Text = dt.Rows(i)("ARCode").ToString()
                        TextBoGSTCode.Text = dt.Rows(i)("GSTCode").ToString()
                        TextBoxOtherCode.Text = dt.Rows(i)("OtherCode").ToString()
                        TextBoxRemarks.Text = dt.Rows(i)("Remarks").ToString()
                        TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
                        TextBoxLocation.Text = dt.Rows(i)("Location").ToString()
                        TextBoxSourceRcno.Text = dt.Rows(i)("SourceRcno").ToString()

                        TextBoxAccountID.Text = dt.Rows(i)("AccountID").ToString()
                        TextBoxCustomerName.Text = dt.Rows(i)("CustomerName").ToString()

                        If rowdeleted = "Y" Then
                            TextBoxSelect.Checked = True
                        End If

                        'Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        'Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

                        'If TextBoxItemType1.Text = "SERVICE" Then
                        '    TextBoxQty1.Enabled = False
                        '    TextBoxQty1.Text = 1

                        'End If

                        'If rowIndex = 0 Then
                        '    If TextBoxItemType1.Text = "SERVICE" Then
                        '        TextBoxItemType1.Enabled = False
                        '    End If
                        'End If

                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "SetPreviousDataBillingDetailsRecs", ex.Message.ToString, "")
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

                        Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtInvoiceDateGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxServiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceNoGV"), TextBox)
                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtInvoiceTypeGV"), TextBox)

                        'Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtItemCodeGV"), TextBox)
                        'Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtOtherCodeGV"), TextBox)
                        'Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtUOMGV"), DropDownList)
                        'Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtTaxTypeGV"), TextBox)
                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtTotalReceiptAmtGV"), TextBox)
                        Dim TextBoxBalanceAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtTotalCreditNoteAmtGV"), TextBox)

                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxRcnoReceipt = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtRcnoReceiptGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(17).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxARCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtARCodeGV"), TextBox)
                        Dim TextBoGSTCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGSTCodeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRemarksGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxLocation As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtLocationGV"), TextBox)
                        Dim TextBoxSourceRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtSourceRcnoGV"), TextBox)

                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxCustomerName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtCustomerNameGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text

                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceDate") = TextBoxInvoiceDate.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceNo") = TextBoxServiceNo.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceType") = TextBoxInvoiceType.Text
                        'dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        'dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        'dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        'dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text
                        dtCurrentTable.Rows(i - 1)("TotalReceiptAmt") = TextBoxTotalReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalCrediteNoteAmt") = TextBoxBalanceAmt.Text

                        dtCurrentTable.Rows(i - 1)("ReceiptAmt") = TextBoxReceiptAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoReceipt") = TextBoxRcnoReceipt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ARCode") = TextBoxARCode.Text
                        dtCurrentTable.Rows(i - 1)("GSTCode") = TextBoGSTCode.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("Location") = TextBoxLocation.Text
                        dtCurrentTable.Rows(i - 1)("SourceRcno") = TextBoxSourceRcno.Text

                        dtCurrentTable.Rows(i - 1)("AccountID") = TextBoxAccountID.Text
                        dtCurrentTable.Rows(i - 1)("CustomerName") = TextBoxCustomerName.Text
                        'TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable
                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "SetRowDataBillingDetailsRecs", ex.Message.ToString, "")
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


    Protected Sub txtInvoiceNoGV_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)


            '''''''''''''''''''''''''''''

            'Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
            'TextBoxInvoiceDate.Text = Convert.ToDateTime(InvoiceDate.Text).ToString("dd/MM/yyyy")

          
            'Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
            'TextBoxTotalPriceWithGST.Text = (Convert.ToDecimal(InvoiceAmmount.Text)).ToString("N2")



            'Dim TextBoxTotalTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
            'TextBoxTotalTotalReceiptAmt.Text = (Convert.ToDecimal(TotalReceiptAmount.Text)).ToString("N2")

            'Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
            'TextBoxTotalCNAmt.Text = (Convert.ToDecimal(TotalCNAmount.Text)).ToString("N2")


            'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
            'TextBoxOtherCode.Text = txtARCode.Text

            'Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            'TextBoxGLDescription.Text = txtARDescription.Text

            'Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
            'TextBoxReceiptAmt.Text = TotalOSAmount.Text

            'Dim TextBoxDocType As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtLocationGV"), TextBox)
            'TextBoxDocType.Text = DocType.Text

            'Dim TextBoxSourceRcno1 As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtSourceRcnoGV"), TextBox)
            'TextBoxSourceRcno1.Text = TextBoxSourceRcno.Text

            'Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtAccountIdGV"), TextBox)
            'TextBoxAccountID.Text = AccountID1.Text

            'Dim TextBoxClientName As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCustomerNameGV"), TextBox)
            'TextBoxClientName.Text = ClientName1.Text


            ''''''''''''''''''''''''

            Dim lblid0 As TextBox = CType(txt1.FindControl("txtInvoiceNoGV"), TextBox)
            Dim lblid1 As TextBox = CType(txt1.FindControl("txtInvoiceDateGV"), TextBox)
            Dim lblid2 As TextBox = CType(txt1.FindControl("txtTotalPriceWithGSTGV"), TextBox)
            Dim lblid3 As TextBox = CType(txt1.FindControl("txtTotalReceiptAmtGV"), TextBox)
            Dim lblid4 As TextBox = CType(txt1.FindControl("txtTotalCreditNoteAmtGV"), TextBox)
            Dim lblid5 As TextBox = CType(txt1.FindControl("txtOtherCodeGV"), TextBox)
            Dim lblid6 As TextBox = CType(txt1.FindControl("txtGLDescriptionGV"), TextBox)
            Dim lblid7 As TextBox = CType(txt1.FindControl("txtReceiptAmtGV"), TextBox)
            Dim lblid8 As TextBox = CType(txt1.FindControl("txtLocationGV"), TextBox)
            Dim lblid9 As TextBox = CType(txt1.FindControl("txtSourceRcnoGV"), TextBox)
            Dim lblid10 As TextBox = CType(txt1.FindControl("txtAccountIdGV"), TextBox)
            Dim lblid11 As TextBox = CType(txt1.FindControl("txtCustomerNameGV"), TextBox)

            Dim lblid12 As DropDownList = CType(txt1.FindControl("txtItemTypeGV"), DropDownList)

            lblAlert.Text = ""
            updPnlMsg.Update()


            '''''''''''''''''''''''''''''''''''
            If lblid12.Text = "ARIN" Then
                lblid2.Text = 0.0
                lblid3.Text = 0.0
                lblid4.Text = 0.0
                lblid7.Text = 0.0

                lblid5.Text = ""
                lblid6.Text = ""
                lblid7.Text = ""
                lblid8.Text = ""

                lblid9.Text = ""
                'lblid11.Text = ""
                lblid10.Text = ""
                lblid11.Text = ""

                'Dim IsInvDet As Boolean
                'IsInvDet = False
                'Dim lService As String
                'lService = ""

                'Dim connIsInvDet As MySqlConnection = New MySqlConnection()

                'connIsInvDet.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'connIsInvDet.Open()

                'Dim commandIsInvDet As MySqlCommand = New MySqlCommand
                'commandIsInvDet.CommandType = CommandType.Text
                ''commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where InvoiceNumber = '" & lblid0.Text.Trim & "'"
                'commandIsInvDet.CommandText = "SELECT RefType, InvoiceNumber from tblsalesdetail where RefType = '" & lblid1.Text.Trim & "'"
                'commandIsInvDet.Connection = connIsInvDet

                'Dim drIsInvDet As MySqlDataReader = commandIsInvDet.ExecuteReader()
                'Dim dtIsInvDet As New DataTable
                'dtIsInvDet.Load(drIsInvDet)

                'If dtIsInvDet.Rows.Count > 0 Then
                '    lService = dtIsInvDet.Rows(0)("InvoiceNumber").ToString
                '    lblid0.Text = lService.Trim
                'End If

                'connIsInvDet.Close()
                'connIsInvDet.Dispose()
                ''''''''''''''''''''''

                Dim IsInvoiceRecord As Boolean
                IsInvoiceRecord = False

                Dim connInvoiceRecord As MySqlConnection = New MySqlConnection()

                connInvoiceRecord.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                connInvoiceRecord.Open()

                Dim commandIsInvoiceNo As MySqlCommand = New MySqlCommand
                commandIsInvoiceNo.CommandType = CommandType.Text
                'commandIsServiceRecord.CommandText = "SELECT BillNo, BilledAmt, RecordNo, ContractNo, LocationID, ServiceDate, Notes, ServiceBy from tblServiceRecord where RecordNo = '" & lService.Trim & "'"
                commandIsInvoiceNo.CommandText = "SELECT InvoiceNumber, SalesDate, AccountID, CustName, ReceiptBase, CreditBase, AppliedBase, BalanceBase from tblSales where InvoiceNumber = '" & lblid0.Text.Trim & "'"

                commandIsInvoiceNo.Connection = connInvoiceRecord

                Dim drIsInvoiceNo As MySqlDataReader = commandIsInvoiceNo.ExecuteReader()
                Dim dtIsInvoiceNo As New DataTable
                dtIsInvoiceNo.Load(drIsInvoiceNo)

                If dtIsInvoiceNo.Rows.Count > 0 Then
                    If String.IsNullOrEmpty(dtIsInvoiceNo.Rows(0)("InvoiceNumber").ToString) = True Then
                        lblid1.Text = ""
                        lblAlert.Text = "INVOICE NUMBER NUMBER NOT FOUND"
                        updPnlMsg.Update()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                        Exit Sub
                    Else
                        If Convert.ToDecimal(dtIsInvoiceNo.Rows(0)("BalanceBase").ToString) = 0.0 Then
                            lblid1.Text = ""
                            lblAlert.Text = "INVOICE DO NOT HAVE OS AMOUT"
                            updPnlMsg.Update()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                            Exit Sub
                        End If

                        lblid1.Text = Convert.ToDateTime(dtIsInvoiceNo.Rows(0)("SalesDate")).ToString("dd/MM/yyyy")
                        lblid2.Text = dtIsInvoiceNo.Rows(0)("Appliedbase").ToString
                        lblid3.Text = dtIsInvoiceNo.Rows(0)("ReceiptBase").ToString
                        lblid4.Text = dtIsInvoiceNo.Rows(0)("CreditBase").ToString
                        lblid7.Text = dtIsInvoiceNo.Rows(0)("BalanceBase").ToString


                        lblid5.Text = txtARCode.Text
                        lblid6.Text = txtARDescription.Text
                        lblid8.Text = "INV"
                        'lblid5.Text = dtIsInvoiceNo.Rows(0)("ContractNo").ToString
                        'lblid6.Text = dtIsInvoiceNo.Rows(0)("LocationID").ToString

                        lblid10.Text = dtIsInvoiceNo.Rows(0)("AccountID").ToString
                        lblid11.Text = dtIsInvoiceNo.Rows(0)("CustName").ToString

                        calculateTotalReceipt()
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

                commandIsInvoiceNo.Dispose()
                connInvoiceRecord.Close()
                connInvoiceRecord.Dispose()
            End If

            '''''''''''''''''''''''''''''''''''''


        
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPTS - " + Session("UserID"), "txtInvoiceNoGV_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtQtyGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        'CalculatePrice()
    End Sub

    Protected Sub txtPricePerUOMGV_TextChanged(sender As Object, e As EventArgs)
        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)

        'CalculatePrice()
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
    'Private Sub CalculatePrice()
    '    Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
    '    Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGV"), TextBox)
    '    Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceGV"), TextBox)

    '    Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGV"), TextBox)
    '    Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGV"), TextBox)
    '    Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGV"), TextBox)

    '    Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
    '    Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTAmtGV"), TextBox)
    '    Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceWithGSTGV"), TextBox)

    '    Dim dblQty As String
    '    Dim dblPricePerUOM As String
    '    Dim dblTotalPrice As String

    '    Dim dblDiscPerc As String
    '    Dim dblDisAmount As String
    '    Dim dblPriceWithDisc As String

    '    Dim dblGSTPerc As String
    '    Dim dblGSTAmt As String
    '    Dim dblTotalPriceWithGST As String


    '    If String.IsNullOrEmpty(lblid1.Text) = True Then
    '        lblid1.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid2.Text) = True Then
    '        lblid2.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid3.Text) = True Then
    '        lblid3.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid4.Text) = True Then
    '        lblid4.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid5.Text) = True Then
    '        lblid5.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid6.Text) = True Then
    '        lblid6.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid7.Text) = True Then
    '        lblid7.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid8.Text) = True Then
    '        lblid8.Text = "0.00"
    '    End If

    '    If String.IsNullOrEmpty(lblid9.Text) = True Then
    '        lblid9.Text = "0.00"
    '    End If


    '    dblQty = (lblid1.Text)
    '    dblPricePerUOM = (lblid2.Text)
    '    dblTotalPrice = (lblid3.Text)

    '    dblDiscPerc = (lblid4.Text)
    '    dblDisAmount = (lblid5.Text)
    '    dblPriceWithDisc = (lblid6.Text)

    '    dblGSTPerc = (lblid7.Text)
    '    dblGSTAmt = (lblid8.Text)
    '    dblTotalPriceWithGST = (lblid9.Text)

    '    lblid3.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
    '    lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
    '    lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
    '    lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid6.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
    '    lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid6.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

    '    'CalculateTotalPrice()


    'End Sub


    'Private Sub CalculateTotalPrice()


    '    Dim TotalAmt As Decimal = 0
    '    Dim TotalDiscAmt As Decimal = 0
    '    Dim TotalWithDiscAmt As Decimal = 0
    '    Dim TotalGSTAmt As Decimal = 0
    '    Dim TotalAmtWithGST As Decimal = 0
    '    Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


    '    If (table.Rows.Count > 0) Then

    '        For i As Integer = 0 To (table.Rows.Count) - 1

    '            Dim TextBoxTotal As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceGV"), TextBox)
    '            Dim TextBoxTotalWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceWithGSTGV"), TextBox)

    '            Dim TextBoxDisAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
    '            Dim TextBoxTotalWithDiscAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtPriceWithDiscGV"), TextBox)

    '            Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtGSTAmtGV"), TextBox)

    '            If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
    '                TextBoxTotal.Text = "0.00"
    '            End If

    '            If String.IsNullOrEmpty(TextBoxDisAmt.Text) = True Then
    '                TextBoxDisAmt.Text = "0.00"
    '            End If

    '            If String.IsNullOrEmpty(TextBoxTotalWithDiscAmt.Text) = True Then
    '                TextBoxTotalWithDiscAmt.Text = "0.00"
    '            End If

    '            If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
    '                TextBoxGSTAmt.Text = "0.00"
    '            End If

    '            If String.IsNullOrEmpty(TextBoxTotalWithGST.Text) = True Then
    '                TextBoxTotalWithGST.Text = "0.00"
    '            End If

    '            TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
    '            TotalAmtWithGST = TotalAmtWithGST + Convert.ToDecimal(TextBoxTotalWithGST.Text)

    '            TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDisAmt.Text)
    '            TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
    '            TotalWithDiscAmt = TotalWithDiscAmt + Convert.ToDecimal(TextBoxTotalWithDiscAmt.Text)
    '        Next i

    '    End If


    '    'txtTotal.Text = TotalAmt.ToString
    '    txtTotalWithGST.Text = TotalAmtWithGST.ToString

    '    'txtTotalDiscAmt.Text = TotalDiscAmt.ToString
    '    txtTotalGSTAmt.Text = TotalGSTAmt.ToString

    '    txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString
    '    updPanelSave.Update()
    'End Sub


    Private Sub MakeMeNullBillingDetails()
        txtReceiptNo.Text = ""
        'txtInvoiceDate.Text = ""
        txtPostStatus.Text = ""
        If txtFrom.Text <> "invoice" And txtFrom.Text <> "invoicePB" Then

            txtAccountIdBilling.Text = ""
            txtAccountName.Text = ""
            txtCompanyGroup.Text = ""

            'txtAccountIdBilling.Text = Session("AccountId")
            'txtAccountName.Text = Session("AccountName")
            'ddlContactType.Text = Session("ContactType")
            'txtCompanyGroup.Text = Session("CompanyGroup")
            txtSalesman.Text = ""


        End If

        ddlBankCode.SelectedIndex = 0
        txtBankName.Text = ""
        txtBankGLCode.Text = ""
        ddlContactType.SelectedIndex = 0
        txtTotalWithGST.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        'txtTotalDiscAmt.Text = "0.00"
        txtTotalWithDiscAmt.Text = "0.00"
        txtReceiptAmt.Text = "0.00"
        txtLocation.Text = ""
        chkChequeReturned.Checked = False
        txtReceiptDate.Attributes.Add("readonly", "readonly")
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
        'updPnlBillingRecs.Update()
        'updPnlBillingRecs.Update()
    End Sub

    Protected Sub btnAddDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        If TotDetailRecords > 0 Then
            AddNewRowWithDetailRecBillingDetailsRecs()
        Else
            AddNewRowBillingDetailsRecs()
        End If
    End Sub


    Private Function FindAccountId() As Boolean
        Try
            Dim IsAccountId As Boolean
            IsAccountId = False

            Dim connIsAccountId As MySqlConnection = New MySqlConnection()

            connIsAccountId.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connIsAccountId.Open()

            Dim commandIsAccountId As MySqlCommand = New MySqlCommand
            commandIsAccountId.CommandType = CommandType.Text

            If ddlContactType.Text = "COMPANY" Then
                commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblCompany where AccountId ='" & txtAccountIdBilling.Text & "'"
            ElseIf ddlContactType.Text = "PERSON" Then
                commandIsAccountId.CommandText = "SELECT count(*) as CountAccountId from tblPerson where AccountId ='" & txtAccountIdBilling.Text & "'"
            End If

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
            Return IsAccountId
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FindAccountId", ex.Message.ToString, "")
            Return Nothing
        End Try
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim rowselected As Integer
        rowselected = 0
        lblAlert.Text = ""

        Try

            '''''''''''''''''''''
            btnSave.Enabled = False

            If ((ddlContactType.SelectedIndex > 0) And (String.IsNullOrEmpty(txtAccountIdBilling.Text) = False)) Then
                Dim IsAccountIdExist As Boolean = FindAccountId()

                If IsAccountIdExist = False Then
                    lblAlert.Text = "INVALID ACCOUNT ID"
                    updPnlMsg.Update()
                    btnSave.Enabled = True

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition(); RefreshSubmit();</Script>", False)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> RefreshSubmit();</Script>", False)
                    Exit Sub
                End If
            End If

            'If String.IsNullOrEmpty(txtCompanyGroup.Text.Trim) = True Then
            '    lblAlert.Text = "INVALID COMPANY GROUP"
            '    updPnlMsg.Update()
            '    btnSave.Enabled = True
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '    Exit Sub
            'End If
            '''''''''''''''''''''

            Dim IsLock = FindRVPeriod(txtReceiptPeriod.Text)
            If IsLock = "Y" Then
                lblAlert.Text = "PERIOD IS LOCKED"
                updPnlMsg.Update()
                txtReceiptDate.Focus()
                btnSave.Enabled = True

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition(); RefreshSubmit();</Script>", False)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> RefreshSubmit();</Script>", False)
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtReceiptAmt.Text.Trim) = True Then
                lblAlert.Text = "THERE SHOULD BE DETAIL RECORDS"
                updPnlMsg.Update()

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition(); RefreshSubmit();</Script>", False)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> RefreshSubmit();</Script>", False)
                Exit Sub
            End If

            If Convert.ToDecimal(txtReceiptAmt.Text) <> Convert.ToDecimal(txtReceivedAmount.Text) Then
                lblAlert.Text = "RECEIPT AMOUNT AND SUM OF APPLIED INVOICES SHOULD BE EQUAL"
                updPnlMsg.Update()
                btnSave.Enabled = True

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition(); RefreshSubmit();</Script>", False)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> RefreshSubmit();</Script>", False)
                Exit Sub
            End If


            If lblMessage.Text = "ACTION: CHEQUE RETURN" Then
                If String.IsNullOrEmpty(txtComments.Text) = True Then
                    lblAlert.Text = "REMARKS CANNOT BE BLANK FOR 'CHEQUE RETURN'"
                    updPnlMsg.Update()
                    btnSave.Enabled = True

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition(); RefreshSubmit();</Script>", False)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> RefreshSubmit();</Script>", False)
                    Exit Sub
                End If
            End If
            ''''''''' check for existing Cheque

            ' ''quote
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()


            'If String.IsNullOrEmpty(txtChequeNo.Text) = False Then
            '    Dim sql As String
            '    sql = ""

            '    If txtMode.Text = "NEW" Then
            '        sql = "Select Cheque  from tblRecv where Cheque ='" & txtChequeNo.Text & "'"
            '    Else
            '        sql = "Select Cheque  from tblRecv where Cheque ='" & txtChequeNo.Text & "' and ReceiptNumber <> '" & txtReceiptNo.Text & "'"
            '    End If

            '    Dim command1 As MySqlCommand = New MySqlCommand
            '    command1.CommandType = CommandType.Text
            '    command1.CommandText = sql
            '    command1.Connection = conn

            '    Dim dr As MySqlDataReader = command1.ExecuteReader()

            '    Dim dt As New DataTable
            '    dt.Load(dr)
            '    'Dim lCountCheque As Integer

            '    If dt.Rows.Count > 0 Then
            '        'lCountCheque = dt.Rows(0)("CountCheque").ToString

            '        lblAlert.Text = "CHEQUE ALREADY EXISTS"
            '        updPnlMsg.Update()
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '        conn.Close()
            '        'Exit Sub
            '    End If

            'End If


            'Start: CheckOSAmount
            'SetRowDataBillingDetailsRecs()
            'Dim tableAdd1 As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            'If tableAdd1 IsNot Nothing Then

            '    For rowIndex1 As Integer = 0 To tableAdd1.Rows.Count - 1
            '        'Dim TextBoxchkSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
            '        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)

            '        If (String.IsNullOrEmpty(TextBoxOtherCode.Text.Trim) = False) Then
            '            Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex1).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
            '            Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex1).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)

            '            If String.IsNullOrEmpty(TextBoxInvoiceNo.Text) = False Then

            '                Dim command1 As MySqlCommand = New MySqlCommand
            '                command1.CommandType = CommandType.Text
            '                Dim qry1 As String

            '                qry1 = "SELECT AppliedBase, BalanceBase from tblSales "
            '                qry1 = qry1 + " where InvoiceNumber ='" & TextBoxInvoiceNo.Text & "'"

            '                command1.CommandText = qry1
            '                command1.Parameters.Clear()
            '                command1.Connection = conn

            '                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
            '                Dim dt1 As New DataTable
            '                dt1.Load(dr1)

            '                If Convert.ToDecimal(TextBoxReceiptAmt.Text) > Convert.ToDecimal(dt1.Rows(0)("AppliedBase").ToString) Then
            '                    lblAlert.Text = "Receipt Amount is more than Invoice Amount for the Invoice : " & TextBoxInvoiceNo.Text
            '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '                    updPnlMsg.Update()
            '                    Exit Sub
            '                End If



            '                '''''''''''''''''''
            '                Dim command2 As MySqlCommand = New MySqlCommand
            '                command2.CommandType = CommandType.Text
            '                Dim qry2 As String

            '                qry2 = "SELECT ifnull(sum(ValueBase+gstBase-receiptbase),0) as OSAmt from tblSales "
            '                qry2 = qry2 + " where InvoiceNumber ='" & TextBoxInvoiceNo.Text & "'"

            '                command2.CommandText = qry2
            '                command2.Parameters.Clear()
            '                command2.Connection = conn

            '                Dim dr2 As MySqlDataReader = command2.ExecuteReader()
            '                Dim dt2 As New DataTable
            '                dt2.Load(dr2)


            '                If Convert.ToDecimal(TextBoxReceiptAmt.Text) > Convert.ToDecimal(dt2.Rows(0)("OSAmt").ToString) Then
            '                    lblAlert.Text = "Receipt Amount is more than Balance Amount for the Invoice : " & TextBoxInvoiceNo.Text
            '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            '                    updPnlMsg.Update()
            '                    Exit Sub
            '                End If

            '                '''''''''''''''''''''
            '            End If
            '        End If

            '    Next rowIndex1
            'End If

            'End: CheckOSAmount

            'btnSave.Enabled = False

            'PopulateArCode()
            SetRowDataBillingDetailsRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd IsNot Nothing Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String

                If txtMode.Text = "NEW" Then

                    If lblMessage.Text = "ACTION: CHEQUE RETURN" Then
                        ''''''''''''
                        Dim commandChequeReturn As MySqlCommand = New MySqlCommand

                        commandChequeReturn.CommandType = CommandType.Text

                        qry = "UPDATE tblRecv SET  ChequeReturned=@ChequeReturned, "
                        qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn =@LastModifiedOn "
                        qry = qry + " where Rcno = @Rcno;"

                        commandChequeReturn.CommandText = qry
                        commandChequeReturn.Parameters.Clear()

                        commandChequeReturn.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                        'commandChequeReturn.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

                        If chkChequeReturned.Checked = True Then
                            commandChequeReturn.Parameters.AddWithValue("@ChequeReturned", 1)
                        Else
                            commandChequeReturn.Parameters.AddWithValue("@ChequeReturned", 0)
                        End If

                        commandChequeReturn.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        commandChequeReturn.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        commandChequeReturn.Connection = conn
                        commandChequeReturn.ExecuteNonQuery()

                        '''''''''''''
                    End If

                    qry = "INSERT INTO tblRecv(ReceiptNumber, ReceiptFrom, AccountId,   "
                    qry = qry + "  ReceiptDate, Cheque, ChequeDate, BankId, LedgerCode, LedgerName,  PaymentType, BankName, Bank,  "
                    qry = qry + " BaseAmount, AppliedBase, AppliedOriginal, GSTAmount,  NetAmount, BankAmount, Comments, ContactType, CompanyGroup, GLPeriod, Salesman, InvoiceType, Addr1, Addr2, Addr3, Addr4, Location,  "
                    qry = qry + " AddPostal, AddCity, AddState, AddCountry,  ChequeReturned, "
                    qry = qry + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qry = qry + " (@ReceiptNumber, @ReceiptFrom, @AccountId,  "
                    qry = qry + " @ReceiptDate, @Cheque, @ChequeDate, @BankId, @LedgerCode, @LedgerName,  @PaymentType, @BankName, @Bank, "
                    qry = qry + " @BaseAmount, @AppliedBase, @AppliedOriginal, @GSTAmount,  @NetAmount, @BankAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod,  @Salesman, @InvoiceType, @Addr1, @Addr2, @Addr3, @Addr4, @Location, "
                    qry = qry + " @Postal, @City, @State, @Country, @ChequeReturned, "
                    qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@ReceiptFrom", txtAccountName.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)

                    If String.IsNullOrEmpty(txtReceiptDate.Text.Trim) = True Then
                        command.Parameters.AddWithValue("@ReceiptDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
                    End If

                    command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@Cheque", txtChequeNo.Text.Trim)
                    If ddlPaymentMode.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@PaymentType", "")
                    Else
                        command.Parameters.AddWithValue("@PaymentType", ddlPaymentMode.Text.Trim)
                    End If

                    If String.IsNullOrEmpty(txtChequeDate.Text.Trim) = True Then
                        command.Parameters.AddWithValue("@ChequeDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ChequeDate", Convert.ToDateTime(txtChequeDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    'command.Parameters.AddWithValue("@ChequeDate", txtChequeDate.Text)

                    command.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(txtReceivedAmount.Text))

                    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@BankAmount", Convert.ToDecimal(txtReceivedAmount.Text))

                    command.Parameters.AddWithValue("@Bank", ddlBankCode.Text.Trim)
                    command.Parameters.AddWithValue("@BankId", txtBankID.Text.Trim)
                    command.Parameters.AddWithValue("@LedgerCode", txtBankGLCode.Text.Trim)
                    command.Parameters.AddWithValue("@LedgerName", txtLedgerName.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@BankName", txtBankName.Text.ToUpper.Trim)

                    command.Parameters.AddWithValue("@ContactType", ddlContactType.Text.Trim)
                    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text.Trim)
                    command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text.Trim)

                    If txtSalesman.Text = "-1" Then
                        command.Parameters.AddWithValue("@Salesman", "")
                    Else
                        command.Parameters.AddWithValue("@Salesman", txtSalesman.Text.Trim)
                    End If

                    command.Parameters.AddWithValue("@InvoiceType", "")

                    command.Parameters.AddWithValue("@Addr1", Server.HtmlDecode(txtBillAddress.Text.Trim))
                    command.Parameters.AddWithValue("@Addr2", Server.HtmlDecode(txtBillStreet.Text.Trim))
                    command.Parameters.AddWithValue("@Addr3", Server.HtmlDecode(txtBillBuilding.Text.Trim))
                    'command.Parameters.AddWithValue("@Addr4", txtBillCountry.Text & ", " & txtBillPostal.Text)
                    command.Parameters.AddWithValue("@Addr4", Server.HtmlDecode(txtBillCountry.Text.Trim))
                    command.Parameters.AddWithValue("@Location", txtLocation.Text.ToUpper.Trim)

                    command.Parameters.AddWithValue("@Postal", txtBillPostal.Text.Trim)
                    command.Parameters.AddWithValue("@City", txtBillCity.Text.Trim)
                    command.Parameters.AddWithValue("@State", txtBillState.Text.Trim)
                    command.Parameters.AddWithValue("@Country", txtBillCountry.Text.Trim)

                    If chkChequeReturned.Checked = True Then
                        command.Parameters.AddWithValue("@ChequeReturned", 1)
                    Else
                        command.Parameters.AddWithValue("@ChequeReturned", 0)
                    End If

                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    If String.IsNullOrEmpty(txtReceiptNo.Text) = True Then
                        GenerateReceiptNo()
                    End If

                    command.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

                    command.Connection = conn
                    command.ExecuteNonQuery()

                    Dim sqlLastId As String
                    sqlLastId = "SELECT last_insert_id() from tblRecv"

                    Dim commandRcno As MySqlCommand = New MySqlCommand
                    commandRcno.CommandType = CommandType.Text
                    commandRcno.CommandText = sqlLastId
                    commandRcno.Parameters.Clear()
                    commandRcno.Connection = conn
                    txtRcno.Text = commandRcno.ExecuteScalar()

                Else
                    qry = "UPDATE tblRecv SET ReceiptNumber =@ReceiptNumber, ReceiptFrom = @ReceiptFrom, AccountId= @AccountId,   "
                    qry = qry + " ReceiptDate =@ReceiptDate, Cheque =@Cheque, ChequeDate =@ChequeDate, BankId =@BankId, LedgerCode =@LedgerCode, Bank =@Bank,   "
                    qry = qry + " LedgerName =@LedgerName,  PaymentType =@PaymentType, BankName = @BankName,   "
                    qry = qry + " BaseAmount =@BaseAmount, AppliedBase =@AppliedBase, AppliedOriginal =@AppliedOriginal, GSTAmount =@GSTAmount,  NetAmount =@NetAmount, BankAmount =@BankAmount,  "
                    qry = qry + " Comments =@Comments, ContactType =@ContactType, CompanyGroup =@CompanyGroup, GLPeriod =@GLPeriod, Addr1= @Addr1, Addr2=@Addr2, Addr3=@Addr3, Addr4=@Addr4,"
                    qry = qry + " AddPostal =@Postal, AddCity =@City, AddState=@State, AddCountry=@Country,  ChequeReturned=@ChequeReturned,"
                    qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn =@LastModifiedOn "
                    qry = qry + " where Rcno = @Rcno;"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@Rcno", Convert.ToInt64(txtRcno.Text))
                    command.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text.Trim)
                    command.Parameters.AddWithValue("@ReceiptFrom", txtAccountName.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text.Trim)

                    If String.IsNullOrEmpty(txtReceiptDate.Text.Trim) = True Then
                        command.Parameters.AddWithValue("@ReceiptDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
                    End If

                    command.Parameters.AddWithValue("@Comments", txtComments.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@Cheque", txtChequeNo.Text.Trim)
                    If ddlPaymentMode.SelectedIndex = 0 Then
                        command.Parameters.AddWithValue("@PaymentType", "")
                    Else
                        command.Parameters.AddWithValue("@PaymentType", ddlPaymentMode.Text.Trim)
                    End If


                    If String.IsNullOrEmpty(txtChequeDate.Text.Trim) = True Then
                        command.Parameters.AddWithValue("@ChequeDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ChequeDate", Convert.ToDateTime(txtChequeDate.Text).ToString("yyyy-MM-dd"))
                    End If
                    'command.Parameters.AddWithValue("@ChequeDate", txtChequeDate.Text)

                    command.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(txtReceivedAmount.Text))

                    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtReceivedAmount.Text))
                    command.Parameters.AddWithValue("@BankAmount", Convert.ToDecimal(txtReceivedAmount.Text))

                    command.Parameters.AddWithValue("@Bank", ddlBankCode.Text.Trim)
                    command.Parameters.AddWithValue("@BankId", txtBankID.Text.Trim)

                    command.Parameters.AddWithValue("@LedgerCode", txtBankGLCode.Text.Trim)
                    command.Parameters.AddWithValue("@LedgerName", txtLedgerName.Text.Trim)
                    command.Parameters.AddWithValue("@BankName", txtBankName.Text.ToUpper.Trim)
                    command.Parameters.AddWithValue("@ContactType", ddlContactType.Text.Trim)
                    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text.Trim)
                    command.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text.Trim)

                    command.Parameters.AddWithValue("@Addr1", txtBillAddress.Text.Trim)
                    command.Parameters.AddWithValue("@Addr2", txtBillStreet.Text.Trim)
                    command.Parameters.AddWithValue("@Addr3", txtBillBuilding.Text.Trim)
                    'command.Parameters.AddWithValue("@Addr4", txtBillCountry.Text & ", " & txtBillPostal.Text)
                    command.Parameters.AddWithValue("@Addr4", txtBillCountry.Text.Trim)

                    command.Parameters.AddWithValue("@Postal", txtBillPostal.Text.Trim)
                    command.Parameters.AddWithValue("@City", txtBillCity.Text.Trim)
                    command.Parameters.AddWithValue("@State", txtBillState.Text.Trim)
                    command.Parameters.AddWithValue("@Country", txtBillCountry.Text.Trim)

                    If chkChequeReturned.Checked = True Then
                        command.Parameters.AddWithValue("@ChequeReturned", 1)
                    Else
                        command.Parameters.AddWithValue("@ChequeReturned", 0)
                    End If

                    'command.Parameters.AddWithValue("@ChequeReturned", chkChequeReturned.Checked)

                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    command.Connection = conn
                    command.ExecuteNonQuery()
                End If

                'Header


                'Dim commandtblServiceBillingDetailItem As MySqlCommand = New MySqlCommand

                'commandtblServiceBillingDetailItem.CommandType = CommandType.Text
                ''Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
                'Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblRecvDet where ReceiptNumber = '" & txtReceiptNo.Text & "'"

                'commandtblServiceBillingDetailItem.CommandText = qrycommandtblServiceBillingDetailItem
                'commandtblServiceBillingDetailItem.Parameters.Clear()
                'commandtblServiceBillingDetailItem.Connection = conn
                'commandtblServiceBillingDetailItem.ExecuteNonQuery()

                'End: Delete Existing Records

                For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                    Dim TextBoxchkSelect As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemTypeGV"), DropDownList)
                    Dim TextBoxRcnoReceipt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoReceiptGV"), TextBox)

                    If lblMessage.Text = "ACTION: CHEQUE RETURN" Then
                        TextBoxRcnoReceipt.Text = "0"
                    End If
                  

                    'If (String.IsNullOrEmpty(TextBoxOtherCode.Text.Trim) = False) Then
                    If (TextBoxItemType.SelectedValue) <> "-1" And String.IsNullOrEmpty(TextBoxOtherCode.Text) = False Then
                        'Header
                        rowselected = rowselected + 1

                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxInvoicePrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                        Dim TextBoxInvoiceGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxInvoiceValue As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                        Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)

                        'Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                        'Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceNoGV"), TextBox)

                        Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)
                        Dim TextBoxTaxType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTaxTypeGV"), TextBox)

                        Dim TextBoxItemCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), TextBox)
                        Dim TextBoxTerms As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtARCodeGV"), TextBox)
                        'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRemarksGV"), TextBox)

                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxDocType As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationGV"), TextBox)
                        Dim TextBoxSourceRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtSourceRcnoGV"), TextBox)

                        If String.IsNullOrEmpty(TextBoxSourceRcno.Text) = True Then
                            TextBoxSourceRcno.Text = 0
                        End If

                        Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxAccountName As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtCustomerNameGV"), TextBox)


                        Dim lContractGroup As String
                        lContractGroup = ""

                        'If String.IsNullOrEmpty(TextBoxContractNo.Text) = False Then
                        '    Dim commandCG As MySqlCommand = New MySqlCommand
                        '    commandCG.CommandType = CommandType.Text

                        '    commandCG.CommandText = "SELECT ContractGroup FROM tblContract where  ContractNo = '" & TextBoxContractNo.Text & "'"
                        '    'command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
                        '    commandCG.Connection = conn

                        '    Dim drCG As MySqlDataReader = commandCG.ExecuteReader()
                        '    Dim dtCG As New DataTable
                        '    dtCG.Load(drCG)


                        '    lContractGroup = dtCG.Rows(0)("ContractGroup").ToString
                        'End If

                        Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                        commandSalesDetail.CommandType = CommandType.Text
                        Dim qrySalesDetail As String
                        qrySalesDetail = ""

                        If String.IsNullOrEmpty(TextBoxRcnoReceipt.Text) = True Then
                            TextBoxRcnoReceipt.Text = "0"
                        End If

                        If Convert.ToInt32(TextBoxRcnoReceipt.Text) = 0 Then
                            qrySalesDetail = "INSERT INTO tblRecvDet(ReceiptNumber, RefType, InvoiceNo, InvoiceDate, InvoicePrice, InvoiceGST, InvoiceValue,  "
                            qrySalesDetail = qrySalesDetail + "ReceiptValue, LedgerCode, LedgerName, RcnoServiceBillingItem, ContractNo, ServiceRecordNo, InvoiceType, TaxType, ItemCode, Terms, ContractGroup,  "
                            qrySalesDetail = qrySalesDetail + "UnitBase, UnitOriginal, ValueBase, ValueOriginal, AppliedBase, AppliedOriginal, SubCode, Description, Quantity, Exchangerate, Currency, DocType, BalanceBase, BalanceOriginal, SourceRcno, AccountID, AccountName, "
                            qrySalesDetail = qrySalesDetail + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                            qrySalesDetail = qrySalesDetail + "(@ReceiptNumber, @RefType, @InvoiceNo, @InvoiceDate, @InvoicePrice, @InvoiceGST,  @InvoiceValue, "
                            qrySalesDetail = qrySalesDetail + "@ReceiptValue, @LedgerCode, @LedgerName, @RcnoServiceBillingItem, @ContractNo, @ServiceRecordNo, @InvoiceType, @TaxType, @ItemCode, @Terms, @ContractGroup, "
                            qrySalesDetail = qrySalesDetail + "@UnitBase, @UnitOriginal, @ValueBase, @ValueOriginal, @AppliedBase, @AppliedOriginal, @SubCode, @Description, @Quantity, @Exchangerate, @Currency, @DocType, @BalanceBase,  @BalanceOriginal, @SourceRcno,  @AccountID, @AccountName, "
                            qrySalesDetail = qrySalesDetail + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"
                        ElseIf Convert.ToInt32(TextBoxRcnoReceipt.Text) > 0 Then
                            qrySalesDetail = "UPDATE tblRecvDet Set ReceiptNumber = @ReceiptNumber, RefType =@RefType, InvoiceNo=@InvoiceNo, InvoiceDate =@InvoiceDate, InvoicePrice=@InvoicePrice, InvoiceGST=@InvoiceGST, InvoiceValue=@InvoiceValue,  "
                            qrySalesDetail = qrySalesDetail + "ReceiptValue=@ReceiptValue, LedgerCode=@LedgerCode, LedgerName=@LedgerName, RcnoServiceBillingItem=@RcnoServiceBillingItem, ContractNo=@ContractNo, ServiceRecordNo=@ServiceRecordNo, InvoiceType=@InvoiceType, TaxType=@TaxType, ItemCode=@ItemCode, Terms=@Terms, ContractGroup=@ContractGroup,  "
                            qrySalesDetail = qrySalesDetail + "UnitBase=@UnitBase, UnitOriginal=@UnitOriginal, ValueBase=@ValueBase, ValueOriginal=@ValueOriginal, AppliedBase=@AppliedBase, AppliedOriginal=@AppliedOriginal, SubCode=@SubCode, Description =@Description, Quantity=@Quantity, Exchangerate=@Exchangerate, Currency=@Currency, BalanceBase=@BalanceBase, BalanceOriginal= @BalanceOriginal, SourceRcno = @SourceRcno,  AccountID=@AccountID, AccountName=@AccountName,  "
                            qrySalesDetail = qrySalesDetail + "LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn "
                            qrySalesDetail = qrySalesDetail + " where Rcno = " & TextBoxRcnoReceipt.Text
                        End If

                        commandSalesDetail.CommandText = qrySalesDetail
                        commandSalesDetail.Parameters.Clear()

                        commandSalesDetail.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)
                        commandSalesDetail.Parameters.AddWithValue("@InvoiceNo", TextBoxInvoiceNo.Text)
                        commandSalesDetail.Parameters.AddWithValue("@RefType", TextBoxInvoiceNo.Text)

                        If TextBoxInvoiceDate.Text.Trim = "" Then
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceDate", DBNull.Value)
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceDate", Convert.ToDateTime(TextBoxInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        End If

                        'commandSalesDetail.Parameters.AddWithValue("@InvoiceDate", TextBoxInvoiceDate.Text)

                        If String.IsNullOrEmpty(TextBoxInvoicePrice.Text) = False Then
                            commandSalesDetail.Parameters.AddWithValue("@InvoicePrice", Convert.ToDecimal(TextBoxInvoicePrice.Text))
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@InvoicePrice", 0.0)
                        End If


                        If String.IsNullOrEmpty(TextBoxInvoiceGST.Text) = False Then
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceGST", Convert.ToDecimal(TextBoxInvoiceGST.Text))
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceGST", 0.0)
                        End If

                        'commandSalesDetail.Parameters.AddWithValue("@InvoiceGST", Convert.ToDecimal(TextBoxInvoiceGST.Text))
                        'commandSalesDetail.Parameters.AddWithValue("@InvoiceValue", Convert.ToDecimal(TextBoxInvoiceValue.Text))

                        If String.IsNullOrEmpty(TextBoxInvoiceValue.Text) = False Then
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceValue", Convert.ToDecimal(TextBoxInvoiceValue.Text))
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@InvoiceValue", 0.0)
                        End If

                        'If String.IsNullOrEmpty(TextBoxInvoicePrice.Text) = False Then
                        '    commandSalesDetail.Parameters.AddWithValue("@InvoicePrice", Convert.ToDecimal(TextBoxInvoicePrice.Text))
                        'Else
                        '    commandSalesDetail.Parameters.AddWithValue("@InvoicePrice", 0.00))
                        'End If

                        commandSalesDetail.Parameters.AddWithValue("@ReceiptValue", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@LedgerCode", TextBoxOtherCode.Text)
                        commandSalesDetail.Parameters.AddWithValue("@LedgerName", TextBoxGLDescription.Text.ToUpper)

                        'commandSalesDetail.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                        'commandSalesDetail.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                        If String.IsNullOrEmpty(TextBoxRcnoInvoice.Text) = False Then
                            commandSalesDetail.Parameters.AddWithValue("@RcnoServiceBillingItem", TextBoxRcnoInvoice.Text)
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@RcnoServiceBillingItem", 0.0)
                        End If

                        'commandSalesDetail.Parameters.AddWithValue("@ContractNo", TextBoxContractNo.Text)
                        'commandSalesDetail.Parameters.AddWithValue("@ServiceRecordNo", TextBoxServiceRecordNo.Text)

                        commandSalesDetail.Parameters.AddWithValue("@ContractNo", "")
                        commandSalesDetail.Parameters.AddWithValue("@ServiceRecordNo", "")

                        commandSalesDetail.Parameters.AddWithValue("@InvoiceType", TextBoxInvoiceType.Text)
                        commandSalesDetail.Parameters.AddWithValue("@TaxType", TextBoxTaxType.Text)

                        commandSalesDetail.Parameters.AddWithValue("@ItemCode", TextBoxItemCode.Text)
                        commandSalesDetail.Parameters.AddWithValue("@Terms", TextBoxTerms.Text.ToUpper)
                        commandSalesDetail.Parameters.AddWithValue("@ContractGroup", lContractGroup)

                        commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(TextBoxReceiptAmt.Text))
                        commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(TextBoxReceiptAmt.Text))

                        commandSalesDetail.Parameters.AddWithValue("@SubCode", TextBoxItemType.Text)
                        commandSalesDetail.Parameters.AddWithValue("@Description", TextBoxRemarks.Text)
                        commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")

                        commandSalesDetail.Parameters.AddWithValue("@Quantity", 1.0)
                        commandSalesDetail.Parameters.AddWithValue("@Exchangerate", 1.0)



                        If TextBoxItemType.Text = "ARIN" And String.IsNullOrEmpty(TextBoxInvoiceNo.Text) = True Then
                            commandSalesDetail.Parameters.AddWithValue("@DocType", "INV")
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@DocType", TextBoxDocType.Text)
                        End If


                        'If TextBoxItemType.Text = "ARIN" And String.IsNullOrEmpty(TextBoxInvoiceNo.Text) = True Then
                        If String.IsNullOrEmpty(TextBoxInvoiceNo.Text) = True Then
                            commandSalesDetail.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(TextBoxReceiptAmt.Text) * (-1))
                            commandSalesDetail.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(TextBoxReceiptAmt.Text) * (-1))
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@BalanceBase", 0.0)
                            commandSalesDetail.Parameters.AddWithValue("@BalanceOriginal", 0.0)
                        End If

                        If TextBoxDocType.Text = "RCT" Then
                            commandSalesDetail.Parameters.AddWithValue("@SourceRcno", TextBoxSourceRcno.Text)
                        Else
                            commandSalesDetail.Parameters.AddWithValue("@SourceRcno", 0)
                        End If


                        commandSalesDetail.Parameters.AddWithValue("@AccountID", TextBoxAccountID.Text)
                        commandSalesDetail.Parameters.AddWithValue("@AccountName", TextBoxAccountName.Text)

                   

                        If Convert.ToInt32(TextBoxRcnoReceipt.Text) = 0 Then
                            commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                            commandSalesDetail.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))
                        End If

                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                        commandSalesDetail.Connection = conn
                        commandSalesDetail.ExecuteNonQuery()
                        'conn.Close()

                    End If   'If String.IsNullOrEmpty(TextBoxInvoiceNo.Text)=False then

                Next rowIndex


                'txt.Text = qry

                If String.IsNullOrEmpty(txt.Text.Trim) = True Then
                    txt.Text = "SELECT PostStatus, BankStatus, GlStatus, ReceiptNumber, ReceiptDate, AccountId, AppliedBase, GSTAmount, "
                    txt.Text = txt.Text & " BaseAmount, ReceiptFrom, ReceiptDate, NetAmount, GlPeriod, CompanyGroup, ContactType, Cheque, ChequeDate, "
                    txt.Text = txt.Text & " BankId,  LedgerCode, LedgerName, Comments, PaymentType, Salesman, Location, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Rcno, ChequeReturned "
                    txt.Text = txt.Text & " FROM tblrecv where 1=1 and ReceiptNumber = '" & txtReceiptNo.Text & "'"

                    txtGrid.Text = "SELECT PostStatus, ReceiptNumber, ReceiptDate, Cheque, AccountId, ContactType, ReceiptFrom, AppliedBase, BankId,  PaymentType, GlPeriod, CompanyGroup,  ChequeDate, ChequeReturned, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn  FROM tblrecv  where  1=1 and ReceiptNumber = '" & txtReceiptNo.Text & "'"
                    'If txtDisplayRecordsLocationwise.Text = "Y" Then
                    '    txt.Text = txt.Text & " and Location = '" & txtLocation.Text & "'"
                    'End If
                End If
                SQLDSReceipt.SelectCommand = txt.Text
                SQLDSReceipt.DataBind()
                GridView1.DataBind()


                'SQLDSReceipt.SelectCommand = txt.Text
                'GridView1.DataSourceID = "SQLDSReceipt"
                'GridView1.DataBind()

                DisplayGLGrid()
                'lblMessage.Text = "ADD: RECEIPT RECORD SUCCESSFULLY ADDED"

                'EnableControls()
            End If


            'FirstGridViewRowBillingDetailsRecs()
            'txtTotalWithDiscAmt.Text = "0.00"
            'txtTotalWithGST.Text = "0.00"
            'txtReceiptAmt.Text = "0.00"
            'MakeMeNull()

            'EnableControls()

            conn.Close()
            conn.Dispose()
            'If rowselected = 0 Then
            '    lblAlert.Text = "PLEASE SELECT A RECORD"
            '    btnShowInvoices.Enabled = False
            '    updPnlMsg.Update()
            '    Exit Sub
            'End If

            DisableControls()

            'InsertNewLogDetail()

            If txtMode.Text = "NEW" Then
                lblMessage.Text = "ADD: RECEIPT RECORD SUCCESSFULLY ADDED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)


                '''''''''''''''''''''''''
                If txtPostUponSave.Text = "1" Then
               IsSuccess = PostReceipt()

                    If IsSuccess = True Then

                        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

                        lblAlert.Text = ""
                        updPnlSearch.Update()
                        updPnlMsg.Update()
                        updpnlBillingDetails.Update()
                        'updpnlServiceRecs.Update()
                        updpnlBillingDetails.Update()

                        btnQuickSearch_Click(sender, e)
                        lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
                        btnReverse.Enabled = True
                        btnReverse.ForeColor = System.Drawing.Color.Black

                        btnEdit.Enabled = False
                        btnEdit.ForeColor = System.Drawing.Color.Gray

                        btnDelete.Enabled = False
                        btnDelete.ForeColor = System.Drawing.Color.Gray

                        btnPost.Enabled = False
                        btnPost.ForeColor = System.Drawing.Color.Gray

                        btnChequeReturn.Enabled = False
                        btnChequeReturn.ForeColor = System.Drawing.Color.Gray


                    End If

                Else
                    mdlPopupConfirmSavePost.Show()
                End If


                'If txtAutoEmailReceipt.Text = "True" Then
                '    mdlConfirmMultiPrint.Show()
                'End If

                '''''''''''''''''''''''''''

            Else
                lblMessage.Text = "EDIT: RECEIPT RECORD SUCCESSFULLY UPDATED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)
            End If

            CalculateTotal()



            txtMode.Text = "View"

            lblAlert.Text = ""
            btnPost.Enabled = True
            updPnlMsg.Update()
            updPnlSearch.Update()
            updPnlBillingRecs.Update()
            'updpnlServiceRecs.Update()

            Session.Add("ReceiptNumber", txtReceiptNo.Text)

            'InsertNewLog()
        
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnSave_Click", ex.Message.ToString, txtReceiptNo.Text)
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
    '        command.CommandText = "SELECT EnableLogforReceipt FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforReceipt").ToString = "1" Then

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

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Receipt")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtReceiptNo.Text.Trim)

    '                commandInsertLog.Connection = connLog
    '                commandInsertLog.ExecuteScalar()

    '                connLog.Close()
    '                commandInsertLog.Dispose()
    '            End If
    '        End If

    '        ' End: Insert NEW Log table
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString
    '        InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION InsertNewLog", ex.Message.ToString, txtReceiptNo.Text)
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
    '        command.CommandText = "SELECT EnableLogforReceipt FROM tblservicerecordmastersetup where rcno=1"
    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            'If Convert.ToBoolean(dt.Rows(0)("EnableLogforCustomer")) = False Then
    '            If dt.Rows(0)("EnableLogforReceipt").ToString = "1" Then

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

    '                commandInsertLog.Parameters.AddWithValue("@pr_ModuleType", "Receipt")
    '                commandInsertLog.Parameters.AddWithValue("@pr_KeyValue", txtReceiptNo.Text.Trim)
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
    '        InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION InsertLogDetail", ex.Message.ToString, txtReceiptNo.Text)
    '        'MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
    '    End Try
    'End Sub
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

            AddNewRowGL()


            ''AR values

            Dim TextBoxGLCodeAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCodeAR.Text = txtBankGLCode.Text

            Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescriptionAR.Text = txtBankName.Text

            Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            'TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtReceiptAmt.Text).ToString("N2")
            TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtReceivedAmount.Text).ToString("N2")

            Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmountAR.Text = (0.0).ToString("N2")



            ' ''GST values

            'rowIndex3 += 1
            'AddNewRowGL()
            'Dim TextBoxGLCodeGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            'TextBoxGLCodeGST.Text = txtGSTOutputCode.Text

            'Dim TextBoxGLDescriptionGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            'TextBoxGLDescriptionGST.Text = txtGSTOutputDescription.Text

            'Dim TextBoxDebitAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            'TextBoxDebitAmountGST.Text = (0.0).ToString("N2")

            'Dim TextBoxCreditAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            'TextBoxCreditAmountGST.Text = Convert.ToDecimal(txtGSTAmount.Text).ToString("N2")
            ' ''GST Values



            rowIndex3 += 1
            AddNewRowGL()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdGL As MySqlCommand = New MySqlCommand
            cmdGL.CommandType = CommandType.Text
            cmdGL.CommandText = "SELECT LedgerCode, LedgerName, AppliedBase   FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "' order by LedgerCode"
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


                lGLCode = dtGL.Rows(0)("LedgerCode").ToString()
                lGLDescription = dtGL.Rows(0)("LedgerName").ToString()
                lCreditAmount = 0.0

                Dim rowIndex4 = 0

                For Each row As DataRow In dtGL.Rows

                    If lGLCode = row("LedgerCode") Then
                        lCreditAmount = lCreditAmount + row("AppliedBase")
                    Else


                        If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
                            AddNewRowGL()
                        End If

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        TextBoxGLCode.Text = lGLCode

                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        TextBoxGLDescription.Text = lGLDescription

                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)

                        If Convert.ToDecimal(lCreditAmount).ToString("N2") < 0.0 Then
                            TextBoxDebitAmount.Text = (Convert.ToDecimal(lCreditAmount) * (-1)).ToString("N2")
                            TextBoxCreditAmount.Text = (0.0).ToString("N2")
                        Else
                            TextBoxDebitAmount.Text = (0.0).ToString("N2")
                            TextBoxCreditAmount.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")
                        End If
                      

                        lGLCode = row("LedgerCode")
                        lGLDescription = If(IsDBNull(row("LedgerName")) = True, "", row("LedgerName"))
                        lCreditAmount = row("AppliedBase")

                        rowIndex3 += 1
                        rowIndex4 += 1
                    End If
                Next row

            End If


            'AddNewRowGL()

            Dim TextBoxGLCode1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCode1.Text = lGLCode

            Dim TextBoxGLDescription1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescription1.Text = lGLDescription

            Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmount1.Text = (0.0).ToString("N2")

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


            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "DisplayGLGrid", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FirstGridViewRowGL", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "AddNewRowGL", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "AddNewRowWithDetailRecGL", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "SetPreviousDataGL", ex.Message.ToString, "")
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "SetRowDataGL", ex.Message.ToString, "")
        End Try

    End Sub
    Protected Sub btnShowInvoices_Click(sender As Object, e As EventArgs) Handles btnShowInvoices.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            lblAlert.Text = ""

            If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = False Then
                'ddlCompanyGrpII.Text = txtCompanyGroup.Text
                ddlContactTypeII.Text = ddlContactType.Text
                txtAccountIdII.Text = txtAccountIdBilling.Text
                txtClientNameII.Text = txtAccountName.Text

                'ddlCompanyGrpII.Enabled = False
                ddlContactTypeII.Enabled = False
                txtAccountIdII.Enabled = False
                'txtClientNameII.Enabled = False
                btnClient.Visible = False
            Else
                'ddlCompanyGrp.Text = txtCompanyGroup.Text
                'txtAccountId.Text = txtAccountIdBilling.Text
                'txtClientName.Text = txtAccountName.Text

                'ddlCompanyGrpII.Enabled = True
                ddlContactType.Enabled = True
                txtAccountIdII.Enabled = True
                txtClientNameII.Enabled = True
                btnClient.Visible = True

            End If

            Dim Query As String

            Query = "SELECT ContractNo FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  AccountID = '" & txtAccountIdBilling.Text & "' order by ContractNo "
            PopulateDropDownList(Query, "ContractNo", "ContractNo", ddlContractNoII)

            mdlImportServices.Show()

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "SetRowDataGL", ex.Message.ToString, "")
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

                'CalculatePrice()
                'If dtGST.Rows(0)("GST").ToString = "P" Then
                '    lblAlert.Text = "SCHEUDLE HAS ALREADY BEEN GENERATED"
                '    conn1.Close()
                '    Exit Sub
                'End If
            End If

            conn1.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "txtTaxTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub


    Protected Sub txtItemTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

            Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
            Dim lblid1 As DropDownList = CType(xrow1.FindControl("txtItemTypeGV"), DropDownList)
            'Dim lblid2 As TextBox = CType(xrow1.FindControl("txtTargtIdGV"), TextBox)


            'lTargetDesciption = lblid1.Text

            Dim rowindex1 As Integer = xrow1.RowIndex
            If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
                btnAddDetail_Click(sender, e)
                'txtRecordAdded.Text = "Y"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "txtItemTypeGV_SelectedIndexChanged", ex.Message.ToString, "")
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
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "txtItemCodeGV_SelectedIndexChanged", ex.Message.ToString, "")
       
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

                Dim TextBoxRcnoRecvDet As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoReceiptGV"), TextBox)
                Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)

                Dim conn As MySqlConnection = New MySqlConnection(constr)
                If (String.IsNullOrEmpty(TextBoxRcnoRecvDet.Text) = False) Then
                    If (Convert.ToInt32(TextBoxRcnoRecvDet.Text) > 0) Then

                        conn.Open()

                        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                        commandUpdGS.CommandType = CommandType.Text
                        commandUpdGS.CommandText = "Delete from tblrecvdet where rcno = " & TextBoxRcnoRecvDet.Text
                        commandUpdGS.Connection = conn
                        commandUpdGS.ExecuteNonQuery()

                        commandUpdGS.Dispose()
                        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", TextBoxRcnoRecvDet.Text)

                        Updatetblservicebillingdetailitem(Convert.ToInt64(TextBoxRcnoInvoice.Text))
                    End If
                End If

                If dt.Rows.Count > 0 Then
                    dt.Rows.Remove(dt.Rows(rowIndex))
                    drCurrentRow = dt.NewRow()
                    ViewState("CurrentTableBillingDetailsRec") = dt
                    grvBillingDetails.DataSource = dt
                    grvBillingDetails.DataBind()

                    rowdeleted = "Y"
                    SetPreviousDataBillingDetailsRecs()

                    calculateTotalReceipt()
                    rowdeleted = "N"
                    If dt.Rows.Count = 0 Then
                        FirstGridViewRowBillingDetailsRecs()
                    End If

                    Dim commandUpdRecv As MySqlCommand = New MySqlCommand
                    commandUpdRecv.CommandType = CommandType.Text
                    commandUpdRecv.CommandText = "Update tblrecv set NetAmount =" & Convert.ToDecimal(txtReceiptAmt.Text) & " where ReceiptNumber = '" & txtReceiptNo.Text & "'"
                    commandUpdRecv.Connection = conn
                    commandUpdRecv.ExecuteNonQuery()

                    commandUpdRecv.Dispose()
                    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                End If

                'CalculateTotalPrice()

                conn.Close()
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "grvBillingDetails_RowDeleting", ex.Message.ToString, "")

        End Try
    End Sub


    Protected Sub Updatetblservicebillingdetailitem(TextBoxRcnoInvoice As Long)
        Try
            Dim lRcoInvoice As Long = TextBoxRcnoInvoice
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()
            Dim cmdCNSum As MySqlCommand = New MySqlCommand
            cmdCNSum.CommandType = CommandType.Text
            cmdCNSum.CommandText = "SELECT sum(AppliedBase) as receiptsum FROM tblrecvdet where  RcnoServiceBillingItem = " & Convert.ToInt64(lRcoInvoice)
            cmdCNSum.Connection = conn

            Dim drCNSum As MySqlDataReader = cmdCNSum.ExecuteReader()
            Dim dtCNSum As New DataTable
            dtCNSum.Load(drCNSum)

            'FirstGridViewRowGL()

            'Dim TotDetailRecordsLoc = dtGL.Rows.Count
            'If dtCNSum.Rows.Count > 0 Then

            Dim lReceiptSum As Decimal


            If dtCNSum.Rows.Count = 0 Then
                lReceiptSum = 0.0
            ElseIf IsDBNull(dtCNSum.Rows(0)("receiptsum")) = True Then
                lReceiptSum = 0.0
            Else
                lReceiptSum = Convert.ToDecimal(dtCNSum.Rows(0)("receiptsum"))
            End If



            'If String.IsNullOrEmpty(dtCNSum.Rows(0)("receiptsum")) = True Then
            '    lReceiptSum = 0.0
            'Else
            '    lReceiptSum = Convert.ToDecimal(dtCNSum.Rows(0)("receiptsum"))
            'End If
            'End If



            Dim commandUpdateInvoiceValue1 As MySqlCommand = New MySqlCommand
            commandUpdateInvoiceValue1.CommandType = CommandType.Text
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & Convert.ToDecimal(lReceptAmtAdjusted) & " where Rcno = " & row1("Rcno")
            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & lReceiptSum & " where Rcno = " & Convert.ToInt64(lRcoInvoice)
            Dim sqlUpdateInvoiceValue1 As String = "Update tblSalesDetail set ReceiptAmount = " & lReceiptSum & " where Rcno = " & Convert.ToInt64(lRcoInvoice)

            commandUpdateInvoiceValue1.CommandText = sqlUpdateInvoiceValue1
            commandUpdateInvoiceValue1.Parameters.Clear()
            commandUpdateInvoiceValue1.Connection = conn
            commandUpdateInvoiceValue1.ExecuteNonQuery()

            conn.Close()
            conn.Dispose()

            cmdCNSum.Dispose()
            commandUpdateInvoiceValue1.Dispose()
            dtCNSum.Dispose()
            drCNSum.Close()
            'End If

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "Updatetblservicebillingdetailitem", ex.Message.ToString, "")

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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "grvBillingDetails_RowDataBound", ex.Message.ToString, "")

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
        '    exstr = ex.ToString
        '    'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
        txtReceiptDateFromSearch.Text = ""
        txtReceiptDateToSearch.Text = ""
        txtCommentsSearch.Text = ""
        ddlBankIDSearch.SelectedIndex = 0
        ddlCompanyGrpSearch.SelectedIndex = 0
        ddlBranch.SelectedIndex = 0
        ddlPaymentModeSearch.SelectedIndex = 0

        ddlContactTypeSearch.SelectedIndex = 0
        txtClientNameSearch.Text = ""
        txtChequeNoSearch.Text = ""

        'txtBillingPeriodSearch.Text = ""
        txtClientNameSearch.Text = ""
        ddlCompanyGrpSearch.SelectedIndex = 0
        'ddlSalesmanSearch.SelectedIndex = 0
        txtSearch1Status.Text = "O,P"
        txtInvoicenoSearch.Text = ""

        'ddlCompanyGrpSearch.SelectedIndex = 0

        'btnQuickSearch_Click(sender, e)


        'btnSearch1Status_Click(sender, e)
    End Sub

    Protected Sub chkSelectGV_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
            xgrvBillingDetails = CType(chk1.NamingContainer, GridViewRow)


            Dim lblid1 As TextBox = CType(chk1.FindControl("txtTotalPriceWithGSTGV"), TextBox)
            Dim lblid2 As TextBox = CType(chk1.FindControl("txtTotalReceiptAmtGV"), TextBox)
            Dim lblid3 As TextBox = CType(chk1.FindControl("txtTotalCreditNoteAmtGV"), TextBox)
            Dim lblid4 As TextBox = CType(chk1.FindControl("txtReceiptAmtGV"), TextBox)

            If chk1.Checked = True Then
                lblid4.Text = lblid1.Text - lblid2.Text - lblid3.Text
            Else
                lblid4.Text = "0.00"
            End If

            lblid4.Text = Convert.ToDecimal(lblid4.Text).ToString("N2")
            calculateTotalReceipt()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "chkSelectGV_CheckedChanged", ex.Message.ToString, "")

        End Try
    End Sub

    Private Sub calculateTotalReceipt()
        Try
            Dim TotalReceiptAmt As Decimal = 0
            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


            If (table.Rows.Count > 0) Then

                For i As Integer = 0 To (table.Rows.Count) - 1

                    Dim TextBoxSelect As CheckBox = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)
                    Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtReceiptAmtGV"), TextBox)

                    If String.IsNullOrEmpty(TextBoxReceiptAmt.Text) = True Then
                        TextBoxReceiptAmt.Text = "0.00"
                    End If

                    'If TextBoxSelect.Checked = True Then
                    TotalReceiptAmt = TotalReceiptAmt + Convert.ToDecimal(TextBoxReceiptAmt.Text)
                    'End If
                Next i

            End If


            txtReceiptAmt.Text = TotalReceiptAmt.ToString("N2")

            updPanelSave.Update()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "calculateTotalReceipt", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub txtReceiptAmtGV_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txt1 As TextBox = DirectCast(sender, TextBox)
            xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

            Dim lblid1 As CheckBox = CType(txt1.FindControl("chkSelectGV"), CheckBox)
            Dim lblid2 As TextBox = CType(txt1.FindControl("txtReceiptAmtGV"), TextBox)
            'Dim lblid3 As TextBox = CType(txt1.FindControl("txtBalanceAmtGV"), TextBox)

            If Convert.ToDecimal(lblid2.Text) > 0.0 Then
                lblid1.Checked = True
            Else
                lblid1.Checked = False
            End If

            'If Convert.ToDecimal(lblid2.Text) > Convert.ToDecimal(lblid3.Text) Then
            '    lblid2.Text = lblid3.Text
            'End If

            lblid2.Text = Convert.ToDecimal(lblid2.Text).ToString("N2")


            calculateTotalReceipt()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "txtReceiptAmtGV_TextChanged", ex.Message.ToString, "")

        End Try
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

        gvClient.PageIndex = e.NewPageIndex



        'If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
        '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' order by name"
        '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' order by name"
        '    End If
        'Else
        '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status = 'O' and (upper(Name) Like '%" + txtPopUpClient.Text.Trim.ToUpper + "%' or accountid like """ + txtPopUpClient.Text + "%"" or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
        '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status = 'O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
        '    End If

        '    'SqlDSClient.DataBind()
        '    'gvClient.DataBind()
        '    'mdlPopUpClient.Show()
        'End If


        If txtClientFrom.Text = "ImportClient" Then
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

    End Sub

    'Protected Sub btnPopUpClientReset_Click(sender As Object, e As EventArgs) Handles btnPopUpClientReset.Click
    '    txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    '    'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "'  or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"

    '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
    '        SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1  order by name"
    '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
    '        SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblPERSON where 1=1  order by name"
    '    End If
    '    SqlDSClient.DataBind()
    '    gvClient.DataBind()
    '    mdlPopUpClient.Show()
    'End Sub

    'Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged

    '    lblAlert.Text = ""

    '    txtAccountIdBilling.Text = ""


    '    If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
    '        txtAccountIdBilling.Text = ""
    '    Else
    '        txtAccountIdBilling.Text = gvClient.SelectedRow.Cells(1).Text.Trim
    '    End If



    '    If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
    '        txtAccountName.Text = ""
    '    Else
    '        txtAccountName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
    '    End If


    'End Sub

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

        sqlDSOnHold.SelectCommand = "Select ContractNo, OnHoldDate, LastModifiedBy, ServiceAddress from tblContract where Status = 'H' and AccountId ='" & txtAccountIdBilling.Text & "'"

        grdViewsqlDSOnHold.DataSourceID = "sqlDSOnHold"
        grdViewsqlDSOnHold.DataBind()

        If grdViewsqlDSOnHold.Rows.Count > 0 Then
            lblOnHold.Visible = True
            lblOnHold.Text = "The following Contracts are On-hold for this Account: "
        Else
            lblOnHold.Visible = False
            lblOnHold.Text = ""
        End If
    End Sub

    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        Try
            lblAlert.Text = ""
            txtSearch.Text = ""
            txtSearch.Text = "CustomerSearch"
            'If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
            '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            '    lblAlert.Text = "SELECT ACCOUNT TYPE TO PROCEED"
            '    Exit Sub
            'End If


            If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIdBilling.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where    (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If
               

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"

                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
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
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr

            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnClient_Click", ex.Message.ToString, "")


        End Try
    End Sub

    Protected Sub ddlPaymentMode_TextChanged(sender As Object, e As EventArgs) Handles ddlPaymentMode.TextChanged
        'If ddlPaymentMode.Text = "CHEQUE" Then
        '    txtChequeNo.Attributes.Remove("readonly")
        '    txtChequeDate.Attributes.Remove("readonly")
        'Else
        '    txtChequeNo.Text = ""
        '    txtChequeDate.Text = ""

        '    txtChequeNo.Attributes.Add("readonly", "readonly")
        '    txtChequeDate.Attributes.Add("readonly", "readonly")
        'End If


        '''''''''''''''

        Try
            Dim lBankId As String = ""

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            Dim sql As String
            sql = ""
            sql = "Select DefaultBank,DefaultPaymentReference from tblSettleType where SettleType = '" & ddlPaymentMode.Text & "'"

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("DefaultBank").ToString <> "" Then : lBankId = dt.Rows(0)("DefaultBank").ToString : End If
            End If
            conn.Close()

            Dim Query As String

            ddlBankCode.Items.Clear()
            ddlBankCode.Items.Add("--SELECT--")

            If txtDisplayRecordsLocationwise.Text = "N" Then
                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank  where BankId = '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)

                ddlBankCode.SelectedIndex = 1

                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank   where BankId <> '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)
            Else
                'Query = "SELECT CONCAT(BankId, ' - ', Bank) AS codedes FROM tblBank   where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and BankId = '" & lBankId & "' ORDER BY BankId"
                'PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)

                'ddlBankCode.SelectedIndex = 1

                'Query = "SELECT CONCAT(BankId, ' - ', Bank) AS codedes FROM tblBank  where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and BankId <> '" & lBankId & "' ORDER BY BankId"
                'PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)



                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank   where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and BankId = '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)

                ddlBankCode.SelectedIndex = 1

                Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank  where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and BankId <> '" & lBankId & "' ORDER BY BankId"
                PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)

                'Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank   where location ='" & txtLocation.Text & "' and BankId = '" & lBankId & "' ORDER BY BankId"
                'PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)

                '    ddlBankCode.SelectedIndex = 1

                '    Query = "SELECT CONCAT(BankId, ' : ', Bank) AS codedes FROM tblBank  where location ='" & txtLocation.Text & "' and BankId <> '" & lBankId & "' ORDER BY BankId"
                '    PopulateDropDownList(Query, "codedes", "codedes", ddlBankCode)
            End If


            txtBankID.Text = ""
            txtBankName.Text = ""
            txtBankGLCode.Text = ""
            txtLedgerName.Text = ""

            conn.Dispose()
            command1.Dispose()
            dt.Dispose()
            dr.Close()

            ddlBankCode_SelectedIndexChanged(sender, e)

            txtChequeNo.Text = ""
            'If dt.Rows(0)("DefaultPaymentReference").ToString <> "" Then
            '    Dim str1 As String
            '    str1 = Trim(dt.Rows(0)("DefaultPaymentReference"))
            '    Dim dt1 As Date
            '    dt1 = Convert.ToDateTime(txtReceiptDate.Text).ToString("dd/MM/yy")

            '    If Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 5) = "YYYY}" Then
            '        txtChequeNo.Text = str1.Replace("{DD/MM/YYYY}", txtReceiptDate.Text)
            '    ElseIf Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 5) = "M/YY}" Then
            '        txtChequeNo.Text = str1.Replace("{DD/MM/YY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("dd/MM/yy"))
            '    End If
            'End If


            If dt.Rows(0)("DefaultPaymentReference").ToString <> "" Then
                Dim str1 As String
                str1 = Trim(dt.Rows(0)("DefaultPaymentReference"))
                Dim dt1 As Date
                dt1 = Convert.ToDateTime(txtReceiptDate.Text).ToString("dd/MM/yy")

                'dd/mm/yy}
                'dd/mm/yyyy}
                'ddmmyy}
                'ddmmyyyy}


                If Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 6) = "MM/YY}" Then
                    txtChequeNo.Text = str1.Replace("{DD/MM/YY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("dd/MM/yy"))
                ElseIf Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 6) = "/YYYY}" Then
                    txtChequeNo.Text = str1.Replace("{DD/MM/YYYY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("dd/MM/yyyy"))
                ElseIf Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 6) = "DMMYY}" Then
                    txtChequeNo.Text = str1.Replace("{DDMMYY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("ddMMyy"))
                ElseIf Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 6) = "MYYYY}" Then
                    txtChequeNo.Text = str1.Replace("{DDMMYYYY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("ddMMyyyy"))

                    'ElseIf Right(Trim(dt.Rows(0)("DefaultPaymentReference").ToString), 4) = "MYY}" Then
                    '    txtChequeNo.Text = str1.Replace("{DDMMYY}", Convert.ToDateTime(txtReceiptDate.Text).ToString("ddMMyy"))
                End If
                End If

                txtChequeDate.Text = txtReceiptDate.Text

                'If Left(ddlPaymentMode.Text, 4) = "CASH" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'ElseIf Left(ddlPaymentMode.Text, 4) = "FAST" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'ElseIf Left(ddlPaymentMode.Text, 4) = "CHEQ" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'ElseIf Left(ddlPaymentMode.Text, 4) = "CRED" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'ElseIf Left(ddlPaymentMode.Text, 4) = "PAYN" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'ElseIf Left(ddlPaymentMode.Text, 4) = "GIRO" Then
                '    txtChequeDate.Text = txtReceiptDate.Text
                'End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "ddlPaymentMode_TextChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub ddlBankCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankCode.SelectedIndexChanged
        Try
            If ddlBankCode.SelectedIndex > 0 Then
                Dim hyphenpos As Integer
                hyphenpos = 0
                hyphenpos = (ddlBankCode.Text.IndexOf(":"))


                txtBankGLCode.Text = Left(ddlBankCode.Text, (hyphenpos - 1))
                txtBankName.Text = Right(ddlBankCode.Text, Len(ddlBankCode.Text) - (Len(txtBankGLCode.Text) + 3))
                txtBankID.Text = Left(ddlBankCode.Text, (hyphenpos - 1))

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Open()

                Dim sql As String
                sql = ""
                sql = "Select LedgerCode, RecvPrefix from tblBank where bankId = '" & txtBankID.Text & "'"

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text
                command1.CommandText = sql
                command1.Connection = conn

                Dim dr As MySqlDataReader = command1.ExecuteReader()

                Dim dt As New DataTable
                dt.Load(dr)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("LedgerCode").ToString <> "" Then : txtBankGLCode.Text = dt.Rows(0)("LedgerCode").ToString : End If
                    If dt.Rows(0)("RecvPrefix").ToString <> "" Then : txtRecvPrefix.Text = dt.Rows(0)("RecvPrefix").ToString : End If

                End If


                sql = ""
                sql = "Select Description from tblChartOfAccounts where COACode = '" & txtBankGLCode.Text & "'"

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text
                command2.CommandText = sql
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                Dim dt2 As New DataTable
                dt2.Load(dr2)

                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("Description").ToString <> "" Then : txtLedgerName.Text = dt2.Rows(0)("Description").ToString : End If
                End If

                conn.Close()

                conn.Dispose()
                command1.Dispose()
                dt.Dispose()
                dr.Close()

                command2.Dispose()
                dt2.Dispose()
                dr2.Close()
                'txtBankGLCode
                'txtLedgerName()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "ddlBankCode_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub


    Private Sub UpdateTblSales(strInvoiceNo As String, strDocType As String, strSourceRcno As Long, Amt As Decimal)
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

            If strDocType = "RCT" Then
                Dim command1Rct As MySqlCommand = New MySqlCommand

                command1Rct.CommandType = CommandType.Text

                command1Rct.CommandText = "SELECT BalanceBase FROM tblRecvDet where Rcno = " & strSourceRcno
                command1Rct.Connection = conn

                Dim dr1Rct As MySqlDataReader = command1Rct.ExecuteReader()
                Dim dt1Rct As New DataTable
                dt1Rct.Load(dr1Rct)

                If dt1Rct.Rows.Count > 0 Then
                    lInvoiceAmount = dt1Rct.Rows(0)("BalanceBase").ToString
                End If

                Dim command2Rct As MySqlCommand = New MySqlCommand
                command2Rct.CommandType = CommandType.Text

                command2Rct.CommandText = "SELECT sum(ValueBase) as totValue FROM tblRecvDet where SourceRcno = " & strSourceRcno
                command2Rct.Connection = conn

                Dim dr2Rct As MySqlDataReader = command2Rct.ExecuteReader()
                Dim dt2Rct As New DataTable
                dt2Rct.Load(dr2Rct)

                If dt2Rct.Rows.Count > 0 Then
                    'lInvoiceAmount = lInvoiceAmount + dt2Rct.Rows(0)("totValue").ToString
                    lInvoiceAmount = lInvoiceAmount - dt2Rct.Rows(0)("totValue").ToString
                End If


                Dim command3Rct As MySqlCommand = New MySqlCommand
                command3Rct.CommandType = CommandType.Text

                command3Rct.CommandText = "UPDATE tblRecvDet SET BalanceBase = " & lInvoiceAmount & " WHERE Rcno = " & strSourceRcno
                command3Rct.Connection = conn

                command3Rct.ExecuteNonQuery()
                conn.Close()
                conn.Dispose()
                command1Rct.Dispose()
                command2Rct.Dispose()
                command3Rct.Dispose()
            Else

                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text

                command1.CommandText = "SELECT AppliedBase FROM tblSales where InvoiceNumber = '" & strInvoiceNo & "'"
                command1.Connection = conn

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                If dt1.Rows.Count > 0 Then
                    lInvoiceAmount = dt1.Rows(0)("AppliedBase").ToString
                End If

                Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text

                'command2.CommandText = "UPDATE tblSales SET ReceiptBase = (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblRecvDet A, tblRecv B WHERE " & _
                '      "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN'  AND A.RefType = tblSales.InvoiceNumber AND " & _
                '      "B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & strInvoiceNo & "'"
                'command2.Connection = conn

                'If strInvoiceNo = "AR202007-026890" Then
                '    InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), command2.CommandText, "", "")
                'End If


                command2.CommandType = CommandType.StoredProcedure
                command2.CommandText = "UpdatetblSalesReceiptBalance"

                'command2.CommandText = "UPDATE tblSales SET ReceiptBase = (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblRecvDet A, tblRecv B WHERE " & _
                '      "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN'  AND A.RefType = tblSales.InvoiceNumber AND " & _
                '      "B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & strInvoiceNo & "'"
                '
                command2.Parameters.AddWithValue("@pr_InvoiceNumber", strInvoiceNo)
                command2.Parameters.AddWithValue("@pr_UpdateType", "ReceiptBase")
                command2.Parameters.AddWithValue("@pr_Balance", 0.0)
                command2.Connection = conn
                command2.ExecuteNonQuery()

                Dim lbalance As Decimal
                Dim command3 As MySqlCommand = New MySqlCommand
                command3.CommandType = CommandType.Text
                command3.CommandText = "SELECT ValueBase, GSTBase ,ReceiptBase, CreditBase, DocType FROM tblSales where InvoiceNumber = '" & strInvoiceNo & "'"
                command3.Connection = conn
                command3.ExecuteNonQuery()

                Dim dr3 As MySqlDataReader = command3.ExecuteReader()
                Dim dt3 As New DataTable
                dt3.Load(dr3)

                If dt3.Rows.Count > 0 Then

                    If String.IsNullOrEmpty(dt3.Rows(0)("ValueBase").ToString) = False Then
                        lbalance = dt3.Rows(0)("ValueBase").ToString
                    Else
                        lbalance = 0.0
                    End If

                    If String.IsNullOrEmpty(dt3.Rows(0)("GSTBase").ToString) = False Then
                        lbalance = lbalance + dt3.Rows(0)("GSTBase").ToString
                    Else
                        'lbalance = 0.0
                    End If

                    If String.IsNullOrEmpty(dt3.Rows(0)("ReceiptBase").ToString) = False Then
                        lbalance = lbalance - dt3.Rows(0)("ReceiptBase").ToString
                    Else
                        'lbalance = 0.0
                    End If

                    If String.IsNullOrEmpty(dt3.Rows(0)("CreditBase").ToString) = False Then
                        lbalance = lbalance - dt3.Rows(0)("CreditBase").ToString
                    Else
                        'lbalance = 0.0
                    End If

                End If

                ''''''''''''''''''''''''


                Dim command4 As MySqlCommand = New MySqlCommand
                'command4.CommandType = CommandType.Text

                ' ''Dim qry4 As String = "Update tblSales Set PaidStatus = '" & lstatus & "', TotalReceiptAmount = " & lTotalReceipt & " where InvoiceNumber = @InvoiceNumber "
                ''Dim qry4 As String = "Update tblSales Set BalanceBase = " & lbalance & " where InvoiceNumber = @InvoiceNumber "

                'Dim qry4 As String = "Update tblSales Set BalanceBase = " & lbalance & " where InvoiceNumber = '" & strInvoiceNo & "'"

                'If strInvoiceNo = "AR202007-026890" Then
                '    InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), qry4, "", "")
                'End If
                'command4.CommandText = qry4

                command4.CommandType = CommandType.StoredProcedure
                command4.CommandText = "UpdatetblSalesReceiptBalance"

                'command2.CommandText = "UPDATE tblSales SET ReceiptBase = (SELECT ifnull(SUM(ifnull(A.ValueBase,0)+ifnull(A.GstBase,0)),0) FROM tblRecvDet A, tblRecv B WHERE " & _
                '      "A.ReceiptNumber=B.ReceiptNumber AND A.SubCode = 'ARIN'  AND A.RefType = tblSales.InvoiceNumber AND " & _
                '      "B.PostStatus = 'P' ) WHERE InvoiceNumber = '" & strInvoiceNo & "'"
                command4.Parameters.Clear()
                command4.Parameters.AddWithValue("@pr_InvoiceNumber", strInvoiceNo)
                command4.Parameters.AddWithValue("@pr_UpdateType", "BalanceBase")
                command4.Parameters.AddWithValue("@pr_Balance", lbalance)


                'command4.Parameters.AddWithValue("@InvoiceNumber", strInvoiceNo)
                command4.Connection = conn
                command4.ExecuteNonQuery()

                '    'End: Update tblSales


                ' ''Start: Update Invoice correponding to CN

                'If dt3.Rows(0)("DocType").ToString() = "ARCN" Or dt3.Rows(0)("DocType").ToString() = "ARDN" Then

                '    Dim command5 As MySqlCommand = New MySqlCommand
                '    command5.CommandType = CommandType.Text
                '    command5.CommandText = "SELECT Appliedbase FROM tblSales where InvoiceNumber = '" & strInvoiceNo & "'"
                '    command5.Connection = conn
                '    command5.ExecuteNonQuery()

                '    Dim dr5 As MySqlDataReader = command5.ExecuteReader()
                '    Dim dt5 As New DataTable
                '    dt5.Load(dr5)

                '    If dt5.Rows(0)("Appliedbase").ToString() = Amt Then

                '        Dim command6 As MySqlCommand = New MySqlCommand
                '        command6.CommandType = CommandType.Text
                '        command6.CommandText = "SELECT AppliedBase, SourceInvoice FROM tblSalesDetail where InvoiceNumber = '" & strInvoiceNo & "'"
                '        command6.Connection = conn
                '        command6.ExecuteNonQuery()

                '        Dim dr6 As MySqlDataReader = command6.ExecuteReader()
                '        Dim dt6 As New DataTable
                '        dt6.Load(dr6)


                '        If String.IsNullOrEmpty(dt6.Rows(0)("SourceInvoice").ToString().Trim) = False Then

                '            For Each row As DataRow In dt6.Rows


                '                Dim command7 As MySqlCommand = New MySqlCommand
                '                command7.CommandType = CommandType.Text
                '                command7.CommandText = "SELECT Creditbase, BalanceBase FROM tblSales where InvoiceNumber = '" & dt6.Rows(0)("SourceInvoice").ToString().Trim & "'"
                '                command7.Connection = conn
                '                command7.ExecuteNonQuery()

                '                Dim dr7 As MySqlDataReader = command7.ExecuteReader()
                '                Dim dt7 As New DataTable
                '                dt7.Load(dr7)

                '                Dim lbal As Decimal = 0.0
                '                Dim rowamt As Decimal = 0.0

                '                lbal = dt7.Rows(0)("BalanceBase").ToString()
                '                rowamt = row("AppliedBase")

                '                Dim command8 As MySqlCommand = New MySqlCommand
                '                command8.CommandType = CommandType.Text

                '                ''Dim qry4 As String = "Update tblSales Set PaidStatus = '" & lstatus & "', TotalReceiptAmount = " & lTotalReceipt & " where InvoiceNumber = @InvoiceNumber "
                '                Dim qry8 As String = "Update tblSales Set Creditbase = " & dt7.Rows(0)("Creditbase").ToString() + row("AppliedBase") & ", BalanceBase = " & dt7.Rows(0)("BalanceBase").ToString() - row("AppliedBase") & " where InvoiceNumber = @InvoiceNumber "

                '                command8.CommandText = qry8
                '                command8.Parameters.Clear()

                '                command8.Parameters.AddWithValue("@InvoiceNumber", row("SourceInvoice"))
                '                command8.Connection = conn
                '                command8.ExecuteNonQuery()
                '            Next
                '        End If
                '    End If
                'End If

                ''End: Update Invoice corresponding to CN

                conn.Close()
                conn.Dispose()

                command1.Dispose()
                command2.Dispose()

                command3.Dispose()
                dt3.Dispose()
                command4.Dispose()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "UpdateTblSales", ex.Message.ToString, txtReceiptNo.Text)
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""
            txtEvent.Visible = False
            txtEvent.Text = "POST"
            'lblEvent.Text = "Confirm POST"
            'lblQuery.Text = "Are you sure to POST the Receipt?"

            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            Dim IsLock = FindRVPeriod(txtReceiptPeriod.Text)
            If IsLock = "Y" Then
                lblAlert.Text = "PERIOD IS LOCKED"
                updPnlMsg.Update()
                txtReceiptDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            If Convert.ToDecimal(txtReceiptAmt.Text) <> Convert.ToDecimal(txtReceivedAmount.Text) Then
                lblAlert.Text = "RECEIPT AMOUNT AND SUM OF APPLIED INVOICES SHOULD BE EQUAL"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            mdlPopupConfirmPost.Show()

            'Dim conn As MySqlConnection = New MySqlConnection()
            ' '''''''''''''''''''''''''''

            'SetRowDataBillingDetailsRecs()
            'Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            'conn.Close()

            'If tableAdd IsNot Nothing Then

            'For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
            '    Dim TextBoxchkSelect1 As CheckBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)

            '    If (TextBoxchkSelect1.Checked = True) Then
            '        rowselected = rowselected + 1
            '    End If
            'Next

            'If rowselected = 0 Then
            '    lblAlert.Text = "PLEASE ENTER DETAIL RECORD"
            '    btnShowInvoices.Enabled = False
            '    updPnlMsg.Update()
            '    Exit Sub
            'End If

            'End If


            ''''''''''''''''''''''''''




        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnPost_Click", ex.Message.ToString, "")
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
        If txtFrom.Text = "invoice" Then
            Session("receiptfrom") = "invoice"
            Response.Redirect("Invoice.aspx")
        ElseIf txtFrom.Text = "invoiceQR" Then
            Session("receiptfrom") = "invoice"
            Response.Redirect("Invoice.aspx")
        ElseIf txtFrom.Text = "invoicePB" Then
            Session("receiptfrom") = "invoicePB"
            Response.Redirect("InvoiceProgressBilling.aspx")
        End If

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

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        Try
            If txtPopUpClient.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter client name", "str")
                Exit Sub
            End If


            If txtClientFrom.Text = "ImportClient" Then

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like '%" + txtPopUpClient.Text.Trim + "%' or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like '%" + txtPopUpClient.Text.Trim + "%' or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If

                txtImportService.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If

            If txtSearch.Text = "CustomerSearch" Then

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
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
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or B.CompanyID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like ""%" + txtPopUpClient.Text.Trim + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text.Trim + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopUpClient.Text + "%"" or B.CompanyID like ""%" + txtPopUpClient.Text + "%"" or B.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopUpClient.Text + "%"" or D.PersonID  like ""%" + txtPopUpClient.Text + "%"" or D.BillContact1Svc like ""%" + txtPopUpClient.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If

                txtInvoiceSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"

            End If

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
        End Try

    End Sub


    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging

    End Sub

    Protected Sub txtReceiptDate_TextChanged(sender As Object, e As EventArgs) Handles txtReceiptDate.TextChanged
        txtReceiptPeriod.Text = Year(Convert.ToDateTime(txtReceiptDate.Text)) & Format(Month(Convert.ToDateTime(txtReceiptDate.Text)), "00")
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        Try

            If txtSearch.Text = "InvoiceSearch" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                End If

            End If

            If txtSearch.Text = "CustomerSearch" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"


                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                End If


            End If

            If txtClientFrom.Text = "ImportClient" Then
                txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeII.Text = "CORPORATE" Or ddlContactTypeII.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeII.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeII.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc,  B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') UNION  SELECT 'PERSON' as AccountType, C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') order by AccountID,  LocationId, ServiceName"
                    End If
                End If


            End If

            SqlDSClient.DataBind()
            gvClient.DataBind()
            mdlPopUpClient.Show()
            txtIsPopup.Text = "Client"
        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message.ToString
            'lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnPopUpClientReset_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        Try
            lblAlert.Text = ""


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
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%"") UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
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
                updPanelReceipt.Update()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, B.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, D.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False  and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like """ + txtPopupClientSearch.Text + "%"" or B.CompanyID like ""%" + txtPopupClientSearch.Text + "%"" or B.contactperson like ""%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like """ + txtPopupClientSearch.Text + "%"" or D.PersonID  like ""%" + txtPopupClientSearch.Text + "%"" or D.contACTperson like ""%" + txtPopupClientSearch.Text + "%"") order by AccountID,  LocationId, ServiceName"
                    End If
                End If


                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelReceipt.Update()
            End If
            txtInvoiceSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message.ToString
            'lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnClientSearch_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged

        lblAlert.Text = ""
        Try
            'txtAccountIdBilling.Text = ""

            If txtSearch.Text = "InvoiceFilter" Then
                txtSearchAccountID.Text = ""
                'txtSearchAccountID.Text = ""


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


                updPanelReceipt.Update()


                txtSearch.Text = "N"
                'mdlImportServices.Show()
                mdlPopupSearch.Show()

            End If


            If txtClientFrom.Text = "ImportClient" Then
                txtAccountIdII.Text = ""
                txtAccountIdII.Text = ""

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

                'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                '    ddlCompanyGrpII.Text = ""
                'Else
                '    ddlCompanyGrpII.Text = gvClient.SelectedRow.Cells(21).Text.Trim
                'End If


                If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                    ddlCompanyGrpII.Text = ""
                Else
                    ddlCompanyGrpII.Text = gvClient.SelectedRow.Cells(22).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    ddlContactTypeII.Text = ""
                Else
                    ddlContactTypeII.Text = gvClient.SelectedRow.Cells(1).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(2).Text = "&nbsp;") Then
                    txtAccountIdII.Text = ""
                Else
                    txtAccountIdII.Text = gvClient.SelectedRow.Cells(2).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(5).Text = "&nbsp;") Then
                    txtClientNameII.Text = ""
                Else
                    txtClientNameII.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(5).Text.Trim)
                End If


                If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                    txtLocationId.Text = ""
                Else
                    txtLocationId.Text = gvClient.SelectedRow.Cells(3).Text.Trim
                End If
                txtClientFrom.Text = "N"
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


                    If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                        txtCompanyGroup.Text = ""
                    Else
                        txtCompanyGroup.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(22).Text.Trim)
                    End If


                    If gvClient.SelectedRow.Cells(13).Text = "" Or gvClient.SelectedRow.Cells(13).Text = "&nbsp;" Then
                        txtSalesman.Text = ""
                    Else
                        gddlvalue = gvClient.SelectedRow.Cells(13).Text
                        txtSalesman.Text = gvClient.SelectedRow.Cells(13).Text
                    End If


                    If gvClient.SelectedRow.Cells(9).Text = "" Or gvClient.SelectedRow.Cells(9).Text = "&nbsp;" Then
                        txtBillAddress.Text = ""
                    Else
                        txtBillAddress.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(9).Text)
                    End If



                    If gvClient.SelectedRow.Cells(14).Text = "" Or gvClient.SelectedRow.Cells(14).Text = "&nbsp;" Then
                        txtBillStreet.Text = ""
                    Else
                        txtBillStreet.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(14).Text)
                    End If


                    If gvClient.SelectedRow.Cells(15).Text = "" Or gvClient.SelectedRow.Cells(15).Text = "&nbsp;" Then
                        txtBillBuilding.Text = ""
                    Else
                        txtBillBuilding.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(15).Text)
                    End If

                    If gvClient.SelectedRow.Cells(16).Text = "" Or gvClient.SelectedRow.Cells(16).Text = "&nbsp;" Then
                        txtBillCity.Text = ""
                    Else
                        txtBillCity.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(16).Text)
                    End If

                    If gvClient.SelectedRow.Cells(17).Text = "" Or gvClient.SelectedRow.Cells(17).Text = "&nbsp;" Then
                        txtBillState.Text = ""
                    Else
                        txtBillState.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(17).Text)
                    End If


                    If gvClient.SelectedRow.Cells(18).Text = "" Or gvClient.SelectedRow.Cells(18).Text = "&nbsp;" Then
                        txtBillCountry.Text = ""
                    Else
                        txtBillCountry.Text = gvClient.SelectedRow.Cells(18).Text
                    End If

                    If gvClient.SelectedRow.Cells(19).Text = "" Or gvClient.SelectedRow.Cells(19).Text = "&nbsp;" Then
                        txtBillPostal.Text = ""
                    Else
                        txtBillPostal.Text = gvClient.SelectedRow.Cells(19).Text
                    End If


                    If gvClient.SelectedRow.Cells(23).Text = "" Or gvClient.SelectedRow.Cells(23).Text = "&nbsp;" Then
                        txtLocation.Text = ""
                    Else
                        txtLocation.Text = gvClient.SelectedRow.Cells(23).Text
                    End If


                    ''

                    sqlDSOnHold.SelectCommand = "Select ContractNo, OnHoldDate, LastModifiedBy, ServiceAddress from tblContract where Status = 'H' and AccountId ='" & txtAccountIdBilling.Text & "'"

                    grdViewsqlDSOnHold.DataSourceID = "sqlDSOnHold"
                    grdViewsqlDSOnHold.DataBind()

                    If grdViewsqlDSOnHold.Rows.Count > 0 Then
                        lblOnHold.Visible = True
                        lblOnHold.Text = "The following Contracts are On-hold for this Account: "
                    Else
                        lblOnHold.Visible = False
                        lblOnHold.Text = ""
                    End If

                    updpnlBillingDetails.Update()
                    ''

                Else
                    txtAccountIdSearch.Text = ""
                    txtAccountIdSearch.Text = ""
                 

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


                    'txtSearch.Text = ""
                    updPnlSearch.Update()
                End If
            End If

            'If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '    txtAccountIdBilling.Text = ""
            'Else
            '    txtAccountIdBilling.Text = gvClient.SelectedRow.Cells(1).Text.Trim
            'End If



            'If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
            '    txtAccountName.Text = ""
            'Else
            '    txtAccountName.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(3).Text.Trim)
            'End If

            'If (gvClient.SelectedRow.Cells(9).Text = "&nbsp;") Then
            '    txtCompanyGroup.Text = ""
            'Else
            '    txtCompanyGroup.Text = gvClient.SelectedRow.Cells(9).Text.Trim
            'End If

            'If (gvClient.SelectedRow.Cells(23).Text = "&nbsp;") Then
            '    txtSalesman.Text = ""
            'Else
            '    txtSalesman.Text = gvClient.SelectedRow.Cells(23).Text.Trim
            'End If

        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message.ToString
            'lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "gvClient_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnGL_Click(sender As Object, e As EventArgs)
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
            'txtIsPopup.Text = "Location"
            'mdlImportServices.Show()
        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message.ToString
            'lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "GrdViewGL_SelectedIndexChanged", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnShowRecords_Click1(sender As Object, e As EventArgs)

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

    Protected Sub btnImportService_Click(sender As Object, e As EventArgs)

    End Sub

    'Protected Sub ProcessRecords()
    '    inProcess = True
    '    'For x As Integer = 1 To 5
    '    '    content &= "Beginning Processing Step " & x.ToString() & " at " & rowIndex.to & "..." & System.Environment.NewLine
    '    '    Thread.Sleep(1000)
    '    '    content &= "Completed Processing Step " & x.ToString() & " at " & Date.Now.ToLongTimeString() & System.Environment.NewLine & System.Environment.NewLine
    '    'Next x
    '    processComplete = True
    '    content += processCompleteMsg
    'End Sub

    'Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
    '    'If inProcess Then
    '    '    txtProgress.Text = content
    '    'End If

    '    'Dim msgLen As Integer = processCompleteMsg.Length
    '    'If processComplete AndAlso txtProgress.Text.Substring(txtProgress.Text.Length - processCompleteMsg.Length) = processCompleteMsg Then 'has final message been set?
    '    '    inProcess = False
    '    '    Timer1.Enabled = False
    '    '    Button1.Enabled = True
    '    'End If
    'End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        txtClientFrom.Text = ""
        mdlImportServices.Hide()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Try
            lblAlert1.Text = ""
            txtSearch.Text = ""
            'If String.IsNullOrEmpty(ddlContactTypeII.Text) Or ddlContactTypeII.Text = "--SELECT--" Then
            '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
            '    lblAlert1.Text = "SELECT CUSTOMER TYPE TO PROCEED"
            '    Exit Sub
            'End If

            txtClientFrom.Text = "ImportClient"
            If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountIdII.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                End If
              

                SqlDSClient.DataBind()
                gvClient.DataBind()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, B.CompanyID as ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD, A.Location From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, D.PersonID as ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD, C.Location From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "ImageButton1_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnShowRecordsII_Click(sender As Object, e As EventArgs) Handles btnShowRecordsII.Click
        Try
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim strsql As String

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand

            strsql = "SELECT a.rcno,  a.CompanyGroup, a.AccountId, a.Custname,  a.InvoiceNumber, a.SalesDate,  a.valuebase, a.gstbase, "
            strsql = strsql + " a.AppliedBase, a.ReceiptBase, a.CreditBase, a.BalanceBase,  a.Terms, a.BalanceBase as OSAmount, 'INV' as Doctype , 0.00, 0.00, 0.00   "
            strsql = strsql + " FROM tblsales a  "
            'strsql = strsql + " where a.PostStatus = 'P' and a.CompanyGroup ='" & ddlCompanyGrpII.Text & "'"
            strsql = strsql + " where a.PostStatus = 'P' "
            strsql = strsql + " and (a.BalanceBase) <> 0 and a.DocType in ('ARIN', 'ARCN', 'ARDN') "

            '''''''''''''

            If ddlCompanyGrpII.SelectedIndex > 0 Then
                strsql = strsql + " and a.CompanyGroup ='" & ddlCompanyGrpII.Text & "'"
            End If


            If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
                strsql = strsql + " and a.AccountId = '" & txtAccountIdII.Text & "' "
            End If

            If String.IsNullOrEmpty(txtClientNameII.Text.Trim) = False Then
                strsql = strsql + " and a.CustName = """ & txtClientNameII.Text & """"
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

            If String.IsNullOrEmpty(txtInvoiceNoII.Text.Trim) = False Then
                strsql = strsql + " and a.InvoiceNumber Like '%" + txtInvoiceNoII.Text.Trim.ToUpper + "%' "
            End If


            ''''''''''''''
            'strsql = strsql + " UNION "
            'strsql = strsql + " SELECT b.rcno, a.CompanyGroup, a.AccountId, a.ReceiptFrom as Custname, "
            'strsql = strsql + " a.ReceiptNumber as InvoiceNumber, a.ReceiptDate as SalesDate,  0.00, 0.00,  0.00, b.ValueBase , 0.00, b.BalanceBase ,  '',"
            'strsql = strsql + " b.BalanceBase  as OSAmount, 'RCT' as Doctype   "
            'strsql = strsql + " FROM tblrecv a, tblRecvdet b  "
            ''strsql = strsql + " where a.ReceiptNumber = b.ReceiptNumber and a.PostStatus = 'P' and a.CompanyGroup ='" & ddlCompanyGrpII.Text & "'"
            'strsql = strsql + " where a.ReceiptNumber = b.ReceiptNumber and a.PostStatus = 'P'"
            'strsql = strsql + " and (b.BalanceBase) < 0   "


            'strsql = strsql + " UNION "
            'strsql = strsql + " SELECT b.rcno, a.CompanyGroup, a.AccountId, a.ReceiptFrom as Custname, "
            'strsql = strsql + " a.ReceiptNumber as InvoiceNumber, a.ReceiptDate as SalesDate,  0.00, 0.00,  0.00, b.ValueBase , 0.00, b.BalanceBase ,  '',"
            'strsql = strsql + " b.BalanceBase  as OSAmount, 'RCT' as Doctype   "
            'strsql = strsql + " FROM tblrecv a, tblRecvdet b  "
            'strsql = strsql + " where a.ReceiptNumber = b.ReceiptNumber and a.PostStatus = 'P'"
            'strsql = strsql + " and (b.BalanceBase) < 0   "

            ''Added on 05.08.20
            'strsql = strsql + "and b.LedgerName like '%DEBTOR%'"
            ''Added on 05.08.20


            'strsql = strsql + " UNION "
            'strsql = strsql + " SELECT b.rcno, a.CompanyGroup, a.AccountId, a.ReceiptFrom as Custname, "
            'strsql = strsql + " a.ReceiptNumber as InvoiceNumber, a.ReceiptDate as SalesDate,  0.00, 0.00,  0.00, b.ValueBase , 0.00, (b.ValueBase - sum(ifnull(c.DebitBase,0)) + sum(ifnull(c.Creditbase,0))) * (-1) as BalanceBase,  '',"
            'strsql = strsql + " b.BalanceBase  as OSAmount, 'RCT' as Doctype,  b.ValueBase, c.DebitBase, c.Creditbase   "
            'strsql = strsql + " FROM tblrecv a inner join tbljrnvdet c on a.ReceiptNumber = c.RefType, tblRecvdet b  "

            'strsql = strsql + " where a.ReceiptNumber = b.ReceiptNumber and a.PostStatus = 'P'"
            'strsql = strsql + " and (b.BalanceBase) < 0   "
            'strsql = strsql + " and b.LedgerName like '%DEBTOR%'"
            'strsql = strsql + " group by a.ReceiptNumber"
            'strsql = strsql + " having  ((b.ValueBase)- sum(ifnull(c.DebitBase,0)) + sum(ifnull(c.Creditbase,0))) <> 0"

            strsql = strsql + " UNION"
            strsql = strsql + " SELECT a.rcno, a.CompanyGroup, a.AccountId, a.Custname,  a.InvoiceNumber, "
            strsql = strsql + " a.SalesDate as SalesDate,  0.00, 0.00,  0.00, a.ValueBase , 0.00, sum(a.BalanceBase),  '', a.BalanceBase  as OSAmount, "
            strsql = strsql + " 'RCT' as Doctype , 0.00, 0.00, 0.00 "
            strsql = strsql + " FROM vwwselectosreceipt a where 1=1  "

            If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
                strsql = strsql + " and a.AccountId = '" & txtAccountIdII.Text & "' "
            End If


            If String.IsNullOrEmpty(txtClientNameII.Text.Trim) = False Then
                strsql = strsql + " and a.Custname = """ & txtClientNameII.Text & """"
            End If


            strsql = strsql + " group by a.InvoiceNumber "
            strsql = strsql + " having sum(a.BalanceBase) <> 0"

            If ddlCompanyGrpII.SelectedIndex > 0 Then
                strsql = strsql + " and a.CompanyGroup ='" & ddlCompanyGrpII.Text & "'"
            End If


            'If String.IsNullOrEmpty(txtAccountIdII.Text.Trim) = False Then
            '    strsql = strsql + " and a.AccountId = '" & txtAccountIdII.Text & "' "
            'End If

            'If String.IsNullOrEmpty(txtClientNameII.Text.Trim) = False Then
            '    strsql = strsql + " and a.ReceiptFrom = """ & txtClientNameII.Text & """"
            'End If


            If String.IsNullOrEmpty(txtDateFromII.Text) = False Then
                strsql = strsql + " and   A.ReceiptDate >= '" & Convert.ToDateTime(txtDateFromII.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateToII.Text) = False Then
                strsql = strsql + " and   A.ReceiptDate <= '" & Convert.ToDateTime(txtDateToII.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtDateFromII.Text) = False And String.IsNullOrEmpty(txtDateToII.Text) = False Then
                strsql = strsql + " and   A.ReceiptDate between '" & Convert.ToDateTime(txtDateFromII.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateToII.Text).ToString("yyyy-MM-dd") & "'"
            End If

            If String.IsNullOrEmpty(txtInvoiceNoII.Text.Trim) = False Then
                strsql = strsql + " and a.ReceiptNumber Like '%" + txtInvoiceNoII.Text.Trim.ToUpper + "%' "
            End If


            strsql = strsql + " order by Salesdate, InvoiceNumber"

            cmdServiceBillingDetailItem.CommandText = strsql

            SqlDSOSInvoice.SelectCommand = strsql
            grvInvoiceRecDetails.DataSourceID = "SqlDSOSInvoice"
            grvInvoiceRecDetails.DataBind()

            conn.Close()
            conn.Dispose()
            btnImportInvoiceII.Enabled = True

            mdlImportServices.Show()

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
        Catch ex As Exception
            'Dim exstr As String
            'exstr = ex.Message.ToString
            'lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnShowRecordsII_Click", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub btnImportServiceII_Click(sender As Object, e As EventArgs)

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
                    GoTo insertRec
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


insertRec:
            If String.IsNullOrEmpty(txtAccountIdBilling.Text.Trim) = True Then
                txtCompanyGroup.Text = ddlCompanyGrpII.Text
                'ddlContactType.Text = ddlContactTypeII.Text
                txtAccountIdBilling.Text = txtAccountIdII.Text
                txtAccountName.Text = txtClientNameII.Text

                'ddlCompanyGrpII.Enabled = False
                ddlContactType.Enabled = False
                txtAccountIdII.Enabled = False
                'txtClientNameII.Enabled = False
                btnClient.Visible = False
            Else
              

            End If

            If ddlContactType.SelectedIndex = 0 Then
                'txtCompanyGroup.Text = ddlCompanyGrpII.Text
                ddlContactType.Text = ddlContactTypeII.Text
                'txtAccountIdBilling.Text = txtAccountIdII.Text
                'txtAccountName.Text = txtClientNameII.Text
            End If
            'System.Threading.Thread.Sleep(5000)

            'Start: Billing Details

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            If grvBillingDetails.Rows.Count = 0 Then
                For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
                    dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                    drCurrentRowLoc = dtScdrLoc.NewRow()
                    ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
                    grvBillingDetails.DataSource = dtScdrLoc
                    grvBillingDetails.DataBind()

                    SetPreviousDataBillingDetailsRecs()
                Next i

                FirstGridViewRowBillingDetailsRecs()
            End If
            'Start: From tblBillingDetailItem

            'txtReceiptAmt.Text = "0.00"

            Dim rowselected As Integer
            rowselected = 0


            rowselected = grvBillingDetails.Rows.Count - 1 '27.11.17

            For rowIndex As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGVII"), CheckBox)

                If (TextBoxchkSelect.Checked = True) Then



                    Dim lblid24 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGVII"), TextBox)
                    Dim TotalOSAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtOSAmountGVII"), TextBox)

                    txtReceiptAmt.Text = (Convert.ToDecimal(txtReceiptAmt.Text) + Convert.ToDecimal(TotalOSAmount.Text)).ToString("N2")
                    '''' Start:Check for Duplicate  Record


                    For j As Integer = 0 To grvBillingDetails.Rows.Count - 1
                        Dim TextBoxServiceRecordBilling As TextBox = CType(grvBillingDetails.Rows(j).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                        If TextBoxServiceRecordBilling.Text.Trim = lblid24.Text.Trim Then
                            GoTo nextrec
                        End If
                    Next

                    '''' End:Check for Duplicate  Record

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
                    Dim InvoiceAmmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAppliedBaseGVII"), TextBox)
                    Dim TotalReceiptAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalReceiptAmountGVII"), TextBox)
                    Dim TotalCNAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalCNAmountGVII"), TextBox)

                    Dim DocType As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIDGVII"), TextBox)
                    Dim TextBoxSourceRcno As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoGVII"), TextBox)

                    Dim AccountID1 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGVII"), TextBox)
                    Dim ClientName1 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtClientNameGVII"), TextBox)

                    'Dim SourceRcno As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoGVII"), TextBox)
                    'Dim DocType As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtDocTypeGVII"), TextBox)

                    'Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)
                    'Dim InvoiceNumber As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNumberGV"), TextBox)


                    Dim TextBoxItemTypeGV As DropDownList = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                    TextBoxItemTypeGV.Text = "ARIN"

                    Dim TextBoxtxtInvoiceNoGV As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                    TextBoxtxtInvoiceNoGV.Text = Convert.ToString(InvoiceNumber.Text)

                    Dim TextBoxInvoiceDate As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtInvoiceDateGV"), TextBox)
                    TextBoxInvoiceDate.Text = Convert.ToDateTime(InvoiceDate.Text).ToString("dd/MM/yyyy")

                    'Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
                    'TextBoxTotalPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ValueBase"))

                    Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                    TextBoxTotalPriceWithGST.Text = (Convert.ToDecimal(InvoiceAmmount.Text)).ToString("N2")

                    'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
                    'TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTBase"))


                    Dim TextBoxTotalTotalReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalReceiptAmtGV"), TextBox)
                    TextBoxTotalTotalReceiptAmt.Text = (Convert.ToDecimal(TotalReceiptAmount.Text)).ToString("N2")

                    Dim TextBoxTotalCNAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtTotalCreditNoteAmtGV"), TextBox)
                    TextBoxTotalCNAmt.Text = (Convert.ToDecimal(TotalCNAmount.Text)).ToString("N2")

                    'Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                    'TextBoxRcnoInvoice.Text = Convert.ToString(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Rcno")))



                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                    TextBoxOtherCode.Text = txtARCode.Text

                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                    TextBoxGLDescription.Text = txtARDescription.Text

                    Dim TextBoxReceiptAmt As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtReceiptAmtGV"), TextBox)
                    TextBoxReceiptAmt.Text = TotalOSAmount.Text

                    Dim TextBoxDocType As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtLocationGV"), TextBox)
                    TextBoxDocType.Text = DocType.Text

                    Dim TextBoxSourceRcno1 As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtSourceRcnoGV"), TextBox)
                    TextBoxSourceRcno1.Text = TextBoxSourceRcno.Text

                    Dim TextBoxAccountID As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                    TextBoxAccountID.Text = AccountID1.Text

                    Dim TextBoxClientName As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtCustomerNameGV"), TextBox)
                    TextBoxClientName.Text = ClientName1.Text

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
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.ToString

                'Button1.Enabled = False
                'Timer1.Enabled = True
                'Thread.Sleep(1000)
                'Dim workerThread As New Thread(New ThreadStart(AddressOf ProcessRecords))
                'workerThread.Start()


                'textbox6.Text = rowselected.ToString + " / " + rowIndex.ToString
                'txtProgress.Text = rowselected.ToString + " / " + rowIndex.ToString

nextrec:
            Next


            calculateTotalReceipt()
            btnSave.Enabled = True
            updpnlBillingDetails.Update()
            updPanelSave.Update()
            updPnlBillingRecs.Update()


        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnImportInvoiceII_Click", ex.Message.ToString, txtAccountIdBilling.Text)
            Exit Sub
        End Try
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Session.Add("ReceiptNumber", txtReceiptNo.Text)
      
        If txtPostStatus.Text = "O" Then
           
            temp.Visible = True
        Else
           
            temp.Visible = False

        End If
        mdlConfirmMultiPrint.Show()
    End Sub

    Protected Sub btnConfirmYes_Click(sender As Object, e As EventArgs) Handles btnConfirmYes.Click
        Try

            IsSuccess = PostReceipt()

            If IsSuccess = True Then

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()


                ' ''''''''''''''''
                'SQLDSReceipt.SelectCommand = txt.Text
                'SQLDSReceipt.DataBind()
                'GridView1.DataBind()

                'updPnlSearch.Update()
                'updPnlMsg.Update()
                'updpnlBillingDetails.Update()
                ''updpnlServiceRecs.Update()
                'updpnlBillingDetails.Update()


                ' ''''''''''''''''''


                btnQuickSearch_Click(sender, e)
                lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
                btnReverse.Enabled = True
                btnReverse.ForeColor = System.Drawing.Color.Black


                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray

                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray

                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray

                'InsertNewLog()

                'If txtAutoEmailReceipt.Text = "True" Then
                '    mdlConfirmMultiPrint.Show()
                'Else
                '    mdlConfirmMultiPrint.Show()
                'End If
               
            End If
            
            'Dim conn As MySqlConnection = New MySqlConnection()

            'Dim rowselected As Integer

            ''Dim confirmValue As String
            ''confirmValue = ""


            ''confirmValue = Request.Form("confirm_value")
            ''If Right(confirmValue, 3) = "Yes" Then
            ' ''''''''''''''' Insert tblAR

            ' ''''''''''''''''''''
            ''PopulateArCode()

            ' '''''''''''''''''''''''''
            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'conn.Open()


            ' '''''''''''''''''''
            'Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            'commandUpdateInvoice.CommandType = CommandType.Text
            'Dim sqlUpdateInvoice As String = "Update tblrecv set PostStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            'commandUpdateInvoice.CommandText = sqlUpdateInvoice
            'commandUpdateInvoice.Parameters.Clear()
            'commandUpdateInvoice.Connection = conn
            'commandUpdateInvoice.ExecuteNonQuery()

            ' '''''''''''''''''''''

            'Dim command5 As MySqlCommand = New MySqlCommand
            'command5.CommandType = CommandType.Text

            'Dim qry5 As String = "DELETE from tblAR where VoucherNumber = '" & txtReceiptNo.Text & "'"

            'command5.CommandText = qry5
            ''command1.Parameters.Clear()
            'command5.Connection = conn
            'command5.ExecuteNonQuery()

            'Dim qryAR As String
            'Dim commandAR As MySqlCommand = New MySqlCommand
            'commandAR.CommandType = CommandType.Text

            'If Convert.ToDecimal(txtReceiptAmt.Text) > 0.0 Then
            '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, BillingPeriod, "
            '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @BillingPeriod,"
            '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    commandAR.CommandText = qryAR
            '    commandAR.Parameters.Clear()
            '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    If txtReceiptDate.Text.Trim = "" Then
            '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    Else
            '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    End If
            '    commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
            '    commandAR.Parameters.AddWithValue("@ContractNo", "")
            '    commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
            '    commandAR.Parameters.AddWithValue("@GLCode", txtBankGLCode.Text)
            '    commandAR.Parameters.AddWithValue("@GLDescription", txtBankName.Text)
            '    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtReceivedAmount.Text))
            '    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
            '    commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
            '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    commandAR.Connection = conn
            '    commandAR.ExecuteNonQuery()
            'End If


            ''Start:Loop thru' Credit values

            'Dim commandValues As MySqlCommand = New MySqlCommand

            'commandValues.CommandType = CommandType.Text
            'commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
            'commandValues.Connection = conn

            'Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            'Dim dtValues As New DataTable
            'dtValues.Load(drValues)


            'Dim lTotalReceiptAmt As Decimal
            'Dim lInvoiceAmt As Decimal
            'Dim lReceptAmtAdjusted As Decimal

            'lTotalReceiptAmt = 0.0
            'lInvoiceAmt = 0.0

            'lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            'Dim rowindex = 0


            'For Each row As DataRow In dtValues.Rows



            '    qryAR = "INSERT INTO tblAR(VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, ServiceRecordNo, ServiceDate, BillingPeriod, Salesman, InvoiceType, GLType,  "
            '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @ServiceRecordNo, @ServiceDate,  @BillingPeriod, @Salesman, @InvoiceType, @GLType,"
            '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    commandAR.CommandText = qryAR
            '    commandAR.Parameters.Clear()
            '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    If txtReceiptDate.Text.Trim = "" Then
            '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    Else
            '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    End If
            '    commandAR.Parameters.AddWithValue("@BillingPeriod", txtReceiptPeriod.Text)
            '    commandAR.Parameters.AddWithValue("@ContractNo", row("ContractNo"))
            '    commandAR.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))
            '    commandAR.Parameters.AddWithValue("@GLCode", row("LedgerCode"))
            '    commandAR.Parameters.AddWithValue("@GLDescription", row("LedgerName"))
            '    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)

            '    'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("ReceiptValue")))
            '    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(dtValues.Rows(rowindex)("ReceiptValue").ToString))

            '    commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
            '    commandAR.Parameters.AddWithValue("@GLType", "Debtor")
            '    commandAR.Parameters.AddWithValue("@ServiceRecordNo", row("ServiceRecordNo"))

            '    commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '    'Else
            '    'commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lServiceDate).ToString("yyyy-MM-dd"))
            '    'End If


            '    commandAR.Parameters.AddWithValue("@Salesman", txtSalesman.Text)
            '    commandAR.Parameters.AddWithValue("@InvoiceType", row("InvoiceType"))

            '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    commandAR.Connection = conn
            '    commandAR.ExecuteNonQuery()


            '    ''Start: Update tblSales

            '    ' ''''''''''''''''''''

            '    If String.IsNullOrEmpty(row("InvoiceNo")) = False Then
            '        UpdateTblSales(row("InvoiceNo"))


            '    End If


            '    ''''''''''''''''''''''''' Sales Commission

            '    ''''''''''' Sales Commission

            '    If String.IsNullOrEmpty(row("ContractNo")) = False Then
            '        Dim sqlCommissionPct As String
            '        sqlCommissionPct = ""

            '        sqlCommissionPct = "Select SalesCommissionPerc, TechCommissionPerc from tblContract where Contractno = '" & row("ContractNo") & "'"

            '        Dim commandCommissionPct As MySqlCommand = New MySqlCommand
            '        commandCommissionPct.CommandType = CommandType.Text
            '        commandCommissionPct.CommandText = sqlCommissionPct
            '        commandCommissionPct.Connection = conn

            '        Dim drCommissionPct As MySqlDataReader = commandCommissionPct.ExecuteReader()

            '        Dim dtCommissionPct As New DataTable
            '        dtCommissionPct.Load(drCommissionPct)

            '        If Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString) > 0 Then

            '            ''''''''''' Sales Commission

            '            Dim qrySalesCommission As String

            '            'Dim qryAR As String
            '            Dim commandSalesCommission As MySqlCommand = New MySqlCommand
            '            commandSalesCommission.CommandType = CommandType.Text

            '            qrySalesCommission = "INSERT INTO tblsalesCommission( CompanyGroup, AccountId, CustomerName, SalesDate,  InvoiceNumber,  DatePaid, ReceiptNumber, Terms, Salesman, ValueBase, ValueOriginal,   SalesCommissionPerc, SalesCommissionAmt, TechCommissionPerc, TechCommissionAmt, ServiceGroup, ContractGroup, ContractNo, GLPeriod, ItemCode, TechnicianId, TechnicianName, InvoiceAmount, PaymentType, "
            '            qrySalesCommission = qrySalesCommission + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '            qrySalesCommission = qrySalesCommission + " ( @CompanyGroup, @AccountId, @CustomerName, @SalesDate,  @InvoiceNumber,  @DatePaid, @ReceiptNumber, @Terms, @Salesman, @ValueBase, @ValueOriginal,  @SalesCommissionPerc, @SalesCommissionAmt, @TechCommissionPerc, @TechCommissionAmt,  @ServiceGroup, @ContractGroup, @ContractNo, @GLPeriod, @ItemCode, @TechnicianId, @TechnicianName, @InvoiceAmount, @PaymentType, "
            '            qrySalesCommission = qrySalesCommission + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '            commandSalesCommission.CommandText = qrySalesCommission
            '            commandSalesCommission.Parameters.Clear()

            '            commandSalesCommission.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '            commandSalesCommission.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '            commandSalesCommission.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)

            '            If row("InvoiceDate").ToString = "" Then
            '                commandSalesCommission.Parameters.AddWithValue("@SalesDate", DBNull.Value)
            '            Else
            '                commandSalesCommission.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(row("InvoiceDate")).ToString("yyyy-MM-dd"))
            '            End If
            '            commandSalesCommission.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))

            '            If txtReceiptDate.Text.Trim = "" Then
            '                commandSalesCommission.Parameters.AddWithValue("@DatePaid", DBNull.Value)
            '            Else
            '                commandSalesCommission.Parameters.AddWithValue("@DatePaid", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '            End If
            '            commandSalesCommission.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

            '            commandSalesCommission.Parameters.AddWithValue("@Terms", row("Terms"))
            '            commandSalesCommission.Parameters.AddWithValue("@Salesman", txtSalesman.Text)

            '            commandSalesCommission.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)
            '            commandSalesCommission.Parameters.AddWithValue("@ContractNo", row("ContractNo"))

            '            commandSalesCommission.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(row("ReceiptValue")))
            '            commandSalesCommission.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(row("ReceiptValue")))
            '            commandSalesCommission.Parameters.AddWithValue("@SalesCommissionPerc", Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString))
            '            commandSalesCommission.Parameters.AddWithValue("@SalesCommissionAmt", Convert.ToDecimal(dtCommissionPct.Rows(0)("SalesCommissionPerc").ToString) * 0.01 * Convert.ToDecimal(row("ReceiptValue")))

            '            commandSalesCommission.Parameters.AddWithValue("@TechCommissionPerc", 0.0)
            '            commandSalesCommission.Parameters.AddWithValue("@TechCommissionAmt", 0.0)

            '            commandSalesCommission.Parameters.AddWithValue("@ServiceGroup", row("InvoiceType"))
            '            commandSalesCommission.Parameters.AddWithValue("@ContractGroup", row("ContractGroup"))
            '            commandSalesCommission.Parameters.AddWithValue("@ItemCode", row("ItemCode"))

            '            commandSalesCommission.Parameters.AddWithValue("@TechnicianId", "")
            '            commandSalesCommission.Parameters.AddWithValue("@TechnicianName", "")
            '            commandSalesCommission.Parameters.AddWithValue("@InvoiceAmount", row("InvoiceValue"))

            '            If ddlPaymentMode.Text = "--SELECT--" Then
            '                commandSalesCommission.Parameters.AddWithValue("@PaymentType", "")
            '            Else
            '                commandSalesCommission.Parameters.AddWithValue("@PaymentType", ddlPaymentMode.Text)
            '            End If


            '            commandSalesCommission.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '            commandSalesCommission.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '            commandSalesCommission.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '            commandSalesCommission.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '            commandSalesCommission.Connection = conn
            '            commandSalesCommission.ExecuteNonQuery()
            '        End If





            '        '''''''''''''''''''''''''' Sales Commission


            '        ''''''''''''''''''''''''' Technician Commission

            '        If String.IsNullOrEmpty(row("ServiceRecordNo")) = False Then
            '            If Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString) > 0 Then


            '                Dim sqlTechnician As String
            '                sqlTechnician = ""

            '                sqlTechnician = "Select StaffId, StaffName from tblServiceRecordStaff where RecordNo = '" & row("ServiceRecordNo") & "'"

            '                Dim commandTechnician As MySqlCommand = New MySqlCommand
            '                commandTechnician.CommandType = CommandType.Text
            '                commandTechnician.CommandText = sqlTechnician
            '                commandTechnician.Connection = conn

            '                Dim drTechnician As MySqlDataReader = commandTechnician.ExecuteReader()

            '                Dim dtTechnician As New DataTable
            '                dtTechnician.Load(drTechnician)

            '                For Each row1 As DataRow In dtTechnician.Rows


            '                    If String.IsNullOrEmpty(dtTechnician.Rows(0)("StaffId").ToString) = False Then

            '                        Dim qrySalesCommission As String
            '                        Dim commandSalesCommission As MySqlCommand = New MySqlCommand
            '                        commandSalesCommission.CommandType = CommandType.Text

            '                        qrySalesCommission = "INSERT INTO tblsalesCommission( CompanyGroup, AccountId, CustomerName, SalesDate,  InvoiceNumber,  DatePaid, ReceiptNumber, Terms, Salesman, ValueBase, ValueOriginal,   SalesCommissionPerc, SalesCommissionAmt, TechCommissionPerc, TechCommissionAmt, ServiceGroup, ContractGroup, ContractNo, GLPeriod, ItemCode, TechnicianId, TechnicianName,  "
            '                        qrySalesCommission = qrySalesCommission + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '                        qrySalesCommission = qrySalesCommission + " ( @CompanyGroup, @AccountId, @CustomerName, @SalesDate,  @InvoiceNumber,  @DatePaid, @ReceiptNumber, @Terms, @Salesman, @ValueBase, @ValueOriginal,  @SalesCommissionPerc, @SalesCommissionAmt, @TechCommissionPerc, @TechCommissionAmt,  @ServiceGroup, @ContractGroup, @ContractNo, @GLPeriod, @ItemCode, @TechnicianId, @TechnicianName, "
            '                        qrySalesCommission = qrySalesCommission + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '                        commandSalesCommission.CommandText = qrySalesCommission
            '                        commandSalesCommission.Parameters.Clear()

            '                        commandSalesCommission.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '                        commandSalesCommission.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '                        commandSalesCommission.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)

            '                        If row("InvoiceDate").ToString = "" Then
            '                            commandSalesCommission.Parameters.AddWithValue("@SalesDate", DBNull.Value)
            '                        Else
            '                            commandSalesCommission.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(row("InvoiceDate")).ToString("yyyy-MM-dd"))
            '                        End If
            '                        commandSalesCommission.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))

            '                        If txtReceiptDate.Text.Trim = "" Then
            '                            commandSalesCommission.Parameters.AddWithValue("@DatePaid", DBNull.Value)
            '                        Else
            '                            commandSalesCommission.Parameters.AddWithValue("@DatePaid", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '                        End If
            '                        commandSalesCommission.Parameters.AddWithValue("@ReceiptNumber", txtReceiptNo.Text)

            '                        commandSalesCommission.Parameters.AddWithValue("@Terms", row("Terms"))
            '                        commandSalesCommission.Parameters.AddWithValue("@Salesman", "")

            '                        commandSalesCommission.Parameters.AddWithValue("@GLPeriod", txtReceiptPeriod.Text)
            '                        commandSalesCommission.Parameters.AddWithValue("@ContractNo", row("ContractNo"))

            '                        commandSalesCommission.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(row("ReceiptValue")))
            '                        commandSalesCommission.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(row("ReceiptValue")))
            '                        commandSalesCommission.Parameters.AddWithValue("@SalesCommissionPerc", 0.0)
            '                        commandSalesCommission.Parameters.AddWithValue("@SalesCommissionAmt", 0.0)

            '                        commandSalesCommission.Parameters.AddWithValue("@TechCommissionPerc", Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString))
            '                        commandSalesCommission.Parameters.AddWithValue("@TechCommissionAmt", Convert.ToDecimal(dtCommissionPct.Rows(0)("TechCommissionPerc").ToString) * 0.01 * Convert.ToDecimal(row("ReceiptValue")))

            '                        commandSalesCommission.Parameters.AddWithValue("@ServiceGroup", row("InvoiceType"))
            '                        commandSalesCommission.Parameters.AddWithValue("@ContractGroup", txtCompanyGroup.Text)
            '                        commandSalesCommission.Parameters.AddWithValue("@ItemCode", row("ItemCode"))

            '                        commandSalesCommission.Parameters.AddWithValue("@TechnicianId", row1("StaffId").ToString)
            '                        commandSalesCommission.Parameters.AddWithValue("@TechnicianName", row1("StaffName").ToString)

            '                        commandSalesCommission.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '                        commandSalesCommission.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '                        commandSalesCommission.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '                        commandSalesCommission.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '                        commandSalesCommission.Connection = conn
            '                        commandSalesCommission.ExecuteNonQuery()
            '                    End If
            '                Next
            '            End If
            '        End If

            '    End If


            '    ''''''''''''''''''''''' Technician Commission
            '    rowindex = rowindex + 1
            'Next row



            ''Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            ''commandUpdateInvoice.CommandType = CommandType.Text
            ''Dim sqlUpdateInvoice As String = "Update tblrecv set GLStatus = 'P'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            ''commandUpdateInvoice.CommandText = sqlUpdateInvoice
            ''commandUpdateInvoice.Parameters.Clear()
            ''commandUpdateInvoice.Connection = conn
            ''commandUpdateInvoice.ExecuteNonQuery()

            'GridView1.DataBind()


            'lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'lblAlert.Text = ""
            'updPnlSearch.Update()
            'updPnlMsg.Update()
            'updpnlBillingDetails.Update()
            ''updpnlServiceRecs.Update()
            'updpnlBillingDetails.Update()

            'btnQuickSearch_Click(sender, e)

            'btnChangeStatus.Enabled = True
            'btnChangeStatus.ForeColor = System.Drawing.Color.Black

            'btnEdit.Enabled = False
            'btnEdit.ForeColor = System.Drawing.Color.Gray

            'btnDelete.Enabled = False
            'btnDelete.ForeColor = System.Drawing.Color.Gray

            'btnPost.Enabled = False
            'btnPost.ForeColor = System.Drawing.Color.Gray

            ''End If


            ''End: Loop thru' Credit Values


            ''''''''''''''' Insert tblAR
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
        End Try

    End Sub

    Protected Sub btnConfirmYesReverse_Click(sender As Object, e As EventArgs) Handles btnConfirmYesReverse.Click

        IsSuccess = ReverseReceipt()

        If IsSuccess = True Then

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

            lblAlert.Text = ""
            SQLDSReceipt.SelectCommand = txt.Text
            SQLDSReceipt.DataBind()
            GridView1.DataBind()

            updPnlSearch.Update()
            updPnlMsg.Update()
            updpnlBillingDetails.Update()
            'updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()

            btnQuickSearch_Click(sender, e)

            'btnQuickSearch_Click(sender, e)
            lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
            btnReverse.Enabled = False
            btnReverse.ForeColor = System.Drawing.Color.Gray

            btnChangeStatus.Enabled = True
            btnChangeStatus.ForeColor = System.Drawing.Color.Black

            btnEdit.Enabled = True
            btnEdit.ForeColor = System.Drawing.Color.Black

            btnDelete.Enabled = True
            btnDelete.ForeColor = System.Drawing.Color.Black

            btnPost.Enabled = True
            btnPost.ForeColor = System.Drawing.Color.Black

            'InsertNewLog()
        End If
    End Sub

    Protected Sub btnConfirmYesSavePost_Click(sender As Object, e As EventArgs) Handles btnConfirmYesSavePost.Click
        Try
            IsSuccess = PostReceipt()

            If IsSuccess = True Then

                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)

                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                'updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()

                btnQuickSearch_Click(sender, e)
                lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
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
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnConfirmYesSavePost_Click", ex.Message.ToString, "")
        End Try
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

            If txtDisplayRecordsLocationwise.Text = "N" Then
                e.Row.Cells(19).Visible = False
                GridView1.HeaderRow.Cells(19).Visible = False
            ElseIf txtDisplayRecordsLocationwise.Text = "Y" Then
                e.Row.Cells(19).Visible = True
                GridView1.HeaderRow.Cells(19).Visible = True
            End If
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

    Protected Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            lblAlert.Text = ""
            lblMessage.Text = ""

            lblEvent.Text = "Confirm REVERSE"
            lblQuery.Text = "Are you sure to REVERSE the Receipt?"

            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            txtEvent.Text = "REVERSE"

            mdlPopupConfirmReverse.Show()


            '    'Dim confirmValue As String
            '    'confirmValue = ""


            '    'confirmValue = Request.Form("confirm_value")
            '    'If Right(confirmValue, 3) = "Yes" Then
            '    ''''''''''''''' Insert tblAR

            '    ''''''''''''''''''''
            '    'PopulateArCode()

            '    '''''''''''''''''''''''''

            '    Dim conn As MySqlConnection = New MySqlConnection()

            '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            '    conn.Open()

            '    Dim command5 As MySqlCommand = New MySqlCommand
            '    command5.CommandType = CommandType.Text

            '    Dim qry5 As String = "DELETE from tblAR where BatchNo = '" & txtReceiptNo.Text & "'"

            '    command5.CommandText = qry5
            '    'command1.Parameters.Clear()
            '    command5.Connection = conn
            '    command5.ExecuteNonQuery()

            '    'Dim qryAR As String
            '    'Dim commandAR As MySqlCommand = New MySqlCommand
            '    'commandAR.CommandType = CommandType.Text

            '    'If Convert.ToDecimal(txtReceiptAmt.Text) > 0.0 Then
            '    '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName,  "
            '    '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName,"
            '    '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    '    commandAR.CommandText = qryAR
            '    '    commandAR.Parameters.Clear()
            '    '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    '    If txtReceiptDate.Text.Trim = "" Then
            '    '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    '    Else
            '    '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    '    End If
            '    '    commandAR.Parameters.AddWithValue("@ContractNo", "")
            '    '    commandAR.Parameters.AddWithValue("@InvoiceNumber", "")
            '    '    commandAR.Parameters.AddWithValue("@GLCode", txtBankGLCode.Text)
            '    '    commandAR.Parameters.AddWithValue("@GLDescription", txtBankName.Text)
            '    '    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
            '    '    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtReceiptAmt.Text))
            '    '    commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    '    commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")
            '    '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    '    commandAR.Connection = conn
            '    '    commandAR.ExecuteNonQuery()
            '    'End If


            '    ''Start:Loop thru' Credit values

            '    'Dim commandValues As MySqlCommand = New MySqlCommand

            '    'commandValues.CommandType = CommandType.Text
            '    'commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
            '    'commandValues.Connection = conn

            '    'Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            '    'Dim dtValues As New DataTable
            '    'dtValues.Load(drValues)


            '    'Dim lTotalReceiptAmt As Decimal
            '    'Dim lInvoiceAmt As Decimal
            '    'Dim lReceptAmtAdjusted As Decimal

            '    'lTotalReceiptAmt = 0.0
            '    'lInvoiceAmt = 0.0

            '    'lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            '    'Dim rowindex = 0


            '    'For Each row As DataRow In dtValues.Rows

            '    '    Dim lContractNo As String
            '    '    Dim lServiceRecordNo As String
            '    '    Dim lServiceDate As String

            '    '    If Convert.ToDecimal(row("ReceiptValue")) > 0.0 Then

            '    '        ''''''''''''''''''''

            '    '        Dim commandUpdateInvoiceValue As MySqlCommand = New MySqlCommand

            '    '        commandUpdateInvoiceValue.CommandType = CommandType.Text
            '    '        commandUpdateInvoiceValue.CommandText = "SELECT *  FROM tblservicebillingdetailitem where InvoiceNo ='" & row("InvoiceNo") & "' order by ServiceDate"
            '    '        commandUpdateInvoiceValue.Connection = conn

            '    '        Dim drInvoiceValue As MySqlDataReader = commandUpdateInvoiceValue.ExecuteReader()
            '    '        Dim dtInvoiceValue As New DataTable
            '    '        dtInvoiceValue.Load(drInvoiceValue)


            '    '        lContractNo = ""
            '    '        lServiceRecordNo = ""
            '    '        lServiceDate = ""

            '    '        For Each row1 As DataRow In dtInvoiceValue.Rows

            '    '            '''''''''''''''''''''''''''
            '    '            lContractNo = row1("ContractNo")
            '    '            lServiceRecordNo = row1("ServiceRecordNo")
            '    '            lServiceDate = row1("ServiceDate")

            '    '            lInvoiceAmt = row1("TotalPriceWithGST")

            '    '            If lTotalReceiptAmt = lInvoiceAmt Then
            '    '                lReceptAmtAdjusted = lInvoiceAmt
            '    '            ElseIf lTotalReceiptAmt > lInvoiceAmt Then
            '    '                lReceptAmtAdjusted = lInvoiceAmt
            '    '            ElseIf lTotalReceiptAmt < lInvoiceAmt Then
            '    '                lReceptAmtAdjusted = lInvoiceAmt - lTotalReceiptAmt
            '    '            End If

            '    '            lTotalReceiptAmt = lTotalReceiptAmt - lReceptAmtAdjusted

            '    '            'Dim commandUpdateInvoiceValue1 As MySqlCommand = New MySqlCommand
            '    '            'commandUpdateInvoiceValue1.CommandType = CommandType.Text
            '    '            'Dim sqlUpdateInvoiceValue1 As String = "Update tblservicebillingdetailitem set ReceiptAmount = " & Convert.ToDecimal(lReceptAmtAdjusted) & " where Rcno = " & row1("Rcno")

            '    '            'commandUpdateInvoiceValue1.CommandText = sqlUpdateInvoiceValue1
            '    '            'commandUpdateInvoiceValue1.Parameters.Clear()
            '    '            'commandUpdateInvoiceValue1.Connection = conn
            '    '            'commandUpdateInvoiceValue1.ExecuteNonQuery()

            '    '            ''''''''''''''''''''''''''''''

            '    '            ''''''''''''''''''''''''''''''

            '    '            'qryAR = "INSERT INTO tblAR(VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, ServiceRecordNo, ServiceDate,  "
            '    '            'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    '            'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @ServiceRecordNo, @ServiceDate, "
            '    '            'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    '            'commandAR.CommandText = qryAR
            '    '            'commandAR.Parameters.Clear()
            '    '            'commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    '            'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    '            'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    '            'If txtReceiptDate.Text.Trim = "" Then
            '    '            '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    '            'Else
            '    '            '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    '            'End If

            '    '            'commandAR.Parameters.AddWithValue("@ContractNo", row1("ContractNo"))
            '    '            'commandAR.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))
            '    '            'commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
            '    '            'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
            '    '            'commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)

            '    '            ''commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("ReceiptValue")))
            '    '            'commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(dtValues.Rows(rowindex)("ReceiptValue").ToString))

            '    '            'commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    '            'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    '            'commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")

            '    '            'commandAR.Parameters.AddWithValue("@ServiceRecordNo", row1("ServiceRecordNo"))


            '    '            ''commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '    '            ''Else
            '    '            'commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(row1("ServiceDate")).ToString("yyyy-MM-dd"))
            '    '            ''End If

            '    '            'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    '            ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '            'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '            'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    '            ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '            'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    '            'commandAR.Connection = conn
            '    '            'commandAR.ExecuteNonQuery()

            '    '        Next row1
            '    '    End If


            '    '    'qryAR = "INSERT INTO tblAR(VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, ServiceRecordNo, ServiceDate,  "
            '    '    'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
            '    '    'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, @ServiceRecordNo, @ServiceDate, "
            '    '    'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

            '    '    'commandAR.CommandText = qryAR
            '    '    'commandAR.Parameters.Clear()
            '    '    'commandAR.Parameters.AddWithValue("@VoucherNumber", txtReceiptNo.Text)
            '    '    'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
            '    '    'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
            '    '    'If txtReceiptDate.Text.Trim = "" Then
            '    '    '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
            '    '    'Else
            '    '    '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtReceiptDate.Text).ToString("yyyy-MM-dd"))
            '    '    'End If

            '    '    'commandAR.Parameters.AddWithValue("@ContractNo", lContractNo)
            '    '    'commandAR.Parameters.AddWithValue("@InvoiceNumber", row("InvoiceNo"))
            '    '    'commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
            '    '    'commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
            '    '    'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(dtValues.Rows(rowindex)("ReceiptValue").ToString))

            '    '    ''commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(row("ReceiptValue")))
            '    '    'commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)

            '    '    'commandAR.Parameters.AddWithValue("@BatchNo", txtReceiptNo.Text)
            '    '    'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
            '    '    'commandAR.Parameters.AddWithValue("@ModuleName", "Receipt")

            '    '    'commandAR.Parameters.AddWithValue("@ServiceRecordNo", lServiceRecordNo)


            '    '    ''commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
            '    '    ''Else
            '    '    'commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lServiceDate).ToString("yyyy-MM-dd"))
            '    '    ''End If

            '    '    'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
            '    '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
            '    '    ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
            '    '    'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            '    '    'commandAR.Connection = conn
            '    '    'commandAR.ExecuteNonQuery()
            '    '    rowindex = rowindex + 1
            '    'Next row



            '    Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            '    commandUpdateInvoice.CommandType = CommandType.Text
            '    Dim sqlUpdateInvoice As String = "Update tblrecv set PostStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

            '    commandUpdateInvoice.CommandText = sqlUpdateInvoice
            '    commandUpdateInvoice.Parameters.Clear()
            '    commandUpdateInvoice.Connection = conn
            '    commandUpdateInvoice.ExecuteNonQuery()


            '    ''''''''''''''''''''''''''''''''

            '    'Start:Loop thru' Credit values

            '    Dim commandValues As MySqlCommand = New MySqlCommand

            '    commandValues.CommandType = CommandType.Text
            '    commandValues.CommandText = "SELECT *  FROM tblrecvdet where ReceiptNumber ='" & txtReceiptNo.Text.Trim & "'"
            '    commandValues.Connection = conn

            '    Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            '    Dim dtValues As New DataTable
            '    dtValues.Load(drValues)


            '    'Dim lTotalReceiptAmt As Decimal
            '    'Dim lInvoiceAmt As Decimal
            '    'Dim lReceptAmtAdjusted As Decimal

            '    'lTotalReceiptAmt = 0.0
            '    'lInvoiceAmt = 0.0

            '    'lTotalReceiptAmt = dtValues.Rows(0)("ReceiptValue").ToString
            '    'Dim rowindex = 0


            '    For Each row As DataRow In dtValues.Rows

            '        ''Start: Update tblSales

            '        ' ''''''''''''''''''''

            '        If String.IsNullOrEmpty(row("InvoiceNo")) = False Then
            '            UpdateTblSales(row("InvoiceNo"))


            '        End If
            '    Next


            '    '''''''''''''''''''''''''''''''''
            '    'GridView1.DataBind()


            '    lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "RECEIPT", txtReceiptNo.Text, "REVERSE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            '    lblAlert.Text = ""
            '    updPnlSearch.Update()
            '    updPnlMsg.Update()
            '    updpnlBillingDetails.Update()
            '    'updpnlServiceRecs.Update()
            '    updpnlBillingDetails.Update()

            '    btnQuickSearch_Click(sender, e)

            '    btnChangeStatus.Enabled = False
            '    btnChangeStatus.ForeColor = System.Drawing.Color.Gray

            '    btnEdit.Enabled = True
            '    btnEdit.ForeColor = System.Drawing.Color.Black

            '    btnDelete.Enabled = True
            '    btnDelete.ForeColor = System.Drawing.Color.Black

            '    btnPost.Enabled = True
            '    btnPost.ForeColor = System.Drawing.Color.Black
            '    'End If


            '    'End: Loop thru' Credit Values


            '    ''''''''''''''' Insert tblAR
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnReverse_Click", ex.Message.ToString, "")
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
                command.CommandText = "UPDATE tblRecv SET PostStatus='" + ddlNewStatus.SelectedValue + "', ReasonChSt ='" & txtReasonChSt.Text.Trim & "' where rcno=" & Convert.ToInt32(txtRcno.Text)
                command.Connection = conn
                command.ExecuteNonQuery()


                ''''''''''
                'If ddlPaymentMode.Text = "CHEQUE" Then
                If ddlPaymentMode.Text <> "CONTRA" Then
                    Dim qry As String
                    Dim commandChequeReturn As MySqlCommand = New MySqlCommand

                    commandChequeReturn.CommandType = CommandType.Text

                    qry = "UPDATE tblRecv SET  ChequeReturned=@ChequeReturned, "
                    qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn =@LastModifiedOn "
                    qry = qry + " where ((ReceiptNumber = @OriginalReceiptNumber) or (ReceiptNumber=@ReturnedReceiptNumber)) "
                    qry = qry + " and (BankID = @BankID);"

                    commandChequeReturn.CommandText = qry
                    commandChequeReturn.Parameters.Clear()

                    'txtComments.Text = Left(txtReceiptNo.Text, Len(txtReceiptNo.Text) - 2)

                    If Right(txtReceiptNo.Text, 1) = "X" Then
                        commandChequeReturn.Parameters.AddWithValue("@ReturnedReceiptNumber", txtReceiptNo.Text)
                        commandChequeReturn.Parameters.AddWithValue("@OriginalReceiptNumber", Left(txtReceiptNo.Text, Len(txtReceiptNo.Text) - 2))

                    Else
                        commandChequeReturn.Parameters.AddWithValue("@OriginalReceiptNumber", txtReceiptNo.Text)
                        commandChequeReturn.Parameters.AddWithValue("@ReturnedReceiptNumber", txtReceiptNo.Text + "-X")
                    End If

                    commandChequeReturn.Parameters.AddWithValue("@BankID", txtBankID.Text)

                    If ddlNewStatus.SelectedValue = "V" Then
                        commandChequeReturn.Parameters.AddWithValue("@ChequeReturned", 0)
                    Else
                        commandChequeReturn.Parameters.AddWithValue("@ChequeReturned", chkChequeReturned.Checked)
                    End If


                    commandChequeReturn.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    commandChequeReturn.Parameters.AddWithValue("@LastModifiedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-GB")))

                    commandChequeReturn.Connection = conn
                    commandChequeReturn.ExecuteNonQuery()

                    commandChequeReturn.Dispose()
                End If


                '''''''''
                '   UpdateContractActSvcDate(conn)

                conn.Close()
                conn.Dispose()
                txtPostStatus.Text = ddlNewStatus.SelectedValue
                'ddlStatus.Text = ddlNewStatus.Text
                ddlNewStatus.SelectedIndex = 0

                lblMessage.Text = "ACTION: STATUS UPDATED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CHST", txtReceiptNo.Text, "CHST", Convert.ToDateTime(txtCreatedOn.Text), txtReceivedAmount.Text, 0, txtReceivedAmount.Text, txtAccountIdBilling.Text, "", txtRcno.Text)


                SQLDSReceipt.SelectCommand = txt.Text
                SQLDSReceipt.DataBind()
                GridView1.DataBind()

                'GridView1.DataSourceID = "SqlDataSource1"
                mdlPopupStatus.Hide()
            End If

        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.Message.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub SQLDSReceipt_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles SQLDSReceipt.Selected
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

    Protected Sub OnSelectedIndexChangedgClient(sender As Object, e As EventArgs)
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
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
                'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
                'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
                'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
                'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GrdViewGL, "Select$" & e.Row.RowIndex)
                e.Row.ToolTip = "Click to select this row."
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "OnRowDataBoundgGL", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub OnSelectedIndexChangedgGL(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In GrdViewGL.Rows
                If row.RowIndex = GrdViewGL.SelectedIndex Then
                    row.BackColor = ColorTranslator.FromHtml("#738A9C")
                    row.ToolTip = String.Empty
                Else
                    row.BackColor = ColorTranslator.FromHtml("#E4E4E4")
                    row.ToolTip = "Click to select this row."
                End If
            Next
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "OnSelectedIndexChangedgGL", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub txtReceiptNoSearch_TextChanged(sender As Object, e As EventArgs) Handles txtReceiptNoSearch.TextChanged
        If Len(txtReceiptNoSearch.Text.Trim) > 2 Then
            btnQuickSearch_Click(sender, e)

            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            If GridView1.Rows.Count > 0 Then

                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
            End If
        End If
    End Sub

    Protected Sub ddlPaymentMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaymentMode.SelectedIndexChanged

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
            lblAlert.Text = exstr

            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnStatusSearch_Click1", ex.Message.ToString, "")
            
        End Try
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
            End If
        End If
    End Sub

    Protected Sub txtClientNameSearch_TextChanged(sender As Object, e As EventArgs) Handles txtClientNameSearch.TextChanged
        If Len(txtAccountIdSearch.Text.Trim) > 2 Then
            btnQuickSearch_Click(sender, e)

            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            MakeMeNullBillingDetails()

            If GridView1.Rows.Count > 0 Then

                txtMode.Text = "View"
                txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                PopulateRecord()
            End If
        End If
    End Sub

    Protected Sub ddlView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView.SelectedIndexChanged
        GridView1.PageSize = Convert.ToInt16(ddlView.SelectedItem.Text)

        SQLDSReceipt.SelectCommand = txt.Text
        SQLDSReceipt.DataBind()
        GridView1.DataBind()
    End Sub

    Public Function FindRVPeriod(BillingPeriod As String) As String
        Try
            Dim IsLock As String
            IsLock = "Y"

            Dim connPeriod As MySqlConnection = New MySqlConnection()

            connPeriod.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connPeriod.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            If txtMode.Text = "NEW" Then
                If txtDisplayRecordsLocationwise.Text = "N" Then
                    command1.CommandText = "SELECT RVLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
                Else
                    command1.CommandText = "SELECT RVLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
                End If

            Else
                If txtDisplayRecordsLocationwise.Text = "N" Then
                    command1.CommandText = "SELECT RVLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"

                Else
                    command1.CommandText = "SELECT RVLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
                End If

            End If

            command1.Connection = connPeriod

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                If txtMode.Text = "NEW" Then
                    IsLock = dt.Rows(0)("RVLock").ToString
                Else
                    IsLock = dt.Rows(0)("RVLocke").ToString
                End If
                'IsLock = dt.Rows(0)("RVLock").ToString
            End If

            connPeriod.Close()
            connPeriod.Dispose()
            command1.Dispose()
            dt.Dispose()

            Return IsLock
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FindRVPeriod", ex.Message.ToString, "")
            Return Nothing
        End Try
    End Function

    Protected Sub txtAccountIdII_TextChanged(sender As Object, e As EventArgs) Handles txtAccountIdII.TextChanged
        If Len(txtAccountIdII.Text) > 2 Then
            ImageButton1_Click(sender, New ImageClickEventArgs(0, 0))
        End If
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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "InsertIntoTblWebEventLog", ex.Message.ToString, "")
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
            lblAlertSearch.Text = ""

            Dim strsql As String

            'strsql = "SELECT PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ValueBase, ValueOriginal, GstBase, GstOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, Salesman, PoNo, OurRef, YourRef, Terms, DiscountAmount, GSTAmount, NetAmount, GlPeriod, CompanyGroup, ContactType, BatchNo, Salesman  Comments, AmountWithDiscount, TermsDay, RecurringInvoice, ReceiptBase, CreditBase, BalanceBase, StaffCode, CustAddress1, CustAddCountry, CustAddPostal, LedgerCode, LedgerName, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno, BillSchedule FROM tblsales WHERE 1=1   "
            strsql = "SELECT A.PostStatus,  A.BankStatus, A.GlStatus, A.ReceiptNumber, A.ReceiptDate, A.AccountId, A.AppliedBase, A.GSTAmount, A.BaseAmount, A.ReceiptFrom, A.ReceiptDate, A.NetAmount, A.GlPeriod, A.CompanyGroup, A.ContactType, A.Cheque, A.ChequeDate, A.BankId,  A.LedgerCode, A.LedgerName, A.Comments, A.PaymentType, A.Salesman, A.Location, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn, A.Rcno, A.ChequeReturned FROM tblrecv A where  1=1  "
            txtGrid.Text = "SELECT A.PostStatus,  A.ReceiptNumber, A.ReceiptDate, A.Cheque, A.AccountId, A.ContactType, A.ReceiptFrom, A.AppliedBase, A.BankId,  A.PaymentType, A.GlPeriod, A.CompanyGroup,   A.ChequeDate, A.ChequeReturned, A.CreatedBy, A.CreatedOn, A.LastModifiedBy, A.LastModifiedOn  FROM tblrecv A where  1=1"


            Dim YrStrList As List(Of [String]) = New List(Of String)()

          
            For Each item As ListItem In chkStatusSearch0.Items
                If item.Selected Then
                    YrStrList.Add("'" & item.Value & "'")
                End If
            Next

            Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())


            txtCondition.Text = txtCondition.Text & " and PostStatus in (" & (YrStr) & ") "

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                'txtCondition.Text = txtCondition.Text + " and Location = '" & txtLocation.Text & "'"
                txtCondition.Text = txtCondition.Text & " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"

            End If

            'txtSearchInvoiceNoTo

            'If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = False Then
            '    txtCondition.Text = txtCondition.Text & " and ReceiptNumber like '%" & txtSearchInvoiceNo.Text.Trim + "%'"
            'End If

            If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = False And String.IsNullOrEmpty(txtSearchInvoiceNoTo.Text) = True Then
                txtCondition.Text = txtCondition.Text & " and ReceiptNumber like '%" & txtSearchInvoiceNo.Text.Trim + "%'"
            End If

            If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = True And String.IsNullOrEmpty(txtSearchInvoiceNoTo.Text) = False Then
                lblAlertSearch.Text = "Please enter Receipt No. in 'Receipt No. From' "
                mdlPopupSearch.Show()
                Exit Sub
            End If

            If String.IsNullOrEmpty(txtSearchInvoiceNo.Text) = False And String.IsNullOrEmpty(txtSearchInvoiceNoTo.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and ReceiptNumber between '" & txtSearchInvoiceNo.Text.Trim + "' and '" & txtSearchInvoiceNoTo.Text.Trim & "'"
            End If


            If String.IsNullOrEmpty(txtSearchAccountID.Text) = False Then
                'strsql = strsql & " and (AccountId like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
                txtCondition.Text = txtCondition.Text & " and (AccountId like '%" & txtSearchAccountID.Text.Trim & "%')"

            End If

            If String.IsNullOrEmpty(txtSearchClientName.Text) = False Then
                txtCondition.Text = txtCondition.Text & " and ReceiptFrom like ""%" & txtSearchClientName.Text.Trim & "%"""
            End If


            If String.IsNullOrEmpty(txtSearchComments.Text.Trim.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and Comments like '%" & txtSearchComments.Text & "%'"
            End If


            If String.IsNullOrEmpty(txtSearchChequeNo.Text.Trim) = False Then
                txtCondition.Text = txtCondition.Text & " and Cheque like '%" & txtSearchChequeNo.Text & "%'"
            End If

            If ddlSearchBankId.SelectedIndex > 0 Then
                txtCondition.Text = txtCondition.Text & " and Bankid like '%" & ddlSearchBankId.Text & "%'"
            End If

            If ddlSearchPaymentMode.SelectedIndex > 0 Then
                txtCondition.Text = txtCondition.Text & " and PaymentType = '" & ddlSearchPaymentMode.Text & "'"
            End If

            If String.IsNullOrEmpty(txtSearchReceiptAmount.Text.Trim) = False Then
                If IsNumeric(txtSearchReceiptAmount.Text) = True Then
                    If String.IsNullOrEmpty(txtSearchReceiptAmount.Text.Trim) = False And txtSearchReceiptAmount.Text > 0.0 Then
                        txtCondition.Text = txtCondition.Text & " and AppliedBase = " & txtSearchReceiptAmount.Text
                    End If
                Else
                    lblAlertSearch.Text = "RECEIPT AMOUNT SHOULD BE NUMERIC"
                    mdlPopupSearch.Show()
                End If
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


            If (ddlSearchSalesman.SelectedIndex > 0) Then
                txtCondition.Text = txtCondition.Text & " and StaffCode like '%" & ddlSearchSalesman.Text.Trim + "%'"
            End If




            'If rdbSearchPaidStatus0.SelectedItem.Value = "Fully Paid" Then
            '    txtCondition.Text = txtCondition.Text + " and BalanceBase = 0 and ValueBase <> 0 "
            'ElseIf rdbSearchPaidStatus0.SelectedItem.Value = "O/S" Then
            '    txtCondition.Text = txtCondition.Text + " and BalanceBase <>  0"
            'End If


            If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ReceiptDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
                txtCondition.Text = txtCondition.Text + " and ReceiptDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            End If


            'If String.IsNullOrEmpty(txtInvoiceDateSearchFrom.Text) = False And txtInvoiceDateSearchFrom.Text <> "__/__/____" Then
            '    txtCondition.Text = txtCondition.Text + " and ReceiptDate >= '" + Convert.ToDateTime(txtInvoiceDateSearchFrom.Text).ToString("yyyy-MM-dd") + "'"
            'End If
            'If String.IsNullOrEmpty(txtInvoiceDateSearchTo.Text) = False And txtInvoiceDateSearchTo.Text <> "__/__/____" Then
            '    txtCondition.Text = txtCondition.Text + " and ReceiptDate <= '" + Convert.ToDateTime(txtInvoiceDateSearchTo.Text).ToString("yyyy-MM-dd") + "'"
            'End If


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



            '''''''''''''''''''''''''''''''

            If String.IsNullOrEmpty(txtSearchDetailReference.Text.Trim) = False Then
                If (ddlSearchContactType.Text.Trim) = "-1" Then
                    lblAlertSearch.Text = "SELECT ACCOUNT TYPE TO SEARCH FOR INVOICE NO."
                    mdlPopupSearch.Show()
                    Exit Sub
                End If

                If String.IsNullOrEmpty(txtSearchDetailReference.Text.Trim) = False Then
                    txtCondition.Text = txtCondition.Text & " AND ReceiptNumber IN (SELECT ReceiptNumber " _
                        & "FROM tblrecvdet WHERE RefType LIKE '%" & (txtSearchDetailReference.Text) & "%') "
                End If
            End If


            If String.IsNullOrEmpty(txtSearchDetailServiceLocation.Text.Trim) = False Then

                If (ddlSearchContactType.Text.Trim) = "-1" Then
                    lblAlertSearch.Text = "SELECT ACCOUNT TYPE TO SEARCH FOR LOCATION"
                    mdlPopupSearch.Show()
                    Exit Sub
                End If

                If ddlSearchContactType.Text.Trim = "CORPORATE" Or ddlSearchContactType.Text.Trim = "COMPANY" Then
                    txtCondition.Text = txtCondition.Text & " AND AccountId IN (Select AccountId from tblCompanyLocation where Address1 LIKE '%" & (txtSearchDetailServiceLocation.Text) & "%') "
                ElseIf ddlSearchContactType.Text.Trim = "RESIDENTIAL" Or ddlSearchContactType.Text.Trim = "PERSON" Then
                    txtCondition.Text = txtCondition.Text & " AND AccountId IN (Select AccountId from tblPersonLocation where Address1 LIKE '%" & (txtSearchDetailServiceLocation.Text) & "%') "
                End If
            End If

            If (ddlSearchContractGroup.Text.Trim) <> "-1" Then

                If (ddlSearchContactType.Text.Trim) = "-1" Then
                    lblAlertSearch.Text = "SELECT ACCOUNT TYPE TO SEARCH FOR CONTRACT GROUP"
                    mdlPopupSearch.Show()
                    Exit Sub
                End If

                If ddlSearchContactType.Text.Trim = "CORPORATE" Or ddlSearchContactType.Text.Trim = "COMPANY" Then
                    txtCondition.Text = txtCondition.Text & " AND AccountId IN (Select AccountId from tblCompanyLocation where ContractGroup = '" & (ddlSearchContractGroup.Text) & "') "
                ElseIf ddlSearchContactType.Text.Trim = "RESIDENTIAL" Or ddlSearchContactType.Text.Trim = "PERSON" Then
                    txtCondition.Text = txtCondition.Text & " AND AccountId IN (Select AccountId from tblPersonLocation where ContractGroup = '" & (ddlSearchContractGroup.Text) & "') "
                End If
            End If


            If (ddlCOACode.Text.Trim) <> "-1" Then
                txtCondition.Text = txtCondition.Text & " AND ReceiptNumber IN (SELECT ReceiptNumber " _
                    & "FROM tblrecvdet WHERE LedgerCode LIKE '%" & (Left(ddlCOACode.Text, 5)) & "%') "
            End If

            '''''''''''''''''''''''''''''''
            txtOrderBy.Text = " order by rcno desc, ReceiptFrom "

            strsql = strsql + txtCondition.Text + txtOrderBy.Text + " limit " & txtLimit.Text
            txt.Text = strsql

            'txtGrid.Text = txtGrid.Text + txtCondition.Text + txtOrderBy.Text + " limit " & txtLimit.Text
            txtGrid.Text = txtGrid.Text + txtCondition.Text + txtOrderBy.Text
            'txtComments.Text = strsql
            SQLDSReceipt.SelectCommand = strsql
            SQLDSReceipt.DataBind()
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

                    'If String.IsNullOrEmpty(txtRcnoSelected.Text.Trim) = False Then
                    '    If txtRcnoSelected.Text > 0 Then
                    '        txtRcno.Text = txtRcnoSelected.Text
                    '        txtRcnoSelected.Text = 0
                    '    Else
                    '        txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                    '    End If
                    'Else
                    '    txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                    'End If

                    'txtRcno.Text = GridView1.Rows(0).Cells(1).Text
                    txtRcno.Text = GridView1.Rows(0).Cells(1).Text
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
            updPanelReceipt.Update()
            'SqlDSMultiPrint.SelectCommand = SQLDSInvoice.SelectCommand
            'GridSelected = "SQLDSContract"



            txtSearchAccountID.Text = ""
            txtSearchClientName.Text = ""
            'txtSearchAddress.Text = ""
            'txtSearchContact.Text = ""
            'txtSearchContactNo.Text = ""
            'txtSearchPostal.Text = ""

            'txtSearchOurRef.Text = ""
            'txtSearchYourRef.Text = ""

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
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnSearch_Click", ex.Message.ToString, "")
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
                updPanelReceipt.Update()
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
                updPanelReceipt.Update()
            End If

            'txtImportService.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
            'txtImportService.Text = SqlDSClient.SelectCommand
            'mdlPopupSearch.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.Message.ToString
            lblAlert.Text = ex.Message.ToString
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnClient2_Click", ex.Message.ToString, "")
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Public Sub FindLocation()
        Try
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
            dr.Close()

        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FindLocation", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub


    Public Sub FindEnableReturn()
        Try
            Dim IsLock As String
            IsLock = ""

            Dim connLocation As MySqlConnection = New MySqlConnection()

            connLocation.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connLocation.Open()


            '''''''''''''
          

            'Dim command As MySqlCommand = New MySqlCommand

            'command.CommandType = CommandType.Text
            'command.CommandText = "SELECT count(Cheque) as TotalChequeNo FROM tblRecv where Cheque = '" & txtChequeNo.Text.Trim + " (RETURNED)" & "' and BankId= '" & txtBankID.Text & "'"
            'command.Connection = connLocation

            'Dim dr As MySqlDataReader = command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)


            'If Val(dt.Rows(0)("TotalChequeNo").ToString) = 0 Then
            ''''''''''''

            If chkChequeReturned.Checked = False Then
                Dim command1 As MySqlCommand = New MySqlCommand

                command1.CommandType = CommandType.Text
                command1.CommandText = "SELECT EnableReturn FROM tblsettletype where SettleType = '" & ddlPaymentMode.Text.Trim & "' and EnableReturn= True"
                command1.Connection = connLocation

                Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                Dim dt1 As New DataTable
                dt1.Load(dr1)

                'Using w As StreamWriter = File.AppendText(ErrOtLo)
                '    w.WriteLine("RCPT 5" + vbLf & vbLf)
                'End Using

                If dt1.Rows.Count > 0 Then
                    'Using w As StreamWriter = File.AppendText(ErrOtLo)
                    '    w.WriteLine("RCPT 6" + vbLf & vbLf)
                    'End Using
                    btnChequeReturn.Enabled = True
                    btnChequeReturn.ForeColor = System.Drawing.Color.Black
                Else
                    btnChequeReturn.Enabled = False
                    btnChequeReturn.ForeColor = System.Drawing.Color.Gray
                End If


                command1.Dispose()
                dt1.Dispose()
                dr1.Close()

                AccessControl()
            End If

            'Using w As StreamWriter = File.AppendText(ErrOtLo)
            '    w.WriteLine("RCPT 7" + vbLf & vbLf)
            'End Using
            connLocation.Close()
            connLocation.Dispose()
            'Command.Dispose()
            'dt.Dispose()
            'dr.Close()


        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FindLocation", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnJournal_Click(sender As Object, e As EventArgs) Handles btnJournal.Click
        Dim sqlstr As String = ""

        sqlstr = "SELECT tblJrnv.PostStatus, " & _
                       "tblJrnv.GlStatus,  " & _
                       "tblJrnv.VoucherNumber, " & _
                       "tblJrnv.JournalDate,  " & _
                       "tblJrnvDet.RefType, " & _
                       "tblJrnvDet.Currency, " & _
                       "tblJrnvDet.CreditBase, " & _
                       "tblJrnvDet.DebitBase, " & _
                       "tblJrnvDet.LedgerCode, " & _
                        "tblJrnvDet.LedgerName, " & _
                       "tblJrnvDet.SubLedgerCode, " & _
                       "tblJrnvDet.Description " & _
               "FROM tblJrnv LEFT OUTER JOIN " & _
                       "tblJrnvDet ON  " & _
                       "tblJrnv.VoucherNumber = tblJrnvDet.VoucherNumber "

        sqlstr = sqlstr & _
                    " WHERE tblJrnvDet.RefType = '" & txtReceiptNo.Text & "' "

        sqlstr = sqlstr & " UNION "

        sqlstr = sqlstr & "SELECT tblRecv.PostStatus, " & _
                       "tblRecv.GlStatus,  " & _
                       "tblRecv.ReceiptNumber, " & _
                       "tblRecv.ReceiptDate,  " & _
                       "tblRecvDet.RefType, " & _
                       "tblRecvDet.Currency, " & _
                       "if(tblRecvDet.Appliedbase < 0,tblRecvDet.Appliedbase * (-1),0), " & _
                       "if(tblRecvDet.Appliedbase >= 0,tblRecvDet.Appliedbase * (-1),0), " & _
                       "tblRecvDet.LedgerCode, " & _
                        "tblRecvDet.LedgerName, " & _
                       "tblRecvDet.SubLedgerCode, " & _
                       "tblRecvDet.Description " & _
               "FROM tblRecv LEFT OUTER JOIN " & _
                       "tblRecvDet ON  " & _
                       "tblRecv.ReceiptNumber = tblRecvDet.ReceiptNumber "
        sqlstr = sqlstr & _
                    " WHERE tblRecvDet.RefType = '" & txtReceiptNo.Text & "' "

        SqlDSJournal.SelectCommand = sqlstr

        'SELECT tblsalesdetail.InvoiceNumber as VoucherNumber,tblsalesdetail.Description as Description,tblsalesdetail.sourceref as Reference,tblsalesdetail.appliedbase as Amount,if(doctype ='ARIN','INVOICE' ,'CN') as Type FROM tblsalesdetail,tblsales where tblsales.invoicenumber=tblsalesdetail.invoicenumber and subcode='service' and reftype=@RecordNo ORDER BY VoucherNumber

        'SqlDSContractNo.DataBind()
        'gvPopUpContractNo.DataBind()
        'updPanelInvoice.Update()

        SqlDSJournal.DataBind()
        GrdJournalView.DataBind()
        updPanelReceipt.Update()
        'updPanelInvoice.Update()
        'Session.Add("customerfrom", "Service")

        'If GrdJournalView.Rows.Count = 0 Then
        '    lblAlertTransactions.Text = "THERE ARE NO TRANSACTIONS FOR THIS INVOICE"

        'Else
        '    lblAlertTransactions.Text = ""

        'End If

        mdlPopUpJournalView.Show()
    End Sub

    Protected Sub btnChequeReturn_Click(sender As Object, e As EventArgs) Handles btnChequeReturn.Click

        lblMessage.Text = ""
        lblAlert.Text = ""

        If txtRcno.Text = "" Then
            lblAlert.Text = "SELECT RECORD TO RETURN CHEQUE"
            Return
        End If

        lblMessage.Text = "ACTION: CHEQUE RETURN"

        'txtMode.Text = "NEW"

        txtMode.Text = "View"

        EnableControls()

        btnChequeReturn.Enabled = False
        btnChequeReturn.ForeColor = System.Drawing.Color.Gray

        btnReverse.Enabled = False
        btnReverse.ForeColor = System.Drawing.Color.Gray

        'AddNewRowBillingDetailsRecs()

      
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black
        Label60.Visible = True
     

        PopulateServiceGridChequeReturn()
        updPanelReceipt.Update()

        'PopulateRecordChequeReturn()
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

            'MakeMeNull()
            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno
            'RetrieveData()

            GridView1.SelectedIndex = rowindex1

            'Dim lblStatus As String = GridView1.Rows(rowindex1).Cells(2).Text
            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(6).Text
            txtLogDocNo.Text = strRecordNo

            'rcno = DirectCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text
            'txtRcno.Text = rcno.ToString()

            lblMessage.Text = ""
            'lblAlertSchDate.Text = ""
            lblAlert.Text = ""
            'txtGridIndex.Text = rowindex1.ToString
            GridView1.SelectedIndex = rowindex1
            sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()

            mdlViewEditHistory.Show()
            updPanelReceipt.Update()

        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, txtRcno.Text)
            lblAlert.Text = ex.Message.ToString
            'InsertIntoTblWebEventLog("btnEditHistory_Click", ex.Message.ToString, txtTechRcNo.Text)
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

    Protected Sub btnExportToExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportToExcel.Click
        Try

            If String.IsNullOrEmpty(txt.Text) = False Then

                'If GetData() = True Then
                'MessageBox.Message.Alert(Page, txtQuery.Text, "str")
                'Return

                Dim dt As DataTable = GetDataSet()
                WriteExcelWithNPOI(dt, "xlsx")
                Return

                Dim attachment As String = "attachment; filename=Receipt.xls"
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

                'End If

                'Dim dt As DataTable = GetDataSet()
                'Dim attachment As String = "attachment; filename=Service.xls"
                'Response.ClearContent()
                'Response.AddHeader("content-disposition", attachment)
                'Response.ContentType = "application/vnd.ms-excel"
                'Response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252")
                'Dim tab As String = ""
                'For Each dc As DataColumn In dt.Columns
                '    Response.Write(tab + dc.ColumnName)
                '    tab = vbTab
                'Next
                'Response.Write(vbLf)
                'Dim i As Integer
                'For Each dr As DataRow In dt.Rows
                '    tab = ""
                '    For i = 0 To dt.Columns.Count - 1
                '        Response.Write(tab & dr(i).ToString())
                '        tab = vbTab
                '    Next
                '    Response.Write(vbLf)
                'Next

                'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SERV", "", "btnExportToExcel_Click", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtReceiptNo.Text, "", txtRcno.Text)
                'Response.End()
                'dt.Clear()


            Else
                lblAlert.Text = "NO DATA TO EXPORT"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "btnExportToExcel_Click", ex.Message.ToString, "")
            'InsertIntoTblWebEventLog("btnExportToExcel_Click", ex.Message.ToString, txtReceiptNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
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
        '  cell1.SetCellValue(Session("Selection").ToString)
        'cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")" + Session("Selection").ToString)
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ") - Receipts Listing")

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
            ' InsertIntoTblWebEventLog("WriteExcel", dt.Columns(j).GetType().ToString(), dt.Columns(j).ToString())

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

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = ""

            'attachment = "attachment; filename=PortfolioSummary" + txtCriteria.Text + "_By" + rbtnSelect.SelectedItem.Text
            attachment = "attachment; filename=Receipt"

            If extension = "xlsx" Then
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format(attachment + ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "PortfolioSummary.xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If

            Response.[End]()
        End Using
    End Sub
    Private Function GetDataSet() As DataTable
        Try
            Dim qry As String = ""

          

            Dim query As String = txtGrid.Text
            'query = qry + query.Substring(query.IndexOf("where"))


            Dim dt As New DataTable()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim cmd As MySqlCommand = New MySqlCommand

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim sda As New MySqlDataAdapter()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query

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
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + Session("UserID"), "FUNCTION GetDataSet", ex.Message.ToString, "")
            'InsertIntoTblWebEventLog("GetDataSet", ex.Message.ToString, txtReceiptNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Function

    Protected Sub ddlViewServiceRecs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewServiceRecs.SelectedIndexChanged
        PopulateServiceGrid()

        lbltotalservices.Text = (grvBillingDetails.Rows.Count).ToString & " out of " & txtTotDetRec.Text

        'SqlDSSalesDetail.SelectCommand = txtSQLDetail.Text & "' Limit " & ddlViewServiceRecs.Text

        ''grvBillingDetailsNew.PageSize = Convert.ToInt16(ddlViewServiceRecs.SelectedItem.Text)
        'grvBillingDetails.DataSourceID = "SqlDSSalesDetail"
        'grvBillingDetails.DataBind()

        'lbltotalservices.Text = grvBillingDetails.Rows.Count & " out of " & txtTotDetRec.Text
        'UpdatePanel5.Update()
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

            If String.IsNullOrEmpty(txtReceiptNo.Text) Then
                lblAlert.Text = "Select a Receipt to Proceed"

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

            lblFileUploadRecNo.Text = txtReceiptNo.Text
            ' lblFileUploadAccountID.Text = txtAccountId.Text
            lblFileUploadName.Text = txtAccountName.Text

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

            SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtReceiptNo.Text + "'"
            gvUpload.DataSourceID = "SqlDSUpload"
            gvUpload.DataBind()

            lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
        End If

    End Sub

    Protected Sub UploadFile(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            lblMessage.Text = ""
            lblAlert.Text = ""
            If String.IsNullOrEmpty(lblFileUploadRecNo.Text) Then
                lblAlert.Text = "SELECT RECEIPT TO UPLOAD FILE"
                Return

            End If

            If String.IsNullOrEmpty(txtFileDescription.Text) Then
                lblAlert.Text = "ENTER FILE DESCRIPTION TO UPLOAD FILE"
                Exit Sub

            End If
            InsertIntoTblWebEventLog("CN - UPLOAD1", "BTNUPLOAD", FileUpload1.HasFile.ToString, txtReceiptNo.Text)

            If FileUpload1.HasFile Then

                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim ext As String = Path.GetExtension(fileName)

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                InsertIntoTblWebEventLog("RECEIPT - UPLOAD2", "BTNUPLOAD", ext, txtReceiptNo.Text)

                If ext = ".DOC" Or ext = ".doc" Or ext = ".DOCX" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Or ext = ".XLS" Or ext = ".XLSX" Or ext = ".CSV" Or ext = ".csv" Or ext = ".ppt" Or ext = ".PPT" Or ext = ".pptx" Or ext = ".PPTX" Or ext = ".PDF" Or ext = ".pdf" Or ext = ".txt" Or ext = ".TXT" Or ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Or ext = ".bmp" Or ext = ".JPG" Or ext = ".JPEG" Or ext = ".PNG" Or ext = ".BMP" Then

                    Dim strFilePath As String = Server.MapPath("~/Uploads/Accounts/Receipt/")

                    strFilePath = strFilePath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")

                    If File.Exists(strFilePath + txtReceiptNo.Text + "_" + fileName) Then

                        Dim command1 As MySqlCommand = New MySqlCommand

                        command1.CommandType = CommandType.Text

                        command1.CommandText = "SELECT * FROM tblFILEUPLOAD where filenamelink=@filenamelink"
                        command1.Parameters.AddWithValue("@filenamelink", txtReceiptNo.Text + "_" + fileName)
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
                        command1.Parameters.AddWithValue("@filenamelink", txtReceiptNo.Text + "_" + fileName)
                        command1.Connection = conn

                        Dim dr As MySqlDataReader = command1.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(dr)

                        If dt.Rows.Count > 0 Then

                            Dim command2 As MySqlCommand = New MySqlCommand

                            command2.CommandType = CommandType.Text

                            command2.CommandText = "delete from fileupload where filenamelink='" + txtReceiptNo.Text + "_" + fileName + "'"

                            command2.Connection = conn

                            command2.ExecuteNonQuery()
                        End If
                    End If
                    FileUpload1.PostedFile.SaveAs(strFilePath + txtReceiptNo.Text + "_" + fileName)

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

                    InsertIntoTblWebEventLog("RECEIPT - UPLOAD3", "BTNUPLOAD", ext, txtReceiptNo.Text)


                    Dim command As MySqlCommand = New MySqlCommand

                    command.CommandType = CommandType.Text
                    Dim qry As String = "INSERT INTO tblfileupload(FileGroup,FileRef,FileName,FileDescription,FileType,FileNameLink,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)"
                    qry = qry + "VALUES(@FileGroup,@FileRef,@FileName,@FileDescription,@FileType,@FileNameLink,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn);"


                    command.CommandText = qry
                    command.Parameters.Clear()

                    If ConfigurationManager.AppSettings("UPPERCASE").ToString() = "YES" Then
                           command.Parameters.AddWithValue("@FileGroup", "RECEIPT")
                        
                        command.Parameters.AddWithValue("@FileRef", txtReceiptNo.Text)
                        command.Parameters.AddWithValue("@FileName", fileName.ToUpper)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text.ToUpper)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtReceiptNo.Text + "_" + fileName.ToUpper)

                    ElseIf ConfigurationManager.AppSettings("UPPERCASE").ToString() = "NO" Then

                        command.Parameters.AddWithValue("@FileGroup", "RECEIPT")

                        command.Parameters.AddWithValue("@FileRef", txtReceiptNo.Text)
                        command.Parameters.AddWithValue("@FileName", fileName)
                        command.Parameters.AddWithValue("@FileDescription", txtFileDescription.Text)
                        command.Parameters.AddWithValue("@FileType", ext.ToUpper)
                        command.Parameters.AddWithValue("@CreatedBy", txtCreatedBy.Text)
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text)
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@FileNameLink", txtReceiptNo.Text + "_" + fileName)

                    End If


                    command.Connection = conn

                    command.ExecuteNonQuery()
                    conn.Close()
                    conn.Dispose()
                    command.Dispose()

                    InsertIntoTblWebEventLog("RECEIPT - UPLOAD4", "BTNUPLOAD", ext, txtReceiptNo.Text)


                    SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtReceiptNo.Text + "'"
                    gvUpload.DataSourceID = "SqlDSUpload"
                    gvUpload.DataBind()
                    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtReceiptNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", txtReceiptNo.Text + "_" + fileName, txtReceiptNo.Text)

                    '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "FILEUPLOAD", txtAccountID.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtAccountID.Text + "_" + fileName)

                    txtFileDescription.Text = ""

                    lblMessage.Text = "FILE UPLOADED"
                    lblFileUploadCount.Text = "File Upload [" & gvUpload.Rows.Count & "]"
                    InsertIntoTblWebEventLog("RECEIPT - UPLOAD5", "BTNUPLOAD", ext, txtReceiptNo.Text)

                Else
                    lblAlert.Text = "FILE FORMAT NOT ALLOWED TO UPLOAD"
                    Return
                End If
            Else
                lblAlert.Text = "SELECT FILE TO UPLOAD"
            End If
            '  Response.Redirect(Request.Url.AbsoluteUri)
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + txtCreatedBy.Text, "Upload File", ex.Message.ToString, txtReceiptNo.Text + "-" + FileUpload1.PostedFile.FileName)
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
            filePath = Server.MapPath("~/Uploads/Accounts/Receipt/") + filePath
            filePath = filePath.Replace("MalaysiaPreProduction", "AnticimexMalaysia")

            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + txtCreatedBy.Text, "DownloadFile", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            txtFileLink.Text = filePath

            filePath = Server.MapPath("~/Uploads/Accounts/Receipt/") + filePath
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
            InsertIntoTblWebEventLog("RECEIPT - " + txtCreatedBy.Text, "DeleteFile", ex.Message.ToString, "")
        End Try
    End Sub

    Protected Sub PreviewFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If ConfigurationManager.AppSettings("DomainName") = "MALAYSIA PRE-PRODUCTION" Then
                Dim filepath1 As String = Server.MapPath("Uploads\Accounts\Receipt\") + txtFileLink.Text
                Dim filepath2 As String = "E:\WEBSITE FILES\MalaysiaPreproduction\Uploads\Accounts\Receipt\" + txtFileLink.Text

                File.Copy(filepath1, filepath2)

            End If

            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            Dim ext As String = Path.GetExtension(filePath)
            filePath = "Uploads/Accounts/Receipt/" + filePath
            ext = ext.ToLower

            '  filePath = Server.MapPath("~/Uploads/") + filePath
            '    frmWord.Attributes["src"] = http://localhost/MyApp/resume.doc;
            ' iframeid.Attributes.Add("src", Server.HtmlDecode("D:\1_CWBInfotech\A_Sitapest\Program\Sitapest\Uploads\10000145_photo (1).JPG"))
            If ext = ".doc" Or ext = ".docx" Or ext = ".xls" Or ext = ".xlsx" Then
                Dim strFilePath As String = Server.MapPath("Uploads\Accounts\Receipt\")
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

                iframeid.Attributes("src") = "Uploads/Accounts/Receipt/A" + strFile.Split("."c)(0) & Convert.ToString(".html")

            Else
                iframeid.Attributes.Add("src", filePath)

            End If
            '  iframeid.Attributes.Add("src", "https://docs.google.com/viewer?url={D:/1_CWBInfotech/A_Sitapest/Program/Sitapest/Uploads/10000145_ActualVsForecast_Format1.pdf?pid=explorer&efh=false&a=v&chrome=false&embedded=true")
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT - " + txtCreatedBy.Text, "PreviewFile", ex.Message.ToString, "")
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
            filePath = Server.MapPath("~/Uploads/Accounts/Receipt/") + filePath
            'lblMessage.Text = filePath
            'Return
            Session.Add("FilePath", filePath)

            '    Response.Redirect("Email.aspx?Type=FileUpload")
            Dim Url As String = "Email.aspx?Type=FileUpload"
            Response.Write("<script language='javascript'>window.open('" & Url & "','_blank','');")
            Response.Write("</script>")
        Catch ex As Exception
            InsertIntoTblWebEventLog("RECEIPT", "EmailFile", ex.Message.ToString, txtReceiptNo.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs) Handles btnConfirmDelete.Click
        ' InsertIntoTblWebEventLog("FILEDELETE1", "ConfirmDelete", "1", txtInvoiceNo.Text)

        Dim deletefilepath1 As String = Server.MapPath("~/Uploads/Accounts/Receipt/DeletedFiles/") + txtFileLink.Text
        Dim deletefilepath As String = Server.MapPath("~/Uploads/Accounts/Receipt/DeletedFiles/") + Path.GetFileNameWithoutExtension(deletefilepath1) + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("ssmmhh") + Path.GetExtension(deletefilepath1)
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

            SqlDSUpload.SelectCommand = "select * from tblfileupload where fileref = '" + txtReceiptNo.Text + "'"
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
            InsertIntoTblWebEventLog("RECEIPT - " + txtCreatedBy.Text, "FILE DELETE", ex.Message.ToString, txtReceiptNo.Text + " " + txtFileLink.Text)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub
End Class
