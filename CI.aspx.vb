Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web
Imports System.Net
Imports System.Text
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel
Imports System.IO

' Include this namespace if it is not already there

Imports System.Globalization
Imports System.Threading
Imports System.Drawing


Imports System.Web.Services
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading.Tasks
Imports EInvoicing.EInvoice
Imports Microsoft.IdentityModel.Clients.ActiveDirectory
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization

Partial Class CI
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
            Dim tt As String = "YES"
            If Not Page.IsPostBack Then
                '    mdlPopUpClient.Hide()
                '  mdlPopupLocation.Hide()
                If btnAdd.Enabled Then
                    tt = "YES"
                Else
                    tt = "NO"
                End If
                InsertIntoTblWebEventLog("CI", "PAGELOAD1", tt, txtCreatedBy.Text)

                MakeMeNull()
                If btnAdd.Enabled Then
                    tt = "YES"
                Else
                    tt = "NO"
                End If
                InsertIntoTblWebEventLog("CI", "PAGELOAD1", tt, txtCreatedBy.Text)
                DisableControls()
                txtSearch1Status.Text = "O,P"
                If btnAdd.Enabled Then
                    tt = "YES"
                Else
                    tt = "NO"
                End If
                InsertIntoTblWebEventLog("CI", "PAGELOAD2", tt, txtCreatedBy.Text)
                'Session.Add("CheckRefresh", Server.UrlDecode(System.DateTime.Now.ToString()))

                Dim Query As String
                'Query = "Select StaffId from tblStaff where (SecGroupAuthority like  'SCHEDULER%' or  SecGroupAuthority like '%ADMINISTRATOR%') and Status = 'O'"
                'Query = "Select StaffId from tblStaff where SecGroupAuthority <> 'GUEST' and Status = 'O'"
                'PopulateDropDownList(Query, "StaffId", "StaffId", ddlScheduler)

                '  txtPreviousBillSchedule.Attributes.Add("readonly", "readonly")
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
                commandServiceRecordMasterSetup.CommandText = "SELECT DisplayRecordsLocationWise, DefaultTaxCode, MaxNoofInvinCI FROM tblservicerecordmastersetup"
                commandServiceRecordMasterSetup.Connection = conn

                Dim drServiceRecordMasterSetup As MySqlDataReader = commandServiceRecordMasterSetup.ExecuteReader()
                Dim dtServiceRecordMasterSetup As New DataTable
                dtServiceRecordMasterSetup.Load(drServiceRecordMasterSetup)

                If dtServiceRecordMasterSetup.Rows.Count > 0 Then
                    'txtLimit.Text = dtServiceRecordMasterSetup.Rows(0)("InvoiceRecordMaxRec")
                    txtDisplayRecordsLocationwise.Text = dtServiceRecordMasterSetup.Rows(0)("DisplayRecordsLocationWise").ToString
                    txtDefaultTaxCode.Text = dtServiceRecordMasterSetup.Rows(0)("DefaultTaxCode").ToString

                    txtMaxNoofInvinCI.Text = dtServiceRecordMasterSetup.Rows(0)("MaxNoofInvinCI").ToString
                End If

                'conn.Close()
                'conn.Dispose()
                ''''''''''''''''''''''''''''''''''''''''''
                InsertIntoTblWebEventLog("CI", "PAGELOAD", Session("invoicefrom"), txtCreatedBy.Text)

                If String.IsNullOrEmpty(Session("invoicefrom")) = False Then
                    '  btnADD_Click(sender, e)
                    txtInvoiceNo.Text = Session("invoiceno")
                    txtInvoicenoSearch.Text = Session("invoiceno")
                    txtRcno.Text = Session("rcnoschedule")
                    InsertIntoTblWebEventLog("CI", "PAGELOAD", txtInvoiceNo.Text, txtCreatedBy.Text)

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
                ' FindLocation()
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
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tbleinvoiceconsolidated WHERE ConsolidatedInvoiceNo in (select ConsolidatedInvoiceNo from tbleinvoiceconsolidateddetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()

                    'Query = "select locationID from tblStaff where StaffID = '" & txtCreatedBy.Text & "'"
                    'PopulateDropDownList(Query, "locationID", "locationID", txtLocation)

                    Query = "select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
                    PopulateComboBox(Query, "locationID", "locationID", ddlLocationSearch)

                    Query = "select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "'"
                    PopulateDropDownList(Query, "locationID", "locationID", txtLocation)

                    Query = "Select TaxType from tbltaxtype where ARIN = True  order by taxtype"
                    PopulateDropDownList(Query, "TaxType", "TaxType", txtGST)

                    lblBranch1.Visible = True
                    txtLocation.Visible = True

                    'lblBranch2.Visible = True
                    'ddlLocationSearch.Visible = True
                    Label48.Visible = True

                Else
                    SQLDSInvoice.SelectCommand = "SELECT * FROM tbleinvoiceconsolidated WHERE (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                    GridView1.DataSourceID = "SQLDSInvoice"
                    GridView1.DataBind()

                    lblBranch1.Visible = False
                    lblBranch2.Visible = False
                    txtLocation.Visible = False
                    ddlLocationSearch.Visible = False
                    Label48.Visible = False
                End If

                '   updPnlBillingRecs.Update()


                ' '''''''''''''''''''''''''''''''' Recurring Invoice

                'calculateRecurringInvoice()   'quoted on 11.04.17 for display error

                ''Dim sql2 As String
                ''sql2 = ""

                ''txtCreatedOn.Text = (Session("SysDate"))
                ''sql2 = "Select count(*) as totrecurring from tblservicebillschedule where RecurringInvoice = 'Y' and NextInvoiceDate = '" & Convert.ToDateTime(Session("SysDate")).ToString("yyyy-MM-dd") & "'"

                ''Dim command2 As MySqlCommand = New MySqlCommand
                ''command2.CommandType = CommandType.Text
                ''command2.CommandText = sql2
                ''command2.Connection = conn

                ''Dim dr2 As MySqlDataReader = command2.ExecuteReader()

                ''Dim dt2 As New DataTable
                ''dt2.Load(dr2)

                ''If dt2.Rows.Count > 0 Then
                ''    btnADDRecurring.Text = "CREATE RECURRING [" & dt2.Rows(0)("totrecurring").ToString & "]"
                ''End If



                ' '''''''''''''''''''''''''''''''' Recurring Invoice

                txt.Text = SQLDSInvoice.SelectCommand
            Else
                'If txtIsPopup.Text = "Team" Then
                '    txtIsPopup.Text = "N"
                '    'mdlPopUpTeam.Show()
                'ElseIf txtIsPopup.Text = "Client" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpClient.Show()
                'ElseIf txtIsPopup.Text = "Staff" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopupStaff.Show()
                'ElseIf txtIsPopup.Text = "ContractNo" Then
                '    txtIsPopup.Text = "N"
                '    mdlPopUpContractNo.Show()
                'End If

                ''If txtSearch.Text = "ContractNoSearch" Then
                ''    txtSearch.Text = "N"
                ''    mdlPopUpContractNo.Show()

                ''End If
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CONSOLIDATED INV - " + Session("UserID"), "Page_load", ex.Message.ToString, "")
            Exit Sub
        End Try
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

    Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
        mdlPopupStatusSearch.Show()
    End Sub

    Protected Sub btnStatusSearch_Click(sender As Object, e As EventArgs) Handles btnStatusSearch.Click
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

    Private Sub DisableControls()

        btnADD.Enabled = True
        btnADD.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray

        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        txtInvoiceNo.Enabled = False

        txtInvoiceDate.Enabled = False
        txtBillingPeriod.Enabled = False
        txtGst.Enabled = False
        txtLocation.Enabled = False

        btnSaveInvoice.Enabled = False
        btnSaveInvoice.ForeColor = System.Drawing.Color.Gray

        btnGenerateInvoice.Enabled = False
        btnGenerateInvoice.ForeColor = System.Drawing.Color.Gray

        btnPostEinvoice.Enabled = False
        btnPostEinvoice.ForeColor = System.Drawing.Color.Gray
        'btnSave.Enabled = False
        'btnSave.ForeColor = System.Drawing.Color.Gray

        btnShowRecords.Enabled = False
        btnShowRecords.ForeColor = System.Drawing.Color.Gray


        '   grvInvoiceRecDetails.Enabled = False



        ddlContactType.Enabled = False
        'ddlServiceFrequency.Enabled = False
        'ddlBillingFrequency.Enabled = False
        'ddlContractGroup.Enabled = False
        txtAccountId.Enabled = False
        'txtContractNo.Enabled = False
        txtClientName.Enabled = False

        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        ddlLocationSearch.Enabled = False
        'btnDelete.Enabled = False
        txtLocation.Enabled = False


        txtInvoicenoSearch.Enabled = True
        txtUUIDSearch.Enabled = True
        txtToInvoiceDate.Enabled = True
        txtFromInvoiceDate.Enabled = True
        txtSearch1Status.Enabled = True

        btnQuickSearch.Enabled = True
        btnQuickReset.Enabled = True

        '   grvInvoiceRecDetails.Enabled = False

        lblBranch2.Visible = False
        ddlLocationSearch.Visible = False
        AccessControl()

    End Sub

    Protected Sub btnChangeStatus_Click(sender As Object, e As EventArgs) Handles btnChangeStatus.Click
        lblAlert.Text = ""
        lblMessage.Text = ""

        Try
            If String.IsNullOrEmpty(txtRcno.Text) = True Then
                lblAlert.Text = "PLEASE SELECT A REORD"
                Exit Sub

            End If

            If txtPostStatus.Text = "P" Then
                lblAlert.Text = "POSTED RECORDS CANNOT BE VOIDED"
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


                'End: Loop thru' Credit Values

                Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                commandUpdateInvoice.CommandType = CommandType.Text
                Dim sqlUpdateInvoice As String = "Update tbleinvoiceconsolidated set PostStatus = 'V' where Rcno =" & Convert.ToInt64(txtRcno.Text)

                commandUpdateInvoice.CommandText = sqlUpdateInvoice
                commandUpdateInvoice.Parameters.Clear()
                commandUpdateInvoice.Connection = conn
                commandUpdateInvoice.ExecuteNonQuery()

                GridView1.DataBind()


                Dim commandUpdateServicebillingdetail As MySqlCommand = New MySqlCommand
                commandUpdateServicebillingdetail.CommandType = CommandType.Text
                Dim sqlUpdateServicebillingdetail As String = "Update tbleinvoiceconsolidateddetail set Posted = 'V'  where ConsolidatedInvoiceNo  ='" & txtInvoiceNo.Text & "'"

                commandUpdateServicebillingdetail.CommandText = sqlUpdateServicebillingdetail
                commandUpdateServicebillingdetail.Parameters.Clear()
                commandUpdateServicebillingdetail.Connection = conn
                commandUpdateServicebillingdetail.ExecuteNonQuery()

                MakeMeNull()
                DisableControls()
                '     FirstGridViewRowGL()



                ''''''''''''''' Insert tblAR


                lblMessage.Text = "RECORD SUCCESSFULLY VOIDED"
                lblAlert.Text = ""

            End If
        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        ''Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
        ''Dim objTextInfo As TextInfo = cultureInfo.TextInfo

        lblAlert.Text = ""
        lblMessage.Text = ""

        If txtMode.Text = "NEW" Or txtMode.Text = "EDIT" Then
            lblAlert.Text = "CANNOT SELECT RECORD IN ADD/EDIT MODE. SAVE OR CANCEL TO PROCEED"
            '   updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Return
        End If

        Try
            Dim MyTi As TextInfo = New CultureInfo("en-US", False).TextInfo
            MakeMeNull()
            '     MakeMeNullBillingDetails()

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
            'sql = "Select PostStatus, BillSchedule, BSDate, BillingDate, ContType, AccountId, ServiceFrom, ServiceTo, Frequency, GLPeriod, CustName, Support,  "
            'sql = sql + "  GroupByBillingFrequency, GroupByStatus, GroupContractNo, GroupLocationID, GroupContractGroup, BatchNo, RecurringInvoice, RecurringScheduled,  TotalRecurringInvoices, NextInvoiceDate,  "
            'sql = sql + " RecurringServiceStartDate, RecurringServiceEndDate, BillAmount, DiscountAmount, AmountWithDiscount, GSTAmount, NetAmount, CompanyGroupSearch, SchedulerSearch, groupfield, Location  "
            'sql = sql + " FROM tblservicebillschedule "

            sql = "select ConsolidatedInvoiceNo,CIDate,PostStatus,UUID,ContType,CustCode,CustName,Frequency,InCharge,Support,Location,"
            sql = sql + "Description, Contract, BillingDate, InvoiceFrom, InvoiceTo, rcno, GroupByBranch, GroupField, CreatedBy,"
            sql = sql + "GlPeriod,CreatedOn, LastModifiedBy, LastModifiedOn, AccountId, BillAmount, DiscountAmount, AmountWithDiscount,GSTAmount,"
            sql = sql + "EI, EInvoiceStatus, UUID,SubmissionID,LongID,SubmissionDate,SubmissionBy,Gst,"
            sql = sql + "NetAmount FROM tbleinvoiceconsolidated "


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
                If dt.Rows(0)("ConsolidatedInvoiceNo").ToString <> "" Then : txtInvoiceNo.Text = dt.Rows(0)("ConsolidatedInvoiceNo").ToString : End If
                If dt.Rows(0)("CIDate").ToString <> "" Then : txtInvoiceDate.Text = Convert.ToDateTime(dt.Rows(0)("CIDate")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("GlPeriod").ToString <> "" Then : txtBillingPeriod.Text = dt.Rows(0)("GLPeriod").ToString : End If
                If String.IsNullOrEmpty(dt.Rows(0)("Location").ToString) = True Then
                    txtLocation.SelectedIndex = 0
                Else
                    If dt.Rows(0)("Location").ToString <> "" Then : txtLocation.Text = dt.Rows(0)("Location").ToString : End If

                End If


                If dt.Rows(0)("InvoiceFrom").ToString <> "" Then : txtDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("InvoiceFrom")).ToString("dd/MM/yyyy") : End If
                If dt.Rows(0)("InvoiceTo").ToString <> "" Then : txtDateTo.Text = Convert.ToDateTime(dt.Rows(0)("InvoiceTo")).ToString("dd/MM/yyyy") : End If

                If dt.Rows(0)("ContType").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("ContType").ToString : End If
                If dt.Rows(0)("AccountId").ToString <> "" Then : txtAccountId.Text = dt.Rows(0)("AccountId").ToString : End If
                If dt.Rows(0)("CustName").ToString <> "" Then : txtClientName.Text = dt.Rows(0)("CustName").ToString : End If


                'If dt.Rows(0)("BillingDate").ToString <> "" Then : txtInvoiceDate.Text = Convert.ToDateTime(dt.Rows(0)("BillingDate")).ToString("dd/MM/yyyy") : End If

                'If dt.Rows(0)("BillAmount").ToString <> "" Then : txtInvoiceAmount.Text = dt.Rows(0)("BillAmount").ToString : End If
                'If dt.Rows(0)("DiscountAmount").ToString <> "" Then : txtDiscountAmount.Text = dt.Rows(0)("DiscountAmount").ToString : End If
                'If dt.Rows(0)("AmountWithDiscount").ToString <> "" Then : txtAmountWithDiscount.Text = dt.Rows(0)("AmountWithDiscount").ToString : End If
                'If dt.Rows(0)("GSTAmount").ToString <> "" Then : txtGSTAmount.Text = dt.Rows(0)("GSTAmount").ToString : End If
                'If dt.Rows(0)("NetAmount").ToString <> "" Then : txtNetAmount.Text = dt.Rows(0)("NetAmount").ToString : End If
                'If dt.Rows(0)("BatchNo").ToString <> "" Then : txtBatchNo.Text = dt.Rows(0)("BatchNo").ToString : End If

              
                'If dt.Rows(0)("Support").ToString <> "" Then : txtServiceBySearch.Text = dt.Rows(0)("Support").ToString : End If
                ''If dt.Rows(0)("TotalRecurringInvoices").ToString <> "" Then : ddlContactType.Text = dt.Rows(0)("TotalRecurringInvoices").ToString : End If
                'If dt.Rows(0)("NextInvoiceDate").ToString <> "" Then : txtRecurringInvoiceDate.Text = Convert.ToDateTime(dt.Rows(0)("NextInvoiceDate")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("RecurringServiceStartDate").ToString <> "" Then : txtRecurringServiceDateFrom.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceStartDate")).ToString("dd/MM/yyyy") : End If
                'If dt.Rows(0)("RecurringServiceEndDate").ToString <> "" Then : txtRecurringServiceDateTo.Text = Convert.ToDateTime(dt.Rows(0)("RecurringServiceEndDate")).ToString("dd/MM/yyyy") : End If

                ''If dt.Rows(0)("BillAmount").ToString <> "" Then : txtTotalServiceSelected.Text = dt.Rows(0)("BillAmount").ToString : End If



                txtTotalSelected.Text = dt.Rows(0)("NetAmount").ToString

                If dt.Rows(0)("EI").ToString <> "" Then
                    txtEI.Text = dt.Rows(0)("EI").ToString
                Else
                    txtEI.Text = ""
                End If

                '  InsertIntoTblWebEventLog("PopulateRecord3", dt.Rows(0)("EI").ToString, txtEI.Text, txtCreatedBy.Text)

                '    InsertIntoTblWebEventLog("PopulateRecord3", dt.Rows(0)("EInvoiceStatus").ToString, txtEInvoiceStatus.Text, txtCreatedBy.Text)

                If String.IsNullOrEmpty(dt.Rows(0)("EInvoiceStatus").ToString) = False Then
                    txtEInvoiceStatus.Text = dt.Rows(0)("EInvoiceStatus").ToString
                Else
                    txtEInvoiceStatus.Text = ""
                End If

                '   InsertIntoTblWebEventLog("PopulateRecord4", dt.Rows(0)("EInvoiceStatus").ToString, txtEInvoiceStatus.Text, txtCreatedBy.Text)

                If dt.Rows(0)("UUID").ToString <> "" Then
                    txtUUID.Text = dt.Rows(0)("UUID").ToString
                Else
                    txtUUID.Text = ""
                End If
                If dt.Rows(0)("SubmissionID").ToString <> "" Then
                    txtSubmissionID.Text = dt.Rows(0)("SubmissionID").ToString
                Else
                    txtSubMissionID.Text = ""
                End If

                If dt.Rows(0)("LongID").ToString <> "" Then
                    txtLongID.Text = dt.Rows(0)("LongID").ToString
                Else
                    txtLongID.Text = ""
                End If
                If dt.Rows(0)("SubmissionBy").ToString <> "" Then : txtLastPostedBy.Text = dt.Rows(0)("SubmissionBy").ToString : End If
                If dt.Rows(0)("SubmissionDate").ToString <> "" Then : txtSubmissionDate.Text = Convert.ToDateTime(dt.Rows(0)("SubmissionDate")).ToString("dd/MM/yyyy") : End If

                InsertIntoTblWebEventLog("CI", "GridView1_Selected", dt.Rows(0)("GST").ToString, txtCreatedBy.Text)

                If dt.Rows(0)("GST").ToString <> "" Then : txtGST.Text = dt.Rows(0)("GST").ToString : End If

            End If

            conn.Close()
            conn.Dispose()
            dt.Dispose()
            dr.Close()

            '''''''''''''''''''''''''''''''''''



         

            '''''''''''''''''''''''''''''''''''''''''
            txtMode.Text = "View"

            '  PopulateGLCodes()
            PopulateInvoiceGrid()
            '   DisplayGLGrid()

            Session.Add("BillSchedule", txtInvoiceNo.Text)

            If txtPostStatus.Text = "P" Or txtPostStatus.Text = "V" Then
                btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                'btnCopy.Enabled = True
                btnChangeStatus.Enabled = True

                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray

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

                btnGenerateInvoice.Enabled = True
                btnGenerateInvoice.ForeColor = System.Drawing.Color.Black

                btnPostEinvoice.Enabled = True
                btnPostEinvoice.ForeColor = System.Drawing.Color.Black
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

                btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray

            End If

            If txtEI.Text = "Y" Then
                btnPostEinvoice.Enabled = False
                btnPostEinvoice.ForeColor = System.Drawing.Color.Gray

              
            End If

            '   grvInvoiceRecDetails.Enabled = False

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CONSOLIDATED INV - " + Session("UserID"), "GridView1_SelectedIndexChanged", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    Private Sub AccessControl()
    
        'If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        command.CommandType = CommandType.Text
        'command.CommandText = "SELECT X0252,  X0252Add, X0252Edit, X0252Delete, X0252Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
        command.CommandText = "SELECT x0259,  x0259Add, x0259Edit, x0259Void, x0259Print, x0259Submit, x0259Cancel FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

        command.Connection = conn

        Dim dr As MySqlDataReader = command.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)
        'conn.Close()
        'conn.Dispose()
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)("x0259")) Then
                If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0259"))) = False Then
                    If Convert.ToBoolean(dt.Rows(0)("x0259")) = False Then
                        Response.Redirect("Home.aspx")
                    End If
                End If
            End If

            If Not IsDBNull(dt.Rows(0)("x0259Add")) Then
                If String.IsNullOrEmpty(dt.Rows(0)("x0259Add")) = False Then
                    Me.btnADD.Enabled = dt.Rows(0)("x0259Add").ToString()
                End If
            End If


            If txtMode.Text = "View" Then
                If Not IsDBNull(dt.Rows(0)("x0259Edit")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0259Edit")) = False Then
                        Me.btnEdit.Enabled = dt.Rows(0)("x0259Edit").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0259Void")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0259Void")) = False Then
                        Me.btnDelete.Enabled = dt.Rows(0)("x0259Void").ToString()
                    End If
                End If

                If Not IsDBNull(dt.Rows(0)("x0259Print")) Then
                    If String.IsNullOrEmpty(dt.Rows(0)("x0259Print")) = False Then
                        Me.btnPrint.Enabled = dt.Rows(0)("x0259Print").ToString()
                    End If
                End If

                If btnPostEInvoice.Text = "SUBMIT CONSOLIDATED INVOICE" Then
                    If Not IsDBNull(dt.Rows(0)("X0259Submit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0259Submit")) = False Then
                            Me.btnPostEInvoice.Enabled = dt.Rows(0)("X0259Submit").ToString()
                        End If
                    End If
                End If

                If btnPostEInvoice.Text = "CANCEL CONSOLIDATED INVOICE" Then
                    If Not IsDBNull(dt.Rows(0)("X0259Cancel")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0259Cancel")) = False Then
                            Me.btnPostEInvoice.Enabled = dt.Rows(0)("X0259Cancel").ToString()
                        End If
                    End If
                End If




            Else
                '   Me.btnAdd.Enabled = False
                '  btnAdd.ForeColor = System.Drawing.Color.Gray
                Me.btnEdit.Enabled = False
                btnEdit.ForeColor = System.Drawing.Color.Gray
                Me.btnDelete.Enabled = False
                btnDelete.ForeColor = System.Drawing.Color.Gray
                Me.btnPrint.Enabled = False
                btnPrint.ForeColor = System.Drawing.Color.Gray
                Me.btnPostEInvoice.Enabled = False
                btnPostEInvoice.ForeColor = System.Drawing.Color.Gray
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

            'If btnPost.Enabled = True Then
            '    btnPost.ForeColor = System.Drawing.Color.Black
            'Else
            '    btnPost.ForeColor = System.Drawing.Color.Gray
            'End If

            If btnPostEInvoice.Enabled = True Then
                btnPostEInvoice.ForeColor = System.Drawing.Color.Black
            Else
                btnPostEInvoice.ForeColor = System.Drawing.Color.Gray
            End If

        End If

        conn.Close()
        conn.Dispose()
        command.Dispose()
        dt.Dispose()
        dr.Close()
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

            'updPnlBillingRecs.Update()

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

    Private Sub PopulateInvoiceGrid()

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

            InsertIntoTblWebEventLog("CInvoice", "PopulateInvoiceGrid", txtMode.Text, txtCreatedBy.Text)

            If txtMode.Text = "View" Then
                '    UpdatePanel3.Update()
                sql = "select ConsolidatedInvoiceNo,CIDate,AccountId,CustName,InvoiceNo as InvoiceNumber,SalesDate,BillingFrequency,LocationId,Address1,BillAmount,RcnoInvoice,RcnoInvoiceDetail,BillNo,ContractNo,Name,OurRef,YourRef,PoNo,CreditTerms,Salesman,BillAddress1,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactType,CompanyGroup,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Rcno,DiscountPerc,DiscountAmount,GSTAmount,TotalWithGST,NetAmount,Status,ContractGroup,Posted,RetentionAmt,BillSchedule,ArTerm,TermsDay,Location,Remarks,Gst,GstRate,Internal,SubmissionDate,DocTypeCode,CancellationDate,CancellationReason,TaxIdentificationNo,BRN,SalesTaxRegistrationNo,LongID,SubmissionBy,EI,EInvoiceStatus,UUID,SubmissionID, Valuebase, Appliedbase, Creditbase, Receiptbase, Balancebase,GstBase "
                sql = sql + " FROM tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "'"

            End If

            If txtMode.Text = "EDIT" Then
                '   UpdatePanel3.Update()
                sqledit = "select ConsolidatedInvoiceNo,CIDate,AccountId,CustName,InvoiceNo,SalesDate,BillingFrequency,LocationId,Address1,BillAmount,RcnoInvoice,RcnoInvoiceDetail,BillNo,ContractNo,Name,OurRef,YourRef,PoNo,CreditTerms,Salesman,BillAddress1,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactType,CompanyGroup,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,Rcno,DiscountPerc,DiscountAmount,GSTAmount,TotalWithGST,NetAmount,Status,ContractGroup,Posted,RetentionAmt,BillSchedule,ArTerm,TermsDay,Location,Remarks,Gst,GstRate,Internal,SubmissionDate,DocTypeCode,CancellationDate,CancellationReason,TaxIdentificationNo,BRN,SalesTaxRegistrationNo,LongID,SubmissionBy,EI,EInvoiceStatus,UUID,SubmissionID,GstBase "
                sqledit = sqledit + " FROM tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "'"

            End If

            If txtMode.Text = "NEW" Then

                'sql = "select * from tblsales A where DocType='ARIN' and PostStatus='P' and (EI='N' OR EI IS NULL OR EI='') and CEI='N' and InvoiceNumber not in ("
                'sql = sql + "select InvoiceNumber from tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "')"

                sql = "select * from tblsales A where DocType='ARIN' and PostStatus='P' and (EI='N' OR EI IS NULL OR EI='') and CEI='N' and Location = '" & txtLocation.Text & "' and InvoiceNumber not in ("
                sql = sql + "select InvoiceNumber from tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "')"
                'sql = sql + "select InvoiceNumber from tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "')"

                'If txtDisplayRecordsLocationwise.Text = "Y" Then
                '    sql = sql + " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                'End If

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

                'If String.IsNullOrEmpty(txtLocationId.Text.Trim) = False Then
                '    sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
                'End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = False And String.IsNullOrEmpty(txtDateTo.Text) = True Then
                    sql = sql + " and   A.SALESDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = True And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.SalesDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.SalesDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If txtGst.SelectedIndex > 0 Then
                    sql = sql + " and A.Gst = '" & txtGst.Text & "'"
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

                sql = "select * from tblsales A where DocType='ARIN' and PostStatus='P' and (EI='N' OR EI IS NULL OR EI='') and InvoiceNumber not in ("
                sql = sql + "select InvoiceNumber from tbleinvoiceconsolidateddetail where ConsolidatedInvoiceNo = '" & txtInvoiceNo.Text & "')"

                'If txtDisplayRecordsLocationwise.Text = "Y" Then
                '    sql = sql + " and A.location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')"
                'End If

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

                'If String.IsNullOrEmpty(txtLocationId.Text.Trim) = False Then
                '    sql = sql + " and   A.LocationId = '" & txtLocationId.Text & "'"
                'End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = False And String.IsNullOrEmpty(txtDateTo.Text) = True Then
                    sql = sql + " and   A.SALESDate >= '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text.Trim) = True And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.SalesDate <= '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If String.IsNullOrEmpty(txtDateFrom.Text) = False And String.IsNullOrEmpty(txtDateTo.Text) = False Then
                    sql = sql + " and   A.SalesDate between '" & Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd") & "' and '" & Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd") & "'"
                End If

                If txtGst.SelectedIndex > 0 Then
                    sql = sql + " and A.Gst = '" & txtGst.Text & "'"
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


            '  sql = sql + " group by A.InvoiceNumber "
            sql1 = sql

            txtShowRecords.Text = sql
            txtShowRecordsSort.Text = sql

            sql2 = sql2 + " order by SalesDate,AccountID limit 50"


            txtShowRecords.Text = sql1
            txtShowRecordsSort.Text = sql2

            sql = sql1 + sql2

            InsertIntoTblWebEventLog("CInvoice2", "PopulateInvoiceGrid", sql, txtCreatedBy.Text)


            SqlDSCEInvoice.SelectCommand = sql
            grvInvoiceRecDetails.DataSourceID = "SqlDSCEInvoice"
            grvInvoiceRecDetails.DataBind()


            Label43.Text = "INVOICE DETAILS : Total Records :" & grvInvoiceRecDetails.Rows.Count.ToString
            '  updpnlInvoiceRecs.Update()

            InsertIntoTblWebEventLog("CInvoice3", "PopulateInvoiceGrid", grvInvoiceRecDetails.Rows.Count.ToString, txtCreatedBy.Text)


            'txtClientName.Text = sql

            'Dim table As DataTable = TryCast(ViewState("CurrentTableInvoiceRec"), DataTable)

            If txtMode.Text = "View" Then
                If txtPostStatus.Text = "O" Then
                    If (grvInvoiceRecDetails.Rows.Count > 0) Then
                        For i As Integer = 0 To (grvInvoiceRecDetails.Rows.Count) - 1
                            Dim TextBoxSel As CheckBox = CType(grvInvoiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)
                            TextBoxSel.Enabled = True
                            TextBoxSel.Checked = True
                        Next i
                    End If

                Else
                    If (grvInvoiceRecDetails.Rows.Count > 0) Then
                        For i As Integer = 0 To (grvInvoiceRecDetails.Rows.Count) - 1
                            Dim TextBoxSel As CheckBox = CType(grvInvoiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)
                            TextBoxSel.Enabled = False
                            TextBoxSel.Checked = True
                        Next i
                    End If

                End If

            End If
            'End: Service Recods
            '   grvInvoiceRecDetails.Enabled = True

            conn.Close()
            conn.Dispose()

            InsertIntoTblWebEventLog("CInvoice4", "PopulateInvoiceGrid", sql, txtCreatedBy.Text)

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("INVOICE CONSOLIDATED - " + Session("UserID"), "PopulateInvoiceGrid", ex.Message.ToString, "")
            Exit Sub
        End Try
    End Sub

    'Private Sub DisplayGLGrid()
    '    Try

    '        ''''''''''''''''' Start: Display GL Grid

    '        FirstGridViewRowGL()

    '        '  updPnlBillingRecs.Update()

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

    '        Dim TextBoxDebitAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '        '    TextBoxDebitAmountAR.Text = Convert.ToDecimal(txtNetAmount.Text).ToString("N2")

    '        Dim TextBoxCreditAmountAR As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '        TextBoxCreditAmountAR.Text = (0.0).ToString("N2")


    '        ''GST values

    '        rowIndex3 += 1
    '        AddNewRowGL()
    '        Dim TextBoxGLCodeGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '        TextBoxGLCodeGST.Text = txtGSTOutputCode.Text

    '        Dim TextBoxGLDescriptionGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '        TextBoxGLDescriptionGST.Text = txtGSTOutputDescription.Text

    '        Dim TextBoxDebitAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '        TextBoxDebitAmountGST.Text = (0.0).ToString("N2")

    '        Dim TextBoxCreditAmountGST As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '        '  TextBoxCreditAmountGST.Text = Convert.ToDecimal(txtGSTAmount.Text).ToString("N2")
    '        ''GST Values



    '        rowIndex3 += 1
    '        AddNewRowGL()
    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '            conn.Dispose()
    '        End If
    '        conn.Open()

    '        Dim cmdGL As MySqlCommand = New MySqlCommand
    '        cmdGL.CommandType = CommandType.Text
    '        'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblservicebillingdetailitem where BatchNo ='" & txtBatchNo.Text.Trim & "' order by OtherCode"
    '        'cmdGL.CommandText = "SELECT OtherCode, GLDescription, PriceWithDisc   FROM tblservicebillingdetailitem where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by OtherCode"
    '        cmdGL.CommandText = "SELECT OtherCode, GLDescription, BillAmount   FROM tblservicebillingdetail where BillSchedule ='" & txtInvoiceNo.Text.Trim & "' order by OtherCode"

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


    '            lGLCode = dtGL.Rows(0)("OtherCode").ToString()
    '            lGLDescription = dtGL.Rows(0)("GLDescription").ToString()
    '            lCreditAmount = 0.0

    '            Dim rowIndex4 = 0

    '            For Each row As DataRow In dtGL.Rows

    '                If lGLCode = row("OtherCode") Then
    '                    lCreditAmount = lCreditAmount + row("BillAmount")
    '                Else


    '                    If (TotDetailRecordsLoc > (rowIndex4 + 1)) Then
    '                        AddNewRowGL()
    '                    End If

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    TextBoxGLCode.Text = lGLCode

    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtGLDescriptionGV"), TextBox)
    '                    TextBoxGLDescription.Text = lGLDescription

    '                    Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '                    TextBoxDebitAmount.Text = (0.0).ToString("N2")

    '                    Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '                    TextBoxCreditAmount.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")

    '                    lGLCode = row("OtherCode")
    '                    lGLDescription = row("GLDescription")
    '                    lCreditAmount = row("BillAmount")

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

    '        Dim TextBoxDebitAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtDebitAmountGV"), TextBox)
    '        TextBoxDebitAmount1.Text = (0.0).ToString("N2")

    '        Dim TextBoxCreditAmount1 As TextBox = CType(grvGL.Rows(rowIndex3).Cells(0).FindControl("txtCreditAmountGV"), TextBox)
    '        TextBoxCreditAmount1.Text = Convert.ToDecimal(lCreditAmount).ToString("N2")


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
    '        dtScdrLoc.Dispose()
    '        dtScdrLoc1.Dispose()

    '        drcmdGL.Close()

    '        ''''''''''''''''' End: Display GL Grid
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '    End Try
    'End Sub



    'Private Sub FirstGridViewRowGL()
    '    Try
    '        Dim dt As New DataTable()
    '        Dim dr As DataRow = Nothing
    '        'dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

    '        dt.Columns.Add(New DataColumn("GLCode", GetType(String)))
    '        dt.Columns.Add(New DataColumn("GLDescription", GetType(String)))
    '        dt.Columns.Add(New DataColumn("DebitAmount", GetType(String)))
    '        dt.Columns.Add(New DataColumn("CreditAmount", GetType(String)))

    '        dr = dt.NewRow()

    '        dr("GLCode") = String.Empty
    '        dr("GLDescription") = String.Empty
    '        dr("DebitAmount") = String.Empty
    '        dr("CreditAmount") = String.Empty
    '        dt.Rows.Add(dr)

    '        ViewState("CurrentTableGL") = dt

    '        grvGL.DataSource = dt
    '        grvGL.DataBind()

    '        'Dim btnAdd As Button = CType(grvInvoiceRecDetails.FooterRow.Cells(1).FindControl("btnViewEdit"), Button)
    '        'Page.Form.DefaultFocus = btnAdd.ClientID

    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub




    'Private Sub AddNewRowGL()
    '    Try
    '        Dim rowIndex As Integer = 0
    '        'Dim Query As String

    '        If ViewState("CurrentTableGL") IsNot Nothing Then
    '            Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing
    '            If dtCurrentTable.Rows.Count > 0 Then
    '                For i As Integer = 1 To dtCurrentTable.Rows.Count

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
    '                    Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
    '                    Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)
    '                    drCurrentRow = dtCurrentTable.NewRow()

    '                    dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
    '                    dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
    '                    dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
    '                    dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text

    '                    rowIndex += 1

    '                Next i
    '                dtCurrentTable.Rows.Add(drCurrentRow)
    '                ViewState("CurrentTableGL") = dtCurrentTable

    '                grvGL.DataSource = dtCurrentTable
    '                grvGL.DataBind()


    '            End If
    '        Else
    '            Response.Write("ViewState is null")
    '        End If
    '        SetPreviousDataGL()
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub


    'Private Sub AddNewRowWithDetailRecGL()
    '    Try
    '        Dim rowIndex As Integer = 0
    '        'Dim Query As String
    '        If ViewState("CurrentTableGL") IsNot Nothing Then
    '            Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing
    '            If TotDetailRecords > 0 Then
    '                For i As Integer = 1 To (dtCurrentTable.Rows.Count)

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
    '                    Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
    '                    Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

    '                    drCurrentRow = dtCurrentTable.NewRow()

    '                    dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
    '                    dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
    '                    dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
    '                    dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text

    '                    rowIndex += 1

    '                Next i
    '                dtCurrentTable.Rows.Add(drCurrentRow)
    '                ViewState("CurrentTableGL") = dtCurrentTable

    '                'grvBillingDetails.DataSource = dtCurrentTable
    '                'grvBillingDetails.DataBind()
    '            End If
    '        Else
    '            Response.Write("ViewState is null")
    '        End If
    '        SetPreviousDataGL()
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub

    'Private Sub SetPreviousDataGL()
    '    Try
    '        Dim rowIndex As Integer = 0

    '        'Dim Query As String

    '        If ViewState("CurrentTableGL") IsNot Nothing Then
    '            Dim dt As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '            If dt.Rows.Count > 0 Then
    '                For i As Integer = 0 To dt.Rows.Count - 1

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
    '                    Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
    '                    Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

    '                    TextBoxGLCode.Text = dt.Rows(i)("GLCode").ToString()
    '                    TextBoxGLDescription.Text = dt.Rows(i)("GLDescription").ToString()
    '                    TextBoxDebitAmount.Text = dt.Rows(i)("DebitAmount").ToString()
    '                    TextBoxCreditAmount.Text = dt.Rows(i)("CreditAmount").ToString()


    '                    rowIndex += 1
    '                Next i
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub

    'Private Sub SetRowDataGL()
    '    Dim rowIndex As Integer = 0
    '    Try
    '        If ViewState("CurrentTableGL") IsNot Nothing Then
    '            Dim dtCurrentTable As DataTable = CType(ViewState("CurrentTableGL"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing
    '            If dtCurrentTable.Rows.Count > 0 Then
    '                For i As Integer = 1 To dtCurrentTable.Rows.Count

    '                    Dim TextBoxGLCode As TextBox = CType(grvGL.Rows(rowIndex).Cells(0).FindControl("txtGLCodeGV"), TextBox)
    '                    Dim TextBoxGLDescription As TextBox = CType(grvGL.Rows(rowIndex).Cells(1).FindControl("txtGLDescriptionGV"), TextBox)
    '                    Dim TextBoxDebitAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(2).FindControl("txtDebitAmountGV"), TextBox)
    '                    Dim TextBoxCreditAmount As TextBox = CType(grvGL.Rows(rowIndex).Cells(3).FindControl("txtCreditAmountGV"), TextBox)

    '                    drCurrentRow = dtCurrentTable.NewRow()

    '                    dtCurrentTable.Rows(i - 1)("GLCode") = TextBoxGLCode.Text
    '                    dtCurrentTable.Rows(i - 1)("GLDescription") = TextBoxGLDescription.Text
    '                    dtCurrentTable.Rows(i - 1)("DebitAmount") = TextBoxDebitAmount.Text
    '                    dtCurrentTable.Rows(i - 1)("CreditAmount") = TextBoxCreditAmount.Text


    '                    rowIndex += 1
    '                Next i

    '                ViewState("CurrentTableGL") = dtCurrentTable


    '            End If
    '        Else
    '            Response.Write("ViewState is null")
    '        End If
    '        SetPreviousDataGL()
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try

    'End Sub


    Protected Sub btnADD_Click(sender As Object, e As EventArgs) Handles btnADD.Click
        ' Try
        InsertIntoTblWebEventLog("CONSOLDATED INV", "btnAdd1", txtInvoiceDate.Text, txtCreatedBy.Text)

        'txtInvoiceDate.Text = ""

        MakeMeNull()


        EnableControls()

        Me.cpnl2.Collapsed = False
        Me.cpnl2.ClientState = "False"

        btnADD.Enabled = False
        btnADD.ForeColor = Color.Gray
        txtInvoiceDate.Enabled = True
        txtGst.Enabled = True

        txtMode.Text = "NEW"
        lblMessage.Text = "ACTION: ADD RECORD"
        '   txtBillingPeriod.Text = Year(Convert.ToDateTime(txtInvoiceDate.Text)) & Format(Month(Convert.ToDateTime(txtInvoiceDate.Text)), "00")

        'Session.Remove("buttonclicked")
        'InsertIntoTblWebEventLog("CONSOLDATED INV", "btnAdd5", txtInvoiceDate.Text, txtCreatedBy.Text)

        'Catch ex As Exception
        '    lblAlert.Text = ex.Message
        '    InsertIntoTblWebEventLog("CONSOLIDATED INV - " + Session("UserID"), "btnAdd_Click", ex.Message.ToString, "")
        '    Exit Sub
        'End Try
    End Sub

    Public Sub MakeMeNull()

        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""

        txtAccountId.Text = ""
        txtClientName.Text = ""
  
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
      
        txtRcno.Text = "0"

        txtInvoiceNo.Text = ""
        txtInvoiceDate.Text = ""
        txtLocation.SelectedIndex = 0
        txtGst.SelectedIndex = 0

        txtBillingPeriod.Text = ""

        ddlContactType.SelectedIndex = 0

        Label43.Text = "INVOICE DETAILS"
        ' updPnlMsg.Update()
        ' updPanelInvoice.Update()
        '   DisableControls()

           grvInvoiceRecDetails.DataSourceID = ""
        grvInvoiceRecDetails.DataBind()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "currentdatetimeinvoice();", True)
        Me.cpnl1.Collapsed = True
        Me.cpnl1.ClientState = "True"

        Me.cpnl2.Collapsed = False
        Me.cpnl2.ClientState = "False"


    End Sub


    '    Private Sub MakeMeNullBillingDetails()

    '    txtInvoiceNo.Text = ""
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
    '  txtLastSalesDetailRcno.Text = "0"

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
    ''End If

    'chkRecurringInvoice.Checked = False
    'chkRecurringInvoice.Enabled = True
    'txtRecurringInvoiceDate.Text = ""
    'txtRecurringServiceDateFrom.Text = ""
    'txtRecurringServiceDateTo.Text = ""
    ''chkRecurringInvoice.Enabled = True
    'ddlRecurringFrequency.SelectedIndex = 0


    'txtInvoiceAmount.Text = 0.0
    'txtDiscountAmount.Text = 0.0
    'txtAmountWithDiscount.Text = 0.0
    'txtGSTAmount.Text = 0.0
    'txtNetAmount.Text = 0.0

    'txtTotal.Text = "0.00"
    'txtTotalWithGST.Text = "0.00"
    'txtTotalGSTAmt.Text = "0.00"
    'txtTotalDiscAmt.Text = "0.00"
    'txtTotalWithDiscAmt.Text = "0.00"
    'txtCreditDays.Text = "0"
    'rdbGrouping.Enabled = True
    'rdbGrouping.SelectedIndex = 0
    'rdbCompleted.Checked = True
    'UpdatePanel1.Update()
    '  FirstGridViewRowGL()

    '   End Sub


    Private Sub EnableControls()
         btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnShowRecords.Enabled = True
        btnShowRecords.ForeColor = System.Drawing.Color.Black

        btnADD.Enabled = False
        btnADD.ForeColor = Color.Gray

        btnDelete.Enabled = False
        btnDelete.ForeColor = System.Drawing.Color.Gray
        ddlContactType.Enabled = True
        txtClientName.Enabled = True
        txtAccountId.Enabled = True
        'txtLocationId.Enabled = True
        'ddlContractNo.Enabled = True
        txtDateFrom.Enabled = True
        txtDateTo.Enabled = True
        txtLocation.Enabled = True
        ddlLocationSearch.Enabled = True

        txtInvoiceNo.Enabled = True
        txtInvoiceDate.Enabled = True
    
        '    grvInvoiceRecDetails.Enabled = True

        txtInvoicenoSearch.Enabled = False

        txtUUIDSearch.Enabled = False
        txtToInvoiceDate.Enabled = False
        txtFromInvoiceDate.Enabled = False
        txtSearch1Status.Enabled = False


        btnQuickSearch.Enabled = False
        btnQuickReset.Enabled = False

        '   grvBillingDetails.Enabled = True
        '   grvInvoiceRecDetails.Enabled = True
        'updPnlSearch.Update()
        ''  updPnlBillingRecs.Update()
        'updpnlInvoiceRecs.Update()
        '' updpnlBillingDetails.Update()
        ''  updPanelSave.Update()
        '  UpdatePanel1.Update()

        btnClient.Visible = True
        ' BtnLocation.Visible = True

        'ImageButton1.Visible = True
        'ImageButton5.Visible = True
    End Sub

    Protected Sub btnShowRecords_Click(sender As Object, e As EventArgs) Handles btnShowRecords.Click
        'MakeMeNull()
        lblAlert.Text = ""
        If txtLocation.SelectedIndex = 0 Then
            ButtonPressed = "N"
            lblAlert.Text = "PLEASE SELECT BRANCH"
            '    updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If txtGst.SelectedIndex = 0 Then
            ButtonPressed = "N"
            lblAlert.Text = "PLEASE SELECT TAX TYPE"
            '    updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        Dim dt1 As Date
        If String.IsNullOrEmpty(txtDateFrom.Text) = False Then
            If Date.TryParseExact(txtDateFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt1) Then
                txtDateFrom.Text = dt1.ToShortDateString

            Else
                lblAlert.Text = "'Invoice Date From' is Invalid"
                '  updPnlMsg.Update()
                txtDateFrom.Focus()
                Return
                Exit Sub

            End If
        Else
            lblAlert.Text = "The Invoice Date From and Date To fields are required to show the Invoice Records."
            '  updPnlMsg.Update()
            txtDateFrom.Focus()
            Return
            Exit Sub
        End If

        Dim dt2 As Date
        If String.IsNullOrEmpty(txtDateTo.Text) = False Then
            If Date.TryParseExact(txtDateTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, dt2) Then
                txtDateTo.Text = dt2.ToShortDateString

            Else
                lblAlert.Text = "'Invoice Date To' is Invalid"
                '    updPnlMsg.Update()
                txtDateTo.Focus()
                Return
                Exit Sub

            End If
        Else
            lblAlert.Text = "The Invoice Date From and Date To fields are required to show the Invoice Records."
            '  updPnlMsg.Update()
            txtDateFrom.Focus()
            Return
            Exit Sub
        End If



        '   updPanelInvoice.Update()

        PopulateInvoiceGrid()

        '  grvInvoiceRecDetails.Enabled = True

        InsertIntoTblWebEventLog("Invoice", "btnShowRecords_Click1", grvInvoiceRecDetails.Rows.Count.ToString, txtCreatedBy.Text)

        '    updpnlInvoiceRecs.Update()
        InsertIntoTblWebEventLog("Invoice", "btnShowRecords_Click2", grvInvoiceRecDetails.Rows.Count.ToString, txtCreatedBy.Text)

        btnSaveInvoice.Enabled = True
        btnSaveInvoice.ForeColor = System.Drawing.Color.Black

        '  ddlLocationSearch.ReadOnly = True
        txtLocation.Enabled = False
        txtInvoiceDate.Enabled = False
        txtGst.Enabled = False

        btnDeleteUnselected.Enabled = True
        btnDeleteUnselected.ForeColor = System.Drawing.Color.Black
        '    updPanelSave.Update()
        '    updPanelInvoice.Update()
        InsertIntoTblWebEventLog("Invoice", "btnShowRecords_Click3", grvInvoiceRecDetails.Rows.Count.ToString, txtCreatedBy.Text)

        'btnSave.Enabled = True
        'updPanelSave.Update()
    End Sub

    Protected Sub grvInvoiceRecDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grvInvoiceRecDetails.SelectedIndexChanged

    End Sub

    Protected Sub grvInvoiceRecDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvInvoiceRecDetails.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '   InsertIntoTblWebEventLog("CI", "1", txtMode.Text, txtCreatedBy.Text)

            Dim ch As CheckBox = CType(e.Row.FindControl("chkSelectGV"), CheckBox)
            'If txtMode.Text.ToUpper = "ADD" Then
            '    ' InsertIntoTblWebEventLog("CI", "2", txtMode.Text, txtCreatedBy.Text)
            '    ' ch.Attributes.Add("enabled", "enabled")
            '    ch.Enabled = True
            'Else
            '    '   e.Row.Cells(0).Attributes("disabled") = "disabled"
            '    '  ch.Attributes.Add("disabled", "disabled")
            '    ch.Enabled = False
            '    '  InsertIntoTblWebEventLog("CI", "3", txtMode.Text, txtCreatedBy.Text)

            'End If
        End If
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


    Private Sub CalculateInvoiceTotalPrice()
        Try
            Dim TotalSalesAmt As Decimal = 0
            Dim TotalAmt As Decimal = 0
            Dim TotalDiscAmt As Decimal = 0
            Dim TotalWithDiscAmt As Decimal = 0
            Dim TotalGSTAmt As Decimal = 0
            Dim TotalAmtWithGST As Decimal = 0
            'Dim table As DataTable = TryCast(ViewState("CurrentTableServiceRec"), DataTable)


            'If (table.Rows.Count > 0) Then
            If grvInvoiceRecDetails.Rows.Count > 0 Then
                For i As Integer = 0 To (grvInvoiceRecDetails.Rows.Count) - 1

                    Dim TextBoxchkSelect As CheckBox = CType(grvInvoiceRecDetails.Rows(i).Cells(0).FindControl("chkSelectGV"), CheckBox)

                    If TextBoxchkSelect.Checked = True Then
                        Dim TextBoxSales As TextBox = CType(grvInvoiceRecDetails.Rows(i).Cells(13).FindControl("txtSalesAmtGV"), TextBox)
                        Dim TextBoxGst As TextBox = CType(grvInvoiceRecDetails.Rows(i).Cells(13).FindControl("txtGSTAmtGV"), TextBox)

                        Dim TextBoxTotal As TextBox = CType(grvInvoiceRecDetails.Rows(i).Cells(13).FindControl("txtToBillAmtGV"), TextBox)

                        If String.IsNullOrEmpty(TextBoxTotal.Text) = True Then
                            TextBoxTotal.Text = "0.00"
                        End If

                        TotalSalesAmt = TotalSalesAmt + Convert.ToDecimal(TextBoxSales.Text)
                        TotalGSTAmt = TotalGSTAmt + Convert.ToDecimal(TextBoxGst.Text)
                        TotalAmt = TotalAmt + Convert.ToDecimal(TextBoxTotal.Text)
                    End If

                Next i

            End If

            txtInvoiceAmount.Text = Convert.ToDecimal(TotalSalesAmt).ToString("N2")
            txtGSTAmount.Text = Convert.ToDecimal(TotalGSTAmt).ToString("N2")
            txtNetAmount.Text = Convert.ToDecimal(TotalAmt).ToString("N2")

            'txtTotal.Text = TotalAmt.ToString
            txtTotalWithGST.Text = txtNetAmount.ToString

            txtTotalDiscAmt.Text = 0.0
            txtTotalGSTAmt.Text = TotalGSTAmt.ToString

            txtTotalWithDiscAmt.Text = txtInvoiceAmount.ToString
            '  UpdatePanel3.Update()
            '  updPanelSave.Update()
        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
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
        If Date.TryParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, d) Then
            strdate = d.ToShortDateString
        End If

        lPrefix = Format(CDate(strdate), "yyyyMM")
        lInvoiceNo = "CAR" + lPrefix + "-"
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
        '  UpdatePanel3.Update()
    End Sub

    Protected Sub btnSaveInvoice_Click(sender As Object, e As EventArgs) Handles btnSaveInvoice.Click
        InsertIntoTblWebEventLog("CONSOLIDATED INV", "btnSaveInvoice_Click", "1", txtCreatedBy.Text)
        'If Session("CheckRefresh").ToString() = ViewState("CheckRefresh").ToString() Then

        lblAlert.Text = ""
        If String.IsNullOrEmpty(txtInvoiceDate.Text) = True Then
            ButtonPressed = "N"
            lblAlert.Text = "PLEASE ENTER INVOICE DATE"
            ' updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        If txtDisplayRecordsLocationwise.Text = "Y" Then
            If txtLocation.SelectedIndex = 0 Then
                ButtonPressed = "N"
                lblAlert.Text = "PLEASE SELECT BRANCH"
                '    updPnlMsg.Update()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
                Exit Sub
            End If
        End If



        Dim IsLock = FindARPeriod(txtBillingPeriod.Text)
        If IsLock = "Y" Then
            ButtonPressed = "N"
            lblAlert.Text = "PERIOD IS LOCKED"
            '    updPnlMsg.Update()
            txtInvoiceDate.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If



        txtClientName.Text = txtClientName.Text.ToUpper
        '   txtLocationId.Text = txtLocationId.Text.ToUpper

        Dim totalRows As Long
        totalRows = 0

        '29.07.25

        For rowIndex2 As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
            Dim TextBoxchkSelect1 As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex2).Cells(0).FindControl("chkSelectGV"), CheckBox)
            If (TextBoxchkSelect1.Checked = True) Then
                totalRows = totalRows + 1

            End If
        Next rowIndex2
        'End If




        If totalRows > CInt(txtMaxNoofInvinCI.Text) Then
            lblAlert.Text = "CANNOT SAVE .. MAXIMUM INVOICES CAN BE SELECTED " & txtMaxNoofInvinCI.Text
            ButtonPressed = "N"
            lblAlert.Focus()
            ' updPnlMsg.Update()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        '29.07.25

        totalRows = 0

        For rowIndex1 As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
            Dim TextBoxchkSelect1 As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex1).Cells(0).FindControl("chkSelectGV"), CheckBox)
            If (TextBoxchkSelect1.Checked = True) Then
                totalRows = totalRows + 1
                GoTo insertRec2
            End If
        Next rowIndex1
        'End If


        If totalRows = 0 Then
            lblAlert.Text = "PLEASE SELECT INVOICES TO CONSOLDATE"
            ButtonPressed = "N"
            lblAlert.Focus()
            ' updPnlMsg.Update()
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
            Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableInvoiceRec"), DataTable)

            'If tableAdd IsNot Nothing Then

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' PopulateGLCodes()
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

            Dim noofRecords As Integer
            noofRecords = 0

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


            CalculateInvoiceTotalPrice()

            ''''''''''''''' tlBillSchedule

            Dim commandBillSchedule As MySqlCommand = New MySqlCommand
            commandBillSchedule.CommandType = CommandType.Text

            Dim qry As String
            If txtMode.Text = "NEW" Then

                qry = "INSERT INTO tbleinvoiceconsolidated(ConsolidatedInvoiceNo,CIDate,PostStatus,UUID,ContType,CustName,Frequency,"
                qry = qry + "InCharge,Support,Description,Contract,BillingDate,InvoiceFrom,InvoiceTo,GlPeriod,CreatedBy,"
                qry = qry + "CreatedOn,LastModifiedBy,LastModifiedOn,AccountId,BillAmount,DiscountAmount,AmountWithDiscount,GSTAmount,NetAmount,"
                qry = qry + "Location,Gst)VALUES(@ConsolidatedInvoiceNo,@CIDate,@PostStatus,@UUID,@ContType,@CustName,@Frequency,@InCharge,"
                qry = qry + "@Support,@Description,@Contract,@BillingDate,@InvoiceFrom,@InvoiceTo,@GlPeriod,@CreatedBy,"
                qry = qry + "@CreatedOn,@LastModifiedBy,@LastModifiedOn,@AccountId,@BillAmount,@DiscountAmount,@AmountWithDiscount,@GSTAmount,"
                qry = qry + "@NetAmount,@Location,@Gst)"

                commandBillSchedule.CommandText = qry
                commandBillSchedule.Parameters.Clear()

                If txtMode.Text = "NEW" Then
                    GenerateBillingSchedule()
                End If

                commandBillSchedule.Parameters.AddWithValue("@ConsolidatedInvoiceNo", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@CIDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                commandBillSchedule.Parameters.AddWithValue("@BillingDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))

                commandBillSchedule.Parameters.AddWithValue("@PostStatus", "O")
                commandBillSchedule.Parameters.AddWithValue("@UUID", "")
                If ddlContactType.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@ContType", ddlContactType.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ContType", "")
                End If

                commandBillSchedule.Parameters.AddWithValue("@CustName", txtClientName.Text)
                commandBillSchedule.Parameters.AddWithValue("@Frequency", "")
                commandBillSchedule.Parameters.AddWithValue("@Incharge", "")
                commandBillSchedule.Parameters.AddWithValue("@Support", "")
                commandBillSchedule.Parameters.AddWithValue("@Description", "")
                commandBillSchedule.Parameters.AddWithValue("@Contract", "")
                commandBillSchedule.Parameters.AddWithValue("@GlPeriod", txtBillingPeriod.Text)

                If txtDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceFrom", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceFrom", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                End If


                If txtDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceTo", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceTo", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                commandBillSchedule.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                commandBillSchedule.Parameters.AddWithValue("@AccountId", txtAccountId.Text)

                commandBillSchedule.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtInvoiceAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                commandBillSchedule.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                commandBillSchedule.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text))

                If txtLocation.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@Location", txtLocation.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@Location", "")
                End If
                commandBillSchedule.Parameters.AddWithValue("@Gst", txtGst.Text)


                '08.03.17

                commandBillSchedule.Connection = conn
                commandBillSchedule.ExecuteNonQuery()

                Dim sqlLastId As String
                sqlLastId = "SELECT last_insert_id() from tbleinvoiceconsolidated"

                Dim commandRcno As MySqlCommand = New MySqlCommand
                commandRcno.CommandType = CommandType.Text
                commandRcno.CommandText = sqlLastId
                commandRcno.Parameters.Clear()
                commandRcno.Connection = conn
                txtRcno.Text = commandRcno.ExecuteScalar()

                ''''''''''''''' tblBillSchedule
            Else
                qry = "UPDATE  tbleinvoiceconsolidated(SET CIDate = @CIDate,PostStatus = @PostStatus,UUID = @UUID,ContType = @ContType,"
                qry = qry + "CustName = @CustName,Frequency = @Frequency,InCharge = @InCharge,Support = @Support,Description = @Description,"
                qry = qry + "InvoiceFrom = @InvoiceFrom,InvoiceTo = @InvoiceTo,CreatedBy = @CreatedBy,CreatedOn = @CreatedOn,"
                qry = qry + "LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,AccountId = @AccountId,BillAmount = @BillAmount,"
                qry = qry + "DiscountAmount = @DiscountAmount,AmountWithDiscount = @AmountWithDiscount,GSTAmount = @GSTAmount,"
                qry = qry + "NetAmount = @NetAmount,Location = @Location,Gst=@Gst "
                qry = qry + " where ConsolidatedInvoiceNo= @ConsolidatedInvoiceNo; "

                commandBillSchedule.CommandText = qry
                commandBillSchedule.Parameters.Clear()

                commandBillSchedule.Parameters.AddWithValue("@ConsolidatedInvoiceNo", txtInvoiceNo.Text)
                commandBillSchedule.Parameters.AddWithValue("@CIDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                commandBillSchedule.Parameters.AddWithValue("@PostStatus", "O")
                commandBillSchedule.Parameters.AddWithValue("@UUID", "")
                If ddlContactType.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@ContType", ddlContactType.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@ContType", "")
                End If

                commandBillSchedule.Parameters.AddWithValue("@CustName", txtClientName.Text)
                commandBillSchedule.Parameters.AddWithValue("@Frequency", "")
                commandBillSchedule.Parameters.AddWithValue("@Incharge", "")
                commandBillSchedule.Parameters.AddWithValue("@Support", "")
                commandBillSchedule.Parameters.AddWithValue("@Description", "")
                commandBillSchedule.Parameters.AddWithValue("@Contract", "")

                If txtDateFrom.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceFrom", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceFrom", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd"))
                End If


                If txtDateTo.Text.Trim = "" Then
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceTo", DBNull.Value)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@InvoiceTo", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd"))
                End If

                commandBillSchedule.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                commandBillSchedule.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                commandBillSchedule.Parameters.AddWithValue("@AccountId", txtAccountId.Text)

                commandBillSchedule.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtInvoiceAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtGSTAmount.Text))
                commandBillSchedule.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtNetAmount.Text))

                commandBillSchedule.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(DiscAmount))
                commandBillSchedule.Parameters.AddWithValue("@AmountWithDiscount", Convert.ToDecimal(txtInvoiceAmount.Text))

                If txtLocation.SelectedIndex > 0 Then
                    commandBillSchedule.Parameters.AddWithValue("@Location", txtLocation.Text)
                Else
                    commandBillSchedule.Parameters.AddWithValue("@Location", "")
                End If
                commandBillSchedule.Parameters.AddWithValue("@Gst", txtGst.Text)


                commandBillSchedule.Connection = conn
                commandBillSchedule.ExecuteNonQuery()

            End If

            For rowIndex As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
                'string txSpareId = row.ItemArray[0] as string;
                Dim TextBoxchkSelect As CheckBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("chkSelectGV"), CheckBox)
                '   Dim lblidRecordType As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtRecordTypeGV"), TextBox)
                Dim lblid4 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtInvoiceNoGV"), TextBox)

                If (TextBoxchkSelect.Checked = True) Then
                    'Dim qry As String
                    qry = ""

                    Dim command As MySqlCommand = New MySqlCommand
                    command.CommandType = CommandType.Text
                    'Header
                    rowselected = rowselected + 1
                    Dim lblidInvoiceNo As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtInvoiceNoGV"), TextBox)

                    Dim lblidSalesDate As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtSalesDateGV"), TextBox)
                    Dim lblidAccountID As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtAccountIdGV"), TextBox)
                    Dim lblidCompanyGroup As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).Cells(0).FindControl("txtCompanyGroupGV"), TextBox)
                    Dim lblidClientName As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtClientNameGV"), TextBox)
                    Dim lblidContactType As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtContactTypeGV"), TextBox)

                    Dim TextBoxRcnoInvoice As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtRcnoInvoiceGV"), TextBox)

                    Dim lblid23 As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtToBillAmtGV"), TextBox)


                    Dim lblidLocation As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtLocationGV"), TextBox)

                    Dim lblidValueBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtValueBaseGV"), TextBox)
                    Dim lblidAppliedBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtAppliedBaseGV"), TextBox)
                    Dim lblidReceiptBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtReceiptBaseGV"), TextBox)

                    Dim lblidCreditBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtCreditBaseGV"), TextBox)

                    Dim lblidBalanceBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtBalanceBaseGV"), TextBox)
                    Dim lblidSalesAmt As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtSalesAmtGV"), TextBox)
                    Dim lblidGStAmt As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtGstAmtGV"), TextBox)
                    Dim lblidGStBase As TextBox = CType(grvInvoiceRecDetails.Rows(rowIndex).FindControl("txtGstBaseGV"), TextBox)

                    '                 
                    ToBillAmt = ToBillAmt + Convert.ToDecimal(lblidSalesAmt.Text)
                    GSTAmount = GSTAmount + Convert.ToDecimal(lblidGStAmt.Text)
                    NetAmount = NetAmount + Convert.ToDecimal(lblid23.Text)

                    If txtMode.Text = "NEW" Then
                        '''''''''''''''''''''
                        qry = "INSERT INTO tbleinvoiceconsolidateddetail(ConsolidatedInvoiceNo,CIDate,AccountId,CustName,InvoiceNo,"
                        qry = qry + "SalesDate,RcnoInvoice,BillAddress1,BillBuilding,BillStreet,BillPostal,BillState,BillCity,BillCountry,ContactType,"
                        qry = qry + "CompanyGroup,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,DiscountPerc,DiscountAmount,GSTAmount,TotalWithGST,"
                        qry = qry + "NetAmount,Status,ContractGroup,Posted,Location,Remarks, Valuebase, Appliedbase, Creditbase, Receiptbase, Balancebase,GstBase"
                        qry = qry + ")VALUES(@ConsolidatedInvoiceNo,@CIDate,@AccountId,@CustName,@InvoiceNo,@SalesDate,"
                        qry = qry + "@RcnoInvoice,@BillAddress1,@BillBuilding,@BillStreet,@BillPostal,@BillState,@BillCity,@BillCountry,@ContactType,"
                        qry = qry + "@CompanyGroup,@CreatedBy,@CreatedOn,@LastModifiedBy,@LastModifiedOn,@DiscountPerc,"
                        qry = qry + "@DiscountAmount,@GSTAmount,@TotalWithGST,@NetAmount,@Status,@ContractGroup,@Posted,@Location,@Remarks, @Valuebase, @Appliedbase, @Creditbase, @Receiptbase, @Balancebase,@GstBase);"

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@ConsolidatedInvoiceNo", txtInvoiceNo.Text)
                        command.Parameters.AddWithValue("@CIDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        command.Parameters.AddWithValue("@AccountId", lblidAccountID.Text)
                        command.Parameters.AddWithValue("@CustName", lblidClientName.Text)

                        command.Parameters.AddWithValue("@InvoiceNo", lblidInvoiceNo.Text)

                        command.Parameters.AddWithValue("@SalesDate", Convert.ToDateTime(lblidSalesDate.Text).ToString("yyyy-MM-dd"))
                        command.Parameters.AddWithValue("@RcNoInvoice", Convert.ToInt32(TextBoxRcnoInvoice.Text))

                        command.Parameters.AddWithValue("@BillAddress1", "")
                        command.Parameters.AddWithValue("@BillBuilding", "")
                        command.Parameters.AddWithValue("@BillStreet", "")
                        command.Parameters.AddWithValue("@BillCountry", "")
                        command.Parameters.AddWithValue("@BillPostal", "")
                        command.Parameters.AddWithValue("@BillState", "")
                        command.Parameters.AddWithValue("@BillCity", "")

                        command.Parameters.AddWithValue("@ContactType", lblidContactType.Text)
                        command.Parameters.AddWithValue("@CompanyGroup", lblidCompanyGroup.Text)

                        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@DiscountPerc", 0.0)
                        command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                        'command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                        'command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        'command.Parameters.AddWithValue("@NetAmount", (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)))

                        command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblidGStAmt.Text))
                        command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(lblidBalanceBase.Text))

                        command.Parameters.AddWithValue("@Valuebase", (Convert.ToDecimal(lblidValueBase.Text)))
                        command.Parameters.AddWithValue("@Appliedbase", (Convert.ToDecimal(lblidAppliedBase.Text)))
                        command.Parameters.AddWithValue("@Receiptbase", (Convert.ToDecimal(lblidReceiptBase.Text)))
                        command.Parameters.AddWithValue("@Creditbase", (Convert.ToDecimal(lblidCreditBase.Text)))
                        command.Parameters.AddWithValue("@Balancebase", (Convert.ToDecimal(lblidBalanceBase.Text)))
                        command.Parameters.AddWithValue("@Gstbase", (Convert.ToDecimal(lblidGStBase.Text)))


                        command.Parameters.AddWithValue("@Status", "")
                        command.Parameters.AddWithValue("@ContractGroup", "")
                        command.Parameters.AddWithValue("@Posted", "")
                        command.Parameters.AddWithValue("@Location", txtLocation.Text.Trim)
                        command.Parameters.AddWithValue("@Remarks", "")

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

                        qry = "UPDATE tbleinvoiceconsolidateddetail tbleinvoiceconsolidateddetail SET ConsolidatedInvoiceNo = @ConsolidatedInvoiceNo,"
                        qry = qry + "CIDate = @CIDate,AccountId = @AccountId,CustName = @CustName,InvoiceNo = @InvoiceNo,SalesDate = @SalesDate,"
                        qry = qry + "BillingFrequency = @BillingFrequency,LocationId = @LocationId,Address1 = @Address1,BillAmount = @BillAmount,"
                        qry = qry + "RcnoInvoice = @RcnoInvoice,BillNo = @BillNo,ContractNo = @ContractNo,Name = @Name,OurRef = @OurRef,YourRef = @YourRef,"
                        qry = qry + "PoNo = @PoNo,CreditTerms = @CreditTerms,Salesman = @Salesman,BillAddress1 = @BillAddress1,BillBuilding = @BillBuilding,"
                        qry = qry + "BillStreet = @BillStreet,BillPostal = @BillPostal,BillState = @BillState,BillCity = @BillCity,BillCountry = @BillCountry,"
                        qry = qry + "ContactType = @ContactType,CompanyGroup = @CompanyGroup,CreatedBy = @CreatedBy,CreatedOn = @CreatedOn,"
                        qry = qry + "LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn,Rcno = @Rcno,DiscountPerc = @DiscountPerc,"
                        qry = qry + "DiscountAmount = @DiscountAmount,GSTAmount = @GSTAmount,TotalWithGST = @TotalWithGST,NetAmount = @NetAmount,"
                        qry = qry + "Status = @Status,ContractGroup = @ContractGroup,Posted = @Posted,  Valuebase=@Valuebase, Appliedbase=@Appliedbase, Creditbase=@Creditbase, Receiptbase=@Receiptbase, Balancebase=@Balancebase,GstBase=@GstBase"
                        qry = qry + " where InvoiceNo  = @InvoiceNo and ConsolidatedInvoiceNo = @ConsolidatedInvoiceNo  "

                        command.CommandText = qry
                        command.Parameters.Clear()

                        command.Parameters.AddWithValue("@ConsolidatedInvoiceNo", txtInvoiceNo.Text)
                        commandBillSchedule.Parameters.AddWithValue("@CIDate", Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd"))
                        command.Parameters.AddWithValue("@AccountId", lblidAccountID.Text)
                        command.Parameters.AddWithValue("@CustName", lblidClientName.Text)

                        command.Parameters.AddWithValue("@InvoiceNo", lblidInvoiceNo.Text)

                        command.Parameters.AddWithValue("@SalesDate", lblidSalesDate.Text)
                        command.Parameters.AddWithValue("@RcNoInvoice", TextBoxRcnoInvoice.Text)

                        command.Parameters.AddWithValue("@BillAddress1", "")
                        command.Parameters.AddWithValue("@BillBuilding", "")
                        command.Parameters.AddWithValue("@BillStreet", "")
                        command.Parameters.AddWithValue("@BillCountry", "")
                        command.Parameters.AddWithValue("@BillPostal", "")
                        command.Parameters.AddWithValue("@BillState", "")
                        command.Parameters.AddWithValue("@BillCity", "")

                        command.Parameters.AddWithValue("@ContactType", lblidContactType.Text)
                        command.Parameters.AddWithValue("@CompanyGroup", lblidCompanyGroup.Text)

                        command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
                        'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
                        command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

                        command.Parameters.AddWithValue("@DiscountPerc", 0.0)
                        command.Parameters.AddWithValue("@DiscountAmount", 0.0)
                        command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblidGStAmt.Text))
                        command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(lblidBalanceBase.Text))

                        'command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)
                        'command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01))
                        'command.Parameters.AddWithValue("@NetAmount", (Convert.ToDecimal(lblid23.Text) + (Convert.ToDecimal(lblid23.Text) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01)))
                        'command.Parameters.AddWithValue("@Status", "")
                        command.Parameters.AddWithValue("@ContractGroup", "")
                        command.Parameters.AddWithValue("@Posted", "")
                        command.Parameters.AddWithValue("@Location", txtLocation.Text.Trim)
                        command.Parameters.AddWithValue("@Remarks", "")

                        command.Parameters.AddWithValue("@Valuebase", (Convert.ToDecimal(lblidValueBase.Text)))
                        command.Parameters.AddWithValue("@Appliedbase", (Convert.ToDecimal(lblidAppliedBase.Text)))
                        command.Parameters.AddWithValue("@Receiptbase", (Convert.ToDecimal(lblidReceiptBase.Text)))
                        command.Parameters.AddWithValue("@Creditbase", (Convert.ToDecimal(lblidCreditBase.Text)))
                        command.Parameters.AddWithValue("@Balancebase", (Convert.ToDecimal(lblidBalanceBase.Text)))
                        command.Parameters.AddWithValue("@Gstbase", (Convert.ToDecimal(lblidGStBase.Text)))


                        command.Connection = conn
                        command.ExecuteNonQuery()

                    End If
                    ToBillAmtTot = Convert.ToDecimal(ToBillAmt)
                    DiscAmountTot = Convert.ToDecimal(DiscAmount)
                    GSTAmountTot = Convert.ToDecimal(GSTAmount)
                    NetAmountTot = Convert.ToDecimal(NetAmount)

                End If


                '03.03.17
            Next rowIndex


            txtInvoiceAmount.Text = Convert.ToDecimal(ToBillAmtTot).ToString("N2")
            txtDiscountAmount.Text = Convert.ToDecimal(DiscAmountTot).ToString("N2")
            txtAmountWithDiscount.Text = (Convert.ToDecimal(ToBillAmtTot) - Convert.ToDecimal(DiscAmountTot)).ToString("N2")

            txtGSTAmount.Text = Convert.ToDecimal(GSTAmountTot).ToString("N2") 'Convert.ToDecimal(ToBillAmtTot) * Convert.ToDecimal(txtTaxRatePct.Text) * 0.01
            txtNetAmount.Text = Convert.ToDecimal(NetAmountTot).ToString("N2")

            'DisplayGLGrid()

            '29.07.25

            Dim commandUpdateTblSales As MySqlCommand = New MySqlCommand
            commandUpdateTblSales.CommandType = CommandType.StoredProcedure
            commandUpdateTblSales.CommandText = "UpdateTblSalesFromCI"
            commandUpdateTblSales.Parameters.Clear()
            commandUpdateTblSales.Parameters.AddWithValue("@pr_InvoiceNumber", txtInvoiceNo.Text.Trim)
            commandUpdateTblSales.Parameters.AddWithValue("@pr_Action", "S")
            commandUpdateTblSales.Connection = conn
            commandUpdateTblSales.ExecuteScalar()

            '29.07.25


            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            'End If


            lblMessage.Text = "ADD: CONSOLIDATED INVOICE SUCCESSFULLY SAVED"
            lblAlert.Text = ""
            'CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "BATCHSCH", txtBatchNo.Text, "SAVE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtBatchNo.Text, "", txtRcno.Text)


            'GridView1.DataSourceID = "SQLDSInvoice"
            'GridView1.DataBind()

            If txtDisplayRecordsLocationwise.Text = "Y" Then

                SQLDSInvoice.SelectCommand = "SELECT * FROM tbleinvoiceconsolidated WHERE ConsolidatedInvoiceNo in (select ConsolidatedInvoiceNo from tbleinvoiceconsolidateddetail where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) and  (PostStatus = 'O' or PostStatus = 'P') ORDER BY rcno desc"
                GridView1.DataSourceID = "SQLDSInvoice"
                GridView1.DataBind()
            Else
                SQLDSInvoice.SelectCommand = "SELECT * FROM tbleinvoiceconsolidated ORDER BY rcno desc"
                GridView1.DataSourceID = "SQLDSInvoice"
                GridView1.DataBind()
            End If

            '     mdlPopupConfirmSavePost.Show()

            'If txtMode.Text = "NEW" Then
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONSOLIDATEDINV", txtInvoiceNo.Text, "ADD", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'Else
            '    CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONSOLIDATEDINV", txtInvoiceNo.Text, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

            'End If


            DisableControls()

            btnSaveInvoice.Enabled = False
            btnSaveInvoice.ForeColor = System.Drawing.Color.Gray

            btnGenerateInvoice.Enabled = True
            btnGenerateInvoice.ForeColor = System.Drawing.Color.Black

            '   ddlLocationSearch.ReadOnly = False
            txtLocation.Enabled = True
            txtInvoiceDate.Enabled = True
            txtGSt.Enabled = True

            txtMode.Text = ""
            ButtonPressed = "N"

            'updPnlMsg.Update()
            'updPnlSearch.Update()
            'updpnlServiceRecs.Update()
            'updPnlBillingRecs.Update()
            'UpdatePanel1.Update()

            Session("CheckRefresh") = Server.UrlDecode(Date.Now.ToString())

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> isSubmitted = False;</Script>", False)

        Catch ex As Exception

            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            InsertIntoTblWebEventLog("CONSOLIDATED INV - " + Session("UserID"), "btnSaveInvoice_Click", ex.Message.ToString, "")
            Exit Sub
        End Try
        'End If

    End Sub

    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
    
        Try
             If String.IsNullOrEmpty(txt.Text) = False Then
                '   InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "2", "")
                Dim dt As DataTable = GetDataSet()
                '  InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "3", "")
                WriteExcelWithNPOI(dt, "xlsx")
                '   InsertIntoTblWebEventLog("INVOICE", "btnExportToExcel_Click", "4", "")
                'End If

                'Return


                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INVOICE", "", "btnExportToExcel_Click", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountId.Text, "", txtRcno.Text)
                Response.End()
                dt.Clear()

            Else
                lblAlert.Text = "NO DATA TO EXPORT"
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("", "btnExportToExcel_Click", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
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
        cell1.SetCellValue("(" + ConfigurationManager.AppSettings("DomainName").ToString() + ")")
        '+ Session("Selection").ToString)

        ' cell1.SetCellValue(Session("Selection").ToString)
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


        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet1.CreateRow(i + 2)

            For j As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.CreateCell(j)

                'If j >= 13 And j <= 20 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Double = Convert.ToDouble("0.00")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _doubleCellStyle

                'ElseIf j = 26 Or j = 27 Or j = 28 Or j = 35 Or j = 39 Or j = 40 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Double = Convert.ToDouble(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Double = Convert.ToDouble("0.00")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _doubleCellStyle
                'ElseIf j = 48 Or j = 36 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As Int32 = Convert.ToInt32(dt.Rows(i)(j).ToString)
                '        cell.SetCellValue(d)
                '    Else
                '        Dim d As Int32 = Convert.ToInt32("0")
                '        cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = _intCellStyle

                'ElseIf j = 4 Then
                '    If String.IsNullOrEmpty(dt.Rows(i)(j).ToString) = False Then
                '        Dim d As DateTime = Convert.ToDateTime(dt.Rows(i)(j).ToString())
                '        cell.SetCellValue(d)
                '        'Else
                '        '    Dim d As Double = Convert.ToDouble("0.00")
                '        '    cell.SetCellValue(d)

                '    End If
                '    cell.CellStyle = dateCellStyle
                'Else
                cell.SetCellValue(dt.Rows(i)(j).ToString)
                cell.CellStyle = AllCellStyle

                '  End If
                If i = dt.Rows.Count - 1 Then
                    sheet1.AutoSizeColumn(j)
                End If
            Next
        Next



        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            '  Dim criteria As String = "_" + txtSvcDateFrom.Text + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss")
            Dim attachment As String = "attachment; filename=ConsolidatedInvoice"


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


    Private Function GetDataSet() As DataTable
        Try
            Dim qry As String = ""

            'qry = "SELECT A.Status, if(C.SvcLock is null, 'N', C.SvcLock) as LockSt, cast(A.Emailsent as unsigned) AS EmailSent, A.ServiceBy, A.ServiceDate, A.SchServiceTime, A.SchServiceTimeOut, A.ScheduleType, A.LocationId, A.ServiceName, A.Address1 AS ServiceAddress, A.AddPostal as Postal, B.BillingFrequency, A.BillAmount as ToBillAmount, A.BilledAmt, A.BillNo, A.ContactPersonID as ContactPerson, replace(replace(A.Notes, char(10), ' '), char(13), ' ') as ServiceDescription, replace(replace(A.Comments, char(10), ' '), char(13), ' ') as ServiceInstruction,"
            'qry = qry + " A.RecordNo, b.ContractNo,B.ContractGroup,A.CompanyGroup,A.ContactType, A.AccountId, A.CustName as AccountName, replace(replace(A.Email, char(10), ' '), char(13), ' ') as Email, replace(replace(A.Description, char(10), ' '), char(13), ' ') as Description, replace(replace(A.Remarks, char(10), ' '), char(13), ' ') as Remarks, A.TeamId, A.Scheduler, A.TimeIn, A.TimeOut, A.Duration, A.OurRef, A.yourRef, A.LocateGrp, A.TabletId as MobileDeviceID, A.TabletDownloadedDate AS MobileDownloadDate, A.UploadDate as MobileUploadDate,  A.EmailSentDate, A.CreatedBy, A.CreatedOn, A.LastModifiedBy as EditedBy, A.LastModifiedOn as EditedOn, A.Rcno"
            'qry = qry + " FROM tblservicerecord as A Left join tblcontract B on A.contractno = B.contractno Left OUTER join tblLockServiceRecord C on A.ServiceDate between c.SvcDateFrom and C.SvcDateTo "



            'qry = "SELECT A.Status, if(C.SvcLock is null, 'N', C.SvcLock) as LockSt, cast(A.Emailsent as unsigned) AS EmailSent, A.ServiceBy, A.ServiceDate, A.SchServiceTime, A.SchServiceTimeOut, A.ScheduleType, A.LocationId, A.ServiceName, A.Address1 AS ServiceAddress, A.AddPostal as Postal, B.BillingFrequency, A.BillAmount as ToBillAmount, A.BilledAmt, A.BillNo, A.ContactPersonID as ContactPerson, replace(replace(A.Notes, char(10), ' '), char(13), ' ') as ServiceDescription, replace(replace(A.Comments, char(10), ' '), char(13), ' ') as ServiceInstruction,"
            'qry = qry + " A.RecordNo, b.ContractNo, A.ContactType, A.AccountId, A.CustName as AccountName, replace(replace(A.Email, char(10), ' '), char(13), ' ') as Email, replace(replace(A.Description, char(10), ' '), char(13), ' ') as Description, replace(replace(A.Remarks, char(10), ' '), char(13), ' ') as Remarks, A.TeamId, A.Scheduler, A.TimeIn, A.TimeOut, A.Duration, A.OurRef, A.yourRef, A.LocateGrp, A.TabletId as MobileDeviceID, A.TabletDownloadedDate AS MobileDownloadDate, A.UploadDate as MobileUploadDate,  A.EmailSentDate, A.CreatedBy, A.CreatedOn, A.LastModifiedBy as EditedBy, A.LastModifiedOn as EditedOn, A.Rcno,  ifnull(left(D.JobStatus,1),'') as JobStatus"
            'qry = qry + " FROM tblservicerecord as A Left join tblcontract B on A.contractno = B.contractno Left OUTER join tblLockServiceRecord C on A.ServiceDate between c.SvcDateFrom and C.SvcDateTo Left OUTER join tblServiceRecord2 D on A.RecordNo =D.RecordNo  where  "

            ' qry = qry.Replace("A.Description", "replace(replace(Notes, char(10), ' '), char(13), ' ') as Notes")

            Dim query As String = txt.Text
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
            InsertIntoTblWebEventLog("", "GetDataSet", ex.Message.ToString, txtCreatedBy.Text)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
        End Try
    End Function

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub btnViewInvoice_Click(sender As Object, e As EventArgs)

        lblAlert.Text = ""

        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
        Dim btn1 As Button = DirectCast(sender, Button)
        'Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
        Dim lblid1 As TextBox = CType(xrow1.FindControl("txtInvoiceNoGV"), TextBox)

        If String.IsNullOrEmpty(lblid1.Text) = False Then
            Session("ConsolInvNo") = txtInvoiceNo.Text
            Session("invoiceno") = lblid1.Text
            Session("gridsqlschedule") = txt.Text
            Session("rcnoschedule") = txtRcno.Text
            'Session("AccountId") = txtAccountIdBilling.Text
            'Session("AccountName") = txtAccountName.Text
            'Session("ContactType") = ddlContactType.Text
            'Session("CompanyGroup") = txtCompanyGroup.Text
            'Session("Salesman") = ddlSalesmanBilling.Text
            Session("invoicefrom") = "consolidate"

            Response.Redirect("Invoice.aspx")
        End If

    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex

        SQLDSInvoice.SelectCommand = txt.Text
        GridView1.DataBind()
    End Sub


    'Protected Sub SaveServiceBillingDetail()
    '    '''''''''''''''''''''''''''''''''''''''

    '    Dim qry As String
    '    Dim command As MySqlCommand = New MySqlCommand
    '    command.CommandType = CommandType.Text
    '    Dim conn As MySqlConnection = New MySqlConnection()

    '    conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '    If conn.State = ConnectionState.Open Then
    '        conn.Close()
    '        conn.Dispose()
    '    End If
    '    conn.Open()

    '    Dim rowselected As Integer = Convert.ToInt32(txtRowSelected.Text)

    '    Dim lblid1 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtAccountIdGV"), TextBox)
    '    Dim lblid2 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtClientNameGV"), TextBox)
    '    Dim lblid3 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtLocationIdGV"), TextBox)
    '    Dim lblid4 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtServiceRecordNoGV"), TextBox)
    '    Dim lblid5 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
    '    Dim lblid6 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtBillingFrequencyGV"), TextBox)
    '    Dim lblid7 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtRcnoServiceRecordGV"), TextBox)
    '    Dim lblid8 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtDeptGV"), TextBox)
    '    Dim lblid9 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtStatusGV"), TextBox)
    '    Dim lblid20 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtContractNoGV"), TextBox)
    '    Dim lblid21 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtServiceAddressGV"), TextBox)
    '    'Dim lblid22 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtServiceDateGV"), TextBox)
    '    Dim lblid23 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)

    '    If txtMode.Text = "NEW" Then

    '        'Dim command As MySqlCommand = New MySqlCommand
    '        'command.CommandType = CommandType.Text

    '        If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
    '            txtRcnotblServiceBillingDetail.Text = 0
    '        End If

    '        'If Convert.ToInt64(txtBatchNo.Text) = 0 Then

    '        '''''''''''''''''''''
    '        Dim commandExistServiceBillingDetail As MySqlCommand = New MySqlCommand
    '        commandExistServiceBillingDetail.CommandType = CommandType.Text
    '        'command1.CommandText = Sql
    '        commandExistServiceBillingDetail.CommandText = "SELECT * FROM tblServiceBillingDetail where RcnoServiceRecord=" & Convert.ToInt64(lblid7.Text) & " and Batchno = '" & txtBatchNo.Text & "'"
    '        commandExistServiceBillingDetail.Connection = conn

    '        Dim drExistServiceBillingDetail As MySqlDataReader = commandExistServiceBillingDetail.ExecuteReader()
    '        Dim dtExistServiceBillingDetail As New DataTable
    '        dtExistServiceBillingDetail.Load(drExistServiceBillingDetail)

    '        If dtExistServiceBillingDetail.Rows.Count = 0 Then

    '            '''''''''''''''''''''
    '            qry = "INSERT INTO tblServiceBillingDetail( AccountId, CustName, LocationId, Name, RecordNo, BillAddress1, BillBuilding, BillStreet, BillCountry, BillPostal, "
    '            qry = qry + " ServiceDate, BillAmount, DiscountAmount,  GSTAmount, TotalWithGST, NetAmount, OurRef,YourRef,ContractNo, PoNo, RcnoServiceRecord, BillingFrequency, Salesman, ContactType, CompanyGroup,   "
    '            qry = qry + " ContractGroup, Status, Address1,   "
    '            qry = qry + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
    '            qry = qry + " (@AccountId, @ClientName, @LocationId, @AccountName, @ServiceRecordNo, @BillAddress1, @BillBuilding, @BillStreet, @BillCountry, @BillPostal, "
    '            qry = qry + " @ServiceDate, @BillAmount, @DiscountAmount,  @GSTAmount, @TotalWithGST, @NetAmount, @OurRef, @YourRef, @ContractNo, @PoNo, @RcnoServiceRecord, @BillingFrequency, @Salesman, @ContactType, @CompanyGroup,   "
    '            qry = qry + " @ContractGroup, @Status,  @Address1,  "
    '            qry = qry + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

    '            command.CommandText = qry
    '            command.Parameters.Clear()

    '            command.Parameters.AddWithValue("@AccountId", lblid1.Text)
    '            command.Parameters.AddWithValue("@ClientName", lblid2.Text)
    '            command.Parameters.AddWithValue("@LocationId", lblid3.Text)
    '            command.Parameters.AddWithValue("@AccountName", txtAccountName.Text)
    '            command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)
    '            command.Parameters.AddWithValue("@Address1", lblid21.Text)
    '            command.Parameters.AddWithValue("@BillAddress1", txtBillAddress.Text)
    '            command.Parameters.AddWithValue("@BillBuilding", txtBillBuilding.Text)
    '            command.Parameters.AddWithValue("@BillStreet", txtBillStreet.Text)
    '            command.Parameters.AddWithValue("@BillCountry", txtBillCountry.Text)
    '            command.Parameters.AddWithValue("@BillPostal", txtBillPostal.Text)
    '            command.Parameters.AddWithValue("@BillingFrequency", lblid6.Text)

    '            If lblid5.Text.Trim = "" Then
    '                command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
    '            Else
    '                command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
    '            End If

    '            'command.Parameters.AddWithValue("@ServiceDate", lblid5.Text)
    '            'Dim lblid23 As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtToBillAmtGV"), TextBox)
    '            Dim TextBoxGSTAmount As TextBox = CType(grvInvoiceRecDetails.Rows(rowselected).FindControl("txtGSTAmountGV"), TextBox)
    '            'Dim lbd30 As String = TextBoxGSTAmount.Text

    '            'If String.IsNullOrEmpty(TextBoxGSTAmount.Text) = True Then
    '            '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(lblid23.Text))
    '            '    command.Parameters.AddWithValue("@DiscountAmount", 0.0)
    '            '    command.Parameters.AddWithValue("@GSTAmount", 0.0)
    '            '    command.Parameters.AddWithValue("@TotalWithGST", 0.0)
    '            '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(lblid23.Text))
    '            'Else
    '            '    command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
    '            '    command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
    '            '    command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
    '            '    command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
    '            '    command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
    '            'End If


    '            command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
    '            command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
    '            command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
    '            command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
    '            command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))

    '            command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
    '            command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
    '            'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
    '            command.Parameters.AddWithValue("@ContractNo", lblid20.Text)
    '            'command.Parameters.AddWithValue("@RcnoServiceRecord", txtRcnoServiceRecord.Text)
    '            command.Parameters.AddWithValue("@RcnoServiceRecord", lblid7.Text)

    '            command.Parameters.AddWithValue("@PoNo", txtPONo.Text)
    '            command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)
    '            command.Parameters.AddWithValue("@Status", lblid9.Text)
    '            command.Parameters.AddWithValue("@ContactType", txtAccountType.Text)
    '            command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)

    '            If ddlSalesmanBilling.Text = "-1" Then
    '                command.Parameters.AddWithValue("@Salesman", "")
    '            Else
    '                command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
    '            End If

    '            command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
    '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '            command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
    '            'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

    '            command.Connection = conn
    '            command.ExecuteNonQuery()

    '            Dim sqlLastId As String
    '            sqlLastId = "SELECT last_insert_id() from tblServiceBillingDetail"

    '            Dim commandRcno As MySqlCommand = New MySqlCommand
    '            commandRcno.CommandType = CommandType.Text
    '            commandRcno.CommandText = sqlLastId
    '            commandRcno.Parameters.Clear()
    '            commandRcno.Connection = conn
    '            txtRcnotblServiceBillingDetail.Text = commandRcno.ExecuteScalar()

    '            If String.IsNullOrEmpty(txtBatchNo.Text) = True Or txtBatchNo.Text = "0" Then
    '                txtBatchNo.Text = txtRcnotblServiceBillingDetail.Text

    '                qry = "Update tblServiceBillingDetail set BatchNo = '" & txtBatchNo.Text & "' where rcno = " & txtBatchNo.Text

    '                command.CommandText = qry
    '                command.Parameters.Clear()
    '                command.Connection = conn
    '                command.ExecuteNonQuery()

    '            End If
    '        End If
    '    Else
    '        If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
    '            txtRcnotblServiceBillingDetail.Text = 0
    '        End If
    '        If Convert.ToInt64(txtRcnotblServiceBillingDetail.Text) > 0 Then
    '            qry = "Update tblServiceBillingDetail set BillAmount = @BillAmount, DiscountAmount= @DiscountAmount,  GSTAmount =@GSTAmount,  "
    '            qry = qry + "TotalWithGST = @TotalWithGST, NetAmount =@NetAmount, OurRef = @OurRef ,YourRef =@YourRef, PoNo =@PoNo, Salesman =@Salesman,    "
    '            qry = qry + " LastModifiedBy =@LastModifiedBy,LastModifiedOn = @LastModifiedOn; "

    '            command.CommandText = qry
    '            command.Parameters.Clear()

    '            command.Parameters.AddWithValue("@BillAmount", Convert.ToDecimal(txtTotal.Text))
    '            command.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtTotalDiscAmt.Text))
    '            command.Parameters.AddWithValue("@GSTAmount", Convert.ToDecimal(txtTotalGSTAmt.Text))
    '            command.Parameters.AddWithValue("@TotalWithGST", Convert.ToDecimal(txtTotalWithGST.Text))
    '            command.Parameters.AddWithValue("@NetAmount", Convert.ToDecimal(txtTotalWithGST.Text))
    '            command.Parameters.AddWithValue("@OurRef", txtOurReference.Text)
    '            command.Parameters.AddWithValue("@YourRef", txtYourReference.Text)
    '            command.Parameters.AddWithValue("@PoNo", txtPONo.Text)

    '            If ddlSalesmanBilling.Text = "-1" Then
    '                command.Parameters.AddWithValue("@Salesman", "")
    '            Else
    '                command.Parameters.AddWithValue("@Salesman", ddlSalesmanBilling.Text)
    '            End If

    '            command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
    '            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

    '            command.Connection = conn
    '            command.ExecuteNonQuery()
    '        End If
    '    End If


    '    '''' Detail

    '    'Dim rowselected As Integer
    '    'rowselected = 0

    '    'Dim conn As MySqlConnection = New MySqlConnection()


    '    'Start: Delete existing Records

    '    'If String.IsNullOrEmpty(txtRcnotblServiceBillingDetail.Text) = True Then
    '    '    txtRcnotblServiceBillingDetail.Text = "0"
    '    'End If

    '    'If txtRcnotblServiceBillingDetail.Text <> "0" Then '04.01.17

    '    Dim commandtblServiceBillingDetailItem As MySqlCommand = New MySqlCommand

    '    commandtblServiceBillingDetailItem.CommandType = CommandType.Text
    '    'Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where BatchNo = '" & txtBatchNo.Text & "'"
    '    Dim qrycommandtblServiceBillingDetailItem As String = "DELETE from tblServiceBillingDetailItem where RcnoServiceBillingDetail = '" & Convert.ToInt32(txtRcnotblServiceBillingDetail.Text) & "'"

    '    commandtblServiceBillingDetailItem.CommandText = qrycommandtblServiceBillingDetailItem
    '    commandtblServiceBillingDetailItem.Parameters.Clear()
    '    commandtblServiceBillingDetailItem.Connection = conn
    '    commandtblServiceBillingDetailItem.ExecuteNonQuery()

    '    'End: Delete Existing Records


    '    SetRowDataServiceRecs()
    '    Dim tableAdd As DataTable = TryCast(ViewState("CurrentTableBillingDetailsRec"), DataTable)

    '    If tableAdd IsNot Nothing Then

    '        For rowIndex As Integer = 0 To tableAdd.Rows.Count - 1
    '            'string txSpareId = row.ItemArray[0] as string;
    '            Dim TextBoxQty As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(0).FindControl("txtQtyGV"), TextBox)
    '            Dim lbd10 As String = TextBoxQty.Text



    '            Dim TextBoxItemTypeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemTypeGV"), DropDownList)
    '            Dim TextBoxItemCodeGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemCodeGV"), DropDownList)
    '            Dim TextBoxItemDescriptionGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtItemDescriptionGV"), TextBox)
    '            Dim TextBoxUOMGV As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtUOMGV"), DropDownList)
    '            Dim TextBoxPricePerUOMGV As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPricePerUOMGV"), TextBox)
    '            Dim TextBoxTotalPrice As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceGV"), TextBox)
    '            Dim TextBoxDiscPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscPercGV"), TextBox)
    '            Dim TextBoxDiscAmount As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtDiscAmountGV"), TextBox)
    '            Dim TextBoxTotalPriceWithDisc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtPriceWithDiscGV"), TextBox)
    '            Dim TextBoxGSTPerc As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTPercGV"), TextBox)
    '            Dim TextBoxGSTAmt As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGSTAmtGV"), TextBox)
    '            Dim TextBoxTotalPriceWithGST As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTotalPriceWithGSTGV"), TextBox)
    '            Dim TextBoxTaxType As DropDownList = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtTaxTypeGV"), DropDownList)
    '            Dim TextBoxOtherCode As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtOtherCodeGV"), TextBox)
    '            Dim TextBoxGLDescription As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtGLDescriptionGV"), TextBox)
    '            Dim TextBoxContractNo As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtContractNoGV"), TextBox)
    '            Dim TextBoxServiceStatus As TextBox = CType(grvBillingDetails.Rows(rowIndex).FindControl("txtServiceStatusGV"), TextBox)

    '            If String.IsNullOrEmpty(lbd10) = False Then
    '                If (Convert.ToInt64(lbd10) > 0) Then

    '                    ''Start:Detail
    '                    Dim commandSalesDetail As MySqlCommand = New MySqlCommand

    '                    commandSalesDetail.CommandType = CommandType.Text
    '                    Dim qryDetail As String = "INSERT INTO tblServiceBillingDetailItem(RcnoServiceBillingDetail,Itemtype, ItemCode, Itemdescription, UOM, Qty,  "
    '                    qryDetail = qryDetail + " PricePerUOM, TotalPrice,DiscPerc, DiscAmount, PriceWithDisc, GSTPerc, GSTAmt, TotalPriceWithGST, TaxType, ARCode, GSTCode, OtherCode, GLDescription,  RcnoServiceRecord, BatchNo,  CompanyGroup, ContractNo, ServiceStatus, ContractGroup, ServiceRecordNo, ServiceDate,"
    '                    qryDetail = qryDetail + " CreatedOn,CreatedBy,LastModifiedBy,LastModifiedOn) VALUES "
    '                    qryDetail = qryDetail + "(@RcnoServiceBillingDetail, @Itemtype, @ItemCode, @Itemdescription, @UOM, @Qty,"
    '                    qryDetail = qryDetail + " @PricePerUOM, @TotalPrice, @DiscPerc, @DiscAmount, @PriceWithDisc, @GSTPerc, @GSTAmt, @TotalPriceWithGST, @TaxType, @ARCode, @GSTCode,  @OtherCode,@GLDescription, @RcnoServiceRecord, @BatchNo, @CompanyGroup, @ContractNo,  @ServiceStatus, @ContractGroup, @ServiceRecordNo, @ServiceDate,"
    '                    qryDetail = qryDetail + " @CreatedOn, @CreatedBy, @LastModifiedBy, @LastModifiedOn);"

    '                    command.CommandText = qryDetail
    '                    command.Parameters.Clear()

    '                    command.Parameters.AddWithValue("@RcnoServiceBillingDetail", Convert.ToInt64(txtRcnotblServiceBillingDetail.Text))
    '                    command.Parameters.AddWithValue("@Itemtype", TextBoxItemTypeGV.Text)
    '                    command.Parameters.AddWithValue("@ItemCode", TextBoxItemCodeGV.Text)
    '                    command.Parameters.AddWithValue("@Itemdescription", TextBoxItemDescriptionGV.Text)

    '                    If TextBoxUOMGV.Text <> "-1" Then
    '                        command.Parameters.AddWithValue("@UOM", TextBoxUOMGV.Text)

    '                    Else
    '                        command.Parameters.AddWithValue("@UOM", "")
    '                    End If

    '                    command.Parameters.AddWithValue("@Qty", TextBoxQty.Text)
    '                    command.Parameters.AddWithValue("@PricePerUOM", TextBoxPricePerUOMGV.Text)
    '                    'command.Parameters.AddWithValue("@BillAmount", 0.0)
    '                    command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(TextBoxTotalPrice.Text))
    '                    command.Parameters.AddWithValue("@DiscPerc", Convert.ToDecimal(TextBoxDiscPerc.Text))
    '                    command.Parameters.AddWithValue("@DiscAmount", Convert.ToDecimal(TextBoxDiscAmount.Text))
    '                    command.Parameters.AddWithValue("@PriceWithDisc", Convert.ToDecimal(TextBoxTotalPriceWithDisc.Text))
    '                    command.Parameters.AddWithValue("@GSTPerc", Convert.ToDecimal(TextBoxGSTPerc.Text))
    '                    command.Parameters.AddWithValue("@GSTAmt", Convert.ToDecimal(TextBoxGSTAmt.Text))
    '                    command.Parameters.AddWithValue("@TotalPriceWithGST", Convert.ToDecimal(TextBoxTotalPriceWithGST.Text))
    '                    command.Parameters.AddWithValue("@TaxType", TextBoxTaxType.Text)
    '                    command.Parameters.AddWithValue("@RcnoServiceRecord", Convert.ToInt64(lblid7.Text))
    '                    command.Parameters.AddWithValue("@ARCode", "")
    '                    command.Parameters.AddWithValue("@GSTCode", "")
    '                    command.Parameters.AddWithValue("@OtherCode", TextBoxOtherCode.Text)
    '                    command.Parameters.AddWithValue("@GLDescription", TextBoxGLDescription.Text)
    '                    command.Parameters.AddWithValue("@BatchNo", txtBatchNo.Text)

    '                    'command.Parameters.AddWithValue("@ContractNo", txtContractNo.Text)
    '                    command.Parameters.AddWithValue("@ContractNo", TextBoxContractNo.Text)
    '                    command.Parameters.AddWithValue("@ServiceStatus", TextBoxServiceStatus.Text)
    '                    command.Parameters.AddWithValue("@CompanyGroup", txtCompanyGroup.Text)
    '                    command.Parameters.AddWithValue("@ContractGroup", lblid8.Text)

    '                    command.Parameters.AddWithValue("@ServiceRecordNo", lblid4.Text)

    '                    If lblid5.Text.Trim = "" Then
    '                        command.Parameters.AddWithValue("@ServiceDate", DBNull.Value)
    '                    Else
    '                        command.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(lblid5.Text).ToString("yyyy-MM-dd"))
    '                    End If
    '                    command.Parameters.AddWithValue("@CreatedBy", Session("UserID"))
    '                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@LastModifiedBy", Session("UserID"))
    '                    'command.Parameters.AddWithValue("@CreatedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))
    '                    command.Connection = conn
    '                    command.ExecuteNonQuery()
    '                    'conn.Close()
    '                End If

    '            End If
    '        Next rowIndex
    '    End If
    'End Sub

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
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub


    'Protected Sub txtItemTypeGV_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

    '    Dim xrow1 As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)
    '    Dim lblid1 As DropDownList = CType(xrow1.FindControl("txtItemTypeGV"), DropDownList)
    '    'Dim lblid2 As TextBox = CType(xrow1.FindControl("txtTargtIdGV"), TextBox)


    '    'lTargetDesciption = lblid1.Text

    '    Dim rowindex1 As Integer = xrow1.RowIndex
    '    If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
    '        btnAddDetail_Click(sender, e)
    '        'txtRecordAdded.Text = "Y"
    '    End If
    'End Sub

    'Protected Sub txtItemCodeGV_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        'Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)

    '        'Dim xgrvBillingDetails As GridViewRow = CType(ddl1.NamingContainer, GridViewRow)

    '        Dim ddl1 As DropDownList = DirectCast(sender, DropDownList)
    '        xgrvBillingDetails = CType(ddl1.NamingContainer, GridViewRow)

    '        Dim lblid1 As DropDownList = CType(xgrvBillingDetails.FindControl("txtItemCodeGV"), DropDownList)
    '        Dim lblid2 As TextBox = CType(xgrvBillingDetails.FindControl("txtItemDescriptionGV"), TextBox)
    '        Dim lblid3 As TextBox = CType(xgrvBillingDetails.FindControl("txtPricePerUOMGV"), TextBox)
    '        Dim lblid4 As TextBox = CType(xgrvBillingDetails.FindControl("txtQtyGV"), TextBox)
    '        Dim lblid5 As TextBox = CType(xgrvBillingDetails.FindControl("txtOtherCodeGV"), TextBox)
    '        Dim lblid6 As DropDownList = CType(xgrvBillingDetails.FindControl("txtTaxTypeGV"), DropDownList)
    '        Dim lblid7 As TextBox = CType(xgrvBillingDetails.FindControl("txtGSTPercGV"), TextBox)
    '        Dim lblid8 As DropDownList = CType(xgrvBillingDetails.FindControl("txtItemTypeGV"), DropDownList)
    '        Dim lblid9 As TextBox = CType(xgrvBillingDetails.FindControl("txtGLDescriptionGV"), TextBox)

    '        'lTargetDesciption = lblid1.Text

    '        Dim rowindex1 As Integer = xgrvBillingDetails.RowIndex

    '        'Get Item desc, price Id

    '        Dim conn As MySqlConnection = New MySqlConnection()

    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '            conn.Dispose()
    '        End If
    '        conn.Open()
    '        Dim command1 As MySqlCommand = New MySqlCommand

    '        command1.CommandType = CommandType.Text

    '        'command1.CommandText = "SELECT * FROM tblbillingproducts where CompanyGroup = '" & txtCompanyGroup.Text & "' and  ProductCode = '" & lblid1.Text & "'"
    '        command1.CommandText = "SELECT * FROM tblbillingproducts where   ProductCode = '" & lblid1.Text & "'"
    '        command1.Connection = conn

    '        Dim dr As MySqlDataReader = command1.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            lblid2.Text = dt.Rows(0)("Description").ToString

    '            If lblid8.Text = "PRODUCT" Then
    '                lblid3.Text = dt.Rows(0)("Price").ToString
    '            End If
    '            lblid4.Text = 1
    '            lblid5.Text = dt.Rows(0)("COACode").ToString
    '            lblid6.Text = dt.Rows(0)("TaxType").ToString
    '            lblid7.Text = dt.Rows(0)("TaxRate").ToString
    '            lblid9.Text = dt.Rows(0)("COADescription").ToString

    '            CalculatePrice()
    '        End If


    '        'If rowindex1 = grvBillingDetails.Rows.Count - 1 Then
    '        '    btnAddDetail_Click(sender, e)
    '        '    'txtRecordAdded.Text = "Y"
    '        'End If
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub

    'Protected Sub grvBillingDetails_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grvBillingDetails.RowDeleting
    '    Try

    '        'If txtRecordDeleted.Text = "Y" Then
    '        '    txtRecordDeleted.Text = "N"
    '        '    Exit Sub
    '        'End If

    '        lblAlert.Text = ""
    '        Dim confirmValue As String
    '        confirmValue = ""

    '        confirmValue = Request.Form("confirm_value")
    '        If Right(confirmValue, 3) = "Yes" Then

    '            SetRowDataBillingDetailsRecs()
    '            Dim dt As DataTable = CType(ViewState("CurrentTableBillingDetailsRec"), DataTable)
    '            Dim drCurrentRow As DataRow = Nothing
    '            Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)

    '            'Dim TextBoxRcno As TextBox = CType(grvBillingDetails.Rows(rowIndex).Cells(19).FindControl("txtRcnoServiceBillingDetailItemGV"), TextBox)

    '            'If (String.IsNullOrEmpty(TextBoxRcno.Text) = False) Then
    '            '    If (Convert.ToInt32(TextBoxRcno.Text) > 0) Then

    '            '        Dim conn As MySqlConnection = New MySqlConnection(constr)
    '            '        conn.Open()

    '            '        Dim commandIsCountRecords As MySqlCommand = New MySqlCommand
    '            '        commandIsCountRecords.CommandType = CommandType.Text
    '            '        commandIsCountRecords.CommandText = "Select count(*) as totalrecords from tblservicebillingdetailitem where rcnoServiceBillingDetail = " & Convert.ToInt64(txtRcnotblServiceBillingDetail.Text)
    '            '        commandIsCountRecords.Connection = conn

    '            '        Dim drIsCountRecords As MySqlDataReader = commandIsCountRecords.ExecuteReader()

    '            '        Dim dtIsCountRecords As New DataTable
    '            '        dtIsCountRecords.Load(drIsCountRecords)

    '            '        If dtIsCountRecords.Rows(0)("totalrecords").ToString = 1 Then
    '            '            Dim commandDeleteServicebillingdetail As MySqlCommand = New MySqlCommand
    '            '            commandDeleteServicebillingdetail.CommandType = CommandType.Text
    '            '            commandDeleteServicebillingdetail.CommandText = "Delete from tblservicebillingdetail where rcno = " & txtRcnotblServiceBillingDetail.Text
    '            '            commandDeleteServicebillingdetail.Connection = conn
    '            '            commandDeleteServicebillingdetail.ExecuteNonQuery()


    '            '            PopulateServiceGrid()
    '            '            'GridView1_SelectedIndexChanged(New Object(), New EventArgs)

    '            '            '''''''''''''''''''''''

    '            '            'Dim dtScdrLoc As DataTable = CType(ViewState("CurrentTableInvoiceRec"), DataTable)
    '            '            'Dim drCurrentRowLoc As DataRow = Nothing

    '            '            'For i As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 1
    '            '            '    dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
    '            '            '    drCurrentRowLoc = dtScdrLoc.NewRow()
    '            '            '    ViewState("CurrentTableInvoiceRec") = dtScdrLoc
    '            '            '    grvInvoiceRecDetails.DataSource = dtScdrLoc
    '            '            '    grvInvoiceRecDetails.DataBind()

    '            '            '    SetPreviousDataServiceRecs()
    '            '            'Next i

    '            '            '''''''''''''''''''''''
    '            '            'Dim dt1 As DataTable = CType(ViewState("CurrentTableInvoiceRec"), DataTable)
    '            '            'Dim drCurrentRow1 As DataRow = Nothing
    '            '            'If dt1.Rows.Count > 0 Then
    '            '            '    dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))
    '            '            '    drCurrentRow1 = dt1.NewRow()
    '            '            '    ViewState("CurrentTableInvoiceRec") = dt1
    '            '            '    grvInvoiceRecDetails.DataSource = dt1
    '            '            '    grvInvoiceRecDetails.DataBind()

    '            '            '    'SetPreviousData()
    '            '            '    'SetPreviousDataBillingDetailsRecs()
    '            '            '    SetPreviousDataServiceRecs()
    '            '            '    If dt1.Rows.Count = 0 Then
    '            '            '        FirstGridViewRowServiceRecs()
    '            '            '    End If
    '            '            '    'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
    '            '            'End If

    '            '            'Dim dt1 As DataTable = CType(ViewState("CurrentTableInvoiceRec"), DataTable)
    '            '            'Dim drCurrentRow1 As DataRow = Nothing
    '            '            'dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))


    '            '            'drCurrentRow1 = dt1.NewRow()
    '            '            'ViewState("CurrentTableInvoiceRec") = dt1
    '            '            'grvInvoiceRecDetails.DataSource = dt1
    '            '            'grvInvoiceRecDetails.DataBind()

    '            '            ' ''SetPreviousData()
    '            '            ' ''SetPreviousDataBillingDetailsRecs()
    '            '            'SetPreviousDataServiceRecs()


    '            '            'For i As Integer = 0 To grvInvoiceRecDetails.Rows.Count - 2

    '            '            '    If i = grcnoservicebillingdetail Then
    '            '            '        dt1.Rows.Remove(dt1.Rows(grcnoservicebillingdetail))
    '            '            '    End If

    '            '            '    'dtScdrLoc.Rows.Remove(dtScdrLoc.Rows(0))
    '            '            '    'drCurrentRowLoc = dtScdrLoc.NewRow()
    '            '            '    'ViewState("CurrentTableInvoiceRec") = dt1
    '            '            '    'grvInvoiceRecDetails.DataSource = dt1
    '            '            '    'grvInvoiceRecDetails.DataBind()

    '            '            '    SetPreviousDataServiceRecs()
    '            '            'Next i

    '            '            'ViewState("CurrentTableInvoiceRec") = dt1
    '            '            'grvInvoiceRecDetails.DataSource = dt1
    '            '            'grvInvoiceRecDetails.DataBind()


    '            '            'If dt1.Rows.Count = 0 Then
    '            '            '    FirstGridViewRowServiceRecs()
    '            '            '    'FirstGridViewRowBillingDetailsRecs()
    '            '            'End If

    '            '            updPanelInvoice.Update()
    '            '        End If


    '            '        Dim commandUpdGS As MySqlCommand = New MySqlCommand
    '            '        commandUpdGS.CommandType = CommandType.Text
    '            '        commandUpdGS.CommandText = "Delete from tblservicebillingdetailitem where rcno = " & TextBoxRcno.Text
    '            '        commandUpdGS.Connection = conn
    '            '        commandUpdGS.ExecuteNonQuery()

    '            '        conn.Close()
    '            '    End If
    '            'End If

    '            If dt.Rows.Count > 0 Then
    '                dt.Rows.Remove(dt.Rows(rowIndex))
    '                drCurrentRow = dt.NewRow()
    '                ViewState("CurrentTableBillingDetailsRec") = dt
    '                grvBillingDetails.DataSource = dt
    '                grvBillingDetails.DataBind()

    '                'SetPreviousData()
    '                SetPreviousDataBillingDetailsRecs()

    '                If dt.Rows.Count = 0 Then
    '                    FirstGridViewRowBillingDetailsRecs()
    '                End If
    '                'Dim i6 As Integer = objCommon.PopulateDropDown(CType(grvTargetDetails.Rows(dt.Rows.Count - 1).Cells(1).FindControl("ddlSpareIdGV"), DropDownList), "Select * from spare where  VATRate > 0.00 and  CompId=" & Convert.ToString(HttpContext.Current.Session("CompId")) & "  and BranchId=" & Convert.ToString(HttpContext.Current.Session("BranchID")), "SpareDesc", "SpareId")
    '            End If

    '            CalculateTotalPrice()

    '        End If
    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub

    'Protected Sub grvBillingDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grvBillingDetails.RowDataBound
    '    Try

    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            ' Delete

    '            For Each cell As DataControlFieldCell In e.Row.Cells
    '                ' check all cells in one row
    '                For Each control As Control In cell.Controls

    '                    Dim button As ImageButton = TryCast(control, ImageButton)
    '                    If button IsNot Nothing AndAlso button.CommandName = "Delete" Then
    '                        'button.OnClientClick = "if (!confirm('Are you sure to DELETE this record?')) return;"
    '                        button.OnClientClick = "Confirm()"
    '                    End If
    '                Next control
    '            Next cell

    '        End If



    '    Catch ex As Exception
    '        Dim exstr As String
    '        exstr = ex.ToString
    '        lblAlert.Text = exstr
    '        'MessageBox.Message.Alert(Page, ex.ToString, "str")
    '    End Try
    'End Sub

    'Protected Sub gvLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLocation.SelectedIndexChanged
    '    'Dim lblid1 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("ddlLocationIdGV"), TextBox)
    '    'Dim lblid2 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtServiceNameGV"), TextBox)
    '    'Dim lblid3 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtServiceAddressGV"), TextBox)
    '    'Dim lblid4 As TextBox = CType(grvServiceLocation.Rows(0).FindControl("txtZoneGV"), TextBox)

    '    If gvLocation.SelectedRow.Cells(1).Text = "&nbsp;" Then
    '        txtLocationId.Text = " "
    '    Else
    '        txtLocationId.Text = gvLocation.SelectedRow.Cells(1).Text
    '        'lblid1.Text = txtLocationId.Text
    '    End If

    '    If gvLocation.SelectedRow.Cells(2).Text = "&nbsp;" Then
    '        txtClientName.Text = " "
    '    Else
    '        txtClientName.Text = Server.HtmlDecode(gvLocation.SelectedRow.Cells(2).Text)
    '        'lblid2.Text = txtServiceName.Text
    '    End If


    'End Sub

    'Protected Sub btnSearch1Status_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch1Status.Click
    '    'mdlPopupStatusSearch.Show()
    'End Sub

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

    'Protected Sub txtAccountId_TextChanged(sender As Object, e As EventArgs) Handles txtAccountId.TextChanged
    '    'Dim query As String
    '    'query = "Select ContractNo from tblContract where AccountId = '" & txtAccountId.Text & "'"
    '    'PopulateDropDownList(query, "ContractNo", "ContractNo", ddlContractNo)
    'End Sub

    Protected Sub btnQuickReset_Click(sender As Object, e As EventArgs) Handles btnQuickReset.Click
        Try
            txtInvoicenoSearch.Text = ""
            txtUUIDSearch.Text = ""
            txtFromInvoiceDate.Text = ""
            txtToInvoiceDate.Text = ""

            btnQuickSearch_Click(sender, e)

        Catch ex As Exception
            Dim exstr As String
            exstr = ex.ToString
            lblAlert.Text = exstr
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
        End Try
        'btnSearch1Status_Click(sender, e)
    End Sub

    Protected Sub btnQuickSearch_Click(sender As Object, e As EventArgs) Handles btnQuickSearch.Click
        Try
            Dim strsql As String


            strsql = "Select * from tbleinvoiceconsolidated  where 1=1   "



            If txtDisplayRecordsLocationwise.Text = "Y" Then
                strsql = strsql + " and ConsolidatedInvoiceNo in (select ConsolidatedInvoiceNo from tbleinvoiceconsolidated where location in (select locationID from tblgroupaccesslocation where GroupAccess = '" & txtGroupAuthority.Text & "')) "

            End If

            If String.IsNullOrEmpty(txtInvoicenoSearch.Text) = False Then
                strsql = strsql & " and ConsolidatedInvoiceNo like '%" & txtInvoicenoSearch.Text.Trim + "%'"
            End If

            If String.IsNullOrEmpty(txtUUIDSearch.Text) = False Then
                strsql = strsql & " and UUID like '" & txtUUIDSearch.Text.Trim + "'"
            End If


            If String.IsNullOrEmpty(txtFromInvoiceDate.Text) = False And txtFromInvoiceDate.Text <> "__/__/____" Then
                strsql = strsql + " and CIDate >= '" + Convert.ToDateTime(txtFromInvoiceDate.Text).ToString("yyyy-MM-dd") + "'"
            End If
            If String.IsNullOrEmpty(txtToInvoiceDate.Text) = False And txtToInvoiceDate.Text <> "__/__/____" Then
                strsql = strsql + " and CIDate <= '" + Convert.ToDateTime(txtToInvoiceDate.Text).ToString("yyyy-MM-dd") + "'"
            End If

            If String.IsNullOrEmpty(txtSearch1Status.Text) = False Then
                Dim stringList As List(Of String) = txtSearch1Status.Text.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList
                    str = "'" + str + "'"
                    YrStrList.Add(str.ToUpper)
                Next

                Dim YrStr As [String] = [String].Join(",", YrStrList.ToArray())
                'txtCondition.Text = txtCondition.Text & " and a.PostStatus in (" + YrStr + ")"
                strsql = strsql + " and PostStatus in (" + YrStr + ")"
                'Else
                '    strsql = strsql + " and a.PostStatus in ('O','P')"
            End If

            strsql = strsql + " order by CIDate desc, ConsolidatedInvoiceNo desc"
            txt.Text = strsql

            InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE", "btnQuickSearch_Click", strsql, txtCreatedBy.Text)

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


    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';")
            'e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E4E4E4';")
            'e.Row.Attributes.Add("onmousedown", "this.style.backgroundColor='#738A9C';")
            'e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#738A9C';")

            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."

            '    InsertIntoTblWebEventLog("INVOICE", "ROWDATABOUND", e.Row.Cells(24).Text.Trim, txtCreatedBy.Text)

            If e.Row.Cells(24).Text.Trim = "VALID" Then
                e.Row.Cells(3).BackColor = Color.YellowGreen
                e.Row.Cells(3).ForeColor = Color.YellowGreen
                'ElseIf e.Row.Cells(29).Text.Trim = "CONSOLIDATED" Then
                '    e.Row.Cells(3).BackColor = Color.BLUE
                '    e.Row.Cells(3).ForeColor = Color.BLUE
            ElseIf e.Row.Cells(24).Text.Trim = "CANCELLED" Then
                e.Row.Cells(3).BackColor = Color.Purple
                e.Row.Cells(3).ForeColor = Color.Purple
            ElseIf e.Row.Cells(24).Text.Trim = "INVALID" Then
                e.Row.Cells(3).BackColor = Color.RED
                e.Row.Cells(3).ForeColor = Color.RED
                e.Row.ForeColor = Color.Red
            ElseIf e.Row.Cells(24).Text.Trim = "SUBMITTED" Or e.Row.Cells(24).Text.Trim = "APPROVED" Or e.Row.Cells(24).Text.Trim = "FAILURE" Then
                e.Row.Cells(3).BackColor = Color.PINK
                e.Row.Cells(3).ForeColor = Color.PINK
                'Else
                '    e.Row.Cells(3).BackColor = Color.white
                '    e.Row.Cells(3).ForeColor = Color.white
            End If

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

    'Protected Sub btnEditHistory_Click(sender As Object, e As EventArgs)
    '    Try

    '        If txtMode.Text = "Add" Or txtMode.Text = "Edit" Or txtMode.Text = "Copy" Then
    '            lblAlert.Text = "RECORD IS IN ADD/EDIT MODE, CLICK SAVE OR CANCEL TO VIEW HISTORY"
    '            Return
    '        End If

    '        lblMessage.Text = ""
    '        lblAlert.Text = ""

    '        Dim btn1 As Button = DirectCast(sender, Button)

    '        Dim xrow1 As GridViewRow = CType(btn1.NamingContainer, GridViewRow)
    '        Dim rowindex1 As Integer = xrow1.RowIndex


    '        Dim lblidRcno As String = TryCast(GridView1.Rows(rowindex1).FindControl("Label1"), Label).Text

    '        txtRcno.Text = lblidRcno

    '        GridView1.SelectedIndex = rowindex1

    '        Dim strRecordNo As String = GridView1.Rows(rowindex1).Cells(4).Text
    '        'txtLogDocNo.Text = strRecordNo
    '        sqlDSViewEditHistory.SelectCommand = "Select * from tblEventlog where  DocRef = '" & strRecordNo & "' order by logdate desc"
    '        sqlDSViewEditHistory.DataBind()

    '        grdViewEditHistory.DataSourceID = "sqlDSViewEditHistory"
    '        grdViewEditHistory.DataBind()

    '        mdlViewEditHistory.Show()


    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("INVOICE - " + Session("UserID"), "btnEditHistory_Click", ex.Message.ToString, "")
    '        lblAlert.Text = ex.Message.ToString
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
    '    End Try

    'End Sub

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
                '  UpdatePanel1.Update()
            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("CNDN - " + Session("UserID"), "FindGSTPct", ex.Message.ToString, "")
            lblAlert.Text = ex.Message.ToString
            Exit Sub
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtMode.Text = ""

        MakeMeNull()
        DisableControls()

        ' txtLocation.ReadOnly = False
        'txtLocation.Enabled = True
        'txtInvoiceDate.Enabled = True
        'txtGst.Enabled = True

        btnCancel.Enabled = False
        btnCancel.ForeColor = Color.Gray
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
     
        txtLocation.Enabled = True
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        lblAlert.Text = ""
        Try
            If txtPostStatus.Text = "P" Then
                lblAlert.Text = "Consolidated Invoice has already been POSTED.. Cannot be VOIDED"

                Exit Sub
            End If

            If txtPostStatus.Text = "V" Then
                lblAlert.Text = "Consolidated Invoice is VOID.. Cannot be VOIDED"
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



                Dim command6 As MySqlCommand = New MySqlCommand
                command6.CommandType = CommandType.Text

                'Dim qry4 As String = "Update tblservicerecord Set BillYN = 'Y', BilledAmt = " & Convert.ToDecimal(txtAmountWithDiscount.Text) & ", BillNo = '" & txtInvoiceNo.Text & "' where Rcno= @Rcno "
                'Dim qry6 As String = "DELETE from tblservicebillschedule where BillSchedule= @BillSchedule"
                Dim qry6 As String = "UPDATE tbleinvoiceconsolidated set PostStatus = 'V' where ConsolidatedInvoiceNo= @ConsolidatedInvoiceNo"
                command6.CommandText = qry6
                command6.Parameters.Clear()

                command6.Parameters.AddWithValue("@ConsolidatedInvoiceNo", txtInvoiceNo.Text)
                command6.Connection = conn
                command6.ExecuteNonQuery()

                ''End: Update tblServiceRecord

                '29.07.25

                Dim commandUpdateTblSales As MySqlCommand = New MySqlCommand
                commandUpdateTblSales.CommandType = CommandType.StoredProcedure
                commandUpdateTblSales.CommandText = "UpdateTblSalesFromCI"
                commandUpdateTblSales.Parameters.Clear()
                commandUpdateTblSales.Parameters.AddWithValue("@pr_InvoiceNumber", txtInvoiceNo.Text.Trim)
                commandUpdateTblSales.Parameters.AddWithValue("@pr_Action", "V")
                commandUpdateTblSales.Connection = conn
                commandUpdateTblSales.ExecuteScalar()

                '29.07.25

                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Dispose()
                End If
                'conn.Close()

                lblMessage.Text = "VOID: CONSOLIDATED INVOICE SUCCESSFULLY VOIDED"
                CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONSINV", txtInvoiceNo.Text, "DELETE", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtLocation.Text, "", txtRcno.Text)


                MakeMeNull()

                SQLDSInvoice.SelectCommand = txt.Text
                SQLDSInvoice.DataBind()
                'GridView1.DataSourceID = "SqlDSContract"


                GridView1.DataBind()
                'updPnlMsg.Update()
                'updPnlSearch.Update()
                'updPanelInvoice.Update()
                'updpnlServiceRecs.Update()
                'GridView1.DataBind()
            End If
        Catch ex As Exception

            lblAlert.Text = ex.Message.ToString

        End Try
    End Sub



    Public Function AssignJsonvalue_Without_certificate(invoiceNumber As String)
        InsertIntoTblWebEventLog("JsonValue0", invoiceNumber, "", Session("UserID").ToString)

        Dim invoiceWrapper As New InvoiceWrapper_Without_certificate()

        Dim invoiceDetails_D = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
        Dim invoiceDetails_A = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
        Dim invoiceDetails_B = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"

        Dim test1 = DateTime.UtcNow.Date
        Dim test = DateTime.UtcNow.TimeOfDay

        Dim invoiceID = invoiceNumber
     
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim command As MySqlCommand = New MySqlCommand

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

        If conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
        conn.Open()

        Dim dtCompanyInfo As DataTable = RetrieveCompanyInfoTable(conn)
        Dim dtEInvoiceMalaysiaCode As DataTable = RetrieveEInvoiceMalaysiaCodeTable(conn)
        '   Dim dtCustomerInfo As DataTable = RetrieveCustomerTable(conn)
        '  InsertIntoTblWebEventLog("JsonValue21", invoiceNumber, txtAccountID.Text, Session("UserID").ToString)

        Dim dtConsolidated As DataTable = RetrieveConsolidatedTable(conn, invoiceID)
        InsertIntoTblWebEventLog("JsonValue22", invoiceNumber, "", Session("UserID").ToString)

        Dim dtConsolidatedDetail As DataTable = RetrieveConsolidatedDetailTable(conn, invoiceID)
        InsertIntoTblWebEventLog("JsonValue23", invoiceNumber, "", Session("UserID").ToString)

        '  Dim dtContractInfo As DataTable = RetrieveContractInfo(conn, dtSalesDetail.Rows(0)("CostCode"))
        Dim dtSetup As DataTable = RetrieveSetupInfo(conn)

        conn.Close()
        InsertIntoTblWebEventLog("JsonValue2", invoiceNumber, "", Session("UserID").ToString)

        Try
         
            Dim AccountingSupplierParty_schemeAgencyName = dtCompanyInfo.Rows(0)("schemeAgencyName").ToString
            Dim AccountingSupplierParty_Party_IndustryClassificationCode_value = dtCompanyInfo.Rows(0)("IndustryClassificationCode").ToString
            Dim AccountingSupplierParty_Party_IndustryClassificationCode_name = dtCompanyInfo.Rows(0)("IndustryClassificationDesc").ToString

            Dim AccountingSupplierParty_Party_PartyIdentification_ID1 = dtCompanyInfo.Rows(0)("TaxIndentificationNo").ToString
            Dim AccountingSupplierParty_Party_PartyIdentification_schemeID1 = dtCompanyInfo.Rows(0)("EInvoiceID1").ToString
            Dim AccountingSupplierParty_Party_PartyIdentification_ID2 = dtCompanyInfo.Rows(0)("BRN").ToString
            Dim AccountingSupplierParty_Party_PartyIdentification_schemeID2 = dtCompanyInfo.Rows(0)("EInvoiceID2").ToString

            Dim AccountingSupplierParty_Party_PostalAddress_CityName = "SHAH ALAM"
            Dim AccountingSupplierParty_Party_PostalAddress_PostalZone = "40400"
            Dim AccountingSupplierParty_Party_PostalAddress_CountrySubentityCode = dtCompanyInfo.Rows(0)("CountryEntityCode").ToString
            Dim AccountingSupplierParty_Party_PostalAddress_AddressLine1 = dtCompanyInfo.Rows(0)("OfficeAddress1").ToString
            Dim AccountingSupplierParty_Party_PostalAddress_AddressLine2 = dtCompanyInfo.Rows(0)("OfficeAddress2").ToString
            Dim AccountingSupplierParty_Party_PostalAddress_AddressLine3 = "SEKSYEN 26"
            Dim AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_value = dtCompanyInfo.Rows(0)("CountryIdentificationValue").ToString
            Dim AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_listID = dtCompanyInfo.Rows(0)("CountryIdentificationListID").ToString
            Dim AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_listAgencyID = dtCompanyInfo.Rows(0)("CountryIdentificationAgencyID").ToString
            Dim AccountingSupplierParty_Party_PartyLegalEntity_RegistrationName = dtCompanyInfo.Rows(0)("CompanyName").ToString
            '  Dim AccountingSupplierParty_Party_Contact_Telephone = dtCompanyInfo.Rows(0)("TelephoneNumber").ToString
            Dim AccountingSupplierParty_Party_Contact_Telephone = "0116015723"

            Dim AccountingSupplierParty_Party_Contact_ElectronicMail = dtCompanyInfo.Rows(0)("Email").ToString

            ' InsertIntoTblWebEventLog("JsonValue3", invoiceNumber, dtCustomerInfo.Rows.Count.ToString, Session("UserID").ToString)


            ' for AccountingCustomerParty

            Dim AccountingCustomerParty_Party_PostalAddress_CityName = ""
            Dim AccountingCustomerParty_Party_PostalAddress_PostalZone = ""
            Dim AccountingCustomerParty_Party_PostalAddress_CountrySubentityCode = ""
            Dim AccountingCustomerParty_Party_PostalAddress_AddressLine1 = "NA"
            Dim AccountingCustomerParty_Party_PostalAddress_AddressLine2 = ""
            Dim AccountingCustomerParty_Party_PostalAddress_AddressLine3 = ""
            Dim AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_value = ""
            Dim AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_listID = "3166-1"
            Dim AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_listAgencyID = "ISO"

            Dim AccountingCustomerParty_Party_PartyLegalEntity_RegistrationName = "Consolidated Buyer's"

            Dim AccountingCustomerParty_Party_PartyIdentification_ID1 = "EI00000000010"
            Dim AccountingCustomerParty_Party_PartyIdentification_schemeID1 = "TIN"
            Dim AccountingCustomerParty_Party_PartyIdentification_ID2 = "NA"
            Dim AccountingCustomerParty_Party_PartyIdentification_schemeID2 = "BRN"
            Dim AccountingCustomerParty_Party_PartyIdentification_ID3 = "NA"
            Dim AccountingCustomerParty_Party_PartyIdentification_schemeID3 = "SST"

            Dim AccountingCustomerParty_Party_Contact_Telephone = "NA"
            Dim AccountingCustomerParty_Party_Contact_ElectronicMail = "NA"

            InsertIntoTblWebEventLog("JsonValue4", invoiceNumber, "", Session("UserID").ToString)


            'for TaxTotal
            Dim TaxTotal_TaxAmount_value = dtConsolidated.Rows(0)("GSTAmount").ToString
            Dim TaxTotal_TaxAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim TaxTotal_TaxSubtotal_TaxableAmount_value = dtConsolidated.Rows(0)("GSTAmount").ToString
            Dim TaxTotal_TaxSubtotal_TaxableAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim TaxTotal_TaxSubtotal_TaxAmount_value = dtConsolidated.Rows(0)("GSTAmount").ToString
            Dim TaxTotal_TaxSubtotal_TaxAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim TaxTotal_TaxSubtotal_TaxCategory_ID_value = "01"
            Dim TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_value = "OTH"
            Dim TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeAgencyID = "6"
            Dim TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeID = "UN/ECE 5153"

            'for LegalMonetaryTotal 
            Dim LegalMonetaryTotal_LineExtensionAmount_value As Decimal = Convert.ToDecimal(dtConsolidated.Rows(0)("BillAmount"))
            Dim LegalMonetaryTotal_LineExtensionAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim LegalMonetaryTotal_TaxExclusiveAmount_value As Decimal = Convert.ToDecimal(dtConsolidated.Rows(0)("BillAmount"))

            Dim LegalMonetaryTotal_TaxExclusiveAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim LegalMonetaryTotal_TaxInclusiveAmount_value = Convert.ToDecimal(dtConsolidated.Rows(0)("BillAmount").ToString) + Convert.ToDecimal(dtConsolidated.Rows(0)("GSTAmount").ToString)
            Dim LegalMonetaryTotal_TaxInclusiveAmount_currencyID = dtSetup.Rows(0)("ARCurrency")
            Dim LegalMonetaryTotal_PayableAmount_value = Convert.ToDecimal(dtConsolidated.Rows(0)("NetAmount").ToString)
            Dim LegalMonetaryTotal_PayableAmount_currencyID = dtSetup.Rows(0)("ARCurrency")

            InsertIntoTblWebEventLog("JsonValue5", invoiceNumber, "", Session("UserID").ToString)


            invoiceWrapper._D = invoiceDetails_D
            invoiceWrapper._A = invoiceDetails_A
            invoiceWrapper._B = invoiceDetails_B

            invoiceWrapper.Invoice = New List(Of Invoice_without_Certificate)()

            Dim invoice As New Invoice_without_Certificate()

            invoice.ID = New List(Of ID)
            Dim invoiceIDObj As New ID
            invoiceIDObj.Value = invoiceID
            invoice.ID.Add(invoiceIDObj)


            Dim IssueDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            Dim IssueTime = DateTime.UtcNow.ToString("HH:mm:ssZ")

            Dim InvoiceTypeCode = "01"

            Dim InvoiceListVersionID = "1.0"
            Dim DocumentCurrencyCode = dtSetup.Rows(0)("ARCurrency")

            ' Set IssueDate
            invoice.IssueDate = New List(Of IssueDate)
            Dim issueDateObj As New IssueDate
            '   issueDateObj.Value = Convert.ToDateTime(dtSales.Rows(0)("SalesDate")).ToString("yyyy-MM-dd")
            issueDateObj.Value = IssueDate
            invoice.IssueDate.Add(issueDateObj)

            InsertIntoTblWebEventLog("JsonValue7", invoiceNumber, "", Session("UserID").ToString)

            ' Set IssueTime
            invoice.IssueTime = New List(Of IssueTime)
            Dim issueTimeObj As New IssueTime
            '    issueTimeObj.Value = Convert.ToDateTime(dtSales.Rows(0)("CreatedOn")).ToString("yyyy-MM-dd")
            issueTimeObj.Value = IssueTime
            invoice.IssueTime.Add(issueTimeObj)

            ' Set InvoiceTypeCode
            invoice.InvoiceTypeCode = New List(Of InvoiceTypeCode)
            Dim invoiceTypeCodeObj As New InvoiceTypeCode
            invoiceTypeCodeObj.Value = InvoiceTypeCode
            invoiceTypeCodeObj.ListVersionID = InvoiceListVersionID
            invoice.InvoiceTypeCode.Add(invoiceTypeCodeObj)

            ' Set DocumentCurrencyCode
            invoice.DocumentCurrencyCode = New List(Of DocumentCurrencyCode)
            Dim docCurrencyCodeObj As New DocumentCurrencyCode
            docCurrencyCodeObj.Value = DocumentCurrencyCode
            invoice.DocumentCurrencyCode.Add(docCurrencyCodeObj)

            InsertIntoTblWebEventLog("JsonValue9", invoiceNumber, "", Session("UserID").ToString)

            ' AccountingSupplierParty start

            invoice.AccountingSupplierParty = New List(Of AccountingSupplierParty)

            ' Create the supplier object
            Dim supplier As New AccountingSupplierParty()


            ' Party
            supplier.Party = New List(Of Party)
            Dim partyObj As New Party()

            ' IndustryClassificationCode
            partyObj.IndustryClassificationCode = New List(Of IndustryClassificationCode)
            Dim industryCodeObj As New IndustryClassificationCode
            industryCodeObj.value = AccountingSupplierParty_Party_IndustryClassificationCode_value
            industryCodeObj.name = AccountingSupplierParty_Party_IndustryClassificationCode_name
            partyObj.IndustryClassificationCode.Add(industryCodeObj)

            ' PartyIdentification
            partyObj.PartyIdentification = New List(Of PartyIdentification)

            Dim partyIdentificationObj1 As New PartyIdentification
            partyIdentificationObj1.ID = New List(Of PartyIdentificationID)
            Dim partyIdentificationID1 As New PartyIdentificationID
            partyIdentificationID1.ID = AccountingSupplierParty_Party_PartyIdentification_ID1
            partyIdentificationID1.schemeID = AccountingSupplierParty_Party_PartyIdentification_schemeID1
            partyIdentificationObj1.ID.Add(partyIdentificationID1)
            partyObj.PartyIdentification.Add(partyIdentificationObj1)

            Dim partyIdentificationObj2 As New PartyIdentification
            partyIdentificationObj2.ID = New List(Of PartyIdentificationID)
            Dim partyIdentificationID2 As New PartyIdentificationID
            partyIdentificationID2.ID = AccountingSupplierParty_Party_PartyIdentification_ID2
            partyIdentificationID2.schemeID = AccountingSupplierParty_Party_PartyIdentification_schemeID2
            partyIdentificationObj2.ID.Add(partyIdentificationID2)
            partyObj.PartyIdentification.Add(partyIdentificationObj2)

            ' PostalAddress
            partyObj.PostalAddress = New List(Of PostalAddress)
            Dim postalAddressObj As New PostalAddress

            postalAddressObj.CityName = New List(Of CityName)
            Dim cityNameObj As New CityName
            cityNameObj.value = AccountingSupplierParty_Party_PostalAddress_CityName
            postalAddressObj.CityName.Add(cityNameObj)

            postalAddressObj.PostalZone = New List(Of PostalZone)
            Dim postalZoneObj As New PostalZone
            postalZoneObj.value = AccountingSupplierParty_Party_PostalAddress_PostalZone
            postalAddressObj.PostalZone.Add(postalZoneObj)

            postalAddressObj.CountrySubentityCode = New List(Of CountrySubentityCode)
            Dim countrySubentityCodeObj As New CountrySubentityCode
            countrySubentityCodeObj.value = AccountingSupplierParty_Party_PostalAddress_CountrySubentityCode
            postalAddressObj.CountrySubentityCode.Add(countrySubentityCodeObj)

            InsertIntoTblWebEventLog("JsonValue10", invoiceNumber, "", Session("UserID").ToString)

            ' AddressLine
            postalAddressObj.AddressLine = New List(Of AddressLine)

            Dim addressLineObj1 As New AddressLine
            addressLineObj1.Line = New List(Of Line)
            Dim lineObj1 As New Line
            lineObj1.value = AccountingSupplierParty_Party_PostalAddress_AddressLine1
            addressLineObj1.Line.Add(lineObj1)
            postalAddressObj.AddressLine.Add(addressLineObj1)

            Dim addressLineObj2 As New AddressLine
            addressLineObj2.Line = New List(Of Line)
            Dim lineObj2 As New Line
            lineObj2.value = AccountingSupplierParty_Party_PostalAddress_AddressLine2
            addressLineObj2.Line.Add(lineObj2)
            postalAddressObj.AddressLine.Add(addressLineObj2)

            Dim addressLineObj3 As New AddressLine
            addressLineObj3.Line = New List(Of Line)
            Dim lineObj3 As New Line
            lineObj3.value = AccountingSupplierParty_Party_PostalAddress_AddressLine3
            addressLineObj3.Line.Add(lineObj3)
            postalAddressObj.AddressLine.Add(addressLineObj3)

            ' Country
            postalAddressObj.Country = New List(Of Country)
            Dim countryObj As New Country
            countryObj.IdentificationCode = New List(Of IdentificationCode)
            Dim identificationCodeObj As New IdentificationCode
            identificationCodeObj.value = AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_value
            identificationCodeObj.listID = AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_listID
            identificationCodeObj.listAgencyID = AccountingSupplierParty_Party_PostalAddress_Country_IdentificationCode_listAgencyID
            countryObj.IdentificationCode.Add(identificationCodeObj)
            postalAddressObj.Country.Add(countryObj)

            partyObj.PostalAddress.Add(postalAddressObj)

            InsertIntoTblWebEventLog("JsonValue11", invoiceNumber, "", Session("UserID").ToString)

            ' PartyLegalEntity
            partyObj.PartyLegalEntity = New List(Of PartyLegalEntity)
            Dim partyLegalEntityObj As New PartyLegalEntity
            partyLegalEntityObj.RegistrationName = New List(Of RegistrationName)
            Dim registrationNameObj As New RegistrationName
            registrationNameObj.value = AccountingSupplierParty_Party_PartyLegalEntity_RegistrationName
            partyLegalEntityObj.RegistrationName.Add(registrationNameObj)
            partyObj.PartyLegalEntity.Add(partyLegalEntityObj)

            ' Contact
            partyObj.Contact = New List(Of Contact)
            Dim contactObj As New Contact

            contactObj.Telephone = New List(Of Telephone)
            Dim telephoneObj As New Telephone
            telephoneObj.value = AccountingSupplierParty_Party_Contact_Telephone
            contactObj.Telephone.Add(telephoneObj)

            contactObj.ElectronicMail = New List(Of ElectronicMail)
            Dim emailObj As New ElectronicMail
            emailObj.value = AccountingSupplierParty_Party_Contact_ElectronicMail
            contactObj.ElectronicMail.Add(emailObj)

            partyObj.Contact.Add(contactObj)

            ' Add the configured party to the supplier
            supplier.Party.Add(partyObj)

            ' Add supplier to AccountingSupplierParty
            invoice.AccountingSupplierParty.Add(supplier)

            ' AccountingSupplierParty end

            InsertIntoTblWebEventLog("JsonValue12", invoiceNumber, "", Session("UserID").ToString)

            ' AccountingCustomerParty start

            invoice.AccountingCustomerParty = New List(Of AccountingCustomerParty)

            ' Create the AccountingCustomerParty object
            Dim customer As New AccountingCustomerParty()

            ' Create the Party list
            customer.Party = New List(Of Party)
            Dim customerpartyObj As New Party()

            ' PostalAddress
            customerpartyObj.PostalAddress = New List(Of PostalAddress)
            Dim customerpostalAddressObj As New PostalAddress

            customerpostalAddressObj.CityName = New List(Of CityName)
            Dim customercityNameObj As New CityName
            customercityNameObj.value = AccountingCustomerParty_Party_PostalAddress_CityName
            customerpostalAddressObj.CityName.Add(customercityNameObj)

            customerpostalAddressObj.PostalZone = New List(Of PostalZone)
            Dim customerpostalZoneObj As New PostalZone
            customerpostalZoneObj.value = AccountingCustomerParty_Party_PostalAddress_PostalZone
            customerpostalAddressObj.PostalZone.Add(customerpostalZoneObj)

            customerpostalAddressObj.CountrySubentityCode = New List(Of CountrySubentityCode)
            Dim customercountrySubentityCodeObj As New CountrySubentityCode
            customercountrySubentityCodeObj.value = AccountingCustomerParty_Party_PostalAddress_CountrySubentityCode
            customerpostalAddressObj.CountrySubentityCode.Add(customercountrySubentityCodeObj)

            ' AddressLine
            customerpostalAddressObj.AddressLine = New List(Of AddressLine)

            Dim customeraddressLineObj1 As New AddressLine
            customeraddressLineObj1.Line = New List(Of Line)
            Dim customerlineObj1 As New Line
            customerlineObj1.value = AccountingCustomerParty_Party_PostalAddress_AddressLine1
            customeraddressLineObj1.Line.Add(customerlineObj1)
            customerpostalAddressObj.AddressLine.Add(customeraddressLineObj1)

            Dim customeraddressLineObj2 As New AddressLine
            customeraddressLineObj2.Line = New List(Of Line)
            Dim customerlineObj2 As New Line
            customerlineObj2.value = AccountingCustomerParty_Party_PostalAddress_AddressLine2
            customeraddressLineObj2.Line.Add(customerlineObj2)
            customerpostalAddressObj.AddressLine.Add(customeraddressLineObj2)

            Dim customeraddressLineObj3 As New AddressLine
            customeraddressLineObj3.Line = New List(Of Line)
            Dim customerlineObj3 As New Line
            customerlineObj3.value = AccountingCustomerParty_Party_PostalAddress_AddressLine3
            customeraddressLineObj3.Line.Add(customerlineObj3)
            customerpostalAddressObj.AddressLine.Add(customeraddressLineObj3)

            ' Country
            customerpostalAddressObj.Country = New List(Of Country)
            Dim customercountryObj As New Country
            customercountryObj.IdentificationCode = New List(Of IdentificationCode)
            Dim customeridentificationCodeObj As New IdentificationCode
            customeridentificationCodeObj.value = AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_value
            customeridentificationCodeObj.listID = AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_listID
            customeridentificationCodeObj.listAgencyID = AccountingCustomerParty_Party_PostalAddress_Country_IdentificationCode_listAgencyID
            customercountryObj.IdentificationCode.Add(customeridentificationCodeObj)
            customerpostalAddressObj.Country.Add(customercountryObj)

            customerpartyObj.PostalAddress.Add(customerpostalAddressObj)

            InsertIntoTblWebEventLog("JsonValue14", invoiceNumber, "", Session("UserID").ToString)

            ' PartyLegalEntity
            customerpartyObj.PartyLegalEntity = New List(Of PartyLegalEntity)
            Dim customerpartyLegalEntityObj As New PartyLegalEntity
            customerpartyLegalEntityObj.RegistrationName = New List(Of RegistrationName)
            Dim customerregistrationNameObj As New RegistrationName
            customerregistrationNameObj.value = AccountingCustomerParty_Party_PartyLegalEntity_RegistrationName
            customerpartyLegalEntityObj.RegistrationName.Add(customerregistrationNameObj)
            customerpartyObj.PartyLegalEntity.Add(customerpartyLegalEntityObj)

            ' PartyIdentification
            customerpartyObj.PartyIdentification = New List(Of PartyIdentification)

            ' First PartyIdentification
            Dim customerpartyIdentificationObj1 As New PartyIdentification
            customerpartyIdentificationObj1.ID = New List(Of PartyIdentificationID)
            Dim customerpartyIdentificationID1 As New PartyIdentificationID
            customerpartyIdentificationID1.ID = AccountingCustomerParty_Party_PartyIdentification_ID1
            customerpartyIdentificationID1.schemeID = AccountingCustomerParty_Party_PartyIdentification_schemeID1
            customerpartyIdentificationObj1.ID.Add(customerpartyIdentificationID1)
            customerpartyObj.PartyIdentification.Add(customerpartyIdentificationObj1)

            ' Second PartyIdentification
            Dim customerpartyIdentificationObj2 As New PartyIdentification
            customerpartyIdentificationObj2.ID = New List(Of PartyIdentificationID)
            Dim customerpartyIdentificationID2 As New PartyIdentificationID
            customerpartyIdentificationID2.ID = AccountingCustomerParty_Party_PartyIdentification_ID2
            customerpartyIdentificationID2.schemeID = AccountingCustomerParty_Party_PartyIdentification_schemeID2
            customerpartyIdentificationObj2.ID.Add(customerpartyIdentificationID2)
            customerpartyObj.PartyIdentification.Add(customerpartyIdentificationObj2)

            ' Third PartyIdentification
            Dim customerpartyIdentificationObj3 As New PartyIdentification
            customerpartyIdentificationObj3.ID = New List(Of PartyIdentificationID)
            Dim customerpartyIdentificationID3 As New PartyIdentificationID
            customerpartyIdentificationID3.ID = AccountingCustomerParty_Party_PartyIdentification_ID3
            customerpartyIdentificationID3.schemeID = AccountingCustomerParty_Party_PartyIdentification_schemeID3
            customerpartyIdentificationObj3.ID.Add(customerpartyIdentificationID3)
            customerpartyObj.PartyIdentification.Add(customerpartyIdentificationObj3)

            ' Contact
            customerpartyObj.Contact = New List(Of Contact)
            Dim customercontactObj As New Contact

            customercontactObj.Telephone = New List(Of Telephone)
            Dim customertelephoneObj As New Telephone
            customertelephoneObj.value = AccountingCustomerParty_Party_Contact_Telephone
            customercontactObj.Telephone.Add(customertelephoneObj)

            customercontactObj.ElectronicMail = New List(Of ElectronicMail)
            Dim customeremailObj As New ElectronicMail
            customeremailObj.value = AccountingCustomerParty_Party_Contact_ElectronicMail
            customercontactObj.ElectronicMail.Add(customeremailObj)

            customerpartyObj.Contact.Add(customercontactObj)

            ' Add the configured party to customer
            customer.Party.Add(customerpartyObj)

            ' IndustryClassificationCode is Nothing in this case
            '  customerpartyObj.IndustryClassificationCode = Nothing

            InsertIntoTblWebEventLog("JsonValue13", invoiceNumber, "", Session("UserID").ToString)

            invoice.AccountingCustomerParty.Add(customer)

            InsertIntoTblWebEventLog("JsonValue15", invoiceNumber, "", Session("UserID").ToString)

            ' AccountingCustomerParty end


            'TaxTotal start


            invoice.TaxTotal = New List(Of TaxTotal)

            ' Create the TaxTotal object
            Dim taxTotal As New TaxTotal()

            ' Set TaxAmount
            taxTotal.TaxAmount = New List(Of TaxAmount)
            Dim taxAmountObj As New TaxAmount
            taxAmountObj.value = TaxTotal_TaxAmount_value
            taxAmountObj.currencyID = TaxTotal_TaxAmount_currencyID
            taxTotal.TaxAmount.Add(taxAmountObj)

            ' Set TaxSubtotal
            taxTotal.TaxSubtotal = New List(Of TaxSubtotal)

            ' First TaxSubtotal
            Dim taxSubtotalObj1 As New TaxSubtotal

            ' TaxableAmount for first TaxSubtotal
            taxSubtotalObj1.TaxableAmount = New List(Of TaxableAmount)
            Dim taxableAmountObj1 As New TaxableAmount
            taxableAmountObj1.value = TaxTotal_TaxSubtotal_TaxableAmount_value
            taxableAmountObj1.currencyID = TaxTotal_TaxSubtotal_TaxableAmount_currencyID
            taxSubtotalObj1.TaxableAmount.Add(taxableAmountObj1)

            ' TaxAmount for first TaxSubtotal
            taxSubtotalObj1.TaxAmount = New List(Of TaxAmount)
            Dim taxAmountObj1_1 As New TaxAmount
            taxAmountObj1_1.value = TaxTotal_TaxSubtotal_TaxAmount_value
            taxAmountObj1_1.currencyID = TaxTotal_TaxSubtotal_TaxAmount_currencyID
            taxSubtotalObj1.TaxAmount.Add(taxAmountObj1_1)

            ' TaxCategory for first TaxSubtotal
            taxSubtotalObj1.TaxCategory = New List(Of TaxCategory)
            Dim taxCategoryObj1 As New TaxCategory

            taxCategoryObj1.ID = New List(Of ID)
            Dim taxCategoryIDObj1 As New ID
            taxCategoryIDObj1.Value = TaxTotal_TaxSubtotal_TaxCategory_ID_value
            taxCategoryObj1.ID.Add(taxCategoryIDObj1)

            taxCategoryObj1.TaxScheme = New List(Of TaxScheme)
            Dim taxSchemeObj1 As New TaxScheme
            taxSchemeObj1.ID = New List(Of TaxSchemeID)
            Dim taxSchemeIDObj1 As New TaxSchemeID
            taxSchemeIDObj1.ID = TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_value
            taxSchemeIDObj1.schemeAgencyID = TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeAgencyID
            taxSchemeIDObj1.schemeID = TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeID
            taxSchemeObj1.ID.Add(taxSchemeIDObj1)

            taxCategoryObj1.TaxScheme.Add(taxSchemeObj1)

            taxSubtotalObj1.TaxCategory.Add(taxCategoryObj1)

            taxTotal.TaxSubtotal.Add(taxSubtotalObj1)

            invoice.TaxTotal.Add(taxTotal)
            'TaxTotal end
            InsertIntoTblWebEventLog("JsonValue21", invoiceNumber, "", Session("UserID").ToString)


            'LegalMonetaryTotal start
            invoice.LegalMonetaryTotal = New List(Of LegalMonetaryTotal)

            ' Create the LegalMonetaryTotal object
            Dim legalMonetaryTotal As New LegalMonetaryTotal()

            ' Set LineExtensionAmount
            legalMonetaryTotal.LineExtensionAmount = New List(Of LineExtensionAmount)
            Dim lineExtensionAmountObj As New LineExtensionAmount
            lineExtensionAmountObj.value = LegalMonetaryTotal_LineExtensionAmount_value
            lineExtensionAmountObj.currencyID = LegalMonetaryTotal_LineExtensionAmount_currencyID
            legalMonetaryTotal.LineExtensionAmount.Add(lineExtensionAmountObj)

            ' Set TaxExclusiveAmount
            legalMonetaryTotal.TaxExclusiveAmount = New List(Of TaxExclusiveAmount)
            Dim taxExclusiveAmountObj As New TaxExclusiveAmount
            taxExclusiveAmountObj.value = LegalMonetaryTotal_TaxExclusiveAmount_value
            taxExclusiveAmountObj.currencyID = LegalMonetaryTotal_TaxExclusiveAmount_currencyID
            legalMonetaryTotal.TaxExclusiveAmount.Add(taxExclusiveAmountObj)

            ' Set TaxInclusiveAmount
            legalMonetaryTotal.TaxInclusiveAmount = New List(Of TaxInclusiveAmount)
            Dim taxInclusiveAmountObj As New TaxInclusiveAmount
            taxInclusiveAmountObj.value = LegalMonetaryTotal_TaxInclusiveAmount_value
            taxInclusiveAmountObj.currencyID = LegalMonetaryTotal_TaxInclusiveAmount_currencyID
            legalMonetaryTotal.TaxInclusiveAmount.Add(taxInclusiveAmountObj)

            ' Set PayableAmount
            legalMonetaryTotal.PayableAmount = New List(Of PayableAmount)
            Dim legalMonetarypayableAmountObj As New PayableAmount
            legalMonetarypayableAmountObj.value = LegalMonetaryTotal_PayableAmount_value
            legalMonetarypayableAmountObj.currencyID = LegalMonetaryTotal_PayableAmount_currencyID
            legalMonetaryTotal.PayableAmount.Add(legalMonetarypayableAmountObj)


            invoice.LegalMonetaryTotal.Add(legalMonetaryTotal)
            'LegalMonetaryTotal end
            InsertIntoTblWebEventLog("JsonValue22", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)



            'InvoiceLine start
            invoice.InvoiceLine = New List(Of InvoiceLine)

            If dtConsolidatedDetail.Rows.Count > 0 Then

                For a = 0 To dtConsolidatedDetail.Rows.Count - 1


                    InsertIntoTblWebEventLog("JsonValue22A", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)


                    Dim InvoiceLine_ID_value = (a + 1).ToString
                    Dim InvoiceLine_InvoicedQuantity_value = 1
                    Dim InvoiceLine_InvoicedQuantity_unitCode = "C62"

                    Dim InvoiceLine_LineExtensionAmount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("AppliedBase")).ToString("F2")
                    Dim InvoiceLine_LineExtensionAmount_currencyID = dtSetup.Rows(0)("ARCurrency")

                    Dim InvoiceLine_TaxTotal_TaxAmount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("GSTAmount")).ToString("F2")
                    Dim InvoiceLine_TaxTotal_TaxAmount_currencyID = dtSetup.Rows(0)("ARCurrency")

                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxableAmount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("ValueBase")).ToString("F2")
                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxableAmount_currencyID = dtSetup.Rows(0)("ARCurrency")

                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxAmount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("GSTAmount")).ToString("F2")
                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxAmount_currencyID = dtSetup.Rows(0)("ARCurrency")

                    Dim InvoiceLine_TaxTotal_TaxSubtotal_Percent = Nothing


                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_ID_value = "01" ' dtSalesDetail.Rows(a)("TaxTypeCode")
                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_value = "OTH"
                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeID = "UN/ECE 5153"
                    Dim InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeAgencyID = "6"


                    Dim InvoiceLine_Item_CommodityClassification_ItemClassificationCode_value = "004"
                    Dim InvoiceLine_Item_CommodityClassification_ItemClassificationCode_listID = "CLASS"

                    '  Dim InvoiceLine_Item_Description_value = "Receipt 001 - 100"
                    Dim InvoiceLine_Item_Description_value = "INVOICE : " & dtConsolidatedDetail.Rows(a)("InvoiceNo").ToString '& " : " '& Convert.ToDateTime(dtSales.Rows(0)("SalesDate")).ToString("yyyy-MM-dd")
                    Dim InvoiceLine_Item_OriginCountry_IdentificationCode_value = "MYS"

                    Dim InvoiceLine_Price_PriceAmount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("ValueBase")).ToString("F2")
                    Dim InvoiceLine_Price_PriceAmount_currencyID = dtSetup.Rows(0)("ARCurrency")


                    Dim InvoiceLine_ItemPriceExtension_Amount_value As Decimal = Convert.ToDecimal(dtConsolidatedDetail.Rows(a)("ValueBase")).ToString("F2")

                    Dim InvoiceLine_ItemPriceExtension_Amount_currencyID = dtSetup.Rows(0)("ARCurrency")

                    ' Create the InvoiceLine object
                    Dim invoiceLine As New InvoiceLine()

                    ' ID
                    invoiceLine.ID = New List(Of ID)
                    Dim invoiceLineIDObj As New ID
                    invoiceLineIDObj.Value = InvoiceLine_ID_value
                    invoiceLine.ID.Add(invoiceLineIDObj)

                    ' InvoicedQuantity
                    invoiceLine.InvoicedQuantity = New List(Of InvoicedQuantity)
                    Dim invoicedQuantityObj As New InvoicedQuantity
                    invoicedQuantityObj.value = InvoiceLine_InvoicedQuantity_value
                    invoicedQuantityObj.unitCode = InvoiceLine_InvoicedQuantity_unitCode
                    invoiceLine.InvoicedQuantity.Add(invoicedQuantityObj)
                    InsertIntoTblWebEventLog("JsonValue22E", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)


                    ' LineExtensionAmount
                    invoiceLine.LineExtensionAmount = New List(Of LineExtensionAmount)
                    Dim invoiceLine_lineExtensionAmountObj As New LineExtensionAmount
                    invoiceLine_lineExtensionAmountObj.value = InvoiceLine_LineExtensionAmount_value
                    invoiceLine_lineExtensionAmountObj.currencyID = InvoiceLine_LineExtensionAmount_currencyID
                    invoiceLine.LineExtensionAmount.Add(invoiceLine_lineExtensionAmountObj)

                    ' TaxTotal
                    invoiceLine.TaxTotal = New List(Of TaxTotal)
                    Dim invoiceLinetaxTotalObj As New TaxTotal
                    InsertIntoTblWebEventLog("JsonValue22G1", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoiceLinetaxTotalObj.TaxAmount = New List(Of TaxAmount)
                    Dim invoiceLinetaxTotalAmountObj As New TaxAmount
                    invoiceLinetaxTotalAmountObj.value = InvoiceLine_TaxTotal_TaxAmount_value
                    invoiceLinetaxTotalAmountObj.currencyID = InvoiceLine_TaxTotal_TaxAmount_currencyID
                    invoiceLinetaxTotalObj.TaxAmount.Add(invoiceLinetaxTotalAmountObj)
                    InsertIntoTblWebEventLog("JsonValue22G2", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoiceLinetaxTotalObj.TaxSubtotal = New List(Of TaxSubtotal)

                    ' First TaxSubtotal
                    Dim invoiceLinetaxSubtotalObj1 As New TaxSubtotal
                    invoiceLinetaxSubtotalObj1.TaxableAmount = New List(Of TaxableAmount)
                    Dim invoiceLinetaxSubtotalTaxableAmountObj1 As New TaxableAmount
                    invoiceLinetaxSubtotalTaxableAmountObj1.value = InvoiceLine_TaxTotal_TaxSubtotal_TaxableAmount_value
                    invoiceLinetaxSubtotalTaxableAmountObj1.currencyID = InvoiceLine_TaxTotal_TaxSubtotal_TaxableAmount_currencyID
                    invoiceLinetaxSubtotalObj1.TaxableAmount.Add(invoiceLinetaxSubtotalTaxableAmountObj1)
                    InsertIntoTblWebEventLog("JsonValue22G3", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoiceLinetaxSubtotalObj1.TaxAmount = New List(Of TaxAmount)
                    Dim invoiceLinetaxSubtotalTaxAmountObj1 As New TaxAmount
                    invoiceLinetaxSubtotalTaxAmountObj1.value = InvoiceLine_TaxTotal_TaxSubtotal_TaxAmount_value
                    invoiceLinetaxSubtotalTaxAmountObj1.currencyID = InvoiceLine_TaxTotal_TaxSubtotal_TaxAmount_currencyID
                    invoiceLinetaxSubtotalObj1.TaxAmount.Add(invoiceLinetaxSubtotalTaxAmountObj1)
                    InsertIntoTblWebEventLog("JsonValue22G4", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoiceLinetaxSubtotalObj1.Percent = InvoiceLine_TaxTotal_TaxSubtotal_Percent

                    invoiceLinetaxSubtotalObj1.TaxCategory = New List(Of TaxCategory)
                    Dim invoiceLinetaxCategoryObj1 As New TaxCategory
                    invoiceLinetaxCategoryObj1.ID = New List(Of ID)
                    Dim invoiceLinetaxCategoryIDObj1 As New ID
                    invoiceLinetaxCategoryIDObj1.Value = InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_ID_value
                    invoiceLinetaxCategoryObj1.ID.Add(invoiceLinetaxCategoryIDObj1)

                    InsertIntoTblWebEventLog("JsonValue22H", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoiceLinetaxCategoryObj1.TaxScheme = New List(Of TaxScheme)
                    Dim invoiceLinetaxSchemeObj1 As New TaxScheme
                    invoiceLinetaxSchemeObj1.ID = New List(Of TaxSchemeID)
                    Dim invoiceLinetaxSchemeIDObj1 As New TaxSchemeID
                    invoiceLinetaxSchemeIDObj1.ID = InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_value
                    invoiceLinetaxSchemeIDObj1.schemeID = InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeID
                    invoiceLinetaxSchemeIDObj1.schemeAgencyID = InvoiceLine_TaxTotal_TaxSubtotal_TaxCategory_TaxScheme_ID_schemeAgencyID
                    invoiceLinetaxSchemeObj1.ID.Add(invoiceLinetaxSchemeIDObj1)
                    invoiceLinetaxCategoryObj1.TaxScheme.Add(invoiceLinetaxSchemeObj1)

                    invoiceLinetaxSubtotalObj1.TaxCategory.Add(invoiceLinetaxCategoryObj1)

                    invoiceLinetaxTotalObj.TaxSubtotal.Add(invoiceLinetaxSubtotalObj1)

                    invoiceLine.TaxTotal.Add(invoiceLinetaxTotalObj)

                    ' Item
                    invoiceLine.Item = New List(Of Item)
                    Dim itemObj As New Item

                    ' CommodityClassification
                    itemObj.CommodityClassification = New List(Of CommodityClassification)
                    Dim invoiceLinecommodityClassificationObj As New CommodityClassification
                    invoiceLinecommodityClassificationObj.ItemClassificationCode = New List(Of ItemClassificationCode)
                    Dim invoiceLineitemClassificationCodeObj As New ItemClassificationCode
                    invoiceLineitemClassificationCodeObj.value = InvoiceLine_Item_CommodityClassification_ItemClassificationCode_value
                    invoiceLineitemClassificationCodeObj.listID = InvoiceLine_Item_CommodityClassification_ItemClassificationCode_listID
                    invoiceLinecommodityClassificationObj.ItemClassificationCode.Add(invoiceLineitemClassificationCodeObj)
                    itemObj.CommodityClassification.Add(invoiceLinecommodityClassificationObj)
                    InsertIntoTblWebEventLog("JsonValue22E1", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    ' Description
                    itemObj.Description = New List(Of Description)
                    Dim invoiceLinedescriptionObj As New Description
                    invoiceLinedescriptionObj.Value = InvoiceLine_Item_Description_value
                    itemObj.Description.Add(invoiceLinedescriptionObj)
                    InsertIntoTblWebEventLog("JsonValue22E2", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    ' OriginCountry
                    itemObj.OriginCountry = New List(Of OriginCountry)
                    Dim invoiceLineoriginCountryObj As New OriginCountry
                    invoiceLineoriginCountryObj.IdentificationCode = New List(Of IdentificationCodeID)
                    Dim identificationCodeIDObj As New IdentificationCodeID
                    identificationCodeIDObj.Value = InvoiceLine_Item_OriginCountry_IdentificationCode_value
                    invoiceLineoriginCountryObj.IdentificationCode.Add(identificationCodeIDObj)
                    itemObj.OriginCountry.Add(invoiceLineoriginCountryObj)

                    invoiceLine.Item.Add(itemObj)
                    InsertIntoTblWebEventLog("JsonValue22F", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    ' ItemPriceExtension
                    invoiceLine.ItemPriceExtension = New List(Of ItemPriceExtension)
                    Dim invoiceLineitemPriceExtensionObj As New ItemPriceExtension
                    invoiceLineitemPriceExtensionObj.Amount = New List(Of Amount)
                    Dim invoiceLineitemPriceExtensionAmountObj As New Amount
                    invoiceLineitemPriceExtensionAmountObj.value = InvoiceLine_ItemPriceExtension_Amount_value
                    invoiceLineitemPriceExtensionAmountObj.currencyID = InvoiceLine_ItemPriceExtension_Amount_currencyID
                    invoiceLineitemPriceExtensionObj.Amount.Add(invoiceLineitemPriceExtensionAmountObj)
                    invoiceLine.ItemPriceExtension.Add(invoiceLineitemPriceExtensionObj)

                    ' Price
                    invoiceLine.Price = New List(Of Price)
                    Dim invoiceLinepriceObj As New Price
                    invoiceLinepriceObj.PriceAmount = New List(Of PriceAmount)
                    Dim invoiceLinepriceAmountObj As New PriceAmount
                    invoiceLinepriceAmountObj.value = InvoiceLine_Price_PriceAmount_value
                    invoiceLinepriceAmountObj.currencyID = InvoiceLine_Price_PriceAmount_currencyID
                    invoiceLinepriceObj.PriceAmount.Add(invoiceLinepriceAmountObj)
                    invoiceLine.Price.Add(invoiceLinepriceObj)
                    InsertIntoTblWebEventLog("JsonValue22G", invoiceNumber, dtConsolidatedDetail.Rows.Count, Session("UserID").ToString)

                    invoice.InvoiceLine.Add(invoiceLine)
                Next
            End If



            InsertIntoTblWebEventLog("JsonValue24", invoiceNumber, "", Session("UserID").ToString)


            invoiceWrapper.Invoice.Add(invoice)
            InsertIntoTblWebEventLog("JsonValue24", invoiceNumber, "", Session("UserID").ToString)


            '  Dim settings As New JsonSerializerSettings With {
            '    .NullValueHandling = NullValueHandling.Ignore
            '}

            InsertIntoTblWebEventLog("JsonValue25", invoiceNumber, JsonConvert.SerializeObject(invoiceWrapper).ToString, Session("UserID").ToString)

            Dim settings As New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Ignore,
                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }

            Dim json As String = JsonConvert.SerializeObject(invoiceWrapper, settings)
            InsertIntoTblWebEventLog("JsonValue26", invoiceNumber, json, Session("UserID").ToString)


            Return json

        Catch ex As Exception
            InsertIntoTblWebEventLog("JsonError2", ex.Message.ToString, invoiceID, Session("UserID"))
            lblMessage.Text = ex.Message.ToString

        End Try
    End Function

    Protected Sub btnPostEInvoice_Click(sender As Object, e As EventArgs) Handles btnPostEInvoice.Click
        InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE - " + txtCreatedBy.Text, txtEInvoiceStatus.Text, txtRcNo.Text, txtInvoiceNo.Text)

        If txtEInvoiceStatus.Text = "FAILURE" Then
            lblAlert.Text = "CONSOLIDATED INVOICE STATUS NOT CONFIRMED. TRY AFTER SOMETIME"

            btnPostEInvoice.Enabled = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "KeyClient", "<script> ResetScrollPosition();</Script>", False)
            Exit Sub
        End If

        mdlEInvoiceConfirm.Show()


        'If txtEInvoiceStatus.Text = "APPROVED" Or txtEInvoiceStatus.Text = "VALID" Then

        '    ' txtReasonCancelEInvoice.Visible = False
        '    txtCancelCode.Text = ""
        '    lblRCNO.Text = "(" & txtRcNo.Text & ")"
        '    InsertIntoTblWebEventLog("INVOICE - " + txtCreatedBy.Text, txtLastPostedDate.Text, txtLastPostedBy.Text, txtInvoiceNo.Text)
        '    'lblRCNO.Text = txtInvoiceNo.Text
        '    'InsertIntoTblWebEventLog("INVOICE - " + txtCreatedBy.Text, txtEInvoiceStatus.Text, lblRcNo.Text, txtInvoiceNo.Text)
        '    ddlReasonforCancellation.SelectedIndex = 0
        '    '  ddlReasonforCancellation_SelectedIndexChanged(sender,e)
        '    reasonother.Visible = False
        '    txtReasonCancelEInvoice.Text = ""

        '    txtCancelInvoiceNo.Text = txtInvoiceNo.Text
        '    txtCancelLastPosted.Text = txtLastPostedDate.Text
        '    txtCancelLastPostedBy.Text = txtLastPostedBy.Text

        '    mdlConfirmCancelEInvoice.Show()
        'Else

        'If Date.Compare(CDate(txtInvoiceDate.Text), CDate(txtEInvoiceStartDate.Text)) < 0 Then
        '    lblAlert.Text = "ONLY INVOICES STARTING " & txtEInvoiceStartDate.Text & " CAN BE POSTED TO E-INVOICE"
        '    Return
        'End If


        '   End If

    End Sub


    Public Shared Function GetSHA256Hash(input As String) As String
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)

        Using sha256 As SHA256 = sha256.Create()
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)

            Dim sb As New StringBuilder()
            For Each b As Byte In hashBytes
                sb.Append(b.ToString("x2")) ' Format as hexadecimal
            Next

            Return sb.ToString()
        End Using
    End Function

    Protected Sub EInvoiceConfirmYes_Click(sender As Object, e As EventArgs) Handles EInvoiceConfirmYes.Click
      
        Dim token = GetToken()
        Dim tokenno As String = ""
        tokenno = AccessToken
        InsertIntoTblWebEventLog("CONSOLIDATED INVOICE", "ACCESSTOKEN", AccessToken, Session("UserID").ToString)

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(tokenno) Then
                '  Try

          
                InsertIntoTblWebEventLog("CONSOLIDATED INVOICE", "EInvoiceConfirmYes0", AccessToken, Session("UserID").ToString)

                Dim RESULT = submitDocument_without_Certificate(tokenno)
                        InsertIntoTblWebEventLog("EInvoiceConfirmYes1", RESULT.ToString, "", Session("UserID").ToString)
                        If Not String.IsNullOrEmpty(RESULT) Then
                            If RESULT = "error message" Then
                                lblMessage.Text = "Document Submission Failed"
                            Else
                        InsertIntoTblWebEventLog("EInvoiceConfirmYes1", RESULT.ToString, "", Session("UserID").ToString)

                                Dim submissionid = GetSubmission(tokenno)

                                If submissionid = "ERROR" Then
                                    txtEInvoiceStatus.Text = "REJECTED"
                                    txtSubmissionID.Text = ""
                                    txtUUID.Text = ""
                                    txtLongID.Text = ""
                                    lblMessage.Text = "Document Submission Failed : Portal Error"
                                    lblMessage.Text = lblMessage.Text + "<br/>" + txtRejectedDocError.Text

                                    Return
                                Else
                                    InsertIntoTblWebEventLog("EInvoiceStatus1", txtUUID.Text, txtEInvoiceStatus.Text, Session("UserID").ToString)
                                    Thread.Sleep(10000)
                                    '  If txtEInvoiceStatus.Text <> "VALID" Then
                                    txtEInvoiceStatus.Text = GetDocument(tokenno, txtUUID.Text)

                                    'End If
                                    InsertIntoTblWebEventLog("EInvoiceStatus2", txtUUID.Text, txtEInvoiceStatus.Text, Session("UserID").ToString)

                                End If

                                InsertIntoTblWebEventLog("EInvoiceConfirmYes2", submissionid.ToString, txtInvoiceNo.Text, Session("UserID").ToString)

                                UpdateEInvoiceSubmission(txtInvoiceNo.Text)

                                InsertIntoTblWebEventLog("EInvoiceConfirmYes3", txtSubmissionDate.Text, txtInvoiceNo.Text, Session("UserID").ToString)

                                InsertIntoTblEInvoiceEventLog("SUBMISSION", txtUUID.Text)


                                '   CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "EINVOICE", txtInvoiceNo.Text, "", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

                                '  CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "EINVOICE", txtInvoiceNo.Text, "SUBMISSION", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, txtSubmissionID.Text, "", txtRcno.Text)
                                '  CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "INVOICE", txtInvoiceNo.Text, "DELETE", Convert.ToDateTime(txt), 0, 0, 0, txtAccountIdBilling.Text, "", txtRcno.Text)

                                SQLDSInvoice.SelectCommand = txt.Text
                                SQLDSInvoice.DataBind()

                                GridView1.DataBind()
                        btnDelete.Enabled = False
                        btnDelete.ForeColor = System.Drawing.Color.Gray
                        btnPostEinvoice.Enabled = False
                        btnPostEinvoice.ForeColor = System.Drawing.Color.Gray
                
                       
                        'Dim result = submitDocument()
                        ' lblMessage.Text = "Document Submitted Successfully <br/> Response From API: </br> " + RESULT + "<br/> Submission ID : " & submissionid
                        '    lblMessage.Text = "Document Submitted Successfully <br/> Submission ID : " & submissionid
                        lblMessage.Text = "Document Submitted Successfully"
                        '     btnPostEInvoice.Text = "CANCEL E-INVOICE"

                        '  btnPostEInvoice.Enabled = True
                        '  btnPostEInvoice.Forecolor = System.Drawing.Color.Black
                    End If

                        End If


                    'Catch ex As Exception
                    '    lblMessage.Text = "Error in submitting the document" & ex.Message.ToString

                    'End Try

                End If



        End Using
    End Sub

    Protected Function GetSubmission(tokenno As String) As String

        'Dim tokenno As String = ""
        'tokenno = AccessToken

        Dim submissionUid As String = txtSubmissionID.Text
        InsertIntoTblWebEventLog("GetSubmission", ConfigurationManager.AppSettings.[Get]("apiEInvoicedocumentsubmissions"), submissionUid, Session("UserID"))
        ' = "2HNM2FC7PSBH8ZDE8C9RA8EJ10"
        Dim apiGetdocumentURL = ConfigurationManager.AppSettings.[Get]("apiEInvoicedocumentsubmissions")

        '   Dim apiGetdocumentURL As String = "https://preprod-api.myinvois.hasil.gov.my/api/v1.0/documentsubmissions/"

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(tokenNO) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetdocument As String = apiGetdocumentURL + submissionUid
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetdocument).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result
                InsertIntoTblWebEventLog("GetSubmission1", "", response.IsSuccessStatusCode.ToString, Session("UserID").ToString)

                If response.IsSuccessStatusCode Then
                    Dim apiResponse As GetSubmissionDocument = JsonConvert.DeserializeObject(Of GetSubmissionDocument)(responseString)
                    txtSubmissionDate.Text = apiResponse.dateTimeReceived.ToString

                    If apiResponse.documentSummary.Count() > 0 Then
                        For Each Item In apiResponse.documentSummary
                            ' txtUUID.Text = Item.uuid
                            txtLongID.Text = Item.longid
                            Exit For
                        Next
                    End If
                    InsertIntoTblWebEventLog("GetSubmission1", txtLongID.Text, response.IsSuccessStatusCode.ToString, Session("UserID").ToString)

                    Return apiResponse.submissionUid
                Else
                    Return "ERROR"
                End If


            Catch ex As Exception
                Dim result = "error Message -" + ex.Message
                Dim result1 = ""
            End Try
        End Using

    End Function

    Protected Function GetDocument(tokenno As String, UUID As String) As String

        'Dim token = GetToken()
        'Dim tokenno As String = AccessToken

        Dim documentUUID As String = UUID + "/raw"
        '  Dim apiGetdocumentURL As String = "https://preprod-api.myinvois.hasil.gov.my/api/v1.0/documents/"
        ' WriteToFile(documentUUID)

        Dim apiGetdocumentURL As String = ConfigurationManager.AppSettings.[Get]("apiEInvoicedocuments")

        Using client = New HttpClient()

            If Not String.IsNullOrWhiteSpace(tokenno) Then
                client.DefaultRequestHeaders.Clear()
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " & tokenno)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            End If

            Try
                Dim apiGetdocument As String = apiGetdocumentURL + documentUUID
                '    WriteToFile(apiGetdocument)
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                Dim response As HttpResponseMessage = client.GetAsync(apiGetdocument).Result
                Dim responseString = response.Content.ReadAsStringAsync().Result

                InsertIntoTblWebEventLog("GetDocument1", apiGetdocument, response.IsSuccessStatusCode.ToString, Session("UserID").ToString)


                If response.IsSuccessStatusCode Then
                    Dim apiResponse As GetDocumentByID = JsonConvert.DeserializeObject(Of GetDocumentByID)(responseString)
                    txtLongID.Text = apiResponse.longID
                    InsertIntoTblWebEventLog("GetDocument2", txtEInvoiceStatus.Text, apiResponse.status.ToString, Session("UserID").ToString)

                    Return apiResponse.status.ToUpper

                Else
                    Return "FAILURE"
                End If


            Catch ex As Exception
                Dim result = "error Message -" + ex.Message
                InsertIntoTblWebEventLog("GetDocumentError", result, ex.Message.ToString, Session("UserID").ToString)

                Return "FAILURE"
            End Try
        End Using

    End Function

    Private Sub UpdateEInvoiceSubmission(InvoiceNumber As String)
        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
            conn.Open()

            Dim commandUpdateC As MySqlCommand = New MySqlCommand
            commandUpdateC.CommandType = CommandType.Text
            Dim sqlUpdateInvoice As String = "Update tbleinvoiceconsolidated set EI='Y',PostStatus='P',EInvoiceStatus=@EInvoiceStatus,UUID=@UUID,SubmissionID=@SubmissionID,SubmissionDate=@SubmissionDate,SubmissionBy=@SubmissionBy,LongID=@LongID where ConsolidatedInvoiceNo=@invoicenumber"
            InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE", "UPDATEINVOICESUBMISSION1", sqlUpdateInvoice, txtCreatedBy.Text)

            commandUpdateC.CommandText = sqlUpdateInvoice
            commandUpdateC.Parameters.Clear()
            commandUpdateC.Parameters.AddwithValue("@EInvoiceStatus", txtEInvoiceStatus.Text)
            commandUpdateC.Parameters.AddwithValue("@UUID", txtUUID.Text)
            commandUpdateC.Parameters.AddwithValue("@SubmissionID", txtSubmissionID.Text)
            commandUpdateC.Parameters.AddwithValue("@LongID", txtLongID.Text)
            commandUpdateC.Parameters.AddwithValue("@InvoiceNumber", InvoiceNumber)
            commandUpdateC.Parameters.AddwithValue("@SubmissionDate", Convert.ToDateTime(txtSubmissionDate.Text))
            commandUpdateC.Parameters.AddwithValue("@SubmissionBy", txtCreatedBy.Text)
            InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE", "UPDATEINVOICESUBMISSION2", InvoiceNumber, txtCreatedBy.Text)

            commandUpdateC.Connection = conn
            commandUpdateC.ExecuteNonQuery()
            commandUpdateC.Dispose()

            '''''''''''''''
            Dim dtConsolidatedDetail As DataTable = RetrieveConsolidatedDetailTable(conn, InvoiceNumber)
            Dim sqlUpdateInvoice1 As String = ""
            InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE", "dtConsolidatedDetail.Rows.Count", dtConsolidatedDetail.Rows.Count.ToString, txtCreatedBy.Text)

            If dtConsolidatedDetail.Rows.Count > 0 Then
                For i = 0 To dtConsolidatedDetail.Rows.Count - 1
                    Dim commandUpdateInvoice As MySqlCommand = New MySqlCommand
                    commandUpdateInvoice.CommandType = CommandType.Text
                    sqlUpdateInvoice1 = "Update tblsales set EInvoiceStatus=@EInvoiceStatus,UUID=@UUID,SubmissionID=@SubmissionID,SubmissionDate=@SubmissionDate,SubmissionBy=@SubmissionBy,LongID=@LongID where InvoiceNumber=@invoicenumber"
                    InsertIntoTblWebEventLog("CONSOLIDATEDINVOICE", "dtConsolidatedDetail", dtConsolidatedDetail.Rows(i)("ConsolidatedInvoiceNo"), txtCreatedBy.Text)

                    commandUpdateInvoice.CommandText = sqlUpdateInvoice1
                    commandUpdateInvoice.Parameters.Clear()
                    commandUpdateInvoice.Parameters.AddwithValue("@EInvoiceStatus", "CONSOLIDATED")
                    commandUpdateInvoice.Parameters.AddwithValue("@UUID", txtUUID.Text)
                    commandUpdateInvoice.Parameters.AddwithValue("@SubmissionID", txtSubmissionID.Text)
                    commandUpdateInvoice.Parameters.AddwithValue("@LongID", txtLongID.Text)
                    commandUpdateInvoice.Parameters.AddwithValue("@InvoiceNumber", dtConsolidatedDetail.Rows(i)("InvoiceNo"))
                    commandUpdateInvoice.Parameters.AddwithValue("@SubmissionDate", Convert.ToDateTime(txtSubmissionDate.Text))
                    commandUpdateInvoice.Parameters.AddwithValue("@SubmissionBy", txtCreatedBy.Text)
                    '   commandUpdateInvoice.Parameters.AddwithValue("@ConsolidatedInvoiceNo", InvoiceNumber)
                    '    InsertIntoTblWebEventLog("INVOICE", "UPDATEINVOICESUBMISSION2", InvoiceNumber, txtCreatedBy.Text)

                    commandUpdateInvoice.Connection = conn
                    commandUpdateInvoice.ExecuteNonQuery()
                    commandUpdateInvoice.Dispose()
                Next
            End If

            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            InsertIntoTblWebEventLog("INVOICE", "UPDATEINVOICESUBMISSION", ex.Message.ToString, txtCreatedBy.Text)

        End Try
    End Sub

    Public Sub InsertIntoTblEInvoiceEventLog(events As String, errorMsg As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblEInvoiceEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", txtInvoiceNo.Text)
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", Session("UserID"))
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

    Private Function RetrieveCompanyInfoTable(conn As MySqlConnection) As DataTable
        Try
            'Dim Command As MySqlCommand = New MySqlCommand

            'Command.CommandType = CommandType.Text
            'Command.CommandText = "SELECT *,AccountName as CompanyName FROM tbllocation where locationid = @location;"
            'Command.Parameters.AddWithValue("@location", txtLocation.Text)
            'Command.Connection = conn

            'Dim dr As MySqlDataReader = Command.ExecuteReader()
            'Dim dt As New DataTable
            'dt.Load(dr)

            'If dt.Rows.Count > 0 Then
            '    Return dt
            'Else
            Dim Command1 As MySqlCommand = New MySqlCommand

            Command1.CommandType = CommandType.Text
            Command1.CommandText = "SELECT * FROM tblcompanyinfo where rcno=1;"
            Command1.Connection = conn

            Dim dr1 As MySqlDataReader = Command1.ExecuteReader()
            Dim dt1 As New DataTable
            dt1.Load(dr1)

            If dt1.Rows.Count > 0 Then
                Return dt1

            End If
            '  End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveCompanyInfoTable", ex.Message.ToString, "", Session("UserID").ToString)

        End Try

    End Function

    Private Function RetrieveEInvoiceMalaysiaCodeTable(conn As MySqlConnection) As DataTable
        Try
            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Command.CommandText = "SELECT * FROM tbleinvoicemalaysiasicode where rcno=1;"
            Command.Connection = conn

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                Return dt

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveEInvoiceMalaysiaCodeTable", ex.Message.ToString, "", Session("UserID").ToString)

        End Try

    End Function


    Private Function RetrieveCustomerTable(conn As MySqlConnection) As DataTable
        Try
            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Dim qry As String = ""
            InsertIntoTblWebEventLog("RetrieveCustomerTable", "ContactType", ddlContactType.Text, Session("UserID").ToString)

            ' If ddlContactType.SelectedIndex = 0 Then
            If ddlContactType.Text = "CORPORATE" Or ddlContactType.Text = "COMPANY" Then

                qry = "SELECT AccountID,Name,Name2,RocNos,TaxIdentificationNo,SalesTaxRegistrationNo,Address1,AddStreet,AddBuilding,"
                qry = qry + "AddState,AddCity,AddCountry,AddPostal,Telephone,Mobile,Email,TaxIdentificationNo,SalesTaxRegistrationNo,RocNos FROM tblcompany where accountid=@accountid;"

                '  ElseIf ddlContactType.SelectedIndex = 1 Then
            ElseIf ddlContactType.Text = "RESIDENTIAL" Or ddlContactType.Text = "PERSON" Then

                qry = "SELECT AccountID,Name,Name2,TaxIdentificationNo,NRIC,SalesTaxRegistrationNo,Address1,AddStreet,AddBuilding,"
                qry = qry + "AddState,AddCity,AddCountry,AddPostal,TelHome as Telephone,TelMobile as Mobile,Email,TaxIdentificationNo,SalesTaxRegistrationNo FROM tblPERSON where accountid=@accountid;"
            End If
            Command.CommandText = qry
            Command.Parameters.AddWithValue("@accountid", txtAccountID.Text)
            Command.Connection = conn
            InsertIntoTblWebEventLog("RetrieveCustomerTable1", txtAccountID.Text, "", Session("UserID").ToString)

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                InsertIntoTblWebEventLog("RetrieveCustomerTable2", dt.Rows.Count.ToString, "", Session("UserID").ToString)

                Return dt
            Else
                InsertIntoTblWebEventLog("RetrieveCustomerTable3", txtAccountID.Text, "", Session("UserID").ToString)

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveCustomerTable", ex.Message.ToString, "", Session("UserID").ToString)

        End Try

    End Function

    Private Function RetrieveConsolidatedTable(conn As MySqlConnection, InvoiceNumber As String) As DataTable
        Try
            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Dim qry As String = ""

            qry = "SELECT *"
            qry = qry + " FROM tbleinvoiceconsolidated where ConsolidatedInvoiceNo=@invoicenumber;"


            Command.CommandText = qry
            Command.Parameters.AddWithValue("@invoicenumber", InvoiceNumber)
            Command.Connection = conn

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                Return dt

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveConsolidatedTable", ex.Message.ToString, "", Session("UserID").ToString)

        End Try


    End Function

    Private Function RetrieveSetUpInfo(conn As MySqlConnection) As DataTable
        Try
            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Command.CommandText = "SELECT ARCurrency FROM tblservicerecordmastersetup where rcno=1;"
            Command.Connection = conn

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                Return dt

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveSetUpInfo", ex.Message.ToString, "", Session("UserID").ToString)

        End Try

    End Function

    Private Function RetrieveConsolidatedDetailTable(conn As MySqlConnection, InvoiceNumber As String) As DataTable
        Try
            Dim Command As MySqlCommand = New MySqlCommand

            Command.CommandType = CommandType.Text
            Dim qry As String = ""

            qry = "SELECT *"
            qry = qry + " FROM tbleinvoiceconsolidateddetail where ConsolidatedInvoiceno=@invoicenumber;"


            Command.CommandText = qry
            Command.Parameters.AddWithValue("@invoicenumber", InvoiceNumber)
            Command.Connection = conn

            Dim dr As MySqlDataReader = Command.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            InsertIntoTblWebEventLog("RetrieveConsolidatedDetailTable", dt.Rows.Count.ToString, "", Session("UserID").ToString)

            If dt.Rows.Count > 0 Then
                Return dt

            End If
        Catch ex As Exception
            InsertIntoTblWebEventLog("RetrieveConsolidatedDetailTable", ex.Message.ToString, "", Session("UserID").ToString)

        End Try
    End Function

    'Private Function RetrieveContractInfo(conn As MySqlConnection, ContractNo As String) As DataTable
    '    Try
    '        Dim Command As MySqlCommand = New MySqlCommand

    '        Command.CommandType = CommandType.Text
    '        Dim qry As String = ""

    '        qry = "SELECT *"
    '        qry = qry + " FROM tblcontract where contractno=@contractno;"


    '        Command.CommandText = qry
    '        Command.Parameters.AddWithValue("@contractno", ContractNo)
    '        Command.Connection = conn

    '        Dim dr As MySqlDataReader = Command.ExecuteReader()
    '        Dim dt As New DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            Return dt

    '        End If
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("RetrieveContractInfo", ex.Message.ToString, "", Session("UserID").ToString)

    '    End Try
    'End Function

  

    Protected Function submitDocument_without_Certificate(tokenno As String)
        InsertIntoTblWebEventLog("SubmitDoc0", "", "", Session("UserID").ToString)

        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        'Dim tokenno As String = ""
        'tokenno = AccessToken
        InsertIntoTblWebEventLog("SubmitDoc0", "", "", Session("UserID").ToString)

        InsertIntoTblWebEventLog("SubmitDoc1", "", "", Session("UserID").ToString)

        Dim DocumentPayload As String = ""
        Dim DocumentHashcode As String = ""
        Dim DocumentFormat As String = ""
        Dim DocumentPayloadbase64 As String = ""
        Dim SubmitDocumentURL = ConfigurationManager.AppSettings.[Get]("apiEInvoicedocumentsubmissions")
        Dim invoiceNumber = txtInvoiceNo.Text '"CI6210502938017"
        '   Dim invoiceNumber = "CI6210502938017"
        InsertIntoTblWebEventLog("SubmitDoc2", SubmitDocumentURL, tokenno, Session("UserID").ToString)
        Try
            Using client = New HttpClient()
                InsertIntoTblWebEventLog("SubmitDoc3", invoiceNumber, tokenno, Session("UserID").ToString)

                If Not String.IsNullOrWhiteSpace(tokenno) Then
                    InsertIntoTblWebEventLog("SubmitDoc4", invoiceNumber, tokenno, Session("UserID").ToString)

                    client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", tokenno)

                    InsertIntoTblWebEventLog("SubmitDoc5", invoiceNumber, tokenno, Session("UserID").ToString)

                    DocumentPayload = AssignJsonvalue_Without_certificate(invoiceNumber)

                    InsertIntoTblWebEventLog("SubmitDoc6", invoiceNumber, DocumentPayload.Length.ToString, Session("UserID").ToString)

                    If Not String.IsNullOrEmpty(DocumentPayload) Then
                        InsertIntoTblWebEventLog("SubmitDoc7", invoiceNumber, tokenno, Session("UserID").ToString)

                        DocumentHashcode = GetSHA256Hash(DocumentPayload)
                        DocumentFormat = "JSON"
                        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(DocumentPayload)
                        DocumentPayloadbase64 = Convert.ToBase64String(byt)

                        InsertIntoTblWebEventLog("SubmitDoc8", invoiceNumber, DocumentHashcode, Session("UserID").ToString)


                        Dim jsonData As New Dictionary(Of String, Object) From {
                                {"documents", New List(Of Object) From {
                                    New Dictionary(Of String, Object) From {
                                        {"format", DocumentFormat},
                                        {"documentHash", DocumentHashcode},
                                        {"codeNumber", invoiceNumber},
                                        {"document", DocumentPayloadbase64}
                                    }
                                }}
                            }

                        InsertIntoTblWebEventLog("SubmitDoc9", invoiceNumber, tokenno, Session("UserID").ToString)


                        Dim jsonContent As String = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData)

                        Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
                        Dim filepath As String = Server.MapPath("~/PDFs/" + Convert.ToString(Session("InvoiceNo") + ".txt"))

                        ' File.WriteAllText(filePath, jsonContent)

                        InsertIntoTblWebEventLog("SubmitDoc10", invoiceNumber, content.ToString, Session("UserID").ToString)
                        InsertIntoTblWebEventLog("SubmitDoc10A", invoiceNumber, jsonContent, Session("UserID").ToString)

                        Try
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                            Dim response As HttpResponseMessage = client.PostAsync(SubmitDocumentURL, content).Result
                            Dim responseString = response.Content.ReadAsStringAsync().Result
                            InsertIntoTblWebEventLog("SubmitDoc11", invoiceNumber, response.IsSuccessStatusCode.ToString, Session("UserID").ToString)
                            '     InsertIntoTblWebEventLog("SubmitDoc11", invoiceNumber, responseString.ToString, Session("UserID").ToString)


                            If response.IsSuccessStatusCode Then
                                InsertIntoTblWebEventLog("SubmitDoc111", invoiceNumber, response.IsSuccessStatusCode.ToString, Session("UserID").ToString)
                                Dim SubmissionResult As New SubmissionResult
                                Thread.Sleep(3000)
                                Try
                                    SubmissionResult = JsonConvert.DeserializeObject(Of SubmissionResult)(responseString)
                                    InsertIntoTblWebEventLog("SubmitDoc112", invoiceNumber, SubmissionResult.submissionUid.Length.ToString, Session("UserID").ToString)

                                    txtSubmissionID.Text = SubmissionResult.submissionUid.ToString


                                Catch ex As Exception
                                    InsertIntoTblWebEventLog("SubmitDocError", invoiceNumber, ex.Message.ToString, Session("UserID").ToString)

                                End Try


                                InsertIntoTblWebEventLog("SubmitDoc12", invoiceNumber, txtSubmissionID.Text, responseString.ToString.Length.ToString)

                                If SubmissionResult.acceptedDocuments.Count() > 0 Then
                                    For Each Item In SubmissionResult.acceptedDocuments
                                        txtUUID.Text = Item.uuid

                                        Exit For
                                    Next
                                End If

                                txtRejectedDocError.Text = ""

                                Dim error1 As String = ""
                                Dim error2 As Int32
                                Dim error3 As Int32
                                If SubmissionResult.rejectedDocuments.Count() > 0 Then
                                    ' InsertIntoTblWebEventLog("SubmitDoc120", invoiceNumber, SubmissionResult.rejectedDocuments.Count.ToString, responseString.ToString.Length.ToString)

                                    For Each Item In SubmissionResult.rejectedDocuments
                                        '    InsertIntoTblWebEventLog("SubmitDoc12", invoiceNumber, txtSubmissionID.Text, responseString.ToString.Length.ToString)

                                        '  responseString.ToString.Split(
                                        '  error1 = responseString.ToString.IndexOf(responseString.ToString.Substring("""propertyPath"":null,""details"":[{""")).ToString
                                        error1 = responseString.ToString.Substring(responseString.ToString.IndexOf("""propertyPath"":null,""details"":[{""") + 57).ToString
                                        InsertIntoTblWebEventLog("SubmitDocR10", invoiceNumber, error1, Item.invoicecodenumber)

                                        error2 = error1.ToString.IndexOf(""",""")
                                        InsertIntoTblWebEventLog("SubmitDocR11", invoiceNumber, error2.ToString, Item.invoicecodenumber)

                                        error3 = responseString.ToString.IndexOf("""propertyPath"":null,""details"":[{""") + 57

                                        InsertIntoTblWebEventLog("SubmitDocR12", invoiceNumber, error3.ToString, Item.invoicecodenumber)

                                        error1 = responseString.ToString.Substring(error3, error2 + 1).ToString

                                        InsertIntoTblWebEventLog("SubmitDocR13", invoiceNumber, error1, Item.invoicecodenumber)

                                        txtRejectedDocError.Text = error1

                                        Exit For
                                    Next
                                End If
                                ' ''Dim AcceptedDocs As AcceptedDocuments = JsonConvert.DeserializeObject(Of AcceptedDocuments)(responseString)
                                ''txtUUID.text = SubmissionResult.acceptedDocuments[0].submissionUid.acceptedDocuments. .UUID.ToString 
                                ' ''txtUUID.Text = AcceptedDocs.uuid.ToString
                                ' ''InsertIntoTblWebEventLog("SubmitDoc13", invoiceNumber, txtUUID.Text, Session("UserID").ToString)

                                txtEInvoiceStatus.Text = "VALID"
                                Return responseString
                            Else
                                txtEInvoiceStatus.Text = "REJECTED"
                                txtSubmissionID.Text = ""
                                txtUUID.Text = ""
                                txtLongID.Text = ""
                                Return responseString
                            End If

                        Catch ex As HttpRequestException

                            Dim result1 = "error message -" + ex.Message
                            Return result1
                        End Try
                    Else
                        Dim result1 = "error message"
                        Return result1
                    End If
                End If
            End Using
        Catch ex As HttpRequestException

            Dim result1 = "error message -" + ex.Message
            Return result1
        End Try
        Return ""
    End Function

    Private Function Get_API_Token() As Task
        Dim clientId As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceclientId")
        Dim clientSecret As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceclientSecret")
        Dim grant_type As String = ConfigurationManager.AppSettings.[Get]("apiEInvoicegrant_type")
        Dim scope As String = ConfigurationManager.AppSettings.[Get]("apiEInvoicescope")
        Dim tokenURL As String = ConfigurationManager.AppSettings.[Get]("apiEInvoiceToken")

        Return Task.Run(Async Function()

                            Dim token As String = ""
                            Using client = New HttpClient()
                                Try

                                    Dim data = {
                                             New KeyValuePair(Of String, String)("client_id", clientId),
                                             New KeyValuePair(Of String, String)("client_secret", clientSecret),
                                             New KeyValuePair(Of String, String)("grant_type", grant_type),
                                             New KeyValuePair(Of String, String)("scope", scope)
                                         }

                                    Dim apiGetToken As String = tokenURL
                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                    Dim response As HttpResponseMessage = client.PostAsync(apiGetToken, New FormUrlEncodedContent(data)).Result
                                    Dim responseString = response.Content.ReadAsStringAsync().Result

                                    InsertIntoTblWebEventLog("GETTOKEN", tokenurl, response.IsSuccessStatusCode.ToString, Session("UserID").ToString)

                                    If response.IsSuccessStatusCode Then
                                        Dim jObject = Newtonsoft.Json.Linq.JObject.Parse(responseString)
                                        AccessToken = jObject("access_token").ToString()
                                    End If
                                Catch ex As Exception

                                    Dim test As String = ""

                                End Try

                            End Using
                        End Function)

    End Function

    Private Async Function GetToken() As Task
        Get_API_Token().Wait()
    End Function

    Private AccessToken As String

    Private schedule As System.Threading.Timer
    Private apitoken As String = ""
    Private token As AuthenticationResult
End Class


Public Class GetDocumentByID
    Public Property submissionUid As String
    Public Property longID As String
    Public Property internalId As String
    Public Property typeName As String
    Public Property typeVersionName As String
    Public Property issuerTin As String
    Public Property issuerName As String
    Public Property receiverId As String
    Public Property receiverName As String
    Public Property dateTimeReceived As String
    Public Property dateTimeValidated As String
    Public Property status As String
    Public Property documentStatusReason As String
    Public Property cancelDateTime As String
    Public Property rejectRequestDateTime As String
    Public Property document As String
    Public Property createdByUserId As String
    Public Property dateTimeIssued As String
    Public Property totalExcludingTax As String
    Public Property totalDiscount As String
    Public Property totalNetAmount As String
    Public Property totalPayableAmount As String

End Class

Public Class GetSubmissionDocument
    Public Property submissionUid As String
    Public Property documentCount As String
    Public Property dateTimeReceived As String
    Public Property overallStatus As String
    Public Property documentSummary As List(Of DocumentSummary)

End Class

Public Class DocumentSummary
    Public Property uuid As String
    Public Property submissionUid As String
    Public Property longId As String
    Public Property internalId As String
    Public Property typeName As String
    Public Property typeVersionName As String
    Public Property issuerTin As String
    Public Property issuerName As String
    Public Property receiverId As String
    Public Property receiverName As String
    Public Property dateTimeIssued As String
    Public Property dateTimeReceived As String
    Public Property dateTimeValidated As String
    Public Property totalPayableAmount As String
    Public Property totalExcludingTax As String
    Public Property totalDiscount As String
    Public Property totalNetAmount As String
    Public Property status As String
    Public Property cancelDateTime As String
    Public Property rejectRequestDateTime As String
    Public Property documentStatusReason As String
    Public Property createdByUserId As String
End Class

Public Class InvoiceWrapper

    <JsonProperty("_D")>
    Public Property _D As String

    <JsonProperty("_A")>
    Public Property _A As String

    <JsonProperty("_B")>
    Public Property _B As String

    <JsonProperty("Invoice")>
    Public Property Invoice As List(Of Invoice)

    <JsonProperty("LastActivityDate")>
    Public Property _LastActDate As DateTime

End Class

Public Class InvoiceWrapper_Without_certificate

    <JsonProperty("_D")>
    Public Property _D As String

    <JsonProperty("_A")>
    Public Property _A As String

    <JsonProperty("_B")>
    Public Property _B As String

    <JsonProperty("Invoice")>
    Public Property Invoice As List(Of Invoice_without_Certificate)

End Class

Public Class Invoice_without_Certificate
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("IssueDate")>
    Public Property IssueDate As List(Of IssueDate)

    <JsonProperty("IssueTime")>
    Public Property IssueTime As List(Of IssueTime)

    <JsonProperty("InvoiceTypeCode")>
    Public Property InvoiceTypeCode As List(Of InvoiceTypeCode)

    <JsonProperty("DocumentCurrencyCode")>
    Public Property DocumentCurrencyCode As List(Of DocumentCurrencyCode)

    <JsonProperty("AccountingSupplierParty")>
    Public Property AccountingSupplierParty As List(Of AccountingSupplierParty)

    <JsonProperty("AccountingCustomerParty")>
    Public Property AccountingCustomerParty As List(Of AccountingCustomerParty)

    <JsonProperty("InvoicePeriod")>
    Public Property InvoicePeriod As List(Of InvoicePeriod)

    <JsonProperty("BillingReference")>
    Public Property BillingReference As List(Of BillingReference)

    <JsonProperty("AdditionalDocumentReference")>
    Public Property AdditionalDocumentReference As List(Of AdditionalDocumentReference)

    <JsonProperty("TaxTotal")>
    Public Property TaxTotal As List(Of TaxTotal)

    <JsonProperty("LegalMonetaryTotal")>
    Public Property LegalMonetaryTotal As List(Of LegalMonetaryTotal)

    <JsonProperty("InvoiceLine")>
    Public Property InvoiceLine As List(Of InvoiceLine)

End Class

Public Class Invoice
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("IssueDate")>
    Public Property IssueDate As List(Of IssueDate)

    <JsonProperty("IssueTime")>
    Public Property IssueTime As List(Of IssueTime)

    <JsonProperty("InvoiceTypeCode")>
    Public Property InvoiceTypeCode As List(Of InvoiceTypeCode)

    <JsonProperty("DocumentCurrencyCode")>
    Public Property DocumentCurrencyCode As List(Of DocumentCurrencyCode)

    <JsonProperty("AccountingSupplierParty")>
    Public Property AccountingSupplierParty As List(Of AccountingSupplierParty)

    <JsonProperty("AccountingCustomerParty")>
    Public Property AccountingCustomerParty As List(Of AccountingCustomerParty)

    <JsonProperty("TaxTotal")>
    Public Property TaxTotal As List(Of TaxTotal)

    <JsonProperty("LegalMonetaryTotal")>
    Public Property LegalMonetaryTotal As List(Of LegalMonetaryTotal)

    <JsonProperty("InvoiceLine")>
    Public Property InvoiceLine As List(Of InvoiceLine)

    <JsonProperty("UBLExtensions")>
    Public Property UBLExtensions As List(Of UBLExtensions)

    <JsonProperty("Signature")>
    Public Property Signature As List(Of Signature)

End Class
Public Class ID
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class IssueDate
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class IssueTime
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class InvoiceTypeCode
    <JsonProperty("_")>
    Public Property Value As String

    <JsonProperty("listVersionID")>
    Public Property ListVersionID As String
End Class

Public Class DocumentCurrencyCode
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class InvoicePeriod
    <JsonProperty("StartDate")>
    Public Property StartDate As List(Of StartDate)

    <JsonProperty("EndDate")>
    Public Property EndDate As List(Of EndDate)

    <JsonProperty("Description")>
    Public Property Description As List(Of Description)
End Class

' Classes for InvoicePeriod sub-properties

Public Class StartDate
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class EndDate
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class Description
    <JsonProperty("_")>
    Public Property Value As String
End Class

Public Class BillingReference
    <JsonProperty("AdditionalDocumentReference")>
    Public Property AdditionalDocumentReference As List(Of AdditionalDocumentReference)
End Class

Public Class AdditionalDocumentReference
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)
End Class

Public Class AccountingSupplierParty
    <JsonProperty("AdditionalAccountID")>
    Public Property AdditionalAccountID As List(Of AdditionalAccountID)

    <JsonProperty("Party")>
    Public Property Party As List(Of Party)
End Class

Public Class AdditionalAccountID
    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("schemeAgencyName")>
    Public Property schemeAgencyName As String
End Class

Public Class Party
    <JsonProperty("IndustryClassificationCode")>
    Public Property IndustryClassificationCode As List(Of IndustryClassificationCode)

    <JsonProperty("PartyIdentification")>
    Public Property PartyIdentification As List(Of PartyIdentification)

    <JsonProperty("PostalAddress")>
    Public Property PostalAddress As List(Of PostalAddress)

    <JsonProperty("PartyLegalEntity")>
    Public Property PartyLegalEntity As List(Of PartyLegalEntity)

    <JsonProperty("Contact")>
    Public Property Contact As List(Of Contact)

End Class

Public Class IndustryClassificationCode
    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("name")>
    Public Property name As String
End Class
Public Class PartyIdentification
    <JsonProperty("ID")>
    Public Property ID As List(Of PartyIdentificationID)

End Class

Public Class PartyIdentificationID
    <JsonProperty("_")>
    Public Property ID As String

    <JsonProperty("schemeID")>
    Public Property schemeID As String


End Class

' PostalAddress Class
Public Class PostalAddress
    <JsonProperty("CityName")>
    Public Property CityName As List(Of CityName)

    <JsonProperty("PostalZone")>
    Public Property PostalZone As List(Of PostalZone)

    <JsonProperty("CountrySubentityCode")>
    Public Property CountrySubentityCode As List(Of CountrySubentityCode)

    <JsonProperty("AddressLine")>
    Public Property AddressLine As List(Of AddressLine)

    <JsonProperty("Country")>
    Public Property Country As List(Of Country)
End Class

' CityName Class
Public Class CityName
    <JsonProperty("_")>
    Public Property value As String
End Class

' PostalZone Class
Public Class PostalZone
    <JsonProperty("_")>
    Public Property value As String
End Class

' CountrySubentityCode Class
Public Class CountrySubentityCode
    <JsonProperty("_")>
    Public Property value As String
End Class

' AddressLine Class
Public Class AddressLine
    <JsonProperty("Line")>
    Public Property Line As List(Of Line)
End Class

' Line Class
Public Class Line
    <JsonProperty("_")>
    Public Property value As String
End Class

' Country Class
Public Class Country
    <JsonProperty("IdentificationCode")>
    Public Property IdentificationCode As List(Of IdentificationCode)
End Class

' IdentificationCode Class
Public Class IdentificationCode
    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("listID")>
    Public Property listID As String

    <JsonProperty("listAgencyID")>
    Public Property listAgencyID As String
End Class

' PartyLegalEntity Class
Public Class PartyLegalEntity
    <JsonProperty("RegistrationName")>
    Public Property RegistrationName As List(Of RegistrationName)
End Class

' RegistrationName Class
Public Class RegistrationName
    <JsonProperty("_")>
    Public Property value As String
End Class

' Contact Class
Public Class Contact
    <JsonProperty("Telephone")>
    Public Property Telephone As List(Of Telephone)

    <JsonProperty("ElectronicMail")>
    Public Property ElectronicMail As List(Of ElectronicMail)
End Class

' Telephone Class
Public Class Telephone
    <JsonProperty("_")>
    Public Property value As String
End Class

' ElectronicMail Class
Public Class ElectronicMail
    <JsonProperty("_")>
    Public Property value As String
End Class

' AccountingCustomerParty Class (new class for customer data)
Public Class AccountingCustomerParty
    <JsonProperty("Party")>
    Public Property Party As List(Of Party)
End Class

' Delivery Class (new class for delivery data)
Public Class Delivery
    <JsonProperty("DeliveryParty")>
    Public Property DeliveryParty As List(Of DeliveryParty)

    <JsonProperty("Shipment")>
    Public Property Shipment As List(Of Shipment)

End Class

' DeliveryParty Class
Public Class DeliveryParty
    <JsonProperty("PartyLegalEntity")>
    Public Property PartyLegalEntity As List(Of PartyLegalEntity)

    <JsonProperty("PostalAddress")>
    Public Property PostalAddress As List(Of PostalAddress)

    <JsonProperty("PartyIdentification")>
    Public Property PartyIdentification As List(Of PartyIdentification)

End Class

Public Class Shipment
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("FreightAllowanceCharge")>
    Public Property FreightAllowanceCharge As List(Of FreightAllowanceCharge)
End Class

' FreightAllowanceCharge Class
Public Class FreightAllowanceCharge
    <JsonProperty("ChargeIndicator")>
    Public Property ChargeIndicator As List(Of ChargeIndicator)

    <JsonProperty("AllowanceChargeReason")>
    Public Property AllowanceChargeReason As List(Of AllowanceChargeReason)

    <JsonProperty("Amount")>
    Public Property Amount As List(Of Amount)
End Class

' ChargeIndicator Class
Public Class ChargeIndicator
    <JsonProperty("_")>
    Public Property value As Boolean
End Class

' AllowanceChargeReason Class
Public Class AllowanceChargeReason
    <JsonProperty("_")>
    Public Property value As String
End Class

' Amount Class
Public Class Amount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' PaymentMeans Class
Public Class PaymentMeans
    <JsonProperty("PaymentMeansCode")>
    Public Property PaymentMeansCode As List(Of PaymentMeansCode)

    <JsonProperty("PayeeFinancialAccount")>
    Public Property PayeeFinancialAccount As List(Of PayeeFinancialAccount)
End Class

' PaymentMeansCode Class
Public Class PaymentMeansCode
    <JsonProperty("_")>
    Public Property value As String
End Class

' PayeeFinancialAccount Class
Public Class PayeeFinancialAccount
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)
End Class

' PaymentTerms Class
Public Class PaymentTerms
    <JsonProperty("Note")>
    Public Property Note As List(Of Note)
End Class

' Note Class
Public Class Note
    <JsonProperty("_")>
    Public Property value As String
End Class

' PrepaidPayment Class
Public Class PrepaidPayment
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("PaidAmount")>
    Public Property PaidAmount As List(Of PaidAmount)

    <JsonProperty("PaidDate")>
    Public Property PaidDate As List(Of PaidDate)

    <JsonProperty("PaidTime")>
    Public Property PaidTime As List(Of PaidTime)
End Class

' PaidAmount Class
Public Class PaidAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' PaidDate Class
Public Class PaidDate
    <JsonProperty("_")>
    Public Property value As String
End Class

' PaidTime Class
Public Class PaidTime
    <JsonProperty("_")>
    Public Property value As String
End Class


' AllowanceCharge Class
Public Class AllowanceCharge

    <JsonProperty("ChargeIndicator")>
    Public Property ChargeIndicator As List(Of ChargeIndicator)

    <JsonProperty("AllowanceChargeReason")>
    Public Property AllowanceChargeReason As List(Of AllowanceChargeReason)

    <JsonProperty("Amount")>
    Public Property Amount As List(Of Amount)

End Class

Public Class InvoiceLineAllowanceCharge

    <JsonProperty("Amount")>
    Public Property Amount As List(Of Amount)

    <JsonProperty("ChargeIndicator")>
    Public Property ChargeIndicator As List(Of ChargeIndicator)

    <JsonProperty("MultiplierFactorNumeric")>
    Public Property MultiplierFactorNumeric As List(Of MultiplierFactorNumeric)

    <JsonProperty("AllowanceChargeReason")>
    Public Property AllowanceChargeReason As List(Of AllowanceChargeReason)

End Class

' TaxTotal Class
Public Class TaxTotal
    <JsonProperty("TaxAmount")>
    Public Property TaxAmount As List(Of TaxAmount)

    <JsonProperty("TaxSubtotal")>
    Public Property TaxSubtotal As List(Of TaxSubtotal)

End Class

' TaxAmount Class
Public Class TaxAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' TaxSubtotal Class
Public Class TaxSubtotal
    <JsonProperty("TaxableAmount")>
    Public Property TaxableAmount As List(Of TaxableAmount)

    <JsonProperty("TaxAmount")>
    Public Property TaxAmount As List(Of TaxAmount)

    <JsonProperty("Percent")>
    Public Property Percent As List(Of Percent)

    <JsonProperty("TaxCategory")>
    Public Property TaxCategory As List(Of TaxCategory)

End Class

' TaxableAmount Class
Public Class TaxableAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' TaxCategory Class
Public Class TaxCategory
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("Percent")>
    Public Property Percent As List(Of Percent)

    <JsonProperty("TaxScheme")>
    Public Property TaxScheme As List(Of TaxScheme)

    <JsonProperty("TaxExemptionReason")>
    Public Property TaxExemptionReason As List(Of TaxExemptionReason)

End Class

' TaxScheme Class
Public Class TaxScheme
    <JsonProperty("ID")>
    Public Property ID As List(Of TaxSchemeID)
End Class

Public Class TaxSchemeID
    <JsonProperty("_")>
    Public Property ID As String

    <JsonProperty("schemeAgencyID")>
    Public Property schemeAgencyID As String

    <JsonProperty("schemeID")>
    Public Property schemeID As String
End Class

' LegalMonetaryTotal Class
Public Class LegalMonetaryTotal
    <JsonProperty("LineExtensionAmount")>
    Public Property LineExtensionAmount As List(Of LineExtensionAmount)

    <JsonProperty("TaxExclusiveAmount")>
    Public Property TaxExclusiveAmount As List(Of TaxExclusiveAmount)

    <JsonProperty("TaxInclusiveAmount")>
    Public Property TaxInclusiveAmount As List(Of TaxInclusiveAmount)

    <JsonProperty("AllowanceTotalAmount")>
    Public Property AllowanceTotalAmount As List(Of AllowanceTotalAmount)

    <JsonProperty("ChargeTotalAmount")>
    Public Property ChargeTotalAmount As List(Of ChargeTotalAmount)

    <JsonProperty("PayableAmount")>
    Public Property PayableAmount As List(Of PayableAmount)

    <JsonProperty("PayableRoundingAmount")>
    Public Property PayableRoundingAmount As List(Of PayableRoundingAmount)
End Class

' LineExtensionAmount Class
Public Class LineExtensionAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' TaxExclusiveAmount Class
Public Class TaxExclusiveAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' TaxInclusiveAmount Class
Public Class TaxInclusiveAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' AllowanceTotalAmount Class
Public Class AllowanceTotalAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' ChargeTotalAmount Class
Public Class ChargeTotalAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' PayableAmount Class
Public Class PayableAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' PayableRoundingAmount Class
Public Class PayableRoundingAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' InvoiceLine Class
Public Class InvoiceLine

    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("InvoicedQuantity")>
    Public Property InvoicedQuantity As List(Of InvoicedQuantity)

    <JsonProperty("LineExtensionAmount")>
    Public Property LineExtensionAmount As List(Of LineExtensionAmount)

    <JsonProperty("TaxTotal")>
    Public Property TaxTotal As List(Of TaxTotal)

    <JsonProperty("Item")>
    Public Property Item As List(Of Item)

    <JsonProperty("ItemPriceExtension")>
    Public Property ItemPriceExtension As List(Of ItemPriceExtension)

    <JsonProperty("Price")>
    Public Property Price As List(Of Price)

End Class

' Item Class
Public Class Item
    <JsonProperty("CommodityClassification")>
    Public Property CommodityClassification As List(Of CommodityClassification)

    <JsonProperty("Description")>
    Public Property Description As List(Of Description)

    <JsonProperty("OriginCountry")>
    Public Property OriginCountry As List(Of OriginCountry)
End Class

' ItemPriceExtension Class
Public Class ItemPriceExtension
    <JsonProperty("Amount")>
    Public Property Amount As List(Of Amount)
End Class

' Price Class
Public Class Price
    <JsonProperty("PriceAmount")>
    Public Property PriceAmount As List(Of PriceAmount)
End Class

' PriceAmount Class
Public Class PriceAmount

    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' CommodityClassification Class
Public Class CommodityClassification

    <JsonProperty("ItemClassificationCode")>
    Public Property ItemClassificationCode As List(Of ItemClassificationCode)
End Class

' ItemClassificationCode Class
Public Class ItemClassificationCode

    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("listID")>
    Public Property listID As String
End Class

' OriginCountry Class
Public Class OriginCountry
    <JsonProperty("IdentificationCode")>
    Public Property IdentificationCode As List(Of IdentificationCodeID)
End Class

' IdentificationCode Class
Public Class IdentificationCodeID
    <JsonProperty("_")>
    Public Property Value As String
End Class

' InvoicedQuantity Class
Public Class InvoicedQuantity

    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("unitCode")>
    Public Property unitCode As String
End Class

' MultiplierFactorNumeric Class
Public Class MultiplierFactorNumeric
    <JsonProperty("_")>
    Public Property value As Int32
End Class

' PerUnitAmount Class
Public Class PerUnitAmount
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("currencyID")>
    Public Property currencyID As String
End Class

' BaseUnitMeasure Class
Public Class BaseUnitMeasure
    <JsonProperty("_")>
    Public Property value As Decimal

    <JsonProperty("unitCode")>
    Public Property unitCode As String
End Class

Public Class Percent
    <JsonProperty("_")>
    Public Property value As Decimal

End Class

Public Class TaxExemptionReason
    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class TaxExchangeRate
    <JsonProperty("SourceCurrencyCode")>
    Public Property SourceCurrencyCode As List(Of SourceCurrencyCode)

    <JsonProperty("TargetCurrencyCode")>
    Public Property TargetCurrencyCode As List(Of TargetCurrencyCode)

    <JsonProperty("CalculationRate")>
    Public Property CalculationRate As List(Of CalculationRate)

End Class

Public Class TargetCurrencyCode
    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class CalculationRate
    <JsonProperty("_")>
    Public Property value As Decimal

End Class

Public Class SourceCurrencyCode
    <JsonProperty("_")>
    Public Property value As String

End Class


'Public Class UBLExtensions
'    <JsonProperty("UBLExtension")>
'    Public Property UBLExtension As List(Of UBLExtension)

'End Class
Public Class Signature
    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("SignatureMethod")>
    Public Property SignatureMethod As List(Of SignatureMethod)

End Class

Public Class SignatureMethod

    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class UBLExtensions
    <JsonProperty("UBLExtension")>
    Public Property UBLExtension As List(Of UBLExtension)

End Class
Public Class UBLExtension
    <JsonProperty("ExtensionURI")>
    Public Property ExtensionURI As List(Of ExtensionURI)

    <JsonProperty("ExtensionContent")>
    Public Property ExtensionContent As List(Of ExtensionContent)

End Class

Public Class ExtensionURI
    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class ExtensionContent

    <JsonProperty("UBLDocumentSignatures")>
    Public Property UBLDocumentSignatures As List(Of UBLDocumentSignatures)

End Class
Public Class UBLDocumentSignatures

    <JsonProperty("SignatureInformation")>
    Public Property SignatureInformation As List(Of SignatureInformation)

End Class

Public Class SignatureInformation

    <JsonProperty("ID")>
    Public Property ID As List(Of ID)

    <JsonProperty("ReferencedSignatureID")>
    Public Property ReferencedSignatureID As List(Of ReferencedSignatureID)

    <JsonProperty("Signature")>
    Public Property SignatureInformation_Signature As List(Of SignatureInformation_Signature)

End Class
Public Class SignatureInformation_Signature

    <JsonProperty("Id")>
    Public Property ID As String

    <JsonProperty("Object")>
    Public Property SignatureObject As List(Of SignatureObject)

    <JsonProperty("KeyInfo")>
    Public Property KeyInfo As List(Of KeyInfo)

    <JsonProperty("SignatureValue")>
    Public Property SignatureValue As List(Of SignatureValue)
    <JsonProperty("SignedInfo")>
    Public Property SignedInfo As List(Of SignedInfo)


End Class
Public Class SignatureValue

    <JsonProperty("_")>
    Public Property value As String

End Class
Public Class SignedInfo

    <JsonProperty("SignatureMethod")>
    Public Property SignedInfoSignatureMethod As List(Of SignedInfoSignatureMethod)

    <JsonProperty("Reference")>
    Public Property Reference As List(Of Reference)

End Class

Public Class Reference

    <JsonProperty("Type")>
    Public Property Type As String

    <JsonProperty("URI")>
    Public Property URI As String

    <JsonProperty("DigestMethod")>
    Public Property DigestMethod As List(Of DigestMethod)
    <JsonProperty("DigestValue")>
    Public Property DigestValue As List(Of DigestValue)

End Class
Public Class DigestMethod

    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("Algorithm")>
    Public Property Algorithm As String
End Class
Public Class SignedInfoSignatureMethod

    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("Algorithm")>
    Public Property Algorithm As String


End Class
Public Class KeyInfo

    <JsonProperty("X509Data")>
    Public Property X509Data As List(Of X509Data)

End Class

Public Class X509Data

    <JsonProperty("X509Certificate")>
    Public Property X509Certificate As List(Of X509Certificate)

    <JsonProperty("X509SubjectName")>
    Public Property X509SubjectName As List(Of X509SubjectName)

    <JsonProperty("X509IssuerSerial")>
    Public Property X509IssuerSerial As List(Of X509IssuerSerial)

End Class

Public Class X509Certificate

    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class X509SubjectName

    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class X509IssuerSerial

    <JsonProperty("X509IssuerName")>
    Public Property X509IssuerName As List(Of X509IssuerName)

    <JsonProperty("X509SerialNumber")>
    Public Property X509SerialNumber As List(Of X509SerialNumber)

End Class

Public Class X509IssuerName

    <JsonProperty("_")>
    Public Property value As String

End Class

Public Class SignatureObject

    <JsonProperty("QualifyingProperties")>
    Public Property QualifyingProperties As List(Of QualifyingProperties)

End Class

Public Class QualifyingProperties

    <JsonProperty("Target")>
    Public Property Target As String

    <JsonProperty("SignedProperties")>
    Public Property SignedProperties As List(Of SignedProperties)

End Class

Public Class SignedProperties

    <JsonProperty("Id")>
    Public Property ID As String

    <JsonProperty("SignedSignatureProperties")>
    Public Property SignedSignatureProperties As List(Of SignedSignatureProperties)

End Class
Public Class SignedSignatureProperties

    <JsonProperty("SigningTime")>
    Public Property SigningTime As List(Of SigningTime)

    <JsonProperty("SigningCertificate")>
    Public Property SigningCertificate As List(Of SigningCertificate)

End Class

Public Class SigningCertificate
    <JsonProperty("Cert")>
    Public Property Cert As List(Of Cert)
End Class

Public Class Cert
    <JsonProperty("CertDigest")>
    Public Property CertDigest As List(Of CertDigest)

    <JsonProperty("IssuerSerial")>
    Public Property IssuerSerial As List(Of IssuerSerial)

End Class
Public Class IssuerSerial

    <JsonProperty("X509IssuerName")>
    Public Property X509IssuerName As List(Of X509IssuerName)

    <JsonProperty("X509SerialNumber")>
    Public Property X509SerialNumber As List(Of X509SerialNumber)

End Class

Public Class CertDigest

    <JsonProperty("DigestMethod")>
    Public Property DigestMethod As List(Of DigestMethod)

    <JsonProperty("DigestValue")>
    Public Property DigestValue As List(Of DigestValue)

End Class

Public Class X509SerialNumber

    <JsonProperty("_")>
    Public Property value As String

End Class
'Public Class DigestMethod

'    <JsonProperty("_")>
'    Public Property value As String

'End Class
Public Class DigestValue

    <JsonProperty("_")>
    Public Property value As String

End Class
Public Class SigningTime

    <JsonProperty("_")>
    Public Property value As String

    <JsonProperty("Algorithm")>
    Public Property Algorithm As String

End Class
Public Class ReferencedSignatureID

    <JsonProperty("_")>
    Public Property value As String

End Class


Public Class SubmissionResult
    Public Property submissionUid As String
    Public Property acceptedDocuments As List(Of AcceptedDocuments)
    Public Property rejectedDocuments As List(Of RejectedDocuments)
End Class

Public Class AcceptedDocuments
    Public Property uuid As String
    Public Property invoiceCodeNumber As String

End Class
Public Class RejectedDocuments
    ' Public Property error As List(of Error)
    Public Property invoiceCodeNumber As String

End Class

'Public Class Error
'    <JsonProperty("-")>
'    Public Property error1 As String
'End Class

Public Class CancelDocument
    Public Property uuid As String
    Public Property status As String
    Public Property Reason As String
End Class

