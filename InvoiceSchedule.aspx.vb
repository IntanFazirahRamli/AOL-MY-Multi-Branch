Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web

' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading
Imports System.Drawing


Partial Class InvoiceSchedule
    Inherits System.Web.UI.Page
    Public rcno As String
    Private Shared GridSelected As String = String.Empty
    Private Shared gScheduler, gSalesman As String
    'Public conn As MySqlConnection = New MySqlConnection()
    Dim constr As String = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    Dim client As String
    'Public rcno As String

    Public TotDetailRecords As Integer

    Dim gSeq As String
    Dim gServiceDate As Date

    Dim rowdeleted As Boolean
    Dim RowNumber As Integer
    Dim RowIndexSch As Integer

    Dim mode As String
    Dim vStrStatus As String

    Public HasDuplicateTarget As Boolean
    Public HasDuplicateLocaion As Boolean
    Public HasDuplicateServices As Boolean
    Public HasDuplicateFrequency As Boolean

    Public xgrvBillingDetails As GridViewRow

    Public lGLCode As String
    Public lGLDescription As String
    Public lCreditAmount As Decimal

    Public gContractNos As String
    Public gTargetIds As String

    Public gBillNo As String
    Dim rowIndex As Integer

    Public grcnoservicebillingdetail As Long
    Public gContactPerson As String
    Public IsSuccess As Boolean

    Public ButtonPressed As Char

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()

      
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim Query As String

        'Restrict users manual date entries
        Try
            txtAccountIdBilling.Attributes.Add("readonly", "readonly")
            txtAccountName.Attributes.Add("readonly", "readonly")
            txtBillAddress.Attributes.Add("readonly", "readonly")
            txtBillBuilding.Attributes.Add("readonly", "readonly")
            txtBillStreet.Attributes.Add("readonly", "readonly")
            txtBillCountry.Attributes.Add("readonly", "readonly")
            txtBillPostal.Attributes.Add("readonly", "readonly")
            txtTotal.Attributes.Add("readonly", "readonly")
            txtTaxRatePct.Attributes.Add("readonly", "readonly")
            txtInvoiceNo.Attributes.Add("readonly", "readonly")
            txtBatchDate.Attributes.Add("readonly", "readonly")

            txtBillingPeriod.Attributes.Add("readonly", "readonly")
            txtCompanyGroup.Attributes.Add("readonly", "readonly")
            txtAccountType.Attributes.Add("readonly", "readonly")
            txtInvoiceAmount.Attributes.Add("readonly", "readonly")
            txtDiscountAmount.Attributes.Add("readonly", "readonly")
            txtAmountWithDiscount.Attributes.Add("readonly", "readonly")

            txtGSTAmount.Attributes.Add("readonly", "readonly")
            txtNetAmount.Attributes.Add("readonly", "readonly")

            txtBatchDate.Attributes.Add("readonly", "readonly")
            txtTotalSelected.Attributes.Add("readonly", "readonly")


            'btnSaveInvoice.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSaveInvoice, Nothing) + ";")

            'txtCreatedOn.Attributes.Add("readonly", "readonly")
            'txtServTimeOut.Attributes.Add("onchange", "getTheDiffTime()")

            'btnSaveInvoice.Attributes.Add("onclick", " this.disabled = true; " & ClientScript.GetPostBackEventReference(btnSaveInvoice, Nothing) & ";")


            If Not Page.IsPostBack Then
                mdlPopUpClient.Hide()
                mdlPopupLocation.Hide()

                MakeMeNull()
                DisableControls()

                'Session.Add("CheckRefresh", Server.UrlDecode(System.DateTime.Now.ToString()))

                Dim Query As String
                'Query = "Select StaffId from tblStaff where (SecGroupAuthority like  'SCHEDULER%' or  SecGroupAuthority like '%ADMINISTRATOR%') and Status = 'O'"
                Query = "Select StaffId from tblStaff where SecGroupAuthority <> 'GUEST' and Status = 'O'"
                PopulateDropDownList(Query, "StaffId", "StaffId", ddlScheduler)

                txtPreviousBillSchedule.Attributes.Add("readonly", "readonly")
                '''''''''''''''''''''''''''''''
                txtGroupAuthority.Text = Session("SecGroupAuthority")

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Dispose()
                End If
                conn.Open()

                ''''''''''''''''''''''''''''''''''''''''''''''''
                Dim commandServiceRecordMasterSetup As MySqlCommand = New MySqlCommand
                commandServiceRecordMasterSetup.CommandType = CommandType.Text
                commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise, DefaultTaxCode FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceRecordMaxRec")
                txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                txtDefaultTaxCode.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString

                'conn.Close()
                'conn.Dispose()
                ''''''''''''''''''''''''''''''''''''''''''

                If String.IsNullOrEmpty(Session("invoicefrom")) = False Then
                    btnADD_Click(sender, e)
                    txtInvoiceNo.Text = Session("invoiceno")
                    txtInvoicenoSearch.Text = Session("invoiceno")
                    txtRcno.Text = Session("rcnoschedule")

                    SQLDSInvoice.SelectCommand = Session("gridsqlschedule")
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()


                    GridView1_SelectedIndexChanged(New Object(), New EventArgs)
                    Session.Remove("invoiceno")
                    Session.Remove("rcnoschedule")
                    Session.Remove("invoicefrom")
                End If

                ''''''''''''''''''''''''''''''''''
                'Dim conn As MySqlConnection = New MySqlConnection()

                'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                'If conn.State = ConnectionState.Open Then
                '    conn.Close()
                '    conn.Dispose()
                'End If
                'conn.Open()

                Dim sql As String
                sql = ""
                sql = "Select TaxRatePct from tbltaxtype where TaxType  = '" & txtDefaultTaxCode.Text & "'"

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

                '20.12.22
                FindGSTPct()

                '20.12.22

                'txtLocation.Attributes.Add("disabled", "true")
                txtCreatedBy.Text = Session("userid")
                FindLocation()
                'btnReceipts_Click(sender, e)

                '''''''''''''''''''''''''''''

                conn.Close()
                conn.Dispose()
                command1.Dispose()
                dt.Dispose()

                'If conn.State = ConnectionState.Open Then
                '    conn.Close()
                'End If
                'MakeMeNull()
                'DisableControls()

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE Billschedule in (select BillSchedule from tblservicebillingdetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()

                    'Query = "select locationID from tblStaff where StaffID = '" & txtCreatedBy.Text & "'"
                    'PopulateDropDownList(Query, "locationID", "locationID", txtLocation)

                    Query = "select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
                    PopulateComboBox(Query, "locationID", "locationID", ddlLocationSearch)

                    Query = "select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
                    PopulateDropDownList(Query, "locationID", "locationID", txtLocation)

                    lblBranch1.Visible = True
                    lblBranch2.Visible = True
                    txtLocation.Visible = True
                    ddlLocationSearch.Visible = True
                    Label48.Visible = True

                Else
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()

                    lblBranch1.Visible = False
                    lblBranch2.Visible = False
                    txtLocation.Visible = False
                    ddlLocationSearch.Visible = False
                    Label48.Visible = False
                End If
              
                updPnlBillingRecs.Update()


                '''''''''''''''''''''''''''''''' Recurring Invoice

                calculateRecurringInvoice()   'quoted on 11.04.17 for display error

                'Dim sql2 As String
                'sql2 = ""

                'txtCreatedOn.Text = (Session("SysDate"))
                'sql2 = "Select count(*) as totrecurring from tblservicebillschedule where RecurringInvoice = 'Y' and NextInvoiceDate = '" & Convert.ToDateTime(Session("SysDate")).ToString("yyyy-MM-dd") & "'"

                'Dim command2 As MySqlCommand = New MySqlCommand
                'command2.CommandType = CommandType.Text
                'command2.CommandText = sql2
                'command2.Connection = conn

                'Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                'Dim dt2 As New DataTable
                'dt2.Load(dr2)

                'If dt2.Rows.Count > 0 Then
                '    btnADDRecurring.Text = "CREATE RECURRING [" & dt2.Rows(0)("totrecurring").ToString & "]"
                'End If



                '''''''''''''''''''''''''''''''' Recurring Invoice

                txt.Text = SQLDSInvoice.SelectCommand
            Else
                If txtIsPopup.Text = "Team" Then
                    txtIsPopup.Text = "N"
                    'mdlPopUpTeam.Show()
                ElseIf txtIsPopup.Text = "Client" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpClient.Show()
                ElseIf txtIsPopup.Text = "Staff" Then
                    txtIsPopup.Text = "N"
                    mdlPopupStaff.Show()
                ElseIf txtIsPopup.Text = "ContractNo" Then
                    txtIsPopup.Text = "N"
                    mdlPopUpContractNo.Show()
                End If

                'If txtSearch.Text = "ContractNoSearch" Then
                '    txtSearch.Text = "N"
                '    mdlPopUpContractNo.Show()

                'End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "Page_load", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub FindDefaultTaxCodeandPctFromPeriod(BillingPeriod As String)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim commandPeriod As MySqlCommand = New MySqlCommand
            commandPeriod.CommandType = CommandType.Text

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                commandPeriod.CommandText = "SELECT GStType, GSTRate FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            Else
                commandPeriod.CommandText = "SELECT GStType, GSTRate FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            End If

            commandPeriod.Connection = conn

            Dim drPeriod As MySqlDataReader = commandPeriod.ExecuteReader()
            Dim dtPeriod As New DataTable
            dtPeriod.Load(drPeriod)

            If String.IsNullOrEmpty(dtPeriod.Rows(0)("GStType").ToString) = False Then
                txtDefaultTaxCode.Text = dtPeriod.Rows(0)("GStType").ToString
                UpdatePanel3.Update()
            End If
            'txtDefaultTaxCode.Text = dtPeriod.Rows(0)("GStType").ToString


            ''''''''''''''''''''''''''''''''''''''''''

            Dim sql As String
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
                If dt.Rows(0)("TaxRatePct").ToString <> "" Then
                    txtTaxRatePct.Text = dt.Rows(0)("TaxRatePct").ToString
                    UpdatePanel3.Update()
                End If
                'txtGST1.Text = dt.Rows(0)("TaxRatePct").ToString
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "FindDefaultTaxCodeandPctFromPeriod", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            Exit Sub
        End Try
    End Sub


    Private Sub FindDefaultTaxCodeandPctFromContractGroup(ContractGroup As String)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim commandPeriod As MySqlCommand = New MySqlCommand
            commandPeriod.CommandType = CommandType.Text

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                commandPeriod.CommandText = "SELECT TaxType FROM tblContractGroup where ContractGroup='" & ContractGroup & "'"
            Else
                commandPeriod.CommandText = "SELECT TaxType FROM tblContractGroup where ContractGroup='" & ContractGroup & "'"
            End If

            commandPeriod.Connection = conn

            Dim drPeriod As MySqlDataReader = commandPeriod.ExecuteReader()
            Dim dtPeriod As New DataTable
            dtPeriod.Load(drPeriod)

            If String.IsNullOrEmpty(dtPeriod.Rows(0)("TaxType").ToString) = False Then
                txtDefaultTaxCode.Text = dtPeriod.Rows(0)("TaxType").ToString
                UpdatePanel3.Update()
            End If


            ''''''''''''''''''''''''''''''''''''''''''

            Dim sql As String
            sql = ""


            'sql = "Select TaxRatePct from tbltaxtype where TaxType = 'SR'"
            'txtGST.Text = txtDefaultTaxCode.Text

            'sql = "Select TaxRatePct from tbltaxtype where TaxType = 'ZR'"
            'txtGST.Text = "ZR"


            'txtGSTSelected.Text = txtGST.Text

            sql = "Select TaxRatePct from tbltaxtype where TaxType = '" & txtDefaultTaxCode.Text & "'"

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("TaxRatePct").ToString <> "" Then
                    txtTaxRatePct.Text = dt.Rows(0)("TaxRatePct").ToString
                    UpdatePanel3.Update()
                End If
                'txtGST1.Text = dt.Rows(0)("TaxRatePct").ToString
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "FindDefaultTaxCodeandPctFromContractGroup", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            Exit Sub
        End Try
    End Sub

    Private Sub calculateRecurringInvoice()
        ' '''''''''''''''''''''''''''''''' Recurring Invoice

        ''calculateRecurringInvoice()

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'If conn.State = ConnectionState.Open Then
        '    conn.Close()
        'End If
        'conn.Open()
        'Dim sql2 As String
        'sql2 = ""

        'txtCreatedOn.Text = (Session("SysDate"))
        ''sql2 = "Select count(*) as totrecurring from tblservicebillschedule where RecurringInvoice = 'Y' and RecurringScheduled = 'N' and NextInvoiceDate <= '" & Convert.ToDateTime(Session("SysDate")).ToString("yyyy-MM-dd") & "'"
        'sql2 = "Select count(*) as totrecurring from tblservicebillschedule where RecurringInvoice = 'Y' and RecurringScheduled = 'N' and NextInvoiceDate <= '" & Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd") & "'"


        'Dim command2 As MySqlCommand = New MySqlCommand
        'command2.CommandType = CommandType.Text
        'command2.CommandText = sql2
        'command2.Connection = conn

        'Dim dr2 As MySqlDataReader = command2.ExecuteReader()

        'Dim dt2 As New DataTable
        'dt2.Load(dr2)

        'If dt2.Rows(0)("totrecurring").ToString > 0 Then
        '    btnADDRecurring.Text = "CREATE RECURRING [" & dt2.Rows(0)("totrecurring").ToString & "]"
        'Else
        '    btnADDRecurring.Enabled = False
        'End If

        'updPnlMsg.Update()

        ' '''''''''''''''''''''''''''''''' Recurring Invoice
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo

        lblAlert.Text = ""
        lblMessage.Text = ""

        If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN ADD/EDIT MODE. SAVE OR CANCEL TO PROCEED"
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

            'PopulateGLCodes()
            Dim editindex As Integer = GridView1.SelectedIndex


            If String.IsNullOrEmpty(Session("invoicefrom")) = False Then
                rcno = Session("rcnoschedule")
            Else
                rcno = GridView1.SelectedRow.Cells(1).Text.Trim
            End If

            txtRcno.Text = 0
            If String.IsNullOrEmpty(rcno) = True Then
                txtRcno.Text = "0"
            Else
                txtRcno.Text = rcno.ToString()
            End If

            'txtRcno.Text = 0
            'txtRcno.Text = GridView1.SelectedRow.Cells(13).Text.Trim


            ''''''''''''''''''''''''''''''''''

            Dim conn As MySqlConnection = New MySqlConnection()

            Try
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

            Catch ex As MySql.Data.MySqlClient.MySqlException
                lblAlert.Text = ex.Message.ToString
            End Try
            Dim sql As String
            sql = ""
            sql = "Select PostStatus, BillSchedule, BSDate, BillingDate, ContType, AccountId, ServiceFrom, ServiceTo, Frequency, GLPeriod, CustName, Support,  "
            sql = sql + "  GroupByBillingFrequency, GroupByStatus, GroupContractNo, GroupLocationID, GroupContractGroup, BatchNo, RecurringInvoice, RecurringScheduled,  TotalRecurringInvoices, NextInvoiceDate,  "
            sql = sql + " RecurringServiceStartDate, RecurringServiceEndDate, BillAmount, DiscountAmount, AmountWithDiscount, GSTAmount, NetAmount, CompanyGroupSearch, SchedulerSearch, groupfield, Location  "
            sql = sql + " FROM tblservicebillschedule "
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
                If dt.Rows(0)("PostStatus").ToString <> "" Then : txtPostStatus.Text = dt.Rows(0)("PostStatus").ToString : End If
                If dt.Rows(0)("BillSchedule").ToString <> "" Then : txtInvoiceNo.Text = dt.Rows(0)("BillSchedule").ToString : End If
                If dt.Rows(0)("BSDate").ToString <> "" Then : txtBatchDate.Text = Convert.ToDateTime(dt.Rows(0)("BSDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("BillingDate").ToString <> "" Then : txtInvoiceDate.Text = Convert.ToDateTime(dt.Rows(0)("BillingDate")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("BillAmount").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("BillAmount").ToString : End If
                If dt.Rows(0)("DiscountAmount").ToString <> "" Then : txtDiscountAmount.Text = dt.Rows(0)("DiscountAmount").ToString : End If
                If dt.Rows(0)("AmountWithDiscount").ToString <> "" Then : txtAmountWithDiscount.Text = dt.Rows(0)("AmountWithDiscount").ToString : End If
                If dt.Rows(0)("GSTAmount").ToString <> "" Then : txtGSTAmount.Text = dt.Rows(0)("GSTAmount").ToString : End If
                If dt.Rows(0)("NetAmount").ToString <> "" Then : txtNetAmount.Text = dt.Rows(0)("NetAmount").ToString : End If
                If dt.Rows(0)("BatchNo").ToString <> "" Then : txtBatchNo.Text = dt.Rows(0)("BatchNo").ToString : End If

                If dt.Rows(0)("ContType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContType").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountId.Text = dt.Rows(0)("AccountId").ToString : End If
                If dt.Rows(0)("CustName").ToString <> "" Then : txtClientName.Text = dt.Rows(0)("CustName").ToString : End If

                If dt.Rows(0)("ServiceFrom").ToString <> "" Then : txtDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("ServiceFrom")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("ServiceTo").ToString <> "" Then : txtDateTo.Text = Convert.ToDateTime(dt.Rows(0)("ServiceTo")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("Support").ToString <> "" Then : txtServiceBySearch.Text = dt.Rows(0)("Support").ToString : End If
                'If dt.Rows(0)("TotalRecurringInvoices").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("TotalRecurringInvoices").ToString : End If
                If dt.Rows(0)("NextInvoiceDate").ToString <> "" Then : txtRecurringInvoiceDate.Text = Convert.ToDateTime(dt.Rows(0)("NextInvoiceDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("RecurringServiceStartDate").ToString <> "" Then : txtRecurringServiceDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceStartDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("RecurringServiceEndDate").ToString <> "" Then : txtRecurringServiceDateTo.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceEndDate")).ToString("dd/MM/yyyy") : End If

                'If dt.Rows(0)("BillAmount").ToString <> "" Then : txtTotalServiceSelected.Text = dt.Rows(0)("BillAmount").ToString : End If


                If dt.Rows(0)("RecurringInvoice").ToString = "Y" Then
                    chkRecurringInvoice.Checked = True
                   
                Else
                    chkRecurringInvoice.Checked = False
                   
                End If

                If dt.Rows(0)("RecurringScheduled").ToString = "Y" Then
                    chkRecurringScheduled.Checked = True
                Else
                    chkRecurringScheduled.Checked = False
                End If



                If dt.Rows(0)("GroupByBillingFrequency").ToString <> "" Then : ddlBillingFrequency.Text = dt.Rows(0)("GroupByBillingFrequency").ToString : End If
                'If dt.Rows(0)("Frequency").ToString <> "" Then : ddlRecurringFrequency.Text = dt.Rows(0)("Frequency").ToString : End If

                If dt.Rows(0)("GroupByStatus").ToString = "P" Then
                    rdbCompleted.Checked = True
                ElseIf dt.Rows(0)("GroupByStatus").ToString = "O" Then
                    rdbNotCompleted.Checked = True
                Else
                    rdbAll.Checked = True
                End If

                If dt.Rows(0)("GroupContractNo").ToString <> "" Then : ddlContractNo.Text = dt.Rows(0)("GroupContractNo").ToString : End If
                If dt.Rows(0)("GroupLocationID").ToString <> "" Then : txtLocationId.Text = dt.Rows(0)("GroupLocationID").ToString : End If
                If dt.Rows(0)("GroupContractGroup").ToString <> "" Then : ddlContractGroup.Text = dt.Rows(0)("GroupContractGroup").ToString : End If
                If dt.Rows(0)("GLPeriod").ToString <> "" Then : txtBillingPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If

                If chkRecurringInvoice.Checked = True Then
                    If chkRecurringScheduled.Checked = True Then
                        btnADDRecurring.Enabled = False
                        btnADDRecurring.ForeColor = System.Drawing.Color.Gray
                    Else
                        btnADDRecurring.Enabled = True
                        btnADDRecurring.ForeColor = System.Drawing.Color.Black
                    End If
                End If


                If dt.Rows(0)("CompanyGroupSearch").ToString <> "" Then : ddlCompanyGrp.Text = dt.Rows(0)("CompanyGroupSearch").ToString : End If

                If dt.Rows(0)("groupfield").ToString = "" Then
                    rdbGrouping.SelectedIndex = 0
                Else
                    If dt.Rows(0)("groupfield").ToString = "AccountID" Then
                        rdbGrouping.SelectedIndex = 0
                    ElseIf dt.Rows(0)("groupfield").ToString = "LocationID" Then
                        rdbGrouping.SelectedIndex = 1
                    ElseIf dt.Rows(0)("groupfield").ToString = "ServiceLocationCode" Then
                        rdbGrouping.SelectedIndex = 2
                    ElseIf dt.Rows(0)("groupfield").ToString = "ContractNo" Then
                        rdbGrouping.SelectedIndex = 3
                    End If
                End If

                'If rdbGrouping.SelectedIndex = 3 Then
                '    Command.Parameters.AddWithValue("@groupfield", "ContractNo")
                'ElseIf rdbGrouping.SelectedIndex = 1 Then
                '    Command.Parameters.AddWithValue("@groupfield", "LocationID")
                'ElseIf rdbGrouping.SelectedIndex = 0 Then
                '    Command.Parameters.AddWithValue("@groupfield", "AccountID")
                'ElseIf rdbGrouping.SelectedIndex = 2 Then
                '    Command.Parameters.AddWithValue("@groupfield", "ServiceLocationCode")
                'End If


                If String.IsNullOrEmpty(dt.Rows(0)("SchedulerSearch").ToString) = True Then
                    ddlScheduler.SelectedIndex = 0
                Else
                    If dt.Rows(0)("SchedulerSearch").ToString <> "" Then : ddlScheduler.Text = dt.Rows(0)("SchedulerSearch").ToString : End If

                End If

                If String.IsNullOrEmpty(dt.Rows(0)("Location").ToString) = True Then
                    txtLocation.SelectedIndex = 0
                Else
                    If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If

                End If

                txtTotalSelected.Text = dt.Rows(0)("BillAmount").ToString
            End If

            conn.Close()
            conn.Dispose()
            dt.Dispose()
            dr.Close()

            '''''''''''''''''''''''''''''''''''



            'If (GridView1.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '    txtPostStatus.Text = ""
            'Else
            '    txtPostStatus.Text = GridView1.SelectedRow.Cells(1).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(2).Text = "&nbsp;") Then
            '    txtInvoiceNo.Text = ""
            'Else
            '    txtInvoiceNo.Text = GridView1.SelectedRow.Cells(2).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(3).Text = "&nbsp;") Then
            '    txtInvoiceDate.Text = ""
            'Else
            '    txtInvoiceDate.Text = GridView1.SelectedRow.Cells(3).Text.Trim
            'End If



            'If (GridView1.SelectedRow.Cells(15).Text = "&nbsp;") Then
            '    txtInvoiceAmount.Text = 0
            'Else
            '    txtInvoiceAmount.Text = GridView1.SelectedRow.Cells(15).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(16).Text = "&nbsp;") Then
            '    txtDiscountAmount.Text = 0
            'Else
            '    txtDiscountAmount.Text = GridView1.SelectedRow.Cells(16).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(17).Text = "&nbsp;") Then
            '    txtAmountWithDiscount.Text = 0
            'Else
            '    txtAmountWithDiscount.Text = GridView1.SelectedRow.Cells(17).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(18).Text = "&nbsp;") Then
            '    txtGSTAmount.Text = 0
            'Else
            '    txtGSTAmount.Text = GridView1.SelectedRow.Cells(18).Text.Trim
            'End If

            'If (GridView1.SelectedRow.Cells(19).Text = "&nbsp;") Then
            '    txtNetAmount.Text = 0
            'Else
            '    txtNetAmount.Text = GridView1.SelectedRow.Cells(19).Text.Trim
            'End If

            'txtRcnoServiceRecord.Text = lblid14.Text
            'txtRcnoServiceRecordDetail.Text = lblid15.Text
            'txtContractNo.Text = lblid16.Text
            'txtRcnoInvoice.Text = lblid17.Text
            'txtRowSelected.Text = rowindex1.ToString
            'txtRcnoservicebillingdetail.Text = lblid18.Text


            ''''''''''''''''''''''''''''''''''''''
            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Open()
            'Dim sql As String
            'sql = ""

            'Dim command21 As MySqlCommand = New MySqlCommand

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

            'Dim command1 As MySqlCommand = New MySqlCommand
            'command21.CommandType = CommandType.Text
            'command21.CommandText = sql
            'command21.Connection = conn

            'Dim dr21 As MySqlDataReader = command21.ExecuteReader()

            'Dim dt21 As New DataTable
            'dt21.Load(dr21)

            'If dt21.Rows.Count > 0 Then
            '    If dt21.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt21.Rows(0)("COACode").ToString : End If
            '    If dt21.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt21.Rows(0)("Description").ToString : End If
            'End If


            ' '''''''''''''''''''''''''''''''''''

            ' '''''''''''''''''''''''''''''''''''

            'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"

            'Dim command23 As MySqlCommand = New MySqlCommand
            'command23.CommandType = CommandType.Text
            'command23.CommandText = sql
            'command23.Connection = conn

            'Dim dr23 As MySqlDataReader = command23.ExecuteReader()

            'Dim dt23 As New DataTable
            'dt23.Load(dr23)

            'If dt23.Rows.Count > 0 Then
            '    If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
            '    If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
            'End If

            'updPnlBillingRecs.Update()
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Close()


            '''''''''''''''''''''''''''''''''''''''''
            txtMode.Text = "View"

            PopulateGLCodes()
            PopulateServiceGrid()
            DisplayGLGrid()

            Session.Add("BillSchedule", txtInvoiceNo.Text)

            If txtPostStatus.Text = "P" Or txtPostStatus.Text = "V" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                'btnCopy.Enabled = True
                btnChangeStatus.Enabled = True

                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray

                btnPrint.Enabled = True
                btnPost.Enabled = False
                'btnDelete.Enabled = True
                grvServiceRecDetails.Enabled = True

                btnGenerateInvoice.Enabled = False
                btnGenerateInvoice.ForeColor = System.Drawing.Color.Gray
                btnCancel.Enabled = False
                btnCancel.ForeColor = System.Drawing.Color.Gray
            Else
                btnEdit.Enabled = True
                btnEdit.ForeColor = System.Drawing.Color.Black
                'btnCopy.Enabled = True
                btnChangeStatus.Enabled = True
                btnDelete.Enabled = True
                btnDelete.ForeColor = System.Drawing.Color.Black
                btnPrint.Enabled = True
                btnPost.Enabled = True
                btnGenerateInvoice.Enabled = True
                btnGenerateInvoice.ForeColor = System.Drawing.Color.Black
                'btnDelete.Enabled = True
            End If

            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                AccessControl()
            End If


            If txtPostStatus.Text = "P" Or txtPostStatus.Text = "V" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                btnPost.Enabled = False
                btnPost.ForeColor = System.Drawing.Color.Gray
            Else

            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "GridView1_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    'Function

    Private Sub GenerateInvoiceNo()
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
            If Date.TryParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
                strdate = d.ToShortDateString
            End If

            lPrefix = Format(CDate(strdate), "yyyyMM")
            lInvoiceNo = "AR" + lPrefix + "-"
            lMonth = Right(lPrefix, 2)
            lYear = Left(lPrefix, 4)

            lPrefix = "AR" + lYear
            lSuffixVal = 0

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim commandDocControl As MySqlCommand = New MySqlCommand
            commandDocControl.CommandType = CommandType.Text
            commandDocControl.CommandText = "SELECT * FROM tbldoccontrol where prefix='" & lPrefix & "'"
            commandDocControl.Connection = conn

            Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                'Start: Continuous Number
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
                'Dim lSuffixVal2 As String
                'Dim lSuffixVal3 As String
                'Dim lSuffixVal4 As String
                'Dim lSuffixVal5 As String
                'Dim lSuffixVal6 As String
                'Dim lSuffixVal7 As String
                'Dim lSuffixVal8 As String
                'Dim lSuffixVal9 As String
                'Dim lSuffixVal10 As String
                'Dim lSuffixVal11 As String
                'Dim lSuffixVal12 As String

                lSuffixVal1 = 0
                'lSuffixVal2 = 0
                'lSuffixVal3 = 0
                'lSuffixVal4 = 0
                'lSuffixVal5 = 0
                'lSuffixVal6 = 0
                'lSuffixVal7 = 0
                'lSuffixVal8 = 0
                'lSuffixVal9 = 0
                'lSuffixVal10 = 0
                'lSuffixVal11 = 0
                'lSuffixVal12 = 0

                If lMonth = "01" Then
                    lSuffixVal1 = 1
                    'ElseIf lMonth = "02" Then
                    '    lSuffixVal2 = 1
                    'ElseIf lMonth = "03" Then
                    '    lSuffixVal3 = 1
                    'ElseIf lMonth = "04" Then
                    '    lSuffixVal4 = 1
                    'ElseIf lMonth = "05" Then
                    '    lSuffixVal5 = 1
                    'ElseIf lMonth = "06" Then
                    '    lSuffixVal6 = 1
                    'ElseIf lMonth = "07" Then
                    '    lSuffixVal7 = 1
                    'ElseIf lMonth = "08" Then
                    '    lSuffixVal8 = 1
                    'ElseIf lMonth = "09" Then
                    '    lSuffixVal9 = 1
                    'ElseIf lMonth = "10" Then
                    '    lSuffixVal10 = 1
                    'ElseIf lMonth = "11" Then
                    '    lSuffixVal11 = 1
                    'ElseIf lMonth = "12" Then
                    '    lSuffixVal12 = 1
                End If

                Dim commandDocControlInsert As MySqlCommand = New MySqlCommand

                commandDocControlInsert.CommandType = CommandType.Text
                'commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                '               "('" & lPrefix & "','M','" & lSeparator & "',6,0,0,0,0,0,0,0,0,0,0,0,0)"

                commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                         "('" & lPrefix & "','M','" & lSeparator & "',6," & lSuffixVal1 & ", 0,0,0,0,0,0,0,0,0,0,0)"

                commandDocControlInsert.Connection = conn

                Dim dr2 As MySqlDataReader = commandDocControlInsert.ExecuteReader()
                Dim dt2 As New DataTable
                dt2.Load(dr2)

                lSetWidth = 6
                lSuffixVal = 1

                'Start: Continuous Number
                '    If lMonth = "01" Then
                '        lSuffixVal = dt.Rows(0)("Period01").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period01 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "02" Then
                '        lSuffixVal = dt.Rows(0)("Period02").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString
                '        strUpdate = " Update tbldoccontrol set Period02 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "03" Then
                '        lSuffixVal = dt.Rows(0)("Period03").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period03 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "04" Then
                '        lSuffixVal = dt.Rows(0)("Period04").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period04 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "05" Then
                '        lSuffixVal = dt.Rows(0)("Period05").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period05 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "06" Then
                '        lSuffixVal = dt.Rows(0)("Period06").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period06 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "07" Then
                '        lSuffixVal = dt.Rows(0)("Period07").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period07 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "08" Then
                '        lSuffixVal = dt.Rows(0)("Period08").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString
                '        strUpdate = " Update tbldoccontrol set Period08 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "09" Then
                '        lSuffixVal = dt.Rows(0)("Period09").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period09 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "10" Then
                '        lSuffixVal = dt.Rows(0)("Period10").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period10 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "11" Then
                '        lSuffixVal = dt.Rows(0)("Period11").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString

                '        strUpdate = " Update tbldoccontrol set Period11 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    ElseIf lMonth = "12" Then
                '        lSuffixVal = dt.Rows(0)("Period12").ToString + 1
                '        lSetWidth = dt.Rows(0)("Width").ToString
                '        strUpdate = " Update tbldoccontrol set Period12 = " & lSuffixVal & " where prefix='" & lPrefix & "'"

                '    End If

                '    Dim commandDocControlEdit As MySqlCommand = New MySqlCommand

                '    commandDocControlEdit.CommandType = CommandType.Text
                '    commandDocControlEdit.CommandText = strUpdate
                '    commandDocControlEdit.Connection = conn

                '    Dim dr2 As MySqlDataReader = commandDocControlEdit.ExecuteReader()
                '    Dim dt2 As New DataTable
                '    dt2.Load(dr2)
                'Else

                '    Dim lSuffixVal1 As String
                '    Dim lSuffixVal2 As String
                '    Dim lSuffixVal3 As String
                '    Dim lSuffixVal4 As String
                '    Dim lSuffixVal5 As String
                '    Dim lSuffixVal6 As String
                '    Dim lSuffixVal7 As String
                '    Dim lSuffixVal8 As String
                '    Dim lSuffixVal9 As String
                '    Dim lSuffixVal10 As String
                '    Dim lSuffixVal11 As String
                '    Dim lSuffixVal12 As String

                '    lSuffixVal1 = 0
                '    lSuffixVal2 = 0
                '    lSuffixVal3 = 0
                '    lSuffixVal4 = 0
                '    lSuffixVal5 = 0
                '    lSuffixVal6 = 0
                '    lSuffixVal7 = 0
                '    lSuffixVal8 = 0
                '    lSuffixVal9 = 0
                '    lSuffixVal10 = 0
                '    lSuffixVal11 = 0
                '    lSuffixVal12 = 0

                '    If lMonth = "01" Then
                '        lSuffixVal1 = 1
                '    ElseIf lMonth = "02" Then
                '        lSuffixVal2 = 1
                '    ElseIf lMonth = "03" Then
                '        lSuffixVal3 = 1
                '    ElseIf lMonth = "04" Then
                '        lSuffixVal4 = 1
                '    ElseIf lMonth = "05" Then
                '        lSuffixVal5 = 1
                '    ElseIf lMonth = "06" Then
                '        lSuffixVal6 = 1
                '    ElseIf lMonth = "07" Then
                '        lSuffixVal7 = 1
                '    ElseIf lMonth = "08" Then
                '        lSuffixVal8 = 1
                '    ElseIf lMonth = "09" Then
                '        lSuffixVal9 = 1
                '    ElseIf lMonth = "10" Then
                '        lSuffixVal10 = 1
                '    ElseIf lMonth = "11" Then
                '        lSuffixVal11 = 1
                '    ElseIf lMonth = "12" Then
                '        lSuffixVal12 = 1
                '    End If

                '    Dim commandDocControlInsert As MySqlCommand = New MySqlCommand

                '    commandDocControlInsert.CommandType = CommandType.Text
                '    'commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                '    '               "('" & lPrefix & "','M','" & lSeparator & "',6,0,0,0,0,0,0,0,0,0,0,0,0)"

                '    commandDocControlInsert.CommandText = "INSERT INTO tbldoccontrol(Prefix,GenerateMethod,`Separator`,Width,Period01,Period02,Period03,Period04,Period05,Period06,Period07,Period08,Period09,Period10,Period11,Period12) VALUES " & _
                '             "('" & lPrefix & "','M','" & lSeparator & "',6," & lSuffixVal1 & "," & lSuffixVal2 & "," & lSuffixVal3 & "," & lSuffixVal4 & "," & lSuffixVal5 & "," & lSuffixVal6 & "," & lSuffixVal7 & "," & lSuffixVal8 & "," & lSuffixVal9 & "," & lSuffixVal10 & "," & lSuffixVal11 & "," & lSuffixVal12 & ")"

                '    commandDocControlInsert.Connection = conn

                '    Dim dr2 As MySqlDataReader = commandDocControlInsert.ExecuteReader()
                '    Dim dt2 As New DataTable
                '    dt2.Load(dr2)

                '    lSetWidth = 6
                '    lSuffixVal = 1
                'End: Continuous Number
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
            gBillNo = lInvoiceNo + lSuffix
            conn.Close()
            conn.Dispose()
            'txtInvoiceNo.Text = lInvoiceNo + lSuffix

        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "GenerateInvoiceNo", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub


    Private Sub GenerateBillingSchedule()

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
        If Date.TryParseExact(txtBatchDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            strdate = d.ToShortDateString
        End If

        lPrefix = Format(CDate(strdate), "yyyyMM")
        lInvoiceNo = "SBS" + lPrefix + "-"
        lMonth = Right(lPrefix, 2)
        lYear = Left(lPrefix, 4)

        lPrefix = "SBS" + lYear
        lSuffixVal = 0

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
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
        gBillNo = lInvoiceNo + lSuffix
        txtInvoiceNo.Text = lInvoiceNo + lSuffix
        UpdatePanel3.Update()
    End Sub
    Public Sub MakeMeNull()

        'txtContractDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy")
        'Session("contractdetailfrom") = ""
        'Session("contractno") = ""
        'Session("serviceschedulefrom") = ""
        'Session("contractfrom") = ""

        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""

        txtAccountId.Text = ""
        txtClientName.Text = ""
        txtLocationId.Text = ""
        ddlContractNo.Text = ""
        txtRowSelected.Text = "0"

        txtDateFrom.Text = ""
        txtDateTo.Text = ""
        txtPopUpClient.Text = ""
        txtServiceBySearch.Text = ""
        ddlCompanyGrp.SelectedIndex = 0

        ddlBillingFrequency.SelectedIndex = 0

        ddlContractGroup.SelectedIndex = 0
        ddlScheduler.SelectedIndex = 0

        txtSearch1Status.Text = "O,P"
        txtBatchNo.Text = ""
        'txtMode.Text = "NEW"
        txtRcno.Text = "0"
        chkIncludeZeroValueServices.Checked = False
        txtTotalSelected.Text = "0.00"

        btnEdit.Enabled = False
        'btnCopy.Enabled = False
        btnChangeStatus.Enabled = False
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        btnDelete.Enabled = False

        Label43.Text = "SERVICE BILLING DETAILS"
        updPnlMsg.Update()
        updPanelInvoice.Update()
        DisableControls()

        FirstGridViewRowBillingDetailsRecs()
       
        'SqlDSServices.SelectCommand = Sql
        grvServiceRecDetails.DataSourceID = ""
        grvServiceRecDetails.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
        Me.cpnl1.Collapsed = True
        Me.cpnl1.ClientState = "True"

        Me.cpnl2.Collapsed = False
        Me.cpnl2.ClientState = "False"


        updpnlServiceRecs.Update()
        updPnlBillingRecs.Update()
        updPnlMsg.Update()
        UpdatePanel1.Update()
        updPanelInvoice.Update()
        'txtDateFrom.Text = txtInvoiceDate.Text
    End Sub

    Private Sub AccessControl()
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        'command.CommandText = "SELECT X0252,  X0252Add, X0252Edit, X0252Delete, X0252Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
        command.CommandText = "SELECT X0253,  X0253Add, X0253Edit, X0253Delete, X0253Print, X0253Post, X0253Reverse, X0253MultiPrint, x253DeSelectContractGroup, x253DeSelectCompanygroup FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)
        'conn.Close()
        'conn.Dispose()
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)("X0253")) Then
                If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("X0253"))) = False Then
                    If Convert.ToBoolean(dt.Rows(0)("X0253")) = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If
            End If

            If Not IsDBNull(dt.Rows(0)("X0253Add")) Then
                If String.IsNullOrEmpty(dt.Rows(0)("X0253Add")) = False Then
                    Me.btnADD.Enabled = dt.Rows(0)("X0253Add").ToString()
                End If
            End If


            If Not IsDBNull(dt.Rows(0)("x253DeSelectContractGroup")) Then
                If String.IsNullOrEmpty(dt.Rows(0)("x253DeSelectContractGroup")) = False Then
                    Me.txtDeSelectContractGroup.Text = dt.Rows(0)("x253DeSelectContractGroup").ToString()
                End If

                If txtDeSelectContractGroup.Text = True Then
                    Label47.Visible = False
                Else
                    Label47.Visible = True
                End If
            End If


            If Not IsDBNull(dt.Rows(0)("x253DeSelectCompanygroup")) Then
                If String.IsNullOrEmpty(dt.Rows(0)("x253DeSelectCompanygroup")) = False Then
                    Me.txtDeSelectCompanyGroup.Text = dt.Rows(0)("x253DeSelectCompanygroup").ToString()
                End If

                If txtDeSelectCompanyGroup.Text = True Then
                    Label44.Visible = False
                Else
                    Label44.Visible = True
                End If
            End If
            If txtMode.Text = "View" Then
                If Not IsDBNull(dt.Rows(0)("X0253Edit")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0253Edit")) = False Then
                        Me.btnEdit.Enabled = dt.Rows(0)("X0253Edit").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("X0253Delete")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0253Delete")) = False Then
                        Me.btnDelete.Enabled = dt.Rows(0)("X0253Delete").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("X0253Print")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0253Print")) = False Then
                        Me.btnPrint.Enabled = dt.Rows(0)("X0253Print").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("X0253Post")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0253Post")) = False Then
                        Me.btnPost.Enabled = dt.Rows(0)("X0253Post").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("X0253Reverse")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("X0253Reverse")) = False Then
                        Me.btnChangeStatus.Enabled = dt.Rows(0)("X0253Reverse").ToString()

                     
                    End If
                End If




            Else
                Me.btnEdit.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnPost.Enabled = False
                Me.btnChangeStatus.Enabled = False
            End If

            'If String.IsNullOrEmpty(dt.Rows(0)("X0252Print")) = False Then
            '    Me.btnDelete.Enabled = dt.Rows(0)("X0252Print").ToString()
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

            If btnChangeStatus.Enabled = True Then
                btnChangeStatus.ForeColor = System.Drawing.Color.Black
            Else
                btnChangeStatus.ForeColor = System.Drawing.Color.Gray
            End If

        End If

        conn.Close()
        conn.Dispose()
        command.Dispose()
        dt.Dispose()
        dr.Close()
        'End If
    End Sub

    Private Sub DisableControls()
        'btnSave.Enabled = False
        'btnSave.ForeColor = System.Drawing.Color.Gray
        'btnCancel.Enabled = False
        'btnCancel.ForeColor = System.Drawing.Color.Gray
        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

     

        txtInvoiceNo.Enabled = False
        txtBatchDate.Enabled = False
        txtInvoiceDate.Enabled = False
        txtBillingPeriod.Enabled = False
        txtCompanyGroup.Enabled = False
        txtAccountIdBilling.Enabled = False
        txtAccountType.Enabled = False
        txtAccountName.Enabled = False
        txtBillAddress.Enabled = False
        txtBillStreet.Enabled = False
        txtBillBuilding.Enabled = False
        txtBillPostal.Enabled = False
        ddlSalesmanBilling.Enabled = False
        txtInvoiceAmount.Enabled = False
        txtBillCountry.Enabled = False
        txtPONo.Enabled = False
        ddlCreditTerms.Enabled = False
        txtDiscountAmount.Enabled = False
        txtAmountWithDiscount.Enabled = False
        txtGSTAmount.Enabled = False
        txtNetAmount.Enabled = False
        txtOurReference.Enabled = False
        txtYourReference.Enabled = False
        txtComments.Enabled = False
        txtCreditDays.Enabled = False

        txtTotalSelected.Enabled = False

        btnSaveInvoice.Enabled = False
        btnSaveInvoice.ForeColor = System.Drawing.Color.Gray

        btnGenerateInvoice.Enabled = False
        btnGenerateInvoice.ForeColor = System.Drawing.Color.Gray

        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray

        btnShowRecords.Enabled = False
        btnShowRecords.ForeColor = System.Drawing.Color.Gray

        btnDeleteUnselected.Enabled = False
        btnDeleteUnselected.ForeColor = System.Drawing.Color.Gray

        grvBillingDetails.Enabled = False
        grvServiceRecDetails.Enabled = False

        'btnPrintBatchInvoice.setAttribute('disabled', true)
        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

        '        var btn = document.getElementById("Button");
        'btn.disabled = false;


        ddlContactType.Enabled = False
        ddlServiceFrequency.Enabled = False
        ddlBillingFrequency.Enabled = False
        ddlContractGroup.Enabled = False
        txtAccountId.Enabled = False
        'txtContractNo.Enabled = False
        txtClientName.Enabled = False
        txtLocationId.Enabled = False
        'ddlContractGrp.Enabled = False
        ddlCompanyGrp.Enabled = False
        ddlContractNo.Enabled = False
        ddlScheduler.Enabled = False

        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        btnDelete.Enabled = False

        'txtTeamSearch.Enabled = False
        'txtInchargeSearch.Enabled = False
        txtServiceBySearch.Enabled = False
        chkIncludeZeroValueServices.Enabled = False

        'rdbAll.Attributes.Add("disabled", "disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")
        rdbAll.Enabled = False
        rdbCompleted.Enabled = False
        rdbNotCompleted.Enabled = False
        btnClient.Visible = False
        BtnLocation.Visible = False
        rdbGrouping.Enabled = False
        ImageButton1.Visible = False
        ImageButton5.Visible = False

        txtRecurringInvoiceDate.Enabled = False
        txtRecurringServiceDateFrom.Enabled = False
        txtRecurringServiceDateTo.Enabled = False
        chkRecurringInvoice.Enabled = False
        ddlRecurringFrequency.Enabled = False

        'txtRecurringInvoiceDate.BackColor = Color.Gray
        'txtRecurringServiceDateFrom.BackColor = Color.Gray
        'txtRecurringServiceDateTo.BackColor = Color.Gray
        'ddlRecurringFrequency.BackColor = Color.Gray
        ''chkRecurringInvoice.BackColor = Color.Gray


        txtInvoicenoSearch.Enabled = True
        ddlCompanyGrpSearch.Enabled = True
        txtAccountIdSearch.Enabled = True
        txtBillingPeriodSearch.Enabled = True
        ddlBillingFrequencySearch.Enabled = True
        ddlContactTypeSearch.Enabled = True
        txtClientNameSearch.Enabled = True
        txtSearch1Status.Enabled = True

        btnClientSearch.Enabled = True
        btnQuickSearch.Enabled = True
        btnQuickReset.Enabled = True
        AccessControl()

    End Sub

    Private Sub EnableControls()
        'btnSave.Enabled = True
        'btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black
        'btnClient.Visible = True
        'btnADD.Enabled = True
        'btnADD.ForeColor = System.Drawing.Color.Black

        btnADD.Enabled = False
        btnADD.ForeColor = Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnADDRecurring.Enabled = False
        btnADDRecurring.ForeColor = System.Drawing.Color.Gray

        btnPrint.Enabled = False
        btnPrint.ForeColor = System.Drawing.Color.Gray

        'rdbAll.Attributes.Remove("disabled")
        'rdbCompleted.Attributes.Add("readonly", "readonly")
        'rdbNotCompleted.Attributes.Add("readonly", "readonly")

        'rdbAll.Enabled = True
        'rdbCompleted.Enabled = True
        'rdbNotCompleted.Enabled = True

        rdbNotCompleted.Enabled = True
        rdbCompleted.Enabled = True
        rdbAll.Enabled = True
        ddlCompanyGrp.Enabled = True
        ddlContactType.Enabled = True
        txtClientName.Enabled = True
        txtAccountId.Enabled = True
        txtLocationId.Enabled = True
        ddlContractNo.Enabled = True
        txtDateFrom.Enabled = True
        txtDateTo.Enabled = True

        ddlServiceFrequency.Enabled = True
        ddlBillingFrequency.Enabled = True
        ddlContractGroup.Enabled = True
        txtAccountIdBilling.Enabled = True


        txtInvoiceNo.Enabled = True
        txtBatchDate.Enabled = True
        txtInvoiceDate.Enabled = True

        txtBillingPeriod.Enabled = True
        txtCompanyGroup.Enabled = True
        txtAccountId.Enabled = True
        txtAccountType.Enabled = True

        txtAccountName.Enabled = True
        txtBillAddress.Enabled = True
        txtBillStreet.Enabled = True
        txtBillBuilding.Enabled = True
        txtBillPostal.Enabled = True
        ddlSalesmanBilling.Enabled = True
        txtInvoiceAmount.Enabled = True
        txtBillCountry.Enabled = True
        txtPONo.Enabled = True
        ddlCreditTerms.Enabled = True
        txtDiscountAmount.Enabled = True
        txtAmountWithDiscount.Enabled = True
        txtGSTAmount.Enabled = True
        txtNetAmount.Enabled = True
        txtOurReference.Enabled = True
        txtYourReference.Enabled = True
        txtComments.Enabled = True
        txtCreditDays.Enabled = True
        ddlScheduler.Enabled = True


        'txtTeamSearch.Enabled = True
        'txtInchargeSearch.Enabled = True
        txtServiceBySearch.Enabled = True
        chkIncludeZeroValueServices.Enabled = True

        'btnSaveInvoice.Enabled = True
        'btnSave.Enabled = True
        btnShowRecords.Enabled = True
        btnShowRecords.ForeColor = System.Drawing.Color.Black
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

        txtInvoicenoSearch.Enabled = False
        ddlCompanyGrpSearch.Enabled = False
        txtAccountIdSearch.Enabled = False
        txtBillingPeriodSearch.Enabled = False

        'chkIncludeZeroValueServices.Enabled = False

        ddlBillingFrequencySearch.Enabled = False
        ddlContactTypeSearch.Enabled = False
        txtClientNameSearch.Enabled = False
        txtSearch1Status.Enabled = False
        btnClientSearch.Enabled = False
        btnQuickSearch.Enabled = False
        btnQuickReset.Enabled = False

        grvBillingDetails.Enabled = True
        grvServiceRecDetails.Enabled = True
        updPnlSearch.Update()
        updPnlBillingRecs.Update()
        updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        updPanelSave.Update()
        UpdatePanel1.Update()

        btnClient.Visible = True
        BtnLocation.Visible = True

        ImageButton1.Visible = True
        ImageButton5.Visible = True
    End Sub


  
    'Function

    'Button-click
    Protected Sub ShowMessage(sender As Object, e As EventArgs, message As String)

        'Dim message As String = "alert('Hello! Mudassar.')"

        ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)

    End Sub



    'Start: Populate Recurring Invoice

    Private Sub PopulateServiceGridForRecurringInvoice()

        Try

            'Start: Service Recods

            Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
            Dim drCurrentRowLoc As DataRow = Nothing

            For i As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                drCurrentRowLoc = dtScdrLoc.NewRow()
                ViewState("CurrentTableServiceRec") = dtScdrLoc
                grvServiceRecDetails.DataSource = dtScdrLoc
                grvServiceRecDetails.DataBind()

                SetPreviousDataServiceRecs()
            Next i



            FirstGridViewRowServiceRecs()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim sql As String
            Dim sqlServicebillschedule As String
            sql = ""
            sqlServicebillschedule = ""

            sqlServicebillschedule = "Select * "
            sqlServicebillschedule = sqlServicebillschedule + " FROM tblservicebillschedule"
            sqlServicebillschedule = sqlServicebillschedule + " WHERE  BillSchedule = '" & txtPreviousBillSchedule.Text & "'"

            Dim command2 As MySqlCommand = New MySqlCommand
            command2.CommandType = CommandType.Text
            command2.CommandText = sqlServicebillschedule
            command2.Connection = conn

            Dim dr2 As MySqlDataReader = command2.ExecuteReader()

            Dim dt2 As New DataTable
            dt2.Load(dr2)


            If dt2.Rows.Count > 0 Then

                For Each row2 As DataRow In dt2.Rows

           

            'If txtMode.Text = "VIEW" Then


            'Else
            If ddlContactType.SelectedIndex = 0 Then

                If ddlServiceFrequency.Text.Trim = "-1" Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                            sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, A.ServiceBy, A.ServiceLocationGroup,  "
                            sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, B.ArTerm, B.TermsDay, "
                            sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup,B.TaxIdentificationNo,B.SalesTaxRegistrationNo  "
                    sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblCompany B"
                            sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId  and (C.ConTractGroup <> 'ST') and A.ContactType = '" & ddlContactType.Text.Trim & "' and B.AccountId = '" & dt2.Rows(0)("AccountId").ToString & "'"
                Else
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                            sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, A.ServiceBy,  A.ServiceLocationGroup,"
                            sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, B.ArTerm, B.TermsDay,  "
                            sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup,B.TaxIdentificationNo,B.SalesTaxRegistrationNo  "
                    sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblCompany B, tblcontractdet D "
                            sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and C.ContractNo = D.ContractNo and (C.ConTractGroup <> 'ST') and A.ContactType = '" & ddlContactType.Text.Trim & "' and B.AccountId = '" & dt2.Rows(0)("AccountId").ToString & "'"
                End If

            ElseIf ddlContactType.SelectedIndex = 1 Then

                If ddlServiceFrequency.Text.Trim = "-1" Then
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                            sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.ServiceBy , A.ServiceLocationGroup,"
                            sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, B.ArTerm, B.TermsDay, "
                            sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup,B.TaxIdentificationNo,B.SalesTaxRegistrationNo  "
                    sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblPerson B "
                            sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and (C.ConTractGroup <> 'ST') and A.ContactType = '" & ddlContactType.Text.Trim & "' and B.AccountId = '" & dt2.Rows(0)("AccountId").ToString & "'"
                Else
                    sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, A.AddBuilding, A.AddStreet, A.AddPostal,"
                            sql = sql + " A.BillAmount, A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.ServiceBy,  A.ServiceLocationGroup,"
                            sql = sql + " B.Address1, B.Salesman, B.Name,  B.BillAddress1, B.BillBuilding, B.BillStreet, B.BillPostal, B.BillCity, B.BillCountry, B.ArTerm, B.TermsDay, "
                            sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup,B.TaxIdentificationNo,B.SalesTaxRegistrationNo  "
                    sql = sql + " FROM tblservicerecord A LEFT JOIN tblContract C ON A.ContractNo = C.ContractNo, tblPerson B, tblcontractdet D "
                            sql = sql + " where 1 = 1  and A.BillYN= 'N' and (A.Status = 'O' or A.Status = 'P') and A.AccountId = B.AccountId and C.ContractNo = D.ContractNo and (C.ConTractGroup <> 'ST') and A.ContactType = '" & ddlContactType.Text.Trim & "' and B.AccountId = '" & dt2.Rows(0)("AccountId").ToString & "'"

                End If

                    End If



                    'If String.IsNullOrEmpty(txtAccountId.Text) = False Then
                    '    sql = sql + " and  A.AccountID like '%" & txtAccountId.Text & "%'"
                    'Else
                    '    If String.IsNullOrEmpty(txtClientName.Text) = False Then
                    '        sql = sql + " and  A.ServiceName like '%" & txtClientName.Text & "%'"
                    '    End If
                    'End If

                    ''If String.IsNullOrEmpty(txtClientName.Text) = False Then
                    ''    sql = sql + " and  A.ServiceName like '%" & txtClientName.Text & "%'"
                    ''End If

                    'If ddlCompanyGrp.Text.Trim <> "-1" Then
                    '    sql = sql + " and   A.CompanyGroup = '" & ddlCompanyGrp.Text.Trim & "'"
                    'End If

                    If ddlContractNo.Text.Trim <> "-1" Then
                        sql = sql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
                    End If

                    'If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
                    '    sql = sql + " and   A.ServiceDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                    'End If

                    'If String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    '    sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                    'End If

                    If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                        sql = sql + " and   A.ServiceDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                    End If

                    If String.IsNullOrEmpty(txtLocationId.Text) = False Then
                        sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
                    End If


                    'If ddlServiceFrequency.Text.Trim <> "-1" Then
                    '    sql = sql + " and   D.Frequency = '" & ddlServiceFrequency.Text.Trim & "'"
                    'End If

                    If ddlBillingFrequency.Text.Trim <> "-1" Then
                        sql = sql + " and   C.BillingFrequency = '" & ddlBillingFrequency.Text.Trim & "'"
                    End If

                    If ddlContractGroup.Text.Trim <> "-1" Then
                        sql = sql + " and   C.ContractGroup = '" & ddlContractGroup.Text.Trim & "'"
                    End If


                    If rdbCompleted.Checked = True Then
                        sql = sql + " and  A.Status = 'P' "
                    End If

                    If rdbNotCompleted.Checked = True Then
                        sql = sql + " and  A.Status = 'O' "
                    End If


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

                    If rdbGrouping.SelectedIndex = 0 Then
                        sql = sql + " order by A.ContractNo, A.ServiceDate"
                    ElseIf rdbGrouping.SelectedIndex = 1 Then
                        sql = sql + " order by A.AccountID, A.LocationId, A.ServiceDate"
                    ElseIf rdbGrouping.SelectedIndex = 2 Then
                        sql = sql + " order by A.AccountID, A.ServiceDate"
                    ElseIf rdbGrouping.SelectedIndex = 3 Then
                        'sql = sql + " order by A.AccountID, A.ContractNo, A.ServiceDate"
                    End If


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
                                AddNewRowServiceRecs()
                            End If

                            Dim TextBoxServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
                            TextBoxServiceRecordNo.Text = Convert.ToString(dt.Rows(rowIndex)("RecordNo"))

                            Dim TextBoxServiceDate As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceDateGV"), TextBox)
                            TextBoxServiceDate.Text = Convert.ToString(Convert.ToDateTime(dt.Rows(rowIndex)("ServiceDate")).ToString("dd/MM/yyyy"))

                            Dim TextBoxAccountId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                            TextBoxAccountId.Text = Convert.ToString(dt.Rows(rowIndex)("AccountId"))

                            Dim TextBoxClientName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtClientNameGV"), TextBox)
                            TextBoxClientName.Text = Convert.ToString(dt.Rows(rowIndex)("CustName"))

                            Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
                            TextBoxContractNo.Text = Convert.ToString(dt.Rows(rowIndex)("ContractNo"))

                            Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
                            TextBoxToBillAmt.Text = Convert.ToString(dt.Rows(rowIndex)("BillAmount"))


                            Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceAddressGV"), TextBox)
                            'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmountGV"), TextBox)
                            'Dim TextBoxDiscAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
                            'Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtNetAmountGV"), TextBox)


                            If txtMode.Text = "View" Then
                                TextBoxServiceAddress.Text = Convert.ToString(dt.Rows(rowIndex)("Address1"))
                                'TextBoxGSTAmount.Text = Convert.ToString(dt.Rows(rowIndex)("GSTAmount"))
                                'TextBoxDiscAmount.Text = Convert.ToString(dt.Rows(rowIndex)("DiscountAmount"))
                                'TextBoxNetAmount.Text = Convert.ToString(dt.Rows(rowIndex)("NetAmount"))
                            Else
                                'TextBoxServiceAddress.Text = Convert.ToString(dt.Rows(rowIndex)("Address1")) + ", " + Convert.ToString(dt.Rows(rowIndex)("AddStreet")) + ", " + Convert.ToString(dt.Rows(rowIndex)("AddBuilding")) + ", " + Convert.ToString(dt.Rows(rowIndex)("AddPostal"))

                                TextBoxServiceAddress.Text = Convert.ToString(dt.Rows(rowIndex)("Address1")).Trim

                                If String.IsNullOrEmpty(Convert.ToString(dt.Rows(rowIndex)("AddStreet")).Trim) = False Then
                                    TextBoxServiceAddress.Text = TextBoxServiceAddress.Text + ", " + Convert.ToString(dt.Rows(rowIndex)("AddStreet"))
                                End If

                                If String.IsNullOrEmpty(Convert.ToString(dt.Rows(rowIndex)("AddBuilding")).Trim) = False Then
                                    TextBoxServiceAddress.Text = TextBoxServiceAddress.Text + ", " + Convert.ToString(dt.Rows(rowIndex)("AddBuilding"))
                                End If

                                If String.IsNullOrEmpty(Convert.ToString(dt.Rows(rowIndex)("AddPostal")).Trim) = False Then
                                    TextBoxServiceAddress.Text = TextBoxServiceAddress.Text + ", " + Convert.ToString(dt.Rows(rowIndex)("AddPostal"))
                                End If

                                'TextBoxGSTAmount.Text = Convert.ToString(dt.Rows(rowIndex)("BillAmount")) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                                'TextBoxDiscAmount.Text = "0.00"
                                'TextBoxNetAmount.Text = Convert.ToString(Convert.ToDecimal(TextBoxToBillAmt.Text) + Convert.ToDecimal(TextBoxGSTAmount.Text))
                            End If

                            Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtLocationIdGV"), TextBox)
                            TextBoxLocationId.Text = Convert.ToString(dt.Rows(rowIndex)("LocationId"))


                            If txtMode.Text = "View" Then
                                Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                                TextBoxRcnoServiceRecord.Text = Convert.ToString(dt.Rows(rowIndex)("RcnoServiceRecord"))
                            Else
                                Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                                TextBoxRcnoServiceRecord.Text = Convert.ToString(dt.Rows(rowIndex)("Rcno"))
                            End If

                            'Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                            'TextBoxRcnoServiceRecord.Text = Convert.ToString(dt.Rows(rowIndex)("Rcno"))

                            Dim TextBoxName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountNameGV"), TextBox)
                            TextBoxName.Text = Convert.ToString(dt.Rows(rowIndex)("Name"))

                            'Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillAddress1GV"), TextBox)
                            'TextBoxBillAddress1.Text = Convert.ToString(dt.Rows(rowIndex)("BillAddress1"))

                            'Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillBuildingGV"), TextBox)
                            'TextBoxBillBuilding.Text = Convert.ToString(dt.Rows(rowIndex)("BillBuilding"))

                            'Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillStreetGV"), TextBox)
                            'TextBoxBillStreet.Text = Convert.ToString(dt.Rows(rowIndex)("BillStreet"))

                            'Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillCountryGV"), TextBox)
                            'TextBoxBillCountry.Text = Convert.ToString(dt.Rows(rowIndex)("BillCountry"))

                            'Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillPostalGV"), TextBox)
                            'TextBoxBillPostal.Text = Convert.ToString(dt.Rows(rowIndex)("BillPostal"))

                            Dim TextBoxOurRef As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtOurReferenceGV"), TextBox)
                            TextBoxOurRef.Text = Convert.ToString(dt.Rows(rowIndex)("OurRef"))

                            Dim TextBoxYourRef As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtYourReferenceGV"), TextBox)
                            TextBoxYourRef.Text = Convert.ToString(dt.Rows(rowIndex)("YourRef"))

                            Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtPoNoGV"), TextBox)
                            TextBoxPoNo.Text = Convert.ToString(dt.Rows(rowIndex)("PoNo"))

                            Dim TextBoxBillingFrequency As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtBillingFrequencyGV"), TextBox)
                            TextBoxBillingFrequency.Text = Convert.ToString(dt.Rows(rowIndex)("BillingFrequency"))

                            Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
                            TextBoxRcnoInvoice.Text = Convert.ToString(dt.Rows(rowIndex)("RcnoInvoice"))

                            Dim TextBoxRcnoservicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServicebillingdetailGV"), TextBox)
                            TextBoxRcnoservicebillingdetail.Text = Convert.ToString(dt.Rows(rowIndex)("rcnotblservicebillingdetail"))

                            Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtStatusGV"), TextBox)
                            TextBoxStatus.Text = Convert.ToString(dt.Rows(rowIndex)("Status"))



                            Dim TextBoxContractGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtDeptGV"), TextBox)
                            TextBoxContractGroup.Text = Convert.ToString(dt.Rows(rowIndex)("ContractGroup"))


                            'Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCreditTermsGV"), TextBox)
                            'TextBoxCreditTerms.Text = Convert.ToString(dt.Rows(rowIndex)("CreditTerms"))

                            Dim TextBoxtxtSalesmanGV As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtSalesmanGV"), TextBox)
                            If Convert.ToString(dt.Rows(rowIndex)("Salesman")) = "-1" Then
                                TextBoxtxtSalesmanGV.Text = "-1"
                            Else
                                TextBoxtxtSalesmanGV.Text = Convert.ToString(dt.Rows(rowIndex)("Salesman"))
                            End If

                            'Dim TextBoxtxtSalesmanGV As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtSalesmanGV"), TextBox)
                            'TextBoxtxtSalesmanGV.Text = Convert.ToString(dt.Rows(rowIndex)("Salesman"))

                            Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtContactTypeGV"), TextBox)
                            TextBoxContactType.Text = Convert.ToString(dt.Rows(rowIndex)("ContactType"))

                            Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                            TextBoxCompanyGroup.Text = Convert.ToString(dt.Rows(rowIndex)("CompanyGroup"))

                            Dim TextBoxTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCreditTermsGV"), TextBox)
                            TextBoxTerms.Text = Convert.ToString(dt.Rows(rowIndex)("ArTerm"))

                            Dim TextBoxServiceBy As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceByGV"), TextBox)
                            TextBoxServiceBy.Text = Convert.ToString(dt.Rows(rowIndex)("ServiceBy"))

                            Dim TextBoxBillNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)
                            TextBoxBillNo.Text = Convert.ToString(dt.Rows(rowIndex)("BillNo"))

                            Dim TextBoxServiceLocationGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceLocationGroupGV"), TextBox)
                            TextBoxServiceLocationGroup.Text = Convert.ToString(dt.Rows(rowIndex)("ServiceLocationGroup"))

                            If TextBoxStatus.Text = "P" Then
                                TextBoxStatus.ForeColor = Color.Blue
                                TextBoxAccountId.ForeColor = Color.Blue
                                TextBoxClientName.ForeColor = Color.Blue
                                TextBoxServiceRecordNo.ForeColor = Color.Blue
                                TextBoxServiceDate.ForeColor = Color.Blue
                                TextBoxBillingFrequency.ForeColor = Color.Blue
                                TextBoxLocationId.ForeColor = Color.Blue
                                TextBoxServiceAddress.ForeColor = Color.Blue
                                TextBoxToBillAmt.ForeColor = Color.Blue
                                TextBoxContractNo.ForeColor = Color.Blue
                                TextBoxContractGroup.ForeColor = Color.Blue
                            End If

                            rowIndex += 1

                        Next row

                        'AddNewRowServiceRecs()
                        'SetPreviousDataServiceRecs()
                        'AddNewRowLoc()
                        'SetPreviousDataLoc()
                    Else
                        FirstGridViewRowServiceRecs()

                    End If

                Next row2
            End If

            'End If

            Label43.Text = "SERVICE BILLING DETAILS : Total Records :" & grvServiceRecDetails.Rows.Count.ToString
            updpnlServiceRecs.Update()
            'txtClientName.Text = sql

            Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            If txtMode.Text = "View" Then

                If (table.Rows.Count > 0) Then
                    For i As Integer = 0 To (table.Rows.Count) - 1
                        Dim TextBoxSel As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)
                        TextBoxSel.Enabled = False
                        TextBoxSel.Checked = True
                    Next i
                End If
            End If
            'End: Service Recods

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    'End: Populate Recurring Invoice


    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
37:     Try
            'txtInvoiceDate.Text = ""
            MakeMeNull()
            MakeMeNullBillingDetails()
            EnableControls()



            Me.cpnl2.Collapsed = False
            Me.cpnl2.ClientState = "False"

            btnADD.Enabled = False
            btnADD.ForeColor = Color.Gray

            'If chkRecurringInvoice.Checked = True Then
            '    txtAccountId.Text = txtAccountIdBilling.Text
            '    UpdatePanel1.Update()
            '    btnShowRecords_Click(sender, e)
            'End If
            'MakeMeNullBillingDetails()


            ' '''''''''''''''''''''''''''''''' Recurring Invoice

            'Dim conn As MySqlConnection = New MySqlConnection()

            'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Open()


            ''sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"

            'Dim sql As String
            'sql = ""
            'sql = "Select * from tblservicebillschedule where RecurringInvoice = 'Y' and NextInvoiceDate = '" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd") & "'"

            'Dim command1 As MySqlCommand = New MySqlCommand
            'command1.CommandType = CommandType.Text
            'command1.CommandText = sql
            'command1.Connection = conn

            'Dim dr As MySqlDataReader = command1.ExecuteReader()

            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then
            '    If dt.Rows(0)("RecurringServiceStartDate").ToString <> "" Then : txtDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceStartDate")).ToString("dd/MM/yyyy") : End If
            '    If dt.Rows(0)("RecurringServiceEndDate").ToString <> "" Then : txtDateTo.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceEndDate")).ToString("dd/MM/yyyy") : End If
            '    If dt.Rows(0)("BillSchedule").ToString <> "" Then : txtPreviousBillSchedule.Text = (dt.Rows(0)("BillSchedule")).ToString : End If

            '    If dt.Rows(0)("GroupByStatus").ToString = "P" Then
            '        rdbCompleted.Checked = True
            '    ElseIf dt.Rows(0)("GroupByStatus").ToString = "O" Then
            '        rdbNotCompleted.Checked = True
            '    ElseIf dt.Rows(0)("GroupByStatus").ToString = "A" Then
            '        rdbAll.Checked = True
            '    End If
            '    If dt.Rows(0)("GroupByBillingFrequency").ToString <> "" Then : ddlBillingFrequency.Text = (dt.Rows(0)("GroupByBillingFrequency")).ToString : End If
            '    If dt.Rows(0)("GroupContractNo").ToString <> "" Then : ddlContractNo.Items.Clear() : ddlContractNo.Items.Add((dt.Rows(0)("GroupContractNo")).ToString) : End If
            '    If dt.Rows(0)("GroupLocationID").ToString <> "" Then : txtLocationId.Text = (dt.Rows(0)("GroupLocationID")).ToString : End If
            '    If dt.Rows(0)("GroupContractGroup").ToString <> "" Then : ddlContractGroup.Text = (dt.Rows(0)("GroupContractGroup")).ToString : End If

            'End If

            'PopulateServiceGridForRecurringInvoice()

            ' '''''''''''''''''''''''''''''''' Recurring Invoice
            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtBillingPeriod.Text = Year(Convert.ToDateTime(txtBatchDate.Text)) & Format(Month(Convert.ToDateTime(txtBatchDate.Text)), "00")

            Session.Remove("buttonclicked")
        Catch ex As Exception
            lblAlert.Text = ex.Message
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "Page_load", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Button clic


    'Pop-up



    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
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
    End Sub


    Public Sub PopulateComboBox1(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As AjaxControlToolkit.ComboBox)
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


    Public Sub PopulateComboBox(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal cmb As Saplin.Controls.DropDownCheckBoxes)
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
        Response.Redirect("Home.aspx")
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

        SQLDSInvoice.SelectCommand = txt.Text
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
    '            exstr = ex.ToString
    '            MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        End Try
    'End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblAlert.Text = ""
        Try
            If txtPostStatus.Text = "P" Then
                lblAlert.Text = "Invoice Schedule has already been POSTED.. Cannot be VOIDED"

                Exit Sub
            End If

            If txtPostStatus.Text = "V" Then
                lblAlert.Text = "Invoice Schedule is VOID.. Cannot be VOIDED"
                Exit Sub
            End If

            lblAlert.Text = ""
            lblMessage.Text = "ACTION: VOID RECORD"


            Dim confirmValue As String
            confirmValue = ""

            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Dispose()
                End If
                conn.Open()


                '''''''''''''''''''''''''''''''''
                'Dim commandValues As MySqlCommand = New MySqlCommand

                'commandValues.CommandType = CommandType.Text
                'commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where BillSchedule ='" & txtInvoiceNo.Text.Trim & "'"
                'commandValues.Connection = conn

                'Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                'Dim dtValues As New DataTable
                'dtValues.Load(drValues)

                'For Each row As DataRow In dtValues.Rows

                '    'Start: Update tblServiceRecord
                '    'If row("ItemCode") = "IN-DEF" Or row("ItemCode") = "IN-SRV" Then
                '    '    Dim command5 As MySqlCommand = New MySqlCommand
                '    '    command5.CommandType = CommandType.Text

                '    '    Dim qry5 As String = "Update tblservicerecord Set BillYN = 'N' where Rcno= @Rcno "
                '    '    'Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y' where Rcno= @Rcno "

                '    '    command5.CommandText = qry5
                '    '    command5.Parameters.Clear()

                '    '    command5.Parameters.AddWithValue("@Rcno", row("RcnoServiceRecord"))
                '    '    command5.Connection = conn
                '    '    command5.ExecuteNonQuery()
                '    'End If


                'Next row

                ''''''''''''''''''''''''''''''''''''


                'Dim command1 As MySqlCommand = New MySqlCommand
                'command1.CommandType = CommandType.Text

                'Dim qry1 As String = "DELETE from tblSales where BillSchedule = @BillSchedule "

                'command1.CommandText = qry1
                'command1.Parameters.Clear()

                'command1.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                'command1.Connection = conn
                'command1.ExecuteNonQuery()


                'Dim command3 As MySqlCommand = New MySqlCommand
                'command3.CommandType = CommandType.Text

                'Dim qry3 As String = "DELETE from tblservicebillingdetail where BillSchedule= @BillSchedule "

                'command3.CommandText = qry3
                'command3.Parameters.Clear()

                'command3.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                'command3.Connection = conn
                'command3.ExecuteNonQuery()


                'Dim command4 As MySqlCommand = New MySqlCommand
                'command4.CommandType = CommandType.Text

                'Dim qry4 As String = "DELETE from tblservicebillingdetailItem where BillSchedule= @BillSchedule "

                'command4.CommandText = qry4
                'command4.Parameters.Clear()

                'command4.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                'command4.Connection = conn
                'command4.ExecuteNonQuery()


                Dim command6 As MySqlCommand = New MySqlCommand
                command6.CommandType = CommandType.Text

                'Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                'Dim qry6 As String = "DELETE from tblservicebillschedule where BillSchedule= @BillSchedule"
                Dim qry6 As String = "UPDATE tblservicebillschedule set PostStatus = 'V' where BillSchedule= @BillSchedule"
                command6.CommandText = qry6
                command6.Parameters.Clear()

                command6.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                command6.Connection = conn
                command6.ExecuteNonQuery()

                ''End: Update tblServiceRecord

                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Dispose()
                End If
                'conn.Close()

                lblMessage.Text = "VOID: BATCH INVOICE SUCCESSFULLY VOIDED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)


                MakeMeNull()

                'Dim message As String = "alert('Contract is deleted Successfully!!!')"
                'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
               
                'btnADD_Click(sender, e)

                'txt.Text = "SELECT * From tblContract where (1=1)  and Status ='O' order by rcno desc, CustName limit 50"
                SQLDSInvoice.SelectCommand = txt.Text
                SQLDSInvoice.DataBind()
                'GridView1.DataSourceID = "SqlDSContract"


                GridView1.DataBind()
                updPnlMsg.Update()
                updPnlSearch.Update()
                updPanelInvoice.Update()
                'updpnlServiceRecs.Update()
                'GridView1.DataBind()
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString

            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "!!!')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)


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

        GridView1.DataBind()
    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        lblAlert.Text = ""
        lblMessage.Text = ""

        Try
            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            Dim confirmValue As String
            confirmValue = ""


            confirmValue = Request.Form("confirm_value")
            If Right(confirmValue, 3) = "Yes" Then
                ''''''''''''''' Insert tblAR

                Dim conn As MySqlConnection = New MySqlConnection()

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Dispose()
                End If
                conn.Open()

                Dim command1 As MySqlCommand = New MySqlCommand
                command1.CommandType = CommandType.Text

                Dim qry1 As String = "DELETE from tblAR where BatchNo = '" & txtBatchNo.Text & "'"

                command1.CommandText = qry1
                'command1.Parameters.Clear()
                command1.Connection = conn
                command1.ExecuteNonQuery()

                Dim qryAR As String
                Dim commandAR As MySqlCommand = New MySqlCommand
                commandAR.CommandType = CommandType.Text

                'If Convert.ToDecimal(txtNetAmount.Text) > 0.0 Then
                '    qryAR = "INSERT INTO tblAR(VoucherNumber, AccountId, CustomerName, VoucherDate, InvoiceNumber,  GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ModuleName, DueDate, GLtype, "
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode,  @GLDescription, @DebitAmount, @CreditAmount,  @BatchNo, @CompanyGroup, @ContractNo,  @ModuleName, @DueDate, @GLtype, "
                '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    commandAR.CommandText = qryAR
                '    commandAR.Parameters.Clear()
                '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    If txtInvoiceDate.Text.Trim = "" Then
                '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    Else
                '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                '    End If
                '    commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                '    commandAR.Parameters.AddWithValue("@GLCode", txtARCode.Text)
                '    commandAR.Parameters.AddWithValue("@GLDescription", txtARDescription.Text)
                '    commandAR.Parameters.AddWithValue("@DebitAmount", 0.0)
                '    commandAR.Parameters.AddWithValue("@CreditAmount", Convert.ToDecimal(txtNetAmount.Text))
                '    commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                '    commandAR.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(Convert.ToInt64(txtCreditDays.Text)).ToString("yyyy-MM-dd"))
                '    commandAR.Parameters.AddWithValue("@GLType", "Debtor")
                '    commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                '    commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                '    commandAR.Connection = conn
                '    commandAR.ExecuteNonQuery()
                'End If

                'If Convert.ToDecimal(txtGSTAmount.Text) > 0.0 Then
                '    qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup,  ContractNo, ModuleName, "
                '    qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                '    qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ModuleName, "
                '    qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                '    commandAR.CommandText = qryAR
                '    commandAR.Parameters.Clear()

                '    commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                '    commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                '    commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                '    If txtInvoiceDate.Text.Trim = "" Then
                '        commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                '    Else
                '        commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                '    End If
                '    commandAR.Parameters.AddWithValue("@ContractNo", "")
                '    commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                '    commandAR.Parameters.AddWithValue("@GLCode", txtGSTOutputCode.Text)
                '    commandAR.Parameters.AddWithValue("@GLDescription", txtGSTOutputDescription.Text)
                '    commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(txtGSTAmount.Text))
                '    commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                '    commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                '    commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                '    commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")

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
                commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where BatchNo ='" & txtBatchNo.Text.Trim & "'"
                commandValues.Connection = conn

                Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
                Dim dtValues As New DataTable
                dtValues.Load(drValues)



                For Each row As DataRow In dtValues.Rows

                    If Convert.ToDecimal(row("PriceWithDisc")) > 0.0 Then
                        ''qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount, CreditAmount, BatchNo, CompanyGroup, ContractNo, ServiceStatus, RcnoServiceRecord, ContractGroup, ModuleName, ItemCode, ServiceRecordNo, "
                        'qryAR = "INSERT INTO tblAR( VoucherNumber,  AccountId, CustomerName, VoucherDate, InvoiceNumber, GLCode, GLDescription, DebitAmount,  "
                        'qryAR = qryAR + " CreditAmount, BatchNo, CompanyGroup, ContractNo, ServiceStatus, RcnoServiceRecord, ContractGroup,   "
                        'qryAR = qryAR + " ModuleName, ItemCode, ServiceRecordNo, ServiceDate, "
                        'qryAR = qryAR + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        ''qryAR = qryAR + " (@VoucherNumber, @AccountId, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount, @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ServiceStatus, @RcnoServiceRecord, @ContractGroup, @ModuleName, @ItemCode, @ServiceRecordNo, "
                        'qryAR = qryAR + " (@VoucherNumber, @AccountId, @CustomerName, @VoucherDate, @InvoiceNumber, @GLCode, @GLDescription, @DebitAmount,  "
                        'qryAR = qryAR + " @CreditAmount, @BatchNo, @CompanyGroup,  @ContractNo, @ServiceStatus, @RcnoServiceRecord, @ContractGroup,  "
                        'qryAR = qryAR + " @ModuleName, @ItemCode, @ServiceRecordNo, @ServiceDate, "
                        'qryAR = qryAR + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        'commandAR.CommandText = qryAR
                        'commandAR.Parameters.Clear()

                        'commandAR.Parameters.AddWithValue("@VoucherNumber", txtInvoiceNo.Text)
                        'commandAR.Parameters.AddWithValue("@AccountId", txtAccountIdBilling.Text)
                        'commandAR.Parameters.AddWithValue("@CustomerName", txtAccountName.Text)
                        'If txtInvoiceDate.Text.Trim = "" Then
                        '    commandAR.Parameters.AddWithValue("@VoucherDate", DBNull.Value)
                        'Else
                        '    commandAR.Parameters.AddWithValue("@VoucherDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        'End If

                        'commandAR.Parameters.AddWithValue("@ContractNo", row("ContractNo"))
                        'commandAR.Parameters.AddWithValue("@RcnoServiceRecord", row("RcnoServiceRecord"))
                        'commandAR.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                        'commandAR.Parameters.AddWithValue("@GLCode", row("OtherCode"))
                        'commandAR.Parameters.AddWithValue("@GLDescription", row("GLDescription"))
                        'commandAR.Parameters.AddWithValue("@DebitAmount", Convert.ToDecimal(row("PriceWithDisc")))
                        'commandAR.Parameters.AddWithValue("@CreditAmount", 0.0)
                        'commandAR.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                        'commandAR.Parameters.AddWithValue("@ServiceStatus", row("ServiceStatus"))
                        'commandAR.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                        'commandAR.Parameters.AddWithValue("@ContractGroup", row("ContractGroup"))
                        'commandAR.Parameters.AddWithValue("@ModuleName", "Invoice")
                        'commandAR.Parameters.AddWithValue("@ItemCode", row("ItemCode"))
                        'commandAR.Parameters.AddWithValue("@ServiceRecordNo", row("ServiceRecordNo"))


                        ''commandAR.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        ''Else
                        'commandAR.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(row("ServiceDate")).ToString("yyyy-MM-dd"))
                        ''End If

                        'commandAR.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'commandAR.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'commandAR.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        ''command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'commandAR.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        'commandAR.Connection = conn
                        'commandAR.ExecuteNonQuery()


                        'Start: Update tblServiceRecord
                        If row("ItemCode") = "IN-DEF" Or row("ItemCode") = "IN-SRV" Then
                            Dim command4 As MySqlCommand = New MySqlCommand
                            command4.CommandType = CommandType.Text

                            Dim qry4 As String = "Update tblservicerecord Set BilledAmt = " & Convert.ToDecimal(row("PriceWithDisc")) & ", BillNo = '' where Rcno= @Rcno "
                            'Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y' where Rcno= @Rcno "

                            command4.CommandText = qry4
                            command4.Parameters.Clear()

                            command4.Parameters.AddWithValue("@Rcno", row("RcnoServiceRecord"))
                            command4.Connection = conn
                            command4.ExecuteNonQuery()
                        End If
                    End If

                Next row

                'End: Loop thru' Credit Values

                Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                commandUpdateInvoice.CommandType = CommandType.Text
                Dim sqlUpdateInvoice As String = "Update tblsales set GLStatus = 'O'  where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateInvoice.CommandText = sqlUpdateInvoice
                commandUpdateInvoice.Parameters.Clear()
                commandUpdateInvoice.Connection = conn
                commandUpdateInvoice.ExecuteNonQuery()

                GridView1.DataBind()


                Dim commandUpdateServicebillingdetail As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetail.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetail As String = "Update tblservicebillingdetail set Posted = 'O'  where InvoiceNo  ='" & txtInvoiceNo.Text & "'"

                commandUpdateServicebillingdetail.CommandText = sqlUpdateServicebillingdetail
                commandUpdateServicebillingdetail.Parameters.Clear()
                commandUpdateServicebillingdetail.Connection = conn
                commandUpdateServicebillingdetail.ExecuteNonQuery()

                Dim commandUpdateServicebillingdetailItem As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetailItem.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetailItem As String = "Update tblservicebillingdetailitem set Posted = 'O'  where InvoiceNo  ='" & txtInvoiceNo.Text & "'"

                commandUpdateServicebillingdetailItem.CommandText = sqlUpdateServicebillingdetailItem
                commandUpdateServicebillingdetailItem.Parameters.Clear()
                commandUpdateServicebillingdetailItem.Connection = conn
                commandUpdateServicebillingdetailItem.ExecuteNonQuery()

                MakeMeNullBillingDetails()
                MakeMeNull()
                DisableControls()
                FirstGridViewRowGL()



                ''''''''''''''' Insert tblAR


                lblMessage.Text = "REVERSE: RECORD SUCCESSFULLY REVERSED"
                lblAlert.Text = ""
                updPnlSearch.Update()
                updPnlMsg.Update()
                updpnlBillingDetails.Update()
                updpnlServiceRecs.Update()
                updpnlBillingDetails.Update()
            End If
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub


    ''''''' Start: Service Details



   


    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
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

            'btnSave.Enabled = True
            'btnSave.ForeColor = System.Drawing.Color.Black
            'btnCancel.Enabled = True
            'btnCancel.ForeColor = System.Drawing.Color.Black
            'btnClient.Visible = True


            'btnADD.Enabled = True
            'btnADD.ForeColor = System.Drawing.Color.Black

            btnEdit.Enabled = False
            btnEdit.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnADDRecurring.Enabled = False
            btnADDRecurring.ForeColor = System.Drawing.Color.Gray


            EnableControls()
            rdbNotCompleted.Enabled = True
            rdbCompleted.Enabled = True
            rdbAll.Enabled = True
            ddlCompanyGrp.Enabled = True
            ddlContactType.Enabled = True
            txtClientName.Enabled = True
            txtAccountId.Enabled = True
            txtLocationId.Enabled = True
            ddlContractNo.Enabled = True
            txtDateFrom.Enabled = True
            txtDateTo.Enabled = True
            txtServiceBySearch.Enabled = True
            ddlContractGroup.Enabled = True
            ddlServiceFrequency.Enabled = True
            ddlScheduler.Enabled = True
            ddlBillingFrequency.Enabled = True
            rdbGrouping.Enabled = True

            'txtAccountIdBilling.Enabled = True

            txtInvoiceNo.Enabled = True
            txtBatchDate.Enabled = True
            txtInvoiceDate.Enabled = True

            txtBillingPeriod.Enabled = True
            txtCompanyGroup.Enabled = True
            'txtAccountId.Enabled = True
            txtAccountType.Enabled = True

            txtAccountName.Enabled = True
            txtBillAddress.Enabled = True
            txtBillStreet.Enabled = True
            txtBillBuilding.Enabled = True
            txtBillPostal.Enabled = True
            ddlSalesmanBilling.Enabled = True
            txtInvoiceAmount.Enabled = True
            txtBillCountry.Enabled = True
            txtPONo.Enabled = True
            ddlCreditTerms.Enabled = True
            txtDiscountAmount.Enabled = True
            txtAmountWithDiscount.Enabled = True
            txtGSTAmount.Enabled = True
            txtNetAmount.Enabled = True
            txtOurReference.Enabled = True
            txtYourReference.Enabled = True
            txtComments.Enabled = True
            'btnSaveInvoice.Enabled = True
            btnSave.Enabled = True
            btnShowRecords.Enabled = True
            'btnSaveInvoice.Enabled = True
            'EnableControls()
            chkRecurringInvoice.Enabled = True

            btnSaveInvoice.Enabled = True
            btnSaveInvoice.ForeColor = System.Drawing.Color.Black

            btnDeleteUnselected.Enabled = True
            btnDeleteUnselected.ForeColor = System.Drawing.Color.Black

            btnGenerateInvoice.Enabled = False
            btnGenerateInvoice.ForeColor = System.Drawing.Color.Gray

            If chkRecurringInvoice.Checked = True Then
                txtRecurringInvoiceDate.Enabled = True
                txtRecurringServiceDateFrom.Enabled = True
                txtRecurringServiceDateTo.Enabled = True
                'chkRecurringInvoice.Enabled = True
                ddlRecurringFrequency.Enabled = True

                'txtRecurringInvoiceDate.BackColor = Color.White
                'txtRecurringServiceDateFrom.BackColor = Color.White
                'txtRecurringServiceDateTo.BackColor = Color.White
                'ddlRecurringFrequency.BackColor = Color.White

            Else
                txtRecurringInvoiceDate.Enabled = False
                txtRecurringServiceDateFrom.Enabled = False
                txtRecurringServiceDateTo.Enabled = False
                'chkRecurringInvoice.Enabled = False
                ddlRecurringFrequency.Enabled = False

                'txtRecurringInvoiceDate.BackColor = Color.Gray
                'txtRecurringServiceDateFrom.BackColor = Color.Gray
                'txtRecurringServiceDateTo.BackColor = Color.Gray
                'ddlRecurringFrequency.BackColor = Color.Gray
            End If



            'grvServiceRecDetails.RowDataBound()
            'btnDelete.Enabled = True

            'grvBillingDetails.Enabled = True
            grvServiceRecDetails.Enabled = True

            Dim rowindex As Integer = 0

            For rowindex = 0 To grvServiceRecDetails.Rows.Count - 1
                Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowindex).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
                TextBoxToBillAmt.ReadOnly = False
                TextBoxToBillAmt.BackColor = Color.White
                TextBoxToBillAmt.BorderStyle = BorderStyle.NotSet
                TextBoxToBillAmt.AutoPostBack = True
            Next
            'Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)



            'grvServiceRecDetails_RowDataBound(sender, e)
            UpdatePanel3.Update()
            UpdatePanel1.Update()
            updPnlBillingRecs.Update()
            updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()
            updPanelSave.Update()
            'UpdatePanel1.Update()
            UpdatePanel2.Update()
            'EnableControls()
            'cpnl2.update()
            'UpdatePanel1.Update()
            'btnDelete.Enabled = True
            'btnDelete.ForeColor = System.Drawing.Color.Black
            btnQuit.Enabled = True
            btnQuit.ForeColor = System.Drawing.Color.Black
        Catch ex As Exception
          
            lblAlert.Text = ex.Message.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


   
    Protected Sub btnQuickSearch_Click(sender As Object, e As EventArgs) Handles btnQuickSearch.Click
        Try
            Dim strsql As String


            'PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount, CreditDays, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, rcno
            'strsql = "Select PostStatus, PaidStatus, GlStatus, InvoiceNumber, SalesDate, AccountId, CustName, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal,  "
            'strsql = strsql & " AppliedBase, Salesman, PoNo, OurRef, yourRef, CreditTerms, DiscountAmount, GSTAmount, NetAmount, GLPeriod, CompanyGroup, ContactType, BatchNo, Salesman, Comments, AmountWithDiscount , CreditDays, CreatedBy, RecurringInvoice,  CreatedOn, LastModifiedBy, LastModifiedOn, Rcno from tblservicebillschedule  where 1=1 and STBilling = 'N'  "


            strsql = "Select * from tblservicebillschedule  where 1=1   "


            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                strsql = strsql + " and PostStatus in (" + YrStr + ")"

            End If

            'If txtDisplayRecordsLocationwise.Text = "Y" Then
            '    'txtCondition.Text = txtCondition.Text & " and Location = '" & txtLocation.Text & "'"
            '    strsql = strsql + " and location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
            'End If


            If txtDisplayRecordsLocationwise.Text = "Y" Then
                strsql = strsql + " and Billschedule in (select BillSchedule from tblservicebillingdetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) "
                'GridView1.DataSourceID = "SQLDSInvoice"
                'GridView1.DataBind()
                'Else
                '    strsql = strsql + " and"
                '    'GridView1.DataSourceID = "SQLDSInvoice"
                '    'GridView1.DataBind()
            End If

            If String.IsNullOrEmpty(txtBillingPeriodSearch.Text) = False Then
                strsql = strsql & " and GLPeriod like '%" & txtBillingPeriodSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtInvoicenoSearch.Text) = False Then
                strsql = strsql & " and BillSchedule like '%" & txtInvoicenoSearch.Text.Trim + "%'"
            End If


            If String.IsNullOrEmpty(txtAccountIdSearch.Text) = False Then
                'strsql = strsql & " and (AccountId like '%" & txtAccountIdSearch.Text & "%' or AccountId like '%" & txtAccountIdSearch.Text & "%')"
                strsql = strsql & " and (AccountId like '%" & txtAccountIdSearch.Text & "%')"

            End If

            If String.IsNullOrEmpty(txtClientNameSearch.Text) = False Then
                strsql = strsql & " and CustName like ""%" & txtClientNameSearch.Text & "%"""
            End If

            If (ddlCompanyGrpSearch.SelectedIndex > 0) Then
                strsql = strsql & " and CompanyGroup like '%" & ddlCompanyGrpSearch.Text.Trim + "%'"
            End If

            If (ddlBillingFrequencySearch.SelectedIndex > 0) Then
                strsql = strsql & " and GroupByBillingFrequency = '" & ddlBillingFrequencySearch.Text.Trim + "'"
            End If


            'If String.IsNullOrEmpty(txtNextInvoiceDate.Text) = False Then
            '    strsql = strsql & " and NextInvoiceDate = '" + Convert.ToDateTime(txtNextInvoiceDate.Text).ToString("yyyy-MM-dd") + "'"
            'End If


            'If cbxRecurring.SelectedIndex = 0 Then
            '    strsql = strsql & " and RecurringInvoice = 'Y'"
            'Else
            '    strsql = strsql & " and RecurringInvoice = 'N'"
            'End If


            'If cbxRecurring.SelectedIndex = 1 Then
            '    strsql = strsql & " and RecurringScheduled = 'Y'"
            'Else
            '    strsql = strsql & " and RecurringScheduled = 'N'"
            'End If




            strsql = strsql + " order by BSDate desc, BillSchedule desc"
            txt.Text = strsql
            txtComments.Text = strsql
            SQLDSInvoice.SelectCommand = strsql
            SQLDSInvoice.DataBind()
            GridView1.DataBind()

            'GridSelected = "SQLDSContract"
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = exstr
            'Dim message As String = "alert('" + exstr + "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End Try
    End Sub

    Protected Sub btnShowRecords_Click(sender As Object, e As EventArgs) Handles btnShowRecords.Click
        'MakeMeNull()
        lblAlert.Text = ""


        If txtDeSelectCompanyGroup.Text = False Then
            If ddlCompanyGrp.SelectedIndex = 0 Then
                lblAlert.Text = "SELECT COMPANY GROUP"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                updPnlMsg.Update()
                Exit Sub
            End If
        End If


        'If ddlCompanyGrp.SelectedIndex = 0 Then
        '    lblAlert.Text = "SELECT COMPANY GROUP"
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    updPnlMsg.Update()
        '    Exit Sub
        'End If

        If txtDeSelectContractGroup.Text = False Then
            If ddlContractGroup.SelectedIndex = 0 Then
                lblAlert.Text = "SELECT CONTRACT GROUP"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                updPnlMsg.Update()
                Exit Sub
            End If
        End If

        If ddlBillingFrequency.SelectedIndex = 0 Then
            lblAlert.Text = "SELECT BILLING FREQUENCY"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            updPnlMsg.Update()
            Exit Sub
        End If

        'If String.IsNullOrEmpty(txtServiceBySearch.Text.Trim) = True Then
        '    lblAlert.Text = "ENTER SERVICE BY"
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        '    updPnlMsg.Update()
        '    Exit Sub
        'End If


        Dim dt1 As Date
        If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
            If Date.TryParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt1) Then
                txtDateFrom.Text = dt1.ToShortDateString

            Else
                lblAlert.Text = "'Service Date From' is Invalid"
                updPnlMsg.Update()
                txtDateFrom.Focus()
                Return
                Exit Sub

            End If
        End If

        Dim dt2 As Date
        If String.IsNullOrEmpty(txtDateTo.Text) = False Then
            If Date.TryParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt2) Then
                txtDateTo.Text = dt2.ToShortDateString

            Else
                lblAlert.Text = "'Service Date To' is Invalid"
                updPnlMsg.Update()
                txtDateTo.Focus()
                Return
                Exit Sub

            End If
        End If


        updPanelInvoice.Update()

        PopulateServiceGrid()


        updpnlServiceRecs.Update()

        btnSaveInvoice.Enabled = True
        btnSaveInvoice.ForeColor = System.Drawing.Color.Black

        btnDeleteUnselected.Enabled = True
        btnDeleteUnselected.ForeColor = System.Drawing.Color.Black
        updPanelSave.Update()
        updPanelInvoice.Update()

        'btnSave.Enabled = True
        'updPanelSave.Update()
    End Sub

    Private Sub PopulateServiceGrid()

        Try

            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim sql As String
            Dim sql1 As String
            Dim sql2 As String
            Dim sqledit As String

            sql = ""
            sql1 = ""
            sql2 = ""
            sqledit = ""

            If txtMode.Text = "View" Then
                UpdatePanel3.Update()
                sql = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.Name,  A.LocationId, A.Address1,  A.BillAmount, "
                sql = sql + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef,  A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, A.Salesman,"
                sql = sql + " A.BillAddress1, A.BillBuilding, A.BillStreet, A.BillPostal, A.BillCity, A.BillCountry, A.BillingFrequency, A.RcnoInvoice,"
                sql = sql + " A.Rcno as rcnotblservicebillingdetail, A.ContractGroup, A.DiscountAmount, A.GSTAmount, A.TotalWithGST, A.NetAmount,   "
                sql = sql + " A.ArTerm, A.TermsDay, A.RcnoServiceRecord,A.ServiceBy, A.ServiceLocationGroup, A.Notes,   "
                sql = sql + " A.AgreeValue, A.Duration, A.DurationMS, A.PerServiceValue, 'E' as RecordType, A.Location, A.Remarks  "
                sql = sql + " FROM tblservicebillingdetail A "
                'sql = sql + " where 1 = 1 and RcnoInvoice =" & txtRcno.Text
                sql = sql + " where 1 = 1 and BillSchedule = '" & txtInvoiceNo.Text & "'"
            End If

            If txtMode.Text = "EDIT" Then
                UpdatePanel3.Update()
                'sqledit = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.Name,  A.LocationId, A.Address1,  A.BillAmount, "
                'sqledit = sqledit + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef,  A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, A.Salesman,"
                'sqledit = sqledit + " A.BillAddress1, A.BillBuilding, A.BillStreet, A.BillPostal, A.BillCity, A.BillCountry, A.BillingFrequency, A.RcnoInvoice,"
                'sqledit = sqledit + " A.Rcno as rcnotblservicebillingdetail, A.ContractGroup, A.DiscountAmount, A.GSTAmount, A.TotalWithGST, A.NetAmount,   "
                'sqledit = sqledit + " A.ArTerm, A.TermsDay, A.RcnoServiceRecord,A.ServiceBy, A.ServiceLocationGroup, A.Notes,   "
                'sqledit = sqledit + " A.AgreeValue, A.Duration, A.DurationMS, A.PerServiceValue, 'E' as RecordType  "

                sqledit = "Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.Name,  A.LocationId, A.Address1,  A.BillAmount, "
                sqledit = sqledit + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef,  A.Status, A.ContractNo,  A.ContactType, A.CompanyGroup, "
                sqledit = sqledit + " A.ServiceBy, A.ServiceLocationGroup, A.Notes, A.AgreeValue, A.Duration, A.DurationMS, "
                sqledit = sqledit + " A.BillingFrequency, A.RcnoInvoice, A.Rcno as rcnotblservicebillingdetail, A.ContractGroup,  A.PerServiceValue, 'E' as RecordType, A.Location, A.Remarks "
                sqledit = sqledit + " FROM tblservicebillingdetail A "
                'sql = sql + " where 1 = 1 and RcnoInvoice =" & txtRcno.Text
                sqledit = sqledit + " where 1 = 1 and BillSchedule = '" & txtInvoiceNo.Text & "'"
            End If

            If txtMode.Text = "NEW" Then

                'sql = " Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, (A.BillAmount - A.BilledAmt) as BillAmount,"
                'sql = sql + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.Location, "
                'sql = sql + "  A.ServiceBy, A.ServiceLocationGroup,  A.Notes,  C.AgreeValue, C.Duration, C.DurationMS,  "
                'sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup , ifnull(sum(D.value),0) as PerServiceValue, 'A' as RecordType, A.Location, A.Remarks "
                'sql = sql + " FROM"
                ''sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo, tblContractdet D "
                'sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo "
                'sql = sql + " LEFT OUTER JOIN  tblContractdet D on  C.AccountID = D.AccountID and  C.ContractNo = D.ContractNo "
                'sql = sql + "  where 1 = 1  and (((A.BillAmount - A.BilledAmt) > 0) or ((A.BillAmount = 0) and (A.BillYN = 'N')))  and (A.Status = 'O' or A.Status = 'P') "
                ''sql = sql + " and C.ContractNo = D.ContractNo and   C.AccountID = D.AccountID and A.LocationId = D.LocationID "

                If chkIncludeZeroValueServices.Checked = True Then
                    sql = " Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, (A.BillAmount - A.BilledAmt) as BillAmount,"
                    sql = sql + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.Location, "
                    sql = sql + "  A.ServiceBy, A.ServiceLocationGroup,  A.Notes,  C.AgreeValue, C.Duration, C.DurationMS,  "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup , ifnull(sum(D.value),0) as PerServiceValue, 'A' as RecordType, A.Remarks "
                    sql = sql + " FROM"
                    'sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo, tblContractdet D "
                    sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo "
                    sql = sql + " LEFT OUTER JOIN  tblContractdet D on  C.AccountID = D.AccountID and  C.ContractNo = D.ContractNo "
                    sql = sql + "  where 1 = 1  and (((A.BillAmount - A.BilledAmt) > 0) or ((A.BillAmount = 0) and (A.BillYN = 'N')))  and (A.Status = 'O' or A.Status = 'P') "
                    'sql = sql + " and C.ContractNo = D.ContractNo and   C.AccountID = D.AccountID and A.LocationId = D.LocationID "
                Else
                    sql = " Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, (A.BillAmount - A.BilledAmt) as BillAmount,"
                    sql = sql + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, A.Location, "
                    sql = sql + "  A.ServiceBy, A.ServiceLocationGroup,  A.Notes,  C.AgreeValue, C.Duration, C.DurationMS,  "
                    sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup , ifnull(sum(D.value),0) as PerServiceValue, 'A' as RecordType,  A.Remarks "
                    sql = sql + " FROM"
                    'sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo, tblContractdet D "
                    sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo "
                    sql = sql + " LEFT OUTER JOIN  tblContractdet D on  C.AccountID = D.AccountID and  C.ContractNo = D.ContractNo "
                    sql = sql + "  where 1 = 1  and (((A.BillAmount - A.BilledAmt) > 0) or ((A.BillAmount <> 0) and (A.BillYN = 'N')))  and (A.Status = 'O' or A.Status = 'P') "
                    'sql = sql + " and C.ContractNo = D.ContractNo and   C.AccountID = D.AccountID and A.LocationId = D.LocationID "
                End If

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    sql = sql + " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                End If

                'If chkIncludeServicesWithCN.Checked = False Then
                '    sql = sql + " and A.RecordNo not in (Select RefType from tblsalesdetail)"
                'End If

                If chkIncludeServicesWithCN.Checked = True Then
                    sql = sql + " and A.RecordNo in (Select RefType from tblsalesdetail)"
                End If


                If ddlCompanyGrp.Text.Trim <> "-1" Then
                    sql = sql + " and   A.CompanyGroup = '" & ddlCompanyGrp.Text.Trim & "'"
                End If

                If ddlContactType.SelectedIndex > 0 Then
                    sql = sql + " and   A.ContactType = '" & ddlContactType.Text.Trim & "'"
                End If

                If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
                    sql = sql + " and  A.AccountID like '%" & txtAccountId.Text & "%'"
                Else
                    If String.IsNullOrEmpty(txtClientName.Text.Trim) = False Then
                        sql = sql + " and  A.CustName like ""%" & txtClientName.Text & "%"""
                    End If
                End If

                'If String.IsNullOrEmpty(txtClientName.Text) = False Then
                '    sql = sql + " and  A.ServiceName like '%" & txtClientName.Text & "%'"
                'End If


                If String.IsNullOrEmpty(txtLocationId.Text.Trim) = False Then
                    sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
                End If

                If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                    'If ddlContractNo.Text.Trim <> "-1" Then
                    sql = sql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = False And String.IsNullOrEmpty(txtDateTo.Text) = True Then
                    sql = sql + " and   A.ServiceDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = True And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If




                If ddlServiceFrequency.Text.Trim <> "-1" Then
                    sql = sql + " and   D.Frequency = '" & ddlServiceFrequency.Text.Trim & "'"
                End If

                If ddlBillingFrequency.Text.Trim <> "-1" Then
                    sql = sql + " and   C.BillingFrequency = '" & ddlBillingFrequency.Text.Trim & "'"
                End If

                If ddlContractGroup.Text.Trim <> "-1" Then
                    sql = sql + " and   C.ContractGroup = '" & ddlContractGroup.Text.Trim & "'"
                End If


                If String.IsNullOrEmpty(txtServiceBySearch.Text.Trim) = False Then
                    sql = sql + " and   A.ServiceBy = '" & txtServiceBySearch.Text & "'"
                End If

               

                If rdbCompleted.Checked = True Then
                    sql = sql + " and  A.Status = 'P' "
                End If

                If rdbNotCompleted.Checked = True Then
                    sql = sql + " and  A.Status = 'O' "
                End If

                If rdbAll.Checked = True Then
                    sql = sql + " and  ((A.Status = 'O') or (A.Status = 'P')) "
                End If

                '' Location
                If txtDisplayRecordsLocationwise.Text = "Y" Then

                    Dim YrStrList1 As List(Of [String]) = New List(Of String)()

                    For Each item As ListItem In ddlLocationSearch.Items
                        If item.Selected Then

                            YrStrList1.Add("""" + item.Value + """")

                        End If
                    Next

                    If YrStrList1.Count > 0 Then

                        Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                        sql = sql + " and A.location in (" + YrStr + ")"
                   
                    Else
                        sql = sql & " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                    End If

                End If
            End If
            ''Location

            If txtMode.Text = "EDIT" Then

                sql = " Select A.Rcno, A.ContactType, A.RecordNo, A.AccountID, A.ContractNo, A.CustName, A.ServiceName,  A.LocationId, A.Address1, (A.BillAmount - A.BilledAmt) as BillAmount,"
                sql = sql + " A.ServiceDate, A.BillNo, A.PoNo, A.OurRef, A.YourRef, A.Status, A.ContractNo, A.ContactType, A.CompanyGroup, "
                sql = sql + "  A.ServiceBy, A.ServiceLocationGroup,  A.Notes,  C.AgreeValue, C.Duration, C.DurationMS,  "
                sql = sql + " C.BillingFrequency, 0 as RcnoInvoice, 0 as rcnotblservicebillingdetail, C.ContractGroup , ifnull(sum(D.value),0) as PerServiceValue, 'A' as RecordType, A.Location, A.Remarks "
                sql = sql + "  FROM"
                'sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo, tblContractdet D "
                sql = sql + " tblservicerecord A LEFT OUTER JOIN tblContract C ON A.AccountID = C.AccountID and A.ContractNo = C.ContractNo "
                sql = sql + " LEFT OUTER JOIN  tblContractdet D on  C.AccountID = D.AccountID and  C.ContractNo = D.ContractNo "
                sql = sql + "  where 1 = 1  and (((A.BillAmount - A.BilledAmt) > 0) or ((A.BillAmount = 0) and (A.BillYN = 'N')))  and (A.Status = 'O' or A.Status = 'P') "
                'sql = sql + " and C.ContractNo = D.ContractNo and   C.AccountID = D.AccountID and A.LocationId = D.LocationID "
                sql = sql + " and RecordNo Not In (select RecordNo from tblservicebillingdetail where BillSchedule ='" & txtInvoiceNo.Text & "')"

                If ddlCompanyGrp.Text.Trim <> "-1" Then
                    sql = sql + " and   A.CompanyGroup = '" & ddlCompanyGrp.Text.Trim & "'"
                End If

                If ddlContactType.SelectedIndex > 0 Then
                    sql = sql + " and   A.ContactType = '" & ddlContactType.Text.Trim & "'"
                End If

                If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
                    sql = sql + " and  A.AccountID like '%" & txtAccountId.Text & "%'"
                Else
                    If String.IsNullOrEmpty(txtClientName.Text.Trim) = False Then
                        sql = sql + " and  A.CustName like ""%" & txtClientName.Text & "%"""
                    End If
                End If

                'If String.IsNullOrEmpty(txtClientName.Text) = False Then
                '    sql = sql + " and  A.ServiceName like '%" & txtClientName.Text & "%'"
                'End If


                If String.IsNullOrEmpty(txtLocationId.Text.Trim) = False Then
                    sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
                End If

                If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                    'If ddlContractNo.Text.Trim <> "-1" Then
                    sql = sql + " and   A.ContractNo = '" & ddlContractNo.Text & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = False And String.IsNullOrEmpty(txtDateTo.Text) = True Then
                    sql = sql + " and   A.ServiceDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = True And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.ServiceDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If


                If ddlServiceFrequency.Text.Trim <> "-1" Then
                    sql = sql + " and   D.Frequency = '" & ddlServiceFrequency.Text.Trim & "'"
                End If

                If ddlBillingFrequency.Text.Trim <> "-1" Then
                    sql = sql + " and   C.BillingFrequency = '" & ddlBillingFrequency.Text.Trim & "'"
                End If

                If ddlContractGroup.Text.Trim <> "-1" Then
                    sql = sql + " and   C.ContractGroup = '" & ddlContractGroup.Text.Trim & "'"
                End If


                If String.IsNullOrEmpty(txtServiceBySearch.Text.Trim) = False Then
                    sql = sql + " and   A.ServiceBy = '" & txtServiceBySearch.Text & "'"
                End If


                If rdbCompleted.Checked = True Then
                    sql = sql + " and  A.Status = 'P' "
                End If

                If rdbNotCompleted.Checked = True Then
                    sql = sql + " and  A.Status = 'O' "
                End If

                If rdbAll.Checked = True Then
                    sql = sql + " and  ((A.Status = 'O') or (A.Status = 'P')) "
                End If


                '' Location
                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    Dim YrStrList1 As List(Of [String]) = New List(Of String)()

                    For Each item As ListItem In ddlLocationSearch.Items
                        If item.Selected Then

                            YrStrList1.Add("""" + item.Value + """")

                        End If
                    Next

                    If YrStrList1.Count > 0 Then
                        Dim YrStr As [String] = [String].Join(",", YrStrList1.ToArray)
                        sql = sql + " and A.location in (" + YrStr + ")"
                    Else
                        sql = sql & " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                    End If

                End If

                ''Location


            End If
            If txtMode.Text = "EDIT" Then
                sql = sqledit + " UNION " + sql
            End If


            sql = sql + " group by A.RecordNo "
            sql1 = sql

            txtShowRecords.Text = sql
            txtShowRecordsSort.Text = sql

            'If rdbGrouping.SelectedIndex = 0 Then
            '    sql2 = sql2 + " order by A.LocationId, A.ServiceDate"
            'ElseIf rdbGrouping.SelectedIndex = 1 Then
            '    sql2 = sql2 + " order by A.LocationId, A.ServiceDate"
            'ElseIf rdbGrouping.SelectedIndex = 2 Then
            '    sql2 = sql2 + " order by A.LocationId, A.ServiceDate"
            'ElseIf rdbGrouping.SelectedIndex = 3 Then
            '    sql2 = sql2 + " order by  A.ContractNo, A.ServiceDate"
            'End If
            If rdbGrouping.SelectedIndex = 0 Then
                sql2 = sql2 + " order by LocationId, ServiceDate"
            ElseIf rdbGrouping.SelectedIndex = 1 Then
                sql2 = sql2 + " order by LocationId, ServiceDate"
            ElseIf rdbGrouping.SelectedIndex = 2 Then
                sql2 = sql2 + " order by LocationId, ServiceDate"
            ElseIf rdbGrouping.SelectedIndex = 3 Then
                sql2 = sql2 + " order by  ContractNo, ServiceDate"
            End If

            txtShowRecords.Text = sql1
            txtShowRecordsSort.Text = sql2

            sql = sql1 + sql2
            SqlDSServices.SelectCommand = sql
            grvServiceRecDetails.DataSourceID = "SqlDSServices"
            grvServiceRecDetails.DataBind()


            Label43.Text = "SERVICE BILLING DETAILS : Total Records :" & grvServiceRecDetails.Rows.Count.ToString
            updpnlServiceRecs.Update()

           

            'txtClientName.Text = sql

            'Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            If txtMode.Text = "View" Then
                If txtPostStatus.Text = "O" Then
                    If (grvServiceRecDetails.Rows.Count > 0) Then
                        For i As Integer = 0 To (grvServiceRecDetails.Rows.Count) - 1
                            Dim TextBoxSel As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)
                            TextBoxSel.Enabled = True
                            TextBoxSel.Checked = True
                        Next i
                    End If

                Else
                    If (grvServiceRecDetails.Rows.Count > 0) Then
                        For i As Integer = 0 To (grvServiceRecDetails.Rows.Count) - 1
                            Dim TextBoxSel As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("chkSelectGV"), CheckBox)
                            TextBoxSel.Enabled = False
                            TextBoxSel.Checked = True
                        Next i
                    End If

                End If

            End If
            'End: Service Recods
            grvServiceRecDetails.Enabled = True

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "PopulateServiceGrid", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub
    '''''''''' Start: Service Record


    Private Sub FirstGridViewRowServiceRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("SelRec", GetType(String)))
            dt.Columns.Add(New DataColumn("AccountId", GetType(String)))
            dt.Columns.Add(New DataColumn("ClientName", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceRecordNo", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceDate", GetType(String)))
            dt.Columns.Add(New DataColumn("BillingFrequency", GetType(String)))
            dt.Columns.Add(New DataColumn("LocationId", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceAddress", GetType(String)))
            dt.Columns.Add(New DataColumn("ToBillAmt", GetType(String)))

            dt.Columns.Add(New DataColumn("Dept", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractNo", GetType(String)))
            dt.Columns.Add(New DataColumn("Status", GetType(String)))

            dt.Columns.Add(New DataColumn("RcnoServicebillingdetail", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoServiceRecord", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoServiceRecordDetail", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoInvoice", GetType(String)))
            dt.Columns.Add(New DataColumn("ContactType", GetType(String)))
            dt.Columns.Add(New DataColumn("CompanyGroup", GetType(String)))

            dt.Columns.Add(New DataColumn("AccountName", GetType(String)))
            dt.Columns.Add(New DataColumn("BillAddress1", GetType(String)))
            dt.Columns.Add(New DataColumn("BillBuilding", GetType(String)))
            dt.Columns.Add(New DataColumn("BillStreet", GetType(String)))
            dt.Columns.Add(New DataColumn("BillCountry", GetType(String)))
            dt.Columns.Add(New DataColumn("BillPostal", GetType(String)))
            dt.Columns.Add(New DataColumn("OurReference", GetType(String)))
            dt.Columns.Add(New DataColumn("YourReference", GetType(String)))
            dt.Columns.Add(New DataColumn("PoNo", GetType(String)))
            dt.Columns.Add(New DataColumn("CreditTerms", GetType(String)))
            dt.Columns.Add(New DataColumn("Salesman", GetType(String)))

            dt.Columns.Add(New DataColumn("DiscAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("NetAmount", GetType(String)))

            dt.Columns.Add(New DataColumn("ServiceBy", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceLocationGroup", GetType(String)))

            dt.Columns.Add(New DataColumn("AgreeValue", GetType(String)))
            dt.Columns.Add(New DataColumn("Duration", GetType(String)))
            dt.Columns.Add(New DataColumn("DurationMS", GetType(String)))
            dt.Columns.Add(New DataColumn("PerServiceValue", GetType(String)))
            dt.Columns.Add(New DataColumn("RecordType", GetType(String)))
            dt.Columns.Add(New DataColumn("Remarks", GetType(String)))

            dr = dt.NewRow()

            dr("SelRec") = String.Empty
            dr("AccountId") = String.Empty
            dr("ClientName") = String.Empty
            dr("ServiceRecordNo") = String.Empty
            dr("ServiceDate") = String.Empty
            dr("BillingFrequency") = String.Empty
            dr("LocationId") = String.Empty
            dr("ServiceAddress") = String.Empty
            dr("ToBillAmt") = 0

            dr("Dept") = String.Empty
            dr("ContractNo") = String.Empty
            dr("Status") = String.Empty

            dr("RcnoServicebillingdetail") = 0
            dr("RcnoServiceRecord") = 0
            dr("RcnoServiceRecordDetail") = 0
            dr("RcnoInvoice") = 0
            dr("ContactType") = String.Empty
            dr("CompanyGroup") = String.Empty

            dr("AccountName") = String.Empty
            dr("BillAddress1") = String.Empty
            dr("BillBuilding") = String.Empty
            dr("BillStreet") = String.Empty
            dr("BillCountry") = String.Empty
            dr("BillPostal") = String.Empty
            dr("OurReference") = String.Empty
            dr("YourReference") = String.Empty
            dr("PoNo") = String.Empty
            dr("CreditTerms") = String.Empty
            dr("Salesman") = String.Empty

            dr("DiscAmount") = String.Empty
            dr("GSTAmount") = String.Empty
            dr("NetAmount") = String.Empty
            dr("ServiceBy") = String.Empty
            dr("InvoiceNo") = String.Empty
            dr("ServiceLocationGroup") = String.Empty

            dr("AgreeValue") = String.Empty
            dr("Duration") = String.Empty
            dr("DurationMS") = String.Empty
            dr("PerServiceValue") = String.Empty
            dr("RecordType") = String.Empty
            dr("Remarks") = String.Empty

            dt.Rows.Add(dr)

            ViewState("CurrentTableServiceRec") = dt

            grvServiceRecDetails.DataSource = dt
            grvServiceRecDetails.DataBind()

            'Dim btnAdd As Button = CType(grvServiceRecDetails.FooterRow.Cells(1).FindControl("btnViewEdit"), Button)
            'Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Private Sub AddNewRowServiceRecs()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String

            If ViewState("CurrentTableServiceRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxAccountId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxClientName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(1).FindControl("txtClientNameGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim TextBoxServiceDate As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceDateGV"), TextBox)
                        Dim TextBoxBillingFrequency As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(4).FindControl("txtBillingFrequencyGV"), TextBox)
                        Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(5).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(6).FindControl("txtServiceAddressGV"), TextBox)
                        Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(8).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        'Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        'Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        'Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        'Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        'Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        'Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        'Dim TextBoxDiscAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(26).FindControl("txtGSTAmountGV"), TextBox)
                        'Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(27).FindControl("txtNetAmountGV"), TextBox)

                        Dim TextBoxServiceBy As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtServiceByGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxServiceLocationGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceLocationGroupGV"), TextBox)


                        Dim TextBoxAgreeValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtAgreeValueGV"), TextBox)
                        Dim TextBoxDuration As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtDurationGV"), TextBox)
                        Dim TextBoxDurationMS As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtDurationMSGV"), TextBox)
                        Dim TextBoxPerServiceValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtPerServiceValueGV"), TextBox)
                        Dim TextBoxRecordType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtRecordTypeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceRemarksGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()


                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("AccountId") = TextBoxAccountId.Text
                        dtCurrentTable.Rows(i - 1)("ClientName") = TextBoxClientName.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceDate") = TextBoxServiceDate.Text
                        dtCurrentTable.Rows(i - 1)("BillingFrequency") = TextBoxBillingFrequency.Text
                        dtCurrentTable.Rows(i - 1)("LocationId") = TextBoxLocationId.Text
                        dtCurrentTable.Rows(i - 1)("ServiceAddress") = TextBoxServiceAddress.Text
                        dtCurrentTable.Rows(i - 1)("ToBillAmt") = TextBoxToBillAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        'dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        'dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        'dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        'dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        'dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        'dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text

                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmount") = TextBoxGSTAmount.Text
                        'dtCurrentTable.Rows(i - 1)("NetAmount") = TextBoxNetAmount.Text

                        dtCurrentTable.Rows(i - 1)("ServiceBy") = TextBoxServiceBy.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceLocationGroup") = TextBoxServiceLocationGroup.Text

                        dtCurrentTable.Rows(i - 1)("AgreeValue") = TextBoxAgreeValue.Text
                        dtCurrentTable.Rows(i - 1)("Duration") = TextBoxDuration.Text
                        dtCurrentTable.Rows(i - 1)("DurationMS") = TextBoxDurationMS.Text
                        dtCurrentTable.Rows(i - 1)("PerServiceValue") = TextBoxPerServiceValue.Text
                        dtCurrentTable.Rows(i - 1)("RecordType") = TextBoxRecordType.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text

                        If TextBoxStatus.Text = "P" Then
                            TextBoxStatus.ForeColor = Color.Blue
                            TextBoxAccountId.ForeColor = Color.Blue
                            TextBoxClientName.ForeColor = Color.Blue
                            TextBoxServiceRecordNo.ForeColor = Color.Blue
                            TextBoxServiceDate.ForeColor = Color.Blue
                            TextBoxBillingFrequency.ForeColor = Color.Blue
                            TextBoxLocationId.ForeColor = Color.Blue
                            TextBoxServiceAddress.ForeColor = Color.Blue
                            TextBoxToBillAmt.ForeColor = Color.Blue
                            TextBoxContractNo.ForeColor = Color.Blue
                            TextBoxDept.ForeColor = Color.Blue
                        End If

                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableServiceRec") = dtCurrentTable

                    grvServiceRecDetails.DataSource = dtCurrentTable
                    grvServiceRecDetails.DataBind()



                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataServiceRecs()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Private Sub AddNewRowWithDetailRecServiceRecs()
        Try
            Dim rowIndex As Integer = 0
            'Dim Query As String
            If ViewState("CurrentTableServiceRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If TotDetailRecords > 0 Then
                    For i As Integer = 1 To (dtCurrentTable.Rows.Count)

                        Dim TextBoxSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxAccountId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxClientName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(1).FindControl("txtClientNameGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim TextBoxServiceDate As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceDateGV"), TextBox)
                        Dim TextBoxBillingFrequency As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(4).FindControl("txtBillingFrequencyGV"), TextBox)
                        Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(5).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(6).FindControl("txtServiceAddressGV"), TextBox)
                        Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(8).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        'Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        'Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        'Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        'Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        'Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        'Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        'Dim TextBoxDiscAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(26).FindControl("txtGSTAmountGV"), TextBox)
                        'Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(27).FindControl("txtNetAmountGV"), TextBox)

                        Dim TextBoxServiceBy As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtServiceByGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxServiceLocationGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceLocationGroupGV"), TextBox)

                        Dim TextBoxAgreeValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtAgreeValueGV"), TextBox)
                        Dim TextBoxDuration As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtDurationGV"), TextBox)
                        Dim TextBoxDurationMS As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtDurationMSGV"), TextBox)
                        Dim TextBoxPerServiceValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtPerServiceValueGV"), TextBox)
                        Dim TextBoxRecordType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtRecordTypeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceRemarksGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Checked
                        dtCurrentTable.Rows(i - 1)("AccountId") = TextBoxAccountId.Text
                        dtCurrentTable.Rows(i - 1)("ClientName") = TextBoxClientName.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceDate") = TextBoxServiceDate.Text
                        dtCurrentTable.Rows(i - 1)("BillingFrequency") = TextBoxBillingFrequency.Text
                        dtCurrentTable.Rows(i - 1)("LocationId") = TextBoxLocationId.Text
                        dtCurrentTable.Rows(i - 1)("ServiceAddress") = TextBoxServiceAddress.Text
                        dtCurrentTable.Rows(i - 1)("ToBillAmt") = TextBoxToBillAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        'dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        'dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        'dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        'dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        'dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        'dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text

                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmount") = TextBoxGSTAmount.Text
                        'dtCurrentTable.Rows(i - 1)("NetAmount") = TextBoxNetAmount.Text

                        dtCurrentTable.Rows(i - 1)("ServiceBy") = TextBoxServiceBy.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceLocationGroup") = TextBoxServiceLocationGroup.Text

                        dtCurrentTable.Rows(i - 1)("AgreeValue") = TextBoxAgreeValue.Text
                        dtCurrentTable.Rows(i - 1)("Duration") = TextBoxDuration.Text
                        dtCurrentTable.Rows(i - 1)("DurationMS") = TextBoxDurationMS.Text
                        dtCurrentTable.Rows(i - 1)("PerServiceValue") = TextBoxPerServiceValue.Text
                        dtCurrentTable.Rows(i - 1)("RecordType") = TextBoxRecordType.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text
                        rowIndex += 1

                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableServiceRec") = dtCurrentTable

                    grvServiceRecDetails.DataSource = dtCurrentTable
                    grvServiceRecDetails.DataBind()



                End If


            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataServiceRecs()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Private Sub SetPreviousDataServiceRecs()
        Try
            Dim rowIndex As Integer = 0

            'Dim Query As String

            If ViewState("CurrentTableServiceRec") IsNot Nothing Then
                Dim dt As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim TextBoxSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxAccountId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxClientName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(1).FindControl("txtClientNameGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim TextBoxServiceDate As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceDateGV"), TextBox)
                        Dim TextBoxBillingFrequency As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(4).FindControl("txtBillingFrequencyGV"), TextBox)
                        Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(5).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(6).FindControl("txtServiceAddressGV"), TextBox)
                        Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(8).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        'Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        'Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        'Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        'Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        'Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        'Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        'Dim TextBoxDiscAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(26).FindControl("txtGSTAmountGV"), TextBox)
                        'Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(27).FindControl("txtNetAmountGV"), TextBox)

                        Dim TextBoxServiceBy As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtServiceByGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxServiceLocationGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceLocationGroupGV"), TextBox)

                        Dim TextBoxAgreeValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtAgreeValueGV"), TextBox)
                        Dim TextBoxDuration As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtDurationGV"), TextBox)
                        Dim TextBoxDurationMS As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtDurationMSGV"), TextBox)
                        Dim TextBoxPerServiceValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtPerServiceValueGV"), TextBox)
                        Dim TextBoxRecordType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtRecordTypeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceRemarksGV"), TextBox)


                        TextBoxSelect.Text = dt.Rows(i)("SelRec").ToString()
                        TextBoxAccountId.Text = dt.Rows(i)("AccountId").ToString()
                        TextBoxClientName.Text = dt.Rows(i)("ClientName").ToString()
                        TextBoxServiceRecordNo.Text = dt.Rows(i)("ServiceRecordNo").ToString()
                        TextBoxServiceDate.Text = dt.Rows(i)("ServiceDate").ToString()
                        TextBoxBillingFrequency.Text = dt.Rows(i)("BillingFrequency").ToString()
                        TextBoxLocationId.Text = dt.Rows(i)("LocationId").ToString()
                        TextBoxServiceAddress.Text = dt.Rows(i)("ServiceAddress").ToString()
                        TextBoxToBillAmt.Text = dt.Rows(i)("ToBillAmt").ToString()
                        TextBoxRcnoServiceRecord.Text = dt.Rows(i)("RcnoServiceRecord").ToString()
                        'TextBoxRcnoServiceRecordDetail.Text = dt.Rows(i)("RcnoServiceRecordDetail").ToString()
                        TextBoxRcnoInvoice.Text = dt.Rows(i)("RcnoInvoice").ToString()
                        TextBoxContactType.Text = dt.Rows(i)("ContactType").ToString()
                        TextBoxCompanyGroup.Text = dt.Rows(i)("CompanyGroup").ToString()

                        TextBoxAccountName.Text = dt.Rows(i)("AccountName").ToString()

                        'TextBoxBillAddress1.Text = dt.Rows(i)("BillAddress1").ToString()
                        'TextBoxBillBuilding.Text = dt.Rows(i)("BillBuilding").ToString()
                        'TextBoxBillStreet.Text = dt.Rows(i)("BillStreet").ToString()
                        'TextBoxBillCountry.Text = dt.Rows(i)("BillCountry").ToString()
                        'TextBoxBillPostal.Text = dt.Rows(i)("BillPostal").ToString()
                        TextBoxOurReference.Text = dt.Rows(i)("OurReference").ToString()
                        TextBoxYourReference.Text = dt.Rows(i)("YourReference").ToString()
                        TextBoxPoNo.Text = dt.Rows(i)("PoNo").ToString()
                        TextBoxCreditTerms.Text = dt.Rows(i)("CreditTerms").ToString()
                        TextBoxSalesman.Text = dt.Rows(i)("Salesman").ToString()
                        TextBoxRcnoServicebillingdetail.Text = dt.Rows(i)("RcnoServicebillingdetail").ToString()

                        TextBoxDept.Text = dt.Rows(i)("Dept").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxStatus.Text = dt.Rows(i)("Status").ToString()

                        'TextBoxDiscAmount.Text = dt.Rows(i)("DiscAmount").ToString()
                        'TextBoxGSTAmount.Text = dt.Rows(i)("GSTAmount").ToString()
                        'TextBoxNetAmount.Text = dt.Rows(i)("NetAmount").ToString()

                        TextBoxServiceBy.Text = dt.Rows(i)("ServiceBy").ToString()
                        TextBoxInvoiceNo.Text = dt.Rows(i)("InvoiceNo").ToString()
                        TextBoxServiceLocationGroup.Text = dt.Rows(i)("ServiceLocationGroup").ToString()


                        TextBoxAgreeValue.Text = dt.Rows(i)("AgreeValue").ToString()
                        TextBoxDuration.Text = dt.Rows(i)("Duration").ToString()
                        TextBoxDurationMS.Text = dt.Rows(i)("DurationMS").ToString()
                        TextBoxPerServiceValue.Text = dt.Rows(i)("PerServiceValue").ToString()
                        TextBoxRecordType.Text = dt.Rows(i)("RecordType").ToString()
                        TextBoxRemarks.Text = dt.Rows(i)("Remarks").ToString()

                        If TextBoxStatus.Text = "P" Then
                            TextBoxStatus.ForeColor = Color.Blue
                            TextBoxAccountId.ForeColor = Color.Blue
                            TextBoxClientName.ForeColor = Color.Blue
                            TextBoxServiceRecordNo.ForeColor = Color.Blue
                            TextBoxServiceDate.ForeColor = Color.Blue
                            TextBoxBillingFrequency.ForeColor = Color.Blue
                            TextBoxLocationId.ForeColor = Color.Blue
                            TextBoxServiceAddress.ForeColor = Color.Blue
                            TextBoxToBillAmt.ForeColor = Color.Blue
                            TextBoxContractNo.ForeColor = Color.Blue
                            TextBoxDept.ForeColor = Color.Blue
                        End If

                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Private Sub SetRowDataServiceRecs()
        Dim rowIndex As Integer = 0
        Try
            If ViewState("CurrentTableServiceRec") IsNot Nothing Then
                Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                Dim drCurrentRow As DataRow = Nothing
                If dtCurrentTable.Rows.Count > 0 Then
                    For i As Integer = 1 To dtCurrentTable.Rows.Count

                        Dim TextBoxSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                        Dim TextBoxAccountId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtAccountIdGV"), TextBox)
                        Dim TextBoxClientName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(1).FindControl("txtClientNameGV"), TextBox)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(2).FindControl("txtServiceRecordNoGV"), TextBox)
                        Dim TextBoxServiceDate As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(3).FindControl("txtServiceDateGV"), TextBox)
                        Dim TextBoxBillingFrequency As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(4).FindControl("txtBillingFrequencyGV"), TextBox)
                        Dim TextBoxLocationId As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(5).FindControl("txtLocationIdGV"), TextBox)
                        Dim TextBoxServiceAddress As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(6).FindControl("txtServiceAddressGV"), TextBox)
                        Dim TextBoxToBillAmt As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(8).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        'Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(9).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxContactType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContactTypeGV"), TextBox)
                        Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtCompanyGroupGV"), TextBox)

                        Dim TextBoxAccountName As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(13).FindControl("txtAccountNameGV"), TextBox)
                        'Dim TextBoxBillAddress1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(14).FindControl("txtBillAddress1GV"), TextBox)
                        'Dim TextBoxBillBuilding As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(15).FindControl("txtBillBuildingGV"), TextBox)
                        'Dim TextBoxBillStreet As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(16).FindControl("txtBillStreetGV"), TextBox)
                        'Dim TextBoxBillCountry As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(17).FindControl("txtBillCountryGV"), TextBox)
                        'Dim TextBoxBillPostal As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(18).FindControl("txtBillPostalGV"), TextBox)
                        Dim TextBoxOurReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(19).FindControl("txtOurReferenceGV"), TextBox)
                        Dim TextBoxYourReference As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(20).FindControl("txtYourReferenceGV"), TextBox)
                        Dim TextBoxPoNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(21).FindControl("txtPoNoGV"), TextBox)
                        Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(22).FindControl("txtCreditTermsGV"), TextBox)
                        Dim TextBoxSalesman As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(23).FindControl("txtSalesmanGV"), TextBox)
                        Dim TextBoxRcnoServicebillingdetail As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(24).FindControl("txtRcnoServicebillingdetailGV"), TextBox)

                        Dim TextBoxDept As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(10).FindControl("txtDeptGV"), TextBox)
                        Dim TextBoxStatus As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(12).FindControl("txtStatusGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        'Dim TextBoxDiscAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(25).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(26).FindControl("txtGSTAmountGV"), TextBox)
                        'Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(27).FindControl("txtNetAmountGV"), TextBox)

                        Dim TextBoxServiceBy As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtServiceByGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtInvoiceNoGV"), TextBox)
                        Dim TextBoxServiceLocationGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceLocationGroupGV"), TextBox)

                        Dim TextBoxAgreeValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(28).FindControl("txtAgreeValueGV"), TextBox)
                        Dim TextBoxDuration As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(29).FindControl("txtDurationGV"), TextBox)
                        Dim TextBoxDurationMS As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtDurationMSGV"), TextBox)
                        Dim TextBoxPerServiceValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtPerServiceValueGV"), TextBox)
                        Dim TextBoxRecordType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtRecordTypeGV"), TextBox)
                        Dim TextBoxRemarks As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(30).FindControl("txtServiceRemarksGV"), TextBox)


                        drCurrentRow = dtCurrentTable.NewRow()


                        dtCurrentTable.Rows(i - 1)("SelRec") = TextBoxSelect.Text
                        dtCurrentTable.Rows(i - 1)("AccountId") = TextBoxAccountId.Text
                        dtCurrentTable.Rows(i - 1)("ClientName") = TextBoxClientName.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceDate") = TextBoxServiceDate.Text
                        dtCurrentTable.Rows(i - 1)("BillingFrequency") = TextBoxBillingFrequency.Text
                        dtCurrentTable.Rows(i - 1)("LocationId") = TextBoxLocationId.Text
                        dtCurrentTable.Rows(i - 1)("ServiceAddress") = TextBoxServiceAddress.Text
                        dtCurrentTable.Rows(i - 1)("ToBillAmt") = TextBoxToBillAmt.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        'dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("ContactType") = TextBoxContactType.Text
                        dtCurrentTable.Rows(i - 1)("CompanyGroup") = TextBoxCompanyGroup.Text

                        dtCurrentTable.Rows(i - 1)("AccountName") = TextBoxAccountName.Text
                        'dtCurrentTable.Rows(i - 1)("BillAddress1") = TextBoxBillAddress1.Text
                        'dtCurrentTable.Rows(i - 1)("BillBuilding") = TextBoxBillBuilding.Text
                        'dtCurrentTable.Rows(i - 1)("BillStreet") = TextBoxBillStreet.Text
                        'dtCurrentTable.Rows(i - 1)("BillCountry") = TextBoxBillCountry.Text
                        'dtCurrentTable.Rows(i - 1)("BillPostal") = TextBoxBillPostal.Text
                        dtCurrentTable.Rows(i - 1)("OurReference") = TextBoxOurReference.Text
                        dtCurrentTable.Rows(i - 1)("YourReference") = TextBoxYourReference.Text
                        dtCurrentTable.Rows(i - 1)("PoNo") = TextBoxPoNo.Text
                        dtCurrentTable.Rows(i - 1)("CreditTerms") = TextBoxCreditTerms.Text
                        dtCurrentTable.Rows(i - 1)("Salesman") = TextBoxSalesman.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServicebillingdetail") = TextBoxRcnoServicebillingdetail.Text

                        dtCurrentTable.Rows(i - 1)("Dept") = TextBoxDept.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("Status") = TextBoxStatus.Text

                        'dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        'dtCurrentTable.Rows(i - 1)("GSTAmount") = TextBoxGSTAmount.Text
                        'dtCurrentTable.Rows(i - 1)("NetAmount") = TextBoxNetAmount.Text

                        dtCurrentTable.Rows(i - 1)("ServiceBy") = TextBoxServiceBy.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ServiceLocationGroup") = TextBoxServiceLocationGroup.Text

                        dtCurrentTable.Rows(i - 1)("AgreeValue") = TextBoxAgreeValue.Text
                        dtCurrentTable.Rows(i - 1)("Duration") = TextBoxDuration.Text
                        dtCurrentTable.Rows(i - 1)("DurationMS") = TextBoxDurationMS.Text
                        dtCurrentTable.Rows(i - 1)("PerServiceValue") = TextBoxPerServiceValue.Text
                        dtCurrentTable.Rows(i - 1)("RecordType") = TextBoxRecordType.Text
                        dtCurrentTable.Rows(i - 1)("Remarks") = TextBoxRemarks.Text
                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableServiceRec") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataServiceRecs()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try

    End Sub

    'End: Serice Record Grid


    'Start: Billing Details Grid


    Private Sub FirstGridViewRowBillingDetailsRecs()
        Try
            Dim dt As New DataTable()
            Dim dr As DataRow = Nothing
            'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(New DataColumn("ItemType", GetType(String)))
            dt.Columns.Add(New DataColumn("ItemCode", GetType(String)))
            dt.Columns.Add(New DataColumn("ItemDescription", GetType(String)))

            dt.Columns.Add(New DataColumn("UOM", GetType(String)))
            dt.Columns.Add(New DataColumn("Qty", GetType(String)))
            dt.Columns.Add(New DataColumn("PricePerUOM", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalPrice", GetType(String)))

            dt.Columns.Add(New DataColumn("DiscPerc", GetType(String)))
            dt.Columns.Add(New DataColumn("DiscAmount", GetType(String)))
            dt.Columns.Add(New DataColumn("PriceWithDisc", GetType(String)))

            dt.Columns.Add(New DataColumn("TaxType", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTPerc", GetType(String)))
            dt.Columns.Add(New DataColumn("GSTAmt", GetType(String)))
            dt.Columns.Add(New DataColumn("TotalPriceWithGST", GetType(String)))

            dt.Columns.Add(New DataColumn("RcnoServiceRecord", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoServiceRecordDetail", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoInvoice", GetType(String)))
            dt.Columns.Add(New DataColumn("InvoiceNo", GetType(String)))
            dt.Columns.Add(New DataColumn("ContractNo", GetType(String)))
            dt.Columns.Add(New DataColumn("RcnoServiceBillingDetailItem", GetType(String)))

            dt.Columns.Add(New DataColumn("GLDescription", GetType(String)))
            dt.Columns.Add(New DataColumn("OtherCode", GetType(String)))
            dt.Columns.Add(New DataColumn("ServiceRecordNo", GetType(String)))
            dr = dt.NewRow()

            dr("ItemType") = String.Empty
            dr("ItemCode") = String.Empty
            dr("ItemDescription") = String.Empty
            dr("UOM") = String.Empty
            dr("Qty") = 0
            dr("PricePerUOM") = 0.0
            dr("TotalPrice") = 0

            dr("DiscPerc") = 0.0
            dr("DiscAmount") = 0
            dr("PriceWithDisc") = 0

            dr("TaxType") = String.Empty
            dr("GSTPerc") = 0.0
            dr("GSTAmt") = 0
            dr("TotalPriceWithGST") = 0

            dr("RcnoServiceRecord") = 0
            dr("RcnoServiceRecordDetail") = 0
            dr("RcnoInvoice") = 0
            dr("InvoiceNo") = String.Empty
            dr("ContractNo") = String.Empty
            dr("RcnoServiceBillingDetailItem") = 0
            dr("GLDescription") = String.Empty
            dr("OtherCode") = 0
            dr("ServiceRecordNo") = String.Empty

            dt.Rows.Add(dr)

            ViewState("CurrentTableBillingDetailsRec") = dt

            grvBillingDetails.DataSource = dt
            grvBillingDetails.DataBind()

            Dim btnAdd As Button = CType(grvBillingDetails.FooterRow.Cells(1).FindControl("btnAddDetail"), Button)
            Page.Form.DefaultFocus = btnAdd.ClientID

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        'Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTPercGV"), TextBox)
                        TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PricePerUOM") = TextBoxPricePerUOM.Text
                        dtCurrentTable.Rows(i - 1)("TotalPrice") = TextBoxTotalPrice.Text

                        dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text

                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceBillingDetailItem") = TextBoxRcnoServiceBillingDetailItem.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text
                        rowIndex += 1


                    Next i
                    dtCurrentTable.Rows.Add(drCurrentRow)
                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable

                    grvBillingDetails.DataSource = dtCurrentTable
                    grvBillingDetails.DataBind()

                    Dim rowIndex2 As Integer = 0
                    Dim j As Integer = 1
                    Do While j <= (rowIndex)

                        Dim TextBoxTargetDesc1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtTaxTypeGV"), DropDownList)
                        Query = "Select TaxType from tbltaxtype"
                        PopulateDropDownList(Query, "TaxType", "TaxType", TextBoxTargetDesc1)


                        'Dim Query As String

                        Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'Query = "Select * from tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'"
                        'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

                        Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex2).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        Query = "Select * from tblunitms order by UnitMs"
                        PopulateDropDownList(Query, "UnitMsID", "UnitMsID", TextBoxUOM1)

                        Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxItemDescription1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)

                        If TextBoxItemType1.Text = "SERVICE" Then
                            TextBoxQty1.Enabled = False
                            TextBoxQty1.Text = 1
                            'TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            TextBoxItemType1.Enabled = False
                        End If

                        If TextBoxItemType1.Text = "SERVICE" And rowIndex = 0 Then
                            'TextBoxQty1.Enabled = False
                            'TextBoxQty1.Text = 1
                            TextBoxItemCode1.Enabled = False
                            TextBoxItemDescription1.Enabled = False
                            'TextBoxItemType1.Enabled = False
                        End If

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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PricePerUOM") = TextBoxPricePerUOM.Text
                        dtCurrentTable.Rows(i - 1)("TotalPrice") = TextBoxTotalPrice.Text

                        dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text
                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text

                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceBillingDetailItem") = TextBoxRcnoServiceBillingDetailItem.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)

                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)

                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        TextBoxItemType.Text = dt.Rows(i)("ItemType").ToString()
                        TextBoxItemCode.Text = dt.Rows(i)("ItemCode").ToString()
                        TextBoxItemDescription.Text = dt.Rows(i)("ItemDescription").ToString()
                        TextBoxUOM.Text = dt.Rows(i)("UOM").ToString()
                        TextBoxQty.Text = dt.Rows(i)("Qty").ToString()
                        TextBoxPricePerUOM.Text = dt.Rows(i)("PricePerUOM").ToString()
                        TextBoxTotalPrice.Text = dt.Rows(i)("TotalPrice").ToString()

                        TextBoxDiscPerc.Text = dt.Rows(i)("DiscPerc").ToString()
                        TextBoxDiscAmount.Text = dt.Rows(i)("DiscAmount").ToString()
                        TextBoxPriceWithDisc.Text = dt.Rows(i)("PriceWithDisc").ToString()

                        TextBoxTaxType.Text = dt.Rows(i)("TaxType").ToString()
                        TextBoxGSTPerc.Text = dt.Rows(i)("GSTPerc").ToString()
                        TextBoxGSTAmt.Text = dt.Rows(i)("GSTAmt").ToString()
                        TextBoxTotalPriceWithGST.Text = dt.Rows(i)("TotalPriceWithGST").ToString()

                        TextBoxRcnoServiceRecord.Text = dt.Rows(i)("RcnoServiceRecord").ToString()
                        TextBoxRcnoServiceRecordDetail.Text = dt.Rows(i)("RcnoServiceRecordDetail").ToString()
                        TextBoxRcnoInvoice.Text = dt.Rows(i)("RcnoInvoice").ToString()
                        TextBoxInvoiceNo.Text = dt.Rows(i)("InvoiceNo").ToString()
                        TextBoxContractNo.Text = dt.Rows(i)("ContractNo").ToString()
                        TextBoxRcnoServiceBillingDetailItem.Text = dt.Rows(i)("RcnoServiceBillingDetailItem").ToString()
                        TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
                        TextBoxOtherCode.Text = dt.Rows(i)("OtherCode").ToString()
                        'TextBoxTaxType.Text = dt.Rows(i)("TaxType").ToString()
                        TextBoxServiceRecordNo.Text = dt.Rows(i)("ServiceRecordNo").ToString()

                        Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        'Dim TextBoxTaxType1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtTaxTypeGV"), TextBox)

                        ''''''''''''''

                        Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)

                        '''''''''''''


                        If TextBoxItemType1.Text = "SERVICE" Then
                            TextBoxQty1.Enabled = False
                            TextBoxQty1.Text = 1
                            'TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            TextBoxItemType1.Enabled = False
                        End If

                        If TextBoxItemType1.Text = "SERVICE" And rowIndex = 0 Then
                            'TextBoxQty1.Enabled = False
                            'TextBoxQty1.Text = 1
                            TextBoxItemCode1.Enabled = False
                            'TextBoxItemDescription1.Enabled = False
                            'TextBoxItemType1.Enabled = False
                        End If

                        Dim query As String


                        'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(i).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
                        'query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"

                        If TextBoxItemType1.Text <> "-1" Then
                            query = "Select * from tblbillingproducts  "
                            PopulateDropDownList(query, "ProductCode", "ProductCode", TextBoxItemCode1)

                            Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                            query = "Select * from tblunitms order by UnitMs"
                            PopulateDropDownList(query, "UnitMsId", "UnitMsID", TextBoxUOM1)
                        End If

                        ''Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
                        'query = "Select * from tblunitms order by UnitMs"
                        'PopulateDropDownList(query, "UnitMs", "UnitMs", TextBoxUOM1)

                        rowIndex += 1
                    Next i
                End If
            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                        Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
                        Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(1).FindControl("txtItemCodeGV"), DropDownList)
                        Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(2).FindControl("txtItemDescriptionGV"), TextBox)
                        Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(3).FindControl("txtUOMGV"), DropDownList)
                        Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)
                        Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(5).FindControl("txtPricePerUOMGV"), TextBox)
                        Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(6).FindControl("txtTotalPriceGV"), TextBox)

                        Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(7).FindControl("txtDiscPercGV"), TextBox)
                        Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(8).FindControl("txtDiscAmountGV"), TextBox)
                        Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(9).FindControl("txtPriceWithDiscGV"), TextBox)

                        Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(10).FindControl("txtGSTPercGV"), TextBox)
                        Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtGSTAmtGV"), TextBox)
                        Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(12).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                        Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(11).FindControl("txtContractNoGV"), TextBox)
                        Dim TextBoxRcnoServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(13).FindControl("txtRcnoServiceRecordGV"), TextBox)
                        Dim TextBoxRcnoServiceRecordDetail As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(14).FindControl("txtRcnoServiceRecordDetailGV"), TextBox)
                        Dim TextBoxRcnoInvoice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(15).FindControl("txtRcnoInvoiceGV"), TextBox)
                        Dim TextBoxInvoiceNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(16).FindControl("txtServiceStatusGV"), TextBox)
                        Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(18).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
                        Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtGLDescriptionGV"), TextBox)
                        Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(21).FindControl("txtOtherCodeGV"), TextBox)
                        Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtTaxTypeGV"), DropDownList)
                        Dim TextBoxServiceRecordNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(22).FindControl("txtServiceRecordGV"), TextBox)

                        drCurrentRow = dtCurrentTable.NewRow()

                        dtCurrentTable.Rows(i - 1)("ItemType") = TextBoxItemType.Text
                        dtCurrentTable.Rows(i - 1)("ItemCode") = TextBoxItemCode.Text
                        dtCurrentTable.Rows(i - 1)("ItemDescription") = TextBoxItemDescription.Text
                        dtCurrentTable.Rows(i - 1)("UOM") = TextBoxUOM.Text
                        dtCurrentTable.Rows(i - 1)("Qty") = TextBoxQty.Text
                        dtCurrentTable.Rows(i - 1)("PricePerUOM") = TextBoxPricePerUOM.Text
                        dtCurrentTable.Rows(i - 1)("TotalPrice") = TextBoxTotalPrice.Text

                        dtCurrentTable.Rows(i - 1)("DiscPerc") = TextBoxDiscPerc.Text
                        dtCurrentTable.Rows(i - 1)("DiscAmount") = TextBoxDiscAmount.Text
                        dtCurrentTable.Rows(i - 1)("PriceWithDisc") = TextBoxPriceWithDisc.Text

                        dtCurrentTable.Rows(i - 1)("TaxType") = TextBoxTaxType.Text
                        dtCurrentTable.Rows(i - 1)("GSTPerc") = TextBoxGSTPerc.Text
                        dtCurrentTable.Rows(i - 1)("GSTAmt") = TextBoxGSTAmt.Text
                        dtCurrentTable.Rows(i - 1)("TotalPriceWithGST") = TextBoxTotalPriceWithGST.Text


                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecord") = TextBoxRcnoServiceRecord.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceRecordDetail") = TextBoxRcnoServiceRecordDetail.Text
                        dtCurrentTable.Rows(i - 1)("RcnoInvoice") = TextBoxRcnoInvoice.Text
                        dtCurrentTable.Rows(i - 1)("InvoiceNo") = TextBoxInvoiceNo.Text
                        dtCurrentTable.Rows(i - 1)("ContractNo") = TextBoxContractNo.Text
                        dtCurrentTable.Rows(i - 1)("RcnoServiceBillingDetailItem") = TextBoxRcnoServiceBillingDetailItem.Text
                        dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
                        dtCurrentTable.Rows(i - 1)("OtherCode") = TextBoxOtherCode.Text
                        dtCurrentTable.Rows(i - 1)("ServiceRecordNo") = TextBoxServiceRecordNo.Text

                        rowIndex += 1
                    Next i

                    ViewState("CurrentTableBillingDetailsRec") = dtCurrentTable


                End If
            Else
                Response.Write("ViewState is null")
            End If
            SetPreviousDataBillingDetailsRecs()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try

    End Sub

    'End: Biling Details Grid

    'Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
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

    Protected Sub btnPopUpClientSearch_Click(sender As Object, e As ImageClickEventArgs)
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


    Protected Sub gvClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvClient.SelectedIndexChanged
        Try
            lblAlert.Text = ""
            txtIsPopup.Text = ""
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo

            If txtSearch.Text = "CustomerSearch" Then
                txtAccountId.Text = ""
                txtClientName.Text = ""

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
                '    ddlCompanyGrp.SelectedIndex = 0
                'Else
                '    ddlCompanyGrp.Text = Server.HtmlDecode(gvClient.SelectedRow.Cells(9).Text.Trim)
                'End If


                'If (gvClient.SelectedRow.Cells(21).Text = "&nbsp;") Then
                '    ddlCompanyGrp.Text = ""
                'Else
                '    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(21).Text.Trim
                'End If

                If (gvClient.SelectedRow.Cells(22).Text = "&nbsp;") Then
                    ddlCompanyGrp.Text = ""
                Else
                    ddlCompanyGrp.Text = gvClient.SelectedRow.Cells(22).Text.Trim
                End If

                If (gvClient.SelectedRow.Cells(1).Text = "&nbsp;") Then
                    ddlContactType.Text = ""
                Else
                    ddlContactType.Text = gvClient.SelectedRow.Cells(1).Text.Trim
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


                If (gvClient.SelectedRow.Cells(3).Text = "&nbsp;") Then
                    txtLocationId.Text = ""
                Else
                    txtLocationId.Text = gvClient.SelectedRow.Cells(3).Text.Trim
                End If

                txtAccountId_TextChanged(sender, e)
                'txtSearch.Text = ""
            Else
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
            End If

            gvLocation.DataBind()
            mdlPopUpClient.Hide()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

    Protected Sub chkSelectGV_CheckedChanged(sender As Object, e As EventArgs)
        'Try
        lblAlert.Text = ""

        Dim chk1 As CheckBox = DirectCast(sender, CheckBox)
        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
        Dim xrow1 As GridViewRow = CType(chk1.NamingContainer, GridViewRow)

        Dim lblid16 As TextBox = CType(xrow1.FindControl("txtCompanyGroupGV"), TextBox)
        Dim lblid27 As CheckBox = CType(xrow1.FindControl("chkSelectGV"), CheckBox)

        Dim rowindex1 As Integer = xrow1.RowIndex

        If String.IsNullOrEmpty(txtCompanyGroup.Text) = False Then
            If txtCompanyGroup.Text <> lblid16.Text Then
                lblAlert.Text = "DIFFERENT COMPANY GROUP CANNOT BE SELECTED"
                updPnlMsg.Update()
                lblid27.Checked = False
                Exit Sub
            End If
        End If

        Dim lblid22 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)

        If String.IsNullOrEmpty(txtCompanyGroup.Text) = True Then
            txtCompanyGroup.Text = lblid16.Text
        End If

        gContractNos = lblid22.Text

        txtComments.Text = gContractNos
        btnSaveInvoice.Enabled = True
        btnSaveInvoice.ForeColor = System.Drawing.Color.Black
        updPanelSave.Update()
        updPanelInvoice.Update()

    End Sub


    Protected Sub btnViewInvoice_Click(sender As Object, e As EventArgs)

        lblAlert.Text = ""

        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
        Dim btn1 As Button = DirectCast(sender, Button)
        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtInvoiceNoGV"), TextBox)

        If String.IsNullOrEmpty(lblid1.Text) = False Then
            Session("invoiceno") = lblid1.Text
            Session("gridsqlschedule") = txt.Text
            Session("rcnoschedule") = txtRcno.Text
            'Session("AccountId") = txtAccountIdBilling.Text
            'Session("AccountName") = txtAccountName.Text
            'Session("ContactType") = ddlContactType.Text
            'Session("CompanyGroup") = txtCompanyGroup.Text
            'Session("Salesman") = ddlSalesmanBilling.Text
            Session("invoicefrom") = "schedule"

            Response.Redirect("Invoice.aspx")
        End If

    End Sub


    'Protected Sub btnViewEdit_Click(sender As Object, e As EventArgs)
    '    Try
    '        lblAlert.Text = ""
    '        'cpnl1.Collapsed = False

    '        Me.cpnl1.Collapsed = False
    '        Me.cpnl1.ClientState = "false"
    '        updPanelInvoice.Update()

    '        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
    '        Dim btn1 As Button = DirectCast(sender, Button)
    '        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
    '        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
    '        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtAccountIdGV"), TextBox)
    '        Dim lblid2 As TextBox = CType(xrow1.FindControl("txtAccountNameGV"), TextBox)
    '        'Dim lblid3 As TextBox = CType(xrow1.FindControl("txtBillAddress1GV"), TextBox)
    '        'Dim lblid4 As TextBox = CType(xrow1.FindControl("txtBillBuildingGV"), TextBox)
    '        'Dim lblid5 As TextBox = CType(xrow1.FindControl("txtBillStreetGV"), TextBox)
    '        'Dim lblid6 As TextBox = CType(xrow1.FindControl("txtBillCountryGV"), TextBox)
    '        'Dim lblid7 As TextBox = CType(xrow1.FindControl("txtBillPostalGV"), TextBox)
    '        Dim lblid8 As TextBox = CType(xrow1.FindControl("txtOurReferenceGV"), TextBox)
    '        Dim lblid9 As TextBox = CType(xrow1.FindControl("txtYourReferenceGV"), TextBox)
    '        Dim lblid10 As TextBox = CType(xrow1.FindControl("txtPoNoGV"), TextBox)
    '        Dim lblid11 As TextBox = CType(xrow1.FindControl("txtCreditTermsGV"), TextBox)
    '        Dim lblid12 As TextBox = CType(xrow1.FindControl("txtSalesmanGV"), TextBox)
    '        Dim lblid13 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
    '        Dim lblid14 As TextBox = CType(xrow1.FindControl("txtRcnoServiceRecordGV"), TextBox)
    '        Dim lblid15 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
    '        Dim lblid16 As TextBox = CType(xrow1.FindControl("txtCompanyGroupGV"), TextBox)
    '        Dim lblid17 As TextBox = CType(xrow1.FindControl("txtRcnoInvoiceGV"), TextBox)
    '        Dim lblid18 As TextBox = CType(xrow1.FindControl("txtRcnoServicebillingdetailGV"), TextBox)
    '        Dim lblid19 As TextBox = CType(xrow1.FindControl("txtContactTypeGV"), TextBox)
    '        Dim lblid20 As TextBox = CType(xrow1.FindControl("txtInvoiceNoGV"), TextBox)

    '        Dim lblid21 As TextBox = CType(xrow1.FindControl("txtDiscAmountGV"), TextBox)
    '        Dim lblid22 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
    '        'Dim lblid23 As TextBox = CType(xrow1.FindControl("txtToBillAmtGV"), TextBox)
    '        Dim lblid24 As TextBox = CType(xrow1.FindControl("txtServiceRecordNoGV"), TextBox)
    '        Dim lblid25 As TextBox = CType(xrow1.FindControl("txtStatusGV"), TextBox)
    '        Dim lblid26 As TextBox = CType(xrow1.FindControl("txtContractNoGV"), TextBox)
    '        Dim lblid27 As TextBox = CType(xrow1.FindControl("txtCreditTermsGV"), TextBox)


    '        Label41.Text = "SERVICE DETAILS : " & lblid24.Text

    '        Dim rowindex1 As Integer = xrow1.RowIndex

    '        grcnoservicebillingdetail = rowindex1

    '        'If txtMode.Text = "NEW" Then
    '        txtAccountType.Text = lblid19.Text
    '        txtCompanyGroup.Text = lblid16.Text
    '        'txtInvoiceDate.Text = lblid20.Text
    '        txtAccountIdBilling.Text = lblid1.Text
    '        txtAccountName.Text = lblid2.Text
    '        'txtBillAddress.Text = lblid3.Text
    '        'txtBillBuilding.Text = lblid4.Text
    '        'txtBillStreet.Text = lblid5.Text
    '        'txtBillCountry.Text = lblid6.Text
    '        'txtBillPostal.Text = lblid7.Text
    '        txtOurReference.Text = lblid8.Text
    '        txtYourReference.Text = lblid9.Text
    '        txtPONo.Text = lblid10.Text
    '        txtContractNo.Text = lblid26.Text
    '        'If String.IsNullOrEmpty(lblid27.Text) = True Then
    '        '    ddlCreditTerms.Text = "C.O.D."
    '        'Else
    '        '    ddlCreditTerms.Text = lblid27.Text
    '        'End If

    '        'End If

    '        'txtCreditDays.Text = FindCreditDays(ddlCreditTerms.Text)

    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '            conn.Dispose()
    '        End If
    '        conn.Open()
    '        Dim sql As String
    '        sql = ""

    '        Dim command21 As MySqlCommand = New MySqlCommand

    '        'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"
    '        sql = "Select COACode, Description from tblchartofaccounts where  GLtype='TRADE DEBTOR'"
    '        'Dim command1 As MySqlCommand = New MySqlCommand
    '        command21.CommandType = CommandType.Text
    '        command21.CommandText = sql
    '        command21.Connection = conn

    '        Dim dr21 As MySqlDataReader = command21.ExecuteReader()

    '        Dim dt21 As New DataTable
    '        dt21.Load(dr21)

    '        If dt21.Rows.Count > 0 Then
    '            If dt21.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt21.Rows(0)("COACode").ToString : End If
    '            If dt21.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt21.Rows(0)("Description").ToString : End If
    '        End If


    '        '''''''''''''''''''''''''''''''''''

    '        '''''''''''''''''''''''''''''''''''


    '        'sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"
    '        sql = "Select COACode, Description from tblchartofaccounts where  GLType='GST OUTPUT'"
    '        Dim command23 As MySqlCommand = New MySqlCommand
    '        command23.CommandType = CommandType.Text
    '        command23.CommandText = sql
    '        command23.Connection = conn

    '        Dim dr23 As MySqlDataReader = command23.ExecuteReader()

    '        Dim dt23 As New DataTable
    '        dt23.Load(dr23)

    '        If dt23.Rows.Count > 0 Then
    '            If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
    '            If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
    '        End If

    '        updPnlBillingRecs.Update()

    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '            conn.Dispose()
    '        End If
    '        'conn.Close()


    '        'Start: Populate the grid
    '        txtRcnoServiceRecord.Text = lblid14.Text
    '        txtRcnoServiceRecordDetail.Text = lblid15.Text
    '        txtContractNo.Text = lblid22.Text
    '        txtRcnoInvoice.Text = lblid17.Text
    '        txtRowSelected.Text = rowindex1.ToString
    '        txtRcnoServiceBillingDetail.Text = lblid18.Text
    '        txtRcnotblServiceBillingDetail.Text = lblid18.Text

    '        If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
    '            txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text
    '        End If
    '        'Start: Billing Details

    '        Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
    '        Dim drCurrentRowLoc As DataRow = Nothing

    '        For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
    '            dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
    '            drCurrentRowLoc = dtScdrLoc.NewRow()
    '            ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
    '            grvBillingDetails.DataSource = dtScdrLoc
    '            grvBillingDetails.DataBind()

    '            SetPreviousDataBillingDetailsRecs()
    '        Next i

    '        FirstGridViewRowBillingDetailsRecs()

    '        If String.IsNullOrEmpty(txtRcnoServiceBillingDetail.Text) = True Then
    '            txtRcnoServiceBillingDetail.Text = "0"
    '        End If

    '        Dim Query As String

    '        Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
    '        'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
    '        Query = "Select * from tblbillingproducts  "
    '        PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

    '        Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtUOMGV"), DropDownList)
    '        Query = "Select * from tblunitms order by UnitMs"
    '        PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM1)


    '        'If Convert.ToInt64(txtRcnoInvoice.Text) = 0 Then
    '        If Convert.ToInt64(txtRcnoServiceBillingDetail.Text) = 0 Then
    '            Dim dt As New DataTable

    '            '''''''''''''''''''''''''

    '            'Get Item desc, price Id

    '            'Dim conn As MySqlConnection = New MySqlConnection()

    '            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '                conn.Dispose()
    '            End If
    '            conn.Open()
    '            Dim command1 As MySqlCommand = New MySqlCommand
    '            command1.CommandType = CommandType.Text

    '            'If lblid25.Text = "P" Then
    '            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
    '            'ElseIf lblid25.Text = "O" Then
    '            '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
    '            'End If

    '            If String.IsNullOrEmpty(lblid26.Text) = False Then
    '                If lblid25.Text = "P" Then
    '                    command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
    '                ElseIf lblid25.Text = "O" Then
    '                    command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
    '                End If
    '            Else
    '                command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-COO'"
    '            End If

    '            command1.Connection = conn

    '            Dim dr1 As MySqlDataReader = command1.ExecuteReader()
    '            Dim dt1 As New DataTable
    '            dt1.Load(dr1)

    '            'AddNewRowBillingDetailsRecs()

    '            Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
    '            TextBoxItemType.Text = "SERVICE"
    '            TextBoxItemType.Enabled = False

    '            Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
    '            TextBoxItemCode.Text = dt1.Rows(0)("ProductCode").ToString()

    '            Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
    '            'TextBoxItemDescription.Text = ""
    '            TextBoxItemDescription.Text = dt1.Rows(0)("Description").ToString()


    '            Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
    '            TextBoxOtherCode.Text = dt1.Rows(0)("COACode").ToString()


    '            Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '            TextBoxGLDescription.Text = dt1.Rows(0)("COADescription").ToString()

    '            Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtUOMGV"), DropDownList)
    '            TextBoxUOM.Text = "--SELECT--"

    '            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtQtyGV"), TextBox)
    '            TextBoxQty.Text = "1"
    '            TextBoxQty.Enabled = False

    '            Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
    '            TextBoxPriceWithDisc.Text = lblid13.Text

    '            Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
    '            TextBoxPricePerUOM.Text = lblid13.Text

    '            Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTPercGV"), TextBox)
    '            TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

    '            Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
    '            TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid13.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))

    '            Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
    '            TextBoxTotalPrice.Text = lblid13.Text

    '            Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '            TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal(TextBoxPricePerUOM.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text))


    '            Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtContractNoGV"), TextBox)
    '            TextBoxContractNo.Text = Convert.ToString(txtContractNo.Text)

    '            Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtServiceStatusGV"), TextBox)
    '            TextBoxServiceStatus.Text = Convert.ToString(lblid25.Text)

    '            Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
    '            TextBoxServiceRecord.Text = Convert.ToString(lblid24.Text)


    '            txtTotal.Text = TextBoxTotalPrice.Text
    '            txtTotalWithGST.Text = TextBoxTotalPriceWithGST.Text

    '            txtTotalDiscAmt.Text = (0.0).ToString("N2")
    '            txtTotalGSTAmt.Text = Convert.ToDecimal(TextBoxGSTAmt.Text).ToString("N2")
    '            txtTotalWithDiscAmt.Text = txtTotal.Text

    '            '''''''''''''''''''''
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '                conn.Dispose()
    '            End If
    '            'conn.Close()
    '        Else

    '            'Start: From tblBillingDetailItem

    '            Dim Total As Decimal
    '            Dim TotalWithGST As Decimal
    '            Dim TotalDiscAmt As Decimal
    '            Dim TotalGSTAmt As Decimal
    '            Dim TotalPriceWithDiscountAmt As Decimal


    '            Total = 0.0
    '            TotalWithGST = 0.0
    '            TotalDiscAmt = 0.0
    '            TotalGSTAmt = 0.0
    '            TotalPriceWithDiscountAmt = 0.0

    '            'Dim conn As MySqlConnection = New MySqlConnection()

    '            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '                conn.Dispose()
    '            End If
    '            conn.Open()

    '            Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
    '            cmdServiceBillingDetailItem.CommandType = CommandType.Text
    '            cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblservicebillingdetailitem where Rcnoservicebillingdetail=" & Convert.ToInt32(txtRcnoServiceBillingDetail.Text)
    '            cmdServiceBillingDetailItem.Connection = conn

    '            Dim drcmdServiceBillingDetailItem As MySqlDataReader = cmdServiceBillingDetailItem.ExecuteReader()
    '            Dim dtServiceBillingDetailItem As New DataTable
    '            dtServiceBillingDetailItem.Load(drcmdServiceBillingDetailItem)

    '            Dim TotDetailRecordsLoc = dtServiceBillingDetailItem.Rows.Count
    '            If dtServiceBillingDetailItem.Rows.Count > 0 Then

    '                Dim rowIndex = 0

    '                For Each row As DataRow In dtServiceBillingDetailItem.Rows
    '                    If (TotDetailRecordsLoc > (rowIndex + 1)) Then
    '                        AddNewRowBillingDetailsRecs()
    '                        'AddNewRow()
    '                    End If

    '                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
    '                    TextBoxItemType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemType"))

    '                    Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
    '                    TextBoxItemCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemCode"))

    '                    Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
    '                    TextBoxItemDescription.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ItemDescription"))

    '                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
    '                    TextBoxOtherCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("OtherCode"))

    '                    Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtUOMGV"), DropDownList)
    '                    If String.IsNullOrEmpty(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("UOM"))) = True Then

    '                    Else
    '                        TextBoxUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("UOM"))
    '                    End If


    '                    Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
    '                    TextBoxQty.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("Qty"))
    '                    'TextBoxQty.Enabled = False

    '                    Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
    '                    TextBoxPricePerUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PricePerUOM"))


    '                    Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscPercGV"), TextBox)
    '                    TextBoxDiscPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscPerc"))

    '                    Dim TextBoxDiscAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
    '                    TextBoxDiscAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("DiscAmount"))


    '                    Dim TextBoxPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
    '                    TextBoxPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("PriceWithDisc"))


    '                    Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
    '                    TextBoxTaxType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TaxType"))

    '                    Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTPercGV"), TextBox)
    '                    TextBoxGSTPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTPerc"))

    '                    Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
    '                    TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GSTAmt"))

    '                    Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
    '                    TextBoxTotalPrice.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TotalPrice"))

    '                    Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '                    TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("TotalPriceWithGST"))

    '                    Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
    '                    TextBoxServiceRecord.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("ServiceRecordNo"))


    '                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '                    TextBoxGLDescription.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("GLDescription"))

    '                    '20.03.17
    '                    Dim TextBoxRcnoServiceBillingDetailItem As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)
    '                    TextBoxRcnoServiceBillingDetailItem.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("rcno"))
    '                    '20.03.17


    '                    'Dim TextBoxInvoiceType As TextBox = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("txtInvoiceTypeGV"), TextBox)
    '                    'TextBoxInvoiceType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex)("InvoiceType"))


    '                    Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
    '                    TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
    '                    TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
    '                    'txtAmountWithDiscount.Text =  Total - TotalDiscAmt
    '                    TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
    '                    TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

    '                    'Dim Query As String

    '                    Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
    '                    'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
    '                    Query = "Select * from tblbillingproducts  "
    '                    PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

    '                    Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
    '                    Query = "Select * from tblunitms order by UnitMs"
    '                    PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


    '                    Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
    '                    Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

    '                    If TextBoxItemType1.Text = "SERVICE" Then
    '                        TextBoxQty1.Enabled = False
    '                        TextBoxQty1.Text = 1
    '                        TextBoxItemType1.Enabled = False
    '                    End If

    '                    rowIndex += 1

    '                Next row
    '                'AddNewRowBillingDetailsRecs()
    '                'SetPreviousDataBillingDetailsRecs()
    '                'AddNewRow()
    '                'SetPreviousData()
    '                txtTotal.Text = Total.ToString("N2")
    '                txtTotalWithGST.Text = TotalWithGST.ToString("N2")
    '                txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
    '                txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")

    '                'txtTotalDiscAmt.Text = 0.0
    '                txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
    '                If conn.State = ConnectionState.Open Then
    '                    conn.Close()
    '                    conn.Dispose()
    '                End If
    '                'conn.Close()
    '            Else
    '                FirstGridViewRowBillingDetailsRecs()
    '                'FirstGridViewRowTarget()
    '                'Dim Query As String
    '                'Dim TextBoxTargetDesc As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
    '                'Query = "Select * from tblTarget"

    '                'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
    '            End If


    '            'End: Detail Records


    '            'End: From tblBillingDetailItem
    '        End If

    '        AddNewRowBillingDetailsRecs()



    '        btnSaveInvoice.Enabled = False
    '        If txtPostStatus.Text <> "P" Then
    '            grvBillingDetails.Enabled = True
    '            btnSave.Enabled = True
    '        End If

    '        updpnlServiceRecs.Update()
    '        updpnlBillingDetails.Update()
    '        'End: Billing Details
    '        updPanelSave.Update()
    '        'End: Populate the grid
    '        updPnlBillingRecs.Update()


    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '    End Try
    'End Sub

    Protected Sub txtQtyGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
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
        CalculatePrice()
    End Sub

    Protected Sub txtDiscPercGV_TextChanged(sender As Object, e As EventArgs)

        Dim btn1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(btn1.NamingContainer, GridViewRow)
        CalculatePrice()
    End Sub
    Private Sub CalculatePrice()
        Dim lblid1 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
        Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGV"), TextBox)
        Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceGV"), TextBox)

        Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscPercGV"), TextBox)
        Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtDiscAmountGV"), TextBox)
        Dim lblid6 As TextBox = CType(xgrvBillingDetails.FindControl("txtPriceWithDiscGV"), TextBox)

        Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
        Dim lblid8 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTAmtGV"), TextBox)
        Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtTotalPriceWithGSTGV"), TextBox)

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

        lblid3.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid1.Text) * Convert.ToDecimal(lblid2.Text)).ToString("N2"))
        lblid5.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid3.Text) * Convert.ToDecimal(lblid4.Text) * 0.01).ToString("N2"))
        lblid6.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid3.Text)) - Convert.ToDecimal(lblid5.Text)).ToString("N2"))
        lblid8.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid6.Text) * Convert.ToDecimal(lblid7.Text) * 0.01).ToString("N2"))
        lblid9.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal((lblid6.Text)) + Convert.ToDecimal(lblid8.Text)).ToString("N2"))

        CalculateTotalPrice()


    End Sub


    Private Sub CalculateTotalPrice()
        Try

            Dim TotalAmt As Decimal = 0
            Dim TotalDiscAmt As Decimal = 0
            Dim TotalWithDiscAmt As Decimal = 0
            Dim TotalGSTAmt As Decimal = 0
            Dim TotalAmtWithGST As Decimal = 0
            Dim table As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)


            If (table.Rows.Count > 0) Then

                For i As Integer = 0 To (table.Rows.Count) - 1

                    Dim TextBoxTotal As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceGV"), TextBox)
                    Dim TextBoxTotalWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                    Dim TextBoxDisAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                    Dim TextBoxTotalWithDiscAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtPriceWithDiscGV"), TextBox)

                    Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtGSTAmtGV"), TextBox)

                    If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
                        TextBoxTotal.Text = "0.00"
                    End If

                    If String.IsNullOrEmpty(TextBoxDisAmt.Text) = True Then
                        TextBoxDisAmt.Text = "0.00"
                    End If

                    If String.IsNullOrEmpty(TextBoxTotalWithDiscAmt.Text) = True Then
                        TextBoxTotalWithDiscAmt.Text = "0.00"
                    End If

                    If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                        TextBoxGSTAmt.Text = "0.00"
                    End If

                    If String.IsNullOrEmpty(TextBoxTotalWithGST.Text) = True Then
                        TextBoxTotalWithGST.Text = "0.00"
                    End If

                    TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
                    TotalAmtWithGST = TotalAmtWithGST + Convert.ToDecimal(TextBoxTotalWithGST.Text)

                    TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDisAmt.Text)
                    TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                    TotalWithDiscAmt = TotalWithDiscAmt + Convert.ToDecimal(TextBoxTotalWithDiscAmt.Text)
                Next i

            End If


            txtTotal.Text = TotalAmt.ToString
            txtTotalWithGST.Text = TotalAmtWithGST.ToString

            txtTotalDiscAmt.Text = TotalDiscAmt.ToString
            txtTotalGSTAmt.Text = TotalGSTAmt.ToString

            txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString
            updPanelSave.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub



    Private Sub CalculateServiceTotalPrice()
        Try

            Dim TotalAmt As Decimal = 0
            Dim TotalDiscAmt As Decimal = 0
            Dim TotalWithDiscAmt As Decimal = 0
            Dim TotalGSTAmt As Decimal = 0
            Dim TotalAmtWithGST As Decimal = 0
            'Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)


            'If (table.Rows.Count > 0) Then
            If grvServiceRecDetails.Rows.Count > 0 Then
                For i As Integer = 0 To (grvServiceRecDetails.Rows.Count) - 1

                    Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    If TextBoxchkSelect.Checked = True Then

                        Dim TextBoxTotal As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                        'Dim TextBoxTotalWithGST As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtTotalPriceWithGSTGV"), TextBox)

                        'Dim TextBoxDisAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                        'Dim TextBoxTotalWithDiscAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtPriceWithDiscGV"), TextBox)

                        'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(i).Cells(7).FindControl("txtGSTAmtGV"), TextBox)

                        If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
                            TextBoxTotal.Text = "0.00"
                        End If

                        'If String.IsNullOrEmpty(TextBoxDisAmt.Text) = True Then
                        '    TextBoxDisAmt.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalWithDiscAmt.Text) = True Then
                        '    TextBoxTotalWithDiscAmt.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxGSTAmt.Text) = True Then
                        '    TextBoxGSTAmt.Text = "0.00"
                        'End If

                        'If String.IsNullOrEmpty(TextBoxTotalWithGST.Text) = True Then
                        '    TextBoxTotalWithGST.Text = "0.00"
                        'End If

                        TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
                        'TotalAmtWithGST = Convert.ToDecimal(TotalAmt)

                        'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDisAmt.Text)
                        'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
                        'TotalWithDiscAmt = TotalWithDiscAmt + Convert.ToDecimal(TextBoxTotalWithDiscAmt.Text)
                    End If

                Next i

            End If

            txtInvoiceAmount.Text = Convert.ToDecimal(TotalAmt).ToString("N2")
            txtGSTAmount.Text = Convert.ToDecimal(TotalAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")
            txtNetAmount.Text = Convert.ToDecimal(Convert.ToDecimal(txtInvoiceAmount.Text) + Convert.ToDecimal(txtGSTAmount.Text)).ToString("N2")

            'txtTotal.Text = TotalAmt.ToString
            txtTotalWithGST.Text = txtNetAmount.ToString

            txtTotalDiscAmt.Text = 0.0
            txtTotalGSTAmt.Text = TotalGSTAmt.ToString

            txtTotalWithDiscAmt.Text = txtInvoiceAmount.ToString
            UpdatePanel3.Update()
            updPanelSave.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub
    Private Sub MakeMeNullBillingDetails()

        txtInvoiceNo.Text = ""
        'chkRecurringInvoice.Checked = False
        ''txtInvoiceDate.Text = ""
        'txtAccountIdBilling.Text = ""
        'txtAccountName.Text = ""
        'txtBillAddress.Text = ""
        'txtBillBuilding.Text = ""
        'txtBillCountry.Text = ""
        'txtBillStreet.Text = ""
        'txtBillPostal.Text = ""
        'txtOurReference.Text = ""
        'txtYourReference.Text = ""
        'txtPONo.Text = ""

        'txtCompanyGroup.Text = ""
        'txtAccountType.Text = ""
        'txtComments.Text = ""

        'ddlSalesmanBilling.SelectedIndex = 0
        'ddlCreditTerms.SelectedIndex = 0

        'txtTotalRecurringInvoices.Text = 0


        'If chkRecurringInvoice.Checked = True Then
        'txtAccountIdSearch.Text = txtAccountIdBilling.Text

        'Else
        'chkRecurringInvoice.Checked = False
        'txtInvoiceDate.Text = ""
        txtLastSalesDetailRcno.Text = "0"

        txtAccountIdBilling.Text = ""
        txtAccountName.Text = ""
        txtBillAddress.Text = ""
        txtBillBuilding.Text = ""
        txtBillCountry.Text = ""
        txtBillStreet.Text = ""
        txtBillPostal.Text = ""
        txtOurReference.Text = ""
        txtYourReference.Text = ""
        txtPONo.Text = ""

        txtCompanyGroup.Text = ""
        txtAccountType.Text = ""
        txtComments.Text = ""

        ddlSalesmanBilling.SelectedIndex = 0
        ddlCreditTerms.SelectedIndex = 0
        'End If

        chkRecurringInvoice.Checked = False
        chkRecurringInvoice.Enabled = True
        txtRecurringInvoiceDate.Text = ""
        txtRecurringServiceDateFrom.Text = ""
        txtRecurringServiceDateTo.Text = ""
        'chkRecurringInvoice.Enabled = True
        ddlRecurringFrequency.SelectedIndex = 0


        txtInvoiceAmount.Text = 0.0
        txtDiscountAmount.Text = 0.0
        txtAmountWithDiscount.Text = 0.0
        txtGSTAmount.Text = 0.0
        txtNetAmount.Text = 0.0

        txtTotal.Text = "0.00"
        txtTotalWithGST.Text = "0.00"
        txtTotalGSTAmt.Text = "0.00"
        txtTotalDiscAmt.Text = "0.00"
        txtTotalWithDiscAmt.Text = "0.00"
        txtCreditDays.Text = "0"
        rdbGrouping.Enabled = True
        rdbGrouping.SelectedIndex = 0
        rdbCompleted.Checked = True
        UpdatePanel1.Update()
        FirstGridViewRowGL()

    End Sub

    Protected Sub btnAddDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        If TotDetailRecords > 0 Then
            AddNewRowWithDetailRecBillingDetailsRecs()
        Else
            AddNewRowBillingDetailsRecs()
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'If txtMode.Text = "Add" Then

        lblAlert.Text = ""
        Try

            ''''''''''''''''''''''''''''''''''''''
            SetRowDataBillingDetailsRecs()

            Dim tableAdd1 As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

            If tableAdd1 IsNot Nothing Then

                For rowIndex30 As Integer = 0 To tableAdd1.Rows.Count - 1

                    Dim TextBoxItemTypeGV30 As DropDownList = CType(grvBillingDetails.Rows(rowIndex30).FindControl("txtItemTypeGV"), DropDownList)
                    Dim lbd30 As String = TextBoxItemTypeGV30.Text

                    Dim TextBoxItemCodeGV31 As DropDownList = CType(grvBillingDetails.Rows(rowIndex30).FindControl("txtItemCodeGV"), DropDownList)
                    Dim lbd31 As String = TextBoxItemCodeGV31.Text


                    If TextBoxItemTypeGV30.SelectedIndex <> 0 Then
                        If String.IsNullOrEmpty(lbd31) = True Or TextBoxItemCodeGV31.SelectedIndex = 0 Then
                            lblAlert.Text = "PLEASE SELECT ITEM CODE FOR ITEM TYPE '" & lbd30 & "'"
                            updPnlMsg.Update()
                            Exit Sub
                        End If
                    End If
                Next rowIndex30
            End If


            '''''''''''''''''''''''''''''''''''''''

            Dim qry As String
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim rowselected As Integer = Convert.ToInt32(txtRowSelected.Text)

            Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtAccountIdGV"), TextBox)


            Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtClientNameGV"), TextBox)
            Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtLocationIdGV"), TextBox)
            Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceRecordNoGV"), TextBox)
            Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
            Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillingFrequencyGV"), TextBox)
            Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtRcnoServiceRecordGV"), TextBox)
            Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtDeptGV"), TextBox)
            Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtStatusGV"), TextBox)
            Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtContractNoGV"), TextBox)
            Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceAddressGV"), TextBox)
            'Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
            Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)

            'Dim lblid24 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtDiscAmountGV"), TextBox)
            'Dim lblid25 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
            'Dim lblid26 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtNetAmountGV"), TextBox)
            Dim lblid27 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceByGV"), TextBox)
            Dim lblid28 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtContactTypeGV"), TextBox)
            Dim lblid29 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceIdGV"), TextBox)


            Dim lblid30 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtOurReferenceGV"), TextBox)
            Dim lblid31 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtYourReferenceGV"), TextBox)
            Dim lblid32 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtPoNoGV"), TextBox)
            Dim lblid33 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceRemarksGV"), TextBox)


            If txtMode.Text = "NEW" Then

                'Dim command As MySqlCommand = New MySqlCommand
                'command.CommandType = CommandType.Text

                If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                    txtRcnotblServiceBillingDetail.Text = 0
                End If

                'If Convert.ToInt64(txtBatchNo.Text) = 0 Then

                '''''''''''''''''''''
                Dim commandExistServiceBillingDetail As MySqlCommand = New MySqlCommand
                commandExistServiceBillingDetail.CommandType = CommandType.Text
                'command1.CommandText = Sql
                commandExistServiceBillingDetail.CommandText = "SELECT * FROM tblServiceBillingDetail where RcnoServiceRecord=" & Convert.ToInt64(lblid7.Text) & " and Batchno = '" & txtBatchNo.Text & "'"
                commandExistServiceBillingDetail.Connection = conn

                Dim drExistServiceBillingDetail As MySqlDataReader = commandExistServiceBillingDetail.ExecuteReader()
                Dim dtExistServiceBillingDetail As New DataTable
                dtExistServiceBillingDetail.Load(drExistServiceBillingDetail)

                If dtExistServiceBillingDetail.Rows.Count = 0 Then

                    '''''''''''''''''''''
                    qry = "INSERT INTO tblServiceBillingDetail( AccountId, CustName, LocationId, Name, RecordNo, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, "
                    qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                    qry = qry + " ContractGroup, Status, Address1, BatchNo, BillSchedule, ServiceBy, Notes, Remarks, "
                    qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                    qry = qry + " (@AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                    qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                    qry = qry + " @ContractGroup, @Status,  @Address1, @BatchNo, @BillSchedule, @ServiceBy, @Notes, @Remarks, "
                    qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                    command.CommandText = qry
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@AccountId", lblid1.Text)
                    command.Parameters.AddWithValue("@ClientName", lblid2.Text)
                    command.Parameters.AddWithValue("@LocationId", lblid3.Text)
                    command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                    command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                    command.Parameters.AddWithValue("@Address1", lblid21.Text)
                    command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                    command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                    command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)
                    command.Parameters.AddWithValue("@BillCountry", txtBillCountry.Text)
                    command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)
                    command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                    If lblid5.Text.Trim = "" Then
                        command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                    Else
                        command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                    End If

                    'command.Parameters.AddWithValue("@ServiceDate", lblid5.Text)
                    'Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)
                    Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
                    'Dim lbd30 As String = TextBoxGSTAmount.Text

                    'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                    '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                    '    command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                    '    command.Parameters.AddWithValue("@GSTAmount", 0.0)
                    '    command.Parameters.AddWithValue("@TotalWithGST", 0.0)
                    '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(lblid23.Text))
                    'Else
                    '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                    '    command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                    '    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                    '    command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                    '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                    'End If

                    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                    command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                    command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))


                    command.Parameters.AddWithValue("@OurRef", lblid30.Text)
                    command.Parameters.AddWithValue("@YourRef", lblid31.Text)
                    command.Parameters.AddWithValue("@PoNo", lblid32.Text)
                    'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                    command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                    'command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                    command.Parameters.AddWithValue("@RcnoServiceRecord", lblid7.Text)


                    command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)
                    command.Parameters.AddWithValue("@Status", lblid9.Text)

                    'If ddlContactType.SelectedIndex > 0 Then
                    '    command.Parameters.AddWithValue("@ContactType", ddlContactType.Text)
                    'Else
                    '    command.Parameters.AddWithValue("@ContactType", "")
                    'End If
                    command.Parameters.AddWithValue("@ContactType", lblid28)

                    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                    command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                    If ddlSalesmanBilling.Text = "-1" Then
                        command.Parameters.AddWithValue("@Salesman", "")
                    Else
                        command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                    End If

                    command.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                    command.Parameters.AddWithValue("@ServiceBy", lblid27.Text)
                    command.Parameters.AddWithValue("@Notes", lblid29.Text)
                    command.Parameters.AddWithValue("@Remarks", lblid33.Text)

                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                    command.Connection = conn
                    command.ExecuteNonQuery()

                    Dim sqlLastId As String
                    sqlLastId = "SELECT last_insert_id() from tblServiceBillingDetail"

                    Dim commandRcno As MySqlCommand = New MySqlCommand
                    commandRcno.CommandType = CommandType.Text
                    commandRcno.CommandText = sqlLastId
                    commandRcno.Parameters.Clear()
                    commandRcno.Connection = conn
                    txtRcnotblServiceBillingDetail.Text = commandRcno.ExecuteScalar()

                    If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
                        txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text

                        '''''''''''''''''''''''''
                        qry = "Update tblServiceBillingDetail set BatchNo = '" & txtBatchNo.Text & "' where rcno = " & txtBatchNo.Text

                        command.CommandText = qry
                        command.Parameters.Clear()
                        command.Connection = conn
                        command.ExecuteNonQuery()

                        ''''''''''''''''''''''''
                    End If
                End If
                Else
                    If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                        txtRcnotblServiceBillingDetail.Text = 0
                    End If
                    If Convert.ToInt64(txtRcnotblServiceBillingDetail.Text) > 0 Then
                        qry = "Update tblServiceBillingDetail set BillAmount = @BillAmount, DiscountAmount= @DiscountAmount,  GSTAmount =@GSTAmount,  "
                    qry = qry + "TotalWithGST = @TotalWithGST, NetAmount =@NetAmount, OurRef = @OurRef ,YourRef =@YourRef, PoNo =@PoNo, Salesman =@Salesman, Remarks=@Remarks,   "
                        qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn = @LastModifiedOn "
                        qry = qry + " where rcno =@rcno; "

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@rcno", txtRcnotblServiceBillingDetail.Text)
                        command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                        command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                        command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                        command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                        command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                    command.Parameters.AddWithValue("@OurRef", lblid30.Text)
                    command.Parameters.AddWithValue("@YourRef", lblid31.Text)
                    command.Parameters.AddWithValue("@PoNo", lblid32.Text)

                        If ddlSalesmanBilling.Text = "-1" Then
                            command.Parameters.AddWithValue("@Salesman", "")
                        Else
                            command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                        End If
                    command.Parameters.AddWithValue("@Remarks", lblid33.Text)
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Connection = conn
                        command.ExecuteNonQuery()
                    End If
                End If


                '''' Detail

                'Dim rowselected As Integer
                'rowselected = 0

                'Dim conn As MySqlConnection = New MySqlConnection()


                'Start: Delete existing Records

                If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                    txtRcnotblServiceBillingDetail.Text = "0"
                End If

                If txtRcnotblServiceBillingDetail.Text <> "0" Then '04.01.17

                    Dim commandtblServiceBillingDetailItem As MySqlCommand = New MySqlCommand

                    commandtblServiceBillingDetailItem.CommandType = CommandType.Text
                    'Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
                    Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where RcnoServiceBillingDetail = '" & Convert.ToInt32(txtRcnotblServiceBillingDetail.Text) & "'"

                    commandtblServiceBillingDetailItem.CommandText = qrycommandtblServiceBillingDetailItem
                    commandtblServiceBillingDetailItem.Parameters.Clear()
                    commandtblServiceBillingDetailItem.Connection = conn
                    commandtblServiceBillingDetailItem.ExecuteNonQuery()

                    'End: Delete Existing Records


                    SetRowDataServiceRecs()
                    Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

                    If tableAdd IsNot Nothing Then

                        For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                            'string txSpareId = row.ItemArray[0] as string;
                            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                            Dim lbd10 As String = TextBoxQty.Text

                            Dim TextBoxItemTypeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemTypeGV"), DropDownList)
                            Dim TextBoxItemCodeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemCodeGV"), DropDownList)
                            Dim TextBoxItemDescriptionGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemDescriptionGV"), TextBox)
                            Dim TextBoxUOMGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtUOMGV"), DropDownList)
                            Dim TextBoxPricePerUOMGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
                            Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
                            Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscPercGV"), TextBox)
                            Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                            Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
                            Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTPercGV"), TextBox)
                            Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
                            Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                            Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
                            Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
                            Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGLDescriptionGV"), TextBox)
                            Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
                            Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtServiceStatusGV"), TextBox)

                            If String.IsNullOrEmpty(lbd10) = False Then
                                If (Convert.ToInt64(lbd10) > 0) Then

                                    ''Start:Detail
                                    Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                    commandSalesDetail.CommandType = CommandType.Text
                                    Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
                                    qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate, InvoiceType, BillSchedule,  "
                                    qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                    qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
                                    qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate, @InvoiceType, @BillSchedule,"
                                    qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                    command.CommandText = qryDetail
                                    command.Parameters.Clear()

                                    command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                                    command.Parameters.AddWithValue("@Itemtype", TextBoxItemTypeGV.Text)
                                    command.Parameters.AddWithValue("@ItemCode", TextBoxItemCodeGV.Text)
                                    command.Parameters.AddWithValue("@Itemdescription", TextBoxItemDescriptionGV.Text)

                                    If TextBoxUOMGV.Text <> "-1" Then
                                        command.Parameters.AddWithValue("@UOM", TextBoxUOMGV.Text)

                                    Else
                                        command.Parameters.AddWithValue("@UOM", "")
                                    End If

                                    command.Parameters.AddWithValue("@Qty", TextBoxQty.Text)
                                    command.Parameters.AddWithValue("@PricePerUOM", TextBoxPricePerUOMGV.Text)
                                    'command.Parameters.AddWithValue("@BillAmount", 0.0)
                                    command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(TextBoxTotalPrice.Text))
                                    command.Parameters.AddWithValue("@DiscPerc", Convert.ToDecimal(TextBoxDiscPerc.Text))
                                    command.Parameters.AddWithValue("@DiscAmount", Convert.ToDecimal(TextBoxDiscAmount.Text))
                                    command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text))
                                    command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(TextBoxGSTPerc.Text))
                                    command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(TextBoxGSTAmt.Text))
                                    command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(TextBoxTotalPriceWithGST.Text))
                                    command.Parameters.AddWithValue("@TaxType", TextBoxTaxType.Text)
                                    command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                                    command.Parameters.AddWithValue("@ARCode", "")
                                    command.Parameters.AddWithValue("@GSTCode", "")
                                    command.Parameters.AddWithValue("@OtherCode", TextBoxOtherCode.Text)
                                    command.Parameters.AddWithValue("@GLDescription", TextBoxGLDescription.Text)
                                    command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                                    command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                                    'command.Parameters.AddWithValue("@ContractNo", TextBoxContractNo.Text)
                                    command.Parameters.AddWithValue("@ServiceStatus", TextBoxServiceStatus.Text)
                                    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                                    command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                                    command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                                    command.Parameters.AddWithValue("@InvoiceType", TextBoxItemTypeGV.Text)
                                    If lblid5.Text.Trim = "" Then
                                        command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                                    Else
                                        command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                                    End If

                                    command.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command.Connection = conn
                                    command.ExecuteNonQuery()
                                    'conn.Close()
                                End If

                            End If
                        Next rowIndex
                    End If

                    Dim lblid10 As CheckBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("chkSelectGV"), CheckBox)
                    Dim lblid11 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)
                    Dim lblid12 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtDiscAmountGV"), TextBox)
                    Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
                    Dim lblid14 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtNetAmountGV"), TextBox)
                    Dim lblid15 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtRcnoServicebillingdetailGV"), TextBox)


                    Dim lblidAccountName50 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtAccountNameGV"), TextBox)
                'Dim lblidBillAddress151 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillAddress1GV"), TextBox)
                'Dim lblidBillBuilding52 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillBuildingGV"), TextBox)
                'Dim lblidBillStreet53 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillStreetGV"), TextBox)
                'Dim lblidBillCountry54 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillCountryGV"), TextBox)
                'Dim lblidBillPostal55 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillPostalGV"), TextBox)
                    Dim lblidOurReference56 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtOurReferenceGV"), TextBox)
                    Dim lblidYourReference57 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtYourReferenceGV"), TextBox)
                    Dim lblidPoNo58 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtPoNoGV"), TextBox)
                    Dim lblidCreditTerms59 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtCreditTermsGV"), TextBox)
                    Dim lblidSalesman60 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtSalesmanGV"), TextBox)


                    lblid10.Checked = True
                    lblid11.Text = Convert.ToDecimal(txtTotal.Text).ToString("N2")
                    lblid12.Text = Convert.ToDecimal(txtTotalDiscAmt.Text).ToString("N2")
                    lblid13.Text = Convert.ToDecimal(txtTotalGSTAmt.Text).ToString("N2")
                    lblid14.Text = Convert.ToDecimal(txtTotalWithGST.Text).ToString("N2")
                    lblid15.Text = Convert.ToInt64(txtRcnotblServiceBillingDetail.Text)

                    '08.03.17
                    lblidAccountName50.Text = (txtAccountName.Text).ToString
                'lblidBillAddress151.Text = (txtBillAddress.Text).ToString
                'lblidBillBuilding52.Text = (txtBillBuilding.Text).ToString
                'lblidBillStreet53.Text = (txtBillStreet.Text).ToString
                'lblidBillCountry54.Text = (txtBillCountry.Text).ToString
                'lblidBillPostal55.Text = (txtBillPostal.Text).ToString
                    lblidOurReference56.Text = (txtOurReference.Text).ToString
                    lblidYourReference57.Text = (txtYourReference.Text).ToString
                    lblidPoNo58.Text = (txtPONo.Text).ToString
                    lblidCreditTerms59.Text = (ddlCreditTerms.Text).ToString
                    lblidSalesman60.Text = (ddlSalesmanBilling.Text).ToString
                    '08.03.17

                    'FirstGridViewRowBillingDetailsRecs()

                    ''Start: Update Invoice Amounts

                    'Dim TotalInvoiceAmount As Decimal = 0
                    'Dim TotalDiscountAmount As Decimal = 0
                    'Dim TotalGSTAmount As Decimal = 0
                    'Dim TotalNetAmount As Decimal = 0

                    'Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

                    'If (table.Rows.Count > 0) Then

                    '    For i As Integer = 0 To (table.Rows.Count) - 1

                    '        Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    '        If (TextBoxchkSelect.Checked = True) Then

                    '            Dim TextBoxInvoiceAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtToBillAmtGV"), TextBox)
                    '            Dim TextBoxDiscountAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtDiscAmountGV"), TextBox)
                    '            Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtGSTAmountGV"), TextBox)
                    '            Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(7).FindControl("txtNetAmountGV"), TextBox)

                    '            If String.IsNullOrEmpty(TextBoxInvoiceAmount.Text) = True Then
                    '                TextBoxInvoiceAmount.Text = "0.00"
                    '            End If

                    '            If String.IsNullOrEmpty(TextBoxDiscountAmount.Text) = True Then
                    '                TextBoxDiscountAmount.Text = "0.00"
                    '            End If

                    '            If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                    '                TextBoxGSTAmount.Text = "0.00"
                    '            End If

                    '            If String.IsNullOrEmpty(TextBoxNetAmount.Text) = True Then
                    '                TextBoxNetAmount.Text = "0.00"
                    '            End If

                    '            TotalInvoiceAmount = TotalInvoiceAmount + Convert.ToDecimal(TextBoxInvoiceAmount.Text)
                    '            TotalDiscountAmount = TotalDiscountAmount + Convert.ToDecimal(TextBoxDiscountAmount.Text)

                    '            TotalGSTAmount = TotalGSTAmount + Convert.ToDecimal(TextBoxGSTAmount.Text)
                    '            TotalNetAmount = TotalNetAmount + Convert.ToDecimal(TextBoxNetAmount.Text)
                    '        End If
                    '    Next i

                    'End If

                    'txtInvoiceAmount.Text = TotalInvoiceAmount.ToString("N2")
                    'txtDiscountAmount.Text = TotalDiscountAmount.ToString("N2")
                    'txtAmountWithDiscount.Text = (Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text)).ToString("N2")
                    'txtGSTAmount.Text = TotalGSTAmount.ToString("N2")
                    'txtNetAmount.Text = TotalNetAmount.ToString("N2")

                    ''txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

                    'updPnlBillingRecs.Update()

                    ''End: Update Invoice Amounts


                End If '04.01.17

                If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
                End If
                'conn.Close()

                'DisplayGLGrid()

                'btnSaveInvoice_Click(sender, e)


                'If txtMode.Text = "NEW" Then
                '    lblMessage.Text = "ADD: RECORD SUCCESSFULLY ADDED"
                'Else
                '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                'End If


                'DisableControls()
                FirstGridViewRowBillingDetailsRecs()
                lblAlert.Text = ""


                btnSave.Enabled = False
            btnSaveInvoice.Enabled = True
            btnSaveInvoice.ForeColor = System.Drawing.Color.Black
                btnPost.Enabled = True
            btnPost.ForeColor = System.Drawing.Color.Black

                updPnlMsg.Update()
                updpnlServiceRecs.Update()
                updPnlBillingRecs.Update()


                '''''''''''''''''''''''''''''
                txtAccountType.Text = ""
                txtAccountIdBilling.Text = ""
                txtAccountName.Text = ""
                txtBillAddress.Text = ""
                txtBillBuilding.Text = ""
                txtBillCountry.Text = ""
                txtBillStreet.Text = ""
                txtBillPostal.Text = ""
                txtOurReference.Text = ""
                txtYourReference.Text = ""
                txtPONo.Text = ""
                txtCompanyGroup.Text = ""
                txtComments.Text = ""
                ddlSalesmanBilling.SelectedIndex = 0
                ddlCreditTerms.SelectedIndex = 0

                '''''''''''''''''''''''''''''

                txtTotal.Text = "0.00"
                txtTotalWithGST.Text = "0.00"
                txtTotalGSTAmt.Text = "0.00"
                txtTotalDiscAmt.Text = "0.00"
                txtTotalWithDiscAmt.Text = "0.00"
                Label41.Text = "SERVICE DETAILS"


                txtRcnotblServiceBillingDetail.Text = "0"
                'txtMode.Text = "EDIT"

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
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

            AddNewRowGL()


            ''AR values

            Dim TextBoxGLCodeAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCodeAR.Text = txtARCode.Text

            Dim TextBoxGLDescriptionAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescriptionAR.Text = txtARDescription.Text

            Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtNetAmount.Text).ToString("N2")

            Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmountAR.Text = (0.0).ToString("N2")


            ''GST values

            rowIndex3 += 1
            AddNewRowGL()
            Dim TextBoxGLCodeGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
            TextBoxGLCodeGST.Text = txtGSTOutputCode.Text

            Dim TextBoxGLDescriptionGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
            TextBoxGLDescriptionGST.Text = txtGSTOutputDescription.Text

            Dim TextBoxDebitAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
            TextBoxDebitAmountGST.Text = (0.0).ToString("N2")

            Dim TextBoxCreditAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
            TextBoxCreditAmountGST.Text = Convert.ToDecimal(txtGSTAmount.Text).ToString("N2")
            ''GST Values



            rowIndex3 += 1
            AddNewRowGL()
            Dim conn As MySqlConnection = New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim cmdGL As MySqlCommand = New MySqlCommand
            cmdGL.CommandType = CommandType.Text
            'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblservicebillingdetailitem where BatchNo ='" & txtBatchNo.Text.Trim & "' order by OtherCode"
            'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblservicebillingdetailitem where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by OtherCode"
            cmdGL.CommandText = "SELECT OtherCode, GLDescription, BillAmount   FROM tblservicebillingdetail where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by OtherCode"

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


                lGLCode = dtGL.Rows(0)("OtherCode").ToString()
                lGLDescription = dtGL.Rows(0)("GLDescription").ToString()
                lCreditAmount = 0.0

                Dim rowIndex4 = 0

                For Each row As DataRow In dtGL.Rows

                    If lGLCode = row("OtherCode") Then
                        lCreditAmount = lCreditAmount + row("BillAmount")
                    Else


                        If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
                            AddNewRowGL()
                        End If

                        Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
                        TextBoxGLCode.Text = lGLCode

                        Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
                        TextBoxGLDescription.Text = lGLDescription

                        Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
                        TextBoxDebitAmount.Text = (0.0).ToString("N2")

                        Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
                        TextBoxCreditAmount.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

                        lGLCode = row("OtherCode")
                        lGLDescription = row("GLDescription")
                        lCreditAmount = row("BillAmount")

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
            exstr = ex.ToString
            lblAlert.Text = exstr
        End Try
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
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub btnSaveInvoice_Click(sender As Object, e As EventArgs) Handles btnSaveInvoice.Click

        'If Session("CheckRefresh").ToString() = ViewState("CheckRefresh").ToString() Then

        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtInvoiceDate.Text) = True Then
            ButtonPressed = "N"
            lblAlert.Text = "PLEASE ENTER INVOICE DATE"
            updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If txtLocation.SelectedIndex = 0 Then
                ButtonPressed = "N"
                lblAlert.Text = "PLEASE SELECT BRANCH"
                updPnlMsg.Update()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If



        Dim IsLock = FindARPeriod(txtBillingPeriod.Text)
        If IsLock = "Y" Then
            ButtonPressed = "N"
            lblAlert.Text = "PERIOD IS LOCKED"
            updPnlMsg.Update()
            txtInvoiceDate.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Dim dtRecur1 As Date
        If String.IsNullOrEmpty(txtRecurringInvoiceDate.Text) = False Then
            If Date.TryParseExact(txtRecurringInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dtRecur1) Then
                txtDateFrom.Text = dtRecur1.ToShortDateString
            Else
                lblAlert.Text = "'Next Batch Date' is Invalid"
                ButtonPressed = "N"
                updPnlMsg.Update()
                txtRecurringInvoiceDate.Focus()
                Return
                Exit Sub
            End If
        End If

        Dim dtRecur2 As Date
        If String.IsNullOrEmpty(txtRecurringServiceDateFrom.Text) = False Then
            If Date.TryParseExact(txtRecurringServiceDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dtRecur2) Then
                txtDateTo.Text = dtRecur2.ToShortDateString
            Else
                lblAlert.Text = "'Service Start Date To' is Invalid"
                ButtonPressed = "N"
                updPnlMsg.Update()
                txtDateTo.Focus()
                Return
                Exit Sub
            End If
        End If

        Dim dtRecur3 As Date
        If String.IsNullOrEmpty(txtRecurringServiceDateTo.Text) = False Then
            If Date.TryParseExact(txtRecurringServiceDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dtRecur3) Then
                txtDateTo.Text = dtRecur3.ToShortDateString
            Else
                lblAlert.Text = "'Service End Date To' is Invalid"
                ButtonPressed = "N"
                updPnlMsg.Update()
                txtRecurringServiceDateTo.Focus()
                Return
                Exit Sub
            End If
        End If


        txtServiceBySearch.Text = txtServiceBySearch.Text.ToUpper
        txtClientName.Text = txtClientName.Text.ToUpper
        txtLocationId.Text = txtLocationId.Text.ToUpper

        'If String.IsNullOrEmpty(txtRecurringInvoiceDate.Text) = True Then
        '    lblAlert.Text = "PLEASE ENTER RECURRING INVOICE DATE"
        '    Exit Sub
        'End If

        'If String.IsNullOrEmpty(txtRecurringServiceDateFrom.Text) = True Then
        '    lblAlert.Text = "PLEASE ENTER RECURRING INVOICE FROM DATE"
        '    Exit Sub
        'End If

        'If String.IsNullOrEmpty(txtRecurringServiceDateTo.Text) = True Then
        '    lblAlert.Text = "PLEASE ENTER RECURRING INVOICE TO DATE"
        '    Exit Sub
        'End If

        '''''''''''''''''''''''''''

        Dim totalRows As Long
        totalRows = 0


        For rowIndex1 As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
            Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
            If (TextBoxchkSelect1.Checked = True) Then
                totalRows = totalRows + 1
                GoTo insertRec2
            End If
        Next rowIndex1
        'End If


        If totalRows = 0 Then
            lblAlert.Text = "PLEASE SELECT SERVICE RECORDS TO BILL"
            ButtonPressed = "N"
            lblAlert.Focus()
            updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''

insertRec2:
        Dim rowselected As Integer
        rowselected = 0

        Dim lExistingBillNo As String
        lExistingBillNo = ""

        Try

            Dim conn As MySqlConnection = New MySqlConnection()
            'SetRowDataServiceRecs()
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)

            'If tableAdd IsNot Nothing Then

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''
            PopulateGLCodes()
            ''''''''''''''''''''''''''''''''''''''''''''''''

            Dim ToBillAmt As Decimal
            Dim DiscAmount As Decimal
            Dim GSTAmount As Decimal
            Dim NetAmount As Decimal
            'Dim ToBillAmt As Decimal

            ToBillAmt = 0.0
            DiscAmount = 0.0
            GSTAmount = 0.0
            NetAmount = 0.0

            Dim ToBillAmtTot As Decimal
            Dim DiscAmountTot As Decimal
            Dim GSTAmountTot As Decimal
            Dim NetAmountTot As Decimal

            Dim ContractGroup As String
            Dim LocationGroup As String
            Dim AccountIdGroup As String

            Dim noofRecords As Integer
            noofRecords = 0

            ContractGroup = ""
            LocationGroup = ""
            AccountIdGroup = ""

            ToBillAmtTot = 0.0
            DiscAmountTot = 0.0
            GSTAmountTot = 0.0
            NetAmountTot = 0.0


            ''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim qry3 As String
            qry3 = ""

            Dim command3 As MySqlCommand = New MySqlCommand
            command3.CommandType = CommandType.Text



            'If txtMode.Text = "EDIT" Then
            '    qry3 = "Update tblServiceBillingDetail set BatchNo = ''  "
            '    qry3 = qry3 + " where BillSchedule = '" & txtInvoiceNo.Text & "'"

            '    command3.CommandText = qry3
            '    command3.Parameters.Clear()
            '    command3.Connection = conn
            '    command3.ExecuteNonQuery()
            'End If


            CalculateServiceTotalPrice()

            ''''''''''''''' tlBillSchedule

            Dim commandBillSchedule As MySqlCommand = New MySqlCommand
            commandBillSchedule.CommandType = CommandType.Text

            Dim qry As String
            If txtMode.Text = "NEW" Then


                qry = "INSERT INTO tblServiceBillSchedule (BillSchedule, BSDate, ContType, CustCode, CustName, AccountId, Frequency, Incharge, Support,  "
                qry = qry + " Description, Contract, BillingDate, ServiceFrom, ServiceTo, BillAmount, DiscountAmount, AmountWithDiscount, GSTAmount, NetAmount, "
                qry = qry + " BatchNo, RecurringInvoice, TotalRecurringInvoices, NextInvoiceDate, RecurringServiceStartDate, RecurringServiceEndDate, "
                qry = qry + " GroupByStatus, GroupByBillingFrequency, GroupContractNo, GroupLocationID, GroupContractGroup, GLPeriod, CompanyGroupSearch, SchedulerSearch, GroupField, Location,   "
                qry = qry + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                qry = qry + "(@BillSchedule, @BSDate, @ContType, @CustCode, @ClientName, @AccountId, @Frequency, @Incharge, @Support, "
                qry = qry + " @Description, @Contract, @BillingDate, @ServiceFrom, @ServiceTo, @BillAmount, @DiscountAmount, @AmountWithDiscount, @GSTAmount, @NetAmount, "
                qry = qry + "  @BatchNo,  @RecurringInvoice, @TotalRecurringInvoices, @NextInvoiceDate, @RecurringServiceStartDate, @RecurringServiceEndDate, "
                qry = qry + " @GroupByStatus, @GroupByBillingFrequency, @GroupContractNo, @GroupLocationID, @GroupContractGroup,  @GLPeriod, @CompanyGroupSearch, @SchedulerSearch, @GroupField, @Location,  "
                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                commandBillSchedule.CommandText = qry
                commandBillSchedule.Parameters.Clear()

                If txtMode.Text = "NEW" Then
                    GenerateBillingSchedule()
                End If
                'commandBillSchedule.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@ClientName", txtClientName.Text)
                commandBillSchedule.Parameters.AddWithValue("@AccountId", txtAccountId.Text)

                If ddlContactType.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@ContType", ddlContactType.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ContType", "")
                End If
                'commandBillSchedule.Parameters.AddWithValue("@ContType", ddlContactType.Text)
                commandBillSchedule.Parameters.AddWithValue("@CustCode", "")
                commandBillSchedule.Parameters.AddWithValue("@Frequency", ddlBillingFrequency.Text)
                commandBillSchedule.Parameters.AddWithValue("@Incharge", "")
                commandBillSchedule.Parameters.AddWithValue("@Support", txtServiceBySearch.Text)
                commandBillSchedule.Parameters.AddWithValue("@Description", "")
                commandBillSchedule.Parameters.AddWithValue("@Contract", ddlContractNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@BillingDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))

                If txtDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@ServiceFrom", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ServiceFrom", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                End If


                If txtDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@ServiceTo", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ServiceTo", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                commandBillSchedule.Parameters.AddWithValue("@BSDate", Convert.ToDateTime(txtBatchDate.Text).ToString("yyyy-MM-dd"))

                'commandBillSchedule.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotalServiceSelected.Text))
                'commandBillSchedule.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalServiceSelected.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                'commandBillSchedule.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalServiceSelected.Text) + (Convert.ToDecimal(txtTotalServiceSelected.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))

                commandBillSchedule.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtInvoiceAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                commandBillSchedule.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                commandBillSchedule.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                If chkRecurringInvoice.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", "Y")
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", "N")
                End If


                'commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", chkRecurringInvoice.Checked)
                commandBillSchedule.Parameters.AddWithValue("@TotalRecurringInvoices", 0)

                If txtRecurringInvoiceDate.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@NextInvoiceDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@NextInvoiceDate", Convert.ToDateTime(txtRecurringInvoiceDate.Text).ToString("yyyy-MM-dd"))
                End If

                If txtRecurringServiceDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceStartDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceStartDate", Convert.ToDateTime(txtRecurringServiceDateFrom.Text).ToString("yyyy-MM-dd"))
                End If

                If txtRecurringServiceDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceEndDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceEndDate", Convert.ToDateTime(txtRecurringServiceDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                If rdbCompleted.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "P")
                ElseIf rdbNotCompleted.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "O")
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "A")
                End If

                If ddlBillingFrequency.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByBillingFrequency", ddlBillingFrequency.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupByBillingFrequency", "")
                End If

                If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractNo", ddlContractNo.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractNo", "")
                End If


                If String.IsNullOrEmpty(txtLocationId.Text) = False Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupLocationID", txtLocationId.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupLocationID", "")
                End If


                If ddlContractGroup.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractGroup", ddlContractGroup.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractGroup", "")
                End If
                commandBillSchedule.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)


                If ddlCompanyGrp.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@CompanyGroupSearch", ddlCompanyGrp.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@CompanyGroupSearch", "")
                End If

                If ddlScheduler.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@SchedulerSearch", ddlScheduler.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@SchedulerSearch", "")
                End If


                If rdbGrouping.SelectedIndex = 3 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "ContractNo")
                ElseIf rdbGrouping.SelectedIndex = 1 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "LocationID")
                ElseIf rdbGrouping.SelectedIndex = 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "AccountID")
                ElseIf rdbGrouping.SelectedIndex = 2 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "ServiceLocationCode")
                End If

                If txtLocation.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@Location", txtLocation.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@Location", "")
                End If


                '08.03.17
                commandBillSchedule.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                commandBillSchedule.Connection = conn
                commandBillSchedule.ExecuteNonQuery()

                Dim sqlLastId As String
                sqlLastId = "SELECT last_insert_id() from tblServiceBillSchedule"

                Dim commandRcno As MySqlCommand = New MySqlCommand
                commandRcno.CommandType = CommandType.Text
                commandRcno.CommandText = sqlLastId
                commandRcno.Parameters.Clear()
                commandRcno.Connection = conn
                txtRcno.Text = commandRcno.ExecuteScalar()

                ''''''''''''''' tblBillSchedule
            Else
                qry = "UPDATE  tblServiceBillSchedule set BSDate =@BSDate, ContType=@ContType, CustCode=@CustCode, CustName=@ClientName, AccountId=@AccountId, Frequency=@Frequency,   "
                qry = qry + " Incharge=@Incharge, Support=@Support, Description=@Description, Contract=@Contract, BillingDate=@BillingDate, ServiceFrom=@ServiceFrom, ServiceTo=@ServiceTo,  "
                qry = qry + " BillAmount=@BillAmount, DiscountAmount=@DiscountAmount, AmountWithDiscount=@AmountWithDiscount, GSTAmount=@GSTAmount, NetAmount=@NetAmount, "
                qry = qry + " BatchNo =@BatchNo, RecurringInvoice =@RecurringInvoice, TotalRecurringInvoices=@TotalRecurringInvoices, NextInvoiceDate = @NextInvoiceDate, RecurringServiceStartDate = @RecurringServiceStartDate, RecurringServiceEndDate =@RecurringServiceEndDate, "
                qry = qry + " GroupByStatus=@GroupByStatus, GroupByBillingFrequency =@GroupByBillingFrequency, GroupContractNo = @GroupContractNo, GroupLocationID = @GroupLocationID,  "
                qry = qry + " GroupContractGroup =@GroupContractGroup, GLPeriod = @GLPeriod, CompanyGroupSearch=@CompanyGroupSearch, SchedulerSearch=@SchedulerSearch, groupfield =@groupfield, Location=@Location, "

                qry = qry + " LastModifiedBy=@LastModifiedBy,LastModifiedOn=@LastModifiedOn "
                qry = qry + " where BillSchedule= @BillSchedule; "

                commandBillSchedule.CommandText = qry
                commandBillSchedule.Parameters.Clear()

                'commandBillSchedule.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@ClientName", txtClientName.Text)
                commandBillSchedule.Parameters.AddWithValue("@AccountId", txtAccountId.Text)

                If ddlContactType.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@ContType", ddlContactType.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ContType", "")
                End If

                commandBillSchedule.Parameters.AddWithValue("@CustCode", "")
                commandBillSchedule.Parameters.AddWithValue("@Frequency", ddlBillingFrequency.Text)
                commandBillSchedule.Parameters.AddWithValue("@Incharge", "")
                commandBillSchedule.Parameters.AddWithValue("@Support", txtServiceBySearch.Text)
                commandBillSchedule.Parameters.AddWithValue("@Description", "")
                commandBillSchedule.Parameters.AddWithValue("@Contract", ddlContractNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@BillingDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))

                If txtDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@ServiceFrom", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ServiceFrom", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                End If

                If txtDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@ServiceTo", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ServiceTo", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                commandBillSchedule.Parameters.AddWithValue("@BSDate", Convert.ToDateTime(txtBatchDate.Text).ToString("yyyy-MM-dd"))
                commandBillSchedule.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtInvoiceAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                commandBillSchedule.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                commandBillSchedule.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text))
                '08.03.17


                commandBillSchedule.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)


                If chkRecurringInvoice.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", "Y")
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", "N")
                End If


                'commandBillSchedule.Parameters.AddWithValue("@RecurringInvoice", chkRecurringInvoice.Checked)
                commandBillSchedule.Parameters.AddWithValue("@TotalRecurringInvoices", 0)

                If txtRecurringInvoiceDate.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@NextInvoiceDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@NextInvoiceDate", Convert.ToDateTime(txtRecurringInvoiceDate.Text).ToString("yyyy-MM-dd"))
                End If

                If txtRecurringServiceDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceStartDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceStartDate", Convert.ToDateTime(txtRecurringServiceDateFrom.Text).ToString("yyyy-MM-dd"))
                End If

                If txtRecurringServiceDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceEndDate", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@RecurringServiceEndDate", Convert.ToDateTime(txtRecurringServiceDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                If rdbCompleted.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "P")
                ElseIf rdbNotCompleted.Checked = True Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "O")
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupByStatus", "A")
                End If

                If ddlBillingFrequency.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupByBillingFrequency", ddlBillingFrequency.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupByBillingFrequency", "")
                End If

                If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractNo", ddlContractNo.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractNo", "")
                End If


                If String.IsNullOrEmpty(txtLocationId.Text) = False Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupLocationID", txtLocationId.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupLocationID", "")
                End If


                If ddlContractGroup.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractGroup", ddlContractGroup.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@GroupContractGroup", "")
                End If

                If ddlCompanyGrp.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@CompanyGroupSearch", ddlCompanyGrp.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@CompanyGroupSearch", "")
                End If

                If ddlScheduler.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@SchedulerSearch", ddlScheduler.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@SchedulerSearch", "")
                End If
                commandBillSchedule.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)

                If rdbGrouping.SelectedIndex = 3 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "ContractNo")
                ElseIf rdbGrouping.SelectedIndex = 1 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "LocationID")
                ElseIf rdbGrouping.SelectedIndex = 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "AccountID")
                ElseIf rdbGrouping.SelectedIndex = 2 Then
                    commandBillSchedule.Parameters.AddWithValue("@groupfield", "ServiceLocationCode")
                End If

                If txtLocation.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@Location", txtLocation.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@Location", "")
                End If

                commandBillSchedule.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                commandBillSchedule.Connection = conn
                commandBillSchedule.ExecuteNonQuery()

            End If


            'command.Dispose()

            'Dim qry1 As String
            'Dim command2 As MySqlCommand = New MySqlCommand
            'command2.CommandType = CommandType.Text

            ''qry1 = "Delete from  tblServiceBillingDetail where   "
            ''qry1 = qry1 + "   BillSchedule = '" & txtInvoiceNo.Text & "'"

            ''command2.CommandText = qry1
            ''command2.Parameters.Clear()
            ''command2.Connection = conn
            ''command2.ExecuteNonQuery()


            'qry1 = "Delete from  tblServiceBillingDetailItem where  "
            'qry1 = qry1 + "   BillSchedule = '" & txtInvoiceNo.Text & "'"

            'command2.CommandText = qry1
            'command2.Parameters.Clear()
            'command2.Connection = conn
            'command2.ExecuteNonQuery()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''

            For rowIndex As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                Dim lblidRecordType As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtRecordTypeGV"), TextBox)
                Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceRecordNoGV"), TextBox)

                If (TextBoxchkSelect.Checked = True) Then
                    'Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text
                    'Header
                    rowselected = rowselected + 1

                    Dim TextBoxRcnoServiceRecord As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)
                    Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtAccountIdGV"), TextBox)
                    Dim TextBoxCompanyGroup As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)

                    Dim lblid31 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtAccountIdGV"), TextBox)
                    Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtLocationIdGV"), TextBox)
                    Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)

                    'Dim lblid50 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
                    Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtClientNameGV"), TextBox)

                    Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceDateGV"), TextBox)
                    Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtBillingFrequencyGV"), TextBox)
                    Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtRcnoServiceRecordGV"), TextBox)
                    Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtDeptGV"), TextBox)
                    Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtStatusGV"), TextBox)
                    Dim lblid10 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtContactTypeGV"), TextBox)

                    Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceAddressGV"), TextBox)
                    Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtToBillAmtGV"), TextBox)


                    '08.03.17
                    Dim lblidAccountName50 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtAccountNameGV"), TextBox)
                    Dim lblidOurReference56 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtOurReferenceGV"), TextBox)
                    Dim lblidYourReference57 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtYourReferenceGV"), TextBox)
                    Dim lblidPoNo58 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtPoNoGV"), TextBox)
                    Dim lblidCreditTerms59 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtCreditTermsGV"), TextBox)
                    Dim lblidSalesman60 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtSalesmanGV"), TextBox)
                    Dim lblidServiceBy61 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceByGV"), TextBox)
                    Dim lblidServiceBy62 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceLocationGroupGV"), TextBox)
                    Dim lblidNotes63 As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceIDGV"), TextBox)

                    Dim lblidAgreeValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtAgreeValueGV"), TextBox)
                    Dim lblidDuration As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtDurationGV"), TextBox)
                    Dim lblidDurationMS As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtDurationMSGV"), TextBox)
                    Dim lblidPerServiceValue As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtPerServiceValueGV"), TextBox)

                    Dim lblidLocation As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtLocationGV"), TextBox)
                    Dim lblidRemarks As TextBox = CType(grvServiceRecDetails.Rows(rowIndex).FindControl("txtServiceRemarksGV"), TextBox)

                    ToBillAmt = ToBillAmt + Convert.ToDecimal(lblid23.Text)

                    ''Start:Detail

                    Dim command1 As MySqlCommand = New MySqlCommand
                    command1.CommandType = CommandType.Text

                    'If lblid9.Text = "P" Then
                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
                    'ElseIf lblid9.Text = "O" Then
                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
                    'End If

                    If lblid9.Text = "P" Then
                        command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-SRV'"
                    ElseIf lblid9.Text = "O" Then
                        command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
                    End If

                    command1.Connection = conn

                    Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                    Dim dt1 As New DataTable
                    dt1.Load(dr1)


                    'If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
                    If lblidRecordType.Text = "A" Then
                        '''''''''''''''''''''
                        qry = "INSERT INTO tblServiceBillingDetail(BillSchedule, AccountId, CustName, LocationId, Name, RecordNo, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, "
                        qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                        qry = qry + " ContractGroup, Status, Address1, BatchNo, ServiceBy, ServiceLocationGroup, Notes, "
                        qry = qry + " AgreeValue, Duration, DurationMS, PerServiceValue, OtherCode, GLDescription, Location, Remarks, "
                        qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        qry = qry + " (@BillSchedule, @AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                        qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                        qry = qry + " @ContractGroup, @Status,  @Address1, @BatchNo, @Serviceby, @ServiceLocationGroup, @Notes,"
                        qry = qry + " @AgreeValue, @Duration,  @DurationMS, @PerServiceValue, @OtherCode, @GLDescription, @Location, @Remarks,"
                        qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                        command.Parameters.AddWithValue("@AccountId", lblid31.Text)
                        command.Parameters.AddWithValue("@ClientName", lblid2.Text)
                        command.Parameters.AddWithValue("@LocationId", lblid3.Text)
                        command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                        command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                        command.Parameters.AddWithValue("@Address1", lblid21.Text)

                        command.Parameters.AddWithValue("@BillAddress1", "")
                        command.Parameters.AddWithValue("@BillBuilding", "")
                        command.Parameters.AddWithValue("@BillStreet", "")
                        command.Parameters.AddWithValue("@BillCountry", "")
                        command.Parameters.AddWithValue("@BillPostal", "")
                        command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                        command.Parameters.AddWithValue("@OurRef", lblidOurReference56.Text)
                        command.Parameters.AddWithValue("@YourRef", lblidYourReference57.Text)
                        command.Parameters.AddWithValue("@PoNo", lblidPoNo58.Text)
                        command.Parameters.AddWithValue("@Salesman", lblidSalesman60.Text)

                        If lblid5.Text.Trim = "" Then
                            command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                        End If

                        command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                        command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                        command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                        command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        command.Parameters.AddWithValue("@NetAmount", (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)))
                        command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                        command.Parameters.AddWithValue("@RcnoServiceRecord", lblid7.Text)
                        command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)
                        command.Parameters.AddWithValue("@Status", lblid9.Text)
                        command.Parameters.AddWithValue("@ContactType", lblid10.Text)
                        command.Parameters.AddWithValue("@CompanyGroup", TextBoxCompanyGroup.Text)
                        command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                        command.Parameters.AddWithValue("@ServiceBy", lblidServiceBy61.Text)
                        command.Parameters.AddWithValue("@ServiceLocationGroup", lblidServiceBy62.Text)
                        command.Parameters.AddWithValue("@Notes", lblidNotes63.Text)

                        command.Parameters.AddWithValue("@AgreeValue", Convert.ToDecimal(lblidAgreeValue.Text))
                        command.Parameters.AddWithValue("@Duration", Convert.ToInt32(lblidDuration.Text))
                        command.Parameters.AddWithValue("@DurationMS", (lblidDurationMS.Text))
                        command.Parameters.AddWithValue("@PerServiceValue", Convert.ToDecimal(lblidPerServiceValue.Text))
                        command.Parameters.AddWithValue("@OtherCode", dt1.Rows(0)("COACode").ToString())
                        command.Parameters.AddWithValue("@GLDescription", dt1.Rows(0)("COADescription").ToString())

                        command.Parameters.AddWithValue("@Location", lblidLocation.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Remarks", lblidRemarks.Text.ToUpper.Trim)

                        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Connection = conn
                        command.ExecuteNonQuery()

                        Dim sqlLastId1 As String
                        sqlLastId1 = "SELECT last_insert_id() from tblServiceBillingDetail"

                        Dim commandRcno1 As MySqlCommand = New MySqlCommand
                        commandRcno1.CommandType = CommandType.Text
                        commandRcno1.CommandText = sqlLastId1
                        commandRcno1.Parameters.Clear()
                        commandRcno1.Connection = conn
                        txtRcnotblServiceBillingDetail.Text = commandRcno1.ExecuteScalar()

                    Else

                        qry = "UPDATE tblServiceBillingDetail SET  AccountId=@AccountId, CustName=@ClientName, LocationId=@LocationId, Name=@AccountName,  BillAddress1=@BillAddress1, BillBuilding=@BillBuilding, BillStreet=@BillStreet, BillCountry=@BillCountry, BillPostal=@BillPostal, "
                        qry = qry + " ServiceDate = @ServiceDate, BillAmount=@BillAmount, DiscountAmount=@DiscountAmount,  GSTAmount=@GSTAmount, TotalWithGST=@TotalWithGST, NetAmount=@NetAmount, OurRef=@OurRef,YourRef=@YourRef,ContractNo=@ContractNo, PoNo=@PoNo, RcnoServiceRecord=@RcnoServiceRecord, BillingFrequency=@BillingFrequency, Salesman=@Salesman, ContactType=@ContactType, CompanyGroup=@CompanyGroup,   "
                        qry = qry + " ContractGroup = @ContractGroup, Status=@Status, Address1=@Address1, BatchNo=@BatchNo, ServiceBy=@ServiceBy, ServiceLocationGroup=@ServiceLocationGroup, Notes=@Notes, "
                        qry = qry + " AgreeValue =@AgreeValue, Duration=@Duration, DurationMS=@DurationMS, PerServiceValue=@PerServiceValue, OtherCode= @OtherCode, GLDescription=@GLDescription, Location=@Location, Remarks=@Remarks, "
                        qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn=@LastModifiedOn  "
                        qry = qry + " where RecordNo  = @ServiceRecordNo and BillSchedule = @BillSchedule  "
                        'qry = qry + " (@BillSchedule, @AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                        'qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                        'qry = qry + " @ContractGroup, @Status,  @Address1, @BatchNo, @Serviceby, @ServiceLocationGroup, @Notes,"
                        'qry = qry + " @AgreeValue, @Duration,  @DurationMS, @PerServiceValue,"
                        'qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                        command.Parameters.AddWithValue("@AccountId", lblid31.Text)
                        command.Parameters.AddWithValue("@ClientName", lblid2.Text)
                        command.Parameters.AddWithValue("@LocationId", lblid3.Text)
                        command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                        command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                        command.Parameters.AddWithValue("@Address1", lblid21.Text)

                        command.Parameters.AddWithValue("@BillAddress1", "")
                        command.Parameters.AddWithValue("@BillBuilding", "")
                        command.Parameters.AddWithValue("@BillStreet", "")
                        command.Parameters.AddWithValue("@BillCountry", "")
                        command.Parameters.AddWithValue("@BillPostal", "")
                        command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                        command.Parameters.AddWithValue("@OurRef", lblidOurReference56.Text)
                        command.Parameters.AddWithValue("@YourRef", lblidYourReference57.Text)
                        command.Parameters.AddWithValue("@PoNo", lblidPoNo58.Text)
                        command.Parameters.AddWithValue("@Salesman", lblidSalesman60.Text)

                        If lblid5.Text.Trim = "" Then
                            command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                        End If

                        command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                        command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                        command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                        command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        command.Parameters.AddWithValue("@NetAmount", (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)))
                        command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                        command.Parameters.AddWithValue("@RcnoServiceRecord", lblid7.Text)
                        command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)
                        command.Parameters.AddWithValue("@Status", lblid9.Text)
                        command.Parameters.AddWithValue("@ContactType", lblid10.Text)
                        command.Parameters.AddWithValue("@CompanyGroup", TextBoxCompanyGroup.Text)
                        command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                        command.Parameters.AddWithValue("@ServiceBy", lblidServiceBy61.Text)
                        command.Parameters.AddWithValue("@ServiceLocationGroup", lblidServiceBy62.Text)
                        command.Parameters.AddWithValue("@Notes", lblidNotes63.Text)

                        command.Parameters.AddWithValue("@AgreeValue", Convert.ToDecimal(lblidAgreeValue.Text))
                        command.Parameters.AddWithValue("@Duration", Convert.ToInt32(lblidDuration.Text))
                        command.Parameters.AddWithValue("@DurationMS", (lblidDurationMS.Text))
                        command.Parameters.AddWithValue("@PerServiceValue", Convert.ToDecimal(lblidPerServiceValue.Text))
                        command.Parameters.AddWithValue("@OtherCode", dt1.Rows(0)("COACode").ToString())
                        command.Parameters.AddWithValue("@GLDescription", dt1.Rows(0)("COADescription").ToString())
                        command.Parameters.AddWithValue("@Rcno", TextBoxRcnoServiceRecord.Text)
                        command.Parameters.AddWithValue("@Location", lblidLocation.Text.ToUpper.Trim)
                        command.Parameters.AddWithValue("@Remarks", lblidRemarks.Text.ToUpper.Trim)


                        'command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Connection = conn
                        command.ExecuteNonQuery()
                        '''''''' Detail

                        '    ''Start:Detail

                        '    Dim command1 As MySqlCommand = New MySqlCommand
                        '    command1.CommandType = CommandType.Text

                        '    'If lblid9.Text = "P" Then
                        '    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
                        '    'ElseIf lblid9.Text = "O" Then
                        '    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
                        '    'End If

                        '    If lblid9.Text = "P" Then
                        '        command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-SRV'"
                        '    ElseIf lblid9.Text = "O" Then
                        '        command1.CommandText = "SELECT * FROM tblbillingproducts where  ProductCode = 'IN-DEF'"
                        '    End If

                        '    command1.Connection = conn

                        '    Dim dr1 As MySqlDataReader = command1.ExecuteReader()
                        '    Dim dt1 As New DataTable
                        '    dt1.Load(dr1)

                        '    Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                        '    commandSalesDetail.CommandType = CommandType.Text
                        '    Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(BillSchedule, RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
                        '    qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate, InvoiceType, "
                        '    qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        '    qryDetail = qryDetail + "(@BillSchedule, @RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
                        '    qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate, @InvoiceType, "
                        '    qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        '    command.CommandText = qryDetail
                        '    command.Parameters.Clear()

                        '    command.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)
                        '    command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                        '    command.Parameters.AddWithValue("@Itemtype", "SERVICE")
                        '    command.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                        '    command.Parameters.AddWithValue("@Itemdescription", dt1.Rows(0)("Description").ToString())
                        '    command.Parameters.AddWithValue("@UOM", "")
                        '    command.Parameters.AddWithValue("@Qty", 1)
                        '    command.Parameters.AddWithValue("@PricePerUOM", Convert.ToDecimal(lblid23.Text))
                        '    'command.Parameters.AddWithValue("@BillAmount", 0.0)
                        '    command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(lblid23.Text))
                        '    command.Parameters.AddWithValue("@DiscPerc", 0.0)
                        '    command.Parameters.AddWithValue("@DiscAmount", 0.0)
                        '    command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(lblid23.Text))
                        '    command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(txtTaxRatePct.Text))
                        '    command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                        '    command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        '    command.Parameters.AddWithValue("@TaxType", "SR")
                        '    command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                        '    command.Parameters.AddWithValue("@ARCode", "")
                        '    command.Parameters.AddWithValue("@GSTCode", "")
                        '    command.Parameters.AddWithValue("@OtherCode", dt1.Rows(0)("COACode").ToString())
                        '    command.Parameters.AddWithValue("@GLDescription", dt1.Rows(0)("COADescription").ToString())
                        '    command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                        '    'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                        '    command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                        '    command.Parameters.AddWithValue("@ServiceStatus", lblid9.Text)
                        '    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                        '    command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                        '    command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                        '    command.Parameters.AddWithValue("@InvoiceType", "SERVICE")
                        '    If lblid5.Text.Trim = "" Then
                        '        command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        '    Else
                        '        command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                        '    End If
                        '    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        '    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        '    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        '    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        '    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        '    command.Connection = conn
                        '    command.ExecuteNonQuery()
                        '    'conn.Close()

                        '    '''''''' Detail
                        'End If  'end of If dtExistServiceBillingDetail.Rows.Count = 0 Then
                    End If
                    ToBillAmtTot = Convert.ToDecimal(ToBillAmt)
                    DiscAmountTot = Convert.ToDecimal(DiscAmount)
                    GSTAmountTot = Convert.ToDecimal(GSTAmount)
                    NetAmountTot = Convert.ToDecimal(NetAmount)
                Else
                    If lblidRecordType.Text = "E" Then
                        Dim qry1 As String
                        Dim command2 As MySqlCommand = New MySqlCommand
                        command2.CommandType = CommandType.Text

                        qry1 = "Delete from  tblServiceBillingDetail where  "
                        qry1 = qry1 + "   RecordNo = '" & lblid4.Text & "' and BillSchedule = '" & txtInvoiceNo.Text & "'"

                        command2.CommandText = qry1
                        command2.Parameters.Clear()
                        command2.Connection = conn
                        command2.ExecuteNonQuery()
                        command2.Dispose()
                    End If
                End If


                '03.03.17
            Next rowIndex


            '30.03.24
            Dim commandUpdateGST As MySqlCommand = New MySqlCommand
            commandUpdateGST.CommandType = CommandType.StoredProcedure
            commandUpdateGST.CommandText = "UpdateGSTFromContractGroupForBatchInvoice"

            commandUpdateGST.Parameters.Clear()

            commandUpdateGST.Parameters.AddWithValue("@pr_BillSchedule", txtInvoiceNo.Text.Trim)
            commandUpdateGST.Parameters.AddWithValue("@pr_GST", txtDefaultTaxCode.Text.Trim)
            commandUpdateGST.Parameters.AddWithValue("@pr_GSTRate", txtTaxRatePct.Text.Trim)

            commandUpdateGST.Connection = conn
            commandUpdateGST.ExecuteScalar()

            'grvServiceRecDetails.DataSourceID = "SqlDSServices"
            'grvServiceRecDetails.DataBind()
            '30.03.24

            txtInvoiceAmount.Text = Convert.ToDecimal(ToBillAmtTot).ToString("N2")
            txtDiscountAmount.Text = Convert.ToDecimal(DiscAmountTot).ToString("N2")
            txtAmountWithDiscount.Text = (Convert.ToDecimal(ToBillAmtTot) - Convert.ToDecimal(DiscAmountTot)).ToString("N2")
            'txtGSTAmount.Text = Convert.ToDecimal(GSTAmountTot).ToString("N2")
            'txtNetAmount.Text = Convert.ToDecimal(NetAmountTot).ToString("N2")

            'txtTotalWithDiscAmt.Text = TotalWithDiscAmt.ToString

            txtGSTAmount.Text = Convert.ToDecimal(ToBillAmtTot) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
            txtNetAmount.Text = Convert.ToDecimal(txtInvoiceAmount.Text) + (Convert.ToDecimal(txtGSTAmount.Text))

            'txtGSTAmount.Text = Convert.ToDecimal(txtTotalServiceSelected.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
            'txtNetAmount.Text = Convert.ToDecimal(txtTotalServiceSelected.Text) + (Convert.ToDecimal(txtTotalServiceSelected.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)


            updPnlBillingRecs.Update()

            'End: Update Invoice Amounts

            '''''''''''''''''''''''''''''''''''''
            DisplayGLGrid()

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            'End If



            'MakeMeNull()

            'If rowselected = 0 Then
            '    lblAlert.Text = "PLEASE SELECT A RECORD"
            '    Exit Sub
            'End If

            'lblMessage.Text = "ADD: BATCH INVOICE SUCCESSFULLY GENERATED"
            lblMessage.Text = "ADD: BATCH SCHEDULE SUCCESSFULLY SAVED"
            lblAlert.Text = ""
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHSCH", txtBatchNo.Text, "SAVE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtBatchNo.Text, "", txtRcno.Text)


            'GridView1.DataSourceID = "SQLDSInvoice"
            'GridView1.DataBind()

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE Billschedule in (select BillSchedule from tblservicebillingdetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                GridView1.DataSourceID = "SQLDSInvoice"
                GridView1.DataBind()
            Else
                SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                GridView1.DataSourceID = "SQLDSInvoice"
                GridView1.DataBind()
            End If

            mdlPopupConfirmSavePost.Show()

            If txtMode.Text = "NEW" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            End If


            DisableControls()

            btnSaveInvoice.Enabled = False
            btnSaveInvoice.ForeColor = System.Drawing.Color.Gray

            btnGenerateInvoice.Enabled = True
            btnGenerateInvoice.ForeColor = System.Drawing.Color.Black

            txtMode.Text = ""
            ButtonPressed = "N"

            updPnlMsg.Update()
            updPnlSearch.Update()
            updpnlServiceRecs.Update()
            updPnlBillingRecs.Update()
            UpdatePanel1.Update()

            Session("CheckRefresh") = Server.UrlDecode(Date.Now.ToString())

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> isSubmitted = False;</Script>", False)

        Catch ex As Exception

            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "btnSaveInvoice_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
        'End If

    End Sub

    Protected Sub SaveServiceBillingDetail()
        '''''''''''''''''''''''''''''''''''''''

        Dim qry As String
        Dim command As MySqlCommand = New MySqlCommand
        command.CommandType = CommandType.Text
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()

        Dim rowselected As Integer = Convert.ToInt32(txtRowSelected.Text)

        Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtAccountIdGV"), TextBox)
        Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtClientNameGV"), TextBox)
        Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtLocationIdGV"), TextBox)
        Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceRecordNoGV"), TextBox)
        Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
        Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtBillingFrequencyGV"), TextBox)
        Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtRcnoServiceRecordGV"), TextBox)
        Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtDeptGV"), TextBox)
        Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtStatusGV"), TextBox)
        Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtContractNoGV"), TextBox)
        Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceAddressGV"), TextBox)
        'Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
        Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)

        If txtMode.Text = "NEW" Then

            'Dim command As MySqlCommand = New MySqlCommand
            'command.CommandType = CommandType.Text

            If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                txtRcnotblServiceBillingDetail.Text = 0
            End If

            'If Convert.ToInt64(txtBatchNo.Text) = 0 Then

            '''''''''''''''''''''
            Dim commandExistServiceBillingDetail As MySqlCommand = New MySqlCommand
            commandExistServiceBillingDetail.CommandType = CommandType.Text
            'command1.CommandText = Sql
            commandExistServiceBillingDetail.CommandText = "SELECT * FROM tblServiceBillingDetail where RcnoServiceRecord=" & Convert.ToInt64(lblid7.Text) & " and Batchno = '" & txtBatchNo.Text & "'"
            commandExistServiceBillingDetail.Connection = conn

            Dim drExistServiceBillingDetail As MySqlDataReader = commandExistServiceBillingDetail.ExecuteReader()
            Dim dtExistServiceBillingDetail As New DataTable
            dtExistServiceBillingDetail.Load(drExistServiceBillingDetail)

            If dtExistServiceBillingDetail.Rows.Count = 0 Then

                '''''''''''''''''''''
                qry = "INSERT INTO tblServiceBillingDetail( AccountId, CustName, LocationId, Name, RecordNo, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, "
                qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                qry = qry + " ContractGroup, Status, Address1,   "
                qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                qry = qry + " (@AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
                qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
                qry = qry + " @ContractGroup, @Status,  @Address1,  "
                qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@AccountId", lblid1.Text)
                command.Parameters.AddWithValue("@ClientName", lblid2.Text)
                command.Parameters.AddWithValue("@LocationId", lblid3.Text)
                command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
                command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
                command.Parameters.AddWithValue("@Address1", lblid21.Text)
                command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
                command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
                command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)
                command.Parameters.AddWithValue("@BillCountry", txtBillCountry.Text)
                command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)
                command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                If lblid5.Text.Trim = "" Then
                    command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                End If

                'command.Parameters.AddWithValue("@ServiceDate", lblid5.Text)
                'Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)
                Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
                'Dim lbd30 As String = TextBoxGSTAmount.Text

                'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
                '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
                '    command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                '    command.Parameters.AddWithValue("@GSTAmount", 0.0)
                '    command.Parameters.AddWithValue("@TotalWithGST", 0.0)
                '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(lblid23.Text))
                'Else
                '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                '    command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                '    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                '    command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                'End If


                command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))

                command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
                'command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                command.Parameters.AddWithValue("@RcnoServiceRecord", lblid7.Text)

                command.Parameters.AddWithValue("@PoNo", txtPONo.Text)
                command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)
                command.Parameters.AddWithValue("@Status", lblid9.Text)
                command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
                command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)

                If ddlSalesmanBilling.Text = "-1" Then
                    command.Parameters.AddWithValue("@Salesman", "")
                Else
                    command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                End If

                command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command.Connection = conn
                command.ExecuteNonQuery()

                Dim sqlLastId As String
                sqlLastId = "SELECT last_insert_id() from tblServiceBillingDetail"

                Dim commandRcno As MySqlCommand = New MySqlCommand
                commandRcno.CommandType = CommandType.Text
                commandRcno.CommandText = sqlLastId
                commandRcno.Parameters.Clear()
                commandRcno.Connection = conn
                txtRcnotblServiceBillingDetail.Text = commandRcno.ExecuteScalar()

                If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
                    txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text

                    qry = "Update tblServiceBillingDetail set BatchNo = '" & txtBatchNo.Text & "' where rcno = " & txtBatchNo.Text

                    command.CommandText = qry
                    command.Parameters.Clear()
                    command.Connection = conn
                    command.ExecuteNonQuery()

                End If
            End If
        Else
            If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
                txtRcnotblServiceBillingDetail.Text = 0
            End If
            If Convert.ToInt64(txtRcnotblServiceBillingDetail.Text) > 0 Then
                qry = "Update tblServiceBillingDetail set BillAmount = @BillAmount, DiscountAmount= @DiscountAmount,  GSTAmount =@GSTAmount,  "
                qry = qry + "TotalWithGST = @TotalWithGST, NetAmount =@NetAmount, OurRef = @OurRef ,YourRef =@YourRef, PoNo =@PoNo, Salesman =@Salesman,    "
                qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn = @LastModifiedOn; "

                command.CommandText = qry
                command.Parameters.Clear()

                command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
                command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
                command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
                command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
                command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
                command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
                command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
                command.Parameters.AddWithValue("@PoNo", txtPONo.Text)

                If ddlSalesmanBilling.Text = "-1" Then
                    command.Parameters.AddWithValue("@Salesman", "")
                Else
                    command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
                End If

                command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                command.Connection = conn
                command.ExecuteNonQuery()
            End If
        End If


        '''' Detail

        'Dim rowselected As Integer
        'rowselected = 0

        'Dim conn As MySqlConnection = New MySqlConnection()


        'Start: Delete existing Records

        'If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
        '    txtRcnotblServiceBillingDetail.Text = "0"
        'End If

        'If txtRcnotblServiceBillingDetail.Text <> "0" Then '04.01.17

        Dim commandtblServiceBillingDetailItem As MySqlCommand = New MySqlCommand

        commandtblServiceBillingDetailItem.CommandType = CommandType.Text
        'Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
        Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where RcnoServiceBillingDetail = '" & Convert.ToInt32(txtRcnotblServiceBillingDetail.Text) & "'"

        commandtblServiceBillingDetailItem.CommandText = qrycommandtblServiceBillingDetailItem
        commandtblServiceBillingDetailItem.Parameters.Clear()
        commandtblServiceBillingDetailItem.Connection = conn
        commandtblServiceBillingDetailItem.ExecuteNonQuery()

        'End: Delete Existing Records


        SetRowDataServiceRecs()
        Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

        If tableAdd IsNot Nothing Then

            For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
                Dim lbd10 As String = TextBoxQty.Text



                Dim TextBoxItemTypeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemTypeGV"), DropDownList)
                Dim TextBoxItemCodeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemCodeGV"), DropDownList)
                Dim TextBoxItemDescriptionGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemDescriptionGV"), TextBox)
                Dim TextBoxUOMGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtUOMGV"), DropDownList)
                Dim TextBoxPricePerUOMGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
                Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
                Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscPercGV"), TextBox)
                Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
                Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
                Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTPercGV"), TextBox)
                Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
                Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceWithGSTGV"), TextBox)
                Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
                Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
                Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGLDescriptionGV"), TextBox)
                Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
                Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtServiceStatusGV"), TextBox)

                If String.IsNullOrEmpty(lbd10) = False Then
                    If (Convert.ToInt64(lbd10) > 0) Then

                        ''Start:Detail
                        Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                        commandSalesDetail.CommandType = CommandType.Text
                        Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
                        qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate,"
                        qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                        qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
                        qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate,"
                        qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                        command.CommandText = qryDetail
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
                        command.Parameters.AddWithValue("@Itemtype", TextBoxItemTypeGV.Text)
                        command.Parameters.AddWithValue("@ItemCode", TextBoxItemCodeGV.Text)
                        command.Parameters.AddWithValue("@Itemdescription", TextBoxItemDescriptionGV.Text)

                        If TextBoxUOMGV.Text <> "-1" Then
                            command.Parameters.AddWithValue("@UOM", TextBoxUOMGV.Text)

                        Else
                            command.Parameters.AddWithValue("@UOM", "")
                        End If

                        command.Parameters.AddWithValue("@Qty", TextBoxQty.Text)
                        command.Parameters.AddWithValue("@PricePerUOM", TextBoxPricePerUOMGV.Text)
                        'command.Parameters.AddWithValue("@BillAmount", 0.0)
                        command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(TextBoxTotalPrice.Text))
                        command.Parameters.AddWithValue("@DiscPerc", Convert.ToDecimal(TextBoxDiscPerc.Text))
                        command.Parameters.AddWithValue("@DiscAmount", Convert.ToDecimal(TextBoxDiscAmount.Text))
                        command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text))
                        command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(TextBoxGSTPerc.Text))
                        command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(TextBoxGSTAmt.Text))
                        command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(TextBoxTotalPriceWithGST.Text))
                        command.Parameters.AddWithValue("@TaxType", TextBoxTaxType.Text)
                        command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
                        command.Parameters.AddWithValue("@ARCode", "")
                        command.Parameters.AddWithValue("@GSTCode", "")
                        command.Parameters.AddWithValue("@OtherCode", TextBoxOtherCode.Text)
                        command.Parameters.AddWithValue("@GLDescription", TextBoxGLDescription.Text)
                        command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                        'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
                        command.Parameters.AddWithValue("@ContractNo", TextBoxContractNo.Text)
                        command.Parameters.AddWithValue("@ServiceStatus", TextBoxServiceStatus.Text)
                        command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
                        command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

                        command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)

                        If lblid5.Text.Trim = "" Then
                            command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                        Else
                            command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
                        End If
                        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Connection = conn
                        command.ExecuteNonQuery()
                        'conn.Close()
                    End If

                End If
            Next rowIndex
        End If
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
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub


    Protected Sub txtItemTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)

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
            Dim lblid8 As DropDownList = CType(xgrvBillingDetails.FindControl("txtItemTypeGV"), DropDownList)
            Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtGLDescriptionGV"), TextBox)

            'lTargetDesciption = lblid1.Text

            Dim rowindex1 As Integer = xgrvBillingDetails.RowIndex

            'Get Item desc, price Id

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            'command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = '" & lblid1.Text & "'"
            command1.CommandText = "SELECT * FROM tblbillingproducts where   ProductCode = '" & lblid1.Text & "'"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                lblid2.Text = dt.Rows(0)("Description").ToString

                If lblid8.Text = "PRODUCT" Then
                    lblid3.Text = dt.Rows(0)("Price").ToString
                End If
                lblid4.Text = 1
                lblid5.Text = dt.Rows(0)("COACode").ToString
                lblid6.Text = dt.Rows(0)("TaxType").ToString
                lblid7.Text = dt.Rows(0)("TaxRate").ToString
                lblid9.Text = dt.Rows(0)("COADescription").ToString

                CalculatePrice()
            End If


            'If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
            '    btnAddDetail_Click(sender, e)
            '    'txtRecordAdded.Text = "Y"
            'End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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

                'Dim TextBoxRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)

                'If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
                '    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

                '        Dim conn As MySqlConnection = New MySqlConnection(constr)
                '        conn.Open()

                '        Dim commandIsCountRecords As MySqlCommand = New MySqlCommand
                '        commandIsCountRecords.CommandType = CommandType.Text
                '        commandIsCountRecords.CommandText = "Select count(*) as totalrecords from tblservicebillingdetailitem where rcnoServiceBillingDetail = " & Convert.ToInt64(txtRcnotblServiceBillingDetail.Text)
                '        commandIsCountRecords.Connection = conn

                '        Dim drIsCountRecords As MySqlDataReader = commandIsCountRecords.ExecuteReader()

                '        Dim dtIsCountRecords As New DataTable
                '        dtIsCountRecords.Load(drIsCountRecords)

                '        If dtIsCountRecords.Rows(0)("totalrecords").ToString = 1 Then
                '            Dim commandDeleteServicebillingdetail As MySqlCommand = New MySqlCommand
                '            commandDeleteServicebillingdetail.CommandType = CommandType.Text
                '            commandDeleteServicebillingdetail.CommandText = "Delete from tblservicebillingdetail where rcno = " & txtRcnotblServiceBillingDetail.Text
                '            commandDeleteServicebillingdetail.Connection = conn
                '            commandDeleteServicebillingdetail.ExecuteNonQuery()


                '            PopulateServiceGrid()
                '            'GridView1_SelectedIndexChanged(New Object(), New EventArgs)

                '            '''''''''''''''''''''''

                '            'Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                '            'Dim drCurrentRowLoc As DataRow = Nothing

                '            'For i As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
                '            '    dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                '            '    drCurrentRowLoc = dtScdrLoc.NewRow()
                '            '    ViewState("CurrentTableServiceRec") = dtScdrLoc
                '            '    grvServiceRecDetails.DataSource = dtScdrLoc
                '            '    grvServiceRecDetails.DataBind()

                '            '    SetPreviousDataServiceRecs()
                '            'Next i

                '            '''''''''''''''''''''''
                '            'Dim dt1 As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                '            'Dim drCurrentRow1 As DataRow = Nothing
                '            'If dt1.Rows.Count > 0 Then
                '            '    dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))
                '            '    drCurrentRow1 = dt1.NewRow()
                '            '    ViewState("CurrentTableServiceRec") = dt1
                '            '    grvServiceRecDetails.DataSource = dt1
                '            '    grvServiceRecDetails.DataBind()

                '            '    'SetPreviousData()
                '            '    'SetPreviousDataBillingDetailsRecs()
                '            '    SetPreviousDataServiceRecs()
                '            '    If dt1.Rows.Count = 0 Then
                '            '        FirstGridViewRowServiceRecs()
                '            '    End If
                '            '    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                '            'End If

                '            'Dim dt1 As DataTable = CType(ViewState("CurrentTableServiceRec"), DataTable)
                '            'Dim drCurrentRow1 As DataRow = Nothing
                '            'dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))


                '            'drCurrentRow1 = dt1.NewRow()
                '            'ViewState("CurrentTableServiceRec") = dt1
                '            'grvServiceRecDetails.DataSource = dt1
                '            'grvServiceRecDetails.DataBind()

                '            ' ''SetPreviousData()
                '            ' ''SetPreviousDataBillingDetailsRecs()
                '            'SetPreviousDataServiceRecs()


                '            'For i As Integer = 0 To grvServiceRecDetails.Rows.Count - 2

                '            '    If i = grcnoservicebillingdetail Then
                '            '        dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))
                '            '    End If

                '            '    'dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
                '            '    'drCurrentRowLoc = dtScdrLoc.NewRow()
                '            '    'ViewState("CurrentTableServiceRec") = dt1
                '            '    'grvServiceRecDetails.DataSource = dt1
                '            '    'grvServiceRecDetails.DataBind()

                '            '    SetPreviousDataServiceRecs()
                '            'Next i

                '            'ViewState("CurrentTableServiceRec") = dt1
                '            'grvServiceRecDetails.DataSource = dt1
                '            'grvServiceRecDetails.DataBind()


                '            'If dt1.Rows.Count = 0 Then
                '            '    FirstGridViewRowServiceRecs()
                '            '    'FirstGridViewRowBillingDetailsRecs()
                '            'End If

                '            updPanelInvoice.Update()
                '        End If


                '        Dim commandUpdGS As MySqlCommand = New MySqlCommand
                '        commandUpdGS.CommandType = CommandType.Text
                '        commandUpdGS.CommandText = "Delete from tblservicebillingdetailitem where rcno = " & TextBoxRcno.Text
                '        commandUpdGS.Connection = conn
                '        commandUpdGS.ExecuteNonQuery()

                '        conn.Close()
                '    End If
                'End If

                If dt.Rows.Count > 0 Then
                    dt.Rows.Remove(dt.Rows(rowIndex))
                    drCurrentRow = dt.NewRow()
                    ViewState("CurrentTableBillingDetailsRec") = dt
                    grvBillingDetails.DataSource = dt
                    grvBillingDetails.DataBind()

                    'SetPreviousData()
                    SetPreviousDataBillingDetailsRecs()

                    If dt.Rows.Count = 0 Then
                        FirstGridViewRowBillingDetailsRecs()
                    End If
                    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
                End If

                CalculateTotalPrice()

            End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub gvLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLocation.SelectedIndexChanged
        'Dim lblid1 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("ddlLocationIdGV"), TextBox)
        'Dim lblid2 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtServiceNameGV"), TextBox)
        'Dim lblid3 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtServiceAddressGV"), TextBox)
        'Dim lblid4 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtZoneGV"), TextBox)

        If gvLocation.SelectedRow.Cells(1).Text = "&nbsp;" Then
            txtLocationId.Text = " "
        Else
            txtLocationId.Text = gvLocation.SelectedRow.Cells(1).Text
            'lblid1.Text = txtLocationId.Text
        End If

        If gvLocation.SelectedRow.Cells(2).Text = "&nbsp;" Then
            txtClientName.Text = " "
        Else
            txtClientName.Text = Server.HtmlDecode(gvLocation.SelectedRow.Cells(2).Text)
            'lblid2.Text = txtServiceName.Text
        End If


    End Sub

    Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
        'mdlPopupStatusSearch.Show()
    End Sub

    'Protected Sub btnStatusSearch_Click(sender As Object, e As EventArgs) Handles btnStatusSearch.Click
    '    Try
    '        Dim YrStrList As List(Of [String]) = New List(Of String)()

    '        'If rdbStatusSearch.SelectedValue = "ALL" Then
    '        '    For Each item As ListItem In chkStatusSearch.Items
    '        '        YrStrList.Add(item.Value)
    '        '    Next
    '        'Else
    '        For Each item As ListItem In chkStatusSearch.Items
    '            If item.Selected Then
    '                YrStrList.Add(item.Value)
    '            End If
    '        Next

    '        Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())

    '        txtSearch1Status.Text = YrStr
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '        lblAlert.Text = exstr
    '        'Dim message As String = "alert('" + exstr + "')"
    '        'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
    '    End Try
    'End Sub

    Protected Sub txtAccountId_TextChanged(sender As Object, e As EventArgs) Handles txtAccountId.TextChanged
        'Dim query As String
        'query = "Select ContractNo from tblContract where AccountId = '" & txtAccountId.Text & "'"
        'PopulateDropDownList(query, "ContractNo", "ContractNo", ddlContractNo)
    End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        Try
            txtInvoicenoSearch.Text = ""
            txtAccountIdSearch.Text = ""
            txtBillingPeriodSearch.Text = ""
            txtClientNameSearch.Text = ""
            ddlCompanyGrpSearch.SelectedIndex = 0
            ddlBillingFrequencySearch.SelectedIndex = 0
            txtSearch1Status.Text = "O,P"
            ddlCompanyGrpSearch.SelectedIndex = 0

            btnQuickSearch_Click(sender, e)

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
        'btnSearch1Status_Click(sender, e)
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try

    End Sub

    Protected Sub grvServiceRecDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvServiceRecDetails.RowCommand
        'If (e.CommandName = "Delete") Then
        '    btnDeleteUnselected_Click(sender, e)
        'End If

    End Sub

    Protected Sub grvServiceRecDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvServiceRecDetails.RowDataBound

        If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim TextBoxToBillAmt As TextBox = CType(e.Row.FindControl("txtToBillAmtGV"), TextBox)
                TextBoxToBillAmt.ReadOnly = False
                TextBoxToBillAmt.BackColor = Color.White
                TextBoxToBillAmt.BorderStyle = BorderStyle.NotSet
                TextBoxToBillAmt.AutoPostBack = True
            End If
        End If

    End Sub

    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        lblAlert.Text = ""
        lblMessage.Text = ""

        Try
            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            If String.IsNullOrEmpty(txtCreditDays.Text) = True Then
                txtCreditDays.Text = "0"
            End If

            Dim confirmValue As String
            confirmValue = ""


            'confirmValue = Request.Form("confirm_value")
            'If Right(confirmValue, 3) = "Yes" Then
            ''''''''''''''' Insert tblAR

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text

            Dim qry1 As String = "DELETE from tblAR where BatchNo = '" & txtBatchNo.Text & "'"

            command1.CommandText = qry1
            command1.Connection = conn
            command1.ExecuteNonQuery()


            ''''''''''''''''''''

            'Start:Loop thru' Credit values


            Dim commandSales As MySqlCommand = New MySqlCommand

            commandSales.CommandType = CommandType.Text
            commandSales.CommandText = "SELECT *  FROM tblSales where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by InvoiceNumber"
            commandSales.Connection = conn

            Dim drSales As MySqlDataReader = commandSales.ExecuteReader()
            Dim dtSales As New DataTable
            dtSales.Load(drSales)

            Dim rowIndex As Integer
            rowIndex = 0
            For Each row1 As DataRow In dtSales.Rows

                '''''''''''''''''''''

                'End: Loop thru' Credit Values

                Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                commandUpdateInvoice.CommandType = CommandType.Text
                Dim sqlUpdateInvoice As String = "Update tblsales set PostStatus = 'P'  where InvoiceNumber = '" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

                commandUpdateInvoice.CommandText = sqlUpdateInvoice
                commandUpdateInvoice.Parameters.Clear()
                commandUpdateInvoice.Connection = conn
                commandUpdateInvoice.ExecuteNonQuery()

                'GridView1.DataBind()

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE Billschedule in (select BillSchedule from tblservicebillingdetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()
                Else
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()
                End If

                Dim commandUpdateServicebillingdetail As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetail.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetail As String = "Update tblservicebillingdetail set Posted = 'P'  where InvoiceNo  ='" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

                commandUpdateServicebillingdetail.CommandText = sqlUpdateServicebillingdetail
                commandUpdateServicebillingdetail.Parameters.Clear()
                commandUpdateServicebillingdetail.Connection = conn
                commandUpdateServicebillingdetail.ExecuteNonQuery()

                Dim commandUpdateServicebillingdetailItem As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetailItem.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetailItem As String = "Update tblservicebillingdetailitem set Posted = 'P'  where InvoiceNo  ='" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

                commandUpdateServicebillingdetailItem.CommandText = sqlUpdateServicebillingdetailItem
                commandUpdateServicebillingdetailItem.Parameters.Clear()
                commandUpdateServicebillingdetailItem.Connection = conn
                commandUpdateServicebillingdetailItem.ExecuteNonQuery()

                rowIndex = rowIndex + 1

            Next row1


            Dim commandUpdateServiceBillschedule As MySqlCommand = New MySqlCommand
            commandUpdateServiceBillschedule.CommandType = CommandType.Text
            Dim sqlUpdateServicebillSchedule As String = "Update tblserviceBillschedule set PostStatus = 'P'  where BillSchedule  ='" & txtInvoiceNo.Text & "'"

            commandUpdateServiceBillschedule.CommandText = sqlUpdateServicebillSchedule
            commandUpdateServiceBillschedule.Parameters.Clear()
            commandUpdateServiceBillschedule.Connection = conn
            commandUpdateServiceBillschedule.ExecuteNonQuery()


            If txtMode.Text = "NEW" Then
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)
            Else
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)
            End If

            conn.Close()
            conn.Dispose()

            MakeMeNullBillingDetails()
            MakeMeNull()
            DisableControls()
            FirstGridViewRowGL()



            ''''''''''''''' Insert tblAR


            lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
            lblAlert.Text = ""
            updPnlSearch.Update()
            updPnlMsg.Update()
            updpnlBillingDetails.Update()
            updpnlServiceRecs.Update()
            updpnlBillingDetails.Update()
            'End If
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "btnPost_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMode.Text = ""
        MakeMeNullBillingDetails()
        MakeMeNull()
        DisableControls()


        btnCancel.Enabled = False
        btnCancel.ForeColor = Color.Gray


        ''   txt.Text = "select * from tblasset where rcno<>0;"
        ''SqlDataSource1.SelectCommand = "SELECT * FROM tblcompany WHERE  Inactive=0 order by AccountId desc limit 100;"
        ''SqlDataSource1.DataBind()
        'SQLDSContract.DataBind()
        'GridView1.DataBind()
    End Sub



    Protected Sub btnClientSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnClientSearch.Click
        lblAlert.Text = ""
        'If String.IsNullOrEmpty(ddlContactType.Text) Or ddlContactType.Text = "--SELECT--" Then
        '    '  MessageBox.Message.Alert(Page, "Select Customer Type to proceed!!!", "str")
        '    lblAlert.Text = "SELECT CUSTOMER TYPE TO PROCEED"
        '    Exit Sub
        'End If

        txtSearch.Text = "Search"
        If String.IsNullOrEmpty(txtAccountIdSearch.Text.Trim) = False Then
            txtPopUpClient.Text = txtAccountIdSearch.Text
            txtPopupClientSearch.Text = txtPopUpClient.Text

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%'  or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where 1=1  and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                End If
            Else
                If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD  From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%'  or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                End If
            End If
           

            SqlDSClient.DataBind()
            gvClient.DataBind()
            updPanelInvoice.Update()
        Else

            If txtDisplayRecordsLocationwise.Text = "Y" Then
                If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where 1=1 and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  C.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"

                End If
            Else
                If ddlContactTypeSearch.Text = "CORPORATE" Or ddlContactTypeSearch.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlContactTypeSearch.SelectedItem.Text = "RESIDENTIAL" Or ddlContactTypeSearch.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD  From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or A.ID like '%" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or C.ID like '%" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"

                End If
            End If
           

            SqlDSClient.DataBind()
            gvClient.DataBind()
            updPanelInvoice.Update()
        End If
        mdlPopUpClient.Show()
    End Sub

    Protected Sub ddlCreditTerms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCreditTerms.SelectedIndexChanged
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim commandDocControl As MySqlCommand = New MySqlCommand
            commandDocControl.CommandType = CommandType.Text
            commandDocControl.CommandText = "SELECT TermsDay FROM tblterms where Terms='" & ddlCreditTerms.Text & "'"
            commandDocControl.Connection = conn

            Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                txtCreditDays.Text = dt.Rows(0)("TermsDay").ToString


            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            commandDocControl.Dispose()
            dt.Dispose()
            dr.Close()
            'conn.Close()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
    End Sub

    Protected Sub txtInvoiceDate_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceDate.TextChanged
        txtBillingPeriod.Text = Year(Convert.ToDateTime(txtInvoiceDate.Text)) & Format(Month(Convert.ToDateTime(txtInvoiceDate.Text)), "00")

        FindGSTPct()

        '24.03.24


        'If ddlContractGroup.SelectedIndex = 0 Then
        '    FindDefaultTaxCodeandPctFromPeriod(txtBillingPeriod.Text)
        '    'Else
        '    '    FindDefaultTaxCodeandPctFromContractGroup(ddlContractGroup.Text)
        'End If


        'FindDefaultTaxCodeandPctFromPeriod(txtBillingPeriod.Text)
        '24.03.24

    End Sub

    Protected Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        'Me.cpnl1.Collapsed = False
        'Me.cpnl1.ClientState = "false"
        'updPnlBillingRecs.Update()

        'Try
        '    Dim rowIndex As Integer = 0
        '    Dim Total As Decimal
        '    Dim TotalWithGST As Decimal
        '    Dim TotalDiscAmt As Decimal
        '    Dim TotalGSTAmt As Decimal
        '    Dim TotalPriceWithDiscountAmt As Decimal

        '    Dim TextBoxGSTAmt As TextBox
        '    Dim TextBoxTotalPrice As TextBox
        '    Dim TextBoxTotalPriceWithGST As TextBox
        '    Dim TextBoxDiscAmt As TextBox
        '    Dim TextBoxPriceWithDisc As TextBox

        '    Total = 0.0
        '    TotalWithGST = 0.0
        '    TotalDiscAmt = 0.0
        '    TotalGSTAmt = 0.0
        '    TotalPriceWithDiscountAmt = 0.0

        '    Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)


        '    If (table.Rows.Count > 0) Then
        '        FirstGridViewRowBillingDetailsRecs()
        '        For i As Integer = 0 To (table.Rows.Count) - 1

        '            Dim TextBoxchkSelect As CheckBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

        '            If (TextBoxchkSelect.Checked = True) Then

        '                Dim TextBoxInvoiceAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
        '                Dim TextBoxDiscountAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)
        '                Dim TextBoxGSTAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtGSTAmountGV"), TextBox)
        '                Dim TextBoxNetAmount As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtNetAmountGV"), TextBox)

        '                Dim lblid1 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtAccountIdGV"), TextBox)
        '                Dim lblid2 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtAccountNameGV"), TextBox)
        '                Dim lblid3 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillAddress1GV"), TextBox)
        '                Dim lblid4 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillBuildingGV"), TextBox)
        '                Dim lblid5 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillStreetGV"), TextBox)
        '                Dim lblid6 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillCountryGV"), TextBox)
        '                Dim lblid7 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtBillPostalGV"), TextBox)
        '                Dim lblid8 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtOurReferenceGV"), TextBox)
        '                Dim lblid9 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtYourReferenceGV"), TextBox)
        '                Dim lblid10 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtPoNoGV"), TextBox)
        '                Dim lblid11 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtCreditTermsGV"), TextBox)
        '                Dim lblid12 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtSalesmanGV"), TextBox)
        '                Dim lblid13 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
        '                Dim lblid14 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoServiceRecordGV"), TextBox)

        '                Dim lblid16 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
        '                Dim lblid17 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoInvoiceGV"), TextBox)
        '                Dim lblid18 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtRcnoServicebillingdetailGV"), TextBox)
        '                Dim lblid19 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContactTypeGV"), TextBox)
        '                Dim lblid20 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtInvoiceNoGV"), TextBox)

        '                Dim lblid21 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtDiscAmountGV"), TextBox)

        '                'Dim lblid23 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtToBillAmtGV"), TextBox)

        '                Dim lblid15 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
        '                'Dim lblid24 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtServiceRecordNoGV"), TextBox)
        '                Dim lblid25 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtStatusGV"), TextBox)

        '                Dim lblid22 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContractNoGV"), TextBox)
        '                'Dim lblid26 As TextBox = CType(grvServiceRecDetails.Rows(i).Cells(0).FindControl("txtContractNoGV"), TextBox)

        '                If txtMode.Text = "NEW" Then
        '                    txtAccountType.Text = lblid19.Text
        '                    txtCompanyGroup.Text = lblid16.Text
        '                    'txtInvoiceDate.Text = lblid20.Text
        '                    txtAccountIdBilling.Text = lblid1.Text
        '                    txtAccountName.Text = lblid2.Text
        '                    txtBillAddress.Text = lblid3.Text
        '                    txtBillBuilding.Text = lblid4.Text
        '                    txtBillStreet.Text = lblid5.Text
        '                    txtBillCountry.Text = lblid6.Text
        '                    txtBillPostal.Text = lblid7.Text
        '                    txtOurReference.Text = lblid8.Text
        '                    txtYourReference.Text = lblid9.Text
        '                    txtPONo.Text = lblid10.Text
        '                    txtContractNo.Text = lblid22.Text
        '                End If

        '                ''''''''''''''''''''''''''''''''''''''''''''''''

        '                Dim conn As MySqlConnection = New MySqlConnection()

        '                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '                If conn.State = ConnectionState.Open Then
        '                    conn.Close()
        '                End If
        '                conn.Open()
        '                Dim sql As String
        '                sql = ""

        '                Dim command21 As MySqlCommand = New MySqlCommand

        '                sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLtype='TRADE DEBTOR'"

        '                'Dim command1 As MySqlCommand = New MySqlCommand
        '                command21.CommandType = CommandType.Text
        '                command21.CommandText = sql
        '                command21.Connection = conn

        '                Dim dr21 As MySqlDataReader = command21.ExecuteReader()

        '                Dim dt21 As New DataTable
        '                dt21.Load(dr21)

        '                If dt21.Rows.Count > 0 Then
        '                    If dt21.Rows(0)("COACode").ToString <> "" Then : txtARCode.Text = dt21.Rows(0)("COACode").ToString : End If
        '                    If dt21.Rows(0)("Description").ToString <> "" Then : txtARDescription.Text = dt21.Rows(0)("Description").ToString : End If
        '                End If

        '                '''''''''''''''''''''''''''''''''''
        '                sql = "Select COACode, Description from tblchartofaccounts where CompanyGroup = '" & txtCompanyGroup.Text & "' and GLType='GST OUTPUT'"

        '                Dim command23 As MySqlCommand = New MySqlCommand
        '                command23.CommandType = CommandType.Text
        '                command23.CommandText = sql
        '                command23.Connection = conn

        '                Dim dr23 As MySqlDataReader = command23.ExecuteReader()

        '                Dim dt23 As New DataTable
        '                dt23.Load(dr23)

        '                If dt23.Rows.Count > 0 Then
        '                    If dt23.Rows(0)("COACode").ToString <> "" Then : txtGSTOutputCode.Text = dt23.Rows(0)("COACode").ToString : End If
        '                    If dt23.Rows(0)("Description").ToString <> "" Then : txtGSTOutputDescription.Text = dt23.Rows(0)("Description").ToString : End If
        '                End If

        '                updPnlBillingRecs.Update()

        '                If conn.State = ConnectionState.Open Then
        '                    conn.Close()
        '                End If
        '                'conn.Close()

        '                Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
        '                Dim drCurrentRowLoc As DataRow = Nothing

        '                'If String.IsNullOrEmpty(txtRcnoServiceRecord.Text) = True Or txtRcnoServiceRecord.Text = "0" Then


        '                '    For j As Integer = 0 To grvBillingDetails.Rows.Count - 1
        '                '        dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
        '                '        drCurrentRowLoc = dtScdrLoc.NewRow()
        '                '        ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
        '                '        grvBillingDetails.DataSource = dtScdrLoc
        '                '        grvBillingDetails.DataBind()

        '                '        SetPreviousDataBillingDetailsRecs()
        '                '    Next j
        '                '    FirstGridViewRowBillingDetailsRecs()
        '                'End If


        '                'Start: Populate the grid
        '                'txtRcnoServiceRecord.Text = lblid14.Text
        '                'txtRcnoServiceRecordDetail.Text = lblid15.Text
        '                'txtContractNo.Text = lblid22.Text
        '                'txtRcnoInvoice.Text = lblid17.Text
        '                'txtRowSelected.Text = rowindex1.ToString


        '                'txtRcnoservicebillingdetail.Text = lblid18.Text
        '                'txtRcnotblServiceBillingDetail.Text = lblid18.Text


        '                If String.IsNullOrEmpty(txtRcnoServiceBillingDetail.Text) = True Then
        '                    txtRcnoServiceBillingDetail.Text = "0"
        '                End If


        '                If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
        '                    txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text
        '                End If
        '                'Start: Billing Details

        '                'Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
        '                'Dim drCurrentRowLoc As DataRow = Nothing

        '                'For i As Integer = 0 To grvBillingDetails.Rows.Count - 1
        '                '    dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
        '                '    drCurrentRowLoc = dtScdrLoc.NewRow()
        '                '    ViewState("CurrentTableBillingDetailsRec") = dtScdrLoc
        '                '    grvBillingDetails.DataSource = dtScdrLoc
        '                '    grvBillingDetails.DataBind()

        '                '    SetPreviousDataBillingDetailsRecs()
        '                'Next i

        '                'Dim Query As String
        '                'Dim Query1 As String

        '                'Dim TextBoxItemCode1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
        '                'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
        '                'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode1)

        '                'Dim TextBoxUOM1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtUOMGV"), DropDownList)
        '                'Query1 = "Select * from tblunitms order by UnitMs"
        '                'PopulateDropDownList(Query1, "UnitMs", "UnitMs", TextBoxUOM1)



        '                'If Convert.ToInt64(txtRcnoServiceBillingDetail.Text) = 0 Then
        '                'If txtMode.Text = "NEW" Then
        '                If Convert.ToInt64(lblid18.Text) = 0 Then
        '                    Dim dt As New DataTable

        '                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '                    If conn.State = ConnectionState.Open Then
        '                        conn.Close()
        '                    End If
        '                    conn.Open()
        '                    'Dim command1 As MySqlCommand = New MySqlCommand
        '                    'command1.CommandType = CommandType.Text

        '                    'If lblid25.Text = "P" Then
        '                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
        '                    'ElseIf lblid25.Text = "O" Then
        '                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
        '                    'End If

        '                    'command1.Connection = conn

        '                    'Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        '                    'Dim dt1 As New DataTable
        '                    'dt1.Load(dr1)
        '                    'AddNewRowBillingDetailsRecs()

        '                    'Dim totrec As Integer
        '                    'totrec = 0

        '                    'If grvBillingDetails.Rows.Count = 1 Then
        '                    '    totrec = 0
        '                    'Else
        '                    '    totrec = grvBillingDetails.Rows.Count
        '                    '    totrec = totrec - 1
        '                    'End If
        '                    AddNewRowBillingDetailsRecs()
        '                    'totrec = grvBillingDetails.Rows.Count - 1
        '                    'AddNewRowBillingDetailsRecs()
        '                    'If (grvBillingDetails.Rows.Count > totrec + 1) Then


        '                    'AddNewRowWithDetailRecBillingDetailsRecs()
        '                    'AddNewRowBillingDetailsRecs()


        '                    Dim command1 As MySqlCommand = New MySqlCommand
        '                    command1.CommandType = CommandType.Text

        '                    'If lblid25.Text = "P" Then
        '                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and ProductCode = 'IN-SRV'"
        '                    'ElseIf lblid25.Text = "O" Then
        '                    '    command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "'and ProductCode = 'IN-DEF'"
        '                    'End If


        '                    If lblid25.Text = "P" Then
        '                        command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
        '                    ElseIf lblid25.Text = "O" Then
        '                        command1.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
        '                    End If
        '                    command1.Connection = conn
        '                    Dim dr1 As MySqlDataReader = command1.ExecuteReader()
        '                    Dim dt1 As New DataTable
        '                    dt1.Load(dr1)

        '                    Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
        '                    TextBoxItemType.Text = "SERVICE"
        '                    TextBoxItemType.Enabled = False

        '                    Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
        '                    TextBoxItemCode.Text = dt1.Rows(0)("ProductCode").ToString()
        '                    TextBoxItemCode.Enabled = False

        '                    Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
        '                    'TextBoxItemDescription.Text = ""
        '                    TextBoxItemDescription.Text = dt1.Rows(0)("Description").ToString()


        '                    Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
        '                    TextBoxOtherCode.Text = dt1.Rows(0)("COACode").ToString()


        '                    Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
        '                    TextBoxGLDescription.Text = dt1.Rows(0)("COADescription").ToString()

        '                    Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
        '                    TextBoxUOM.Text = "--SELECT--"

        '                    Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
        '                    TextBoxQty.Text = "1"
        '                    TextBoxQty.Enabled = False

        '                    TextBoxPriceWithDisc = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPriceWithDiscGV"), TextBox)
        '                    TextBoxPriceWithDisc.Text = lblid13.Text

        '                    Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtPricePerUOMGV"), TextBox)
        '                    TextBoxPricePerUOM.Text = lblid13.Text

        '                    Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTPercGV"), TextBox)
        '                    TextBoxGSTPerc.Text = Convert.ToDecimal(txtTaxRatePct.Text).ToString("N4")

        '                    'Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
        '                    TextBoxGSTAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtGSTAmtGV"), TextBox)
        '                    TextBoxGSTAmt.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(lblid13.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2"))

        '                    TextBoxTotalPrice = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceGV"), TextBox)
        '                    TextBoxTotalPrice.Text = lblid13.Text

        '                    TextBoxTotalPriceWithGST = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
        '                    TextBoxTotalPriceWithGST.Text = Convert.ToString(Convert.ToDecimal(TextBoxPricePerUOM.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text))


        '                    Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtContractNoGV"), TextBox)
        '                    TextBoxContractNo.Text = Convert.ToString(txtContractNo.Text)

        '                    Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceStatusGV"), TextBox)
        '                    TextBoxServiceStatus.Text = Convert.ToString(lblid25.Text)


        '                    Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtDiscPercGV"), TextBox)
        '                    TextBoxDiscPerc.Text = Convert.ToString("0.00")

        '                    TextBoxDiscAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
        '                    TextBoxDiscAmt.Text = Convert.ToString("0.00")

        '                    Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtServiceRecordGV"), TextBox)
        '                    TextBoxServiceRecord.Text = Convert.ToString(lblid15.Text)

        '                    'txtTotal.Text = Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(TextBoxTotalPrice.Text)
        '                    'txtTotalWithGST.Text = Convert.ToDecimal(txtTotalWithGST.Text) + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)

        '                    'txtTotalDiscAmt.Text = (0.0).ToString("N2")
        '                    'txtTotalGSTAmt.Text = (Convert.ToDecimal(txtTotalGSTAmt.Text) + Convert.ToDecimal(TextBoxGSTAmt.Text)).ToString("N2")
        '                    'txtTotalWithDiscAmt.Text = Convert.ToDecimal(txtTotal.Text)

        '                    '''''''''''''''''''''
        '                    'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(totrec).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
        '                    'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
        '                    'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

        '                    'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(totrec).Cells(0).FindControl("txtUOMGV"), DropDownList)
        '                    'Query = "Select * from tblunitms order by UnitMs"
        '                    'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


        '                    Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
        '                    TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
        '                    TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
        '                    txtAmountWithDiscount.Text = Total - TotalDiscAmt
        '                    TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
        '                    TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

        '                    rowIndex = rowIndex + 1
        '                Else

        '                    'Start: From tblBillingDetailItem

        '                    'Dim Total As Decimal
        '                    'Dim TotalWithGST As Decimal
        '                    'Dim TotalDiscAmt As Decimal
        '                    'Dim TotalGSTAmt As Decimal
        '                    'Dim TotalPriceWithDiscountAmt As Decimal


        '                    'Total = 0.0
        '                    'TotalWithGST = 0.0
        '                    'TotalDiscAmt = 0.0
        '                    'TotalGSTAmt = 0.0
        '                    'TotalPriceWithDiscountAmt = 0.0

        '                    'Dim conn As MySqlConnection = New MySqlConnection()

        '                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        '                    If conn.State = ConnectionState.Open Then
        '                        conn.Close()
        '                    End If
        '                    conn.Open()

        '                    Dim cmdServiceBillingDetailItem As MySqlCommand = New MySqlCommand
        '                    cmdServiceBillingDetailItem.CommandType = CommandType.Text
        '                    cmdServiceBillingDetailItem.CommandText = "SELECT * FROM tblservicebillingdetailitem where Rcnoservicebillingdetail=" & Convert.ToInt32(lblid18.Text)
        '                    cmdServiceBillingDetailItem.Connection = conn

        '                    Dim drcmdServiceBillingDetailItem As MySqlDataReader = cmdServiceBillingDetailItem.ExecuteReader()
        '                    Dim dtServiceBillingDetailItem As New DataTable
        '                    dtServiceBillingDetailItem.Load(drcmdServiceBillingDetailItem)

        '                    Dim TotDetailRecordsLoc = dtServiceBillingDetailItem.Rows.Count
        '                    If dtServiceBillingDetailItem.Rows.Count > 0 Then



        '                        Dim rowIndex1 = 0

        '                        For Each row As DataRow In dtServiceBillingDetailItem.Rows
        '                            If (TotDetailRecordsLoc > (rowIndex + 1)) Then
        '                                AddNewRowBillingDetailsRecs()
        '                                'AddNewRow()
        '                            End If

        '                            'If rowIndex = dtServiceBillingDetailItem.Rows.Count Then
        '                            '    AddNewRowBillingDetailsRecs()
        '                            'End If

        '                            If rowIndex = grvBillingDetails.Rows.Count Then
        '                                AddNewRowBillingDetailsRecs()
        '                            End If


        '                            Dim TextBoxItemType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
        '                            TextBoxItemType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemType"))

        '                            Dim TextBoxItemCode As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
        '                            'TextBoxItemCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemCode"))

        '                            TextBoxItemCode.Items.Clear()
        '                            TextBoxItemCode.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemCode")))


        '                            Dim TextBoxItemDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemDescriptionGV"), TextBox)
        '                            TextBoxItemDescription.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("ItemDescription"))

        '                            Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
        '                            TextBoxOtherCode.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("OtherCode"))

        '                            Dim TextBoxUOM As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtUOMGV"), DropDownList)
        '                            'If String.IsNullOrEmpty(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM"))) = True Then

        '                            'Else
        '                            '    TextBoxUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM"))
        '                            'End If

        '                            TextBoxUOM.Items.Clear()
        '                            TextBoxUOM.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("UOM")))

        '                            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
        '                            TextBoxQty.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("Qty"))
        '                            'TextBoxQty.Enabled = False

        '                            Dim TextBoxPricePerUOM As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
        '                            TextBoxPricePerUOM.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("PricePerUOM"))


        '                            Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscPercGV"), TextBox)
        '                            TextBoxDiscPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("DiscPerc"))

        '                            TextBoxDiscAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
        '                            TextBoxDiscAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("DiscAmount"))


        '                            TextBoxPriceWithDisc = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
        '                            TextBoxPriceWithDisc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("PriceWithDisc"))


        '                            Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
        '                            'TextBoxTaxType.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TaxType"))

        '                            TextBoxTaxType.Items.Clear()
        '                            TextBoxTaxType.Items.Add(Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TaxType")))


        '                            Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTPercGV"), TextBox)
        '                            TextBoxGSTPerc.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("GSTPerc"))

        '                            TextBoxGSTAmt = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
        '                            TextBoxGSTAmt.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("GSTAmt"))

        '                            TextBoxTotalPrice = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
        '                            TextBoxTotalPrice.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPrice"))

        '                            TextBoxTotalPriceWithGST = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtTotalPriceWithGSTGV"), TextBox)
        '                            TextBoxTotalPriceWithGST.Text = Convert.ToString(dtServiceBillingDetailItem.Rows(rowIndex1)("TotalPriceWithGST"))


        '                            Dim TextBoxServiceRecord As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(rowIndex).FindControl("txtServiceRecordGV"), TextBox)
        '                            TextBoxServiceRecord.Text = Convert.ToString(lblid15.Text)


        '                            Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
        '                            TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
        '                            TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
        '                            txtAmountWithDiscount.Text = Total - TotalDiscAmt
        '                            TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
        '                            TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

        '                            'Dim Query As String

        '                            'Dim TextBoxItemCode2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemCodeGV"), DropDownList)
        '                            'Query = "Select * from tblbillingproducts  where CompanyGroup = '" & txtCompanyGroup.Text & "'"
        '                            'PopulateDropDownList(Query, "ProductCode", "ProductCode", TextBoxItemCode2)

        '                            'Dim TextBoxUOM2 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtUOMGV"), DropDownList)
        '                            'Query = "Select * from tblunitms order by UnitMs"
        '                            'PopulateDropDownList(Query, "UnitMs", "UnitMs", TextBoxUOM2)


        '                            Dim TextBoxItemType1 As DropDownList = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtItemTypeGV"), DropDownList)
        '                            Dim TextBoxQty1 As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(4).FindControl("txtQtyGV"), TextBox)

        '                            If TextBoxItemType1.Text = "SERVICE" Then
        '                                TextBoxQty1.Enabled = False
        '                                TextBoxQty1.Text = "1.00"
        '                                TextBoxItemType1.Enabled = False
        '                            End If

        '                            rowIndex = rowIndex + 1
        '                            rowIndex1 = rowIndex1 + 1


        '                        Next row

        '                        'txtTotal.Text = Total.ToString("N2")
        '                        'txtTotalWithGST.Text = TotalWithGST.ToString("N2")
        '                        'txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
        '                        'txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")

        '                        'txtTotalDiscAmt.Text = 0.0
        '                        txtTotalWithDiscAmt.Text = TotalPriceWithDiscountAmt
        '                        AddNewRowBillingDetailsRecs()
        '                        'rowIndex = rowIndex + 1
        '                    Else
        '                        FirstGridViewRowBillingDetailsRecs()
        '                        'FirstGridViewRowTarget()
        '                        'Dim Query As String
        '                        'Dim TextBoxTargetDesc As DropDownList = CType(grvBillingDetails.Rows(0).Cells(0).FindControl("ddlTargetDescGV"), DropDownList)
        '                        'Query = "Select * from tblTarget"

        '                        'PopulateDropDownList(Query, "descrip1", "descrip1", TextBoxTargetDesc)
        '                    End If


        '                    'End: Detail Records


        '                    'End: From tblBillingDetailItem
        '                End If

        '                If txtMode.Text <> "NEW" Then
        '                    AddNewRowBillingDetailsRecs()
        '                End If

        '                grvBillingDetails.Enabled = True

        '                btnSave.Enabled = False
        '                If txtPostStatus.Text <> "P" Then
        '                    'btnSave.Enabled = True
        '                    btnSaveInvoice.Enabled = True
        '                End If

        '                updpnlServiceRecs.Update()
        '                updpnlBillingDetails.Update()
        '                'End: Billing Details
        '                updPanelSave.Update()
        '                'End: Populate the grid
        '                updPnlBillingRecs.Update()


        '                '''''''''''''''''''''''''''''''''''''''''''''''''



        '                'If String.IsNullOrEmpty(TextBoxInvoiceAmount.Text) = True Then
        '                '    TextBoxInvoiceAmount.Text = "0.00"
        '                'End If

        '                'If String.IsNullOrEmpty(TextBoxDiscountAmount.Text) = True Then
        '                '    TextBoxDiscountAmount.Text = "0.00"
        '                'End If

        '                'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
        '                '    TextBoxGSTAmount.Text = "0.00"
        '                'End If

        '                'If String.IsNullOrEmpty(TextBoxNetAmount.Text) = True Then
        '                '    TextBoxNetAmount.Text = "0.00"
        '                'End If

        '                'TotalInvoiceAmount = TotalInvoiceAmount + Convert.ToDecimal(TextBoxInvoiceAmount.Text)
        '                'TotalDiscountAmount = TotalDiscountAmount + Convert.ToDecimal(TextBoxDiscountAmount.Text)

        '                'TotalGSTAmount = TotalGSTAmount + Convert.ToDecimal(TextBoxGSTAmount.Text)
        '                'TotalNetAmount = TotalNetAmount + Convert.ToDecimal(TextBoxNetAmount.Text)


        '                'TextBoxGSTAmt.Text = "0.00"
        '                'TextBoxTotalPrice.Text = "0.00"
        '                'TextBoxTotalPriceWithGST.Text = "0.00"
        '                'TextBoxDiscAmt.Text = "0.00"
        '                'TextBoxPriceWithDisc.Text = "0.00"

        '                'Total = Total + Convert.ToDecimal(TextBoxTotalPrice.Text)
        '                'TotalWithGST = TotalWithGST + Convert.ToDecimal(TextBoxTotalPriceWithGST.Text)
        '                'TotalDiscAmt = TotalDiscAmt + Convert.ToDecimal(TextBoxDiscAmt.Text)
        '                ''txtAmountWithDiscount.Text =  Total - TotalDiscAmt
        '                'TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGSTAmt.Text)
        '                'TotalPriceWithDiscountAmt = TotalPriceWithDiscountAmt + Convert.ToDecimal(TextBoxPriceWithDisc.Text)

        '                'rowIndex = rowIndex + 1
        '            End If
        '        Next i

        '        txtTotal.Text = Total.ToString("N2")
        '        txtTotalDiscAmt.Text = TotalDiscAmt.ToString("N2")
        '        txtTotalWithDiscAmt.Text = (Total - TotalDiscAmt).ToString("N2")
        '        txtTotalGSTAmt.Text = TotalGSTAmt.ToString("N2")
        '        txtTotalWithGST.Text = TotalWithGST.ToString("N2")

        '        grvBillingDetails.Enabled = False
        '    End If
        'Catch ex As Exception
        '    Dim exstr As String
        '    exstr = ex.ToString
        '    lblAlert.Text = exstr
        '    'MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
    End Sub

    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(grvServiceRecDetails.HeaderRow.Cells(0).FindControl("CheckBox1"), CheckBox)
        For Each row As GridViewRow In grvServiceRecDetails.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelectGV"), CheckBox)
            chk.Checked = chk1.Checked
        Next

        If chk1.Checked = True Then
            btnSaveInvoice.Enabled = True
            btnSaveInvoice.ForeColor = System.Drawing.Color.Black
            updPanelInvoice.Update()
        Else
            btnSaveInvoice.Enabled = False
            btnSaveInvoice.ForeColor = System.Drawing.Color.Gray
            updPanelInvoice.Update()
        End If
    End Sub

    Protected Sub txtPopUpClient_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpClient.TextChanged
        Try
            If txtPopUpClient.Text.Trim = "" Then
                MessageBox.Message.Alert(Page, "Please enter client name", "str")
            Else


                If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like '%" + txtPopUpClient.Text.Trim + "%' or A.ID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                    SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where 1=1 and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or C.accountid like '%" + txtPopUpClient.Text.Trim + "%' or C.ID like '%" + txtPopUpClient.Text + "%' or D.BillContact1Svc like '%" + txtPopUpClient.Text.Trim + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                Else
                    SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile,  B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where 1=1 and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopUpClient.Text + "%' or A.ID like '%" + txtPopUpClient.Text + "%' or B.BillContact1Svc like '%" + txtPopUpClient.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where 1=1 and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopUpClient.Text + "%' or C.ID like '%" + txtPopUpClient.Text + "%' or D.BillContact1Svc like '%" + txtPopUpClient.Text + "%') order by AccountID,  LocationId, ServiceName"
                End If
                txtCustomerSearch.Text = SqlDSClient.SelectCommand
                SqlDSClient.DataBind()
                gvClient.DataBind()
                mdlPopUpClient.Show()
                txtIsPopup.Text = "Client"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "txtPopUpClient_TextChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
        'txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
    End Sub

    Protected Sub btnPopUpClientReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpClientReset.Click
        txtPopUpClient.Text = "Search Here for AccountID or Client Name or Contact Person"
        'SqlDSClient.SelectCommand = "SELECT distinct * From tblContactMaster where 1=1 and ContName like '" + ViewState("ClientCurrentAlphabet") + "%' and (Upper(ContType) = '" + txtContType1.Text.ToString + "' or Upper(ContType) = '" + txtContType2.Text.ToString + "'  or Upper(ContType) = '" + txtContType3.Text.ToString + "' or Upper(ContType) = '" + txtContType4.Text.ToString + "')"

        If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
            SqlDSClient.SelectCommand = "SELECT 'COMPANY', AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status ='O' order by name"
        ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
            SqlDSClient.SelectCommand = "SELECT 'PERSON', AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status ='O' order by name"
        End If
        SqlDSClient.DataBind()
        gvClient.DataBind()
        mdlPopUpClient.Show()
        txtIsPopup.Text = "Client"
    End Sub


    Protected Sub btnClient_Click(sender As Object, e As ImageClickEventArgs) Handles btnClient.Click
        lblAlert.Text = ""
        txtSearch.Text = ""
        txtSearch.Text = "CustomerSearch"
        Try


            If String.IsNullOrEmpty(txtAccountId.Text.Trim) = False Then
                txtPopUpClient.Text = txtAccountId.Text
                txtPopupClientSearch.Text = txtPopUpClient.Text


                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                End If
               

                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelInvoice.Update()
            Else

                If txtDisplayRecordsLocationwise.Text = "Y" Then
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and  B.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and  D.Location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "') and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                Else
                    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType, A.CompanyGroup, B.SalesmanSvc, B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, B.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where A.Inactive = False and   (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') order by B.AccountID,  B.LocationId, B.ServiceName"
                    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
                        SqlDSClient.SelectCommand = "SELECT 'PERSON' as AccountType,  C.PersonGroup as CompanyGroup, D.SalesmanSvc, D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where  C.Inactive = False and  (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '')  and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by D.AccountID,  D.LocationId, D.ServiceName"
                    Else
                        SqlDSClient.SelectCommand = "SELECT 'COMPANY' as AccountType,  A.CompanyGroup, B.SalesmanSvc,B.ARTermSvc, B.BillStreetSvc, B.BillBuildingSvc, B.BillCitySvc, B.BillStateSvc, B.BillCountrySvc, B.BillPostalSvc, B.BillContact1Svc, B.AccountID, A.ID, B.LocationId, B.ServiceName, B.BillingNameSvc, B.Address1, A.AddPostal, B.BillAddressSvc, B.BillPostalSvc, B.ContactPerson, B.telephone, B.Mobile, B.CompanyGroupD From tblCompany A Left join tblCompanyLocation B on A.Accountid = B.Accountid  where  A.Inactive = False and (A.Accountid is not null and A.Accountid <> '') and  (B.Accountid is not null and B.Accountid <> '') and (upper(B.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or B.accountid like '" + txtPopupClientSearch.Text + "%' or B.contactperson like '%" + txtPopupClientSearch.Text + "%') UNION  SELECT 'PERSON' as AccountType,   C.PersonGroup as CompanyGroup, D.SalesmanSvc,D.ARTermSvc, D.BillStreetSvc, D.BillBuildingSvc, D.BillCitySvc, D.BillStateSvc, D.BillCountrySvc, D.BillPostalSvc, D.BillContact1Svc, D.AccountID, C.ID, D.LocationId, D.ServiceName, D.BillingNameSvc, D.Address1, D.AddPostal, D.BillAddressSvc, D.BillPostalSvc, D.ContactPerson, D.telephone, D.Mobile, D.PersonGroupD as CompanyGroupD From tblperson C Left join tblPersonLocation D on C.Accountid = D.Accountid  where   C.Inactive = False and (C.Accountid is not null and C.Accountid <> '') and  (C.Accountid is not null and C.Accountid <> '') and (upper(D.ServiceName) Like ""%" + txtPopupClientSearch.Text.Trim.ToUpper + "%"" or D.accountid like '" + txtPopupClientSearch.Text + "%' or D.contACTperson like '%" + txtPopupClientSearch.Text + "%') order by AccountID,  LocationId, ServiceName"
                    End If
                End If
               

                SqlDSClient.DataBind()
                gvClient.DataBind()
                updPanelInvoice.Update()
            End If

            txtCustomerSearch.Text = SqlDSClient.SelectCommand
            mdlPopUpClient.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "btnClient_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub BtnLocation_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLocation.Click
        mdlPopupLocation.Show()
    End Sub


    Public Function FindTerms(ByVal strAccountId As String, strAccountType As String) As String

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()

        Dim commandDocControl As MySqlCommand = New MySqlCommand
        commandDocControl.CommandType = CommandType.Text
        If strAccountType = "COMPANY" Then
            commandDocControl.CommandText = "SELECT ArTerm, ContactPerson FROM tblCompany where AccountId='" & strAccountId & "'"
        Else
            commandDocControl.CommandText = "SELECT ArTerm, ContactPerson FROM tblPerson where AccountId='" & strAccountId & "'"
        End If

        commandDocControl.Connection = conn

        Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)
        gContactPerson = ""



        If dt.Rows.Count > 0 Then
            FindTerms = dt.Rows(0)("ArTerm").ToString
            gContactPerson = dt.Rows(0)("ContactPerson").ToString
        End If

        conn.Close()
        conn.Dispose()
        commandDocControl.Dispose()
        dt.Dispose()
        dr.Close()

        'Return FindTerms
    End Function


    Public Function FindCreditDays(ByVal strTerms As String) As String

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()

        Dim commandDocControl As MySqlCommand = New MySqlCommand
        commandDocControl.CommandType = CommandType.Text
        commandDocControl.CommandText = "SELECT TermsDay FROM tblterms where Terms ='" & strTerms & "'"
        commandDocControl.Connection = conn

        Dim dr As MySqlDataReader = commandDocControl.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            FindCreditDays = dt.Rows(0)("TermsDay").ToString
        End If

        conn.Close()
        conn.Dispose()
        commandDocControl.Dispose()
        dt.Dispose()
        dr.Close()

        'Return FindTerms
    End Function


    Protected Sub btnGenerateInvoice_Click(sender As Object, e As EventArgs) Handles btnGenerateInvoice.Click
        lblAlert.Text = ""
        Try
            If String.IsNullOrEmpty(txtInvoiceDate.Text) = True Then
                lblAlert.Text = "PLEASE ENTER INVOICE DATE"
                Exit Sub
            End If

            Dim IsLock = FindARPeriod(txtBillingPeriod.Text)
            If IsLock = "Y" Then
                lblAlert.Text = "PERIOD IS LOCKED"
                updPnlMsg.Update()
                txtInvoiceDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If

            PostBatchSchedule()

            
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "btnGenerateInvoice_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub btnADDRecurring_Click(sender As Object, e As EventArgs) Handles btnADDRecurring.Click
        Try
            'txtInvoiceDate.Text = ""



            'If chkRecurringInvoice.Checked = True Then
            '    txtAccountId.Text = txtAccountIdBilling.Text
            '    UpdatePanel1.Update()
            '    btnShowRecords_Click(sender, e)
            'End If
            'MakeMeNullBillingDetails()


            '''''''''''''''''''''''''''''''' Recurring Invoice

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim sql As String
            sql = ""
            If txtMode.Text = "View" Then
                sql = "Select * from tblservicebillschedule where RecurringInvoice = 'Y' and RecurringScheduled = 'N' and NextInvoiceDate = '" & Convert.ToDateTime(txtBatchDate.Text).ToString("yyyy-MM-dd") & "'"
            ElseIf txtMode.Text = "EDIT" Then
                sql = "Select * from tblservicebillschedule where RecurringInvoice = 'Y' and RecurringScheduled = 'N' and BillSchedule = '" & txtInvoiceNo.Text & "'"
            End If

            Dim command1 As MySqlCommand = New MySqlCommand
            command1.CommandType = CommandType.Text
            command1.CommandText = sql
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(dr)

            MakeMeNull()
            MakeMeNullBillingDetails()
            EnableControls()

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("RecurringServiceStartDate").ToString <> "" Then : txtDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceStartDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("RecurringServiceEndDate").ToString <> "" Then : txtDateTo.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceEndDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("BillSchedule").ToString <> "" Then : txtPreviousBillSchedule.Text = (dt.Rows(0)("BillSchedule")).ToString : End If

                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountId.Text = (dt.Rows(0)("AccountId")).ToString : End If


                If dt.Rows(0)("GroupByStatus").ToString = "P" Then
                    rdbCompleted.Checked = True
                ElseIf dt.Rows(0)("GroupByStatus").ToString = "O" Then
                    rdbNotCompleted.Checked = True
                ElseIf dt.Rows(0)("GroupByStatus").ToString = "A" Then
                    rdbAll.Checked = True
                End If
                If dt.Rows(0)("GroupByBillingFrequency").ToString <> "" Then : ddlBillingFrequency.Text = (dt.Rows(0)("GroupByBillingFrequency")).ToString : End If
                If dt.Rows(0)("GroupContractNo").ToString <> "" Then : ddlContractNo.Text = ((dt.Rows(0)("GroupContractNo")).ToString) : End If
                If dt.Rows(0)("GroupLocationID").ToString <> "" Then : txtLocationId.Text = (dt.Rows(0)("GroupLocationID")).ToString : End If
                If dt.Rows(0)("GroupContractGroup").ToString <> "" Then : ddlContractGroup.Text = (dt.Rows(0)("GroupContractGroup")).ToString : End If

            End If

            PopulateServiceGridForRecurringInvoice()

            '''''''''''''''''''''''''''''''' Recurring Invoice



            txtMode.Text = "NEW"
            lblMessage.Text = "ACTION: ADD RECORD"
            txtBillingPeriod.Text = Year(Convert.ToDateTime(txtBatchDate.Text)) & Format(Month(Convert.ToDateTime(txtBatchDate.Text)), "00")

            If txtPostStatus.Text = "O" Then
                btnSaveInvoice.Enabled = True
            End If

        Catch ex As Exception
            lblAlert.Text = ex.Message
            'MsgBox("Error" & ex.Message)
        End Try
    End Sub

    Protected Sub btnReceipts_Click(sender As Object, e As EventArgs)

        mdlPopupLocation.Hide()
        '''''''''''''''''''''''''''''''' Recurring Invoice

        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()



        Dim sql2 As String
        sql2 = ""
        sql2 = "Select count(*) as totrecerring from tblservicebillschedule where RecurringInvoice = 'Y' and NextInvoiceDate = '" & Convert.ToDateTime(txtCreatedOn.Text).ToString("yyyy-MM-dd") & "'"


        Dim command2 As MySqlCommand = New MySqlCommand
        command2.CommandType = CommandType.Text
        command2.CommandText = sql2
        command2.Connection = conn

        Dim dr2 As MySqlDataReader = command2.ExecuteReader()

        Dim dt2 As New DataTable
        dt2.Load(dr2)

        If dt2.Rows.Count > 0 Then
            btnADDRecurring.Text = "CREATE RECURRING [" & dt2.Rows(0)("totrecerring").ToString & "]"
        End If



        ' '''''''''''''''''''''''''''''''' Recurring Invoice
    End Sub


    Protected Sub gvClient_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        'If txtPopUpClient.Text.Trim = "Search Here for AccountID or Client Name or Contact Person" Then
        '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where status ='O' order by name"
        '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where status ='O' order by name"
        '    End If
        'Else
        '    If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, Mobile, Email,  LocateGRP, CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, Fax, Mobile, Telephone, Salesman From tblCompany where 1=1 and status ='O' and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contactperson like '%" + txtPopUpClient.Text + "%') order by name"
        '    ElseIf ddlContactType.SelectedItem.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then
        '        SqlDSClient.SelectCommand = "SELECT AccountID, ID, Name, ContactPerson, Address1, TelMobile as Mobile, Email,  LocateGRP, PersonGroup as CompanyGroup, AddBlock, AddNos, AddFloor, AddUnit, AddStreet, AddBuilding, AddCity, AddState, AddCountry, AddPostal, TelFax as Fax, TelMobile as Mobile, TelHome as Telephone, Salesman From tblPERSON where 1=1 and status ='O'and (upper(Name) Like ""%" + txtPopUpClient.Text.Trim.ToUpper + "%"" or accountid like '" + txtPopUpClient.Text + "%' or contACTperson like '%" + txtPopUpClient.Text + "%') order by name"
        '    End If

        '    'SqlDSClient.DataBind()
        '    'gvClient.DataBind()
        '    'mdlPopUpClient.Show()
        'End If

        If txtSearch.Text = "ImportService" Then
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

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
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

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
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

    Protected Sub btnPopUpStaffReset_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffReset.Click
        txtPopUpStaff.Text = ""
        txtPopupStaffSearch.Text = ""
        SqlDSStaffID.SelectCommand = "SELECT distinct * From tblstaff order by staffid"

        SqlDSStaffID.DataBind()

        gvStaff.DataBind()
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
            txtIsPopup.Text = "Staff"
        End If

        'txtPopUpStaff.Text = "Search Here"
    End Sub

    Protected Sub btnPopUpStaffSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnPopUpStaffSearch.Click
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

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If String.IsNullOrEmpty(txtServiceBySearch.Text.Trim) = False Then
                txtPopUpStaff.Text = ""
                txtPopUpStaff.Text = txtServiceBySearch.Text
                txtPopupStaffSearch.Text = txtPopUpStaff.Text

                SqlDSStaffID.SelectCommand = "SELECT  * From tblstaff where staffid like '%" + txtPopupStaffSearch.Text.ToUpper + "%' or name like '%" + txtPopupStaffSearch.Text + "%' order by staffid"

                SqlDSStaffID.DataBind()
                gvStaff.DataBind()
                updPanelInvoice.Update()
            Else
                txtPopUpStaff.Text = ""

                SqlDSStaffID.SelectCommand = "SELECT  * From tblstaff order by staffid"

                SqlDSStaffID.DataBind()
                gvStaff.DataBind()
                updPanelInvoice.Update()
            End If
            txtIsPopup.Text = "Staff"
            mdlPopupStaff.Show()
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "ImageButton1_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub gvStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStaff.SelectedIndexChanged
        Try
            'If txtModal.Text = "EditIncharge" Then
            '    If (gvStaff.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '        txtEditIncharge.Text = ""
            '    Else
            '        txtEditIncharge.Text = gvStaff.SelectedRow.Cells(1).Text.Trim
            '    End If
            '    mdlPopupEditTeam.Show()

            'End If

            'If txtModal.Text = "EditSvcBy" Then
            If (gvStaff.SelectedRow.Cells(1).Text = "&nbsp;") Then
                txtServiceBySearch.Text = ""
            Else
                txtServiceBySearch.Text = gvStaff.SelectedRow.Cells(1).Text.Trim
            End If
            'mdlImportServices.Show()

            'End If

            'If txtModal.Text = "EditSupervisor" Then
            '    If (gvStaff.SelectedRow.Cells(1).Text = "&nbsp;") Then
            '        txtEditSupervisor.Text = ""
            '    Else
            '        txtEditSupervisor.Text = gvStaff.SelectedRow.Cells(1).Text.Trim
            '    End If
            '    mdlPopupEditTeam.Show()

            'End If
            'If txtModal.Text = "SvcBySearch" Then
            '    If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '        txtSearch1SvcBy.Text = " "
            '    Else
            '        txtSearch1SvcBy.Text = gvStaff.SelectedRow.Cells(1).Text
            '    End If
            'End If
            'If txtModal.Text = "InchargeSearch" Then
            '    If gvStaff.SelectedRow.Cells(1).Text = "&nbsp;" Then
            '        txtSearch1Incharge.Text = " "
            '    Else
            '        txtSearch1Incharge.Text = gvStaff.SelectedRow.Cells(1).Text
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Message.Alert(Page, ex.ToString, "str")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Protected Sub txtDateFrom_TextChanged(sender As Object, e As EventArgs) Handles txtDateFrom.TextChanged

    End Sub

    Private Sub FindGSTPct()
        Try
            Dim sql As String

            sql = ""

            sql = "Select GSTRate from tblperiod where GStType = '" & txtDefaultTaxCode.Text & "' and AccountingPeriod = '" & txtBillingPeriod.Text & "'"

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim commandGSTPct As MySqlCommand = New MySqlCommand
            commandGSTPct.CommandType = CommandType.Text
            commandGSTPct.CommandText = sql
            commandGSTPct.Connection = conn

            Dim drGSTPct As MySqlDataReader = commandGSTPct.ExecuteReader()

            Dim dtGSTPct As New DataTable
            dtGSTPct.Load(drGSTPct)

            If dtGSTPct.Rows.Count > 0 Then
                If dtGSTPct.Rows(0)("GSTRate").ToString <> "" Then : txtTaxRatePct.Text = dtGSTPct.Rows(0)("GSTRate").ToString : End If
                'txtGST1.Text = txtTaxRatePct.Text
                UpdatePanel1.Update()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "FindGSTPct", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            Exit Sub
        End Try
    End Sub

    Protected Sub txtInvoiceDate_TextChanged1(sender As Object, e As EventArgs) Handles txtInvoiceDate.TextChanged
        txtBillingPeriod.Text = Year(Convert.ToDateTime(txtInvoiceDate.Text)) & Format(Month(Convert.ToDateTime(txtInvoiceDate.Text)), "00")

        FindGSTPct()
    End Sub

    Public Function FindARPeriod(BillingPeriod As String) As String
        Dim IsLock As String
        IsLock = "Y"

        Dim connPeriod As MySqlConnection = New MySqlConnection()

        connPeriod.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        connPeriod.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        If txtMode.Text = "NEW" Then
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command1.CommandText = "SELECT ARLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            Else
                command1.CommandText = "SELECT ARLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            End If

        Else
            If txtDisplayRecordsLocationwise.Text = "Y" Then
                command1.CommandText = "SELECT ARLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "' and Location ='" & txtLocation.Text & "'"
            Else
                command1.CommandText = "SELECT ARLocke FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
            End If

        End If

        'command1.CommandText = "SELECT ARLock FROM tblperiod where CalendarPeriod='" & BillingPeriod & "'"
        command1.Connection = connPeriod

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If txtMode.Text = "NEW" Then
                IsLock = dt.Rows(0)("ARLock").ToString
            Else
                IsLock = dt.Rows(0)("ARLocke").ToString
            End If

        End If

        'If dt.Rows.Count > 0 Then
        '    IsLock = dt.Rows(0)("ARLock").ToString
        'End If

        connPeriod.Close()
        connPeriod.Dispose()
        command1.Dispose()
        dt.Dispose()
        dr.Close()
        Return IsLock
    End Function


    Private Function PostBatchSchedule() As Boolean
        lblAlert.Text = ""

        Dim conn As MySqlConnection = New MySqlConnection()
        ' ''''''''''''''''''''''''''''''''
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim IsSuccess As Boolean = False
        Dim IsServiceRecords As Integer
        Dim lStatus As String
        Dim commandIsExists As MySqlCommand = New MySqlCommand

        commandIsExists.CommandType = CommandType.StoredProcedure
        commandIsExists.CommandText = "IsServiceRecordBilledBatch"
        'commandIsExists.Connection = conn
        commandIsExists.Parameters.Clear()

        commandIsExists.Parameters.AddWithValue("@pr_Source", "BATCH")
        commandIsExists.Parameters.AddWithValue("@pr_BillSchedule", txtInvoiceNo.Text.Trim)
        commandIsExists.Parameters.Add(New MySqlParameter("@pr_status", MySqlDbType.String))
        commandIsExists.Parameters("@pr_status").Direction = ParameterDirection.Output

        commandIsExists.Parameters.Add(New MySqlParameter("@pr_count", MySqlDbType.Int64))
        commandIsExists.Parameters("@pr_count").Direction = ParameterDirection.Output

        commandIsExists.Connection = conn
        commandIsExists.ExecuteScalar()

        IsServiceRecords = commandIsExists.Parameters("@pr_count").Value
        lStatus = commandIsExists.Parameters("@pr_status").Value

        If (Right(lStatus.Trim, 1)) = "," Then
            lStatus = Left(lStatus.Trim, Len(lStatus.Trim) - 1)
        End If

        commandIsExists.Dispose()
        conn.Close()
        conn.Dispose()

        If IsServiceRecords > 0 Then
            lblAlert.Text = " THERE IS NO BALANCE AMOUNT TO BE BILLED FOR " & IsServiceRecords & " SERVICE RECORD (S): " & lStatus & " OF THIS BATCH NUMBER .. CANNOT BE POSTED "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            'Exit Function
            updPnlMsg.Update()
            btnSaveInvoice.Enabled = True
            IsSuccess = False
            Return IsSuccess
        End If


        '''''''''''''''''''''''''''''''
        ''Start: from Table
        Dim strRecordNo As String
        Dim strLocationId1 As String
        Dim strServiceLocationGroup1 As String
        Dim strBillAmount As String
        Dim strStatus As String
        Dim strGSTAmount As Decimal
        Dim strTotalWithGST As Decimal
        Dim strNetAmount As Decimal
        Dim strServiceDate As String
        Dim strServiceBy As String
        Dim strBillingFrequency As String
        Dim strRcnoServiceRecord As String
        Dim strContractGroupDept As String
        Dim strLocation As String

        '07.04.24
        Dim strGSTNew As String
        Dim strGSTRateNew As String
        '07.04.24


        Dim lBillingNameSvc, lBillAddressSvc, lBillBuildingSvc, lBillStreetSvc, lBillCountrySvc, lBillCitySvc, lBillPOstalSvc, lContact1Svc, lArTermSvc, lSalesmanSvc, lBillStateSvc As String

        strRecordNo = ""
        strLocationId1 = ""
        strServiceLocationGroup1 = ""
        strStatus = ""
        strGSTAmount = 0.0
        strTotalWithGST = 0.0
        strNetAmount = 0.0
        strServiceBy = ""
        strBillingFrequency = ""
        strRcnoServiceRecord = 0
        strContractGroupDept = ""
        strLocation = ""

        '07.04.24
        strGSTNew = ""
        strGSTRateNew = 0.0
        '07.04.24

        lBillingNameSvc = ""
        lBillAddressSvc = ""
        lBillBuildingSvc = ""
        lBillStreetSvc = ""
        lBillCountrySvc = ""
        lBillCitySvc = ""
        lBillPOstalSvc = ""
        lArTermSvc = ""
        lSalesmanSvc = ""
        lContact1Svc = ""
        lBillStateSvc = ""

        Dim sql1 As String
        Dim sql2 As String
        Dim sql3 As String

        Dim qrySales As String
        qrySales = ""
        Dim ContractGroup As String
        Dim LocationGroup As String
        Dim AccountIdGroup As String
        Dim ServiceLocationGroup As String

        '07.04.24
        Dim ContractGroupGroup As String
        '07.04.24

        Dim strGroupingBy As String
        strGroupingBy = ""

        ContractGroup = ""
        LocationGroup = ""
        AccountIdGroup = ""
        ServiceLocationGroup = ""

        '07.04.24
        ContractGroupGroup = ""
        '07.04.24

        sql1 = ""
        sql2 = ""
        sql3 = ""

        Dim ToBillAmt As Decimal
        Dim DiscAmount As Decimal
        Dim GSTAmount As Decimal
        Dim NetAmount As Decimal
        'Dim ToBillAmt As Decimal

        ToBillAmt = 0.0
        DiscAmount = 0.0
        GSTAmount = 0.0
        NetAmount = 0.0

        'Dim ToBillAmtTot As Decimal
        'Dim DiscAmountTot As Decimal
        'Dim GSTAmountTot As Decimal
        'Dim NetAmountTot As Decimal

        Dim qryInsertTblServicebillingdetail As String
        Dim qryDeleteTblServicebillingdetail As String

        Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand
        Dim drcommandTbwservicebillingdetail As MySqlDataReader
        'Dim dtTbwservicebillingdetail As New DataTable

        Dim gstHeader As Decimal = 0.0
        txtLastSalesDetailRcno.Text = "0"

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        qryDeleteTblServicebillingdetail = "DELETE from tbwservicebillingdetail  "
        qryDeleteTblServicebillingdetail = qryDeleteTblServicebillingdetail + "  where BillSchedule = '" & txtInvoiceNo.Text & "'"

        Dim commandDeleteTblServicebillingdetail As MySqlCommand = New MySqlCommand
        commandDeleteTblServicebillingdetail.CommandType = CommandType.Text
        commandDeleteTblServicebillingdetail.CommandText = qryDeleteTblServicebillingdetail
        commandDeleteTblServicebillingdetail.Connection = conn

        Dim drDeleteTblServicebillingdetail As MySqlDataReader = commandDeleteTblServicebillingdetail.ExecuteReader()

        drDeleteTblServicebillingdetail.Close()

        qryInsertTblServicebillingdetail = "INSERT INTO tbwservicebillingdetail Select * from tblservicebillingdetail  "
        qryInsertTblServicebillingdetail = qryInsertTblServicebillingdetail + "  where BillSchedule = '" & txtInvoiceNo.Text & "'"

        Dim commandInsertTblServicebillingdetail As MySqlCommand = New MySqlCommand
        commandInsertTblServicebillingdetail.CommandType = CommandType.Text
        commandInsertTblServicebillingdetail.CommandText = qryInsertTblServicebillingdetail
        commandInsertTblServicebillingdetail.Connection = conn

        'Dim command3 As MySqlCommand = New MySqlCommand

        '''''''''''''''''''''
        ''''''''tblSales

        Dim commandtblSalesDetail As MySqlCommand = New MySqlCommand

        commandtblSalesDetail.CommandType = CommandType.Text
        'Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
        Dim qrycommandtblSalesDetail As String = "DELETE from tblSalesdetail where invoiceNumber = '" & txtInvoiceNo.Text & "'"

        commandtblSalesDetail.CommandText = qrycommandtblSalesDetail
        commandtblSalesDetail.Parameters.Clear()
        commandtblSalesDetail.Connection = conn
        commandtblSalesDetail.ExecuteNonQuery()

        '''''''''''''''''''''''

        Dim drInsertTblServicebillingdetail As MySqlDataReader = commandInsertTblServicebillingdetail.ExecuteReader()

        drInsertTblServicebillingdetail.Close()

        sql1 = "Select distinct(Accountid), ContactType  "
        sql1 = sql1 + " FROM tblservicebillingdetail A "
        sql1 = sql1 + " where 1 = 1 "
        sql1 = sql1 + " and BillSchedule = '" & txtInvoiceNo.Text & "'"
        sql1 = sql1 + " order by AccountId"

        Dim command1 As MySqlCommand = New MySqlCommand
        command1.CommandType = CommandType.Text
        command1.CommandText = sql1
        command1.Connection = conn

        Dim command3 As MySqlCommand = New MySqlCommand

        Dim dr1 As MySqlDataReader = command1.ExecuteReader()

        Dim dt1 As New DataTable
        dt1.Load(dr1)

        If dt1.Rows.Count > 0 Then

            Dim strAccountId As String
            Dim strContactType As String

            Dim strLocationIdSelected As String

            strAccountId = ""
            strContactType = ""
            strLocationIdSelected = ""
            'strContractNo = ""

            Dim strTIN As String = ""
            Dim strSST As String = ""

            Dim rowIndex = 0

            For Each row As DataRow In dt1.Rows
                strAccountId = Convert.ToString(dt1.Rows(rowIndex)("AccountId"))
              
                strContactType = Convert.ToString(dt1.Rows(rowIndex)("ContactType"))

                If strContactType = "COMPANY" Then
                    sql2 = "Select BillingSettings,TaxIdentificationNo,SalesTaxRegistrationNo  "
                    sql2 = sql2 + " FROM tblCompany A "
                    sql2 = sql2 + " where 1 = 1 "
                    sql2 = sql2 + " and AccountID = '" & strAccountId & "'"
                Else
                    sql2 = "Select BillingSettings,TaxIdentificationNo,SalesTaxRegistrationNo  "
                    sql2 = sql2 + " FROM tblPerson A "
                    sql2 = sql2 + " where 1 = 1 "
                    sql2 = sql2 + " and AccountID = '" & strAccountId & "'"
                End If



                'If strContactType = "COMPANY" Then
                '    sql3 = "Select BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc  "
                '    sql3 = sql3 + " FROM tblCompanyLocation A "
                '    sql3 = sql3 + " where 1 = 1 "
                '    sql3 = sql3 + " and AccountID = '" & strAccountId & "' and LocationID = '" & strLocationIdSelected & "'"
                'Else
                '    sql3 = "Select BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc  "
                '    sql3 = sql3 + " FROM tblPersonLocation A "
                '    sql3 = sql3 + " where 1 = 1 "
                '    sql3 = sql3 + " and AccountID = '" & strAccountId & "' and LocationID = '" & strLocationIdSelected & "'"
                'End If

                Dim command2 As MySqlCommand = New MySqlCommand
                command2.CommandType = CommandType.Text
                command2.CommandText = sql2
                command2.Connection = conn

                Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                Dim dt2 As New DataTable
                dt2.Load(dr2)


                If dt2.Rows.Count > 0 Then

                    strGroupingBy = Convert.ToString(dt2.Rows(0)("BillingSettings"))
                    strTIN = Convert.ToString(dt2.Rows(0)("TaxIdentificationNo"))
                    strSST = Convert.ToString(dt2.Rows(0)("SalesTaxRegistrationNo"))

                    If String.IsNullOrEmpty(strGroupingBy) = True Then
                        strGroupingBy = "AccountID"
                    End If
                    '''''''''''''''''''''''''''

                    ''''''''''''''' taking Bill Schedule Grouping instaed of Customer Bill Settings
                    If rdbGrouping.SelectedIndex = 3 Then
                        strGroupingBy = "ContractNo"
                    ElseIf rdbGrouping.SelectedIndex = 1 Then
                        strGroupingBy = "LocationID"
                    ElseIf rdbGrouping.SelectedIndex = 0 Then
                        strGroupingBy = "AccountID"
                    ElseIf rdbGrouping.SelectedIndex = 2 Then
                        strGroupingBy = "ServiceLocationCode"
                    End If

                    ''''''''''''''' taking Bill Schedule Grouping instaed of Customer Bill Settings

                    Dim sqltbwservicebillingdetail As String

                    If strGroupingBy = "ContractNo" Then

                        sqltbwservicebillingdetail = "Select *   "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " FROM tbwservicebillingdetail A "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " where 1 = 1 "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " and BillSchedule = '" & txtInvoiceNo.Text & "' and AccountId = '" & strAccountId & "'"
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " Order by AccountID, ContractNo, ServiceDate"

                        'Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand
                        commandTbwservicebillingdetail.CommandType = CommandType.Text
                        commandTbwservicebillingdetail.CommandText = sqltbwservicebillingdetail
                        commandTbwservicebillingdetail.Connection = conn

                        'Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand

                        drcommandTbwservicebillingdetail = commandTbwservicebillingdetail.ExecuteReader()

                        Dim dtTbwservicebillingdetailContractNo As New DataTable
                        dtTbwservicebillingdetailContractNo.Load(drcommandTbwservicebillingdetail)

                        'Dim TotRec = dt1.Rows.Count
                        If dtTbwservicebillingdetailContractNo.Rows.Count > 0 Then

                            'Dim strAccountId As String
                            'Dim strContactType As String

                            Dim strLocationId As String
                            Dim strContractNo As String

                            strAccountId = ""
                            strContactType = ""
                            strLocationId = ""
                            strContractNo = ""

                            '07.04.24
                            Dim strContractGroup As String
                            strContractGroup = ""
                            '07.04.24

                            Dim rowIndex1 = 0

                            '''''''''''''''''''''''''''''''

                            For Each row1 As DataRow In dtTbwservicebillingdetailContractNo.Rows

                                strLocationId = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("LocationId"))
                                strContractNo = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContractNo"))
                                'strContactType = Convert.ToString(dt1.Rows(rowIndex)("ContactType"))

                                strContractGroup = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContractGroup")) '07.04.24

                                strRecordNo = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("RecordNo"))
                                strLocationId1 = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("LocationId"))
                                strServiceLocationGroup1 = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ServiceLocationGroup"))
                                strBillAmount = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount"))
                                strStatus = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("Status"))
                                strGSTAmount = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount"))
                                strTotalWithGST = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("TotalWithGST"))
                                'strNetAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount"))
                                strServiceDate = Convert.ToDateTime(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ServiceDate")).ToString("dd/MM/yy")

                                strServiceBy = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ServiceBy"))
                                strBillingFrequency = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillingFrequency"))
                                strRcnoServiceRecord = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("RcnoServiceRecord"))
                                strContractGroupDept = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContractGroup"))
                                strLocation = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("Location"))

                                '07.04.24
                                strGSTNew = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GST"))
                                strGSTRateNew = Convert.ToString(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTRate"))
                                '07.04.24

                                'If ContractGroup <> strContractNo Then
                                'If ContractGroup <> strContractNo Or ContractGroupGroup <> strContractGroup Then
                                If ContractGroup <> strContractNo Then  '23.06.24

                                    '''''''''''27.10.17   Start: Adjustment of GST decimal value for the last record

                                    If Convert.ToInt32(txtLastSalesDetailRcno.Text) > 0 Then

                                        Dim dblTotalValue As Decimal = 0.0
                                        Dim commandSalesDetailSum As MySqlCommand = New MySqlCommand

                                        commandSalesDetailSum.CommandType = CommandType.Text
                                        commandSalesDetailSum.CommandText = "SELECT sum(GSTBase) as totalvalue FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "'"
                                        commandSalesDetailSum.Connection = conn

                                        Dim drSalesDetailSum As MySqlDataReader = commandSalesDetailSum.ExecuteReader()
                                        Dim dtSalesDetailSum As New DataTable
                                        dtSalesDetailSum.Load(drSalesDetailSum)

                                        dblTotalValue = Convert.ToDouble(dtSalesDetailSum.Rows(0)("totalvalue").ToString)

                                        commandSalesDetailSum.Dispose()

                                        If gstHeader <> dblTotalValue Then
                                            Dim diff As Decimal = 0.0
                                            diff = gstHeader - dblTotalValue

                                            ''''''''''''''''''''''
                                            Dim commandSalesDetailLastRecord As MySqlCommand = New MySqlCommand

                                            commandSalesDetailLastRecord.CommandType = CommandType.Text
                                            commandSalesDetailLastRecord.CommandText = "SELECT GSTBase, AppliedBase FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "' and Rcno = " & txtLastSalesDetailRcno.Text
                                            commandSalesDetailLastRecord.Connection = conn

                                            Dim drSalesDetailLastRecord As MySqlDataReader = commandSalesDetailLastRecord.ExecuteReader()
                                            Dim dtSalesDetailLastRecord As New DataTable
                                            dtSalesDetailLastRecord.Load(drSalesDetailLastRecord)

                                            Dim lastGSTBase As Double = 0.0
                                            Dim lastAppliedBase As Double = 0.0

                                            lastGSTBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("GSTBase").ToString)
                                            lastAppliedBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("AppliedBase").ToString)

                                            ''''''''''''''''''''''''''
                                            lastGSTBase = lastGSTBase + diff
                                            lastAppliedBase = lastAppliedBase + diff
                                            Dim commandAdjustLastRec As MySqlCommand = New MySqlCommand
                                            commandAdjustLastRec.CommandType = CommandType.Text

                                            Dim qryT As String = "UPDATE tblsalesdetail SET  GSTbase = " & lastGSTBase & ", GSTOriginal= " & lastGSTBase & ", AppliedBase = " & lastAppliedBase & ", AppliedOriginal= " & lastAppliedBase & " where rcno = " & txtLastSalesDetailRcno.Text

                                            commandAdjustLastRec.CommandText = qryT
                                            commandAdjustLastRec.Parameters.Clear()
                                            commandAdjustLastRec.Connection = conn
                                            commandAdjustLastRec.ExecuteNonQuery()
                                            commandAdjustLastRec.Dispose()
                                        End If

                                        '''''''''''''''''''''''''''

                                        ' 18.04.20 : Start:Update Service Address in tblSales

                                        Dim commandUpdateServiceAddress As MySqlCommand = New MySqlCommand
                                        commandUpdateServiceAddress.CommandType = CommandType.StoredProcedure
                                        commandUpdateServiceAddress.CommandText = "UpdateTblSalesServiceAddress"

                                        commandUpdateServiceAddress.Parameters.Clear()

                                        commandUpdateServiceAddress.Parameters.AddWithValue("@pr_InvoiceNumber", gBillNo.Trim)

                                        commandUpdateServiceAddress.Connection = conn
                                        commandUpdateServiceAddress.ExecuteScalar()


                                        ' 18.04.20 : End:Update Service Address in tblSales

                                        ''''''''''''''''''''''''''''''''
                                    End If

                                    ' ''''''''''''27.10.17  Start: Adjustment of GST decimal value for the last record

                                    ContractGroup = strContractNo
                                    LocationGroup = strLocationId
                                    AccountIdGroup = strAccountId

                                    ContractGroupGroup = strContractGroup

                                    GenerateInvoiceNo()

                                    '''''''''''''''''''''''''

                                    If dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString = "COMPANY" Then
                                        sql3 = "Select BillingNameSvc, BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblCompanyLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId & "'"
                                    Else
                                        sql3 = "Select BillingNameSvc, BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblPersonLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    End If

                                    Dim commandCustomerDetails As MySqlCommand = New MySqlCommand
                                    commandCustomerDetails.CommandType = CommandType.Text
                                    commandCustomerDetails.CommandText = sql3
                                    commandCustomerDetails.Connection = conn

                                    Dim drCustomerDetails As MySqlDataReader = commandCustomerDetails.ExecuteReader()

                                    Dim dtCustomerDetails As New DataTable
                                    dtCustomerDetails.Load(drCustomerDetails)

                                    lBillingNameSvc = ""
                                    lBillAddressSvc = ""
                                    lBillBuildingSvc = ""
                                    lBillStreetSvc = ""
                                    lBillCountrySvc = ""
                                    lBillCitySvc = ""
                                    lBillPOstalSvc = ""
                                    lArTermSvc = ""
                                    lSalesmanSvc = ""
                                    lContact1Svc = ""
                                    lBillStateSvc = ""

                                    If dtCustomerDetails.Rows.Count > 0 Then
                                        lBillingNameSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillingNameSvc"))
                                        lBillAddressSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillAddressSvc"))
                                        lBillBuildingSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillBuildingSvc"))
                                        lBillStreetSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStreetSvc"))
                                        lBillCountrySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCountrySvc"))
                                        lBillCitySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCitySvc"))
                                        lBillPOstalSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillPOstalSvc"))
                                        lArTermSvc = Convert.ToString(dtCustomerDetails.Rows(0)("ArTermSvc"))
                                        lSalesmanSvc = Convert.ToString(dtCustomerDetails.Rows(0)("SalesmanSvc"))
                                        lContact1Svc = Convert.ToString(dtCustomerDetails.Rows(0)("BillContact1Svc"))
                                        lBillStateSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStateSvc"))
                                    End If

                                    commandCustomerDetails.Dispose()
                                    dtCustomerDetails.Dispose()
                                    drCustomerDetails.Close()

                                    ''''''''''''''''''''''''
                                    '' Sales

                                    qrySales = "INSERT INTO tblSales(DocType, InvoiceNumber, CustAttention, CustName, AccountId, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, BillCity, BillState, "
                                    qrySales = qrySales + "  ServiceRecordNo, SalesDate, OurRef,YourRef, PoNo, RcnoServiceRecord,   Salesman, CreditTerms, CreditDays, Terms, TermsDay,"
                                    qrySales = qrySales + "  ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, "
                                    qrySales = qrySales + "  DiscountAmount, GSTAmount, NetAmount, Comments, ContactType, CompanyGroup, GLPeriod, AmountWithDiscount, BatchNo, RecurringInvoice, GLStatus, BillSchedule,  "
                                    qrySales = qrySales + "  StaffCode, CustAddress1, CustAddPostal, CustAddCountry, custAddBuilding, CustAddStreet, CustAddCity, CustAddState, DueDate, ContractGroup, LedgerCode, LedgerName, GST, GSTRate, Location,  "
                                    qrySales = qrySales + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn,TaxIdentificationNo,SalesTaxRegistrationNo) VALUES "
                                    qrySales = qrySales + "(@DocType, @InvoiceNumber, @CustAttention, @ClientName, @AccountId, @BillAddress1, @BillBuilding, @BillStreet,  @BillCountry, @BillPostal, @BillCity, @BillState,"
                                    qrySales = qrySales + "@ServiceRecordNo, @SalesDate, @ourRef, @YourRef,  @PoNo, @RcnoServiceRecord, @Salesman, @CreditTerms, @CreditDays, @Terms, @TermsDay,"
                                    qrySales = qrySales + " @ValueBase, @ValueOriginal, @GSTBase, @GSTOriginal, @AppliedBase, @AppliedOriginal, @BalanceBase, @BalanceOriginal, "
                                    qrySales = qrySales + " @DiscountAmount, @GSTAmount, @NetAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod, @AmountWithDiscount, @BatchNo, @RecurringInvoice, @GLStatus, @BillSchedule, "
                                    qrySales = qrySales + " @StaffCode, @CustAddress1, @CustAddPostal, @CustAddCountry, @custAddBuilding, @CustAddStreet, @CustAddCity, @CustAddState, @DueDate, @ContractGroup, @LedgerCode, @LedgerName, @GST, @GSTRate, @Location, "
                                    qrySales = qrySales + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn,@TaxIdentificationNo,@SalesTaxRegistrationNo );"

                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString)

                                    Dim lCreditDays As Decimal
                                    If String.IsNullOrEmpty(lArTermSvc) = True Then
                                        lArTermSvc = "C.O.D."
                                        lCreditDays = 0.0
                                    Else
                                        lCreditDays = FindCreditDays(lArTermSvc)
                                    End If

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    command3.Parameters.AddWithValue("@DocType", "ARIN")
                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                    command3.Parameters.AddWithValue("@CustAttention", lContact1Svc)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("CustName").ToString)
                                    command3.Parameters.AddWithValue("@ClientName", lBillingNameSvc.Trim.ToUpper)


                                    command3.Parameters.AddWithValue("@AccountId", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString)

                                    command3.Parameters.AddWithValue("@BillAddress1", lBillAddressSvc)
                                    command3.Parameters.AddWithValue("@BillBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@BillStreet", lBillStreetSvc)
                                    command3.Parameters.AddWithValue("@BillCountry", lBillCountrySvc)
                                    command3.Parameters.AddWithValue("@BillPostal", lBillPOstalSvc)

                                    command3.Parameters.AddWithValue("@BillCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@BillState", lBillStateSvc)
                                    'command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                                    command3.Parameters.AddWithValue("@OurRef", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("OurRef").ToString)
                                    command3.Parameters.AddWithValue("@YourRef", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("YourRef").ToString)
                                    command3.Parameters.AddWithValue("@PoNo", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("PoNo").ToString)
                                    command3.Parameters.AddWithValue("@Salesman", lSalesmanSvc)


                                    If txtBatchDate.Text.Trim = "" Then
                                        command3.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                                    Else
                                        command3.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                                    End If

                                    command3.Parameters.AddWithValue("@Comments", "")
                                    command3.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)
                                    'command3.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                                    command3.Parameters.AddWithValue("@RcnoServiceRecord", 0)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString))

                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString))

                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) + Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))

                                    command3.Parameters.AddWithValue("@ContactType", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString)
                                    command3.Parameters.AddWithValue("@CompanyGroup", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("CompanyGroup").ToString)
                                    command3.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)
                                    command3.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                                    command3.Parameters.AddWithValue("@GLStatus", "O")
                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If

                                    command3.Parameters.AddWithValue("@CreditTerms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)

                                    command3.Parameters.AddWithValue("@Terms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@TermsDay", lCreditDays)
                                    'command3.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

                                    'If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                    '    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditDays", 0)
                                    'End If


                                    If chkRecurringInvoice.Checked = True Then
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "Y")
                                    Else
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "N")
                                    End If

                                    command3.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)


                                    command3.Parameters.AddWithValue("@StaffCode", lSalesmanSvc)
                                    command3.Parameters.AddWithValue("@CustAddress1", lBillAddressSvc)
                                    command3.Parameters.AddWithValue("@CustAddPostal", lBillPOstalSvc)
                                    command3.Parameters.AddWithValue("@CustAddCountry", lBillCountrySvc)

                                    command3.Parameters.AddWithValue("@CustAddBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@CustAddStreet", lBillStreetSvc)

                                    command3.Parameters.AddWithValue("@CustAddCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@CustAddState", lBillStateSvc)

                                    command3.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(lCreditDays))
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)
                                    command3.Parameters.AddWithValue("@ContractGroup", ContractGroupGroup)
                                    '07.04.24

                                    command3.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                    command3.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)


                                    'command3.Parameters.AddWithValue("@GST", "SR")
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    command3.Parameters.AddWithValue("@Gst", strGSTNew)
                                    command3.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))
                                    '07.04.24


                                    'command3.Parameters.AddWithValue("@Location", strLocation)
                                    command3.Parameters.AddWithValue("@Location", txtLocation.Text)

                                    '08.03.17
                                    command3.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                                    command3.Parameters.AddWithValue("@TaxIdentificationNo", strTIN)
                                    command3.Parameters.AddWithValue("@SalesTaxRegistrationNo", strSST)


                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()

                                    Dim sqlLastId As String
                                    sqlLastId = "SELECT last_insert_id() from tblSales"

                                    Dim commandRcno As MySqlCommand = New MySqlCommand
                                    commandRcno.CommandType = CommandType.Text
                                    commandRcno.CommandText = sqlLastId
                                    commandRcno.Parameters.Clear()
                                    commandRcno.Connection = conn
                                    txtRcno.Text = commandRcno.ExecuteScalar()

                                    ToBillAmt = Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString)
                                    DiscAmount = Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17

                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    ''Start: Update tblServiceRecord
                                    'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                                    'commandUpdateServiceRecord.CommandType = CommandType.Text

                                    ''Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                                    'Dim qryUpdateServiceRecord As String = "Update tblservicerecord Set BillYN = 'Y', BillNo = '" & gBillNo & "', BilledAmt = " & Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString) & "  where Rcno= @Rcno "

                                    'commandUpdateServiceRecord.CommandText = qryUpdateServiceRecord
                                    'commandUpdateServiceRecord.Parameters.Clear()

                                    'commandUpdateServiceRecord.Parameters.AddWithValue("@Rcno", strRcnoServiceRecord)
                                    'commandUpdateServiceRecord.Connection = conn
                                    'commandUpdateServiceRecord.ExecuteNonQuery()

                                    ''End: Update tblServiceRecord


                                Else

                                    ToBillAmt = ToBillAmt + Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString)

                                    DiscAmount = DiscAmount + Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = GSTAmount + Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = NetAmount + Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17

                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    NetAmount = gstHeader + Convert.ToDecimal(ToBillAmt)

                                    qrySales = "UPDATE tblSales SET ValueBase=@ValueBase, ValueOriginal = @ValueOriginal, AppliedBase =@AppliedBase, BalanceBase=@BalanceBase, BalanceOriginal = @BalanceOriginal, "
                                    qrySales = qrySales + " GSTBase =@GSTBase, GSTOriginal = @GSTOriginal, AppliedOriginal = @AppliedOriginal, DiscountAmount =@DiscountAmount, AmountWithDiscount=@AmountWithDiscount, GSTAmount=@GSTAmount, NetAmount=@NetAmount"
                                    qrySales = qrySales + " where InvoiceNumber= @InvoiceNumber "

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    '27.10.17  End: Adjustment of GST decimal value for the last record

                                    'command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(NetAmount))

                                    'command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()
                                End If  ' If ContractGroup <> strContractNo Then




                                '''''''''''''' tblSalesDetail

                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text

                                Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
                                qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
                                qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
                                qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, BillSchedule, ServiceBy, ServiceDate, BillingFrequency, ItemDescription, ContractGroup, "
                                qryDetail = qryDetail + " Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, Location,  "
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                                qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                                qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                                qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @BillSchedule, @ServiceBy, @ServiceDate, @BillingFrequency, @ItemDescription, @ContractGroup,  "
                                qryDetail = qryDetail + " @Address1, @AddBuilding, @AddStreet, @AddCity, @AddState, @AddCountry, @AddPostal,  @Location, "
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                commandSalesDetail.CommandText = qryDetail
                                commandSalesDetail.Parameters.Clear()

                                commandSalesDetail.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                commandSalesDetail.Parameters.AddWithValue("@Sequence", 0)

                                'If lblidItemType.Text = "SERVICE" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERV")
                                'ElseIf lblidItemType.Text = "STOCK" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "STCK")
                                'ElseIf lblidItemType.Text = "OTHERS" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "DIST")
                                'End If
                                'Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                                commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERVICE")

                                ''''''''''''''''''''''''''''''''''''''
                                Dim commandBP As MySqlCommand = New MySqlCommand
                                commandBP.CommandType = CommandType.Text

                                If String.IsNullOrEmpty(strContractNo) = False Then
                                    If strStatus = "P" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                                    ElseIf strStatus = "O" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                                    End If
                                Else
                                    commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-COO'"
                                End If

                                commandBP.Connection = conn

                                Dim drBP As MySqlDataReader = commandBP.ExecuteReader()
                                Dim dtBP As New DataTable
                                dtBP.Load(drBP)

                                ''''''''''''''''''''''''''''''''''''

                                'Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtOtherCodeGV"), TextBox)
                                'TextBoxOtherCode.Text = dt1.Rows(0)("COACode").ToString()

                                'Dim TextBoxDescription As TextBox = CType(grvBillingDetails.Rows(rowselected - 1).Cells(0).FindControl("txtDescriptionGV"), TextBox)
                                'TextBoxDescription.Text = dt1.Rows(0)("COADescription").ToString()

                                'commandSalesDetail.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                'commandSalesDetail.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                commandSalesDetail.Parameters.AddWithValue("@LedgerCode", dtBP.Rows(0)("COACode").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@LedgerName", dtBP.Rows(0)("COADescription").ToString())

                                commandSalesDetail.Parameters.AddWithValue("@SubLedgerCode", "")
                                commandSalesDetail.Parameters.AddWithValue("@SONUMBER", "")
                                commandSalesDetail.Parameters.AddWithValue("@RefType", strRecordNo)


                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                '07.04.24
                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                commandSalesDetail.Parameters.AddWithValue("@Gst", strGSTNew)
                                commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))

                                '07.04.24

                                commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                                commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                                commandSalesDetail.Parameters.AddWithValue("@Quantity", 1)
                                commandSalesDetail.Parameters.AddWithValue("@UnitMs", "XOJ")
                                commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@DiscP", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@TaxBase", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@GstOriginal", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@GstBase", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@AppliedBase", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(strBillAmount))

                                '''''''''''''''''''''''''''''''
                                Dim command10 As MySqlCommand = New MySqlCommand
                                command10.CommandType = CommandType.Text
                                command10.CommandText = "SELECT Rcno, Status, ContractNo, Description, ServiceDate, BillAmount, LocationID, ServiceLocationGroup, Notes, Address1, AddBuilding, AddStreet, AddCity, AddCountry, AddState,  AddPostal FROM tblservicerecord where RecordNo =@RecordNo"
                                command10.Parameters.AddWithValue("@RecordNo", strRecordNo)
                                command10.Connection = conn

                                Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                                Dim dt10 As New DataTable
                                dt10.Load(dr10)

                                ''''''''''''''''''''''''''''''

                                commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate + ", " + dt10.Rows(0)("Notes").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ItemDescription", dtBP.Rows(0)("Description").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@Comments", "")
                                commandSalesDetail.Parameters.AddWithValue("@GroupId", "")
                                commandSalesDetail.Parameters.AddWithValue("@DetailID", "")
                                commandSalesDetail.Parameters.AddWithValue("@GrpDetName", "")

                                commandSalesDetail.Parameters.AddWithValue("@SoCode", 0.0)
                                'commandSalesDetail.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                                commandSalesDetail.Parameters.AddWithValue("@ItemCode", dtBP.Rows(0)("ProductCode").ToString().Trim())

                                commandSalesDetail.Parameters.AddWithValue("@AvgCost", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@CostValue", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@COSTCODE", strContractNo)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceStatus", strStatus)

                                commandSalesDetail.Parameters.AddWithValue("@DiscAmount", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@LocationId", strLocationId1)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceLocationGroup", strServiceLocationGroup1)

                                commandSalesDetail.Parameters.AddWithValue("@RcnoServiceRecord", strRcnoServiceRecord)
                                commandSalesDetail.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                commandSalesDetail.Parameters.AddWithValue("@ServiceBy", strServiceBy)

                                If String.IsNullOrEmpty(strServiceDate) = True Then
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                                Else
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(strServiceDate).ToString("yyyy-MM-dd"))
                                End If

                                'commandSalesDetail.Parameters.AddWithValue("@ServiceDate", lblidServiceDate.Text)
                                commandSalesDetail.Parameters.AddWithValue("@BillingFrequency", strBillingFrequency)

                                commandSalesDetail.Parameters.AddWithValue("@Address1", dt10.Rows(0)("Address1").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddBuilding", dt10.Rows(0)("AddBuilding").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddStreet", dt10.Rows(0)("AddStreet").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCity", dt10.Rows(0)("AddCity").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddState", dt10.Rows(0)("AddState").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCountry", dt10.Rows(0)("AddCountry").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddPostal", dt10.Rows(0)("AddPostal").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ContractGroup", strContractGroupDept)
                                commandSalesDetail.Parameters.AddWithValue("@Location", strLocation)

                                commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Connection = conn
                                commandSalesDetail.ExecuteNonQuery()

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                Dim sqlLastIdSalesDetail As String
                                sqlLastIdSalesDetail = "SELECT last_insert_id() from tblsalesdetail"

                                Dim commandRcnoSalesDetail As MySqlCommand = New MySqlCommand
                                commandRcnoSalesDetail.CommandType = CommandType.Text
                                commandRcnoSalesDetail.CommandText = sqlLastIdSalesDetail
                                commandRcnoSalesDetail.Parameters.Clear()
                                commandRcnoSalesDetail.Connection = conn
                                txtLastSalesDetailRcno.Text = commandRcnoSalesDetail.ExecuteScalar()

                                commandRcnoSalesDetail.Dispose()

                                'UpdateLastBillDate(strContractNo)

                                'txtLastSalesDetailRcno.Text = "SELECT last_insert_id() from tblsalesdetail"

                                ''''''''''''''''''''''''''''''''' tblSalesDetail

                                ''''''''''''' tblSalesDetail
                                '''''''''''''''''''''''''''''''

                                'Header

                                Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetail.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & txtInvoiceNo.Text & "',  BatchNo = '" & txtBatchNo.Text & "', rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text
                                Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & gBillNo & "', BillNo ='" & gBillNo & "', rcnoInvoice = " & txtRcno.Text & "  where Rcno =" & dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                                commandServiceBillingDetail.Parameters.Clear()
                                commandServiceBillingDetail.Connection = conn
                                commandServiceBillingDetail.ExecuteNonQuery()

                                Dim commandServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetailItem.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & txtInvoiceNo.Text & "' where BatchNo = '" & txtBatchNo.Text & "'"
                                Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & gBillNo & "' where RcnoServiceBillingDetail = " & dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetailItem.CommandText = sqlUpdateServiceBillingDetailItem
                                commandServiceBillingDetailItem.Parameters.Clear()
                                commandServiceBillingDetailItem.Connection = conn
                                commandServiceBillingDetailItem.ExecuteNonQuery()
                                ''''''''''''''''''''''''''''''
                                rowIndex1 = rowIndex1 + 1
                            Next row1
                        End If

                    ElseIf strGroupingBy = "LocationID" Then

                        '''''''''''''''''''''''''''''''''
                        sqltbwservicebillingdetail = ""
                        'Dim conn1 As MySqlConnection = New MySqlConnection()
                        'conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn1.Open()

                        sqltbwservicebillingdetail = "Select *   "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " FROM tbwservicebillingdetail A "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " where 1 = 1 "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " and BillSchedule = '" & txtInvoiceNo.Text.Trim & "' and AccountId = '" & strAccountId.Trim & "'"
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " Order by  LocationId, ServiceDate"

                        'Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand
                        commandTbwservicebillingdetail.CommandType = CommandType.Text
                        commandTbwservicebillingdetail.CommandText = sqltbwservicebillingdetail
                        'commandTbwservicebillingdetail.Connection = ""
                        commandTbwservicebillingdetail.Connection = conn

                        ''Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand

                        drcommandTbwservicebillingdetail = commandTbwservicebillingdetail.ExecuteReader()

                        Dim dtTbwservicebillingdetailLocation As New DataTable
                        dtTbwservicebillingdetailLocation.Load(drcommandTbwservicebillingdetail)

                        'Dim TotRec = dt1.Rows.Count
                        If dtTbwservicebillingdetailLocation.Rows.Count > 0 Then

                            'Dim strAccountId As String
                            'Dim strContactType As String

                            Dim strLocationId As String
                            Dim strContractNo As String


                            'strAccountId = ""
                            strContactType = ""
                            strLocationId = ""
                            strContractNo = ""

                            '07.04.24
                            Dim strContractGroup As String
                            strContractGroup = ""
                            '07.04.24

                            Dim rowIndex1 = 0

                            '''''''''''''''''''''''''''''''

                            For Each row1 As DataRow In dtTbwservicebillingdetailLocation.Rows

                                strLocationId = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("LocationId"))
                                strContractNo = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContractNo"))
                                'strContactType = Convert.ToString(dt1.Rows(rowIndex)("ContactType"))
                                strAccountId = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("AccountId"))

                                strContractGroup = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContractGroup")) '07.04.24

                                strRecordNo = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("RecordNo"))
                                strLocationId1 = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("LocationId"))
                                strServiceLocationGroup1 = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ServiceLocationGroup"))
                                strBillAmount = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount"))
                                strStatus = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("Status"))
                                strGSTAmount = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount"))
                                strTotalWithGST = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("TotalWithGST"))
                                'strNetAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount"))
                                strServiceDate = Convert.ToDateTime(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ServiceDate")).ToString("dd/MM/yy")
                                strServiceBy = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ServiceBy"))
                                strBillingFrequency = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillingFrequency"))
                                strRcnoServiceRecord = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("RcnoServiceRecord"))
                                strContractGroupDept = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContractGroup"))
                                strLocation = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("Location"))

                                '07.04.24
                                strGSTNew = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GST"))
                                strGSTRateNew = Convert.ToString(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTRate"))
                                '07.04.24

                                'If LocationGroup <> strLocationId Then
                                'If LocationGroup <> strLocationId Or ContractGroupGroup <> strContractGroup Then
                                If LocationGroup <> strLocationId Then  '23.06.24

                                    '''''''''''27.10.17   Start: Adjustment of GST decimal value for the last record

                                    If Convert.ToInt32(txtLastSalesDetailRcno.Text) > 0 Then

                                        Dim dblTotalValue As Decimal = 0.0
                                        Dim commandSalesDetailSum As MySqlCommand = New MySqlCommand

                                        commandSalesDetailSum.CommandType = CommandType.Text
                                        commandSalesDetailSum.CommandText = "SELECT sum(GSTBase) as totalvalue FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "'"
                                        commandSalesDetailSum.Connection = conn

                                        Dim drSalesDetailSum As MySqlDataReader = commandSalesDetailSum.ExecuteReader()
                                        Dim dtSalesDetailSum As New DataTable
                                        dtSalesDetailSum.Load(drSalesDetailSum)

                                        dblTotalValue = Convert.ToDouble(dtSalesDetailSum.Rows(0)("totalvalue").ToString)

                                        commandSalesDetailSum.Dispose()

                                        If gstHeader <> dblTotalValue Then
                                            Dim diff As Decimal = 0.0
                                            diff = gstHeader - dblTotalValue

                                            ''''''''''''''''''''''
                                            Dim commandSalesDetailLastRecord As MySqlCommand = New MySqlCommand

                                            commandSalesDetailLastRecord.CommandType = CommandType.Text
                                            commandSalesDetailLastRecord.CommandText = "SELECT GSTBase, AppliedBase FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "' and Rcno = " & txtLastSalesDetailRcno.Text
                                            commandSalesDetailLastRecord.Connection = conn

                                            Dim drSalesDetailLastRecord As MySqlDataReader = commandSalesDetailLastRecord.ExecuteReader()
                                            Dim dtSalesDetailLastRecord As New DataTable
                                            dtSalesDetailLastRecord.Load(drSalesDetailLastRecord)

                                            Dim lastGSTBase As Double = 0.0
                                            Dim lastAppliedBase As Double = 0.0

                                            lastGSTBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("GSTBase").ToString)
                                            lastAppliedBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("AppliedBase").ToString)

                                            ''''''''''''''''''''''''''
                                            lastGSTBase = lastGSTBase + diff
                                            lastAppliedBase = lastAppliedBase + diff
                                            Dim commandAdjustLastRec As MySqlCommand = New MySqlCommand
                                            commandAdjustLastRec.CommandType = CommandType.Text

                                            Dim qryT As String = "UPDATE tblsalesdetail SET  GSTbase = " & lastGSTBase & ", GSTOriginal= " & lastGSTBase & ", AppliedBase = " & lastAppliedBase & ", AppliedOriginal= " & lastAppliedBase & " where rcno = " & txtLastSalesDetailRcno.Text

                                            commandAdjustLastRec.CommandText = qryT
                                            commandAdjustLastRec.Parameters.Clear()
                                            commandAdjustLastRec.Connection = conn
                                            commandAdjustLastRec.ExecuteNonQuery()
                                            commandAdjustLastRec.Dispose()
                                        End If


                                        '''''''''''''''''''''''''''
                                        ' 18.04.20 : Start:Update Service Address in tblSales

                                        Dim commandUpdateServiceAddress As MySqlCommand = New MySqlCommand
                                        commandUpdateServiceAddress.CommandType = CommandType.StoredProcedure
                                        commandUpdateServiceAddress.CommandText = "UpdateTblSalesServiceAddress"
                                        commandUpdateServiceAddress.Parameters.Clear()
                                        commandUpdateServiceAddress.Parameters.AddWithValue("@pr_InvoiceNumber", gBillNo.Trim)
                                        commandUpdateServiceAddress.Connection = conn
                                        commandUpdateServiceAddress.ExecuteScalar()

                                        ' 18.04.20 : End:Update Service Address in tblSales

                                        ''''''''''''''''''''''''''''''''
                                    End If

                                    ' ''''''''''''27.10.17  Start: Adjustment of GST decimal value for the last record

                                    ContractGroup = strContractNo
                                    LocationGroup = strLocationId
                                    AccountIdGroup = strAccountId
                                    ContractGroupGroup = strContractGroup

                                    GenerateInvoiceNo()

                                    '''''''''''''''''''''''''

                                    If dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContactType").ToString = "COMPANY" Then
                                        sql3 = "Select BillingNameSvc,BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblCompanyLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailLocation.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    Else
                                        sql3 = "Select BillingNameSvc,BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblPersonLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailLocation.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    End If

                                    Dim commandCustomerDetails As MySqlCommand = New MySqlCommand
                                    commandCustomerDetails.CommandType = CommandType.Text
                                    commandCustomerDetails.CommandText = sql3
                                    commandCustomerDetails.Connection = conn

                                    Dim drCustomerDetails As MySqlDataReader = commandCustomerDetails.ExecuteReader()

                                    Dim dtCustomerDetails As New DataTable
                                    dtCustomerDetails.Load(drCustomerDetails)
                                    lBillingNameSvc = ""
                                    lBillAddressSvc = ""
                                    lBillBuildingSvc = ""
                                    lBillStreetSvc = ""
                                    lBillCountrySvc = ""
                                    lBillCitySvc = ""
                                    lBillPOstalSvc = ""
                                    lArTermSvc = ""
                                    lSalesmanSvc = ""
                                    lContact1Svc = ""
                                    lBillStateSvc = ""

                                    If dtCustomerDetails.Rows.Count > 0 Then
                                        lBillingNameSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillingNameSvc"))
                                        lBillAddressSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillAddressSvc"))
                                        lBillBuildingSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillBuildingSvc"))
                                        lBillStreetSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStreetSvc"))
                                        lBillCountrySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCountrySvc"))
                                        lBillCitySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCitySvc"))
                                        lBillPOstalSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillPOstalSvc"))
                                        lArTermSvc = Convert.ToString(dtCustomerDetails.Rows(0)("ArTermSvc"))
                                        lSalesmanSvc = Convert.ToString(dtCustomerDetails.Rows(0)("SalesmanSvc"))
                                        lContact1Svc = Convert.ToString(dtCustomerDetails.Rows(0)("BillContact1Svc"))
                                        lBillStateSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStateSvc"))
                                    End If

                                    commandCustomerDetails.Dispose()
                                    dtCustomerDetails.Dispose()
                                    drCustomerDetails.Close()

                                    ''''''''''''''''''''''''
                                    '' Sales

                                    qrySales = "INSERT INTO tblSales(DocType, InvoiceNumber, CustAttention, CustName, AccountId, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, BillCity, BillState, "
                                    qrySales = qrySales + "  ServiceRecordNo, SalesDate, OurRef,YourRef, PoNo, RcnoServiceRecord,   Salesman, CreditTerms, CreditDays, Terms, TermsDay,"
                                    qrySales = qrySales + "  ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, "
                                    qrySales = qrySales + "  DiscountAmount, GSTAmount, NetAmount, Comments, ContactType, CompanyGroup, GLPeriod, AmountWithDiscount, BatchNo, RecurringInvoice, GLStatus,  BillSchedule, "
                                    qrySales = qrySales + " StaffCode, CustAddress1, CustAddPostal, CustAddCountry, custAddBuilding, CustAddStreet, CustAddCity, CustAddState, DueDate, ContractGroup, LedgerCode, LedgerName, GST, GSTRate, Location, "
                                    qrySales = qrySales + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn,TaxIdentificationNo,SalesTaxRegistrationNo) VALUES "
                                    qrySales = qrySales + "(@DocType, @InvoiceNumber, @CustAttention, @ClientName, @AccountId, @BillAddress1, @BillBuilding, @BillStreet,  @BillCountry, @BillPostal, @BillCity, @BillState,"
                                    qrySales = qrySales + "@ServiceRecordNo, @SalesDate, @ourRef, @YourRef,  @PoNo, @RcnoServiceRecord,  @Salesman, @CreditTerms, @CreditDays, @Terms, @TermsDay,"
                                    qrySales = qrySales + " @ValueBase, @ValueOriginal, @GSTBase, @GSTOriginal, @AppliedBase, @AppliedOriginal, @BalanceBase, @BalanceOriginal, "
                                    qrySales = qrySales + " @DiscountAmount, @GSTAmount, @NetAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod, @AmountWithDiscount, @BatchNo, @RecurringInvoice, @GLStatus, @BillSchedule, "
                                    qrySales = qrySales + " @StaffCode, @CustAddress1, @CustAddPostal, @CustAddCountry, @custAddBuilding, @CustAddStreet, @CustAddCity, @CustAddState, @DueDate,  @ContractGroup, @LedgerCode, @LedgerName, @GST, @GSTRate, @Location,"
                                    qrySales = qrySales + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn,@TaxIdentificationNo,@SalesTaxRegistrationNo);"


                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContactType").ToString)

                                    'Dim lCreditDays As Decimal
                                    'If String.IsNullOrEmpty(lTerms) = True Then
                                    '    lTerms = "C.O.D."
                                    '    lCreditDays = 0.0
                                    'Else
                                    '    lCreditDays = FindCreditDays(lTerms)
                                    'End If

                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString)

                                    Dim lCreditDays As Decimal
                                    If String.IsNullOrEmpty(lArTermSvc) = True Then
                                        lArTermSvc = "C.O.D."
                                        lCreditDays = 0.0
                                    Else
                                        lCreditDays = FindCreditDays(lArTermSvc)
                                    End If

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    'BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ""
                                    'qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                                    'qry = qry + " ContractGroup, Status, Address1, BatchNo,"


                                    'command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                                    command3.Parameters.AddWithValue("@DocType", "ARIN")
                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                    command3.Parameters.AddWithValue("@CustAttention", lContact1Svc)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("CustName").ToString)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("CustName").ToString)
                                    command3.Parameters.AddWithValue("@ClientName", lBillingNameSvc.Trim.ToUpper)

                                    command3.Parameters.AddWithValue("@AccountId", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("AccountId").ToString)

                                    command3.Parameters.AddWithValue("@BillAddress1", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAddress1").ToString)
                                    command3.Parameters.AddWithValue("@BillBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@BillStreet", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillStreet").ToString)
                                    command3.Parameters.AddWithValue("@BillCountry", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillCountry").ToString)
                                    command3.Parameters.AddWithValue("@BillPostal", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillPostal").ToString)

                                    command3.Parameters.AddWithValue("@BillCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@BillState", lBillStateSvc)
                                    'command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                                    command3.Parameters.AddWithValue("@OurRef", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("OurRef").ToString)
                                    command3.Parameters.AddWithValue("@YourRef", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("YourRef").ToString)
                                    command3.Parameters.AddWithValue("@PoNo", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("PoNo").ToString)

                                    command3.Parameters.AddWithValue("@Salesman", lSalesmanSvc.Trim)


                                    If txtBatchDate.Text.Trim = "" Then
                                        command3.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                                    Else
                                        command3.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                                    End If

                                    command3.Parameters.AddWithValue("@Comments", "")
                                    command3.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)
                                    'command3.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                                    command3.Parameters.AddWithValue("@RcnoServiceRecord", 0)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString))

                                    command3.Parameters.AddWithValue("@ContactType", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("ContactType").ToString)
                                    command3.Parameters.AddWithValue("@CompanyGroup", dtTbwservicebillingdetailLocation.Rows(rowIndex1)("CompanyGroup").ToString)
                                    command3.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)
                                    command3.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                                    command3.Parameters.AddWithValue("@GLStatus", "O")

                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If

                                    command3.Parameters.AddWithValue("@CreditTerms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)

                                    command3.Parameters.AddWithValue("@Terms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@TermsDay", lCreditDays)


                                    'If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                    '    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditDays", 0)
                                    'End If


                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If


                                    'If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                    '    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditDays", 0)
                                    'End If


                                    If chkRecurringInvoice.Checked = True Then
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "Y")
                                    Else
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "N")
                                    End If
                                    command3.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                    command3.Parameters.AddWithValue("@StaffCode", lSalesmanSvc)
                                    command3.Parameters.AddWithValue("@CustAddress1", lBillAddressSvc)
                                    command3.Parameters.AddWithValue("@CustAddPostal", lBillPOstalSvc)
                                    command3.Parameters.AddWithValue("@CustAddCountry", lBillCountrySvc)

                                    command3.Parameters.AddWithValue("@CustAddBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@CustAddStreet", lBillStreetSvc)

                                    command3.Parameters.AddWithValue("@CustAddCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@CustAddState", lBillStateSvc)

                                    command3.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(lCreditDays))
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)
                                    command3.Parameters.AddWithValue("@ContractGroup", ContractGroupGroup)
                                    '07.04.24

                                    command3.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                    command3.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                    'command3.Parameters.AddWithValue("@GST", "SR")
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    command3.Parameters.AddWithValue("@Gst", strGSTNew)
                                    command3.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))
                                    '07.04.24


                                    'command3.Parameters.AddWithValue("@Location", strLocation)
                                    command3.Parameters.AddWithValue("@Location", txtLocation.Text)

                                    '08.03.17
                                    command3.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@TaxIdentificationNo", strTIN)
                                    command3.Parameters.AddWithValue("@SalesTaxRegistrationNo", strSST)

                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()

                                    Dim sqlLastId As String
                                    sqlLastId = "SELECT last_insert_id() from tblSales"

                                    Dim commandRcno As MySqlCommand = New MySqlCommand
                                    commandRcno.CommandType = CommandType.Text
                                    commandRcno.CommandText = sqlLastId
                                    commandRcno.Parameters.Clear()
                                    commandRcno.Connection = conn
                                    txtRcno.Text = commandRcno.ExecuteScalar()

                                    ToBillAmt = Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString)
                                    DiscAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17
                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    ''Start: Update tblServiceRecord
                                    'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                                    'commandUpdateServiceRecord.CommandType = CommandType.Text

                                    ''Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                                    'Dim qryUpdateServiceRecord As String = "Update tblservicerecord Set BillYN = 'Y', BillNo = '" & gBillNo & "', BilledAmt = " & Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString) & "  where Rcno= @Rcno "

                                    'commandUpdateServiceRecord.CommandText = qryUpdateServiceRecord
                                    'commandUpdateServiceRecord.Parameters.Clear()

                                    'commandUpdateServiceRecord.Parameters.AddWithValue("@Rcno", strRcnoServiceRecord)
                                    'commandUpdateServiceRecord.Connection = conn
                                    'commandUpdateServiceRecord.ExecuteNonQuery()

                                    ''End: Update tblServiceRecord

                                Else
                                    ToBillAmt = ToBillAmt + Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("BillAmount").ToString)

                                    DiscAmount = DiscAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = GSTAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = NetAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocation.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17
                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    NetAmount = gstHeader + Convert.ToDecimal(ToBillAmt)

                                    qrySales = "UPDATE tblSales SET ValueBase=@ValueBase, ValueOriginal = @ValueOriginal, AppliedBase =@AppliedBase, BalanceBase=@BalanceBase, BalanceOriginal = @BalanceOriginal, "
                                    qrySales = qrySales + " GSTBase =@GSTBase, GSTOriginal = @GSTOriginal, AppliedOriginal = @AppliedOriginal, DiscountAmount =@DiscountAmount, AmountWithDiscount=@AmountWithDiscount, GSTAmount=@GSTAmount, NetAmount=@NetAmount"
                                    qrySales = qrySales + " where InvoiceNumber= @InvoiceNumber "

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    '27.10.17  End: Adjustment of GST decimal value for the last record

                                    'command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    'command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()
                                End If  ' If ContractGroup <> strContractNo Then

                                '''''''''''''''''''''''''''''''

                                '''''''''''''' tblSalesDetail

                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text

                                Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
                                qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
                                qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
                                qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, BillSchedule, ServiceBy, ServiceDate, BillingFrequency, ItemDescription, ContractGroup,"
                                qryDetail = qryDetail + " Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, Location, "
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                                qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                                qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                                qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @BillSchedule, @ServiceBy, @ServiceDate, @BillingFrequency, @ItemDescription, @ContractGroup,"
                                qryDetail = qryDetail + " @Address1, @AddBuilding, @AddStreet, @AddCity, @AddState, @AddCountry, @AddPostal, @Location, "
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                commandSalesDetail.CommandText = qryDetail
                                commandSalesDetail.Parameters.Clear()

                                commandSalesDetail.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                commandSalesDetail.Parameters.AddWithValue("@Sequence", 0)

                                'If lblidItemType.Text = "SERVICE" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERV")
                                'ElseIf lblidItemType.Text = "STOCK" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "STCK")
                                'ElseIf lblidItemType.Text = "OTHERS" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "DIST")
                                'End If
                                'Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                                commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERVICE")

                                ''''''''''''''''''''''''''''''''''''''
                                Dim commandBP As MySqlCommand = New MySqlCommand
                                commandBP.CommandType = CommandType.Text

                                If String.IsNullOrEmpty(strContractNo) = False Then
                                    If strStatus = "P" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                                    ElseIf strStatus = "O" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                                    End If
                                Else
                                    commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-COO'"
                                End If

                                commandBP.Connection = conn

                                Dim drBP As MySqlDataReader = commandBP.ExecuteReader()
                                Dim dtBP As New DataTable
                                dtBP.Load(drBP)

                                ''''''''''''''''''''''''''''''''''''

                                'commandSalesDetail.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                'commandSalesDetail.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                commandSalesDetail.Parameters.AddWithValue("@LedgerCode", dtBP.Rows(0)("COACode").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@LedgerName", dtBP.Rows(0)("COADescription").ToString())

                                commandSalesDetail.Parameters.AddWithValue("@SubLedgerCode", "")
                                commandSalesDetail.Parameters.AddWithValue("@SONUMBER", "")
                                commandSalesDetail.Parameters.AddWithValue("@RefType", strRecordNo)

                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                '07.04.24
                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                commandSalesDetail.Parameters.AddWithValue("@Gst", strGSTNew)
                                commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))

                                '07.04.24
                                commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                                commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                                commandSalesDetail.Parameters.AddWithValue("@Quantity", 1)
                                commandSalesDetail.Parameters.AddWithValue("@UnitMs", "XOJ")
                                commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@DiscP", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@TaxBase", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@GstOriginal", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@GstBase", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@AppliedBase", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(strBillAmount))

                                '''''''''''''''''''''''''''''''
                                Dim command10 As MySqlCommand = New MySqlCommand
                                command10.CommandType = CommandType.Text
                                command10.CommandText = "SELECT Rcno, Status, ContractNo, Description, ServiceDate, BillAmount, LocationID, ServiceLocationGroup, Notes, Address1, AddBuilding, AddStreet, AddCity, AddCountry, AddState,  AddPostal FROM tblservicerecord where RecordNo =@RecordNo"
                                command10.Parameters.AddWithValue("@RecordNo", strRecordNo)
                                command10.Connection = conn

                                Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                                Dim dt10 As New DataTable
                                dt10.Load(dr10)

                                ''''''''''''''''''''''''''''''

                                commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate + ", " + dt10.Rows(0)("Notes").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ItemDescription", dtBP.Rows(0)("Description").ToString())
                                'commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate)
                                commandSalesDetail.Parameters.AddWithValue("@Comments", "")
                                commandSalesDetail.Parameters.AddWithValue("@GroupId", "")
                                commandSalesDetail.Parameters.AddWithValue("@DetailID", "")
                                commandSalesDetail.Parameters.AddWithValue("@GrpDetName", "")

                                commandSalesDetail.Parameters.AddWithValue("@SoCode", 0.0)
                                'commandSalesDetail.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                                commandSalesDetail.Parameters.AddWithValue("@ItemCode", dtBP.Rows(0)("ProductCode").ToString().Trim())
                                commandSalesDetail.Parameters.AddWithValue("@AvgCost", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@CostValue", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@COSTCODE", strContractNo)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceStatus", strStatus)

                                commandSalesDetail.Parameters.AddWithValue("@DiscAmount", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@LocationId", strLocationId1)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceLocationGroup", strServiceLocationGroup1)

                                commandSalesDetail.Parameters.AddWithValue("@RcnoServiceRecord", strRcnoServiceRecord)
                                commandSalesDetail.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                commandSalesDetail.Parameters.AddWithValue("@ServiceBy", strServiceBy)

                                If String.IsNullOrEmpty(strServiceDate) = True Then
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                                Else
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(strServiceDate).ToString("yyyy-MM-dd"))
                                End If

                                'commandSalesDetail.Parameters.AddWithValue("@ServiceDate", lblidServiceDate.Text)
                                commandSalesDetail.Parameters.AddWithValue("@BillingFrequency", strBillingFrequency)

                                commandSalesDetail.Parameters.AddWithValue("@Address1", dt10.Rows(0)("Address1").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddBuilding", dt10.Rows(0)("AddBuilding").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddStreet", dt10.Rows(0)("AddStreet").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCity", dt10.Rows(0)("AddCity").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddState", dt10.Rows(0)("AddState").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCountry", dt10.Rows(0)("AddCountry").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddPostal", dt10.Rows(0)("AddPostal").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ContractGroup", strContractGroupDept)
                                commandSalesDetail.Parameters.AddWithValue("@Location", strLocation)

                                commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Connection = conn
                                commandSalesDetail.ExecuteNonQuery()


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                Dim sqlLastIdSalesDetail As String
                                sqlLastIdSalesDetail = "SELECT last_insert_id() from tblsalesdetail"

                                Dim commandRcnoSalesDetail As MySqlCommand = New MySqlCommand
                                commandRcnoSalesDetail.CommandType = CommandType.Text
                                commandRcnoSalesDetail.CommandText = sqlLastIdSalesDetail
                                commandRcnoSalesDetail.Parameters.Clear()
                                commandRcnoSalesDetail.Connection = conn
                                txtLastSalesDetailRcno.Text = commandRcnoSalesDetail.ExecuteScalar()

                                commandRcnoSalesDetail.Dispose()

                                'UpdateLastBillDate(strContractNo)
                                'txtLastSalesDetailRcno.Text = "SELECT last_insert_id() from tblsalesdetail"

                                ''''''''''''''''''''''''''''''''' tblSalesDetail

                                ''''''''''''' tblSalesDetail

                                'Header

                                Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetail.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & txtInvoiceNo.Text & "',  BatchNo = '" & txtBatchNo.Text & "', rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text
                                Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & gBillNo & "', BillNo ='" & gBillNo & "', rcnoInvoice = " & txtRcno.Text & "  where Rcno =" & dtTbwservicebillingdetailLocation.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                                commandServiceBillingDetail.Parameters.Clear()
                                commandServiceBillingDetail.Connection = conn
                                commandServiceBillingDetail.ExecuteNonQuery()

                                Dim commandServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetailItem.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & txtInvoiceNo.Text & "' where BatchNo = '" & txtBatchNo.Text & "'"
                                Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & gBillNo & "' where RcnoServiceBillingDetail = " & dtTbwservicebillingdetailLocation.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetailItem.CommandText = sqlUpdateServiceBillingDetailItem
                                commandServiceBillingDetailItem.Parameters.Clear()
                                commandServiceBillingDetailItem.Connection = conn
                                commandServiceBillingDetailItem.ExecuteNonQuery()
                                ''''''''''''''''''''''''''''''
                                rowIndex1 = rowIndex1 + 1
                            Next row1
                            drcommandTbwservicebillingdetail.Close()
                            'conn1.Close()
                            'commandTbwservicebillingdetail..Cancel()
                        End If  ' If strGroupingBy = "ByContractNo" Then

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ElseIf strGroupingBy = "ServiceLocationCode" Then

                        '''''''''''''''''''''''''''''''''
                        sqltbwservicebillingdetail = ""
                        'Dim conn1 As MySqlConnection = New MySqlConnection()
                        'conn1.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                        'conn1.Open()

                        sqltbwservicebillingdetail = "Select *   "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " FROM tbwservicebillingdetail A "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " where 1 = 1 "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " and BillSchedule = '" & txtInvoiceNo.Text.Trim & "' and AccountId = '" & strAccountId.Trim & "'"
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " Order by  ServiceLocationGroup, ServiceDate"

                        'Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand
                        commandTbwservicebillingdetail.CommandType = CommandType.Text
                        commandTbwservicebillingdetail.CommandText = sqltbwservicebillingdetail
                        'commandTbwservicebillingdetail.Connection = ""
                        commandTbwservicebillingdetail.Connection = conn

                        ''Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand

                        drcommandTbwservicebillingdetail = commandTbwservicebillingdetail.ExecuteReader()

                        Dim dtTbwservicebillingdetailLocationCode As New DataTable
                        dtTbwservicebillingdetailLocationCode.Load(drcommandTbwservicebillingdetail)

                        'Dim TotRec = dt1.Rows.Count
                        If dtTbwservicebillingdetailLocationCode.Rows.Count > 0 Then

                            'Dim strAccountId As String
                            'Dim strContactType As String

                            Dim strLocationId As String
                            Dim strContractNo As String
                            Dim strServiceLocationGroup As String

                            'strAccountId = ""
                            strContactType = ""
                            strLocationId = ""
                            strContractNo = ""
                            strServiceLocationGroup = ""

                            '07.04.24
                            Dim strContractGroup As String
                            strContractGroup = ""
                            '07.04.24

                            Dim rowIndex1 = 0

                            '''''''''''''''''''''''''''''''

                            For Each row1 As DataRow In dtTbwservicebillingdetailLocationCode.Rows

                                strLocationId = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("LocationId"))
                                strContractNo = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContractNo"))
                                'strContactType = Convert.ToString(dt1.Rows(rowIndex)("ContactType"))
                                strAccountId = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("AccountId"))
                                strServiceLocationGroup = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ServiceLocationGroup"))

                                strContractGroup = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContractGroup")) '07.04.24

                                strRecordNo = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("RecordNo"))
                                strLocationId1 = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("LocationId"))
                                strServiceLocationGroup1 = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ServiceLocationGroup"))
                                strBillAmount = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount"))
                                strStatus = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("Status"))
                                strGSTAmount = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount"))
                                strTotalWithGST = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("TotalWithGST"))
                                'strNetAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount"))
                                strServiceDate = Convert.ToDateTime(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ServiceDate")).ToString("dd/MM/yy")
                                strServiceBy = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ServiceBy"))
                                strBillingFrequency = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillingFrequency"))
                                strRcnoServiceRecord = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("RcnoServiceRecord"))
                                strContractGroupDept = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContractGroup"))
                                strLocation = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("Location"))

                                '07.04.24
                                strGSTNew = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GST"))
                                strGSTRateNew = Convert.ToString(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTRate"))
                                '07.04.24

                                'If ServiceLocationGroup <> strServiceLocationGroup Then
                                'If ServiceLocationGroup <> strServiceLocationGroup Or ContractGroupGroup <> strContractGroup Then
                                If ServiceLocationGroup <> strServiceLocationGroup Then  '23.06.24

                                    '''''''''''''''''''''''''''

                                    '''''''''''27.10.17   Start: Adjustment of GST decimal value for the last record

                                    If Convert.ToInt32(txtLastSalesDetailRcno.Text) > 0 Then

                                        Dim dblTotalValue As Decimal = 0.0
                                        Dim commandSalesDetailSum As MySqlCommand = New MySqlCommand

                                        commandSalesDetailSum.CommandType = CommandType.Text
                                        commandSalesDetailSum.CommandText = "SELECT sum(GSTBase) as totalvalue FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "'"
                                        commandSalesDetailSum.Connection = conn

                                        Dim drSalesDetailSum As MySqlDataReader = commandSalesDetailSum.ExecuteReader()
                                        Dim dtSalesDetailSum As New DataTable
                                        dtSalesDetailSum.Load(drSalesDetailSum)

                                        dblTotalValue = Convert.ToDouble(dtSalesDetailSum.Rows(0)("totalvalue").ToString)

                                        commandSalesDetailSum.Dispose()

                                        If gstHeader <> dblTotalValue Then
                                            Dim diff As Decimal = 0.0
                                            diff = gstHeader - dblTotalValue

                                            ''''''''''''''''''''''
                                            Dim commandSalesDetailLastRecord As MySqlCommand = New MySqlCommand

                                            commandSalesDetailLastRecord.CommandType = CommandType.Text
                                            commandSalesDetailLastRecord.CommandText = "SELECT GSTBase, AppliedBase FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "' and Rcno = " & txtLastSalesDetailRcno.Text
                                            commandSalesDetailLastRecord.Connection = conn

                                            Dim drSalesDetailLastRecord As MySqlDataReader = commandSalesDetailLastRecord.ExecuteReader()
                                            Dim dtSalesDetailLastRecord As New DataTable
                                            dtSalesDetailLastRecord.Load(drSalesDetailLastRecord)

                                            Dim lastGSTBase As Double = 0.0
                                            Dim lastAppliedBase As Double = 0.0

                                            lastGSTBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("GSTBase").ToString)
                                            lastAppliedBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("AppliedBase").ToString)

                                            ''''''''''''''''''''''''''
                                            lastGSTBase = lastGSTBase + diff
                                            lastAppliedBase = lastAppliedBase + diff
                                            Dim commandAdjustLastRec As MySqlCommand = New MySqlCommand
                                            commandAdjustLastRec.CommandType = CommandType.Text

                                            Dim qryT As String = "UPDATE tblsalesdetail SET  GSTbase = " & lastGSTBase & ", GSTOriginal= " & lastGSTBase & ", AppliedBase = " & lastAppliedBase & ", AppliedOriginal= " & lastAppliedBase & " where rcno = " & txtLastSalesDetailRcno.Text

                                            commandAdjustLastRec.CommandText = qryT
                                            commandAdjustLastRec.Parameters.Clear()
                                            commandAdjustLastRec.Connection = conn
                                            commandAdjustLastRec.ExecuteNonQuery()
                                            commandAdjustLastRec.Dispose()
                                        End If

                                        ' 18.04.20 : Start:Update Service Address in tblSales

                                        Dim commandUpdateServiceAddress As MySqlCommand = New MySqlCommand
                                        commandUpdateServiceAddress.CommandType = CommandType.StoredProcedure
                                        commandUpdateServiceAddress.CommandText = "UpdateTblSalesServiceAddress"

                                        commandUpdateServiceAddress.Parameters.Clear()

                                        commandUpdateServiceAddress.Parameters.AddWithValue("@pr_InvoiceNumber", gBillNo.Trim)

                                        commandUpdateServiceAddress.Connection = conn
                                        commandUpdateServiceAddress.ExecuteScalar()


                                        ' 18.04.20 : End:Update Service Address in tblSales

                                        ''''''''''''''''''''''''''''''''
                                    End If

                                    ' ''''''''''''27.10.17  Start: Adjustment of GST decimal value for the last record

                                    ContractGroup = strContractNo
                                    LocationGroup = strLocationId
                                    AccountIdGroup = strAccountId
                                    ServiceLocationGroup = strServiceLocationGroup

                                    ContractGroupGroup = strContractGroup

                                    GenerateInvoiceNo()


                                    '''''''''''''''''''''''''

                                    If dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContactType").ToString = "COMPANY" Then
                                        sql3 = "Select BillingNameSvc,BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblCompanyLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    Else
                                        sql3 = "Select BillingNameSvc,BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblPersonLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    End If

                                    Dim commandCustomerDetails As MySqlCommand = New MySqlCommand
                                    commandCustomerDetails.CommandType = CommandType.Text
                                    commandCustomerDetails.CommandText = sql3
                                    commandCustomerDetails.Connection = conn

                                    Dim drCustomerDetails As MySqlDataReader = commandCustomerDetails.ExecuteReader()

                                    Dim dtCustomerDetails As New DataTable
                                    dtCustomerDetails.Load(drCustomerDetails)

                                    lBillingNameSvc = ""
                                    lBillAddressSvc = ""
                                    lBillBuildingSvc = ""
                                    lBillStreetSvc = ""
                                    lBillCountrySvc = ""
                                    lBillCitySvc = ""
                                    lBillPOstalSvc = ""
                                    lArTermSvc = ""
                                    lSalesmanSvc = ""
                                    lContact1Svc = ""
                                    lBillStateSvc = ""

                                    If dtCustomerDetails.Rows.Count > 0 Then
                                        lBillingNameSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillingNameSvc"))
                                        lBillAddressSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillAddressSvc"))
                                        lBillBuildingSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillBuildingSvc"))
                                        lBillStreetSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStreetSvc"))
                                        lBillCountrySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCountrySvc"))
                                        lBillCitySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCitySvc"))
                                        lBillPOstalSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillPOstalSvc"))
                                        lArTermSvc = Convert.ToString(dtCustomerDetails.Rows(0)("ArTermSvc"))
                                        lSalesmanSvc = Convert.ToString(dtCustomerDetails.Rows(0)("SalesmanSvc"))
                                        lContact1Svc = Convert.ToString(dtCustomerDetails.Rows(0)("BillContact1Svc"))
                                        lBillStateSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStateSvc"))
                                    End If

                                    commandCustomerDetails.Dispose()
                                    dtCustomerDetails.Dispose()
                                    drCustomerDetails.Close()

                                    ''''''''''''''''''''''''


                                    '' Sales

                                    qrySales = "INSERT INTO tblSales(DocType, InvoiceNumber, CustAttention, CustName, AccountId, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, BillCity, BillState, "
                                    qrySales = qrySales + "  ServiceRecordNo, SalesDate, OurRef,YourRef, PoNo, RcnoServiceRecord,   Salesman, CreditTerms, CreditDays, Terms, TermsDay,"
                                    qrySales = qrySales + "  ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, "
                                    qrySales = qrySales + "  DiscountAmount, GSTAmount, NetAmount, Comments, ContactType, CompanyGroup, GLPeriod, AmountWithDiscount, BatchNo, RecurringInvoice, GLStatus,  BillSchedule, "
                                    qrySales = qrySales + " StaffCode, CustAddress1, CustAddPostal, CustAddCountry, custAddBuilding, CustAddStreet, CustAddCity, CustAddState, DueDate, ContractGroup, LedgerCode, LedgerName, GST, GSTRate, Location,"
                                    qrySales = qrySales + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn,TaxIdentificationNo,SalesTaxRegistrationNo) VALUES "
                                    qrySales = qrySales + "(@DocType, @InvoiceNumber, @CustAttention, @ClientName, @AccountId, @BillAddress1, @BillBuilding, @BillStreet,  @BillCountry, @BillPostal, @BillCity, @BillState,"
                                    qrySales = qrySales + "@ServiceRecordNo, @SalesDate, @ourRef, @YourRef,  @PoNo, @RcnoServiceRecord, @Salesman, @CreditTerms, @CreditDays, @Terms, @TermsDay,"
                                    qrySales = qrySales + " @ValueBase, @ValueOriginal, @GSTBase, @GSTOriginal, @AppliedBase, @AppliedOriginal, @BalanceBase, @BalanceOriginal, "
                                    qrySales = qrySales + " @DiscountAmount, @GSTAmount, @NetAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod, @AmountWithDiscount, @BatchNo, @RecurringInvoice, @GLStatus, @BillSchedule, "
                                    qrySales = qrySales + " @StaffCode, @CustAddress1, @CustAddPostal, @CustAddCountry, @custAddBuilding, @CustAddStreet, @CustAddCity, @CustAddState, @DueDate, @ContractGroup, @LedgerCode, @LedgerName, @GST, @GSTRate, @Location,"
                                    qrySales = qrySales + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn,@TaxIdentificationNo,@SalesTaxRegistrationNo);"

                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContactType").ToString)

                                    'Dim lCreditDays As Decimal
                                    'If String.IsNullOrEmpty(lTerms) = True Then
                                    '    lTerms = "C.O.D."
                                    '    lCreditDays = 0.0
                                    'Else
                                    '    lCreditDays = FindCreditDays(lTerms)
                                    'End If
                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString)

                                    Dim lCreditDays As Decimal
                                    If String.IsNullOrEmpty(lArTermSvc) = True Then
                                        lArTermSvc = "C.O.D."
                                        lCreditDays = 0.0
                                    Else
                                        lCreditDays = FindCreditDays(lArTermSvc)
                                    End If

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    'BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, ""
                                    'qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
                                    'qry = qry + " ContractGroup, Status, Address1, BatchNo,"


                                    'command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                                    command3.Parameters.AddWithValue("@DocType", "ARIN")
                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                    command3.Parameters.AddWithValue("@CustAttention", lContact1Svc)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("CustName").ToString)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("CustName").ToString)
                                    command3.Parameters.AddWithValue("@ClientName", lBillingNameSvc.Trim.ToUpper)

                                    command3.Parameters.AddWithValue("@AccountId", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("AccountId").ToString)

                                    command3.Parameters.AddWithValue("@BillAddress1", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAddress1").ToString)
                                    command3.Parameters.AddWithValue("@BillBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@BillStreet", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillStreet").ToString)
                                    command3.Parameters.AddWithValue("@BillCountry", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillCountry").ToString)
                                    command3.Parameters.AddWithValue("@BillPostal", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillPostal").ToString)
                                    command3.Parameters.AddWithValue("@BillCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@BillState", lBillStateSvc)

                                    'command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                                    command3.Parameters.AddWithValue("@OurRef", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("OurRef").ToString)
                                    command3.Parameters.AddWithValue("@YourRef", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("YourRef").ToString)
                                    command3.Parameters.AddWithValue("@PoNo", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("PoNo").ToString)

                                    command3.Parameters.AddWithValue("@Salesman", lSalesmanSvc.Trim)


                                    If txtBatchDate.Text.Trim = "" Then
                                        command3.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                                    Else
                                        command3.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                                    End If

                                    command3.Parameters.AddWithValue("@Comments", "")
                                    command3.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)
                                    'command3.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                                    command3.Parameters.AddWithValue("@RcnoServiceRecord", 0)


                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString))

                                    command3.Parameters.AddWithValue("@ContactType", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("ContactType").ToString)
                                    command3.Parameters.AddWithValue("@CompanyGroup", dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("CompanyGroup").ToString)
                                    command3.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)
                                    command3.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                                    command3.Parameters.AddWithValue("@GLStatus", "O")

                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If

                                    command3.Parameters.AddWithValue("@CreditTerms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)

                                    command3.Parameters.AddWithValue("@Terms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@TermsDay", lCreditDays)


                                    'If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                    '    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditDays", 0)
                                    'End If


                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If


                                    'If String.IsNullOrEmpty(txtCreditDays.Text) = False Then
                                    '    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditDays", 0)
                                    'End If


                                    If chkRecurringInvoice.Checked = True Then
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "Y")
                                    Else
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "N")
                                    End If
                                    command3.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                    command3.Parameters.AddWithValue("@StaffCode", lSalesmanSvc)
                                    command3.Parameters.AddWithValue("@CustAddress1", lBillAddressSvc)
                                    command3.Parameters.AddWithValue("@CustAddPostal", lBillPOstalSvc)
                                    command3.Parameters.AddWithValue("@CustAddCountry", lBillCountrySvc)

                                    command3.Parameters.AddWithValue("@CustAddBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@CustAddStreet", lBillStreetSvc)
                                    command3.Parameters.AddWithValue("@CustAddCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@CustAddState", lBillStateSvc)
                                    command3.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(lCreditDays))
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)
                                    command3.Parameters.AddWithValue("@ContractGroup", ContractGroupGroup)
                                    '07.04.24

                                    command3.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                    command3.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                    'command3.Parameters.AddWithValue("@GST", "SR")
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))
                                    'command3.Parameters.AddWithValue("@Location", strLocation)

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    command3.Parameters.AddWithValue("@Gst", strGSTNew)
                                    command3.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))
                                    '07.04.24


                                    command3.Parameters.AddWithValue("@Location", txtLocation.Text)

                                    '08.03.17
                                    command3.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                    'command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@TaxIdentificationNo", strTIN)
                                    command3.Parameters.AddWithValue("@SalesTaxRegistrationNo", strSST)


                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()

                                    Dim sqlLastId As String
                                    sqlLastId = "SELECT last_insert_id() from tblSales"

                                    Dim commandRcno As MySqlCommand = New MySqlCommand
                                    commandRcno.CommandType = CommandType.Text
                                    commandRcno.CommandText = sqlLastId
                                    commandRcno.Parameters.Clear()
                                    commandRcno.Connection = conn
                                    txtRcno.Text = commandRcno.ExecuteScalar()

                                    ToBillAmt = Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString)
                                    DiscAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17
                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    ''Start: Update tblServiceRecord
                                    'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                                    'commandUpdateServiceRecord.CommandType = CommandType.Text

                                    ''Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                                    'Dim qryUpdateServiceRecord As String = "Update tblservicerecord Set BillYN = 'Y', BillNo = '" & gBillNo & "', BilledAmt = " & Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString) & "  where Rcno= @Rcno "

                                    'commandUpdateServiceRecord.CommandText = qryUpdateServiceRecord
                                    'commandUpdateServiceRecord.Parameters.Clear()

                                    'commandUpdateServiceRecord.Parameters.AddWithValue("@Rcno", strRcnoServiceRecord)
                                    'commandUpdateServiceRecord.Connection = conn
                                    'commandUpdateServiceRecord.ExecuteNonQuery()

                                    ''End: Update tblServiceRecord

                                Else
                                    ToBillAmt = ToBillAmt + Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("BillAmount").ToString)

                                    DiscAmount = DiscAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = GSTAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = NetAmount + Convert.ToDecimal(dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17
                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    NetAmount = gstHeader + Convert.ToDecimal(ToBillAmt)

                                    qrySales = "UPDATE tblSales SET ValueBase=@ValueBase, ValueOriginal = @ValueOriginal, AppliedBase =@AppliedBase, BalanceBase=@BalanceBase, BalanceOriginal = @BalanceOriginal, "
                                    qrySales = qrySales + " GSTBase =@GSTBase, GSTOriginal = @GSTOriginal, AppliedOriginal = @AppliedOriginal, DiscountAmount =@DiscountAmount, AmountWithDiscount=@AmountWithDiscount, GSTAmount=@GSTAmount, NetAmount=@NetAmount"
                                    qrySales = qrySales + " where InvoiceNumber= @InvoiceNumber "

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    '27.10.17  End: Adjustment of GST decimal value for the last record

                                    'command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    'command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))
                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()
                                End If  ' If ContractGroup <> strContractNo Then

                                '''''''''''''''''''''''''''''''


                                '''''''''''''' tblSalesDetail

                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text

                                Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
                                qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
                                qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
                                qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, BillSchedule, ServiceBy, ServiceDate, BillingFrequency, ItemDescription, ContractGroup,"
                                qryDetail = qryDetail + " Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, Location,"
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                                qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                                qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                                qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @BillSchedule, @ServiceBy, @ServiceDate, @BillingFrequency, @ItemDescription, @ContractGroup,"
                                qryDetail = qryDetail + " @Address1, @AddBuilding, @AddStreet, @AddCity, @AddState, @AddCountry, @AddPostal, @Location, "
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                commandSalesDetail.CommandText = qryDetail
                                commandSalesDetail.Parameters.Clear()

                                commandSalesDetail.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                commandSalesDetail.Parameters.AddWithValue("@Sequence", 0)

                                'If lblidItemType.Text = "SERVICE" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERV")
                                'ElseIf lblidItemType.Text = "STOCK" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "STCK")
                                'ElseIf lblidItemType.Text = "OTHERS" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "DIST")
                                'End If
                                'Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                                commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERVICE")

                                ''''''''''''''''''''''''''''''''''''''
                                Dim commandBP As MySqlCommand = New MySqlCommand
                                commandBP.CommandType = CommandType.Text

                                If String.IsNullOrEmpty(strContractNo) = False Then
                                    If strStatus = "P" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                                    ElseIf strStatus = "O" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                                    End If
                                Else
                                    commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-COO'"
                                End If

                                commandBP.Connection = conn

                                Dim drBP As MySqlDataReader = commandBP.ExecuteReader()
                                Dim dtBP As New DataTable
                                dtBP.Load(drBP)

                                ''''''''''''''''''''''''''''''''''''

                                'commandSalesDetail.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                'commandSalesDetail.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                commandSalesDetail.Parameters.AddWithValue("@LedgerCode", dtBP.Rows(0)("COACode").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@LedgerName", dtBP.Rows(0)("COADescription").ToString())

                                commandSalesDetail.Parameters.AddWithValue("@SubLedgerCode", "")
                                commandSalesDetail.Parameters.AddWithValue("@SONUMBER", "")
                                commandSalesDetail.Parameters.AddWithValue("@RefType", strRecordNo)

                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                '07.04.24
                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                commandSalesDetail.Parameters.AddWithValue("@Gst", strGSTNew)
                                commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))

                                '07.04.24

                                commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                                commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                                commandSalesDetail.Parameters.AddWithValue("@Quantity", 1)
                                commandSalesDetail.Parameters.AddWithValue("@UnitMs", "XOJ")
                                commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@DiscP", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@TaxBase", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@GstOriginal", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@GstBase", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@AppliedBase", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(strBillAmount))

                                '''''''''''''''''''''''''''''''
                                Dim command10 As MySqlCommand = New MySqlCommand
                                command10.CommandType = CommandType.Text
                                command10.CommandText = "SELECT Rcno, Status, ContractNo, Description, ServiceDate, BillAmount, LocationID, ServiceLocationGroup, Notes, Address1, AddBuilding, AddStreet, AddCity, AddCountry, AddState,  AddPostal FROM tblservicerecord where RecordNo =@RecordNo"
                                command10.Parameters.AddWithValue("@RecordNo", strRecordNo)
                                command10.Connection = conn

                                Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                                Dim dt10 As New DataTable
                                dt10.Load(dr10)


                                commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate + ", " + dt10.Rows(0)("Notes").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ItemDescription", dtBP.Rows(0)("Description").ToString())
                                'commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate)
                                commandSalesDetail.Parameters.AddWithValue("@Comments", "")
                                commandSalesDetail.Parameters.AddWithValue("@GroupId", "")
                                commandSalesDetail.Parameters.AddWithValue("@DetailID", "")
                                commandSalesDetail.Parameters.AddWithValue("@GrpDetName", "")

                                commandSalesDetail.Parameters.AddWithValue("@SoCode", 0.0)
                                'commandSalesDetail.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                                commandSalesDetail.Parameters.AddWithValue("@ItemCode", dtBP.Rows(0)("ProductCode").ToString().Trim())
                                commandSalesDetail.Parameters.AddWithValue("@AvgCost", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@CostValue", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@COSTCODE", strContractNo)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceStatus", strStatus)

                                commandSalesDetail.Parameters.AddWithValue("@DiscAmount", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@LocationId", strLocationId1)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceLocationGroup", strServiceLocationGroup1)

                                commandSalesDetail.Parameters.AddWithValue("@RcnoServiceRecord", strRcnoServiceRecord)
                                commandSalesDetail.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                commandSalesDetail.Parameters.AddWithValue("@ServiceBy", strServiceBy)

                                If String.IsNullOrEmpty(strServiceDate) = True Then
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                                Else
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(strServiceDate).ToString("yyyy-MM-dd"))
                                End If

                                'commandSalesDetail.Parameters.AddWithValue("@ServiceDate", lblidServiceDate.Text)
                                commandSalesDetail.Parameters.AddWithValue("@BillingFrequency", strBillingFrequency)

                                commandSalesDetail.Parameters.AddWithValue("@Address1", dt10.Rows(0)("Address1").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddBuilding", dt10.Rows(0)("AddBuilding").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddStreet", dt10.Rows(0)("AddStreet").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCity", dt10.Rows(0)("AddCity").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddState", dt10.Rows(0)("AddState").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCountry", dt10.Rows(0)("AddCountry").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddPostal", dt10.Rows(0)("AddPostal").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ContractGroup", strContractGroupDept)
                                commandSalesDetail.Parameters.AddWithValue("@Location", strLocation)

                                commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Connection = conn
                                commandSalesDetail.ExecuteNonQuery()


                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                Dim sqlLastIdSalesDetail As String
                                sqlLastIdSalesDetail = "SELECT last_insert_id() from tblsalesdetail"

                                Dim commandRcnoSalesDetail As MySqlCommand = New MySqlCommand
                                commandRcnoSalesDetail.CommandType = CommandType.Text
                                commandRcnoSalesDetail.CommandText = sqlLastIdSalesDetail
                                commandRcnoSalesDetail.Parameters.Clear()
                                commandRcnoSalesDetail.Connection = conn
                                txtLastSalesDetailRcno.Text = commandRcnoSalesDetail.ExecuteScalar()

                                commandRcnoSalesDetail.Dispose()

                                'UpdateLastBillDate(strContractNo)

                                'txtLastSalesDetailRcno.Text = "SELECT last_insert_id() from tblsalesdetail"

                                ''''''''''''''''''''''''''''''''' tblSalesDetail
                                ''''''''''''' tblSalesDetail

                                'Header

                                Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetail.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & txtInvoiceNo.Text & "',  BatchNo = '" & txtBatchNo.Text & "', rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text
                                Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & gBillNo & "', BillNo ='" & gBillNo & "', rcnoInvoice = " & txtRcno.Text & "  where Rcno =" & dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                                commandServiceBillingDetail.Parameters.Clear()
                                commandServiceBillingDetail.Connection = conn
                                commandServiceBillingDetail.ExecuteNonQuery()

                                Dim commandServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetailItem.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & txtInvoiceNo.Text & "' where BatchNo = '" & txtBatchNo.Text & "'"
                                Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & gBillNo & "' where RcnoServiceBillingDetail = " & dtTbwservicebillingdetailLocationCode.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetailItem.CommandText = sqlUpdateServiceBillingDetailItem
                                commandServiceBillingDetailItem.Parameters.Clear()
                                commandServiceBillingDetailItem.Connection = conn
                                commandServiceBillingDetailItem.ExecuteNonQuery()
                                ''''''''''''''''''''''''''''''
                                rowIndex1 = rowIndex1 + 1
                            Next row1
                            drcommandTbwservicebillingdetail.Close()
                            'conn1.Close()
                            'commandTbwservicebillingdetail..Cancel()
                        End If  ' If strGroupingBy = "ByContractNo" Then

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ElseIf strGroupingBy = "AccountID" Then

                        '''''''''''''''''''''''''''''''''
                        sqltbwservicebillingdetail = "Select *   "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " FROM tbwservicebillingdetail A "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " where 1 = 1 "
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " and BillSchedule = '" & txtInvoiceNo.Text & "' and AccountId = '" & strAccountId & "'"
                        sqltbwservicebillingdetail = sqltbwservicebillingdetail + " Order by  ServiceDate"

                        'Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand
                        commandTbwservicebillingdetail.CommandType = CommandType.Text
                        commandTbwservicebillingdetail.CommandText = sqltbwservicebillingdetail
                        commandTbwservicebillingdetail.Connection = conn

                        ''Dim commandTbwservicebillingdetail As MySqlCommand = New MySqlCommand

                        drcommandTbwservicebillingdetail = commandTbwservicebillingdetail.ExecuteReader()

                        Dim dtTbwservicebillingdetailAccountId As New DataTable
                        dtTbwservicebillingdetailAccountId.Load(drcommandTbwservicebillingdetail)

                        'Dim TotRec = dt1.Rows.Count
                        If dtTbwservicebillingdetailAccountId.Rows.Count > 0 Then

                            'Dim strAccountId As String
                            'Dim strContactType As String

                            Dim strLocationId As String
                            Dim strContractNo As String

                            ''07.04.24
                            'Dim strContractGroup As String
                            'strContractGroup = ""
                            ''07.04.24

                            'strAccountId = ""
                            strContactType = ""
                            strLocationId = ""
                            strContractNo = ""


                            '07.04.24
                            Dim strContractGroup As String
                            strContractGroup = ""
                            '07.04.24

                            Dim rowIndex1 = 0

                            '''''''''''''''''''''''''''''''

                            For Each row1 As DataRow In dtTbwservicebillingdetailAccountId.Rows

                                strLocationId = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("LocationId"))
                                strContractNo = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContractNo"))
                                strAccountId = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("AccountId"))

                                strContractGroup = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContractGroup")) '07.04.24

                                strRecordNo = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("RecordNo"))
                                strLocationId1 = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("LocationId"))
                                strServiceLocationGroup1 = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ServiceLocationGroup"))
                                strBillAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount"))
                                strStatus = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("Status"))
                                strGSTAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount"))
                                strTotalWithGST = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("TotalWithGST"))
                                'strNetAmount = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount"))
                                strServiceDate = Convert.ToDateTime(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ServiceDate")).ToString("dd/MM/yy")
                                strServiceBy = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ServiceBy"))
                                strBillingFrequency = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillingFrequency"))
                                strRcnoServiceRecord = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("RcnoServiceRecord"))
                                strContractGroupDept = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContractGroup"))
                                strLocation = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("Location"))

                                '07.04.24
                                strGSTNew = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GST"))
                                strGSTRateNew = Convert.ToString(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTRate"))
                                '07.04.24

                                'If AccountIdGroup <> strAccountId Then
                                'If AccountIdGroup <> strAccountId Or ContractGroupGroup <> strContractGroup Then
                                If AccountIdGroup <> strAccountId Then   '23.06.24


                                    '''''''''''27.10.17   Start: Adjustment of GST decimal value for the last record

                                    If Convert.ToInt32(txtLastSalesDetailRcno.Text) > 0 Then

                                        Dim dblTotalValue As Decimal = 0.0
                                        Dim commandSalesDetailSum As MySqlCommand = New MySqlCommand

                                        commandSalesDetailSum.CommandType = CommandType.Text
                                        commandSalesDetailSum.CommandText = "SELECT sum(GSTBase) as totalvalue FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "'"
                                        commandSalesDetailSum.Connection = conn

                                        Dim drSalesDetailSum As MySqlDataReader = commandSalesDetailSum.ExecuteReader()
                                        Dim dtSalesDetailSum As New DataTable
                                        dtSalesDetailSum.Load(drSalesDetailSum)

                                        dblTotalValue = Convert.ToDouble(dtSalesDetailSum.Rows(0)("totalvalue").ToString)

                                        commandSalesDetailSum.Dispose()

                                        If gstHeader <> dblTotalValue Then
                                            Dim diff As Decimal = 0.0
                                            diff = gstHeader - dblTotalValue

                                            ''''''''''''''''''''''
                                            Dim commandSalesDetailLastRecord As MySqlCommand = New MySqlCommand

                                            commandSalesDetailLastRecord.CommandType = CommandType.Text
                                            commandSalesDetailLastRecord.CommandText = "SELECT GSTBase, AppliedBase FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "' and Rcno = " & txtLastSalesDetailRcno.Text
                                            commandSalesDetailLastRecord.Connection = conn

                                            Dim drSalesDetailLastRecord As MySqlDataReader = commandSalesDetailLastRecord.ExecuteReader()
                                            Dim dtSalesDetailLastRecord As New DataTable
                                            dtSalesDetailLastRecord.Load(drSalesDetailLastRecord)

                                            Dim lastGSTBase As Double = 0.0
                                            Dim lastAppliedBase As Double = 0.0

                                            lastGSTBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("GSTBase").ToString)
                                            lastAppliedBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("AppliedBase").ToString)

                                            ''''''''''''''''''''''''''
                                            lastGSTBase = lastGSTBase + diff
                                            lastAppliedBase = lastAppliedBase + diff
                                            Dim commandAdjustLastRec As MySqlCommand = New MySqlCommand
                                            commandAdjustLastRec.CommandType = CommandType.Text

                                            Dim qryT As String = "UPDATE tblsalesdetail SET  GSTbase = " & lastGSTBase & ", GSTOriginal= " & lastGSTBase & ", AppliedBase = " & lastAppliedBase & ", AppliedOriginal= " & lastAppliedBase & " where rcno = " & txtLastSalesDetailRcno.Text

                                            commandAdjustLastRec.CommandText = qryT
                                            commandAdjustLastRec.Parameters.Clear()
                                            commandAdjustLastRec.Connection = conn
                                            commandAdjustLastRec.ExecuteNonQuery()
                                            commandAdjustLastRec.Dispose()
                                        End If

                                        '''''''''''''''''''''''''''

                                        ' 18.04.20 : Start:Update Service Address in tblSales

                                        Dim commandUpdateServiceAddress As MySqlCommand = New MySqlCommand
                                        commandUpdateServiceAddress.CommandType = CommandType.StoredProcedure
                                        commandUpdateServiceAddress.CommandText = "UpdateTblSalesServiceAddress"

                                        commandUpdateServiceAddress.Parameters.Clear()

                                        commandUpdateServiceAddress.Parameters.AddWithValue("@pr_InvoiceNumber", gBillNo.Trim)

                                        commandUpdateServiceAddress.Connection = conn
                                        commandUpdateServiceAddress.ExecuteScalar()


                                        ' 18.04.20 : End:Update Service Address in tblSales

                                        ''''''''''''''''''''''''''''''''
                                    End If

                                    ' ''''''''''''27.10.17  Start: Adjustment of GST decimal value for the last record

                                    ContractGroup = strContractNo
                                    LocationGroup = strLocationId
                                    AccountIdGroup = strAccountId

                                    ContractGroupGroup = strContractGroup

                                    GenerateInvoiceNo()

                                    If dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContactType").ToString = "COMPANY" Then
                                        sql3 = "Select BillingNameSvc, BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblCompanyLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    Else
                                        sql3 = "Select BillingNameSvc, BillAddressSvc, BillBuildingSvc, BillStreetSvc, BillCountrySvc, BillCitySvc,  BillPOstalSvc, BillContact1Svc, ArTermSvc, SalesmanSvc, BillStateSvc  "
                                        sql3 = sql3 + " FROM tblPersonLocation A "
                                        sql3 = sql3 + " where 1 = 1 "
                                        sql3 = sql3 + " and AccountID = '" & dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("AccountId").ToString & "' and LocationID = '" & strLocationId1 & "'"
                                    End If

                                    Dim commandCustomerDetails As MySqlCommand = New MySqlCommand
                                    commandCustomerDetails.CommandType = CommandType.Text
                                    commandCustomerDetails.CommandText = sql3
                                    commandCustomerDetails.Connection = conn

                                    Dim drCustomerDetails As MySqlDataReader = commandCustomerDetails.ExecuteReader()

                                    Dim dtCustomerDetails As New DataTable
                                    dtCustomerDetails.Load(drCustomerDetails)

                                    lBillingNameSvc = ""
                                    lBillAddressSvc = ""
                                    lBillBuildingSvc = ""
                                    lBillStreetSvc = ""
                                    lBillCountrySvc = ""
                                    lBillCitySvc = ""
                                    lBillPOstalSvc = ""
                                    lArTermSvc = ""
                                    lSalesmanSvc = ""
                                    lContact1Svc = ""
                                    lBillStateSvc = ""

                                    If dtCustomerDetails.Rows.Count > 0 Then
                                        lBillingNameSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillingNameSvc"))
                                        lBillAddressSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillAddressSvc"))
                                        lBillBuildingSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillBuildingSvc"))
                                        lBillStreetSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStreetSvc"))
                                        lBillCountrySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCountrySvc"))
                                        lBillCitySvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillCitySvc"))
                                        lBillPOstalSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillPOstalSvc"))
                                        lArTermSvc = Convert.ToString(dtCustomerDetails.Rows(0)("ArTermSvc"))
                                        lSalesmanSvc = Convert.ToString(dtCustomerDetails.Rows(0)("SalesmanSvc"))
                                        lContact1Svc = Convert.ToString(dtCustomerDetails.Rows(0)("BillContact1Svc"))
                                        lBillStateSvc = Convert.ToString(dtCustomerDetails.Rows(0)("BillStateSvc"))
                                    End If

                                    commandCustomerDetails.Dispose()
                                    dtCustomerDetails.Dispose()
                                    drCustomerDetails.Close()

                                    ''''''''''''''''''''''''
                                    '' Sales

                                    qrySales = "INSERT INTO tblSales(DocType, InvoiceNumber, CustAttention,CustName, AccountId, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, BillCity, BillState, "
                                    qrySales = qrySales + "  ServiceRecordNo, SalesDate, OurRef,YourRef, PoNo, RcnoServiceRecord,   Salesman, CreditTerms, CreditDays, Terms, TermsDay,"
                                    qrySales = qrySales + "  ValueBase, ValueOriginal, GSTBase, GSTOriginal, AppliedBase, AppliedOriginal, BalanceBase, BalanceOriginal, "
                                    qrySales = qrySales + "  DiscountAmount, GSTAmount, NetAmount, Comments, ContactType, CompanyGroup, GLPeriod, AmountWithDiscount, BatchNo, RecurringInvoice, GLStatus, BillSchedule,  "
                                    qrySales = qrySales + " StaffCode, CustAddress1, CustAddPostal, CustAddCountry, custAddBuilding, CustAddStreet, CustAddCity, CustAddState, DueDate, ContractGroup, LedgerCode, LedgerName, GST, GSTRate, Location,"
                                    qrySales = qrySales + "CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn,TaxIdentificationNo,SalesTaxRegistrationNo) VALUES "
                                    qrySales = qrySales + "(@DocType, @InvoiceNumber, @CustAttention, @ClientName, @AccountId, @BillAddress1, @BillBuilding, @BillStreet,  @BillCountry, @BillPostal, @BillCity, @BillState, "
                                    qrySales = qrySales + "@ServiceRecordNo, @SalesDate, @ourRef, @YourRef,  @PoNo, @RcnoServiceRecord,  @Salesman, @CreditTerms, @CreditDays, @Terms, @TermsDay, "
                                    qrySales = qrySales + " @ValueBase, @ValueOriginal, @GSTBase, @GSTOriginal, @AppliedBase, @AppliedOriginal, @BalanceBase, @BalanceOriginal, "
                                    qrySales = qrySales + " @DiscountAmount, @GSTAmount, @NetAmount, @Comments, @ContactType, @CompanyGroup, @GLPeriod, @AmountWithDiscount, @BatchNo, @RecurringInvoice, @GLStatus, @BillSchedule, "
                                    qrySales = qrySales + " @StaffCode, @CustAddress1, @CustAddPostal, @CustAddCountry, @custAddBuilding, @CustAddStreet,@CustAddCity, @CustAddState, @DueDate,@ContractGroup, @LedgerCode, @LedgerName, @GST, @GSTRate, @Location, "
                                    qrySales = qrySales + "@CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn,@TaxIdentificationNo,@SalesTaxRegistrationNo);"


                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContactType").ToString)

                                    'Dim lCreditDays As Decimal
                                    'If String.IsNullOrEmpty(lTerms) = True Then
                                    '    lTerms = "C.O.D."
                                    '    lCreditDays = 0.0
                                    'Else
                                    '    lCreditDays = FindCreditDays(lTerms)
                                    'End If

                                    'Dim lTerms As String = FindTerms(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("AccountId").ToString, dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("ContactType").ToString)

                                    Dim lCreditDays As Decimal
                                    If String.IsNullOrEmpty(lArTermSvc) = True Then
                                        lArTermSvc = "C.O.D."
                                        lCreditDays = 0.0
                                    Else
                                        lCreditDays = FindCreditDays(lArTermSvc)
                                    End If

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    'command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNo.Text)
                                    command3.Parameters.AddWithValue("@DocType", "ARIN")
                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                    command3.Parameters.AddWithValue("@CustAttention", lContact1Svc)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("CustName").ToString)
                                    'command3.Parameters.AddWithValue("@ClientName", dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("CustName").ToString)
                                    command3.Parameters.AddWithValue("@ClientName", lBillingNameSvc.Trim.ToUpper)

                                    command3.Parameters.AddWithValue("@AccountId", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("AccountId").ToString)

                                    command3.Parameters.AddWithValue("@BillAddress1", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAddress1").ToString)
                                    command3.Parameters.AddWithValue("@BillBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@BillStreet", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillStreet").ToString)
                                    command3.Parameters.AddWithValue("@BillCountry", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillCountry").ToString)
                                    command3.Parameters.AddWithValue("@BillPostal", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillPostal").ToString)
                                    command3.Parameters.AddWithValue("@BillCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@BillState", lBillStateSvc)

                                    'command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

                                    command3.Parameters.AddWithValue("@OurRef", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("OurRef").ToString)
                                    command3.Parameters.AddWithValue("@YourRef", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("YourRef").ToString)
                                    command3.Parameters.AddWithValue("@PoNo", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("PoNo").ToString)

                                    command3.Parameters.AddWithValue("@Salesman", lSalesmanSvc.Trim)


                                    If txtBatchDate.Text.Trim = "" Then
                                        command3.Parameters.AddWithValue("@SalesDate", DBNull.Value)
                                    Else
                                        command3.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                                    End If

                                    command3.Parameters.AddWithValue("@Comments", "")
                                    command3.Parameters.AddWithValue("@ServiceRecordNo", txtRecordNo.Text)
                                    'command3.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
                                    command3.Parameters.AddWithValue("@RcnoServiceRecord", 0)


                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text) - Convert.ToDecimal(txtDiscountAmount.Text))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(dtTbwservicebillingdetailContractNo.Rows(rowIndex1)("BillAmount").ToString))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString) - Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount").ToString))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString))


                                    command3.Parameters.AddWithValue("@ContactType", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("ContactType").ToString)
                                    command3.Parameters.AddWithValue("@CompanyGroup", dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("CompanyGroup").ToString)
                                    command3.Parameters.AddWithValue("@GLPeriod", txtBillingPeriod.Text)
                                    command3.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)
                                    command3.Parameters.AddWithValue("@GLStatus", "O")


                                    'If ddlCreditTerms.Text = "-1" Then
                                    '    command3.Parameters.AddWithValue("@CreditTerms", "")
                                    'Else
                                    '    command3.Parameters.AddWithValue("@CreditTerms", ddlCreditTerms.Text)
                                    'End If

                                    command3.Parameters.AddWithValue("@CreditTerms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@CreditDays", lCreditDays)

                                    command3.Parameters.AddWithValue("@Terms", lArTermSvc)
                                    command3.Parameters.AddWithValue("@TermsDay", lCreditDays)


                                    If chkRecurringInvoice.Checked = True Then
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "Y")
                                    Else
                                        command3.Parameters.AddWithValue("@RecurringInvoice", "N")
                                    End If

                                    command3.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                    command3.Parameters.AddWithValue("@StaffCode", lSalesmanSvc)
                                    command3.Parameters.AddWithValue("@CustAddress1", lBillAddressSvc)
                                    command3.Parameters.AddWithValue("@CustAddPostal", lBillPOstalSvc)
                                    command3.Parameters.AddWithValue("@CustAddCountry", lBillCountrySvc)

                                    command3.Parameters.AddWithValue("@CustAddBuilding", Left(lBillBuildingSvc.Trim + " " + lBillCitySvc.Trim + " " + lBillStateSvc.Trim, 100))
                                    command3.Parameters.AddWithValue("@CustAddStreet", lBillStreetSvc)

                                    command3.Parameters.AddWithValue("@CustAddCity", lBillCitySvc)
                                    command3.Parameters.AddWithValue("@CustAddState", lBillStateSvc)

                                    command3.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(txtInvoiceDate.Text).AddDays(lCreditDays))

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@ContractGroup", ddlContractGroup.Text)
                                    command3.Parameters.AddWithValue("@ContractGroup", ContractGroupGroup)
                                    '07.04.24

                                    command3.Parameters.AddWithValue("@LedgerCode", txtARCode.Text)
                                    command3.Parameters.AddWithValue("@LedgerName", txtARDescription.Text)

                                    'command3.Parameters.AddWithValue("@GST", "SR")

                                    '07.04.24
                                    'command3.Parameters.AddWithValue("@GST", txtDefaultTaxCode.Text)
                                    'command3.Parameters.AddWithValue("@GSTRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                    command3.Parameters.AddWithValue("@Gst", strGSTNew)
                                    command3.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))
                                    '07.04.24

                                    'command3.Parameters.AddWithValue("@Location", strLocation)
                                    command3.Parameters.AddWithValue("@Location", txtLocation.Text)

                                    '08.03.17
                                    command3.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                    command3.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                    command3.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                    command3.Parameters.AddWithValue("@TaxIdentificationNo", strTIN)
                                    command3.Parameters.AddWithValue("@SalesTaxRegistrationNo", strSST)

                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()

                                    Dim sqlLastId As String
                                    sqlLastId = "SELECT last_insert_id() from tblSales"

                                    Dim commandRcno As MySqlCommand = New MySqlCommand
                                    commandRcno.CommandType = CommandType.Text
                                    commandRcno.CommandText = sqlLastId
                                    commandRcno.Parameters.Clear()
                                    commandRcno.Connection = conn
                                    txtRcno.Text = commandRcno.ExecuteScalar()

                                    ToBillAmt = Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString)

                                    DiscAmount = Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17

                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    'NetAmount = gstHeader + Convert.ToDecimal(ToBillAmt)

                                    '27.10.17 End: Adjustment of GST decimal value for the last record

                                    ''Start: Update tblServiceRecord
                                    'Dim commandUpdateServiceRecord As MySqlCommand = New MySqlCommand
                                    'commandUpdateServiceRecord.CommandType = CommandType.Text

                                    ''Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                                    'Dim qryUpdateServiceRecord As String = "Update tblservicerecord Set BillYN = 'Y', BillNo = '" & gBillNo & "', BilledAmt = " & Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString) & "  where Rcno= @Rcno "

                                    'commandUpdateServiceRecord.CommandText = qryUpdateServiceRecord
                                    'commandUpdateServiceRecord.Parameters.Clear()

                                    'commandUpdateServiceRecord.Parameters.AddWithValue("@Rcno", strRcnoServiceRecord)
                                    'commandUpdateServiceRecord.Connection = conn
                                    'commandUpdateServiceRecord.ExecuteNonQuery()

                                    ''End: Update tblServiceRecord

                                Else

                                    ToBillAmt = ToBillAmt + Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("BillAmount").ToString)

                                    DiscAmount = DiscAmount + Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("DiscountAmount").ToString)
                                    GSTAmount = GSTAmount + Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("GSTAmount").ToString)
                                    NetAmount = NetAmount + Convert.ToDecimal(dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("NetAmount").ToString)

                                    '27.10.17 Start: Adjustment of GST decimal value for the last record
                                    'gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01).ToString("N2")  '27.10.17

                                    gstHeader = Convert.ToDecimal(ToBillAmt * Convert.ToDecimal(strGSTRateNew) * 0.01).ToString("N2")  '07.04.24
                                    NetAmount = gstHeader + Convert.ToDecimal(ToBillAmt)

                                    qrySales = "UPDATE tblSales SET ValueBase=@ValueBase, ValueOriginal = @ValueOriginal, AppliedBase =@AppliedBase, BalanceBase=@BalanceBase, BalanceOriginal = @BalanceOriginal, "
                                    qrySales = qrySales + " GSTBase =@GSTBase, GSTOriginal = @GSTOriginal, AppliedOriginal = @AppliedOriginal, DiscountAmount =@DiscountAmount, AmountWithDiscount=@AmountWithDiscount, GSTAmount=@GSTAmount, NetAmount=@NetAmount"
                                    qrySales = qrySales + " where InvoiceNumber= @InvoiceNumber "

                                    command3.CommandText = qrySales
                                    command3.Parameters.Clear()

                                    command3.Parameters.AddWithValue("@InvoiceNumber", gBillNo)

                                    command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(gstHeader))
                                    command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    '27.10.17  End: Adjustment of GST decimal value for the last record

                                    'command3.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(ToBillAmt))
                                    'command3.Parameters.AddWithValue("@GSTBase", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@GSTOriginal", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@AppliedBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@AppliedOriginal", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))
                                    'command3.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(GSTAmount))
                                    'command3.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(ToBillAmt) - Convert.ToDecimal(DiscAmount))

                                    'command3.Parameters.AddWithValue("@BalanceBase", Convert.ToDecimal(NetAmount))
                                    'command3.Parameters.AddWithValue("@BalanceOriginal", Convert.ToDecimal(NetAmount))

                                    command3.Connection = conn
                                    command3.ExecuteNonQuery()
                                End If  ' If ContractGroup <> strContractNo Then

                                '''''''''''''''''''''''''''''''

                                '''''''''''''' tblSalesDetail

                                Dim commandSalesDetail As MySqlCommand = New MySqlCommand

                                commandSalesDetail.CommandType = CommandType.Text

                                Dim qryDetail As String = "INSERT INTO tblSalesDetail(InvoiceNumber, Sequence, SubCode, LedgerCode, LedgerName, SubLedgerCode, SONUMBER, RefType, Gst, "
                                qryDetail = qryDetail + " GstRate, ExchangeRate, Currency, Quantity, UnitMs, UnitOriginal, UnitBase,  DiscP, TaxBase, GstOriginal,"
                                qryDetail = qryDetail + " GstBase, ValueOriginal, ValueBase, AppliedOriginal, AppliedBase, Description, Comments, GroupId, "
                                qryDetail = qryDetail + " DetailID, GrpDetName, SoCode, ItemCode, AvgCost, CostValue, COSTCODE, ServiceStatus, DiscAmount, TotalPrice, LocationId, ServiceLocationGroup, RcnoServiceRecord, BillSchedule, ServiceBy, ServiceDate, BillingFrequency, ItemDescription, ContractGroup,"
                                qryDetail = qryDetail + " Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal, Location,"
                                qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
                                qryDetail = qryDetail + "(@InvoiceNumber, @Sequence, @SubCode, @LedgerCode, @LedgerName, @SubLedgerCode, @SONUMBER, @RefType, @Gst,"
                                qryDetail = qryDetail + " @GstRate, @ExchangeRate, @Currency, @Quantity, @UnitMs, @UnitOriginal, @UnitBase,  @DiscP, @TaxBase, @GstOriginal, "
                                qryDetail = qryDetail + " @GstBase, @ValueOriginal, @ValueBase, @AppliedOriginal, @AppliedBase, @Description, @Comments, @GroupId, "
                                qryDetail = qryDetail + " @DetailID, @GrpDetName, @SoCode, @ItemCode, @AvgCost, @CostValue, @COSTCODE, @ServiceStatus, @DiscAmount, @TotalPrice, @LocationId, @ServiceLocationGroup, @RcnoServiceRecord, @BillSchedule, @ServiceBy, @ServiceDate, @BillingFrequency, @ItemDescription, @ContractGroup,"
                                qryDetail = qryDetail + " @Address1, @AddBuilding, @AddStreet, @AddCity, @AddState, @AddCountry, @AddPostal, @Location,"
                                qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

                                commandSalesDetail.CommandText = qryDetail
                                commandSalesDetail.Parameters.Clear()

                                commandSalesDetail.Parameters.AddWithValue("@InvoiceNumber", gBillNo)
                                commandSalesDetail.Parameters.AddWithValue("@Sequence", 0)

                                'If lblidItemType.Text = "SERVICE" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERV")
                                'ElseIf lblidItemType.Text = "STOCK" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "STCK")
                                'ElseIf lblidItemType.Text = "OTHERS" Then
                                '    commandSalesDetail.Parameters.AddWithValue("@SubCode", "DIST")
                                'End If
                                'Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
                                commandSalesDetail.Parameters.AddWithValue("@SubCode", "SERVICE")

                                ''''''''''''''''''''''''''''''''''''''
                                Dim commandBP As MySqlCommand = New MySqlCommand
                                commandBP.CommandType = CommandType.Text

                                If String.IsNullOrEmpty(strContractNo) = False Then
                                    If strStatus = "P" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-SRV'"
                                    ElseIf strStatus = "O" Then
                                        commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-DEF'"
                                    End If
                                Else
                                    commandBP.CommandText = "SELECT * FROM tblbillingproducts where ProductCode = 'IN-COO'"
                                End If

                                commandBP.Connection = conn

                                Dim drBP As MySqlDataReader = commandBP.ExecuteReader()
                                Dim dtBP As New DataTable
                                dtBP.Load(drBP)

                                ''''''''''''''''''''''''''''''''''''

                                commandSalesDetail.Parameters.AddWithValue("@LedgerCode", dtBP.Rows(0)("COACode").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@LedgerName", dtBP.Rows(0)("COADescription").ToString())

                                commandSalesDetail.Parameters.AddWithValue("@SubLedgerCode", "")
                                commandSalesDetail.Parameters.AddWithValue("@SONUMBER", "")
                                commandSalesDetail.Parameters.AddWithValue("@RefType", strRecordNo)

                                '07.04.24
                                'commandSalesDetail.Parameters.AddWithValue("@Gst", txtDefaultTaxCode.Text)
                                ''commandSalesDetail.Parameters.AddWithValue("@Gst", "SR")
                                'commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(txtTaxRatePct.Text))

                                commandSalesDetail.Parameters.AddWithValue("@Gst", strGSTNew)
                                commandSalesDetail.Parameters.AddWithValue("@GstRate", Convert.ToDecimal(strGSTRateNew))

                                '07.04.24

                                commandSalesDetail.Parameters.AddWithValue("@ExchangeRate", 1.0)
                                commandSalesDetail.Parameters.AddWithValue("@Currency", "SGD")
                                commandSalesDetail.Parameters.AddWithValue("@Quantity", 1)
                                commandSalesDetail.Parameters.AddWithValue("@UnitMs", "XOJ")
                                commandSalesDetail.Parameters.AddWithValue("@UnitOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@UnitBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@DiscP", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@TaxBase", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@GstOriginal", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@GstBase", strGSTAmount)
                                commandSalesDetail.Parameters.AddWithValue("@ValueOriginal", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@ValueBase", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@AppliedOriginal", strTotalWithGST)
                                commandSalesDetail.Parameters.AddWithValue("@AppliedBase", strTotalWithGST)

                                commandSalesDetail.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(strBillAmount))

                                '''''''''''''''''''''''''''''''

                                Dim command10 As MySqlCommand = New MySqlCommand
                                command10.CommandType = CommandType.Text
                                command10.CommandText = "SELECT Rcno, Status, ContractNo, Description, ServiceDate, BillAmount, LocationID, ServiceLocationGroup, Notes, Address1, AddBuilding, AddStreet, AddCity, AddState, AddCountry, AddPostal FROM tblservicerecord where RecordNo =@RecordNo"
                                command10.Parameters.AddWithValue("@RecordNo", strRecordNo)
                                command10.Connection = conn

                                Dim dr10 As MySqlDataReader = command10.ExecuteReader()
                                Dim dt10 As New DataTable
                                dt10.Load(dr10)

                                ''''''''''''''''''''''''''''''

                                commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate + ", " + dt10.Rows(0)("Notes").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ItemDescription", dtBP.Rows(0)("Description").ToString())
                                'commandSalesDetail.Parameters.AddWithValue("@Description", strRecordNo + ", " + strServiceDate)
                                commandSalesDetail.Parameters.AddWithValue("@Comments", "")
                                commandSalesDetail.Parameters.AddWithValue("@GroupId", "")
                                commandSalesDetail.Parameters.AddWithValue("@DetailID", "")
                                commandSalesDetail.Parameters.AddWithValue("@GrpDetName", "")

                                commandSalesDetail.Parameters.AddWithValue("@SoCode", 0.0)
                                'commandSalesDetail.Parameters.AddWithValue("@ItemCode", "IN-SRV")
                                commandSalesDetail.Parameters.AddWithValue("@ItemCode", dtBP.Rows(0)("ProductCode").ToString().Trim())
                                commandSalesDetail.Parameters.AddWithValue("@AvgCost", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@CostValue", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@COSTCODE", strContractNo)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceStatus", strStatus)

                                commandSalesDetail.Parameters.AddWithValue("@DiscAmount", 0.0)
                                commandSalesDetail.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(strBillAmount))
                                commandSalesDetail.Parameters.AddWithValue("@LocationId", strLocationId1)
                                commandSalesDetail.Parameters.AddWithValue("@ServiceLocationGroup", strServiceLocationGroup1)

                                commandSalesDetail.Parameters.AddWithValue("@RcnoServiceRecord", strRcnoServiceRecord)
                                commandSalesDetail.Parameters.AddWithValue("@BillSchedule", txtInvoiceNo.Text)

                                commandSalesDetail.Parameters.AddWithValue("@ServiceBy", strServiceBy)

                                If String.IsNullOrEmpty(strServiceDate) = True Then
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
                                Else
                                    commandSalesDetail.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(strServiceDate).ToString("yyyy-MM-dd"))
                                End If

                                'commandSalesDetail.Parameters.AddWithValue("@ServiceDate", lblidServiceDate.Text)
                                commandSalesDetail.Parameters.AddWithValue("@BillingFrequency", strBillingFrequency)

                                commandSalesDetail.Parameters.AddWithValue("@Address1", dt10.Rows(0)("Address1").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddBuilding", dt10.Rows(0)("AddBuilding").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddStreet", dt10.Rows(0)("AddStreet").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCity", dt10.Rows(0)("AddCity").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddState", dt10.Rows(0)("AddState").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddCountry", dt10.Rows(0)("AddCountry").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@AddPostal", dt10.Rows(0)("AddPostal").ToString())
                                commandSalesDetail.Parameters.AddWithValue("@ContractGroup", strContractGroupDept)
                                commandSalesDetail.Parameters.AddWithValue("@Location", strLocation)

                                commandSalesDetail.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                                commandSalesDetail.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
                                commandSalesDetail.Connection = conn
                                commandSalesDetail.ExecuteNonQuery()

                                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                Dim sqlLastIdSalesDetail As String
                                sqlLastIdSalesDetail = "SELECT last_insert_id() from tblsalesdetail"

                                Dim commandRcnoSalesDetail As MySqlCommand = New MySqlCommand
                                commandRcnoSalesDetail.CommandType = CommandType.Text
                                commandRcnoSalesDetail.CommandText = sqlLastIdSalesDetail
                                commandRcnoSalesDetail.Parameters.Clear()
                                commandRcnoSalesDetail.Connection = conn
                                txtLastSalesDetailRcno.Text = commandRcnoSalesDetail.ExecuteScalar()

                                commandRcnoSalesDetail.Dispose()

                                'UpdateLastBillDate(strContractNo)
                                'txtLastSalesDetailRcno.Text = "SELECT last_insert_id() from tblsalesdetail"

                                ''''''''''''''''''''''''''''''''' tblSalesDetail


                                'Header

                                Dim commandServiceBillingDetail As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetail.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & txtInvoiceNo.Text & "',  BatchNo = '" & txtBatchNo.Text & "', rcnoInvoice = " & Convert.ToInt64(txtRcno.Text) & "  where RcnoServiceRecord =" & TextBoxRcnoServiceRecord.Text
                                Dim sqlUpdateServiceBillingDetail As String = "Update tblservicebillingdetail set InvoiceNo = '" & gBillNo & "', BillNo = '" & gBillNo & "', rcnoInvoice = " & txtRcno.Text & "  where Rcno =" & dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetail.CommandText = sqlUpdateServiceBillingDetail
                                commandServiceBillingDetail.Parameters.Clear()
                                commandServiceBillingDetail.Connection = conn
                                commandServiceBillingDetail.ExecuteNonQuery()

                                Dim commandServiceBillingDetailItem As MySqlCommand = New MySqlCommand
                                commandServiceBillingDetailItem.CommandType = CommandType.Text
                                'Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & txtInvoiceNo.Text & "' where BatchNo = '" & txtBatchNo.Text & "'"
                                Dim sqlUpdateServiceBillingDetailItem As String = "Update tblservicebillingdetailitem set InvoiceNo = '" & gBillNo & "' where RcnoServiceBillingDetail = " & dtTbwservicebillingdetailAccountId.Rows(rowIndex1)("Rcno").ToString
                                commandServiceBillingDetailItem.CommandText = sqlUpdateServiceBillingDetailItem
                                commandServiceBillingDetailItem.Parameters.Clear()
                                commandServiceBillingDetailItem.Connection = conn
                                commandServiceBillingDetailItem.ExecuteNonQuery()
                                ''''''''''''''''''''''''''''''
                                rowIndex1 = rowIndex1 + 1
                            Next row1
                        End If  ' If strGroupingBy = "ByContractNo" Then

                        'commandTbwservicebillingdetail.Cancel()

                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    End If  ' If strGroupingBy = "ByContractNo" Then

                    ToBillAmt = 0.0
                    DiscAmount = 0.0
                    GSTAmount = 0.0
                    NetAmount = 0.0

                End If    ' If dt2.Rows.Count > 0 Then

                rowIndex = rowIndex + 1
            Next row


            '''''''''''''''''''  Start: Adjustment of GST decimal value for the last record

            '''''''''''27.10.17

            If Convert.ToInt32(txtLastSalesDetailRcno.Text) > 0 Then

                '''''''''''''''''''''''
                ' Start:Update Service Address in tblSales


                Dim commandUpdateServiceAddress As MySqlCommand = New MySqlCommand
                commandUpdateServiceAddress.CommandType = CommandType.StoredProcedure
                commandUpdateServiceAddress.CommandText = "UpdateTblSalesServiceAddress"

                commandUpdateServiceAddress.Parameters.Clear()

                commandUpdateServiceAddress.Parameters.AddWithValue("@pr_InvoiceNumber", gBillNo.Trim)

                commandUpdateServiceAddress.Connection = conn
                commandUpdateServiceAddress.ExecuteScalar()


                ' End:Update Service Address in tblSales

                ''''''''''''''''''''''''''''''''''''

                Dim dblTotalValue As Decimal = 0.0
                Dim commandSalesDetailSum As MySqlCommand = New MySqlCommand

                commandSalesDetailSum.CommandType = CommandType.Text
                commandSalesDetailSum.CommandText = "SELECT sum(GSTBase) as totalvalue FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "'"
                commandSalesDetailSum.Connection = conn

                Dim drSalesDetailSum As MySqlDataReader = commandSalesDetailSum.ExecuteReader()
                Dim dtSalesDetailSum As New DataTable
                dtSalesDetailSum.Load(drSalesDetailSum)

                dblTotalValue = Convert.ToDouble(dtSalesDetailSum.Rows(0)("totalvalue").ToString)

                commandSalesDetailSum.Dispose()

                If gstHeader <> dblTotalValue Then
                    Dim diff As Decimal = 0.0
                    diff = gstHeader - dblTotalValue

                    ''''''''''''''''''''''
                    Dim commandSalesDetailLastRecord As MySqlCommand = New MySqlCommand

                    commandSalesDetailLastRecord.CommandType = CommandType.Text
                    commandSalesDetailLastRecord.CommandText = "SELECT GSTBase, AppliedBase FROM tblsalesdetail where Invoicenumber = '" & gBillNo & "' and Rcno = " & txtLastSalesDetailRcno.Text
                    commandSalesDetailLastRecord.Connection = conn

                    Dim drSalesDetailLastRecord As MySqlDataReader = commandSalesDetailLastRecord.ExecuteReader()
                    Dim dtSalesDetailLastRecord As New DataTable
                    dtSalesDetailLastRecord.Load(drSalesDetailLastRecord)

                    Dim lastGSTBase As Double = 0.0
                    Dim lastAppliedBase As Double = 0.0

                    lastGSTBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("GSTBase").ToString)
                    lastAppliedBase = Convert.ToDouble(dtSalesDetailLastRecord.Rows(0)("AppliedBase").ToString)

                    ''''''''''''''''''''''''''
                    lastGSTBase = lastGSTBase + diff
                    lastAppliedBase = lastAppliedBase + diff
                    Dim commandAdjustLastRec As MySqlCommand = New MySqlCommand
                    commandAdjustLastRec.CommandType = CommandType.Text

                    Dim qryT As String = "UPDATE tblsalesdetail SET  GSTbase = " & lastGSTBase & ", GSTOriginal= " & lastGSTBase & ", AppliedBase = " & lastAppliedBase & ", AppliedOriginal= " & lastAppliedBase & " where rcno = " & txtLastSalesDetailRcno.Text

                    commandAdjustLastRec.CommandText = qryT
                    commandAdjustLastRec.Parameters.Clear()
                    commandAdjustLastRec.Connection = conn
                    commandAdjustLastRec.ExecuteNonQuery()
                    commandAdjustLastRec.Dispose()
                End If
            End If

            ''''''''''''''27.10.17

            '''''''''''''''''''' End: Adjustment of GST decimal value for the last record
        End If

        ''End: From Table

        ''''''''''''''''' Posting


        If String.IsNullOrEmpty(txtCreditDays.Text) = True Then
            txtCreditDays.Text = "0"
        End If

        'Dim confirmValue As String
        'confirmValue = ""


        'confirmValue = Request.Form("confirm_value")
        'If Right(confirmValue, 3) = "Yes" Then
        ''''''''''''''' Insert tblAR

        'Dim conn As MySqlConnection = New MySqlConnection()

        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        'If conn.State = ConnectionState.Open Then
        '    conn.Close()
        'End If
        'conn.Open()

        Dim commandPost As MySqlCommand = New MySqlCommand
        commandPost.CommandType = CommandType.Text

        Dim qry1 As String = "DELETE from tblAR where BatchNo = '" & txtBatchNo.Text & "'"

        commandPost.CommandText = qry1
        commandPost.Connection = conn
        commandPost.ExecuteNonQuery()

        ''''''''''''''''''''
        'Start:Loop thru' Credit values


        Dim commandSales As MySqlCommand = New MySqlCommand

        commandSales.CommandType = CommandType.Text
        commandSales.CommandText = "SELECT InvoiceNumber  FROM tblSales where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by InvoiceNumber"
        commandSales.Connection = conn

        Dim drSales As MySqlDataReader = commandSales.ExecuteReader()
        Dim dtSales As New DataTable
        dtSales.Load(drSales)

        Dim rowIndex2 As Integer
        rowIndex2 = 0
        For Each row1 As DataRow In dtSales.Rows

            '''''''''''''''''''''

            ''Start:Loop thru' Credit values

            Dim invoiceno As String
            invoiceno = Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber"))
            Dim commandValues As MySqlCommand = New MySqlCommand

            commandValues.CommandType = CommandType.Text
            'commandValues.CommandText = "SELECT *  FROM tblservicebillingdetailitem where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' and InvoiceNo ='" & invoiceno & "'"
            commandValues.CommandText = "SELECT RecordNo, ContractNo, BillAmount FROM tblservicebillingdetail where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' and InvoiceNo ='" & invoiceno & "'"

            commandValues.Connection = conn

            Dim drValues As MySqlDataReader = commandValues.ExecuteReader()
            Dim dtValues As New DataTable
            dtValues.Load(drValues)

            For Each row As DataRow In dtValues.Rows

                '''''''''''''''''''''''''''''''''

                Dim cmdServiceBilledAmt As MySqlCommand = New MySqlCommand
                cmdServiceBilledAmt.CommandType = CommandType.Text
                cmdServiceBilledAmt.CommandText = "SELECT RecordNo, ifnull(Billedamt,0) as TotalBilledAmount  FROM tblservicerecord where RecordNo= '" & row("RecordNo") & "'"
                cmdServiceBilledAmt.Connection = conn

                Dim drcmdServiceBilledAmt As MySqlDataReader = cmdServiceBilledAmt.ExecuteReader()
                Dim dtServiceBilledAmt As New DataTable
                dtServiceBilledAmt.Load(drcmdServiceBilledAmt)

                ''''''''''''''''''''''''''''''''''''
                'Start: Update tblServiceRecord
                'If row("ItemCode") = "IN-DEF" Or row("ItemCode") = "IN-SRV" Then
                Dim command4 As MySqlCommand = New MySqlCommand
                command4.CommandType = CommandType.Text

                Dim qry4 As String = "Update tblservicerecord Set BillYN= 'Y', BilledAmt = " & Convert.ToDecimal(dtServiceBilledAmt.Rows(0)("TotalBilledAmount")) + Convert.ToDecimal(row("BillAmount")) & ", BillNo = '" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "' where RecordNo = @Rcno "

                command4.CommandText = qry4
                command4.Parameters.Clear()

                command4.Parameters.AddWithValue("@Rcno", row("RecordNo"))
                command4.Connection = conn
                command4.ExecuteNonQuery()


                'End If
                '    End If

            Next row

            'End: Loop thru' Credit Values

            Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
            commandUpdateInvoice.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tblsales set PostStatus = 'P'  where InvoiceNumber = '" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

            commandUpdateInvoice.CommandText = sqlUpdateInvoice
            commandUpdateInvoice.Parameters.Clear()
            commandUpdateInvoice.Connection = conn
            commandUpdateInvoice.ExecuteNonQuery()

            GridView1.DataBind()
            commandUpdateInvoice.Dispose()

            Dim commandUpdateServicebillingdetail As MySqlCommand = New MySqlCommand
            commandUpdateServicebillingdetail.CommandType = CommandType.Text
            Dim sqlUpdateServicebillingdetail As String = "Update tblservicebillingdetail set Posted = 'P'  where InvoiceNo  ='" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

            commandUpdateServicebillingdetail.CommandText = sqlUpdateServicebillingdetail
            commandUpdateServicebillingdetail.Parameters.Clear()
            commandUpdateServicebillingdetail.Connection = conn
            commandUpdateServicebillingdetail.ExecuteNonQuery()

            commandUpdateServicebillingdetail.Dispose()

            Dim commandUpdateServicebillingdetailItem As MySqlCommand = New MySqlCommand
            commandUpdateServicebillingdetailItem.CommandType = CommandType.Text
            Dim sqlUpdateServicebillingdetailItem As String = "Update tblservicebillingdetailitem set Posted = 'P'  where InvoiceNo  ='" & Convert.ToString(dtSales.Rows(rowIndex)("InvoiceNumber")) & "'"

            commandUpdateServicebillingdetailItem.CommandText = sqlUpdateServicebillingdetailItem
            commandUpdateServicebillingdetailItem.Parameters.Clear()
            commandUpdateServicebillingdetailItem.Connection = conn
            commandUpdateServicebillingdetailItem.ExecuteNonQuery()

            commandUpdateServicebillingdetailItem.Dispose()

            rowIndex = rowIndex + 1

        Next row1


        Dim commandUpdateServiceBillschedule As MySqlCommand = New MySqlCommand
        commandUpdateServiceBillschedule.CommandType = CommandType.Text
        Dim sqlUpdateServicebillSchedule As String = "Update tblserviceBillschedule set PostStatus = 'P'  where BillSchedule  ='" & txtInvoiceNo.Text & "'"

        commandUpdateServiceBillschedule.CommandText = sqlUpdateServicebillSchedule
        commandUpdateServiceBillschedule.Parameters.Clear()
        commandUpdateServiceBillschedule.Connection = conn
        commandUpdateServiceBillschedule.ExecuteNonQuery()
        conn.Close()
        conn.Dispose()

        UpdateLastBillDate()


        If txtMode.Text = "NEW" Then
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtInvoiceNo.Text)
        Else
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtInvoiceNo.Text)
        End If

        'MakeMeNullBillingDetails()
        'MakeMeNull()
        DisableControls()
        FirstGridViewRowGL()

        grvServiceRecDetails.Enabled = True
        updpnlServiceRecs.Update()

        commandUpdateServiceBillschedule.Dispose()


        ''''''''''''''' Insert tblAR


        lblMessage.Text = "POST: RECORD SUCCESSFULLY POSTED"
        lblAlert.Text = ""
        updPnlSearch.Update()
        updPnlMsg.Update()
        updpnlBillingDetails.Update()
        updpnlServiceRecs.Update()
        updpnlBillingDetails.Update()
        'End If

        '''''''''''''''''''' Posting

        lblMessage.Text = "ADD: INVOICE RECORD SUCCESSFULLY ADDED"
        lblAlert.Text = ""

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE Billschedule in (select BillSchedule from tblservicebillingdetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
            GridView1.DataSourceID = "SQLDSInvoice"
            GridView1.DataBind()

        Else
            SQLDSInvoice.SelectCommand = "SELECT * FROM tblservicebillschedule WHERE (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
            GridView1.DataSourceID = "SQLDSInvoice"
            GridView1.DataBind()

        End If

        'GridView1.DataSourceID = "SQLDSInvoice"
        'GridView1.DataBind()


        'If txtMode.Text = "NEW" Then
        '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BTAHSCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)
        'Else
        '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BTAHSCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)
        'End If

        'If conn.State = ConnectionState.Open Then
        '    conn.Close()
        'End If
        conn.Close()
        conn.Dispose()

        'txtBatchNo.Text = "0"
        DisableControls()
        'FirstGridViewRowServiceRecs()
        'btnSaveInvoice.Enabled = False
        btnGenerateInvoice.Enabled = False
        btnGenerateInvoice.ForeColor = System.Drawing.Color.Gray
        'updPnlMsg.Update()
        updPnlMsg.Update()
        updPnlSearch.Update()
        updpnlServiceRecs.Update()
        updPnlBillingRecs.Update()
        UpdatePanel1.Update()

        'btnPost_Click(sender, e)
        IsSuccess = True
        Return IsSuccess
    End Function

    Protected Sub btnConfirmYesSavePost_Click(sender As Object, e As EventArgs) Handles btnConfirmYesSavePost.Click
        IsSuccess = PostBatchSchedule()

        If IsSuccess = True Then

            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHINV", txtInvoiceNo.Text, "POST", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtInvoiceNo.Text)

            lblAlert.Text = ""
            updPnlSearch.Update()
            updPnlMsg.Update()

            'updpnlServiceRecs.Update()


            'btnQuickSearch_Click(sender, e)

            lblMessage.Text = "INVOICES SUCCESSFULLY GENERATED"
            updPnlMsg.Update()

            'btnReverse.Enabled = True
            'btnReverse.ForeColor = System.Drawing.Color.Black

            btnEdit.Enabled = False
            btnEdit.ForeColor = System.Drawing.Color.Gray

            btnDelete.Enabled = False
            btnDelete.ForeColor = System.Drawing.Color.Gray

            btnPost.Enabled = False
            btnPost.ForeColor = System.Drawing.Color.Gray

            txtMode.Text = "View"
            PopulateServiceGrid()
            DisplayGLGrid()
        End If
    End Sub



    Protected Sub txtToBillAmtGV_TextChanged(sender As Object, e As EventArgs)

        Dim txt1 As TextBox = DirectCast(sender, TextBox)
        xgrvBillingDetails = CType(txt1.NamingContainer, GridViewRow)

        Dim lblid1 As TextBox = CType(txt1.FindControl("txtServiceRecordNoGV"), TextBox)
        Dim lblid2 As TextBox = CType(txt1.FindControl("txtServiceDateGV"), TextBox)
        Dim lblid3 As TextBox = CType(txt1.FindControl("txtToBillAmtGV"), TextBox)
        Dim lblid4 As TextBox = CType(txt1.FindControl("txtCreditTermsGV"), TextBox)

        'Dim xrow1 As GridViewRow = CType(txt1.NamingContainer, GridViewRow)
        Dim rowindex1 As Integer = xgrvBillingDetails.RowIndex


        ''''''''''''''''''
        Dim sqlstr As String
        sqlstr = ""

        sqlstr = "SELECT svcLock FROM tbllockservicerecord where '" & Convert.ToDateTime(lblid2.Text).ToString("yyyy-MM-dd") & "' between svcdatefrom and svcdateto"

        Dim command As MySqlCommand = New MySqlCommand

        Dim conn As MySqlConnection = New MySqlConnection()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        command.CommandText = sqlstr
        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("svcLock").ToString = "Y" Then
                lblid3.Text = Convert.ToDecimal(lblid4.Text).ToString("N2")
                mdlLockedServiceRecord.Show()
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ServiceLock();</Script>", False)

            Else

                Session.Add("servicerecordForupdateservicetable", lblid1.Text)
                Session.Add("AmountForupdateservicetable", lblid3.Text)
                Session.Add("AmountForupdateRowNo", rowindex1)

                mdlUpdateServiceRecord.Show()
             
            End If
        Else
            Session.Add("servicerecordForupdateservicetable", lblid1.Text)
            Session.Add("AmountForupdateservicetable", lblid3.Text)
            Session.Add("AmountForupdateRowNo", rowindex1)

            mdlUpdateServiceRecord.Show()
        End If
        dr.Close()
        dt.Dispose()
        conn.Close()
        conn.Dispose()

    End Sub


    Protected Sub btnConfirmYesUpdateServiceRecord_Click(sender As Object, e As EventArgs) Handles btnConfirmYesUpdateServiceRecord.Click
        If String.IsNullOrEmpty(Session("servicerecordForupdateservicetable")) = False Then
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim command As MySqlCommand = New MySqlCommand
            command.CommandType = CommandType.Text

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim strRecordNo As String = Session("servicerecordForupdateservicetable")
            Dim strBillAmt As String = Session("AmountForupdateservicetable")
            'Session.Add("servicerecordForupdateservicetable", lblid1.Text)
            'Session.Add("AmountForupdateservicetable", lblid2.Text)


            Dim command4 As MySqlCommand = New MySqlCommand
            command4.CommandType = CommandType.Text

            Dim qry4 As String = "Update tblservicerecord Set BillAmount = " & Convert.ToDecimal(strBillAmt) & " where RecordNo = '" & strRecordNo & "'"

            command4.CommandText = qry4
            command4.Connection = conn
            command4.ExecuteNonQuery()

            conn.Close()
            command4.Dispose()
            conn.Dispose()
        End If
    End Sub

    Protected Sub gvStaff_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvStaff.PageIndexChanging
        gvStaff.PageIndex = e.NewPageIndex

        'If txtPopUpTeam.Text.Trim = "Search Here for Team or In-ChargeId" Then
        '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where  Status <> 'N' order by TeamName "
        'Else
        '    SqlDSTeam.SelectCommand = "SELECT distinct * From tblTeam where 1=1 and  Status <> 'N' and TeamName like '%" + txtPopUpTeam.Text.Trim.ToUpper + "%' order by TeamName"
        'End If

        If String.IsNullOrEmpty(txtPopUpStaff.Text.Trim) = False Then
            'txtPopUpStaff.Text = ""
            'txtPopUpStaff.Text = txtServiceBySearch.Text
            'txtPopupStaffSearch.Text = txtPopUpStaff.Text

            SqlDSStaffID.SelectCommand = "SELECT * From tblstaff where staffid like '%" + txtPopUpStaff.Text.ToUpper + "%' or name like '%" + txtPopUpStaff.Text + "%' order by staffid"
            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
            updPanelInvoice.Update()
        Else
            'txtPopUpClient.Text = ""

            SqlDSStaffID.SelectCommand = "SELECT * From tblstaff order by staffid"

            SqlDSStaffID.DataBind()
            gvStaff.DataBind()
            updPanelInvoice.Update()
        End If

        gvStaff.DataBind()
        'gvStaff.DataBind()
        mdlPopupStaff.Show()
    End Sub

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton5.Click

        lblAlert.Text = ""
        txtIsPopup.Text = ""
        txtIsPopup.Text = "ContractNo"
        Try

            If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
                txtPopUpContractNo.Text = ddlContractNo.Text

                'If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                '    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"
                'Else
                '    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
                'End If

                If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status <> 'V') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"
                Else
                    SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status <> 'V') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
                End If

                SqlDSClient.DataBind()
                gvPopUpContractNo.DataBind()
                updPanelInvoice.Update()
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
                updPanelInvoice.Update()
            End If

            mdlPopUpContractNo.Show()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "ImageButton5_Click", ex.Message.ToString, txtAccountId.Text & " - " & ddlContractNo.Text)
            Exit Sub
        End Try

    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        'txtPopUpContractNo.Text = ""
        'SqlDSContractNo.SelectCommand = "SELECT ContractNo, CustName From tblContract where (Status = 'O' or Status = 'P')"
        'SqlDSContractNo.DataBind()
        'gvPopUpContractNo.DataBind()
        'mdlPopUpContractNo.Show()

        'If String.IsNullOrEmpty(ddlContractNo.Text.Trim) = False Then
        'txtPopUpContractNo.Text = ddlContractNo.Text

        If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
            SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P')  order by ContractNo"
        Else
            SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
        End If

        SqlDSContractNo.DataBind()
        gvPopUpContractNo.DataBind()
        updPanelInvoice.Update()
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
    End Sub

    Protected Sub txtPopUpContractNo_TextChanged(sender As Object, e As EventArgs) Handles txtPopUpContractNo.TextChanged
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
    End Sub

    Protected Sub gvPopUpContractNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvPopUpContractNo.SelectedIndexChanged
        If (gvPopUpContractNo.SelectedRow.Cells(1).Text = "&nbsp;") Then
            ddlContractNo.Text = ""
        Else
            ddlContractNo.Text = gvPopUpContractNo.SelectedRow.Cells(1).Text.Trim
        End If
    End Sub

    Protected Sub gvPopUpContractNo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPopUpContractNo.PageIndexChanging
        gvPopUpContractNo.PageIndex = e.NewPageIndex

        If String.IsNullOrEmpty(txtAccountId.Text.Trim) = True Then
            SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' order by ContractNo"
        Else
            SqlDSContractNo.SelectCommand = "SELECT ContractNo,CustName, AccountId  FROM tblcontract WHERE (Status = 'O' or Status = 'P') and  ContractNo like '%" & txtPopUpContractNo.Text.Trim & "%' and  AccountID = '" + txtAccountId.Text.Trim.ToUpper + "' order by ContractNo"
        End If

        SqlDSClient.DataBind()
        gvPopUpContractNo.DataBind()
        updPanelInvoice.Update()

        mdlPopUpContractNo.Show()
    End Sub

    Protected Sub ImageButton2_Unload(sender As Object, e As EventArgs) Handles ImageButton2.Unload

    End Sub

    Protected Sub grvServiceRecDetails_Sorting(sender As Object, e As GridViewSortEventArgs) Handles grvServiceRecDetails.Sorting
        Dim strsql As String
        Dim strsql1 As String
        Dim strsql2 As String
        strsql = ""
        strsql1 = ""
        strsql2 = ""

        If e.SortExpression = "LocationID" Then
            strsql2 = " order by A." + e.SortExpression
        ElseIf e.SortExpression = "CustName" Then
            strsql2 = " order by A." + e.SortExpression
        ElseIf e.SortExpression = "ServiceDate" Then
            strsql2 = " order by A." + e.SortExpression
        End If
        'grvServiceRecDetails.DataBind()



        'strsql2 = e.SortExpression

        strsql1 = txtShowRecords.Text

        strsql = strsql1 + strsql2

        SqlDSServices.SelectCommand = strsql
        grvServiceRecDetails.DataSourceID = "SqlDSServices"
        grvServiceRecDetails.DataBind()

        updpnlServiceRecs.Update()

    End Sub

    Protected Sub grvServiceRecDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grvServiceRecDetails.SelectedIndexChanged

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
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub UpdateLastBillDate()
        Dim query As String
        query = ""
        Try
            Dim command As MySqlCommand = New MySqlCommand
            Dim commandMaxContractDate As MySqlCommand = New MySqlCommand
            Dim commandSales As MySqlCommand = New MySqlCommand
            commandSales.CommandType = CommandType.Text

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()
            '''''''''''''''''''''''
            'Dim commandSales As MySqlCommand = New MySqlCommand

            'commandSales.CommandType = CommandType.Text
            commandSales.CommandText = "SELECT distinct(CostCode) as CostCode1 FROM tblSalesDetail  where InvoiceNumber = (Select InvoiceNumber from tblSales where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' and PostStatus ='P' order by InvoiceNumber)"
            commandSales.Connection = conn

            Dim drSales As MySqlDataReader = commandSales.ExecuteReader()
            Dim dtSales As New DataTable
            dtSales.Load(drSales)

            Dim rowIndex2 As Integer
            rowIndex2 = 0
            For Each row1 As DataRow In dtSales.Rows


                '''''''''''''''''''''''
                'Dim command As MySqlCommand = New MySqlCommand
                'Dim commandMaxContractDate As MySqlCommand = New MySqlCommand
                Dim MaxContractdate As Date

                commandMaxContractDate.CommandType = CommandType.Text
                commandMaxContractDate.CommandText = "SELECT A.SalesDate, B.ValueBase from tblSales a, tblSalesDetail b where a.InvoiceNumber = b.InvoiceNumber and a.PostStatus = 'P' and b.CostCode = '" & row1("CostCode1") & "' order by a.salesdate desc limit 1"
                'commandBilledAmt.CommandText = "SELECT sum(PriceWithDisc) as TotalBilledAmount FROM tblSalesDetail  where RefType= '" & ServiceRecordNo & "'"

                commandMaxContractDate.Connection = conn

                Dim drMaxContractDate As MySqlDataReader = commandMaxContractDate.ExecuteReader()
                Dim dtMaxContractDate As New DataTable
                dtMaxContractDate.Load(drMaxContractDate)

                If dtMaxContractDate.Rows.Count > 0 Then
                    command.CommandType = CommandType.Text

                    MaxContractdate = dtMaxContractDate.Rows(0)("SalesDate")
                    query = "Update tblContract Set LastBillDate = @LastBilldate, LastBillAmount=@LastBillAmount where ContractNo = @ContractNo "
                    command.CommandText = query
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@LastBilldate", Convert.ToDateTime(dtMaxContractDate.Rows(0)("SalesDate")).ToString("yyyy-MM-dd"))
                    command.Parameters.AddWithValue("@LastBillAmount", dtMaxContractDate.Rows(0)("ValueBase"))

                    command.Parameters.AddWithValue("@ContractNo", row1("CostCode1"))
                    command.Connection = conn
                    command.ExecuteNonQuery()
                Else
                    query = "Update tblContract Set LastBillDate = @LastBilldate where ContractNo = @ContractNo "
                    command.CommandText = query
                    command.Parameters.Clear()

                    command.Parameters.AddWithValue("@LastBilldate", DBNull.Value)
                    command.Parameters.AddWithValue("@LastBillAmount", 0.0)
                    command.Parameters.AddWithValue("@ContractNo", row1("CostCode1"))
                    command.Connection = conn
                    command.ExecuteNonQuery()
                End If

                'command.Dispose()
            Next

            commandMaxContractDate.Dispose()
            commandSales.Dispose()
            command.Dispose()
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "UpdateLastBillDate", ex.Message.ToString, query)
            Exit Sub
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
        command1.CommandText = "SELECT LocationID FROM tblstaff where StaffId='" & txtCreatedBy.Text.ToUpper & "'"
        command1.Connection = connLocation

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then
            'txtLocation.Text = dt.Rows(0)("LocationID").ToString
            txtLocation.Items.Add(dt.Rows(0)("LocationID").ToString)
        End If

        connLocation.Close()
        connLocation.Dispose()
        command1.Dispose()
        dt.Dispose()
        dr.Close()
    End Sub

    Protected Sub btnDeleteUnselected_Click(sender As Object, e As EventArgs) Handles btnDeleteUnselected.Click

        Dim totalRows, totalRows1 As Long
        totalRows = 0
        totalRows1 = 0

        For rowIndex1 As Integer = 0 To grvServiceRecDetails.Rows.Count - 1
            Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
            If (TextBoxchkSelect1.Checked = False) Then

                grvServiceRecDetails.Rows(rowIndex1).Visible = False
                totalRows1 = totalRows1 + 1
            Else
                totalRows = totalRows + 1
            End If


        Next rowIndex1
        'End If


        If totalRows = 0 Then
            lblAlert.Text = "PLEASE SELECT SERVICE RECORDS"
            lblAlert.Focus()
            updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        Else
            'updpnlServiceRecs.Update()
            Label43.Text = "SERVICE BILLING DETAILS : Total Records :" & totalRows
            updPanelInvoice.Update()
            'updpnlServiceRecs.Update()
        End If
        ''''''''''''''''''''''''''''''''''

       
    End Sub

    Protected Sub SqlDSServices_Deleting(sender As Object, e As SqlDataSourceCommandEventArgs) Handles SqlDSServices.Deleting
      
    End Sub

    Protected Sub btnConfirmNoUpdateServiceRecord_Click(sender As Object, e As EventArgs) Handles btnConfirmNoUpdateServiceRecord.Click
        Try
            Dim lRowNo As Long
            lRowNo = 0
            lRowNo = Session("AmountForupdateRowNo")
            'Dim TextBoxchkSelect1 As CheckBox = CType(grvServiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
            Dim TextBoxBillamt As TextBox = CType(grvServiceRecDetails.Rows(lRowNo).Cells(0).FindControl("txtToBillAmtGV"), TextBox)
            Dim TextBoxCreditTerms As TextBox = CType(grvServiceRecDetails.Rows(lRowNo).Cells(0).FindControl("txtCreditTermsGV"), TextBox)

            TextBoxBillamt.Text = TextBoxCreditTerms.Text
            updpnlServiceRecs.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE SCHEDULE - " + Session("UserID"), "btnConfirmNoUpdateServiceRecord_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Protected Sub txtLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtLocation.SelectedIndexChanged

    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'ViewState("CheckRefresh") = Session("CheckRefresh")

    End Sub

    Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
        Try

            If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
                lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
                Return
            End If

            lblMessage.Text = ""
            lblAlert.Text = ""

            Dim btn1 As Button = DirectCast(sender, Button)

            Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
            Dim rowindex1 As Integer = xrow1.RowIndex


            Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

            txtRcno.Text = lblidRcno

            GridView1.SelectedIndex = rowindex1

            Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(4).Text
            'txtLogDocNo.Text = strRecordNo
            sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
            sqlDSViewEditHistory.DataBind()

            grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
            grdViewEditHistory.DataBind()

            mdlViewEditHistory.Show()


        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try

    End Sub

    Protected Sub ddlContractGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContractGroup.SelectedIndexChanged
        'If ddlContractGroup.SelectedIndex = 0 Then
        '    FindDefaultTaxCodeandPctFromPeriod(txtBillingPeriod.Text)
        'Else
        '    FindDefaultTaxCodeandPctFromContractGroup(ddlContractGroup.Text)
        'End If

    End Sub
End Class
