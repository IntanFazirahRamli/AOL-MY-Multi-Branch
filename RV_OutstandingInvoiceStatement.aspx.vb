

Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Data
Imports MySql.Data

Partial Class RV_OutstandingInvoiceStatement
    Inherits System.Web.UI.Page

    Dim crReportDocument As New ReportDocument()
    Dim expo As New ExportOptions
    Dim oDfDopt As New DiskFileDestinationOptions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '  If Not IsPostBack Then
            Dim AccountID As String = Convert.ToString(Session("AccountID"))
            txtAccountID.Text = AccountID

            Dim AccountType As String = Convert.ToString(Session("AccountTypeSOA"))
            txtAccountType.Text = AccountType
            'txtCutOffDate.Text = Convert.ToString(Session("SysDate"))
            txtCutOffDate.Text = Convert.ToString(Session("cutoffoscustomer"))
            txtCreatedBy.Text = Convert.ToString(Session("UserID"))

            GetDataInvRecvTest(AccountID, AccountType, txtCutOffDate.Text, txtCreatedBy.Text)

            '   GetDataInvRecv()

            '   btnQuit.Attributes.Add("onClick", "javascript:history.back(); return false;")

            '  crReportDocument.Load(Server.MapPath("~/Reports/ARReports/OutstandingInvoiceStatement.rpt"))
            crReportDocument.Load(Server.MapPath("~/Reports/ARNewReports/StatementOfAccountsReports/StOfAcInvRecvBaseCurOS_Cust.rpt"))
            crReportDocument.SetParameterValue("pBal", "ALL")
            '  crReportDocument.SetParameterValue("pUserId", Convert.ToString(Session("UserId")))
            '  crReportDocument.SetParameterValue("pSelectedFields", Convert.ToString(Session("selection")))
            'crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(Session("PrintDate")))

            crReportDocument.SetParameterValue("pStatementDate", Convert.ToDateTime(txtCutOffDate.Text))


            Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
            Dim myTables As Tables = crReportDocument.Database.Tables

            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                myConnectionInfo.ServerName = ConfigurationManager.AppSettings("ServerName")
                myConnectionInfo.DatabaseName = ConfigurationManager.AppSettings("DatabaseName")
                myConnectionInfo.UserID = ConfigurationManager.AppSettings("UserName")
                myConnectionInfo.Password = ConfigurationManager.AppSettings("Password")
                myTable.ApplyLogOnInfo(myTableLogonInfo)
                myTableLogonInfo.ConnectionInfo = myConnectionInfo
            Next
            '      crReportDocument.RecordSelectionFormula = "{tblsales1.AccountId}='" + txtAccountID.Text + "' and {tblsales1.BalanceBase} <> 0 and {tblsales1.DocType} = 'ARIN' and {tblsales1.PostStatus} = 'P'"
            crReportDocument.RecordSelectionFormula = "{m02AR22.Balance} <> 0 AND {m02AR22.JobOrder} = '" + txtCreatedBy.Text + "'"
            Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + txtAccountID.Text + "_SOA.PDF"))

            If File.Exists(FilePath) Then
                File.Delete(FilePath)

            End If
            oDfDopt.DiskFileName = FilePath 'path of file where u want to locate ur PDF

            expo = crReportDocument.ExportOptions

            expo.ExportDestinationType = ExportDestinationType.DiskFile

            expo.ExportFormatType = ExportFormatType.PortableDocFormat

            expo.DestinationOptions = oDfDopt

            '    oRDoc.SetDatabaseLogon("PaySquare", "paysquare") 'login for your DataBase

            crReportDocument.Export()

            Dim User As New WebClient()
            Dim FileBuffer As [Byte]() = User.DownloadData(FilePath)
            If FileBuffer IsNot Nothing Then
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", FileBuffer.Length.ToString())
                Response.AddHeader("content-disposition", "inline; filename=" & txtAccountID.Text & "_SOA.pdf")

                Response.BinaryWrite(FileBuffer)
            End If


        Catch ex As Exception
            InsertIntoTblWebEventLog("Page_Load", ex.Message.ToString, txtAccountID.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs)
        '  Response.Redirect("Reports.aspx")
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        Dim FilePath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("UserID") + "_" + txtAccountID.Text + "_OutstandingInvoice.PDF"))

        If File.Exists(FilePath) Then
            File.Delete(FilePath)

        End If
        crReportDocument.Close()
        crReportDocument.Dispose()
    End Sub

    'Private Function GetDataInvRecv() As Boolean
    '    Dim qry As String = ""
    '    Dim qryrecv As String = ""
    '    Dim qryrecv1 As String = ""

    '    Dim selFormula As String
    '    Dim selection As String
    '    selection = ""
    '    'If chkCheckCutOff.Checked = True Then
    '    '    selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
    '    '    qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
    '    '    qry = qry + "OPeriodBalance as Balancebase,"
    '    '    qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo FROM tblsales"
    '    '    qry = qry + " where poststatus='P'"
    '    '    'If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then

    '    '    '    qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

    '    '    'End If
    '    'Else
    '    '  selFormula = "{m02AR22.rcno} <> 0 AND {m02AR22.JobOrder} = '" + Convert.ToString(Session("UserID")) + "'"
    '    qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
    '    qry = qry + "BalanceBase,"
    '    qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo, Rcno FROM tblsales"
    '    'qry = qry + " where balancebase <> 0 and doctype='ARIN' and poststatus='P'"
    '    qry = qry + " where balancebase <> 0  and poststatus='P'"
    '    qry = qry + " and tblsales.Accountid = '" + txtAccountID.Text + "'"
    '    qry = qry + " and tblsales.SalesDate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"


    '    '    End If
    '    qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
    '    'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
    '    qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
    '    qryrecv = qryrecv + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
    '    qryrecv = qryrecv + " and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"


    '    qryrecv1 = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,-tblrecvdet.appliedbase as appliedbase,-tblrecvdet.valuebase as valuebase"
    '    'qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND BankID='CONTRA'"
    '    qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
    '    qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + txtAccountID.Text + "'"
    '    qryrecv1 = qryrecv1 + " and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

    '    'If chkCheckCutOff.Checked = True Then

    '    '    If String.IsNullOrEmpty(txtCutOffDate.Text) = False Then
    '    '        Dim d As DateTime
    '    '        If Date.TryParseExact(txtCutOffDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '    '        Else
    '    '            MessageBox.Message.Alert(Page, "CutOff Date is invalid", "str")
    '    '            '  lblAlert.Text = "INVALID START DATE"
    '    '            Return False
    '    '        End If
    '    '        qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"
    '    '        qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

    '    '        '     selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
    '    '        selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
    '    '    End If
    '    'Else
    '    '    If String.IsNullOrEmpty(txtPrintDate.Text) = False Then
    '    '        Dim d As DateTime
    '    '        If Date.TryParseExact(txtPrintDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then

    '    '        Else
    '    '            MessageBox.Message.Alert(Page, "Print Date is invalid", "str")
    '    '            '  lblAlert.Text = "INVALID START DATE"
    '    '            Return False
    '    '        End If
    '    '        qry = qry + " and tblsales.salesdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"
    '    '        qryrecv = qryrecv + " and tblrecv.receiptdate<= '" + Convert.ToDateTime(txtPrintDate.Text).ToString("yyyy-MM-dd") + "'"

    '    '        '  selFormula = selFormula + " and {m02AR22.VoucherDate} <=" + "#" + d.ToString("MM-dd-yyyy") + "#"
    '    '        selection = selection + ", Date >= " + d.ToString("dd-MM-yyyy")
    '    '    End If
    '    'End If

    '    qry = qry + " ORDER BY AccountId, InvoiceNumber"
    '    qryrecv = qryrecv + " ORDER BY AccountId, tblrecv.ReceiptNumber"
    '    qryrecv1 = qryrecv1 + " ORDER BY AccountId, tblrecv.ReceiptNumber"


    '    txtQuery.Text = qry
    '    txtQueryRecv.Text = qryrecv
    '    txtQueryRecv1.Text = qryrecv1

    '    'InsertM02AR22()

    '    InsertM02AR22Sen()

    '    Session.Add("selFormula", selFormula)
    '    Session.Add("selection", selection)
    '    Session.Add("PrintDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

    '    Return True
    'End Function

    ''Private Sub InsertM02AR22()
    ''    Try
    ''        Dim conn As MySqlConnection = New MySqlConnection()

    ''        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    ''        conn.Open()

    ''        Dim command0 As MySqlCommand = New MySqlCommand

    ''        command0.CommandType = CommandType.Text
    ''        command0.CommandText = "delete from tblAR22 where Joborder='" + Convert.ToString(Session("UserID")) + "'"

    ''        command0.Connection = conn

    ''        command0.ExecuteNonQuery()

    ''        command0.Dispose()

    ''        Dim command1 As MySqlCommand = New MySqlCommand

    ''        command1.CommandType = CommandType.Text

    ''        command1.CommandText = txtQuery.Text
    ''        command1.Connection = conn

    ''        Dim dr As MySqlDataReader = command1.ExecuteReader()
    ''        Dim dt As New DataTable
    ''        dt.Load(dr)

    ''        If dt.Rows.Count > 0 Then
    ''            For i As Int16 = 0 To dt.Rows.Count - 1
    ''                Dim command As MySqlCommand = New MySqlCommand

    ''                command.CommandType = CommandType.Text
    ''                Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

    ''                command.CommandText = qry
    ''                command.Parameters.Clear()

    ''                command.Parameters.AddWithValue("@VoucherDate", dt.Rows(i)("SalesDate"))
    ''                command.Parameters.AddWithValue("@Type", "INVOICE")
    ''                command.Parameters.AddWithValue("@VoucherNumber", dt.Rows(i)("InvoiceNumber"))
    ''                command.Parameters.AddWithValue("@Debit", dt.Rows(i)("AppliedBase"))
    ''                command.Parameters.AddWithValue("@Credit", dt.Rows(i)("CreditBase") + dt.Rows(i)("ReceiptBase"))
    ''                command.Parameters.AddWithValue("@Balance", dt.Rows(i)("BalanceBase"))
    ''                command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
    ''                command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountID"))
    ''                command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
    ''                command.Parameters.AddWithValue("@Comments", dt.Rows(i)("Comments"))
    ''                command.Parameters.AddWithValue("@AccountDate", dt.Rows(i)("SalesDate"))
    ''                command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
    ''                command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
    ''                command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
    ''                command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
    ''                command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
    ''                command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
    ''                command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

    ''                command.Connection = conn

    ''                command.ExecuteNonQuery()
    ''                command.Dispose()

    ''            Next

    ''            command1.Dispose()
    ''            dt.Clear()
    ''            dt.Dispose()
    ''            dr.Close()

    ''        Else
    ''            Dim command As MySqlCommand = New MySqlCommand

    ''            command.CommandType = CommandType.Text
    ''            Dim qry As String = "INSERT INTO tblar22(AccountId,JobOrder) VALUES (@AccountId,@JobOrder);"

    ''            command.CommandText = qry
    ''            command.Parameters.Clear()


    ''            command.Parameters.AddWithValue("@AccountId", txtAccountID.Text)
    ''            command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))

    ''            command.Connection = conn

    ''            command.ExecuteNonQuery()
    ''            command.Dispose()

    ''        End If

    ''        For J As Int16 = 0 To 1
    ''            Dim command2 As MySqlCommand = New MySqlCommand

    ''            command2.CommandType = CommandType.Text

    ''            If J = 0 Then
    ''                command2.CommandText = txtQueryRecv.Text
    ''            ElseIf J = 1 Then
    ''                command2.CommandText = txtQueryRecv1.Text
    ''            End If

    ''              command2.Connection = conn

    ''            Dim dr2 As MySqlDataReader = command2.ExecuteReader()
    ''            Dim dt2 As New DataTable
    ''            dt2.Load(dr2)

    ''            If dt2.Rows.Count > 0 Then
    ''                For i As Int16 = 0 To dt2.Rows.Count - 1
    ''                    Dim command As MySqlCommand = New MySqlCommand

    ''                    command.CommandType = CommandType.Text
    ''                    Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

    ''                    command.CommandText = qry
    ''                    command.Parameters.Clear()

    ''                    command.Parameters.AddWithValue("@VoucherDate", dt2.Rows(i)("ReceiptDate"))
    ''                    command.Parameters.AddWithValue("@Type", "RECEIPT")
    ''                    command.Parameters.AddWithValue("@VoucherNumber", dt2.Rows(i)("Cheque"))
    ''                    command.Parameters.AddWithValue("@Debit", 0)
    ''                    command.Parameters.AddWithValue("@Credit", dt2.Rows(i)("AppliedBase"))
    ''                    command.Parameters.AddWithValue("@Balance", -(dt2.Rows(i)("AppliedBase")))
    ''                    command.Parameters.AddWithValue("@ContactType", dt2.Rows(i)("ContactType"))
    ''                    command.Parameters.AddWithValue("@AccountId", dt2.Rows(i)("AccountID"))
    ''                    command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
    ''                    command.Parameters.AddWithValue("@Comments", dt2.Rows(i)("Comments"))
    ''                    command.Parameters.AddWithValue("@AccountDate", dt2.Rows(i)("ReceiptDate"))
    ''                    command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
    ''                    command.Parameters.AddWithValue("@CustName", dt2.Rows(i)("ReceiptFrom"))
    ''                    command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
    ''                    command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
    ''                    command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
    ''                    command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
    ''                    command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

    ''                    command.Connection = conn

    ''                    command.ExecuteNonQuery()
    ''                    command.Dispose()

    ''                Next

    ''                command2.Dispose()
    ''                dt2.Clear()
    ''                dt2.Dispose()
    ''                dr2.Close()

    ''            End If
    ''        Next

    ''        conn.Close()

    ''    Catch ex As Exception
    ''        InsertIntoTblWebEventLog("InserM02AR22", ex.Message.ToString, "")

    ''    End Try

    ''End Sub


    'Private Sub InsertM02AR22Sen()
    '    Try

    '        Dim sqlst, sqlst1, isWhere, isWhere1 As String
    '        Dim inIsWhere, inIsWhere1 As Integer

    '        sqlst = ""
    '        sqlst1 = ""

    '        isWhere = txtQuery.Text
    '        inIsWhere = isWhere.IndexOf("where")

    '        'txtCustName.Text = ""

    '        sqlst = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT SalesDate, 'INVOICE',  InvoiceNumber, appliedbase, (CreditBase+ReceiptBase), OPeriodBalance, contacttype, AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' ,comments, SalesDate,'" & Session("UserID") & "', CustName, InvoiceNumber, Rcno, 'ARIN' from tblSales "
    '        sqlst = sqlst + " " + txtQuery.Text.Substring(inIsWhere)

    '        ''''''''''''''''''''''''
    '        'txtCustName.Text = txtQueryRecv.Text
    '        isWhere1 = txtQueryRecv.Text
    '        inIsWhere1 = isWhere1.IndexOf("where")

    '        'txtCustName.Text = ""

    '        sqlst1 = "insert into tblar22 (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tblrecv.ReceiptDate, 'RECEIPT',  tblrecv.Cheque, 0, tblrecvdet.appliedbase, tblrecvdet.appliedbase * (-1), tblrecv.contacttype, tblrecv.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblrecv.comments, tblrecv.ReceiptDate,'" & Session("UserID") & "', tblrecv.ReceiptFrom, tblrecv.ReceiptNumber, tblRecvdet.Rcno, 'RECV' from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber  "
    '        sqlst1 = sqlst1 + " " + txtQueryRecv.Text.Substring(inIsWhere1)

    '        ''''''''''''''''''''''''''

    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        Dim command As MySqlCommand = New MySqlCommand
    '        command.CommandType = CommandType.StoredProcedure
    '        command.CommandText = "SaveTbwAR22"

    '        command.Parameters.Clear()

    '        'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
    '        'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
    '        command.Parameters.AddWithValue("@pr_Query", sqlst)
    '        command.Parameters.AddWithValue("@pr_Query1", sqlst1)
    '        command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
    '        command.Connection = conn
    '        command.ExecuteScalar()
    '        'conn.Close()
    '        'conn.Dispose()
    '        command.Dispose()

    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''


    '        'Dim command As MySqlCommand = New MySqlCommand
    '        command.CommandType = CommandType.StoredProcedure
    '        'If rbtnSelectDetSumm.SelectedValue.ToString = "1" Then
    '        'If rbtnSelectDetSumm.SelectedIndex = 0 Then
    '        '    command.CommandText = "SaveTbwARDetail1SOA"
    '        'Else
    '        '    'command.CommandText = "SaveTbwARSummary"
    '        '    command.CommandText = "SaveTbwARDetail1"
    '        'End If
    '        command.CommandText = "SaveTbwARDetail1SOA"
    '        command.Parameters.Clear()

    '        command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
    '        command.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))

    '        command.Parameters.AddWithValue("@pr_AccountType", txtAccountType.Text)
    '        command.Parameters.AddWithValue("@pr_AccountIdFrom", txtAccountID.Text.Trim)
    '        command.Parameters.AddWithValue("@pr_AccountIdTo", txtAccountID.Text.Trim)


    '        command.Connection = conn
    '        command.ExecuteScalar()
    '        'conn.Close()
    '        'conn.Dispose()
    '        command.Dispose()

    '        '''''''''''''''''''''''''''''''''''''

    '        Dim command5 As MySqlCommand = New MySqlCommand
    '        command5.CommandType = CommandType.StoredProcedure

    '        command5.CommandText = "SaveTbwARDetail2SOA"
    '        command5.Parameters.Clear()

    '        command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
    '        command5.Parameters.AddWithValue("@pr_CreatedBy", Session("UserID"))
    '        command5.Connection = conn
    '        command5.ExecuteScalar()
    '        conn.Close()
    '        conn.Dispose()
    '        command5.Dispose()

    '        '''''''''''''''''''''''''''''''''''''
    '        'Dim conn As MySqlConnection = New MySqlConnection()

    '        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        'conn.Open()

    '        'Dim command0 As MySqlCommand = New MySqlCommand

    '        'command0.CommandType = CommandType.Text
    '        'command0.CommandText = "delete from tblAR22 where Joborder='" + Convert.ToString(Session("UserID")) + "'"

    '        'command0.Connection = conn

    '        'command0.ExecuteNonQuery()

    '        'command0.Dispose()

    '        'Dim command1 As MySqlCommand = New MySqlCommand

    '        'command1.CommandType = CommandType.Text

    '        'command1.CommandText = txtQuery.Text
    '        'command1.Connection = conn

    '        'Dim dr As MySqlDataReader = command1.ExecuteReader()
    '        'Dim dt As New DataTable
    '        'dt.Load(dr)

    '        'If dt.Rows.Count > 0 Then
    '        '    For i As Int16 = 0 To dt.Rows.Count - 1
    '        '        Dim command As MySqlCommand = New MySqlCommand

    '        '        command.CommandType = CommandType.Text
    '        '        Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

    '        '        command.CommandText = qry
    '        '        command.Parameters.Clear()

    '        '        command.Parameters.AddWithValue("@VoucherDate", dt.Rows(i)("SalesDate"))
    '        '        command.Parameters.AddWithValue("@Type", "INVOICE")
    '        '        command.Parameters.AddWithValue("@VoucherNumber", dt.Rows(i)("InvoiceNumber"))
    '        '        command.Parameters.AddWithValue("@Debit", dt.Rows(i)("AppliedBase"))
    '        '        command.Parameters.AddWithValue("@Credit", dt.Rows(i)("CreditBase") + dt.Rows(i)("ReceiptBase"))
    '        '        command.Parameters.AddWithValue("@Balance", dt.Rows(i)("BalanceBase"))
    '        '        command.Parameters.AddWithValue("@ContactType", dt.Rows(i)("ContactType"))
    '        '        command.Parameters.AddWithValue("@AccountId", dt.Rows(i)("AccountID"))
    '        '        command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
    '        '        command.Parameters.AddWithValue("@Comments", dt.Rows(i)("Comments"))
    '        '        command.Parameters.AddWithValue("@AccountDate", dt.Rows(i)("SalesDate"))
    '        '        command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
    '        '        command.Parameters.AddWithValue("@CustName", dt.Rows(i)("CustName"))
    '        '        command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
    '        '        command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
    '        '        command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
    '        '        command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
    '        '        command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

    '        '        command.Connection = conn

    '        '        command.ExecuteNonQuery()
    '        '        command.Dispose()

    '        '    Next

    '        '    command1.Dispose()
    '        '    dt.Clear()
    '        '    dt.Dispose()
    '        '    dr.Close()

    '        'Else
    '        '    Dim command As MySqlCommand = New MySqlCommand

    '        '    command.CommandType = CommandType.Text
    '        '    Dim qry As String = "INSERT INTO tblar22(AccountId,JobOrder) VALUES (@AccountId,@JobOrder);"

    '        '    command.CommandText = qry
    '        '    command.Parameters.Clear()


    '        '    command.Parameters.AddWithValue("@AccountId", txtAccountID.Text)
    '        '    command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))

    '        '    command.Connection = conn

    '        '    command.ExecuteNonQuery()
    '        '    command.Dispose()

    '        'End If

    '        'For J As Int16 = 0 To 1
    '        '    Dim command2 As MySqlCommand = New MySqlCommand

    '        '    command2.CommandType = CommandType.Text

    '        '    If J = 0 Then
    '        '        command2.CommandText = txtQueryRecv.Text
    '        '    ElseIf J = 1 Then
    '        '        command2.CommandText = txtQueryRecv1.Text
    '        '    End If

    '        '    command2.Connection = conn

    '        '    Dim dr2 As MySqlDataReader = command2.ExecuteReader()
    '        '    Dim dt2 As New DataTable
    '        '    dt2.Load(dr2)

    '        '    If dt2.Rows.Count > 0 Then
    '        '        For i As Int16 = 0 To dt2.Rows.Count - 1
    '        '            Dim command As MySqlCommand = New MySqlCommand

    '        '            command.CommandType = CommandType.Text
    '        '            Dim qry As String = "INSERT INTO tblar22(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName,LedgerCode,Status,Currency,SubLedgerCode,CustBranchCode) VALUES (@VoucherDate,@Type,@VoucherNumber,@Debit,@Credit,@Balance,@ContactType,@AccountId,@StatementDate,@Comments,@AccountDate,@JobOrder,@CustName,@LedgerCode,@Status,@Currency,@SubLedgerCode,@CustBranchCode);"

    '        '            command.CommandText = qry
    '        '            command.Parameters.Clear()

    '        '            command.Parameters.AddWithValue("@VoucherDate", dt2.Rows(i)("ReceiptDate"))
    '        '            command.Parameters.AddWithValue("@Type", "RECEIPT")
    '        '            command.Parameters.AddWithValue("@VoucherNumber", dt2.Rows(i)("Cheque"))
    '        '            command.Parameters.AddWithValue("@Debit", 0)
    '        '            command.Parameters.AddWithValue("@Credit", dt2.Rows(i)("AppliedBase"))
    '        '            command.Parameters.AddWithValue("@Balance", -(dt2.Rows(i)("AppliedBase")))
    '        '            command.Parameters.AddWithValue("@ContactType", dt2.Rows(i)("ContactType"))
    '        '            command.Parameters.AddWithValue("@AccountId", dt2.Rows(i)("AccountID"))
    '        '            command.Parameters.AddWithValue("@StatementDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))
    '        '            command.Parameters.AddWithValue("@Comments", dt2.Rows(i)("Comments"))
    '        '            command.Parameters.AddWithValue("@AccountDate", dt2.Rows(i)("ReceiptDate"))
    '        '            command.Parameters.AddWithValue("@JobOrder", Convert.ToString(Session("UserID")))
    '        '            command.Parameters.AddWithValue("@CustName", dt2.Rows(i)("ReceiptFrom"))
    '        '            command.Parameters.AddWithValue("@LedgerCode", DBNull.Value.ToString)
    '        '            command.Parameters.AddWithValue("@Status", DBNull.Value.ToString)
    '        '            command.Parameters.AddWithValue("@Currency", DBNull.Value.ToString)
    '        '            command.Parameters.AddWithValue("@SubLedgerCode", DBNull.Value.ToString)
    '        '            command.Parameters.AddWithValue("@CustBranchCode", DBNull.Value.ToString)

    '        '            command.Connection = conn

    '        '            command.ExecuteNonQuery()
    '        '            command.Dispose()

    '        '        Next

    '        '        command2.Dispose()
    '        '        dt2.Clear()
    '        '        dt2.Dispose()
    '        '        dr2.Close()

    '        '    End If
    '        'Next

    '        'conn.Close()

    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("InserM02AR22", ex.Message.ToString, "")

    '    End Try

    'End Sub
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
            insCmds.Parameters.AddWithValue("@LoginId", "Cust_SOA - " + Convert.ToString(Session("UserID")))
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            '   lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
    End Sub


    Private Sub GetDataInvRecvTest(AccountID As String, AccountType As String, CutOffDate As String, UserID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '    InsertIntoTblWebEventLog("GetDataInvRecvTest-1", AccountID, UserID)

            Dim CompanyName As String = System.Configuration.ConfigurationManager.AppSettings("CompanyName").ToString

            Dim CustName As String = ""

            Dim command0 As MySqlCommand = New MySqlCommand

            command0.CommandType = CommandType.Text
            Dim qry As String = "delete from tbwar22autoemail where Joborder='" & UserID & "'"

            command0.CommandText = qry

            command0.Connection = conn

            command0.ExecuteNonQuery()
            command0.Dispose()

            InsertIntoTblWebEventLog("Page_Load", "arg", txtAccountID.Text)
            GetDataInvRecv(conn, AccountID, AccountType, CutOffDate, UserID)
            InsertIntoTblWebEventLog("Page_Load", "sen", txtAccountID.Text)
            ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "SaveTbwAR2AutoEMail"

            command.Parameters.Clear()

            '  command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"))
            command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(CutOffDate).ToString("yyyy-MM-dd"))

            command.Parameters.AddWithValue("@pr_CreatedBy", UserID)
            command.Parameters.AddWithValue("@pr_AccountType", "")
            command.Parameters.AddWithValue("@pr_AccountIDFrom", AccountID)

            command.Parameters.AddWithValue("@pr_AccountIDTo", AccountID)


            command.Connection = conn
            command.CommandTimeout = 0
            command.ExecuteScalar()

            command.Dispose()

            InsertIntoTblWebEventLog("Page_Load", "sen1", txtAccountID.Text)
            Dim command5 As MySqlCommand = New MySqlCommand
            command5.CommandType = CommandType.StoredProcedure

            command5.CommandText = "SaveTbwAR2AutoEmail2"
            command5.Parameters.Clear()

            '  command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"))
            command5.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(CutOffDate).ToString("yyyy-MM-dd"))

            command5.Parameters.AddWithValue("@pr_CreatedBy", UserID)
            command5.Connection = conn
            command5.CommandTimeout = 0

            command5.ExecuteScalar()
            command5.Dispose()

            conn.Close()
            conn.Dispose()

            InsertIntoTblWebEventLog("Page_Load", "sen2", txtAccountID.Text)
            '  InsertIntoTblWebEventLog("GetDataInvRecvTest-2", AccountID, UserID)


            '  InsertIntoTblWebEventLog("GetDataInvRecvTest-3", AccountID, UserID)

            'Dim DueInv As String = ""
            'Dim OverDueInv As String = ""
            'Dim totDueInv As Decimal = 0
            'Dim totOverDueInv As Decimal = 0
            'Dim ContentDue As String = ""
            'Dim ContentOverDue As String = ""

            'Dim command1 As MySqlCommand = New MySqlCommand

            'command1.CommandType = CommandType.Text

            '' command1.CommandText = "SELECT * FROM tbwosageingautoemail where createdby='" + UserID + "'"
            'command1.CommandText = "SELECT * FROM tbwar22autoemail a left join tbwosageingautoemail b on a.vouchernumber=b.InvoiceNumber where a.balance<>0 and a.joborder='" + UserID + "' and b.CreatedBy='" + UserID + "';"
            'command1.Connection = conn

            'Dim dr As MySqlDataReader = command1.ExecuteReader()

            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then
            '    CustName = dt.Rows(0)("CustName").ToString

            '    ContentDue = "<table cellspacing=""5""><tr><th>DueDate</th><th>  </th><th>InvoiceNumber</th><th>Amount</th></tr>"
            '    ContentOverDue = "<table cellspacing=""5""><tr><th>DueDate</th><th>  </th><th>InvoiceNumber</th><th>Amount</th></tr>"

            '    '  ContentOverDue = "<table><tr><th width=""10"">DueDate</th><th width=""10"">InvoiceNumber</th><th width=""10"">Amount</th></tr>"

            '    For i As Integer = 0 To dt.Rows.Count - 1
            '        If dt.Rows(i)("Current").ToString <> "0.00" Then
            '            DueInv = DueInv + "<br/>" + dt.Rows(i)("InvoiceNumber").ToString + " - " + dt.Rows(i)("UnpaidBalance").ToString + " - " + Convert.ToDateTime(dt.Rows(i)("DueDate")).ToString("dd/MM/yyyy")
            '            totDueInv = totDueInv + Convert.ToDecimal(dt.Rows(i)("UnpaidBalance"))

            '            ContentDue = ContentDue + "<tr><td align=""center"">" + Convert.ToDateTime(dt.Rows(i)("DueDate")).ToString("dd/MM/yyyy") + "</td><td></td><td align=""justify"">" + dt.Rows(i)("InvoiceNumber").ToString + "</td><td align=""right"">" + Convert.ToDecimal(dt.Rows(i)("UnpaidBalance")).ToString("#,##0.00") + "</td></tr>"

            '        Else
            '            OverDueInv = OverDueInv + "<br/>" + dt.Rows(i)("InvoiceNumber").ToString + " - " + dt.Rows(i)("UnpaidBalance").ToString + " - " + Convert.ToDateTime(dt.Rows(i)("DueDate")).ToString("dd/MM/yyyy")
            '            totOverDueInv = totOverDueInv + Convert.ToDecimal(dt.Rows(i)("UnpaidBalance"))

            '            ContentOverDue = ContentOverDue + "<tr><td align=""center"">" + Convert.ToDateTime(dt.Rows(i)("DueDate")).ToString("dd/MM/yyyy") + "</td><td></td><td align=""justify"">" + dt.Rows(i)("InvoiceNumber").ToString + "</td><td align=""right"">" + Convert.ToDecimal(dt.Rows(i)("UnpaidBalance")).ToString("#,##0.00") + "</td></tr>"
            '        End If
            '    Next
            '    ContentDue = ContentDue + "<tr><td colspan=""4""><b><font size=""+1"">Due Amount : SGD " + totDueInv.ToString("#,##0.00") + "</font></b></td></tr>"
            '    ContentDue = ContentDue + "</table>"

            '    ContentOverDue = ContentOverDue + "<tr><td colspan=""4""><b><font color=""red"" size=""+1"">Overdue Amount : SGD " + totOverDueInv.ToString("#,##0.00") + "</font></b></td></tr>"
            '    ContentOverDue = ContentOverDue + "</table>"
            'End If

            'command1.Dispose()
            'dt.Dispose()
            'dr.Close()

            'If totDueInv = 0 And totOverDueInv = 0 Then
            '    UpdateSOALog(conn, "0", AccountID, AccountType, UserID, "No Due Invoices")

            '    Return

            'End If

            'Dim content As String = ""
            ' '' content = content + "To Our Valued Customer,<br/><br/>"
            ''content = content + "To " + AccountID + " - " + CustName + "<br/><br/>"
            ''content = content + "This is to highlight to your attention that your account with " + CompanyName + " has the following invoices which are <b>overdue</b> and <b>due for payment</b>.<br/><br/>"

            'If totOverDueInv <> 0 Then
            '    content = content + "<b><u>Overdue Invoices</u></b>"
            '    content = content + "<br/>"
            '    '  content = content + "<br/>"
            '    '  content = content + OverDueInv
            '    content = content + ContentOverDue
            '    'content = content + "<br/>"
            '    'content = content + "<b><font color=""red"">Overdue Amount : SGD " + totOverDueInv.ToString("#,##0.00") + "</font></b>"
            '    content = content + "<br/>"
            '    content = content + "<br/>"

            'End If

            'If totDueInv <> 0 Then
            '    content = content + "<b><u>Due Invoices</u></b>"
            '    content = content + "<br/>"
            '    '    content = content + "<br/>"
            '    ' content = content + DueInv
            '    content = content + ContentDue
            '    'content = content + "<br/>"
            '    'content = content + "<b>Due Amount : SGD " + totDueInv.ToString("#,##0.00") + "</b>"

            '    content = content + "<br/>"
            '    content = content + "<br/>"
            'End If
            'Dim totamt As Decimal = totDueInv + totOverDueInv

            'content = content + "<hr><b><font size=""+2"">Total Amount Due : " + totamt.ToString("#,##0.00") + "</font></b><br/>"


            ''content = content + "Please pay full amount on or before the due date to avoid disruption of services.<br/>"
            ''content = content + "Take note that admin charges may apply for reactivation of pest control services due non-payment.<br/>"
            ''content = content + "(If the account has already been settled, please ignore this email).<br/><br/>"
            ''content = content + "Note: This is a system generated email, no signature is required."
            ''content = content + "<br/>"
            ''content = content + "<br/>"
            ''content = content + "Thank you,<br/>"
            ''content = content + CompanyName
            ''content = content + "<br/>"

            'GenerateAttachmentSOA(AccountID, CutOffDate, UserID)

            ' ''''''''''''''GET EMAIL TEMPLATE''''''''''''''''''''''

            'Dim commandEm As MySqlCommand = New MySqlCommand

            'commandEm.CommandType = CommandType.Text

            'commandEm.Connection = conn

            'commandEm.CommandText = "Select * from tblEmailSetUp where SetUpID='AUTO-OSINV'"


            'Dim drEm As MySqlDataReader = commandEm.ExecuteReader()
            'Dim dtEm As New DataTable
            'dtEm.Load(drEm)

            'Dim subject As String = ""
            'Dim content1 As String = ""
            'Dim ReplySender As String = ""

            'If dtEm.Rows.Count > 0 Then
            '    subject = dtEm.Rows(0)("Subject").ToString
            '    content1 = dtEm.Rows(0)("Contents").ToString
            '    ReplySender = dtEm.Rows(0)("Sender").ToString
            'End If

            'subject = System.Text.RegularExpressions.Regex.Replace(subject, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            'subject = subject.Replace("CUSTNAME", CustName)
            'subject = subject.Replace("ACCOUNTID", AccountID)

            'content1 = System.Text.RegularExpressions.Regex.Replace(content1, "\{\*?\\[^{}]+}|[{}]|\\\n?[A-Za-z]+\n?(?:-?\d+)?[ ]?", "")
            'content1 = content1.Replace("CUSTNAME", CustName)
            'content1 = content1.Replace("ACCOUNTID", AccountID)
            'content1 = content1.Replace("COMPANYNAME", CompanyName)
            'content1 = content1.Replace("DUECONTENT", content)
            'If totOverDueInv = 0 Then
            '    content1 = content1.Replace("OVERDUESTMT", "")
            '    content1 = content1.Replace("OVERDUEAMT", "")
            'Else
            '    content1 = content1.Replace("OVERDUESTMT", "Your current total")
            '    content1 = content1.Replace("OVERDUEAMT", "overdue amount is SGD " & totOverDueInv)

            'End If
            'content1 = content1.Replace("SYSDATE", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt", New System.Globalization.CultureInfo("en-GB")))


            'dtEm.Clear()
            'drEm.Close()
            'dtEm.Dispose()
            'commandEm.Dispose()

            ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Dim oMail As New SmtpMail(ConfigurationManager.AppSettings("EmailLicense").ToString())
            'Dim oSmtp As New SmtpClient()

            'oMail.Subject = subject

            'oMail.HtmlBody = content1

            'Dim oServer As New SmtpServer(ConfigurationManager.AppSettings("EmailSMTP").ToString())
            'oServer.Port = ConfigurationManager.AppSettings("EmailPort").ToString()
            'oServer.ConnectType = SmtpConnectType.ConnectDirectSSL

            'oMail.From = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()

            'InsertIntoTblWebEventLog("SOA - ToEmailID", ToEmailID, AccountID)

            ''ToEmailID = "Christian.Reyes@anticimex.com.sg"
            ''ToEmailID = "sasi.vishwa@gmail.com"

            'ToEmailID = ValidateEmail(ToEmailID)
            'If ToEmailID.Last.ToString = ";" Then
            '    ToEmailID = ToEmailID.Remove(ToEmailID.Length - 1)

            'End If

            'If ToEmailID.First.ToString = ";" Then
            '    ToEmailID = ToEmailID.Remove(0)

            'End If

            'Dim pattern As String
            'pattern = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

            'If String.IsNullOrEmpty(ToEmailID) = False Then
            '    Dim ToAddress As String() = ToEmailID.Split(";"c)
            '    If ToAddress.Count() > 0 Then
            '        For i As Integer = 0 To ToAddress.Count() - 1
            '            If Regex.IsMatch(ToAddress(i).ToString.Trim, pattern) Then

            '            Else
            '                '  MessageBox.Message.Alert(Page, "Enter valid 'TO' Email address" + " (" + ToAddress(i).ToString() + ")", "str")
            '                UpdateSOALog(conn, "0", AccountID, AccountType, UserID, "INVALID EMAIL ADDRESS")

            '                Return
            '            End If
            '            oMail.[To].Add(New MailAddress(ToAddress(i).ToString.Trim))
            '        Next
            '    End If
            'End If
            'oMail.Bcc = "SG.SERVICEREPORT@ANTICIMEX.COM.SG"
            'oMail.ReplyTo = "accounts@anticimex.com.sg"

            'oServer.User = ConfigurationManager.AppSettings("EmailInvoiceFrom").ToString()
            'oServer.Password = ConfigurationManager.AppSettings("EmailInvoicePassword").ToString()
            'oMail.AddAttachment(AppDomain.CurrentDomain.BaseDirectory + "PDFs\" + AccountID + "_SOA.PDF")
            'oSmtp.SendMail(oServer, oMail)
            '' Delete pdf attachment

            'System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "PDFs\" + AccountID + "_SOA.PDF")
            'InsertIntoTblWebEventLog("AUTOEMAIL SOA - EMAILED", AccountID, UserID)

            'UpdateSOALog(conn, "1", AccountID, AccountType, UserID, "SOA - Sent")


            ''SOA Email Interval

            'Dim commandInterval As MySqlCommand = New MySqlCommand

            'commandInterval.CommandType = CommandType.Text

            'commandInterval.CommandText = "SELECT SOAEmailInterval FROM tblservicerecordmastersetup where rcno=1"

            'commandInterval.Connection = conn

            'Dim drInterval As MySqlDataReader = commandInterval.ExecuteReader()
            'Dim dtInterval As New DataTable
            'dtInterval.Load(drInterval)

            'Dim interval As Integer = 0

            'If dtInterval.Rows.Count > 0 Then
            '    interval = dtInterval.Rows(0)("SOAEmailInterval")
            'Else
            '    interval = 0
            'End If
            'interval = interval * 1000
            'System.Threading.Thread.Sleep(interval)

            'dtInterval.Clear()
            'dtInterval.Dispose()
            'drInterval.Close()
            'commandInterval.Dispose()

            Return

        Catch ex As Exception
            InsertIntoTblWebEventLog("GetDataInvRecvTest", ex.Message.ToString, AccountID)
        End Try
    End Sub

    Private Function GetDataInvRecv(Conn As MySqlConnection, AccountID As String, AccountType As String, CutOffDate As String, UserID As String) As Boolean
        Dim qry As String = ""
        Dim qryrecv As String = ""
        Dim qryrecv1 As String = ""

        qry = "SELECT AccountId, InvoiceNumber, SalesDate, ValueBase, GstBase,appliedbase, CreditBase, ReceiptBase,"
        qry = qry + "BalanceBase,"
        qry = qry + "StaffCode,comments,contacttype, CustName, CustAttention, CustAddress1, CustAddStreet, CustAddBuilding, CustAddPostal, CustAddCountry, TermsDay, PoNo, Rcno FROM tblsales"
        'qry = qry + " where balancebase <> 0 and doctype='ARIN' and poststatus='P'"
        'qry = qry + " where balancebase <> 0  and poststatus='P'"
        qry = qry + " where  poststatus='P'"
        qry = qry + " and tblsales.Accountid = '" + AccountID + "'"
        qry = qry + " and tblsales.salesdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

        '    End If
        qryrecv = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,tblrecvdet.appliedbase,tblrecvdet.valuebase"
        'qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) and BankID<>'CONTRA'"
        qryrecv = qryrecv + ",tblrecvdet.gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
        qryrecv = qryrecv + " and tblrecv.Accountid = '" + AccountID + "'"
        qryrecv = qryrecv + " and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"

        qryrecv1 = "select tblrecv.receiptnumber,tblrecv.receiptdate,tblrecv.receiptfrom,tblrecv.contacttype,tblrecv.comments,tblrecv.baseamount,tblrecv.accountid,-tblrecvdet.appliedbase as appliedbase,-tblrecvdet.valuebase as valuebase"
        'qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where (tblrecvdet.reftype='' or tblrecvdet.reftype=null) AND BankID='CONTRA'"
        qryrecv1 = qryrecv1 + ",-tblrecvdet.gstbase as gstbase,tblrecv.cheque, tblrecvdet.rcno from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber where 1=1 "
        qryrecv1 = qryrecv1 + " and tblrecv.Accountid = '" + AccountID + "'"
        qryrecv1 = qryrecv1 + " and tblrecv.receiptdate <= '" + Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd") + "'"



        qry = qry + " ORDER BY AccountId, InvoiceNumber"
        qryrecv = qryrecv + " ORDER BY AccountId, tblrecv.ReceiptNumber"
        qryrecv1 = qryrecv1 + " ORDER BY AccountId, tblrecv.ReceiptNumber"




        InsertM02AR22(Conn, AccountID, AccountType, CutOffDate, qry, qryrecv, qryrecv1, UserID)

        Return True
    End Function

    Private Sub InsertM02AR22(conn As MySqlConnection, AccountID As String, AccountType As String, CutOffDate As String, Query As String, QueryRecv As String, QueryRecv1 As String, UserID As String)
        Try

            Dim sqlst, sqlst1, isWhere, isWhere1 As String
            Dim inIsWhere, inIsWhere1 As Integer

            sqlst = ""
            sqlst1 = ""

            isWhere = Query
            inIsWhere = isWhere.IndexOf("where")

            'txtCustName.Text = ""

            sqlst = "insert into tbwar22autoemail(VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT SalesDate, 'INVOICE',  InvoiceNumber, appliedbase, (CreditBase+ReceiptBase), OPeriodBalance, contacttype, AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' ,comments, SalesDate,'" & UserID & "', CustName, InvoiceNumber, Rcno, 'ARIN' from tblSales "
            sqlst = sqlst + " " + Query.Substring(inIsWhere)

            ''''''''''''''''''''''''
            'txtCustName.Text = txtQueryRecv.Text
            isWhere1 = QueryRecv
            inIsWhere1 = isWhere1.IndexOf("where")

            'txtCustName.Text = ""

            sqlst1 = "insert into tbwar22autoemail (VoucherDate,Type,VoucherNumber,Debit,Credit,Balance,ContactType,AccountId,StatementDate,Comments,AccountDate,JobOrder,CustName, ReferenceNumber, ModuleRcno, DocumentType) SELECT tblrecv.ReceiptDate, 'RECEIPT',  tblrecv.Cheque, 0, tblrecvdet.appliedbase, tblrecvdet.appliedbase * (-1), tblrecv.contacttype, tblrecv.AccountId, '" & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")) & "' , tblrecv.comments, tblrecv.ReceiptDate,'" & UserID & "', tblrecv.ReceiptFrom, tblrecv.ReceiptNumber, tblRecvdet.Rcno, 'RECV' from tblrecv left outer join tblrecvdet on tblrecv.ReceiptNumber=tblrecvdet.receiptnumber  "
            sqlst1 = sqlst1 + " " + QueryRecv.Substring(inIsWhere1)

            ''''''''''''''''''''''''''

            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "SaveTbwAR22AutoEmail"

            command.Parameters.Clear()

            'command.Parameters.AddWithValue("@pr_CutOffdate", Convert.ToDateTime(txtCutOffDate.Text).ToString("yyyy-MM-dd"))
            'command.Parameters.AddWithValue("@pr_Query", txtQuery.Text)
            command.Parameters.AddWithValue("@pr_Query", sqlst)
            command.Parameters.AddWithValue("@pr_Query1", sqlst1)
            command.Parameters.AddWithValue("@pr_CreatedBy", UserID)
            command.Connection = conn
            command.ExecuteScalar()
            command.CommandTimeout = 0

            command.Dispose()

        Catch ex As Exception
            InsertIntoTblWebEventLog("CustSOA - InserM02AR22", ex.Message.ToString, AccountID)

        End Try

    End Sub

   
End Class

